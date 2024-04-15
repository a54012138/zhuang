using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace MyUtils;

public class WindowUtils
{
	public struct POINT
	{
		public int X;

		public int Y;

		public POINT(int x, int y)
		{
		//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00007698
		}

		public static implicit operator Point(POINT p)
		{
		//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000076AC
		}

		public static implicit operator POINT(Point p)
		{
		//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000076D0
		}
	}

	private struct MyRect
	{
		public int Left;

		public int Top;

		public int Right;

		public int Bottom;
	}

	[Flags]
	public enum WindowStyles : uint
	{
		WS_OVERLAPPED = 0u,
		WS_POPUP = 0x80000000u,
		WS_CHILD = 0x40000000u,
		WS_MINIMIZE = 0x20000000u,
		WS_VISIBLE = 0x10000000u,
		WS_DISABLED = 0x8000000u,
		WS_CLIPSIBLINGS = 0x4000000u,
		WS_CLIPCHILDREN = 0x2000000u,
		WS_MAXIMIZE = 0x1000000u,
		WS_BORDER = 0x800000u,
		WS_DLGFRAME = 0x400000u,
		WS_VSCROLL = 0x200000u,
		WS_HSCROLL = 0x100000u,
		WS_SYSMENU = 0x80000u,
		WS_THICKFRAME = 0x40000u,
		WS_GROUP = 0x20000u,
		WS_TABSTOP = 0x10000u,
		WS_MINIMIZEBOX = 0x20000u,
		WS_MAXIMIZEBOX = 0x10000u
	}

	private const int GCL_HICON = -14;

	private const int GCL_HICONSM = -34;

	private const int GWL_STYLE = -16;

	private const int ICON_BIG = 1;

	private const int ICON_SMALL = 0;

	private const int ICON_SMALL2 = 2;

	private const int WM_GETICON = 127;

	private const int WM_GETTEXT = 13;

	public static bool Enabled(IntPtr win)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003DD4
	}

	public static bool Exists(IntPtr win)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003DFC
	}

	public static bool FocuseWindow(IntPtr hWnd)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003E18
	}

	public static Icon GetAppIcon(IntPtr hwnd)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003E30
	}

	public static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003ED8
	}

	public static Rectangle GetClientRect(IntPtr win)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003F10
	}

	public static string GetForegroundWindowText()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003F88
	}

	public static int GetProcessId(IntPtr win)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003FA8
	}

	public static List<WindowStyles> GetStyles(IntPtr win)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003FC8
	}

	public static string GetWindowCaption(IntPtr hWnd)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00004060
	}

	public static string GetWindowClass(IntPtr win)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00004094
	}

	public static Rectangle GetWindowRect(IntPtr win)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000040E0
	}

	public static string GetWindowText(IntPtr hWnd)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00004154
	}

	[DllImport("user32.dll")]
	public static extern IntPtr GetForegroundWindow();

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool IsWindowVisible(IntPtr win);

	[DllImport("user32.dll")]
	private static extern bool ClientToScreen(IntPtr win, ref POINT point);

	[DllImport("user32.dll", EntryPoint = "GetClassLong")]
	private static extern uint GetClassLongPtr32(IntPtr hWnd, int nIndex);

	[DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
	private static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

	[DllImport("user32.dll")]
	private static extern bool GetClientRect(IntPtr hWnd, out MyRect lpRect);

	[DllImport("user32.dll")]
	private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetWindowRect(IntPtr win, out MyRect rect);

	[DllImport("user32.dll")]
	private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

	[DllImport("user32.dll")]
	private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, ref int processId);

	[DllImport("user32.dll")]
	private static extern uint RealGetWindowClass(IntPtr win, StringBuilder pszType, uint cchType);

	[DllImport("user32.dll")]
	private static extern bool ScreenToClient(IntPtr win, ref POINT point);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, StringBuilder lParam);

	[DllImport("User32.dll")]
	private static extern bool SetForegroundWindow(IntPtr hWnd);
}
