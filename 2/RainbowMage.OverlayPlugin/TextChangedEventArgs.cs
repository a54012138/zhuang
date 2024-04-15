using System;

namespace RainbowMage.OverlayPlugin;

public class TextChangedEventArgs : EventArgs
{
	public string Text { get; private set; }

	public TextChangedEventArgs(string text)
	{
		Text = text;
	}
}
