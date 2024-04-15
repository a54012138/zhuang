using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Daigassou.Input_Midi;

public class BackgroundKey
{
	private const int WmKeydown = 256;

	private const int WmKeyup = 257;

	private static IntPtr _gameIntPtr;

	[DllImport("user32.dll")]
	private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

	[DllImport("user32.dll")]
	public static extern bool PostMessage(IntPtr hwnd, uint msg, uint wParam, uint lParam);

	public static IEnumerable<int> GetPids()
	{
		Process[] processes = Process.GetProcesses();
		foreach (Process p in processes)
		{
			if (string.Equals(p.ProcessName, "ffxiv", StringComparison.Ordinal) || string.Equals(p.ProcessName, "ffxiv_dx11", StringComparison.Ordinal))
			{
				yield return p.Id;
			}
			p.Dispose();
		}
	}

	public void Init(IntPtr gameIntPtr)
	{
		_gameIntPtr = gameIntPtr;
	}

	public void BackgroundKeyPress(Keys viKeys)
	{
		if (_gameIntPtr != IntPtr.Zero)
		{
			PostMessage(_gameIntPtr, 256u, (uint)viKeys, 0u);
		}
	}

	public void BackgroundKeyRelease(Keys viKeys)
	{
		if (_gameIntPtr != IntPtr.Zero)
		{
			PostMessage(_gameIntPtr, 257u, (uint)viKeys, 0u);
		}
	}
}
