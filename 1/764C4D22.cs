public class _764C4D22
{
	public const int _13651B8F = 32;

	public static uint _7B3F3479(uint _57EA30D4, int _71054117)
	{
		uint num = _57EA30D4 << _71054117;
		uint num2 = _57EA30D4 >> 32 - _71054117;
		return num | num2;
	}

	public static uint _6BCB53C3(uint _0A9C7DCE, int _7E2753BF)
	{
		uint num = _0A9C7DCE >> _7E2753BF;
		uint num2 = _0A9C7DCE << 32 - _7E2753BF;
		return num | num2;
	}

	public static uint _0D437AF8(uint _61B91379)
	{
		uint num = _61B91379 & 0xFF00FFu;
		uint num2 = _61B91379 & 0xFF00FF00u;
		return ((num >> 8) | (num << 24)) + ((num2 << 8) | (num2 >> 24));
	}
}
