using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Daigassou.Utils;

public class LogForm : Form
{
	private IContainer components = null;

	public RichTextBox LogTextBox;

	public LogForm()
	{
		InitializeComponent();
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
		this.LogTextBox = new System.Windows.Forms.RichTextBox();
		base.SuspendLayout();
		this.LogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.LogTextBox.Font = new System.Drawing.Font("微软雅黑", 10.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.LogTextBox.Location = new System.Drawing.Point(0, 0);
		this.LogTextBox.Name = "LogTextBox";
		this.LogTextBox.Size = new System.Drawing.Size(304, 308);
		this.LogTextBox.TabIndex = 0;
		this.LogTextBox.Text = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(304, 308);
		base.Controls.Add(this.LogTextBox);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "LogForm";
		this.Text = "LogForm";
		base.ResumeLayout(false);
	}
}
