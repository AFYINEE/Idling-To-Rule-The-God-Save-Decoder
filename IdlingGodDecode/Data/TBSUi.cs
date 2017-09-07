using Assets.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class TBSUi : GuiBase
	{
		public enum HitType
		{
			left,
			right,
			top,
			bottom,
			eyes,
			eyes_non_mirror
		}

		public class HitGame
		{
			public float progress;

			public float progressSpeed = 0.5f;

			public float totalWith = 150f;

			public float lastHitProgress = -1f;

			public bool isStarted;

			public string lastHitText = string.Empty;

			public int hitAreaLeft = 100;

			public int hitAreaWith = 10;

			public TBSUi.HitType TypeEnum;

			private bool isStopped;

			public int MissCount;

			public void SetDifficulty(int level)
			{
				this.MissCount = 0;
				this.hitAreaLeft = UnityEngine.Random.Range(20, (int)(this.totalWith - 30f));
				this.hitAreaWith = 20 - level / 8;
				if (this.hitAreaWith < 3)
				{
					this.hitAreaWith = 3;
				}
				this.hitAreaWith += App.State.PremiumBoni.TbsExtraPixels;
				if (this.TypeEnum == TBSUi.HitType.bottom || this.TypeEnum == TBSUi.HitType.top)
				{
					this.hitAreaWith = (int)GuiBase.Height((float)this.hitAreaWith);
				}
				else
				{
					this.hitAreaWith = (int)GuiBase.Width((float)this.hitAreaWith);
				}
				this.progressSpeed = 0.5f + 0.1f * (float)level / 5f;
				if (this.progressSpeed > 1.5f)
				{
					this.progressSpeed = 1.5f;
				}
				this.progressSpeed = this.progressSpeed * this.totalWith / 100f;
			}

			public void start(int level, TBSUi.HitType type, float width)
			{
				this.totalWith = width;
				this.TypeEnum = type;
				if (!this.isStopped)
				{
					this.SetDifficulty(level);
				}
				this.lastHitText = string.Empty;
				this.progress = 0f;
				this.lastHitProgress = -1f;
				this.isStarted = true;
				this.isStopped = false;
			}

			public void stop()
			{
				this.lastHitText = string.Empty;
				this.progress = 0f;
				this.lastHitProgress = -1f;
				this.isStarted = false;
				this.isStopped = true;
			}

			public bool IsHit(int level)
			{
				this.lastHitProgress = this.progress;
				if (this.TypeEnum == TBSUi.HitType.right)
				{
					this.lastHitProgress = this.totalWith - this.lastHitProgress;
				}
				int num = this.hitAreaLeft + this.hitAreaWith;
				Log.Info("lastHitProgress: " + this.lastHitProgress);
				Log.Info(string.Concat(new object[]
				{
					"hitAreaLeft: ",
					this.hitAreaLeft,
					"max",
					num
				}));
				bool flag = this.lastHitProgress >= (float)(this.hitAreaLeft - 1) && this.lastHitProgress <= (float)num;
				if (flag)
				{
					this.lastHitProgress = -1f;
					this.SetDifficulty(level);
					if (App.State.Statistic.TBSScore < App.State.Crits.Score(App.State.GameSettings.TBSEyesIsMirrored) + 1)
					{
						App.State.Statistic.TBSScore = App.State.Crits.Score(App.State.GameSettings.TBSEyesIsMirrored) + 1;
						Leaderboards.SubmitStat(LeaderBoardType.TBSScore, App.State.Crits.Score(App.State.GameSettings.TBSEyesIsMirrored) + 1, false);
					}
				}
				return flag;
			}
		}

		public static TBSUi Instance = new TBSUi();

		private static Texture2D hitArea = Texture2D.whiteTexture;

		private static Texture2D center;

		private static Texture2D lastHitPoint;

		private static Texture2D prinny;

		public float lastScreenWidth;

		public static bool ScreenChanged = false;

		private long timeMs;

		private bool showBP;

		private bool showInfo;

		private TBSUi.HitGame eyesGame = new TBSUi.HitGame();

		private TBSUi.HitGame eyesNonMirrorGame = new TBSUi.HitGame();

		private TBSUi.HitGame wingsGame = new TBSUi.HitGame();

		private TBSUi.HitGame tailGame = new TBSUi.HitGame();

		private TBSUi.HitGame feetGame = new TBSUi.HitGame();

		private TBSUi.HitGame mouthGame = new TBSUi.HitGame();

		private bool isFirstDown = true;

		public void Init()
		{
			TBSUi.center = (Texture2D)Resources.Load("Gui/blue", typeof(Texture2D));
			TBSUi.prinny = (Texture2D)Resources.Load("Gui/prinny", typeof(Texture2D));
			TBSUi.lastHitPoint = (Texture2D)Resources.Load("Gui/green", typeof(Texture2D));
		}

		public static void Show()
		{
			TBSUi.Instance.show();
		}

		private void show()
		{
			if (this.lastScreenWidth != (float)Screen.width)
			{
				this.lastScreenWidth = (float)Screen.width;
				TBSUi.ScreenChanged = true;
			}
			this.lastScreenWidth = (float)Screen.width;
			if (this.timeMs == 0L)
			{
				this.timeMs = UpdateStats.CurrentTimeMillis();
			}
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = GuiBase.FontSize(20);
			GUI.BeginGroup(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)));
			GUI.Box(new Rect(GuiBase.Width(0f), GuiBase.Height(0f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			if (App.State.Statistic.HighestGodDefeated < 28 || App.State.Statistic.HasStartedArtyChallenge)
			{
				GUI.Label(new Rect(GuiBase.Width(140f), GuiBase.Height(150f), GuiBase.Width(640f), GuiBase.Height(30f)), "You need to defeat Baal to enter 'The Baal Slayer'.", style);
				GUI.EndGroup();
				return;
			}
			style.fontStyle = FontStyle.Bold;
			GUI.Label(new Rect(GuiBase.Width(245f), GuiBase.Height(10f), GuiBase.Width(250f), GuiBase.Height(30f)), new GUIContent("The Baal Slayer"));
			style.fontSize = GuiBase.FontSize(16);
			string text = "Info";
			if (this.showInfo)
			{
				text = "Back";
			}
			if (!this.showBP && GUI.Button(new Rect(GuiBase.Width(450f), GuiBase.Height(10f), GuiBase.Width(65f), GuiBase.Height(30f)), text))
			{
				this.showInfo = !this.showInfo;
			}
			string text2 = "Baal Power";
			if (this.showBP)
			{
				text2 = "Back";
			}
			if (!this.showInfo && GUI.Button(new Rect(GuiBase.Width(530f), GuiBase.Height(10f), GuiBase.Width(100f), GuiBase.Height(30f)), text2))
			{
				this.showBP = !this.showBP;
			}
			if (this.showInfo)
			{
				style.fontStyle = FontStyle.Normal;
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(80f), GuiBase.Width(580f), GuiBase.Height(400f)), "Hit different body-parts to increase the critical chance and damage when fighting gods. Critical damage will always ignore the defense.\nAll P. Baals have 100% critical chance and 500% critical damage. Now you can beat that!\nClick 'Start' and then 'Hit' when the blue progress is within the white box. If your timing is right, it increases the level of the nearest body part by 1, if it is wrong, it decreases the level by 1.\nThe difficulty increases with higher levels.\nEvery level from Wings, Tail, Mouth and Feet will increase the critical damage by 1% and every level to the eyes will increase the chance by 1. \nThe eyes are special, you have to hit an invisible white box where it is mirrored on the blue line. Alternatively you can play it normal but then you only get 1 score each level and 1 % crit every 2.5 levels.\nThe maximum chance is 100% and the maximum damage is 1000%");
			}
			else if (this.showBP)
			{
				this.showBPOptions();
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height(50f), GuiBase.Width(250f), GuiBase.Height(30f)), "Critical Chance % " + App.State.Crits.CriticalPercent(App.State.GameSettings.TBSEyesIsMirrored));
				GUI.Label(new Rect(GuiBase.Width(240f), GuiBase.Height(50f), GuiBase.Width(250f), GuiBase.Height(30f)), "Critical Damage % " + App.State.Crits.CriticalDamage);
				GUI.Label(new Rect(GuiBase.Width(450f), GuiBase.Height(50f), GuiBase.Width(250f), GuiBase.Height(30f)), new GUIContent("Score " + App.State.Crits.Score(App.State.GameSettings.TBSEyesIsMirrored), "Eyes count 4x if mirrored and all other levels count 1x. This is added to your statistic multi and highscore."));
				style.fontStyle = FontStyle.Normal;
				GUI.DrawTexture(new Rect(GuiBase.Width(210f), GuiBase.Height(120f), GuiBase.Width(250f), GuiBase.Height(250f)), TBSUi.prinny);
				string arg = string.Empty;
				string tooltip = string.Empty;
				int num = App.State.Crits.EyesNoMirror;
				if (App.State.GameSettings.TBSEyesIsMirrored)
				{
					arg = " (mirrored)";
					tooltip = "This one is mirrored. Hit where the white box would be mirrored from the blue line in the center.";
					num = App.State.Crits.Eyes;
				}
				GUI.Label(new Rect(GuiBase.Width(220f), GuiBase.Height(140f), GuiBase.Width(175f), GuiBase.Height(30f)), new GUIContent("Eyes Lv " + num + arg, tooltip));
				GUI.Label(new Rect(GuiBase.Width(165f), GuiBase.Height(230f), GuiBase.Width(100f), GuiBase.Height(30f)), "Wings Lv " + App.State.Crits.Wings);
				GUI.Label(new Rect(GuiBase.Width(210f), GuiBase.Height(330f), GuiBase.Width(100f), GuiBase.Height(30f)), "Tail Lv " + App.State.Crits.Tail);
				GUI.Label(new Rect(GuiBase.Width(390f), GuiBase.Height(320f), GuiBase.Width(100f), GuiBase.Height(30f)), "Feet Lv " + App.State.Crits.Feet);
				GUI.Label(new Rect(GuiBase.Width(400f), GuiBase.Height(150f), GuiBase.Width(100f), GuiBase.Height(30f)), "Mouth Lv " + App.State.Crits.Mouth);
				long timeProgressed = UpdateStats.CurrentTimeMillis() - this.timeMs;
				this.timeMs = UpdateStats.CurrentTimeMillis();
				if (App.State.GameSettings.TBSEyesIsMirrored)
				{
					App.State.Crits.Eyes = this.CreateProgressGame(50, 80, this.eyesGame, App.State.Crits.Eyes, TBSUi.HitType.eyes, timeProgressed);
				}
				else
				{
					App.State.Crits.EyesNoMirror = this.CreateProgressGame(50, 80, this.eyesNonMirrorGame, App.State.Crits.EyesNoMirror, TBSUi.HitType.eyes_non_mirror, timeProgressed);
				}
				App.State.Crits.Wings = this.CreateProgressGame(30, 210, this.wingsGame, App.State.Crits.Wings, TBSUi.HitType.top, timeProgressed);
				App.State.Crits.Tail = this.CreateProgressGame(100, 360, this.tailGame, App.State.Crits.Tail, TBSUi.HitType.left, timeProgressed);
				App.State.Crits.Feet = this.CreateProgressGame(350, 350, this.feetGame, App.State.Crits.Feet, TBSUi.HitType.right, timeProgressed);
				App.State.Crits.Mouth = this.CreateProgressGame(480, 180, this.mouthGame, App.State.Crits.Mouth, TBSUi.HitType.bottom, timeProgressed);
				TBSUi.ScreenChanged = false;
				App.State.GameSettings.TBSEyesIsMirrored = GUI.Toggle(new Rect(GuiBase.Width(220f), GuiBase.Height(110f), GuiBase.Width(300f), GuiBase.Height(30f)), App.State.GameSettings.TBSEyesIsMirrored, new GUIContent("Mirrored Eyes", "If this is on, you need to hit the white box from the eyes mirrored from the blue line. While mirrored you get 1 % crit each level and 4 score. If it is not mirrored you get 1% crit every 2.5 levels and 1 score each level."));
			}
			GUI.EndGroup();
		}

		private void showBPOptions()
		{
			GUIStyle style = GUI.skin.GetStyle("Label");
			GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height(50f), GuiBase.Width(250f), GuiBase.Height(30f)), "You have " + App.State.HomePlanet.BaalPower + " Baal Power.");
			style.fontStyle = FontStyle.Normal;
			GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height(90f), GuiBase.Width(580f), GuiBase.Height(30f)), "You can spend Baal Power to increase the level by 1 each BP.");
			int num = 130;
			App.State.Crits.EyesNoMirror = this.addBPRow("Eyes (normal)", ref num, App.State.Crits.EyesNoMirror);
			App.State.Crits.Mouth = this.addBPRow("Mouth", ref num, App.State.Crits.Mouth);
			App.State.Crits.Wings = this.addBPRow("Wings", ref num, App.State.Crits.Wings);
			App.State.Crits.Tail = this.addBPRow("Tail", ref num, App.State.Crits.Tail);
			App.State.Crits.Feet = this.addBPRow("Feet", ref num, App.State.Crits.Feet);
		}

		private int addBPRow(string name, ref int marginTop, int value)
		{
			int result = value;
			GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height((float)marginTop), GuiBase.Width(150f), GuiBase.Height(30f)), name);
			GUI.Label(new Rect(GuiBase.Width(180f), GuiBase.Height((float)marginTop), GuiBase.Width(150f), GuiBase.Height(30f)), "level: " + value);
			if (App.State.HomePlanet.BaalPower >= 1 && GUI.Button(new Rect(GuiBase.Width(300f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+1"))
			{
				App.State.HomePlanet.BaalPower--;
				result = value + 1;
			}
			if (App.State.HomePlanet.BaalPower >= 5 && GUI.Button(new Rect(GuiBase.Width(350f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+5"))
			{
				App.State.HomePlanet.BaalPower -= 5;
				result = value + 5;
			}
			if (App.State.HomePlanet.BaalPower >= 10 && GUI.Button(new Rect(GuiBase.Width(400f), GuiBase.Height((float)marginTop), GuiBase.Width(45f), GuiBase.Height(30f)), "+10"))
			{
				App.State.HomePlanet.BaalPower -= 10;
				result = value + 10;
			}
			if (App.State.HomePlanet.BaalPower >= 50 && GUI.Button(new Rect(GuiBase.Width(455f), GuiBase.Height((float)marginTop), GuiBase.Width(45f), GuiBase.Height(30f)), "+50"))
			{
				App.State.HomePlanet.BaalPower -= 50;
				result = value + 50;
			}
			marginTop += 30;
			return result;
		}

		public static void Reset()
		{
			TBSUi.Instance.eyesGame = new TBSUi.HitGame();
			TBSUi.Instance.eyesNonMirrorGame = new TBSUi.HitGame();
			TBSUi.Instance.wingsGame = new TBSUi.HitGame();
			TBSUi.Instance.tailGame = new TBSUi.HitGame();
			TBSUi.Instance.feetGame = new TBSUi.HitGame();
			TBSUi.Instance.mouthGame = new TBSUi.HitGame();
		}

		public int CreateProgressGame(int marginLeft, int marginTop, TBSUi.HitGame game, int level, TBSUi.HitType type, long timeProgressed)
		{
			float num = GuiBase.Width(150f);
			float num2 = GuiBase.Height(20f);
			if (type == TBSUi.HitType.left || type == TBSUi.HitType.right || type == TBSUi.HitType.eyes || type == TBSUi.HitType.eyes_non_mirror)
			{
				if (game.lastHitText != null && game.lastHitText.Contains("Double"))
				{
					GUI.Label(new Rect(GuiBase.Width((float)(marginLeft + 30)), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), game.lastHitText);
				}
				else
				{
					GUI.Label(new Rect(GuiBase.Width((float)(marginLeft + 50)), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), game.lastHitText);
				}
				GUI.BeginGroup(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)(marginTop + 30)), num, GuiBase.Height(100f)));
				GUI.DrawTexture(new Rect(0f, 0f, num, num2), GuiBase.progressBg);
				float progress = game.progress;
				float x = 0f;
				if (type == TBSUi.HitType.right)
				{
					x = GuiBase.Width(150f) - progress;
				}
				GUI.DrawTexture(new Rect(x, 0f, progress, num2), GuiBase.progressFgBlue);
				x = (float)game.hitAreaLeft;
				float x2 = game.lastHitProgress;
				if (type == TBSUi.HitType.eyes)
				{
					x = num - (float)game.hitAreaWith - (float)game.hitAreaLeft;
					x2 = num - game.lastHitProgress;
					GUI.DrawTexture(new Rect(num / 2f - 1f, -5f, GuiBase.Width(2f), num2 + 10f), TBSUi.center);
				}
				GUI.DrawTexture(new Rect(x, 1f, (float)game.hitAreaWith, num2 - 2f), TBSUi.hitArea);
				if (game.lastHitProgress != -1f)
				{
					GUI.DrawTexture(new Rect(x2, 1f, 2f, num2 - 2f), TBSUi.lastHitPoint);
				}
			}
			else if (type == TBSUi.HitType.top || type == TBSUi.HitType.bottom)
			{
				if (game.lastHitText != null && game.lastHitText.Contains("Double"))
				{
					GUI.Label(new Rect(GuiBase.Width((float)(marginLeft - 10)), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), game.lastHitText);
				}
				else
				{
					GUI.Label(new Rect(GuiBase.Width((float)(marginLeft + 20)), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), game.lastHitText);
				}
				GUI.BeginGroup(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(150f), GuiBase.Height(200f)));
				GUI.DrawTexture(new Rect(GuiBase.Width(80f), 0f, num2, num), GuiBase.progressBgRotated);
				float progress2 = game.progress;
				float y = 0f;
				if (type == TBSUi.HitType.bottom)
				{
					y = GuiBase.Width(150f) - progress2;
				}
				float y2 = (float)game.hitAreaLeft;
				float y3 = game.lastHitProgress;
				if (type == TBSUi.HitType.bottom)
				{
					y2 = num - (float)game.hitAreaWith - (float)game.hitAreaLeft;
					y3 = num - game.lastHitProgress;
				}
				GUI.DrawTexture(new Rect(GuiBase.Width(80f), y, num2, progress2), GuiBase.progressFgBlueRotated);
				GUI.DrawTexture(new Rect(GuiBase.Width(81f), y2, num2 - GuiBase.Width(2f), (float)game.hitAreaWith), TBSUi.hitArea);
				if (game.lastHitProgress != -1f)
				{
					GUI.DrawTexture(new Rect(GuiBase.Width(81f), y3, num2 - 2f, 2f), TBSUi.lastHitPoint);
				}
			}
			if (TBSUi.ScreenChanged)
			{
				bool isStarted = game.isStarted;
				game.stop();
				game.start(level, type, num);
				game.SetDifficulty(level);
				if (!isStarted)
				{
					game.stop();
				}
			}
			if (game.isStarted)
			{
				game.progress += game.progressSpeed * (float)timeProgressed / 14f;
				if (game.progress > game.totalWith)
				{
					game.progress = 0f;
				}
				int num3 = 80;
				int num4 = 30;
				if (type == TBSUi.HitType.top || type == TBSUi.HitType.bottom)
				{
					num3 = 0;
					num4 = 70;
				}
				if (GUI.Button(new Rect(GuiBase.Width((float)num3), GuiBase.Height((float)num4), GuiBase.Width(70f), GuiBase.Height(30f)), "Stop"))
				{
					game.stop();
				}
			}
			else if (GUI.Button(new Rect(GuiBase.Width(0f), GuiBase.Height(30f), GuiBase.Width(70f), GuiBase.Height(30f)), "Start"))
			{
				game.start(level, type, num);
			}
			GUI.EndGroup();
			int num5 = marginTop + 60;
			int num6 = 70;
			int num7 = 30;
			if (type == TBSUi.HitType.top || type == TBSUi.HitType.bottom)
			{
				num5 = marginTop + 30;
			}
			if (game.isStarted && GUI.RepeatButton(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)num5), GuiBase.Width((float)num6), GuiBase.Height((float)num7)), "Hit"))
			{
				if (this.isFirstDown)
				{
					if (game.IsHit(level))
					{
						game.lastHitText = "Hit!";
						level++;
						int num8 = UnityEngine.Random.Range(0, 100);
						if (num8 < App.State.PremiumBoni.TbsDoublePoints)
						{
							game.lastHitText = "Double Hit!";
							level++;
						}
					}
					else
					{
						game.lastHitText = "Miss!";
						game.MissCount++;
						if (game.MissCount == 5)
						{
							game.SetDifficulty(level);
						}
						int num9 = UnityEngine.Random.Range(0, 100);
						if (num9 > App.State.PremiumBoni.TbsMissreduction)
						{
							level--;
						}
						if (level < 0)
						{
							level = 0;
						}
					}
					this.isFirstDown = false;
				}
				Event current = Event.current;
				if (current.type == EventType.Used)
				{
					this.isFirstDown = true;
				}
			}
			return level;
		}
	}
}
