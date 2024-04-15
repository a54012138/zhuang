using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Daigassou.Input_Midi;
using Daigassou.Properties;
using Daigassou.Utils;

namespace Daigassou;

public class KeyController
{
	public delegate void stopped();

	private static Dictionary<int, Keys> _keymap = new Dictionary<int, Keys>();

	private readonly BackgroundKey bkKeyController = new BackgroundKey();

	private readonly object keyLock = new object();

	public volatile bool isBackGroundKey = false;

	public volatile bool isPlayingFlag = false;

	public volatile bool isRunningFlag = false;

	public stopped stopHandler;

	public int pauseOffset = 0;

	private Keys _lastCtrlKey;

	[DllImport("User32.dll")]
	public static extern void keybd_event(Keys bVk, byte bScan, int dwFlags, int dwExtraInfo);

	[DllImport("user32.dll")]
	private static extern uint MapVirtualKey(uint uCode, uint uMapType);

	public void KeyboardPress(int pitch)
	{
		if (pitch <= 84 && pitch >= 48)
		{
			if (Settings.Default.IsEightKeyLayout)
			{
				KeyboardPress(KeyBinding.GetNoteToCtrlKey(pitch), KeyBinding.GetNoteToKey(pitch));
			}
			else if (isBackGroundKey)
			{
				bkKeyController.BackgroundKeyPress(KeyBinding.GetNoteToKey(pitch));
			}
			else
			{
				KeyboardPress(KeyBinding.GetNoteToKey(pitch));
			}
			ParameterController.GetInstance().NetSyncQueue.Enqueue(new TimedNote
			{
				Note = pitch - 24,
				StartTime = DateTime.Now
			});
		}
	}

	public void KeyboardPress(Keys ctrKeys, Keys viKeys)
	{
		if (_lastCtrlKey != ctrKeys)
		{
			keybd_event(_lastCtrlKey, (byte)MapVirtualKey((uint)_lastCtrlKey, 0u), 2, 0);
			Thread.Sleep(8);
			if (ctrKeys != 0)
			{
				keybd_event(ctrKeys, (byte)MapVirtualKey((uint)ctrKeys, 0u), 0, 0);
				Thread.Sleep(8);
			}
		}
		keybd_event(viKeys, (byte)MapVirtualKey((uint)viKeys, 0u), 0, 0);
		_lastCtrlKey = ctrKeys;
	}

	private void KeyboardPress(Keys viKeys)
	{
		lock (keyLock)
		{
			keybd_event(viKeys, (byte)MapVirtualKey((uint)viKeys, 0u), 0, 0);
		}
	}

	public void KeyboardRelease(int pitch)
	{
		lock (keyLock)
		{
			if (pitch <= 84 && pitch >= 48)
			{
				if (Settings.Default.IsEightKeyLayout)
				{
					KeyboardRelease(KeyBinding.GetNoteToCtrlKey(pitch), KeyBinding.GetNoteToKey(pitch));
				}
				else if (isBackGroundKey)
				{
					bkKeyController.BackgroundKeyRelease(KeyBinding.GetNoteToKey(pitch));
				}
				else
				{
					KeyboardRelease(KeyBinding.GetNoteToKey(pitch));
				}
			}
		}
	}

	public void KeyboardRelease(Keys ctrKeys, Keys viKeys)
	{
		keybd_event(viKeys, (byte)MapVirtualKey((uint)viKeys, 0u), 2, 0);
		Thread.Sleep(5);
	}

	public void KeyboardRelease(Keys viKeys)
	{
		keybd_event(viKeys, (byte)MapVirtualKey((uint)viKeys, 0u), 2, 0);
	}

	public void InitBackGroundKey(IntPtr pid)
	{
		bkKeyController.Init(pid);
	}

	public void ResetKey()
	{
		isPlayingFlag = false;
		isRunningFlag = false;
		pauseOffset = 0;
		KeyboardRelease(Keys.ControlKey);
		Thread.Sleep(1);
		KeyboardRelease(Keys.ShiftKey);
		Thread.Sleep(1);
		KeyboardRelease(Keys.Menu);
		Thread.Sleep(1);
		for (int i = 48; i < 84; i++)
		{
			KeyboardRelease(KeyBinding.GetNoteToKey(i));
			Thread.Sleep(1);
		}
		ParameterController.GetInstance().Pitch = 0;
	}

	public void UpdateKeyMap()
	{
		for (int i = 48; i < 84; i++)
		{
			_keymap[i] = KeyBinding.GetNoteToKey(i);
		}
	}

	public void KeyPlayBack(Queue<KeyPlayList> keyQueue, double speed, CancellationToken token, int startOffset)
	{
		isRunningFlag = true;
		UpdateKeyMap();
		double? num = keyQueue.LastOrDefault()?.TimeMs;
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		while (keyQueue.Any())
		{
			KeyPlayList keyPlayList = keyQueue.Dequeue();
			double num2 = (double)startOffset + keyPlayList.TimeMs * speed;
			while (!isPlayingFlag || num2 + (double)ParameterController.GetInstance().Offset + (double)pauseOffset > (double)stopwatch.ElapsedMilliseconds)
			{
				Thread.Sleep(1);
			}
			if (keyPlayList.Ev == KeyPlayList.NoteEvent.NoteOn)
			{
				KeyboardPress(keyPlayList.Pitch + ParameterController.GetInstance().Pitch);
			}
			else
			{
				KeyboardRelease(keyPlayList.Pitch + ParameterController.GetInstance().Pitch);
			}
			Log.overlayProcess(((int)(keyPlayList.TimeMs * 100.0 / num).Value).ToString());
		}
		Log.overlayLog("演奏：演奏结束");
		if (stopHandler != null)
		{
			stopHandler.BeginInvoke(null, null);
		}
		ResetKey();
	}
}
