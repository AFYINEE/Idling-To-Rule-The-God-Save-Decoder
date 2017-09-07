using Assets.Scripts.Helper;
using System;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class ClothingPart
	{
		public ClothingPartEnum Id;

		public CDouble PermanentGPCost = 0;

		public int CreationTierNeeded;

		public int GodDefeatedTierNeeded;

		public bool IsPermanentUnlocked;

		public bool IsPlayerSet;

		public bool PreviousValue;

		public string Name = string.Empty;

		public Texture2D Image;

		public bool IsPreview;

		public bool IsSet
		{
			get
			{
				return this.IsPreview || (this.IsPlayerSet && (this.IsPermanentUnlocked || (this.Id == ClothingPartEnum.arty_reward && App.State != null && App.State.Statistic.ArtyChallengesFinished > 0) || string.IsNullOrEmpty(this.NameOfMissing())));
			}
		}

		public ClothingPart()
		{
		}

		public ClothingPart(ClothingPartEnum id, int gpCost, int godTierNeeded, bool isPermanentUnlocked, bool isPlayerSet)
		{
			this.Id = id;
			this.PermanentGPCost = gpCost;
			this.GodDefeatedTierNeeded = godTierNeeded;
			this.IsPermanentUnlocked = isPermanentUnlocked;
			this.IsPlayerSet = isPlayerSet;
		}

		public string NameOfMissing()
		{
			string result = string.Empty;
			if (this.IsPermanentUnlocked)
			{
				return string.Empty;
			}
			if (this.CreationTierNeeded > 0)
			{
				Creation creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == (Creation.CreationType)this.CreationTierNeeded);
				if (!creation.CanBuy)
				{
					result = creation.Name;
				}
			}
			else if (this.GodDefeatedTierNeeded > 0)
			{
				if (App.State.Statistic.HighestGodDefeated >= this.GodDefeatedTierNeeded)
				{
					result = string.Empty;
				}
				else if (this.GodDefeatedTierNeeded > 28)
				{
					if (App.State.PrinnyBaal.Level + 28 <= this.GodDefeatedTierNeeded)
					{
						result = "P. Baal v " + (this.GodDefeatedTierNeeded - 28);
					}
				}
				else
				{
					Creation creation2 = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == (Creation.CreationType)this.GodDefeatedTierNeeded);
					if (!creation2.GodToDefeat.IsDefeated)
					{
						result = creation2.GodToDefeat.Name;
					}
				}
			}
			else if (this.PermanentGPCost > 0)
			{
				result = this.PermanentGPCost + string.Empty;
			}
			return result;
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", (int)this.Id);
			Conv.AppendValue(stringBuilder, "b", this.PermanentGPCost.ToString());
			Conv.AppendValue(stringBuilder, "c", this.CreationTierNeeded.ToString());
			Conv.AppendValue(stringBuilder, "d", this.GodDefeatedTierNeeded.ToString());
			Conv.AppendValue(stringBuilder, "e", this.IsPermanentUnlocked.ToString());
			Conv.AppendValue(stringBuilder, "f", this.IsPlayerSet.ToString());
			return Conv.ToBase64(stringBuilder.ToString(), "ClothingPart");
		}

		internal static ClothingPart FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("ClothingPart.FromString with empty value!");
				return null;
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "ClothingPart");
			ClothingPart clothingPart = new ClothingPart();
			clothingPart.Id = (ClothingPartEnum)Conv.getIntFromParts(parts, "a");
			clothingPart.PermanentGPCost = Conv.getDoubleFromParts(parts, "b");
			clothingPart.CreationTierNeeded = Conv.getIntFromParts(parts, "c");
			clothingPart.GodDefeatedTierNeeded = Conv.getIntFromParts(parts, "d");
			clothingPart.IsPermanentUnlocked = Conv.getStringFromParts(parts, "e").ToLower().Equals("true");
			clothingPart.IsPlayerSet = Conv.getStringFromParts(parts, "f").ToLower().Equals("true");
			if (clothingPart.Id == ClothingPartEnum.arty_reward)
			{
				clothingPart.PermanentGPCost = 999999;
			}
			return clothingPart;
		}
	}
}
