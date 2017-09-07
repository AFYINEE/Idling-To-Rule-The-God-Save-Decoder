using Assets.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	internal class ConnectArea : GuiBase
	{
		private static ConnectArea Instance = new ConnectArea();

		private bool showKongregateConnect;

		private bool showSteamConnect;

		private bool showAndroidConnect;

		public static long KongIdInput = 0L;

		public static string KongNameInput = string.Empty;

		public static long SteamIdInput = 0L;

		public static string SteamNameInput = string.Empty;

		public static string AndroidIdInput = string.Empty;

		public static string AndroidNameInput = string.Empty;

		public static void Show()
		{
			ConnectArea.Instance.show();
		}

		private void show()
		{
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = GuiBase.FontSize(16);
			GUI.BeginGroup(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)));
			GUI.Box(new Rect(GuiBase.Width(0f), GuiBase.Height(0f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			int num = 20;
			int num2 = 20;
			if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height((float)num2), GuiBase.Width(80f), GuiBase.Height(30f)), "Back"))
			{
				if (this.showAndroidConnect)
				{
					this.showAndroidConnect = false;
				}
				else if (this.showSteamConnect)
				{
					this.showSteamConnect = false;
				}
				else if (this.showKongregateConnect)
				{
					this.showKongregateConnect = false;
				}
				else
				{
					InfoArea.ShowConnect = false;
				}
			}
			style.fontStyle = FontStyle.Bold;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)num2), GuiBase.Width(600f), GuiBase.Height(30f)), "Connect your online save");
			style.fontStyle = FontStyle.Normal;
			num2 += 35;
			bool flag = App.State.SteamIdLong > 0L;
			bool flag2 = App.State.KongUserIdLong > 0L;
			bool flag3 = !string.IsNullOrEmpty(App.State.AndroidId);
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)num2), GuiBase.Width(600f), GuiBase.Height(230f)), "If you play the game on multiple plattforms, you can connect your online save here.\nWhen you switch from one plattform to a different one, please 'Save Online' on the plattform before you close the game, then click 'Load Online' on the other plattform as the first thing you do.\n\nYou need to have an online save on each plattform for it to work. So make sure to click 'Save Online' on all plattforms, before you try to connect the saves!");
			num2 += 195;
			if (this.showSteamConnect)
			{
				this.ShowSteamConnect(num, num2);
				GUI.EndGroup();
				return;
			}
			if (this.showAndroidConnect)
			{
				this.ShowAndroidConnect(num, num2);
				GUI.EndGroup();
				return;
			}
			if (this.showKongregateConnect)
			{
				this.ShowKongregateConnect(num, num2);
				GUI.EndGroup();
				return;
			}
			if (flag)
			{
				if (App.CurrentPlattform == Plattform.Steam && App.State.Ext.ImportedSaveFromKongToSteam)
				{
					GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)(num2 - 30)), GuiBase.Width(600f), GuiBase.Height(230f)), "You loaded your Kongregate save to your Steam version, so this save can't get any steam achievements or highscores. If you want to change that, you need to reset your game.");
					num2 += 55;
				}
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)num2), GuiBase.Width(600f), GuiBase.Height(230f)), string.Concat(new object[]
				{
					"Your online save is connected to Steam\nSteam Id: ",
					App.State.SteamIdLong + -36546L,
					" - Steam Name: ",
					App.State.SteamName
				}));
				if (App.CurrentPlattform != Plattform.Steam && GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)(num2 + 5)), GuiBase.Width(130f), GuiBase.Height(30f)), "Disconnect"))
				{
					App.State.SteamId = string.Empty;
					App.State.SteamName = string.Empty;
				}
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)num2), GuiBase.Width(600f), GuiBase.Height(230f)), "Your online save is not connected to Steam");
				if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)(num2 + 5)), GuiBase.Width(130f), GuiBase.Height(30f)), "Connect Now"))
				{
					this.showSteamConnect = true;
				}
			}
			num2 += 55;
			if (flag2)
			{
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)num2), GuiBase.Width(600f), GuiBase.Height(230f)), string.Concat(new object[]
				{
					"Your online save is connected to Kongregate\nKongregate Id: ",
					App.State.KongUserIdLong + -36546L,
					" - Kongregate Name: ",
					App.State.KongUserName
				}));
				if (App.CurrentPlattform != Plattform.Kongregate && GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)(num2 + 5)), GuiBase.Width(130f), GuiBase.Height(30f)), "Disconnect"))
				{
					App.State.KongUserId = string.Empty;
					App.State.KongUserName = string.Empty;
				}
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)num2), GuiBase.Width(600f), GuiBase.Height(230f)), "Your online save is not connected to Kongregate");
				if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)(num2 + 5)), GuiBase.Width(130f), GuiBase.Height(30f)), "Connect Now"))
				{
					this.showKongregateConnect = true;
				}
			}
			num2 += 55;
			if (!string.IsNullOrEmpty(App.State.AndroidId))
			{
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)num2), GuiBase.Width(600f), GuiBase.Height(230f)), "Your online save is connected to Android\nAndroid Id: " + App.State.AndroidId + " - Android Name: " + App.State.AndroidName);
				if (App.CurrentPlattform != Plattform.Android && GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)(num2 + 5)), GuiBase.Width(130f), GuiBase.Height(30f)), "Disconnect"))
				{
					App.State.AndroidId = string.Empty;
					App.State.AndroidName = string.Empty;
				}
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)num2), GuiBase.Width(600f), GuiBase.Height(230f)), "Your online save is not connected to Android");
				if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)(num2 + 5)), GuiBase.Width(130f), GuiBase.Height(30f)), "Connect Now"))
				{
					this.showAndroidConnect = true;
				}
			}
			num2 += 50;
			GUI.EndGroup();
		}

		private void ShowKongregateConnect(int marginLeft, int marginTop)
		{
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(230f)), "Please open the game on Kongregate an input the Kongregate Name and Id to the input-fields below.");
			marginTop += 60;
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(230f)), "Kongregate Id: ");
			string s = GUI.TextField(new Rect(GuiBase.Width((float)(marginLeft + 180)), GuiBase.Height((float)marginTop), GuiBase.Width(200f), GuiBase.Height(30f)), ConnectArea.KongIdInput.ToString());
			long.TryParse(s, out ConnectArea.KongIdInput);
			marginTop += 40;
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(230f)), "Kongregate Name: ");
			ConnectArea.KongNameInput = GUI.TextField(new Rect(GuiBase.Width((float)(marginLeft + 180)), GuiBase.Height((float)marginTop), GuiBase.Width(200f), GuiBase.Height(30f)), ConnectArea.KongNameInput);
			marginTop += 60;
			if (App.CurrentPlattform == Plattform.Steam)
			{
				GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(230f)), "If you get the Kongregate Save, you can't get achievements in Steam!");
				marginTop += 35;
			}
			if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(250f), GuiBase.Height(30f)), new GUIContent("Get Kongregate Save", "This will overwrite your current save, then import the online save from Kongregate, if the id and name are correct. Loading online in Kongregate will then connect your saves.")))
			{
				UpdateStats.LoadFromServer(UpdateStats.ServerSaveType.Automatic);
				UpdateStats.GetKongSaveAfterConnecting = true;
			}
			marginTop += 40;
			if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(250f), GuiBase.Height(30f)), new GUIContent("Overwrite Kongregate Save", "This will overwrite your Kongregate online save if the id and name are correct. Loading online in Kongregate will then move your current save to Kongregate and connect the saves.")))
			{
				UpdateStats.LoadFromServer(UpdateStats.ServerSaveType.Automatic);
				UpdateStats.OverwriteKongSave = true;
			}
		}

		private void ShowSteamConnect(int marginLeft, int marginTop)
		{
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(230f)), "Please open the game on Steam an input the Steam Name and Id to the input-fields below.");
			marginTop += 60;
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(230f)), "Steam Id: ");
			string s = GUI.TextField(new Rect(GuiBase.Width((float)(marginLeft + 180)), GuiBase.Height((float)marginTop), GuiBase.Width(200f), GuiBase.Height(30f)), ConnectArea.SteamIdInput.ToString());
			long.TryParse(s, out ConnectArea.SteamIdInput);
			marginTop += 40;
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(230f)), "Steam Name: ");
			ConnectArea.SteamNameInput = GUI.TextField(new Rect(GuiBase.Width((float)(marginLeft + 180)), GuiBase.Height((float)marginTop), GuiBase.Width(200f), GuiBase.Height(30f)), ConnectArea.SteamNameInput);
			marginTop += 60;
			if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(250f), GuiBase.Height(30f)), new GUIContent("Get Steam Save", "This will overwrite your current save, then import the online save from Steam, if the id and name are correct. Loading online in Steam will then connect your saves.")))
			{
				UpdateStats.LoadFromServer(UpdateStats.ServerSaveType.Automatic);
				UpdateStats.GetSteamSaveAfterConnecting = true;
			}
			marginTop += 40;
			if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(250f), GuiBase.Height(30f)), new GUIContent("Overwrite Steam Save", "This will overwrite your Steam online save if the id and name are correct. Loading online in Steam will then move your current save to Steam and connect the saves.")))
			{
				UpdateStats.LoadFromServer(UpdateStats.ServerSaveType.Automatic);
				UpdateStats.OverwriteSteamSave = true;
			}
		}

		private void ShowAndroidConnect(int marginLeft, int marginTop)
		{
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(230f)), "Please open the game on Android an input the Android Name and Id to the input-fields below.");
			marginTop += 60;
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(230f)), "Android Id: ");
			ConnectArea.AndroidIdInput = GUI.TextField(new Rect(GuiBase.Width((float)(marginLeft + 180)), GuiBase.Height((float)marginTop), GuiBase.Width(200f), GuiBase.Height(30f)), ConnectArea.AndroidIdInput);
			marginTop += 40;
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(230f)), "Android Name: ");
			ConnectArea.AndroidNameInput = GUI.TextField(new Rect(GuiBase.Width((float)(marginLeft + 180)), GuiBase.Height((float)marginTop), GuiBase.Width(200f), GuiBase.Height(30f)), ConnectArea.AndroidNameInput);
			marginTop += 50;
			if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(250f), GuiBase.Height(30f)), new GUIContent("Get Android Save", "This will overwrite your current save, then import the online save from Android, if the id and name are correct. Loading online in Android will then connect your saves.")))
			{
				UpdateStats.LoadFromServer(UpdateStats.ServerSaveType.Automatic);
				UpdateStats.GetAndroidSaveAfterConnecting = true;
			}
			marginTop += 40;
			if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(250f), GuiBase.Height(30f)), new GUIContent("Overwrite Android Save", "This will overwrite your Android online save if the id and name are correct. Loading online in Android will then move your current save to Android and connect the saves.")))
			{
				UpdateStats.LoadFromServer(UpdateStats.ServerSaveType.Automatic);
				UpdateStats.OverwriteAndroidSave = true;
			}
		}
	}
}
