using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BondTech.HotkeyManagement.Win;
using Daigassou.Forms;
using Daigassou.Input_Midi;
using Daigassou.Properties;
using Daigassou.Utils;
using RainbowMage.HtmlRenderer;
using RainbowMage.OverlayPlugin;

namespace Daigassou;

public class MainForm : Form
{
	private delegate void remotePlay(int time, string name);

	private readonly KeyController kc = new KeyController();

	private readonly KeyBindFormOld keyForm37 = new KeyBindFormOld();

	private readonly KeyBindForm8Key keyForm13 = new KeyBindForm8Key();

	private readonly MidiToKey mtk = new MidiToKey();

	private bool _playingFlag;

	private bool _runningFlag;

	private Thread _runningTask;

	private List<string> _tmpScore;

	private CancellationTokenSource cts = new CancellationTokenSource();

	internal HotKeyManager hkm;

	private ArrayList hotkeysArrayList;

	private int pauseTime = 0;

	private bool isCaptureFlag;

	private Queue<KeyPlayList> keyPlayLists;

	private NetworkClass net;

	private StatusOverlay.OverlayControl a;

	private IContainer components = null;

	private ComboBox trackComboBox;

	private Label label2;

	private Button btnFileSelect;

	private TextBox pathTextBox;

	private DateTimePicker dtpSyncTime;

	private OpenFileDialog midFileDiag;

	private Button btnSyncReady;

	private Label label7;

	private Label label5;

	private NumericUpDown nudBpm;

	private RadioButton radioButton2;

	private RadioButton radioButton1;

	private NumericUpDown numericUpDown2;

	private Button btnSwitch;

	private Button btnTimeSync;

	private Button btnStop;

	private Button btnPause;

	private Button btnPlay;

	private ToolStripStatusLabel tlblTime;

	private StatusStrip statusStrip1;

	private System.Windows.Forms.Timer playTimer;

	private ToolStripStatusLabel timeStripStatus;

	private ToolTip tipTsukkomi;

	private ToolStripSplitButton toolStripSplitButton1;

	private Button btn13Key;

	private Button btn37Key;

	private TrackBar tbMidiProcess;

	private ToolStripStatusLabel toolStripStatusLabel1;

	private ToolStripStatusLabel toolStripStatusLabel2;

	private Button btnKeyboardConnect;

	private ComboBox cbMidiKeyboard;

	private Button button1;

	[DllImport("winmm.dll")]
	internal static extern uint timeBeginPeriod(uint period);

	[DllImport("winmm.dll")]
	internal static extern uint timeEndPeriod(uint period);

	public MainForm()
	{
		InitializeComponent();
		formUpdate();
		KeyBinding.LoadConfig();
		timeBeginPeriod(1u);
		ThreadPool.SetMaxThreads(25, 50);
		Task.Run(delegate
		{
			TimeSync();
		});
		Text += $" Ver{Assembly.GetExecutingAssembly().GetName().Version}";
		cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
		KeyController keyController = kc;
		keyController.stopHandler = (KeyController.stopped)Delegate.Combine(keyController.stopHandler, new KeyController.stopped(StopKeyPlay));
	}

