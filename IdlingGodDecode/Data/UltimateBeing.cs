using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class UltimateBeing
	{
		public CDouble NextMultiplier = new CDouble();

		private StringBuilder crystalFightBuilder = new StringBuilder();

		private List<int> skillOrder = new List<int>
		{
			1,
			2,
			3,
			1,
			1,
			2,
			1,
			1,
			2,
			3
		};

		public double HPPercent
		{
			get;
			set;
		}

		public int TimesDefeated
		{
			get;
			set;
		}

		public int Tier
		{
			get;
			set;
		}

		public long TimeUntilComeBack
		{
			get;
			set;
		}

		public bool IsAvailable
		{
			get;
			set;
		}

		public int PowerBoni
		{
			get
			{
				int num = this.Tier * (10 - this.TimesDefeated);
				if (num < this.Tier)
				{
					num = this.Tier;
				}
				return num;
			}
		}

		public long TimeUntilComeBackBase
		{
			get
			{
				long num = (long)(this.Tier * 3600000);
				if (App.State != null)
				{
					int num2 = App.State.Statistic.NoRbChallengesFinished.ToInt();
					if (num2 > 20)
					{
						num2 = 20;
					}
					num = num * (long)(100 - num2) / 100L;
				}
				return num;
			}
		}

		public string Name
		{
			get
			{
				if (this.Tier == 1)
				{
					return "Planet Eater";
				}
				if (this.Tier == 2)
				{
					return "Godly Tribunal";
				}
				if (this.Tier == 3)
				{
					return "Living Sun";
				}
				if (this.Tier == 4)
				{
					return "God Above All";
				}
				if (this.Tier == 5)
				{
					return "ITRTG";
				}
				return string.Empty;
			}
		}

		public string SkillName1
		{
			get
			{
				if (this.Tier == 1)
				{
					return "Sweet Tongue";
				}
				if (this.Tier == 2)
				{
					return "Judge Laser";
				}
				if (this.Tier == 3)
				{
					return "Magmacalypse";
				}
				if (this.Tier == 4)
				{
					return "Atomification";
				}
				if (this.Tier == 5)
				{
					return "Lazy Punch";
				}
				return string.Empty;
			}
		}

		public string SkillName2
		{
			get
			{
				if (this.Tier == 1)
				{
					return "Big Hunger";
				}
				if (this.Tier == 2)
				{
					return "Gods Judgment";
				}
				if (this.Tier == 3)
				{
					return "Burning Hell";
				}
				if (this.Tier == 4)
				{
					return "Ragnarok";
				}
				if (this.Tier == 5)
				{
					return "Idle Rule";
				}
				return string.Empty;
			}
		}

		public string SkillName3
		{
			get
			{
				if (this.Tier == 1)
				{
					return "Mighty Stomp";
				}
				if (this.Tier == 2)
				{
					return "Final Judgement";
				}
				if (this.Tier == 3)
				{
					return "Supernova";
				}
				if (this.Tier == 4)
				{
					return "Divine Nothingness";
				}
				if (this.Tier == 5)
				{
					return "Ultra Epic Idle Finger Snap";
				}
				return string.Empty;
			}
		}

		public string Description
		{
			get
			{
				if (this.Tier == 1)
				{
					return "A really huge being. So big that you need to look at it from a distance further than 1000 miles to be able to fully see it. \nHis hobby is eating planets. Impossible to defeat alone, but if you attack him with all your clones, you might stand a chance! lowerTextYou killed it " + this.TimesDefeated + " times.";
				}
				if (this.Tier == 2)
				{
					return "Only takes orders from Above. His power is unbelievable. Even if he is much smaller than the planet eater, he can kill it in one hit! lowerTextYou killed it " + this.TimesDefeated + " times.";
				}
				if (this.Tier == 3)
				{
					return "A mystic being. His origin is unknown. He once flew to a sun and touched it with his naked hands. \nInstead of being burned, he absorbed all the heat and made it his own. Since then even Godly Tribunal fears him. lowerTextYou killed it " + this.TimesDefeated + " times.";
				}
				if (this.Tier == 4)
				{
					return "Legends say he created everything. Not only all other gods but also multiple universes. As he created everything, he might also be able to destroy everything.\nIt is said nothing will ever be able to match his power. Can you prove it wrong? lowerTextYou killed it " + this.TimesDefeated + " times.";
				}
				if (this.Tier == 5)
				{
					return "If 'God Above All' created everything, who created him? The answer lies here! Suddenly he stands before you. Forget everything you defeated before. It is there! The one - the only - the last challenge? \nIf you defeat ITRTG, a button to create a clear data will appear. With this you can export the clear data to a file and import it to my future games for some extra goodies! lowerTextYou killed it " + this.TimesDefeated + " times.";
				}
				return string.Empty;
			}
		}

		public static List<UltimateBeing> Initial
		{
			get
			{
				List<UltimateBeing> list = new List<UltimateBeing>();
				for (int i = 0; i < 5; i++)
				{
					list.Add(new UltimateBeing
					{
						Tier = i + 1
					});
				}
				return list;
			}
		}

		public UltimateBeing()
		{
			this.HPPercent = 100.0;
		}

		public void ComeBack()
		{
			this.HPPercent = 100.0;
			this.TimeUntilComeBack = this.TimeUntilComeBackBase;
		}

		public void UpdateDuration(long ms)
		{
			if (this.TimeUntilComeBack <= 0L)
			{
				this.ComeBack();
			}
			this.TimeUntilComeBack -= ms;
		}

		internal string Fight(GameState gameState, int bpAtk, int bpDef, int powersurge, CDouble shadowCloneCount, bool crystalFactoryAttack = false)
		{
			gameState.HomePlanet.BaalPower -= bpAtk + bpDef;
			int num = gameState.ClonesPlanetMod + bpAtk * 50 + powersurge;
			int num2 = gameState.ClonesPlanetMod + bpDef * 50 + powersurge;
			StringBuilder stringBuilder = new StringBuilder();
			int num3 = 1;
			int num4 = 0;
			if (crystalFactoryAttack)
			{
				this.crystalFightBuilder = new StringBuilder();
				CultureInfo currentCulture = CultureInfo.CurrentCulture;
				this.crystalFightBuilder.Append(DateTime.Now.ToString(currentCulture) + "\n");
				if (shadowCloneCount == 0)
				{
					this.crystalFightBuilder.Append(this.Name + " attacked your crystal factory and you lost the fight!");
				}
			}
			while (gameState.CurrentHealth > 0 && shadowCloneCount > 0 && this.HPPercent > 0.0)
			{
				int num5 = 7000;
				if (this.Tier == 2)
				{
					num5 = 16000;
				}
				else if (this.Tier == 3)
				{
					num5 = 25000;
				}
				else if (this.Tier == 4)
				{
					num5 = 40000;
				}
				else if (this.Tier == 5)
				{
					num5 = 60000;
				}
				int num6 = this.skillOrder[(num3 - 1) % 10];
				string value = this.SkillName1;
				if (num6 == 3)
				{
					num5 *= 4;
					value = this.SkillName3;
				}
				else if (num6 == 2)
				{
					num5 *= 2;
					value = this.SkillName2;
				}
				long num7 = (long)UnityEngine.Random.Range((int)((double)num5 * 0.9), (int)((double)num5 * 1.1));
				if (crystalFactoryAttack && gameState.PremiumBoni.CrystalBonusDefender > 0)
				{
					num7 = (num7 * (100 - gameState.PremiumBoni.CrystalBonusDefender) / 100).ToLong();
				}
				int num8 = (int)((double)(num7 / 4000L) - shadowCloneCount.Double / 40000.0);
				if (num8 < 0)
				{
					num8 = 0;
				}
				num7 = num7 * 100L / (long)num2;
				if (num7 >= shadowCloneCount)
				{
					num7 = (long)shadowCloneCount.ToInt();
					num8 = 100;
				}
				if (crystalFactoryAttack)
				{
					gameState.Ext.Factory.DefenderClones -= num7;
				}
				else
				{
					gameState.CurrentHealth -= gameState.MaxHealth / 100 * num8;
					gameState.HomePlanet.ShadowCloneCount -= num7;
				}
				shadowCloneCount -= (int)num7;
				num4 += (int)num7;
				shadowCloneCount.Round();
				gameState.Clones.RemoveUsedShadowClones((int)num7);
				gameState.Clones.Count -= (int)num7;
				gameState.Clones.TotalClonesKilled += (int)num7;
				gameState.Statistic.TotalShadowClonesDied += num7;
				stringBuilder.Append(num3).Append(". ").Append(this.Name).Append(" uses ").Append(value).Append(", kills ").Append(num7).Append(" Shadow Clones and deals ").Append(num8).Append(" % damage to your hp.\n");
				if (gameState.CurrentHealth == 0 || shadowCloneCount <= 0)
				{
					stringBuilder.Append("You lost the fight!");
					this.crystalFightBuilder.Append(this.Name + " attacked your crystal factory and your defender clones lost the fight!");
				}
				else
				{
					double num9 = shadowCloneCount.Double / 20000.0 / (double)this.Tier + 1.0;
					if (shadowCloneCount <= 100000)
					{
						num9 *= 2.0;
					}
					else if (shadowCloneCount <= 150000)
					{
						num9 *= 1.8;
					}
					else if (shadowCloneCount <= 200000)
					{
						num9 *= 1.6;
					}
					else if (shadowCloneCount <= 250000)
					{
						num9 *= 1.4;
					}
					else if (shadowCloneCount <= 300000)
					{
						num9 *= 1.2;
					}
					num9 = num9 * (double)num / 100.0;
					string str = "Shadow Clones Power Charge";
					if (num6 != 1)
					{
						num9 *= 1.5;
						str = "Ultimate Shadow Clone Blast";
					}
					stringBuilder.Append("You used " + str + " and dealt ").Append((int)num9).Append(" % damage to ").Append(this.Name).Append("\n");
					if (num9 > this.HPPercent)
					{
						num9 = this.HPPercent;
					}
					this.HPPercent -= num9;
					if (this.HPPercent <= 0.0)
					{
						Statistic expr_585 = gameState.Statistic;
						expr_585.UBsDefeated = ++expr_585.UBsDefeated;
						gameState.HomePlanet.UBMultiplier += this.NextMultiplier;
						this.TimesDefeated++;
						gameState.HomePlanet.RecalculateMultiplier();
						CDouble cDouble = 100000;
						foreach (Fight current in gameState.AllFights)
						{
							if (current.IsAvailable)
							{
								cDouble *= 2;
							}
						}
						if (gameState.Generator != null && gameState.Generator.IsBuilt)
						{
							cDouble += gameState.Generator.DivinitySec * 3600;
						}
						cDouble = cDouble * this.Tier / 2;
						gameState.Money += cDouble;
						this.crystalFightBuilder.Append(string.Concat(new object[]
						{
							this.Name,
							" attacked your defender clones and you won the fight!\nYou lost a total of ",
							num4,
							" clones and gained "
						})).Append(cDouble.ToGuiText(true)).Append(" Divinity, your Planet Stat Multiplier increased by ").Append(this.NextMultiplier.ToGuiText(true)).Append(" %");
						stringBuilder.Append("You won the fight!\nYou gained ").Append(cDouble.ToGuiText(true)).Append(" Divinity, your Planet Stat Multiplier increased by ").Append(this.NextMultiplier.ToGuiText(true)).Append(" %");
						bool flag = false;
						if (this.TimesDefeated % 5 == 0 && this.Tier == 1)
						{
							flag = true;
						}
						else if ((this.TimesDefeated % 5 == 0 && this.Tier == 2) || (this.TimesDefeated % 5 == 3 && this.Tier == 2))
						{
							flag = true;
						}
						else if (this.TimesDefeated % 5 != 0 && this.Tier == 3 && this.TimesDefeated % 5 != 3 && this.Tier == 3)
						{
							flag = true;
						}
						else if (this.TimesDefeated % 5 != 3 && this.Tier == 4)
						{
							flag = true;
						}
						else if (this.Tier == 5)
						{
							flag = true;
						}
						Log.Info(string.Concat(new object[]
						{
							"times defeated: ",
							this.TimesDefeated,
							", drop GP = ",
							flag
						}));
						if (flag)
						{
							gameState.PremiumBoni.GodPower++;
							gameState.HomePlanet.TotalGainedGodPower++;
							gameState.PremiumBoni.CheckIfGPIsAdjusted();
							stringBuilder.Append(" and got 1 God Power!");
							this.crystalFightBuilder.Append("\nYou received 1 God Power.");
						}
						if (gameState.IsCrystalFactoryAvailable)
						{
							CDouble cDouble2 = this.Tier * 100;
							gameState.Ext.Factory.Energy += cDouble2;
							stringBuilder.Append("\nYour crystal factory received " + cDouble2.GuiText + " energy!");
							this.crystalFightBuilder.Append("\nYour crystal factory received " + cDouble2.GuiText + " energy!");
						}
						gameState.HomePlanet.InitUBMultipliers();
					}
				}
				num3++;
			}
			if (!crystalFactoryAttack)
			{
				return stringBuilder.ToString();
			}
			return this.crystalFightBuilder.ToString();
		}

		public void SetNextMultiplier(CDouble currentMultiplier)
		{
			CDouble leftSide = currentMultiplier + 100;
			this.NextMultiplier = leftSide * this.PowerBoni / 100;
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.HPPercent.ToString());
			Conv.AppendValue(stringBuilder, "b", this.TimesDefeated.ToString());
			Conv.AppendValue(stringBuilder, "c", this.Tier.ToString());
			Conv.AppendValue(stringBuilder, "d", this.TimeUntilComeBack.ToString());
			Conv.AppendValue(stringBuilder, "e", this.IsAvailable.ToString());
			return Conv.ToBase64(stringBuilder.ToString(), "UltimateBeing");
		}

		internal static UltimateBeing FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				return new UltimateBeing();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "UltimateBeing");
			return new UltimateBeing
			{
				HPPercent = Conv.getDoubleFromParts(parts, "a"),
				TimesDefeated = Conv.getIntFromParts(parts, "b"),
				Tier = Conv.getIntFromParts(parts, "c"),
				TimeUntilComeBack = Conv.getLongFromParts(parts, "d"),
				IsAvailable = Conv.getStringFromParts(parts, "e").ToLower().Equals("true")
			};
		}
	}
}
