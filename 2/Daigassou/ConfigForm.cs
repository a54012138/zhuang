using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BondTech.HotkeyManagement.Win;
using Daigassou.Forms;
using Daigassou.Properties;
using Daigassou.Utils;

namespace Daigassou;

public class ConfigForm : Form
{
	private int ClickCount;

	private readonly HotKeyManager hkm;

	private readonly KeyController kc;

	private readonly HotKeyControl[] keyBindings;

	private readonly ArrayList keyList;

	private IContainer components = null;

	internal HotKeyControl hotKeyControl4;

	internal HotKeyControl hotKeyControl3;

	internal HotKeyControl hotKeyControl2;

	private Label label6;

	private Label label9;

	internal HotKeyControl hotKeyControl1;

	private Label label8;

	private Label label7;

	private Label label1;

	private Label label2;

	private TextBox tbNtpServer;

	private Label label3;

	private CheckBox cbAutoChord;

	private Label label5;

	private NumericUpDown chordEventNum;

	private NumericUpDown minEventNum;

	private Panel panel1;

	private Panel panel3;

	internal HotKeyControl hotKeyControl5;

	private Label label11;

	private TabControl tabControl1;

	private TabPage tpKey;

	private TabPage tpPlaySetting;

	private TabPage tbLyric;

	private Label label10;

	private ComboBox cbSuffix;

	private Label label4;

	private Label label12;

	private CheckBox cbLrcEnable;

	private NumericUpDown nudPort;

	public ConfigForm(ArrayList _keyList, KeyController _kc, HotKeyManager _hkm)
	{
		keyList = _keyList;
		InitializeComponent();
		keyBindings = (HotKeyControl[])(object)new HotKeyControl[5] { hotKeyControl1, hotKeyControl2, hotKeyControl3, hotKeyControl4, hotKeyControl5 };
		kc = _kc;
		hkm = _hkm;
		InitValue();
		tabControl1.TabPages.Remove(tbLyric);
	}

