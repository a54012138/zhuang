using System;
using System.IO;

public class _28397894
{
	private class _7BF72693
	{
		private _754C0BC8 _4F9F3352;

		private _754C0BC8 _68752AC4;

		private readonly _33CA4DE0[] _77626CD3 = new _33CA4DE0[16];

		private readonly _33CA4DE0[] _78D45548 = new _33CA4DE0[16];

		private _33CA4DE0 _2E344988 = new _33CA4DE0(8);

		private uint _72717F7B;

		public void _23F83E0B(uint _6E7D7565)
		{
			for (uint num = _72717F7B; num < _6E7D7565; num++)
			{
				_77626CD3[num] = new _33CA4DE0(3);
				_78D45548[num] = new _33CA4DE0(3);
			}
			_72717F7B = _6E7D7565;
		}

		public void _5B3A1AE8()
		{
			_4F9F3352._589B6BBB();
			for (uint num = 0u; num < _72717F7B; num++)
			{
				_77626CD3[num]._6E9F15CB();
				_78D45548[num]._6E9F15CB();
			}
			_68752AC4._589B6BBB();
			_2E344988._6E9F15CB();
		}

		public uint _49E117D8(_24087979 _1AD92BEB, uint _2499664E)
		{
			if (_4F9F3352._064238E9(_1AD92BEB) == 0)
			{
				return _77626CD3[_2499664E]._40C16862(_1AD92BEB);
			}
			uint num = 8u;
			if (_68752AC4._064238E9(_1AD92BEB) == 0)
			{
				return num + _78D45548[_2499664E]._40C16862(_1AD92BEB);
			}
			num += 8;
			return num + _2E344988._40C16862(_1AD92BEB);
		}
	}

	private class _17BD4CCC
	{
		private struct _7B2558FD
		{
			private _754C0BC8[] _50F45691;

			public void _47BA4126()
			{
				_50F45691 = new _754C0BC8[768];
			}

			public void _28AC48A7()
			{
				for (int i = 0; i < 768; i++)
				{
					_50F45691[i]._589B6BBB();
				}
			}

			public byte _2F3F094C(_24087979 _4F5A1959)
			{
				uint num = 1u;
				do
				{
					num = (num << 1) | _50F45691[num]._064238E9(_4F5A1959);
				}
				while (num < 256);
				return (byte)num;
			}

			public byte _6D5A577F(_24087979 _695A674D, byte _7BBB4B0E)
			{
				uint num = 1u;
				do
				{
					uint num2 = (uint)(_7BBB4B0E >> 7) & 1u;
					_7BBB4B0E <<= 1;
					uint num3 = _50F45691[(1 + num2 << 8) + num]._064238E9(_695A674D);
					num = (num << 1) | num3;
					if (num2 != num3)
					{
						while (num < 256)
						{
							num = (num << 1) | _50F45691[num]._064238E9(_695A674D);
						}
						break;
					}
				}
				while (num < 256);
				return (byte)num;
			}
		}

		private uint _63187E68 = 1u;

		private _7B2558FD[] _138E2DC9;

		private int _654732AE;

		private int _1B8027EA;

		private uint _404047F2;

		public void _404E4927(int _72B439C2, int _5D962B8F)
		{
			if (_138E2DC9 == null || _654732AE != _5D962B8F || _1B8027EA != _72B439C2)
			{
				_1B8027EA = _72B439C2;
				_404047F2 = (uint)((1 << _72B439C2) - 1);
				_654732AE = _5D962B8F;
				uint num = (uint)(1 << _654732AE + _1B8027EA);
				_138E2DC9 = new _7B2558FD[num];
				for (uint num2 = 0u; num2 < num; num2++)
				{
					_138E2DC9[num2]._47BA4126();
				}
			}
		}

		public void _722C235B()
		{
			uint num = (uint)(1 << _654732AE + _1B8027EA);
			for (uint num2 = 0u; num2 < num; num2++)
			{
				_138E2DC9[num2]._28AC48A7();
			}
		}

		private uint _66494883(uint _67F607BC, byte _11A73FA5)
		{
			return ((_67F607BC & _404047F2) << _654732AE) + (uint)(_11A73FA5 >> 8 - _654732AE);
		}

		public byte _557335B4(_24087979 _7C1D364A, uint _30382381, byte _571941E9)
		{
			return _138E2DC9[_66494883(_30382381, _571941E9)]._2F3F094C(_7C1D364A);
		}

