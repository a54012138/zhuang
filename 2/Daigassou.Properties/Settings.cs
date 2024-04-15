using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Daigassou.Properties;

[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
[CompilerGenerated]
public sealed class Settings : ApplicationSettingsBase
{
	private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

	public static Settings Default => defaultInstance;

	[UserScopedSetting]
	[DebuggerNonUserCode]
	public ArrayList KeyBinding13
	{
		get
		{
			return (ArrayList)this["KeyBinding13"];
		}
		set
		{
			this["KeyBinding13"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	public int StartKey
	{
		get
		{
			return (int)this["StartKey"];
		}
		set
		{
			this["StartKey"] = value;
		}
	}

	[DebuggerNonUserCode]
	[UserScopedSetting]
	public ArrayList CtrlKeyBinding
	{
		get
		{
			return (ArrayList)this["CtrlKeyBinding"];
		}
		set
		{
			this["CtrlKeyBinding"] = value;
		}
	}

	[DefaultSettingValue("False")]
	[DebuggerNonUserCode]
	[UserScopedSetting]
	public bool IsEightKeyLayout
	{
		get
		{
			return (bool)this["IsEightKeyLayout"];
		}
		set
		{
			this["IsEightKeyLayout"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	public ArrayList KeyBinding37
	{
		get
		{
			return (ArrayList)this["KeyBinding37"];
		}
		set
		{
			this["KeyBinding37"] = value;
		}
	}

	[UserScopedSetting]
	[DefaultSettingValue("50")]
	[DebuggerNonUserCode]
	public uint MinEventMs
	{
		get
		{
			return (uint)this["MinEventMs"];
		}
		set
		{
			this["MinEventMs"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("50")]
	public uint MinChordMs
	{
		get
		{
			return (uint)this["MinChordMs"];
		}
		set
		{
			this["MinChordMs"] = value;
		}
	}

	[DebuggerNonUserCode]
	[DefaultSettingValue("ntp3.aliyun.com")]
	[UserScopedSetting]
	public string NtpServer
	{
		get
		{
			return (string)this["NtpServer"];
		}
		set
		{
			this["NtpServer"] = value;
		}
	}

	[DebuggerNonUserCode]
	[UserScopedSetting]
	[DefaultSettingValue("False")]
	public bool AutoChord
	{
		get
		{
			return (bool)this["AutoChord"];
		}
		set
		{
			this["AutoChord"] = value;
		}
	}

	[DefaultSettingValue("[{\"Name\":\"Start\",\"Key\":121,\"Modifiers\":2,\"Enabled\":true},{\"Name\":\"Stop\",\"Key\":122,\"Modifiers\":2,\"Enabled\":true},{\"Name\":\"PitchUp\",\"Key\":119,\"Modifiers\":2,\"Enabled\":true},{\"Name\":\"PitchDown\",\"Key\":120,\"Modifiers\":2,\"Enabled\":true}]")]
	[DebuggerNonUserCode]
	[UserScopedSetting]
	public string HotKeyBinding
	{
		get
		{
			return (string)this["HotKeyBinding"];
		}
		set
		{
			this["HotKeyBinding"] = value;
		}
	}

	private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
	{
	}

	private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
	{
	}
}
