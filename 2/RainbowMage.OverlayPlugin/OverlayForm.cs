using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using RainbowMage.HtmlRenderer;
using Xilium.CefGlue;

namespace RainbowMage.OverlayPlugin;

public class OverlayForm : Form
{
	private static object xivProcLocker = new object();

	private static TimeSpan tryInterval = new TimeSpan(0, 0, 15);

	private object surfaceBufferLocker = new object();

	private DIBitmap surfaceBuffer;

	private int maxFrameRate;

	private System.Threading.Timer zorderCorrector;

	private bool terminated;

	private bool shiftKeyPressed;

	private bool altKeyPressed;

	private bool controlKeyPressed;

	private string url;

	private bool isClickThru;

	private bool isDragging;

	private Point offset;

	private static Process xivProc;

	private static DateTime lastTry;

	private IContainer components;

	public Renderer Renderer { get; private set; }

	public string Url
	{
		get
		{
			return url;
		}
		set
		{
			url = value;
			UpdateRender();
		}
	}

	public bool IsClickThru
	{
		get
		{
			return isClickThru;
		}
		set
		{
			if (isClickThru != value)
			{
				isClickThru = value;
				UpdateMouseClickThru();
			}
		}
	}

	public bool IsLoaded { get; private set; }

	public bool Locked { get; set; }

	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = base.CreateParams;
			createParams.ExStyle = createParams.ExStyle | 8 | 0x80000 | 0x8000000;
			createParams.ClassStyle |= 512;
			return createParams;
		}
	}

	public OverlayForm(string overlayVersion, string overlayName, string url, int maxFrameRate = 30)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		InitializeComponent();
		Renderer = new Renderer(overlayVersion, overlayName);
		Renderer.Initialize();
		this.maxFrameRate = maxFrameRate;
		Renderer.Render += renderer_Render;
		base.MouseWheel += OverlayForm_MouseWheel;
		this.url = url;
		Util.HidePreview(this);
	}

	public void Reload()
	{
		Renderer.Reload();
	}

	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
		if (m.Msg == 132 && !Locked)
		{
			Point point = PointToClient(new Point(m.LParam.ToInt32() & 0xFFFF, m.LParam.ToInt32() >> 16));
			int num = point.X;
			int num2 = base.ClientSize.Width - 16;
			if (num >= num2)
			{
				int num3 = point.Y;
				int num4 = base.ClientSize.Height - 16;
				if (num3 >= num4)
				{
					m.Result = (IntPtr)17;
					return;
				}
			}
		}
		if (m.Msg == 256 || m.Msg == 257 || m.Msg == 258 || m.Msg == 260 || m.Msg == 261 || m.Msg == 262)
		{
			OnKeyEvent(ref m);
		}
	}

	private void UpdateLayeredWindowBitmap()
	{
		if (surfaceBuffer.IsDisposed || terminated)
		{
			return;
		}
		using Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
		IntPtr hdc = graphics.GetHdc();
		IntPtr hgdiobj = NativeMethods.SelectObject(surfaceBuffer.DeviceContext, surfaceBuffer.Handle);
		NativeMethods.BlendFunction blendFunction = default(NativeMethods.BlendFunction);
		blendFunction.BlendOp = 0;
		blendFunction.BlendFlags = 0;
		blendFunction.SourceConstantAlpha = byte.MaxValue;
		blendFunction.AlphaFormat = 1;
		NativeMethods.BlendFunction pBlend = blendFunction;
		NativeMethods.Point point = default(NativeMethods.Point);
		point.X = base.Left;
		point.Y = base.Top;
		NativeMethods.Point pptDst = point;
		NativeMethods.Size size = default(NativeMethods.Size);
		size.Width = surfaceBuffer.Width;
		size.Height = surfaceBuffer.Height;
		NativeMethods.Size pSize = size;
		point = default(NativeMethods.Point);
		point.X = 0;
		point.Y = 0;
		NativeMethods.Point pptSrc = point;
		IntPtr handle = IntPtr.Zero;
		try
		{
			if (!terminated)
			{
				if (base.InvokeRequired)
				{
					Invoke((Action)delegate
					{
						handle = base.Handle;
					});
				}
				else
				{
					handle = base.Handle;
				}
				NativeMethods.UpdateLayeredWindow(handle, hdc, ref pptDst, ref pSize, surfaceBuffer.DeviceContext, ref pptSrc, 0, ref pBlend, 2u);
			}
		}
		catch (ObjectDisposedException)
		{
			return;
		}
		NativeMethods.SelectObject(surfaceBuffer.DeviceContext, hgdiobj);
		graphics.ReleaseHdc(hdc);
	}

	private void UpdateMouseClickThru()
	{
		if (IsLoaded)
		{
			if (isClickThru)
			{
				EnableMouseClickThru();
			}
			else
			{
				DisableMouseClickThru();
			}
		}
	}

	private void EnableMouseClickThru()
	{
		NativeMethods.SetWindowLong(base.Handle, -20, NativeMethods.GetWindowLong(base.Handle, -20) | 0x20u);
	}

	private void DisableMouseClickThru()
	{
		NativeMethods.SetWindowLong(base.Handle, -20, (int)NativeMethods.GetWindowLong(base.Handle, -20) & -33);
	}

	private void renderer_Render(object sender, RenderEventArgs e)
	{
		if (terminated)
		{
			return;
		}
		try
		{
			if (surfaceBuffer != null && (surfaceBuffer.Width != e.Width || surfaceBuffer.Height != e.Height))
			{
				surfaceBuffer.Dispose();
				surfaceBuffer = null;
			}
			if (surfaceBuffer == null)
			{
				surfaceBuffer = new DIBitmap(e.Width, e.Height);
			}
			surfaceBuffer.SetSurfaceData(e.Buffer, (uint)(e.Width * e.Height * 4));
			UpdateLayeredWindowBitmap();
		}
		catch
		{
		}
	}

	private void UpdateRender()
	{
		if (Renderer != null)
		{
			Renderer.BeginRender(base.Width, base.Height, Url, maxFrameRate);
		}
	}

	private void OverlayForm_Load(object sender, EventArgs e)
	{
		IsLoaded = true;
		UpdateMouseClickThru();
		zorderCorrector = new System.Threading.Timer(delegate
		{
			if (base.Visible && !IsOverlaysGameWindow())
			{
				EnsureTopMost();
			}
		}, null, 0, 1000);
	}

	private void OverlayForm_FormClosed(object sender, FormClosedEventArgs e)
	{
		Renderer.EndRender();
		terminated = true;
	}

	protected override void Dispose(bool disposing)
	{
		if (zorderCorrector != null)
		{
			zorderCorrector.Dispose();
		}
		if (Renderer != null)
		{
			Renderer.Dispose();
			Renderer = null;
		}
		if (surfaceBuffer != null)
		{
			surfaceBuffer.Dispose();
		}
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void OverlayForm_Resize(object sender, EventArgs e)
	{
		if (Renderer != null)
		{
			Renderer.Resize(base.Width, base.Height);
		}
	}

	private void OverlayForm_MouseDown(object sender, MouseEventArgs e)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (!Locked)
		{
			isDragging = true;
			offset = e.Location;
		}
		Renderer.SendMouseUpDown(e.X, e.Y, GetMouseButtonType(e), false);
	}

	private void OverlayForm_MouseMove(object sender, MouseEventArgs e)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		if (isDragging)
		{
			Point point = PointToScreen(e.Location);
			base.Location = new Point(point.X - offset.X, point.Y - offset.Y);
		}
		else
		{
			Renderer.SendMouseMove(e.X, e.Y, GetMouseButtonType(e));
		}
	}

	private void OverlayForm_MouseUp(object sender, MouseEventArgs e)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		isDragging = false;
		Renderer.SendMouseUpDown(e.X, e.Y, GetMouseButtonType(e), true);
	}

	private void OverlayForm_MouseWheel(object sender, MouseEventArgs e)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Expected I4, but got Unknown
		int num = (int)GetMouseEventFlags(e);
		Renderer.SendMouseWheel(e.X, e.Y, e.Delta, shiftKeyPressed);
	}

	private CefMouseButtonType GetMouseButtonType(MouseEventArgs e)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (e.Button == MouseButtons.Left)
		{
			return (CefMouseButtonType)0;
		}
		if (e.Button == MouseButtons.Middle)
		{
			return (CefMouseButtonType)1;
		}
		return (CefMouseButtonType)((e.Button == MouseButtons.Right) ? 2 : 0);
	}

	private CefEventFlags GetMouseEventFlags(MouseEventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		CefEventFlags val = (CefEventFlags)0;
		if (e.Button == MouseButtons.Left)
		{
			val = (CefEventFlags)(val | 0x10);
		}
		else if (e.Button == MouseButtons.Middle)
		{
			val = (CefEventFlags)(val | 0x20);
		}
		else if (e.Button == MouseButtons.Right)
		{
			val = (CefEventFlags)(val | 0x40);
		}
		if (shiftKeyPressed)
		{
			val = (CefEventFlags)(val | 2);
		}
		if (altKeyPressed)
		{
			val = (CefEventFlags)(val | 8);
		}
		if (controlKeyPressed)
		{
			val = (CefEventFlags)(val | 4);
		}
		return val;
	}

	private bool IsOverlaysGameWindow()
	{
		IntPtr gameWindowHandle = GetGameWindowHandle();
		IntPtr handle = base.Handle;
		while (handle != IntPtr.Zero)
		{
			if (handle == gameWindowHandle)
			{
				return false;
			}
			handle = NativeMethods.GetWindow(handle, 3u);
		}
		return true;
	}

	private void EnsureTopMost()
	{
		NativeMethods.SetWindowPos(base.Handle, NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, 19u);
	}

	private static IntPtr GetGameWindowHandle()
	{
		lock (xivProcLocker)
		{
			try
			{
				if (xivProc != null && xivProc.HasExited)
				{
					xivProc = null;
				}
				if (xivProc == null && DateTime.Now - lastTry > tryInterval)
				{
					xivProc = Process.GetProcessesByName("ffxiv").FirstOrDefault();
					if (xivProc == null)
					{
						xivProc = Process.GetProcessesByName("ffxiv_dx11").FirstOrDefault();
					}
					lastTry = DateTime.Now;
				}
				if (xivProc != null)
				{
					return xivProc.MainWindowHandle;
				}
			}
			catch (Win32Exception)
			{
			}
			return IntPtr.Zero;
		}
	}

	private void OverlayForm_KeyDown(object sender, KeyEventArgs e)
	{
		altKeyPressed = e.Alt;
		shiftKeyPressed = e.Shift;
		controlKeyPressed = e.Control;
	}

	private void OverlayForm_KeyUp(object sender, KeyEventArgs e)
	{
		altKeyPressed = e.Alt;
		shiftKeyPressed = e.Shift;
		controlKeyPressed = e.Control;
	}

	private void OnKeyEvent(ref Message m)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		CefKeyEvent val = new CefKeyEvent();
		val.WindowsKeyCode = m.WParam.ToInt32();
		val.NativeKeyCode = (int)m.LParam.ToInt64();
		val.IsSystemKey = m.Msg == 262 || m.Msg == 260 || m.Msg == 261;
		val.EventType = (CefKeyEventType)((m.Msg != 256 && m.Msg != 260) ? ((m.Msg == 257 || m.Msg == 261) ? 2 : 3) : 0);
		val.Modifiers = GetKeyboardModifiers(ref m);
		if (Renderer != null)
		{
			Renderer.SendKeyEvent(val);
		}
	}

	private CefEventFlags GetKeyboardModifiers(ref Message m)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		CefEventFlags val = (CefEventFlags)0;
		if (IsKeyDown(Keys.Shift))
		{
			val = (CefEventFlags)(val | 2);
		}
		if (IsKeyDown(Keys.Control))
		{
			val = (CefEventFlags)(val | 4);
		}
		if (IsKeyDown(Keys.Menu))
		{
			val = (CefEventFlags)(val | 8);
		}
		if (IsKeyToggled(Keys.NumLock))
		{
			val = (CefEventFlags)(val | 0x100);
		}
		if (IsKeyToggled(Keys.Capital))
		{
			val = (CefEventFlags)(val | 1);
		}
		switch ((Keys)(int)m.WParam)
		{
		case Keys.Clear:
		case Keys.NumPad0:
		case Keys.NumPad1:
		case Keys.NumPad2:
		case Keys.NumPad3:
		case Keys.NumPad4:
		case Keys.NumPad5:
		case Keys.NumPad6:
		case Keys.NumPad7:
		case Keys.NumPad8:
		case Keys.NumPad9:
		case Keys.Multiply:
		case Keys.Add:
		case Keys.Subtract:
		case Keys.Decimal:
		case Keys.Divide:
		case Keys.NumLock:
			val = (CefEventFlags)(val | 0x200);
			break;
		case Keys.Return:
			if (((m.LParam.ToInt64() >> 48) & 0x100) != 0)
			{
				val = (CefEventFlags)(val | 0x200);
			}
			break;
		case Keys.Prior:
		case Keys.Next:
		case Keys.End:
		case Keys.Home:
		case Keys.Left:
		case Keys.Up:
		case Keys.Right:
		case Keys.Down:
		case Keys.Insert:
		case Keys.Delete:
			if (((m.LParam.ToInt64() >> 48) & 0x100) == 0)
			{
				val = (CefEventFlags)(val | 0x200);
			}
			break;
		case Keys.LWin:
			val = (CefEventFlags)(val | 0x400);
			break;
		case Keys.RWin:
			val = (CefEventFlags)(val | 0x800);
			break;
		case Keys.Shift:
			val = (CefEventFlags)((!IsKeyDown(Keys.LShiftKey)) ? (val | 0x800) : (val | 0x400));
			break;
		case Keys.Control:
			val = (CefEventFlags)((!IsKeyDown(Keys.LControlKey)) ? (val | 0x800) : (val | 0x400));
			break;
		case Keys.Alt:
			val = (CefEventFlags)((!IsKeyDown(Keys.LMenu)) ? (val | 0x800) : (val | 0x400));
			break;
		}
		return val;
	}

	private bool IsKeyDown(Keys key)
	{
		return (NativeMethods.GetKeyState((int)key) & 0x8000) != 0;
	}

	private bool IsKeyToggled(Keys key)
	{
		return (NativeMethods.GetKeyState((int)key) & 1) == 1;
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(394, 242);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "OverlayForm";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "OverlayForm";
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(OverlayForm_FormClosed);
		base.Load += new System.EventHandler(OverlayForm_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(OverlayForm_KeyDown);
		base.KeyUp += new System.Windows.Forms.KeyEventHandler(OverlayForm_KeyUp);
		base.MouseDown += new System.Windows.Forms.MouseEventHandler(OverlayForm_MouseDown);
		base.MouseMove += new System.Windows.Forms.MouseEventHandler(OverlayForm_MouseMove);
		base.MouseUp += new System.Windows.Forms.MouseEventHandler(OverlayForm_MouseUp);
		base.Resize += new System.EventHandler(OverlayForm_Resize);
		base.ResumeLayout(false);
	}
}
