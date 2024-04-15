using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Daigassou.Properties;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Interaction;

namespace Daigassou;

internal class MidiToKey
{
	private readonly bool autoChord;

	private readonly uint MIN_DELAY_TIME_MS_CHORD;

	private readonly uint MIN_DELAY_TIME_MS_EVENT;

	private readonly List<NotesManager> tracks;

	public int Index = 0;

	private MidiFile midi;

	private OutputDevice outputDevice;

	private Playback playback;

	private TempoMap Tmap;

	private List<TrackChunk> trunks;

	public EnumPitchOffset Offset { get; set; }

	public int Bpm { get; set; }

	public MidiToKey()
	{
		tracks = new List<NotesManager>();
		Bpm = 80;
		Offset = EnumPitchOffset.None;
		MIN_DELAY_TIME_MS_EVENT = Settings.Default.MinEventMs;
		MIN_DELAY_TIME_MS_CHORD = Settings.Default.MinChordMs;
		autoChord = Settings.Default.AutoChord;
	}

	public void OpenFile(byte[] s)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		midi = MidiFile.Read((Stream)new MemoryStream(s), new ReadingSettings
		{
			NoHeaderChunkPolicy = (NoHeaderChunkPolicy)1,
			NotEnoughBytesPolicy = (NotEnoughBytesPolicy)1,
			InvalidChannelEventParameterValuePolicy = (InvalidChannelEventParameterValuePolicy)1,
			InvalidChunkSizePolicy = (InvalidChunkSizePolicy)1,
			InvalidMetaEventParameterValuePolicy = (InvalidMetaEventParameterValuePolicy)1,
			MissedEndOfTrackPolicy = (MissedEndOfTrackPolicy)0,
			UnexpectedTrackChunksCountPolicy = (UnexpectedTrackChunksCountPolicy)0,
			ExtraTrackChunkPolicy = (ExtraTrackChunkPolicy)0,
			UnknownChunkIdPolicy = (UnknownChunkIdPolicy)0,
			SilentNoteOnPolicy = (SilentNoteOnPolicy)0,
			TextEncoding = Encoding.Default,
			InvalidSystemCommonEventParameterValuePolicy = (InvalidSystemCommonEventParameterValuePolicy)1
		});
		Tmap = TempoMapManagingUtilities.GetTempoMap(midi);
	}

	public void OpenFile(string path)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		midi = MidiFile.Read(path, new ReadingSettings
		{
			NoHeaderChunkPolicy = (NoHeaderChunkPolicy)1,
			NotEnoughBytesPolicy = (NotEnoughBytesPolicy)1,
			InvalidChannelEventParameterValuePolicy = (InvalidChannelEventParameterValuePolicy)1,
			InvalidChunkSizePolicy = (InvalidChunkSizePolicy)1,
			InvalidMetaEventParameterValuePolicy = (InvalidMetaEventParameterValuePolicy)1,
			MissedEndOfTrackPolicy = (MissedEndOfTrackPolicy)0,
			UnexpectedTrackChunksCountPolicy = (UnexpectedTrackChunksCountPolicy)0,
			ExtraTrackChunkPolicy = (ExtraTrackChunkPolicy)0,
			UnknownChunkIdPolicy = (UnknownChunkIdPolicy)0,
			SilentNoteOnPolicy = (SilentNoteOnPolicy)0,
			TextEncoding = Encoding.Default
		});
		Tmap = TempoMapManagingUtilities.GetTempoMap(midi);
	}

	public List<string> GetTrackManagers()
	{
		try
		{
			tracks.Clear();
			List<string> list = new List<string>();
			trunks = new List<TrackChunk>();
			foreach (TrackChunk trackChunk in TrackChunkUtilities.GetTrackChunks(midi))
			{
				if (((IEnumerable<Note>)NotesManagingUtilities.ManageNotes(trackChunk, (Comparison<MidiEvent>)null).Notes).Count() != 0)
				{
					tracks.Add(NotesManagingUtilities.ManageNotes(trackChunk, (Comparison<MidiEvent>)null));
					trunks.Add(trackChunk);
				}
			}
			for (int i = 0; i < trunks.Count; i++)
			{
				string item = "Untitled";
				foreach (MidiEvent @event in trunks[i].Events)
				{
					if (@event is SequenceTrackNameEvent)
					{
						SequenceTrackNameEvent val = (SequenceTrackNameEvent)(object)((@event is SequenceTrackNameEvent) ? @event : null);
						item = ((BaseTextEvent)val).Text;
						break;
					}
				}
				list.Add(item);
			}
			return list;
		}
		catch (Exception ex)
		{
			MessageBox.Show($"这个Midi文件读取出错！请使用其他软件重新保存。\r\n异常信息：{ex.Message}\r\n 异常类型{ex.GetType()}", "读取错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return null;
		}
	}

	public void PreProcessChord()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		long num = Convert.ToInt64(((object)midi.TimeDivision).ToString().TrimEnd(" ticks/qnote".ToCharArray()));
		float num2 = 60000f / (float)Bpm / (float)num;
		ChordsManager val = new ChordsManager(trunks.ElementAt(Index).Events, 0L, (Comparison<MidiEvent>)null);
		try
		{
			foreach (Chord item in (TimedObjectsCollection<Chord>)(object)val.Chords)
			{
				if (((IEnumerable<Note>)item.Notes).Count() <= 1)
				{
					continue;
				}
				int num3 = 0;
				if (autoChord)
				{
					long num4 = ((IEnumerable<Note>)item.Notes).First().Length / (((IEnumerable<Note>)item.Notes).Count() + 1);
					foreach (Note item2 in ((IEnumerable<Note>)item.Notes).OrderBy((Note x) => x.NoteNumber))
					{
						item2.Time += num3 * num4;
						item2.Length -= num3 * num4;
						num3++;
					}
					continue;
				}
				num2 = 60000f / (float)Tmap.Tempo.AtTime(((IEnumerable<Note>)item.Notes).First().Time).BeatsPerMinute / (float)num;
				long num5 = (long)((float)MIN_DELAY_TIME_MS_CHORD / num2);
				double num6 = (double)(((IEnumerable<Note>)item.Notes).Count() - 1) / 2.0;
				long num7 = 0L;
				foreach (Note item3 in ((IEnumerable<Note>)item.Notes).OrderBy((Note x) => x.NoteNumber))
				{
					num7 = (long)(((double)num3 - num6) * (double)num5);
					item3.Length = ((item3.Length - num3 * num5 > num5) ? (item3.Length - num3 * num5) : num5);
					item3.Time = ((item3.Time + num7 < 0) ? 0 : (item3.Time + num7));
					num3++;
				}
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	public void PreProcessEvents()
	{
		long num = Convert.ToInt64(((object)midi.TimeDivision).ToString().TrimEnd(" ticks/qnote".ToCharArray()));
		float num2 = 60000f / (float)Bpm / (float)num;
		TimedEvent[] array = (TimedEvent[])(object)new TimedEvent[127];
		NotesManager val = NotesManagingUtilities.ManageNotes(trunks.ElementAt(Index), (Comparison<MidiEvent>)null);
		try
		{
			Note val2 = null;
			foreach (Note item in (TimedObjectsCollection<Note>)(object)val.Notes)
			{
				num2 = 60000f / (float)Tmap.Tempo.AtTime(item.Time).BeatsPerMinute / (float)num;
				long num3 = (long)(85f / num2);
				if (val2 != null && val2.Time + val2.Length + num3 > item.Time)
				{
					val2.Length = ((val2.Length < num3) ? num3 : (val2.Length - num3));
				}
				val2 = item;
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	public void PreProcessNoise()
	{
		long num = Convert.ToInt64(((object)midi.TimeDivision).ToString().TrimEnd(" ticks/qnote".ToCharArray()));
		float num2 = 60000f / (float)Bpm / (float)num;
		TimedEvent[] array = (TimedEvent[])(object)new TimedEvent[127];
		NotesManager val = NotesManagingUtilities.ManageNotes(trunks.ElementAt(Index).Events, (Comparison<MidiEvent>)null);
		try
		{
			foreach (Note item in (TimedObjectsCollection<Note>)(object)val.Notes)
			{
				num2 = 60000f / (float)Tmap.Tempo.AtTime(item.Time).BeatsPerMinute / (float)num;
				long num3 = (long)((float)MIN_DELAY_TIME_MS_EVENT / num2);
				if (item.Length < num3 / 2)
				{
					((TimedObjectsCollection<Note>)(object)val.Notes).Remove((Note[])(object)new Note[1] { item });
				}
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	public void SaveToFile()
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		PreProcessTempoMap();
		for (int i = 0; i < trunks.Count; i++)
		{
			PreProcessNoise();
			PreProcessChord();
			PreProcessEvents();
		}
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = "Midi文件|*.mid";
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			midi.Write(saveFileDialog.FileName, false, (MidiFileFormat)1, new WritingSettings
			{
				TextEncoding = Encoding.Default
			});
		}
	}

	public Queue<KeyPlayList> ArrangeKeyPlaysNew(double speed)
	{
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		EventsCollection events = trunks.ElementAt(Index).Events;
		long num = Convert.ToInt64(((object)midi.TimeDivision).ToString().TrimEnd(" ticks/qnote".ToCharArray()));
		double num2 = 60000.0 / (double)Bpm / (double)num;
		double num3 = 0.0;
		Queue<KeyPlayList> queue = new Queue<KeyPlayList>();
		PreProcessTempoMap();
		PreProcessNoise();
		PreProcessSpeed(speed);
		PreProcessChord();
		PreProcessEvents();
		TimedEventsManager val = TimedEventsManagingUtilities.ManageTimedEvents(events, (Comparison<MidiEvent>)null);
		try
		{
			foreach (TimedEvent item in (TimedObjectsCollection<TimedEvent>)(object)val.Events)
			{
				MidiEvent @event = item.Event;
				MidiEvent val2 = @event;
				NoteOnEvent val3 = (NoteOnEvent)(object)((val2 is NoteOnEvent) ? val2 : null);
				if (val3 == null)
				{
					NoteOffEvent val4 = (NoteOffEvent)(object)((val2 is NoteOffEvent) ? val2 : null);
					if (val4 == null)
					{
						SetTempoEvent val5 = (SetTempoEvent)(object)((val2 is SetTempoEvent) ? val2 : null);
						if (val5 != null)
						{
							num3 += num2 * (double)((MidiEvent)val5).DeltaTime;
							num2 = (double)val5.MicrosecondsPerQuarterNote / (1000.0 * (double)num);
						}
						else
						{
							num3 += (double)(int)(num2 * (double)item.Event.DeltaTime);
						}
					}
					else
					{
						int pitch = (int)(SevenBitNumber.op_Implicit(((NoteEvent)val4).NoteNumber) + Offset);
						num3 += num2 * (double)((MidiEvent)val4).DeltaTime;
						queue.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOff, pitch, num3));
					}
				}
				else
				{
					int pitch2 = (int)(SevenBitNumber.op_Implicit(((NoteEvent)val3).NoteNumber) + Offset);
					num3 += num2 * (double)((MidiEvent)val3).DeltaTime;
					queue.Enqueue(new KeyPlayList(KeyPlayList.NoteEvent.NoteOn, pitch2, num3));
				}
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
		return queue;
	}

	public void PreProcessTempoMap()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		foreach (TrackChunk trunk in trunks)
		{
			TimedEventsManager val = TimedEventsManagingUtilities.ManageTimedEvents(trunk.Events, (Comparison<MidiEvent>)null);
			try
			{
				foreach (ValueChange<Tempo> item in Tmap.Tempo)
				{
					SetTempoEvent val2 = new SetTempoEvent(item.Value.MicrosecondsPerQuarterNote);
					TimedEventsManagingUtilities.AddEvent(val.Events, (MidiEvent)(object)val2, item.Time);
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
	}

	public void PreProcessSpeed(double speed)
	{
		TimedEventsManager val = TimedEventsManagingUtilities.ManageTimedEvents(trunks.ElementAt(Index).Events, (Comparison<MidiEvent>)null);
		try
		{
			foreach (TimedEvent item in (TimedObjectsCollection<TimedEvent>)(object)val.Events)
			{
				item.Time = (long)((double)item.Time * speed);
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	public int PlaybackPause()
	{
		if (playback == null)
		{
			return -1;
		}
		if (!playback.IsRunning)
		{
			return -2;
		}
		playback.Stop();
		return 0;
	}

	public int PlaybackStart(int BPM, EventHandler playbackFinishHandler)
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		if (midi == null)
		{
			return -1;
		}
		if (OutputDevice.GetDevicesCount() == 0)
		{
			return -2;
		}
		if (outputDevice == null && (outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth")) == null)
		{
			outputDevice = OutputDevice.GetAll().ElementAt(0);
		}
		if (playback == null)
		{
			playback = new Playback((IEnumerable<MidiEvent>)trunks.ElementAt(Index).Events, TempoMapManagingUtilities.GetTempoMap(midi), outputDevice, (MidiClockSettings)null);
		}
		playback.Speed = (double)BPM / (double)GetBpm();
		playback.InterruptNotesOnStop = true;
		playback.Start();
		playback.Finished += playbackFinishHandler;
		return 0;
	}

	public int PlaybackPercentGet()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		if (playback.IsRunning)
		{
			MidiTimeSpan val = (MidiTimeSpan)playback.GetCurrentTime((TimeSpanType)4);
			MidiTimeSpan val2 = (MidiTimeSpan)playback.GetDuration((TimeSpanType)4);
			return (int)(val.TimeSpan * 100 / val2.TimeSpan);
		}
		return 0;
	}

	public void PlaybackPercentSet(int process)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		if (playback.IsRunning)
		{
			MidiTimeSpan val = (MidiTimeSpan)playback.GetDuration((TimeSpanType)4);
			MidiTimeSpan val2 = new MidiTimeSpan(val.TimeSpan * process / 100);
			playback.MoveToTime((ITimeSpan)(object)val2);
		}
	}

	public string PlaybackInfo()
	{
		string result = "";
		string pattern = "\\d*:(?<time>.+):\\d+";
		if (playback.IsRunning)
		{
			ITimeSpan currentTime = playback.GetCurrentTime((TimeSpanType)0);
			ITimeSpan duration = playback.GetDuration((TimeSpanType)0);
			result = Regex.Match(((object)currentTime).ToString(), pattern).Groups["time"].Value + "/" + Regex.Match(((object)duration).ToString(), pattern).Groups["time"].Value;
		}
		return result;
	}

	public int PlaybackRestart()
	{
		if (midi == null)
		{
			return -1;
		}
		if (playback == null)
		{
			return -2;
		}
		playback.Stop();
		playback.Dispose();
		playback = null;
		return 0;
	}

	public int GetBpm()
	{
		return (int)Tmap.Tempo.AtTime(0L).BeatsPerMinute;
	}
}
