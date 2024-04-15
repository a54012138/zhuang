using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFwTypeLib;

[ComImport]
[Guid("D46D2478-9AC9-4008-9DC7-5563CE5536CC")]
[TypeIdentifier]
[CompilerGenerated]
public interface INetFwPolicy
{
	[DispId(1)]
	INetFwProfile CurrentProfile
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(1)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}
}
