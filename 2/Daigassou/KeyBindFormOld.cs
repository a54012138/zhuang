using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Daigassou.Properties;

namespace Daigassou;

public class KeyBindFormOld : Form
{
	private const int NUMBER_OF_KEY = 37;

	private TextBox[] keyBoxs;

	private IContainer components = null;

	private Panel panel1;

	private TextBox textBox24;

	private TextBox textBox23;

	private TextBox textBox22;

	private TextBox textBox21;

	private TextBox textBox20;

	private TextBox textBox19;

	private TextBox textBox18;

	private TextBox textBox17;

	private TextBox textBox16;

	private TextBox textBox15;

	private TextBox textBox14;

	private TextBox textBox13;

	private TextBox textBox12;

	private TextBox textBox11;

	private TextBox textBox10;

	private TextBox textBox9;

	private TextBox textBox8;

	private TextBox textBox7;

	private TextBox textBox6;

	private TextBox textBox5;

	private TextBox textBox4;

	private TextBox textBox3;

	private TextBox textBox2;

	private TextBox textBox1;

	private Button btnReset;

	private Button btnApply;

	private TextBox textBox37;

	private TextBox textBox36;

	private TextBox textBox35;

	private TextBox textBox34;

	private TextBox textBox33;

	private TextBox textBox32;

	private TextBox textBox31;

	private TextBox textBox30;

	private TextBox textBox29;

	private TextBox textBox28;

	private TextBox textBox27;

	private TextBox textBox26;

	private TextBox textBox25;

	private Button btnExport;

	private Button btnImport;

	private OpenFileDialog ofdKey;

	private SaveFileDialog sfdKey;

	public KeyBindFormOld()
	{
		InitializeComponent();
		InitForm();
	}

	private void InitForm()
	{
		keyBoxs = new TextBox[37]
		{
			textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10,
			textBox11, textBox12, textBox13, textBox14, textBox15, textBox16, textBox17, textBox18, textBox19, textBox20,
			textBox21, textBox22, textBox23, textBox24, textBox25, textBox26, textBox27, textBox28, textBox29, textBox30,
			textBox31, textBox32, textBox33, textBox34, textBox35, textBox36, textBox37
		};
	}

	private void textBox_KeyDown(object sender, KeyEventArgs e)
	{
		TextBox textBox = (TextBox)sender;
		if (textBox == null)
		{
			throw new ArgumentNullException("tmpBox");
		}
		textBox.Text = e.KeyCode.ToString();
		KeyBinding.SetKeyToNote_22(Array.IndexOf(keyBoxs, textBox) + 48, e.KeyValue);
	}

	private void KeyBindForm_Load(object sender, EventArgs e)
	{
		KeyBinding.LoadConfig();
		updateDisplay();
	}

	private void updateDisplay()
	{
		for (int i = 0; i < 37; i++)
		{
			keyBoxs[i].Text = KeyBinding.GetKeyChar(KeyBinding.GetNoteToKey(i + 48)).ToString();
		}
	}

