using System;
using System.ComponentModel;
using System.Windows.Forms;
using MyUtils;
using War3_72bian.Model;

namespace WinForms;

public class Form1 : Form
{
	private string banbenhao;

	private string banbenqueren;

	private readonly string _processName;

	private readonly string _moduleName;

	private HeroInfo _heroInfo;

	private HeroInfo2 _heroInfo2;

	private MemoryUtils _memoryUtils;

	private string gonggao;

	private string jichuxinxi;

	private string gonggao0;

	private int ZBjizhi;

	private int JBjizhi1;

	private int JBjizhi2;

	private int JBjizhi3;

	private int JBjizhi4;

	private int JBjizhi5;

	private int count;

	private int pd;

	private bool rdbcheck;

	private bool bfq;

	private IContainer components;

	private Label label1;

	private RadioButton radioButton1;

	private Label label2;

	private Label label3;

	private Button button11;

	private Button button12;

	private Button button13;

	private Button button14;

	private Button button15;

	private Button button16;

	private Button button17;

	private TextBox textBox1;

	private TextBox textBox2;

	private TextBox textBox3;

	private TextBox textBox4;

	private TextBox textBox5;

	private Timer timershang;

	private Timer timerxia;

	private Timer timerzuo;

	private Timer timeryou;

	private Timer timergao;

	private Timer timerdi;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private Label label8;

	private Label label9;

	private TextBox textBox6;

	private Label label10;

	private LinkLabel linkLabel1;

	private Timer timer_gonggao;

	private Label lb_gonggao;

	private Timer timer_liuyanban;

	private Label label11;

	private ToolTip toolTip1;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private Panel panel1;

	private void GONGGAO()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000046A8
	}

	private void chuangkou()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000047A8
	}

	private bool iniSoftInfo()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00004818
	}

	private void xunhuan()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00004A48
	}

	private void ModifyJingYan(int value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00004CD4
	}

	private void zuobiao(IntPtr zb, float value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00004CEF
	}

	private void Z(float value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00004D00
	}

	private void ModifyRead()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00004D1B
	}

	private void button1_Click(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00004D35
	}

	private void label1_Click(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00004D44
	}

	private void button2_Click(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00004D6E
	}

	private void button3_Click(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00004D9C
	}

	private void radioButton1_Click(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00004DB0
	}

	private void button9_Click(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005048
	}

	private void button12_Click(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005148
	}

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000051BC
	}

	private void timershang_Tick(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000054F0
	}

	private void timerxia_Tick(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005554
	}

	private void timerzuo_Tick(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000055B8
	}

	private void timeryou_Tick(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000561C
	}

	private void timergao_Tick(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005680
	}

	private void timerdi_Tick(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000056E4
	}

	private void button14_MouseDown(object sender, MouseEventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005748
	}

	private void button15_MouseDown(object sender, MouseEventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000057E4
	}

	private void button11_MouseDown(object sender, MouseEventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005880
	}

	private void button13_MouseDown(object sender, MouseEventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000591C
	}

	private void button16_MouseDown(object sender, MouseEventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000059B8
	}

	private void button17_MouseDown(object sender, MouseEventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005A54
	}

	private void button14_MouseUp(object sender, MouseEventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005AF0
	}

	private void Form1_Load(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005B4C
	}

	private void Form1_FormClosed(object sender, FormClosedEventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005BB8
	}

	private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005BEF
	}

	private void timer_gonggao_Tick(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005C00
	}

	private void timer_liuyanban_Tick(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005C48
	}

	private void LiuYan()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005C6F
	}

	public bool Bofang()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005C80
	}

	private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005CE4
	}

	protected override void Dispose(bool disposing)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005D08
	}

	private void InitializeComponent()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00005D40
	}
}
