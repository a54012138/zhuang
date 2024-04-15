using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Input;
using RainbowMage.HtmlRenderer;

namespace RainbowMage.OverlayPlugin;

public abstract class OverlayBase<TConfig> : IOverlay, IDisposable where TConfig : OverlayConfigBase
{
	internal static class Util
	{
		public static string CreateJsonSafeString(string str)
		{
			return str.Replace("\"", "\\\"").Replace("'", "\\'").Replace("\r", "\\r")
				.Replace("\n", "\\n")
				.Replace("\t", "\\t");
		}

		public static string ReplaceNaNString(string str, string replace)
		{
			return str.Replace(double.NaN.ToString(), replace);
		}

		public static bool IsOnScreen(Form form)
		{
			Screen[] allScreens = Screen.AllScreens;
			foreach (Screen screen in allScreens)
			{
				if (screen.WorkingArea.IntersectsWith(new Rectangle(form.Left, form.Top, form.Width, form.Height)))
				{
					return true;
				}
			}
			return false;
		}

		public static void HidePreview(Form form)
		{
			uint num = NativeMethods.GetWindowLong(form.Handle, -20) | 0x80u;
			NativeMethods.SetWindowLongA(form.Handle, -20, (IntPtr)num);
		}

		public static string GetHotkeyString(Keys modifier, Keys key, string defaultText = "")
		{
			StringBuilder stringBuilder = new StringBuilder();
			if ((modifier & Keys.Shift) == Keys.Shift)
			{
				stringBuilder.Append("Shift + ");
			}
			if ((modifier & Keys.Control) == Keys.Control)
			{
				stringBuilder.Append("Ctrl + ");
			}
			if ((modifier & Keys.Alt) == Keys.Alt)
			{
				stringBuilder.Append("Alt + ");
			}
			if ((modifier & Keys.LWin) == Keys.LWin || (modifier & Keys.RWin) == Keys.RWin)
			{
				stringBuilder.Append("Win + ");
			}
			stringBuilder.Append(Enum.ToObject(typeof(Keys), key).ToString());
			return stringBuilder.ToString();
		}

		public static Keys RemoveModifiers(Keys keyCode, Keys modifiers)
		{
			Keys keys = keyCode;
			foreach (Keys item in new List<Keys>
			{
				Keys.ControlKey,
				Keys.LControlKey,
				Keys.Alt,
				Keys.ShiftKey,
				Keys.Shift,
				Keys.LShiftKey,
				Keys.RShiftKey,
				Keys.Control,
				Keys.LWin,
				Keys.RWin
			})
			{
				if (keys.HasFlag(item) && keys == item)
				{
					keys &= ~item;
				}
			}
			return keys;
		}
	}

	protected System.Timers.Timer timer;

	protected System.Timers.Timer xivWindowTimer;

	public string Name { get; private set; }

	public OverlayForm Overlay { get; private set; }

	public TConfig Config { get; private set; }

	public IPluginConfig PluginConfig { get; set; }

	public event EventHandler<LogEventArgs> OnLog;

	protected OverlayBase(TConfig config, string name)
	{
		Config = config;
		Name = name;
		InitializeOverlay();
		InitializeTimer();
		InitializeConfigHandlers();
	}

	public void Start()
	{
		timer.Start();
		xivWindowTimer.Start();
	}

	public void Stop()
	{
		timer.Stop();
		xivWindowTimer.Stop();
	}

	protected virtual void InitializeOverlay()
	{
		try
		{
			Overlay = new OverlayForm(Assembly.GetExecutingAssembly().GetName().Version.ToString(), Name, "about:blank", Config.MaxFrameRate);
			if (!Util.IsOnScreen(Overlay))
			{
				Overlay.StartPosition = FormStartPosition.WindowsDefaultLocation;
			}
			else
			{
				Overlay.Location = Config.Position;
			}
			Overlay.Text = Name;
			Overlay.Size = Config.Size;
			Overlay.IsClickThru = Config.IsClickThru;
			Overlay.Renderer.BrowserError += delegate(object o, BrowserErrorEventArgs e)
			{
				//IL_0010: Unknown result type (might be due to invalid IL or missing references)
				Log((LogLevel)4, "浏览器错误: {0}, {1}, {2}", e.ErrorCode, e.ErrorText, e.Url);
			};
			Overlay.Renderer.BrowserLoad += delegate(object o, BrowserLoadEventArgs e)
			{
				Log((LogLevel)1, "浏览器读取: {0}: {1}", e.HttpStatusCode, e.Url);
				NotifyOverlayState();
			};
			Overlay.Renderer.BrowserConsoleLog += delegate(object o, BrowserConsoleLogEventArgs e)
			{
				Log((LogLevel)2, "浏览器控制: {0} (目标: {1}, 行: {2})", e.Message, e.Source, e.Line);
			};
			Config.UrlChanged += delegate(object o, UrlChangedEventArgs e)
			{
				Navigate(e.NewUrl);
			};
			if (CheckUrl(Config.Url))
			{
				Navigate(Config.Url);
			}
			else
			{
				Navigate("about:blank");
			}
			Overlay.Show();
			Overlay.Visible = Config.IsVisible;
			Overlay.Locked = Config.IsLocked;
		}
		catch (Exception ex)
		{
			Log((LogLevel)4, "初始化美化窗口: {0}", Name, ex);
		}
	}

