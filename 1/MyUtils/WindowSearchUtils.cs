using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MyUtils;

public class WindowSearchUtils
{
	public enum LVM
	{
		FIRST = 4096,
		SETUNICODEFORMAT = 8197,
		GETUNICODEFORMAT = 8198,
		GETBKCOLOR = 4096,
		SETBKCOLOR = 4097,
		GETIMAGELIST = 4098,
		SETIMAGELIST = 4099,
		GETITEMCOUNT = 4100,
		GETITEMA = 4101,
		GETITEMW = 4171,
		GETITEM = 4171,
		SETITEMA = 4102,
		SETITEMW = 4172,
		SETITEM = 4172,
		INSERTITEMA = 4103,
		INSERTITEMW = 4173,
		INSERTITEM = 4173,
		DELETEITEM = 4104,
		DELETEALLITEMS = 4105,
		GETCALLBACKMASK = 4106,
		SETCALLBACKMASK = 4107,
		GETNEXTITEM = 4108,
		FINDITEMA = 4109,
		FINDITEMW = 4179,
		GETITEMRECT = 4110,
		SETITEMPOSITION = 4111,
		GETITEMPOSITION = 4112,
		GETSTRINGWIDTHA = 4113,
		GETSTRINGWIDTHW = 4183,
		HITTEST = 4114,
		ENSUREVISIBLE = 4115,
		SCROLL = 4116,
		REDRAWITEMS = 4117,
		ARRANGE = 4118,
		EDITLABELA = 4119,
		EDITLABELW = 4214,
		EDITLABEL = 4214,
		GETEDITCONTROL = 4120,
		GETCOLUMNA = 4121,
		GETCOLUMNW = 4191,
		SETCOLUMNA = 4122,
		SETCOLUMNW = 4192,
		INSERTCOLUMNA = 4123,
		INSERTCOLUMNW = 4193,
		DELETECOLUMN = 4124,
		GETCOLUMNWIDTH = 4125,
		SETCOLUMNWIDTH = 4126,
		GETHEADER = 4127,
		CREATEDRAGIMAGE = 4129,
		GETVIEWRECT = 4130,
		GETTEXTCOLOR = 4131,
		SETTEXTCOLOR = 4132,
		GETTEXTBKCOLOR = 4133,
		SETTEXTBKCOLOR = 4134,
		GETTOPINDEX = 4135,
		GETCOUNTPERPAGE = 4136,
		GETORIGIN = 4137,
		UPDATE = 4138,
		SETITEMSTATE = 4139,
		GETITEMSTATE = 4140,
		GETITEMTEXTA = 4141,
		GETITEMTEXTW = 4211,
		SETITEMTEXTA = 4142,
		SETITEMTEXTW = 4212,
		SETITEMCOUNT = 4143,
		SORTITEMS = 4144,
		SETITEMPOSITION32 = 4145,
		GETSELECTEDCOUNT = 4146,
		GETITEMSPACING = 4147,
		GETISEARCHSTRINGA = 4148,
		GETISEARCHSTRINGW = 4213,
		GETISEARCHSTRING = 4213,
		SETICONSPACING = 4149,
		SETEXTENDEDLISTVIEWSTYLE = 4150,
		GETEXTENDEDLISTVIEWSTYLE = 4151,
		GETSUBITEMRECT = 4152,
		SUBITEMHITTEST = 4153,
		SETCOLUMNORDERARRAY = 4154,
		GETCOLUMNORDERARRAY = 4155,
		SETHOTITEM = 4156,
		GETHOTITEM = 4157,
		SETHOTCURSOR = 4158,
		GETHOTCURSOR = 4159,
		APPROXIMATEVIEWRECT = 4160,
		SETWORKAREAS = 4161,
		GETWORKAREAS = 4166,
		GETNUMBEROFWORKAREAS = 4169,
		GETSELECTIONMARK = 4162,
		SETSELECTIONMARK = 4163,
		SETHOVERTIME = 4167,
		GETHOVERTIME = 4168,
		SETTOOLTIPS = 4170,
		GETTOOLTIPS = 4174,
		SORTITEMSEX = 4177,
		SETBKIMAGEA = 4164,
		SETBKIMAGEW = 4234,
		GETBKIMAGEA = 4165,
		GETBKIMAGEW = 4235,
		SETSELECTEDCOLUMN = 4236,
		SETVIEW = 4238,
		GETVIEW = 4239,
		INSERTGROUP = 4241,
		SETGROUPINFO = 4243,
		GETGROUPINFO = 4245,
		REMOVEGROUP = 4246,
		MOVEGROUP = 4247,
		GETGROUPCOUNT = 4248,
		GETGROUPINFOBYINDEX = 4249,
		MOVEITEMTOGROUP = 4250,
		GETGROUPRECT = 4194,
		SETGROUPMETRICS = 4251,
		GETGROUPMETRICS = 4252,
		ENABLEGROUPVIEW = 4253,
		SORTGROUPS = 4254,
		INSERTGROUPSORTED = 4255,
		REMOVEALLGROUPS = 4256,
		HASGROUP = 4257,
		GETGROUPSTATE = 4188,
		GETFOCUSEDGROUP = 4189,
		SETTILEVIEWINFO = 4258,
		GETTILEVIEWINFO = 4259,
		SETTILEINFO = 4260,
		GETTILEINFO = 4261,
		SETINSERTMARK = 4262,
		GETINSERTMARK = 4263,
		INSERTMARKHITTEST = 4264,
		GETINSERTMARKRECT = 4265,
		SETINSERTMARKCOLOR = 4266,
		GETINSERTMARKCOLOR = 4267,
		GETSELECTEDCOLUMN = 4270,
		ISGROUPVIEWENABLED = 4271,
		GETOUTLINECOLOR = 4272,
		SETOUTLINECOLOR = 4273,
		CANCELEDITLABEL = 4275,
		MAPINDEXTOID = 4276,
		MAPIDTOINDEX = 4277,
		ISITEMVISIBLE = 4278,
		GETACCVERSION = 4289,
		GETEMPTYTEXT = 4300,
		GETFOOTERRECT = 4301,
		GETFOOTERINFO = 4302,
		GETFOOTERITEMRECT = 4303,
		GETFOOTERITEM = 4304,
		GETITEMINDEXRECT = 4305,
		SETITEMINDEXSTATE = 4306,
		GETNEXTITEMINDEX = 4307,
		SETPRESERVEALPHA = 4308,
		SETBKIMAGE = 4234,
		GETBKIMAGE = 4235
	}

