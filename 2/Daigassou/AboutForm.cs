using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Daigassou.Properties;

namespace Daigassou;

public class AboutForm : Form
{
	private KeyController kc;

	private int ClickCount = 0;

	private IContainer components = null;

	private Label lblVersion;

	private Label label2;

	private Label label3;

	private Label label1;

	private Label label4;

	public AboutForm(KeyController _kc)
	{
		kc = _kc;
		InitializeComponent();
	}

	private void AboutForm_Load(object sender, EventArgs e)
	{
		lblVersion.Text = "Ver " + Assembly.GetExecutingAssembly().GetName().Version;
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
		this.lblVersion = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.lblVersion.AutoSize = true;
		this.lblVersion.BackColor = System.Drawing.Color.Transparent;
		this.lblVersion.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(255, 128, 128);
		this.lblVersion.Location = new System.Drawing.Point(197, 342);
		this.lblVersion.Name = "lblVersion";
		this.lblVersion.Size = new System.Drawing.Size(94, 38);
		this.lblVersion.TabIndex = 1;
		this.lblVersion.Text = "Ver 1.1.0.54\r\n\r\n";
		this.label2.AutoSize = true;
		this.label2.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.label2.Location = new System.Drawing.Point(239, 104);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(236, 102);
		this.label2.TabIndex = 2;
		this.label2.Text = "您同意使用本软件产品风险由用户自行承担\r\n包括但不限于死机，封号，硬盘爆炸等\r\n为避免滥用，您同意使用本程序进行的活动\r\n不会侵犯第三方的权利。\r\n当发生滥用时\r\n作者保留禁止您继续使用本程序的权利。\r\n";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label3.ForeColor = System.Drawing.Color.FromArgb(243, 75, 107);
		this.label3.Location = new System.Drawing.Point(239, 227);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(164, 102);
		this.label3.TabIndex = 3;
		this.label3.Text = "程序：黑尾白猫@神意之地\r\n助手：酒酿和歌子@神意之地\r\n\r\n发布：blog.ffxiv.cat\r\nBug反馈裙：720145203\r\n\r\n";
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Location = new System.Drawing.Point(415, 241);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(54, 51);
		this.label1.TabIndex = 4;
		this.label1.Text = "              ";
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.ForeColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.label4.Location = new System.Drawing.Point(239, 261);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(140, 17);
		this.label4.TabIndex = 5;
		this.label4.Text = "两只猫娘是不是很可爱！";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		this.BackgroundImage = Daigassou.Properties.Resources.about2;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		base.ClientSize = new System.Drawing.Size(490, 362);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.lblVersion);
		base.Controls.Add(this.label1);
		this.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "AboutForm";
		this.Text = "关于[大合奏！]";
		base.Load += new System.EventHandler(AboutForm_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
