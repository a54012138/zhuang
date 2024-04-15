using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace RainbowMage.OverlayPlugin;

public static class NativeMethods
{
	public struct BitmapInfo
	{
		public BitmapInfoHeader bmiHeader;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.Struct)]
		public RgbQuad[] bmiColors;
	}

	public struct BitmapInfoHeader
	{
		public uint biSize;

		public int biWidth;

		public int biHeight;

		public ushort biPlanes;

		public ushort biBitCount;

		public BitmapCompressionMode biCompression;

		public uint biSizeImage;

		public int biXPelsPerMeter;

		public int biYPelsPerMeter;

		public uint biClrUsed;

		public uint biClrImportant;

		public void Init()
		{
			biSize = (uint)Marshal.SizeOf((object)this);
		}
	}

	public struct BlendFunction
	{
		public byte BlendOp;

		public byte BlendFlags;

		public byte SourceConstantAlpha;

		public byte AlphaFormat;
	}

	public struct Point
	{
		public int X;

		public int Y;
	}

	public struct Size
	{
		public int Width;

		public int Height;
	}

	public struct RgbQuad
	{
		public byte rgbBlue;

		public byte rgbGreen;

		public byte rgbRed;

		public byte rgbReserved;
	}

	public enum BitmapCompressionMode : uint
	{
		BI_RGB,
		BI_RLE8,
		BI_RLE4,
		BI_BITFIELDS,
		BI_JPEG,
		BI_PNG
	}

	public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

	public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

	public static readonly IntPtr HWND_TOP = new IntPtr(0);

	public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

	public const int GWL_EXSTYLE = -20;

	public const int WS_EX_LAYERED = 524288;

	public const int WS_EX_TOOLWINDOW = 128;

	public const int WS_EX_TRANSPARENT = 32;

	public const int LWA_ALPHA = 2;

	public const int LWA_COLORKEY = 1;

	public const int SW_HIDE = 0;

	public const int SW_SHOW = 5;

	public const uint SWP_NOSIZE = 1u;

	public const uint SWP_NOMOVE = 2u;

	public const uint TOPMOST_FLAGS = 3u;

	public const int ERROR_INSUFFICIENT_BUFFER = 122;

	public const byte AC_SRC_ALPHA = 1;

	public const byte AC_SRC_OVER = 0;

	public const int ULW_ALPHA = 2;

	public const int DIB_RGB_COLORS = 0;

	public const uint GW_HWNDPREV = 3u;

	public const uint SWP_NOACTIVATE = 16u;

	public const int WM_KEYDOWN = 256;

	public const int WM_KEYUP = 257;

	public const int WM_CHAR = 258;

	public const int WM_SYSKEYDOWN = 260;

	public const int WM_SYSKEYUP = 261;

	public const int WM_SYSCHAR = 262;

	[DllImport("user32")]
	public static extern bool UpdateLayeredWindow(IntPtr hWnd, IntPtr hdcDst, [In] ref Point pptDst, [In] ref Size pSize, IntPtr hdcSrc, [In] ref Point pptSrc, int crKey, [In] ref BlendFunction pBlend, uint dwFlags);

	[DllImport("gdi32")]
	public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

	[DllImport("gdi32")]
	public static extern bool DeleteObject(IntPtr hObject);

	[DllImport("gdi32")]
	public static extern bool DeleteDC(IntPtr hdc);

	[DllImport("gdi32")]
	public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

	[DllImport("gdi32")]
	public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

	[DllImport("gdi32")]
	public static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BitmapInfo pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

	[DllImport("kernel32")]
	public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

	[DllImport("kernel32.dll")]
	public static extern void SetLastError(int dwErrorCode);

	private static int ToIntPtr32(IntPtr intPtr)
	{
		return (int)intPtr.ToInt64();
	}

	public static IntPtr SetWindowLongA(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
	{
		IntPtr zero = IntPtr.Zero;
		SetLastError(0);
		int lastWin32Error;
		if (IntPtr.Size == 4)
		{
			int value = SetWindowLong(hWnd, nIndex, ToIntPtr32(dwNewLong));
			lastWin32Error = Marshal.GetLastWin32Error();
			zero = new IntPtr(value);
		}
		else
		{
			zero = SetWindowLongPtr(hWnd, nIndex, dwNewLong);
			lastWin32Error = Marshal.GetLastWin32Error();
		}
		if (zero == IntPtr.Zero && lastWin32Error != 0)
		{
			throw new Win32Exception(lastWin32Error);
		}
		return zero;
	}

	[DllImport("user32.dll")]
	public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern int GetModuleFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);

	[DllImport("user32.dll")]
	public static extern short GetKeyState(int nVirtKey);

	[DllImport("winmm.dll", EntryPoint = "timeBeginPeriod", SetLastError = true)]
	internal static extern uint TimeBeginPeriod(uint uMilliseconds);

	[DllImport("winmm.dll", EntryPoint = "timeEndPeriod", SetLastError = true)]
	internal static extern uint TimeEndPeriod(uint uMilliseconds);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

	[DllImport("user32.dll")]
	internal static extern IntPtr GetForegroundWindow();

	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	internal static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	internal static extern int SetWindowLong(IntPtr Handle, int nIndex, uint dwNewLong);

	[DllImport("user32.dll", SetLastError = true)]
	internal static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	internal static extern bool ShowWindow(IntPtr Handle, uint nCmdShow);

	[DllImport("kernel32.dll")]
	internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, IntPtr nSize, ref IntPtr lpNumberOfBytesRead);
}
