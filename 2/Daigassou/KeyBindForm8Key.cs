using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Daigassou.Properties;

namespace Daigassou;

public class KeyBindForm8Key : Form
{
	private TextBox[] keyBoxs = new TextBox[13];

	private const int OCTAVE_KEY_LOW = 59;

	private const int OCTAVE_KEY_HIGH = 72;

	private IContainer components = null;

	private TextBox textBox1;

	private TextBox textBox2;

	private TextBox textBox3;

	private TextBox textBox4;

	private TextBox textBox5;

	private TextBox textBox6;

	private TextBox textBox7;

	private TextBox textBox8;

	private TextBox textBox9;

	private TextBox textBox10;

	private TextBox textBox11;

	private TextBox textBox12;

	private TextBox textBox13;

	private ComboBox cbOctaveHigher;

	private ComboBox cbOctaveLower;

	private Button button1;

	private Button button2;

	public KeyBindForm8Key()
	{
		InitializeComponent();
		InitForm();
	}

	private void InitForm()
	{
		keyBoxs = new TextBox[13]
		{
			textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10,
			textBox11, textBox12, textBox13
		};
		for (int i = 0; i < 13; i++)
		{
			keyBoxs[i].KeyDown += textBox_KeyDown;
		}
	}

	private void textBox_KeyDown(object sender, KeyEventArgs e)
	{
		TextBox textBox = (TextBox)sender;
		if (textBox == null)
		{
			throw new ArgumentNullException("tmpBox");
		}
		textBox.Text = e.KeyCode.ToString();
		KeyBinding.SetKeyToNote_13(Array.IndexOf(keyBoxs, textBox) + 60, e.KeyValue);
	}

	private void KeyBindForm_Load(object sender, EventArgs e)
	{
		KeyBinding.LoadConfig();
		for (int i = 0; i < 13; i++)
		{
			keyBoxs[i].Text = KeyBinding.GetKeyChar(KeyBinding.GetNoteToKey(i + 72)).ToString();
		}
		Keys noteToCtrlKey = KeyBinding.GetNoteToCtrlKey(59);
		Keys noteToCtrlKey2 = KeyBinding.GetNoteToCtrlKey(72);
		switch (noteToCtrlKey)
		{
		case Keys.ControlKey:
			cbOctaveLower.Text = "Ctrl";
			break;
		case Keys.Menu:
			cbOctaveLower.Text = "Alt";
			break;
		case Keys.ShiftKey:
			cbOctaveLower.Text = "Shift";
			break;
		}
		switch (noteToCtrlKey2)
		{
		case Keys.ControlKey:
			cbOctaveHigher.Text = "Ctrl";
			break;
		case Keys.Menu:
			cbOctaveHigher.Text = "Alt";
			break;
		case Keys.ShiftKey:
			cbOctaveHigher.Text = "Shift";
			break;
		}
	}

	private void KeyBindForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		e.Cancel = true;
		Hide();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void cbOctaveHigh_SelectedIndexChanged(object sender, EventArgs e)
	{
		string selectedItem = cbOctaveLower.Text;
		switch (cbOctaveHigher.Text)
		{
		case "Ctrl":
			KeyBinding.SetCtrlKeyToNote(72, Keys.ControlKey);
			break;
		case "Alt":
			KeyBinding.SetCtrlKeyToNote(72, Keys.Menu);
			break;
		case "Shift":
			KeyBinding.SetCtrlKeyToNote(72, Keys.ShiftKey);
			break;
		}
		List<string> list = new List<string> { "Ctrl", "Alt", "Shift" };
		list.Remove(cbOctaveHigher.Text);
		cbOctaveLower.SelectedIndexChanged -= cbOctaveLow_SelectedIndexChanged;
		cbOctaveLower.DataSource = list;
		cbOctaveLower.SelectedItem = selectedItem;
		cbOctaveLower.SelectedIndexChanged += cbOctaveLow_SelectedIndexChanged;
	}

