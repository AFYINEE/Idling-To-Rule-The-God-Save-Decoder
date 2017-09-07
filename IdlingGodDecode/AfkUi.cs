using Assets.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	internal class AfkUi : GuiBase
	{
		public static AfkUi Instance = new AfkUi();

		public bool ShowAfk;

		private Texture2D auraball;

		private Texture2D afkyman;

		private Texture2D afkymanShoot;

		private Texture2D auraballInit;

		private Texture2D clone;

		private Texture2D cloneDead;

		private static bool EventInited = false;

		private bool ShowAfkyGod;

		private int toolbarInt;

		private string[] toolbarStrings = new string[]
		{
			"Aura Ball",
			"Present",
			"Heart"
		};

		public void Show(GUIStyle labelStyle)
		{
			labelStyle.alignment = TextAnchor.UpperCenter;
			GUIStyle style = GUI.skin.GetStyle("Button");
			style.fontSize = GuiBase.FontSize(14);
			GUI.BeginGroup(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
			GUI.Box(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), string.Empty);
			GUI.Label(new Rect(GuiBase.Width(300f), GuiBase.Height(20f), GuiBase.Width(400f), GuiBase.Height(40f)), GuiBase.Logo);
			this.ShowAfk = GUI.Toggle(new Rect(GuiBase.Width(780f), GuiBase.Height(20f), GuiBase.Width(80f), GuiBase.Height(30f)), this.ShowAfk, new GUIContent("AFK", "Click to go on afk mode. Progress will be 100% but the cpu usage is lower."));
			GUI.Label(new Rect(GuiBase.Width(250f), GuiBase.Height(80f), GuiBase.Width(500f), GuiBase.Height(250f)), "Afk Mode is on. While this is on, you can't do anything but watching this beautiful screen. The game also progresses fully and uses less cpu.\n");
			GUI.Label(new Rect(GuiBase.Width(235f), GuiBase.Height(180f), GuiBase.Width(500f), GuiBase.Height(25f)), new GUIContent("Your Power level: " + App.State.PowerLevel.ToGuiText(true), "Your total power, including Physical, Mystic, Battle and Creating.\nIt's an indicator of how strong you are compared to other gods. But whether you can defeat one depends on your individual values."));
			int num = 50;
			int num2 = 220;
			this.ShowAfkyGod = GUI.Toggle(new Rect(GuiBase.Width(400f), GuiBase.Height((float)num2), GuiBase.Width(120f), GuiBase.Height(30f)), this.ShowAfkyGod, new GUIContent("Show Afky God", "This is just a minigame and has not much to do with the rest of the game."));
			if (this.ShowAfkyGod)
			{
				AfkyGame game = App.State.Ext.AfkGame;
				num2 += 30;
				labelStyle.alignment = TextAnchor.UpperLeft;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)num2), GuiBase.Width(100f), GuiBase.Height(40f)), "Firing Speed");
				GUI.Label(new Rect(GuiBase.Width((float)(num + 120)), GuiBase.Height((float)num2), GuiBase.Width(400f), GuiBase.Height(40f)), game.FiringSpeed.Level.GuiText);
				if (game.FiringSpeed.Level >= 3800)
				{
					game.FiringSpeed.Level = 3800;
					GUI.Label(new Rect(GuiBase.Width((float)(num + 250)), GuiBase.Height((float)num2), GuiBase.Width(100f), GuiBase.Height(40f)), "Maxed");
				}
				else
				{
					this.AddLvUpButtons(game, num + 250, num2, ref game.FiringSpeed, null);
				}
				num2 += 25;
				string tooltip = string.Empty;
				if (game.Power.Level > 1000000)
				{
					tooltip = string.Empty + game.Power.Level;
				}
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)num2), GuiBase.Width(100f), GuiBase.Height(40f)), new GUIContent("Power", tooltip));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 120)), GuiBase.Height((float)num2), GuiBase.Width(400f), GuiBase.Height(40f)), game.Power.Level.GuiText);
				this.AddLvUpButtons(game, num + 250, num2, ref game.Power, null);
				num2 += 25;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)num2), GuiBase.Width(100f), GuiBase.Height(40f)), "Clone Hp");
				GUI.Label(new Rect(GuiBase.Width((float)(num + 120)), GuiBase.Height((float)num2), GuiBase.Width(400f), GuiBase.Height(40f)), game.CloneHp.Level.GuiText);
				this.AddLvUpButtons(game, num + 250, num2, ref game.CloneHp, delegate
				{
					GuiBase.ShowDialog("Max", "Do you really want to spend all your exp on clone hp?", delegate
					{
						game.Exp -= game.CloneHp.LevelUp(game.Exp, 0, true);
						this.CheckHighestPower(game);
					}, delegate
					{
					}, "Yes", "No", false, false);
				});
				num2 += 25;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)num2), GuiBase.Width(100f), GuiBase.Height(40f)), "Clone Count");
				GUI.Label(new Rect(GuiBase.Width((float)(num + 120)), GuiBase.Height((float)num2), GuiBase.Width(400f), GuiBase.Height(40f)), game.CloneCount.Level.GuiText);
				this.AddLvUpButtons(game, num + 250, num2, ref game.CloneCount, delegate
				{
					GuiBase.ShowDialog("Max", "Do you really want to spend all your exp on clone count?", delegate
					{
						game.Exp -= game.CloneCount.LevelUp(game.Exp, 0, true);
						this.CheckHighestPower(game);
					}, delegate
					{
					}, "Yes", "No", false, false);
				});
				num2 += 25;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)num2), GuiBase.Width(100f), GuiBase.Height(40f)), new GUIContent("Experience", "Exp multiplier = " + App.State.Ext.AfkGame.ExpMulti.ToGuiText(false)));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 120)), GuiBase.Height((float)num2), GuiBase.Width(400f), GuiBase.Height(40f)), game.Exp.GuiText);
				int num3 = this.toolbarInt;
				if (UpdateStats.TimeLeftForEvent < 0L)
				{
					this.toolbarInt = GUI.Toolbar(new Rect(GuiBase.Width((float)(num + 250)), GuiBase.Height((float)(num2 + 2)), GuiBase.Width(275f), GuiBase.Height(25f)), this.toolbarInt, this.toolbarStrings);
				}
				bool flag = this.afkyman == null || this.toolbarInt != num3;
				num2 = 300;
				num = 590;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)num2), GuiBase.Width(100f), GuiBase.Height(40f)), "Clones Killed");
				GUI.Label(new Rect(GuiBase.Width((float)(num + 120)), GuiBase.Height((float)num2), GuiBase.Width(400f), GuiBase.Height(40f)), game.KilledClones.GuiText);
				num2 += 75;
				if (flag)
				{
					if (this.toolbarInt == 0)
					{
						this.afkyman = (Texture2D)Resources.Load("Gui/afkyman", typeof(Texture2D));
						this.afkymanShoot = (Texture2D)Resources.Load("Gui/afkymanshoot", typeof(Texture2D));
						this.auraballInit = (Texture2D)Resources.Load("Gui/auraball_init", typeof(Texture2D));
						this.clone = (Texture2D)Resources.Load("Gui/afky_clone", typeof(Texture2D));
						this.cloneDead = (Texture2D)Resources.Load("Gui/afky_clone_dead", typeof(Texture2D));
						this.auraball = (Texture2D)Resources.Load("Gui/auraball", typeof(Texture2D));
					}
					else if (this.toolbarInt == 1)
					{
						this.afkyman = (Texture2D)Resources.Load("Gui/afkyman_c", typeof(Texture2D));
						this.afkymanShoot = (Texture2D)Resources.Load("Gui/afkymanshoot_c", typeof(Texture2D));
						this.auraballInit = (Texture2D)Resources.Load("Gui/auraball_c", typeof(Texture2D));
						this.clone = (Texture2D)Resources.Load("Gui/afky_clone_c", typeof(Texture2D));
						this.cloneDead = (Texture2D)Resources.Load("Gui/afky_clone_dead_c", typeof(Texture2D));
						this.auraball = (Texture2D)Resources.Load("Gui/auraball_c", typeof(Texture2D));
					}
					else if (this.toolbarInt == 2)
					{
						this.afkyman = (Texture2D)Resources.Load("Gui/afkyman", typeof(Texture2D));
						this.afkymanShoot = (Texture2D)Resources.Load("Gui/afkymanshoot", typeof(Texture2D));
						this.auraballInit = (Texture2D)Resources.Load("Gui/auraball_init_v", typeof(Texture2D));
						this.clone = (Texture2D)Resources.Load("Gui/afky_clone", typeof(Texture2D));
						this.cloneDead = (Texture2D)Resources.Load("Gui/afky_clone_dead", typeof(Texture2D));
						this.auraball = (Texture2D)Resources.Load("Gui/auraball_v", typeof(Texture2D));
					}
				}
				if (UpdateStats.TimeLeftForEvent > 0L && !AfkUi.EventInited)
				{
					AfkUi.EventInited = true;
					this.afkyman = (Texture2D)Resources.Load("Gui/afkyman", typeof(Texture2D));
					this.afkymanShoot = (Texture2D)Resources.Load("Gui/afkymanshoot", typeof(Texture2D));
					this.auraballInit = (Texture2D)Resources.Load("Gui/auraball_init_v", typeof(Texture2D));
					this.clone = (Texture2D)Resources.Load("Gui/afky_clone", typeof(Texture2D));
					this.cloneDead = (Texture2D)Resources.Load("Gui/afky_clone_dead", typeof(Texture2D));
					this.auraball = (Texture2D)Resources.Load("Gui/auraball_v", typeof(Texture2D));
				}
				if (game.FiringPhase)
				{
					GUI.Label(new Rect(GuiBase.Width(220f), GuiBase.Height((float)num2), GuiBase.Width(200f), GuiBase.Height(200f)), this.afkymanShoot);
				}
				else
				{
					GUI.Label(new Rect(GuiBase.Width(220f), GuiBase.Height((float)num2), GuiBase.Width(200f), GuiBase.Height(200f)), this.afkyman);
					GUI.Label(new Rect(GuiBase.Width(260f), GuiBase.Height((float)(num2 + 40)), GuiBase.Width(80f), GuiBase.Height(80f)), this.auraballInit);
				}
				for (int i = game.AuraballPositions.Count - 1; i >= 0; i--)
				{
					GUI.Label(new Rect(GuiBase.Width(game.AuraballPositions[i].Pos), GuiBase.Height(410f), GuiBase.Width(80f), GuiBase.Height(80f)), this.auraball);
				}
				int num4 = game.CloneCount.Level.ToInt();
				if (num4 > 10)
				{
					num4 = 10;
				}
				for (int j = 0; j < num4; j++)
				{
					if (game.deadCloneTimer > 0f && j < game.ShootKillCount && game.ShootKillCount > 0)
					{
						GUI.Label(new Rect(GuiBase.Width((float)(game.PosFirstClone + j * 25)), GuiBase.Height((float)num2), GuiBase.Width(200f), GuiBase.Height(200f)), this.cloneDead);
					}
					else
					{
						GUI.Label(new Rect(GuiBase.Width((float)(game.PosFirstClone + j * 25)), GuiBase.Height((float)num2), GuiBase.Width(200f), GuiBase.Height(200f)), this.clone);
					}
				}
				if (game.deadCloneTimer > 0f)
				{
					GUI.Label(new Rect(GuiBase.Width(710f), GuiBase.Height(320f), GuiBase.Width(310f), GuiBase.Height(200f)), " + " + game.ShootKillCount.GuiText);
					GUI.Label(new Rect(GuiBase.Width(170f), GuiBase.Height(375f), GuiBase.Width(200f), GuiBase.Height(200f)), " + " + game.CloneExpTotal.ToGuiText(false));
				}
			}
			GUI.EndGroup();
		}

		private void AddLvUpButtons(AfkyGame game, int marginLeft, int marginTop, ref AfkyGame.StatExp power, Action askDialog = null)
		{
			if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(30f), GuiBase.Height(25f)), new GUIContent(" + ", "Cost: " + power.ExpCostNextLevel.GuiText + " experience.")))
			{
				game.Exp -= power.LevelUp(game.Exp, 1, false);
				this.CheckHighestPower(game);
			}
			marginLeft += 35;
			bool flag = power.ExpCostNext10000Levels > 0 && game.Exp > power.ExpCostNext10000Levels;
			if (!flag)
			{
				if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(55f), GuiBase.Height(25f)), new GUIContent(" + 10", "Cost: " + power.ExpCostNext10Levels.GuiText + " experience.")))
				{
					game.Exp -= power.LevelUp(game.Exp, 10, false);
					this.CheckHighestPower(game);
				}
				marginLeft += 60;
			}
			if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(55f), GuiBase.Height(25f)), new GUIContent(" + 100", "Cost: " + power.ExpCostNext100Levels.GuiText + " experience.")))
			{
				game.Exp -= power.LevelUp(game.Exp, 100, false);
				this.CheckHighestPower(game);
			}
			marginLeft += 60;
			if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(55f), GuiBase.Height(25f)), new GUIContent(" + 1k", "Cost: " + power.ExpCostNext1000Levels.GuiText + " experience.")))
			{
				game.Exp -= power.LevelUp(game.Exp, 1000, false);
				this.CheckHighestPower(game);
			}
			if (flag)
			{
				marginLeft += 60;
				if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(55f), GuiBase.Height(25f)), new GUIContent(" + 10k", "Cost: " + power.ExpCostNext10000Levels.GuiText + " experience.")))
				{
					game.Exp -= power.LevelUp(game.Exp, 10000, false);
					this.CheckHighestPower(game);
				}
			}
			marginLeft += 60;
			if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(60f), GuiBase.Height(25f)), new GUIContent(" + Max", "Almost all your experience.")))
			{
				if (askDialog != null)
				{
					askDialog();
				}
				else
				{
					game.Exp -= power.LevelUp(game.Exp, 0, true);
					this.CheckHighestPower(game);
				}
			}
		}

		private void CheckHighestPower(AfkyGame game)
		{
			if (App.State.Statistic.AfkyGodPower < game.Power.Level)
			{
				App.State.Statistic.AfkyGodPower = game.Power.Level;
				Leaderboards.SubmitStat(LeaderBoardType.AfkyGodPower, App.State.Statistic.AfkyGodPower.ToInt(), false);
			}
		}
	}
}
