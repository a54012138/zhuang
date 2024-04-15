using System;
using System.Xml.Serialization;

namespace RainbowMage.OverlayPlugin;

[Serializable]
public class LabelOverlayConfig : OverlayConfigBase
{
	private string text;

	private string process;

	[XmlElement("Text")]
	public string Text
	{
		get
		{
			return text;
		}
		set
		{
			if (text != value)
			{
				text = value;
				if (this.TextChanged != null)
				{
					this.TextChanged(this, new TextChangedEventArgs(text));
				}
			}
		}
	}

	public string Process
	{
		get
		{
			return process;
		}
		set
		{
			if (process != value)
			{
				process = value;
				if (this.processChanged != null)
				{
					this.processChanged(this, new TextChangedEventArgs(process));
				}
			}
		}
	}

	public override Type OverlayType => typeof(StatusOverlay);

	public event EventHandler<TextChangedEventArgs> TextChanged;

	public event EventHandler<TextChangedEventArgs> processChanged;

	public LabelOverlayConfig(string name)
		: base(name)
	{
		Text = "";
	}

	private LabelOverlayConfig()
		: base(null)
	{
	}
}
