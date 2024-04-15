using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RainbowMage.OverlayPlugin;

[Serializable]
public abstract class OverlayConfigBase : IOverlayConfig
{
	private bool isVisible;

	private bool isClickThru;

	private string url;

	private int maxFrameRate;

	private bool globalHotkeyEnabled;

	private Keys globalHotkey;

	private Keys globalHotkeyModifiers;

	private GlobalHotkeyType globalHotkeyType;

	private bool isLocked;

	[XmlElement("Name")]
	public string Name { get; set; }

	[XmlElement("IsVisible")]
	public bool IsVisible
	{
		get
		{
			return isVisible;
		}
		set
		{
			if (isVisible != value)
			{
				isVisible = value;
				if (this.VisibleChanged != null)
				{
					this.VisibleChanged(this, new VisibleStateChangedEventArgs(isVisible));
				}
			}
		}
	}

	[XmlElement("IsClickThru")]
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
				if (this.ClickThruChanged != null)
				{
					this.ClickThruChanged(this, new ThruStateChangedEventArgs(isClickThru));
				}
			}
		}
	}

	[XmlElement("Position")]
	public Point Position { get; set; }

	[XmlElement("Size")]
	public Size Size { get; set; }

	[XmlElement("Url")]
	public string Url
	{
		get
		{
			return url;
		}
		set
		{
			if (url != value)
			{
				url = value;
				if (this.UrlChanged != null)
				{
					this.UrlChanged(this, new UrlChangedEventArgs(url));
				}
			}
		}
	}

	[XmlElement("MaxFrameRate")]
	public int MaxFrameRate
	{
		get
		{
			return maxFrameRate;
		}
		set
		{
			if (maxFrameRate != value)
			{
				maxFrameRate = value;
				if (this.MaxFrameRateChanged != null)
				{
					this.MaxFrameRateChanged(this, new MaxFrameRateChangedEventArgs(maxFrameRate));
				}
			}
		}
	}

	[XmlElement("GlobalHotkeyEnabled")]
	public bool GlobalHotkeyEnabled
	{
		get
		{
			return globalHotkeyEnabled;
		}
		set
		{
			if (globalHotkeyEnabled != value)
			{
				globalHotkeyEnabled = value;
				if (this.GlobalHotkeyEnabledChanged != null)
				{
					this.GlobalHotkeyEnabledChanged(this, new GlobalHotkeyEnabledChangedEventArgs(globalHotkeyEnabled));
				}
			}
		}
	}

	[XmlElement("GlobalHotkey")]
	public Keys GlobalHotkey
	{
		get
		{
			return globalHotkey;
		}
		set
		{
			if (globalHotkey != value)
			{
				globalHotkey = value;
				if (this.GlobalHotkeyChanged != null)
				{
					this.GlobalHotkeyChanged(this, new GlobalHotkeyChangedEventArgs(globalHotkey));
				}
			}
		}
	}

	[XmlElement("GlobalHotkeyModifiers")]
	public Keys GlobalHotkeyModifiers
	{
		get
		{
			return globalHotkeyModifiers;
		}
		set
		{
			if (globalHotkeyModifiers != value)
			{
				globalHotkeyModifiers = value;
				if (this.GlobalHotkeyModifiersChanged != null)
				{
					this.GlobalHotkeyModifiersChanged(this, new GlobalHotkeyChangedEventArgs(globalHotkeyModifiers));
				}
			}
		}
	}

	[XmlElement("GlobalHotkeyType")]
	public GlobalHotkeyType GlobalHotkeyType
	{
		get
		{
			return globalHotkeyType;
		}
		set
		{
			if (globalHotkeyType != value)
			{
				globalHotkeyType = value;
				if (this.GlobalHotkeyTypeChanged != null)
				{
					this.GlobalHotkeyTypeChanged(this, new GlobalHotkeyTypeChangedEventArgs(globalHotkeyType));
				}
			}
		}
	}

	[XmlElement("IsLocked")]
	public bool IsLocked
	{
		get
		{
			return isLocked;
		}
		set
		{
			if (isLocked != value)
			{
				isLocked = value;
				if (this.LockChanged != null)
				{
					this.LockChanged(this, new LockStateChangedEventArgs(isLocked));
				}
			}
		}
	}

	[XmlIgnore]
	public abstract Type OverlayType { get; }

	public event EventHandler<VisibleStateChangedEventArgs> VisibleChanged;

	public event EventHandler<ThruStateChangedEventArgs> ClickThruChanged;

	public event EventHandler<UrlChangedEventArgs> UrlChanged;

	public event EventHandler<MaxFrameRateChangedEventArgs> MaxFrameRateChanged;

	public event EventHandler<GlobalHotkeyEnabledChangedEventArgs> GlobalHotkeyEnabledChanged;

	public event EventHandler<GlobalHotkeyChangedEventArgs> GlobalHotkeyChanged;

	public event EventHandler<GlobalHotkeyChangedEventArgs> GlobalHotkeyModifiersChanged;

	public event EventHandler<LockStateChangedEventArgs> LockChanged;

	public event EventHandler<GlobalHotkeyTypeChangedEventArgs> GlobalHotkeyTypeChanged;

	protected OverlayConfigBase(string name)
	{
		Name = name;
		IsVisible = true;
		IsClickThru = false;
		Position = new Point(20, 20);
		Size = new Size(300, 300);
		Url = "";
		MaxFrameRate = 30;
		globalHotkeyEnabled = false;
		GlobalHotkey = Keys.None;
		globalHotkeyModifiers = Keys.None;
		globalHotkeyType = GlobalHotkeyType.ToggleVisible;
	}
}
