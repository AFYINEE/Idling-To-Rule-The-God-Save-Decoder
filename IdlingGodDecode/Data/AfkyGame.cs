using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Data
{
	public class AfkyGame
	{
		public class StatExp
		{
			public CDouble Level = 1;

			public CDouble ExpModi = 0;

			public CDouble ExpCostNextLevel = 0;

			public CDouble ExpCostNext10Levels = 0;

			public CDouble ExpCostNext100Levels = 0;

			public CDouble ExpCostNext1000Levels = 0;

			public CDouble ExpCostNext10000Levels = 0;

			public string Serialize()
			{
				StringBuilder stringBuilder = new StringBuilder();
				Conv.AppendValue(stringBuilder, "a", this.Level.Serialize());
				Conv.AppendValue(stringBuilder, "b", this.ExpModi.Serialize());
				return Conv.ToBase64(stringBuilder.ToString(), "StatExp");
			}

			internal static AfkyGame.StatExp FromString(string base64String)
			{
				if (string.IsNullOrEmpty(base64String))
				{
					return new AfkyGame.StatExp();
				}
				string[] parts = Conv.StringPartsFromBase64(base64String, "StatExp");
				AfkyGame.StatExp statExp = new AfkyGame.StatExp();
				statExp.Level = Conv.getCDoubleFromParts(parts, "a", false);
				statExp.ExpModi = Conv.getCDoubleFromParts(parts, "b", false);
				statExp.InitExpCost();
				return statExp;
			}

			public CDouble ExpCost(CDouble value)
			{
				if (this.ExpModi == 0)
				{
					return value;
				}
				if (this.ExpModi == 1)
				{
					return Math.Pow(10.0 * value.Double, 2.0);
				}
				if (this.ExpModi == 2)
				{
					return Math.Pow(10.0 * value.Double, 2.0) / 3.0;
				}
				return value;
			}

			public CDouble LevelUp(CDouble exp, int levels, bool max = false)
			{
				if (this.ExpCostNextLevel == 0 || this.ExpCostNext10Levels == 0 || this.ExpCostNext100Levels == 0 || this.ExpCostNext1000Levels == 0)
				{
					this.InitExpCost();
				}
				CDouble cDouble = 0;
				if (max)
				{
					CDouble cDouble2 = exp;
					while (cDouble2 > this.ExpCostNext1000Levels)
					{
						this.Level += 1000;
						cDouble += this.ExpCostNext1000Levels;
						cDouble2 -= this.ExpCostNext1000Levels;
						if (this.ExpModi == 0)
						{
							this.InitExpCost();
						}
					}
					int num = 0;
					while (true)
					{
						CDouble rightSide = this.ExpCost(this.Level + num);
						if (cDouble + rightSide > cDouble2)
						{
							break;
						}
						cDouble += rightSide;
						num++;
					}
					this.Level += num;
					this.InitExpCost();
					return cDouble;
				}
				if (levels == 1 && exp >= this.ExpCostNextLevel)
				{
					this.Level = ++this.Level;
					cDouble = this.ExpCostNextLevel;
				}
				else if (levels == 10 && exp >= this.ExpCostNext10Levels)
				{
					this.Level += levels;
					cDouble = this.ExpCostNext10Levels;
				}
				else if (levels == 100 && exp >= this.ExpCostNext100Levels)
				{
					this.Level += levels;
					cDouble = this.ExpCostNext100Levels;
				}
				else if (levels == 1000 && exp >= this.ExpCostNext1000Levels)
				{
					this.Level += levels;
					cDouble = this.ExpCostNext1000Levels;
				}
				else if (levels == 10000 && exp >= this.ExpCostNext10000Levels)
				{
					this.Level += levels;
					cDouble = this.ExpCostNext10000Levels;
				}
				this.InitExpCost();
				return cDouble;
			}

			public void InitExpCost()
			{
				this.ExpCostNextLevel = this.ExpCost(this.Level);
				this.ExpCostNext10Levels = 0;
				this.ExpCostNext100Levels = 0;
				this.ExpCostNext1000Levels = 0;
				this.ExpCostNext10000Levels = 0;
				if (this.ExpModi == 0)
				{
					this.ExpCostNext10Levels = (this.Level + 10) * (this.Level + 11) / 2 - this.Level * (this.Level + 1) / 2 - 10;
					this.ExpCostNext100Levels = (this.Level + 100) * (this.Level + 101) / 2 - this.Level * (this.Level + 1) / 2 - 100;
					this.ExpCostNext1000Levels = (this.Level + 1000) * (this.Level + 1001) / 2 - this.Level * (this.Level + 1) / 2 - 1000;
					this.ExpCostNext10000Levels = (this.Level + 10000) * (this.Level + 10001) / 2 - this.Level * (this.Level + 1) / 2 - 10000;
				}
				else
				{
					for (int i = 0; i < 1000; i++)
					{
						if (i < 10)
						{
							this.ExpCostNext10Levels += this.ExpCost(this.Level + i);
						}
						if (i < 100)
						{
							this.ExpCostNext100Levels += this.ExpCost(this.Level + i);
						}
						this.ExpCostNext1000Levels += this.ExpCost(this.Level + i);
					}
				}
			}
		}

		public class Ball
		{
			public float Pos;

			public bool DidDamage;
		}

		public AfkyGame.StatExp Power = new AfkyGame.StatExp();

		public AfkyGame.StatExp FiringSpeed = new AfkyGame.StatExp();

		public AfkyGame.StatExp CloneHp = new AfkyGame.StatExp
		{
			ExpModi = 2
		};

		public AfkyGame.StatExp CloneCount = new AfkyGame.StatExp
		{
			ExpModi = 1
		};

		public CDouble CloneCurrentHP = 1;

		public CDouble TimeToNextFire = 0;

		public CDouble Exp = 0;

		public CDouble KilledClones = 0;

		public CDouble ExpMulti = 1;

		public bool FiringPhase;

		public CDouble TimeAfterFire = 0;

		public List<AfkyGame.Ball> AuraballPositions = new List<AfkyGame.Ball>();

		public CDouble ShootKillCount = 0;

		public int PosFirstClone = 500;

		public float deadCloneTimer;

		public long FiringTimeMs
		{
			get
			{
				if (this.FiringSpeed.Level > 3800)
				{
					return 200L;
				}
				return (long)(4000 - this.FiringSpeed.Level.ToInt());
			}
		}

		public CDouble CloneExp
		{
			get
			{
				return this.CloneHp.Level;
			}
		}

		public CDouble CloneExpTotal
		{
			get
			{
				return Math.Pow(this.CloneHp.Level.Double, 1.1) * Math.Pow(this.ShootKillCount.Double, 0.9) * this.ExpMulti;
			}
		}

		public void InitExpCost()
		{
			this.Power.InitExpCost();
			this.FiringSpeed.InitExpCost();
			this.CloneHp.InitExpCost();
			this.CloneCount.InitExpCost();
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.Power.Serialize());
			Conv.AppendValue(stringBuilder, "b", this.FiringSpeed.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.CloneHp.Serialize());
			Conv.AppendValue(stringBuilder, "d", this.CloneCount.Serialize());
			Conv.AppendValue(stringBuilder, "e", this.CloneCurrentHP.Serialize());
			Conv.AppendValue(stringBuilder, "f", this.TimeToNextFire.Serialize());
			Conv.AppendValue(stringBuilder, "g", this.Exp.Serialize());
			Conv.AppendValue(stringBuilder, "h", this.KilledClones.Serialize());
			Conv.AppendValue(stringBuilder, "i", this.ExpMulti.Serialize());
			return Conv.ToBase64(stringBuilder.ToString(), "StatExp");
		}

		internal static AfkyGame FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				return new AfkyGame();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "AfkyGame");
			AfkyGame afkyGame = new AfkyGame();
			afkyGame.Power = AfkyGame.StatExp.FromString(Conv.getStringFromParts(parts, "a"));
			afkyGame.FiringSpeed = AfkyGame.StatExp.FromString(Conv.getStringFromParts(parts, "b"));
			afkyGame.CloneHp = AfkyGame.StatExp.FromString(Conv.getStringFromParts(parts, "c"));
			afkyGame.CloneCount = AfkyGame.StatExp.FromString(Conv.getStringFromParts(parts, "d"));
			afkyGame.CloneCurrentHP = Conv.getCDoubleFromParts(parts, "e", false);
			afkyGame.TimeToNextFire = Conv.getCDoubleFromParts(parts, "f", false);
			afkyGame.Exp = Conv.getCDoubleFromParts(parts, "g", false);
			afkyGame.KilledClones = Conv.getCDoubleFromParts(parts, "h", false);
			afkyGame.ExpMulti = Conv.getCDoubleFromParts(parts, "i", false);
			if (afkyGame.ExpMulti < 1)
			{
				afkyGame.ExpMulti = 1;
			}
			return afkyGame;
		}

		public string AddOfflineExp(long ms, GameState state)
		{
			long value = ms / this.FiringTimeMs;
			CDouble rightSide = this.CloneHp.Level + 1;
			CDouble leftSide = this.Power.Level * value;
			CDouble cDouble = 0;
			if (leftSide > rightSide)
			{
				cDouble = (leftSide / rightSide).ToInt();
			}
			if (cDouble > this.CloneCount.Level * value)
			{
				cDouble = this.CloneCount.Level * value;
			}
			cDouble = cDouble.ToLong();
			if (cDouble == 0)
			{
				return string.Empty;
			}
			CDouble cDouble2 = Math.Pow(this.CloneHp.Level.Double, 1.1) * Math.Pow(cDouble.Double, 0.9) * this.ExpMulti;
			this.Exp += cDouble2;
			this.KilledClones += cDouble;
			state.Statistic.AfkyClonesKilled += cDouble;
			if (state.Statistic.AfkyClonesKilled < this.KilledClones)
			{
				state.Statistic.AfkyClonesKilled = this.KilledClones;
			}
			return string.Concat(new string[]
			{
				"Afky god killed ",
				cDouble.GuiText,
				" shadow clones and got ",
				cDouble2.GuiText,
				" exp."
			});
		}

		public void DamageClone()
		{
			CDouble cDouble = this.Power.Level;
			CDouble cDouble2 = 0;
			if (cDouble > this.CloneHp.Level)
			{
				cDouble2 = (cDouble / this.CloneHp.Level).Floor().ToInt();
				cDouble -= cDouble2 * this.CloneHp.Level;
			}
			this.CloneCurrentHP -= cDouble;
			if (this.CloneCurrentHP <= 0)
			{
				this.CloneCurrentHP = this.CloneHp.Level + this.CloneCurrentHP;
				cDouble2 = ++cDouble2;
			}
			if (cDouble2 > this.CloneCount.Level)
			{
				cDouble2 = this.CloneCount.Level;
			}
			this.ShootKillCount = cDouble2;
			this.Exp += this.CloneExpTotal;
			this.KilledClones += cDouble2;
			App.State.Statistic.AfkyClonesKilled += cDouble2;
		}

		public void Update(long ms)
		{
			this.TimeToNextFire -= ms;
			if (this.FiringPhase)
			{
				this.TimeAfterFire += ms;
			}
			if (this.TimeToNextFire <= 0)
			{
				this.AuraballPositions.Add(new AfkyGame.Ball
				{
					Pos = 330f
				});
				this.TimeToNextFire = this.FiringTimeMs;
				this.TimeAfterFire = 0;
				this.FiringPhase = true;
			}
			if (this.FiringPhase && this.TimeAfterFire > this.FiringTimeMs / 2L)
			{
				this.TimeAfterFire = 0;
				this.FiringPhase = false;
			}
			this.deadCloneTimer -= (float)(ms / 6L);
			for (int i = 0; i < this.AuraballPositions.Count; i++)
			{
				float num = (float)ms;
				this.AuraballPositions[i].Pos += num;
				if (this.AuraballPositions[i].Pos > (float)this.PosFirstClone && !this.AuraballPositions[i].DidDamage)
				{
					this.AuraballPositions[i].DidDamage = true;
					this.DamageClone();
					this.deadCloneTimer = 200f;
				}
				if (this.AuraballPositions[i].Pos > 800f)
				{
					this.AuraballPositions.Remove(this.AuraballPositions[i]);
					break;
				}
			}
		}
	}
}
