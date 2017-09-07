using Assets.Scripts.Helper;
using System;
using System.Text;

namespace Assets.Scripts.Data
{
	public class Multi
	{
		public CDouble PetMultiPhysical = 100;

		public CDouble PetMultiMystic = 100;

		public CDouble PetMultiBattle = 100;

		public CDouble PetMultiPhysicalBase = 100;

		public CDouble PetMultiMysticBase = 100;

		public CDouble PetMultiBattleBase = 100;

		public CDouble PetMultiPhysicalRebirthBase = 100;

		public CDouble PetMultiMysticRebirthBase = 100;

		public CDouble PetMultiBattleRebirthBase = 100;

		public CDouble PetMultiPhysicalRebirth = 100;

		public CDouble PetMultiMysticRebirth = 100;

		public CDouble PetMultiBattleRebirth = 100;

		public CDouble PetCampainBoostRebirth = 0;

		public CDouble PetCampainBoost = 100;

		public CDouble DrawMultiPhysical = 1;

		public CDouble DrawMultiMystic = 1;

		public CDouble DrawMultiBattle = 1;

		public CDouble DrawMultiCreating = 1;

		public CDouble MonumentMultiPhysical = 1;

		public CDouble AchievementMultiPhysical = 1;

		public CDouble MonumentMultiMystic = 1;

		public CDouble AchievementMultiMystic = 1;

		public CDouble MonumentMultiBattle = 1;

		public CDouble AchievementMultiBattle = 50;

		public CDouble MonumentMultiCreating = 1;

		public CDouble AchievementMultiCreating = 2;

		public CDouble MonumentMultiPhysicalRebirth = 1;

		public CDouble AchievementMultiPhysicalRebirth = 1;

		public CDouble MonumentMultiMysticRebirth = 1;

		public CDouble AchievementMultiMysticRebirth = 1;

		public CDouble MonumentMultiBattleRebirth = 1;

		public CDouble AchievementMultiBattleRebirth = 50;

		public CDouble MonumentMultiCreatingRebirth = 1;

		public CDouble AchievementMultiCreatingRebirth = 2;

		public CDouble GodMultiFromRebirth = 0;

		public CDouble CurrentMultiPhysical
		{
			get
			{
				return this.MonumentMultiPhysical * this.AchievementMultiPhysical * this.DrawMultiPhysical * this.PetMultiPhysical / 100;
			}
		}

		public CDouble CurrentMultiMystic
		{
			get
			{
				return this.MonumentMultiMystic * this.AchievementMultiMystic * this.DrawMultiMystic * this.PetMultiMystic / 100;
			}
		}

		public CDouble CurrentMultiBattle
		{
			get
			{
				return this.MonumentMultiBattle * this.AchievementMultiBattle * this.DrawMultiBattle * this.PetMultiBattle / 100;
			}
		}

		public CDouble CurrentMultiCreating
		{
			get
			{
				return this.MonumentMultiCreating * this.AchievementMultiCreating * this.DrawMultiCreating;
			}
		}

		public CDouble MultiBoniPhysicalRebirth
		{
			get
			{
				return this.MonumentMultiPhysicalRebirth * this.AchievementMultiPhysicalRebirth * this.PetMultiPhysicalRebirth / 100;
			}
		}

		public CDouble MultiBoniMysticRebirth
		{
			get
			{
				return this.MonumentMultiMysticRebirth * this.AchievementMultiMysticRebirth * this.PetMultiMysticRebirth / 100;
			}
		}

		public CDouble MultiBoniBattleRebirth
		{
			get
			{
				return this.MonumentMultiBattleRebirth * this.AchievementMultiBattleRebirth * this.PetMultiBattleRebirth / 100;
			}
		}

		public CDouble MultiBoniCreatingRebirth
		{
			get
			{
				return this.MonumentMultiCreatingRebirth * this.AchievementMultiCreatingRebirth;
			}
		}

		public CDouble RebirthMulti
		{
			get
			{
				CDouble cDouble = 1;
				foreach (Creation current in App.State.AllCreations)
				{
					if (current.TypeEnum != Creation.CreationType.Shadow_clone && current.GodToDefeat.IsDefeated)
					{
						cDouble *= 2;
					}
				}
				if (App.State.PrinnyBaal.IsUnlocked)
				{
					for (int i = 1; i < App.State.PrinnyBaal.Level; i++)
					{
						cDouble *= 2;
					}
				}
				return cDouble;
			}
		}

		public Multi()
		{
			this.AchievementMultiPhysicalRebirth = 1;
			this.AchievementMultiMysticRebirth = 1;
			this.AchievementMultiBattleRebirth = 50;
			this.AchievementMultiCreatingRebirth = 2;
		}