	private void InitHotKeyBiding()
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		try
		{
			hkm = new HotKeyManager((IWin32Window)this);
			hkm.Enabled = false;
			if (KeyBinding.hotkeyArrayList == null || KeyBinding.hotkeyArrayList.Count < 5)
			{
				hotkeysArrayList = new ArrayList();
				hotkeysArrayList.Clear();
				hotkeysArrayList.Add((object)new GlobalHotKey("Start", (Modifiers)2, Keys.F10, true));
				hotkeysArrayList.Add((object)new GlobalHotKey("Stop", (Modifiers)2, Keys.F11, true));
				hotkeysArrayList.Add((object)new GlobalHotKey("PitchUp", (Modifiers)2, Keys.F8, true));
				hotkeysArrayList.Add((object)new GlobalHotKey("PitchDown", (Modifiers)2, Keys.F9, true));
				hotkeysArrayList.Add((object)new GlobalHotKey("Pause", (Modifiers)2, Keys.F12, true));
				KeyBinding.hotkeyArrayList = hotkeysArrayList;
			}
			else
			{
				hotkeysArrayList = KeyBinding.hotkeyArrayList;
			}
			((GlobalHotKey)hotkeysArrayList[0]).HotKeyPressed += new GlobalHotKeyEventHandler(Start_HotKeyPressed);
			((GlobalHotKey)hotkeysArrayList[1]).HotKeyPressed += new GlobalHotKeyEventHandler(Stop_HotKeyPressed);
			((GlobalHotKey)hotkeysArrayList[2]).HotKeyPressed += new GlobalHotKeyEventHandler(PitchUp_HotKeyPressed);
			((GlobalHotKey)hotkeysArrayList[3]).HotKeyPressed += new GlobalHotKeyEventHandler(PitchDown_HotKeyPressed);
			((GlobalHotKey)hotkeysArrayList[4]).HotKeyPressed += new GlobalHotKeyEventHandler(Pause_HotKeyPressed);
			bool flag = true;
			foreach (GlobalHotKey hotkeysArray in hotkeysArrayList)
			{
				GlobalHotKey val = hotkeysArray;
				if (val.Enabled)
				{
					try
					{
						hkm.AddGlobalHotKey(val);
					}
					catch (Exception)
					{
						flag = false;
					}
				}
			}
			if (!flag)
			{
				throw new Exception();
			}
		}
		catch (Exception)
		{
			MessageBox.Show(new Form
			{
				TopMost = true
			}, "部分快捷键注册失败,程序可能无法正常运行。\r\n请检查是否有其他程序占用。\r\n点击下方小齿轮重新配置快捷键", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		finally
		{
			hkm.Enabled = true;
		}
	}

	private void Pause_HotKeyPressed(object sender, GlobalHotKeyEventArgs e)
	{
		if (kc.isRunningFlag && kc.isPlayingFlag)
		{
			Log.overlayLog("快捷键：演奏暂停");
			pauseTime = Environment.TickCount;
			kc.isPlayingFlag = false;
		}
	}

	private void formUpdate()
	{
		if (Settings.Default.IsEightKeyLayout)
		{
			btn13Key.ForeColor = Color.WhiteSmoke;
			btn37Key.ForeColor = Color.WhiteSmoke;
			btn13Key.BackColor = Color.FromArgb(255, 110, 128);
			btn37Key.BackColor = Color.Silver;
			btnSwitch.BackgroundImage = Resources.a0;
			btn13Key.Enabled = true;
			btn37Key.Enabled = false;
		}
		else
		{
			btn13Key.ForeColor = Color.WhiteSmoke;
			btn37Key.ForeColor = Color.WhiteSmoke;
			btn37Key.BackColor = Color.FromArgb(255, 110, 128);
			btn13Key.BackColor = Color.Silver;
			btnSwitch.BackgroundImage = Resources.a1;
			btn13Key.Enabled = false;
			btn37Key.Enabled = true;
		}
	}

	private void Start_HotKeyPressed(object sender, GlobalHotKeyEventArgs e)
	{
		if (!kc.isRunningFlag)
		{
			Log.overlayLog("快捷键：演奏开始");
			StartKeyPlayback(1000);
		}
		else
		{
			Log.overlayLog("快捷键：演奏恢复");
			kc.isPlayingFlag = true;
			kc.pauseOffset += Environment.TickCount - pauseTime;
		}
	}

	private void Stop_HotKeyPressed(object sender, GlobalHotKeyEventArgs e)
	{
		Log.overlayLog("快捷键：演奏停止");
		StopKeyPlay();
	}

	private void StopKeyPlay()
	{
		_runningFlag = false;
		_runningTask?.Abort();
		while (_runningTask != null && _runningTask.ThreadState != System.Threading.ThreadState.Stopped && _runningTask.ThreadState != System.Threading.ThreadState.Aborted)
		{
			Thread.Sleep(1);
		}
		lyricPoster.LrcStop();
		btnSyncReady.BackColor = Color.FromArgb(255, 110, 128);
		btnSyncReady.Text = "准备好了";
		kc.ResetKey();
	}

	private void PitchUp_HotKeyPressed(object sender, GlobalHotKeyEventArgs e)
	{
		ParameterController.GetInstance().Pitch++;
		Log.overlayLog($"快捷键：向上移调 当前 {ParameterController.GetInstance().Pitch}");
	}

	private void PitchDown_HotKeyPressed(object sender, GlobalHotKeyEventArgs e)
	{
		ParameterController.GetInstance().Pitch--;
		Log.overlayLog($"快捷键：向下移调 当前 {ParameterController.GetInstance().Pitch}");
	}

	private void StartKeyPlayback(int interval)
	{
		kc.isPlayingFlag = false;
		kc.isRunningFlag = false;
		kc.pauseOffset = 0;
		if (Path.GetExtension(midFileDiag.FileName) != ".mid" && Path.GetExtension(midFileDiag.FileName) != ".midi")
		{
			Log.overlayLog("错误：没有Midi文件");
			MessageBox.Show(new Form
			{
				TopMost = true
			}, "没有导入mini？", "注意", MessageBoxButtons.OK, MessageBoxIcon.Question);
		}
		else
		{
			if (_runningTask != null && (_runningTask.ThreadState == System.Threading.ThreadState.Running || _runningTask.ThreadState == System.Threading.ThreadState.Suspended))
			{
				return;
			}
			_runningTask?.Abort();
			kc.isPlayingFlag = true;
			btnSyncReady.BackColor = Color.Aquamarine;
			btnSyncReady.Text = "中断演奏";
			int num = ((interval < 1000) ? 1000 : interval);
			long sub = 1000 - interval;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Log.overlayLog("文件名：" + Path.GetFileName(midFileDiag.FileName));
			Log.overlayLog($"定时：{num}毫秒后演奏");
			OpenFile(midFileDiag.FileName);
			lyricPoster.LrcStart(midFileDiag.FileName.Replace(".mid", ".mml").Replace(".mml", ".lrc"), interval);
			mtk.GetTrackManagers();
			keyPlayLists = mtk.ArrangeKeyPlaysNew((double)((decimal)mtk.GetBpm() / nudBpm.Value));
			if (interval < 0)
			{
				IEnumerable<KeyPlayList> enumerable = keyPlayLists.Where((KeyPlayList x) => x.TimeMs > (double)sub);
				keyPlayLists = new Queue<KeyPlayList>();
				foreach (KeyPlayList item in enumerable)
				{
					item.TimeMs -= sub;
					keyPlayLists.Enqueue(item);
				}
			}
			stopwatch.Stop();
			_runningFlag = true;
			cts = new CancellationTokenSource();
			_runningTask = createPerformanceTask(cts.Token, interval - (int)stopwatch.ElapsedMilliseconds);
			_runningTask.Priority = ThreadPriority.Highest;
		}
	}

	private void OpenFile(string fileName)
	{
		string extension = Path.GetExtension(fileName);
		string text = extension;
		if (!(text == ".mid"))
		{
			if (text == ".mml")
			{
				byte[] array = MmlMidiConventer.mmlRead(fileName);
				if (array != null)
				{
					mtk.OpenFile(array);
				}
			}
		}
		else
		{
			mtk.OpenFile(fileName);
		}
	}

	private Thread createPerformanceTask(CancellationToken token, int startOffset)
	{
		ParameterController.GetInstance().InternalOffset = (int)numericUpDown2.Value;
		ParameterController.GetInstance().Offset = 0;
		Thread thread = new Thread((ThreadStart)delegate
		{
			kc.KeyPlayBack(keyPlayLists, 1.0, cts.Token, startOffset);
			_runningFlag = false;
		});
		thread.Start();
		return thread;
	}

	private void trackComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		mtk.Index = trackComboBox.SelectedIndex;
	}

