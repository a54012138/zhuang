using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace RainbowMage.OverlayPlugin;

public class KeyPressedEventArgs : EventArgs
{
	private ModifierKeys _modifier;

	private Keys _key;

	public ModifierKeys Modifier => _modifier;

	public Keys Key => _key;

	internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
	{
		_modifier = modifier;
		_key = key;
	}
}