		public void UpdatePetMultis(GameState state)
		{
			this.PetMultiPhysicalBase = 100;
			this.PetMultiMysticBase = 100;
			this.PetMultiBattleBase = 100;
			this.PetMultiPhysicalRebirthBase = 100;
			this.PetMultiMysticRebirthBase = 100;
			this.PetMultiBattleRebirthBase = 100;
			if (this.PetCampainBoost < 100)
			{
				this.PetCampainBoost = 100;
			}
			if (state.Statistic.HasStartedArtyChallenge || state.Statistic.HasStartedUltimateBaalChallenge)
			{
				return;
			}
			foreach (Pet current in state.Ext.AllPets)
			{
				if (current.IsUnlocked && current.CurrentHealth > 0)
				{
					this.PetMultiPhysicalBase += current.Physical / 100;
					this.PetMultiMysticBase += current.Mystic / 100;
					this.PetMultiBattleBase += current.Battle / 100;
					this.PetMultiPhysicalRebirthBase += current.Physical / 5000;
					this.PetMultiMysticRebirthBase += current.Mystic / 5000;
					this.PetMultiBattleRebirthBase += current.Battle / 5000;
				}
			}
			this.PetMultiPhysical = this.PetMultiPhysicalBase * this.PetCampainBoost / 100;
			this.PetMultiMystic = this.PetMultiMysticBase * this.PetCampainBoost / 100;
			this.PetMultiBattle = this.PetMultiBattleBase * this.PetCampainBoost / 100;
			if (this.PetMultiPhysicalRebirthBase > 999)
			{
				this.PetMultiPhysicalRebirthBase = 999;
			}
			if (this.PetMultiMysticRebirthBase > 999)
			{
				this.PetMultiMysticRebirthBase = 999;
			}
			if (this.PetMultiBattleRebirthBase > 999)
			{
				this.PetMultiBattleRebirthBase = 999;
			}
			this.PetMultiPhysicalRebirth = this.PetMultiPhysicalRebirthBase;
			this.PetMultiMysticRebirth = this.PetMultiMysticRebirthBase;
			this.PetMultiBattleRebirth = this.PetMultiBattleRebirthBase;
			this.PetMultiPhysicalRebirth += this.PetCampainBoostRebirth;
			this.PetMultiMysticRebirth += this.PetCampainBoostRebirth;
			this.PetMultiBattleRebirth += this.PetCampainBoostRebirth;
		}

		public void RecalculateMonumentMultis(GameState state)
		{
			this.MonumentMultiPhysicalRebirth = 1;
			this.MonumentMultiMysticRebirth = 1;
			this.MonumentMultiBattleRebirth = 1;
			this.MonumentMultiCreatingRebirth = 1;
			this.MonumentMultiPhysical = 1;
			this.MonumentMultiMystic = 1;
			this.MonumentMultiBattle = 1;
			this.MonumentMultiCreating = 1;
			foreach (Monument current in state.AllMonuments)
			{
				this.MonumentMultiPhysical += current.TotalMultiPhysical;
				this.MonumentMultiMystic += current.TotalMultiMystic;
				this.MonumentMultiBattle += current.TotalMultiBattle;
				this.MonumentMultiCreating += current.TotalMultiCreating;
				this.MonumentMultiPhysicalRebirth += current.TotalRebirthMultiPhysical;
				this.MonumentMultiMysticRebirth += current.TotalRebirthMultiMystic;
				this.MonumentMultiBattleRebirth += current.TotalRebirthMultiBattle;
				this.MonumentMultiCreatingRebirth += current.TotalRebirthMultiCreating;
			}
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.AchievementMultiPhysical.Serialize());
			Conv.AppendValue(stringBuilder, "b", this.AchievementMultiMystic.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.AchievementMultiBattle.Serialize());
			Conv.AppendValue(stringBuilder, "d", this.AchievementMultiCreating.Serialize());
			Conv.AppendValue(stringBuilder, "e", this.AchievementMultiPhysicalRebirth.Serialize());
			Conv.AppendValue(stringBuilder, "f", this.AchievementMultiMysticRebirth.Serialize());
			Conv.AppendValue(stringBuilder, "g", this.AchievementMultiBattleRebirth.Serialize());
			Conv.AppendValue(stringBuilder, "h", this.AchievementMultiCreatingRebirth.Serialize());
			Conv.AppendValue(stringBuilder, "i", this.GodMultiFromRebirth.Serialize());
			Conv.AppendValue(stringBuilder, "j", this.MonumentMultiPhysical.Serialize());
			Conv.AppendValue(stringBuilder, "k", this.MonumentMultiMystic.Serialize());
			Conv.AppendValue(stringBuilder, "l", this.MonumentMultiBattle.Serialize());
			Conv.AppendValue(stringBuilder, "m", this.MonumentMultiCreating.Serialize());
			Conv.AppendValue(stringBuilder, "n", this.MonumentMultiPhysicalRebirth.Serialize());
			Conv.AppendValue(stringBuilder, "o", this.MonumentMultiMysticRebirth.Serialize());
			Conv.AppendValue(stringBuilder, "p", this.MonumentMultiBattleRebirth.Serialize());
			Conv.AppendValue(stringBuilder, "q", this.MonumentMultiCreatingRebirth.Serialize());
			Conv.AppendValue(stringBuilder, "r", this.DrawMultiPhysical.Serialize());
			Conv.AppendValue(stringBuilder, "s", this.DrawMultiMystic.Serialize());
			Conv.AppendValue(stringBuilder, "t", this.DrawMultiBattle.Serialize());
			Conv.AppendValue(stringBuilder, "u", this.DrawMultiCreating.Serialize());
			Conv.AppendValue(stringBuilder, "v", this.PetCampainBoost.Serialize());
			Conv.AppendValue(stringBuilder, "w", this.PetCampainBoostRebirth.Serialize());
			return Conv.ToBase64(stringBuilder.ToString(), "Multi");
		}