	private void selectFileButton_Click(object sender, EventArgs e)
	{
		if (midFileDiag.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		OpenFile(midFileDiag.FileName);
		pathTextBox.Text = Path.GetFileName(midFileDiag.FileName);
		Log.overlayLog("打开文件：" + Path.GetFileName(midFileDiag.FileName));
		_tmpScore = mtk.GetTrackManagers();
		int bpm = mtk.GetBpm();
		List<string> list = new List<string>();
		if (_tmpScore != null)
		{
			for (int i = 0; i < _tmpScore.Count; i++)
			{
				list.Add($"track_{i}:{_tmpScore[i]}");
			}
		}
		trackComboBox.DataSource = list;
		trackComboBox.SelectedIndex = 0;
		if ((decimal)bpm >= nudBpm.Maximum)
		{
			nudBpm.Value = nudBpm.Maximum;
		}
		else if ((decimal)bpm <= nudBpm.Minimum)
		{
			nudBpm.Value = nudBpm.Minimum;
		}
		else
		{
			nudBpm.Value = bpm;
		}
	}

	private void radioButton1_CheckedChanged(object sender, EventArgs e)
	{
		mtk.Offset = EnumPitchOffset.OctaveLower;
	}

	private void radioButton2_CheckedChanged(object sender, EventArgs e)
	{
		mtk.Offset = EnumPitchOffset.None;
	}

	private void radioButton3_CheckedChanged(object sender, EventArgs e)
	{
		mtk.Offset = EnumPitchOffset.OctaveHigher;
	}

	private void numericUpDown1_ValueChanged(object sender, EventArgs e)
	{
		mtk.Bpm = (int)nudBpm.Value;
	}

	private void SyncButton_Click(object sender, EventArgs e)
	{
		TimeSpan timeSpan = dtpSyncTime.Value - DateTime.Now;
		if (kc.isRunningFlag)
		{
			StopKeyPlay();
		}
		else
		{
			StartKeyPlayback((int)timeSpan.TotalMilliseconds + (int)numericUpDown2.Value);
		}
	}

	private void Form1_FormClosing(object sender, FormClosingEventArgs e)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		KeyboardUtilities.Disconnect();
		hkm.Enabled = false;
		ArrayList arrayList = new ArrayList();
		foreach (GlobalHotKey enumerateGlobalHotKey in hkm.EnumerateGlobalHotKeys)
		{
			GlobalHotKey value = enumerateGlobalHotKey;
			arrayList.Add(value);
		}
		foreach (GlobalHotKey item in arrayList)
		{
			GlobalHotKey val = item;
			hkm.RemoveGlobalHotKey(val);
		}
		hkm.Dispose();
		if (a != null && a.f != null)
		{
			a.f.Dispose();
			Renderer.Shutdown();
		}
		StopKeyPlay();
		timeEndPeriod(1u);
	}

	private void keyForm13Button_Click(object sender, EventArgs e)
	{
		keyForm13.ShowDialog();
	}

	private void btnKeyboardConnect_Click(object sender, EventArgs e)
	{
		if (cbMidiKeyboard.SelectedItem == null)
		{
			return;
		}
		if (cbMidiKeyboard.Enabled)
		{
			if (KeyboardUtilities.Connect(cbMidiKeyboard.SelectedItem.ToString(), kc) == 0)
			{
				cbMidiKeyboard.Enabled = false;
				btnKeyboardConnect.BackColor = Color.Aquamarine;
			}
		}
		else
		{
			KeyboardUtilities.Disconnect();
			cbMidiKeyboard.Enabled = true;
			cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
			btnKeyboardConnect.BackColor = Color.FromArgb(255, 110, 128);
		}
	}

	protected override void WndProc(ref Message m)
	{
		try
		{
			int msg = m.Msg;
			int num = msg;
			if (num == 537)
			{
				cbMidiKeyboard.DataSource = KeyboardUtilities.GetKeyboardList();
			}
		}
		catch (Exception)
		{
		}
		base.WndProc(ref m);
	}

	private void cbMidiKeyboard_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void btnSwitch_Click(object sender, EventArgs e)
	{
		if (Settings.Default.IsEightKeyLayout)
		{
			Settings.Default.IsEightKeyLayout = false;
		}
		else
		{
			Settings.Default.IsEightKeyLayout = true;
		}
		Settings.Default.Save();
		formUpdate();
	}

	private void btn37key_Click(object sender, EventArgs e)
	{
		keyForm37.ShowDialog();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		new AboutForm(kc).ShowDialog();
	}

	private void TimeSync()
	{
		try
		{
			Log.overlayLog("时间同步：NTP请求发送");
			double errorMilliseconds;
			TimeSpan offset = new NtpClient(Settings.Default.NtpServer).GetOffset(out errorMilliseconds);
			Log.overlayLog($"时间同步：与北京时间相差{offset.Milliseconds}毫秒");
			if (CommonUtilities.SetSystemDateTime.SetLocalTimeByStr(DateTime.Now.AddMilliseconds(offset.TotalMilliseconds * -0.5)))
			{
				tlblTime.Text = "本地时钟已同步";
			}
		}
		catch (Exception)
		{
			tlblTime.Text = "设置时间出错";
		}
	}

	private void Playback_Finished(object sender, EventArgs e)
	{
		Invoke((Action)delegate
		{
			btnStop_Click(sender, e);
		});
	}

	private void btnPlay_Click(object sender, EventArgs e)
	{
		if (mtk.PlaybackStart((int)nudBpm.Value, Playback_Finished) == 0)
		{
			mtk.PlaybackPercentSet(tbMidiProcess.Value);
			btnPlay.BackgroundImage = Resources.c_play_1;
			btnPause.BackgroundImage = Resources.c_pause;
			btnStop.BackgroundImage = Resources.c_stop;
			tbMidiProcess.Visible = true;
			_playingFlag = true;
		}
	}

