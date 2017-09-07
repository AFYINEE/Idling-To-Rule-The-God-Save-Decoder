using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class God
	{
		public enum GodType
		{
			None,
			Hyperion,
			Itztli,
			Gaia,
			Shu,
			Suijin,
			Gefion,
			Hathor,
			Pontus,
			Diana,
			Izanagi,
			Nephthys,
			Cybele,
			Artemis,
			Eros,
			Freya,
			Poseidon,
			Laima,
			Athena,
			Susano_o,
			Zeus,
			Nyx,
			Luna,
			Jupiter,
			Odin,
			Amaterasu,
			Coatlicue,
			Chronos,
			Tyrant_Overlord_Baal
		}

		public bool IsDefeatedForFirstTime;

		public bool IsDefeatedPetChallenge;

		private CDouble currentHealth;

		private static readonly CDouble HP1 = new CDouble("5000000");

		private static readonly CDouble HP2 = new CDouble("75000000");

		private static readonly CDouble HP3 = new CDouble("1500000000");

		private static readonly CDouble HP4 = new CDouble("35000000000");

		private static readonly CDouble HP5 = new CDouble("650000000000");

		private static readonly CDouble HP6 = new CDouble("10000000000000");

		private static readonly CDouble HP7 = new CDouble("500000000000000");

		private static readonly CDouble HP8 = new CDouble("10000000000000000");

		private static readonly CDouble HP9 = new CDouble("1000000000000000000");

		private static readonly CDouble HP10 = new CDouble("100000000000000000000");

		private static readonly CDouble HP11 = new CDouble("10000000000000000000000");

		private static readonly CDouble HP12 = new CDouble("1000000000000000000000000");

		private static readonly CDouble HP13 = new CDouble("100000000000000000000000000");

		private static readonly CDouble HP14 = new CDouble("10000000000000000000000000000");

		private static readonly CDouble HP15 = new CDouble("1000000000000000000000000000000");

		private static readonly CDouble HP16 = new CDouble("100000000000000000000000000000000");

		private static readonly CDouble HP17 = new CDouble("10000000000000000000000000000000000");

		private static readonly CDouble HP18 = new CDouble("1000000000000000000000000000000000000");

		private static readonly CDouble HP19 = new CDouble("100000000000000000000000000000000000000");

		private static readonly CDouble HP20 = new CDouble("10000000000000000000000000000000000000000");

		private static readonly CDouble HP21 = new CDouble("1000000000000000000000000000000000000000000");

		private static readonly CDouble HP22 = new CDouble("100000000000000000000000000000000000000000000");

		private static readonly CDouble HP23 = new CDouble("10000000000000000000000000000000000000000000000");

		private static readonly CDouble HP24 = new CDouble("1000000000000000000000000000000000000000000000000");

		private static readonly CDouble HP25 = new CDouble("100000000000000000000000000000000000000000000000000");

		private static readonly CDouble HP26 = new CDouble("10000000000000000000000000000000000000000000000000000");

		private static readonly CDouble HP27 = new CDouble("1000000000000000000000000000000000000000000000000000000");

		private static readonly CDouble HP28 = new CDouble("499999999999999999999999999999999999999999999999999999999");

		private CDouble criticalChance;

		public CDouble powerLevel;

		private string powerLevelText = string.Empty;

		public CDouble DamageSec = new CDouble();

		public God.GodType TypeEnum
		{
			get;
			set;
		}

		public bool IsDefeated
		{
			get;
			set;
		}

		public CDouble CurrentHealth
		{
			get
			{
				if (this.currentHealth < 0)
				{
					return 0;
				}
				if (this.currentHealth > this.MaxHealth)
				{
					return this.MaxHealth;
				}
				return this.currentHealth;
			}
			set
			{
				this.currentHealth = value;
			}
		}

		public string Name
		{
			get
			{
				return EnumName.Name(this.TypeEnum);
			}
		}

		public string Desc
		{
			get
			{
				switch (this.TypeEnum)
				{
				case God.GodType.Hyperion:
					return "God of light to create your own light.\nTo damage gods your attack has to be higher than the gods defense." + this.PowerLevelText;
				case God.GodType.Itztli:
					return "God of stone and sacrifice.\nYou just want to create some stones.\nHe won't tell you how until you beat him!" + this.PowerLevelText;
				case God.GodType.Gaia:
					return "The personification of earth.\nShe should just tell you how to create some soil." + this.PowerLevelText;
				case God.GodType.Shu:
					return "God of the wind and air.\nDefeat him so you can create your air to be able to breathe again!" + this.PowerLevelText;
				case God.GodType.Suijin:
					return "God of Water.\nBeat him to learn how to create some water from your newly gained air." + this.PowerLevelText;
				case God.GodType.Gefion:
					return "Goddess of agriculture and the plough.\nSo you want to learn how to create some plants?" + this.PowerLevelText;
				case God.GodType.Hathor:
					return "Goddess of the sky, love, beauty, joy, motherhood, foreign lands, mining, music and fertility.\nThat's a lot but she also knows how to create trees." + this.PowerLevelText;
				case God.GodType.Pontus:
					return "God of the sea, father of the fish and other sea creatures.\nMaybe he will tell you how to create some fish?" + this.PowerLevelText;
				case God.GodType.Diana:
					return "Goddess of the hunt, wild animals and the wilderness.\nAfter your fish you might want some meat. So create some animals first!" + this.PowerLevelText;
				case God.GodType.Izanagi:
					return "God of the sky and the creator of everything good and right.\nSo why can you create humans after beating him?" + this.PowerLevelText;
				case God.GodType.Nephthys:
					return "Goddess of rivers.\nTo create a river might be a good idea, right? So your water won't disappear into the earth again." + this.PowerLevelText;
				case God.GodType.Cybele:
					return "Goddess of caverns, mountains, nature and wild animals.\nSo you can create your own mountain after you beat her. Not that you need this knowledge..." + this.PowerLevelText;
				case God.GodType.Artemis:
					return "Goddess of the Hunt, Forests and Hills, the Moon, Archery. associated with the night.\nNot enough to create the night though. But maybe some forests." + this.PowerLevelText;
				case God.GodType.Eros:
					return "God of Erotic love/Passion/Sex.\nJust what you need to know to create a village" + this.PowerLevelText;
				case God.GodType.Freya:
					return "Goddess of Fertility/Love/Beauty/Sex/War.\nShe knows how to create bigger villages!" + this.PowerLevelText;
				case God.GodType.Poseidon:
					return "King of the sea and lord of the sea gods.\nAlso god of something you don't need to know, just know you can create oceans after beating him." + this.PowerLevelText;
				case God.GodType.Laima:
					return "Goddess of fate. She was associated with childbirth, marriage, and death.\nThat sounds like some knowledge you need to create a nation." + this.PowerLevelText;
				case God.GodType.Athena:
					return "Goddess of wisdom, warfare, civilization, strength, strategy, crafts, and justice.\nYep a much deeper knowledge so you might be able to create a continent." + this.PowerLevelText;
				case God.GodType.Susano_o:
					return "God of the winds, storms, ocean and snakes.\nYou need a weather to grow your plants...\nA little late now isn't it? Well its really hard to create the weather!" + this.PowerLevelText;
				case God.GodType.Zeus:
					return "King of the gods, god of the sky, weather, thunder, law, order and fate.\nYou already know how the create the weather...\nBut you might learn how to create a sky!" + this.PowerLevelText;
				case God.GodType.Nyx:
					return "Goddess of night.\nIt’s boring with only a bright sky, and it’s hard to sleep. You now need to create the night." + this.PowerLevelText;
				case God.GodType.Luna:
					return "Divine embodiment of the Moon.\nLearn how to create your own Moon with her divine knowledge!\nShe only tells you after you beat her." + this.PowerLevelText;
				case God.GodType.Jupiter:
					return "King of heaven and god of the sky and weather.\nWith your and his knowledge combined, you now might be powerful enough to create your own planet." + this.PowerLevelText;
				case God.GodType.Odin:
					return "The chief god of the Aesir the allfather\nHe might tell you how to create an earthlike planet.\nThe last planet you created was quite lifeless." + this.PowerLevelText;
				case God.GodType.Amaterasu:
					return "Goddess of the sun.\nYes you need a sun to bring life to your planet.\nAfter you beat her, you might be able to create one!" + this.PowerLevelText;
				case God.GodType.Coatlicue:
					return "The Mother of Gods who gave birth to the moon, stars.\nJust what you need to create a whole solar system.\nYour last one didn't have enough gravity." + this.PowerLevelText;
				case God.GodType.Chronos:
					return "The personification of time.\nYou have to be faster than time to beat him!\nAnd you also need a lot of time to create a whole galaxy..." + this.PowerLevelText;
				case God.GodType.Tyrant_Overlord_Baal:
					return "It's said to be the strongest being in the universe.\nYou need to beat the strongest one in the universe to create one, right?" + this.PowerLevelText;
				default:
					return string.Empty;
				}
			}
		}

		public string Description
		{
			get
			{
				if (this.TypeEnum == God.GodType.Tyrant_Overlord_Baal)
				{
					return this.Desc + "\nDoes a 500% damage critical attack every time.";
				}
				return string.Concat(new object[]
				{
					this.Desc,
					"\nDoes a 500% damage critical attack every ",
					(int)((God.GodType)29 - this.TypeEnum),
					" attacks."
				});
			}
		}

		public CDouble Attack
		{
			get
			{
				switch (this.TypeEnum)
				{
				case God.GodType.Hyperion:
					return God.HP1;
				case God.GodType.Itztli:
					return God.HP2;
				case God.GodType.Gaia:
					return God.HP3;
				case God.GodType.Shu:
					return God.HP4;
				case God.GodType.Suijin:
					return God.HP5;
				case God.GodType.Gefion:
					return God.HP6;
				case God.GodType.Hathor:
					return God.HP7;
				case God.GodType.Pontus:
					return God.HP8;
				case God.GodType.Diana:
					return God.HP9;
				case God.GodType.Izanagi:
					return God.HP10;
				case God.GodType.Nephthys:
					return God.HP11;
				case God.GodType.Cybele:
					return God.HP12;
				case God.GodType.Artemis:
					return God.HP13;
				case God.GodType.Eros:
					return God.HP14;
				case God.GodType.Freya:
					return God.HP15;
				case God.GodType.Poseidon:
					return God.HP16;
				case God.GodType.Laima:
					return God.HP17;
				case God.GodType.Athena:
					return God.HP18;
				case God.GodType.Susano_o:
					return God.HP19;
				case God.GodType.Zeus:
					return God.HP20;
				case God.GodType.Nyx:
					return God.HP21;
				case God.GodType.Luna:
					return God.HP22;
				case God.GodType.Jupiter:
					return God.HP23;
				case God.GodType.Odin:
					return God.HP24;
				case God.GodType.Amaterasu:
					return God.HP25;
				case God.GodType.Coatlicue:
					return God.HP26;
				case God.GodType.Chronos:
					return God.HP27;
				case God.GodType.Tyrant_Overlord_Baal:
					return God.HP28;
				default:
					return God.HP28;
				}
			}
		}

		public CDouble Defense
		{
			get
			{
				return this.Attack / 8 * 5;
			}
		}

		public CDouble MaxHealth
		{
			get
			{
				return this.Attack * 10;
			}
		}

		public CDouble CriticalChance
		{
			get
			{
				if (this.criticalChance == null)
				{
					if (this.TypeEnum >= God.GodType.Tyrant_Overlord_Baal)
					{
						this.criticalChance = 1;
					}
					else
					{
						this.criticalChance = new CDouble(1.0 / (double)((God.GodType)29 - this.TypeEnum));
					}
				}
				return this.criticalChance;
			}
		}

		public CDouble PowerLevel
		{
			get
			{
				if (this.powerLevel == null)
				{
					this.powerLevel = (this.Defense + this.Attack * 2) * (1 + this.CriticalChance * 5);
				}
				return this.powerLevel;
			}
		}

		protected string PowerLevelText
		{
			get
			{
				if (string.IsNullOrEmpty(this.powerLevelText))
				{
					this.powerLevelText = string.Concat(new string[]
					{
						"lowerTextPower level: ",
						this.PowerLevel.ToGuiText(true),
						"\nHP: ",
						this.MaxHealth.ToGuiText(true),
						"\nAttack: ",
						this.Attack.ToGuiText(true),
						"\nDefense ",
						this.Defense.ToGuiText(true)
					});
				}
				return this.powerLevelText;
			}
		}

		public God(God.GodType type)
		{
			this.IsDefeated = false;
			this.TypeEnum = type;
			this.CurrentHealth = this.MaxHealth;
			if (type == God.GodType.None)
			{
				this.IsDefeated = true;
			}
			else
			{
				this.IsDefeated = false;
			}
		}

		internal static List<God> InitialGods()
		{
			return new List<God>
			{
				new God(God.GodType.Hyperion),
				new God(God.GodType.Itztli),
				new God(God.GodType.Gaia),
				new God(God.GodType.Shu),
				new God(God.GodType.Suijin),
				new God(God.GodType.Gefion),
				new God(God.GodType.Hathor),
				new God(God.GodType.Pontus),
				new God(God.GodType.Diana),
				new God(God.GodType.Izanagi),
				new God(God.GodType.Nephthys),
				new God(God.GodType.Cybele),
				new God(God.GodType.Artemis),
				new God(God.GodType.Eros),
				new God(God.GodType.Freya),
				new God(God.GodType.Poseidon),
				new God(God.GodType.Laima),
				new God(God.GodType.Athena),
				new God(God.GodType.Susano_o),
				new God(God.GodType.Zeus),
				new God(God.GodType.Nyx),
				new God(God.GodType.Luna),
				new God(God.GodType.Jupiter),
				new God(God.GodType.Odin),
				new God(God.GodType.Amaterasu),
				new God(God.GodType.Coatlicue),
				new God(God.GodType.Chronos),
				new God(God.GodType.Tyrant_Overlord_Baal)
			};
		}

		public void InitPowerLevelText()
		{
			this.powerLevelText = string.Empty;
		}

		public void GetAttacked(CDouble attackPower, long millisecs)
		{
			int value = UnityEngine.Random.Range(1, 100);
			CDouble cDouble;
			if (value <= App.State.Crits.CriticalPercent(App.State.GameSettings.TBSEyesIsMirrored))
			{
				attackPower = attackPower * App.State.Crits.CriticalDamage / 100;
				cDouble = attackPower / 5000 * millisecs;
			}
			else
			{
				cDouble = (attackPower - this.Defense) / 5000 * millisecs;
			}
			this.DamageSec = cDouble * 33;
			if (cDouble > 0)
			{
				this.CurrentHealth -= cDouble;
			}
			if (this.CurrentHealth <= 0)
			{
				App.State.Title = EnumName.Title(App.State.Avatar.IsFemale, this.TypeEnum);
				App.State.TitleGod = this.Name;
				if (App.State.Statistic.HighestGodDefeated < (int)this.TypeEnum)
				{
					App.State.Statistic.HighestGodDefeated = (int)this.TypeEnum;
				}
				if (App.State.Statistic.HasStartedArtyChallenge && App.State.Statistic.HighestGodInUAC < (int)this.TypeEnum)
				{
					App.State.Statistic.HighestGodInUAC = (int)this.TypeEnum;
				}
				this.IsDefeated = true;
				if (this.TypeEnum > God.GodType.Diana || !this.IsDefeatedForFirstTime)
				{
					this.IsDefeatedForFirstTime = true;
					App.State.PremiumBoni.GodPower++;
					GuiBase.ShowToast("Your god power is increased by 1!");
					App.State.PremiumBoni.CheckIfGPIsAdjusted();
				}
				Statistic expr_212 = App.State.Statistic;
				expr_212.TotalGodsDefeated = ++expr_212.TotalGodsDefeated;
				StoryUi.SetUnlockedStoryParts(App.State);
				Leaderboards.SubmitStat(LeaderBoardType.GodsDefeated, App.State.Statistic.TotalGodsDefeated.ToInt(), false);
				Leaderboards.SubmitStat(LeaderBoardType.HighestGodDefeated, App.State.Statistic.HighestGodDefeated.ToInt(), false);
				HeroImage.Init(true);
				if (this.TypeEnum == God.GodType.Diana)
				{
					App.State.IsMonumentUnlocked = true;
					GuiBase.ShowContentUnlocked("You can now build monuments.");
				}
				else if (this.TypeEnum == God.GodType.Nephthys)
				{
					App.State.IsBuyUnlocked = true;
					App.State.GameSettings.AutoBuyCreationsForMonuments = App.State.GameSettings.AutoBuyCreationsForMonumentsBeforeRebirth;
					GuiBase.ShowContentUnlocked("You can now buy creations after you created at least one.");
				}
				else if (this.TypeEnum == God.GodType.Poseidon && App.CurrentPlattform == Plattform.Android && !App.State.Ext.RateDialogShown)
				{
					App.State.Ext.RateDialogShown = true;
					GuiBase.ShowDialog("Rate ItRtG", "If you like the game, please rate it on google play. Good ratings helps to get more players, and more players will lead to more success of the game, and more time for me to work on updates or future games.", delegate
					{
						App.OpenWebsite("https://play.google.com/store/apps/details?id=de.shugasu.itrtg");
					}, delegate
					{
					}, "Rate it", "Cancel", true, true);
				}
				else if (this.TypeEnum == God.GodType.Freya)
				{
					App.State.IsUpgradeUnlocked = true;
					GuiBase.ShowContentUnlocked("You can now upgrade your monuments!");
				}
				else if (this.TypeEnum == God.GodType.Chronos && App.State.Statistic.HasStartedArtyChallenge)
				{
					UpdateStats.SaveToServer(UpdateStats.ServerSaveType.UAChallengeSave2);
				}
				else if (this.TypeEnum == God.GodType.Tyrant_Overlord_Baal)
				{
					if (App.State.Statistic.HasStartedDoubleRebirthChallenge)
					{
						App.State.Statistic.HasStartedDoubleRebirthChallenge = false;
						if (App.State.Statistic.FastestDRCallenge <= 0 || App.State.Statistic.FastestDRCallenge > App.State.Statistic.TimeAfterDRCStarted)
						{
							App.State.Statistic.FastestDRCallenge = App.State.Statistic.TimeAfterDRCStarted;
							Leaderboards.SubmitStat(LeaderBoardType.FastestDRCallenge, App.State.Statistic.FastestDRCallenge.ToInt() / 1000, false);
						}
						App.State.Statistic.TimeAfterDRCStarted = 0;
						Statistic expr_493 = App.State.Statistic;
						expr_493.DoubleRebirthChallengesFinished = ++expr_493.DoubleRebirthChallengesFinished;
						int num = 10;
						if (App.State.Statistic.DoubleRebirthChallengesFinished > 50)
						{
							num += 10;
						}
						App.State.PremiumBoni.GodPower += num;
						App.State.PremiumBoni.CheckIfGPIsAdjusted();
						App.SaveGameState();
						GuiBase.ShowToast("Your god power is increased by " + num + " because you beat the double rebirth challenge!");
					}
					if (App.State.Statistic.HasStarted1kChallenge)
					{
						App.State.Statistic.HasStarted1kChallenge = false;
						if (App.State.Statistic.Fastest1KCCallenge <= 0 || App.State.Statistic.Fastest1KCCallenge > App.State.Statistic.TimeAfter1KCStarted)
						{
							App.State.Statistic.Fastest1KCCallenge = App.State.Statistic.TimeAfter1KCStarted;
							Leaderboards.SubmitStat(LeaderBoardType.Fastest1KCCallenge, App.State.Statistic.Fastest1KCCallenge.ToInt() / 1000, false);
						}
						App.State.Statistic.TimeAfter1KCStarted = 0;
						Statistic expr_5DA = App.State.Statistic;
						expr_5DA.OnekChallengesFinished = ++expr_5DA.OnekChallengesFinished;
						App.State.Clones.MaxShadowClones = App.State.Clones.AbsoluteMaximum;
						App.State.PremiumBoni.GodPower += 20;
						App.State.PremiumBoni.CheckIfGPIsAdjusted();
						App.SaveGameState();
						GuiBase.ShowToast("Your god power is increased by " + 20 + " because you beat the 1k challenge!");
					}
					if (App.State.Statistic.HasStartedUltimatePetChallenge)
					{
						App.State.Statistic.HasStartedUltimatePetChallenge = false;
						if (App.State.Statistic.FastestUPCallenge <= 0 || App.State.Statistic.FastestUPCallenge > App.State.Statistic.TimeAfterUPCStarted)
						{
							App.State.Statistic.FastestUPCallenge = App.State.Statistic.TimeAfterUPCStarted;
							Leaderboards.SubmitStat(LeaderBoardType.FastestUPCallenge, App.State.Statistic.FastestUPCallenge.ToInt() / 1000, false);
						}
						App.State.Statistic.TimeAfterUPCStarted = 0;
						Statistic expr_71F = App.State.Statistic;
						expr_71F.UltimatePetChallengesFinished = ++expr_71F.UltimatePetChallengesFinished;
						App.SaveGameState();
						GuiBase.ShowToast("You have finished the ultimate pet challenge! The rewards you can get from pet campaigns are increased by 5%!");
					}
					if (App.State.Statistic.HasStartedNoRbChallenge)
					{
						App.State.Statistic.HasStartedNoRbChallenge = false;
						if (App.State.Statistic.FastestNoRbCCallenge <= 0 || App.State.Statistic.FastestNoRbCCallenge > App.State.Statistic.TimeAfterNoRbStarted)
						{
							App.State.Statistic.FastestNoRbCCallenge = App.State.Statistic.TimeAfterNoRbStarted;
							Leaderboards.SubmitStat(LeaderBoardType.FastestNRChallenge, App.State.Statistic.FastestNoRbCCallenge.ToInt() / 1000, false);
						}
						App.State.Statistic.TimeAfterNoRbStarted = 0;
						Statistic expr_809 = App.State.Statistic;
						expr_809.NoRbChallengesFinished = ++expr_809.NoRbChallengesFinished;
						App.State.PremiumBoni.GodPower += 250;
						App.State.PremiumBoni.CheckIfGPIsAdjusted();
						App.SaveGameState();
						GuiBase.ShowToast("Your god power is increased by " + 250 + " because you beat the no rebirth challenge!");
					}
					if (App.State.Statistic.HasStartedUltimateBaalChallenge)
					{
						App.State.Statistic.HasStartedUltimateBaalChallenge = false;
						if (App.State.Statistic.FastestUBCallenge <= 0 || App.State.Statistic.FastestUBCallenge > App.State.Statistic.TimeAfterUBCStarted)
						{
							App.State.Statistic.FastestUBCallenge = App.State.Statistic.TimeAfterUBCStarted;
							if (App.State.ShouldSubmitScore)
							{
								long num2 = 0L;
								long.TryParse((App.State.Statistic.FastestUBCallenge.Double / 1000.0).ToString(), out num2);
								if (num2 == 0L)
								{
									num2 = App.State.Statistic.FastestUBCallenge.ToLong() / 1000L;
								}
								Leaderboards.SubmitStat(LeaderBoardType.FastestUBCallenge, (int)num2, false);
							}
						}
						App.State.Statistic.TimeAfterUBCStarted = 0;
						Statistic expr_983 = App.State.Statistic;
						expr_983.UltimateBaalChallengesFinished = ++expr_983.UltimateBaalChallengesFinished;
						if (App.State.Statistic.MinRebirthsAfterUBC == -1 || App.State.Statistic.MinRebirthsAfterUBC > App.State.Statistic.RebirthsAfterUBC)
						{
							App.State.Statistic.MinRebirthsAfterUBC = App.State.Statistic.RebirthsAfterUBC;
						}
						App.State.Statistic.RebirthsAfterUBC = 0;
						App.State.HomePlanet.UltimateBeingsV2[0].IsAvailable = true;
						Premium premiumBoni = App.State.PremiumBoni;
						App.State.PremiumBoni = Premium.FromString(App.State.Statistic.PremiumStatsBeforeUBCChallenge);
						App.State.PremiumBoni.AddPremiumAfterChallenge(premiumBoni);
						App.State.Statistic.PremiumStatsBeforeUBCChallenge = string.Empty;
						App.State.Clones.AbsoluteMaximum = App.State.Statistic.AbsoluteMaxClonesBeforeUBCChallenge.ToInt();
						App.State.Clones.MaxShadowClones = App.State.Statistic.MaxClonesBeforeUBCChallenge.ToInt();
						App.State.PremiumBoni.GodPower += 100;
						App.State.PremiumBoni.CheckIfGPIsAdjusted();
						App.SaveGameState();
						GuiBase.ShowToast("Your god power is increased by 100 because you beat the ultimate baal challenge!");
					}
					if (App.State.Statistic.HasStartedArtyChallenge)
					{
						App.State.Statistic.HasStartedArtyChallenge = false;
						long num3 = 0L;
						if (App.State.Statistic.FastestUACallenge <= 0 || App.State.Statistic.FastestUACallenge > App.State.Statistic.TimeAfterUACStarted)
						{
							App.State.Statistic.FastestUACallenge = App.State.Statistic.TimeAfterUACStarted;
							num3 = (long)(App.State.Statistic.FastestUACallenge.Double / 1000.0);
							if (App.State.ShouldSubmitScore)
							{
								if (num3 == 0L)
								{
									num3 = App.State.Statistic.FastestUACallenge.ToLong() / 1000L;
								}
								if (num3 > 2000000L || App.State.PremiumBoni.HasPurchasedGodPowerOnce)
								{
									Leaderboards.SubmitStat(LeaderBoardType.FastestUACallenge, (int)num3, false);
								}
							}
						}
						else
						{
							num3 = (long)(App.State.Statistic.TimeAfterUACStarted.Double / 1000.0);
						}
						App.State.Statistic.TimeAfterUACStarted = 0;
						Statistic expr_C4C = App.State.Statistic;
						expr_C4C.ArtyChallengesFinished = ++expr_C4C.ArtyChallengesFinished;
						App.State.HomePlanet.IsCreated = true;
						if (App.State.HomePlanet.UpgradeLevelArtyChallenge > App.State.HomePlanet.UpgradeLevel)
						{
							App.State.HomePlanet.UpgradeLevel = App.State.HomePlanet.UpgradeLevelArtyChallenge;
						}
						if (App.State.Statistic.UniverseChallengesFinished > 0 && App.State.HomePlanet.UpgradeLevel < App.State.Statistic.UniverseChallengesFinished + 5)
						{
							App.State.HomePlanet.UpgradeLevel = App.State.Statistic.UniverseChallengesFinished + 5;
						}
						App.State.HomePlanet.UpgradeLevelArtyChallenge = 0;
						foreach (UltimateBeing current in App.State.HomePlanet.UltimateBeings)
						{
							if (current.Tier <= App.State.HomePlanet.UpgradeLevel)
							{
								current.IsAvailable = true;
							}
						}
						App.State.HomePlanet.UltimateBeingsV2[0].IsAvailable = true;
						if (App.State.Statistic.MinRebirthsAfterUAC <= 0 || App.State.Statistic.MinRebirthsAfterUAC > App.State.Statistic.RebirthsAfterUAC)
						{
							App.State.Statistic.MinRebirthsAfterUAC = App.State.Statistic.RebirthsAfterUAC;
						}
						App.State.Statistic.RebirthsAfterUAC = 0;
						Premium premiumBoni2 = App.State.PremiumBoni;
						App.State.PremiumBoni = Premium.FromString(App.State.Statistic.PremiumStatsBeforeUBCChallenge);
						App.State.PremiumBoni.AddPremiumAfterChallenge(premiumBoni2);
						App.State.Statistic.PremiumStatsBeforeUBCChallenge = string.Empty;
						App.State.Statistic.HighestGodInUAC = 0;
						App.State.Clones.AbsoluteMaximum = App.State.Statistic.AbsoluteMaxClonesBeforeUBCChallenge.ToInt();
						App.State.Clones.MaxShadowClones = App.State.Statistic.MaxClonesBeforeUBCChallenge.ToInt();
						string[] array = App.State.Statistic.SkillUsageCountBeforeUAC.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							int num4 = 0;
							int.TryParse(array[i], out num4);
							if (App.State.AllSkills.Count > i)
							{
								App.State.AllSkills[i].Extension.UsageCount += (long)num4;
							}
						}
						App.State.Statistic.SkillUsageCountBeforeUAC = string.Empty;
						App.State.PremiumBoni.GodPower += 200;
						App.State.PremiumBoni.CheckIfGPIsAdjusted();
						App.SaveGameState();
						if (num3 < 2000000L)
						{
							GuiBase.ShowBigMessage("Contratulations, you have beaten the Ultimate Arty Challenge!\nHowever your time doesn't seem to be legit, you won't get the Pet Token Reward.\nIf you really beat the challenge in this time without cheating, please report to denny.stoehr@shugasu.com, so I can look at it.");
						}
						else if (App.State.Statistic.ArtyChallengesFinished == 1)
						{
							GuiBase.ShowBigMessage("Contratulations, you have beaten the Ultimate Arty Challenge!\nYour god power is increased by 200, you unlocked the turtle with the 'Arty was here' sign and you received the turtle pet!");
						}
						else if (App.State.Statistic.ArtyChallengesFinished == 2)
						{
							Premium expr_1032 = App.State.PremiumBoni;
							expr_1032.PetToken = ++expr_1032.PetToken;
							GuiBase.ShowBigMessage("Contratulations, you have beaten the Ultimate Arty Challenge!\nYour god power is increased by 200, and you received a pet token!");
						}
					}
					App.State.PrinnyBaal.IsUnlocked = true;
				}
			}
			Log.Info("CurrentHealth: " + this.CurrentHealth);
		}

		public void RecoverHealth(long millisecs)
		{
			if (this.IsDefeated && this.CurrentHealth == this.MaxHealth)
			{
				return;
			}
			this.CurrentHealth += millisecs * this.Defense / 20000 + 1;
		}

		public double getPercentOfHP()
		{
			return (this.CurrentHealth / this.MaxHealth).Double;
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", (int)this.TypeEnum);
			Conv.AppendValue(stringBuilder, "b", this.IsDefeated.ToString());
			Conv.AppendValue(stringBuilder, "f", this.CurrentHealth.Serialize());
			Conv.AppendValue(stringBuilder, "g", this.IsDefeatedForFirstTime.ToString());
			Conv.AppendValue(stringBuilder, "h", this.IsDefeatedPetChallenge.ToString());
			return Conv.ToBase64(stringBuilder.ToString(), "God");
		}

		internal static God FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("God.FromString with empty value!");
				return new God(God.GodType.Tyrant_Overlord_Baal);
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "God");
			return new God((God.GodType)Conv.StringToInt(Conv.getStringFromParts(parts, "a")))
			{
				IsDefeated = Conv.getStringFromParts(parts, "b").ToLower().Equals("true"),
				CurrentHealth = new CDouble(Conv.getStringFromParts(parts, "f")),
				IsDefeatedForFirstTime = Conv.getStringFromParts(parts, "g").ToLower().Equals("true"),
				IsDefeatedPetChallenge = Conv.getStringFromParts(parts, "h").ToLower().Equals("true")
			};
		}
	}
}