	private void InitValue()
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		hkm.Enabled = false;
		minEventNum.Value = Settings.Default.MinEventMs;
		chordEventNum.Value = Settings.Default.MinChordMs;
		cbAutoChord.Checked = Settings.Default.AutoChord;
		tbNtpServer.Text = Settings.Default.NtpServer;
		foreach (GlobalHotKey enumerateGlobalHotKey in hkm.EnumerateGlobalHotKeys)
		{
			GlobalHotKey value = enumerateGlobalHotKey;
			int num = keyList.IndexOf(value);
			((Control)(object)keyBindings[num]).Text = ((object)(GlobalHotKey)keyList[num]).ToString().Split(';')[1].Trim();
		}
	}

	private void MinEventNum_NumChanged(object sender, EventArgs e)
	{
		Settings.Default.MinEventMs = (uint)minEventNum.Value;
		Settings.Default.Save();
	}

	private void ChordEventNum_NumChanged(object sender, EventArgs e)
	{
		Settings.Default.MinChordMs = (uint)chordEventNum.Value;
		Settings.Default.Save();
	}

	private void CbAutoChord_CheckedChangeEvent(object sender, EventArgs e)
	{
		Settings.Default.AutoChord = cbAutoChord.Checked;
		Settings.Default.Save();
	}

	private void TbNtpServer_TextChanged(object sender, EventArgs e)
	{
		Settings.Default.NtpServer = tbNtpServer.Text;
		Settings.Default.Save();
	}

	private void HotKeyControl1_HotKeyIsSet(object sender, HotKeyIsSetEventArgs e)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		HotKeyControl val = (HotKeyControl)((sender is HotKeyControl) ? sender : null);
		int index = Array.IndexOf(keyBindings, val);
		((GlobalHotKey)keyList[index]).Enabled = false;
		((GlobalHotKey)keyList[index]).Key = val.UserKey;
		((GlobalHotKey)keyList[index]).Modifier = val.UserModifier;
		((GlobalHotKey)keyList[index]).Enabled = true;
		KeyBinding.SaveConfig();
	}

	private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		HotKeyControl[] array = keyBindings;
		foreach (HotKeyControl val in array)
		{
			((GlobalHotKey)keyList[Array.IndexOf(keyBindings, val)]).Enabled = ((Control)(object)val).Text != string.Empty;
		}
		KeyBinding.SaveConfig();
		hkm.RemoveHotKey("Start");
		hkm.RemoveHotKey("Stop");
		hkm.RemoveHotKey("Pause");
		hkm.RemoveHotKey("PitchUp");
		hkm.RemoveHotKey("PitchDown");
		foreach (GlobalHotKey key in keyList)
		{
			GlobalHotKey val2 = key;
			if (val2.Enabled)
			{
				hkm.AddGlobalHotKey(val2);
			}
		}
		hkm.Enabled = true;
	}

	private void Panel2_Click(object sender, EventArgs e)
	{
		PidSelect pidSelect = new PidSelect();
		pidSelect.GetPid = (PidSelect.PidSelector)Delegate.Combine(pidSelect.GetPid, (PidSelect.PidSelector)delegate(int x)
		{
			kc.isBackGroundKey = true;
			kc.InitBackGroundKey(Process.GetProcessById(x).MainWindowHandle);
		});
		pidSelect.ShowDialog();
	}

	private void HotKeyControl1_HotKeyIsReset(object sender, EventArgs e)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		HotKeyControl value = (HotKeyControl)((sender is HotKeyControl) ? sender : null);
		int index = Array.IndexOf(keyBindings, value);
		((GlobalHotKey)keyList[index]).Enabled = false;
		KeyBinding.SaveConfig();
	}

	private void panel2_Paint(object sender, PaintEventArgs e)
	{
	}

	private void panel4_Paint(object sender, PaintEventArgs e)
	{
	}

	private void cbLrcEnable_CheckedChanged(object sender, EventArgs e)
	{
		lyricPoster.IsLrcEnable = cbLrcEnable.Checked;
	}

	private void nudPort_ValueChanged(object sender, EventArgs e)
	{
		lyricPoster.port = (uint)nudPort.Value;
	}

	private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void cbSuffix_SelectedIndexChanged(object sender, EventArgs e)
	{
		lyricPoster.suffix = cbSuffix.SelectedItem.ToString();
	}

	private void panel4_Click(object sender, EventArgs e)
	{
	}

	private void panel4_MouseClick(object sender, MouseEventArgs e)
	{
		if (!tabControl1.TabPages.Contains(tbLyric) && e.Button == MouseButtons.Middle)
		{
			tabControl1.TabPages.Add(tbLyric);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_0b8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b98: Expected O, but got Unknown
		//IL_0c5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c68: Expected O, but got Unknown
		//IL_0d2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d38: Expected O, but got Unknown
		//IL_0dfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e08: Expected O, but got Unknown
		//IL_0f6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f79: Expected O, but got Unknown
		this.label6 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.tbNtpServer = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.cbAutoChord = new System.Windows.Forms.CheckBox();
		this.label5 = new System.Windows.Forms.Label();
		this.chordEventNum = new System.Windows.Forms.NumericUpDown();
		this.minEventNum = new System.Windows.Forms.NumericUpDown();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.hotKeyControl5 = new HotKeyControl();
		this.hotKeyControl4 = new HotKeyControl();
		this.hotKeyControl3 = new HotKeyControl();
		this.hotKeyControl1 = new HotKeyControl();
		this.label11 = new System.Windows.Forms.Label();
		this.hotKeyControl2 = new HotKeyControl();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tpKey = new System.Windows.Forms.TabPage();
		this.tpPlaySetting = new System.Windows.Forms.TabPage();
		this.tbLyric = new System.Windows.Forms.TabPage();
		this.label10 = new System.Windows.Forms.Label();
		this.cbSuffix = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.cbLrcEnable = new System.Windows.Forms.CheckBox();
		this.nudPort = new System.Windows.Forms.NumericUpDown();
		((System.ComponentModel.ISupportInitialize)this.chordEventNum).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.minEventNum).BeginInit();
		this.panel1.SuspendLayout();
		this.panel3.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tpKey.SuspendLayout();
		this.tpPlaySetting.SuspendLayout();
		this.tbLyric.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudPort).BeginInit();
		base.SuspendLayout();
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(14, 18);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(65, 20);
		this.label6.TabIndex = 42;
		this.label6.Text = "开始演奏";
		this.label9.AutoSize = true;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.ForeColor = System.Drawing.Color.Gray;
		this.label9.Location = new System.Drawing.Point(15, 117);
		this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(65, 20);
		this.label9.TabIndex = 45;
		this.label9.Text = "向下移调";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(14, 84);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(65, 20);
		this.label8.TabIndex = 44;
		this.label8.Text = "向上移调";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(14, 51);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(65, 20);
		this.label7.TabIndex = 43;
		this.label7.Text = "结束演奏";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(4, 19);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(107, 20);
		this.label1.TabIndex = 33;
		this.label1.Text = "音符间最小间隔";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(4, 57);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(121, 20);
		this.label2.TabIndex = 34;
		this.label2.Text = "和弦解析最小间隔";
		this.tbNtpServer.ForeColor = System.Drawing.Color.Gray;
		this.tbNtpServer.Location = new System.Drawing.Point(140, 131);
		this.tbNtpServer.Name = "tbNtpServer";
		this.tbNtpServer.Size = new System.Drawing.Size(142, 26);
		this.tbNtpServer.TabIndex = 40;
		this.tbNtpServer.TextChanged += new System.EventHandler(TbNtpServer_TextChanged);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(4, 95);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(79, 20);
		this.label3.TabIndex = 35;
		this.label3.Text = "等比例琶音";
		this.cbAutoChord.AutoSize = true;
		this.cbAutoChord.Location = new System.Drawing.Point(140, 95);
		this.cbAutoChord.Name = "cbAutoChord";
		this.cbAutoChord.Size = new System.Drawing.Size(56, 24);
		this.cbAutoChord.TabIndex = 39;
		this.cbAutoChord.Text = "开启";
		this.cbAutoChord.UseVisualStyleBackColor = true;
		this.cbAutoChord.CheckedChanged += new System.EventHandler(CbAutoChord_CheckedChangeEvent);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(4, 135);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(79, 20);
		this.label5.TabIndex = 36;
		this.label5.Text = "NTP服务器";
		this.chordEventNum.ForeColor = System.Drawing.Color.Gray;
		this.chordEventNum.Location = new System.Drawing.Point(140, 55);
		this.chordEventNum.Maximum = new decimal(new int[4] { 200, 0, 0, 0 });
		this.chordEventNum.Minimum = new decimal(new int[4] { 40, 0, 0, 0 });
		this.chordEventNum.Name = "chordEventNum";
		this.chordEventNum.Size = new System.Drawing.Size(120, 26);
		this.chordEventNum.TabIndex = 38;
		this.chordEventNum.Value = new decimal(new int[4] { 50, 0, 0, 0 });
		this.chordEventNum.ValueChanged += new System.EventHandler(ChordEventNum_NumChanged);
		this.minEventNum.ForeColor = System.Drawing.Color.Gray;
		this.minEventNum.Location = new System.Drawing.Point(140, 13);
		this.minEventNum.Maximum = new decimal(new int[4] { 200, 0, 0, 0 });
		this.minEventNum.Minimum = new decimal(new int[4] { 40, 0, 0, 0 });
		this.minEventNum.Name = "minEventNum";
		this.minEventNum.Size = new System.Drawing.Size(120, 26);
		this.minEventNum.TabIndex = 37;
		this.minEventNum.Value = new decimal(new int[4] { 50, 0, 0, 0 });
		this.minEventNum.ValueChanged += new System.EventHandler(MinEventNum_NumChanged);
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.tbNtpServer);
		this.panel1.Controls.Add(this.label3);
		this.panel1.Controls.Add(this.cbAutoChord);
		this.panel1.Controls.Add(this.label5);
		this.panel1.Controls.Add(this.minEventNum);
		this.panel1.Controls.Add(this.chordEventNum);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(3, 3);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(295, 237);
		this.panel1.TabIndex = 50;
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.label6);
		this.panel3.Controls.Add(this.label7);
		this.panel3.Controls.Add((System.Windows.Forms.Control)(object)this.hotKeyControl5);
		this.panel3.Controls.Add((System.Windows.Forms.Control)(object)this.hotKeyControl4);
		this.panel3.Controls.Add(this.label8);
		this.panel3.Controls.Add((System.Windows.Forms.Control)(object)this.hotKeyControl3);
		this.panel3.Controls.Add((System.Windows.Forms.Control)(object)this.hotKeyControl1);
		this.panel3.Controls.Add(this.label11);
		this.panel3.Controls.Add((System.Windows.Forms.Control)(object)this.hotKeyControl2);
		this.panel3.Controls.Add(this.label9);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(3, 3);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(295, 237);
		this.panel3.TabIndex = 51;
		((System.Windows.Forms.Control)(object)this.hotKeyControl5).Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.hotKeyControl5.ForceModifiers = false;
		((System.Windows.Forms.Control)(object)this.hotKeyControl5).Location = new System.Drawing.Point(131, 151);
		((System.Windows.Forms.Control)(object)this.hotKeyControl5).Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		((System.Windows.Forms.Control)(object)this.hotKeyControl5).Name = "hotKeyControl5";
		((System.Windows.Forms.Control)(object)this.hotKeyControl5).Size = new System.Drawing.Size(150, 23);
		((System.Windows.Forms.Control)(object)this.hotKeyControl5).TabIndex = 48;
		this.hotKeyControl5.ToolTip = null;
		this.hotKeyControl5.HotKeyIsSet += new HotKeyIsSetEventHandler(HotKeyControl1_HotKeyIsSet);
		this.hotKeyControl5.HotKeyIsReset += new System.EventHandler(HotKeyControl1_HotKeyIsReset);
		((System.Windows.Forms.Control)(object)this.hotKeyControl4).Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.hotKeyControl4.ForceModifiers = false;
		((System.Windows.Forms.Control)(object)this.hotKeyControl4).Location = new System.Drawing.Point(131, 117);
		((System.Windows.Forms.Control)(object)this.hotKeyControl4).Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		((System.Windows.Forms.Control)(object)this.hotKeyControl4).Name = "hotKeyControl4";
		((System.Windows.Forms.Control)(object)this.hotKeyControl4).Size = new System.Drawing.Size(150, 23);
		((System.Windows.Forms.Control)(object)this.hotKeyControl4).TabIndex = 48;
		this.hotKeyControl4.ToolTip = null;
		this.hotKeyControl4.HotKeyIsSet += new HotKeyIsSetEventHandler(HotKeyControl1_HotKeyIsSet);
		this.hotKeyControl4.HotKeyIsReset += new System.EventHandler(HotKeyControl1_HotKeyIsReset);
		((System.Windows.Forms.Control)(object)this.hotKeyControl3).Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.hotKeyControl3.ForceModifiers = false;
		((System.Windows.Forms.Control)(object)this.hotKeyControl3).Location = new System.Drawing.Point(131, 84);
		((System.Windows.Forms.Control)(object)this.hotKeyControl3).Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		((System.Windows.Forms.Control)(object)this.hotKeyControl3).Name = "hotKeyControl3";
		((System.Windows.Forms.Control)(object)this.hotKeyControl3).Size = new System.Drawing.Size(150, 23);
		((System.Windows.Forms.Control)(object)this.hotKeyControl3).TabIndex = 47;
		this.hotKeyControl3.ToolTip = null;
		this.hotKeyControl3.HotKeyIsSet += new HotKeyIsSetEventHandler(HotKeyControl1_HotKeyIsSet);
		this.hotKeyControl3.HotKeyIsReset += new System.EventHandler(HotKeyControl1_HotKeyIsReset);
		((System.Windows.Forms.Control)(object)this.hotKeyControl1).Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.hotKeyControl1.ForceModifiers = false;
		((System.Windows.Forms.Control)(object)this.hotKeyControl1).Location = new System.Drawing.Point(131, 18);
		((System.Windows.Forms.Control)(object)this.hotKeyControl1).Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		((System.Windows.Forms.Control)(object)this.hotKeyControl1).Name = "hotKeyControl1";
		((System.Windows.Forms.Control)(object)this.hotKeyControl1).Size = new System.Drawing.Size(150, 23);
		((System.Windows.Forms.Control)(object)this.hotKeyControl1).TabIndex = 41;
		this.hotKeyControl1.ToolTip = null;
		this.hotKeyControl1.HotKeyIsSet += new HotKeyIsSetEventHandler(HotKeyControl1_HotKeyIsSet);
		this.hotKeyControl1.HotKeyIsReset += new System.EventHandler(HotKeyControl1_HotKeyIsReset);
		this.label11.AutoSize = true;
		this.label11.BackColor = System.Drawing.Color.Transparent;
		this.label11.ForeColor = System.Drawing.Color.Gray;
		this.label11.Location = new System.Drawing.Point(15, 150);
		this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 20);
		this.label11.TabIndex = 45;
		this.label11.Text = "暂停演奏";
		((System.Windows.Forms.Control)(object)this.hotKeyControl2).Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.hotKeyControl2.ForceModifiers = false;
		((System.Windows.Forms.Control)(object)this.hotKeyControl2).Location = new System.Drawing.Point(131, 51);
		((System.Windows.Forms.Control)(object)this.hotKeyControl2).Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		((System.Windows.Forms.Control)(object)this.hotKeyControl2).Name = "hotKeyControl2";
		((System.Windows.Forms.Control)(object)this.hotKeyControl2).Size = new System.Drawing.Size(150, 23);
		((System.Windows.Forms.Control)(object)this.hotKeyControl2).TabIndex = 46;
		this.hotKeyControl2.ToolTip = null;
		this.hotKeyControl2.HotKeyIsSet += new HotKeyIsSetEventHandler(HotKeyControl1_HotKeyIsSet);
		this.hotKeyControl2.HotKeyIsReset += new System.EventHandler(HotKeyControl1_HotKeyIsReset);
		this.tabControl1.Controls.Add(this.tpKey);
		this.tabControl1.Controls.Add(this.tpPlaySetting);
		this.tabControl1.Controls.Add(this.tbLyric);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Location = new System.Drawing.Point(0, 0);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(309, 276);
		this.tabControl1.TabIndex = 53;
		this.tpKey.Controls.Add(this.panel3);
		this.tpKey.Location = new System.Drawing.Point(4, 29);
		this.tpKey.Name = "tpKey";
		this.tpKey.Padding = new System.Windows.Forms.Padding(3);
		this.tpKey.Size = new System.Drawing.Size(301, 243);
		this.tpKey.TabIndex = 0;
		this.tpKey.Text = "快捷键绑定";
		this.tpKey.UseVisualStyleBackColor = true;
		this.tpPlaySetting.Controls.Add(this.panel1);
		this.tpPlaySetting.Location = new System.Drawing.Point(4, 29);
		this.tpPlaySetting.Name = "tpPlaySetting";
		this.tpPlaySetting.Padding = new System.Windows.Forms.Padding(3);
		this.tpPlaySetting.Size = new System.Drawing.Size(301, 243);
		this.tpPlaySetting.TabIndex = 1;
		this.tpPlaySetting.Text = "播放参数";
		this.tpPlaySetting.UseVisualStyleBackColor = true;
		this.tbLyric.Controls.Add(this.label10);
		this.tbLyric.Controls.Add(this.cbSuffix);
		this.tbLyric.Controls.Add(this.label4);
		this.tbLyric.Controls.Add(this.label12);
		this.tbLyric.Controls.Add(this.cbLrcEnable);
		this.tbLyric.Controls.Add(this.nudPort);
		this.tbLyric.Location = new System.Drawing.Point(4, 29);
		this.tbLyric.Name = "tbLyric";
		this.tbLyric.Padding = new System.Windows.Forms.Padding(3);
		this.tbLyric.Size = new System.Drawing.Size(301, 243);
		this.tbLyric.TabIndex = 2;
		this.tbLyric.Text = "歌词设置";
		this.tbLyric.UseVisualStyleBackColor = true;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(9, 98);
		this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(65, 20);
		this.label10.TabIndex = 49;
		this.label10.Text = "消息类别";
		this.cbSuffix.ForeColor = System.Drawing.Color.Gray;
		this.cbSuffix.FormattingEnabled = true;
		this.cbSuffix.Items.AddRange(new object[5] { "/说话频道", "/呼喊频道", "/喊话频道", "/小队频道", "/感情表现 " });
		this.cbSuffix.Location = new System.Drawing.Point(144, 95);
		this.cbSuffix.Name = "cbSuffix";
		this.cbSuffix.Size = new System.Drawing.Size(121, 28);
		this.cbSuffix.TabIndex = 48;
		this.cbSuffix.SelectedIndexChanged += new System.EventHandler(cbSuffix_SelectedIndexChanged);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(9, 58);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(93, 20);
		this.label4.TabIndex = 41;
		this.label4.Text = "鲶鱼精端口号";
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(9, 21);
		this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(65, 20);
		this.label12.TabIndex = 43;
		this.label12.Text = "歌词功能";
		this.cbLrcEnable.AutoSize = true;
		this.cbLrcEnable.Location = new System.Drawing.Point(145, 21);
		this.cbLrcEnable.Name = "cbLrcEnable";
		this.cbLrcEnable.Size = new System.Drawing.Size(56, 24);
		this.cbLrcEnable.TabIndex = 47;
		this.cbLrcEnable.Text = "开启";
		this.cbLrcEnable.UseVisualStyleBackColor = true;
		this.cbLrcEnable.CheckedChanged += new System.EventHandler(cbLrcEnable_CheckedChanged);
		this.nudPort.ForeColor = System.Drawing.Color.Gray;
		this.nudPort.Location = new System.Drawing.Point(144, 56);
		this.nudPort.Maximum = new decimal(new int[4] { 65535, 0, 0, 0 });
		this.nudPort.Name = "nudPort";
		this.nudPort.Size = new System.Drawing.Size(120, 26);
		this.nudPort.TabIndex = 45;
		this.nudPort.Value = new decimal(new int[4] { 2345, 0, 0, 0 });
		this.nudPort.ValueChanged += new System.EventHandler(nudPort_ValueChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 20f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.ClientSize = new System.Drawing.Size(309, 276);
		base.Controls.Add(this.tabControl1);
		this.Font = new System.Drawing.Font("微软雅黑", 10.5f);
		this.ForeColor = System.Drawing.Color.Gray;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		base.Name = "ConfigForm";
		this.Text = "快捷键设置";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ConfigForm_FormClosing);
		((System.ComponentModel.ISupportInitialize)this.chordEventNum).EndInit();
		((System.ComponentModel.ISupportInitialize)this.minEventNum).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.tabControl1.ResumeLayout(false);
		this.tpKey.ResumeLayout(false);
		this.tpPlaySetting.ResumeLayout(false);
		this.tbLyric.ResumeLayout(false);
		this.tbLyric.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudPort).EndInit();
		base.ResumeLayout(false);
	}
}
