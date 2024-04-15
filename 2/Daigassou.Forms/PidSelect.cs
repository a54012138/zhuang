using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Daigassou.Input_Midi;

namespace Daigassou.Forms;

public class PidSelect : Form
{
	public delegate void PidSelector(int id);

	private BackgroundKey kc = new BackgroundKey();

	public PidSelector GetPid;

	private IContainer components = null;

	private ComboBox comboBox1;

	private Button button1;

	private Button button2;

	public PidSelect()
	{
		InitializeComponent();
		comboBox1.DataSource = BackgroundKey.GetPids().ToList();
	}

	private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void button1_Click(object sender, EventArgs e)
	{
		GetPid(Convert.ToInt32(comboBox1.SelectedItem?.ToString()));
		Close();
	}

	private void button2_Click(object sender, EventArgs e)
	{
		kc.Init(Process.GetProcessById(Convert.ToInt32(comboBox1.SelectedItem?.ToString())).MainWindowHandle);
		System.Threading.Timer timer = new System.Threading.Timer(delegate
		{
			kc.BackgroundKeyPress(Keys.Space);
		}, new object(), 100, 0);
		System.Threading.Timer timer2 = new System.Threading.Timer(delegate
		{
			kc.BackgroundKeyRelease(Keys.Space);
		}, new object(), 200, 0);
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
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Location = new System.Drawing.Point(12, 12);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(183, 23);
		this.comboBox1.TabIndex = 0;
		this.comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
		this.button1.Location = new System.Drawing.Point(257, 11);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(54, 23);
		this.button1.TabIndex = 1;
		this.button1.Text = "确定";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Location = new System.Drawing.Point(201, 11);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(50, 23);
		this.button2.TabIndex = 2;
		this.button2.Text = "跳";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(318, 43);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.comboBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "PidSelect";
		base.ShowIcon = false;
		this.Text = " ";
		base.ResumeLayout(false);
	}
}
