using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MyUtils;

public class MouseKeyboardUtils
{
	[Flags]
	public enum MouseFlags
	{
		Move = 1,
		LeftDown = 2,
		LeftUp = 4,
		RightDown = 8,
		RightUp = 0x10,
		Absolute = 0x8000,
		MiddleDown = 0x20,
		MiddleUp = 0x40,
		Scroll = 0x800
	}

	public enum MouseCursor
	{
		Unknown = 0,
		Arrow = 65539,
		Text = 65541
	}

	public enum WMessages
	{
		WM_LBUTTONDOWN = 513,
		WM_LBUTTONUP = 514,
		WM_LBUTTONDBLCLK = 515,
		WM_RBUTTONDOWN = 516,
		WM_RBUTTONUP = 517,
		WM_RBUTTONDBLCLK = 518,
		WM_KEYDOWN = 256,
		WM_KEYUP = 257
	}

	private struct MyPoint
	{
		public int x;

		public int y;
	}

	private struct CursorInfo
	{
		public int cbSize;

		public int flags;

		public IntPtr hCursor;

		public MyPoint ptScreenPos;
	}

	public enum KeyEnum
	{
		VK_LBUTTON = 1,
		VK_RBUTTON = 2,
		VK_CANCEL = 3,
		VK_MBUTTON = 4,
		VK_BACK = 8,
		VK_TAB = 9,
		VK_CLEAR = 12,
		VK_RETURN = 13,
		VK_SHIFT = 10,
		VK_CONTROL = 17,
		VK_MENU = 18,
		VK_PAUSE = 19,
		VK_CAPITAL = 20,
		VK_ESCAPE = 27,
		VK_SPACE = 32,
		VK_PRIOR = 33,
		VK_NEXT = 34,
		VK_END = 35,
		VK_HOME = 36,
		VK_LEFT = 37,
		VK_UP = 38,
		VK_RIGHT = 39,
		VK_DOWN = 40,
		VK_SELECT = 41,
		VK_PRINT = 42,
		VK_EXECUTE = 43,
		VK_SNAPSHOT = 44,
		VK_INSERT = 45,
		VK_DELETE = 46,
		VK_HELP = 47,
		VK_LWIN = 91,
		VK_RWIN = 92,
		VK_APPS = 93,
		VK_NUMPAD0 = 96,
		VK_NUMPAD1 = 97,
		VK_NUMPAD2 = 98,
		VK_NUMPAD3 = 99,
		VK_NUMPAD4 = 100,
		VK_NUMPAD5 = 101,
		VK_NUMPAD6 = 102,
		VK_NUMPAD7 = 103,
		VK_NUMPAD8 = 104,
		VK_NUMPAD9 = 105,
		VK_MULTIPLY = 106,
		VK_ADD = 107,
		VK_SEPARATOR = 108,
		VK_SUBTRACT = 109,
		VK_DECIMAL = 110,
		VK_DIVIDE = 111,
		VK_F1 = 112,
		VK_F2 = 113,
		VK_F3 = 114,
		VK_F4 = 115,
		VK_F5 = 116,
		VK_F6 = 117,
		VK_F7 = 118,
		VK_F8 = 119,
		VK_F9 = 120,
		VK_F10 = 121,
		VK_F11 = 122,
		VK_F12 = 123,
		VK_F13 = 124,
		VK_F14 = 125,
		VK_F15 = 126,
		VK_F16 = 127,
		VK_F17 = 128,
		VK_F18 = 129,
		VK_F19 = 130,
		VK_F20 = 131,
		VK_F21 = 132,
		VK_F22 = 133,
		VK_F23 = 134,
		VK_F24 = 135,
		VK_NUMLOCK = 144,
		VK_SCROLL = 145,
		VK_LSHIFT = 160,
		VK_RSHIFT = 161,
		VK_LCONTROL = 162,
		VK_RCONTROL = 163,
		VK_LMENU = 164,
		VK_RMENU = 165,
		VK_PROCESSKEY = 229,
		VK_ATTN = 246,
		VK_CRSEL = 247,
		VK_EXSEL = 248,
		VK_EREOF = 249,
		VK_PLAY = 250,
		VK_ZOOM = 251,
		VK_NONAME = 252,
		VK_PA1 = 253,
		VK_OEM_CLEAR = 254
	}