		public byte _59F04FB3(_24087979 _26E2252C, uint _0A5D4390, byte _02D154F2, byte _7D937A94)
		{
			return _138E2DC9[_66494883(_0A5D4390, _02D154F2)]._6D5A577F(_26E2252C, _7D937A94);
		}
	}

	private uint _367445ED = 1u;

	private readonly _6C046AA7 _11B668AC = new _6C046AA7();

	private readonly _24087979 _7A0A739D = new _24087979();

	private readonly _754C0BC8[] _6C91449F = new _754C0BC8[192];

	private readonly _754C0BC8[] _57E25F3F = new _754C0BC8[12];

	private readonly _754C0BC8[] _0B676F23 = new _754C0BC8[12];

	private readonly _754C0BC8[] _3E84070D = new _754C0BC8[12];

	private readonly _754C0BC8[] _562148CB = new _754C0BC8[12];

	private readonly _754C0BC8[] _034945CC = new _754C0BC8[192];

	private readonly _33CA4DE0[] _6BFC2532 = new _33CA4DE0[4];

	private readonly _754C0BC8[] _576D4396 = new _754C0BC8[114];

	private _33CA4DE0 _613A61E1 = new _33CA4DE0(4);

	private readonly _7BF72693 _136563DD = new _7BF72693();

	private readonly _7BF72693 _05186473 = new _7BF72693();

	private readonly _17BD4CCC _4C7354AD = new _17BD4CCC();

	private uint _43FF43F7;

	private uint _68772331;

	private uint _79FE2195;

	public _28397894()
	{
		_43FF43F7 = uint.MaxValue;
		for (int i = 0; (long)i < 4L; i++)
		{
			_6BFC2532[i] = new _33CA4DE0(6);
		}
	}

	private void _12BE0801(uint _475A2665)
	{
		if (_43FF43F7 != _475A2665)
		{
			_43FF43F7 = _475A2665;
			_68772331 = Math.Max(_43FF43F7, 1u);
			uint _252C66CA = Math.Max(_68772331, 4096u);
			_11B668AC._56AC763D(_252C66CA);
		}
	}

	private void _6CA70255(int _108B7E33, int _2B221415)
	{
		if (_108B7E33 > 8)
		{
			throw new ArgumentException("lp > 8");
		}
		if (_2B221415 > 8)
		{
			throw new ArgumentException("lc > 8");
		}
		_4C7354AD._404E4927(_108B7E33, _2B221415);
	}

	private void _03975161(int _55AE787B)
	{
		if (_55AE787B > 4)
		{
			throw new ArgumentException("pb > Base.KNumPosStatesBitsMax");
		}
		uint num = (uint)(1 << _55AE787B);
		_136563DD._23F83E0B(num);
		_05186473._23F83E0B(num);
		_79FE2195 = num - 1;
	}

	private void _6A0975ED(Stream _027951BB, Stream _0EF03E56)
	{
		_7A0A739D._49787475(_027951BB);
		_11B668AC._5A5D5C79(_0EF03E56, _47ED008B: false);
		for (uint num = 0u; num < 12; num++)
		{
			for (uint num2 = 0u; num2 <= _79FE2195; num2++)
			{
				uint num3 = (num << 4) + num2;
				_6C91449F[num3]._589B6BBB();
				_034945CC[num3]._589B6BBB();
			}
			_57E25F3F[num]._589B6BBB();
			_0B676F23[num]._589B6BBB();
			_3E84070D[num]._589B6BBB();
			_562148CB[num]._589B6BBB();
		}
		_4C7354AD._722C235B();
		for (uint num = 0u; num < 4; num++)
		{
			_6BFC2532[num]._6E9F15CB();
		}
		for (uint num = 0u; num < 114; num++)
		{
			_576D4396[num]._589B6BBB();
		}
		_136563DD._5B3A1AE8();
		_05186473._5B3A1AE8();
		_613A61E1._6E9F15CB();
	}

