using System;
using System.Drawing;
using System.Text;
using RainbowMage.OverlayPlugin;

namespace Daigassou.Utils;

public static class Log
{
	private static DateTime lastTime;

	public static LabelOverlayConfig log;

	public static bool isBeta;

	private static LogForm logform { get; set; }

	public static void overlayLog(string text)
	{
		if (log != null)
		{
			log.Text = string.Format("[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " + text);
			Console.WriteLine(string.Format("[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " + text));
		}
	}

	public static void overlayProcess(string process)
	{
		if (log != null)
		{
			log.Process = process;
		}
	}

	public static void Debug(string text)
	{
		Console.WriteLine(text);
	}

	private static void output(Color c, string s)
	{
		logform?.Invoke((Action)delegate
		{
			logform.LogTextBox.SelectionColor = c;
			logform.LogTextBox.AppendText(s);
			logform.LogTextBox.SelectionColor = logform.LogTextBox.ForeColor;
		});
	}

	public static void I(string text)
	{
		Debug(text);
	}

	public static void E(string text)
	{
		Debug(text);
	}

	public static void Ex(Exception e, string text)
	{
		Debug(text);
	}

	public static void S(string text)
	{
		Debug(text);
	}

	public static void B(byte[] text, bool isoffset)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int num = 0;
		for (int i = 0; i < text.Length; i++)
		{
			if (text[i] == byte.MaxValue)
			{
				num += Convert.ToInt32(text[i + 1]);
			}
		}
		if (isoffset)
		{
			if ((DateTime.Now - lastTime).Milliseconds - num > 150)
			{
				Console.WriteLine("???");
			}
			stringBuilder.Append(string.Format("{0}   {1} Bytes {2} ms Interval {3} ms", DateTime.Now.ToString("O"), text.Length, num, (DateTime.Now - lastTime).Milliseconds));
			stringBuilder.AppendLine();
			lastTime = DateTime.Now;
		}
		else
		{
			stringBuilder.Append(string.Format("{0}   {1} Bytes", DateTime.Now.ToString("O"), text.Length));
		}
		for (int j = 0; j < text.Length; j++)
		{
			stringBuilder.Append(' ');
			stringBuilder.Append(text[j].ToString("X2"));
		}
		Debug(stringBuilder.ToString());
	}
}
