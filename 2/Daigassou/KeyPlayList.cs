namespace Daigassou;

public class KeyPlayList
{
	public enum NoteEvent
	{
		NoteOff,
		NoteOn
	}

	public NoteEvent Ev;

	public int Pitch;

	public double TimeMs;

	public KeyPlayList(NoteEvent ev, int pitch, double timeMs)
	{
		TimeMs = timeMs;
		Ev = ev;
		Pitch = pitch;
	}
}