	public delegate bool EnumWindowsProc(IntPtr hWnd, int parameter);

	private delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

	private const int GW_HWNDNEXT = 2;

	private const int GW_CHILD = 5;

	private const int TV_FIRST = 4352;

	private const int TVGN_ROOT = 0;

	private const int TVGN_NEXT = 1;

	private const int TVGN_CHILD = 4;

	private const int TVGN_FIRSTVISIBLE = 5;

	private const int TVGN_NEXTVISIBLE = 6;

	private const int TVGN_CARET = 9;

	private const int TVM_SELECTITEM = 4363;

	private const int TVM_GETNEXTITEM = 4362;

	private const int TVM_GETITEM = 4364;

	private const int TVM_EXPAND = 4354;

	private const int TVE_COLLAPSE = 1;

	private const int TVE_EXPAND = 2;

	private const int TVE_COLLAPSERESET = 32768;

	private const int LVM_GETNEXTITEM = 4108;

	private const int LVNI_BELOW = 512;

	[DllImport("user32.dll")]
	private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

	[DllImport("user32")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool EnumChildWindows(IntPtr window, EnumWindowsProc callback, IntPtr i);

	[DllImport("user32.dll")]
	public static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

	[DllImport("User32.dll")]
	private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

	public static IntPtr FindWindowByClassNameOrTitle(string className = null, string title = null)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000032A4
	}

	public static List<IntPtr> GetChildWindows(IntPtr parent)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000032C0
	}

	private static bool EnumWindow(IntPtr handle, int pointer)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003334
	}

	[DllImport("user32.dll")]
	private static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

	private static IEnumerable<IntPtr> EnumerateProcessWindowHandles(int processId)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000337C
	}

	public static IntPtr GetWindowByText(string processName, string windowText, bool exact, bool onlyVisible)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003428
	}

	public static IntPtr GetWindowByText(string processName, string windowText, bool onlyVisible)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000034B8
	}

	public static IntPtr GetWindowByText(string processName, string windowText)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000034D4
	}

	public static IntPtr GetThreadWindowByText(string processName, string windowText, bool exact, bool onlyVisible)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000034F0
	}

	public static IntPtr GetThreadWindowByText(string processName, string windowText, bool exact)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003580
	}

	public static IntPtr GetChildWindowByText(IntPtr parent, string windowText, bool exact)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000359C
	}

	public static IntPtr GetWindowByCaption(string processName, string windowCaption, bool exact)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003628
	}

	public static List<IntPtr> GetAllWindowsByText(string processName, string windowText, bool exact)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000036B8
	}

	public static List<IntPtr> GetAllWindowsByCaption(string processName, string windowCaption, bool exact)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003750
	}

	public static List<IntPtr> GetAllWindowsByProcess(string processName, bool onlyVisible)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000037E8
	}

	public static List<IntPtr> GetAllThreadWindows(string processName, bool onlyVisible)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003920
	}

	private static void ParseNode(string node, out string name, out string caption, out int index)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000039BC
	}

	public static IntPtr GetWindowByPath(string path)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003A80
	}

	public static List<IntPtr> GetDirectChildWindows(IntPtr parent)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003B54
	}

	public static List<IntPtr> GetTreeViewItemChildItems(IntPtr treeView, IntPtr item)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003B98
	}

	public static List<IntPtr> GetAllTreeViewItems(IntPtr treeView)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003C20
	}

	public static IntPtr GetChildByPath(IntPtr parent, params int[] path)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003C7C
	}

	public static IntPtr GetWindowByClass(IntPtr parent, string className)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003CF4
	}

	public static List<IntPtr> GetListViewItems(IntPtr listView)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00003D88
	}
}
