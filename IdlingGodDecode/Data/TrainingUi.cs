using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class TrainingUi : AreaRightUi
	{
		public static TrainingUi Instance = new TrainingUi();

		private CDouble totalPhysicalGain;

		private static bool stopAtInitialized = false;

		private static string stopAtString = "100000";

		private int updateTime = 60;

		private static bool scrollBarsToZero = false;

		protected string StopAtString
		{
			get
			{
				return TrainingUi.stopAtString;
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
					TrainingUi.stopAtString = num.ToString();
					if (num < 100000000)
					{
						App.State.GameSettings.StopClonesAtTrainings = num;
					}
					else
					{
						TrainingUi.stopAtString = "100000000";
						App.State.GameSettings.StopClonesAtTrainings = 100000000;
					}
				}
				catch (Exception)
				{
					TrainingUi.stopAtString = "100000";
					App.State.GameSettings.StopClonesAtTrainings = 100000;
				}
			}
		}

		public new void Show(bool isAchievement)
		{
			if (!TrainingUi.stopAtInitialized)
			{
				TrainingUi.stopAtString = App.State.GameSettings.StopClonesAtTrainings.ToString();
				TrainingUi.stopAtInitialized = true;
			}
			this.IsAchievement = isAchievement;
			base.Show(true);
		}

		public override bool Init()
		{
			this.scrollViewHeight = 20 + App.State.AllTrainings.Count * 35;
			if (this.IsAchievement)
			{
				GuiBase.ShowAchievements(App.State.TrainingAchievements, "physical");
			}
			return this.IsAchievement;
		}

		protected override void ShowLabels(int marginTop, GUIStyle labelStyle)
		{
			labelStyle.alignment = TextAnchor.MiddleLeft;
			labelStyle.fontStyle = FontStyle.Normal;
			labelStyle.fontSize = GuiBase.FontSize(16);
			if (this.totalPhysicalGain == null)
			{
				this.totalPhysicalGain = new CDouble();
			}
			GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height(42f), GuiBase.Width(340f), GuiBase.Height(30f)), new GUIContent("Total Physical / s: " + this.totalPhysicalGain.ToGuiText(true), "The total amount of physical you receive each second from all physical trainings."), labelStyle);
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
					base.ShowNumberInput(text, App.State.GameSettings.StopClonesAtTrainings, 9223372036854775807L, delegate(CDouble x)
					{
						this.StopAtString = x.ToString();
						if (App.State.GameSettings.SyncTrainingSkill)
						{
							App.State.GameSettings.StopClonesAtSkills = App.State.GameSettings.StopClonesAtTrainings;
						}
					});
				}
			}
			else
			{
				this.StopAtString = GUI.TextField(new Rect(GuiBase.Width(470f), GuiBase.Height(43f), GuiBase.Width(100f), GuiBase.Height(25f)), this.StopAtString);
				if (App.State.GameSettings.SyncTrainingSkill)
				{
					App.State.GameSettings.StopClonesAtSkills = App.State.GameSettings.StopClonesAtTrainings;
				}
			}
			App.State.GameSettings.IsStopAtOnTrainings = GUI.Toggle(new Rect(GuiBase.Width(593f), GuiBase.Height(46f), GuiBase.Width(60f), GuiBase.Height(30f)), App.State.GameSettings.IsStopAtOnTrainings, string.Empty);
			marginTop += 5;
			labelStyle.alignment = TextAnchor.UpperCenter;
			labelStyle.fontStyle = FontStyle.Bold;
			labelStyle.fontSize = GuiBase.FontSize(18);
			GUI.Label(new Rect(GuiBase.Width(15f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), "Training");
			GUI.Label(new Rect(GuiBase.Width(230f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), "Level", labelStyle);
			GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), "Clones", labelStyle);
			labelStyle.fontSize = GuiBase.FontSize(16);
			GUI.Label(new Rect(GuiBase.Width(510f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Sync", "If is this is on, clicking + or - will automatically add or remove the same number of clones from skills, if there are clones on the skill, and the skill is unlocked."), labelStyle);
			App.State.GameSettings.SyncTrainingSkill = GUI.Toggle(new Rect(GuiBase.Width(593f), GuiBase.Height((float)(marginTop + 4)), GuiBase.Width(60f), GuiBase.Height(30f)), App.State.GameSettings.SyncTrainingSkill, string.Empty);
		}

		protected override void ShowScrollViewElements(int marginTop, GUIStyle labelStyle)
		{
			if (TrainingUi.scrollBarsToZero)
			{
				base.SetScrollbarPosition(Vector2.zero);
				TrainingUi.scrollBarsToZero = false;
			}
			if (this.updateTime == 60)
			{
				this.totalPhysicalGain = new CDouble();
			}
			using (List<Training>.Enumerator enumerator = App.State.AllTrainings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Training training = enumerator.Current;
					labelStyle.fontSize = GuiBase.FontSize(16);
					double percent = 0.0;
					if (training.CapCount > 1)
					{
						percent = training.getPercent();
					}
					GuiBase.CreateProgressBar(marginTop, percent, training.Name, training.Description, GuiBase.progressBg, GuiBase.progressFgBlue);
					GUI.Label(new Rect(GuiBase.Width(230f), GuiBase.Height((float)marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), string.Empty + training.LevelText);
					GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), string.Empty + training.ShadowCloneCount);
					if (training.IsAvailable)
					{
						if (this.updateTime == 60 && training.ShadowCloneCount > 0)
						{
							int num = training.ShadowCloneCount.ToInt();
							if (training.CapCount < num)
							{
								num = training.CapCount;
							}
							this.totalPhysicalGain += training.PowerGainInSec(num);
						}
						if (GUI.Button(new Rect(GuiBase.Width(450f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+"))
						{
							int value = App.State.GameSettings.ClonesToAddCount;
							if (Event.current.button == 1)
							{
								value = 28;
							}
							training.AddCloneCount(value);
							if (App.State.GameSettings.SyncTrainingSkill)
							{
								Skill skill = App.State.AllSkills.FirstOrDefault((Skill x) => x.TypeEnum == (Skill.SkillType)training.TypeEnum);
								if (skill != null && skill.IsAvailable)
								{
									skill.AddCloneCount(value);
								}
							}
						}
						if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "-"))
						{
							training.RemoveCloneCount(App.State.GameSettings.ClonesToAddCount);
							if (App.State.GameSettings.SyncTrainingSkill)
							{
								Skill skill2 = App.State.AllSkills.FirstOrDefault((Skill x) => x.TypeEnum == (Skill.SkillType)training.TypeEnum);
								if (skill2 != null && skill2.IsAvailable)
								{
									skill2.RemoveCloneCount(App.State.GameSettings.ClonesToAddCount);
								}
							}
						}
						if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height((float)marginTop), GuiBase.Width(50f), GuiBase.Height(30f)), "CAP"))
						{
							Skill skill3 = null;
							if (App.State.GameSettings.SyncTrainingSkill)
							{
								skill3 = App.State.AllSkills.FirstOrDefault((Skill x) => x.TypeEnum == (Skill.SkillType)training.TypeEnum);
							}
							training.RemoveCloneCount(9999999);
							if (skill3 != null && skill3.IsAvailable)
							{
								skill3.RemoveCloneCount(9999999);
							}
							int num2 = App.State.Clones.IdleClones();
							if (skill3 != null && skill3.IsAvailable)
							{
								num2 /= 2;
							}
							if (num2 > 0)
							{
								int num3 = training.DurationInMS(1) / 30 + 1;
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
								training.AddCloneCount(i);
								if (skill3 != null && skill3.IsAvailable)
								{
									skill3.AddCloneCount(i);
								}
							}
						}
					}
					marginTop += 35;
				}
			}
			if (this.updateTime == 60)
			{
				this.updateTime = 0;
			}
			this.updateTime++;
		}

		internal static void ScrollbarToZero()
		{
			TrainingUi.scrollBarsToZero = true;
		}
	}
}
