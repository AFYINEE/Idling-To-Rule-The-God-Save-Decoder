using Assets.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class MonumentUi : AreaRightUi
	{
		public static MonumentUi Instance = new MonumentUi();

		private static bool scrollBarsToZero = false;

		public new void Show(bool isAchievement)
		{
			if (!App.State.IsMonumentUnlocked)
			{
				GUI.Box(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
				GUIStyle style = GUI.skin.GetStyle("Label");
				style.fontSize = GuiBase.FontSize(16);
				style.alignment = TextAnchor.MiddleCenter;
				GUI.Label(new Rect(GuiBase.Width(310f), GuiBase.Height(150f), GuiBase.Width(640f), GuiBase.Height(30f)), "You need to defeat Diana to unlock monuments.", style);
				return;
			}
			this.IsAchievement = isAchievement;
			base.Show(false);
		}

		public override bool Init()
		{
			if (App.State.IsUpgradeUnlocked)
			{
				this.scrollViewHeight = 650;
			}
			return false;
		}

		protected override void ShowLabels(int marginTop, GUIStyle labelStyle)
		{
			labelStyle.alignment = TextAnchor.MiddleLeft;
			labelStyle.fontStyle = FontStyle.Normal;
			labelStyle.fontSize = GuiBase.FontSize(16);
			GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height(40f), GuiBase.Width(200f), GuiBase.Height(30f)), new GUIContent("Stop after finish", "If this is on, clones will be removed from monuments when it is finished. This will ignore the number in 'Stop at'."));
			App.State.GameSettings.StopMonumentBuilding = GUI.Toggle(new Rect(GuiBase.Width(200f), GuiBase.Height(47f), GuiBase.Width(300f), GuiBase.Height(30f)), App.State.GameSettings.StopMonumentBuilding, new GUIContent(string.Empty));
			if (App.State.IsBuyUnlocked)
			{
				string text = "If this is on, missing creations will be bought automatically if you have enough divinity.";
				if (App.State.PremiumBoni.AutoBuyCostReduction < 20)
				{
					text = string.Concat(new object[]
					{
						text,
						"\nBut beware: there is an additional ",
						20 - App.State.PremiumBoni.AutoBuyCostReduction,
						"% transaction fee!"
					});
				}
				GUI.Label(new Rect(GuiBase.Width(365f), GuiBase.Height(40f), GuiBase.Width(220f), GuiBase.Height(30f)), new GUIContent("Auto buy missing creations", text));
				App.State.GameSettings.AutoBuyCreationsForMonuments = GUI.Toggle(new Rect(GuiBase.Width(593f), GuiBase.Height(47f), GuiBase.Width(300f), GuiBase.Height(30f)), App.State.GameSettings.AutoBuyCreationsForMonuments, new GUIContent(string.Empty));
			}
			labelStyle.alignment = TextAnchor.UpperCenter;
			labelStyle.fontStyle = FontStyle.Bold;
			labelStyle.fontSize = GuiBase.FontSize(18);
			marginTop += 5;
			GUI.Label(new Rect(GuiBase.Width(15f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), new GUIContent("Monument", "All monuments share the same multiplier. That means, if one monument multiplies a stat with 1 + 200 and another one multiplies it with 1 + 400, it would be a total multiplier of 601."));
			GUI.Label(new Rect(GuiBase.Width(220f), GuiBase.Height((float)marginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Count", labelStyle);
			GUI.Label(new Rect(GuiBase.Width(290f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Next At", "If your monument count is at least as much as the count in the input-field, the clones will be moved to the next monument if the next monument has neither 0 as an input nor the level has reached that number. '0' is not limited."), labelStyle);
			GUI.Label(new Rect(GuiBase.Width(380f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), "Clones", labelStyle);
			labelStyle.fontSize = GuiBase.FontSize(16);
		}

		protected override void ShowScrollViewElements(int marginTop, GUIStyle labelStyle)
		{
			if (MonumentUi.scrollBarsToZero)
			{
				base.SetScrollbarPosition(Vector2.zero);
				MonumentUi.scrollBarsToZero = false;
			}
			foreach (Monument current in App.State.AllMonuments)
			{
				labelStyle.fontSize = GuiBase.FontSize(16);
				GuiBase.CreateProgressBar(marginTop, current.getPercent(), current.Name, current.Description + current.MissingItems, GuiBase.progressBg, GuiBase.progressFgBlue);
				labelStyle.fontSize = GuiBase.FontSize(18);
				GUI.Label(new Rect(GuiBase.Width(210f), GuiBase.Height((float)marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), current.Level.CommaFormatted, labelStyle);
				GUI.Label(new Rect(GuiBase.Width(380f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), current.ShadowCloneCount.CommaFormatted, labelStyle);
				if (current.IsAvailable)
				{
					if (current.ShadowCloneCount > 0)
					{
						current.ShouldUpdateText = true;
					}
					if (App.CurrentPlattform == Plattform.Android)
					{
						GUIStyle textField = Gui.ChosenSkin.textField;
						if (GUI.Button(new Rect(GuiBase.Width(315f), GuiBase.Height((float)marginTop), GuiBase.Width(55f), GuiBase.Height(25f)), current.StopAt + string.Empty, textField))
						{
							Monument monu = current;
							base.ShowNumberInput("Next at for " + current.Name, current.StopAt, 2147483647, delegate(CDouble x)
							{
								monu.StopAt = x.ToInt();
							});
						}
					}
					else
					{
						current.StopAtString = GUI.TextField(new Rect(GuiBase.Width(315f), GuiBase.Height((float)marginTop), GuiBase.Width(55f), GuiBase.Height(25f)), current.StopAtString);
					}
					if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+"))
					{
						current.AddCloneCount(App.State.GameSettings.ClonesToAddCount);
					}
					if (GUI.Button(new Rect(GuiBase.Width(560f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "-"))
					{
						current.RemoveCloneCount(App.State.GameSettings.ClonesToAddCount);
					}
					if (current.Upgrade.IsAvailable)
					{
						if (current.Upgrade.ShadowCloneCount > 0)
						{
							current.Upgrade.ShouldUpdateText = true;
						}
						marginTop += 35;
						labelStyle.fontSize = GuiBase.FontSize(16);
						GuiBase.CreateProgressBar(marginTop, current.Upgrade.getPercent(), current.Upgrade.Name, current.Upgrade.Description + current.Upgrade.MissingItems, GuiBase.progressBg, GuiBase.progressFgGreen);
						labelStyle.fontSize = GuiBase.FontSize(18);
						GUI.Label(new Rect(GuiBase.Width(210f), GuiBase.Height((float)marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), current.Upgrade.Level.CommaFormatted, labelStyle);
						GUI.Label(new Rect(GuiBase.Width(380f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), current.Upgrade.ShadowCloneCount.CommaFormatted, labelStyle);
						if (App.CurrentPlattform == Plattform.Android)
						{
							Monument monu = current;
							GUIStyle textField2 = Gui.ChosenSkin.textField;
							if (GUI.Button(new Rect(GuiBase.Width(315f), GuiBase.Height((float)marginTop), GuiBase.Width(55f), GuiBase.Height(25f)), monu.Upgrade.StopAt + string.Empty, textField2))
							{
								base.ShowNumberInput("Stop at for " + monu.Name + " " + monu.Upgrade.Name, monu.Upgrade.StopAt, 2147483647, delegate(CDouble x)
								{
									monu.Upgrade.StopAt = x.ToInt();
								});
							}
						}
						else
						{
							current.Upgrade.StopAtString = GUI.TextField(new Rect(GuiBase.Width(315f), GuiBase.Height((float)marginTop), GuiBase.Width(55f), GuiBase.Height(25f)), current.Upgrade.StopAtString);
						}
						if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+"))
						{
							current.Upgrade.AddCloneCount(App.State.GameSettings.ClonesToAddCount);
						}
						if (GUI.Button(new Rect(GuiBase.Width(560f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "-"))
						{
							current.Upgrade.RemoveCloneCount(App.State.GameSettings.ClonesToAddCount);
						}
						marginTop += 10;
					}
				}
				marginTop += 35;
			}
		}

		internal static void ScrollbarToZero()
		{
			MonumentUi.scrollBarsToZero = true;
		}
	}
}
