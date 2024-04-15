using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Commons.Music.Midi;
using Commons.Music.Midi.Mml;

namespace Daigassou.Utils;

internal class MmlMidiConventer
{
	public unsafe static byte[] mmlRead(string mmlPath)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		string text = mmlPath.Replace(".mml", ".mid");
		string text2 = File.ReadAllText(mmlPath);
		if (text2.Contains("MML@"))
		{
			try
			{
				fixed (byte* ptr2 = Encoding.Default.GetBytes(mmlPath))
				{
					fixed (byte* ptr = Encoding.Default.GetBytes(text))
					{
					}
				}
			}
			catch (Exception)
			{
				return null;
			}
			if (num == 0)
			{
				byte[] result = File.ReadAllBytes(text);
				File.Delete(text);
				return result;
			}
			return null;
		}
		try
		{
			List<MmlInputSource> list = new List<MmlInputSource>();
			list.Add(new MmlInputSource("fake.mml", (TextReader)new StringReader(File.ReadAllText(mmlPath))));
			using MemoryStream memoryStream = new MemoryStream();
			new MmlCompiler().Compile(false, (IList<MmlInputSource>)list, (Func<bool, MidiMessage, Stream, int>)null, (Stream)memoryStream, false);
			return memoryStream.ToArray();
		}
		catch (Exception)
		{
			return null;
		}
	}
}