	private static Random random;

	public const uint KEYEVENTF_EXTENDEDKEY = 1u;

	public const uint KEYEVENTF_KEYUP = 2u;

	public const int KEY_ALT = 18;

	public const int KEY_CONTROL = 17;

	public static int GetLastError()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000027D4
	}

	private static int GetRandomPart()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000027EC
	}

	private static int GetRandomCoord(int length)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002858
	}

	private static Point GetRandomPoint(Rectangle rect)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000290C
	}

	public static void VisualizeRandomPoints(string dest, int tries, int width, int height)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002950
	}

	public static void MoveTo(int x, int y)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002A3C
	}

	public static void MoveTo(IntPtr windowHandle, int x, int y)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002A48
	}

	public static void SetCursorPosition(int x, int y)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002A8C
	}

	public static void SetCursorPosition(Point p)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002A97
	}

	public static MouseCursor GetCursorInfo()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002AB0
	}

	public static void PressLeftMouseButton(IntPtr windowHandle, int time, int x, int y)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002B24
	}

	public static void PressLeftMouseButton(IntPtr windowHandle, int time, Point p)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002B6F
	}

	public static void PressLeftMouseButton(int time, int x, int y)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002B88
	}

	public static void PressLeftMouseButton(int time, Point p)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002B99
	}

	public static void PressLeftMouseButton(Point p)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002BAA
	}

	public static void PressMiddleMouseButton(Point p)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002BBB
	}

	public static void Drag(Point p1, Point p2)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002BCC
	}

	public static void Drag(Rectangle r1, Rectangle r2)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002C8C
	}

	public static void Drag(Point p1, int dx, int dy)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002CB0
	}

	public static void MoveToRect(Rectangle rect)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002CE0
	}

	public static void ClickOnRect(Rectangle rect, int clicks, bool left)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002D0C
	}

	public static void ClickOnRect(int x, int y)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002D68
	}

	public static void ClickOnRect(Rectangle rect)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002D7A
	}

	public static void ClickOnRect(Rectangle rect, int clicks)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002D86
	}

	public static void ClickOnRect(IntPtr windowHandle, Rectangle rect, int clicks)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002D94
	}

	public static bool ClickOnButton(string processName, string buttonText, bool exact)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002DE4
	}

	public static void LeftClick()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002E9B
	}

	public static void LeftDoubleClick()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002EA6
	}

	public static void PressLeftMouseButton()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002EC1
	}

	public static void PressLeftMouseButton(int time)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002ECC
	}

	public static void PressMiddleMouseButton(int time)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002F34
	}

	public static void RightClick()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002F99
	}

	public static void RightDoubleClick()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002FA4
	}

	public static void PressRightMouseButton(IntPtr windowHandle, int time, int x, int y)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002FC0
	}

	public static void PressRightMouseButton(int time)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000300C
	}

	public static void PressKey(int key, bool shift, bool alt, bool ctrl, int holdFor)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003074
	}

	public static void PressKey(KeyEnum key, bool shift, bool alt, bool ctrl)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003139
	}

	public static void PressKey(KeyEnum key)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003147
	}

	public static void PressKey(int key, bool shift, bool alt, bool ctrl)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003154
	}

	public static void Paste(string text, int delay)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003162
	}

	public static void Paste(string text, int delay, bool withBackup)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003170
	}

	public static void Type(string text, int delay)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000031DA
	}

	public static void Type(string text, int delayFrom, int delayTo)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000031E8
	}

	public static void Scroll(int scrollValue)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003274
	}

	[DllImport("user32.dll")]
	private static extern void keybd_event(int bVk, byte bScan, uint dwFlags, int dwExtraInfo);

	[DllImport("user32.dll")]
	private static extern bool GetCursorPos(ref Point lpPoint);

	[DllImport("user32.dll")]
	private static extern void SetCursorPos(int x, int y);

	[DllImport("user32.dll")]
	private static extern void mouse_event(MouseFlags dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);

	[DllImport("user32.dll")]
	private static extern bool GetCursorInfo(out CursorInfo pci);
}
