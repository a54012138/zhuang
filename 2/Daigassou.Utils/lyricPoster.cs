using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace Daigassou.Utils;

public static class lyricPoster
{
	public class lyricLine
	{
		public string text = "";

		public int startTimeMs = 0;

		public lyricLine(string _time, string _text)
		{
			text = _text;
			Match match = Regex.Match(_time, "(?<min>\\d+):(?<sec>\\d+).(?<hm>\\d+)");
			startTimeMs += Convert.ToInt32(match.Groups["min"].Value) * 60000 + Convert.ToInt32(match.Groups["sec"].Value) * 1000 + Convert.ToInt32(match.Groups["hm"].Value) * 10;
		}
	}

	public static uint port = 2345u;

	public static string suffix = "/s";

	public static string url = $"http://127.0.0.1:{port}/command";

	public static Thread LrcThread;

	public static bool IsLrcEnable = false;

	internal static Queue<lyricLine> AnalyzeLrc(string path)
	{
		Queue<lyricLine> queue = new Queue<lyricLine>();
		string input = File.ReadAllText(path);
		Regex regex = new Regex("\\[(?<time>.*)\\](?<lyric>.*)\\r\\n");
		MatchCollection matchCollection = regex.Matches(input);
		foreach (Match item in matchCollection)
		{
			queue.Enqueue(new lyricLine(item.Groups["time"].Value, item.Groups["lyric"].Value));
		}
		return queue;
	}

	public static void PostJson(string text)
	{
		try
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://127.0.0.1:{port}/command");
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";
			using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				streamWriter.Write("/s ♪ " + text + " ♪");
				streamWriter.Flush();
				streamWriter.Close();
			}
			httpWebRequest.GetResponse();
		}
		catch (Exception)
		{
		}
	}

	public static void LrcStart(string path, int startOffset)
	{
		try
		{
			if (File.Exists(path) && IsLrcEnable)
			{
				Queue<lyricLine> lyric = AnalyzeLrc(path);
				LrcThread = new Thread((ThreadStart)delegate
				{
					RunningLrc(lyric, startOffset);
				});
				Log.overlayLog("【歌词播放】歌词导入成功，开始播放");
				LrcThread.Start();
			}
			else
			{
				Log.overlayLog("【歌词播放】找不到lrc文件");
			}
		}
		catch (Exception)
		{
			Log.overlayLog("【歌词播放】解析出错");
			throw;
		}
	}

	public static void LrcStop()
	{
		if (LrcThread != null)
		{
			LrcThread.Abort();
		}
	}

	public static void RunningLrc(Queue<lyricLine> lyric, int startOffset)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		while (lyric.Any())
		{
			lyricLine lyricLine = lyric.Dequeue();
			double num = (double)startOffset + (double)lyricLine.startTimeMs;
			while ((double)ParameterController.GetInstance().Offset + num > (double)stopwatch.ElapsedMilliseconds)
			{
				Thread.Sleep(1);
			}
			PostJson(lyricLine.text);
		}
	}
}
