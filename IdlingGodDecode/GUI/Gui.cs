using Assets.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class Gui : MonoBehaviour
	{
		public const string LowerText = "lowerText";

		public GUISkin blackSkin;

		public GUISkin blueSkin;

		public GUISkin redSkin;

		public GUISkin whiteSkin;

		public static GUISkin ChosenSkin = null;

		public static Color MainColor = Color.white;

		public AudioSource bgMusic;

		public AudioClip bgMusicMp3;

		public AudioSource silentBgMusic;

		public AudioClip silentBgMusicWave;

		public static bool SetFont = true;

		private void Awake()
		{
			if (App.State == null)
			{
				return;
			}
			Application.targetFrameRate = App.State.GameSettings.Framerate;
			QualitySettings.vSyncCount = 0;
		}

		public void OnApplicationPause(bool paused)
		{
			if (paused)
			{
				App.State.TimeStampGameClosed = App.ServerTime;
				App.SaveGameState();
			}
			else
			{
				MainUi.Instance.Init(false);
				App.ServerTime = 0L;
				App.OfflineStatsChecked = false;
			}
		}

		private void Start()
		{
			try
			{
				GuiBase.InitImages();
				MainUi.Instance.Init(false);
			}
			catch (Exception ex)
			{
				Log.Error("Error while initializing: " + ex.Message);
				GuiBase.InitImages();
				MainUi.Instance.Init(false);
			}
		}

		public void Update()
		{
			MainUi.Instance.Update();
		}

		private void OnGUI()
		{
			if (App.State == null)
			{
				return;
			}
			Gui.MainColor = Color.white;
			if (this.blackSkin != null && App.State.GameSettings.UIStyle == 0)
			{
				GUI.skin = this.blackSkin;
				Gui.ChosenSkin = this.blackSkin;
			}
			else if (this.blueSkin != null && App.State.GameSettings.UIStyle == 1)
			{
				GUI.skin = this.blueSkin;
				Gui.ChosenSkin = this.blueSkin;
			}
			else if (this.redSkin != null && App.State.GameSettings.UIStyle == 2)
			{
				GUI.skin = this.redSkin;
				Gui.ChosenSkin = this.redSkin;
			}
			else if (this.whiteSkin != null && App.State.GameSettings.UIStyle == 3)
			{
				GUI.skin = this.whiteSkin;
				Gui.ChosenSkin = this.whiteSkin;
				Gui.MainColor = Color.black;
			}
			if (Gui.SetFont)
			{
				if (App.State.GameSettings.FontType == 0)
				{
					GUI.skin.font = (Font)Resources.Load("Fonts/Roboto-Regular");
				}
				else if (App.State.GameSettings.FontType == 1)
				{
					GUI.skin.font = (Font)Resources.Load("Fonts/OpenSans-Regular");
				}
				else if (App.State.GameSettings.FontType == 2)
				{
					GUI.skin.font = (Font)Resources.Load("Fonts/FiraSans-Light");
				}
				else if (App.State.GameSettings.FontType == 3)
				{
					GUI.skin.font = (Font)Resources.Load("Fonts/Aleo-Light");
				}
				else if (App.State.GameSettings.FontType == 4)
				{
					GUI.skin.font = (Font)Resources.Load("Fonts/Oswald-Light");
				}
				Gui.SetFont = false;
			}
			base.GetComponent<Camera>().backgroundColor = SettingsUi.Instance.BGColor;
			if (this.bgMusic != null)
			{
				if (App.State.GameSettings.SoundOn && !this.bgMusic.isPlaying)
				{
					this.bgMusic.Play();
				}
				if (!App.State.GameSettings.SoundOn && this.bgMusic.isPlaying)
				{
					this.bgMusic.Pause();
				}
			}
			if (this.bgMusic == null && App.State.GameSettings.SoundOn)
			{
				if (this.bgMusicMp3 == null)
				{
					this.bgMusicMp3 = (AudioClip)Resources.Load("RiseOfTheFallen");
				}
				GameObject gameObject = new GameObject("bgmusic");
				this.bgMusic = gameObject.AddComponent<AudioSource>();
				this.bgMusic.clip = this.bgMusicMp3;
				this.bgMusic.loop = true;
			}
			MainUi.Instance.Show();
		}
	}
}
