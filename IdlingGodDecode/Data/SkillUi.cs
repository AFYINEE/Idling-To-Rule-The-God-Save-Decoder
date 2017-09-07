using Assets.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class SkillUi : AreaRightUi
	{
		public static SkillUi Instance = new SkillUi();

		private CDouble totalMysticGain;

		private static bool stopAtInitialized = false;

		private static string stopAtString = "100000";

		private int updateTime = 60;

		private static bool scrollBarsToZero = false;

		protected string StopAtString
		{
			get
			{
				return SkillUi.stopAtString;
			}
			set
			{
				try
				{
					if (!string.IsNullOrEmpty(value) && value.StartsWith("-"))
					{
						value = "0";
					}
					int num = 0;
					int.TryParse(value, out num);
					SkillUi.stopAtString = num.ToString();
					if (num < 100000000)
					{
						App.State.GameSettings.StopClonesAtSkills = num;
					}
					else
					{
						SkillUi.stopAtString = "100000000";
						App.State.GameSettings.StopClonesAtSkills = 100000000;
					}
				}
				catch (Exception)
				{
					SkillUi.stopAtString = "100000000";
					App.State.GameSettings.StopClonesAtSkills = 100000000;
				}
			}
		}

		public new void Show(bool isAchievement)
		{
			if (!SkillUi.stopAtInitialized)
			{
				SkillUi.stopAtString = App.State.GameSettings.StopClonesAtSkills.ToString();
				SkillUi.stopAtInitialized = true;
			}
			this.IsAchievement = isAchievement;
			base.Show(true);
		}

		public override bool Init()
		{
			this.scrollViewHeight = 20 + App.State.AllSkills.Count * 35;
			if (this.IsAchievement)
			{
				GuiBase.ShowAchievements(App.State.SkillAchievements, "mystic");
			}
			return this.IsAchievement;
		}

		protected override void ShowLabels(int marginTop, GUIStyle labelStyle)
		{
			labelStyle.alignment = TextAnchor.MiddleLeft;
			labelStyle.fontStyle = FontStyle.Normal;
			labelStyle.fontSize = GuiBase.FontSize(16);
			if (this.totalMysticGain == null)
			{
				this.totalMysticGain = new CDouble();
			}
			GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height(42f), GuiBase.Width(340f), GuiBase.Height(30f)), new GUIContent("Total Mystic / s: " + this.totalMysticGain.ToGuiText(true), "The total amount of mystic you receive each second from all skill trainings."), labelStyle);
			string text = "Next at";
			if (App.State.GameSettings.UseStopAt)
			{
				text = "Stop at";
			}
			GUI.Label(new Rect(GuiBase.Width(390f), GuiBase.Height(42f), GuiBase.Width(60f), GuiBase.Height(30f)), new GUIContent(text, "Clones will be removed automatically if the level on the right input field is reached. \nIt is not that useful, but some people like to have even numbers."));
			if (App.CurrentPlattform == Plattform.Android)
			{
				GUIStyle textField = Gui.ChosenSkin.textField;
				if (GUI.Button(new Rect(GuiBase.Width(470f), GuiBase.Height(43f), GuiBase.Width(100f), GuiBase.Height(25f)), this.StopAtString, textField))
				{
					base.ShowNumberInput(text, App.State.GameSettings.StopClonesAtSkills, 9223372036854775807L, delegate(CDouble x)
					{
						this.StopAtString = x.ToString();
						if (App.State.GameSettings.SyncTrainingSkill)
						{
							App.State.GameSettings.StopClonesAtTrainings = App.State.GameSettings.StopClonesAtSkills;
						}
					});
				}
			}
			else
			{
				this.StopAtString = GUI.TextField(new Rect(GuiBase.Width(470f), GuiBase.Height(43f), GuiBase.Width(100f), GuiBase.Height(25f)), this.StopAtString);
				if (App.State.GameSettings.SyncTrainingSkill)
				{
					App.State.GameSettings.StopClonesAtTrainings = App.State.GameSettings.StopClonesAtSkills;
				}
			}
			App.State.GameSettings.IsStopAtOnSkills = GUI.Toggle(new Rect(GuiBase.Width(593f), GuiBase.Height(46f), GuiBase.Width(60f), GuiBase.Height(30f)), App.State.GameSettings.IsStopAtOnSkills, string.Empty);
			marginTop += 5;
			labelStyle.alignment = TextAnchor.UpperCenter;
			labelStyle.fontStyle = FontStyle.Bold;
			labelStyle.fontSize = GuiBase.FontSize(18);
			GUI.Label(new Rect(GuiBase.Width(15f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), "Skill");
			GUI.Label(new Rect(GuiBase.Width(230f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), "Level", labelStyle);
			GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), "Clones", labelStyle);
			labelStyle.fontSize = GuiBase.FontSize(16);
		}

		protected override void ShowScrollViewElements(int marginTop, GUIStyle labelStyle)
		{
			if (SkillUi.scrollBarsToZero)
			{
				base.SetScrollbarPosition(Vector2.zero);
				SkillUi.scrollBarsToZero = false;
			}
			if (this.updateTime == 60)
			{
				this.totalMysticGain = new CDouble();
			}
			foreach (Skill current in App.State.AllSkills)
			{
				labelStyle.fontSize = GuiBase.FontSize(16);
				double percent = 0.0;
				if (current.CapCount > 1)
				{
					percent = current.getPercent();
				}
				GuiBase.CreateProgressBar(marginTop, percent, current.Name, current.Description, GuiBase.progressBg, GuiBase.progressFgBlue);
				GUI.Label(new Rect(GuiBase.Width(230f), GuiBase.Height((float)marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), string.Empty + current.LevelText, labelStyle);
				GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), string.Empty + current.ShadowCloneCount, labelStyle);
				if (current.IsAvailable)
				{
					if (this.updateTime == 60 && current.ShadowCloneCount > 0)
					{
						int num = current.ShadowCloneCount.ToInt();
						if (current.CapCount < num)
						{
							num = current.CapCount;
						}
						this.totalMysticGain += current.PowerGainInSec(num);
					}
					if (GUI.Button(new Rect(GuiBase.Width(450f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+"))
					{
						if (Event.current.button == 1)
						{
							current.AddCloneCount(28);
						}
						else
						{
							current.AddCloneCount(App.State.GameSettings.ClonesToAddCount);
						}
					}
					if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "-"))
					{
						current.RemoveCloneCount(App.State.GameSettings.ClonesToAddCount);
					}
					if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height((float)marginTop), GuiBase.Width(50f), GuiBase.Height(30f)), "CAP"))
					{
						current.RemoveCloneCount(999999);
						int num2 = App.State.Clones.IdleClones();
						if (num2 > 0)
						{
							int num3 = current.DurationInMS(1) / 30 + 1;
							int i = num3;
							int num4 = 2;
							while (i > num2)
							{
								i = num3 / num4 + 1;
								num4++;
								if (num4 > App.State.Clones.MaxShadowClones)
								{
									return;
								}
							}
							current.AddCloneCount(i);
						}
					}
				}
				marginTop += 35;
			}
			if (this.updateTime == 60)
			{
				this.updateTime = 0;
			}
			this.updateTime++;
		}

		internal static void ScrollbarToZero()
		{
			SkillUi.scrollBarsToZero = true;
		}
	}
}
