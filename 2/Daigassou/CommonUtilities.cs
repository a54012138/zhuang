using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Daigassou;

internal class CommonUtilities
{
	public class versionObject
	{
		public bool isForceUpdate { get; set; }

		public bool isRefuseToUse { get; set; }

		public string Version { get; set; }

		public string Description { get; set; }

		public uint countDownPacket { get; set; }

		public uint ensembleStopPacket { get; set; }

		public uint partyStopPacket { get; set; }

		public uint ensembleStartPacket { get; set; }

		public uint ensemblePacket { get; set; }
	}

	public struct SystemTime
	{
		public ushort wYear;

		public ushort wMonth;

		public ushort wDayOfWeek;

		public ushort wDay;

		public ushort wHour;

		public ushort wMinute;

		public ushort wSecond;

		public ushort wMiliseconds;
	}

	public class SetSystemDateTime
	{
		[DllImport("Kernel32.dll")]
		public static extern bool SetLocalTime(ref SystemTime sysTime);

		public static bool SetLocalTimeByStr(DateTime dt)
		{
			bool result = false;
			SystemTime sysTime = default(SystemTime);
			sysTime.wYear = Convert.ToUInt16(dt.Year);
			sysTime.wMonth = Convert.ToUInt16(dt.Month);
			sysTime.wDay = Convert.ToUInt16(dt.Day);
			sysTime.wHour = Convert.ToUInt16(dt.Hour);
			sysTime.wMinute = Convert.ToUInt16(dt.Minute);
			sysTime.wSecond = Convert.ToUInt16(dt.Second);
			sysTime.wMiliseconds = Convert.ToUInt16(dt.Millisecond);
			try
			{
				result = SetLocalTime(ref sysTime);
			}
			catch (Exception ex)
			{
				WriteLog("Failed to set system date time with exception " + ex.Message);
			}
			return result;
		}
	}

	public static async void GetLatestVersion()
	{
		new WebClient();
		try
		{
			string nowVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			try
			{
				if (nowVersion != null && MessageBox.Show(new Form
				{
					TopMost = true
				}, "检测到新版本已经发布，点击确定下载最新版哦！\r\n 当然就算你点了取消，这个提示每次打开还会出现的哦！下载错误可以去NGA发布帖哦！bbs.nga.cn/read.php?tid=18790669 \r\n新版本更新内容：", "哇——更新啦！", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
				{
					Process.Start("http://blog.ffxiv.cat/index.php/download/");
				}
			}
			catch (Exception ex)
			{
				Exception e = ex;
				Console.WriteLine(e);
				throw;
			}
		}
		catch (Exception)
		{
		}
	}

	public static void WriteLog(string msg)
	{
		Console.WriteLine(DateTime.Now.ToString("O") + "\t\t\t" + msg);
	}
}
