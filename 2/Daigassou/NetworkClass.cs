using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Daigassou.Utils;
using Machina;
using Machina.FFXIV;
using NetFwTypeLib;

namespace Daigassou;

public class NetworkClass
{
	internal class ParseResult
	{
		public FFXIVMessageHeader header;

		public byte[] data;
	}

	internal class TimedNote
	{
		public DateTime StartTime;

		public int Note;

		public override string ToString()
		{
			return string.Format("StartTime: " + StartTime.ToString("HH:mm:ss.fff") + ", Note: " + Note.ToString("X2"));
		}
	}

	private DateTime lastSentTime;

	public bool _shouldStop = false;

	public event EventHandler<PlayEvent> Play;

	private void MessageReceived(long epoch, byte[] message, int set, ConnectionType connectionType)
	{
		ParseResult parseResult = Parse(message);
		if (parseResult.header.MessageType == ParameterController.countDownPacket)
		{
			byte b = parseResult.data[36];
			uint num = BitConverter.ToUInt32(parseResult.data, 24);
			byte[] array = new byte[18];
			Array.Copy(parseResult.data, 41, array, 0, 18);
			string text = Encoding.UTF8.GetString(array) ?? "";
			this.Play?.Invoke(this, new PlayEvent(0, Convert.ToInt32(num + b), text));
		}
		if (parseResult.header.MessageType == ParameterController.partyStopPacket || parseResult.header.MessageType == ParameterController.ensembleStopPacket)
		{
			this.Play?.Invoke(this, new PlayEvent(1, 0, " "));
		}
	}

	private void MessageSent(long epoch, byte[] message, int set, ConnectionType connectionType)
	{
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		ParseResult parseResult = Parse(message);
		PacketEntry packetEntry = new PacketEntry
		{
			IsVisible = true,
			ActorControl = -1,
			Data = message,
			Message = parseResult.header.MessageType.ToString("X4"),
			Direction = "C",
			Category = set.ToString(),
			Size = parseResult.header.MessageLength.ToString(),
			Set = set,
			RouteID = parseResult.header.RouteID.ToString(),
			PacketUnixTime = parseResult.header.Seconds,
			Connection = connectionType
		};
		if (parseResult.header.MessageType == 647)
		{
			byte b = parseResult.data[32];
			byte[] destinationArray = new byte[b];
			Array.Copy(parseResult.data, 33, destinationArray, 0, b);
		}
	}

	private static ParseResult Parse(byte[] data)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		GCHandle gCHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
		FFXIVMessageHeader header = (FFXIVMessageHeader)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(FFXIVMessageHeader));
		gCHandle.Free();
		ParseResult parseResult = new ParseResult();
		parseResult.header = header;
		parseResult.data = data;
		return parseResult;
	}

	public void Run(uint processID)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		Log.overlayLog($"开始检测进程：{processID}");
		FFXIVNetworkMonitor val = new FFXIVNetworkMonitor();
		RegisterToFirewall();
		val.MonitorType = (NetworkMonitorType)1;
		val.MessageReceived = new MessageReceivedDelegate(MessageReceived);
		val.MessageSent = new MessageSentDelegate(MessageSent);
		val.ProcessID = processID;
		val.Start();
		while (!_shouldStop)
		{
			Thread.Sleep(1);
		}
		Console.WriteLine("MachinaCaptureWorker: Terminating");
		val.Stop();
	}

	private void RegisterToFirewall()
	{
		try
		{
			Process process = new Process();
			string fileName = Process.GetCurrentProcess().MainModule.FileName;
			process.StartInfo.FileName = "cmd.exe";
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardInput = true;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			process.StartInfo.CreateNoWindow = true;
			process.Start();
			process.StandardInput.WriteLine("netsh advfirewall firewall add rule name=\"WinClient\" dir=in program=\"" + fileName + "\" action=allow localip=any remoteip=any security=notrequired description=DFAssist");
			process.StandardInput.WriteLine("exit");
			INetFwMgr instance = GetInstance<INetFwMgr>("HNetCfg.FwMgr");
			INetFwAuthorizedApplications authorizedApplications = instance.LocalPolicy.CurrentProfile.AuthorizedApplications;
			bool flag = false;
			foreach (object item in authorizedApplications)
			{
				if (item is INetFwAuthorizedApplication netFwAuthorizedApplication && netFwAuthorizedApplication.ProcessImageFileName == fileName && netFwAuthorizedApplication.Enabled)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				INetFwAuthorizedApplication instance2 = GetInstance<INetFwAuthorizedApplication>("HNetCfg.FwAuthorizedApplication");
				instance2.Enabled = true;
				instance2.Name = "Daigassou";
				instance2.ProcessImageFileName = fileName;
				instance2.Scope = NET_FW_SCOPE_.NET_FW_SCOPE_ALL;
				authorizedApplications.Add(instance2);
			}
			Log.S("l-firewall-registered");
		}
		catch (Exception e)
		{
			Log.Ex(e, "l-firewall-error");
		}
	}

	private T GetInstance<T>(string typeName)
	{
		return (T)Activator.CreateInstance(Type.GetTypeFromProgID(typeName));
	}
}
