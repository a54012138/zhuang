using System.IO;

internal class _24087979
{
	private uint _151364DF = 1u;

	public const uint _6B79029F = 16777216u;

	public uint _55800856;

	public uint _50186FF3;

	public Stream _23312878;

	public void _49787475(Stream _41E36957)
	{
		_23312878 = _41E36957;
		_50186FF3 = 0u;
		_55800856 = uint.MaxValue;
		for (int i = 0; i < 5; i++)
		{
			_50186FF3 = (_50186FF3 << 8) | (byte)_23312878.ReadByte();
		}
	}

	public void _3EC3697A()
	{
		_23312878 = null;
	}

	public uint _4A1A4CDC(int _24105298)
	{
		uint num = _55800856;
		uint num2 = _50186FF3;
		uint num3 = 0u;
		for (int num4 = _24105298; num4 > 0; num4--)
		{
			num >>= 1;
			uint num5 = num2 - num >> 31;
			num2 -= num & (num5 - 1);
			num3 = (num3 << 1) | (1 - num5);
			if (num < 16777216)
			{
				num2 = (num2 << 8) | (byte)_23312878.ReadByte();
				num <<= 8;
			}
		}
		_55800856 = num;
		_50186FF3 = num2;
		return num3;
	}
}