	private ModifierKeys GetModifierKey(Keys modifier)
	{
		ModifierKeys modifierKeys = ModifierKeys.None;
		if ((modifier & Keys.Shift) == Keys.Shift)
		{
			modifierKeys |= ModifierKeys.Shift;
		}
		if ((modifier & Keys.Control) == Keys.Control)
		{
			modifierKeys |= ModifierKeys.Control;
		}
		if ((modifier & Keys.Alt) == Keys.Alt)
		{
			modifierKeys |= ModifierKeys.Alt;
		}
		if ((modifier & Keys.LWin) == Keys.LWin || (modifier & Keys.RWin) == Keys.RWin)
		{
			modifierKeys |= ModifierKeys.Windows;
		}
		return modifierKeys;
	}

	private bool CheckUrl(string url)
	{
		try
		{
			Uri uri = new Uri(url);
			if (uri.Scheme == "file" && !File.Exists(uri.LocalPath))
			{
				Log((LogLevel)3, "初始化美化窗口: 本地文件 {0} 不存在!", uri.LocalPath);
				return false;
			}
		}
		catch (Exception ex)
		{
			Log((LogLevel)4, "初始化美化窗口: URI解析错误! 请核对URL. (Config.Url = {0}): {1}", Config.Url, ex);
			return false;
		}
		return true;
	}

	protected virtual void InitializeTimer()
	{
		timer = new System.Timers.Timer();
		timer.Interval = 1000.0;
		timer.Elapsed += delegate
		{
			try
			{
				Update();
			}
			catch (Exception ex2)
			{
				Log((LogLevel)4, "更新: {0}", ex2.ToString());
			}
		};
		xivWindowTimer = new System.Timers.Timer();
		xivWindowTimer.Interval = 1000.0;
		xivWindowTimer.Elapsed += delegate
		{
			try
			{
				if (Config.IsVisible && PluginConfig.HideOverlaysWhenNotActive)
				{
					IntPtr foregroundWindow = NativeMethods.GetForegroundWindow();
					if (!(foregroundWindow == IntPtr.Zero))
					{
						uint lpdwProcessId;
						int windowThreadProcessId = (int)NativeMethods.GetWindowThreadProcessId(foregroundWindow, out lpdwProcessId);
						string fileName = Process.GetProcessById((int)lpdwProcessId).MainModule.FileName;
						if (Path.GetFileName(fileName.ToString()) == "ffxiv.exe" || Path.GetFileName(fileName.ToString()) == "ffxiv_dx11.exe" || fileName.ToString() == Process.GetCurrentProcess().MainModule.FileName)
						{
							Overlay.Visible = true;
						}
						else
						{
							Overlay.Visible = false;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log((LogLevel)4, "最终幻想14窗口监视: {0}", ex.ToString());
			}
		};
	}

	protected virtual void InitializeConfigHandlers()
	{
		Config.VisibleChanged += delegate(object o, VisibleStateChangedEventArgs e)
		{
			Overlay.Visible = e.IsVisible;
		};
		Config.ClickThruChanged += delegate(object o, ThruStateChangedEventArgs e)
		{
			Overlay.IsClickThru = e.IsClickThru;
		};
		Config.LockChanged += delegate(object o, LockStateChangedEventArgs e)
		{
			Overlay.Locked = e.IsLocked;
			NotifyOverlayState();
		};
	}

	protected abstract void Update();

	public virtual void Dispose()
	{
		try
		{
			if (timer != null)
			{
				timer.Stop();
			}
			if (xivWindowTimer != null)
			{
				xivWindowTimer.Stop();
			}
			if (Overlay != null)
			{
				Overlay.Close();
				Overlay.Dispose();
			}
		}
		catch (Exception ex)
		{
			Log((LogLevel)4, "清除: {0}", ex);
		}
	}

	public virtual void Navigate(string url)
	{
		Overlay.Url = url;
	}

	protected void Log(LogLevel level, string message)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		if (this.OnLog != null)
		{
			this.OnLog(this, new LogEventArgs(level, $"{Name}: {message}"));
		}
	}

	protected void Log(LogLevel level, string format, params object[] args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Log(level, string.Format(format, args));
	}

	public void SavePositionAndSize()
	{
		Config.Position = Overlay.Location;
		Config.Size = Overlay.Size;
	}

	private void NotifyOverlayState()
	{
		string text = string.Format("document.dispatchEvent(new CustomEvent('onOverlayStateUpdate', {{ detail: {{ isLocked: {0} }} }}));", Config.IsLocked ? "true" : "false");
		if (Overlay != null && Overlay.Renderer != null && Overlay.Renderer.Browser != null)
		{
			Overlay.Renderer.ExecuteScript(text);
		}
	}

	public void SendMessage(string message)
	{
		string text = $"document.dispatchEvent(new CustomEvent('onBroadcastMessageReceive', {{ detail: {{ message: \"{Util.CreateJsonSafeString(message)}\" }} }}));";
		if (Overlay != null && Overlay.Renderer != null && Overlay.Renderer.Browser != null)
		{
			Overlay.Renderer.ExecuteScript(text);
		}
	}

	public virtual void OverlayMessage(string message)
	{
	}
}
