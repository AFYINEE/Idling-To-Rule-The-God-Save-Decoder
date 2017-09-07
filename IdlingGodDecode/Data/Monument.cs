using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
	public class Monument : TrainingBase
	{
		public enum MonumentType
		{
			mighty_statue,
			mystic_garden,
			tomb_of_god,
			everlasting_lighthouse,
			godly_statue,
			pyramids_of_power,
			temple_of_god,
			black_hole
		}

		public Monument.MonumentType TypeEnum;

		public bool IsPaid;

		public int StopAt;

		public string stopAtString = "0";

		private string description = string.Empty;

		public string MissingItems = string.Empty;

		public MonumentUpgrade Upgrade
		{
			get;
			set;
		}

		public CDouble PhysicalPowerBase
		{
			get;
			set;
		}

		public CDouble MysticPowerBase
		{
			get;
			set;
		}

		public CDouble BattlePowerBase
		{
			get;
			set;
		}

		public CDouble CreatingPowerBase
		{
			get;
			set;
		}

		public CDouble PhysicalPower
		{
			get
			{
				return this.Upgrade.PhysicalPower * this.PhysicalPowerBase;
			}
		}

		public CDouble MysticPower
		{
			get
			{
				return this.Upgrade.MysticPower * this.MysticPowerBase;
			}
		}

		public CDouble BattlePower
		{
			get
			{
				return this.Upgrade.BattlePower * this.BattlePowerBase;
			}
		}

		public CDouble CreatingPower
		{
			get
			{
				return this.Upgrade.CreatingPower * this.CreatingPowerBase;
			}
		}

		public string StopAtString
		{
			get
			{
				return this.stopAtString;
			}
			set
			{
				try
				{
					if (!string.IsNullOrEmpty(value) && value.StartsWith("-"))
					{
						value = "0";
					}
					int.TryParse(value, out this.StopAt);
					this.stopAtString = this.StopAt.ToString();
				}
				catch (Exception)
				{
					this.stopAtString = "0";
					this.StopAt = 0;
				}
			}
		}

		public bool IsAvailable
		{
			get
			{
				int num = this.EnumValue - 1;
				return num == -1 || App.State.AllMonuments[num].Level > 0;
			}
		}

		public string Name
		{
			get
			{
				if (!this.IsAvailable)
				{
					return "???";
				}
				return EnumName.Name(this.TypeEnum);
			}
		}

		public string Description
		{
			get
			{
				if (!this.IsAvailable)
				{
					return "You still need to think about this monument.";
				}
				if (!string.IsNullOrEmpty(this.description) && !this.ShouldUpdateText)
				{
					return this.description;
				}
				StringBuilder stringBuilder = new StringBuilder("\nTo create 1 ").Append(this.Name).Append(", you need:\n");
				foreach (CreationCost current in this.RequiredCreations(this.Level))
				{
					stringBuilder.Append(current.CountNeeded.ToGuiText(true)).Append(" x ").Append(EnumName.Name(current.TypeEnum)).Append(", ");
				}
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
				switch (this.TypeEnum)
				{
				case Monument.MonumentType.mighty_statue:
					this.description = string.Concat(new string[]
					{
						"A colossal statue. It reminds you of yourself. ",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\nPhysical multi (now) x (1 + ",
						this.PhysicalPower.ToGuiText(true),
						" x Count)."
					});
					break;
				case Monument.MonumentType.mystic_garden:
					this.description = string.Concat(new string[]
					{
						"A really big and beautiful garden.",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\nMystic multi (now) x (1 + ",
						this.MysticPower.ToGuiText(true),
						" x Count)."
					});
					break;
				case Monument.MonumentType.tomb_of_god:
					this.description = string.Concat(new string[]
					{
						"A really big tomb of your previous reincarnation. Take god-care of it!",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\nBattle multi (now) x (1 + ",
						this.BattlePower.ToGuiText(true),
						" x Count)."
					});
					break;
				case Monument.MonumentType.everlasting_lighthouse:
					this.description = string.Concat(new string[]
					{
						"A building which illuminates a whole ocean.",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\nCreating multi (now) x (1 + ",
						this.CreatingPower.ToGuiText(true),
						" x Count)."
					});
					break;
				case Monument.MonumentType.godly_statue:
					this.description = string.Concat(new string[]
					{
						"A bigger version of the mighty statue. As big as a mountain.",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\n4 power stats x (1 + ",
						this.PhysicalPower.ToGuiText(true),
						" x count)."
					});
					break;
				case Monument.MonumentType.pyramids_of_power:
					this.description = string.Concat(new string[]
					{
						"A few big pyramids to make you feel almighty.",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\n4 power stats x (1 + ",
						this.PhysicalPower.ToGuiText(true),
						" x count)."
					});
					break;
				case Monument.MonumentType.temple_of_god:
					this.description = string.Concat(new string[]
					{
						"That's your temple! It makes you feel invincible. Somehow it's bigger than your pyramids.",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\n4 power stats x (1 + ",
						this.PhysicalPower.ToGuiText(true),
						" x count)."
					});
					break;
				case Monument.MonumentType.black_hole:
					this.description = "Sucks in all the power in whole galaxies, just to make you stronger. Has a 25% chance to generate 1 God Power every hour. (Online only and maxed at 1 gp/hour with 4 black holes)";
					if (App.State != null && App.State.Statistic.BlackHoleChallengesFinished > 0)
					{
						CDouble cDouble = App.State.Statistic.BlackHoleChallengesFinished * 5;
						if (this.Level < App.State.Statistic.BlackHoleChallengesFinished)
						{
							cDouble = this.Level * 5;
						}
						if (cDouble > 200)
						{
							cDouble = 200;
						}
						this.description = string.Concat(new object[]
						{
							this.description,
							"\nWith your BHCs finished and count, it has an additional ",
							cDouble,
							" % chance to generate godpower."
						});
					}
					this.description = string.Concat(new string[]
					{
						this.description,
						stringBuilder.ToString(),
						this.DurationInfo,
						"\n4 power stats x (1 + ",
						this.PhysicalPower.ToGuiText(true),
						" x count)."
					});
					break;
				}
				this.ShouldUpdateText = false;
				return this.description;
			}
		}

		private string DurationInfo
		{
			get
			{
				int num = this.ShadowCloneCount.ToInt();
				if (num == 0)
				{
					num = 1000;
				}
				long num2 = this.DurationInMS(1);
				CDouble rightSide = (100 + App.State.PremiumBoni.BuildingSpeedUpPercent(true)) / 100.0 * App.State.PremiumBoni.MonumentBuildTimeDivider;
				num2 = ((num2 / rightSide - this.CurrentDuration / rightSide) / num).ToLong();
				return "lowerTextTime to build: " + Conv.MsToGuiText(num2, true) + string.Format(" ({0} Clones)", num);
			}
		}

		public CDouble TotalMultiPhysical
		{
			get
			{
				return this.PhysicalPower * this.Level;
			}
		}

		public CDouble TotalMultiMystic
		{
			get
			{
				return this.MysticPower * this.Level;
			}
		}

		public CDouble TotalMultiBattle
		{
			get
			{
				return this.BattlePower * this.Level;
			}
		}

		public CDouble TotalMultiCreating
		{
			get
			{
				return this.CreatingPower * this.Level;
			}
		}

		public CDouble TotalRebirthMultiPhysical
		{
			get
			{
				return this.PhysicalPowerBase * this.Upgrade.PhysicalPowerRebirth * this.Level;
			}
		}

		public CDouble TotalRebirthMultiMystic
		{
			get
			{
				return this.MysticPowerBase * this.Upgrade.MysticPowerRebirth * this.Level;
			}
		}

		public CDouble TotalRebirthMultiBattle
		{
			get
			{
				return this.BattlePowerBase * this.Upgrade.BattlePowerRebirth * this.Level;
			}
		}

		public CDouble TotalRebirthMultiCreating
		{
			get
			{
				return this.CreatingPowerBase * this.Upgrade.CreatingPowerRebirth * this.Level;
			}
		}

		public Monument()
		{
		}

		public Monument(Monument.MonumentType type)
		{
			this.Init(type);
		}

		private void Init(Monument.MonumentType type)
		{
			this.TypeEnum = type;
			this.EnumValue = (int)type;
			this.PhysicalPowerBase = new CDouble();
			this.MysticPowerBase = new CDouble();
			this.BattlePowerBase = new CDouble();
			this.CreatingPowerBase = new CDouble();
			this.Upgrade = new MonumentUpgrade(type);
			switch (this.TypeEnum)
			{
			case Monument.MonumentType.mighty_statue:
				this.PhysicalPowerBase = 1;
				break;
			case Monument.MonumentType.mystic_garden:
				this.MysticPowerBase = 6;
				break;
			case Monument.MonumentType.tomb_of_god:
				this.BattlePowerBase = 30;
				break;
			case Monument.MonumentType.everlasting_lighthouse:
				this.CreatingPowerBase = 150;
				break;
			case Monument.MonumentType.godly_statue:
				this.PhysicalPowerBase = 200;
				this.MysticPowerBase = 200;
				this.BattlePowerBase = 200;
				this.CreatingPowerBase = 200;
				break;
			case Monument.MonumentType.pyramids_of_power:
				this.PhysicalPowerBase = 600;
				this.MysticPowerBase = 600;
				this.BattlePowerBase = 600;
				this.CreatingPowerBase = 600;
				break;
			case Monument.MonumentType.temple_of_god:
				this.PhysicalPowerBase = 1500;
				this.MysticPowerBase = 1500;
				this.BattlePowerBase = 1500;
				this.CreatingPowerBase = 1500;
				break;
			case Monument.MonumentType.black_hole:
				this.PhysicalPowerBase = 100000000;
				this.MysticPowerBase = 100000000;
				this.BattlePowerBase = 100000000;
				this.CreatingPowerBase = 100000000;
				break;
			}
		}

		internal static List<Monument> Initial()
		{
			return new List<Monument>
			{
				new Monument(Monument.MonumentType.mighty_statue),
				new Monument(Monument.MonumentType.mystic_garden),
				new Monument(Monument.MonumentType.tomb_of_god),
				new Monument(Monument.MonumentType.everlasting_lighthouse),
				new Monument(Monument.MonumentType.godly_statue),
				new Monument(Monument.MonumentType.pyramids_of_power),
				new Monument(Monument.MonumentType.temple_of_god),
				new Monument(Monument.MonumentType.black_hole)
			};
		}

		public List<CreationCost> RequiredCreations(CDouble level)
		{
			CDouble rightSide = 1;
			if (level > 0)
			{
				rightSide = level * 5;
			}
			List<CreationCost> list = new List<CreationCost>();
			switch (this.TypeEnum)
			{
			case Monument.MonumentType.mighty_statue:
				list.Add(new CreationCost(2000 * rightSide, Creation.CreationType.Stone));
				break;
			case Monument.MonumentType.mystic_garden:
				list.Add(new CreationCost(250 * rightSide, Creation.CreationType.Water));
				list.Add(new CreationCost(100 * rightSide, Creation.CreationType.Plant));
				break;
			case Monument.MonumentType.tomb_of_god:
				list.Add(new CreationCost(1000 * rightSide, Creation.CreationType.Stone));
				list.Add(new CreationCost(1 * rightSide, Creation.CreationType.Human));
				break;
			case Monument.MonumentType.everlasting_lighthouse:
				list.Add(new CreationCost(250000 * rightSide, Creation.CreationType.Light));
				list.Add(new CreationCost(50000 * rightSide, Creation.CreationType.Stone));
				break;
			case Monument.MonumentType.godly_statue:
				list.Add(new CreationCost(1 * rightSide, Creation.CreationType.Mountain));
				list.Add(new CreationCost(100000 * rightSide, Creation.CreationType.Stone));
				list.Add(new CreationCost(25000 * rightSide, Creation.CreationType.Water));
				break;
			case Monument.MonumentType.pyramids_of_power:
				list.Add(new CreationCost(50000 * rightSide, Creation.CreationType.Water));
				list.Add(new CreationCost(500000 * rightSide, Creation.CreationType.Stone));
				list.Add(new CreationCost(200000 * rightSide, Creation.CreationType.Light));
				break;
			case Monument.MonumentType.temple_of_god:
				list.Add(new CreationCost(1000000 * rightSide, Creation.CreationType.Stone));
				list.Add(new CreationCost(100000 * rightSide, Creation.CreationType.Light));
				list.Add(new CreationCost(2000 * rightSide, Creation.CreationType.Plant));
				list.Add(new CreationCost(1000 * rightSide, Creation.CreationType.Tree));
				list.Add(new CreationCost(60000 * rightSide, Creation.CreationType.Water));
				list.Add(new CreationCost(1000 * rightSide, Creation.CreationType.Fish));
				break;
			case Monument.MonumentType.black_hole:
			{
				CDouble cDouble = 25 * rightSide;
				if (App.State != null && App.State.Statistic.BlackHoleChallengesFinished > 0)
				{
					CDouble cDouble2 = App.State.Statistic.BlackHoleChallengesFinished * 2;
					if (cDouble2 > 80)
					{
						cDouble2 = 80;
					}
					cDouble = cDouble * (100 - cDouble2) / 100;
				}
				cDouble.Round();
				list.Add(new CreationCost(cDouble, Creation.CreationType.Galaxy));
				break;
			}
			}
			return list;
		}

		public new double getPercent()
		{
			return (double)this.CurrentDuration / (double)this.DurationInMS(1);
		}

		public new void UpdateDuration(long ms)
		{
			if (this.Upgrade.UpdateDuration(ms))
			{
				this.AddUpgradeLevel();
			}
			if (App.State == null || this.ShadowCloneCount == 0)
			{
				return;
			}
			if (this.Level >= this.StopAt && this.StopAt != 0)
			{
				CDouble shadowCloneCount = this.ShadowCloneCount;
				App.State.Clones.RemoveUsedShadowClones(shadowCloneCount);
				this.ShadowCloneCount = 0;
				if (this.Upgrade.StopAt > 0 && this.Upgrade.IsAvailable)
				{
					this.Upgrade.AddCloneCount(shadowCloneCount);
				}
				else
				{
					Monument monument = App.State.AllMonuments.FirstOrDefault((Monument x) => x.TypeEnum == this.TypeEnum + 1);
					if (monument != null && monument.StopAt > 0)
					{
						monument.AddCloneCount(shadowCloneCount);
					}
				}
				return;
			}
			if (!this.IsPaid)
			{
				bool flag = false;
				StringBuilder stringBuilder = new StringBuilder("You still need:\n");
				using (List<CreationCost>.Enumerator enumerator = this.RequiredCreations(this.Level).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CreationCost cost = enumerator.Current;
						Creation creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
						cost.CountNeeded.Round();
						creation.count.Round();
						if (cost.CountNeeded > creation.Count)
						{
							if (App.State.IsBuyUnlocked && App.State.GameSettings.AutoBuyCreationsForMonuments && creation.CanBuy)
							{
								CDouble cDouble = cost.CountNeeded - creation.Count;
								CDouble rightSide = cDouble * creation.BuyCost * (120 - App.State.PremiumBoni.AutoBuyCostReduction) / 100;
								if (App.State.Money >= rightSide)
								{
									App.State.Money -= rightSide;
									creation.count += cDouble;
									App.State.Statistic.TotalMoneySpent += rightSide;
								}
								else
								{
									flag = true;
									stringBuilder.Append(creation.Name).Append(" x ").Append((cost.CountNeeded - creation.Count).ToGuiText(true)).Append("\n");
								}
							}
							else
							{
								flag = true;
								stringBuilder.Append(creation.Name).Append(" x ").Append((cost.CountNeeded - creation.Count).ToGuiText(true)).Append("\n");
							}
						}
					}
				}
				if (flag)
				{
					stringBuilder.Append("to build ").Append(this.Name).ToString();
					if (!App.State.GameSettings.StickyClones)
					{
						GuiBase.ShowToast(stringBuilder.ToString());
						App.State.Clones.RemoveUsedShadowClones(this.ShadowCloneCount);
						this.ShadowCloneCount = 0;
					}
					else
					{
						this.MissingItems = "\n\n" + stringBuilder.ToString();
					}
					return;
				}
				using (List<CreationCost>.Enumerator enumerator2 = this.RequiredCreations(this.Level).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						CreationCost cost = enumerator2.Current;
						Creation creation2 = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
						creation2.Count -= cost.CountNeeded;
					}
				}
				this.IsPaid = true;
			}
			this.MissingItems = string.Empty;
			this.CurrentDuration += (ms * (long)this.ShadowCloneCount.ToInt() * (long)App.State.PremiumBoni.MonumentBuildTimeDivider * (100 + App.State.PremiumBoni.BuildingSpeedUpPercent(true)) / 100).ToLong();
			if (this.CurrentDuration > this.DurationInMS(1))
			{
				this.CurrentDuration = 0L;
				this.Level = ++this.Level;
				this.IsPaid = false;
				Statistic expr_4D4 = App.State.Statistic;
				expr_4D4.MonumentsCreated = ++expr_4D4.MonumentsCreated;
				Leaderboards.SubmitStat(LeaderBoardType.Monuments, App.State.Statistic.MonumentsCreated.ToInt(), false);
				if (this.TypeEnum == Monument.MonumentType.temple_of_god && this.Level == 1)
				{
					GuiBase.ShowContentUnlocked("You can now build a divinity generator!");
				}
				this.AddStatBoni();
				if (App.State.GameSettings.StopMonumentBuilding)
				{
					App.State.Clones.RemoveUsedShadowClones(this.ShadowCloneCount);
					this.ShadowCloneCount = 0;
				}
				if (this.TypeEnum == Monument.MonumentType.temple_of_god)
				{
					App.State.Generator.IsAvailable = true;
				}
			}
		}

		internal void AddUpgradeLevel()
		{
			CDouble physicalPower = this.Upgrade.PhysicalPower;
			CDouble mysticPower = this.Upgrade.MysticPower;
			CDouble battlePower = this.Upgrade.BattlePower;
			CDouble creatingPower = this.Upgrade.CreatingPower;
			MonumentUpgrade expr_36 = this.Upgrade;
			expr_36.Level = ++expr_36.Level;
			Statistic expr_50 = App.State.Statistic;
			expr_50.TotalUpgrades = ++expr_50.TotalUpgrades;
			if (this.PhysicalPowerBase > 0)
			{
				CDouble cDouble = (this.Upgrade.PhysicalPower - physicalPower) * this.Level * this.PhysicalPowerBase;
				App.State.Multiplier.MonumentMultiPhysical += cDouble;
				Log.Info("difPhysical: " + cDouble.ToGuiText(true));
			}
			if (this.MysticPowerBase > 0)
			{
				CDouble cDouble2 = (this.Upgrade.MysticPower - mysticPower) * this.Level * this.MysticPowerBase;
				App.State.Multiplier.MonumentMultiMystic += cDouble2;
				Log.Info("difMystic: " + cDouble2.ToGuiText(true));
			}
			if (this.BattlePowerBase > 0)
			{
				CDouble cDouble3 = (this.Upgrade.BattlePower - battlePower) * this.Level * this.BattlePowerBase;
				App.State.Multiplier.MonumentMultiBattle += cDouble3;
				Log.Info("difBattle: " + cDouble3.ToGuiText(true));
			}
			if (this.CreatingPowerBase > 0)
			{
				CDouble cDouble4 = (this.Upgrade.CreatingPower - creatingPower) * this.Level * this.CreatingPowerBase;
				App.State.Multiplier.MonumentMultiCreating += cDouble4;
				Log.Info("difCreating: " + cDouble4.ToGuiText(true));
			}
			App.State.Multiplier.RecalculateMonumentMultis(App.State);
			App.State.UpdateAllInfoTexts();
		}

		internal void AddStatBoni()
		{
			App.State.Multiplier.RecalculateMonumentMultis(App.State);
			App.State.UpdateAllInfoTexts();
		}

		public new long DurationInMS(int shadowCloneCount)
		{
			long num = 9999999999999L;
			switch (this.TypeEnum)
			{
			case Monument.MonumentType.mighty_statue:
				num = 120000000L;
				break;
			case Monument.MonumentType.mystic_garden:
				num = 720000000L;
				break;
			case Monument.MonumentType.tomb_of_god:
                num = 720000000L;//(ulong)((ulong) - 694967296)); 
				break;
			case Monument.MonumentType.everlasting_lighthouse:
				num = 18000000000L;
				break;
			case Monument.MonumentType.godly_statue:
				num = 30000000000L;
				break;
			case Monument.MonumentType.pyramids_of_power:
				num = 75000000000L;
				break;
			case Monument.MonumentType.temple_of_god:
				num = 150000000000L;
				break;
			case Monument.MonumentType.black_hole:
				num = 399000000000000L;
				break;
			}
			if (shadowCloneCount == 0)
			{
				return num;
			}
			return num / (long)shadowCloneCount * (long)(1 + this.Level.ToInt());
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.EnumValue);
			Conv.AppendValue(stringBuilder, "b", this.Level.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.ShadowCloneCount.Serialize());
			Conv.AppendValue(stringBuilder, "d", this.CurrentDuration);
			Conv.AppendValue(stringBuilder, "e", this.Upgrade.Serialize());
			Conv.AppendValue(stringBuilder, "f", this.IsPaid.ToString());
			Conv.AppendValue(stringBuilder, "g", this.StopAt.ToString());
			return Conv.ToBase64(stringBuilder.ToString(), "Monument");
		}

		internal static Monument FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("Monument.FromString with empty value!");
				return null;
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "Monument");
			Monument monument = new Monument();
			monument.EnumValue = Conv.getIntFromParts(parts, "a");
			monument.Init((Monument.MonumentType)monument.EnumValue);
			monument.Level = new CDouble(Conv.getStringFromParts(parts, "b"));
			monument.ShadowCloneCount = Conv.getCDoubleFromParts(parts, "c", false);
			monument.CurrentDuration = Conv.getLongFromParts(parts, "d");
			monument.Upgrade = MonumentUpgrade.FromString(Conv.getStringFromParts(parts, "e"));
			monument.IsPaid = Conv.getStringFromParts(parts, "f").ToLower().Equals("true");
			monument.StopAt = Conv.getIntFromParts(parts, "g");
			monument.StopAtString = monument.StopAt.ToString();
			return monument;
		}
	}
}
