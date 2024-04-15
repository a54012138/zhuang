#define DEBUG
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BondTech.HotkeyManagement.Win;
using Daigassou.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Daigassou;

public static class KeyBinding
{
	public static ArrayList hotkeyArrayList;

	private static Dictionary<int, int> _keymap = new Dictionary<int, int>
	{
		{ 48, 81 },
		{ 49, 49 },
		{ 50, 87 },
		{ 51, 50 },
		{ 52, 69 },
		{ 53, 82 },
		{ 54, 51 },
		{ 55, 84 },
		{ 56, 52 },
		{ 57, 89 },
		{ 58, 53 },
		{ 59, 85 },
		{ 60, 73 },
		{ 61, 54 },
		{ 62, 79 },
		{ 63, 55 },
		{ 64, 80 },
		{ 65, 65 },
		{ 66, 56 },
		{ 67, 83 },
		{ 68, 57 },
		{ 69, 68 },
		{ 70, 48 },
		{ 71, 70 },
		{ 72, 71 },
		{ 73, 189 },
		{ 74, 72 },
		{ 75, 187 },
		{ 76, 74 },
		{ 77, 75 },
		{ 78, 219 },
		{ 79, 76 },
		{ 80, 221 },
		{ 81, 90 },
		{ 82, 220 },
		{ 83, 88 },
		{ 84, 67 }
	};

	private static readonly Dictionary<string, Keys> _ctrKeyMap = new Dictionary<string, Keys>
	{
		{
			"OctaveLower",
			Keys.ShiftKey
		},
		{
			"OctaveHigher",
			Keys.ControlKey
		}
	};

	[DllImport("user32.dll")]
	private static extern uint MapVirtualKey(uint uCode, uint uMapType);

	public static char GetKeyChar(Keys k)
	{
		uint value = MapVirtualKey((uint)k, 2u);
		return Convert.ToChar(value);
	}

	public static Keys GetNoteToKey(int note)
	{
		if (!Settings.Default.IsEightKeyLayout)
		{
			return (Keys)_keymap[note];
		}
		if (note == 84)
		{
			return (Keys)_keymap[84];
		}
		return (Keys)_keymap[note % 12 + 60];
	}

	public static Keys GetNoteToCtrlKey(int note)
	{
		if (note < 60)
		{
			return _ctrKeyMap["OctaveLower"];
		}
		if (note > 71)
		{
			return _ctrKeyMap["OctaveHigher"];
		}
		return Keys.None;
	}

	public static void SetKeyToNote_22(int note, int keyValue)
	{
		_keymap[note] = keyValue;
		SaveConfig();
	}

	public static void SetKeyToNote_13(int note, int key)
	{
		int num = note % 12;
		if (note == 72)
		{
			_keymap[84] = key;
		}
		else
		{
			_keymap[48 + num] = key;
			_keymap[60 + num] = key;
			_keymap[72 + num] = key;
		}
		SaveConfig();
	}

	public static void SetCtrlKeyToNote(int note, Keys key)
	{
		if (note < 60)
		{
			_ctrKeyMap["OctaveLower"] = key;
		}
		else if (note > 71)
		{
			_ctrKeyMap["OctaveHigher"] = key;
		}
		SaveConfig();
	}

	public static void SaveConfig()
	{
		ArrayList arrayList = new ArrayList();
		foreach (KeyValuePair<int, int> item in _keymap)
		{
			arrayList.Add(item.Value);
		}
		ArrayList arrayList2 = new ArrayList();
		foreach (KeyValuePair<string, Keys> item2 in _ctrKeyMap)
		{
			arrayList2.Add(item2.Value);
		}
		if (Settings.Default.IsEightKeyLayout)
		{
			Settings.Default.KeyBinding13 = arrayList;
			Settings.Default.CtrlKeyBinding = arrayList2;
		}
		else
		{
			Settings.Default.KeyBinding37 = arrayList;
		}
		Settings.Default.HotKeyBinding = JsonConvert.SerializeObject((object)hotkeyArrayList);
		Settings.Default.Save();
	}

	public static void LoadConfig()
	{
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		ArrayList arrayList = Settings.Default.KeyBinding37;
		if (Settings.Default.IsEightKeyLayout)
		{
			arrayList = Settings.Default.KeyBinding13;
		}
		ArrayList ctrlKeyBinding = Settings.Default.CtrlKeyBinding;
		if (arrayList != null)
		{
			for (int i = 0; i < arrayList.Count; i++)
			{
				_keymap[i + 48] = (int)arrayList[i];
			}
		}
		if (ctrlKeyBinding != null)
		{
			_ctrKeyMap["OctaveLower"] = (Keys)ctrlKeyBinding[0];
			_ctrKeyMap["OctaveHigher"] = (Keys)ctrlKeyBinding[1];
		}
		ArrayList arrayList2 = JsonConvert.DeserializeObject<ArrayList>(Settings.Default.HotKeyBinding);
		hotkeyArrayList = new ArrayList();
		foreach (JObject item in arrayList2)
		{
			JObject val = item;
			hotkeyArrayList.Add((object)new GlobalHotKey(((object)val["Name"]).ToString(), (Modifiers)Extensions.Value<int>((IEnumerable<JToken>)val["Modifiers"]), (Keys)Extensions.Value<int>((IEnumerable<JToken>)val["Key"]), Extensions.Value<bool>((IEnumerable<JToken>)val["Enabled"])));
		}
	}

	public static string SaveConfigToFile()
	{
		string text = JsonConvert.SerializeObject((object)_keymap);
		Debug.WriteLine(text);
		return text;
	}

	public static void LoadConfigFromFile(string config)
	{
		try
		{
			Dictionary<int, int> keymap = JsonConvert.DeserializeObject<Dictionary<int, int>>(config);
			_keymap = keymap;
		}
		catch
		{
		}
		finally
		{
			SaveConfig();
		}
	}
}
