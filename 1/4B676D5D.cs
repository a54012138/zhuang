internal abstract class _4B676D5D
{
	public struct _7A4A5B7E
	{
		public uint _40972DC8;

		public void _78885D65()
		{
			_40972DC8 = 0u;
		}

		public void _410B4CD3()
		{
			if (_40972DC8 < 4)
			{
				_40972DC8 = 0u;
			}
			else if (_40972DC8 < 10)
			{
				_40972DC8 -= 3u;
			}
			else
			{
				_40972DC8 -= 6u;
			}
		}

		public void _7CFE4AE9()
		{
			_40972DC8 = ((_40972DC8 < 7) ? 7u : 10u);
		}

		public void _7B143D3C()
		{
			_40972DC8 = ((_40972DC8 < 7) ? 8u : 11u);
		}

		public void _5D7D6C7F()
		{
			_40972DC8 = ((_40972DC8 < 7) ? 9u : 11u);
		}

		public bool _445471B8()
		{
			return _40972DC8 < 7;
		}
	}

	public const uint _09E63094 = 12u;

	public const int _49AD6463 = 6;

	private const int _79EB0148 = 2;

	public const uint _68867B3D = 4u;

	public const uint _68787496 = 2u;

	public const int _48FF44ED = 4;

	public const uint _7B545876 = 4u;

	public const uint _033C32D8 = 14u;

	public const uint _70E67A07 = 128u;

	public const int _56F350B1 = 4;

	public const uint _05A54532 = 16u;

	public const int _5C7C013C = 3;

	public const int _4DD0078A = 3;

	public const int _4C287858 = 8;

	public const uint _575F5A77 = 8u;

	public const uint _4FD4163C = 8u;

	public static uint _060B7341(uint _2D0E45C9)
	{
		_2D0E45C9 -= 2;
		if (_2D0E45C9 < 4)
		{
			return _2D0E45C9;
		}
		return 3u;
	}
}
