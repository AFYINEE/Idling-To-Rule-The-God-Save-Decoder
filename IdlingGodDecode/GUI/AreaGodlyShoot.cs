using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class AreaGodlyShoot : GuiBase
	{
		public class Element
		{
			public Texture2D Image;

			public float marginLeft;

			public float marginTop;

			public float width;

			public float height;

			public Element()
			{
			}

			public Element(Texture2D Image, float marginLeft, float marginTop, float width, float height)
			{
				this.marginLeft = marginLeft;
				this.marginTop = marginTop;
				this.width = width;
				this.height = height;
				this.Image = Image;
			}

			public float GetCenterHeight()
			{
				return this.marginTop + this.height / 2f;
			}

			public float GetCenterWidth()
			{
				return this.marginLeft + this.width / 2f;
			}

			public bool Check(AreaGodlyShoot.Element element)
			{
				float num = element.marginTop + element.height * 0.2f;
				float num2 = element.marginLeft + element.width * 0.3f;
				float num3 = element.marginLeft + element.width * 0.7f;
				float num4 = element.marginTop + element.height * 0.8f;
				float num5 = this.marginTop + this.height * 0.2f;
				float num6 = this.marginLeft + this.width * 0.3f;
				float num7 = this.marginLeft + this.width * 0.7f;
				float num8 = this.marginTop + this.height * 0.8f;
				bool flag = num2 < num7 && num2 > num6;
				bool flag2 = num3 < num7 && num3 > num6;
				if (flag || flag2)
				{
					bool flag3 = num4 > num8 && num < num5;
					bool flag4 = num < num8 && num > num5;
					bool flag5 = num8 > num4 && num5 < num4;
					if (flag3 || flag5 || flag4)
					{
						return true;
					}
				}
				return false;
			}

			public void Shoot(float distance)
			{
				this.marginLeft += distance;
			}

			public void ShootDown(float distance)
			{
				this.marginTop += distance;
			}

			public void Fall(float distance)
			{
				this.marginLeft -= distance;
				this.marginTop += distance * 2f;
			}

			internal void Move(float distance)
			{
				this.marginLeft -= distance;
			}

			public void Update(float marginLeft, float marginTop)
			{
				this.marginLeft = marginLeft;
				this.marginTop = marginTop;
				GUI.Label(new Rect(GuiBase.Width(marginLeft), GuiBase.Height(marginTop), GuiBase.Width(this.width), GuiBase.Height(this.height)), this.Image);
			}

			public void Draw()
			{
				GUI.Label(new Rect(GuiBase.Width(this.marginLeft), GuiBase.Height(this.marginTop), GuiBase.Width(this.width), GuiBase.Height(this.height)), this.Image);
			}
		}

		public class Boss : AreaGodlyShoot.Element
		{
			private CDouble MaxHp = 0;

			private CDouble CurrentHP = 0;

			public CDouble CurrentLevel = 0;

			private bool MoveDown = true;

			public Boss(Texture2D Image, float marginLeft, float marginTop, float width, float height, int level = 1)
			{
				this.marginLeft = marginLeft;
				this.marginTop = marginTop;
				this.width = width;
				this.height = height;
				this.Image = Image;
				this.MaxHp = level * 50;
				this.CurrentHP = this.MaxHp;
				this.CurrentLevel = level;
			}

			public bool GetHit()
			{
				this.CurrentHP = --this.CurrentHP;
				return this.CurrentHP <= 0;
			}

			public void UpgradeLevel()
			{
				this.CurrentLevel = ++this.CurrentLevel;
				this.MaxHp = this.CurrentLevel * 50;
				this.CurrentHP = this.MaxHp;
			}

			internal new void Move(float distance)
			{
				if (this.marginTop > 500f)
				{
					this.MoveDown = false;
				}
				if (this.marginTop < 0f)
				{
					this.MoveDown = true;
				}
				if (!this.MoveDown)
				{
					distance *= -1f;
				}
				this.marginTop += distance;
			}

			public new void Draw()
			{
				base.Draw();
				double percent = this.CurrentHP.Double / this.MaxHp.Double;
				GuiBase.CreateBossHPBar(750, 560, 200f, 35f, percent, "Boss HP: " + this.CurrentHP.GuiText + " / " + this.MaxHp.GuiText);
			}
		}

		public static bool IsShown = false;

		private Texture2D Cloud;

		private Texture2D Background;

		private Texture2D ImageShot;

		private Texture2D ImageBoulder;

		private Texture2D ImageBoss;

		private Texture2D ImageBossShoot;

		private Texture2D ImageLaser;

		public Texture2D Avatar;

		public static AreaGodlyShoot Instance = new AreaGodlyShoot();

		private float marginLeft = 100f;

		private float marginTop = 200f;

		private bool isFlying;

		private List<AreaGodlyShoot.Element> AllShots = new List<AreaGodlyShoot.Element>();

		private List<AreaGodlyShoot.Element> AllBoulders = new List<AreaGodlyShoot.Element>();

		private List<AreaGodlyShoot.Element> BossShoots = new List<AreaGodlyShoot.Element>();

		private List<AreaGodlyShoot.Element> AllClouds = new List<AreaGodlyShoot.Element>();

		private AreaGodlyShoot.Element Player;

		private List<AreaGodlyShoot.Element> Lasers = new List<AreaGodlyShoot.Element>();

		private long time;

		private long newBoulderTimer = 100L;

		private long timeCloud;

		private long newCloudTimer = 1000L;

		private long bossSpecialAttack = 2000L;

		private long bossSpecialAttackTimer;

		private long bossShoot = 400L;

		private long bossShootTimer;

		public CDouble Points = 0;

		private long timeMs;

		private float multi;

		private AreaGodlyShoot.Boss boss;

		private long autoshootTimer;

		private int LaserIndex;

		private bool LaserIndexV2;

		private long timeMsMove;

		public void Show()
		{
			if (this.Avatar == null || this.Player == null)
			{
				this.ImageBoss = (Texture2D)Resources.Load("Gui/boss_slimie", typeof(Texture2D));
				this.ImageBossShoot = (Texture2D)Resources.Load("Gui/boss_slimie_shoot", typeof(Texture2D));
				this.ImageShot = (Texture2D)Resources.Load("Gui/ball", typeof(Texture2D));
				this.ImageBoulder = (Texture2D)Resources.Load("Gui/stone", typeof(Texture2D));
				this.Background = (Texture2D)Resources.Load("Gui/bgblue", typeof(Texture2D));
				this.ImageLaser = (Texture2D)Resources.Load("Gui/blaser", typeof(Texture2D));
				this.Cloud = (Texture2D)Resources.Load("Gui/cloud", typeof(Texture2D));
				this.Player = new AreaGodlyShoot.Element(this.Avatar, this.marginLeft, this.marginTop, 100f, 100f);
			}
			if (this.timeMs == 0L)
			{
				this.timeMs = UpdateStats.CurrentTimeMillis();
			}
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.Background, ScaleMode.StretchToFill, true);
			foreach (AreaGodlyShoot.Element current in this.AllClouds)
			{
				current.Draw();
			}
			this.Player.Update(this.marginLeft, this.marginTop);
			foreach (AreaGodlyShoot.Element current2 in this.AllShots)
			{
				current2.Draw();
			}
			foreach (AreaGodlyShoot.Element current3 in this.AllBoulders)
			{
				current3.Draw();
			}
			if (this.boss != null)
			{
				this.boss.Draw();
				foreach (AreaGodlyShoot.Element current4 in this.BossShoots)
				{
					current4.Draw();
				}
				foreach (AreaGodlyShoot.Element current5 in this.Lasers)
				{
					current5.Draw();
				}
			}
			GUI.Box(new Rect(GuiBase.Width(10f), GuiBase.Height(10f), GuiBase.Width(150f), GuiBase.Height(26f)), string.Empty);
			GUI.Label(new Rect(GuiBase.Width(15f), GuiBase.Height(12f), GuiBase.Width(140f), GuiBase.Height(25f)), "Points: " + this.Points.ToGuiText(true));
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.fontSize = GuiBase.FontSize(24);
			style.fontStyle = FontStyle.Bold;
			GUI.Box(new Rect(GuiBase.Width(390f), GuiBase.Height(10f), GuiBase.Width(165f), GuiBase.Height(40f)), string.Empty);
			GUI.Label(new Rect(GuiBase.Width(400f), GuiBase.Height(13f), GuiBase.Width(160f), GuiBase.Height(40f)), "Godly Shoot");
			style.fontSize = GuiBase.FontSize(16);
			style.fontStyle = FontStyle.Normal;
			if (!this.isFlying)
			{
				GUI.Box(new Rect(GuiBase.Width(350f), GuiBase.Height(180f), GuiBase.Width(300f), GuiBase.Height(205f)), string.Empty);
				GUI.Label(new Rect(GuiBase.Width(360f), GuiBase.Height(190f), GuiBase.Width(240f), GuiBase.Height(100f)), "Move with wasd or arrows and press space or return to shoot meteorites while dodging them.\nPress 'c' to continue if you die.");
				GUI.Label(new Rect(GuiBase.Width(360f), GuiBase.Height(300f), GuiBase.Width(240f), GuiBase.Height(100f)), "Highscore: " + App.State.Statistic.GodlyShootScore.ToGuiText(true));
				GUI.Label(new Rect(GuiBase.Width(360f), GuiBase.Height(340f), GuiBase.Width(240f), GuiBase.Height(100f)), "Highscore: " + App.State.Statistic.GodlyShootScoreBoss.ToGuiText(true));
				if (GUI.Button(new Rect(GuiBase.Width(510f), GuiBase.Height(300f), GuiBase.Width(130f), GuiBase.Height(30f)), "Start Game"))
				{
					this.Start(null);
				}
				if (GUI.Button(new Rect(GuiBase.Width(510f), GuiBase.Height(340f), GuiBase.Width(130f), GuiBase.Height(30f)), "Fight Boss"))
				{
					this.Start(new AreaGodlyShoot.Boss(this.ImageBoss, 600f, 250f, 200f, 200f, 1));
				}
			}
			this.UpdateProgress();
		}

		private void Start(AreaGodlyShoot.Boss boss = null)
		{
			this.AllShots = new List<AreaGodlyShoot.Element>();
			this.AllBoulders = new List<AreaGodlyShoot.Element>();
			this.AllClouds = new List<AreaGodlyShoot.Element>();
			this.BossShoots = new List<AreaGodlyShoot.Element>();
			this.Lasers = new List<AreaGodlyShoot.Element>();
			this.Points = 0;
			this.isFlying = true;
			this.marginTop = 200f;
			this.marginLeft = 100f;
			this.time = 0L;
			this.timeMs = UpdateStats.CurrentTimeMillis();
			this.multi = 0f;
			this.AddCloud((float)UnityEngine.Random.Range(0, 900));
			this.AddCloud((float)UnityEngine.Random.Range(0, 900));
			this.AddCloud((float)UnityEngine.Random.Range(0, 900));
			this.AddCloud((float)UnityEngine.Random.Range(0, 900));
			this.AddCloud((float)UnityEngine.Random.Range(0, 900));
			this.AddCloud((float)UnityEngine.Random.Range(0, 900));
			this.AddCloud((float)UnityEngine.Random.Range(0, 900));
			this.AddCloud((float)UnityEngine.Random.Range(0, 900));
			this.AddCloud((float)UnityEngine.Random.Range(0, 900));
			this.AddCloud((float)UnityEngine.Random.Range(0, 900));
			this.boss = boss;
			this.timeMsMove = UpdateStats.CurrentTimeMillis();
			this.Player = new AreaGodlyShoot.Element(this.Avatar, this.marginLeft, this.marginTop, 100f, 100f);
			if (Application.targetFrameRate != 60)
			{
				Application.targetFrameRate = 60;
				QualitySettings.vSyncCount = 0;
			}
		}

		private void AddCloud(float left)
		{
			float num = (float)UnityEngine.Random.Range(80, 300);
			this.AllClouds.Add(new AreaGodlyShoot.Element(this.Cloud, left, (float)UnityEngine.Random.Range(-100, 600), num, num / 2f));
		}

		private void EndGame()
		{
			if (this.boss == null && this.Points > App.State.Statistic.GodlyShootScore)
			{
				App.State.Statistic.GodlyShootScore = this.Points;
				Leaderboards.SubmitStat(LeaderBoardType.ScoreGodlyShoot, App.State.Statistic.GodlyShootScore.ToInt(), false);
			}
			if (this.boss != null && this.Points > App.State.Statistic.GodlyShootScoreBoss)
			{
				App.State.Statistic.GodlyShootScoreBoss = this.Points;
				Leaderboards.SubmitStat(LeaderBoardType.GodlyShootScoreBoss, App.State.Statistic.GodlyShootScoreBoss.ToInt(), false);
			}
			this.isFlying = false;
		}

		public void UpdateProgress()
		{
			if (this.isFlying)
			{
				long num = UpdateStats.CurrentTimeMillis() - this.timeMs;
				this.timeMs = UpdateStats.CurrentTimeMillis();
				this.multi = (float)(num / 12L);
				this.marginTop += 3f * this.multi;
				this.time += num;
				this.timeCloud += num;
				int num2 = -1;
				if (this.time > this.newBoulderTimer && this.boss == null)
				{
					this.time = 0L;
					float num3 = (float)UnityEngine.Random.Range(40, 100);
					this.AllBoulders.Add(new AreaGodlyShoot.Element(this.ImageBoulder, (float)UnityEngine.Random.Range(0, 1200), -200f, num3, num3));
				}
				if (this.boss != null)
				{
					this.bossShootTimer += num;
					this.bossSpecialAttackTimer += num;
					if (this.bossShootTimer > this.bossShoot)
					{
						if (this.boss.CurrentLevel.ToInt() > 4)
						{
							int num4 = 50;
							if (this.LaserIndexV2 && this.boss.CurrentLevel.ToInt() > 5)
							{
								num4 = 10;
							}
							this.Lasers.Add(new AreaGodlyShoot.Element(this.ImageLaser, (float)(num4 + this.LaserIndex * 100), -400f, 100f, 364f));
							this.LaserIndex++;
							if (this.LaserIndex > 4)
							{
								this.LaserIndexV2 = !this.LaserIndexV2;
								this.LaserIndex = 0;
							}
						}
						this.bossShootTimer = 0L;
						this.BossShoots.Add(new AreaGodlyShoot.Element(this.ImageBossShoot, this.boss.marginLeft, this.boss.marginTop + this.boss.height * 0.44f, 90f, 50f));
					}
					if (this.bossSpecialAttackTimer > this.bossSpecialAttack)
					{
						this.bossSpecialAttackTimer = 0L;
						for (int i = 0; i < 15; i++)
						{
							this.AllBoulders.Add(new AreaGodlyShoot.Element(this.ImageBoulder, (float)(500 + i * 50), (float)(i * 50 - 300), 150f, 150f));
						}
						if (this.boss.CurrentLevel.ToInt() > 2)
						{
							for (int j = 0; j < 15; j++)
							{
								this.AllBoulders.Add(new AreaGodlyShoot.Element(this.ImageBoulder, (float)(400 + j * 50), (float)(j * 50 - 300), 150f, 150f));
							}
						}
						if (this.boss.CurrentLevel.ToInt() > 3)
						{
							for (int k = 0; k < 15; k++)
							{
								this.AllBoulders.Add(new AreaGodlyShoot.Element(this.ImageBoulder, (float)(300 + k * 50), (float)(k * 50 - 300), 150f, 150f));
							}
						}
					}
					for (int l = 0; l < this.BossShoots.Count; l++)
					{
						AreaGodlyShoot.Element element = this.BossShoots[l];
						if (element.Check(this.Player))
						{
							this.EndGame();
							return;
						}
						if (element.marginLeft < -50f)
						{
							num2 = l;
						}
						element.Shoot(-8f * this.multi);
					}
					if (num2 != -1 && num2 < this.BossShoots.Count)
					{
						this.BossShoots.RemoveAt(num2);
					}
					int num5 = 3 + this.boss.CurrentLevel.ToInt();
					if (num5 > 10)
					{
						num5 = 10;
					}
					this.boss.Move((float)num5 * this.multi);
					num2 = -1;
					for (int m = 0; m < this.Lasers.Count; m++)
					{
						AreaGodlyShoot.Element element2 = this.Lasers[m];
						if (element2 != null)
						{
							if (element2.Check(this.Player))
							{
								this.EndGame();
								return;
							}
							element2.ShootDown(20f * this.multi);
							if (element2.marginTop > 800f)
							{
								num2 = m;
							}
						}
					}
					if (num2 != -1 && num2 < this.Lasers.Count)
					{
						this.Lasers.RemoveAt(num2);
					}
				}
				if (this.timeCloud > this.newCloudTimer)
				{
					this.newCloudTimer = (long)UnityEngine.Random.Range(800, 2400);
					this.timeCloud = 0L;
					this.AddCloud(960f);
				}
				for (int n = 0; n < this.AllShots.Count; n++)
				{
					AreaGodlyShoot.Element element3 = this.AllShots[n];
					if (this.boss != null && element3.Check(this.boss) && this.boss.GetHit())
					{
						this.Points += this.boss.CurrentLevel * 50;
						this.Points *= 1.2;
						this.boss.UpgradeLevel();
					}
					for (int num6 = 0; num6 < this.AllBoulders.Count; num6++)
					{
						if (element3.Check(this.AllBoulders[num6]))
						{
							this.AllBoulders.RemoveAt(num6);
							this.Points = ++this.Points;
							break;
						}
					}
					if (GuiBase.Width((float)((int)element3.marginLeft)) > GuiBase.Width(900f))
					{
						this.AllShots.RemoveAt(n);
						break;
					}
					element3.Shoot(12f * this.multi);
				}
				if (this.boss != null && this.boss.Check(this.Player))
				{
					this.EndGame();
					return;
				}
				num2 = -1;
				for (int num7 = 0; num7 < this.AllBoulders.Count; num7++)
				{
					AreaGodlyShoot.Element element4 = this.AllBoulders[num7];
					if (element4.Check(this.Player))
					{
						this.EndGame();
						return;
					}
					if (element4.marginLeft < -50f)
					{
						num2 = num7;
					}
					element4.Fall(3f * this.multi);
				}
				if (num2 != -1 && num2 < this.AllBoulders.Count)
				{
					this.AllBoulders.RemoveAt(num2);
				}
				num2 = -1;
				for (int num8 = 0; num8 < this.AllClouds.Count; num8++)
				{
					AreaGodlyShoot.Element element5 = this.AllClouds[num8];
					if (element5.marginLeft < -50f)
					{
						num2 = num8;
					}
					element5.Move((float)UnityEngine.Random.Range(0, 4));
				}
				if (num2 != -1 && num2 < this.AllClouds.Count)
				{
					this.AllClouds.RemoveAt(num2);
				}
			}
			if (this.marginTop > 500f || this.marginLeft < -20f || this.marginLeft > 890f || this.marginTop < -20f)
			{
				this.EndGame();
			}
		}

		internal void Update()
		{
			if (!AreaGodlyShoot.IsShown)
			{
				return;
			}
			if (Input.GetKey(KeyCode.C))
			{
				if (this.boss == null)
				{
					this.Start(null);
				}
				else
				{
					this.Start(new AreaGodlyShoot.Boss(this.ImageBoss, 600f, 250f, 200f, 200f, 1));
				}
			}
			if (!this.isFlying)
			{
				return;
			}
			if (this.timeMsMove == 0L)
			{
				this.timeMsMove = UpdateStats.CurrentTimeMillis();
			}
			long num = UpdateStats.CurrentTimeMillis() - this.timeMsMove;
			this.timeMsMove = UpdateStats.CurrentTimeMillis();
			long num2 = num / 12L;
			long num3 = UpdateStats.CurrentTimeMillis() - this.autoshootTimer;
			if (num3 > 200L)
			{
				this.autoshootTimer = UpdateStats.CurrentTimeMillis();
				this.AllShots.Add(new AreaGodlyShoot.Element(this.ImageShot, this.marginLeft + 30f, this.marginTop, 30f, 20f));
			}
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
			{
				this.marginLeft -= (float)(5L * num2);
			}
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
			{
				this.marginTop -= (float)(7L * num2);
			}
			if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
			{
				this.marginLeft += (float)(5L * num2);
			}
			if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
			{
				this.marginTop += (float)(5L * num2);
			}
		}
	}
}
