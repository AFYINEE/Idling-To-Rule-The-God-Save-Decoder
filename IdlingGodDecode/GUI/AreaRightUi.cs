using Assets.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class AreaRightUi : GuiBase
	{
		protected bool IsAchievement;

		protected static Vector2 scrollPositionStatic = Vector2.zero;

		protected Vector2 scrollPosition = Vector2.zero;

		protected int scrollViewHeight;

		public static bool IsCloneCountInitialized = false;

		protected bool SyncScrollbars;

		private static string cloneCountString = "1";

		public static string CloneCountString
		{
			get
			{
				return AreaRightUi.cloneCountString;
			}
			set
			{
				try
				{
					if (!string.IsNullOrEmpty(value) && value.StartsWith("-"))
					{
						value = string.Empty;
					}
					int num = 0;
					int.TryParse(value, out num);
					AreaRightUi.cloneCountString = num.ToString();
					if (num < App.State.Clones.MaxShadowClones)
					{
						App.State.GameSettings.ClonesToAddCount = num;
					}
					else
					{
						App.State.GameSettings.ClonesToAddCount = App.State.Clones.MaxShadowClones.ToInt();
						AreaRightUi.cloneCountString = App.State.GameSettings.ClonesToAddCount.ToString();
					}
				}
				catch (Exception)
				{
					AreaRightUi.cloneCountString = string.Empty;
					App.State.GameSettings.ClonesToAddCount = 1;
				}
			}
		}

		protected void SetScrollbarPosition(Vector2 position)
		{
			if (App.State.GameSettings.SyncScrollbars && this.SyncScrollbars)
			{
				AreaRightUi.scrollPositionStatic = position;
			}
			else
			{
				this.scrollPosition = position;
			}
		}

		private void SetCountAfterButton(int count)
		{
			int num;
			if (Event.current.button == 1)
			{
				num = App.State.GameSettings.ClonesToAddCount - count;
			}
			else
			{
				num = count;
			}
			if (num <= 0)
			{
				num = 1;
			}
			AreaRightUi.CloneCountString = num.ToString();
		}

		protected void Show(bool syncScrollbars)
		{
			if (!AreaRightUi.IsCloneCountInitialized)
			{
				AreaRightUi.cloneCountString = App.State.GameSettings.ClonesToAddCount.ToString();
				AreaRightUi.IsCloneCountInitialized = true;
			}
			if (this.Init())
			{
				return;
			}
			this.SyncScrollbars = syncScrollbars;
			GUI.BeginGroup(new Rect(GuiBase.Width(280f), GuiBase.Height(110f), GuiBase.Width(670f), GuiBase.Height(480f)));
			GUI.Box(new Rect(GuiBase.Width(10f), GuiBase.Height(0f), GuiBase.Width(660f), GuiBase.Height(80f)), string.Empty);
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = GuiBase.FontSize(16);
			GUIStyle style2 = GUI.skin.GetStyle("TextField");
			style2.fontSize = GuiBase.FontSize(14);
			style2.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height(11f), GuiBase.Width(220f), GuiBase.Height(30f)), new GUIContent("Clones to add / remove", "Click on the buttons for standard values or in the input field, then input the number of clones each click on + / - should add / remove."));
			if (App.CurrentPlattform == Plattform.Android)
			{
				GUIStyle textField = Gui.ChosenSkin.textField;
				if (GUI.Button(new Rect(GuiBase.Width(210f), GuiBase.Height(10f), GuiBase.Width(65f), GuiBase.Height(25f)), AreaRightUi.CloneCountString, textField))
				{
					base.ShowNumberInput("Clones to add / remove", App.State.GameSettings.ClonesToAddCount, App.State.Clones.MaxShadowClones, delegate(CDouble x)
					{
						AreaRightUi.CloneCountString = x.ToString();
					});
				}
			}
			else
			{
				AreaRightUi.CloneCountString = GUI.TextField(new Rect(GuiBase.Width(210f), GuiBase.Height(10f), GuiBase.Width(65f), GuiBase.Height(25f)), AreaRightUi.CloneCountString, style2);
			}
			if (GUI.Button(new Rect(GuiBase.Width(290f), GuiBase.Height(10f), GuiBase.Width(45f), GuiBase.Height(25f)), "1"))
			{
				this.SetCountAfterButton(1);
			}
			if (GUI.Button(new Rect(GuiBase.Width(340f), GuiBase.Height(10f), GuiBase.Width(45f), GuiBase.Height(25f)), "10"))
			{
				this.SetCountAfterButton(10);
			}
			if (GUI.Button(new Rect(GuiBase.Width(390f), GuiBase.Height(10f), GuiBase.Width(45f), GuiBase.Height(25f)), "100"))
			{
				this.SetCountAfterButton(100);
			}
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(10f), GuiBase.Width(45f), GuiBase.Height(25f)), "1K"))
			{
				this.SetCountAfterButton(1000);
			}
			if (GUI.Button(new Rect(GuiBase.Width(490f), GuiBase.Height(10f), GuiBase.Width(45f), GuiBase.Height(25f)), "10K"))
			{
				this.SetCountAfterButton(10000);
			}
			if (GUI.Button(new Rect(GuiBase.Width(540f), GuiBase.Height(10f), GuiBase.Width(50f), GuiBase.Height(25f)), "100K"))
			{
				this.SetCountAfterButton(100000);
			}
			if (GUI.Button(new Rect(GuiBase.Width(595f), GuiBase.Height(10f), GuiBase.Width(53f), GuiBase.Height(25f)), "MAX"))
			{
				AreaRightUi.CloneCountString = App.State.Clones.MaxShadowClones.ToString();
			}
			GUI.Box(new Rect(GuiBase.Width(10f), GuiBase.Height(85f), GuiBase.Width(660f), GuiBase.Height(395f)), string.Empty);
			int num = 90;
			style.alignment = TextAnchor.UpperCenter;
			style.fontStyle = FontStyle.Bold;
			this.ShowLabels(num, style);
			style.fontStyle = FontStyle.Normal;
			num += 45;
			if (App.State.GameSettings.SyncScrollbars && this.SyncScrollbars)
			{
				AreaRightUi.scrollPositionStatic = GuiBase.TouchScrollView(new Rect(GuiBase.Width(0f), GuiBase.Height((float)num), GuiBase.Width(650f), GuiBase.Height(330f)), AreaRightUi.scrollPositionStatic, new Rect(0f, GuiBase.Height((float)num), GuiBase.Width(620f), GuiBase.Height((float)this.scrollViewHeight)));
			}
			else
			{
				this.scrollPosition = GuiBase.TouchScrollView(new Rect(GuiBase.Width(0f), GuiBase.Height((float)num), GuiBase.Width(650f), GuiBase.Height(330f)), this.scrollPosition, new Rect(0f, GuiBase.Height((float)num), GuiBase.Width(620f), GuiBase.Height((float)this.scrollViewHeight)));
			}
			this.ShowScrollViewElements(num, style);
			GUI.EndScrollView();
			GUI.EndGroup();
		}

		public virtual bool Init()
		{
			return this.IsAchievement;
		}

		protected virtual void ShowLabels(int marginTop, GUIStyle labelStyle)
		{
		}

		protected virtual void ShowScrollViewElements(int marginTop, GUIStyle labelStyle)
		{
		}
	}
}