	private void KeyBindForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		e.Cancel = true;
		Hide();
	}

	private void btnApply_Click(object sender, EventArgs e)
	{
		KeyBinding.SaveConfig();
		Close();
	}

	private void btnReset_Click(object sender, EventArgs e)
	{
		MessageBox.Show("这个按钮是美工画的的，实际上并没有这个功能。");
	}

	private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		TextBox textBox = (TextBox)sender;
		if (textBox == null)
		{
			throw new ArgumentNullException("tmpBox");
		}
		textBox.Text = e.KeyChar.ToString();
	}

	private void BtnExport_Click(object sender, EventArgs e)
	{
		if (sfdKey.ShowDialog() == DialogResult.OK)
		{
			File.WriteAllText(sfdKey.FileName, KeyBinding.SaveConfigToFile());
		}
	}

	private void BtnImport_Click(object sender, EventArgs e)
	{
		if (ofdKey.ShowDialog() == DialogResult.OK)
		{
			string config = File.ReadAllText(ofdKey.FileName);
			KeyBinding.LoadConfigFromFile(config);
			updateDisplay();
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnExport = new System.Windows.Forms.Button();
		this.btnImport = new System.Windows.Forms.Button();
		this.btnReset = new System.Windows.Forms.Button();
		this.btnApply = new System.Windows.Forms.Button();
		this.textBox37 = new System.Windows.Forms.TextBox();
		this.textBox36 = new System.Windows.Forms.TextBox();
		this.textBox35 = new System.Windows.Forms.TextBox();
		this.textBox34 = new System.Windows.Forms.TextBox();
		this.textBox33 = new System.Windows.Forms.TextBox();
		this.textBox32 = new System.Windows.Forms.TextBox();
		this.textBox31 = new System.Windows.Forms.TextBox();
		this.textBox30 = new System.Windows.Forms.TextBox();
		this.textBox29 = new System.Windows.Forms.TextBox();
		this.textBox28 = new System.Windows.Forms.TextBox();
		this.textBox27 = new System.Windows.Forms.TextBox();
		this.textBox26 = new System.Windows.Forms.TextBox();
		this.textBox25 = new System.Windows.Forms.TextBox();
		this.textBox24 = new System.Windows.Forms.TextBox();
		this.textBox23 = new System.Windows.Forms.TextBox();
		this.textBox22 = new System.Windows.Forms.TextBox();
		this.textBox21 = new System.Windows.Forms.TextBox();
		this.textBox20 = new System.Windows.Forms.TextBox();
		this.textBox19 = new System.Windows.Forms.TextBox();
		this.textBox18 = new System.Windows.Forms.TextBox();
		this.textBox17 = new System.Windows.Forms.TextBox();
		this.textBox16 = new System.Windows.Forms.TextBox();
		this.textBox15 = new System.Windows.Forms.TextBox();
		this.textBox14 = new System.Windows.Forms.TextBox();
		this.textBox13 = new System.Windows.Forms.TextBox();
		this.textBox12 = new System.Windows.Forms.TextBox();
		this.textBox11 = new System.Windows.Forms.TextBox();
		this.textBox10 = new System.Windows.Forms.TextBox();
		this.textBox9 = new System.Windows.Forms.TextBox();
		this.textBox8 = new System.Windows.Forms.TextBox();
		this.textBox7 = new System.Windows.Forms.TextBox();
		this.textBox6 = new System.Windows.Forms.TextBox();
		this.textBox5 = new System.Windows.Forms.TextBox();
		this.textBox4 = new System.Windows.Forms.TextBox();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.ofdKey = new System.Windows.Forms.OpenFileDialog();
		this.sfdKey = new System.Windows.Forms.SaveFileDialog();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.panel1.BackgroundImage = Daigassou.Properties.Resources.key22;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Controls.Add(this.btnExport);
		this.panel1.Controls.Add(this.btnImport);
		this.panel1.Controls.Add(this.btnReset);
		this.panel1.Controls.Add(this.btnApply);
		this.panel1.Controls.Add(this.textBox37);
		this.panel1.Controls.Add(this.textBox36);
		this.panel1.Controls.Add(this.textBox35);
		this.panel1.Controls.Add(this.textBox34);
		this.panel1.Controls.Add(this.textBox33);
		this.panel1.Controls.Add(this.textBox32);
		this.panel1.Controls.Add(this.textBox31);
		this.panel1.Controls.Add(this.textBox30);
		this.panel1.Controls.Add(this.textBox29);
		this.panel1.Controls.Add(this.textBox28);
		this.panel1.Controls.Add(this.textBox27);
		this.panel1.Controls.Add(this.textBox26);
		this.panel1.Controls.Add(this.textBox25);
		this.panel1.Controls.Add(this.textBox24);
		this.panel1.Controls.Add(this.textBox23);
		this.panel1.Controls.Add(this.textBox22);
		this.panel1.Controls.Add(this.textBox21);
		this.panel1.Controls.Add(this.textBox20);
		this.panel1.Controls.Add(this.textBox19);
		this.panel1.Controls.Add(this.textBox18);
		this.panel1.Controls.Add(this.textBox17);
		this.panel1.Controls.Add(this.textBox16);
		this.panel1.Controls.Add(this.textBox15);
		this.panel1.Controls.Add(this.textBox14);
		this.panel1.Controls.Add(this.textBox13);
		this.panel1.Controls.Add(this.textBox12);
		this.panel1.Controls.Add(this.textBox11);
		this.panel1.Controls.Add(this.textBox10);
		this.panel1.Controls.Add(this.textBox9);
		this.panel1.Controls.Add(this.textBox8);
		this.panel1.Controls.Add(this.textBox7);
		this.panel1.Controls.Add(this.textBox6);
		this.panel1.Controls.Add(this.textBox5);
		this.panel1.Controls.Add(this.textBox4);
		this.panel1.Controls.Add(this.textBox3);
		this.panel1.Controls.Add(this.textBox2);
		this.panel1.Controls.Add(this.textBox1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(828, 326);
		this.panel1.TabIndex = 0;
		this.btnExport.BackColor = System.Drawing.Color.Transparent;
		this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnExport.Font = new System.Drawing.Font("微软雅黑", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnExport.Location = new System.Drawing.Point(106, 284);
		this.btnExport.Name = "btnExport";
		this.btnExport.Size = new System.Drawing.Size(63, 29);
		this.btnExport.TabIndex = 40;
		this.btnExport.Text = "导出";
		this.btnExport.UseVisualStyleBackColor = false;
		this.btnExport.Click += new System.EventHandler(BtnExport_Click);
		this.btnImport.BackColor = System.Drawing.Color.Transparent;
		this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnImport.Font = new System.Drawing.Font("微软雅黑", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnImport.Location = new System.Drawing.Point(33, 284);
		this.btnImport.Name = "btnImport";
		this.btnImport.Size = new System.Drawing.Size(63, 29);
		this.btnImport.TabIndex = 39;
		this.btnImport.Text = "导入";
		this.btnImport.UseVisualStyleBackColor = false;
		this.btnImport.Click += new System.EventHandler(BtnImport_Click);
		this.btnReset.BackColor = System.Drawing.Color.Transparent;
		this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnReset.Location = new System.Drawing.Point(548, 284);
		this.btnReset.Name = "btnReset";
		this.btnReset.Size = new System.Drawing.Size(119, 29);
		this.btnReset.TabIndex = 38;
		this.btnReset.UseVisualStyleBackColor = false;
		this.btnReset.Click += new System.EventHandler(btnReset_Click);
		this.btnApply.BackColor = System.Drawing.Color.Transparent;
		this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnApply.Location = new System.Drawing.Point(687, 284);
		this.btnApply.Name = "btnApply";
		this.btnApply.Size = new System.Drawing.Size(119, 29);
		this.btnApply.TabIndex = 37;
		this.btnApply.UseVisualStyleBackColor = false;
		this.btnApply.Click += new System.EventHandler(btnApply_Click);
		this.textBox37.Location = new System.Drawing.Point(769, 229);
		this.textBox37.Name = "textBox37";
		this.textBox37.ReadOnly = true;
		this.textBox37.Size = new System.Drawing.Size(31, 26);
		this.textBox37.TabIndex = 36;
		this.textBox37.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox37.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox36.Location = new System.Drawing.Point(734, 229);
		this.textBox36.Name = "textBox36";
		this.textBox36.ReadOnly = true;
		this.textBox36.Size = new System.Drawing.Size(31, 26);
		this.textBox36.TabIndex = 35;
		this.textBox36.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox36.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox35.Location = new System.Drawing.Point(718, 35);
		this.textBox35.Name = "textBox35";
		this.textBox35.ReadOnly = true;
		this.textBox35.Size = new System.Drawing.Size(31, 26);
		this.textBox35.TabIndex = 34;
		this.textBox35.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox35.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox34.Location = new System.Drawing.Point(699, 229);
		this.textBox34.Name = "textBox34";
		this.textBox34.ReadOnly = true;
		this.textBox34.Size = new System.Drawing.Size(31, 26);
		this.textBox34.TabIndex = 33;
		this.textBox34.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox34.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox33.Location = new System.Drawing.Point(681, 35);
		this.textBox33.Name = "textBox33";
		this.textBox33.ReadOnly = true;
		this.textBox33.Size = new System.Drawing.Size(31, 26);
		this.textBox33.TabIndex = 32;
		this.textBox33.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox33.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox32.Location = new System.Drawing.Point(664, 229);
		this.textBox32.Name = "textBox32";
		this.textBox32.ReadOnly = true;
		this.textBox32.Size = new System.Drawing.Size(31, 26);
		this.textBox32.TabIndex = 31;
		this.textBox32.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox32.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox31.Location = new System.Drawing.Point(645, 35);
		this.textBox31.Name = "textBox31";
		this.textBox31.ReadOnly = true;
		this.textBox31.Size = new System.Drawing.Size(31, 26);
		this.textBox31.TabIndex = 30;
		this.textBox31.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox31.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox30.Location = new System.Drawing.Point(629, 229);
		this.textBox30.Name = "textBox30";
		this.textBox30.ReadOnly = true;
		this.textBox30.Size = new System.Drawing.Size(31, 26);
		this.textBox30.TabIndex = 29;
		this.textBox30.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox30.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox29.Location = new System.Drawing.Point(594, 229);
		this.textBox29.Name = "textBox29";
		this.textBox29.ReadOnly = true;
		this.textBox29.Size = new System.Drawing.Size(31, 26);
		this.textBox29.TabIndex = 28;
		this.textBox29.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox29.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox28.Location = new System.Drawing.Point(575, 35);
		this.textBox28.Name = "textBox28";
		this.textBox28.ReadOnly = true;
		this.textBox28.Size = new System.Drawing.Size(31, 26);
		this.textBox28.TabIndex = 27;
		this.textBox28.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox28.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox27.Location = new System.Drawing.Point(560, 229);
		this.textBox27.Name = "textBox27";
		this.textBox27.ReadOnly = true;
		this.textBox27.Size = new System.Drawing.Size(31, 26);
		this.textBox27.TabIndex = 26;
		this.textBox27.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox27.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox26.Location = new System.Drawing.Point(541, 35);
		this.textBox26.Name = "textBox26";
		this.textBox26.ReadOnly = true;
		this.textBox26.Size = new System.Drawing.Size(31, 26);
		this.textBox26.TabIndex = 25;
		this.textBox26.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox26.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox25.Location = new System.Drawing.Point(525, 229);
		this.textBox25.Name = "textBox25";
		this.textBox25.ReadOnly = true;
		this.textBox25.Size = new System.Drawing.Size(31, 26);
		this.textBox25.TabIndex = 24;
		this.textBox25.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox25.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox24.Location = new System.Drawing.Point(487, 229);
		this.textBox24.Name = "textBox24";
		this.textBox24.ReadOnly = true;
		this.textBox24.Size = new System.Drawing.Size(31, 26);
		this.textBox24.TabIndex = 23;
		this.textBox24.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox24.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox23.Location = new System.Drawing.Point(471, 35);
		this.textBox23.Name = "textBox23";
		this.textBox23.ReadOnly = true;
		this.textBox23.Size = new System.Drawing.Size(31, 26);
		this.textBox23.TabIndex = 22;
		this.textBox23.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox23.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox22.Location = new System.Drawing.Point(453, 229);
		this.textBox22.Name = "textBox22";
		this.textBox22.ReadOnly = true;
		this.textBox22.Size = new System.Drawing.Size(31, 26);
		this.textBox22.TabIndex = 21;
		this.textBox22.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox22.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox21.Location = new System.Drawing.Point(435, 35);
		this.textBox21.Name = "textBox21";
		this.textBox21.ReadOnly = true;
		this.textBox21.Size = new System.Drawing.Size(31, 26);
		this.textBox21.TabIndex = 20;
		this.textBox21.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox21.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox20.Location = new System.Drawing.Point(418, 229);
		this.textBox20.Name = "textBox20";
		this.textBox20.ReadOnly = true;
		this.textBox20.Size = new System.Drawing.Size(31, 26);
		this.textBox20.TabIndex = 19;
		this.textBox20.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox20.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox19.Location = new System.Drawing.Point(399, 35);
		this.textBox19.Name = "textBox19";
		this.textBox19.ReadOnly = true;
		this.textBox19.Size = new System.Drawing.Size(31, 26);
		this.textBox19.TabIndex = 18;
		this.textBox19.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox19.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox18.Location = new System.Drawing.Point(383, 229);
		this.textBox18.Name = "textBox18";
		this.textBox18.ReadOnly = true;
		this.textBox18.Size = new System.Drawing.Size(31, 26);
		this.textBox18.TabIndex = 17;
		this.textBox18.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox18.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox17.Location = new System.Drawing.Point(348, 229);
		this.textBox17.Name = "textBox17";
		this.textBox17.ReadOnly = true;
		this.textBox17.Size = new System.Drawing.Size(31, 26);
		this.textBox17.TabIndex = 16;
		this.textBox17.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox17.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox16.Location = new System.Drawing.Point(329, 35);
		this.textBox16.Name = "textBox16";
		this.textBox16.ReadOnly = true;
		this.textBox16.Size = new System.Drawing.Size(31, 26);
		this.textBox16.TabIndex = 15;
		this.textBox16.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox16.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox15.Location = new System.Drawing.Point(313, 229);
		this.textBox15.Name = "textBox15";
		this.textBox15.ReadOnly = true;
		this.textBox15.Size = new System.Drawing.Size(31, 26);
		this.textBox15.TabIndex = 14;
		this.textBox15.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox15.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox14.Location = new System.Drawing.Point(294, 35);
		this.textBox14.Name = "textBox14";
		this.textBox14.ReadOnly = true;
		this.textBox14.Size = new System.Drawing.Size(31, 26);
		this.textBox14.TabIndex = 13;
		this.textBox14.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox14.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox13.Location = new System.Drawing.Point(279, 229);
		this.textBox13.Name = "textBox13";
		this.textBox13.ReadOnly = true;
		this.textBox13.Size = new System.Drawing.Size(31, 26);
		this.textBox13.TabIndex = 12;
		this.textBox13.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox13.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox12.Location = new System.Drawing.Point(242, 229);
		this.textBox12.Name = "textBox12";
		this.textBox12.ReadOnly = true;
		this.textBox12.Size = new System.Drawing.Size(31, 26);
		this.textBox12.TabIndex = 11;
		this.textBox12.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox12.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox11.Location = new System.Drawing.Point(225, 35);
		this.textBox11.Name = "textBox11";
		this.textBox11.ReadOnly = true;
		this.textBox11.Size = new System.Drawing.Size(31, 26);
		this.textBox11.TabIndex = 10;
		this.textBox11.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox11.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox10.Location = new System.Drawing.Point(208, 229);
		this.textBox10.Name = "textBox10";
		this.textBox10.ReadOnly = true;
		this.textBox10.Size = new System.Drawing.Size(31, 26);
		this.textBox10.TabIndex = 9;
		this.textBox10.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox10.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox9.Location = new System.Drawing.Point(190, 35);
		this.textBox9.Name = "textBox9";
		this.textBox9.ReadOnly = true;
		this.textBox9.Size = new System.Drawing.Size(31, 26);
		this.textBox9.TabIndex = 8;
		this.textBox9.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox9.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox8.Location = new System.Drawing.Point(173, 229);
		this.textBox8.Name = "textBox8";
		this.textBox8.ReadOnly = true;
		this.textBox8.Size = new System.Drawing.Size(31, 26);
		this.textBox8.TabIndex = 7;
		this.textBox8.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox7.Location = new System.Drawing.Point(155, 35);
		this.textBox7.Name = "textBox7";
		this.textBox7.ReadOnly = true;
		this.textBox7.Size = new System.Drawing.Size(31, 26);
		this.textBox7.TabIndex = 6;
		this.textBox7.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox6.Location = new System.Drawing.Point(138, 229);
		this.textBox6.Name = "textBox6";
		this.textBox6.ReadOnly = true;
		this.textBox6.Size = new System.Drawing.Size(31, 26);
		this.textBox6.TabIndex = 5;
		this.textBox6.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox5.Location = new System.Drawing.Point(103, 229);
		this.textBox5.Name = "textBox5";
		this.textBox5.ReadOnly = true;
		this.textBox5.Size = new System.Drawing.Size(31, 26);
		this.textBox5.TabIndex = 4;
		this.textBox5.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox4.Location = new System.Drawing.Point(83, 35);
		this.textBox4.Name = "textBox4";
		this.textBox4.ReadOnly = true;
		this.textBox4.Size = new System.Drawing.Size(31, 26);
		this.textBox4.TabIndex = 3;
		this.textBox4.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox3.Location = new System.Drawing.Point(68, 229);
		this.textBox3.Name = "textBox3";
		this.textBox3.ReadOnly = true;
		this.textBox3.Size = new System.Drawing.Size(31, 26);
		this.textBox3.TabIndex = 2;
		this.textBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox2.Location = new System.Drawing.Point(48, 35);
		this.textBox2.Name = "textBox2";
		this.textBox2.ReadOnly = true;
		this.textBox2.Size = new System.Drawing.Size(31, 26);
		this.textBox2.TabIndex = 1;
		this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.textBox1.Location = new System.Drawing.Point(33, 229);
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(31, 26);
		this.textBox1.TabIndex = 0;
		this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
		this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		this.ofdKey.Filter = "keyconfig|*.cfg";
		this.sfdKey.FileName = "key";
		this.sfdKey.Filter = "keyconfig|*.cfg";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 20f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(828, 326);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		base.Name = "KeyBindFormOld";
		this.Text = "按键绑定";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(KeyBindForm_FormClosing);
		base.Load += new System.EventHandler(KeyBindForm_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
