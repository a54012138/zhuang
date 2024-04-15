using System;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BondTech.HotkeyManagement.Win;

namespace Daigassou;

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		try
		{
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			Application.ThreadException += Application_ThreadException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Application.Run(new MainForm());
		}
		catch (Exception ex)
		{
			if (ex is HotKeyAlreadyRegisteredException)
			{
				MessageBox.Show("快捷键似乎注册失败了，是否已经被占用？", "快捷键无法注册", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			string exceptionMsg = GetExceptionMsg(ex, string.Empty);
			MessageBox.Show(exceptionMsg, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
	{
		string exceptionMsg = GetExceptionMsg(e.Exception, e.ToString());
		MessageBox.Show(exceptionMsg, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
	}

	private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		string exceptionMsg = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
		MessageBox.Show(exceptionMsg, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
	}

	private static string GetExceptionMsg(Exception ex, string backStr)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("*************************软件内部出错*************************");
		stringBuilder.AppendLine("****************请将当前界面截图并反馈给我们****************");
		stringBuilder.AppendLine("【异常时间】：" + DateTime.Now.ToString());
		stringBuilder.AppendLine("【软件版本】：" + $"Ver{Assembly.GetExecutingAssembly().GetName().Version}");
		if (ex != null)
		{
			stringBuilder.AppendLine("【异常类型】：" + ex.GetType().Name);
			stringBuilder.AppendLine("【异常信息】：" + ex.Message);
			stringBuilder.AppendLine("【堆栈调用】：" + ex.StackTrace);
		}
		else
		{
			stringBuilder.AppendLine("【未处理异常】：" + backStr);
		}
		stringBuilder.AppendLine("***************************************************************");
		return stringBuilder.ToString();
	}
}
