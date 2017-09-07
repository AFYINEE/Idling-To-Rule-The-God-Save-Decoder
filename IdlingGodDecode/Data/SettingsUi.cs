using Assets.Scripts.Data;
using Assets.Scripts.Helper;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class SettingsUi : GuiBase
	{
		public static SettingsUi Instance = new SettingsUi();

		private float Red;

		private float Blue;

		private float Green;

		public Color UiColor;

		private float RedBG;

		private float BlueBG;

		private float GreenBG;

		public Color BGColor;

		private string[] UIStyleLeftStrings = new string[]
		{
			"Black",
			"Blue",
			"Red",
			"White"
		};

		private string[] ProgressbarStrings = new string[]
		{
			"Gradient",
			"Plain"
		};

		private string[] FontStrings = new string[]
		{
			"1",
			"2",
			"3",
			"4",
			"5"
		};

		private static string ignoreClonesString = "1";

		private static string IdleClonesString = "0";

		private bool exponentSettings;

		private int progressBarType;

		public void Init()
		{
			this.UiColor = Conv.StringToColor(App.State.GameSettings.CustomColor);
			this.Red = this.UiColor.r;
			this.Blue = this.UiColor.b;
			this.Green = this.UiColor.g;
			if (this.Red <= 0f && this.Blue <= 0f && this.Green <= 0f)
			{
				this.Red = 1f;
				this.Blue = 1f;
				this.Green = 1f;
				this.UiColor = Color.white;
			}
			this.BGColor = Conv.StringToColor(App.State.GameSettings.CustomBackground);
			this.RedBG = this.BGColor.r;
			this.BlueBG = this.BGColor.b;
			this.GreenBG = this.BGColor.g;
			this.exponentSettings = App.State.GameSettings.UseExponentNumbers;
			this.progressBarType = App.State.GameSettings.ProgressbarType;
		}

		public static void Show()
		{
			if (App.State != null)
			{
				SettingsUi.ignoreClonesString = App.State.GameSettings.TrainIgnoreCount.ToString();
				SettingsUi.IdleClonesString = App.State.GameSettings.SavedClonesForFight.ToString();
			}
			SettingsUi.Instance.show();
		}

		private void show()
		{
			int num = 15;
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.fontSize = GuiBase.FontSize(14);
			style.alignment = TextAnchor.UpperLeft;
			GUI.BeginGroup(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)));
			style.fontStyle = FontStyle.Bold;
			GUI.Box(new Rect(GuiBase.Width(0f), GuiBase.Height(0f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			Application.targetFrameRate = App.State.GameSettings.Framerate;
			QualitySettings.vSyncCount = 0;
			string text = "Lower Framerate means less cpu usage but the gui updates the values less frequently.";
			if (App.CurrentPlattform == Plattform.Kongregate)
			{
				text += "\nSomehow it seems to affect the whole browser so if you use the same browser for other things, don't set it lower than 30.";
			}
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent("Current Framerate: " + App.State.GameSettings.Framerate + " Fps", text));
			App.State.GameSettings.Framerate = (int)GUI.HorizontalSlider(new Rect(GuiBase.Width(20f), GuiBase.Height(50f), GuiBase.Width(280f), GuiBase.Height(30f)), (float)App.State.GameSettings.Framerate, 5f, 60f);
			num += 55;
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent("UI Style"));
			num += 30;
			App.State.GameSettings.UIStyle = GUI.Toolbar(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(280f), GuiBase.Height(30f)), App.State.GameSettings.UIStyle, this.UIStyleLeftStrings);
			num += 35;
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent("Font"));
			num += 28;
			int num2 = GUI.Toolbar(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(280f), GuiBase.Height(30f)), App.State.GameSettings.FontType, this.FontStrings);
			if (num2 != App.State.GameSettings.FontType)
			{
				App.State.GameSettings.FontType = num2;
				Gui.SetFont = true;
			}
			num += 35;
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent("Progressbars"));
			num += 28;
			this.progressBarType = GUI.Toolbar(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(280f), GuiBase.Height(30f)), this.progressBarType, this.ProgressbarStrings);
			num += 10;
			style.fontStyle = FontStyle.Normal;
			if (!App.State.PossibleCheater)
			{
				num += 30;
				string text2 = "kongregate";
				if (App.CurrentPlattform == Plattform.Steam)
				{
					text2 = "Steam";
					if (App.State.Ext.ImportedSaveFromKongToSteam || App.IsTimeTooOldForSteam)
					{
						text2 += ". Highscores for your Save are disabled, so it won't turn in highscores even if you turn this on.";
					}
				}
				App.State.ShouldSubmitScore = GUI.Toggle(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(235f), GuiBase.Height(25f)), App.State.ShouldSubmitScore, new GUIContent("Submit Highscore", "If this is on, your rebirths, gods defeated and total achievements will be submitted to the highscore in " + text2 + "."));
			}
			num += 30;
			App.State.GameSettings.SyncScrollbars = GUI.Toggle(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(260f), GuiBase.Height(25f)), App.State.GameSettings.SyncScrollbars, new GUIContent("Sync Scrollbars", "If this is on, scrollbars from training are synced together."));
			num += 30;
			App.State.GameSettings.ShowAchievementPopups = GUI.Toggle(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(260f), GuiBase.Height(25f)), App.State.GameSettings.ShowAchievementPopups, new GUIContent("Show Achievement Popups", "If this is on, achievements will show a popup, else not."));
			num += 30;
			App.State.GameSettings.AchievementsOnTop = GUI.Toggle(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(260f), GuiBase.Height(25f)), App.State.GameSettings.AchievementsOnTop, new GUIContent("Achievements on Top ", "If this is on, the info after an achievement is obtained will be shown top right."));
			num += 30;
			App.State.GameSettings.ShowToolTipsOnTop = GUI.Toggle(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(260f), GuiBase.Height(25f)), App.State.GameSettings.ShowToolTipsOnTop, new GUIContent("Show Tooltips on Top", "If this is on, tooltips will be shown on top else left to the mouse position."));
			num += 30;
			string text3 = "Tooltips on right click";
			string tooltip = "If this is on, tooltips will only be shown if you press the right mouse button.";
			if (App.CurrentPlattform == Plattform.Android)
			{
				text3 = "Disable Tooltips";
				tooltip = "If this is on, no tooltips will be shown.";
			}
			App.State.GameSettings.ShowToolTipsOnRightClick = GUI.Toggle(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(260f), GuiBase.Height(25f)), App.State.GameSettings.ShowToolTipsOnRightClick, new GUIContent(text3, tooltip));
			num += 30;
			App.State.GameSettings.StickyClones = GUI.Toggle(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(260f), GuiBase.Height(25f)), App.State.GameSettings.StickyClones, new GUIContent("Sticky Clones ", "If this is on, Clones will be left on monuments / div gen, even if you don't have enough ressources to build it."));
			num = 320;
			App.State.GameSettings.SoundOn = GUI.Toggle(new Rect(GuiBase.Width(330f), GuiBase.Height((float)num), GuiBase.Width(300f), GuiBase.Height(25f)), App.State.GameSettings.SoundOn, new GUIContent("Music", "If this is on, the game will play some background music."));
			num += 30;
			App.State.GameSettings.UseStopAt = GUI.Toggle(new Rect(GuiBase.Width(330f), GuiBase.Height((float)num), GuiBase.Width(260f), GuiBase.Height(25f)), App.State.GameSettings.UseStopAt, new GUIContent("'Stop at' or 'Next at'", "If this is turned on, clones from the related training will be removed when the value is reached instead of moving to the next one."));
			num += 30;
			this.exponentSettings = GUI.Toggle(new Rect(GuiBase.Width(330f), GuiBase.Height((float)num), GuiBase.Width(260f), GuiBase.Height(25f)), this.exponentSettings, new GUIContent("Use scientific notation", "If this is on, scientific notation will be used instead of spoken names."));
			num += 30;
			App.State.GameSettings.IgnoreCloneCountOn = GUI.Toggle(new Rect(GuiBase.Width(330f), GuiBase.Height((float)num), GuiBase.Width(220f), GuiBase.Height(25f)), App.State.GameSettings.IgnoreCloneCountOn, new GUIContent("Ignore Clonecount", "If this is on, the 'stop at' or 'next at' setting will let up to the count in the input field stay to train."));
			num += 30;
			GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)num), GuiBase.Width(210f), GuiBase.Height(30f)), new GUIContent("Save Clones for Fight", "The number of Clones you can't adjust to anything but fighting monsters or pets. So you can use the MAX Button and still have enough clones left to use for fights."));
			num -= 30;
			if (App.CurrentPlattform == Plattform.Android)
			{
				GUIStyle textField = Gui.ChosenSkin.textField;
				if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height((float)num), GuiBase.Width(65f), GuiBase.Height(25f)), App.State.GameSettings.TrainIgnoreCount + string.Empty, textField))
				{
					base.ShowNumberInput("Ignore Clonecount", App.State.GameSettings.TrainIgnoreCount, 50000, delegate(CDouble x)
					{
						App.State.GameSettings.TrainIgnoreCount = x.ToInt();
					});
				}
				num += 30;
				if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height((float)num), GuiBase.Width(65f), GuiBase.Height(25f)), App.State.GameSettings.SavedClonesForFight + string.Empty, textField))
				{
					base.ShowNumberInput("Save Clones for Fight", App.State.GameSettings.SavedClonesForFight, App.State.Clones.MaxShadowClones, delegate(CDouble x)
					{
						App.State.GameSettings.SavedClonesForFight = x.ToInt();
					});
				}
			}
			else
			{
				SettingsUi.ignoreClonesString = GUI.TextField(new Rect(GuiBase.Width(550f), GuiBase.Height((float)num), GuiBase.Width(65f), GuiBase.Height(25f)), SettingsUi.ignoreClonesString);
				num += 30;
				SettingsUi.IdleClonesString = GUI.TextField(new Rect(GuiBase.Width(550f), GuiBase.Height((float)num), GuiBase.Width(65f), GuiBase.Height(25f)), SettingsUi.IdleClonesString);
			}
			int.TryParse(SettingsUi.ignoreClonesString, out App.State.GameSettings.TrainIgnoreCount);
			if (App.State.GameSettings.TrainIgnoreCount > 50000)
			{
				App.State.GameSettings.TrainIgnoreCount = 50000;
			}
			if (App.State.GameSettings.TrainIgnoreCount < 1)
			{
				App.State.GameSettings.TrainIgnoreCount = 1;
			}
			int.TryParse(SettingsUi.IdleClonesString, out App.State.GameSettings.SavedClonesForFight);
			if (App.State.GameSettings.SavedClonesForFight > App.State.Clones.MaxShadowClones.ToInt())
			{
				App.State.GameSettings.SavedClonesForFight = App.State.Clones.MaxShadowClones.ToInt();
			}
			if (App.State.GameSettings.SavedClonesForFight < 0)
			{
				App.State.GameSettings.SavedClonesForFight = 0;
			}
			if (this.exponentSettings != App.State.GameSettings.UseExponentNumbers)
			{
				App.State.GameSettings.UseExponentNumbers = this.exponentSettings;
				App.State.InitAchievementNames();
				foreach (Creation current in App.State.AllCreations)
				{
					current.GodToDefeat.InitPowerLevelText();
				}
			}
			if (this.progressBarType != App.State.GameSettings.ProgressbarType)
			{
				App.State.GameSettings.ProgressbarType = this.progressBarType;
				GuiBase.InitImages();
			}
			num = 15;
			style.fontStyle = FontStyle.Bold;
			GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)num), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent("Adjust Color"));
			if (GUI.Button(new Rect(GuiBase.Width(525f), GuiBase.Height((float)num), GuiBase.Width(90f), GuiBase.Height(35f)), new GUIContent("Default", "Reverts to the default color of the style.")))
			{
				this.Red = (this.Blue = (this.Green = 1f));
			}
			style.fontStyle = FontStyle.Normal;
			num += 50;
			GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)(num - 7)), GuiBase.Width(60f), GuiBase.Height(30f)), new GUIContent("Red"));
			this.Red = GUI.HorizontalSlider(new Rect(GuiBase.Width(380f), GuiBase.Height((float)num), GuiBase.Width(235f), GuiBase.Height(30f)), this.Red, 0f, 1f);
			num += 35;
			GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)(num - 7)), GuiBase.Width(60f), GuiBase.Height(30f)), new GUIContent("Green"));
			this.Green = GUI.HorizontalSlider(new Rect(GuiBase.Width(380f), GuiBase.Height((float)num), GuiBase.Width(235f), GuiBase.Height(30f)), this.Green, 0f, 1f);
			num += 35;
			GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)(num - 7)), GuiBase.Width(60f), GuiBase.Height(30f)), new GUIContent("Blue"));
			this.Blue = GUI.HorizontalSlider(new Rect(GuiBase.Width(380f), GuiBase.Height((float)num), GuiBase.Width(235f), GuiBase.Height(30f)), this.Blue, 0f, 1f);
			this.UiColor.r = this.Red;
			this.UiColor.b = this.Blue;
			this.UiColor.g = this.Green;
			App.State.GameSettings.CustomColor = Conv.ColorToString(this.UiColor);
			style.fontStyle = FontStyle.Bold;
			num += 30;
			GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)num), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent("Background Color"));
			style.fontStyle = FontStyle.Normal;
			num += 40;
			GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)(num - 7)), GuiBase.Width(60f), GuiBase.Height(30f)), new GUIContent("Red"));
			this.RedBG = GUI.HorizontalSlider(new Rect(GuiBase.Width(380f), GuiBase.Height((float)num), GuiBase.Width(235f), GuiBase.Height(30f)), this.RedBG, 0f, 1f);
			num += 35;
			GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)(num - 7)), GuiBase.Width(60f), GuiBase.Height(30f)), new GUIContent("Green"));
			this.GreenBG = GUI.HorizontalSlider(new Rect(GuiBase.Width(380f), GuiBase.Height((float)num), GuiBase.Width(235f), GuiBase.Height(30f)), this.GreenBG, 0f, 1f);
			num += 35;
			GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)(num - 7)), GuiBase.Width(60f), GuiBase.Height(30f)), new GUIContent("Blue"));
			this.BlueBG = GUI.HorizontalSlider(new Rect(GuiBase.Width(380f), GuiBase.Height((float)num), GuiBase.Width(235f), GuiBase.Height(30f)), this.BlueBG, 0f, 1f);
			this.BGColor.r = this.RedBG;
			this.BGColor.b = this.BlueBG;
			this.BGColor.g = this.GreenBG;
			App.State.GameSettings.CustomBackground = Conv.ColorToString(this.BGColor);
			GUI.EndGroup();
		}

		private int AddLine(int marginTop, string left, string right, string info)
		{
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent(left, info));
			GUI.Label(new Rect(GuiBase.Width(320f), GuiBase.Height((float)marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent(right, info));
			marginTop += 25;
			return marginTop;
		}
	}
}
