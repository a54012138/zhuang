using System;
using System.Runtime.InteropServices;
using System.Text;

internal static class _5C3B69E4
{
	[Flags]
	public enum _6969517D : uint
	{
		_179C67AD = 0x1000u,
		_764D10E1 = 0x2000u
	}

	[Flags]
	public enum _64FA57A5 : uint
	{
		_543A300D = 1u,
		_510259CB = 2u,
		_21A973D3 = 4u,
		_4EB81DCB = 8u,
		_557A332B = 0x10u,
		_718F3EEF = 0x20u,
		_505D35D4 = 0x40u,
		_64D67390 = 0x100u
	}

	[Flags]
	public enum _06541A9D : uint
	{
		_5BC21029 = 1u,
		_36CD574A = 2u,
		_58C50AEF = 4u,
		_724D1D95 = 0x1Fu
	}

	[Flags]
	public enum _26C61C84 : uint
	{
		_0C4F501D = 0x20000000u,
		_71E565E2 = 0x40000000u,
		_5B1C3050 = 0x80000000u
	}

	public const int _496D49E5 = int.MinValue;

	public const int _7E812A11 = 3;

	public const int _48E64ECA = 128;

	public const int _01291C55 = 1;

	public const int _78A64C27 = 2;

	public static readonly IntPtr _02754E88 = new IntPtr(-1);

	public static readonly IntPtr _590D7A33 = IntPtr.Zero;

	public static readonly IntPtr _6DD2022F = new IntPtr(-1);

	[DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "CreateFile", SetLastError = true)]
	public static extern IntPtr _1DCC34DA(string _078768BB, int _0C586042, int _2AF22F8B, IntPtr _0BF62F81, int _742F4B24, int _7E750664, IntPtr _69A46CA8);

	[DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "CreateFileMapping", SetLastError = true)]
	public static extern IntPtr _3B105B3C(IntPtr _43812CBA, IntPtr _3F7814F2, _64FA57A5 _409979D2, int _18472278, int _47667186, string _3E1A7676);

	[DllImport("kernel32", EntryPoint = "FlushViewOfFile", SetLastError = true)]
	public static extern bool _637E4CDA(IntPtr _364C3278, int _67967549);

	[DllImport("kernel32", EntryPoint = "MapViewOfFile", SetLastError = true)]
	public static extern IntPtr _150E4F8B(IntPtr _0740199C, _06541A9D _5D736618, int _40D93EF7, int _143075BA, IntPtr _7DA92FA6);

	[DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "OpenFileMapping", SetLastError = true)]
	public static extern IntPtr _46F60B81(int _0E0560D4, bool _7DCD0921, string _191A0942);

	[DllImport("kernel32", EntryPoint = "UnmapViewOfFile", SetLastError = true)]
	public static extern bool _1F295B44(IntPtr _76C359C1);

	[DllImport("kernel32", EntryPoint = "CloseHandle", SetLastError = true)]
	public static extern bool _30ED0476(IntPtr _3DF67AB9);

	[DllImport("kernel32", EntryPoint = "GetFileSize", SetLastError = true)]
	public static extern uint _750467CF(IntPtr _157C6A80, IntPtr _723A22AA);

	[DllImport("kernel32", EntryPoint = "VirtualAlloc", SetLastError = true)]
	public static extern IntPtr _703C69FA(IntPtr _66B25601, UIntPtr _638F3F69, _6969517D _197C10D0, _64FA57A5 _6EDD4A8E);

	[DllImport("kernel32", EntryPoint = "VirtualFree", SetLastError = true)]
	public static extern bool _6363242C(IntPtr _502046EA, uint _17BE629C, uint _73283280);

	[DllImport("kernel32", EntryPoint = "VirtualProtect", SetLastError = true)]
	public static extern bool _77F122B5(IntPtr _143277E0, UIntPtr _3AB84FF8, _64FA57A5 _4094409C, out _64FA57A5 _621E1335);

	[DllImport("kernel32", EntryPoint = "GetVolumeInformation", SetLastError = true)]
	public static extern bool _36E368DA(string _0FCC3865, StringBuilder _39AB0218, uint _4BC72ECA, ref uint _0AC15846, ref uint _3A242936, ref uint _022E182C, StringBuilder _79301C18, uint _14B82D41);

	[DllImport("kernel32", EntryPoint = "IsDebuggerPresent")]
	public static extern bool _43A26CB5();

	[DllImport("kernel32", EntryPoint = "CheckRemoteDebuggerPresent")]
	public static extern bool _62E912F7();

	[DllImport("kernel32", EntryPoint = "EnumSystemFirmwareTables", SetLastError = true)]
	public static extern uint _560131B4(uint _29153F40, IntPtr _11F9038B, uint _354E018C);

	[DllImport("kernel32", EntryPoint = "GetSystemFirmwareTable", SetLastError = true)]
	public static extern uint _14D81AA8(uint _596E3865, uint _525E5FC5, IntPtr _65CA487F, uint _03C222C4);

	[DllImport("ntdll", EntryPoint = "NtQueryInformationProcess")]
	public static extern int _05B17099(IntPtr _22905F26, int _3E2448D9, IntPtr _3FE02AC6, uint _79D11E6D, out uint _78915AAD);
}
