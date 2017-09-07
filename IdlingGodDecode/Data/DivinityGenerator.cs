using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
	public class DivinityGenerator : TrainingBase
	{
		public bool IsAvailable;

		public bool IsBuilt;

		public bool IsPaid;

		public string CloneFillSpeedText = string.Empty;

		public CDouble FilledCapacity = new CDouble();

		private CDouble baseCapacity = 100000000000000L;

		private int baseDivinityEachCapacity = 1;

		private CDouble baseConvertSec = 10000000000L;

		public CDouble TotalMoneyGenerated = new CDouble();

		public List<GeneratorUpgrade> Upgrades = new List<GeneratorUpgrade>();

		private string description;

		public string MissingItems = string.Empty;

		public CDouble DivinitySec = 0;

		public CDouble DivinitySecWithWorker = 0;

		public CDouble DivinitySecWithWorkerCrystal = 0;

		public CDouble FreeCapacity
		{
			get
			{
				return this.Capacity - this.FilledCapacity;
			}
		}

		public CDouble Capacity
		{
			get
			{
				CDouble cDouble = this.baseCapacity * (App.State.Statistic.HighestGodDefeated - 18) / 10;
				if (App.State.Statistic.HasStartedArtyChallenge)
				{
					cDouble = this.baseCapacity * (App.State.Statistic.HighestGodInUAC - 18) / 10;
				}
				if (cDouble <= 0)
				{
					cDouble = this.baseCapacity / 10;
				}
				GeneratorUpgrade generatorUpgrade = this.Upgrades.FirstOrDefault((GeneratorUpgrade x) => x.type == GeneratorUpgrade.UpgradeType.max_capacity);
				if (generatorUpgrade != null)
				{
					cDouble *= 1 + generatorUpgrade.Level;
				}
				return cDouble;
			}
		}

		public int DivinityEachCapacity
		{
			get
			{
				int num = this.baseDivinityEachCapacity;
				GeneratorUpgrade generatorUpgrade = this.Upgrades.FirstOrDefault((GeneratorUpgrade x) => x.type == GeneratorUpgrade.UpgradeType.money_gain);
				if (generatorUpgrade != null)
				{
					num *= (1 + generatorUpgrade.Level).ToInt();
				}
				return num + 1;
			}
		}

		public CDouble ConvertSec
		{
			get
			{
				CDouble cDouble = this.baseConvertSec * (App.State.Statistic.HighestGodDefeated - 18) / 10;
				if (App.State.Statistic.HasStartedArtyChallenge)
				{
					cDouble = this.baseConvertSec * (App.State.Statistic.HighestGodInUAC - 18) / 10;
				}
				if (cDouble <= 0)
				{
					cDouble = this.baseConvertSec / 10;
				}
				GeneratorUpgrade generatorUpgrade = this.Upgrades.FirstOrDefault((GeneratorUpgrade x) => x.type == GeneratorUpgrade.UpgradeType.speed);
				if (generatorUpgrade != null)
				{
					cDouble *= (1 + generatorUpgrade.Level).ToInt();
				}
				return cDouble;
			}
		}

		public string Name
		{
			get
			{
				return "Divinity generator";
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
					stringBuilder.Append(current.CountNeeded).Append(" x ").Append(EnumName.Name(current.TypeEnum)).Append(", ");
				}
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
				this.description = "A useful tool to generate divinity from all creations. \nYou just have to add creations and wait." + stringBuilder.ToString() + this.DurationInfo;
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
				double num3 = (double)((100 + App.State.PremiumBoni.BuildingSpeedUpPercent(true)) / 100.0 * App.State.PremiumBoni.MonumentBuildTimeDivider).ToLong();
				num2 = (long)((double)num2 / num3 - (double)this.CurrentDuration / num3) / (long)num;
				return "lowerTextTime to build: " + Conv.MsToGuiText(num2, true) + string.Format(" ({0} Clones)", num);
			}
		}

		public List<CreationCost> RequiredCreations
		{
			get
			{
				return new List<CreationCost>
				{
					new CreationCost(1000000, Creation.CreationType.Stone),
					new CreationCost(250000, Creation.CreationType.Light),
					new CreationCost(250000, Creation.CreationType.Water),
					new CreationCost(100000, Creation.CreationType.Tree)
				};
			}
		}

		public new double getPercent()
		{
			return (double)this.CurrentDuration / (double)this.DurationInMS(1);
		}

		public void UpdateCloneFillSpeedText()
		{
			this.CloneFillSpeedText = App.State.ClonesDifGenMod * 50 + " stones / sec.";
		}

		public int GetBreakEvenWorker(CDouble cost)
		{
			if (cost == 0)
			{
				cost = 1250;
			}
			CDouble cDouble = 30 * App.State.ClonesDifGenMod / 20 * cost;
			if (cDouble == 0)
			{
				cDouble = 150 * cost;
			}
			CDouble leftSide = this.ConvertSec * 30 / 1000;
			return (leftSide / cDouble).ToNextInt();
		}

		public void UseBreakEvenWorkerCount()
		{
			base.RemoveCloneCount(this.ShadowCloneCount);
			Creation creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Stone);
			int breakEvenWorker = this.GetBreakEvenWorker(creation.BuyCost);
			base.AddCloneCount(breakEvenWorker);
		}

		public new void UpdateDuration(long ms)
		{
			foreach (GeneratorUpgrade current in this.Upgrades)
			{
				current.UpdateDuration(ms);
			}
			CDouble leftSide = 0;
			int num = 0;
			if (this.IsBuilt && this.ShadowCloneCount > 0)
			{
				Creation creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Stone);
				if (creation != null)
				{
					CDouble rightSide = ms * (long)App.State.ClonesDifGenMod / 20L;
					CDouble cDouble = this.ShadowCloneCount * rightSide;
					if (creation.Count < cDouble)
					{
						cDouble = creation.Count;
					}
					CDouble cDouble2 = this.Capacity - this.FilledCapacity;
					CDouble cDouble3 = cDouble * creation.BuyCost;
					if (cDouble3 > cDouble2)
					{
						leftSide = cDouble3 - cDouble2;
						cDouble3 = cDouble2;
					}
					if (leftSide > 0)
					{
						cDouble -= leftSide / creation.BuyCost;
						num = (leftSide / 962500000).ToInt();
					}
					this.FilledCapacity += cDouble3;
					creation.Count -= cDouble;
				}
			}
			if (this.FilledCapacity > 0)
			{
				CDouble cDouble4 = this.ConvertSec * ms / 1000;
				if (this.FilledCapacity > cDouble4)
				{
					this.FilledCapacity -= cDouble4;
				}
				else
				{
					cDouble4 = this.FilledCapacity;
					this.FilledCapacity = 0;
				}
				CDouble cDouble5 = cDouble4 * this.DivinityEachCapacity;
				this.DivinitySec = cDouble5 * 1000 / ms;
				if (num > 400)
				{
					num = 400;
				}
				cDouble5 = cDouble5 * (num + 100) / 100;
				this.DivinitySecWithWorker = cDouble5 * 1000 / ms;
				if (App.State.PremiumBoni.CrystalBonusDivinity > 0)
				{
					cDouble5 = cDouble5 * (100 + App.State.PremiumBoni.CrystalBonusDivinity) / 100;
					this.DivinitySecWithWorkerCrystal = cDouble5 * 1000 / ms;
				}
				App.State.Money += cDouble5;
			}
			else
			{
				this.DivinitySecWithWorker = 0;
				this.DivinitySec = 0;
			}
			if (this.ShadowCloneCount == 0 || this.IsBuilt)
			{
				return;
			}
			if (!this.IsPaid)
			{
				bool flag = false;
				StringBuilder stringBuilder = new StringBuilder("You still need:\n");
				using (List<CreationCost>.Enumerator enumerator2 = this.RequiredCreations.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						CreationCost cost = enumerator2.Current;
						Creation creation2 = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
						if (!CreationCost.HasCreations(App.State, cost, App.State.GameSettings.AutoBuyCreationsForDivGen))
						{
							flag = true;
							stringBuilder.Append(creation2.Name).Append(" x ").Append((cost.CountNeeded - creation2.Count).ToGuiText(true)).Append("\n");
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
				using (List<CreationCost>.Enumerator enumerator3 = this.RequiredCreations.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						CreationCost cost = enumerator3.Current;
						Creation creation3 = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
						creation3.Count -= cost.CountNeeded;
					}
				}
				this.IsPaid = true;
			}
			this.MissingItems = string.Empty;
			this.CurrentDuration += (ms * (long)this.ShadowCloneCount.ToInt() * (long)App.State.PremiumBoni.MonumentBuildTimeDivider * (100 + App.State.PremiumBoni.BuildingSpeedUpPercent(true)) / 100).ToLong();
			Log.Info(string.Concat(new object[]
			{
				this.Name,
				", Duration: ",
				this.CurrentDuration,
				" / ",
				this.DurationInMS(1)
			}));
			if (this.CurrentDuration > this.DurationInMS(1))
			{
				this.IsPaid = false;
				this.CurrentDuration = 0L;
				this.IsBuilt = true;
				App.State.Clones.RemoveUsedShadowClones(this.ShadowCloneCount);
				this.ShadowCloneCount = 0;
			}
		}

		public new long DurationInMS(int shadowCloneCount)
		{
			long num = 150000000000L;
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
			Conv.AppendValue(stringBuilder, "e", this.IsAvailable.ToString());
			Conv.AppendValue(stringBuilder, "f", this.IsBuilt.ToString());
			Conv.AppendValue(stringBuilder, "g", this.FilledCapacity.Serialize());
			Conv.AppendValue(stringBuilder, "k", this.TotalMoneyGenerated.Serialize());
			Conv.AppendList<GeneratorUpgrade>(stringBuilder, this.Upgrades, "l");
			Conv.AppendValue(stringBuilder, "m", this.IsPaid.ToString());
			return Conv.ToBase64(stringBuilder.ToString(), "MoneyGenerator");
		}

		internal static DivinityGenerator FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("MoneyGenerator.FromString with empty value!");
				return new DivinityGenerator();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "MoneyGenerator");
			DivinityGenerator divinityGenerator = new DivinityGenerator();
			divinityGenerator.EnumValue = Conv.getIntFromParts(parts, "a");
			divinityGenerator.Level = new CDouble(Conv.getStringFromParts(parts, "b"));
			divinityGenerator.ShadowCloneCount = Conv.getCDoubleFromParts(parts, "c", false);
			divinityGenerator.CurrentDuration = Conv.getLongFromParts(parts, "d");
			divinityGenerator.IsAvailable = Conv.getStringFromParts(parts, "e").ToLower().Equals("true");
			divinityGenerator.IsBuilt = Conv.getStringFromParts(parts, "f").ToLower().Equals("true");
			divinityGenerator.ShadowCloneCount.Round();
			divinityGenerator.FilledCapacity = new CDouble(Conv.getStringFromParts(parts, "g"));
			divinityGenerator.TotalMoneyGenerated = new CDouble(Conv.getStringFromParts(parts, "k"));
			string stringFromParts = Conv.getStringFromParts(parts, "l");
			string[] array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (!string.IsNullOrEmpty(text))
				{
					divinityGenerator.Upgrades.Add(GeneratorUpgrade.FromString(text));
				}
			}
			if (divinityGenerator.Upgrades.Count == 0)
			{
				divinityGenerator.Upgrades = GeneratorUpgrade.Initial();
			}
			divinityGenerator.IsPaid = Conv.getStringFromParts(parts, "m").ToLower().Equals("true");
			return divinityGenerator;
		}
	}
}
