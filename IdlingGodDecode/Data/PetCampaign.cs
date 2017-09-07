using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class PetCampaign
	{
		public Campaigns Type;

		public CDouble Difficulty = 0;

		public CDouble CurrentDuration = 0;

		public CDouble TotalDuration = 0;

		public List<PetType> PetIdsCampaign = new List<PetType>();

		public CDouble CampaingsFinished = 0;

		public List<int> RandomChances = new List<int>();

		public Growth SelectedGrowth = Growth.All;

		public string Description = string.Empty;

		public List<Pet> PetsInCampaign = new List<Pet>();

		public static List<PetCampaign> Initial = new List<PetCampaign>
		{
			new PetCampaign
			{
				Type = Campaigns.Growth
			},
			new PetCampaign
			{
				Type = Campaigns.Divinity
			},
			new PetCampaign
			{
				Type = Campaigns.Food
			},
			new PetCampaign
			{
				Type = Campaigns.Item
			},
			new PetCampaign
			{
				Type = Campaigns.Level
			},
			new PetCampaign
			{
				Type = Campaigns.Multiplier
			},
			new PetCampaign
			{
				Type = Campaigns.GodPower
			}
		};

		public string Desc
		{
			get
			{
				return EnumName.Description(this.Type);
			}
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", (int)this.Type);
			Conv.AppendValue(stringBuilder, "b", this.Difficulty.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.CurrentDuration.Serialize());
			Conv.AppendList<PetType>(stringBuilder, this.PetIdsCampaign, "d");
			Conv.AppendValue(stringBuilder, "e", this.TotalDuration.Serialize());
			Conv.AppendValue(stringBuilder, "f", this.CampaingsFinished.Serialize());
			Conv.AppendList<int>(stringBuilder, this.RandomChances, "g");
			Conv.AppendValue(stringBuilder, "h", (int)this.SelectedGrowth);
			return Conv.ToBase64(stringBuilder.ToString(), "PetCampaign");
		}

		internal static PetCampaign Deserialize(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				return new PetCampaign();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "PetCampaign");
			PetCampaign petCampaign = new PetCampaign();
			petCampaign.Type = (Campaigns)Conv.getIntFromParts(parts, "a");
			petCampaign.Difficulty = Conv.getCDoubleFromParts(parts, "b", false);
			petCampaign.CurrentDuration = Conv.getCDoubleFromParts(parts, "c", false);
			string stringFromParts = Conv.getStringFromParts(parts, "d");
			string[] array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string value = array2[i];
				if (!string.IsNullOrEmpty(value))
				{
					petCampaign.PetIdsCampaign.Add((PetType)Conv.StringToInt(value));
				}
			}
			petCampaign.TotalDuration = Conv.getCDoubleFromParts(parts, "e", false);
			petCampaign.CampaingsFinished = Conv.getCDoubleFromParts(parts, "f", false);
			stringFromParts = Conv.getStringFromParts(parts, "g");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array3 = array;
			for (int j = 0; j < array3.Length; j++)
			{
				string value2 = array3[j];
				if (!string.IsNullOrEmpty(value2))
				{
					petCampaign.RandomChances.Add(Conv.StringToInt(value2));
				}
			}
			petCampaign.SelectedGrowth = (Growth)Conv.getIntFromParts(parts, "h");
			return petCampaign;
		}

		public void Start(GameState state, int durationHours, List<Pet> selectedPets)
		{
			int num = 3600000;
			this.TotalDuration = num;
			if (durationHours > 0 && durationHours <= 12)
			{
				this.TotalDuration = num * durationHours;
			}
			this.CurrentDuration = 0;
			this.PetIdsCampaign = new List<PetType>();
			foreach (Pet current in selectedPets)
			{
				this.PetIdsCampaign.Add(current.TypeEnum);
			}
			this.InitPetsInCampaign(state);
			foreach (Pet current2 in this.PetsInCampaign)
			{
				current2.RemoveCloneCount(current2.ShadowCloneCount.ToInt());
				this.RandomChances.Add(UnityEngine.Random.Range(1, 100));
			}
		}

		public void InitPetsInCampaign(GameState state)
		{
			this.PetsInCampaign = new List<Pet>();
			bool flag = this.RandomChances.Count == this.PetIdsCampaign.Count;
			if (!flag)
			{
				this.RandomChances = new List<int>();
			}
			using (List<PetType>.Enumerator enumerator = this.PetIdsCampaign.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PetType petType = enumerator.Current;
					Pet pet = state.Ext.AllPets.FirstOrDefault((Pet x) => x.TypeEnum == petType);
					if (pet != null)
					{
						pet.IsInCampaign = true;
						this.PetsInCampaign.Add(pet);
					}
					if (!flag)
					{
						this.RandomChances.Add(UnityEngine.Random.Range(1, 100));
					}
				}
			}
		}

		public void Cancel()
		{
			this.TotalDuration = 0;
			this.CurrentDuration = 0;
			foreach (Pet current in this.PetsInCampaign)
			{
				current.IsInCampaign = false;
			}
			this.PetsInCampaign = new List<Pet>();
			this.PetIdsCampaign = new List<PetType>();
			this.Description = string.Empty;
		}

		public string CalculateResult(GameState state)
		{
			StringBuilder stringBuilder = new StringBuilder();
			CDouble cDouble = 0;
			Pet pet = null;
			foreach (Pet current in this.PetsInCampaign)
			{
				if (cDouble == 0 || current.GetTotalGrowth() < cDouble)
				{
					cDouble = current.GetTotalGrowth();
					pet = current;
				}
			}
			CDouble cDouble2 = 0;
			CDouble cDouble3 = 0;
			int num = (this.TotalDuration / 3600000).ToInt();
			CDouble cDouble4 = 0;
			CDouble cDouble5 = 0;
			CDouble cDouble6 = 0;
			CDouble cDouble7 = 0;
			CDouble cDouble8 = 0;
			CDouble cDouble9 = 0;
			CDouble cDouble10 = 0;
			CDouble cDouble11 = 0;
			stringBuilder.Append("Your pets ");
			for (int i = 0; i < this.PetsInCampaign.Count; i++)
			{
				stringBuilder.Append(this.PetsInCampaign[i]);
				if (i == this.PetsInCampaign.Count - 2)
				{
					stringBuilder.Append(" and ");
				}
				else if (i != this.PetsInCampaign.Count - 1)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.Append(" finished their " + this.Type + " Campaign.\n\n");
			int num2 = 0;
			CDouble cDouble12 = 100;
			foreach (Pet current2 in this.PetsInCampaign)
			{
				List<CampaignBoost> campaignBoost = current2.GetCampaignBoost();
				foreach (CampaignBoost current3 in campaignBoost)
				{
					CDouble rightSide = current3.Value;
					if (current2.TypeEnum == PetType.Turtle && num == 1)
					{
						rightSide = -25;
					}
					else if (current2.TypeEnum == PetType.Turtle && num == 12)
					{
						rightSide = 25;
					}
					if (current3.Type == Campaigns.Level)
					{
						cDouble12 += rightSide;
					}
				}
			}
			foreach (Pet current4 in this.PetsInCampaign)
			{
				List<CampaignBoost> campaignBoost2 = current4.GetCampaignBoost();
				CDouble leftSide = 100;
				CDouble leftSide2 = 100;
				CDouble cDouble13 = 100;
				CDouble cDouble14 = 100;
				CDouble cDouble15 = 100;
				CDouble leftSide3 = 100;
				foreach (CampaignBoost current5 in campaignBoost2)
				{
					CDouble rightSide2 = current5.Value;
					if (current4.TypeEnum == PetType.Turtle && num == 1)
					{
						rightSide2 = -25;
					}
					else if (current4.TypeEnum == PetType.Turtle && num == 12)
					{
						rightSide2 = 25;
					}
					if (current5.Type == Campaigns.Growth)
					{
						leftSide += rightSide2;
					}
					else if (current5.Type == Campaigns.Divinity)
					{
						leftSide2 += rightSide2;
					}
					else if (current5.Type == Campaigns.Food)
					{
						cDouble13 += rightSide2;
					}
					else if (current5.Type == Campaigns.Item)
					{
						cDouble14 += rightSide2;
					}
					else if (current5.Type == Campaigns.Multiplier)
					{
						cDouble15 += rightSide2;
					}
					else if (current5.Type == Campaigns.GodPower)
					{
						leftSide3 += rightSide2;
					}
				}
				int num3 = UnityEngine.Random.Range(1, 100);
				if (this.RandomChances.Count > num2)
				{
					num3 = this.RandomChances[num2];
				}
				CDouble cDouble16 = 100;
				if (state.Statistic.UltimatePetChallengesFinished > 0)
				{
					cDouble16 = 100 + state.Statistic.UltimatePetChallengesFinished * 5;
					if (cDouble16 > 200)
					{
						cDouble16 = 200;
					}
				}
				switch (this.Type)
				{
				case Campaigns.Growth:
					if (current4 != pet)
					{
						CDouble campaignValue = current4.GetCampaignValue(this.Type, num, state);
						cDouble2 += campaignValue;
						stringBuilder.Append(string.Concat(new object[]
						{
							current4.Name,
							" increased the growth gain for ",
							pet,
							" by ",
							campaignValue.ToGuiText(false),
							".\n"
						}));
					}
					break;
				case Campaigns.Divinity:
				{
					CDouble campaignValue2 = current4.GetCampaignValue(this.Type, num, state);
					cDouble3 += campaignValue2;
					stringBuilder.Append(current4.Name + " gained " + campaignValue2.GuiText + " divinity.\n");
					break;
				}
				case Campaigns.Food:
				{
					CDouble cDouble17 = 0;
					CDouble cDouble18 = 0;
					CDouble cDouble19 = 0;
					int num4 = (num * 3 * cDouble13 / 100).ToInt();
					num4 = (num4 * cDouble16 / 100).ToInt();
					bool flag = num3 <= num4;
					if (flag)
					{
						cDouble19 = 1;
					}
					int j = (num * 10 * cDouble13 / 100).ToInt();
					j = (j * cDouble16 / 100).ToInt();
					while (j > 100)
					{
						j -= 100;
						cDouble18 += 1;
					}
					bool flag2 = num3 <= j;
					if (flag2)
					{
						cDouble18 += 1;
					}
					int k = (num * 20 * cDouble13 / 100).ToInt();
					k = (k * cDouble16 / 100).ToInt();
					while (k > 100)
					{
						k -= 100;
						cDouble17 += 1;
					}
					bool flag3 = num3 <= k;
					if (flag2)
					{
						cDouble17 += 1;
					}
					if (cDouble19 > 0)
					{
						cDouble6 += cDouble19;
						stringBuilder.Append(current4.Name + " found " + cDouble19.GuiText + " mighty food!\n");
					}
					if (cDouble18 > 0)
					{
						cDouble5 += cDouble18;
						stringBuilder.Append(current4.Name + " found " + cDouble18.GuiText + " strong food.\n");
					}
					if (cDouble17 > 0)
					{
						cDouble4 += cDouble17;
						stringBuilder.Append(current4.Name + " found " + cDouble17.GuiText + " puny food.\n");
					}
					if (cDouble17 == 0)
					{
						stringBuilder.Append(current4.Name + " didn't find any food...\n");
					}
					break;
				}
				case Campaigns.Item:
				{
					CDouble campaignValue3 = current4.GetCampaignValue(this.Type, num, state);
					cDouble8 += campaignValue3;
					stringBuilder.Append(string.Concat(new object[]
					{
						current4.Name,
						" found ",
						campaignValue3,
						" pet stones.\n"
					}));
					int num5 = (num * (campaignValue3 / 100) * cDouble14 / 100).ToInt();
					bool flag4 = num3 <= num5;
					int num6 = num5 / 3;
					bool flag5 = num3 <= num6;
					bool flag6 = num3 <= num6 * 2;
					if (flag5)
					{
						cDouble9 = ++cDouble9;
						stringBuilder.Append(current4.Name + " found a lucky draw!\n");
					}
					else if (flag6)
					{
						cDouble11 = ++cDouble11;
						stringBuilder.Append(current4.Name + " found a godly liquid!\n");
					}
					else if (flag4)
					{
						cDouble10 = ++cDouble10;
						stringBuilder.Append(current4.Name + " found a chakra pill!\n");
					}
					break;
				}
				case Campaigns.Level:
				{
					CDouble campaignValue4 = current4.GetCampaignValue(this.Type, num, state);
					CDouble cDouble20 = campaignValue4 * cDouble12 / 100;
					current4.Level += cDouble20;
					if (cDouble20 > campaignValue4)
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							current4.Name,
							" gained ",
							campaignValue4.GuiText,
							" and an additional ",
							(cDouble20 - campaignValue4).GuiText,
							" levels!\n"
						}));
					}
					else
					{
						stringBuilder.Append(current4.Name + " gained " + campaignValue4.GuiText + " levels!\n");
					}
					break;
				}
				case Campaigns.Multiplier:
				{
					CDouble cDouble21 = 1;
					if (current4.GetTotalGrowth() > 500)
					{
						cDouble21 += 1;
					}
					if (current4.GetTotalGrowth() > 1000)
					{
						cDouble21 += 1;
					}
					if (current4.GetTotalGrowth() > 2500)
					{
						cDouble21 += 1;
					}
					if (current4.GetTotalGrowth() > 5000)
					{
						cDouble21 += 1;
					}
					if (current4.GetTotalGrowth() > 10000)
					{
						cDouble21 += 1;
					}
					if (current4.GetTotalGrowth() > 25000)
					{
						cDouble21 += 1;
					}
					CDouble cDouble22 = 0.5;
					if (current4.GetTotalStats() > 10000)
					{
						cDouble22 += 0.5;
					}
					if (current4.GetTotalStats() > 100000)
					{
						cDouble22 += 0.5;
					}
					if (current4.GetTotalStats() > 1000000)
					{
						cDouble22 += 0.5;
					}
					if (current4.GetTotalStats() > 5000000)
					{
						cDouble22 += 0.5;
					}
					if (current4.GetTotalStats() > 10000000)
					{
						cDouble22 += 0.5;
					}
					if (current4.GetTotalStats() > 100000000)
					{
						cDouble22 += 0.5;
					}
					cDouble21 = cDouble21 * num * cDouble15 / 200;
					cDouble22 = cDouble22 * num * cDouble15 / 500;
					cDouble22 = cDouble22 * cDouble16 / 100;
					cDouble21 = cDouble21 * cDouble16 / 100;
					CDouble petMultiBattle = state.Multiplier.PetMultiBattle;
					CDouble petMultiMystic = state.Multiplier.PetMultiMystic;
					CDouble petMultiPhysical = state.Multiplier.PetMultiPhysical;
					state.Multiplier.PetCampainBoost += cDouble22;
					state.Multiplier.PetCampainBoostRebirth += cDouble21;
					state.Multiplier.UpdatePetMultis(state);
					CDouble cDouble23 = state.Multiplier.PetMultiPhysical - petMultiPhysical;
					CDouble cDouble24 = state.Multiplier.PetMultiMystic - petMultiMystic;
					CDouble cDouble25 = state.Multiplier.PetMultiBattle - petMultiBattle;
					stringBuilder.Append(string.Concat(new string[]
					{
						current4.Name,
						" increased all your pet multipliers by ",
						cDouble22.ToGuiText(false),
						" % and added ",
						cDouble21.GuiText,
						"% to all pet rebirth multipliers!\n"
					}));
					break;
				}
				case Campaigns.GodPower:
				{
					CDouble campaignValue5 = current4.GetCampaignValue(this.Type, num, state);
					bool flag7 = num3 <= campaignValue5;
					if (flag7)
					{
						cDouble7 = ++cDouble7;
						stringBuilder.Append(current4.Name + " found one god power!\n");
					}
					else
					{
						stringBuilder.Append(current4.Name + " didn't find any god power...\n");
					}
					break;
				}
				}
				num2++;
			}
			if (this.Type == Campaigns.Divinity)
			{
				stringBuilder.Append("You received a total of " + cDouble3.GuiText + "!");
				state.Money += cDouble3;
			}
			else if (this.Type == Campaigns.GodPower)
			{
				if (cDouble7 == 0)
				{
					stringBuilder.Append("None of your pets found any god power...\n");
				}
				else
				{
					if (cDouble7 > 10)
					{
						cDouble7 = 10;
					}
					state.PremiumBoni.GodPowerFromPets += cDouble7;
					state.PremiumBoni.GodPower += cDouble7.ToInt();
					stringBuilder.Append("Your pets found a total of " + cDouble7.GuiText + " god power!\n");
				}
			}
			else if (this.Type == Campaigns.Growth)
			{
				switch (this.SelectedGrowth)
				{
				case Growth.Physical:
					pet.PhysicalGrowth += cDouble2;
					stringBuilder.Append(string.Concat(new string[]
					{
						"The physical growth for ",
						pet.Name,
						" has increased by ",
						cDouble2.ToGuiText(false),
						"!"
					}));
					break;
				case Growth.Mystic:
					pet.MysticGrowth += cDouble2;
					stringBuilder.Append(string.Concat(new string[]
					{
						"The mystic growth for ",
						pet.Name,
						" has increased by ",
						cDouble2.ToGuiText(false),
						"!"
					}));
					break;
				case Growth.Battle:
					pet.BattleGrowth += cDouble2;
					stringBuilder.Append(string.Concat(new string[]
					{
						"The battle growth for ",
						pet.Name,
						" has increased by ",
						cDouble2.ToGuiText(false),
						"!"
					}));
					break;
				case Growth.All:
				{
					CDouble cDouble26 = cDouble2 / 3;
					if (cDouble26 < 0.25)
					{
						cDouble26 = 0.25;
					}
					cDouble26 = Conv.RoundToOneFourth(cDouble26.Double);
					pet.PhysicalGrowth += cDouble26;
					pet.MysticGrowth += cDouble26;
					pet.BattleGrowth += cDouble26;
					stringBuilder.Append(string.Concat(new string[]
					{
						"The growth of physical, mystic and battle for ",
						pet.Name,
						" has increased by ",
						cDouble26.ToGuiText(false),
						"!"
					}));
					break;
				}
				}
				pet.CalculateValues();
			}
			else if (this.Type == Campaigns.Food)
			{
				if (cDouble6 > 0)
				{
					state.Ext.MightyFood += cDouble6;
					stringBuilder.Append("Your pets found a total of " + cDouble6.GuiText + " mighty food!\n");
				}
				if (cDouble5 > 0)
				{
					state.Ext.StrongFood += cDouble5;
					stringBuilder.Append("Your pets found a total of " + cDouble5.GuiText + " strong food!\n");
				}
				if (cDouble4 > 0)
				{
					state.Ext.PunyFood += cDouble4;
					stringBuilder.Append("Your pets found a total of " + cDouble4.GuiText + " puny food!\n");
				}
				if (cDouble4 == 0)
				{
					stringBuilder.Append("None of your pets found any food...\n");
				}
			}
			else if (this.Type == Campaigns.Item)
			{
				state.Ext.PetStones += cDouble8;
				if (state.Statistic.HasStartedUltimatePetChallenge)
				{
					CDouble cDouble27 = cDouble8 / 10;
					if (cDouble27 < 1)
					{
						cDouble27 = 1;
					}
					stringBuilder.Append(string.Concat(new string[]
					{
						"Your pets found a total of ",
						cDouble27.GuiText,
						" pet pills. Your pets ate them to increase their multi by ",
						cDouble27.GuiText,
						".\n"
					}));
					if (state.Ext.PetPowerMultiCampaigns < 1)
					{
						state.Ext.PetPowerMultiCampaigns = 1;
					}
					state.Ext.PetPowerMultiCampaigns = state.Ext.PetPowerMultiCampaigns * cDouble27;
				}
				stringBuilder.Append("Your pets found a total of " + cDouble8.GuiText + " pet stones.\n");
				if (cDouble9 > 0)
				{
					stringBuilder.Append("Your pets found a total of " + cDouble9.GuiText + " lucky draws!\n");
					state.PremiumBoni.LuckyDraws += cDouble9;
				}
				if (cDouble11 > 0)
				{
					stringBuilder.Append("Your pets found a total of " + cDouble11.GuiText + " godly liquids!\n");
					state.PremiumBoni.GodlyLiquidCount += cDouble11;
				}
				if (cDouble10 > 0)
				{
					stringBuilder.Append("Your pets found a total of " + cDouble10.GuiText + " chakra pills!\n");
					state.PremiumBoni.ChakraPillCount += cDouble10;
				}
			}
			this.CampaingsFinished = ++this.CampaingsFinished;
			this.Cancel();
			return stringBuilder.ToString();
		}

		public void UpdateDuration(long ms)
		{
			if (string.IsNullOrEmpty(this.Description))
			{
				this.Description = this.Desc;
			}
			if (this.TotalDuration > 0)
			{
				this.CurrentDuration += ms;
				this.Description = this.Desc + "\nThe campaign is finished in " + Conv.MsToGuiText((this.TotalDuration - this.CurrentDuration).ToLong(), true);
			}
		}
	}
}
