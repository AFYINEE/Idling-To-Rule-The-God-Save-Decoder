using Assets.Scripts.Data;
using Assets.Scripts.Helper;
using Assets.Scripts.Save;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class InfoArea : MonoBehaviour
	{
		public static bool ShowConnect = false;

		private bool ShowCredits;

		private bool TouIsShowing;

		private bool PrivacyIsShowing;

		private bool ImprintIsShowing;

		private static Vector2 scrollPositionLeftSide = Vector2.zero;

		public static bool ShowArea = false;

		public static string InfoAreaTooltip = string.Empty;

		public static int TimeSincePressed = 5000;

		public static float Height(float px)
		{
			return GuiBase.Height(px);
		}

		public static float Width(float px)
		{
			return GuiBase.Width(px);
		}

		public static int FontSize(int px)
		{
			return GuiBase.FontSize(px);
		}

		[DllImport("__Internal")]
		private static extern void DownloadText(string filename, string text);

		[DllImport("__Internal")]
		private static extern void TextUploadClick();

		private void Start()
		{
		}

        //[DebuggerHidden]
        //private IEnumerator LoadText(string url)
        //{
        //    InfoArea.<LoadText>c__Iterator0 <LoadText>c__Iterator = new InfoArea.<LoadText>c__Iterator0();
        //    <LoadText>c__Iterator.url = url;
        //    <LoadText>c__Iterator.$this = this;
        //    return <LoadText>c__Iterator;
        //}

		private void FileSelected(string url)
		{
			//base.StartCoroutine(this.LoadText(url));
		}

		public void OnButtonPointerDown()
		{
			InfoArea.TextUploadClick();
		}

		private void SendEmail()
		{
			string text = "support@shugasu.com";
			string text2 = this.MyEscapeURL("Idling to Rule the Gods -  Feedback");
			if (App.CurrentPlattform == Plattform.Android)
			{
				text2 += " - Android";
			}
			else if (App.CurrentPlattform == Plattform.Kongregate)
			{
				text2 += " - Kongregate";
			}
			else if (App.CurrentPlattform == Plattform.Steam)
			{
				text2 += " - Steam";
			}
			string text3 = this.MyEscapeURL("Hello, \n");
			Application.OpenURL(string.Concat(new string[]
			{
				"mailto:",
				text,
				"?subject=",
				text2,
				"&body=",
				text3
			}));
		}

		private string MyEscapeURL(string url)
		{
			return WWW.EscapeURL(url).Replace("+", "%20");
		}

		public void OnGUI()
		{
			if (!InfoArea.ShowArea || GuiBase.LeftDialogIsShowing || GuiBase.FullScreenDialogIsShowing || AfkUi.Instance.ShowAfk)
			{
				return;
			}
			GUI.skin = Gui.ChosenSkin;
			GUI.backgroundColor = SettingsUi.Instance.UiColor;
			GUI.Box(new Rect(InfoArea.Width(10f), InfoArea.Height(110f), InfoArea.Width(275f), InfoArea.Height(480f)), string.Empty);
			int num = 60;
			GUIStyle style = GUI.skin.GetStyle("Button");
			style.fontSize = InfoArea.FontSize(16);
			style.fontStyle = FontStyle.Normal;
			GUIStyle style2 = GUI.skin.GetStyle("Label");
			style2.fontSize = InfoArea.FontSize(14);
			style2.fontStyle = FontStyle.Normal;
			style2.alignment = TextAnchor.UpperLeft;
			if (this.TouIsShowing)
			{
				this.showTOU(style2);
				return;
			}
			if (this.ImprintIsShowing)
			{
				this.showLegals(style2);
				return;
			}
			if (this.ShowCredits)
			{
				this.showCredits(style2);
				return;
			}
			if (this.PrivacyIsShowing)
			{
				this.showPrivacy(style2);
				return;
			}
			GUI.BeginGroup(new Rect(InfoArea.Width(25f), InfoArea.Height(140f), InfoArea.Width(265f), InfoArea.Height(490f)));
			float x = InfoArea.Width(2f);
			GUI.Label(new Rect(x, InfoArea.Height((float)num), InfoArea.Width(115f), InfoArea.Height(50f)), "Version: 2.13.740\nDate: 2017-08-09");
			if (GUI.Button(new Rect(InfoArea.Width(125f), InfoArea.Height((float)num), InfoArea.Width(120f), InfoArea.Height(35f)), new GUIContent("Connect", "If you want to connect your online save from kongregate to steam, click here for more info.")))
			{
				InfoArea.ShowConnect = !InfoArea.ShowConnect;
			}
			num += 80;
			string text = Application.dataPath + "/Saves/ManualSave.txt";
			if (App.CurrentPlattform == Plattform.Android)
			{
				text = " the internal storage";
			}
			string text2 = "Save";
			string text3 = "Load";
			string tooltip = "This will save your game to " + text + ".\nYou can load the save with the 'Load' button. The game will never load this automatically.";
			string tooltip2 = "This will load your game from " + text + ". It does nothing, if this is not a valid save or this file does not exist.";
			if (App.CurrentPlattform == Plattform.Kongregate)
			{
				tooltip = "This will open a dialog where you can save your game data.";
				tooltip2 = "This will open a dialog where you can select previously saved data.";
			}
			if (GUI.Button(new Rect(InfoArea.Width(0f), InfoArea.Height((float)num), InfoArea.Width(115f), InfoArea.Height(35f)), new GUIContent(text2, tooltip)))
			{
				if (App.CurrentPlattform == Plattform.Kongregate)
				{
					this.SaveWebGl();
				}
				else
				{
					this.Save(text);
				}
			}
			if (GUI.Button(new Rect(InfoArea.Width(125f), InfoArea.Height((float)num), InfoArea.Width(120f), InfoArea.Height(35f)), new GUIContent(text3, tooltip2)))
			{
				this.Load();
			}
			num += 45;
			string text4 = "This is an additional online save. The game saves online once every 15 minutes automatically. If you need it to save now, just press the button.";
			if (UpdateStats.LastServerSaveTime > 0L)
			{
				long time = UpdateStats.CurrentTimeMillis() - UpdateStats.LastServerSaveTime;
				text4 = text4 + "\nThe last online save was " + Conv.MsToGuiText(time, true) + " ago.";
			}
			else
			{
				text4 += "\nThe game was not saved online yet since you started the game.";
			}
			text4 = text4 + "\nThe next online save will be in: " + Conv.MsToGuiText(UpdateStats.NextOnlineSaveTime, false);
			string str = "Kongregate";
			bool flag = !string.IsNullOrEmpty(App.State.KongUserId);
			if (App.CurrentPlattform == Plattform.Steam)
			{
				str = "Steam";
				flag = !string.IsNullOrEmpty(App.State.SteamId);
			}
			if (App.CurrentPlattform == Plattform.Android)
			{
				flag = true;
			}
			if (GUI.Button(new Rect(InfoArea.Width(0f), InfoArea.Height((float)num), InfoArea.Width(115f), InfoArea.Height(35f)), new GUIContent("Save Online", text4)))
			{
				if (!flag)
				{
					GuiBase.ShowToast("Sorry, you need to be logged in in " + str + " to use this feature.");
				}
				else
				{
					this.SaveOnline();
				}
			}
			if (GUI.Button(new Rect(InfoArea.Width(125f), InfoArea.Height((float)num), InfoArea.Width(120f), InfoArea.Height(35f)), new GUIContent("Load Online", "Loads the online save and overwrites your current save. If you lose your local save, this is the way to get back your game state, if there is an online save. You need to do this within 15 minutes, or the autosave will overwrite your online save.")))
			{
				if (!flag)
				{
					GuiBase.ShowToast("Sorry, you need to be logged in in " + str + " to use this feature.");
				}
				else
				{
					this.LoadOnline();
				}
			}
			num += 45;
			int num2 = 114;
			if (GUI.Button(new Rect(InfoArea.Width(0f), InfoArea.Height((float)num), InfoArea.Width((float)num2), InfoArea.Height(35f)), new GUIContent("Credits", "A list of contributers to the game.")))
			{
				this.ShowCredits = true;
			}
			if (GUI.Button(new Rect(InfoArea.Width(125f), InfoArea.Height((float)num), InfoArea.Width(120f), InfoArea.Height(35f)), new GUIContent("Feedback", "Email the developer to give feedback or report bugs.")))
			{
				this.SendEmail();
			}
			num += 45;
			if (GUI.Button(new Rect(InfoArea.Width(0f), InfoArea.Height((float)num), InfoArea.Width(245f), InfoArea.Height(35f)), new GUIContent("My Other Games", "Opens my website to show the other games I made.\nBecause it only works as either a popup or it would replace the page, it is shown as a popup. So make sure to view blocked popups if your browser blocks it.")))
			{
				App.OpenWebsite("https://shugasu.com/games");
			}
			num += 45;
			if (GUI.Button(new Rect(InfoArea.Width(0f), InfoArea.Height((float)num), InfoArea.Width(75f), InfoArea.Height(35f)), new GUIContent("Legals")))
			{
				this.ImprintIsShowing = true;
			}
			if (GUI.Button(new Rect(InfoArea.Width(85f), InfoArea.Height((float)num), InfoArea.Width(75f), InfoArea.Height(35f)), new GUIContent("TOU")))
			{
				this.TouIsShowing = true;
			}
			if (GUI.Button(new Rect(InfoArea.Width(170f), InfoArea.Height((float)num), InfoArea.Width(75f), InfoArea.Height(35f)), new GUIContent("Privacy")))
			{
				this.PrivacyIsShowing = true;
			}
			num += 70;
			if (GUI.Button(new Rect(InfoArea.Width(0f), InfoArea.Height((float)num), InfoArea.Width(245f), InfoArea.Height(35f)), new GUIContent("Reset game", "This will reset all stats and multipliers to the base values.")))
			{
				this.ResetGame();
			}
			GUI.EndGroup();
			InfoArea.InfoAreaTooltip = GUI.tooltip;
		}

		private void Save(string saveLocation)
		{
			Storage.SaveGameState(App.State, "ManualSave.txt");
			GuiBase.ShowToast("Saved the game to " + saveLocation);
		}

		private void Load()
		{
			GuiBase.ShowDialog("This will overwrite your current data.", "Are you sure you want that?", delegate
			{
				CDouble count = App.State.Clones.Count;
				GameState gameState = Storage.LoadGameState("ManualSave.txt");
				if (gameState != null)
				{
					App.State = gameState;
					this.InitAfterLoad();
				}
				else
				{
					App.State.Clones.Count = count;
				}
			}, delegate
			{
			}, "Yes", "No", false, false);
		}

		private void Export()
		{
			MainUi.ExportToClipboard();
			GuiBase.ShowToast("Gamestate is saved to the clipboard!");
		}

		private void Import(bool editorImport)
		{
			GuiBase.ShowDialog("This will overwrite your current data.", "Are you sure you want that?", delegate
			{
				TextEditor textEditor = new TextEditor();
				textEditor.Paste();
				string text = textEditor.text;
				string text2 = "The pasted save is broken. Please try again. Copy an previous exported save to your clipboard first. It also probably won't work if you try to manipulate the save.";
				try
				{
					CDouble count = App.State.Clones.Count;
					GameState gameState = Storage.FromCompressedString(text);
					gameState.InitAchievementNames();
					if (!string.IsNullOrEmpty(gameState.KongUserId) && !string.IsNullOrEmpty(App.State.KongUserId) && !gameState.KongUserId.Equals(App.State.KongUserId) && !editorImport)
					{
						GuiBase.ShowToast("This save is from: " + gameState.KongUserName + ". You are not allowed to use a save from a different user!");
						App.State.Clones.Count = count;
					}
					else if (gameState != null && gameState.Clones.Count > 0)
					{
						App.State = gameState;
						this.InitAfterLoad();
					}
					else
					{
						GuiBase.ShowToast(text2);
						App.State.Clones.Count = count;
					}
				}
				catch (Exception)
				{
					GuiBase.ShowToast(text2);
				}
			}, delegate
			{
			}, "Yes", "No", false, false);
		}

		private void SaveWebGl()
		{
			InfoArea.DownloadText("SaveItRtG.txt", Storage.ToCompressedString(App.State));
		}

		private void LoadWebGl(int marginTop)
		{
			if (GUI.RepeatButton(new Rect(InfoArea.Width(125f), InfoArea.Height((float)marginTop), InfoArea.Width(120f), InfoArea.Height(35f)), new GUIContent("Load", "This will open a dialog where you can select previously saved data.")) && InfoArea.TimeSincePressed >= 5000)
			{
				InfoArea.TimeSincePressed = 0;
				InfoArea.TextUploadClick();
			}
		}

		private void InitAfterLoad()
		{
			App.State.InitAchievementNames();
			foreach (Creation current in App.State.AllCreations)
			{
				current.InitSubItemCost(0);
			}
			PlanetUi.Instance.Reset();
			SpecialFightUi.SortSkills();
			MainUi.Instance.Init(false);
			App.State.Multiplier.RecalculateMonumentMultis(App.State);
		}

		private void SaveOnline()
		{
			UpdateStats.SaveToServer(UpdateStats.ServerSaveType.UserRequest);
		}

		private void LoadOnline()
		{
			if (UpdateStats.TimeSinceLastOnlineRequest < 60L)
			{
				GuiBase.ShowToast("You need to wait another " + (60L - UpdateStats.TimeSinceLastOnlineRequest) + " seconds to load your online save.");
			}
			else
			{
				GuiBase.ShowDialog("This will overwrite your current data.", "Are you sure you want that?", delegate
				{
					UpdateStats.LoadFromServer(UpdateStats.ServerSaveType.UserRequest);
				}, delegate
				{
				}, "Yes", "No", false, false);
			}
		}

		private void ResetGame()
		{
			GuiBase.ShowDialog("Everything will be resetted to the base values.", "Do you really want to reset the game?", delegate
			{
				if (App.CurrentPlattform == Plattform.Kongregate)
				{
					MainUi.ExportToClipboard();
					MainUi.ResetGame();
					GuiBase.ShowToast("Game resetted and your gamestate is saved to the clipboard in case you still want to keep it somewhere.");
				}
				else if (App.CurrentPlattform == Plattform.Steam)
				{
					string text = "SaveBeforeReset";
					string str = Application.dataPath + "\\" + text;
					Storage.SaveGameState(App.State, text);
					MainUi.ResetGame();
					GuiBase.ShowToast("Game resetted and your game is saved to " + str + " in case you still want to keep it somewhere.");
				}
				else
				{
					MainUi.ResetGame();
				}
			}, delegate
			{
			}, "Yes", "No", false, false);
		}

		private void showLegals(GUIStyle labelStyle)
		{
			labelStyle.fontStyle = FontStyle.Bold;
			GUI.Label(new Rect(InfoArea.Width(30f), InfoArea.Height(210f), InfoArea.Width(220f), 30f), new GUIContent("Legal Notice"));
			labelStyle.fontStyle = FontStyle.Normal;
			string text = "Information referring to. § 5 TMG\n\nResponsible for this game is:\n\nShugasu UG (haftungsbeschränkt)\nKirchenweg 26\nHinterhof\n90419 Nürnberg\nGermany\n\nRepresented by:\nDenny Stöhr\n\nContact:\nPhone: +49 1520 3674040\nE-Mail: denny.stoehr@shugasu.com\nWebsite: www.shugasu.com\n\nVAT number:\nVAT identification number in accordance with section 27 a of the German VAT act:\nDE298011143\n\nRegister entry:\nHandelsregister Entry\nRegister Number: HRB 31203\nRegister Court: Nuremberg\n\nCopyright:\nThis game is subject to German copyright law. Unless expressly permitted by law \n(§ 44a et seq. of the copyright law), every form of utilizing, reproducing or processing works  subject to copyright protection on this game requires the prior consent of Denny Stöhr.\nUnauthorized utilization of copyrighted works is punishable (§ 106 of the copyright law).\n\n";
			int num = 900;
			InfoArea.scrollPositionLeftSide = GuiBase.TouchScrollView(new Rect(InfoArea.Width(10f), InfoArea.Height(240f), InfoArea.Width(260f), InfoArea.Height(260f)), InfoArea.scrollPositionLeftSide, new Rect(InfoArea.Width(10f), InfoArea.Height(240f), InfoArea.Width(220f), InfoArea.Height((float)num)));
			GUI.Label(new Rect(InfoArea.Width(30f), InfoArea.Height(240f), InfoArea.Width(220f), InfoArea.Height((float)num)), new GUIContent(text));
			GUI.EndScrollView();
			if (GUI.Button(new Rect(InfoArea.Width(25f), InfoArea.Height(530f), InfoArea.Width(245f), InfoArea.Height(35f)), new GUIContent("Close")))
			{
				this.ImprintIsShowing = false;
			}
		}

		private void showTOU(GUIStyle labelStyle)
		{
			labelStyle.fontStyle = FontStyle.Bold;
			GUI.Label(new Rect(InfoArea.Width(30f), InfoArea.Height(210f), InfoArea.Width(220f), 30f), new GUIContent("Terms of Use"));
			labelStyle.fontStyle = FontStyle.Normal;
			string text = "The game automatically saves every few seconds, but this doesn't mean it can't be lost. To prevent the unlikely event to lose your save, please do a manual backup.\nYou can create a manual backup of your save if you click the 'Save'-Button in the Info Tab. The tooltip shows you the save location.\nThe responsibility to keep your save is on yourself.\n\nPlease manually do an online save to make sure your game has saved. If it shows 'Game Saved online', then everything was done correctly. If not, then there could be something blocking that feature such as a plugin. \nIf you lose your save, and have no manual backup, you can still try to 'Load Online' within the first 15 minutes after you start the game to get your save back.\n\nYou are NOT allowed to decompile, or hack the game in any way. You are also not allowed to spread, or sell this game under your own name.\n\nDo not manipulate the game save in any way as there is some protection placed within it. If you are able to manipulate it and would like to use it anyway despite being told not to, turn off or disable online highscoring. This will not bother other players who have played the game legitimately.\n\nIf you buy items for real money, then no refund is possible. If they are usable and are used up, they will be gone. If they are permanent through a rebirth, then they will stay.";
			int num = 1050;
			InfoArea.scrollPositionLeftSide = GuiBase.TouchScrollView(new Rect(InfoArea.Width(15f), InfoArea.Height(240f), InfoArea.Width(260f), InfoArea.Height(260f)), InfoArea.scrollPositionLeftSide, new Rect(InfoArea.Width(15f), InfoArea.Height(240f), InfoArea.Width(220f), InfoArea.Height((float)num)));
			GUI.Label(new Rect(InfoArea.Width(30f), InfoArea.Height(240f), InfoArea.Width(220f), InfoArea.Height((float)num)), new GUIContent(text));
			GUI.EndScrollView();
			if (GUI.Button(new Rect(InfoArea.Width(25f), InfoArea.Height(530f), InfoArea.Width(245f), InfoArea.Height(35f)), new GUIContent("Close")))
			{
				this.TouIsShowing = false;
			}
		}

		private void showPrivacy(GUIStyle labelStyle)
		{
			labelStyle.fontStyle = FontStyle.Bold;
			GUI.Label(new Rect(InfoArea.Width(30f), InfoArea.Height(210f), InfoArea.Width(220f), 30f), new GUIContent("Privacy"));
			labelStyle.fontStyle = FontStyle.Normal;
			string text = "\r\nThis game saves your game data to a server once every 15 minutes to back up your data. \r\nThis contains everything you see in the game and also an unique id depending on the plattform where you play it. No personal data is taken.\r\nFor Android the id from Google Play Games is used. If you didn't install Google Play Games, no backup will be made and so no data taken.\r\nIn Steam and Kongregate, the id from your login-name is used.\r\nThis game recognizes your unique id and you may get your game save back if lose it, or change your device as long as you log in with the same account and click 'Load Online' before the autosave takes place.\r\n\r\nThis game is build with Unity. \r\nUnity also Unity collects error reports and also some data like your IP address, country, device model.\r\nUnity Ads has probably collected device information, like IP address and device identifiers,and information regarding the delivery of ads and your interaction with them, all of which may be shared with ad publishers and attribution companies. Unity Ads may also incorporate data derived from the Unity Analytics service in user profiles and use that profile data in order to provide personalized advertising.\r\n";
			int num = 900;
			InfoArea.scrollPositionLeftSide = GuiBase.TouchScrollView(new Rect(InfoArea.Width(15f), InfoArea.Height(240f), InfoArea.Width(260f), InfoArea.Height(260f)), InfoArea.scrollPositionLeftSide, new Rect(InfoArea.Width(15f), InfoArea.Height(240f), InfoArea.Width(220f), InfoArea.Height((float)num)));
			GUI.Label(new Rect(InfoArea.Width(30f), InfoArea.Height(240f), InfoArea.Width(220f), InfoArea.Height((float)num)), new GUIContent(text));
			GUI.EndScrollView();
			if (GUI.Button(new Rect(InfoArea.Width(25f), InfoArea.Height(530f), InfoArea.Width(245f), InfoArea.Height(35f)), new GUIContent("Close")))
			{
				this.PrivacyIsShowing = false;
			}
		}

		private void showCredits(GUIStyle labelStyle)
		{
			int num = 210;
			GUIStyle gUIStyle = new GUIStyle();
			gUIStyle.fontStyle = FontStyle.Bold;
			gUIStyle.fontSize = InfoArea.FontSize(16);
			gUIStyle.normal.textColor = Gui.MainColor;
			gUIStyle.wordWrap = true;
			int num2 = 550;
			InfoArea.scrollPositionLeftSide = GuiBase.TouchScrollView(new Rect(InfoArea.Width(15f), InfoArea.Height((float)num), InfoArea.Width(260f), InfoArea.Height(280f)), InfoArea.scrollPositionLeftSide, new Rect(InfoArea.Width(15f), InfoArea.Height((float)num), InfoArea.Width(235f), InfoArea.Height((float)num2)));
			GUI.Label(new Rect(InfoArea.Width(35f), InfoArea.Height((float)num), InfoArea.Width(220f), InfoArea.Height(30f)), new GUIContent("Idea, Coding, Art and Story"), gUIStyle);
			num += 30;
			GUI.Label(new Rect(InfoArea.Width(35f), InfoArea.Height((float)num), InfoArea.Width(220f), InfoArea.Height(30f)), new GUIContent("Denny Stöhr"));
			num += 45;
			GUI.Label(new Rect(InfoArea.Width(35f), InfoArea.Height((float)num), InfoArea.Width(220f), InfoArea.Height(30f)), new GUIContent("Story Proofreading"), gUIStyle);
			num += 30;
			GUI.Label(new Rect(InfoArea.Width(35f), InfoArea.Height((float)num), InfoArea.Width(220f), InfoArea.Height(30f)), new GUIContent("Quizer (C1-7,13)"));
			num += 25;
			GUI.Label(new Rect(InfoArea.Width(35f), InfoArea.Height((float)num), InfoArea.Width(220f), InfoArea.Height(30f)), new GUIContent("Little Girl Blue (C8+9)"));
			num += 25;
			GUI.Label(new Rect(InfoArea.Width(35f), InfoArea.Height((float)num), InfoArea.Width(220f), InfoArea.Height(30f)), new GUIContent("Tangrid (C1-9)"));
			num += 25;
			GUI.Label(new Rect(InfoArea.Width(35f), InfoArea.Height((float)num), InfoArea.Width(220f), InfoArea.Height(30f)), new GUIContent("Ensignia (C10-13)"));
			num += 25;
			GUI.Label(new Rect(InfoArea.Width(35f), InfoArea.Height((float)num), InfoArea.Width(220f), InfoArea.Height(30f)), new GUIContent("Pom6 (C12)"));
			num += 25;
			GUI.Label(new Rect(InfoArea.Width(35f), InfoArea.Height((float)num), InfoArea.Width(220f), InfoArea.Height(30f)), new GUIContent("Timothy Chong (C14)"));
			num += 45;
			GUI.Label(new Rect(InfoArea.Width(35f), InfoArea.Height((float)num), InfoArea.Width(220f), InfoArea.Height(30f)), new GUIContent("Music"), gUIStyle);
			num += 30;
			labelStyle.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(InfoArea.Width(35f), InfoArea.Height((float)num), InfoArea.Width(220f), InfoArea.Height(30f)), new GUIContent("bearhack"));
			if (GUI.Button(new Rect(InfoArea.Width(130f), InfoArea.Height((float)num), InfoArea.Width(115f), InfoArea.Height(30f)), new GUIContent("SoundCloud")))
			{
				App.OpenWebsite("https://soundcloud.com/bearhack145-953728889");
			}
			num += 50;
			GUI.Label(new Rect(InfoArea.Width(35f), InfoArea.Height((float)num), InfoArea.Width(220f), InfoArea.Height(100f)), new GUIContent("Players who helped to improve the game (reported and helped to fix bugs, good ideas)"), gUIStyle);
			num += 100;
			GUI.Label(new Rect(InfoArea.Width(35f), InfoArea.Height((float)num), InfoArea.Width(220f), InfoArea.Height(150f)), new GUIContent("ArtyD42, Pom6, Little Girl Blue, Zongre, Bajablo, mnjiman, Dzugavili, Shadow543211, EuroWolf, Robstradomus, notfromearth, somethingggg, Zongre, elara85"));
			GUI.EndScrollView();
			if (GUI.Button(new Rect(InfoArea.Width(25f), InfoArea.Height(530f), InfoArea.Width(245f), InfoArea.Height(35f)), new GUIContent("Close")))
			{
				this.ShowCredits = false;
			}
		}
	}
}
