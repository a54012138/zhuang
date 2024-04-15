using System.Collections.Generic;
using System.Diagnostics;

namespace Daigassou.Utils;

public static class FFProcess
{
	public static List<Process> FindFFXIVProcess()
	{
		List<Process> list = new List<Process>();
		list.AddRange(Process.GetProcessesByName("ffxiv"));
		list.AddRange(Process.GetProcessesByName("ffxiv_dx11"));
		return list;
	}

	public static List<Process> FindDaigassouProcess()
	{
		List<Process> list = new List<Process>();
		list.AddRange(Process.GetProcessesByName("Daigassou"));
		return list;
	}
}
