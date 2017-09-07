using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
	public class GeneratorUpgrade : TrainingBase
	{
		public enum UpgradeType
		{
			max_capacity,
			money_gain,
			speed
		}

		public GeneratorUpgrade.UpgradeType type;

		public bool IsPaid;

		public int StopAt;

		private string description = string.Empty;

		public string MissingItems = string.Empty;

		public string Name
		{
			get
			{
				GeneratorUpgrade.UpgradeType upgradeType = this.type;
				if (upgradeType == GeneratorUpgrade.UpgradeType.max_capacity)
				{
					return "Capacity";
				}
				if (upgradeType == GeneratorUpgrade.UpgradeType.money_gain)
				{
					return "Divinity gain";
				}
				if (upgradeType != GeneratorUpgrade.UpgradeType.speed)
				{
					return string.Empty;
				}
				return "Converting speed";
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
				StringBuilder stringBuilder = new StringBuilder("\nTo create 1 level of ").Append(this.Name).Append(", you need:\n");
				foreach (CreationCost current in this.RequiredCreations)
				{
					stringBuilder.Append(current.CountNeeded.ToGuiText(true)).Append(" x ").Append(EnumName.Name(current.TypeEnum)).Append(", ");
				}
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
				GeneratorUpgrade.UpgradeType upgradeType = this.type;
				if (upgradeType != GeneratorUpgrade.UpgradeType.max_capacity)
				{
					if (upgradeType != GeneratorUpgrade.UpgradeType.money_gain)
					{
						if (upgradeType == GeneratorUpgrade.UpgradeType.speed)
						{
							this.description = "Increases the converts / s of the divinity generator by 1 x the level value every time.\n" + stringBuilder.ToString() + this.DurationInfo;
						}
					}
					else
					{
						this.description = "Increases the divinity gain for converting creations by 1 divinity each convert.\n" + stringBuilder.ToString() + this.DurationInfo;
					}
				}
				else
				{
					this.description = "Increases the capacity of the divinity generator so bigger creations can fit in.\n" + stringBuilder.ToString() + this.DurationInfo;
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

		public List<CreationCost> RequiredCreations
		{
			get
			{
				CDouble cDouble = (int)Math.Pow((double)this.Level.ToInt(), 2.0);
				if (cDouble < 1)
				{
					cDouble = 1;
				}
				List<CreationCost> list = new List<CreationCost>();
				GeneratorUpgrade.UpgradeType upgradeType = this.type;
				if (upgradeType != GeneratorUpgrade.UpgradeType.max_capacity)
				{
					if (upgradeType != GeneratorUpgrade.UpgradeType.money_gain)
					{
						if (upgradeType == GeneratorUpgrade.UpgradeType.speed)
						{
							list.Add(new CreationCost(1000000 * cDouble, Creation.CreationType.Air));
						}
					}
					else
					{
						list.Add(new CreationCost(10000000 * cDouble, Creation.CreationType.Light));
					}
				}
				else
				{
					list.Add(new CreationCost(5000000 * cDouble, Creation.CreationType.Stone));
				}
				return list;
			}
		}

		private GeneratorUpgrade()
		{
		}

		public GeneratorUpgrade(GeneratorUpgrade.UpgradeType type)
		{
			this.Init(type);
		}

		private void Init(GeneratorUpgrade.UpgradeType type)
		{
			this.type = type;
			this.EnumValue = (int)type;
		}

		internal static List<GeneratorUpgrade> Initial()
		{
			return new List<GeneratorUpgrade>
			{
				new GeneratorUpgrade(GeneratorUpgrade.UpgradeType.max_capacity),
				new GeneratorUpgrade(GeneratorUpgrade.UpgradeType.money_gain),
				new GeneratorUpgrade(GeneratorUpgrade.UpgradeType.speed)
			};
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
			if (this.StopAt != 0 && this.StopAt <= this.Level)
			{
				App.State.Clones.RemoveUsedShadowClones(this.ShadowCloneCount);
				this.ShadowCloneCount = 0;
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
						if (!CreationCost.HasCreations(App.State, cost, App.State.GameSettings.AutoBuyCreationsForDivGen))
						{
							flag = true;
							stringBuilder.Append(creation.Name).Append(" x ").Append((cost.CountNeeded - creation.Count).ToGuiText(true)).Append("\n");
						}
					}
				}
				if (flag)
				{
					if (!App.State.GameSettings.StickyClones)
					{
						stringBuilder.Append("to upgrade ").Append(this.Name).ToString();
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
				this.IsPaid = false;
				this.CurrentDuration = 0L;
				this.Level = ++this.Level;
				if (App.State.GameSettings.StopDivinityGenBuilding)
				{
					App.State.Clones.RemoveUsedShadowClones(this.ShadowCloneCount);
					this.ShadowCloneCount = 0;
				}
				return true;
			}
			return false;
		}

		public new long DurationInMS(int shadowCloneCount)
		{
			long num = 9999999999999L;
			GeneratorUpgrade.UpgradeType upgradeType = this.type;
			if (upgradeType != GeneratorUpgrade.UpgradeType.max_capacity)
			{
				if (upgradeType != GeneratorUpgrade.UpgradeType.money_gain)
				{
					if (upgradeType == GeneratorUpgrade.UpgradeType.speed)
					{
						num = 1000000000000L;
					}
				}
				else
				{
					num = 1000000000000L;
				}
			}
			else
			{
				num = 1000000000000L;
			}
			if (shadowCloneCount == 0)
			{
				return num;
			}
			return num / (long)shadowCloneCount * (long)(1 + this.Level.ToInt() * 2);
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.EnumValue);
			Conv.AppendValue(stringBuilder, "b", this.Level.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.ShadowCloneCount.Serialize());
			Conv.AppendValue(stringBuilder, "d", this.CurrentDuration);
			Conv.AppendValue(stringBuilder, "e", this.IsPaid.ToString());
			Conv.AppendValue(stringBuilder, "f", this.StopAt.ToString());
			return Conv.ToBase64(stringBuilder.ToString(), "GeneratorUpdate");
		}

		internal static GeneratorUpgrade FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("GeneratorUpgrade.FromString with empty value!");
				return null;
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "GeneratorUpdate");
			GeneratorUpgrade generatorUpgrade = new GeneratorUpgrade();
			generatorUpgrade.EnumValue = Conv.getIntFromParts(parts, "a");
			generatorUpgrade.Level = new CDouble(Conv.getStringFromParts(parts, "b"));
			generatorUpgrade.ShadowCloneCount = Conv.getCDoubleFromParts(parts, "c", false);
			generatorUpgrade.CurrentDuration = Conv.getLongFromParts(parts, "d");
			generatorUpgrade.IsPaid = Conv.getStringFromParts(parts, "e").ToLower().Equals("true");
			generatorUpgrade.StopAt = Conv.getIntFromParts(parts, "f");
			generatorUpgrade.Init((GeneratorUpgrade.UpgradeType)generatorUpgrade.EnumValue);
			return generatorUpgrade;
		}
	}
}
