using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Data
{
	public class Premium
	{
		public int TotalItemsBought;

		public long GodlyLiquidDuration;

		public CDouble GodlyLiquidCount = 0;

		public CDouble GodlyLiquidV2Count = 0;

		public bool GodlyLiquidV2InUse;

		public long ChakraPillDuration;

		public CDouble ChakraPillCount = 0;

		public CDouble ChakraPillV2Count = 0;

		public bool ChakraPillV2InUse;

		public bool ImprovedNextAt;

		public bool BoughtLimitedPetToken;

		public bool ChallengeImprovedNextAt;

		public bool PetFoodAfterRebirth;

		public bool CanShowAlerts;

		public bool HasUnlimitedGenderChange;

		public bool HasPetHalfStats;

		public bool HasAutoSelectPets;

		public CDouble DrawsUsedSinceFreeDraw = 0;

		public int UltimateShadowSummonCount;

		public string ItemToPurchase = string.Empty;

		public CDouble creatingSpeedUpPercent = 0;

		public CDouble buildingSpeedUpPercent = 0;

		public CDouble creationCountBoni = 0;

		private int tbsExtraPixels;

		private int tbsDoublePoints;

		private CDouble statisticMulti = 0;

		public int GodPowerMod;

		private int godPower;

		public int TotalGodPowerSpent;

		public CDouble PetToken = 0;

		public CDouble LuckyDraws = 0;

		public CDouble TimeForNextLuckyDraw = 0;

		public int TotalLuckyDrawCount;

		public CDouble GPFromLuckyDraws = 0;

		public ulong OrderIdLastSteamPurchase;

		public Purchase ItemIdToPurchase;

		public CDouble GodPowerBought = 0;

		public string SteamPurchasedOrderIds = string.Empty;

		public string AndroidPurchasedOrderIds = string.Empty;

		public long TimeForNextDailyPack;

		public CDouble DailyPackDaysLeft = 0;

		public CDouble GodPowerFromCrystals = 0;

		public bool HasCrystalImprovement;

		public CDouble MaxCrystals = 2;

		public CDouble CrystalPower = 0;

		public CDouble GodPowerFromPets = 0;

		private long totalMight;

		public bool TotalMightIsUnlocked;

		public CDouble GpBoniPhysical = 0;

		public CDouble GpBoniMystic = 0;

		public CDouble GpBoniBattle = 0;

		public CDouble GpBoniCreating = 0;

		private int autoBuyCostReduction;

		private int tbsMissreduction;

		private int tbsProgressAfterRebirth;

		public bool CheckGP;

		public bool HasPurchasedGodPowerOnce;

		public bool HasObtainedFreeGodPower;

		public CDouble CrystalBonusPhysical = 0;

		public CDouble CrystalBonusMystic = 0;

		public CDouble CrystalBonusBattle = 0;

		public CDouble CrystalBonusCreation = 0;

		public CDouble CrystalBonusBuilding = 0;

		public CDouble CrystalBonusDefender = 0;

		public CDouble CrystalBonusDivinity = 0;

		public CDouble CrystalBonusCC = 0;

		public CDouble CrystalGPTimeTotal = 0;

		public CDouble CrystalGPTimeCurrent = 0;

		public const string ID_CHAKRA_PILL = "xzp";

		public const string ID_CHAKRA_PILL10 = "helloworld";

		public static string ID_CHAKRA_PILL_REBIRTH = "hellomyworld";

		public static string ID_CHAKRA_PILL_REBIRTH10 = "hellomyworldone";

		public const string ID_GODLY_LIQUID = "nightmare";

		public const string ID_GODLY_LIQUID10 = "myfirstid";

		public static string ID_GODLY_LIQUID_REBIRTH = "nightymare";

		public static string ID_GODLY_LIQUID_REBIRTH10 = "nightymareone";

		public static string ID_SHADOW_SUMMON = "blahlala";

		public static string ID_SHADOW_SUMMON10 = "confusingname";

		public static string ID_GOD_POWER5 = "intsanid";

		public static string ID_GOD_POWER25 = "littlebit";

		public const string ID_GOD_POWER50 = "jeahitsmine";

		public static string ID_GOD_POWER100 = "muaha";

		public static string ID_UGC = "ugc";

		public static string ID_PET_TOKEN = "pts";

		public static string ID_PET_TOKEN3 = "ptstr";

		public static string ID_PET_TOKEN_LIMITED = "ptsli";

		public static string ID_LUCKY_DRAW = "lucky1";

		public static string ID_LUCKY_DRAW10 = "lucky2";

		public static string ID_LUCKY_DRAW50 = "lucky3";

		public static string ID_DAILY_PACK = "mydp";

		public static string ID_DAILY_PACK_15 = "mydp15";

		public static string ID_CHALLENGE_NEXT_AT = "challenext";

		public static string ID_FOOD_AFTER_REBIRTH = "foodyreb";

		public static string ID_CRYSTAL_CHANCE = "oldone";

		public static string ID_CRYSTAL_SLOT = "itsmine";

		public int permanentGPSpent;

		public int TbsExtraPixels
		{
			get
			{
				return this.tbsExtraPixels - this.GodPowerMod;
			}
			set
			{
				this.tbsExtraPixels = value + this.GodPowerMod;
			}
		}

		public int TbsDoublePoints
		{
			get
			{
				return this.tbsDoublePoints - this.GodPowerMod;
			}
			set
			{
				this.tbsDoublePoints = value + this.GodPowerMod;
			}
		}

		public CDouble StatisticMulti
		{
			get
			{
				return this.statisticMulti - this.GodPowerMod;
			}
			set
			{
				this.statisticMulti = value + this.GodPowerMod;
			}
		}

		public int GodPower
		{
			get
			{
				return this.godPower - this.GodPowerMod;
			}
			set
			{
				this.godPower = value + this.GodPowerMod;
			}
		}

		public long TotalMight
		{
			get
			{
				return this.totalMight - (long)this.GodPowerMod;
			}
			set
			{
				this.totalMight = value + (long)this.GodPowerMod;
			}
		}

		public int AutoBuyCostReduction
		{
			get
			{
				if (this.autoBuyCostReduction >= 20)
				{
					return 20;
				}
				return this.autoBuyCostReduction;
			}
			set
			{
				this.autoBuyCostReduction = value;
			}
		}

		public int TbsMissreduction
		{
			get
			{
				if (this.tbsMissreduction >= 100)
				{
					return 100;
				}
				return this.tbsMissreduction;
			}
			set
			{
				this.tbsMissreduction = value;
			}
		}

		public int TbsProgressAfterRebirth
		{
			get
			{
				if (this.tbsProgressAfterRebirth >= 100)
				{
					return 100;
				}
				return this.tbsProgressAfterRebirth;
			}
			set
			{
				this.tbsProgressAfterRebirth = value;
			}
		}

		public int MaxBoniPhysical
		{
			get
			{
				return (this.GodPower - this.GpBoniMystic - this.GpBoniBattle - this.GpBoniCreating).ToInt();
			}
		}

		public int MaxBoniMystic
		{
			get
			{
				return (this.GodPower - this.GpBoniPhysical - this.GpBoniBattle - this.GpBoniCreating).ToInt();
			}
		}

		public int MaxBoniBattle
		{
			get
			{
				return (this.GodPower - this.GpBoniMystic - this.GpBoniPhysical - this.GpBoniCreating).ToInt();
			}
		}

		public int MaxBoniCreating
		{
			get
			{
				return (this.GodPower - this.GpBoniMystic - this.GpBoniBattle - this.GpBoniPhysical).ToInt();
			}
		}

		public int CreationDopingDivider
		{
			get
			{
				int num = 1;
				if (this.GodlyLiquidDuration > 0L)
				{
					num *= 2;
				}
				if (this.GodlyLiquidV2InUse)
				{
					num *= 2;
				}
				return num;
			}
		}

		public int MonumentBuildTimeDivider
		{
			get
			{
				int num = 1;
				if (this.ChakraPillDuration > 0L)
				{
					num *= 2;
				}
				if (this.ChakraPillV2InUse)
				{
					num *= 2;
				}
				return num;
			}
		}

		public int PermanentGPSpent
		{
			get
			{
				if (this.permanentGPSpent == 0 && App.State != null)
				{
					return this.CalculateGPSpent(App.State);
				}
				return this.permanentGPSpent;
			}
		}

		public CDouble CreatingSpeedUpPercent(bool withCrystalPower = true)
		{
			if (withCrystalPower)
			{
				return this.CrystalPower * 0.5 + this.creatingSpeedUpPercent;
			}
			return this.creatingSpeedUpPercent;
		}

		public void AddCreationSpeed(CDouble speed)
		{
			this.creatingSpeedUpPercent += speed;
		}

		public CDouble BuildingSpeedUpPercent(bool withCrystalPower = true)
		{
			if (withCrystalPower)
			{
				return (this.CrystalPower * 0.5 + this.buildingSpeedUpPercent) * (100 + this.CrystalBonusBuilding) / 100;
			}
			return this.buildingSpeedUpPercent;
		}

		public void AddBuildingSpeed(CDouble speed)
		{
			this.buildingSpeedUpPercent += speed;
		}

		public CDouble CreationCountBoni(bool withEquippedCrystal = true)
		{
			if (withEquippedCrystal)
			{
				return this.creationCountBoni + this.CrystalBonusCC;
			}
			return this.creationCountBoni;
		}

		public void AddCreationCountBoni(CDouble count)
		{
			this.creationCountBoni += count;
		}

		public void CheckGPBoniValues()
		{
			CDouble cDouble = this.GpBoniPhysical + this.GpBoniMystic + this.GpBoniBattle + this.GpBoniCreating;
			cDouble.Round();
			if (cDouble > this.GodPower || cDouble < 0)
			{
				this.GpBoniPhysical = 0;
				this.GpBoniMystic = 0;
				this.GpBoniBattle = 0;
				this.GpBoniCreating = 0;
			}
		}

		public void CheckIfGPIsAdjusted()
		{
			this.CheckGP = (this.GodPower - this.GpBoniPhysical - this.GpBoniMystic - this.GpBoniBattle - this.GpBoniCreating > 0);
		}

		public void CheckCrystalBonus(GameState state)
		{
			List<Crystal> equippedCrystals = state.Ext.Factory.EquippedCrystals;
			this.CrystalBonusPhysical = 0;
			this.CrystalBonusMystic = 0;
			this.CrystalBonusBattle = 0;
			this.CrystalBonusCreation = 0;
			this.CrystalBonusBuilding = 0;
			this.CrystalBonusDefender = 0;
			this.CrystalBonusDivinity = 0;
			this.CrystalBonusCC = 0;
			this.CrystalGPTimeTotal = 0;
			if (equippedCrystals.Count > 0)
			{
				foreach (Crystal current in equippedCrystals)
				{
					switch (current.Type)
					{
					case ModuleType.Physical:
						this.CrystalBonusPhysical += 100 * current.Level;
						this.CrystalBonusBuilding += 3 * current.Level;
						break;
					case ModuleType.Mystic:
						this.CrystalBonusMystic += 100 * current.Level;
						this.CrystalBonusDefender += 3 * current.Level;
						break;
					case ModuleType.Battle:
						this.CrystalBonusBattle += 100 * current.Level;
						this.CrystalBonusDivinity += 3 * current.Level;
						break;
					case ModuleType.Creation:
						this.CrystalBonusCreation += 100 * current.Level;
						this.CrystalBonusCC += current.Level;
						break;
					case ModuleType.Ultimate:
						this.CrystalBonusPhysical += 100 * current.Level;
						this.CrystalBonusMystic += 100 * current.Level;
						this.CrystalBonusBattle += 100 * current.Level;
						this.CrystalBonusCreation += 100 * current.Level;
						this.CrystalBonusBuilding += 3 * current.Level;
						this.CrystalBonusDefender += 3 * current.Level;
						this.CrystalBonusDivinity += 3 * current.Level;
						this.CrystalBonusCC += current.Level;
						break;
					case ModuleType.God:
						this.CrystalGPTimeTotal = 27000000 / current.Level;
						break;
					}
				}
				if (this.CrystalBonusDefender > 95)
				{
					this.CrystalBonusDefender = 95;
				}
			}
		}

		public void AddPremiumWhenStartChallenge(Premium oldPremium)
		{
			this.PetToken = oldPremium.PetToken;
			this.LuckyDraws = oldPremium.LuckyDraws;
			this.TimeForNextLuckyDraw = oldPremium.TimeForNextLuckyDraw;
			this.HasObtainedFreeGodPower = true;
			this.BoughtLimitedPetToken = oldPremium.BoughtLimitedPetToken;
			this.HasUnlimitedGenderChange = oldPremium.HasUnlimitedGenderChange;
			this.HasPetHalfStats = oldPremium.HasPetHalfStats;
			this.HasCrystalImprovement = oldPremium.HasCrystalImprovement;
			this.HasPurchasedGodPowerOnce = oldPremium.HasPurchasedGodPowerOnce;
			this.UltimateShadowSummonCount = oldPremium.UltimateShadowSummonCount;
			this.GodlyLiquidCount = oldPremium.GodlyLiquidCount;
			this.GodlyLiquidV2Count = oldPremium.GodlyLiquidV2Count;
			this.ChakraPillCount = oldPremium.ChakraPillCount;
			this.ChakraPillV2Count = oldPremium.ChakraPillV2Count;
			this.DailyPackDaysLeft = oldPremium.DailyPackDaysLeft;
			this.GPFromLuckyDraws = oldPremium.GPFromLuckyDraws;
			this.TimeForNextDailyPack = oldPremium.TimeForNextDailyPack;
			this.TotalItemsBought = oldPremium.TotalItemsBought;
			this.StatisticMulti = 1;
			this.ChallengeImprovedNextAt = oldPremium.ChallengeImprovedNextAt;
			this.PetFoodAfterRebirth = oldPremium.PetFoodAfterRebirth;
			this.CanShowAlerts = oldPremium.CanShowAlerts;
			this.MaxCrystals = oldPremium.MaxCrystals;
			this.CrystalPower = 0;
			this.HasAutoSelectPets = oldPremium.HasAutoSelectPets;
			if (this.ChallengeImprovedNextAt)
			{
				this.ImprovedNextAt = true;
			}
		}

		public void AddPremiumAfterChallenge(Premium premiumFromChallenge)
		{
			this.PetToken = premiumFromChallenge.PetToken;
			this.LuckyDraws = premiumFromChallenge.LuckyDraws;
			this.TimeForNextLuckyDraw = premiumFromChallenge.TimeForNextLuckyDraw;
			this.UltimateShadowSummonCount = premiumFromChallenge.UltimateShadowSummonCount;
			this.GodlyLiquidCount = premiumFromChallenge.GodlyLiquidCount;
			this.GodlyLiquidV2Count = premiumFromChallenge.GodlyLiquidV2Count;
			this.ChakraPillCount = premiumFromChallenge.ChakraPillCount;
			this.ChakraPillV2Count = premiumFromChallenge.ChakraPillV2Count;
			this.ChakraPillV2InUse = premiumFromChallenge.ChakraPillV2InUse;
			this.GodlyLiquidV2InUse = premiumFromChallenge.GodlyLiquidV2InUse;
			this.GodPower = this.GodPower + premiumFromChallenge.TotalGodPowerSpent + premiumFromChallenge.GodPower;
			this.TotalMight += premiumFromChallenge.TotalMight;
			this.GodPowerBought += premiumFromChallenge.GodPowerBought;
			if (string.IsNullOrEmpty(this.SteamPurchasedOrderIds))
			{
				this.SteamPurchasedOrderIds = premiumFromChallenge.SteamPurchasedOrderIds;
			}
			else
			{
				this.SteamPurchasedOrderIds = this.SteamPurchasedOrderIds + "," + premiumFromChallenge.SteamPurchasedOrderIds;
			}
			if (string.IsNullOrEmpty(this.AndroidPurchasedOrderIds))
			{
				this.AndroidPurchasedOrderIds = premiumFromChallenge.AndroidPurchasedOrderIds;
			}
			else
			{
				this.AndroidPurchasedOrderIds = this.AndroidPurchasedOrderIds + "," + premiumFromChallenge.AndroidPurchasedOrderIds;
			}
			this.TotalItemsBought = premiumFromChallenge.TotalItemsBought;
			if (!this.BoughtLimitedPetToken)
			{
				this.BoughtLimitedPetToken = premiumFromChallenge.BoughtLimitedPetToken;
			}
			this.DailyPackDaysLeft = premiumFromChallenge.DailyPackDaysLeft;
			this.GPFromLuckyDraws += premiumFromChallenge.GPFromLuckyDraws;
			this.TimeForNextDailyPack = premiumFromChallenge.TimeForNextDailyPack;
			if (!this.ChallengeImprovedNextAt)
			{
				this.ChallengeImprovedNextAt = premiumFromChallenge.ChallengeImprovedNextAt;
			}
			if (!this.PetFoodAfterRebirth)
			{
				this.PetFoodAfterRebirth = premiumFromChallenge.PetFoodAfterRebirth;
			}
			if (!this.CanShowAlerts)
			{
				this.CanShowAlerts = premiumFromChallenge.CanShowAlerts;
			}
			if (!this.HasPetHalfStats)
			{
				this.HasPetHalfStats = premiumFromChallenge.HasPetHalfStats;
			}
			if (!this.HasCrystalImprovement)
			{
				this.HasCrystalImprovement = premiumFromChallenge.HasCrystalImprovement;
			}
			if (!this.HasAutoSelectPets)
			{
				this.HasAutoSelectPets = premiumFromChallenge.HasAutoSelectPets;
			}
			this.MaxCrystals = premiumFromChallenge.MaxCrystals;
			this.CrystalPower += premiumFromChallenge.CrystalPower;
			this.GodPowerFromCrystals += premiumFromChallenge.GodPowerFromCrystals;
			this.GodPowerFromPets += premiumFromChallenge.GodPowerFromPets;
		}

		public int GodPowerPurchaseBonusPercent()
		{
			int num = App.State.Statistic.TotalRebirths.ToInt() / 10;
			if (num > 50)
			{
				num = 50;
			}
			int num2 = (App.State.Statistic.AchievementChallengesFinished / 4 + App.State.Statistic.ArtyChallengesFinished * 3 + App.State.Statistic.DoubleRebirthChallengesFinished / 10 + App.State.Statistic.NoRbChallengesFinished + App.State.Statistic.OnekChallengesFinished / 10 + App.State.Statistic.UltimateBaalChallengesFinished / 2 + App.State.Statistic.UniverseChallengesFinished / 10).ToInt();
			if (num2 > 50)
			{
				num2 = 50;
			}
			int num3 = this.GodPowerBought.ToInt() / 20 + this.TotalItemsBought / 2;
			if (num3 > 300)
			{
				num3 = 300;
			}
			return num3 + num2 + num;
		}

		public void GetDailyPack()
		{
			if (this.TimeForNextDailyPack <= 0L && this.DailyPackDaysLeft.ToInt() > 0)
			{
				this.LuckyDraws = ++this.LuckyDraws;
				this.GodPower += 2;
				this.GPFromLuckyDraws += 2;
				this.TimeForNextDailyPack = 84600000L;
				this.DailyPackDaysLeft = --this.DailyPackDaysLeft;
				GuiBase.ShowToast("You got one Lucky Draw and 2 God Power!");
			}
		}

		public void GetFreeLuckyDraw()
		{
			if (this.TimeForNextLuckyDraw <= 0)
			{
				this.DrawsUsedSinceFreeDraw = 0;
				this.LuckyDraws = ++this.LuckyDraws;
				this.TimeForNextLuckyDraw = 84600000;
				if (App.State != null)
				{
					App.State.Ext.AdsWatched = 0;
				}
				GuiBase.ShowToast("You got one free draw!");
			}
		}

		public string UseLuckyDraw()
		{
			if (this.LuckyDraws < 1)
			{
				return "You don't have enough lucky draws!";
			}
			if (App.State.Statistic.HasStartedArtyChallenge || App.State.Statistic.HasStartedUltimateBaalChallenge)
			{
				return "You can't use lucky draws while you are in an Ultimate Baal or Ultimate Arty Challenge!";
			}
			if (this.DrawsUsedSinceFreeDraw >= 50)
			{
				return "Please wait until tomorrow to upen your next lucky draw.";
			}
			LuckyType type = App.State.Ext.Lucky.Draw();
			this.DrawsUsedSinceFreeDraw = ++this.DrawsUsedSinceFreeDraw;
			switch (type)
			{
			case LuckyType.USS:
				this.UltimateShadowSummonCount++;
				break;
			case LuckyType.PetToken:
				this.PetToken = ++this.PetToken;
				break;
			case LuckyType.GP1:
				this.GPFromLuckyDraws += 1;
				this.GodPower++;
				break;
			case LuckyType.GP2:
				this.GPFromLuckyDraws += 2;
				this.GodPower += 2;
				break;
			case LuckyType.GP5:
				this.GPFromLuckyDraws += 5;
				this.GodPower += 5;
				break;
			case LuckyType.GP10:
				this.GPFromLuckyDraws += 10;
				this.GodPower += 10;
				break;
			case LuckyType.GP25:
				this.GPFromLuckyDraws += 25;
				this.GodPower += 25;
				break;
			case LuckyType.GP50:
				this.GPFromLuckyDraws += 50;
				this.GodPower += 50;
				break;
			case LuckyType.GP100:
				this.GPFromLuckyDraws += 100;
				this.GodPower += 100;
				break;
			case LuckyType.Liquid1:
				this.GodlyLiquidCount = ++this.GodlyLiquidCount;
				break;
			case LuckyType.Liquid2:
				this.GodlyLiquidV2Count = ++this.GodlyLiquidV2Count;
				break;
			case LuckyType.Chakra1:
				this.ChakraPillCount = ++this.ChakraPillCount;
				break;
			case LuckyType.Chakra2:
				this.ChakraPillV2Count = ++this.ChakraPillV2Count;
				break;
			case LuckyType.LuckyDraw:
				this.LuckyDraws += 2;
				break;
			case LuckyType.Dall:
				App.State.Multiplier.DrawMultiPhysical = App.State.Multiplier.DrawMultiPhysical * 2;
				App.State.Multiplier.DrawMultiMystic = App.State.Multiplier.DrawMultiMystic * 2;
				App.State.Multiplier.DrawMultiCreating = App.State.Multiplier.DrawMultiCreating * 2;
				App.State.Multiplier.DrawMultiBattle = App.State.Multiplier.DrawMultiBattle * 2;
				App.State.Money = App.State.Money * 2;
				break;
			case LuckyType.DPhysical:
				App.State.Multiplier.DrawMultiPhysical = App.State.Multiplier.DrawMultiPhysical * 2;
				break;
			case LuckyType.DMystic:
				App.State.Multiplier.DrawMultiMystic = App.State.Multiplier.DrawMultiMystic * 2;
				break;
			case LuckyType.DCreating:
				App.State.Multiplier.DrawMultiCreating = App.State.Multiplier.DrawMultiCreating * 2;
				break;
			case LuckyType.DBattle:
				App.State.Multiplier.DrawMultiBattle = App.State.Multiplier.DrawMultiBattle * 2;
				break;
			case LuckyType.DDiv:
				App.State.Money = App.State.Money * 2;
				break;
			case LuckyType.Div1:
			{
				CDouble rightSide = App.State.DivinityGainSec(true) * 3600;
				App.State.Money += rightSide;
				break;
			}
			case LuckyType.Div5:
			{
				CDouble rightSide2 = App.State.DivinityGainSec(true) * 3600 * 5;
				App.State.Money += rightSide2;
				break;
			}
			case LuckyType.Growth1:
				foreach (Pet current in App.State.Ext.AllPets)
				{
					if (current.IsUnlocked)
					{
						current.PhysicalGrowth += 1;
						current.MysticGrowth += 1;
						current.BattleGrowth += 1;
						current.CalculateValues();
					}
				}
				break;
			case LuckyType.Growth2:
				foreach (Pet current2 in App.State.Ext.AllPets)
				{
					if (current2.IsUnlocked)
					{
						current2.PhysicalGrowth += 2;
						current2.MysticGrowth += 2;
						current2.BattleGrowth += 2;
						current2.CalculateValues();
					}
				}
				break;
			case LuckyType.Growth5:
				foreach (Pet current3 in App.State.Ext.AllPets)
				{
					if (current3.IsUnlocked)
					{
						current3.PhysicalGrowth += 5;
						current3.MysticGrowth += 5;
						current3.BattleGrowth += 5;
						current3.CalculateValues();
					}
				}
				break;
			case LuckyType.MightyFood1:
			{
				State2 expr_6FE = App.State.Ext;
				expr_6FE.MightyFood = ++expr_6FE.MightyFood;
				break;
			}
			case LuckyType.MightyFood2:
				App.State.Ext.MightyFood += 2;
				break;
			case LuckyType.MightyFood5:
				App.State.Ext.MightyFood += 5;
				break;
			case LuckyType.CrystalSlot:
				this.MaxCrystals = ++this.MaxCrystals;
				break;
			}
			App.State.PremiumBoni.CheckIfGPIsAdjusted();
			return LuckyDraw.DrawText(type);
		}

		public bool UseGodlyLiquid()
		{
			if (this.GodlyLiquidCount > 0)
			{
				GuiBase.ShowDialog("Godly liquid", "This will increase the duration to double the speed for creations by 90 minutes. Do you want to use it?", delegate
				{
					this.GodlyLiquidCount = --this.GodlyLiquidCount;
					this.GodlyLiquidDuration += 5400000L;
					GuiBase.ShowToast("Duration is increased by 90 minutes!");
				}, delegate
				{
				}, "Yes", "No", false, false);
				return true;
			}
			return false;
		}

		public bool UseGodlyLiquidRebirth()
		{
			if (this.GodlyLiquidV2InUse)
			{
				GuiBase.ShowToast("You took already one, wait until your next rebirth to use another one!");
				return false;
			}
			if (this.GodlyLiquidV2Count > 0)
			{
				GuiBase.ShowDialog("Godly liquid V2", "This will increase the duration to double the speed for creations until your next rebirth. Do you want to use it?", delegate
				{
					this.GodlyLiquidV2Count = --this.GodlyLiquidV2Count;
					this.GodlyLiquidV2InUse = true;
					GuiBase.ShowToast("Your creation speed is doubled until your next rebirth!");
				}, delegate
				{
				}, "Yes", "No", false, false);
				return true;
			}
			return false;
		}

		public bool UseChakraPill()
		{
			if (this.ChakraPillCount > 0)
			{
				GuiBase.ShowDialog("Chakra pill", "This will increase the duration to double the speed for building monuments and upgrades by 90 minutes. Do you want to use it?", delegate
				{
					this.ChakraPillCount = --this.ChakraPillCount;
					this.ChakraPillDuration += 5400000L;
					GuiBase.ShowToast("Duration is increased by 90 minutes!");
				}, delegate
				{
				}, "Yes", "No", false, false);
				return true;
			}
			return false;
		}

		internal bool UseChakraPillRebirth()
		{
			if (this.ChakraPillV2InUse)
			{
				GuiBase.ShowToast("You took already one, wait until your next rebirth to use another one!");
				return false;
			}
			if (this.ChakraPillV2Count > 0)
			{
				GuiBase.ShowDialog("Chakra pill V2", "This will increase the duration to double the speed for building monuments and upgrades until your next rebirth. Do you want to use it?", delegate
				{
					this.ChakraPillV2Count = --this.ChakraPillV2Count;
					this.ChakraPillV2InUse = true;
					GuiBase.ShowToast("Duration is increased until your next rebirth!");
				}, delegate
				{
				}, "Yes", "No", false, false);
				return true;
			}
			return false;
		}

		public bool UseUltimateShadowSummon()
		{
			if (this.UltimateShadowSummonCount > 0)
			{
				CDouble newClones = App.State.Clones.MaxShadowClones - App.State.Clones.Count;
				GuiBase.ShowDialog("Ultimate shadow summon", "This will let you create " + newClones + " shadow clones at once. Do you want to use it?", delegate
				{
					this.UltimateShadowSummonCount--;
					App.State.Clones.Count = App.State.Clones.MaxShadowClones;
					App.State.Clones.TotalClonesCreated += newClones;
					App.State.Statistic.TotalShadowClonesCreated += newClones;
					GuiBase.ShowToast("You created " + newClones + " shadow clones at once!");
				}, delegate
				{
				}, "Yes", "No", false, false);
				return true;
			}
			return false;
		}

		public string UpdateDuration(long ms)
		{
			this.GodlyLiquidDuration -= ms;
			this.ChakraPillDuration -= ms;
			if (this.GodlyLiquidDuration < 0L)
			{
				this.GodlyLiquidDuration = 0L;
			}
			if (this.ChakraPillDuration < 0L)
			{
				this.ChakraPillDuration = 0L;
			}
			if (this.CrystalGPTimeTotal > 0)
			{
				if (this.CrystalGPTimeCurrent > this.CrystalGPTimeTotal)
				{
					this.CrystalGPTimeCurrent = this.CrystalGPTimeTotal;
				}
				this.CrystalGPTimeCurrent -= ms;
				int num = 0;
				while (this.CrystalGPTimeCurrent <= 0)
				{
					num++;
					this.CrystalGPTimeCurrent += this.CrystalGPTimeTotal;
					if (num > 9)
					{
						this.CrystalGPTimeCurrent = this.CrystalGPTimeTotal;
					}
					else
					{
						this.GodPower++;
						this.GodPowerFromCrystals = ++this.GodPowerFromCrystals;
						this.CheckIfGPIsAdjusted();
					}
				}
				if (num > 0)
				{
					return "Your god crystal produced " + num + " god power!\n";
				}
			}
			return string.Empty;
		}

		public string GetItemNameAndAddItem(string itemId, Purchase steamId = Purchase.None)
		{
			if (steamId == Purchase.GenderChange || itemId.Equals(Premium.ID_UGC))
			{
				this.HasUnlimitedGenderChange = true;
				return "You can now change your name and gender for free!";
			}
			if (steamId == Purchase.PetToken || steamId == Purchase.PetTokenLimited || itemId.Equals(Premium.ID_PET_TOKEN) || itemId.Equals(Premium.ID_PET_TOKEN_LIMITED))
			{
				if (steamId == Purchase.PetTokenLimited || itemId.Equals(Premium.ID_PET_TOKEN_LIMITED))
				{
					this.BoughtLimitedPetToken = true;
				}
				this.PetToken = ++this.PetToken;
				return "You got: 1 x Pet Token!";
			}
			if (steamId == Purchase.PetToken3 || itemId.Equals(Premium.ID_PET_TOKEN3))
			{
				this.PetToken += 3;
				return "You got: 3 x Pet Token!";
			}
			if (steamId == Purchase.ChallengeNext || itemId.Equals(Premium.ID_CHALLENGE_NEXT_AT))
			{
				this.ImprovedNextAt = true;
				this.ChallengeImprovedNextAt = true;
				return "Improved Next at will now work in all challenges!";
			}
			if (steamId == Purchase.FoodRebirth || itemId.Equals(Premium.ID_FOOD_AFTER_REBIRTH))
			{
				this.PetFoodAfterRebirth = true;
				return "You won't lose food after rebirthing anymore!";
			}
			if (steamId == Purchase.ChakraPill || itemId.Equals("xzp"))
			{
				this.ChakraPillCount = ++this.ChakraPillCount;
				return "You got: 1 x Chakra Pill!";
			}
			if (steamId == Purchase.ChakraPill10 || itemId.Equals("helloworld"))
			{
				this.ChakraPillCount += 10;
				return "You got: 10 x Chakra Pill!";
			}
			if (steamId == Purchase.ChakraPillRebirth || itemId.Equals(Premium.ID_CHAKRA_PILL_REBIRTH))
			{
				this.ChakraPillV2Count = ++this.ChakraPillV2Count;
				return "You got: 1 x Chakra Pill V2!";
			}
			if (steamId == Purchase.ChakraPillRebirth10 || itemId.Equals(Premium.ID_CHAKRA_PILL_REBIRTH10))
			{
				this.ChakraPillV2Count += 10;
				return "You got: 10 x Chakra Pill V2!";
			}
			if (steamId == Purchase.GodlyLiquid || itemId.Equals("nightmare"))
			{
				this.GodlyLiquidCount = ++this.GodlyLiquidCount;
				return "You got: 1 x Godly Liquid!";
			}
			if (steamId == Purchase.GodlyLiquid10 || itemId.Equals("myfirstid"))
			{
				this.GodlyLiquidCount += 10;
				return "You got: 10 x Godly Liquid!";
			}
			if (steamId == Purchase.GodlyLiquidRebirth || itemId.Equals(Premium.ID_GODLY_LIQUID_REBIRTH))
			{
				this.GodlyLiquidV2Count = ++this.GodlyLiquidV2Count;
				return "You got: 1 x Godly Liquid V2!";
			}
			if (steamId == Purchase.GodlyLiquidRebirth10 || itemId.Equals(Premium.ID_GODLY_LIQUID_REBIRTH10))
			{
				this.GodlyLiquidV2Count += 10;
				return "You got: 10 x Godly Liquid V2!";
			}
			if (steamId == Purchase.ShadowSummon || itemId.Equals(Premium.ID_SHADOW_SUMMON))
			{
				this.UltimateShadowSummonCount++;
				return "You got: 1 x Ultimate Shadow Summon!";
			}
			if (steamId == Purchase.ShadowSummon10 || itemId.Equals(Premium.ID_SHADOW_SUMMON10))
			{
				this.UltimateShadowSummonCount += 10;
				return "You got: 10 x Ultimate Shadow Summon!";
			}
			if (steamId == Purchase.LuckyDraw || itemId.Equals(Premium.ID_LUCKY_DRAW))
			{
				this.LuckyDraws = ++this.LuckyDraws;
				return "You got: 1 x Lucky Draw!";
			}
			if (steamId == Purchase.LuckyDraw10 || itemId.Equals(Premium.ID_LUCKY_DRAW10))
			{
				this.LuckyDraws += 10;
				return "You got: 10 x Lucky Draw!";
			}
			if (steamId == Purchase.LuckyDraw50 || itemId.Equals(Premium.ID_LUCKY_DRAW50))
			{
				this.LuckyDraws += 50;
				return "You got: 50 x Lucky Draw!";
			}
			if (steamId == Purchase.DailyPack || itemId.Equals(Premium.ID_DAILY_PACK))
			{
				if (this.TimeForNextDailyPack <= 0L)
				{
					this.TimeForNextDailyPack = 0L;
				}
				this.DailyPackDaysLeft += 30;
				return "You got: 1 x Daily Pack for 30 days!";
			}
			if (steamId == Purchase.DailyPack15 || itemId.Equals(Premium.ID_DAILY_PACK_15))
			{
				if (this.TimeForNextDailyPack <= 0L)
				{
					this.TimeForNextDailyPack = 0L;
				}
				this.DailyPackDaysLeft += 15;
				return "You got: 1 x Daily Pack for 15 days!";
			}
			if (steamId == Purchase.CrystalSlot || itemId.Equals(Premium.ID_CRYSTAL_SLOT))
			{
				this.MaxCrystals = ++this.MaxCrystals;
				return "You can now equip one more crystal!";
			}
			if (steamId == Purchase.CrystalUpgradeChance || itemId.Equals(Premium.ID_CRYSTAL_CHANCE))
			{
				this.HasCrystalImprovement = true;
				return "Your chance to upgrade crystals is now 25% higher! (maxed at 95%)";
			}
			int num = this.GodPowerPurchaseBonusPercent();
			if (steamId == Purchase.GodPower5 || itemId.Equals(Premium.ID_GOD_POWER5))
			{
				int num2 = 5 * (num + 100) / 100;
				string result = "You received " + num2 + " God Power!";
				if (!this.HasPurchasedGodPowerOnce)
				{
					this.HasPurchasedGodPowerOnce = true;
					result = string.Concat(new object[]
					{
						"You received ",
						num2,
						" God Power and an additional ",
						num2,
						" God Power for your first purchase!"
					});
					num2 *= 2;
				}
				this.GodPower += num2;
				this.GodPowerBought += num2;
				App.State.PremiumBoni.CheckIfGPIsAdjusted();
				return result;
			}
			if (steamId == Purchase.GodPower25 || itemId.Equals(Premium.ID_GOD_POWER25))
			{
				int num3 = 25 * (num + 100) / 100;
				string result2 = "You received " + num3 + " God Power!";
				if (!this.HasPurchasedGodPowerOnce)
				{
					this.HasPurchasedGodPowerOnce = true;
					result2 = string.Concat(new object[]
					{
						"You received ",
						num3,
						" God Power and an additional ",
						num3,
						" God Power for your first purchase!"
					});
					num3 *= 2;
				}
				this.GodPower += num3;
				this.GodPowerBought += num3;
				App.State.PremiumBoni.CheckIfGPIsAdjusted();
				return result2;
			}
			if (steamId == Purchase.GodPower50 || itemId.Equals("jeahitsmine"))
			{
				int num4 = 50 * (num + 100) / 100;
				string result3 = "You received " + num4 + " God Power!";
				if (!this.HasPurchasedGodPowerOnce)
				{
					this.HasPurchasedGodPowerOnce = true;
					result3 = string.Concat(new object[]
					{
						"You received ",
						num4,
						" God Power and an additional ",
						num4,
						" God Power for your first purchase!"
					});
					num4 *= 2;
				}
				this.GodPower += num4;
				this.GodPowerBought += num4;
				App.State.PremiumBoni.CheckIfGPIsAdjusted();
				return result3;
			}
			if (steamId == Purchase.GodPower100 || itemId.Equals(Premium.ID_GOD_POWER100))
			{
				int num5 = 100 * (num + 100) / 100;
				string result4 = "You received " + num5 + " God Power!";
				if (!this.HasPurchasedGodPowerOnce)
				{
					this.HasPurchasedGodPowerOnce = true;
					result4 = string.Concat(new object[]
					{
						"You received ",
						num5,
						" God Power and an additional ",
						num5,
						" God Power for your first purchase!"
					});
					num5 *= 2;
				}
				this.GodPower += num5;
				this.GodPowerBought += num5;
				App.State.PremiumBoni.CheckIfGPIsAdjusted();
				return result4;
			}
			return string.Empty;
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.GodlyLiquidDuration);
			Conv.AppendValue(stringBuilder, "b", this.GodlyLiquidCount.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.ChakraPillDuration);
			Conv.AppendValue(stringBuilder, "d", this.ChakraPillCount.Serialize());
			Conv.AppendValue(stringBuilder, "e", this.UltimateShadowSummonCount);
			Conv.AppendValue(stringBuilder, "f", this.TotalItemsBought);
			Conv.AppendValue(stringBuilder, "g", this.ItemToPurchase);
			Conv.AppendValue(stringBuilder, "h", this.creatingSpeedUpPercent.Serialize());
			Conv.AppendValue(stringBuilder, "i", this.buildingSpeedUpPercent.Serialize());
			Conv.AppendValue(stringBuilder, "j", this.GodPower);
			Conv.AppendValue(stringBuilder, "k", this.HasPurchasedGodPowerOnce.ToString());
			Conv.AppendValue(stringBuilder, "l", this.HasObtainedFreeGodPower.ToString());
			Conv.AppendValue(stringBuilder, "m", this.GodlyLiquidV2Count.Serialize());
			Conv.AppendValue(stringBuilder, "n", this.ChakraPillV2Count.Serialize());
			Conv.AppendValue(stringBuilder, "o", this.GodlyLiquidV2InUse.ToString());
			Conv.AppendValue(stringBuilder, "p", this.ChakraPillV2InUse.ToString());
			Conv.AppendValue(stringBuilder, "q", this.creationCountBoni.Serialize());
			Conv.AppendValue(stringBuilder, "r", this.GpBoniPhysical.Serialize());
			Conv.AppendValue(stringBuilder, "s", this.GpBoniMystic.Serialize());
			Conv.AppendValue(stringBuilder, "t", this.GpBoniBattle.Serialize());
			Conv.AppendValue(stringBuilder, "u", this.GpBoniCreating.Serialize());
			Conv.AppendValue(stringBuilder, "v", this.TotalGodPowerSpent);
			Conv.AppendValue(stringBuilder, "w", this.AutoBuyCostReduction);
			Conv.AppendValue(stringBuilder, "x", this.GodPowerMod);
			Conv.AppendValue(stringBuilder, "y", this.TbsMissreduction);
			Conv.AppendValue(stringBuilder, "z", this.TbsProgressAfterRebirth);
			Conv.AppendValue(stringBuilder, "B", this.ImprovedNextAt.ToString());
			Conv.AppendValue(stringBuilder, "C", this.StatisticMulti.Serialize());
			Conv.AppendValue(stringBuilder, "D", this.TbsExtraPixels);
			Conv.AppendValue(stringBuilder, "E", this.TbsDoublePoints);
			Conv.AppendValue(stringBuilder, "F", this.TotalMight);
			Conv.AppendValue(stringBuilder, "G", this.TotalMightIsUnlocked.ToString());
			Conv.AppendValue(stringBuilder, "H", this.HasUnlimitedGenderChange.ToString());
			Conv.AppendValue(stringBuilder, "I", this.PetToken.Serialize());
			Conv.AppendValue(stringBuilder, "J", this.BoughtLimitedPetToken.ToString());
			Conv.AppendValue(stringBuilder, "K", this.LuckyDraws.Serialize());
			Conv.AppendValue(stringBuilder, "L", this.TimeForNextLuckyDraw.Serialize());
			Conv.AppendValue(stringBuilder, "M", this.TotalLuckyDrawCount);
			Conv.AppendValue(stringBuilder, "N", this.GPFromLuckyDraws.Serialize());
			Conv.AppendValue(stringBuilder, "O", this.OrderIdLastSteamPurchase.ToString());
			Conv.AppendValue(stringBuilder, "P", (int)this.ItemIdToPurchase);
			Conv.AppendValue(stringBuilder, "Q", this.GodPowerBought.Serialize());
			Conv.AppendValue(stringBuilder, "R", this.SteamPurchasedOrderIds);
			Conv.AppendValue(stringBuilder, "S", this.TimeForNextDailyPack);
			Conv.AppendValue(stringBuilder, "T", this.DailyPackDaysLeft.Serialize());
			Conv.AppendValue(stringBuilder, "U", this.ChallengeImprovedNextAt.ToString());
			Conv.AppendValue(stringBuilder, "V", this.PetFoodAfterRebirth.ToString());
			Conv.AppendValue(stringBuilder, "W", this.AndroidPurchasedOrderIds);
			Conv.AppendValue(stringBuilder, "X", this.CanShowAlerts.ToString());
			Conv.AppendValue(stringBuilder, "Y", this.HasPetHalfStats.ToString());
			Conv.AppendValue(stringBuilder, "Z", this.HasCrystalImprovement.ToString());
			Conv.AppendValue(stringBuilder, NS.n1.Nr(), this.MaxCrystals.Serialize());
			Conv.AppendValue(stringBuilder, NS.n2.Nr(), this.CrystalPower.Serialize());
			Conv.AppendValue(stringBuilder, NS.n3.Nr(), this.GodPowerFromCrystals.Serialize());
			Conv.AppendValue(stringBuilder, NS.n4.Nr(), this.CrystalGPTimeCurrent.Serialize());
			Conv.AppendValue(stringBuilder, NS.n5.Nr(), this.GodPowerFromPets.Serialize());
			Conv.AppendValue(stringBuilder, NS.n6.Nr(), this.HasAutoSelectPets.ToString());
			Conv.AppendValue(stringBuilder, NS.n7.Nr(), this.DrawsUsedSinceFreeDraw.Serialize());
			return Conv.ToBase64(stringBuilder.ToString(), "Premium");
		}

		internal static Premium FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				return new Premium();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "Premium");
			Premium premium = new Premium();
			premium.GodPowerMod = Conv.getIntFromParts(parts, "x");
			premium.GodlyLiquidDuration = Conv.getLongFromParts(parts, "a");
			premium.GodlyLiquidCount = Conv.getCDoubleFromParts(parts, "b", true);
			premium.ChakraPillDuration = Conv.getLongFromParts(parts, "c");
			premium.ChakraPillCount = Conv.getCDoubleFromParts(parts, "d", true);
			premium.UltimateShadowSummonCount = Conv.getIntFromParts(parts, "e");
			premium.TotalItemsBought = Conv.getIntFromParts(parts, "f");
			premium.ItemToPurchase = Conv.getStringFromParts(parts, "g");
			premium.creatingSpeedUpPercent = Conv.getCDoubleFromParts(parts, "h", false);
			premium.buildingSpeedUpPercent = Conv.getCDoubleFromParts(parts, "i", false);
			premium.GodPower = Conv.getIntFromParts(parts, "j");
			premium.HasPurchasedGodPowerOnce = Conv.getStringFromParts(parts, "k").ToLower().Equals("true");
			premium.HasObtainedFreeGodPower = Conv.getStringFromParts(parts, "l").ToLower().Equals("true");
			premium.GodlyLiquidV2Count = Conv.getCDoubleFromParts(parts, "m", true);
			premium.ChakraPillV2Count = Conv.getCDoubleFromParts(parts, "n", true);
			premium.GodlyLiquidV2InUse = Conv.getStringFromParts(parts, "o").ToLower().Equals("true");
			premium.ChakraPillV2InUse = Conv.getStringFromParts(parts, "p").ToLower().Equals("true");
			premium.creationCountBoni = Conv.getCDoubleFromParts(parts, "q", false);
			premium.GpBoniPhysical = Conv.getCDoubleFromParts(parts, "r", false);
			premium.GpBoniMystic = Conv.getCDoubleFromParts(parts, "s", false);
			premium.GpBoniBattle = Conv.getCDoubleFromParts(parts, "t", false);
			premium.GpBoniCreating = Conv.getCDoubleFromParts(parts, "u", false);
			premium.TotalGodPowerSpent = Conv.getIntFromParts(parts, "v");
			premium.AutoBuyCostReduction = Conv.getIntFromParts(parts, "w");
			premium.TbsMissreduction = Conv.getIntFromParts(parts, "y");
			premium.TbsProgressAfterRebirth = Conv.getIntFromParts(parts, "z");
			premium.ImprovedNextAt = Conv.getStringFromParts(parts, "B").ToLower().Equals("true");
			premium.StatisticMulti = Conv.getCDoubleFromParts(parts, "C", false);
			premium.TbsExtraPixels = Conv.getIntFromParts(parts, "D");
			premium.TbsDoublePoints = Conv.getIntFromParts(parts, "E");
			premium.TotalMight = Conv.getLongFromParts(parts, "F");
			premium.TotalMightIsUnlocked = Conv.getStringFromParts(parts, "G").ToLower().Equals("true");
			premium.HasUnlimitedGenderChange = Conv.getStringFromParts(parts, "H").ToLower().Equals("true");
			premium.PetToken = Conv.getCDoubleFromParts(parts, "I", false);
			premium.BoughtLimitedPetToken = Conv.getStringFromParts(parts, "J").ToLower().Equals("true");
			premium.LuckyDraws = Conv.getCDoubleFromParts(parts, "K", false);
			premium.TimeForNextLuckyDraw = Conv.getCDoubleFromParts(parts, "L", false);
			premium.TotalLuckyDrawCount = Conv.getIntFromParts(parts, "M");
			premium.GPFromLuckyDraws = Conv.getCDoubleFromParts(parts, "N", false);
			premium.OrderIdLastSteamPurchase = Conv.getUlongFromParts(parts, "O");
			premium.ItemIdToPurchase = (Purchase)Conv.getIntFromParts(parts, "P");
			premium.GodPowerBought = Conv.getCDoubleFromParts(parts, "Q", false);
			premium.SteamPurchasedOrderIds = Conv.getStringFromParts(parts, "R");
			premium.TimeForNextDailyPack = Conv.getLongFromParts(parts, "S");
			premium.DailyPackDaysLeft = Conv.getCDoubleFromParts(parts, "T", false);
			premium.ChallengeImprovedNextAt = Conv.getStringFromParts(parts, "U").ToLower().Equals("true");
			premium.PetFoodAfterRebirth = Conv.getStringFromParts(parts, "V").ToLower().Equals("true");
			premium.AndroidPurchasedOrderIds = Conv.getStringFromParts(parts, "W");
			premium.CanShowAlerts = Conv.getStringFromParts(parts, "X").ToLower().Equals("true");
			premium.HasPetHalfStats = Conv.getStringFromParts(parts, "Y").ToLower().Equals("true");
			premium.HasCrystalImprovement = Conv.getStringFromParts(parts, "Z").ToLower().Equals("true");
			premium.MaxCrystals = Conv.getCDoubleFromParts(parts, NS.n1.Nr(), false);
			if (premium.MaxCrystals < 2)
			{
				premium.MaxCrystals = 2;
			}
			premium.CrystalPower = Conv.getCDoubleFromParts(parts, NS.n2.Nr(), true);
			premium.GodPowerFromCrystals = Conv.getCDoubleFromParts(parts, NS.n3.Nr(), true);
			premium.CrystalGPTimeCurrent = Conv.getCDoubleFromParts(parts, NS.n4.Nr(), true);
			premium.GodPowerFromPets = Conv.getCDoubleFromParts(parts, NS.n5.Nr(), true);
			premium.HasAutoSelectPets = Conv.getStringFromParts(parts, NS.n6.Nr()).ToLower().Equals("true");
			premium.DrawsUsedSinceFreeDraw = Conv.getCDoubleFromParts(parts, NS.n7.Nr(), true);
			return premium;
		}

		internal int CalculateGPSpent(GameState state)
		{
			int num = 0;
			int num2 = state.Clones.AbsoluteMaximum - 99999;
			if (num2 > 0)
			{
				num = num2 / 10000 * 25;
				string text = num2.ToString();
				if (text.EndsWith("1"))
				{
					num++;
				}
			}
			int num3 = state.PremiumBoni.creatingSpeedUpPercent.ToInt() / 35 * 10;
			int num4 = state.PremiumBoni.buildingSpeedUpPercent.ToInt() / 35 * 10;
			int num5 = state.PremiumBoni.TbsDoublePoints + state.PremiumBoni.TbsExtraPixels * 33 + state.PremiumBoni.TbsMissreduction * 2 + state.PremiumBoni.TbsProgressAfterRebirth;
			int num6 = state.PremiumBoni.creationCountBoni.ToInt() * 50;
			int num7 = state.PremiumBoni.AutoBuyCostReduction * 3;
			int num8 = 0;
			CDouble leftSide = state.PremiumBoni.StatisticMulti;
			while (leftSide > 1)
			{
				num8++;
				leftSide /= 2;
			}
			int num9 = num8 * 50;
			int num10 = num + num3 + num4 + num5 + num9 + num6 + num7;
			if (state.PremiumBoni.ImprovedNextAt && !state.Statistic.HasStartedArtyChallenge && !state.Statistic.HasStartedUltimateBaalChallenge)
			{
				num10 += 99;
			}
			return num10;
		}
	}
}
