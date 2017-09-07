using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Data
{
	public class Planet
	{
		public CDouble PowerSurge = new CDouble();

		private int powerSurgeBoni;

		private int baalPower;

		public int PlanetMultiplierMod;

		private CDouble uBMultiplier = 0;

		private CDouble powerSurgeMultiplier = 0;

		public CDouble UpgradeLevel = 0;

		public CDouble UpgradeLevelArtyChallenge = 0;

		private CDouble shadowCloneCount = 0;

		public bool IsCreated
		{
			get;
			set;
		}

		public int PowerSurgeBoni
		{
			get
			{
				if (this.powerSurgeBoni < 0)
				{
					return 0;
				}
				if (this.powerSurgeBoni > 100)
				{
					return 100;
				}
				return this.powerSurgeBoni;
			}
			set
			{
				this.powerSurgeBoni = value;
			}
		}

		public int BaalPower
		{
			get
			{
				return this.baalPower - this.PlanetMultiplierMod;
			}
			set
			{
				this.baalPower = value + this.PlanetMultiplierMod;
			}
		}

		public CDouble UBMultiplier
		{
			get
			{
				return this.uBMultiplier - this.PlanetMultiplierMod;
			}
			set
			{
				this.uBMultiplier = value + this.PlanetMultiplierMod;
			}
		}

		public CDouble PowerSurgeMultiplier
		{
			get
			{
				return this.powerSurgeMultiplier - this.PlanetMultiplierMod;
			}
			set
			{
				this.powerSurgeMultiplier = value + this.PlanetMultiplierMod;
			}
		}

		public CDouble PlanetMultiplier
		{
			get
			{
				return (this.UBMultiplier + 100) * (this.PowerSurgeMultiplier + 100) / 100 - 100;
			}
		}

		public CDouble ShadowCloneCount
		{
			get
			{
				return this.shadowCloneCount - this.PlanetMultiplierMod;
			}
			set
			{
				this.shadowCloneCount = value + this.PlanetMultiplierMod;
			}
		}

		public List<UltimateBeing> UltimateBeings
		{
			get;
			set;
		}

		public int TotalGainedGodPower
		{
			get;
			set;
		}

		public List<UltimateBeingV2> UltimateBeingsV2
		{
			get;
			set;
		}

		public string ProgressInfo
		{
			get;
			set;
		}

		public double Percent
		{
			get;
			set;
		}

		public string Name
		{
			get
			{
				if (this.UpgradeLevel == 1)
				{
					return "Cold Planet";
				}
				if (this.UpgradeLevel == 2)
				{
					return "Blue Planet";
				}
				if (this.UpgradeLevel == 3)
				{
					return "Beautiful Planet";
				}
				if (this.UpgradeLevel == 4)
				{
					return "One Planet in a Million";
				}
				if (this.UpgradeLevel >= 5)
				{
					return "Almighty Planet in the Universe";
				}
				return "Lifeless Stone";
			}
		}

		public Planet()
		{
			this.UBMultiplier = new CDouble();
			this.UltimateBeings = UltimateBeing.Initial;
			this.UltimateBeingsV2 = UltimateBeingV2.Initial;
		}

		public void RoudClones()
		{
			this.shadowCloneCount.Round();
		}

		public void InitUBMultipliers()
		{
			foreach (UltimateBeing current in this.UltimateBeings)
			{
				current.SetNextMultiplier(this.UBMultiplier);
			}
		}

		internal void RecalculateMultiplier()
		{
			CDouble cDouble = 100;
			foreach (UltimateBeing current in this.UltimateBeings)
			{
				for (int i = 0; i < current.TimesDefeated; i++)
				{
					CDouble leftSide = current.Tier * (10 - i);
					if (leftSide < current.Tier)
					{
						leftSide = current.Tier;
					}
					cDouble *= 1 + leftSide / 100;
				}
			}
			foreach (UltimateBeingV2 current2 in this.UltimateBeingsV2)
			{
				if (current2.IsDefeated)
				{
					cDouble += current2.GetMultiplier(cDouble);
				}
			}
			Log.Info(string.Concat(new object[]
			{
				"Recalc ub multi, old: ",
				this.UBMultiplier,
				", new: ",
				cDouble
			}));
			this.UBMultiplier = cDouble - 100;
		}

		public void AddCloneCount(CDouble count)
		{
			CDouble availableClones = App.State.GetAvailableClones(false);
			if (count > availableClones)
			{
				count = availableClones;
			}
			if (count < 0)
			{
				count = 0;
			}
			App.State.Clones.UseShadowClones(count);
			this.ShadowCloneCount += count;
			this.shadowCloneCount.Round();
		}

		public void RemoveCloneCount(CDouble count)
		{
			if (this.ShadowCloneCount <= count)
			{
				count = this.ShadowCloneCount;
			}
			this.ShadowCloneCount -= count;
			this.shadowCloneCount.Round();
			App.State.Clones.RemoveUsedShadowClones(count);
		}

		public void UpdateDuration(long ms)
		{
			foreach (UltimateBeing current in this.UltimateBeings)
			{
				current.UpdateDuration(ms);
			}
			foreach (UltimateBeingV2 current2 in this.UltimateBeingsV2)
			{
				current2.UpdateDuration(ms);
			}
			this.updateInfoTexts();
			if (this.ShadowCloneCount <= 0)
			{
				return;
			}
			this.PowerSurge += this.ShadowCloneCount * ms * (this.UpgradeLevel + 1) * App.State.PowerSurgeMod / 100;
			CDouble leftSide = this.PowerSurge.Double / 3600000.0;
			if (leftSide > 100000)
			{
				this.PowerSurge = 0;
				this.PowerSurgeMultiplier += 1;
				this.PowerSurgeBoni++;
				Statistic expr_160 = App.State.Statistic;
				expr_160.TotalPowersurge = ++expr_160.TotalPowersurge;
			}
		}

		private void updateInfoTexts()
		{
			int num = this.ShadowCloneCount.ToInt();
			if (num == 0)
			{
				num = 1000;
			}
			int num2 = (int)(this.PowerSurge.Double / 3600000.0);
			this.Percent = this.PowerSurge.Double / 360000000000.0;
			this.ProgressInfo = string.Concat(new object[]
			{
				"Power: ",
				num2,
				" / ",
				100000
			});
			long time = (long)((double)(100000 - num2) / (double)num * 3600000.0 / (double)(this.UpgradeLevel.ToInt() + 1)) / (long)App.State.PowerSurgeMod * 100L;
			this.ProgressInfo = this.ProgressInfo + "\nTime to level up: " + Conv.MsToGuiText(time, true) + string.Format(" ({0} Clones)", num);
		}

		internal void Upgrade(GameState state)
		{
			if (!this.IsCreated)
			{
				this.IsCreated = true;
				return;
			}
			this.UpgradeLevel += 1;
			foreach (UltimateBeing current in this.UltimateBeings)
			{
				if (current.Tier <= this.UpgradeLevel && !current.IsAvailable)
				{
					current.IsAvailable = true;
					current.ComeBack();
				}
			}
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.IsCreated.ToString());
			Conv.AppendValue(stringBuilder, "b", this.PowerSurge.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.UBMultiplier.Serialize());
			Conv.AppendValue(stringBuilder, "d", this.UpgradeLevel.Serialize());
			Conv.AppendValue(stringBuilder, "e", this.ShadowCloneCount.Serialize());
			Conv.AppendList<UltimateBeing>(stringBuilder, this.UltimateBeings, "f");
			Conv.AppendValue(stringBuilder, "g", this.TotalGainedGodPower.ToString());
			Conv.AppendValue(stringBuilder, "h", this.BaalPower.ToString());
			Conv.AppendValue(stringBuilder, "i", this.PowerSurgeBoni.ToString());
			Conv.AppendValue(stringBuilder, "j", this.PowerSurgeMultiplier.Serialize());
			Conv.AppendList<UltimateBeingV2>(stringBuilder, this.UltimateBeingsV2, "k");
			Conv.AppendValue(stringBuilder, "l", this.UpgradeLevelArtyChallenge.Serialize());
			return Conv.ToBase64(stringBuilder.ToString(), "Planet");
		}

		internal static Planet FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				return new Planet();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "Planet");
			Planet planet = new Planet();
			planet.IsCreated = Conv.getStringFromParts(parts, "a").ToLower().Equals("true");
			planet.PowerSurge = Conv.getCDoubleFromParts(parts, "b", false);
			planet.UBMultiplier = Conv.getCDoubleFromParts(parts, "c", false);
			planet.UpgradeLevel = Conv.getCDoubleFromParts(parts, "d", false);
			planet.ShadowCloneCount = Conv.getCDoubleFromParts(parts, "e", false);
			planet.UltimateBeings = new List<UltimateBeing>();
			string stringFromParts = Conv.getStringFromParts(parts, "f");
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
					planet.UltimateBeings.Add(UltimateBeing.FromString(text));
				}
			}
			if (planet.UltimateBeings.Count == 0)
			{
				planet.UltimateBeings = UltimateBeing.Initial;
			}
			planet.TotalGainedGodPower = Conv.getIntFromParts(parts, "g");
			planet.BaalPower = Conv.getIntFromParts(parts, "h");
			planet.PowerSurgeBoni = Conv.getIntFromParts(parts, "i");
			planet.PowerSurgeMultiplier = Conv.getCDoubleFromParts(parts, "j", true);
			planet.UltimateBeingsV2 = new List<UltimateBeingV2>();
			stringFromParts = Conv.getStringFromParts(parts, "k");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array3 = array;
			for (int j = 0; j < array3.Length; j++)
			{
				string text2 = array3[j];
				if (!string.IsNullOrEmpty(text2))
				{
					planet.UltimateBeingsV2.Add(UltimateBeingV2.FromString(text2));
				}
			}
			if (planet.UltimateBeingsV2.Count == 0)
			{
				planet.UltimateBeingsV2 = UltimateBeingV2.Initial;
			}
			planet.UpgradeLevelArtyChallenge = Conv.getCDoubleFromParts(parts, "l", false);
			return planet;
		}
	}
}
