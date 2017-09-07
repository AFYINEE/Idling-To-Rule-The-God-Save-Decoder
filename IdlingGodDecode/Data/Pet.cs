using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class Pet
	{
		private enum HighestGrowth
		{
			physical,
			mystic,
			battle,
			notPhysical,
			notMystic,
			notBattle,
			none
		}

		public string Name = string.Empty;

		public string Description = string.Empty;

		public string ImagePath = string.Empty;

		public Texture2D Image;

		public CDouble PhysicalGrowth = 0;

		public CDouble MysticGrowth = 0;

		public CDouble BattleGrowth = 0;

		public CDouble Level = 0;

		public CDouble Exp = 0;

		public CDouble CurrentHealth = 0;

		public PetType TypeEnum = PetType.Cat;

		public CDouble FeedTimer = 0;

		public CDouble ShadowCloneCount = 0;

		public CDouble ClonePhysical = 0;

		public CDouble CloneMystic = 0;

		public CDouble CloneBattle = 0;

		public CDouble CloneCurrentHealth = 0;

		public long ZeroHealthTime;

		public bool IsInCampaign;

		public int SortValue;

		private List<CampaignBoost> campaignBoosts = new List<CampaignBoost>();

		private string campaignDescShort = string.Empty;

		private string campaignDesc = string.Empty;

		public string DescGrowthCampaign = string.Empty;

		public string DescDivinityCampaign = string.Empty;

		public string DescFoodCampaign = string.Empty;

		public string DescItemsCampaign = string.Empty;

		public string DescLevelCampaign = string.Empty;

		public string DescMultiplierCampaign = string.Empty;

		public string DescGodPowerCampaign = string.Empty;

		public CDouble Physical = 0;

		public CDouble Mystic = 0;

		public CDouble Battle = 0;

		public CDouble MaxHealth = 0;

		public CDouble FullnessPercent = 0;

		public CDouble CloneExp = 0;

		public CDouble CloneMaxHealth = 0;

		public CDouble ExpNeeded = 10;

		private CDouble HpRecover = new CDouble();

		public CDouble HpRecoverSec = new CDouble();

		public CDouble HpRecoverSecClone = new CDouble();

		public const long MaxFeedTime = 43200000L;

		public const long FeedTime75 = 32400000L;

		public const long FeedTime50 = 21600000L;

		public const long FeedTime10 = 4320000L;

		public const string DescFreeFood = "Increases each of the three growth stats by 1, if the feeding bar is below 10% or by 0.5, if the feeding bar is between 10% and 50%, or by 0.25, if the feeding bar is above 50%.";

		public const string DescPunyFood = "Increases each of the three growth stats by 2, if the feeding bar is below 10% or by 1, if the feeding bar is between 10% and 50%, or by 0.5, if the feeding bar is above 50%.";

		public const string DescStrongFood = "Increases each of the three growth stats by 3.5, if the feeding bar is below 10% or by 2, if the feeding bar is between 10% and 50%, or by 1, if the feeding bar is above 50%.";

		public const string DescMightyFood = "Increases each of the three growth stats by 5, if the feeding bar is below 10% or by 3, if the feeding bar is between 10% and 50%, or by 1.5, if the feeding bar is above 50%.";

		public string Desc
		{
			get
			{
				return EnumName.Description(this.TypeEnum);
			}
		}

		public bool IsUnlocked
		{
			get;
			private set;
		}

		private CDouble LevelMultiplier
		{
			get
			{
				this.Level.Round();
				CDouble rightSide;
				if (this.Level > 900)
				{
					rightSide = 450 + (this.Level - 900);
				}
				else if (this.Level > 800)
				{
					rightSide = 360 + 0.9 * (this.Level - 800);
				}
				else if (this.Level > 700)
				{
					rightSide = 280 + 0.8 * (this.Level - 700);
				}
				else if (this.Level > 600)
				{
					rightSide = 210 + 0.7 * (this.Level - 600);
				}
				else if (this.Level > 500)
				{
					rightSide = 150 + 0.6 * (this.Level - 500);
				}
				else if (this.Level > 400)
				{
					rightSide = 100 + 0.5 * (this.Level - 400);
				}
				else if (this.Level > 300)
				{
					rightSide = 60 + 0.4 * (this.Level - 300);
				}
				else if (this.Level > 200)
				{
					rightSide = 30 + 0.3 * (this.Level - 200);
				}
				else if (this.Level > 100)
				{
					rightSide = 10 + 0.2 * (this.Level - 100);
				}
				else
				{
					rightSide = 0.1 * this.Level;
				}
				return 0.9 + rightSide;
			}
		}

		public bool CanFeed
		{
			get
			{
				return this.FeedTimer <= 32400000L;
			}
		}

		public Pet()
		{
			this.IsUnlocked = false;
		}

		public override string ToString()
		{
			return this.Name;
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.Name.ToString());
			Conv.AppendValue(stringBuilder, "b", this.ImagePath.ToString());
			Conv.AppendValue(stringBuilder, "d", this.PhysicalGrowth.Serialize());
			Conv.AppendValue(stringBuilder, "e", this.MysticGrowth.Serialize());
			Conv.AppendValue(stringBuilder, "f", this.BattleGrowth.Serialize());
			Conv.AppendValue(stringBuilder, "g", this.Level.Serialize());
			Conv.AppendValue(stringBuilder, "h", this.Exp.Serialize());
			Conv.AppendValue(stringBuilder, "j", this.CurrentHealth.Serialize());
			Conv.AppendValue(stringBuilder, "k", (int)this.TypeEnum);
			Conv.AppendValue(stringBuilder, "l", this.IsUnlocked.ToString());
			Conv.AppendValue(stringBuilder, "m", this.FeedTimer.Serialize());
			Conv.AppendValue(stringBuilder, "n", this.ShadowCloneCount.Serialize());
			Conv.AppendValue(stringBuilder, "o", this.ClonePhysical.Serialize());
			Conv.AppendValue(stringBuilder, "p", this.CloneMystic.Serialize());
			Conv.AppendValue(stringBuilder, "q", this.CloneBattle.Serialize());
			Conv.AppendValue(stringBuilder, "r", this.CloneCurrentHealth.Serialize());
			Conv.AppendValue(stringBuilder, "s", this.ZeroHealthTime);
			return Conv.ToBase64(stringBuilder.ToString(), "Pet");
		}

		internal static Pet Deserialize(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("Pet.FromString with empty value!");
				return new Pet();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "Pet");
			Pet pet = new Pet();
			pet.Name = Conv.getStringFromParts(parts, "a");
			pet.ImagePath = Conv.getStringFromParts(parts, "b");
			pet.PhysicalGrowth = Conv.getCDoubleFromParts(parts, "d", false);
			pet.MysticGrowth = Conv.getCDoubleFromParts(parts, "e", false);
			pet.BattleGrowth = Conv.getCDoubleFromParts(parts, "f", false);
			pet.Level = Conv.getCDoubleFromParts(parts, "g", false);
			pet.Exp = Conv.getCDoubleFromParts(parts, "h", false);
			pet.CurrentHealth = Conv.getCDoubleFromParts(parts, "j", false);
			pet.TypeEnum = (PetType)Conv.getIntFromParts(parts, "k");
			pet.IsUnlocked = Conv.getStringFromParts(parts, "l").ToLower().Equals("true");
			pet.FeedTimer = Conv.getCDoubleFromParts(parts, "m", false);
			pet.ShadowCloneCount = Conv.getCDoubleFromParts(parts, "n", false);
			pet.ClonePhysical = Conv.getCDoubleFromParts(parts, "o", false);
			pet.CloneMystic = Conv.getCDoubleFromParts(parts, "p", false);
			pet.CloneBattle = Conv.getCDoubleFromParts(parts, "q", false);
			pet.CloneCurrentHealth = Conv.getCDoubleFromParts(parts, "r", false);
			pet.ZeroHealthTime = Conv.getLongFromParts(parts, "s");
			if (pet.Level == 0)
			{
				pet.Level = 1;
			}
			return pet;
		}

		public static List<Pet> InitialPets()
		{
			return new List<Pet>
			{
				new Pet
				{
					Name = "Mouse",
					ImagePath = "Gui/pets/mouse",
					TypeEnum = PetType.Mouse,
					PhysicalGrowth = 3,
					MysticGrowth = 4,
					BattleGrowth = 5
				},
				new Pet
				{
					Name = "Cupid",
					ImagePath = "Gui/pets/cupid",
					TypeEnum = PetType.Cupid,
					PhysicalGrowth = 14,
					MysticGrowth = 2,
					BattleGrowth = 17
				},
				new Pet
				{
					Name = "Squirrel",
					ImagePath = "Gui/pets/squirrel",
					TypeEnum = PetType.Squirrel,
					PhysicalGrowth = 20,
					MysticGrowth = 20,
					BattleGrowth = 20
				},
				new Pet
				{
					Name = "Rabbit",
					ImagePath = "Gui/pets/rabbit",
					TypeEnum = PetType.Rabbit,
					PhysicalGrowth = 40,
					MysticGrowth = 10,
					BattleGrowth = 60
				},
				new Pet
				{
					Name = "Cat",
					ImagePath = "Gui/pets/cat",
					TypeEnum = PetType.Cat,
					PhysicalGrowth = 70,
					MysticGrowth = 90,
					BattleGrowth = 80
				},
				new Pet
				{
					Name = "Dog",
					ImagePath = "Gui/pets/dog",
					TypeEnum = PetType.Dog,
					PhysicalGrowth = 150,
					MysticGrowth = 35,
					BattleGrowth = 99
				},
				new Pet
				{
					Name = "Fairy",
					ImagePath = "Gui/pets/faery",
					TypeEnum = PetType.Fairy,
					PhysicalGrowth = 1,
					MysticGrowth = 250,
					BattleGrowth = 120
				},
				new Pet
				{
					Name = "Dragon",
					ImagePath = "Gui/pets/dragon",
					TypeEnum = PetType.Dragon,
					PhysicalGrowth = 300,
					MysticGrowth = 300,
					BattleGrowth = 300
				},
				new Pet
				{
					Name = "Snake",
					ImagePath = "Gui/pets/snake",
					TypeEnum = PetType.Snake,
					PhysicalGrowth = 350,
					MysticGrowth = 500,
					BattleGrowth = 350
				},
				new Pet
				{
					Name = "Shark",
					ImagePath = "Gui/pets/shark",
					TypeEnum = PetType.Shark,
					PhysicalGrowth = 400,
					MysticGrowth = 400,
					BattleGrowth = 850
				},
				new Pet
				{
					Name = "Octopus",
					ImagePath = "Gui/pets/octopus",
					TypeEnum = PetType.Octopus,
					PhysicalGrowth = 777,
					MysticGrowth = 777,
					BattleGrowth = 777
				},
				new Pet
				{
					Name = "Slime",
					ImagePath = "Gui/pets/slime",
					TypeEnum = PetType.Slime,
					PhysicalGrowth = 1250,
					MysticGrowth = 1000,
					BattleGrowth = 1250
				},
				new Pet
				{
					Name = "Mole",
					ImagePath = "Gui/pets/mole",
					TypeEnum = PetType.Mole,
					PhysicalGrowth = 50,
					MysticGrowth = 50,
					BattleGrowth = 50
				},
				new Pet
				{
					Name = "Camel",
					ImagePath = "Gui/pets/camel",
					TypeEnum = PetType.Camel,
					PhysicalGrowth = 200,
					MysticGrowth = 150,
					BattleGrowth = 100
				},
				new Pet
				{
					Name = "Goat",
					ImagePath = "Gui/pets/goat",
					TypeEnum = PetType.Goat,
					PhysicalGrowth = 300,
					MysticGrowth = 300,
					BattleGrowth = 300
				},
				new Pet
				{
					Name = "Turtle",
					ImagePath = "Gui/pets/turtle",
					TypeEnum = PetType.Turtle,
					PhysicalGrowth = 600,
					MysticGrowth = 60,
					BattleGrowth = 1
				},
				new Pet
				{
					Name = "Doughnut",
					ImagePath = "Gui/pets/donut",
					TypeEnum = PetType.Doughnut,
					PhysicalGrowth = 7,
					MysticGrowth = 7,
					BattleGrowth = 7
				},
				new Pet
				{
					Name = "Eagle",
					ImagePath = "Gui/pets/eagle",
					TypeEnum = PetType.Eagle,
					PhysicalGrowth = 77,
					MysticGrowth = 77,
					BattleGrowth = 77
				},
				new Pet
				{
					Name = "Penguin",
					ImagePath = "Gui/pets/penguin",
					TypeEnum = PetType.Penguin,
					PhysicalGrowth = 50,
					MysticGrowth = 200,
					BattleGrowth = 150
				},
				new Pet
				{
					Name = "Phoenix",
					ImagePath = "Gui/pets/phoenix",
					TypeEnum = PetType.Phoenix,
					PhysicalGrowth = 150,
					MysticGrowth = 150,
					BattleGrowth = 150
				},
				new Pet
				{
					Name = "Wizard",
					ImagePath = "Gui/pets/wizard",
					TypeEnum = PetType.Wizard,
					PhysicalGrowth = 1,
					MysticGrowth = 60,
					BattleGrowth = 600
				},
				new Pet
				{
					Name = "Pegasus",
					ImagePath = "Gui/pets/pegasus",
					TypeEnum = PetType.Pegasus,
					PhysicalGrowth = 150,
					MysticGrowth = 500,
					BattleGrowth = 150
				},
				new Pet
				{
					Name = "Ufo",
					ImagePath = "Gui/pets/ufo",
					TypeEnum = PetType.Ufo,
					PhysicalGrowth = 250,
					MysticGrowth = 999,
					BattleGrowth = 250
				},
				new Pet
				{
					Name = "Robot",
					ImagePath = "Gui/pets/robot",
					TypeEnum = PetType.Robot,
					PhysicalGrowth = 750,
					MysticGrowth = 999,
					BattleGrowth = 1500
				}
			};
		}

		internal static void CheckIfAllAdded(ref List<Pet> allPets)
		{
			if (allPets == null || allPets.Count == 0)
			{
				allPets = Pet.InitialPets();
				return;
			}
			List<Pet> list = Pet.InitialPets();
			foreach (Pet current in list)
			{
				Pet.AddIfMissing(ref allPets, list, current.TypeEnum);
			}
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Mouse).SortValue = 0;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Cupid).SortValue = 1;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Squirrel).SortValue = 2;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Rabbit).SortValue = 3;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Cat).SortValue = 4;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Dog).SortValue = 5;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Fairy).SortValue = 6;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Dragon).SortValue = 7;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Snake).SortValue = 8;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Shark).SortValue = 9;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Octopus).SortValue = 10;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Slime).SortValue = 11;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Mole).SortValue = 12;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Camel).SortValue = 13;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Goat).SortValue = 14;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Turtle).SortValue = 15;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Doughnut).SortValue = 16;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Eagle).SortValue = 17;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Penguin).SortValue = 18;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Phoenix).SortValue = 19;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Wizard).SortValue = 20;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Pegasus).SortValue = 21;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Ufo).SortValue = 22;
			allPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Robot).SortValue = 23;
		}

		private static void AddIfMissing(ref List<Pet> allPets, List<Pet> initial, PetType missingPet)
		{
			if (allPets.FirstOrDefault((Pet x) => x.TypeEnum == missingPet) == null)
			{
				Pet item = initial.FirstOrDefault((Pet x) => x.TypeEnum == missingPet);
				allPets.Add(item);
			}
		}

		public CDouble GetTotalGrowth()
		{
			return this.BattleGrowth + this.PhysicalGrowth + this.MysticGrowth;
		}

		public CDouble GetTotalStats()
		{
			return this.Battle + this.Physical + this.Mystic;
		}

		public List<CampaignBoost> GetCampaignBoost()
		{
			this.SetCampaignInfo();
			return this.campaignBoosts;
		}

		public string GetCampaignDescription()
		{
			if (string.IsNullOrEmpty(this.campaignDesc))
			{
				this.SetCampaignInfo();
			}
			return this.campaignDesc;
		}

		public string GetShortCampaignDescription()
		{
			if (string.IsNullOrEmpty(this.campaignDescShort))
			{
				this.SetCampaignInfo();
			}
			return this.campaignDescShort;
		}

		public string GetResultDescForCampagin(Campaigns campaign)
		{
			switch (campaign)
			{
			case Campaigns.Growth:
				return this.DescGrowthCampaign;
			case Campaigns.Divinity:
				return this.DescDivinityCampaign;
			case Campaigns.Food:
				return this.DescFoodCampaign;
			case Campaigns.Item:
				return this.DescItemsCampaign;
			case Campaigns.Level:
				return this.DescLevelCampaign;
			case Campaigns.Multiplier:
				return this.DescMultiplierCampaign;
			case Campaigns.GodPower:
				return this.DescGodPowerCampaign;
			default:
				return string.Empty;
			}
		}

		public CDouble GetCampaignValue(Campaigns campaign, CDouble durationHours, GameState state)
		{
			CDouble cDouble = 100;
			CDouble cDouble2 = 100;
			CDouble cDouble3 = 100;
			CDouble cDouble4 = 100;
			CDouble cDouble5 = 100;
			CDouble cDouble6 = 100;
			CDouble cDouble7 = 100;
			foreach (CampaignBoost current in this.campaignBoosts)
			{
				CDouble rightSide = current.Value;
				if (this.TypeEnum == PetType.Turtle && durationHours == 1)
				{
					rightSide = -25;
				}
				else if (this.TypeEnum == PetType.Turtle && durationHours == 12)
				{
					rightSide = 25;
				}
				if (current.Type == Campaigns.Growth)
				{
					cDouble += rightSide;
				}
				else if (current.Type == Campaigns.Divinity)
				{
					cDouble2 += rightSide;
				}
				else if (current.Type == Campaigns.Food)
				{
					cDouble3 += rightSide;
				}
				else if (current.Type == Campaigns.Item)
				{
					cDouble4 += rightSide;
				}
				else if (current.Type == Campaigns.Multiplier)
				{
					cDouble5 += rightSide;
				}
				else if (current.Type == Campaigns.GodPower)
				{
					cDouble6 += rightSide;
				}
			}
			if (state.Statistic.UltimatePetChallengesFinished > 0)
			{
				cDouble7 = 100 + state.Statistic.UltimatePetChallengesFinished * 5;
				if (cDouble7 > 200)
				{
					cDouble7 = 200;
				}
			}
			switch (campaign)
			{
			case Campaigns.Growth:
			{
				CDouble cDouble8 = 0.25;
				if (this.GetTotalGrowth() > 500)
				{
					cDouble8 += 0.25;
				}
				if (this.GetTotalGrowth() > 1000)
				{
					cDouble8 += 0.25;
				}
				if (this.GetTotalGrowth() > 2500)
				{
					cDouble8 += 0.25;
				}
				if (this.GetTotalGrowth() > 5000)
				{
					cDouble8 += 0.25;
				}
				if (this.GetTotalGrowth() > 10000)
				{
					cDouble8 += 0.25;
				}
				if (this.GetTotalGrowth() > 25000)
				{
					cDouble8 += 0.5;
				}
				cDouble8 = cDouble8 * cDouble / 100 * durationHours;
				cDouble8 = cDouble8 * cDouble7 / 100;
				cDouble8 = Conv.RoundToOneFourth(cDouble8.Double);
				this.DescGrowthCampaign = this.Name + " will increase the growth of your weakest pet by " + cDouble8.ToGuiText(false) + ".";
				return cDouble8;
			}
			case Campaigns.Divinity:
			{
				CDouble cDouble9 = 300;
				if (this.GetTotalStats() > 10000)
				{
					cDouble9 += 300;
				}
				if (this.GetTotalStats() > 100000)
				{
					cDouble9 += 300;
				}
				if (this.GetTotalStats() > 1000000)
				{
					cDouble9 += 300;
				}
				if (this.GetTotalStats() > 5000000)
				{
					cDouble9 += 300;
				}
				if (this.GetTotalStats() > 10000000)
				{
					cDouble9 += 300;
				}
				if (this.GetTotalStats() > 100000000)
				{
					cDouble9 += 300;
				}
				cDouble9 = state.DivinityGainSec(true) * cDouble9 * durationHours * cDouble2 / 100;
				cDouble9 = cDouble9 * cDouble7 / 100;
				this.DescDivinityCampaign = this.Name + " will increase your divinity by " + cDouble9.GuiText + ".";
				return cDouble9;
			}
			case Campaigns.Food:
			{
				int num = (durationHours * 3 * cDouble3 / 100).ToInt();
				int num2 = (durationHours * 10 * cDouble3 / 100).ToInt();
				int num3 = (durationHours * 20 * cDouble3 / 100).ToInt();
				num = (num * cDouble7 / 100).ToInt();
				num2 = (num2 * cDouble7 / 100).ToInt();
				num3 = (num3 * cDouble7 / 100).ToInt();
				this.DescFoodCampaign = string.Concat(new object[]
				{
					this.Name,
					" has a ",
					num,
					"% chance to find mighty food, ",
					num2,
					"% chance to find strong food, ",
					num3,
					"% chance to find puny food."
				});
				return 0;
			}
			case Campaigns.Item:
			{
				CDouble cDouble10 = 1;
				if (this.GetTotalStats() > 10000)
				{
					cDouble10 += 1;
				}
				if (this.GetTotalStats() > 100000)
				{
					cDouble10 += 1;
				}
				if (this.GetTotalStats() > 500000)
				{
					cDouble10 += 1;
				}
				if (this.GetTotalStats() > 1000000)
				{
					cDouble10 += 1;
				}
				if (this.GetTotalStats() > 5000000)
				{
					cDouble10 += 1;
				}
				if (this.GetTotalStats() > 10000000)
				{
					cDouble10 += 1;
				}
				if (this.GetTotalStats() > 50000000)
				{
					cDouble10 += 1;
				}
				if (this.GetTotalStats() > 100000000)
				{
					cDouble10 += 2;
				}
				cDouble10 = cDouble10 * durationHours * cDouble4 / 100;
				cDouble10 = cDouble10 * cDouble7 / 100;
				cDouble10.Round();
				int num4 = (durationHours * cDouble10 / 100).ToInt();
				this.DescItemsCampaign = string.Concat(new object[]
				{
					this.Name,
					" will find ",
					cDouble10.GuiText,
					" pet stones, and has a chance of ",
					num4,
					" % to find a lucky draw, godly liquid or chakra pill."
				});
				return cDouble10;
			}
			case Campaigns.Level:
			{
				CDouble cDouble11 = 10;
				if (this.GetTotalGrowth() > 500)
				{
					cDouble11 += 10;
				}
				if (this.GetTotalGrowth() > 1000)
				{
					cDouble11 += 15;
				}
				if (this.GetTotalGrowth() > 2500)
				{
					cDouble11 += 20;
				}
				if (this.GetTotalGrowth() > 5000)
				{
					cDouble11 += 25;
				}
				if (this.GetTotalGrowth() > 10000)
				{
					cDouble11 += 30;
				}
				if (this.GetTotalGrowth() > 25000)
				{
					cDouble11 += 50;
				}
				CDouble leftSide = 10;
				if (this.GetTotalStats() > 10000)
				{
					leftSide += 10;
				}
				if (this.GetTotalStats() > 100000)
				{
					leftSide += 15;
				}
				if (this.GetTotalStats() > 1000000)
				{
					leftSide += 20;
				}
				if (this.GetTotalStats() > 5000000)
				{
					leftSide += 25;
				}
				if (this.GetTotalStats() > 10000000)
				{
					leftSide += 30;
				}
				if (this.GetTotalStats() > 100000000)
				{
					leftSide += 50;
				}
				CDouble cDouble12 = (leftSide + cDouble11) * durationHours;
				if (this.Level + cDouble12 > 1000)
				{
					cDouble12 *= 0.9;
				}
				if (this.Level + cDouble12 > 5000)
				{
					cDouble12 *= 0.9;
				}
				if (this.Level + cDouble12 > 10000)
				{
					cDouble12 *= 0.8;
				}
				cDouble12 = cDouble12 * cDouble7 / 100;
				this.DescLevelCampaign = this.Name + " can gain " + cDouble12.GuiText + " levels. This can be increased if pets who are good at this campaign are in the party.";
				return cDouble12;
			}
			case Campaigns.Multiplier:
			{
				CDouble cDouble13 = 1;
				if (this.GetTotalGrowth() > 500)
				{
					cDouble13 += 1;
				}
				if (this.GetTotalGrowth() > 1000)
				{
					cDouble13 += 1;
				}
				if (this.GetTotalGrowth() > 2500)
				{
					cDouble13 += 1;
				}
				if (this.GetTotalGrowth() > 5000)
				{
					cDouble13 += 1;
				}
				if (this.GetTotalGrowth() > 10000)
				{
					cDouble13 += 1;
				}
				if (this.GetTotalGrowth() > 25000)
				{
					cDouble13 += 1;
				}
				CDouble cDouble14 = 0.5;
				if (this.GetTotalStats() > 10000)
				{
					cDouble14 += 0.5;
				}
				if (this.GetTotalStats() > 100000)
				{
					cDouble14 += 0.5;
				}
				if (this.GetTotalStats() > 1000000)
				{
					cDouble14 += 0.5;
				}
				if (this.GetTotalStats() > 5000000)
				{
					cDouble14 += 0.5;
				}
				if (this.GetTotalStats() > 10000000)
				{
					cDouble14 += 0.5;
				}
				if (this.GetTotalStats() > 100000000)
				{
					cDouble14 += 0.5;
				}
				cDouble13 = cDouble13 * durationHours * cDouble5 / 200;
				cDouble14 = cDouble14 * durationHours * cDouble5 / 500;
				cDouble14 = cDouble14 * cDouble7 / 100;
				cDouble13 = cDouble13 * cDouble7 / 100;
				this.DescMultiplierCampaign = string.Concat(new string[]
				{
					this.Name,
					" can add ",
					cDouble13.GuiText,
					"% to the pet rebirth multiplier and multiply your current pet multiplier by ",
					cDouble14.GuiText,
					"%"
				});
				return cDouble13;
			}
			case Campaigns.GodPower:
			{
				CDouble cDouble15 = 1;
				CDouble totalStats = this.GetTotalStats();
				if (totalStats < 10000)
				{
					cDouble15 += totalStats / 20000;
				}
				else if (this.GetTotalStats() < 100000)
				{
					cDouble15 += 0.5 + totalStats / 200000;
				}
				else if (this.GetTotalStats() < 1000000)
				{
					cDouble15 += 1 + totalStats / 2000000;
				}
				else if (this.GetTotalStats() < 5000000)
				{
					cDouble15 += 1.5 + totalStats / 10000000;
				}
				else if (this.GetTotalStats() < 10000000)
				{
					cDouble15 += 2 + totalStats / 20000000;
				}
				else if (this.GetTotalStats() < 100000000)
				{
					cDouble15 += 2.5 + totalStats / 200000000;
				}
				else
				{
					cDouble15 += 3;
				}
				cDouble15 = (3 * cDouble15 * durationHours * cDouble6 / 100).ToInt();
				cDouble15 = cDouble15 * cDouble7 / 100;
				if (cDouble15 > 100)
				{
					cDouble15 = 100;
				}
				this.DescGodPowerCampaign = this.Name + " has a chance of " + cDouble15.GuiText + " % to find one god power.";
				return cDouble15;
			}
			default:
				return 0;
			}
		}

		public void SetCampaignInfo()
		{
			string text = string.Empty;
			string text2 = "It is ";
			this.campaignBoosts = new List<CampaignBoost>();
			switch (this.TypeEnum)
			{
			case PetType.Mouse:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Food,
					Value = -50
				});
				break;
			case PetType.Rabbit:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Level,
					Value = 35
				});
				break;
			case PetType.Cat:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.GodPower,
					Value = 50
				});
				break;
			case PetType.Dog:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Item,
					Value = 50
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Multiplier,
					Value = -50
				});
				break;
			case PetType.Fairy:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Item,
					Value = 50
				});
				break;
			case PetType.Dragon:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Multiplier,
					Value = 100
				});
				break;
			case PetType.Doughnut:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Food,
					Value = 75
				});
				break;
			case PetType.Eagle:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Item,
					Value = 75
				});
				break;
			case PetType.Phoenix:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Level,
					Value = 50
				});
				break;
			case PetType.Squirrel:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Food,
					Value = 75
				});
				break;
			case PetType.Turtle:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Divinity,
					Value = 0
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Food,
					Value = 0
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.GodPower,
					Value = 0
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Growth,
					Value = 0
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Item,
					Value = 0
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Level,
					Value = 0
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Multiplier,
					Value = 0
				});
				break;
			case PetType.Penguin:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.GodPower,
					Value = 75
				});
				break;
			case PetType.Cupid:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Divinity,
					Value = 100
				});
				break;
			case PetType.Camel:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.GodPower,
					Value = 60
				});
				break;
			case PetType.Goat:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Food,
					Value = -100
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Item,
					Value = 100
				});
				break;
			case PetType.Mole:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Food,
					Value = -50
				});
				break;
			case PetType.Octopus:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Food,
					Value = 100
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.GodPower,
					Value = -80
				});
				break;
			case PetType.Pegasus:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Level,
					Value = 40
				});
				break;
			case PetType.Robot:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Food,
					Value = 100
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Growth,
					Value = 100
				});
				break;
			case PetType.Shark:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Growth,
					Value = 100
				});
				break;
			case PetType.Slime:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Divinity,
					Value = 25
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Food,
					Value = 25
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.GodPower,
					Value = 25
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Growth,
					Value = 25
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Item,
					Value = 25
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Level,
					Value = 20
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Multiplier,
					Value = 25
				});
				break;
			case PetType.Snake:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Divinity,
					Value = 50
				});
				break;
			case PetType.Ufo:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Multiplier,
					Value = 75
				});
				break;
			case PetType.Wizard:
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Divinity,
					Value = -50
				});
				this.campaignBoosts.Add(new CampaignBoost
				{
					Type = Campaigns.Multiplier,
					Value = 75
				});
				break;
			}
			if (string.IsNullOrEmpty(text))
			{
				text = this.Name + string.Empty;
			}
			foreach (CampaignBoost current in this.campaignBoosts)
			{
				string str = string.Empty;
				string text3 = " increases ";
				string text4 = current.Value + "% in ";
				if (current.Value < 0)
				{
					text3 = " decreases ";
					text4 = current.Value * -1 + "% in ";
				}
				if (current.Value > 0)
				{
					text2 = text2 + "good in " + current.Type.ToString().ToLower() + " campaigns, ";
				}
				else if (current.Value < 0)
				{
					text2 = text2 + "bad in " + current.Type.ToString().ToLower() + " campaigns, ";
				}
				if (current.Type == Campaigns.GodPower)
				{
					str = text3 + "the chance to find god power by " + text4;
				}
				else if (current.Type == Campaigns.Growth || current.Type == Campaigns.Multiplier || current.Type == Campaigns.Divinity)
				{
					str = string.Concat(new string[]
					{
						text3,
						"the ",
						current.Type.ToString().ToLower(),
						" gain by ",
						text4
					});
				}
				else if (current.Type == Campaigns.Item)
				{
					str = text3 + "the chance to find rare items and the number of pet stones by " + text4;
				}
				else if (current.Type == Campaigns.Food)
				{
					str = text3 + "the chance to find food and better quality food by " + text4;
				}
				else if (current.Type == Campaigns.Level)
				{
					str = text3 + "the level gained by all pets by " + text4;
				}
				if (current.Value > 0)
				{
					text = text + str + current.Type.ToString().ToLower() + " campaigns,";
				}
				else if (current.Value < 0)
				{
					text = text + str + current.Type.ToString().ToLower() + " campaigns,";
				}
			}
			if (this.TypeEnum == PetType.Turtle)
			{
				text2 = "It is bad in 1 hour campaigns, good in 12 hour campaigns.";
				text = "Turtle starts out slow and is bad in 1 hour campaigns (decreases all gains by 25%), but good in 12 hour campaigns (increases all gains by 25%).";
			}
			else if (this.TypeEnum == PetType.Slime)
			{
				text2 = "It is good in all campaigns.";
				text = "Slime increases all gains by 25% (20% for level campaigns).";
			}
			this.campaignDesc = string.Concat(new string[]
			{
				Conv.ReplaceLastOccurrence(text, ",", "."),
				"lowerTextTotal Growth: ",
				this.GetTotalGrowth().GuiText,
				"\nTotal Stats: ",
				this.GetTotalStats().GuiText
			});
			this.campaignDescShort = Conv.ReplaceLastOccurrence(text2, ",", ".");
		}

		public static bool CheckGoatUnlock()
		{
			bool flag = false;
			foreach (Fight current in App.State.AllFights)
			{
				if (current.Level > 0)
				{
					flag = true;
					break;
				}
			}
			foreach (UltimateBeing current2 in App.State.HomePlanet.UltimateBeings)
			{
				if (current2.TimesDefeated > 0)
				{
					flag = true;
					break;
				}
			}
			foreach (UltimateBeingV2 current3 in App.State.HomePlanet.UltimateBeingsV2)
			{
				if (current3.IsDefeated)
				{
					flag = true;
					break;
				}
			}
			bool flag2 = true;
			foreach (Creation current4 in App.State.AllCreations)
			{
				if (current4.Count <= 0)
				{
					flag2 = false;
					break;
				}
			}
			Monument monument = App.State.AllMonuments.First((Monument x) => x.TypeEnum == Monument.MonumentType.mystic_garden);
			return monument.Level >= 5000 && flag2 && !flag;
		}

		public void Unlock()
		{
			if (this.IsUnlocked)
			{
				return;
			}
			CDouble cDouble = 999;
			bool flag = false;
			if (this.TypeEnum == PetType.Goat || this.TypeEnum == PetType.Mole || this.TypeEnum == PetType.Camel)
			{
				flag = true;
			}
			else if (this.TypeEnum == PetType.Mouse || this.TypeEnum == PetType.Turtle || this.TypeEnum == PetType.Cupid)
			{
				cDouble = 998;
			}
			else if (this.TypeEnum == PetType.Squirrel)
			{
				cDouble = 0;
			}
			else if (this.TypeEnum == PetType.Rabbit)
			{
				cDouble = 5;
			}
			else if (this.TypeEnum == PetType.Cat)
			{
				cDouble = 10;
			}
			else if (this.TypeEnum == PetType.Dog)
			{
				cDouble = 15;
			}
			else if (this.TypeEnum == PetType.Fairy)
			{
				cDouble = 20;
			}
			else if (this.TypeEnum == PetType.Dragon)
			{
				cDouble = 25;
			}
			else if (this.TypeEnum == PetType.Snake)
			{
				cDouble = 30;
			}
			else if (this.TypeEnum == PetType.Shark)
			{
				cDouble = 35;
			}
			else if (this.TypeEnum == PetType.Octopus)
			{
				cDouble = 40;
			}
			else if (this.TypeEnum == PetType.Slime)
			{
				cDouble = 50;
			}
			CDouble leftSide = App.State.Statistic.HighestGodDefeated - 28;
			string text = string.Empty;
			if (flag)
			{
				if (this.TypeEnum == PetType.Mole)
				{
					if (App.State.AllMights.First((Might x) => x.TypeEnum == Might.MightType.physical_attack).Level >= 100 && BattleState.JackyLeeDefeated)
					{
						this.IsUnlocked = true;
					}
				}
				else if (this.TypeEnum == PetType.Camel)
				{
					bool flag2 = false;
					foreach (Creation current in App.State.AllCreations)
					{
						if (current.GodToDefeat.IsDefeated && current.TypeEnum != Creation.CreationType.Shadow_clone)
						{
							flag2 = true;
							break;
						}
					}
					bool flag3 = true;
					foreach (Achievement current2 in App.State.TrainingAchievements)
					{
						if (!current2.Reached)
						{
							flag3 = false;
						}
					}
					foreach (Achievement current3 in App.State.SkillAchievements)
					{
						if (!current3.Reached)
						{
							flag3 = false;
						}
					}
					foreach (Achievement current4 in App.State.FightingAchievements)
					{
						if (!current4.Reached)
						{
							flag3 = false;
						}
					}
					if (flag3 && !flag2)
					{
						this.IsUnlocked = true;
					}
				}
				else if (this.TypeEnum == PetType.Goat)
				{
					this.IsUnlocked = Pet.CheckGoatUnlock();
					if (this.IsUnlocked)
					{
						foreach (Creation current5 in App.State.AllCreations)
						{
							if (current5.TypeEnum != Creation.CreationType.Shadow_clone)
							{
								current5.Count -= 1;
							}
						}
					}
				}
				text = "Find out the secret way to unlock " + this.Name + "!";
			}
			else if (this.TypeEnum == PetType.Turtle)
			{
				if (App.State.Statistic.ArtyChallengesFinished > 0)
				{
					this.IsUnlocked = true;
				}
				else
				{
					text = "You need to beat the Ultimate Arty Challenge to unlock " + this.Name + "!";
				}
			}
			else if (this.TypeEnum == PetType.Mouse)
			{
				Creation creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.GodToDefeat.TypeEnum == God.GodType.Hyperion);
				if (creation.GodToDefeat.IsDefeated)
				{
					this.IsUnlocked = true;
				}
				else
				{
					text = "You need to defeat Hyperion to unlock " + this.Name + "!";
				}
			}
			else if (this.TypeEnum == PetType.Cupid)
			{
				Creation creation2 = App.State.AllCreations.FirstOrDefault((Creation x) => x.GodToDefeat.TypeEnum == God.GodType.Eros);
				if (creation2.GodToDefeat.IsDefeated)
				{
					this.IsUnlocked = true;
				}
				else
				{
					text = "You need to defeat Eros to unlock " + this.Name + "!";
				}
			}
			else if (cDouble == 0)
			{
				Creation creation3 = App.State.AllCreations.FirstOrDefault((Creation x) => x.GodToDefeat.TypeEnum == God.GodType.Tyrant_Overlord_Baal);
				if (creation3.GodToDefeat.IsDefeated)
				{
					this.IsUnlocked = true;
				}
				else
				{
					text = "You need to defeat Tyrant Overlord Baal to unlock " + this.Name + "!";
				}
			}
			else if (leftSide >= cDouble)
			{
				this.IsUnlocked = true;
			}
			else if (cDouble == 999)
			{
				text = "You need 1 pet Token to unlock " + this.Name + "!";
			}
			else
			{
				text = string.Concat(new string[]
				{
					"You need to defeat P.Baal v ",
					cDouble.ToGuiText(true),
					" to unlock ",
					this.Name,
					"!"
				});
			}
			if (App.State.PremiumBoni.PetToken == 0)
			{
				if (cDouble != 999)
				{
					text += "\nAlternatively you can unlock it with 1 Pet Token.";
				}
				GuiBase.ShowToast(text);
			}
			else if (!this.IsUnlocked)
			{
				string text2 = "Do you want to spend 1 Pet Token to unlock " + this.Name + " instantly?";
				if (flag)
				{
					text2 = "Read the description for hints. \nYou can also unlock it with 1 Pet Token.\n" + text2;
				}
				GuiBase.ShowDialog(text, text2, delegate
				{
					Premium expr_0A = App.State.PremiumBoni;
					expr_0A.PetToken = --expr_0A.PetToken;
					this.IsUnlocked = true;
					this.FinishUnlocking();
				}, delegate
				{
				}, "Yes", "No", false, false);
			}
			this.FinishUnlocking();
		}

		private void FinishUnlocking()
		{
			if (this.IsUnlocked)
			{
				Pet shownPet = HeroImage.ShownPet;
				List<Pet> list = new List<Pet>();
				foreach (Pet current in App.State.Ext.AllPets)
				{
					if (current.IsUnlocked)
					{
						list.Add(current);
					}
				}
				list = (from o in list
				orderby o.SortValue
				select o).ToList<Pet>();
				if (list.Count >= App.State.Ext.PetIdInAvatar && list[App.State.Ext.PetIdInAvatar - 1].TypeEnum != shownPet.TypeEnum)
				{
					App.State.Ext.PetIdInAvatar++;
				}
				this.FeedTimer = 43200000L;
				this.Level = 1;
				GuiBase.ShowToast(this.Name + " has become your loyal pet!");
				MainUi.Instance.Init(false);
			}
		}

		public double getPercentOfHP()
		{
			if (this.MaxHealth == 0)
			{
				return 100.0;
			}
			CDouble cDouble = this.CurrentHealth / this.MaxHealth;
			return cDouble.Double;
		}

		public double getPercentOfHPClone()
		{
			if (this.CloneMaxHealth == 0)
			{
				return 100.0;
			}
			CDouble cDouble = this.CloneCurrentHealth / this.CloneMaxHealth;
			return cDouble.Double;
		}

		public void RecoverHealth(long millisecs)
		{
			CDouble cDouble = this.CloneMystic / 20000 * millisecs + 1;
			this.HpRecoverSecClone = cDouble * 1000 / millisecs;
			this.CloneCurrentHealth += cDouble;
			if (this.CloneCurrentHealth > this.CloneMaxHealth)
			{
				this.CloneCurrentHealth = this.CloneMaxHealth;
			}
			if (this.ZeroHealthTime > 0L)
			{
				this.ZeroHealthTime -= millisecs;
				if (this.ZeroHealthTime <= 0L)
				{
					this.CurrentHealth = this.MaxHealth / 10;
				}
				return;
			}
			this.HpRecover = this.Mystic / 20000 * millisecs + 1;
			this.HpRecoverSec = this.HpRecover * 1000 / millisecs;
			this.CurrentHealth += this.HpRecover;
			if (this.CurrentHealth > this.MaxHealth)
			{
				this.CurrentHealth = this.MaxHealth;
			}
		}

		public void Fight(long ms)
		{
			if (this.ShadowCloneCount < 1 || this.CloneBattle == 0 || this.ClonePhysical == 0 || this.CloneMystic == 0 || this.CurrentHealth <= 0)
			{
				return;
			}
			CDouble cDouble = (this.CloneBattle - this.Mystic / 2) * ms / 1000;
			if (cDouble < 0)
			{
				cDouble = 0;
			}
			this.CurrentHealth -= cDouble;
			if (this.CurrentHealth <= 0)
			{
				this.CurrentHealth = 0;
				this.ZeroHealthTime = 60000L;
				return;
			}
			CDouble cDouble2 = (this.Battle - this.CloneMystic / 2) * ms / 1000;
			if (cDouble2 < 0)
			{
				cDouble2 = 0;
			}
			this.CloneCurrentHealth -= cDouble2;
			if (this.CloneCurrentHealth <= 0)
			{
				this.CloneCurrentHealth = this.CloneMaxHealth;
				this.Exp += this.CloneExp;
				this.ShadowCloneCount = --this.ShadowCloneCount;
				Statistic expr_1B4 = App.State.Statistic;
				expr_1B4.TotalShadowClonesDied = ++expr_1B4.TotalShadowClonesDied;
				if (this.ShadowCloneCount < 1)
				{
					this.ShadowCloneCount = 0;
				}
			}
			if (this.Exp >= this.ExpNeeded)
			{
				this.Level = ++this.Level;
				this.Exp = 0;
				this.CalculateValues();
			}
		}

		public static CDouble CalcCloneExp(CDouble physical, CDouble mystic, CDouble battle)
		{
			return Math.Pow(Math.Pow(physical.Double, 2.3) * mystic.Double * battle.Double, 0.38461538461538458);
		}

		public void CalculateValues()
		{
			this.CloneMaxHealth = this.ClonePhysical * 10;
			this.CloneExp = Pet.CalcCloneExp(this.ClonePhysical, this.CloneMystic, this.CloneBattle);
			this.ExpNeeded = 2 * this.Level * this.Level * this.Level;
			this.Physical = this.PhysicalGrowth * this.LevelMultiplier;
			this.Mystic = this.MysticGrowth * this.LevelMultiplier;
			this.Battle = this.BattleGrowth * this.LevelMultiplier;
			this.MaxHealth = this.Physical * 10;
			this.GetShortCampaignDescription();
			if (this.IsUnlocked)
			{
				this.Description = string.Concat(new string[]
				{
					this.Desc,
					"\n",
					this.campaignDescShort,
					"lowerTextHunger %: ",
					(this.FullnessPercent * 100).ToGuiText(false),
					"\nClones left: ",
					this.ShadowCloneCount.GuiText,
					"\nPhysical Growth: ",
					this.PhysicalGrowth.ToGuiText(false),
					"\nMystic Growth: ",
					this.MysticGrowth.ToGuiText(false),
					"\nBattle Growth: ",
					this.BattleGrowth.ToGuiText(false)
				});
			}
			else
			{
				this.Description = string.Concat(new object[]
				{
					this.Desc,
					"\n",
					this.campaignDescShort,
					"lowerTextPhysical Growth: ",
					this.PhysicalGrowth,
					"\nMystic Growth: ",
					this.MysticGrowth,
					"\nBattle Growth: ",
					this.BattleGrowth
				});
			}
		}

		public string GetFoodDescription(FoodType food, SauceType sauce)
		{
			return string.Concat(new string[]
			{
				"Increases the growth of Physical by ",
				this.FeedingPhysicalGain(food, sauce).ToGuiText(false),
				", the growth of Mystic by ",
				this.FeedingMysticGain(food, sauce).ToGuiText(false),
				" and the growth of Battle by ",
				this.FeedingBattleGain(food, sauce).ToGuiText(false)
			});
		}

		private CDouble BaseFoodValue(FoodType food)
		{
			CDouble cDouble = 0;
			if (food == FoodType.None)
			{
				cDouble = 1;
			}
			else if (food == FoodType.Puny)
			{
				cDouble = 2;
			}
			else if (food == FoodType.Strong)
			{
				cDouble = 3.5;
			}
			else if (food == FoodType.Mighty)
			{
				cDouble = 5;
			}
			else if (food == FoodType.Chocolate)
			{
				cDouble = 7;
			}
			if (this.FeedTimer > 4320000L)
			{
				cDouble /= 2;
				if (food == FoodType.Strong)
				{
					cDouble = 2;
				}
				else if (food == FoodType.Mighty)
				{
					cDouble = 3;
				}
				else if (food == FoodType.Mighty)
				{
					cDouble = 4;
				}
			}
			if (this.FeedTimer > 21600000L)
			{
				cDouble /= 2;
			}
			return cDouble;
		}

		private Pet.HighestGrowth CheckHighest()
		{
			if (this.PhysicalGrowth > this.MysticGrowth && this.PhysicalGrowth > this.BattleGrowth)
			{
				return Pet.HighestGrowth.physical;
			}
			if (this.MysticGrowth > this.PhysicalGrowth && this.MysticGrowth > this.BattleGrowth)
			{
				return Pet.HighestGrowth.mystic;
			}
			if (this.BattleGrowth > this.PhysicalGrowth && this.BattleGrowth > this.MysticGrowth)
			{
				return Pet.HighestGrowth.battle;
			}
			if (this.BattleGrowth > this.PhysicalGrowth && this.BattleGrowth == this.MysticGrowth)
			{
				return Pet.HighestGrowth.notPhysical;
			}
			if (this.PhysicalGrowth > this.MysticGrowth && this.PhysicalGrowth == this.BattleGrowth)
			{
				return Pet.HighestGrowth.notMystic;
			}
			if (this.PhysicalGrowth == this.MysticGrowth && this.PhysicalGrowth > this.BattleGrowth)
			{
				return Pet.HighestGrowth.notBattle;
			}
			return Pet.HighestGrowth.none;
		}

		public CDouble HighestDifference()
		{
			CDouble cDouble = this.PhysicalGrowth - this.MysticGrowth;
			if (this.PhysicalGrowth - this.BattleGrowth > cDouble)
			{
				cDouble = this.PhysicalGrowth - this.BattleGrowth;
			}
			if (this.MysticGrowth - this.PhysicalGrowth > cDouble)
			{
				cDouble = this.MysticGrowth - this.PhysicalGrowth;
			}
			if (this.MysticGrowth - this.BattleGrowth > cDouble)
			{
				cDouble = this.MysticGrowth - this.BattleGrowth;
			}
			if (this.BattleGrowth - this.PhysicalGrowth > cDouble)
			{
				cDouble = this.BattleGrowth - this.PhysicalGrowth;
			}
			if (this.BattleGrowth - this.MysticGrowth > cDouble)
			{
				cDouble = this.BattleGrowth - this.MysticGrowth;
			}
			return cDouble;
		}

		public CDouble FeedingPhysicalGain(FoodType food, SauceType sauce)
		{
			CDouble cDouble = this.BaseFoodValue(food);
			if (sauce == SauceType.Hot)
			{
				cDouble *= 2;
			}
			else if (sauce == SauceType.Sweet)
			{
				cDouble /= 2;
			}
			else if (sauce == SauceType.Sour)
			{
				cDouble /= 2;
			}
			else if (sauce == SauceType.Mayonaise)
			{
				Pet.HighestGrowth highestGrowth = this.CheckHighest();
				if (highestGrowth == Pet.HighestGrowth.none)
				{
					return cDouble;
				}
				CDouble leftSide = this.HighestDifference();
				if (leftSide < cDouble / 2)
				{
					CDouble cDouble2 = cDouble / 2;
					if (highestGrowth == Pet.HighestGrowth.battle || highestGrowth == Pet.HighestGrowth.notPhysical)
					{
						return this.BattleGrowth - this.PhysicalGrowth + cDouble2;
					}
					if (highestGrowth == Pet.HighestGrowth.mystic)
					{
						return this.MysticGrowth - this.PhysicalGrowth + cDouble2;
					}
					return cDouble2;
				}
				else
				{
					CDouble rightSide = this.PhysicalGrowth + this.BattleGrowth + this.MysticGrowth;
					CDouble rightSide2 = this.PhysicalGrowth / rightSide;
					cDouble = Conv.RoundToOneFourth((cDouble * (1 - rightSide2) * 1.5).Double);
					if (cDouble > this.BaseFoodValue(food) * 2.5)
					{
						cDouble = this.BaseFoodValue(food) * 2.5;
					}
					if (highestGrowth == Pet.HighestGrowth.physical)
					{
						return cDouble - 0.5;
					}
					if (highestGrowth == Pet.HighestGrowth.notMystic || highestGrowth == Pet.HighestGrowth.notBattle)
					{
						return cDouble - 0.25;
					}
					if (highestGrowth == Pet.HighestGrowth.notPhysical)
					{
						return cDouble + 0.5;
					}
					if (highestGrowth == Pet.HighestGrowth.mystic || highestGrowth == Pet.HighestGrowth.battle)
					{
						return cDouble + 0.25;
					}
					return cDouble;
				}
			}
			return cDouble;
		}

		public CDouble FeedingMysticGain(FoodType food, SauceType sauce)
		{
			CDouble cDouble = this.BaseFoodValue(food);
			if (sauce == SauceType.Hot)
			{
				cDouble /= 2;
			}
			else if (sauce == SauceType.Sweet)
			{
				cDouble *= 2;
			}
			else if (sauce == SauceType.Sour)
			{
				cDouble /= 2;
			}
			else if (sauce == SauceType.Mayonaise)
			{
				Pet.HighestGrowth highestGrowth = this.CheckHighest();
				if (highestGrowth == Pet.HighestGrowth.none)
				{
					return cDouble;
				}
				CDouble leftSide = this.HighestDifference();
				if (leftSide < cDouble / 2)
				{
					CDouble cDouble2 = cDouble / 2;
					if (highestGrowth == Pet.HighestGrowth.physical || highestGrowth == Pet.HighestGrowth.notMystic)
					{
						return this.PhysicalGrowth - this.MysticGrowth + cDouble2;
					}
					if (highestGrowth == Pet.HighestGrowth.battle)
					{
						return this.BattleGrowth - this.MysticGrowth + cDouble2;
					}
					return cDouble2;
				}
				else
				{
					CDouble rightSide = this.PhysicalGrowth + this.BattleGrowth + this.MysticGrowth;
					CDouble rightSide2 = this.MysticGrowth / rightSide;
					cDouble = Conv.RoundToOneFourth((cDouble * (1 - rightSide2) * 1.5).Double);
					if (cDouble > this.BaseFoodValue(food) * 2.5)
					{
						cDouble = this.BaseFoodValue(food) * 2.5;
					}
					if (highestGrowth == Pet.HighestGrowth.mystic)
					{
						return cDouble - 0.5;
					}
					if (highestGrowth == Pet.HighestGrowth.notPhysical || highestGrowth == Pet.HighestGrowth.notBattle)
					{
						return cDouble - 0.25;
					}
					if (highestGrowth == Pet.HighestGrowth.notMystic)
					{
						return cDouble + 0.5;
					}
					if (highestGrowth == Pet.HighestGrowth.physical || highestGrowth == Pet.HighestGrowth.battle)
					{
						return cDouble + 0.25;
					}
					return cDouble;
				}
			}
			return cDouble;
		}

		public CDouble FeedingBattleGain(FoodType food, SauceType sauce)
		{
			CDouble cDouble = this.BaseFoodValue(food);
			if (sauce == SauceType.Hot)
			{
				cDouble /= 2;
			}
			else if (sauce == SauceType.Sweet)
			{
				cDouble /= 2;
			}
			else if (sauce == SauceType.Sour)
			{
				cDouble *= 2;
			}
			else if (sauce == SauceType.Mayonaise)
			{
				Pet.HighestGrowth highestGrowth = this.CheckHighest();
				if (highestGrowth == Pet.HighestGrowth.none)
				{
					return cDouble;
				}
				CDouble leftSide = this.HighestDifference();
				if (leftSide < cDouble * 3 / 2)
				{
					CDouble cDouble2 = cDouble / 2;
					if (highestGrowth == Pet.HighestGrowth.physical || highestGrowth == Pet.HighestGrowth.notBattle)
					{
						return this.PhysicalGrowth - this.BattleGrowth + cDouble2;
					}
					if (highestGrowth == Pet.HighestGrowth.mystic)
					{
						return this.MysticGrowth - this.BattleGrowth + cDouble2;
					}
					return cDouble2;
				}
				else
				{
					CDouble rightSide = this.PhysicalGrowth + this.BattleGrowth + this.MysticGrowth;
					CDouble rightSide2 = this.BattleGrowth / rightSide;
					cDouble = Conv.RoundToOneFourth((cDouble * (1 - rightSide2) * 1.5).Double);
					if (cDouble > this.BaseFoodValue(food) * 2.5)
					{
						cDouble = this.BaseFoodValue(food) * 2.5;
					}
					if (highestGrowth == Pet.HighestGrowth.battle)
					{
						return cDouble - 0.5;
					}
					if (highestGrowth == Pet.HighestGrowth.notPhysical || highestGrowth == Pet.HighestGrowth.notMystic)
					{
						return cDouble - 0.25;
					}
					if (highestGrowth == Pet.HighestGrowth.notBattle)
					{
						return cDouble + 0.5;
					}
					if (highestGrowth == Pet.HighestGrowth.physical || highestGrowth == Pet.HighestGrowth.mystic)
					{
						return cDouble + 0.25;
					}
					return cDouble;
				}
			}
			return cDouble;
		}

		public bool Feed(FoodType food, SauceType sauce, bool showToast = true)
		{
			if (this.CanFeed)
			{
				CDouble cDouble = this.FeedingPhysicalGain(food, sauce);
				CDouble cDouble2 = this.FeedingMysticGain(food, sauce);
				CDouble cDouble3 = this.FeedingBattleGain(food, sauce);
				if (food == FoodType.Puny)
				{
					if (App.State.Ext.PunyFood < 1)
					{
						return false;
					}
					State2 expr_58 = App.State.Ext;
					expr_58.PunyFood = --expr_58.PunyFood;
				}
				else if (food == FoodType.Strong)
				{
					if (App.State.Ext.StrongFood < 1)
					{
						return false;
					}
					State2 expr_9F = App.State.Ext;
					expr_9F.StrongFood = --expr_9F.StrongFood;
				}
				else if (food == FoodType.Mighty)
				{
					if (App.State.Ext.MightyFood < 1)
					{
						return false;
					}
					State2 expr_E6 = App.State.Ext;
					expr_E6.MightyFood = --expr_E6.MightyFood;
				}
				else if (food == FoodType.Chocolate)
				{
					if (App.State.Ext.Chocolate < 1)
					{
						return false;
					}
					State2 expr_12D = App.State.Ext;
					expr_12D.Chocolate = --expr_12D.Chocolate;
				}
				this.PhysicalGrowth += cDouble;
				this.MysticGrowth += cDouble2;
				this.BattleGrowth += cDouble3;
				this.FeedTimer = 43200000L;
				this.CalculateValues();
				App.State.Statistic.CalculateTotalPetGrowth(App.State.Ext.AllPets);
				Leaderboards.SubmitStat(LeaderBoardType.TotalPetGrowth, App.State.Statistic.TotalPetGrowth.ToInt(), false);
				if (showToast)
				{
					GuiBase.ShowToast(string.Concat(new string[]
					{
						"Physical growth increased by ",
						cDouble.ToGuiText(false),
						"!\nMystic growth increased by ",
						cDouble2.ToGuiText(false),
						"!\nBattle growth increased by ",
						cDouble3.ToGuiText(false),
						"!"
					}));
				}
				return true;
			}
			return false;
		}

		public void AddCloneCount(int count)
		{
			int num = App.State.Clones.IdleClones();
			if (count > num)
			{
				count = num;
			}
			App.State.Clones.UseShadowClones(count);
			this.ShadowCloneCount += count;
		}

		public void RemoveCloneCount(int count)
		{
			if (this.ShadowCloneCount <= count)
			{
				count = this.ShadowCloneCount.ToInt();
			}
			this.ShadowCloneCount -= count;
			App.State.Clones.RemoveUsedShadowClones(count);
		}

		public void CreateClones(CDouble count, CDouble physical, CDouble mystic, CDouble battle)
		{
			if (count > 0)
			{
				this.RemoveCloneCount(this.ShadowCloneCount.ToInt());
				this.AddCloneCount(count.ToInt());
			}
			this.ClonePhysical = physical;
			this.CloneMystic = mystic;
			this.CloneBattle = battle;
			this.CalculateValues();
			this.CloneCurrentHealth = this.CloneMaxHealth;
		}

		public void UpdateDuration(long ms)
		{
			if (this.IsUnlocked)
			{
				this.FeedTimer -= ms;
				if (this.FeedTimer <= 0)
				{
					this.CurrentHealth = 0;
				}
				this.Fight(ms);
			}
		}

		public void UpdateFeedTimerMultis()
		{
			if (this.IsUnlocked)
			{
				if (this.FeedTimer > 0)
				{
					this.FullnessPercent = this.FeedTimer / 43200000L;
					this.RecoverHealth(500L);
					this.CalculateValues();
				}
				else
				{
					this.FullnessPercent = 0;
				}
			}
		}
	}
}
