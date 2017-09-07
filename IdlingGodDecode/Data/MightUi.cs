using Assets.Scripts.Data;
using Assets.Scripts.Helper;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class MightUi : AreaRightUi
	{
		private static MightUi Instance = new MightUi();

		private static bool scrollBarsToZero = false;

		public static void Show()
		{
			MightUi.Instance.IsAchievement = false;
			MightUi.Instance.Show(true);
		}

		public override bool Init()
		{
			this.scrollViewHeight = 790;
			return false;
		}

		protected override void ShowLabels(int marginTop, GUIStyle labelStyle)
		{
			this.SyncScrollbars = false;
			labelStyle.alignment = TextAnchor.MiddleLeft;
			labelStyle.fontStyle = FontStyle.Normal;
			labelStyle.fontSize = GuiBase.FontSize(16);
			string text = "Every 4 Might will increase your Physical, Mystic, Battle and Creating by 1%. \nYou will gain 1 Might for each level gained in this tab.\nTotal Might will not reset on rebirth but you will have to unlock the tab again.\nThe levels below will reset after rebirth.";
			if (App.State.Statistic.OnekChallengesFinished > 0)
			{
				int num = App.State.Statistic.OnekChallengesFinished.ToInt();
				if (num > 40)
				{
					num = 40;
				}
				text = string.Concat(new object[]
				{
					text,
					"\nSpeed multiplier: ",
					num * 5,
					" %"
				});
			}
			GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)(marginTop - 48)), GuiBase.Width(400f), GuiBase.Height(30f)), new GUIContent(string.Concat(new object[]
			{
				"Total Might: ",
				App.State.PremiumBoni.TotalMight,
				" (",
				App.State.PremiumBoni.TotalMight / 4L,
				"%)"
			}), text));
			marginTop += 5;
			labelStyle.fontStyle = FontStyle.Bold;
			labelStyle.alignment = TextAnchor.UpperCenter;
			labelStyle.fontSize = GuiBase.FontSize(18);
			GUI.Label(new Rect(GuiBase.Width(15f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), "Might");
			GUI.Label(new Rect(GuiBase.Width(220f), GuiBase.Height((float)marginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Level", labelStyle);
			GUI.Label(new Rect(GuiBase.Width(290f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Next At", "If the level of the might is at least as much as the count in the input-field, the clones will move to the next one. '0' is not limited."), labelStyle);
			GUI.Label(new Rect(GuiBase.Width(380f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), "Clones", labelStyle);
			labelStyle.fontSize = GuiBase.FontSize(16);
		}

		protected override void ShowScrollViewElements(int marginTop, GUIStyle labelStyle)
		{
			if (MightUi.scrollBarsToZero)
			{
				base.SetScrollbarPosition(Vector2.zero);
				MightUi.scrollBarsToZero = false;
			}
			foreach (Might current in App.State.AllMights)
			{
				if (current.ShadowCloneCount > 0)
				{
					current.ShouldUpdateText = true;
				}
				if (current.TypeEnum == Might.MightType.focused_breathing)
				{
					marginTop += 35;
					labelStyle.alignment = TextAnchor.UpperCenter;
					labelStyle.fontStyle = FontStyle.Bold;
					labelStyle.fontSize = GuiBase.FontSize(18);
					GUI.Label(new Rect(GuiBase.Width(15f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), "Usable Skills");
					labelStyle.fontStyle = FontStyle.Normal;
					marginTop += 35;
				}
				labelStyle.fontSize = GuiBase.FontSize(16);
				GuiBase.CreateProgressBar(marginTop, current.getPercent(), current.Name, current.Description, GuiBase.progressBg, GuiBase.progressFgBlue);
				GUI.Label(new Rect(GuiBase.Width(210f), GuiBase.Height((float)marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), current.Level.CommaFormatted);
				GUI.Label(new Rect(GuiBase.Width(380f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), current.ShadowCloneCount.CommaFormatted);
				if (App.CurrentPlattform == Plattform.Android)
				{
					GUIStyle textField = Gui.ChosenSkin.textField;
					if (GUI.Button(new Rect(GuiBase.Width(315f), GuiBase.Height((float)marginTop), GuiBase.Width(55f), GuiBase.Height(25f)), current.NextAt.ToString(), textField))
					{
						Might migh = current;
						base.ShowNumberInput("Next at for " + migh.Name, migh.NextAt, 2147483647, delegate(CDouble x)
						{
							migh.NextAt = x.ToInt();
						});
					}
				}
				else
				{
					int num = 0;
					string s = GUI.TextField(new Rect(GuiBase.Width(315f), GuiBase.Height((float)marginTop), GuiBase.Width(55f), GuiBase.Height(25f)), current.NextAt.ToString());
					int.TryParse(s, out num);
					if (num >= 0)
					{
						current.NextAt = num;
					}
				}
				if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+"))
				{
					current.AddCloneCount(App.State.GameSettings.ClonesToAddCount);
				}
				if (GUI.Button(new Rect(GuiBase.Width(560f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "-"))
				{
					current.RemoveCloneCount(App.State.GameSettings.ClonesToAddCount);
				}
				if (current.IsUsable)
				{
					marginTop += 35;
					if (current.DurationLeft > 0L)
					{
						float num2 = GuiBase.Width(185f);
						GUI.BeginGroup(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), num2, GuiBase.Height(25f)));
						GUI.DrawTexture(new Rect(0f, 0f, num2, GuiBase.Height(25f)), GuiBase.progressBg);
						float num3 = (float)current.DurationLeft / ((float)current.UseDuration * 1000f);
						float width = num3 * num2;
						float x2 = 0f;
						GUI.DrawTexture(new Rect(x2, 0f, width, GuiBase.Height(25f)), GuiBase.progressFgGreen);
						GUI.EndGroup();
					}
					else if (current.UseCoolDown > 0L)
					{
						double percent = (double)current.UseCoolDown / 3600000.0;
						GuiBase.CreateProgressBar(marginTop, percent, Conv.MsToGuiText(current.UseCoolDown, true), current.UnleashDesc + "\n\nYou can use it again when timer hits zero.", GuiBase.progressBg, GuiBase.progressFgRed);
					}
					else if (GUI.Button(new Rect(GuiBase.Width(60f), GuiBase.Height((float)marginTop), GuiBase.Width(120f), GuiBase.Height(30f)), new GUIContent("Unleash", current.UnleashDesc)))
					{
						if (SpecialFightUi.IsFighting)
						{
							GuiBase.ShowToast("Please finish your special fight first!");
						}
						else
						{
							current.Unleash();
						}
					}
				}
				marginTop += 35;
			}
		}

		internal static void ScrollbarToZero()
		{
			MightUi.scrollBarsToZero = true;
		}
	}
}
