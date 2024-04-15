using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Daigassou.Utils;

internal class ParameterController
{
	private static ParameterController Parameter;

	private static readonly object locker = new object();

	public volatile int InternalOffset;

	public volatile int Offset;

	private DateTime lastSentTime;

	private Timer offsetTimer;

	public static uint countDownPacket = 428u;

	public static uint ensembleStopPacket = 345u;

	public static uint partyStopPacket = 65535u;

	public static uint ensembleStartPacket = 65535u;

	public static uint ensemblePacket = 65535u;

	public Queue<TimedNote> NetSyncQueue { get; set; }

	public Queue<TimedNote> LocalPlayQueue { get; set; }

	public int Pitch { get; set; }

	public int Speed { get; set; }

	public bool NeedSync { get; set; } = true;


	public bool isEnsembleSync { get; set; } = false;


	private ParameterController()
	{
		NetSyncQueue = new Queue<TimedNote>();
		LocalPlayQueue = new Queue<TimedNote>();
	}

	private void OffsetTimer_Elapsed(object sender, ElapsedEventArgs e)
	{
		Offset = InternalOffset;
		Console.WriteLine($"Clear offset.now is {Offset}");
		lock (locker)
		{
			if ((DateTime.Now - lastSentTime).TotalMilliseconds > 1200.0)
			{
				NeedSync = true;
			}
		}
	}

	public static ParameterController GetInstance()
	{
		lock (locker)
		{
			if (Parameter == null)
			{
				Parameter = new ParameterController();
			}
		}
		return Parameter;
	}

	public void CheckSyncStatus()
	{
		lock (locker)
		{
			if ((DateTime.Now - lastSentTime).TotalMilliseconds > 1200.0)
			{
				NeedSync = true;
			}
		}
	}

	internal void AnalyzeNotes(byte[] msg)
	{
		lock (locker)
		{
			Console.Write(DateTime.Now.ToString("hh:mm:ss.fff :"));
			foreach (byte b in msg)
			{
				Console.Write(b.ToString("X2") + " ");
			}
			Console.WriteLine();
			offsetTimer.Enabled = false;
			lastSentTime = DateTime.Now;
			DateTime dateTime = lastSentTime + new TimeSpan(0, 0, 0, 0, -500);
			int num = 0;
			for (int j = 0; j < msg.Length; j++)
			{
				if (msg[j] != byte.MaxValue && msg[j] != 254)
				{
					num = j * 50;
					LocalPlayQueue.Enqueue(new TimedNote
					{
						Note = msg[j],
						StartTime = dateTime + new TimeSpan(0, 0, 0, 0, num)
					});
				}
			}
			offsetTimer.Enabled = true;
			while (LocalPlayQueue.Count > 0)
			{
				TimedNote timedNote = LocalPlayQueue.Dequeue();
				while (NetSyncQueue.Any())
				{
					TimedNote timedNote2 = NetSyncQueue.Dequeue();
					if (timedNote.Note == timedNote2.Note)
					{
						TimeSpan timeSpan = timedNote.StartTime - timedNote2.StartTime;
						if (timeSpan.TotalMilliseconds > 50.0)
						{
							Console.WriteLine(timedNote.ToString() + $"Offset={timeSpan.TotalMilliseconds}");
						}
						break;
					}
				}
			}
		}
	}

	private void OffsetSync(int packetTime)
	{
		if (NeedSync)
		{
			Offset = InternalOffset + (500 - packetTime);
			Console.WriteLine($"InternalOffset is sync to {Offset}");
			Log.overlayLog($"网络同步:内部延迟同步至{Offset}毫秒");
			NeedSync = false;
		}
	}
}
