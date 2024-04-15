using System;
using System.Runtime.InteropServices;

namespace RainbowMage.OverlayPlugin;

internal class DIBitmap : IDisposable
{
	public int Width { get; private set; }

	public int Height { get; private set; }

	public IntPtr Bits { get; private set; }

	public IntPtr Handle { get; private set; }

	public IntPtr DeviceContext { get; private set; }

	public bool IsDisposed { get; private set; }

	public DIBitmap(int width, int height)
	{
		IsDisposed = false;
		Width = width;
		Height = height;
		DeviceContext = NativeMethods.CreateCompatibleDC(NativeMethods.CreateCompatibleDC(IntPtr.Zero));
		NativeMethods.BitmapInfo pbmi = default(NativeMethods.BitmapInfo);
		pbmi.bmiHeader.biSize = (uint)Marshal.SizeOf((object)pbmi);
		pbmi.bmiHeader.biBitCount = 32;
		pbmi.bmiHeader.biPlanes = 1;
		pbmi.bmiHeader.biWidth = width;
		pbmi.bmiHeader.biHeight = -height;
		Handle = NativeMethods.CreateDIBSection(DeviceContext, ref pbmi, 0u, out var ppvBits, IntPtr.Zero, 0u);
		Bits = ppvBits;
	}

	public void SetSurfaceData(IntPtr srcSurfaceData, uint count)
	{
		NativeMethods.CopyMemory(Bits, srcSurfaceData, count);
	}

	public void Dispose()
	{
		if (Handle != IntPtr.Zero)
		{
			NativeMethods.DeleteObject(Handle);
		}
		if (DeviceContext != IntPtr.Zero)
		{
			NativeMethods.DeleteDC(DeviceContext);
		}
		IsDisposed = true;
	}
}
