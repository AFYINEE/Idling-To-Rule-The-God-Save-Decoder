using Assets.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class FightingUi : AreaRightUi
	{
		public static FightingUi Instance = new FightingUi();

		private CDouble totalBattleGain;

		private int updateTime = 60;

		private static bool scrollBarsToZero = false;

		public new void Show(bool isAchievement)
		{
			this.IsAchievement = isAchievement;
			base.Show(false);
		}

		public override bool Init()
		{
			this.scrollViewHeight = 50 + App.State.AllFights.Count * 35;
			if (this.IsAchievement)
			{
				GuiBase.ShowAchievements(App.State.FightingAchievements, "battle");
			}
			return this.IsAchievement;
		}

		protected override void ShowLabels(int marginTop, GUIStyle labelStyle)
		{
			labelStyle.alignment = TextAnchor.MiddleLeft;
			labelStyle.fontStyle = FontStyle.Normal;
			labelStyle.fontSize = GuiBase.FontSize(16);
			if (this.totalBattleGain == null)
			{
				this.totalBattleGain = new CDouble();
			}
			GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height(42f), GuiBase.Width(340f), GuiBase.Height(30f)), new GUIContent("Total Battle / s: " + this.totalBattleGain.ToGuiText(true), "The total amount of battle you receive each second from all fights." + App.State.DivGainText), labelStyle);
			GUI.Label(new Rect(GuiBase.Width(400f), GuiBase.Height(42f), GuiBase.Width(200f), GuiBase.Height(30f)), new GUIContent("Add clones if defeated", "If this is on and you are fighting an enemy who can kill your clones, new clones will be added automatically if you have enough."), labelStyle);
			App.State.GameSettings.AutoAddClones = GUI.Toggle(new Rect(GuiBase.Width(593f), GuiBase.Height(46f), GuiBase.Width(300f), GuiBase.Height(30f)), App.State.GameSettings.AutoAddClones, string.Empty);
			marginTop += 5;
			labelStyle.alignment = TextAnchor.UpperCenter;
			labelStyle.fontStyle = FontStyle.Bold;
			labelStyle.fontSize = GuiBase.FontSize(18);
			GUI.Label(new Rect(GuiBase.Width(15f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), "Enemy");
			GUI.Label(new Rect(GuiBase.Width(230f), GuiBase.Height((float)marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), "Defeated", labelStyle);
			GUI.Label(new Rect(GuiBase.Width(340f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), "Clones", labelStyle);
			labelStyle.fontSize = GuiBase.FontSize(16);
			labelStyle.fontStyle = FontStyle.Normal;
			GUI.Label(new Rect(GuiBase.Width(450f), GuiBase.Height((float)marginTop), GuiBase.Width(150f), GuiBase.Height(30f)), new GUIContent("Next if 1 cloned", "If this is on, all but 1 clone will move to the next fight, if you only need 1 clone to fight a monster."), labelStyle);
			App.State.GameSettings.NextFightIf1Cloned = GUI.Toggle(new Rect(GuiBase.Width(593f), GuiBase.Height((float)marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), App.State.GameSettings.NextFightIf1Cloned, string.Empty);
		}

		protected override void ShowScrollViewElements(int marginTop, GUIStyle labelStyle)
		{
			if (FightingUi.scrollBarsToZero)
			{
				base.SetScrollbarPosition(Vector2.zero);
				FightingUi.scrollBarsToZero = false;
			}
			if (this.updateTime == 60)
			{
				this.totalBattleGain = new CDouble();
			}
			foreach (Fight current in App.State.AllFights)
			{
				labelStyle.fontSize = GuiBase.FontSize(16);
				GuiBase.CreateProgressBar(marginTop, current.getPercentOfHP(), current.Name, current.Description, GuiBase.progressBg, GuiBase.progressFgRed);
				GUI.Label(new Rect(GuiBase.Width(230f), GuiBase.Height((float)(marginTop + 3)), GuiBase.Width(110f), GuiBase.Height(30f)), current.LevelText, labelStyle);
				GUI.Label(new Rect(GuiBase.Width(340f), GuiBase.Height((float)(marginTop + 3)), GuiBase.Width(100f), GuiBase.Height(30f)), string.Empty + current.ShadowCloneCount, labelStyle);
				if (current.IsAvailable)
				{
					if (this.updateTime == 60 && current.ShadowCloneCount > 0)
					{
						this.totalBattleGain += current.PowerGainInSec(current.ShadowCloneCount);
					}
					if (GUI.Button(new Rect(GuiBase.Width(450f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+"))
					{
						if (Event.current.button == 1)
						{
							current.AddCloneCount(34);
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
						int num = ((167 * current.MaxHealth - 1 + current.Defense) / (App.State.CloneAttack + 1)).ToNextInt();
						if (num <= 0)
						{
							num = 1;
						}
						else if (num > App.State.Clones.IdleClones() + current.ShadowCloneCount)
						{
							num = App.State.Clones.IdleClones() + current.ShadowCloneCount.ToInt();
						}
						current.RemoveCloneCount(current.ShadowCloneCount);
						current.AddCloneCount(num);
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
			FightingUi.scrollBarsToZero = true;
		}
	}
}
