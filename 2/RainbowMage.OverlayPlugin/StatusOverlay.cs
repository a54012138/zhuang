using System;
using System.Drawing;

namespace RainbowMage.OverlayPlugin;

public class StatusOverlay : OverlayBase<LabelOverlayConfig>
{
	public class OverlayControl
	{
		public StatusOverlay f;

		public LabelOverlayConfig config = new LabelOverlayConfig("喵");

		public void InitializeOverlays(Point p)
		{
			config.IsClickThru = false;
			config.Url = "http://overlay.ffxiv.cat:8088/index.html";
			config.MaxFrameRate = 60;
			config.IsVisible = true;
			config.Size = new Size(250, 150);
			config.Position = p;
			f = new StatusOverlay(config);
			f.Start();
		}

		internal void RegisterOverlay(IOverlay overlay)
		{
		}

		internal void RemoveOverlay(IOverlay overlay)
		{
		}
	}

	public StatusOverlay(LabelOverlayConfig config)
		: base(config, config.Name)
	{
		timer.Stop();
		config.TextChanged += delegate
		{
			UpdateOverlayText();
		};
		config.processChanged += delegate
		{
			UpdateOverlayProcess();
		};
	}

	private void UpdateOverlayProcess()
	{
		try
		{
			string text = CreateEventDispatcherScript(CreateJsonProcess());
			if (base.Overlay != null && base.Overlay.Renderer != null && base.Overlay.Renderer.Browser != null)
			{
				base.Overlay.Renderer.ExecuteScript(text);
			}
			else
			{
				Log((LogLevel)4, "更新: 浏览器未准备好");
			}
		}
		catch (Exception)
		{
		}
	}

	private void UpdateOverlayText()
	{
		try
		{
			string text = CreateEventDispatcherScript(CreateJsonLog());
			if (base.Overlay != null && base.Overlay.Renderer != null && base.Overlay.Renderer.Browser != null)
			{
				base.Overlay.Renderer.ExecuteScript(text);
			}
			else
			{
				Log((LogLevel)4, "更新: 浏览器未准备好");
			}
		}
		catch (Exception ex)
		{
			Log((LogLevel)4, "更新: {1}", base.Name, ex);
		}
	}

	private string CreateEventDispatcherScript(string json)
	{
		return $"document.dispatchEvent(new CustomEvent('onOverlayDataUpdate', {{ detail: {json} }}));";
	}

	internal string CreateJsonLog()
	{
		return $"{{ log: \"{Util.CreateJsonSafeString(base.Config.Text)}\"}}";
	}

	internal string CreateJsonProcess()
	{
		return $"{{process: \"{Util.CreateJsonSafeString(base.Config.Process)}\"}}";
	}

	protected override void Update()
	{
	}
}
