using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class GuiBase
	{
		protected static Texture2D Logo;

		protected static Texture2D progressBg;

		protected static Texture2D progressFgRed;

		protected static Texture2D progressFgBlue;

		protected static Texture2D progressFgGreen;

		protected static Texture2D progressFgBlueRotated;

		protected static Texture2D progressBgRotated;

		private static float yposMouse = 0f;

		private static bool mouseTouchStarted = false;

		private static int scrollFinger = -1;

		private static long toasttimer = 0L;

		private static bool toastIsShowing = false;

		private static string toastTest = string.Empty;

		protected static Color FontColorProgressbar = Color.white;

		private static Vector2 scrollPosition = Vector2.zero;

		public static string SecondMessage = string.Empty;

		protected static bool BigMessageIsShowing = false;

		protected static string BigMessageText = string.Empty;

		private static Vector2 BigMessageScrollBar = Vector2.zero;

		private static long achievementShowTimer = 0L;

		private static Achievement achievementToShow = null;

		private static string unlockedContent = string.Empty;

		public static bool FullScreenDialogIsShowing = false;

		public static bool LeftDialogIsShowing = false;

		protected static Action leftAction = null;

		protected static Action rightAction = null;

		protected static string leftDialogHeader = string.Empty;

		protected static string leftDialogText = string.Empty;

		protected static string lButtonText = string.Empty;

		protected static string rButtonText = string.Empty;

		protected static Action<CDouble> NumberInputAction = null;

		private static string NumberInputText = string.Empty;

		private static CDouble NumberInputMaxNumber = 0;

		private static CDouble NumberInputNumber = 0;

		public static void InitImages()
		{
			GuiBase.Logo = (Texture2D)Resources.Load("logo", typeof(Texture2D));
			int num = 0;
			if (App.State != null)
			{
				num = App.State.GameSettings.ProgressbarType;
			}
			if (num == 0)
			{
				GuiBase.progressBg = (Texture2D)Resources.Load("Gui/progress_bg", typeof(Texture2D));
				GuiBase.progressFgBlue = (Texture2D)Resources.Load("Gui/progress_fg", typeof(Texture2D));
				GuiBase.progressFgRed = (Texture2D)Resources.Load("Gui/progress_fg_red", typeof(Texture2D));
				GuiBase.progressFgGreen = (Texture2D)Resources.Load("Gui/progress_fg_green", typeof(Texture2D));
				GuiBase.progressBgRotated = (Texture2D)Resources.Load("Gui/progress_bg_rotated", typeof(Texture2D));
				GuiBase.progressFgBlueRotated = (Texture2D)Resources.Load("Gui/progress_fg_rotated", typeof(Texture2D));
			}
			else if (num == 1)
			{
				GuiBase.progressBg = (Texture2D)Resources.Load("Gui/progress_bg_simple", typeof(Texture2D));
				GuiBase.progressFgBlue = (Texture2D)Resources.Load("Gui/progress_fg_simple", typeof(Texture2D));
				GuiBase.progressFgRed = (Texture2D)Resources.Load("Gui/progress_fg_red_simple", typeof(Texture2D));
				GuiBase.progressFgGreen = (Texture2D)Resources.Load("Gui/progress_fg_green_simple", typeof(Texture2D));
				GuiBase.progressBgRotated = (Texture2D)Resources.Load("Gui/progress_bg_rotated_simple", typeof(Texture2D));
				GuiBase.progressFgBlueRotated = (Texture2D)Resources.Load("Gui/progress_fg_rotated_simple", typeof(Texture2D));
			}
		}

		public static float Height(float px)
		{
			return px * App.HeightMulti;
		}

		public static float Width(float px)
		{
			return px * App.WidthMulti;
		}

		public static int FontSize(int px)
		{
			return (int)((float)px * App.HeightMulti);
		}

		public static Vector2 TouchScrollView(Rect aScreenRect, Vector2 aScrollPos, Rect aContentRect)
		{
			aScrollPos = GUI.BeginScrollView(aScreenRect, aScrollPos, aContentRect);
			return aScrollPos;
		}

		public static void ShowToast(string text)
		{
			GuiBase.toastIsShowing = true;
			GuiBase.toastTest = text;
			GuiBase.toasttimer = DateTime.Now.Ticks / 10000L;
		}

		protected void showToast()
		{
			if (!GuiBase.toastIsShowing)
			{
				return;
			}
			long num = DateTime.Now.Ticks / 10000L;
			long num2 = num - GuiBase.toasttimer;
			if (num2 < 2500L)
			{
				if (App.State.GameSettings.ShowToolTipsOnRightClick)
				{
					GuiBase.ShowTextBoxOnTop(GuiBase.toastTest);
				}
				else
				{
					GuiBase.ShowTextBox(GuiBase.toastTest, false);
				}
			}
			else
			{
				GuiBase.toastIsShowing = false;
			}
		}

		private static void ShowTextBox(string textToShow, bool isFromInfoArea = false)
		{
			if (!string.IsNullOrEmpty(textToShow))
			{
				GUIStyle style = GUI.skin.GetStyle("Label");
				style.fontSize = GuiBase.FontSize(15);
				style.alignment = TextAnchor.UpperLeft;
				float x = Input.mousePosition.x;
				float num = (float)Screen.height - Input.mousePosition.y;
				Vector2 vector = new Vector2(x, num);
				float num2 = GuiBase.Width(275f);
				float width = GuiBase.Width(260f);
				string text = string.Empty;
				string text2 = string.Empty;
				string[] array = textToShow.Split(new string[]
				{
					"lowerText"
				}, StringSplitOptions.None);
				if (array.Length != 2)
				{
					text = textToShow;
				}
				else
				{
					text = array[0];
					text2 = array[1];
				}
				float num3 = style.CalcHeight(new GUIContent(text), width) + GuiBase.Height(10f);
				float num4 = 0f;
				if (!string.IsNullOrEmpty(text2))
				{
					num4 = style.CalcHeight(new GUIContent(text2), width) + GuiBase.Height(10f);
				}
				float num5 = vector.x - num2 - 5f;
				float num6 = vector.y - num3 - num4 + GuiBase.Height(20f);
				float num7 = num2;
				if (x < num7 || AfkUi.Instance.ShowAfk)
				{
					num5 = vector.x + GuiBase.Width(20f);
				}
				if (num < num3 + num4)
				{
					num6 = vector.y - GuiBase.Height(10f);
				}
				if (isFromInfoArea)
				{
					num5 = GuiBase.Width(10f);
					num6 -= GuiBase.Height(30f);
				}
				GUI.Box(new Rect(num5, num6, num2, num3), string.Empty);
				GUI.Label(new Rect(num5 + GuiBase.Width(10f), num6 + GuiBase.Height(5f), width, num3 - GuiBase.Height(10f)), text);
				if (!string.IsNullOrEmpty(text2))
				{
					style.fontSize = GuiBase.FontSize(13);
					num4 = style.CalcHeight(new GUIContent(text2), width) + GuiBase.Height(10f);
					GUI.Box(new Rect(num5, num6 + num3, num2, num4), string.Empty);
					GUI.Label(new Rect(num5 + GuiBase.Width(10f), num6 + num3 + GuiBase.Height(5f), width, num4), text2);
				}
			}
		}

		public static void ShowToolTip(string infotext = "")
		{
			if (Event.current.button != 1 && App.State.GameSettings.ShowToolTipsOnRightClick)
			{
				return;
			}
			if (GuiBase.toastIsShowing && !App.State.GameSettings.ShowToolTipsOnRightClick)
			{
				return;
			}
			string text = GUI.tooltip;
			if (!string.IsNullOrEmpty(infotext))
			{
				text = infotext;
			}
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (App.State.GameSettings.ShowToolTipsOnTop && string.IsNullOrEmpty(infotext))
			{
				GuiBase.ShowTextBoxOnTop(text);
			}
			else
			{
				GuiBase.ShowTextBox(text, !string.IsNullOrEmpty(infotext));
			}
		}

		private static void ShowTextBoxOnTop(string infotext)
		{
			GUI.Box(new Rect(GuiBase.Width(290f), GuiBase.Height(10f), GuiBase.Width(660f), GuiBase.Height(95f)), string.Empty);
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.fontSize = GuiBase.FontSize(12);
			style.alignment = TextAnchor.UpperLeft;
			int num = 12;
			int num2 = 300;
			int num3 = 650;
			string[] array = infotext.Split(new string[]
			{
				"lowerText"
			}, StringSplitOptions.None);
			string text = string.Empty;
			if (array.Length > 0)
			{
				text = array[0];
			}
			if (array.Length == 2)
			{
				text = text + "\n" + array[1].Replace("\n", " - ");
				if (text.Contains(" - Defense") || text.Contains("Clones) - "))
				{
					text = text.Replace(" - Defense", "\nDefense:");
					text = text.Replace("Clones) - ", "Clones)\n");
				}
			}
			GUI.Label(new Rect(GuiBase.Width((float)num2), GuiBase.Height((float)num), GuiBase.Width((float)num3), GuiBase.Height(110f)), text);
		}

		public static void CreateBossHPBar(int marginLeft, int marginTop, float width, float height, double percent, string name)
		{
			GuiBase.CreateProgressBar(marginLeft, marginTop, width, height, percent, name, string.Empty, GuiBase.progressBg, GuiBase.progressFgRed);
		}

		public static void CreateProgressBar(int marginLeft, int marginTop, float width, float height, double percent, string name, string mouseover, Texture2D bgImage, Texture2D fgImage)
		{
			float num = GuiBase.Width((float)marginLeft);
			float num2 = GuiBase.Height((float)marginTop);
			width = GuiBase.Width(width);
			height = GuiBase.Height(height - 5f);
			GUI.DrawTexture(new Rect(num, num2, width, height), bgImage);
			GUI.DrawTexture(new Rect(num + GuiBase.Width(1f), num2, (float)(percent * (double)(width - GuiBase.Width(1f))), height - GuiBase.Height(0f)), fgImage);
			GUIStyle style = GUI.skin.GetStyle("Label");
			GUIStyle gUIStyle = new GUIStyle();
			gUIStyle.alignment = TextAnchor.MiddleCenter;
			gUIStyle.fontSize = style.fontSize;
			gUIStyle.normal.textColor = GuiBase.FontColorProgressbar;
			GUI.BeginGroup(new Rect(num, num2 - 2f, width, height));
			GUI.Label(new Rect(GuiBase.Width(0f), GuiBase.Height(0f), width, height), new GUIContent(name, mouseover), gUIStyle);
			GUI.EndGroup();
		}

		public static void CreateProgressBar(int marginTop, double percent, string name, string mouseover, Texture2D bgImage, Texture2D fgImage)
		{
			GuiBase.CreateProgressBar(30, marginTop, 185f, 31f, percent, name, mouseover, bgImage, fgImage);
		}

		public static void ShowAchievements(List<Achievement> achievements, string typeName)
		{
			if (!typeName.Equals("creating"))
			{
				achievements = (from o in achievements
				orderby o.CountNeeded.Double
				select o).ToList<Achievement>();
			}
			GUIStyle style = GUI.skin.GetStyle("Label");
			GUI.BeginGroup(new Rect(GuiBase.Width(290f), GuiBase.Height(100f), GuiBase.Width(660f), GuiBase.Height(490f)));
			style.fontSize = GuiBase.FontSize(18);
			int num = 0;
			foreach (Achievement current in achievements)
			{
				if (current.Reached)
				{
					num++;
				}
			}
			GUI.Box(new Rect(GuiBase.Width(0f), GuiBase.Height(10f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			style.fontStyle = FontStyle.Bold;
			style.alignment = TextAnchor.UpperCenter;
			string tooltip = string.Empty;
			if (App.State.Statistic.AchievementChallengesFinished > 0)
			{
				int num2 = App.State.Statistic.AchievementChallengesFinished.ToInt();
				if (num2 > 50)
				{
					num2 = 50;
				}
				tooltip = "Additional multiplier from Achievement Challenges: " + num2 + " %";
			}
			GUI.Label(new Rect(GuiBase.Width(0f), GuiBase.Height(30f), GuiBase.Width(650f), GuiBase.Height(60f)), new GUIContent(string.Concat(new object[]
			{
				"Achievements for ",
				typeName,
				" ( ",
				num,
				" / ",
				achievements.Count,
				")"
			}), tooltip));
			style.alignment = TextAnchor.UpperLeft;
			style.fontStyle = FontStyle.Normal;
			int num3 = 85;
			int num4 = 30;
			int num5 = 90 + achievements.Count * 65 / 9;
			GuiBase.scrollPosition = GuiBase.TouchScrollView(new Rect(GuiBase.Width(0f), GuiBase.Height((float)num3), GuiBase.Width(640f), GuiBase.Height(390f)), GuiBase.scrollPosition, new Rect(0f, GuiBase.Height((float)num3), GuiBase.Width(600f), GuiBase.Height((float)num5)));
			int num6 = 0;
			foreach (Achievement current2 in achievements)
			{
				num6++;
				string tooltip2 = current2.NonReachedName;
				if (current2.ShowRealName)
				{
					tooltip2 = current2.Description;
				}
				if (current2.Reached)
				{
					GUI.Label(new Rect(GuiBase.Width((float)num4), GuiBase.Height((float)num3), GuiBase.Width(60f), GuiBase.Height(60f)), new GUIContent(string.Empty, current2.ImageReached, tooltip2));
				}
				else
				{
					GUI.Label(new Rect(GuiBase.Width((float)num4), GuiBase.Height((float)num3), GuiBase.Width(60f), GuiBase.Height(60f)), new GUIContent(string.Empty, current2.Image, tooltip2));
				}
				if (num6 % 9 == 0)
				{
					num3 += 65;
					num4 = 30;
				}
				else
				{
					num4 += 65;
				}
			}
			GUI.EndScrollView();
			GUI.EndGroup();
		}

		public static void ShowBigMessage()
		{
			if (!string.IsNullOrEmpty(GuiBase.BigMessageText))
			{
				GUIStyle style = GUI.skin.GetStyle("Label");
				style.fontSize = GuiBase.FontSize(16);
				style.alignment = TextAnchor.UpperLeft;
				int num = (int)style.CalcHeight(new GUIContent(GuiBase.BigMessageText), GuiBase.Width(500f));
				if (num < 320)
				{
					num = 320;
				}
				GUI.BeginGroup(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
				GUI.Box(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), string.Empty);
				GUI.Label(new Rect(GuiBase.Width(350f), GuiBase.Height(20f), GuiBase.Width(400f), GuiBase.Height(40f)), GuiBase.Logo);
				GuiBase.BigMessageScrollBar = GuiBase.TouchScrollView(new Rect(GuiBase.Width(120f), GuiBase.Height(100f), GuiBase.Width(650f), GuiBase.Height(400f)), GuiBase.BigMessageScrollBar, new Rect(GuiBase.Width(120f), GuiBase.Height(100f), GuiBase.Width(620f), (float)num));
				GUI.Label(new Rect(GuiBase.Width(240f), GuiBase.Height(100f), GuiBase.Width(500f), GuiBase.Height((float)num)), GuiBase.BigMessageText);
				GUI.EndScrollView();
				if (GUI.Button(new Rect(GuiBase.Width(415f), GuiBase.Height(520f), GuiBase.Width(100f), GuiBase.Height(30f)), "Close"))
				{
					if (string.IsNullOrEmpty(GuiBase.SecondMessage))
					{
						GuiBase.BigMessageIsShowing = false;
						GuiBase.BigMessageText = string.Empty;
					}
					else
					{
						GuiBase.BigMessageText = GuiBase.SecondMessage;
						GuiBase.SecondMessage = string.Empty;
					}
				}
				GUI.EndGroup();
			}
			else
			{
				GuiBase.BigMessageIsShowing = false;
			}
		}

		public static void ShowBigMessage(string textToShow)
		{
			if (!string.IsNullOrEmpty(GuiBase.BigMessageText) && string.IsNullOrEmpty(GuiBase.SecondMessage))
			{
				GuiBase.SecondMessage = textToShow;
			}
			else
			{
				GuiBase.BigMessageText = textToShow;
			}
			GuiBase.BigMessageIsShowing = true;
		}

		public static void ShowAchievementReached(Achievement achievement)
		{
			GuiBase.achievementToShow = achievement;
			GuiBase.achievementShowTimer = DateTime.Now.Ticks / 10000L;
		}

		public static void ShowContentUnlocked(string text)
		{
			GuiBase.unlockedContent = text;
			GuiBase.achievementShowTimer = DateTime.Now.Ticks / 10000L;
		}

		protected void showAchievementReached()
		{
			if ((GuiBase.achievementToShow == null && string.IsNullOrEmpty(GuiBase.unlockedContent)) || !App.State.GameSettings.ShowAchievementPopups)
			{
				return;
			}
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.fontSize = GuiBase.FontSize(16);
			style.alignment = TextAnchor.UpperLeft;
			if (App.State.GameSettings.AchievementsOnTop)
			{
				GUI.BeginGroup(new Rect(GuiBase.Width(540f), GuiBase.Height(10f), GuiBase.Width(410f), GuiBase.Height(240f)));
			}
			else
			{
				GUI.BeginGroup(new Rect(0f, GuiBase.Height(270f), GuiBase.Width(410f), GuiBase.Height(240f)));
			}
			Rect position = new Rect(GuiBase.Width(10f), 0f, GuiBase.Width(400f), GuiBase.Height(120f));
			if (!string.IsNullOrEmpty(GuiBase.unlockedContent))
			{
				GUI.Box(position, "New content unlocked!");
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(35f), GuiBase.Width(360f), GuiBase.Height(80f)), GuiBase.unlockedContent);
			}
			else
			{
				GUI.Box(position, "Achievement reached!");
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(30f), GuiBase.Width(60f), GuiBase.Height(60f)), GuiBase.achievementToShow.ImageReached);
				GUI.Label(new Rect(GuiBase.Width(80f), GuiBase.Height(35f), GuiBase.Width(300f), GuiBase.Height(80f)), GuiBase.achievementToShow.Description);
			}
			long num = DateTime.Now.Ticks / 10000L;
			if (num - GuiBase.achievementShowTimer > 5000L || (position.Contains(Event.current.mousePosition) && Event.current.button == 0 && Event.current.type == EventType.MouseDown))
			{
				GuiBase.achievementToShow = null;
				GuiBase.unlockedContent = string.Empty;
			}
			GUI.EndGroup();
		}

		public static void ShowDialog(string header, string text, Action leftButton, Action rightButton, string leftButtonText = "Yes", string rightButtonText = "No", bool fullScreen = false, bool notInMainThread = false)
		{
			GuiBase.FullScreenDialogIsShowing = fullScreen;
			GuiBase.LeftDialogIsShowing = !fullScreen;
			GuiBase.leftAction = leftButton;
			GuiBase.rightAction = rightButton;
			GuiBase.leftDialogHeader = header;
			GuiBase.leftDialogText = text;
			GuiBase.lButtonText = leftButtonText;
			GuiBase.rButtonText = rightButtonText;
			if (notInMainThread)
			{
				return;
			}
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.fontSize = GuiBase.FontSize(16);
			float width = GuiBase.Width(255f);
			float num = style.CalcHeight(new GUIContent(text), width);
			float num2 = num + GuiBase.Height(100f);
			if (fullScreen)
			{
				GUI.Box(new Rect(GuiBase.Width(0f), GuiBase.Height(0f), (float)Screen.width, (float)Screen.height), string.Empty);
				GUI.BeginGroup(new Rect(GuiBase.Width(342f), (float)(Screen.height / 2) - num2 / 2f, GuiBase.Width(275f), num2));
				GUI.Box(new Rect(0f, 0f, GuiBase.Width(275f), num2), string.Empty);
			}
			else
			{
				GUI.BeginGroup(new Rect(GuiBase.Width(10f), (float)(Screen.height / 2) - num2 / 2f + GuiBase.Height(40f), GuiBase.Width(285f), num2));
				GUI.Box(new Rect(0f, 0f, GuiBase.Width(275f), num2), string.Empty);
			}
			style.fontSize = GuiBase.FontSize(18);
			style.alignment = TextAnchor.MiddleCenter;
			style.fontStyle = FontStyle.Bold;
			GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(5f), GuiBase.Width(255f), GuiBase.Height(50f)), header);
			style.fontSize = GuiBase.FontSize(16);
			style.fontStyle = FontStyle.Normal;
			style.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(50f), width, num), text);
			if (GUI.Button(new Rect(GuiBase.Width(10f), num + GuiBase.Height(60f), GuiBase.Width(125f), GuiBase.Height(30f)), GuiBase.lButtonText))
			{
				GuiBase.FullScreenDialogIsShowing = false;
				GuiBase.LeftDialogIsShowing = false;
				leftButton();
			}
			if (GUI.Button(new Rect(GuiBase.Width(140f), num + GuiBase.Height(60f), GuiBase.Width(125f), GuiBase.Height(30f)), rightButtonText))
			{
				GuiBase.FullScreenDialogIsShowing = false;
				GuiBase.LeftDialogIsShowing = false;
				if (rightButton != null)
				{
					rightButton();
				}
			}
			GUI.EndGroup();
		}

		protected void ShowNumberInput(string calculatorText, CDouble calculatorNumber, CDouble calculatorMaxNumber, Action<CDouble> calculatorAction)
		{
			GuiBase.NumberInputAction = calculatorAction;
			GuiBase.NumberInputText = calculatorText;
			GuiBase.NumberInputMaxNumber = calculatorMaxNumber;
			GuiBase.NumberInputNumber = calculatorNumber;
		}

		protected void ResetNumberInput()
		{
			GuiBase.NumberInputAction = null;
			GuiBase.NumberInputText = string.Empty;
			GuiBase.NumberInputMaxNumber = 0;
			GuiBase.NumberInputNumber = 0;
		}

		protected void AddInputButton(int marginLeft, int marginTop, int number)
		{
			string text = number.ToString();
			if (number == -1)
			{
				text = "Clear";
			}
			else if (number == -2)
			{
				text = "<-";
			}
			if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(60f), GuiBase.Height(30f)), text))
			{
				if (number == -1)
				{
					GuiBase.NumberInputNumber = 0;
				}
				else if (number == -2)
				{
					GuiBase.NumberInputNumber /= 10;
					GuiBase.NumberInputNumber.Floor();
				}
				else
				{
					GuiBase.NumberInputNumber = GuiBase.NumberInputNumber * 10 + number;
					if (GuiBase.NumberInputNumber > GuiBase.NumberInputMaxNumber && GuiBase.NumberInputMaxNumber > 0)
					{
						GuiBase.NumberInputNumber = GuiBase.NumberInputMaxNumber;
					}
				}
			}
		}

		protected void NumberInput(int marginLeft = 500, int marginTop = 200)
		{
			GUI.Box(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(350f), GuiBase.Height(50f)), GuiBase.NumberInputText);
			marginTop += 50;
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(200f), GuiBase.Height(30f)), GuiBase.NumberInputNumber.GuiText, Gui.ChosenSkin.textField);
			marginTop += 40;
			this.AddInputButton(marginLeft, marginTop, 1);
			this.AddInputButton(marginLeft + 70, marginTop, 2);
			this.AddInputButton(marginLeft + 140, marginTop, 3);
			this.AddInputButton(marginLeft, marginTop + 35, 4);
			this.AddInputButton(marginLeft + 70, marginTop + 35, 5);
			this.AddInputButton(marginLeft + 140, marginTop + 35, 6);
			this.AddInputButton(marginLeft, marginTop + 70, 7);
			this.AddInputButton(marginLeft + 70, marginTop + 70, 8);
			this.AddInputButton(marginLeft + 140, marginTop + 70, 9);
			this.AddInputButton(marginLeft, marginTop + 105, 0);
			this.AddInputButton(marginLeft + 70, marginTop + 105, -1);
			this.AddInputButton(marginLeft + 140, marginTop + 105, -2);
			if (GUI.Button(new Rect(GuiBase.Width((float)(marginLeft + 105)), GuiBase.Height((float)(marginTop + 145)), GuiBase.Width(95f), GuiBase.Height(30f)), "OK"))
			{
				GuiBase.NumberInputAction(GuiBase.NumberInputNumber);
				this.ResetNumberInput();
			}
			if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)(marginTop + 145)), GuiBase.Width(95f), GuiBase.Height(30f)), "Cancel"))
			{
				this.ResetNumberInput();
			}
		}
	}
}
