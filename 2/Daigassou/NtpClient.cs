using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Daigassou;

internal class NtpClient
{
	private readonly string _server;

	public NtpClient(string server)
	{
		if (string.IsNullOrEmpty(server))
		{
			throw new ArgumentException("Must be non-empty", "server");
		}
		_server = server;
	}

	public static TimeSpan Offset(string server = "pool.ntp.org")
	{
		double errorMilliseconds;
		return new NtpClient(server).GetOffset(out errorMilliseconds);
	}

	public TimeSpan GetOffset(out double errorMilliseconds)
	{
		TimeSpan timeSpan = new TimeSpan(0L);
		IPAddress[] addressList = Dns.GetHostEntry(_server).AddressList;
		IPEndPoint remoteEP = new IPEndPoint(addressList[0], 123);
		Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
		{
			ReceiveTimeout = 3000
		};
		errorMilliseconds = 0.0;
		try
		{
			byte[] array = new byte[48];
			array[0] = 27;
			socket.Connect(remoteEP);
			DateTime utcNow = DateTime.UtcNow;
			socket.Send(array);
			socket.Receive(array);
			DateTime utcNow2 = DateTime.UtcNow;
			socket.Close();
			byte[] array2 = new byte[8];
			Array.Copy(array, 32, array2, 0, 8);
			DateTime dateTime = byteToTime(array2);
			Array.Copy(array, 40, array2, 0, 8);
			DateTime dateTime2 = byteToTime(array2);
			ulong num = ((ulong)array[10] << 8) | array[11];
			errorMilliseconds = num * 1000 / 65536;
			timeSpan = utcNow2 - dateTime2 - (dateTime - utcNow);
			CommonUtilities.WriteLog("localTransmitTime=" + utcNow.ToString("O") + "\r\n localReceiveTime = " + utcNow2.ToString("O") + "\r\n serverReceiveTime=" + dateTime.ToString("O") + "\r\nserverTransmitTime=" + dateTime2.ToString("O") + "\r\n" + $"offset={timeSpan.TotalMilliseconds}ms\r\n" + $"error={errorMilliseconds}ms");
		}
		catch (Exception ex)
		{
			CommonUtilities.WriteLog(ex.Message);
			MessageBox.Show("同步失败\r\n" + ex.Message);
			throw ex;
		}
		return timeSpan;
	}

	private DateTime byteToTime(byte[] timeBytes)
	{
		ulong num = ((ulong)timeBytes[0] << 24) | ((ulong)timeBytes[1] << 16) | ((ulong)timeBytes[2] << 8) | timeBytes[3];
		ulong num2 = ((ulong)timeBytes[4] << 24) | ((ulong)timeBytes[5] << 16) | ((ulong)timeBytes[6] << 8) | timeBytes[7];
		ulong num3 = num * 1000 + num2 * 1000 / 4294967296L;
		return new DateTime(1900, 1, 1).AddMilliseconds((long)num3);
	}
}