		internal static Multi FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				return new Multi();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "Multi");
			Multi multi = new Multi();
			multi.AchievementMultiPhysical = new CDouble(Conv.getStringFromParts(parts, "a"));
			multi.AchievementMultiMystic = new CDouble(Conv.getStringFromParts(parts, "b"));
			multi.AchievementMultiBattle = new CDouble(Conv.getStringFromParts(parts, "c"));
			multi.AchievementMultiCreating = new CDouble(Conv.getStringFromParts(parts, "d"));
			multi.AchievementMultiPhysicalRebirth = new CDouble(Conv.getStringFromParts(parts, "e"));
			multi.AchievementMultiMysticRebirth = new CDouble(Conv.getStringFromParts(parts, "f"));
			multi.AchievementMultiBattleRebirth = new CDouble(Conv.getStringFromParts(parts, "g"));
			multi.AchievementMultiCreatingRebirth = new CDouble(Conv.getStringFromParts(parts, "h"));
			multi.GodMultiFromRebirth = new CDouble(Conv.getStringFromParts(parts, "i"));
			multi.MonumentMultiPhysical = new CDouble(Conv.getStringFromParts(parts, "j"));
			multi.MonumentMultiMystic = new CDouble(Conv.getStringFromParts(parts, "k"));
			multi.MonumentMultiBattle = new CDouble(Conv.getStringFromParts(parts, "l"));
			multi.MonumentMultiCreating = new CDouble(Conv.getStringFromParts(parts, "m"));
			multi.MonumentMultiPhysicalRebirth = new CDouble(Conv.getStringFromParts(parts, "n"));
			multi.MonumentMultiMysticRebirth = new CDouble(Conv.getStringFromParts(parts, "o"));
			multi.MonumentMultiBattleRebirth = new CDouble(Conv.getStringFromParts(parts, "p"));
			multi.MonumentMultiCreatingRebirth = new CDouble(Conv.getStringFromParts(parts, "q"));
			multi.DrawMultiPhysical = new CDouble(Conv.getStringFromParts(parts, "r"));
			multi.DrawMultiMystic = new CDouble(Conv.getStringFromParts(parts, "s"));
			multi.DrawMultiBattle = new CDouble(Conv.getStringFromParts(parts, "t"));
			multi.DrawMultiCreating = new CDouble(Conv.getStringFromParts(parts, "u"));
			multi.PetCampainBoost = new CDouble(Conv.getStringFromParts(parts, "v"));
			multi.PetCampainBoostRebirth = new CDouble(Conv.getStringFromParts(parts, "w"));
			if (multi.MonumentMultiPhysicalRebirth == 0)
			{
				multi.MonumentMultiPhysicalRebirth = 1;
			}
			if (multi.MonumentMultiMysticRebirth == 0)
			{
				multi.MonumentMultiMysticRebirth = 1;
			}
			if (multi.MonumentMultiBattleRebirth == 0)
			{
				multi.MonumentMultiBattleRebirth = 50;
			}
			if (multi.MonumentMultiCreatingRebirth == 0)
			{
				multi.MonumentMultiCreatingRebirth = 2;
			}
			if (multi.AchievementMultiPhysical == 0)
			{
				multi.AchievementMultiPhysical = 1;
			}
			if (multi.AchievementMultiMystic == 0)
			{
				multi.AchievementMultiMystic = 1;
			}
			if (multi.AchievementMultiBattle == 0)
			{
				multi.AchievementMultiBattle = 1;
			}
			if (multi.AchievementMultiCreating == 0)
			{
				multi.AchievementMultiCreating = 1;
			}
			if (multi.AchievementMultiPhysicalRebirth == 0)
			{
				multi.AchievementMultiPhysicalRebirth = 1;
			}
			if (multi.AchievementMultiMysticRebirth == 0)
			{
				multi.AchievementMultiMysticRebirth = 1;
			}
			if (multi.AchievementMultiBattleRebirth == 0)
			{
				multi.AchievementMultiBattleRebirth = 1;
			}
			if (multi.AchievementMultiCreatingRebirth == 0)
			{
				multi.AchievementMultiCreatingRebirth = 1;
			}
			if (multi.DrawMultiPhysical == 0)
			{
				multi.DrawMultiPhysical = 1;
			}
			if (multi.DrawMultiMystic == 0)
			{
				multi.DrawMultiMystic = 1;
			}
			if (multi.DrawMultiBattle == 0)
			{
				multi.DrawMultiBattle = 1;
			}
			if (multi.DrawMultiCreating == 0)
			{
				multi.DrawMultiCreating = 1;
			}
			return multi;
		}
	}
}
