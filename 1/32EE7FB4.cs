public class _32EE7FB4
{
	private uint _09191067;

	public _32EE7FB4()
	{
		_09191067 = 1653363842u;
	}

	public uint _7FA4527B(uint _11AD5DE1)
	{
		uint num = _11AD5DE1 ^ _09191067;
		_09191067 = _764C4D22._7B3F3479(_09191067, 7) ^ num;
		return num;
	}
}
