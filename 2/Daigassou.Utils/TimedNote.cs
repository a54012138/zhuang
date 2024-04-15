using System;

namespace Daigassou.Utils;

internal class TimedNote
{
	public DateTime StartTime;

	public int Note;

	public override string ToString()
	{
		return string.Format("StartTime: " + StartTime.ToString("HH:mm:ss.fff") + ", Note: " + Note.ToString("X2"));
	}
}
