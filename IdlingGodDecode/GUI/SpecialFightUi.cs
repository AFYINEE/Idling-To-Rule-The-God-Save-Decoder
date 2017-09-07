using Assets.Scripts.Data;
using Assets.Scripts.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class SpecialFightUi : GuiBase
	{
		public static SpecialFightUi Instance = new SpecialFightUi();

		private static Texture2D coolDownBg;

		private static Texture2D coolDownProgress;

		private bool showToolTips = true;

		private bool sortSkills;

		private static long timeLeft = 60000L;

		private bool moreInfo;

		public int ImageWidth = 100;

		public int ImageHeight = 100;

		public static Color UIColor = default(Color);

		private Skill skillToSelectKey;

		private static List<Skill> sortedSkills = new List<Skill>();

		private List<Skill> skillsToShow;

		private bool showLog;

		private static Vector2 scrollPosition = Vector2.zero;

		private BattleState Battle
		{
			get
			{
				if (App.State == null)
				{
					return new BattleState();
				}
				return App.State.Battle;
			}
		}

		public static bool IsFighting
		{
			get
			{
				return SpecialFightUi.Instance.Battle.IsFighting;
			}
		}

		public bool NeedsKeyInputs
		{
			get
			{
				return this.Battle.IsFighting || this.skillToSelectKey != null;
			}
		}

		public static void SortSkills()
		{
			SpecialFightUi.sortedSkills = new List<Skill>();
			List<Skill> list = new List<Skill>();
			List<Skill> list2 = new List<Skill>();
			List<Skill> list3 = new List<Skill>();
			List<Skill> list4 = new List<Skill>();
			List<Skill> list5 = new List<Skill>();
			foreach (Skill current in App.State.AllSkills)
			{
				SkillExtension extension = current.Extension;
				if (extension.DamagePercent > 0)
				{
					list.Add(current);
				}
				else if (extension.DodgeChance > 0)
				{
					list3.Add(current);
				}
				else if (extension.DamageBlock || extension.DamageReflect)
				{
					list4.Add(current);
				}
				else if (extension.DamageDecreaseDuration > 0L || extension.DamageIncreaseDuration > 0L)
				{
					list2.Add(current);
				}
				else
				{
					list5.Add(current);
				}
			}
			SpecialFightUi.sortedSkills.AddRange(list);
			SpecialFightUi.sortedSkills.AddRange(list2);
			SpecialFightUi.sortedSkills.AddRange(list3);
			SpecialFightUi.sortedSkills.AddRange(list4);
			SpecialFightUi.sortedSkills.AddRange(list5);
		}

		public void Init()
		{
			SpecialFightUi.coolDownBg = (Texture2D)Resources.Load("Gui/progress_bg", typeof(Texture2D));
			SpecialFightUi.coolDownProgress = (Texture2D)Resources.Load("Gui/progress_fg_red", typeof(Texture2D));
			this.sortSkills = App.State.GameSettings.SpecialFightSkillsSorted;
			if (this.sortSkills)
			{
				SpecialFightUi.SortSkills();
			}
		}

		internal void Update()
		{
			if (this.skillToSelectKey != null)
			{
				IEnumerator enumerator = Enum.GetValues(typeof(KeyCode)).GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						KeyCode keyCode = (KeyCode)enumerator.Current;
						if (Input.GetKeyDown(keyCode) && this.skillsToShow != null)
						{
							if (keyCode != KeyCode.Mouse0 && keyCode != KeyCode.Mouse1)
							{
								KeyCode keyPress = this.skillToSelectKey.Extension.KeyPress;
								this.skillToSelectKey.Extension.KeyPress = keyCode;
								GuiBase.ShowToast(this.skillToSelectKey.Name + " is now bound to " + keyCode.ToString());
								foreach (Skill current in this.skillsToShow)
								{
									if (current != this.skillToSelectKey && current.Extension.KeyPress == keyCode)
									{
										current.Extension.KeyPress = keyPress;
									}
								}
							}
							this.skillToSelectKey = null;
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			if (!this.Battle.IsFighting)
			{
				return;
			}
			if (this.skillsToShow != null)
			{
				foreach (Skill current2 in this.skillsToShow)
				{
					if (Input.GetKey(current2.Extension.KeyPress))
					{
						if (!this.Battle.IsFighting)
						{
							GuiBase.ShowToast("Start the fight first!");
						}
						else if (current2.Extension.CoolDownCurrent <= 0L && current2.IsAvailable)
						{
							this.Battle.UseSkill(current2, true);
						}
					}
				}
			}
		}

		public static void Show()
		{
			SpecialFightUi.Instance.show();
		}

		public static void UpdateAutoMode(long ms)
		{
			if (App.State.GameSettings.AutoFightIsOn)
			{
				SpecialFightUi.timeLeft -= 30L;
				if (App.State.CreatingSpeedBoniDuration > 0L)
				{
					SpecialFightUi.timeLeft -= 60L;
				}
				if (SpecialFightUi.timeLeft <= 0L)
				{
					SpecialFightUi.timeLeft = 60000L;
					foreach (Skill current in App.State.AllSkills)
					{
						if (current.IsAvailable)
						{
							current.Extension.UsageCount += 1L;
						}
					}
				}
			}
		}

		private void show()
		{
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.fontSize = GuiBase.FontSize(16);
			style.alignment = TextAnchor.UpperLeft;
			GUIStyle style2 = GUI.skin.GetStyle("Button");
			style2.fontSize = GuiBase.FontSize(16);
			GUI.BeginGroup(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)));
			GUI.Box(new Rect(GuiBase.Width(0f), GuiBase.Height(0f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			if (this.Battle.IsBattleFinished)
			{
				this.ShowBattleFinished();
			}
			else
			{
				if (!this.Battle.IsFighting && !App.State.GameSettings.AutoFightIsOn)
				{
					if (this.moreInfo)
					{
						if (GUI.Button(new Rect(GuiBase.Width(520f), GuiBase.Height(10f), GuiBase.Width(90f), GuiBase.Height(30f)), new GUIContent("Back", string.Empty)))
						{
							this.moreInfo = false;
						}
						style.fontSize = GuiBase.FontSize(14);
						GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(10f), GuiBase.Width(620f), GuiBase.Height(300f)), "Mystic is useless in this fight.\nEnemy attack and hp is calculated from your power level.\nThe divinity you gain is (divinity gained from battles every second * 2 + divinity gained from generator) * 200 for Jacky, 300 for Cthulhu, 500 for doppel, 500 + 1000 for D.evelope, 4*gods defeated for gods, 10,30,60,100… for shadow clones.\nSkillPower = 1 x skill level until 100k + 0.05 x skill level until 1100k + 0.01 x skill level after 1100k.\nThe damage you do is: SkillPower * base damage * damage increase shown on top (the percent one) * Player attack / 1 billion * number of hits.\n");
						style.fontSize = GuiBase.FontSize(16);
					}
					else
					{
						if (GUI.Button(new Rect(GuiBase.Width(40f), GuiBase.Height(100f), GuiBase.Width(180f), GuiBase.Height(30f)), new GUIContent("Fight " + this.Battle.Enemies.Endless.Name, this.Battle.Enemies.Endless.Description)))
						{
							this.Battle.StartFight(BattleState.BattleType.endless);
						}
						if (GUI.Button(new Rect(GuiBase.Width(240f), GuiBase.Height(100f), GuiBase.Width(180f), GuiBase.Height(30f)), new GUIContent("Fight " + this.Battle.Enemies.Jacky.Name, this.Battle.Enemies.Jacky.Description)))
						{
							this.Battle.StartFight(BattleState.BattleType.jacky);
						}
						if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(100f), GuiBase.Width(180f), GuiBase.Height(30f)), new GUIContent("Fight " + this.Battle.Enemies.Cthulhu.Name, this.Battle.Enemies.Cthulhu.Description)))
						{
							this.Battle.StartFight(BattleState.BattleType.cthulhu);
						}
						if (GUI.Button(new Rect(GuiBase.Width(40f), GuiBase.Height(140f), GuiBase.Width(180f), GuiBase.Height(30f)), new GUIContent("Fight " + this.Battle.Enemies.Doppelganger.Name, this.Battle.Enemies.Doppelganger.Description)))
						{
							this.Battle.StartFight(BattleState.BattleType.doppel);
						}
						if (GUI.Button(new Rect(GuiBase.Width(240f), GuiBase.Height(140f), GuiBase.Width(180f), GuiBase.Height(30f)), new GUIContent("Fight " + this.Battle.Enemies.Developer.Name, this.Battle.Enemies.Developer.Description)))
						{
							this.Battle.StartFight(BattleState.BattleType.developer);
						}
						if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(140f), GuiBase.Width(180f), GuiBase.Height(30f)), new GUIContent("Fight Gods", "Fight all the other gods. Unlike all other fights, their stats are constant and not scaled by your power.\nThey do not count for any statistic.")))
						{
							this.Battle.StartFight(BattleState.BattleType.gods);
						}
						GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height(10f), GuiBase.Width(470f), GuiBase.Height(100f)), "Using skills 3 times in battle reduces the clone cap of Physical and Skill training of the same tier by one. This means you need less clones for the same power-gain. Lasts through rebirths.");
						if (GUI.Button(new Rect(GuiBase.Width(520f), GuiBase.Height(10f), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("More Info", string.Empty)))
						{
							this.moreInfo = true;
						}
					}
				}
				else if (App.State.GameSettings.AutoFightIsOn)
				{
					style.fontStyle = FontStyle.Bold;
					style.alignment = TextAnchor.MiddleCenter;
					long time = SpecialFightUi.timeLeft;
					if (App.State.CreatingSpeedBoniDuration > 0L)
					{
						time = SpecialFightUi.timeLeft / 3L;
						GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(20f), GuiBase.Width(640f), GuiBase.Height(50f)), "Trainingspeed is 300% for the next " + Conv.MsToGuiText(App.State.CreatingSpeedBoniDuration, true) + "!");
					}
					GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(50f), GuiBase.Width(640f), GuiBase.Height(50f)), "Automode is on. \nNext skill training is in " + Conv.MsToGuiSec(time));
					style.alignment = TextAnchor.UpperLeft;
					style.fontStyle = FontStyle.Normal;
				}
				else
				{
					string text = App.State.AvatarName;
					if (string.IsNullOrEmpty(text))
					{
						text = "Guest";
					}
					if (GUI.Button(new Rect(GuiBase.Width(10f), GuiBase.Height(10f), GuiBase.Width(80f), GuiBase.Height(30f)), new GUIContent("Flee")))
					{
						this.Battle.IsFighting = false;
						this.Battle.ResetSkillCoolDowns(true);
						GuiBase.ShowToast("You ran away!\nThe cooldowns are resetted to 0 but to prevent exploiting that the usage count of all skills with a higher cooldown than 500 ms is reduced by 1.");
					}
					style.fontStyle = FontStyle.Bold;
					this.CreateProgressBar(120, 11, 185, this.Battle.PlayerHp.Double, App.State.MaxHealth.Double, text, this.Battle.PlayerHp.ToGuiText(true) + " / " + this.Battle.PlayerMaxHP.ToGuiText(true), style);
					style.fontSize = GuiBase.FontSize(22);
					GUI.Label(new Rect(GuiBase.Width(325f), GuiBase.Height(10f), GuiBase.Width(60f), GuiBase.Height(30f)), "VS");
					style.fontSize = GuiBase.FontSize(16);
					this.CreateProgressBar(375, 11, 185, this.Battle.CurrentEnemy.HP.Double, this.Battle.CurrentEnemy.HPMax.Double, this.Battle.CurrentEnemy.Name, this.Battle.CurrentEnemy.HP.ToGuiText(true) + " / " + this.Battle.CurrentEnemy.HPMax.ToGuiText(true), style);
					style.fontStyle = FontStyle.Normal;
					style.fontSize = GuiBase.FontSize(16);
					style.fontStyle = FontStyle.Bold;
					GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(50f), GuiBase.Width(640f), GuiBase.Height(30f)), "Your total Damage: " + this.Battle.TotalPlayerDamage.ToGuiText(true));
					style.fontStyle = FontStyle.Normal;
					style.fontSize = GuiBase.FontSize(14);
					GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(75f), GuiBase.Width(640f), GuiBase.Height(45f)), "Your Damage: " + this.Battle.DamageIncrease + " %");
					GUI.Label(new Rect(GuiBase.Width(220f), GuiBase.Height(75f), GuiBase.Width(640f), GuiBase.Height(45f)), "Enemy Damage: " + this.Battle.DamageDecrease + " %");
					GUI.Label(new Rect(GuiBase.Width(430f), GuiBase.Height(75f), GuiBase.Width(640f), GuiBase.Height(45f)), "Dodge Chance = " + this.Battle.DodgeChance + " %");
					GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(95f), GuiBase.Width(640f), GuiBase.Height(45f)), "Counter Chance = " + this.Battle.CounterChance + " %");
					GUI.Label(new Rect(GuiBase.Width(220f), GuiBase.Height(95f), GuiBase.Width(640f), GuiBase.Height(45f)), "God Speed: " + this.Battle.GodSpeedModeDuration + " ms");
					GUI.Label(new Rect(GuiBase.Width(430f), GuiBase.Height(95f), GuiBase.Width(640f), GuiBase.Height(45f)), "Gear Eyes: " + this.Battle.GearEyesDuration + " ms");
					GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(115f), GuiBase.Width(640f), GuiBase.Height(45f)), "Double Damage = " + this.Battle.DoubleUp);
					GUI.Label(new Rect(GuiBase.Width(220f), GuiBase.Height(115f), GuiBase.Width(640f), GuiBase.Height(45f)), "Damage Block = " + this.Battle.DamageBlock);
					GUI.Label(new Rect(GuiBase.Width(430f), GuiBase.Height(115f), GuiBase.Width(640f), GuiBase.Height(45f)), "Damage Reflect = " + this.Battle.DamageReflect);
					GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(150f), GuiBase.Width(640f), GuiBase.Height(45f)), this.Battle.SkillUseText);
					GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(185f), GuiBase.Width(640f), GuiBase.Height(45f)), this.Battle.EnemyDamageText);
				}
				if (!this.Battle.IsFighting)
				{
					this.showToolTips = GUI.Toggle(new Rect(GuiBase.Width(20f), GuiBase.Height(190f), GuiBase.Width(200f), GuiBase.Height(30f)), this.showToolTips, new GUIContent("Show skill tooltips"));
					this.sortSkills = GUI.Toggle(new Rect(GuiBase.Width(220f), GuiBase.Height(190f), GuiBase.Width(140f), GuiBase.Height(30f)), this.sortSkills, new GUIContent("Sort skills", "If this is on, skills are sorted top to bottom in order of damage skills, buff skills, dodge/block skills, other skills."));
					App.State.GameSettings.AutoFightIsOn = GUI.Toggle(new Rect(GuiBase.Width(370f), GuiBase.Height(190f), GuiBase.Width(200f), GuiBase.Height(30f)), App.State.GameSettings.AutoFightIsOn, new GUIContent("Auto", "If this is on, every skill will be used once a minute while you are online and not fighting."));
					if (App.State.GameSettings.SpecialFightSkillsSorted != this.sortSkills)
					{
						App.State.GameSettings.SpecialFightSkillsSorted = this.sortSkills;
						this.Init();
					}
				}
				style2.fontSize = GuiBase.FontSize(13);
				int num = 220;
				int num2 = 10;
				int num3 = 0;
				this.skillsToShow = App.State.AllSkills;
				if (this.sortSkills)
				{
					this.skillsToShow = SpecialFightUi.sortedSkills;
				}
				foreach (Skill current in this.skillsToShow)
				{
					string text2 = "You need to unlock this skill first!";
					if (current.IsAvailable)
					{
						text2 = current.Extension.Description;
					}
					if (!this.showToolTips)
					{
						text2 = string.Empty;
					}
					if (current.Extension.CoolDownCurrent > 0L)
					{
						this.CreateProgressBar(num2, num, 160, (double)current.Extension.CoolDownCurrent, (double)current.Extension.CoolDownBase, Conv.MsToGuiSec(current.Extension.CoolDownCurrent), text2, style);
					}
					else
					{
						string text3 = current.Name;
						if (App.CurrentPlattform != Plattform.Android)
						{
							if (current.TypeEnum == Skill.SkillType.unlimited_creation_works && current.IsAvailable)
							{
								text3 = "UCW (" + EnumName.Name(current.Extension.KeyPress) + ")";
							}
							else if (current.TypeEnum == Skill.SkillType.elemental_manipulation && current.IsAvailable)
							{
								text3 = "Elemental m. (" + EnumName.Name(current.Extension.KeyPress) + ")";
							}
							else
							{
								text3 = text3 + " (" + EnumName.Name(current.Extension.KeyPress) + ")";
							}
						}
						if (GUI.Button(new Rect(GuiBase.Width((float)num2), GuiBase.Height((float)num), GuiBase.Width(152f), GuiBase.Height(30f)), new GUIContent(text3, text2)))
						{
							if (!this.Battle.IsFighting)
							{
								if (App.CurrentPlattform == Plattform.Android)
								{
									GuiBase.ShowToast("Start the fight first!");
								}
								else
								{
									this.skillToSelectKey = current;
									GuiBase.ShowToast("Press a key now to bind it to " + current.Name);
								}
							}
							else if (current.IsAvailable)
							{
								this.Battle.UseSkill(current, false);
							}
						}
					}
					num2 += 161;
					num3++;
					if (num3 % 4 == 0)
					{
						num2 = 10;
						num += 35;
					}
				}
			}
			GUI.EndGroup();
		}

		private void ShowBattleFinished()
		{
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.fontSize = GuiBase.FontSize(16);
			style.alignment = TextAnchor.UpperCenter;
			style.fontStyle = FontStyle.Bold;
			GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(20f), GuiBase.Width(640f), GuiBase.Height(45f)), "Battle finished");
			style.fontStyle = FontStyle.Normal;
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = GuiBase.FontSize(14);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(50f), GuiBase.Width(640f), GuiBase.Height(45f)), this.Battle.BattleRewardText);
			if (GUI.Button(new Rect(GuiBase.Width(200f), GuiBase.Height(100f), GuiBase.Width(100f), GuiBase.Height(30f)), "Finish"))
			{
				this.Battle.IsBattleFinished = false;
			}
			string text = "Show Battle log";
			if (this.showLog)
			{
				text = "Hide Battle log";
			}
			if (GUI.Button(new Rect(GuiBase.Width(20f), GuiBase.Height(100f), GuiBase.Width(150f), GuiBase.Height(30f)), text))
			{
				this.showLog = !this.showLog;
			}
			if (this.showLog)
			{
				int num = 150;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string current in this.Battle.FightingLog)
				{
					stringBuilder.Append(current).Append("\n");
				}
				string text2 = stringBuilder.ToString();
				int num2 = (int)style.CalcHeight(new GUIContent(text2), GuiBase.Width(600f));
				SpecialFightUi.scrollPosition = GuiBase.TouchScrollView(new Rect(GuiBase.Width(20f), GuiBase.Height(150f), GuiBase.Width(630f), GuiBase.Height(320f)), SpecialFightUi.scrollPosition, new Rect(0f, GuiBase.Height(150f), GuiBase.Width(600f), GuiBase.Height((float)num2)));
				GUI.Label(new Rect(GuiBase.Width(0f), GuiBase.Height((float)(num - 2)), GuiBase.Width(600f), (float)num2), text2, style);
				GUI.EndScrollView();
			}
		}

		public void CreateProgressBar(int marginLeft, int marginTop, int width, double current, double max, string name, string mouseOver, GUIStyle labelStyle)
		{
			float num = GuiBase.Width((float)width);
			float height = GuiBase.Height((float)((int)((double)width / 6.2)));
			GUI.BeginGroup(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), num, height));
			GUI.Label(new Rect(0f, 0f, num, height), SpecialFightUi.coolDownBg);
			float width2 = (float)(current * 100.0 / max) * num / 100f;
			GUI.BeginGroup(new Rect(0f, 0f, width2, height));
			GUI.Label(new Rect(0f, 0f, num, height), SpecialFightUi.coolDownProgress);
			GUI.EndGroup();
			labelStyle.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(0f, 0f, num, height), new GUIContent(name, mouseOver));
			labelStyle.alignment = TextAnchor.UpperLeft;
			GUI.EndGroup();
		}
	}
}