	private void btnStop_Click(object sender, EventArgs e)
	{
		if (mtk.PlaybackRestart() == 0)
		{
			btnPlay.BackgroundImage = Resources.c_play;
			btnPause.BackgroundImage = Resources.c_pause;
			btnStop.BackgroundImage = Resources.c_stop_1;
			tbMidiProcess.Value = 0;
			tbMidiProcess.Visible = false;
			_playingFlag = false;
		}
	}

	private void btnPause_Click(object sender, EventArgs e)
	{
		if (mtk.PlaybackPause() == 0)
		{
			btnPlay.BackgroundImage = Resources.c_play;
			btnPause.BackgroundImage = Resources.c_pause_1;
			btnStop.BackgroundImage = Resources.c_stop;
			_playingFlag = false;
		}
	}

	private void BtnTimeSync_Click(object sender, EventArgs e)
	{
		if (isCaptureFlag)
		{
			net._shouldStop = true;
			isCaptureFlag = false;
			(sender as Button).Text = "网络同步";
			(sender as Button).BackColor = Color.FromArgb(255, 110, 128);
			return;
		}
		TimeSync();
		net = new NetworkClass();
		net.Play += Net_Play;
		try
		{
			List<Process> list = FFProcess.FindFFXIVProcess();
			if (list.Count == 1)
			{
				Task.Run(delegate
				{
					net.Run((uint)FFProcess.FindFFXIVProcess().First().Id);
				});
				isCaptureFlag = true;
				(sender as Button).Text = "停止同步";
				(sender as Button).BackColor = Color.Aquamarine;
			}
			else if (list.Count >= 2)
			{
				uint id = 0u;
				PidSelect pidSelect = new PidSelect();
				pidSelect.GetPid = (PidSelect.PidSelector)Delegate.Combine(pidSelect.GetPid, (PidSelect.PidSelector)delegate(int x)
				{
					id = (uint)x;
				});
				int num = (int)pidSelect.ShowDialog();
				Task.Run(delegate
				{
					net.Run(id);
				});
				isCaptureFlag = true;
				(sender as Button).Text = "停止同步";
				(sender as Button).BackColor = Color.Aquamarine;
			}
			else
			{
				MessageBox.Show(new Form
				{
					TopMost = true
				}, "你开游戏了吗？", "？", MessageBoxButtons.OK, MessageBoxIcon.Question);
			}
		}
		catch (Exception)
		{
		}
	}

	private void Net_Play(object sender, PlayEvent e)
	{
		if (base.InvokeRequired)
		{
			if (e.Mode == 0)
			{
				remotePlay method = NetPlay;
				Invoke(method, e.Time, e.Text);
			}
			else if (e.Mode == 1)
			{
				remotePlay method2 = NetStop;
				Invoke(method2, e.Time, e.Text);
			}
		}
		else if (e.Mode == 0)
		{
			NetPlay(e.Time, e.Text);
		}
		else if (e.Mode == 1)
		{
			NetStop(e.Time, e.Text);
		}
	}

	private void NetPlay(int time, string name)
	{
		DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(time);
		dtpSyncTime.Value = dateTime;
		double totalMilliseconds = (dateTime - DateTime.Now).TotalMilliseconds;
		StartKeyPlayback((int)totalMilliseconds + (int)numericUpDown2.Value);
		Log.overlayLog("网络控制：" + name.Trim().Replace("\0", string.Empty) + "发起倒计时，目标时间:" + dateTime.ToString("HH:mm:ss"));
		tlblTime.Text = string.Format("{0}发起倒计时:{1}毫秒", name.Trim().Replace("\0", string.Empty), totalMilliseconds);
	}

	private void NetStop(int time, string name)
	{
		StopKeyPlay();
		Log.overlayLog("网络控制：" + name.Trim().Replace("\0", string.Empty) + "停止了演奏");
		tlblTime.Text = name.Trim().Replace("\0", string.Empty) + "停止了演奏";
	}

	private void PlayTimer_Tick(object sender, EventArgs e)
	{
		if (_playingFlag)
		{
			int value = mtk.PlaybackPercentGet();
			tbMidiProcess.Value = value;
		}
		timeStripStatus.Text = DateTime.Now.ToString("T");
	}

	private void DateTimePicker1_KeyDown(object sender, KeyEventArgs e)
	{
		if (!e.Alt && !e.Shift && e.Control && e.KeyCode == Keys.V)
		{
			string s = Convert.ToString(Clipboard.GetDataObject().GetData(DataFormats.Text));
			try
			{
				DateTime value = DateTime.ParseExact(s, "HH:mm:ss", CultureInfo.CurrentCulture);
				dtpSyncTime.Value = value;
				return;
			}
			catch (Exception)
			{
				return;
			}
		}
		if (!e.Alt && !e.Shift && e.Control && e.KeyCode == Keys.C)
		{
			string dataObject = dtpSyncTime.Value.ToString("HH:mm:ss");
			Clipboard.SetDataObject(dataObject);
		}
	}