	private void cbOctaveLow_SelectedIndexChanged(object sender, EventArgs e)
	{
		string selectedItem = cbOctaveHigher.Text;
		switch (cbOctaveLower.Text)
		{
		case "Ctrl":
			KeyBinding.SetCtrlKeyToNote(59, Keys.ControlKey);
			break;
		case "Alt":
			KeyBinding.SetCtrlKeyToNote(59, Keys.Menu);
			break;
		case "Shift":
			KeyBinding.SetCtrlKeyToNote(59, Keys.ShiftKey);
			break;
		}
		List<string> list = new List<string> { "Ctrl", "Alt", "Shift" };
		list.Remove(cbOctaveLower.Text);
		cbOctaveHigher.SelectedIndexChanged -= cbOctaveHigh_SelectedIndexChanged;
		cbOctaveHigher.DataSource = list;
		cbOctaveHigher.SelectedItem = selectedItem;
		cbOctaveHigher.SelectedIndexChanged += cbOctaveHigh_SelectedIndexChanged;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		Close();
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
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.textBox4 = new System.Windows.Forms.TextBox();
		this.textBox5 = new System.Windows.Forms.TextBox();
		this.textBox6 = new System.Windows.Forms.TextBox();
		this.textBox7 = new System.Windows.Forms.TextBox();
		this.textBox8 = new System.Windows.Forms.TextBox();
		this.textBox9 = new System.Windows.Forms.TextBox();
		this.textBox10 = new System.Windows.Forms.TextBox();
		this.textBox11 = new System.Windows.Forms.TextBox();
		this.textBox12 = new System.Windows.Forms.TextBox();
		this.textBox13 = new System.Windows.Forms.TextBox();
		this.cbOctaveHigher = new System.Windows.Forms.ComboBox();
		this.cbOctaveLower = new System.Windows.Forms.ComboBox();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.textBox1.BackColor = System.Drawing.Color.White;
		this.textBox1.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.textBox1.ForeColor = System.Drawing.Color.DarkGray;
		this.textBox1.Location = new System.Drawing.Point(330, 336);
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(111, 26);
		this.textBox1.TabIndex = 0;
		this.textBox1.Text = "请设置";
		this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox2.BackColor = System.Drawing.Color.White;
		this.textBox2.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.textBox2.ForeColor = System.Drawing.Color.DarkGray;
		this.textBox2.Location = new System.Drawing.Point(43, 313);
		this.textBox2.Name = "textBox2";
		this.textBox2.ReadOnly = true;
		this.textBox2.Size = new System.Drawing.Size(111, 26);
		this.textBox2.TabIndex = 1;
		this.textBox2.Text = "请设置";
		this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox3.BackColor = System.Drawing.Color.White;
		this.textBox3.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.textBox3.ForeColor = System.Drawing.Color.DarkGray;
		this.textBox3.Location = new System.Drawing.Point(330, 294);
		this.textBox3.Name = "textBox3";
		this.textBox3.ReadOnly = true;
		this.textBox3.Size = new System.Drawing.Size(111, 26);
		this.textBox3.TabIndex = 2;
		this.textBox3.Text = "请设置";
		this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox4.BackColor = System.Drawing.Color.White;
		this.textBox4.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.textBox4.ForeColor = System.Drawing.Color.DarkGray;
		this.textBox4.Location = new System.Drawing.Point(43, 272);
		this.textBox4.Name = "textBox4";
		this.textBox4.ReadOnly = true;
		this.textBox4.Size = new System.Drawing.Size(111, 26);
		this.textBox4.TabIndex = 3;
		this.textBox4.Text = "请设置";
		this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox5.BackColor = System.Drawing.Color.White;
		this.textBox5.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.textBox5.ForeColor = System.Drawing.Color.DarkGray;
		this.textBox5.Location = new System.Drawing.Point(330, 250);
		this.textBox5.Name = "textBox5";
		this.textBox5.ReadOnly = true;
		this.textBox5.Size = new System.Drawing.Size(111, 26);
		this.textBox5.TabIndex = 4;
		this.textBox5.Text = "请设置";
		this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox6.BackColor = System.Drawing.Color.White;
		this.textBox6.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.textBox6.ForeColor = System.Drawing.Color.DarkGray;
		this.textBox6.Location = new System.Drawing.Point(330, 209);
		this.textBox6.Name = "textBox6";
		this.textBox6.ReadOnly = true;
		this.textBox6.Size = new System.Drawing.Size(111, 26);
		this.textBox6.TabIndex = 5;
		this.textBox6.Text = "请设置";
		this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox7.BackColor = System.Drawing.Color.White;
		this.textBox7.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.textBox7.ForeColor = System.Drawing.Color.DarkGray;
		this.textBox7.Location = new System.Drawing.Point(43, 189);
		this.textBox7.Name = "textBox7";
		this.textBox7.ReadOnly = true;
		this.textBox7.Size = new System.Drawing.Size(111, 26);
		this.textBox7.TabIndex = 6;
		this.textBox7.Text = "请设置";
		this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox8.BackColor = System.Drawing.Color.White;
		this.textBox8.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.textBox8.ForeColor = System.Drawing.Color.DarkGray;
		this.textBox8.Location = new System.Drawing.Point(330, 166);
		this.textBox8.Name = "textBox8";
		this.textBox8.ReadOnly = true;
		this.textBox8.Size = new System.Drawing.Size(111, 26);
		this.textBox8.TabIndex = 7;
		this.textBox8.Text = "请设置";
		this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox9.BackColor = System.Drawing.Color.White;
		this.textBox9.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.textBox9.ForeColor = System.Drawing.Color.DarkGray;
		this.textBox9.Location = new System.Drawing.Point(43, 145);
		this.textBox9.Name = "textBox9";
		this.textBox9.ReadOnly = true;
		this.textBox9.Size = new System.Drawing.Size(111, 26);
		this.textBox9.TabIndex = 8;
		this.textBox9.Text = "请设置";
		this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox10.BackColor = System.Drawing.Color.White;
		this.textBox10.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.textBox10.ForeColor = System.Drawing.Color.DarkGray;
		this.textBox10.Location = new System.Drawing.Point(330, 125);
		this.textBox10.Name = "textBox10";
		this.textBox10.ReadOnly = true;
		this.textBox10.Size = new System.Drawing.Size(111, 26);
		this.textBox10.TabIndex = 9;
		this.textBox10.Text = "请设置";
		this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox11.BackColor = System.Drawing.Color.White;
		this.textBox11.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.textBox11.ForeColor = System.Drawing.Color.DarkGray;
		this.textBox11.Location = new System.Drawing.Point(43, 104);
		this.textBox11.Name = "textBox11";
		this.textBox11.ReadOnly = true;
		this.textBox11.Size = new System.Drawing.Size(111, 26);
		this.textBox11.TabIndex = 10;
		this.textBox11.Text = "请设置";
		this.textBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox12.BackColor = System.Drawing.Color.White;
		this.textBox12.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.textBox12.ForeColor = System.Drawing.Color.DarkGray;
		this.textBox12.Location = new System.Drawing.Point(330, 81);
		this.textBox12.Name = "textBox12";
		this.textBox12.ReadOnly = true;
		this.textBox12.Size = new System.Drawing.Size(111, 26);
		this.textBox12.TabIndex = 11;
		this.textBox12.Text = "请设置";
		this.textBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox13.BackColor = System.Drawing.Color.White;
		this.textBox13.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.textBox13.ForeColor = System.Drawing.Color.DarkGray;
		this.textBox13.Location = new System.Drawing.Point(330, 40);
		this.textBox13.Name = "textBox13";
		this.textBox13.ReadOnly = true;
		this.textBox13.Size = new System.Drawing.Size(111, 26);
		this.textBox13.TabIndex = 12;
		this.textBox13.Text = "请设置";
		this.textBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.cbOctaveHigher.FormattingEnabled = true;
		this.cbOctaveHigher.Items.AddRange(new object[3] { "Shift", "Alt", "Ctrl" });
		this.cbOctaveHigher.Location = new System.Drawing.Point(330, 384);
		this.cbOctaveHigher.Name = "cbOctaveHigher";
		this.cbOctaveHigher.Size = new System.Drawing.Size(111, 28);
		this.cbOctaveHigher.TabIndex = 13;
		this.cbOctaveHigher.Text = "Ctrl";
		this.cbOctaveHigher.SelectedIndexChanged += new System.EventHandler(cbOctaveHigh_SelectedIndexChanged);
		this.cbOctaveLower.FormattingEnabled = true;
		this.cbOctaveLower.Items.AddRange(new object[3] { "Alt", "Ctrl", "Shift" });
		this.cbOctaveLower.Location = new System.Drawing.Point(330, 424);
		this.cbOctaveLower.Name = "cbOctaveLower";
		this.cbOctaveLower.Size = new System.Drawing.Size(111, 28);
		this.cbOctaveLower.TabIndex = 14;
		this.cbOctaveLower.Text = "Shift";
		this.cbOctaveLower.SelectedIndexChanged += new System.EventHandler(cbOctaveLow_SelectedIndexChanged);
		this.button1.BackColor = System.Drawing.Color.Transparent;
		this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button1.ForeColor = System.Drawing.Color.Transparent;
		this.button1.Location = new System.Drawing.Point(262, 501);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(123, 29);
		this.button1.TabIndex = 15;
		this.button1.UseVisualStyleBackColor = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.BackColor = System.Drawing.Color.Transparent;
		this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button2.ForeColor = System.Drawing.Color.Transparent;
		this.button2.Location = new System.Drawing.Point(114, 503);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(123, 29);
		this.button2.TabIndex = 16;
		this.button2.UseVisualStyleBackColor = false;
		this.button2.Click += new System.EventHandler(button2_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 20f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		this.BackgroundImage = Daigassou.Properties.Resources.keybindingBGimg;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.ClientSize = new System.Drawing.Size(484, 561);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.cbOctaveLower);
		base.Controls.Add(this.cbOctaveHigher);
		base.Controls.Add(this.textBox13);
		base.Controls.Add(this.textBox12);
		base.Controls.Add(this.textBox11);
		base.Controls.Add(this.textBox10);
		base.Controls.Add(this.textBox9);
		base.Controls.Add(this.textBox8);
		base.Controls.Add(this.textBox7);
		base.Controls.Add(this.textBox6);
		base.Controls.Add(this.textBox5);
		base.Controls.Add(this.textBox4);
		base.Controls.Add(this.textBox3);
		base.Controls.Add(this.textBox2);
		base.Controls.Add(this.textBox1);
		this.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		base.Name = "KeyBindForm";
		this.Text = "按键绑定";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(KeyBindForm_FormClosing);
		base.Load += new System.EventHandler(KeyBindForm_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
