using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RainbowMage.OverlayPlugin;

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
