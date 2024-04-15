using System;

namespace RainbowMage.OverlayPlugin;

public class ThruStateChangedEventArgs : EventArgs
{
	public bool IsClickThru { get; private set; }

	public ThruStateChangedEventArgs(bool isClickThru)
	{
		IsClickThru = isClickThru;
	}
}
