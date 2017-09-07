using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using System;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class PBaal
	{
		private CDouble currentHealth;

		public bool IsUnlocked;

		public bool IsFighting;

		public CDouble powerLevel;

		public CDouble DamageSec = new CDouble();

		public CDouble CurrentHealth
		{
			get
			{
				if (this.currentHealth < 0)
				{
					return 0;
				}
				if (this.currentHealth > this.MaxHealth)
				{
					return this.MaxHealth;
				}
				return this.currentHealth;
			}
			set
			{
				this.currentHealth = value;
			}
		}

		public int Level
		{
			get;
			set;
		}

		public CDouble AttackBase
		{
			get;
			set;
		}

		public string Name
		{
			get
			{
				if (this.Level > 1)
				{
					return "P. Baal v " + this.Level;
				}
				return "P. Baal";
			}
		}

		public string Description
		{
			get
			{
				return "After his defeat he was reborn and is a lot stronger than ever! His look changed quite a bit though...\nEach version of him you defeat will increase the rebirth limit for monuments by 1." + this.PowerLevelText + "\nDoes a 500% damage critical attack every time.";
			}
		}

		public CDouble Attack
		{
			get
			{
				return this.AttackBase;
			}
		}

		public CDouble Defense
		{
			get
			{
				return this.Attack / 8 * 5;
			}
		}

		public CDouble MaxHealth
		{
			get
			{
				return this.Attack * 10;
			}
		}

		public CDouble PowerLevel
		{
			get
			{
				if (this.powerLevel == null)
				{
					this.powerLevel = this.Defense + this.Attack * 2 * 5;
				}
				return this.powerLevel;
			}
		}

		protected string PowerLevelText
		{
			get
			{
				return string.Concat(new string[]
				{
					"lowerTextPower level: ",
					this.PowerLevel.ToGuiText(true),
					"\nHP: ",
					this.MaxHealth.ToGuiText(true),
					"\nAttack: ",
					this.Attack.ToGuiText(true),
					"\nDefense ",
					this.Defense.ToGuiText(true)
				});
			}
		}

		public PBaal()
		{
			this.Level = 1;
			this.AttackBase = new CDouble("99999999999999999999999999999999999999999999999999999999000");
			this.currentHealth = this.MaxHealth;
		}

		public void IncreaseLevel()
		{
			this.Level++;
			CDouble cDouble = 100 - App.State.Statistic.UltimateBaalChallengesFinished - 2 * App.State.Statistic.ArtyChallengesFinished;
			if (cDouble < 50)
			{
				cDouble = 50;
			}
			this.AttackBase *= cDouble;
			this.currentHealth = this.MaxHealth;
			this.powerLevel = null;
		}

		public void Fight(long ms)
		{
			this.RecoverHealth(ms);
			if (!this.IsFighting || !this.IsUnlocked)
			{
				return;
			}
			if (App.State.GetAttacked(this.Attack, ms, true))
			{
				this.IsFighting = false;
				GodUi.EnableCreating();
			}
			else
			{
				this.GetAttacked(App.State.Attack, ms);
			}
		}

		public void GetAttacked(CDouble attackPower, long millisecs)
		{
			int value = UnityEngine.Random.Range(1, 100);
			CDouble cDouble = new CDouble();
			if (value <= App.State.Crits.CriticalPercent(App.State.GameSettings.TBSEyesIsMirrored))
			{
				attackPower = attackPower * App.State.Crits.CriticalDamage / 100;
				cDouble = attackPower / 5000 * millisecs;
			}
			else
			{
				cDouble = (attackPower - this.Defense) / 5000 * millisecs;
			}
			this.DamageSec = cDouble * 33;
			if (cDouble > 0)
			{
				this.CurrentHealth -= cDouble;
			}
			if (this.CurrentHealth <= 0)
			{
				if (App.State.Statistic.HighestGodDefeated < this.Level + 28)
				{
					App.State.Statistic.HighestGodDefeated = this.Level + 28;
				}
				App.State.PremiumBoni.GodPower++;
				App.State.HomePlanet.BaalPower += this.Level;
				GuiBase.ShowToast("Your god power is increased by 1, you also received " + this.Level + " Baal Power!");
				App.State.PremiumBoni.CheckIfGPIsAdjusted();
				this.IsFighting = false;
				GodUi.EnableCreating();
				Statistic expr_1AF = App.State.Statistic;
				expr_1AF.TotalGodsDefeated = ++expr_1AF.TotalGodsDefeated;
				Leaderboards.SubmitStat(LeaderBoardType.GodsDefeated, App.State.Statistic.TotalGodsDefeated.ToInt(), false);
				Leaderboards.SubmitStat(LeaderBoardType.HighestGodDefeated, App.State.Statistic.HighestGodDefeated.ToInt(), false);
				this.IncreaseLevel();
			}
			Log.Info("CurrentHealth: " + this.CurrentHealth);
		}

		public void RecoverHealth(long millisecs)
		{
			if (this.CurrentHealth == this.MaxHealth)
			{
				return;
			}
			this.CurrentHealth += millisecs * this.Defense / 20000 + 1;
		}

		public double getPercentOfHP()
		{
			return (this.CurrentHealth / this.MaxHealth).Double;
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.IsUnlocked.ToString());
			Conv.AppendValue(stringBuilder, "b", this.CurrentHealth.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.Level.ToString());
			Conv.AppendValue(stringBuilder, "d", this.AttackBase.Serialize());
			Conv.AppendValue(stringBuilder, "e", this.IsFighting.ToString());
			return Conv.ToBase64(stringBuilder.ToString(), "PBaal");
		}

		internal static PBaal FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("PBaal.FromString with empty value!");
				return new PBaal();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "PBaal");
			return new PBaal
			{
				IsUnlocked = Conv.getStringFromParts(parts, "a").ToLower().Equals("true"),
				CurrentHealth = new CDouble(Conv.getStringFromParts(parts, "b")),
				Level = Conv.getIntFromParts(parts, "c"),
				AttackBase = new CDouble(Conv.getStringFromParts(parts, "d")),
				IsFighting = Conv.getStringFromParts(parts, "e").ToLower().Equals("true")
			};
		}
	}
}
