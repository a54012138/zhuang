using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MyUtils;

public class MemoryUtils
{
	private IntPtr _handle;

	private int _pid;

	public MemoryUtils(int pid)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000220D
	}

	~MemoryUtils()
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002244
	}

	public byte[] ReadToBytes(IntPtr address, int size)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000227C
	}

	public T ReadObject<T>(IntPtr address) where T : struct
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000022AC
	}

	public char ReadToChar(IntPtr address)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002308
	}

	public short ReadToShort(IntPtr address)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000232C
	}

	public int ReadToInt(IntPtr address)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002350
	}

	public long ReadToLong(IntPtr address)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002374
	}

	public float ReadToFloat(IntPtr address)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002398
	}

	public double ReadToDouble(IntPtr address)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000023BC
	}

	public string ReadToString(IntPtr address, int stringSize)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000023E0
	}

	public bool WriteByteArray(IntPtr address, byte[] byteData)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002404
	}

	public bool WriteChar(IntPtr address, char value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000242C
	}

	public bool WriteShort(IntPtr address, short value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000244C
	}

	public bool WriteInt(IntPtr address, int value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000246C
	}

	public bool WriteLong(IntPtr address, long value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000248C
	}

	public bool WriteFloat(IntPtr address, float value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000024AC
	}

	public bool WriteDouble(IntPtr address, double value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000024CC
	}

	public bool WriteString(IntPtr address, string value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000024EC
	}

	public static int GetPidByProcessName(string processName)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002510
	}

	public static Process GetProcessByProcessName(string processName)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002534
	}

	public static IntPtr FindWindow(string title)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002584
	}

	public IntPtr GetModuleBaseAddress(string moduleName)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000025E0
	}

	public IntPtr GetMemoryAddress(string moduleName, params int[] offsetArray)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002650
	}

	public static IntPtr GetWindowHwndByProcessName(string processName)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000026D0
	}

	public static int ConvertFrom16To10(string value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002720
	}

	public static int ConvertFrom16To10(int value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000273C
	}

	public static string ConvertFrom10To16(int value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000275C
	}

	public static string ConvertFrom10To16(IntPtr value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000277C
	}

	public static string ConvertFrom16Or10To2(int value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000279C
	}

	public static int ConvertFrom2To10(string value)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x000027B8
	}

	[DllImport("kernel32.dll")]
	private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, IntPtr lpNumberOfBytesRead);

	[DllImport("kernel32.dll")]
	private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, IntPtr lpNumberOfBytesWritten);

	[DllImport("kernel32.dll")]
	private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

	[DllImport("kernel32.dll")]
	private static extern void CloseHandle(IntPtr hObject);
}
