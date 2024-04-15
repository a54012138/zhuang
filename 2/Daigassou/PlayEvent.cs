using System;

namespace Daigassou;

public class PlayEvent : EventArgs
{
	private readonly string text;

	private readonly int time;

	private readonly int mode;

	public string Text => text;

	public int Time => time;

	public int Mode => mode;

	public PlayEvent(int mode, int Time, string text)
	{
		this.mode = mode;
		time = Time;
		this.text = text;
	}
}
