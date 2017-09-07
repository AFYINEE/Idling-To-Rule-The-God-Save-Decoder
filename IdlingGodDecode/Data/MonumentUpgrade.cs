using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
	public class MonumentUpgrade : TrainingBase
	{
		private Monument.MonumentType type;

		public bool IsPaid;

		public int StopAt;

		public string stopAtString = "0";

		private string description = string.Empty;

		public string MissingItems = string.Empty;

		public CDouble PhysicalPower
		{
			get
			{
				if (this.Level == 0)
				{
					return 1;
				}
				return 5 * (this.Level * this.Level) + 10 * this.Level;
			}
		}

		public CDouble MysticPower
		{
			get
			{
				if (this.Level == 0)
				{
					return 1;
				}
				return 5 * (this.Level * this.Level) + 10 * this.Level;
			}
		}

		public CDouble BattlePower
		{
			get
			{
				if (this.Level == 0)
				{
					return 1;
				}
				return 5 * (this.Level * this.Level) + 10 * this.Level;
			}
		}

		public CDouble CreatingPower
		{
			get
			{
				if (this.Level == 0)
				{
					return 1;
				}
				return 5 * (this.Level * this.Level) + 10 * this.Level;
			}
		}

		public CDouble PhysicalPowerRebirth
		{
			get
			{
				return this.UpgradeRebirthMulti;
			}
		}

		public CDouble MysticPowerRebirth
		{
			get
			{
				return this.UpgradeRebirthMulti;
			}
		}

		public CDouble BattlePowerRebirth
		{
			get
			{
				return this.UpgradeRebirthMulti;
			}
		}

		public CDouble CreatingPowerRebirth
		{
			get
			{
				return this.UpgradeRebirthMulti;
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

		public static int MaxRebirthMulti
		{
			get
			{
				if (App.State == null)
				{
					return 5;
				}
				return App.State.PrinnyBaal.Level + 4;
			}
		}

		private CDouble UpgradeRebirthMulti
		{
			get
			{
				CDouble cDouble = this.Level / 4;
				if (cDouble > MonumentUpgrade.MaxRebirthMulti)
				{
					cDouble = MonumentUpgrade.MaxRebirthMulti;
				}
				return cDouble;
			}
		}

		public bool IsAvailable
		{
			get
			{
				return App.State.IsUpgradeUnlocked;
			}
		}

		public string Name
		{
			get
			{
				return "Upgrade";
			}
		}

		public string Description
		{
			get
			{
				if (!string.IsNullOrEmpty(this.description) && !this.ShouldUpdateText)
				{
					return this.description;
				}
				StringBuilder stringBuilder = new StringBuilder("\nTo create 1 ").Append(this.Name).Append(", you need:\n");
				foreach (CreationCost current in this.RequiredCreations)
				{
					stringBuilder.Append(current.CountNeeded.ToGuiText(true)).Append(" x ").Append(EnumName.Name(current.TypeEnum)).Append(", ");
				}
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
				switch (this.type)
				{
				case Monument.MonumentType.mighty_statue:
					this.description = string.Concat(new string[]
					{
						"Adding a mountain or two might help to make it look more grand. ",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\nMighty Statue multi (now) x ",
						this.PhysicalPower.ToGuiText(true),
						", (rebirth) ",
						this.PhysicalPowerRebirth.ToGuiText(false)
					});
					break;
				case Monument.MonumentType.mystic_garden:
					this.description = string.Concat(new string[]
					{
						"Some forests around the garden will surely improve the quality.",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\nMystic garden multi (now) x ",
						this.PhysicalPower.ToGuiText(true),
						", (rebirth) ",
						this.PhysicalPowerRebirth.ToGuiText(false)
					});
					break;
				case Monument.MonumentType.tomb_of_god:
					this.description = string.Concat(new string[]
					{
						"Get prayers to your tomb with a village around it!",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\nTomb of gods multi (now) x ",
						this.PhysicalPower.ToGuiText(true),
						", (rebirth) ",
						this.PhysicalPowerRebirth.ToGuiText(false)
					});
					break;
				case Monument.MonumentType.everlasting_lighthouse:
					this.description = string.Concat(new string[]
					{
						"With an ocean the lighthouse will bring out its full use.",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\nEverlasting lighthouse multi (now) x ",
						this.PhysicalPower.ToGuiText(true),
						", (rebirth) ",
						this.PhysicalPowerRebirth.ToGuiText(false)
					});
					break;
				case Monument.MonumentType.godly_statue:
					this.description = string.Concat(new string[]
					{
						"With a nation around the godly statue it might become their emblem.",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\nGodly statue (now) x ",
						this.PhysicalPower.ToGuiText(true),
						", (rebirth) ",
						this.PhysicalPowerRebirth.ToGuiText(false)
					});
					break;
				case Monument.MonumentType.pyramids_of_power:
					this.description = string.Concat(new string[]
					{
						"Pyramids need a lot of space. So better create a whole continent for them.",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\nPyramids of power (now) x ",
						this.PhysicalPower.ToGuiText(true),
						", (rebirth) ",
						this.PhysicalPowerRebirth.ToGuiText(false)
					});
					break;
				case Monument.MonumentType.temple_of_god:
					this.description = string.Concat(new string[]
					{
						"Control the weather so your temple always looks beautiful.",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\nTemple of god (now) x ",
						this.PhysicalPower.ToGuiText(true),
						", (rebirth) ",
						this.PhysicalPowerRebirth.ToGuiText(false)
					});
					break;
				case Monument.MonumentType.black_hole:
					this.description = string.Concat(new string[]
					{
						"Add some more universes, so the galaxies won't run out. Adds 1 God Power every upgrade after Rebirthing. Maxed at 50.",
						stringBuilder.ToString(),
						this.DurationInfo,
						"\nBlack Hole (now) x ",
						this.PhysicalPower.ToGuiText(true),
						", (rebirth) ",
						this.PhysicalPowerRebirth.ToGuiText(false)
					});
					break;
				}
				this.description = string.Concat(new object[]
				{
					this.description,
					" (maxed at ",
					MonumentUpgrade.MaxRebirthMulti,
					")"
				});
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

		public List<CreationCost> RequiredCreations
		{
			get
			{
				CDouble cDouble = this.Level * this.Level;
				if (cDouble < 1)
				{
					cDouble = 1;
				}
				List<CreationCost> list = new List<CreationCost>();
				switch (this.type)
				{
				case Monument.MonumentType.mighty_statue:
					list.Add(new CreationCost(10 * cDouble, Creation.CreationType.Mountain));
					break;
				case Monument.MonumentType.mystic_garden:
					list.Add(new CreationCost(10 * cDouble, Creation.CreationType.Forest));
					break;
				case Monument.MonumentType.tomb_of_god:
					list.Add(new CreationCost(5 * cDouble, Creation.CreationType.Village));
					break;
				case Monument.MonumentType.everlasting_lighthouse:
					list.Add(new CreationCost(1 * cDouble, Creation.CreationType.Ocean));
					break;
				case Monument.MonumentType.godly_statue:
					list.Add(new CreationCost(1 * cDouble, Creation.CreationType.Nation));
					break;
				case Monument.MonumentType.pyramids_of_power:
					list.Add(new CreationCost(1 * cDouble, Creation.CreationType.Continent));
					break;
				case Monument.MonumentType.temple_of_god:
					list.Add(new CreationCost(1 * cDouble, Creation.CreationType.Weather));
					break;
				case Monument.MonumentType.black_hole:
				{
					CDouble cDouble2 = 20 * cDouble;
					if (App.State != null && App.State.Statistic.BlackHoleChallengesFinished > 0)
					{
						CDouble cDouble3 = App.State.Statistic.BlackHoleChallengesFinished * 2;
						if (cDouble3 > 80)
						{
							cDouble3 = 80;
						}
						cDouble2 = cDouble2 * (100 - cDouble3) / 100;
					}
					cDouble2.Round();
					list.Add(new CreationCost(cDouble2, Creation.CreationType.Universe));
					break;
				}
				}
				return list;
			}
		}

		private MonumentUpgrade()
		{
		}

		public MonumentUpgrade(Monument.MonumentType type)
		{
			this.Init(type);
		}

		private void Init(Monument.MonumentType type)
		{
			this.type = type;
			this.EnumValue = (int)type;
		}

		public new double getPercent()
		{
			return (double)this.CurrentDuration / (double)this.DurationInMS(1);
		}

		public new bool UpdateDuration(long ms)
		{
			if (this.ShadowCloneCount == 0)
			{
				return false;
			}
			if (this.Level >= this.StopAt && this.StopAt != 0)
			{
				CDouble shadowCloneCount = this.ShadowCloneCount;
				App.State.Clones.RemoveUsedShadowClones(shadowCloneCount);
				this.ShadowCloneCount = 0;
				Monument monument = App.State.AllMonuments.FirstOrDefault((Monument x) => x.TypeEnum == this.type + 1);
				if (monument != null && monument.StopAt > 0)
				{
					monument.AddCloneCount(shadowCloneCount);
				}
				return false;
			}
			if (!this.IsPaid)
			{
				bool flag = false;
				StringBuilder stringBuilder = new StringBuilder("You still need:\n");
				using (List<CreationCost>.Enumerator enumerator = this.RequiredCreations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CreationCost cost = enumerator.Current;
						Creation creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
						if (cost.CountNeeded > creation.Count)
						{
							if (App.State.GameSettings.AutoBuyCreationsForMonuments && creation.CanBuy)
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
					stringBuilder.Append("to upgrade ").Append(EnumName.Name(this.type)).ToString();
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
					return false;
				}
				using (List<CreationCost>.Enumerator enumerator2 = this.RequiredCreations.GetEnumerator())
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
				if (App.State.GameSettings.StopMonumentBuilding)
				{
					App.State.Clones.RemoveUsedShadowClones(this.ShadowCloneCount);
					this.ShadowCloneCount = 0;
				}
				this.IsPaid = false;
				this.CurrentDuration = 0L;
				return true;
			}
			return false;
		}

		public new long DurationInMS(int shadowCloneCount)
		{
			long num = 9999999999999L;
			switch (this.type)
			{
			case Monument.MonumentType.mighty_statue:
				num = 60000000L;
				break;
			case Monument.MonumentType.mystic_garden:
				num = 360000000L;
				break;
			case Monument.MonumentType.tomb_of_god:
				num = 1800000000L;
				break;
			case Monument.MonumentType.everlasting_lighthouse:
				num = 9000000000L;
				break;
			case Monument.MonumentType.godly_statue:
				num = 15000000000L;
				break;
			case Monument.MonumentType.pyramids_of_power:
				num = 37500000000L;
				break;
			case Monument.MonumentType.temple_of_god:
				num = 75000000000L;
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
			Conv.AppendValue(stringBuilder, "e", this.IsPaid.ToString());
			Conv.AppendValue(stringBuilder, "f", this.StopAt);
			return Conv.ToBase64(stringBuilder.ToString(), "MonumentUpgrade");
		}

		internal static MonumentUpgrade FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("MonumentUpgrade.FromString with empty value!");
				return null;
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "MonumentUpgrade");
			MonumentUpgrade monumentUpgrade = new MonumentUpgrade();
			monumentUpgrade.EnumValue = Conv.getIntFromParts(parts, "a");
			monumentUpgrade.Level = new CDouble(Conv.getStringFromParts(parts, "b"));
			monumentUpgrade.ShadowCloneCount = Conv.getCDoubleFromParts(parts, "c", false);
			monumentUpgrade.CurrentDuration = Conv.getLongFromParts(parts, "d");
			monumentUpgrade.IsPaid = Conv.getStringFromParts(parts, "e").ToLower().Equals("true");
			monumentUpgrade.StopAt = Conv.getIntFromParts(parts, "f");
			monumentUpgrade.StopAtString = monumentUpgrade.StopAt.ToString();
			monumentUpgrade.Init((Monument.MonumentType)monumentUpgrade.EnumValue);
			return monumentUpgrade;
		}
	}
}
