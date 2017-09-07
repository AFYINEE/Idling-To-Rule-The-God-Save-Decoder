using Assets.Scripts.Data;
using Assets.Scripts.Helper;
using Assets.Scripts.Save;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class MainUi : GuiBase
	{
		public enum Pages
		{
			Create,
			Monuments,
			Divinity,
			PlanetUB,
			PlanetUBV2,
			PlanetCrystal,
			Physical,
			Skills,
			Might,
			TBS,
			Monster,
			Gods,
			Special,
			Pets,
			Statistics,
			Settings,
			Story,
			FAQ,
			Shortcuts,
			GodPower,
			Purchases,
			Planet,
			Campaigns,
			PetsVisit
		}

		public static MainUi Instance = new MainUi();

		private Texture2D purchaseIcon;

		private Texture2D luckyDraw;

		public Texture2D godPower;

		public Texture2D godPowerAttention;

		public Texture2D imageAchievement;

		private Texture2D heroSettingImg;

		private bool achievementToggle;

		private bool heroSetting;

		private bool showShortCuts;

		public static bool ShowSocialDialog = false;

		private bool escMenuShowing;

		public static string NewVersionInfo = string.Empty;

		private static bool VersionsInfoWasShown = false;

		private bool sizesSet;

		private List<string> inputs = new List<string>();

		private int customWidth = 480;

		private int customHeight = 300;

		private bool kredsToggle;

		private bool lastKredsToggle;

		private bool godpowerToggle;

		private bool lastGodpowerToggle;

		public const int STATS = 0;

		public const int REBIRTH = 1;

		public const int INFO = 2;

		private int leftToolbarInt;

		private string[] toolbarLeftStrings = new string[]
		{
			"Stats",
			"Rebirth",
			"Info"
		};

		private int calculatedSize = 18;

		private int calcFontSizeCount = 99;

		private const int BUILD = 0;

		private const int TRAIN = 1;

		private const int Fight = 2;

		private const int OTHER = 3;

		private int CREATE;

		private int MONUMENTS = 1;

		private int DIVINITY = 2;

		private int PLANET = 3;

		private int PHYSICAL;

		private int SKILLS = 1;

		private int TBS = 2;

		private int MIGHT = 2;

		private int TBS_AFTER_MIGHT = 3;

		private int MONSTER;

		private int GODS = 1;

		private int SPECIAL = 2;

		private int PETS = 3;

		private int STATISTICS;

		private int SETTINGS = 1;

		private int STORY = 2;

		private int FAQ = 3;

		public int selGridInt;

		private int toolbarIntBuild;

		private string[] toolbarStringsBuild = new string[]
		{
			"Create",
			"Monument",
			"Divinity",
			"Planet"
		};

		private int toolbarIntTrain;

		private string[] toolbarStringsTrain = new string[]
		{
			"Physical",
			"Skills",
			"TBS"
		};

		private string[] toolbarStringsTrain2 = new string[]
		{
			"Physical",
			"Skills",
			"Might",
			"TBS"
		};

		private int toolbarIntFight;

		private string[] toolbarStringsFight = new string[]
		{
			"Monster",
			"Gods",
			"Special",
			"Pets"
		};

		private int toolbarIntOther;

		private string[] toolbarStringsOther = new string[]
		{
			"Statistics",
			"Settings",
			"Story",
			"FAQ"
		};

		public string[] selStrings = new string[]
		{
			"Build",
			"Train",
			"Fight",
			"Other"
		};

		private static bool CampaignFinished = false;

		public void Init(bool initScrollbars = false)
		{
			this.purchaseIcon = (Texture2D)Resources.Load("Gui/kreds", typeof(Texture2D));
			if (App.CurrentPlattform == Plattform.Steam || App.CurrentPlattform == Plattform.Android)
			{
				this.purchaseIcon = (Texture2D)Resources.Load("Gui/coins", typeof(Texture2D));
			}
			this.luckyDraw = (Texture2D)Resources.Load("Gui/lucky_bright", typeof(Texture2D));
			this.godPower = (Texture2D)Resources.Load("Gui/godpower", typeof(Texture2D));
			this.godPowerAttention = (Texture2D)Resources.Load("Gui/godpower_attention", typeof(Texture2D));
			this.imageAchievement = (Texture2D)Resources.Load("Gui/achievement", typeof(Texture2D));
			this.heroSettingImg = (Texture2D)Resources.Load("Gui/settings", typeof(Texture2D));
			if (App.CurrentPlattform == Plattform.Kongregate)
			{
				KredOffersUi.Instance.Init();
			}
			else if (App.CurrentPlattform != Plattform.Steam)
			{
				if (App.CurrentPlattform == Plattform.Android)
				{
				}
			}
			if (App.CurrentPlattform == Plattform.Steam && (double)Screen.currentResolution.width / (double)Screen.currentResolution.height < 1.4)
			{
				Screen.SetResolution(1280, 800, Screen.fullScreen);
			}
			GodPowerUi.Instance.Init();
			CreatingUi.Instance.Init();
			SettingsUi.Instance.Init();
			SpecialFightUi.Instance.Init();
			TBSUi.Instance.Init();
			PlanetUi.Instance.Init();
			PetUi.Instance.Init();
			HeroImage.Init(true);
			CrystalUi.Instance.Init();
			DivinityGeneratorUi.AddIsOpen = false;
			ChallengesArea.Instance.isOpen = false;
			if (initScrollbars)
			{
				DivinityGeneratorUi.ScrollPosition = Vector2.zero;
				SkillUi.ScrollbarToZero();
				FightingUi.ScrollbarToZero();
				TrainingUi.ScrollbarToZero();
				MonumentUi.ScrollbarToZero();
				CreatingUi.ScrollbarToZero();
			}
			if (App.State != null)
			{
				AreaRightUi.CloneCountString = App.State.GameSettings.ClonesToAddCount.ToString();
				StoryUi.SetUnlockedStoryParts(App.State);
				App.State.InitAchievementNames();
				foreach (Creation current in App.State.AllCreations)
				{
					current.GodToDefeat.InitPowerLevelText();
				}
			}
		}

		private void SetSizes()
		{
			GUIStyle style = GUI.skin.GetStyle("horizontalslider");
			style.fixedHeight = GuiBase.Height(12f);
			GUIStyle style2 = GUI.skin.GetStyle("horizontalsliderthumb");
			style2.fixedHeight = GuiBase.Height(16f);
			style2.fixedWidth = GuiBase.Width(16f);
			GUIStyle style3 = GUI.skin.GetStyle("verticalscrollbar");
			style3.fixedWidth = GuiBase.Height(15f);
			GUIStyle style4 = GUI.skin.GetStyle("verticalscrollbarthumb");
			style4.fixedWidth = GuiBase.Height(15f);
			GUIStyle style5 = GUI.skin.GetStyle("toggle");
			style5.fontSize = GuiBase.FontSize(14);
			style5.fixedWidth = GuiBase.Width(50f);
			style5.fixedHeight = GuiBase.Height(22f);
			style5.contentOffset = new Vector2(GuiBase.Width(60f), 0f);
		}

		private void addValueCheckInputs(string newValue)
		{
			this.inputs.Add(newValue);
			string text = string.Empty;
			foreach (string current in this.inputs)
			{
				text += current;
			}
			if (text.Contains("upupdowndownleftrightleftrightab") || text.Contains("upupdowndownleftrightleftrightba"))
			{
				AreaGodlyShoot.IsShown = true;
				this.inputs = new List<string>();
			}
		}

		public void Update()
		{
			if (App.CurrentPlattform == Plattform.Steam)
			{
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					this.escMenuShowing = !this.escMenuShowing;
					return;
				}
			}
			else if (App.CurrentPlattform == Plattform.Android && Input.GetKeyDown(KeyCode.Escape))
			{
				Log.Info("Game Quit");
				App.SaveGameState();
				Application.Quit();
				return;
			}
			//AreaGodlyShoot.Instance.Update();
			SpecialFightUi.Instance.Update();
			if (this.selGridInt == 2 && this.toolbarIntFight == this.SPECIAL && SpecialFightUi.Instance.NeedsKeyInputs)
			{
				return;
			}
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				this.addValueCheckInputs("up");
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				this.addValueCheckInputs("down");
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				this.addValueCheckInputs("left");
			}
			else if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				this.addValueCheckInputs("right");
			}
			else if (Input.GetKeyDown(KeyCode.A))
			{
				this.addValueCheckInputs("a");
			}
			else if (Input.GetKeyDown(KeyCode.B))
			{
				this.addValueCheckInputs("b");
			}
			else if (Input.GetKeyDown(KeyCode.F1))
			{
				MainUi.ShowPage(MainUi.Pages.Shortcuts);
			}
			else if (Input.GetKeyDown(KeyCode.F2))
			{
				this.showPage(MainUi.Pages.FAQ);
			}
			else if (Input.GetKeyDown(KeyCode.F3))
			{
				ChatRoom.Show();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				this.showPage(MainUi.Pages.Create);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				this.showPage(MainUi.Pages.Monuments);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				this.showPage(MainUi.Pages.Divinity);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				this.showPage(MainUi.Pages.Planet);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				this.showPage(MainUi.Pages.Physical);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				this.showPage(MainUi.Pages.Skills);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				this.showPage(MainUi.Pages.Might);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha8))
			{
				this.showPage(MainUi.Pages.TBS);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha9))
			{
				this.showPage(MainUi.Pages.Monster);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha0))
			{
				this.showPage(MainUi.Pages.Gods);
			}
			if (Input.GetKey(KeyCode.LeftShift))
			{
				if (Input.GetKeyDown(KeyCode.A))
				{
					AreaRightUi.CloneCountString = App.State.Clones.MaxShadowClones.ToString();
				}
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					this.showPage(MainUi.Pages.Special);
				}
				if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					this.showPage(MainUi.Pages.Statistics);
				}
				if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					this.showPage(MainUi.Pages.Settings);
				}
				if (Input.GetKeyDown(KeyCode.Alpha4))
				{
					this.showPage(MainUi.Pages.Story);
				}
				if (Input.GetKeyDown(KeyCode.Alpha5))
				{
					this.showPage(MainUi.Pages.Pets);
				}
				if (Input.GetKeyDown(KeyCode.R))
				{
					App.State.RemoveAllClones(true);
					GuiBase.ShowToast("All shadow clones from Monument, Might, Divinity and Planet are removed!");
				}
			}
		}

		private void ShowEscMenu()
		{
			GUI.Box(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), string.Empty);
			string text = "Window Mode";
			if (!Screen.fullScreen)
			{
				text = "Full screen";
			}
			int num = 100;
			GUI.Label(new Rect(GuiBase.Width(320f), GuiBase.Height((float)num), GuiBase.Width(400f), GuiBase.Height(40f)), "Quit game or change resolutions");
			num += 50;
			if (GUI.Button(new Rect(GuiBase.Width(150f), GuiBase.Height((float)num), GuiBase.Width(150f), GuiBase.Height(30f)), "Back"))
			{
				this.escMenuShowing = false;
			}
			num += 40;
			if (GUI.Button(new Rect(GuiBase.Width(150f), GuiBase.Height((float)num), GuiBase.Width(150f), GuiBase.Height(30f)), text))
			{
				Screen.fullScreen = !Screen.fullScreen;
			}
			num += 40;
			if (GUI.Button(new Rect(GuiBase.Width(150f), GuiBase.Height((float)num), GuiBase.Width(150f), GuiBase.Height(30f)), "Quit"))
			{
				Application.Quit();
			}
			num = 150;
			int num2 = 350;
			float num3 = 480f;
			float num4 = 300f;
			for (float num5 = 0f; num5 < 10f; num5 += 1f)
			{
				float width = num3 * (1f + num5 / 2f);
				float height = num4 * (1f + num5 / 2f);
				this.AddSizeChangeButton(num2, ref num, width, height);
			}
			GUI.Label(new Rect(GuiBase.Width((float)(num2 + 200)), GuiBase.Height(150f), GuiBase.Width(400f), GuiBase.Height(40f)), "Custom");
			string s = GUI.TextField(new Rect(GuiBase.Width((float)(num2 + 200)), GuiBase.Height(190f), GuiBase.Width(65f), GuiBase.Height(25f)), this.customWidth.ToString());
			GUI.Label(new Rect(GuiBase.Width((float)(num2 + 275)), GuiBase.Height(192f), GuiBase.Width(20f), GuiBase.Height(40f)), "*");
			string s2 = GUI.TextField(new Rect(GuiBase.Width((float)(num2 + 290)), GuiBase.Height(190f), GuiBase.Width(65f), GuiBase.Height(25f)), this.customHeight.ToString());
			int.TryParse(s, out this.customWidth);
			int.TryParse(s2, out this.customHeight);
			int num6 = 230;
			this.AddSizeChangeButton(num2 + 200, ref num6, (float)this.customWidth, (float)this.customHeight);
		}

		private void AddSizeChangeButton(int marginLeft, ref int marginTop, float width, float height)
		{
			if (width < 480f)
			{
				width = 480f;
			}
			if (width > 8000f)
			{
				width = 8000f;
			}
			if (height < 300f)
			{
				height = 300f;
			}
			if (height > 4000f)
			{
				height = 4000f;
			}
			if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(150f), GuiBase.Height(30f)), width + " * " + height))
			{
				Screen.SetResolution((int)width, (int)height, Screen.fullScreen);
				TBSUi.ScreenChanged = true;
				this.SetSizes();
			}
			marginTop += 40;
		}

		public void Show()
		{
			if (!this.sizesSet)
			{
				this.SetSizes();
				this.sizesSet = true;
			}
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.normal.textColor = Gui.MainColor;
			style.fontSize = GuiBase.FontSize(16);
			style.alignment = TextAnchor.UpperLeft;
			GUI.backgroundColor = SettingsUi.Instance.UiColor;
			GUIStyle style2 = GUI.skin.GetStyle("TextField");
			style2.fontSize = GuiBase.FontSize(16);
			if (AreaGodlyShoot.IsShown)
			{
				AreaGodlyShoot.Instance.Show();
				if (GUI.Button(new Rect(GuiBase.Width(870f), GuiBase.Height(20f), GuiBase.Width(60f), GuiBase.Height(30f)), "Back"))
				{
					AreaGodlyShoot.IsShown = false;
					Application.targetFrameRate = App.State.GameSettings.Framerate;
					QualitySettings.vSyncCount = 0;
				}
				return;
			}
			if (this.escMenuShowing)
			{
				this.ShowEscMenu();
				return;
			}
			if (ChatRoom.IsShowing)
			{
				return;
			}
			if (!MainUi.VersionsInfoWasShown && !string.IsNullOrEmpty(MainUi.NewVersionInfo))
			{
				style.alignment = TextAnchor.UpperCenter;
				style.fontStyle = FontStyle.Bold;
				style.fontSize = GuiBase.FontSize(18);
				GUI.BeginGroup(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
				GUI.Box(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), string.Empty);
				GUI.DrawTexture(new Rect(GuiBase.Width(300f), GuiBase.Height(20f), GuiBase.Width(400f), GuiBase.Height(40f)), GuiBase.Logo);
				GUI.Label(new Rect(GuiBase.Width(240f), GuiBase.Height(200f), GuiBase.Width(500f), GuiBase.Height(250f)), MainUi.NewVersionInfo);
				style.fontStyle = FontStyle.Normal;
				string text = "Please refresh the game.";
				if (App.CurrentPlattform == Plattform.Android)
				{
					text = "Please get the new version from the Playstore.";
				}
				if (App.CurrentPlattform == Plattform.Steam)
				{
					text = "Please get the newest version (restart Steam if necessary).";
				}
				GUI.Label(new Rect(GuiBase.Width(250f), GuiBase.Height(230f), GuiBase.Width(500f), GuiBase.Height(250f)), text);
				style2.alignment = TextAnchor.MiddleCenter;
				if (GUI.Button(new Rect(GuiBase.Width(370f), GuiBase.Height(300f), GuiBase.Width(110f), GuiBase.Height(30f)), "Changelog"))
				{
					App.OpenWebsite("https://shugasu.com/games/itrtg/changelog.html");
				}
				if (GUI.Button(new Rect(GuiBase.Width(510f), GuiBase.Height(300f), GuiBase.Width(110f), GuiBase.Height(30f)), "Okay"))
				{
					MainUi.NewVersionInfo = string.Empty;
					MainUi.VersionsInfoWasShown = true;
				}
				style.fontSize = GuiBase.FontSize(16);
				GUI.EndGroup();
				return;
			}
			if (App.State.ChangeAvatarName)
			{
				style.alignment = TextAnchor.UpperCenter;
				style.fontStyle = FontStyle.Bold;
				style.fontSize = GuiBase.FontSize(18);
				GUI.BeginGroup(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
				GUI.Box(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), string.Empty);
				GUI.DrawTexture(new Rect(GuiBase.Width(300f), GuiBase.Height(20f), GuiBase.Width(400f), GuiBase.Height(40f)), GuiBase.Logo);
				GUI.Label(new Rect(GuiBase.Width(250f), GuiBase.Height(200f), GuiBase.Width(500f), GuiBase.Height(250f)), "Please name your character.");
				style2.alignment = TextAnchor.MiddleCenter;
				App.State.AvatarName = GUI.TextField(new Rect(GuiBase.Width(420f), GuiBase.Height(250f), GuiBase.Width(150f), GuiBase.Height(25f)), App.State.AvatarName);
				if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(300f), GuiBase.Width(110f), GuiBase.Height(30f)), "Confirm"))
				{
					App.State.ChangeAvatarName = false;
				}
				style.fontStyle = FontStyle.Normal;
				style.fontSize = GuiBase.FontSize(16);
				GUI.EndGroup();
				return;
			}
			if (!App.State.Avatar.GenderChosen)
			{
				style.alignment = TextAnchor.UpperCenter;
				style.fontStyle = FontStyle.Bold;
				style.fontSize = GuiBase.FontSize(18);
				GUI.BeginGroup(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
				GUI.Box(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), string.Empty);
				GUI.Label(new Rect(GuiBase.Width(300f), GuiBase.Height(20f), GuiBase.Width(400f), GuiBase.Height(40f)), GuiBase.Logo);
				GUI.Label(new Rect(GuiBase.Width(250f), GuiBase.Height(100f), GuiBase.Width(500f), GuiBase.Height(250f)), "Please choose your character.");
				style.fontStyle = FontStyle.Normal;
				style.fontSize = GuiBase.FontSize(16);
				int num = 135;
				if (App.CurrentPlattform == Plattform.Android)
				{
					if (string.IsNullOrEmpty(App.State.AndroidName))
					{
						App.State.AndroidName = "Player";
					}
					GUI.Label(new Rect(GuiBase.Width(345f), GuiBase.Height((float)num), GuiBase.Width(100f), GuiBase.Height(230f)), "Name: ");
					App.State.AndroidName = GUI.TextField(new Rect(GuiBase.Width(445f), GuiBase.Height((float)(num - 3)), GuiBase.Width(160f), GuiBase.Height(25f)), App.State.AndroidName);
					App.State.AvatarName = App.State.AndroidName;
					num += 35;
				}
				HeroImage.ShowChooseAvatar(num);
				App.State.GameSettings.SoundOn = GUI.Toggle(new Rect(GuiBase.Width(670f), GuiBase.Height((float)(num + 250)), GuiBase.Width(300f), GuiBase.Height(30f)), App.State.GameSettings.SoundOn, new GUIContent("Music", "If this is on, the game will play some background music. Can be set in Other -> Settings."));
				if (App.CurrentPlattform == Plattform.Kongregate)
				{
					style.alignment = TextAnchor.UpperLeft;
					GUI.Label(new Rect(GuiBase.Width(250f), GuiBase.Height((float)(num + 300)), GuiBase.Width(500f), GuiBase.Height(250f)), "If you played this game before and you see this screen, it probably means the local save was lost. \nIn this case, Please wait up to 5-10 seconds. The game will try to load online now... ");
				}
				GUI.EndGroup();
				return;
			}
			if (AfkUi.Instance.ShowAfk)
			{
				AfkUi.Instance.Show(style);
				if (GuiBase.LeftDialogIsShowing)
				{
					GuiBase.ShowDialog(GuiBase.leftDialogHeader, GuiBase.leftDialogText, GuiBase.leftAction, GuiBase.rightAction, GuiBase.lButtonText, GuiBase.rButtonText, false, false);
					return;
				}
				GuiBase.ShowToolTip(string.Empty);
				return;
			}
			else
			{
				if (App.State.Avatar.ClothingPartsOld.Count > 0)
				{
					int num2 = 0;
					foreach (ClothingPart current in App.State.Avatar.ClothingPartsOld)
					{
						if (current.IsPermanentUnlocked && current.Id != ClothingPartEnum.arty_reward)
						{
							num2 += current.PermanentGPCost.ToInt();
						}
					}
					App.State.Avatar.ClothingPartsOld = new List<ClothingPart>();
					HeroImage.Init(true);
					string str = string.Empty;
					if (num2 > 0)
					{
						str = "You got back the " + num2 + " god power you spent for unlocking avatar parts. ";
						App.State.PremiumBoni.GodPower += num2;
						num2 = 0;
					}
					GuiBase.ShowBigMessage("The handling of the avatar parts has been changed.\nFrom now on, when you unlock something, it will always be permanent and won't reset after rebirthing anymore.\n" + str + "Your current avatar has been reset to the default.\n\nNow half of the avatar parts will be unlocked when you defeat gods and the other half only with god power.");
				}
				if (GuiBase.FullScreenDialogIsShowing)
				{
					GuiBase.ShowDialog(GuiBase.leftDialogHeader, GuiBase.leftDialogText, GuiBase.leftAction, GuiBase.rightAction, GuiBase.lButtonText, GuiBase.rButtonText, true, false);
					return;
				}
				if (MainUi.ShowSocialDialog)
				{
					style.fontSize = GuiBase.FontSize(16);
					style.alignment = TextAnchor.UpperLeft;
					GUI.BeginGroup(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
					GUI.Box(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), string.Empty);
					GUI.Label(new Rect(GuiBase.Width(350f), GuiBase.Height(20f), GuiBase.Width(400f), GuiBase.Height(40f)), GuiBase.Logo);
					GUI.Label(new Rect(GuiBase.Width(250f), GuiBase.Height(100f), GuiBase.Width(500f), GuiBase.Height(250f)), "This dialog is only shown once. \nI recently made a Twitter and Facebook account where I will post updates for Idling to Rule the Gods and other games I might make in the future.\nIf you click the links, you will receive 5 God Power each!\nYou could also like / follow them to not miss out something!");
					Texture2D image = (Texture2D)Resources.Load("Gui/facebook", typeof(Texture2D));
					Texture2D image2 = (Texture2D)Resources.Load("Gui/twitter", typeof(Texture2D));
					if (GUI.Button(new Rect(GuiBase.Width(250f), GuiBase.Height(250f), GuiBase.Width(120f), GuiBase.Height(40f)), image))
					{
						App.State.Ext.FacebookClicked = true;
						App.OpenWebsite("https://www.facebook.com/shugasu");
					}
					if (GUI.Button(new Rect(GuiBase.Width(400f), GuiBase.Height(250f), GuiBase.Width(120f), GuiBase.Height(40f)), image2))
					{
						App.State.Ext.TwitterClicked = true;
						App.OpenWebsite("https://twitter.com/Shugasu_Games");
					}
					int num3 = 0;
					if (App.State.Ext.FacebookClicked)
					{
						num3 += 5;
					}
					if (App.State.Ext.TwitterClicked)
					{
						num3 += 5;
					}
					if (GUI.Button(new Rect(GuiBase.Width(250f), GuiBase.Height(320f), GuiBase.Width(120f), GuiBase.Height(30f)), "No thanks"))
					{
						if (num3 > 0)
						{
							App.State.PremiumBoni.GodPower += num3;
							GuiBase.ShowToast("Thanks you, you recieved " + num3 + " God Power!");
						}
						MainUi.ShowSocialDialog = false;
						App.State.IsSocialDialogShown = true;
					}
					if (GUI.Button(new Rect(GuiBase.Width(400f), GuiBase.Height(320f), GuiBase.Width(120f), GuiBase.Height(30f)), "I clicked them"))
					{
						if (num3 > 0)
						{
							App.State.PremiumBoni.GodPower += num3;
							GuiBase.ShowToast("Thanks you, you recieved " + num3 + " God Power!");
						}
						MainUi.ShowSocialDialog = false;
						App.State.IsSocialDialogShown = true;
					}
					GUI.EndGroup();
					return;
				}
				if (!App.State.IsGuestMsgShown && Kongregate.IsGuest)
				{
					style.fontSize = GuiBase.FontSize(16);
					style.alignment = TextAnchor.UpperLeft;
					GUI.BeginGroup(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
					GUI.Box(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), string.Empty);
					GUI.Label(new Rect(GuiBase.Width(350f), GuiBase.Height(20f), GuiBase.Width(400f), GuiBase.Height(40f)), GuiBase.Logo);
					GUI.Label(new Rect(GuiBase.Width(250f), GuiBase.Height(100f), GuiBase.Width(500f), GuiBase.Height(250f)), "You are logged in as a guest. The game is fully playable as a guest but if you want to support the game or kongregate, you might want to login.\n\nDo you want to login/register or play as a guest?");
					if (GUI.Button(new Rect(GuiBase.Width(310f), GuiBase.Height(310f), GuiBase.Width(150f), GuiBase.Height(30f)), "Register"))
					{
						Kongregate.ShowSignIn();
						App.State.IsGuestMsgShown = true;
					}
					if (GUI.Button(new Rect(GuiBase.Width(510f), GuiBase.Height(310f), GuiBase.Width(150f), GuiBase.Height(30f)), "Play as guest"))
					{
						App.State.IsGuestMsgShown = true;
					}
					GUI.EndGroup();
					return;
				}
				if (!App.UserIdIsCorrect)
				{
					style.fontSize = GuiBase.FontSize(16);
					style.alignment = TextAnchor.UpperLeft;
					GUI.BeginGroup(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
					GUI.Box(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), string.Empty);
					GUI.Label(new Rect(GuiBase.Width(350f), GuiBase.Height(20f), GuiBase.Width(400f), GuiBase.Height(40f)), GuiBase.Logo);
					GUI.Label(new Rect(GuiBase.Width(250f), GuiBase.Height(100f), GuiBase.Width(500f), GuiBase.Height(250f)), string.Concat(new string[]
					{
						"This save is bound to the user ",
						App.State.KongUserName,
						"! \nIt is not allowed to use the save from another user.\nPlease sign in as ",
						App.State.KongUserName,
						" or reset the game.\nDo you want to reset the game and bind it to ",
						Kongregate.Username,
						" ?"
					}));
					if (GUI.Button(new Rect(GuiBase.Width(310f), GuiBase.Height(310f), GuiBase.Width(150f), GuiBase.Height(30f)), "Export save"))
					{
						MainUi.ExportToClipboard();
						GuiBase.ShowToast("The gamestate from " + App.State.KongUserName + " is saved to the clipboard in case you still want to keep it somewhere.");
					}
					if (GUI.Button(new Rect(GuiBase.Width(510f), GuiBase.Height(310f), GuiBase.Width(150f), GuiBase.Height(30f)), "Reset game"))
					{
						Storage.SaveGameState(App.State, App.State.KongUserId + "backup");
						MainUi.ResetGame();
						App.UserIdIsCorrect = true;
					}
					GUI.EndGroup();
					base.showToast();
					return;
				}
				if (!App.State.IsTutorialShown)
				{
					App.State.IsTutorialShown = true;
					this.selGridInt = 3;
					this.toolbarIntOther = this.FAQ;
				}
				if (GuiBase.BigMessageIsShowing)
				{
					GuiBase.ShowBigMessage();
					base.showToast();
					return;
				}
				style.fontSize = GuiBase.FontSize(16);
				style.fontStyle = FontStyle.Normal;
				for (float x = GUI.skin.label.CalcSize(new GUIContent("Power level: " + App.State.PowerLevel.ToGuiText(true))).x; x > GuiBase.Width(245f); x = GUI.skin.label.CalcSize(new GUIContent("Power level: " + App.State.PowerLevel.ToGuiText(true))).x)
				{
					style.fontSize--;
				}
				GUI.Box(new Rect(GuiBase.Width(10f), GuiBase.Height(10f), GuiBase.Width(275f), GuiBase.Height(95f)), string.Empty);
				style.alignment = TextAnchor.UpperLeft;
				GUI.Label(new Rect(GuiBase.Width(25f), GuiBase.Height(25f), GuiBase.Width(250f), GuiBase.Height(25f)), new GUIContent("Power level: " + App.State.PowerLevel.ToGuiText(true), "Your total power, including Physical, Mystic, Battle and Creating.\nIt's an indicator of how strong you are compared to other gods. But if you can defeat one depends on your individual values."));
				GUI.Label(new Rect(GuiBase.Width(25f), GuiBase.Height(46f), GuiBase.Width(250f), GuiBase.Height(50f)), new GUIContent(App.State.Clones.ToGuiText(), string.Concat(new object[]
				{
					"Idle / All Shadow clones.\n",
					Conv.AddCommaSeparator(App.State.Clones.MaxShadowClones.ToInt()),
					" is your current limit.\nAttack = (Battle + Creating / 2 + Physical / 4)  / ",
					App.State.CloneAttackDivider,
					"\nDefense = Mystic / ",
					App.State.CloneDefenseDivider,
					"\nHp = Hp / ",
					App.State.CloneHealthDivider
				})));
				GUIStyle style3 = GUI.skin.GetStyle("Button");
				style3.fontSize = GuiBase.FontSize(18);
				if (App.State.IsMonumentUnlocked)
				{
					style3.fontSize = GuiBase.FontSize(16);
				}
				GUI.Box(new Rect(GuiBase.Width(290f), GuiBase.Height(10f), GuiBase.Width(660f), GuiBase.Height(95f)), string.Empty);
				GUI.Label(new Rect(GuiBase.Width(480f), GuiBase.Height(20f), GuiBase.Width(400f), GuiBase.Height(40f)), GuiBase.Logo);
				this.LeftToolbar();
				if (GuiBase.NumberInputAction != null)
				{
					base.NumberInput(500, 200);
					return;
				}
				if (this.showShortCuts)
				{
					NavigationMenu.Show();
					base.showToast();
					return;
				}
				if (this.heroSetting)
				{
					bool flag = false;
					foreach (ClothingPart current2 in App.State.Avatar.ClothingParts)
					{
						if (current2.IsPreview)
						{
							flag = true;
						}
						if (flag)
						{
							current2.IsPreview = false;
						}
					}
					HeroImage.ShowComplete();
					base.showToast();
					return;
				}
				if (HeroImage.DialogPart != null)
				{
					HeroImage.DialogPart.IsPreview = false;
					HeroImage.DialogPart = null;
				}
				if (InfoArea.ShowConnect)
				{
					ConnectArea.Show();
					GuiBase.ShowToolTip(string.Empty);
					base.showToast();
					return;
				}
				if (this.OffersToggle())
				{
					return;
				}
				this.Toolbar();
				base.showAchievementReached();
				GuiBase.ShowToolTip(string.Empty);
				base.showToast();
				return;
			}
		}

		private bool OffersToggle()
		{
			string str = "Kreds";
			if (App.CurrentPlattform != Plattform.Kongregate)
			{
				str = "money";
			}
			Texture2D image = this.purchaseIcon;
			if (App.State.PremiumBoni.TimeForNextLuckyDraw <= 0)
			{
				image = this.luckyDraw;
			}
			this.kredsToggle = GUI.Toggle(new Rect(GuiBase.Width(240f), GuiBase.Height(60f), GuiBase.Width(40f), GuiBase.Height(40f)), this.kredsToggle, new GUIContent(image, "If you feel like spending some " + str + " and the game is idling your time away, that's the right button.\nIt shows you some special offers with beautiful duration timers.\nNext Lucky Draw: " + Conv.MsToGuiText(App.State.PremiumBoni.TimeForNextLuckyDraw.ToLong(), true)), GUI.skin.GetStyle("Button"));
			string tooltip = "Use up the power you gained from gods for permanent bonuses! \nYou will get one for each god you defeat for the first time and then once each time from Izanagi on.\nYou can also earn it from completing challenges, defeating ultimate beings, black holes, lucky draws, or by buying it.";
			if (App.State != null && App.State.PremiumBoni.CheckGP)
			{
				this.godpowerToggle = GUI.Toggle(new Rect(GuiBase.Width(195f), GuiBase.Height(60f), GuiBase.Width(40f), GuiBase.Height(40f)), this.godpowerToggle, new GUIContent(this.godPowerAttention, tooltip), GUI.skin.GetStyle("Button"));
			}
			else
			{
				this.godpowerToggle = GUI.Toggle(new Rect(GuiBase.Width(195f), GuiBase.Height(60f), GuiBase.Width(40f), GuiBase.Height(40f)), this.godpowerToggle, new GUIContent(this.godPower, tooltip), GUI.skin.GetStyle("Button"));
			}
			if (this.godpowerToggle != this.lastGodpowerToggle)
			{
				this.kredsToggle = (this.lastKredsToggle = false);
				this.lastGodpowerToggle = this.godpowerToggle;
			}
			else if (this.kredsToggle != this.lastKredsToggle)
			{
				this.godpowerToggle = (this.lastGodpowerToggle = false);
				this.lastKredsToggle = this.kredsToggle;
			}
			if (this.kredsToggle)
			{
				if (App.CurrentPlattform == Plattform.Steam)
				{
					SteamOffersUi.Show();
				}
				else if (App.CurrentPlattform == Plattform.Kongregate)
				{
					KredOffersUi.Show();
				}
				else if (App.CurrentPlattform == Plattform.Android)
				{
				}
				this.godpowerToggle = false;
			}
			else
			{
				Kongregate.IsWaitingForPurchase = false;
			}
			if (this.godpowerToggle)
			{
				this.kredsToggle = false;
				GodPowerUi.Show();
			}
			if (InfoArea.ShowArea)
			{
				GuiBase.ShowToolTip(InfoArea.InfoAreaTooltip);
			}
			else
			{
				GuiBase.ShowToolTip(string.Empty);
			}
			base.showToast();
			return this.kredsToggle || this.godpowerToggle;
		}

		private void LeftToolbar()
		{
			if (GuiBase.LeftDialogIsShowing)
			{
				GuiBase.ShowDialog(GuiBase.leftDialogHeader, GuiBase.leftDialogText, GuiBase.leftAction, GuiBase.rightAction, GuiBase.lButtonText, GuiBase.rButtonText, false, false);
				return;
			}
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.fontSize = GuiBase.FontSize(16);
			style.alignment = TextAnchor.UpperLeft;
			GUIStyle style2 = GUI.skin.GetStyle("Button");
			style2.fontSize = GuiBase.FontSize(18);
			if (!InfoArea.ShowArea)
			{
				GUI.Box(new Rect(GuiBase.Width(10f), GuiBase.Height(110f), GuiBase.Width(275f), GuiBase.Height(480f)), string.Empty);
			}
			style.fontStyle = FontStyle.Bold;
			string str = string.Empty;
			if (!string.IsNullOrEmpty(App.State.TitleGod))
			{
				str = "Received from defeating " + App.State.TitleGod;
				if (App.State.Title.Contains("universe"))
				{
					style.fontSize = GuiBase.FontSize(16);
				}
			}
			string text = App.State.Title;
			string tooltip = "Your title. You will earn a new title for each god you defeat.\n\n" + str;
			if (App.State.PossibleCheater)
			{
				text = "Cheating God";
				tooltip = "Baal doesn't believe in your power. 'You must have cheated!' He said.\nThe game is still fully playable, but you can't submit any highscores anymore and this title will stay forever.\nIf you think, he is wrong, you can try to send an email with your export data to denny.stoehr@shugasu.com and let him judge it.";
			}
			GUI.Label(new Rect(GuiBase.Width(25f), GuiBase.Height(120f), GuiBase.Width(260f), GuiBase.Height(30f)), new GUIContent(text, tooltip));
			style.fontStyle = FontStyle.Normal;
			this.leftToolbarInt = GUI.Toolbar(new Rect(GuiBase.Width(25f), GuiBase.Height(150f), GuiBase.Width(245f), GuiBase.Height(30f)), this.leftToolbarInt, this.toolbarLeftStrings);
			int num = this.leftToolbarInt;
			if (num != 0)
			{
				if (num != 1)
				{
					if (num == 2)
					{
						InfoArea.ShowArea = true;
					}
				}
				else
				{
					InfoArea.ShowArea = false;
					this.RebirthArea(style);
				}
			}
			else
			{
				InfoArea.ShowArea = false;
				this.StatsArea(style);
			}
		}

		private void CalcFontSize(GUIStyle labelStyle)
		{
			if (this.calcFontSizeCount == 100)
			{
				this.calcFontSizeCount = 0;
				labelStyle.fontSize = GuiBase.FontSize(18);
				string text = App.State.Money.ToGuiText(true);
				if (App.State.PhysicalPower.ToGuiText(true).Length > text.Length)
				{
					text = App.State.PhysicalPower.ToGuiText(true);
				}
				if (App.State.Attack.ToGuiText(true).Length > text.Length)
				{
					text = App.State.Attack.ToGuiText(true);
				}
				if (App.State.MysticPower.ToGuiText(true).Length > text.Length)
				{
					text = App.State.MysticPower.ToGuiText(true);
				}
				if (App.State.BattlePower.ToGuiText(true).Length > text.Length)
				{
					text = App.State.BattlePower.ToGuiText(true);
				}
				if (App.State.CreatingPower.ToGuiText(true).Length > text.Length)
				{
					text = App.State.CreatingPower.ToGuiText(true);
				}
				for (float x = GUI.skin.label.CalcSize(new GUIContent(text)).x; x > GuiBase.Width(175f); x = GUI.skin.label.CalcSize(new GUIContent(text)).x)
				{
					labelStyle.fontSize--;
				}
				this.calculatedSize = labelStyle.fontSize;
			}
			this.calcFontSizeCount++;
		}

		private void StatsArea(GUIStyle labelStyle)
		{
			labelStyle.fontSize = this.calculatedSize;
			labelStyle.fontStyle = FontStyle.Bold;
			GUI.BeginGroup(new Rect(GuiBase.Width(15f), GuiBase.Height(140f), GuiBase.Width(265f), GuiBase.Height(490f)));
			HeroImage.ShowImage(25, 47, 180, 220, false);
			this.CalcFontSize(labelStyle);
			int num = 55;
			this.heroSetting = GUI.Toggle(new Rect(GuiBase.Width(10f), GuiBase.Height((float)num), GuiBase.Width(30f), GuiBase.Height(30f)), this.heroSetting, new GUIContent(this.heroSettingImg, "Here you can adjust your hero image."), GUI.skin.GetStyle("Button"));
			num += 35;
			this.showShortCuts = GUI.Toggle(new Rect(GuiBase.Width(10f), GuiBase.Height((float)num), GuiBase.Width(30f), GuiBase.Height(30f)), this.showShortCuts, new GUIContent("?", "Show shortcuts."), GUI.skin.GetStyle("Button"));
			labelStyle.fontStyle = FontStyle.Normal;
			labelStyle.normal.textColor = Gui.MainColor;
			if (App.State.UnleashRegenBoni > 0)
			{
				labelStyle.normal.textColor = Color.green;
			}
			GuiBase.CreateProgressBar(10, 265, 245f, 40f, App.State.getPercentOfHP(), "HP " + App.State.CurrentHealth.ToGuiText(true), "Your health is needed when fighting gods.\nIncreases with physical and creation power. lowerTextMax HP: " + App.State.MaxHealth.ToGuiText(true) + "\nHP Recover / s: " + App.State.HpRecoverSec.ToGuiText(true), GuiBase.progressBg, GuiBase.progressFgRed);
			labelStyle.normal.textColor = Gui.MainColor;
			GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(305f), GuiBase.Width(255f), GuiBase.Height(30f)), new GUIContent("Divinity: " + App.State.Money.ToGuiText(true), "Divinity is your trade currency if you want to buy creations from other gods (after defeating Nephthys) cause creating anything by yourself is way too slow.\nYou will earn it while fighting enemies."));
			if (App.State.UnleashAttackBoni > 0)
			{
				labelStyle.normal.textColor = Color.green;
			}
			GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(327f), GuiBase.Width(260f), GuiBase.Height(30f)), new GUIContent("Attack: " + App.State.Attack.ToGuiText(true), App.State.AttackDescription));
			labelStyle.normal.textColor = Gui.MainColor;
			GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(349f), GuiBase.Width(260f), GuiBase.Height(30f)), new GUIContent("Physical: " + App.State.PhysicalPower.ToGuiText(true), App.State.PhysicalDescription));
			if (App.State.UnleashDefenseBoni > 0)
			{
				labelStyle.normal.textColor = Color.green;
			}
			GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(371f), GuiBase.Width(260f), GuiBase.Height(30f)), new GUIContent("Mystic: " + App.State.MysticPower.ToGuiText(true), App.State.MysticDescription));
			labelStyle.normal.textColor = Gui.MainColor;
			GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(393f), GuiBase.Width(260f), GuiBase.Height(30f)), new GUIContent("Battle: " + App.State.BattlePower.ToGuiText(true), App.State.BattleDescription));
			GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(415f), GuiBase.Width(260f), GuiBase.Height(30f)), new GUIContent("Creating: " + App.State.CreatingPower.ToGuiText(true), App.State.CreatingDescription));
			GUI.EndGroup();
		}

		private void RebirthArea(GUIStyle labelStyle)
		{
			GUI.BeginGroup(new Rect(GuiBase.Width(16f), GuiBase.Height(140f), GuiBase.Width(265f), GuiBase.Height(490f)));
			if (ChallengesArea.Instance.isOpen)
			{
				ChallengesArea.Instance.Show(labelStyle);
				if (GUI.Button(new Rect(GuiBase.Width(9f), GuiBase.Height(410f), GuiBase.Width(245f), GuiBase.Height(30f)), new GUIContent("Back")))
				{
					ChallengesArea.Instance.isOpen = false;
				}
			}
			else
			{
				CDouble rightSide = App.State.Statistic.ApplyTimeMulti(App.State.Multiplier.RebirthMulti * App.State.Statistic.StatisticRebirthMultiplier);
				string text = App.State.Multiplier.AchievementMultiPhysical.ToGuiText(true) + " / " + (App.State.Multiplier.MultiBoniPhysicalRebirth * rightSide).ToGuiText(true);
				string text2 = App.State.Multiplier.AchievementMultiMystic.ToGuiText(true) + " / " + (App.State.Multiplier.MultiBoniMysticRebirth * rightSide).ToGuiText(true);
				string text3 = App.State.Multiplier.AchievementMultiBattle.ToGuiText(true) + " / " + (App.State.Multiplier.MultiBoniBattleRebirth * rightSide).ToGuiText(true);
				string text4 = App.State.Multiplier.AchievementMultiCreating.ToGuiText(true) + " / " + (App.State.Multiplier.MultiBoniCreatingRebirth * rightSide).ToGuiText(true);
				float x5 = GUI.skin.label.CalcSize(new GUIContent(text)).x;
				float x2 = GUI.skin.label.CalcSize(new GUIContent(text2)).x;
				float x3 = GUI.skin.label.CalcSize(new GUIContent(text3)).x;
				float x4 = GUI.skin.label.CalcSize(new GUIContent(text4)).x;
				float num = x5;
				string text5 = text;
				if (x2 > num)
				{
					num = x2;
					text5 = text2;
				}
				if (x3 > num)
				{
					text5 = text3;
					num = x3;
				}
				if (x4 > num)
				{
					text5 = text4;
					num = x4;
				}
				while (num > GuiBase.Width(240f))
				{
					labelStyle.fontSize--;
					num = GUI.skin.label.CalcSize(new GUIContent(text5)).x;
				}
				labelStyle.fontStyle = FontStyle.Normal;
				GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(60f), GuiBase.Width(240f), GuiBase.Height(50f)), new GUIContent("Rebirthing is safe when everything is green.", "The numbers will turn green when your multiplier at rebirth is higher than your current base multiplier. Your base multiplier is the multiplier you had at your current rebirth plus achievements. It does not include your monuments."));
				if (App.State.Multiplier.MultiBoniPhysicalRebirth * rightSide > App.State.Multiplier.AchievementMultiPhysical)
				{
					labelStyle.normal.textColor = Color.green;
				}
				else
				{
					labelStyle.normal.textColor = Color.red;
				}
				string text6 = "All your physical power is multiplied by this now / your starting multiplier after rebirth";
				if (App.State.Multiplier.MonumentMultiPhysical > 1)
				{
					text6 = text6 + "\n\nCurrent value with monuments: " + App.State.Multiplier.CurrentMultiPhysical.ToGuiText(true);
				}
				GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(110f), GuiBase.Width(240f), GuiBase.Height(50f)), new GUIContent("Physical multi: \n" + text, text6));
				if (App.State.Multiplier.MultiBoniMysticRebirth * rightSide > App.State.Multiplier.AchievementMultiMystic)
				{
					labelStyle.normal.textColor = Color.green;
				}
				else
				{
					labelStyle.normal.textColor = Color.red;
				}
				text6 = "All your mystic power is multiplied by this now / after your starting multiplier rebirth";
				if (App.State.Multiplier.MonumentMultiMystic > 1)
				{
					text6 = text6 + "\n\nCurrent value with monuments: " + App.State.Multiplier.CurrentMultiMystic.ToGuiText(true);
				}
				GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(155f), GuiBase.Width(240f), GuiBase.Height(50f)), new GUIContent("Mystic multi: \n" + text2, text6));
				if (App.State.Multiplier.MultiBoniBattleRebirth * rightSide > App.State.Multiplier.AchievementMultiBattle)
				{
					labelStyle.normal.textColor = Color.green;
				}
				else
				{
					labelStyle.normal.textColor = Color.red;
				}
				text6 = "All your battle power is multiplied by this now / after your starting multiplier rebirth";
				if (App.State.Multiplier.MonumentMultiBattle > 1)
				{
					text6 = text6 + "\n\nCurrent value with monuments: " + App.State.Multiplier.CurrentMultiBattle.ToGuiText(true);
				}
				GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(200f), GuiBase.Width(240f), GuiBase.Height(50f)), new GUIContent("Battle multi: \n" + text3, text6));
				if (App.State.Multiplier.MultiBoniCreatingRebirth * rightSide > App.State.Multiplier.AchievementMultiCreating)
				{
					labelStyle.normal.textColor = Color.green;
				}
				else
				{
					labelStyle.normal.textColor = Color.red;
				}
				text6 = "All your creating power is multiplied by this now / after your starting multiplier rebirth";
				if (App.State.Multiplier.MonumentMultiCreating > 1)
				{
					text6 = text6 + "\n\nCurrent value with monuments: " + App.State.Multiplier.CurrentMultiCreating.ToGuiText(true);
				}
				GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(245f), GuiBase.Width(240f), GuiBase.Height(50f)), new GUIContent("Creating multi: \n" + text4, text6));
				labelStyle.normal.textColor = Gui.MainColor;
				GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(295f), GuiBase.Width(240f), GuiBase.Height(30f)), new GUIContent("God multi: " + App.State.Multiplier.RebirthMulti.ToGuiText(true), "Each God you defeat doubles your bonus after rebirth."));
				GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(320f), GuiBase.Width(240f), GuiBase.Height(30f)), new GUIContent("Statistics multi: " + App.State.Statistic.StatisticRebirthMultiplier.ToGuiText(true), "A multiplier depending on your statistics. For more info look at the statistics page."));
				GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(345f), GuiBase.Width(240f), GuiBase.Height(30f)), new GUIContent("Max clones: " + Conv.AddCommaSeparator(App.State.Clones.MaxShadowClonesRebirth), "The Maximum Shadow Clones you will start with after rebirthing. \nIt will increase by 1 when 20 shadow clones are either created or defeated.\nYour soft cap is " + Conv.AddCommaSeparator(App.State.Clones.AbsoluteMaximum) + ".\nYou can increase that cap by spending god power."));
				string text7 = "Rebirth now";
				if (App.State.Statistic.HasStartedArtyChallenge)
				{
					text7 = "Rebirth now (" + App.State.Statistic.RebirthsAfterUAC + ")";
				}
				if (GUI.Button(new Rect(GuiBase.Width(9f), GuiBase.Height(375f), GuiBase.Width(245f), GuiBase.Height(30f)), new GUIContent(text7, "A Rebirth will reset all your stats and achievements, replaces your current multipliers with new multipliers and updates your Max Shadow Clones.")))
				{
					App.CheckRebirth(delegate(bool x)
					{
						if (x)
						{
							App.Rebirth();
						}
					}, true);
				}
				if (App.State.Statistic.CanStartAChallenge())
				{
					if (GUI.Button(new Rect(GuiBase.Width(9f), GuiBase.Height(410f), GuiBase.Width(245f), GuiBase.Height(30f)), new GUIContent("Challenges", "Opens another page to show the currently available challenges.")))
					{
						ChallengesArea.Instance.isOpen = true;
					}
				}
				else if (App.State.Statistic.HighestGodDefeated >= 28)
				{
					string text8 = "Currently in UUC";
					string tooltip = "Time since started: " + Conv.MsToGuiText(App.State.Statistic.TimeAfterUUCStarted.ToLong(), true);
					if (App.State.Statistic.HasStartedUniverseChallenge && GUI.Button(new Rect(GuiBase.Width(155f), GuiBase.Height(410f), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Cancel", "Cancels this challenge.")))
					{
						GuiBase.ShowDialog("Cancel challenge", "This will cancel your ultimate universe challenge. You will receive no rewards. Do you really want to cancel it?", delegate
						{
							App.State.Statistic.HasStartedUniverseChallenge = false;
						}, delegate
						{
						}, "Yes", "No", false, false);
					}
					if (App.State.Statistic.HasStartedUltimateBaalChallenge)
					{
						text8 = "Currently in UBC";
						tooltip = "Time since started: " + Conv.MsToGuiText(App.State.Statistic.TimeAfterUBCStarted.ToLong(), true);
					}
					if (App.State.Statistic.HasStartedDoubleRebirthChallenge)
					{
						text8 = "Currently in DRC";
						tooltip = "Time since started: " + Conv.MsToGuiText(App.State.Statistic.TimeAfterDRCStarted.ToLong(), true);
					}
					if (App.State.Statistic.HasStartedArtyChallenge)
					{
						text8 = "Currently in UAC";
						tooltip = "Time since started: " + Conv.MsToGuiText(App.State.Statistic.TimeAfterUACStarted.ToLong(), true);
					}
					if (App.State.Statistic.HasStarted1kChallenge)
					{
						text8 = "Currently in 1KC";
						tooltip = "Time since started: " + Conv.MsToGuiText(App.State.Statistic.TimeAfter1KCStarted.ToLong(), true);
						if (GUI.Button(new Rect(GuiBase.Width(155f), GuiBase.Height(410f), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Cancel", "Cancels this challenge.")))
						{
							GuiBase.ShowDialog("Cancel challenge", "This will cancel your 1000 clones challenge. You will receive no rewards and you won't get your old multis back. Do you really want to cancel it?", delegate
							{
								App.State.Statistic.HasStarted1kChallenge = false;
								App.State.Statistic.TimeAfter1KCStarted = 0;
							}, delegate
							{
							}, "Yes", "No", false, false);
						}
					}
					if (App.State.Statistic.HasStartedNoRbChallenge)
					{
						text8 = "Currently in NRC";
						tooltip = "Time since started: " + Conv.MsToGuiText(App.State.Statistic.TimeAfterNoRbStarted.ToLong(), true);
					}
					if (App.State.Statistic.HasStartedAchievementChallenge)
					{
						text8 = "Currently in AAC";
						tooltip = "Time is not recorded for AAC";
						if (GUI.Button(new Rect(GuiBase.Width(155f), GuiBase.Height(410f), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Cancel", "Cancels this challenge.")))
						{
							GuiBase.ShowDialog("Cancel challenge", "This will cancel your all achievements challenge. You will receive no rewards. Do you really want to cancel it?", delegate
							{
								App.State.Statistic.HasStartedAchievementChallenge = false;
							}, delegate
							{
							}, "Yes", "No", false, false);
						}
					}
					if (App.State.Statistic.HasStartedUltimatePetChallenge)
					{
						text8 = "Currently in UPC";
						tooltip = "Time since started: " + Conv.MsToGuiText(App.State.Statistic.TimeAfterUPCStarted.ToLong(), true);
						if (GUI.Button(new Rect(GuiBase.Width(155f), GuiBase.Height(410f), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Cancel", "Cancels this challenge.")))
						{
							GuiBase.ShowDialog("Cancel challenge", "This will cancel your ultimate pet challenge. You will receive no rewards. Do you really want to cancel it?", delegate
							{
								App.State.Statistic.HasStartedUltimatePetChallenge = false;
								App.State.Statistic.TimeAfterUPCStarted = 0;
								App.State.Ext.PetPowerMultiCampaigns = 1;
								App.State.Ext.PetPowerMultiGods = 1;
							}, delegate
							{
							}, "Yes", "No", false, false);
						}
					}
					if (App.State.Statistic.HasStartedBlackHoleChallenge)
					{
						text8 = "Currently in BHC";
						tooltip = "Time since started: " + Conv.MsToGuiText(App.State.Statistic.TimeAfterBHCStarted.ToLong(), true);
					}
					GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(410f), GuiBase.Width(150f), GuiBase.Height(30f)), new GUIContent(text8, tooltip));
				}
			}
			GUI.EndGroup();
		}

		public static void ResetGame()
		{
			Achievement.InitAchievements(0);
			int totalItemsBought = App.State.PremiumBoni.TotalItemsBought;
			Currency steamCurrency = App.State.Ext.SteamCurrency;
			string steamCountry = App.State.Ext.SteamCountry;
			App.State = new GameState(true, 0);
			App.State.Ext.SteamCurrency = steamCurrency;
			App.State.Ext.SteamCountry = steamCountry;
			HeroImage.Init(true);
			App.State.IsGuestMsgShown = true;
			App.State.PremiumBoni.TotalItemsBought = totalItemsBought;
			if (App.State.PremiumBoni.GodPower == 0)
			{
				App.State.PremiumBoni.GodPower = 10;
			}
			if (App.CurrentPlattform == Plattform.Steam)
			{
				SteamHelper.InitSteamId();
			}
			if (Kongregate.APILoaded)
			{
				App.State.KongUserId = Kongregate.UserId;
				App.State.KongUserName = Kongregate.Username;
				Kongregate.GetUserItems2();
			}
			App.SaveGameState();
			MainUi.Instance.Init(false);
		}

		public static void ExportToClipboard()
		{
			if (App.State != null && string.IsNullOrEmpty(App.State.KongUserName) && !Kongregate.IsGuest)
			{
				App.State.KongUserName = Kongregate.Username;
				App.State.KongUserId = Kongregate.UserId;
			}
			string text = App.SaveGameState();
			TextEditor textEditor = new TextEditor();
			textEditor.text = text;
			textEditor.SelectAll();
			textEditor.Copy();
		}

		public static void ShowPage(MainUi.Pages page)
		{
			MainUi.Instance.showPage(page);
		}

		public void showPage(MainUi.Pages page)
		{
			this.showShortCuts = false;
			this.kredsToggle = false;
			this.godpowerToggle = false;
			this.heroSetting = false;
			switch (page)
			{
			case MainUi.Pages.Create:
				this.selGridInt = 0;
				this.toolbarIntBuild = this.CREATE;
				break;
			case MainUi.Pages.Monuments:
				this.selGridInt = 0;
				this.toolbarIntBuild = this.MONUMENTS;
				break;
			case MainUi.Pages.Divinity:
				this.selGridInt = 0;
				this.toolbarIntBuild = this.DIVINITY;
				break;
			case MainUi.Pages.PlanetUB:
				this.selGridInt = 0;
				this.toolbarIntBuild = this.PLANET;
				PlanetUi.ToolbarIntPlanet = 0;
				break;
			case MainUi.Pages.PlanetUBV2:
				this.selGridInt = 0;
				this.toolbarIntBuild = this.PLANET;
				PlanetUi.ToolbarIntPlanet = 1;
				break;
			case MainUi.Pages.PlanetCrystal:
				this.selGridInt = 0;
				this.toolbarIntBuild = this.PLANET;
				PlanetUi.ToolbarIntPlanet = 2;
				break;
			case MainUi.Pages.Physical:
				this.selGridInt = 1;
				this.toolbarIntTrain = this.PHYSICAL;
				break;
			case MainUi.Pages.Skills:
				this.selGridInt = 1;
				this.toolbarIntTrain = this.SKILLS;
				break;
			case MainUi.Pages.Might:
				this.selGridInt = 1;
				this.toolbarIntTrain = this.MIGHT;
				break;
			case MainUi.Pages.TBS:
				this.selGridInt = 1;
				if (App.State.PremiumBoni.TotalMightIsUnlocked)
				{
					this.toolbarIntTrain = this.TBS_AFTER_MIGHT;
				}
				else
				{
					this.toolbarIntTrain = this.TBS;
				}
				break;
			case MainUi.Pages.Monster:
				this.selGridInt = 2;
				this.toolbarIntFight = this.MONSTER;
				break;
			case MainUi.Pages.Gods:
				this.selGridInt = 2;
				this.toolbarIntFight = this.GODS;
				break;
			case MainUi.Pages.Special:
				this.selGridInt = 2;
				this.toolbarIntFight = this.SPECIAL;
				break;
			case MainUi.Pages.Pets:
				this.selGridInt = 2;
				this.toolbarIntFight = this.PETS;
				PetUi.ToolbarInt = 0;
				break;
			case MainUi.Pages.Statistics:
				this.selGridInt = 3;
				this.toolbarIntOther = this.STATISTICS;
				break;
			case MainUi.Pages.Settings:
				this.selGridInt = 3;
				this.toolbarIntOther = this.SETTINGS;
				break;
			case MainUi.Pages.Story:
				this.selGridInt = 3;
				this.toolbarIntOther = this.STORY;
				break;
			case MainUi.Pages.FAQ:
				this.selGridInt = 3;
				this.toolbarIntOther = this.FAQ;
				break;
			case MainUi.Pages.Shortcuts:
				this.showShortCuts = true;
				break;
			case MainUi.Pages.GodPower:
				this.godpowerToggle = true;
				break;
			case MainUi.Pages.Purchases:
				this.kredsToggle = false;
				break;
			case MainUi.Pages.Planet:
				this.selGridInt = 0;
				this.toolbarIntBuild = this.PLANET;
				break;
			case MainUi.Pages.Campaigns:
				this.selGridInt = 2;
				this.toolbarIntFight = this.PETS;
				PetUi.ToolbarInt = 1;
				break;
			case MainUi.Pages.PetsVisit:
				this.selGridInt = 2;
				this.toolbarIntFight = this.PETS;
				PetUi.ToolbarInt = 0;
				break;
			}
		}

		private void Toolbar()
		{
			GUIStyle style = GUI.skin.GetStyle("Button");
			style.fontSize = GuiBase.FontSize(16);
			this.selGridInt = GUI.SelectionGrid(new Rect(GuiBase.Width(300f), GuiBase.Height(20f), GuiBase.Width(120f), GuiBase.Height(75f)), this.selGridInt, this.selStrings, 2);
			AfkUi.Instance.ShowAfk = GUI.Toggle(new Rect(GuiBase.Width(780f), GuiBase.Height(20f), GuiBase.Width(80f), GuiBase.Height(30f)), AfkUi.Instance.ShowAfk, new GUIContent("AFK", "Click to go on afk mode. Progress will be 100% but the cpu usage is lower."));
			bool flag = false;
			switch (this.selGridInt)
			{
			case 0:
				this.toolbarIntBuild = GUI.Toolbar(new Rect(GuiBase.Width(440f), GuiBase.Height(65f), GuiBase.Width(390f), GuiBase.Height(30f)), this.toolbarIntBuild, this.toolbarStringsBuild);
				if (this.toolbarIntBuild == this.CREATE)
				{
					flag = true;
					CreatingUi.Show(this.achievementToggle);
				}
				else if (this.toolbarIntBuild == this.MONUMENTS)
				{
					MonumentUi.Instance.Show(this.achievementToggle);
				}
				else if (this.toolbarIntBuild == this.DIVINITY)
				{
					DivinityGeneratorUi.Instance.Show();
				}
				else if (this.toolbarIntBuild == this.PLANET)
				{
					PlanetUi.Show();
				}
				break;
			case 1:
				if (App.State.PremiumBoni.TotalMightIsUnlocked)
				{
					this.toolbarIntTrain = GUI.Toolbar(new Rect(GuiBase.Width(440f), GuiBase.Height(65f), GuiBase.Width(390f), GuiBase.Height(30f)), this.toolbarIntTrain, this.toolbarStringsTrain2);
					if (this.toolbarIntTrain == this.TBS_AFTER_MIGHT)
					{
						flag = false;
						TBSUi.Show();
					}
					else if (this.toolbarIntTrain == this.MIGHT)
					{
						flag = false;
						MightUi.Show();
					}
				}
				else
				{
					this.toolbarIntTrain = GUI.Toolbar(new Rect(GuiBase.Width(440f), GuiBase.Height(65f), GuiBase.Width(390f), GuiBase.Height(30f)), this.toolbarIntTrain, this.toolbarStringsTrain);
					if (this.toolbarIntTrain == this.TBS)
					{
						flag = false;
						TBSUi.Show();
					}
				}
				if (this.toolbarIntTrain == this.PHYSICAL)
				{
					flag = true;
					TrainingUi.Instance.Show(this.achievementToggle);
				}
				else if (this.toolbarIntTrain == this.SKILLS)
				{
					flag = true;
					SkillUi.Instance.Show(this.achievementToggle);
				}
				break;
			case 2:
				this.toolbarIntFight = GUI.Toolbar(new Rect(GuiBase.Width(440f), GuiBase.Height(65f), GuiBase.Width(390f), GuiBase.Height(30f)), this.toolbarIntFight, this.toolbarStringsFight);
				if (this.toolbarIntFight == this.MONSTER)
				{
					flag = true;
					FightingUi.Instance.Show(this.achievementToggle);
				}
				else if (this.toolbarIntFight == this.GODS)
				{
					GodUi.Instance.Show();
				}
				else if (this.toolbarIntFight == this.SPECIAL)
				{
					SpecialFightUi.Show();
				}
				else if (this.toolbarIntFight == this.PETS)
				{
					PetUi.Instance.Show();
				}
				break;
			case 3:
				this.toolbarIntOther = GUI.Toolbar(new Rect(GuiBase.Width(440f), GuiBase.Height(65f), GuiBase.Width(390f), GuiBase.Height(30f)), this.toolbarIntOther, this.toolbarStringsOther);
				if (this.toolbarIntOther == this.STATISTICS)
				{
					StatisticUi.Show();
				}
				else if (this.toolbarIntOther == this.SETTINGS)
				{
					SettingsUi.Show();
				}
				else if (this.toolbarIntOther == this.STORY)
				{
					StoryUi.Show();
				}
				else if (this.toolbarIntOther == this.FAQ)
				{
					FAQUi.Show();
				}
				break;
			}
			if (flag)
			{
				Rect position = new Rect(GuiBase.Width(890f), GuiBase.Height(20f), GuiBase.Width(50f), GuiBase.Height(50f));
				this.achievementToggle = GUI.Toggle(position, this.achievementToggle, new GUIContent(this.imageAchievement, "Toggle on to see your achievements. They will increase your multipliers."), GUI.skin.GetStyle("Button"));
			}
			Rect position2 = new Rect(GuiBase.Width(890f), GuiBase.Height(75f), GuiBase.Width(50f), GuiBase.Height(25f));
			style.fontSize = GuiBase.FontSize(14);
			if (ChatRoom.NewMessageAvailable)
			{
				style.normal.textColor = Color.green;
			}
			if (GUI.Button(new Rect(GuiBase.Width(890f), GuiBase.Height(75f), GuiBase.Width(50f), GuiBase.Height(25f)), new GUIContent("Chat", "Click here, if you want to chat with other people playing this game. If you are new and have some questions, you might just find someone to help you.")))
			{
				ChatRoom.Show();
			}
			style.normal.textColor = Gui.MainColor;
			position2 = new Rect(GuiBase.Width(855f), GuiBase.Height(75f), GuiBase.Width(30f), GuiBase.Height(25f));
			if (App.State.PremiumBoni.CanShowAlerts)
			{
				GUIStyle gUIStyle = new GUIStyle(style);
				gUIStyle.fontSize = GuiBase.FontSize(16);
				gUIStyle.alignment = TextAnchor.UpperCenter;
				gUIStyle.normal.textColor = Color.red;
				if (UpdateStats.DivGenEmpty)
				{
					if (GUI.Button(position2, new GUIContent("!", "Div gen is empty!"), gUIStyle))
					{
						MainUi.ShowPage(MainUi.Pages.Divinity);
					}
				}
				else
				{
					bool flag2 = false;
					if (MainUi.CampaignFinished)
					{
						if (GUI.Button(position2, new GUIContent("!", "At least one campaign is finished."), gUIStyle))
						{
							MainUi.ShowPage(MainUi.Pages.Campaigns);
						}
					}
					else if (UpdateStats.PetLowestHunger == 10)
					{
						flag2 = true;
						gUIStyle.normal.textColor = Color.red;
					}
					else if (UpdateStats.PetLowestHunger == 50)
					{
						flag2 = true;
						gUIStyle.normal.textColor = Color.yellow;
					}
					else if (UpdateStats.PetLowestHunger == 75)
					{
						flag2 = true;
						gUIStyle.normal.textColor = Color.green;
					}
					if (flag2 && GUI.Button(position2, new GUIContent("!", "Pet hunger is below " + UpdateStats.PetLowestHunger + "."), gUIStyle))
					{
						MainUi.ShowPage(MainUi.Pages.PetsVisit);
					}
				}
			}
		}

		public static void CheckAlertButton(GameState state)
		{
			MainUi.CampaignFinished = false;
			foreach (PetCampaign current in state.Ext.AllCampaigns)
			{
				if (current.TotalDuration > 0 && current.CurrentDuration >= current.TotalDuration)
				{
					MainUi.CampaignFinished = true;
				}
			}
		}
	}
}