	public void _7CD923BE(Stream _03456280, Stream _046B42C5, long _32935D05)
	{
		_6A0975ED(_03456280, _046B42C5);
		_4B676D5D._7A4A5B7E _7A4A5B7E = default(_4B676D5D._7A4A5B7E);
		_7A4A5B7E._78885D65();
		uint num = 0u;
		uint num2 = 0u;
		uint num3 = 0u;
		uint num4 = 0u;
		ulong num5 = 0uL;
		if (num5 < (ulong)_32935D05)
		{
			if (_6C91449F[_7A4A5B7E._40972DC8 << 4]._064238E9(_7A0A739D) != 0)
			{
				throw new InvalidDataException("IsMatchDecoders");
			}
			_7A4A5B7E._410B4CD3();
			byte _14E = _4C7354AD._557335B4(_7A0A739D, 0u, 0);
			_11B668AC._659C10BA(_14E);
			num5++;
		}
		while (num5 < (ulong)_32935D05)
		{
			uint num6 = (uint)(int)num5 & _79FE2195;
			if (_6C91449F[(_7A4A5B7E._40972DC8 << 4) + num6]._064238E9(_7A0A739D) == 0)
			{
				byte b = _11B668AC._09A054A2(0u);
				byte _14E2 = (_7A4A5B7E._445471B8() ? _4C7354AD._557335B4(_7A0A739D, (uint)num5, b) : _4C7354AD._59F04FB3(_7A0A739D, (uint)num5, b, _11B668AC._09A054A2(num)));
				_11B668AC._659C10BA(_14E2);
				_7A4A5B7E._410B4CD3();
				num5++;
				continue;
			}
			uint num8;
			if (_57E25F3F[_7A4A5B7E._40972DC8]._064238E9(_7A0A739D) == 1)
			{
				if (_0B676F23[_7A4A5B7E._40972DC8]._064238E9(_7A0A739D) == 0)
				{
					if (_034945CC[(_7A4A5B7E._40972DC8 << 4) + num6]._064238E9(_7A0A739D) == 0)
					{
						_7A4A5B7E._5D7D6C7F();
						_11B668AC._659C10BA(_11B668AC._09A054A2(num));
						num5++;
						continue;
					}
				}
				else
				{
					uint num7;
					if (_3E84070D[_7A4A5B7E._40972DC8]._064238E9(_7A0A739D) == 0)
					{
						num7 = num2;
					}
					else
					{
						if (_562148CB[_7A4A5B7E._40972DC8]._064238E9(_7A0A739D) == 0)
						{
							num7 = num3;
						}
						else
						{
							num7 = num4;
							num4 = num3;
						}
						num3 = num2;
					}
					num2 = num;
					num = num7;
				}
				num8 = _05186473._49E117D8(_7A0A739D, num6) + 2;
				_7A4A5B7E._7B143D3C();
			}
			else
			{
				num4 = num3;
				num3 = num2;
				num2 = num;
				num8 = 2 + _136563DD._49E117D8(_7A0A739D, num6);
				_7A4A5B7E._7CFE4AE9();
				uint num9 = _6BFC2532[_4B676D5D._060B7341(num8)]._40C16862(_7A0A739D);
				if (num9 >= 4)
				{
					int num10 = (int)((num9 >> 1) - 1);
					num = (2 | (num9 & 1)) << num10;
					if (num9 < 14)
					{
						num += _33CA4DE0._1B0B5C09(_576D4396, num - num9 - 1, _7A0A739D, num10);
					}
					else
					{
						num += _7A0A739D._4A1A4CDC(num10 - 4) << 4;
						num += _613A61E1._1A256455(_7A0A739D);
					}
				}
				else
				{
					num = num9;
				}
			}
			if (num >= _11B668AC._78C8148A + num5 || num >= _68772331)
			{
				if (num == uint.MaxValue)
				{
					break;
				}
				throw new InvalidDataException("rep0");
			}
			_11B668AC._502A2B36(num, num8);
			num5 += num8;
		}
		_11B668AC._569A2AF1();
		_11B668AC._25DF08F1();
		_7A0A739D._3EC3697A();
	}

	public void _09C85E6F(byte[] _40CA3A1D)
	{
		if (_40CA3A1D.Length < 5)
		{
			throw new ArgumentException("properties.Length < 5");
		}
		int _2B = _40CA3A1D[0] % 9;
		int num = _40CA3A1D[0] / 9;
		int _108B7E = num % 5;
		int num2 = num / 5;
		if (num2 > 4)
		{
			throw new ArgumentException("pb > Base.kNumPosStatesBitsMax");
		}
		uint num3 = 0u;
		for (int i = 0; i < 4; i++)
		{
			num3 += (uint)(_40CA3A1D[1 + i] << i * 8);
		}
		_12BE0801(num3);
		_6CA70255(_108B7E, _2B);
		_03975161(num2);
	}
}