	private void TrackComboBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Alt && e.Control && e.Shift && e.KeyCode == Keys.S)
		{
			mtk.SaveToFile();
		}
	}

	private void ToolStripSplitButton1_ButtonClick(object sender, EventArgs e)
	{
		ConfigForm configForm = new ConfigForm(hotkeysArrayList, kc, hkm);
		configForm.ShowDialog();
	}

	private void MainForm_Load(object sender, EventArgs e)
	{
		InitHotKeyBiding();
	}

	private void toolStripStatusLabel1_Click(object sender, EventArgs e)
	{
		try
		{
			if (a != null)
			{
				a.config.IsVisible = !a.config.IsVisible;
				return;
			}
			a = new StatusOverlay.OverlayControl();
			a.InitializeOverlays(PointToScreen(new Point(base.Width, base.Height - 150)));
			Log.log = a.config;
		}
		catch (NullReferenceException)
		{
			MessageBox.Show(new Form
			{
				TopMost = true
			}, "悬浮窗库文件似乎不完全", "？", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnSyncReady_MouseClick(object sender, MouseEventArgs e)
	{
		SyncButton_Click(sender, e);
	}

	private void label4_DoubleClick(object sender, EventArgs e)
	{
	}

	private void label4_Click(object sender, EventArgs e)
	{
		DateTime dateTime = DateTime.Now.AddMinutes(2.0);
		DateTime value = ((dateTime.Minute % 2 != 1) ? new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0) : new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute - 1, 0));
		dtpSyncTime.Value = value;
		SyncButton_Click(sender, e);
	}

	private void btnSyncReady_Click(object sender, EventArgs e)
	{
	}

	private void tbMidiProcess_Scroll(object sender, EventArgs e)
	{
		mtk.PlaybackPercentSet(tbMidiProcess.Value);
	}

	private void button1_Click_1(object sender, EventArgs e)
	{
		Process.Start(Application.StartupPath + "\\shuoming.png");
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Daigassou.MainForm));
		this.trackComboBox = new System.Windows.Forms.ComboBox();
		this.tbMidiProcess = new System.Windows.Forms.TrackBar();
		this.btnFileSelect = new System.Windows.Forms.Button();
		this.pathTextBox = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.btnTimeSync = new System.Windows.Forms.Button();
		this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
		this.btnSyncReady = new System.Windows.Forms.Button();
		this.dtpSyncTime = new System.Windows.Forms.DateTimePicker();
		this.midFileDiag = new System.Windows.Forms.OpenFileDialog();
		this.nudBpm = new System.Windows.Forms.NumericUpDown();
		this.radioButton2 = new System.Windows.Forms.RadioButton();
		this.radioButton1 = new System.Windows.Forms.RadioButton();
		this.label5 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.btn37Key = new System.Windows.Forms.Button();
		this.btn13Key = new System.Windows.Forms.Button();
		this.tlblTime = new System.Windows.Forms.ToolStripStatusLabel();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.timeStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
		this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
		this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
		this.playTimer = new System.Windows.Forms.Timer(this.components);
		this.tipTsukkomi = new System.Windows.Forms.ToolTip(this.components);
		this.btnKeyboardConnect = new System.Windows.Forms.Button();
		this.cbMidiKeyboard = new System.Windows.Forms.ComboBox();
		this.btnSwitch = new System.Windows.Forms.Button();
		this.btnPlay = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.btnPause = new System.Windows.Forms.Button();
		this.btnStop = new System.Windows.Forms.Button();
		System.Windows.Forms.RadioButton radioButton = new System.Windows.Forms.RadioButton();
		((System.ComponentModel.ISupportInitialize)this.tbMidiProcess).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudBpm).BeginInit();
		this.statusStrip1.SuspendLayout();
		base.SuspendLayout();
		radioButton.AutoSize = true;
		radioButton.Font = new System.Drawing.Font("微软雅黑", 12f);
		radioButton.Location = new System.Drawing.Point(356, 69);
		radioButton.Name = "radioButton3";
		radioButton.Size = new System.Drawing.Size(69, 25);
		radioButton.TabIndex = 7;
		radioButton.Text = "高8度";
		this.tipTsukkomi.SetToolTip(radioButton, "点这里是设置音高的");
		radioButton.UseVisualStyleBackColor = true;
		radioButton.CheckedChanged += new System.EventHandler(radioButton3_CheckedChanged);
		this.trackComboBox.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.trackComboBox.ForeColor = System.Drawing.Color.Gray;
		this.trackComboBox.FormattingEnabled = true;
		this.trackComboBox.Location = new System.Drawing.Point(244, 2);
		this.trackComboBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.trackComboBox.Name = "trackComboBox";
		this.trackComboBox.Size = new System.Drawing.Size(123, 28);
		this.trackComboBox.TabIndex = 1;
		this.tipTsukkomi.SetToolTip(this.trackComboBox, "点这里选择音轨");
		this.trackComboBox.SelectedIndexChanged += new System.EventHandler(trackComboBox_SelectedIndexChanged);
		this.trackComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(TrackComboBox_KeyDown);
		this.tbMidiProcess.BackColor = System.Drawing.Color.White;
		this.tbMidiProcess.Location = new System.Drawing.Point(3, 104);
		this.tbMidiProcess.Maximum = 100;
		this.tbMidiProcess.Name = "tbMidiProcess";
		this.tbMidiProcess.Size = new System.Drawing.Size(421, 45);
		this.tbMidiProcess.TabIndex = 28;
		this.tbMidiProcess.TickStyle = System.Windows.Forms.TickStyle.None;
		this.tipTsukkomi.SetToolTip(this.tbMidiProcess, "我给你讲哦这个东西叫进度条的说\r\n只要用力拖就可以改变试听的位置嗷！");
		this.tbMidiProcess.Visible = false;
		this.tbMidiProcess.Scroll += new System.EventHandler(tbMidiProcess_Scroll);
		this.btnFileSelect.BackColor = System.Drawing.Color.White;
		this.btnFileSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnFileSelect.Font = new System.Drawing.Font("微软雅黑", 7f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.btnFileSelect.ForeColor = System.Drawing.Color.Black;
		this.btnFileSelect.Location = new System.Drawing.Point(3, 2);
		this.btnFileSelect.Name = "btnFileSelect";
		this.btnFileSelect.Size = new System.Drawing.Size(60, 26);
		this.btnFileSelect.TabIndex = 7;
		this.btnFileSelect.Text = "导入mini";
		this.btnFileSelect.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.tipTsukkomi.SetToolTip(this.btnFileSelect, "点这里选择Midi文件或者mml文件");
		this.btnFileSelect.UseVisualStyleBackColor = false;
		this.btnFileSelect.Click += new System.EventHandler(selectFileButton_Click);
		this.pathTextBox.Enabled = false;
		this.pathTextBox.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.pathTextBox.ForeColor = System.Drawing.Color.Gray;
		this.pathTextBox.Location = new System.Drawing.Point(75, 2);
		this.pathTextBox.Name = "pathTextBox";
		this.pathTextBox.Size = new System.Drawing.Size(98, 26);
		this.pathTextBox.TabIndex = 6;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5f);
		this.label2.ForeColor = System.Drawing.Color.Gray;
		this.label2.Location = new System.Drawing.Point(179, 5);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(65, 20);
		this.label2.TabIndex = 4;
		this.label2.Text = "选择音轨";
		this.btnTimeSync.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		this.btnTimeSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnTimeSync.Font = new System.Drawing.Font("微软雅黑", 9f);
		this.btnTimeSync.ForeColor = System.Drawing.Color.White;
		this.btnTimeSync.Location = new System.Drawing.Point(97, 137);
		this.btnTimeSync.Name = "btnTimeSync";
		this.btnTimeSync.Size = new System.Drawing.Size(119, 26);
		this.btnTimeSync.TabIndex = 6;
		this.btnTimeSync.Text = "同步演奏时间校准";
		this.tipTsukkomi.SetToolTip(this.btnTimeSync, "开启或停止网络同步\r\n点击后\r\n1.接收到合奏停止数据包后自动停止\r\n2.接收到标点数据包后自动停止\r\n3.接收到小队倒计时后数据包后自动开始定时\r\n点一次就行了！\r\n一次！");
		this.btnTimeSync.UseVisualStyleBackColor = false;
		this.btnTimeSync.Click += new System.EventHandler(BtnTimeSync_Click);
		this.numericUpDown2.Font = new System.Drawing.Font("微软雅黑", 10.5f);
		this.numericUpDown2.Location = new System.Drawing.Point(222, 137);
		this.numericUpDown2.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
		this.numericUpDown2.Minimum = new decimal(new int[4] { 10000, 0, 0, -2147483648 });
		this.numericUpDown2.Name = "numericUpDown2";
		this.numericUpDown2.Size = new System.Drawing.Size(71, 26);
		this.numericUpDown2.TabIndex = 5;
		this.tipTsukkomi.SetToolTip(this.numericUpDown2, "海外党适用\r\n当队员们与服务器延迟过大的时候\r\n用于补正Ping值\r\n设置方法为全员平均Ping值-每个人的Ping值");
		this.btnSyncReady.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		this.btnSyncReady.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSyncReady.Font = new System.Drawing.Font("微软雅黑", 9f);
		this.btnSyncReady.ForeColor = System.Drawing.Color.White;
		this.btnSyncReady.Location = new System.Drawing.Point(299, 137);
		this.btnSyncReady.Name = "btnSyncReady";
		this.btnSyncReady.Size = new System.Drawing.Size(119, 26);
		this.btnSyncReady.TabIndex = 3;
		this.btnSyncReady.Text = "同步演奏延迟";
		this.tipTsukkomi.SetToolTip(this.btnSyncReady, "点击后进游戏等待就可以了\r\n点一次就行了嗷！\r\n");
		this.btnSyncReady.UseVisualStyleBackColor = false;
		this.btnSyncReady.Click += new System.EventHandler(btnSyncReady_Click);
		this.btnSyncReady.MouseDown += new System.Windows.Forms.MouseEventHandler(btnSyncReady_MouseClick);
		this.dtpSyncTime.CalendarFont = new System.Drawing.Font("微软雅黑", 10.5f);
		this.dtpSyncTime.CustomFormat = "HH-mm-ss";
		this.dtpSyncTime.Font = new System.Drawing.Font("微软雅黑", 10.5f);
		this.dtpSyncTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
		this.dtpSyncTime.Location = new System.Drawing.Point(3, 137);
		this.dtpSyncTime.Name = "dtpSyncTime";
		this.dtpSyncTime.ShowUpDown = true;
		this.dtpSyncTime.Size = new System.Drawing.Size(88, 26);
		this.dtpSyncTime.TabIndex = 1;
		this.tipTsukkomi.SetToolTip(this.dtpSyncTime, "合奏用\r\n与队友们设定同样的时间后\r\n点击[准备好了]，就可以自动合奏\r\n*时间支持复制粘贴*\r\n点一下按Ctrl+C，不用全选！");
		this.dtpSyncTime.KeyDown += new System.Windows.Forms.KeyEventHandler(DateTimePicker1_KeyDown);
		this.midFileDiag.FileName = "";
		this.midFileDiag.Filter = ".mid文件|*.mid|.mml文件|*.mml";
		this.nudBpm.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.nudBpm.ForeColor = System.Drawing.Color.Gray;
		this.nudBpm.Location = new System.Drawing.Point(345, 34);
		this.nudBpm.Maximum = new decimal(new int[4] { 250, 0, 0, 0 });
		this.nudBpm.Minimum = new decimal(new int[4] { 40, 0, 0, 0 });
		this.nudBpm.Name = "nudBpm";
		this.nudBpm.Size = new System.Drawing.Size(72, 29);
		this.nudBpm.TabIndex = 9;
		this.tipTsukkomi.SetToolTip(this.nudBpm, "点这里可以设置速度");
		this.nudBpm.Value = new decimal(new int[4] { 80, 0, 0, 0 });
		this.nudBpm.ValueChanged += new System.EventHandler(numericUpDown1_ValueChanged);
		this.radioButton2.AutoSize = true;
		this.radioButton2.Checked = true;
		this.radioButton2.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.radioButton2.Location = new System.Drawing.Point(290, 69);
		this.radioButton2.Name = "radioButton2";
		this.radioButton2.Size = new System.Drawing.Size(60, 25);
		this.radioButton2.TabIndex = 6;
		this.radioButton2.TabStop = true;
		this.radioButton2.Text = "原始";
		this.tipTsukkomi.SetToolTip(this.radioButton2, "点这里是设置音高的");
		this.radioButton2.UseVisualStyleBackColor = true;
		this.radioButton2.CheckedChanged += new System.EventHandler(radioButton2_CheckedChanged);
		this.radioButton1.AutoSize = true;
		this.radioButton1.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.radioButton1.Location = new System.Drawing.Point(215, 69);
		this.radioButton1.Name = "radioButton1";
		this.radioButton1.Size = new System.Drawing.Size(69, 25);
		this.radioButton1.TabIndex = 5;
		this.radioButton1.Text = "低8度";
		this.tipTsukkomi.SetToolTip(this.radioButton1, "点这里是设置音高的");
		this.radioButton1.UseVisualStyleBackColor = true;
		this.radioButton1.CheckedChanged += new System.EventHandler(radioButton1_CheckedChanged);
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label5.ForeColor = System.Drawing.Color.Gray;
		this.label5.Location = new System.Drawing.Point(144, 72);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(65, 20);
		this.label5.TabIndex = 1;
		this.label5.Text = "音高调整";
		this.tipTsukkomi.SetToolTip(this.label5, "点这里是设置音高的");
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label7.ForeColor = System.Drawing.Color.Gray;
		this.label7.Location = new System.Drawing.Point(217, 40);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(118, 20);
		this.label7.TabIndex = 2;
		this.label7.Text = "节拍速度,40~250";
		this.btn37Key.BackColor = System.Drawing.Color.Silver;
		this.btn37Key.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btn37Key.Font = new System.Drawing.Font("微软雅黑", 9f);
		this.btn37Key.ForeColor = System.Drawing.Color.WhiteSmoke;
		this.btn37Key.Location = new System.Drawing.Point(141, 34);
		this.btn37Key.Name = "btn37Key";
		this.btn37Key.Size = new System.Drawing.Size(60, 26);
		this.btn37Key.TabIndex = 18;
		this.btn37Key.Text = "37键设置";
		this.tipTsukkomi.SetToolTip(this.btn37Key, "是开启全音阶的布局啦");
		this.btn37Key.UseVisualStyleBackColor = false;
		this.btn37Key.Click += new System.EventHandler(btn37key_Click);
		this.btn13Key.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		this.btn13Key.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btn13Key.Font = new System.Drawing.Font("微软雅黑", 9f);
		this.btn13Key.ForeColor = System.Drawing.Color.WhiteSmoke;
		this.btn13Key.Location = new System.Drawing.Point(3, 34);
		this.btn13Key.Name = "btn13Key";
		this.btn13Key.Size = new System.Drawing.Size(60, 26);
		this.btn13Key.TabIndex = 17;
		this.btn13Key.Text = "13键设置";
		this.tipTsukkomi.SetToolTip(this.btn13Key, "就是默认的那个键位布局啦");
		this.btn13Key.UseVisualStyleBackColor = false;
		this.btn13Key.Click += new System.EventHandler(keyForm13Button_Click);
		this.tlblTime.BackColor = System.Drawing.Color.Transparent;
		this.tlblTime.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tlblTime.ForeColor = System.Drawing.Color.Gray;
		this.tlblTime.Name = "tlblTime";
		this.tlblTime.Size = new System.Drawing.Size(68, 21);
		this.tlblTime.Text = "时钟未同步";
		this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.tlblTime, this.timeStripStatus, this.toolStripSplitButton1, this.toolStripStatusLabel1, this.toolStripStatusLabel2 });
		this.statusStrip1.Location = new System.Drawing.Point(0, 686);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(429, 26);
		this.statusStrip1.TabIndex = 18;
		this.statusStrip1.Text = "statusStrip1";
		this.timeStripStatus.BackColor = System.Drawing.Color.Transparent;
		this.timeStripStatus.Name = "timeStripStatus";
		this.timeStripStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.timeStripStatus.Size = new System.Drawing.Size(56, 21);
		this.timeStripStatus.Text = "20:00:00";
		this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripSplitButton1.Image = Daigassou.Properties.Resources.s2;
		this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripSplitButton1.Name = "toolStripSplitButton1";
		this.toolStripSplitButton1.Size = new System.Drawing.Size(36, 24);
		this.toolStripSplitButton1.Text = "toolStripSplitButton1";
		this.toolStripSplitButton1.ButtonClick += new System.EventHandler(ToolStripSplitButton1_ButtonClick);
		this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent;
		this.toolStripStatusLabel1.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Gray;
		this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
		this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 21);
		this.toolStripStatusLabel1.Text = "快捷键设置";
		this.toolStripStatusLabel2.BackColor = System.Drawing.Color.Transparent;
		this.toolStripStatusLabel2.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.Gray;
		this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
		this.toolStripStatusLabel2.Size = new System.Drawing.Size(159, 21);
		this.toolStripStatusLabel2.Text = "播放代码引自AmanoTooko";
		this.playTimer.Enabled = true;
		this.playTimer.Tick += new System.EventHandler(PlayTimer_Tick);
		this.btnKeyboardConnect.BackColor = System.Drawing.Color.FromArgb(255, 110, 128);
		this.btnKeyboardConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnKeyboardConnect.Font = new System.Drawing.Font("微软雅黑", 10f);
		this.btnKeyboardConnect.ForeColor = System.Drawing.Color.White;
		this.btnKeyboardConnect.Location = new System.Drawing.Point(286, 485);
		this.btnKeyboardConnect.Name = "btnKeyboardConnect";
		this.btnKeyboardConnect.Size = new System.Drawing.Size(81, 29);
		this.btnKeyboardConnect.TabIndex = 17;
		this.btnKeyboardConnect.Text = "开始连接";
		this.tipTsukkomi.SetToolTip(this.btnKeyboardConnect, "如果你有Midi键盘或其他Midi设备可以插上在这里连接\r\n没有的就不要凑热闹了！\r\n蓝牙的不行！LaunchPad你自己说你是Midi键盘吗！\r\n樱桃键盘不行！Filco也不行！\r\nHHKB也不行！带不带RGB都不行！\r\n王总这不是钱的问题！\r\n\r\n");
		this.btnKeyboardConnect.UseVisualStyleBackColor = false;
		this.btnKeyboardConnect.Click += new System.EventHandler(btnKeyboardConnect_Click);
		this.cbMidiKeyboard.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.cbMidiKeyboard.ForeColor = System.Drawing.Color.Gray;
		this.cbMidiKeyboard.FormattingEnabled = true;
		this.cbMidiKeyboard.Location = new System.Drawing.Point(113, 501);
		this.cbMidiKeyboard.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.cbMidiKeyboard.Name = "cbMidiKeyboard";
		this.cbMidiKeyboard.Size = new System.Drawing.Size(154, 28);
		this.cbMidiKeyboard.TabIndex = 8;
		this.tipTsukkomi.SetToolTip(this.cbMidiKeyboard, "如果你有Midi键盘可以插上在这里连接\r\n没有的就不要凑热闹了！\r\n蓝牙的不行！LaunchPad你自己说你是Midi键盘吗！\r\n樱桃键盘不行！Filco也不行！\r\nHHKB也不行！带不带RGB都不行！\r\n王总这不是钱的问题！\r\n");
		this.cbMidiKeyboard.SelectedIndexChanged += new System.EventHandler(cbMidiKeyboard_SelectedIndexChanged);
		this.btnSwitch.BackColor = System.Drawing.Color.Transparent;
		this.btnSwitch.BackgroundImage = Daigassou.Properties.Resources.a0;
		this.btnSwitch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.btnSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSwitch.Font = new System.Drawing.Font("微软雅黑", 10f);
		this.btnSwitch.ForeColor = System.Drawing.Color.White;
		this.btnSwitch.Location = new System.Drawing.Point(75, 34);
		this.btnSwitch.Name = "btnSwitch";
		this.btnSwitch.Size = new System.Drawing.Size(60, 26);
		this.btnSwitch.TabIndex = 6;
		this.tipTsukkomi.SetToolTip(this.btnSwitch, "点一下可以切换8键和22键\r\n我当然是建议用22键啦！");
		this.btnSwitch.UseVisualStyleBackColor = false;
		this.btnSwitch.Click += new System.EventHandler(btnSwitch_Click);
		this.btnPlay.AutoSize = true;
		this.btnPlay.BackColor = System.Drawing.Color.White;
		this.btnPlay.BackgroundImage = Daigassou.Properties.Resources.c_play;
		this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnPlay.ForeColor = System.Drawing.Color.White;
		this.btnPlay.Location = new System.Drawing.Point(56, 69);
		this.btnPlay.Name = "btnPlay";
		this.btnPlay.Size = new System.Drawing.Size(28, 29);
		this.btnPlay.TabIndex = 22;
		this.tipTsukkomi.SetToolTip(this.btnPlay, "这是西瓜视频的Logo\r\n真的不是试听的播放键");
		this.btnPlay.UseVisualStyleBackColor = false;
		this.btnPlay.Click += new System.EventHandler(btnPlay_Click);
		this.button1.ForeColor = System.Drawing.Color.Red;
		this.button1.Location = new System.Drawing.Point(373, 5);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(44, 23);
		this.button1.TabIndex = 29;
		this.button1.Text = "说明";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click_1);
		this.btnPause.BackgroundImage = Daigassou.Properties.Resources.c_pause;
		this.btnPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnPause.ForeColor = System.Drawing.Color.Transparent;
		this.btnPause.Location = new System.Drawing.Point(109, 69);
		this.btnPause.Name = "btnPause";
		this.btnPause.Size = new System.Drawing.Size(29, 29);
		this.btnPause.TabIndex = 23;
		this.btnPause.UseVisualStyleBackColor = true;
		this.btnPause.Click += new System.EventHandler(btnPause_Click);
		this.btnStop.BackColor = System.Drawing.Color.Transparent;
		this.btnStop.BackgroundImage = Daigassou.Properties.Resources.c_stop;
		this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnStop.ForeColor = System.Drawing.Color.Transparent;
		this.btnStop.Location = new System.Drawing.Point(-1, 69);
		this.btnStop.Name = "btnStop";
		this.btnStop.Size = new System.Drawing.Size(31, 29);
		this.btnStop.TabIndex = 24;
		this.btnStop.UseVisualStyleBackColor = false;
		this.btnStop.Click += new System.EventHandler(btnStop_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(429, 712);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.btn13Key);
		base.Controls.Add(this.btnTimeSync);
		base.Controls.Add(this.btn37Key);
		base.Controls.Add(this.btnKeyboardConnect);
		base.Controls.Add(this.btnSwitch);
		base.Controls.Add(this.numericUpDown2);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.radioButton1);
		base.Controls.Add(this.radioButton2);
		base.Controls.Add(this.cbMidiKeyboard);
		base.Controls.Add(radioButton);
		base.Controls.Add(this.btnSyncReady);
		base.Controls.Add(this.btnFileSelect);
		base.Controls.Add(this.nudBpm);
		base.Controls.Add(this.pathTextBox);
		base.Controls.Add(this.btnPause);
		base.Controls.Add(this.dtpSyncTime);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.statusStrip1);
		base.Controls.Add(this.btnStop);
		base.Controls.Add(this.tbMidiProcess);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.trackComboBox);
		base.Controls.Add(this.btnPlay);
		this.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		base.MaximizeBox = false;
		base.Name = "MainForm";
		this.Text = "大合奏!";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Form1_FormClosing);
		base.Load += new System.EventHandler(MainForm_Load);
		((System.ComponentModel.ISupportInitialize)this.tbMidiProcess).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudBpm).EndInit();
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
