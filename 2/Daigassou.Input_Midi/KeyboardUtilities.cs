using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daigassou.Properties;
using Daigassou.Utils;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;

namespace Daigassou.Input_Midi;

public static class KeyboardUtilities
{
	private static InputDevice wetMidiKeyboard;

	private static readonly object NoteOnlock = new object();

	private static readonly object NoteOfflock = new object();

	private static readonly object noteLock = new object();

	private static readonly Queue<NoteEvent> noteQueue = new Queue<NoteEvent>();

	private static CancellationTokenSource cts = new CancellationTokenSource();

	private static KeyController kc;

	public static int Connect(string name, KeyController _keyController)
	{
		wetMidiKeyboard = InputDevice.GetByName(name);
		try
		{
			wetMidiKeyboard.EventReceived += MidiKeyboard_EventReceived;
			wetMidiKeyboard.StartEventsListening();
			cts = new CancellationTokenSource();
			kc = _keyController;
			kc.UpdateKeyMap();
			Task.Run(delegate
			{
				NoteProcess(cts.Token);
			}, cts.Token);
		}
		catch (Exception ex)
		{
			MessageBox.Show("连接错误 \r\n " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		return 0;
	}

	private static void MidiKeyboard_EventReceived(object sender, MidiEventReceivedEventArgs e)
	{
		Log.overlayLog("收到Note@" + DateTime.Now.ToString("HH: mm:ss.fff") + " ");
		MidiEvent @event = e.Event;
		MidiEvent val = @event;
		NoteOnEvent val2 = (NoteOnEvent)(object)((val is NoteOnEvent) ? val : null);
		if (val2 == null)
		{
			NoteOffEvent val3 = (NoteOffEvent)(object)((val is NoteOffEvent) ? val : null);
			if (val3 != null)
			{
				noteQueue.Enqueue((NoteEvent)(object)val3);
			}
		}
		else
		{
			noteQueue.Enqueue((NoteEvent)(object)val2);
		}
	}

	public static void Disconnect()
	{
		if (wetMidiKeyboard == null || !wetMidiKeyboard.IsListeningForEvents)
		{
			return;
		}
		try
		{
			wetMidiKeyboard.StopEventsListening();
			wetMidiKeyboard.Reset();
			wetMidiKeyboard.EventReceived -= MidiKeyboard_EventReceived;
			((MidiDevice)wetMidiKeyboard).Dispose();
			cts.Cancel();
		}
		catch (Exception ex)
		{
			MessageBox.Show("断开错误 \r\n " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public static List<string> GetKeyboardList()
	{
		List<string> list = new List<string>();
		foreach (InputDevice item in InputDevice.GetAll())
		{
			list.Add(((MidiDevice)item).Name);
		}
		return list;
	}

	public static void NoteProcess(CancellationToken token)
	{
		int minEventMs = (int)Settings.Default.MinEventMs;
		while (!token.IsCancellationRequested)
		{
			NoteEvent val;
			lock (noteLock)
			{
				if (noteQueue.Count <= 0)
				{
					Thread.Sleep(1);
					continue;
				}
				val = noteQueue.Dequeue();
			}
			NoteEvent val2 = val;
			NoteEvent val3 = val2;
			NoteOnEvent val4 = (NoteOnEvent)(object)((val3 is NoteOnEvent) ? val3 : null);
			if (val4 == null)
			{
				NoteOffEvent val5 = (NoteOffEvent)(object)((val3 is NoteOffEvent) ? val3 : null);
				if (val5 != null)
				{
					NoteOff(val5);
					Thread.Sleep(5);
				}
			}
			else
			{
				NoteOn(val4);
				Thread.Sleep(5);
			}
			Thread.Sleep(1);
		}
	}

	public static void NoteOn(NoteOnEvent msg)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		lock (NoteOnlock)
		{
			Log.Debug($"msg  {((NoteEvent)msg).NoteNumber} on at time {DateTime.Now:O}");
			if (Convert.ToInt32(SevenBitNumber.op_Implicit(((NoteEvent)msg).NoteNumber)) <= 84 && Convert.ToInt32(SevenBitNumber.op_Implicit(((NoteEvent)msg).NoteNumber)) >= 48)
			{
				if (SevenBitNumber.op_Implicit(((NoteEvent)msg).Velocity) == 0)
				{
					kc.KeyboardRelease(Convert.ToInt32(SevenBitNumber.op_Implicit(((NoteEvent)msg).NoteNumber)));
				}
				else
				{
					kc.KeyboardPress(Convert.ToInt32(SevenBitNumber.op_Implicit(((NoteEvent)msg).NoteNumber)));
				}
			}
		}
	}

	public static void NoteOff(NoteOffEvent msg)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		lock (NoteOfflock)
		{
			Log.Debug($"msg  {((NoteEvent)msg).NoteNumber} off at time {DateTime.Now:O}");
			if (Convert.ToInt32(SevenBitNumber.op_Implicit(((NoteEvent)msg).NoteNumber)) <= 84 && Convert.ToInt32(SevenBitNumber.op_Implicit(((NoteEvent)msg).NoteNumber)) >= 48)
			{
				kc.KeyboardRelease(Convert.ToInt32(SevenBitNumber.op_Implicit(((NoteEvent)msg).NoteNumber)));
			}
		}
	}
}
