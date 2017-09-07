using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class Statistic
	{
		public const int sec = 1000;

		public const int min = 60000;

		public const int hour = 3600000;

		public const long day = 86400000L;

		public long TimePlayedSinceRebirth;

		public long TimePlayed;

		public long TimeOffline;

		public long TimeUntilNextChocolate;

		public CDouble TotalTrainingLevels = new CDouble();

		public CDouble TotalSkillLevels = new CDouble();

		public CDouble TotalEnemiesDefeated = new CDouble();

		public CDouble TotalCreations = new CDouble();

		public CDouble TotalShadowClonesCreated = new CDouble();

		public CDouble TotalShadowClonesDied = new CDouble();

		public CDouble TotalGodsDefeated = new CDouble();

		public CDouble TotalMoneySpent = new CDouble();

		public CDouble TotalAchievements = 0;

		public CDouble TotalRebirths = 0;

		public CDouble MonumentsCreated = 0;

		public CDouble HighestGodDefeated = 0;

		public CDouble TotalUpgrades = 0;

		public CDouble MostDefeatedShadowClones = 0;

		public CDouble UBsDefeated = 0;

		public CDouble TBSScore = 0;

		public CDouble GodlyShootScore = 0;

		public CDouble GodlyShootScoreBoss = 0;

		public CDouble TotalPowersurge = 0;

		public bool HasStartedDoubleRebirthChallenge;

		public CDouble DoubleRebirthChallengesFinished = 0;

		public CDouble TimeAfterDRCStarted = 0;

		public CDouble FastestDRCallenge = -1;

		public bool HasStartedUltimateBaalChallenge;

		public CDouble UltimateBaalChallengesFinished = 0;

		public CDouble TimeAfterUBCStarted = 0;

		public CDouble FastestUBCallenge = -1;

		public CDouble RebirthsAfterUBC = 0;

		public string PremiumStatsBeforeUBCChallenge = string.Empty;

		public CDouble AbsoluteMaxClonesBeforeUBCChallenge = 0;

		public CDouble MaxClonesBeforeUBCChallenge = 0;

		public bool CountRebirthsInUBC;

		public bool HasStartedUniverseChallenge;

		public CDouble UniverseChallengesFinished = 0;

		public CDouble TimeAfterUUCStarted = 0;

		public CDouble FastestUUCallenge = -1;

		public bool HasStartedBlackHoleChallenge;

		public CDouble BlackHoleChallengesFinished = 0;

		public CDouble TimeAfterBHCStarted = 0;

		public CDouble FastestBHCallenge = -1;

		public bool HasStartedUltimatePetChallenge;

		public CDouble UltimatePetChallengesFinished = 0;

		public CDouble TimeAfterUPCStarted = 0;

		public CDouble FastestUPCallenge = -1;

		public bool HasStartedArtyChallenge;

		public CDouble ArtyChallengesFinished = 0;

		public CDouble TimeAfterUACStarted = 0;

		public CDouble FastestUACallenge = -1;

		public CDouble RebirthsAfterUAC = 0;

		public string SkillUsageCountBeforeUAC = string.Empty;

		public CDouble HighestGodInUAC = 0;

		public CDouble MinRebirthsAfterUAC = -1;

		public CDouble MinRebirthsAfterUBC = -1;

		public List<CDouble> Last5RebirthTimes = new List<CDouble>();

		public bool HasStartedAchievementChallenge;

		public CDouble AchievementChallengesFinished = 0;

		public bool HasStarted1kChallenge;

		public CDouble OnekChallengesFinished = 0;

		public CDouble TimeAfter1KCStarted = 0;

		public CDouble Fastest1KCCallenge = -1;

		public bool HasStartedNoRbChallenge;

		public CDouble NoRbChallengesFinished = 0;

		public CDouble TimeAfterNoRbStarted = 0;

		public CDouble FastestNoRbCCallenge = -1;

		public CDouble TotalPetGrowth = 0;

		public CDouble GPFromBlackHole = 0;

		public CDouble GPFromBlackHoleUpgrade = 0;

		public CDouble BlackHoleGPTimer = 0;

		public CDouble AfkyClonesKilled = 0;

		public CDouble AfkyGodPower = 0;

		public bool HasReceivedPresent;

		public string GodDefeatedBeforeRebirth = string.Empty;

		public CDouble PresentCount = 0;

		public List<SteamAndroidAchievement> ReachedAndroidAchievements = new List<SteamAndroidAchievement>();

		public bool CreatorBeaten;

		private int countMulti;

		public CDouble statisticRebirthMultiplier;

		public CDouble RandomDividerLastRebirth = 1;

		public CDouble MultiTimePlayed
		{
			get
			{
				return this.TimePlayed / 60000L;
			}
		}

		public CDouble MultiTimeOffline
		{
			get
			{
				return this.TimeOffline * 250L / 60000L;
			}
		}

		public CDouble MultiTotalTrainingLevels
		{
			get
			{
				return this.TotalTrainingLevels / 10000;
			}
		}

		public CDouble MultiTotalSkillLevels
		{
			get
			{
				return this.TotalSkillLevels / 10000;
			}
		}

		public CDouble MultiTotalEnemiesDefeated
		{
			get
			{
				return this.TotalEnemiesDefeated / 40000;
			}
		}

		public CDouble MultiTotalCreations
		{
			get
			{
				return this.TotalCreations / 1000;
			}
		}

		public CDouble MultiTotalGodsDefeated
		{
			get
			{
				return this.TotalGodsDefeated * 50;
			}
		}

		public CDouble MultiTotalRebirths
		{
			get
			{
				return this.TotalRebirths * 1000;
			}
		}

		public CDouble MultiTotalAchievements
		{
			get
			{
				return this.TotalAchievements * 5;
			}
		}

		public CDouble MultiTotalUpgrades
		{
			get
			{
				return this.TotalUpgrades * 5;
			}
		}

		public CDouble MultiHighestGodDefeated
		{
			get
			{
				if (this.HighestGodDefeated > 28)
				{
					return this.HighestGodDefeated * 5000;
				}
				return this.HighestGodDefeated * 50;
			}
		}

		public CDouble MultiMostClonesDefeated
		{
			get
			{
				return this.MostDefeatedShadowClones * this.MostDefeatedShadowClones * 500;
			}
		}

		public CDouble MultiMonumentsCreated
		{
			get
			{
				return this.MonumentsCreated;
			}
		}

		public CDouble MultiTBSScore
		{
			get
			{
				return this.TBSScore * 200;
			}
		}

		public CDouble MultiUBsDefeated
		{
			get
			{
				return this.UBsDefeated * 40;
			}
		}

		public CDouble MultiPetGrowth
		{
			get
			{
				return this.TotalPetGrowth * 10;
			}
		}

		public CDouble MultiAfkyClonesKilled
		{
			get
			{
				CDouble cDouble = this.AfkyClonesKilled / 1000;
				if (cDouble > 25000000)
				{
					cDouble = 25000000;
				}
				return cDouble;
			}
		}

		public CDouble MultiAfkyGodPower
		{
			get
			{
				CDouble cDouble = this.AfkyGodPower / 10;
				if (cDouble > 25000000)
				{
					cDouble = 25000000;
				}
				return cDouble;
			}
		}

		public CDouble MultiGodlyShootScoreBoss
		{
			get
			{
				return this.GodlyShootScoreBoss.ToInt() * 250;
			}
		}

		public CDouble MultiGodlyShootScore
		{
			get
			{
				return this.GodlyShootScore.ToInt() * 500;
			}
		}

		public CDouble MultiTotalPowersurge
		{
			get
			{
				return this.TotalPowersurge;
			}
		}

		public CDouble MultiDBChallenge
		{
			get
			{
				return this.DoubleRebirthChallengesFinished * 500000;
			}
		}

		public CDouble MultiUUChallenge
		{
			get
			{
				return this.UniverseChallengesFinished * 500000;
			}
		}

		public CDouble MultiUBChallenge
		{
			get
			{
				return this.UltimateBaalChallengesFinished * 5000000;
			}
		}

		public CDouble MultiUAChallenge
		{
			get
			{
				return this.ArtyChallengesFinished * 20000000;
			}
		}

		public CDouble MultiBHChallenge
		{
			get
			{
				return this.BlackHoleChallengesFinished * 750000;
			}
		}

		public CDouble MultiUPChallenge
		{
			get
			{
				return this.UltimatePetChallengesFinished * 2000000;
			}
		}

		public CDouble Multi1KChallenge
		{
			get
			{
				return this.OnekChallengesFinished * 1500000;
			}
		}

		public CDouble MultiNoRbChallenge
		{
			get
			{
				return this.NoRbChallengesFinished * 10000000;
			}
		}

		public CDouble MultiAchievementChallenge
		{
			get
			{
				return this.AchievementChallengesFinished * 750000;
			}
		}

		public CDouble MultiTotalMight
		{
			get
			{
				if (App.State != null)
				{
					return App.State.PremiumBoni.TotalMight * 10L;
				}
				return 0;
			}
		}

		public CDouble MultiTotalGPsUsed
		{
			get
			{
				if (App.State != null)
				{
					int totalGodPowerSpent = App.State.PremiumBoni.TotalGodPowerSpent;
					return totalGodPowerSpent * 20;
				}
				return 0;
			}
		}

		public CDouble MultiTotalMoneySpent
		{
			get
			{
				double value = this.TotalMoneySpent.Value;
				if (value == 0.0)
				{
					return 0;
				}
				double num = Math.Floor(Math.Log10(value) + 1.0) * 3.0;
				return new CDouble(num * num * num);
			}
		}

		public CDouble MultiTotalShadowClonesCreated
		{
			get
			{
				return this.TotalShadowClonesCreated / 2000;
			}
		}

		public CDouble MultiTotalShadowClonesDied
		{
			get
			{
				return this.TotalShadowClonesDied / 500;
			}
		}

		public CDouble StatisticRebirthMultiplierBase
		{
			get
			{
				CDouble leftSide = 1;
				leftSide += this.MultiTimePlayed;
				leftSide += this.MultiTimeOffline;
				leftSide += this.MultiTotalTrainingLevels;
				leftSide += this.MultiTotalSkillLevels;
				leftSide += this.MultiTotalEnemiesDefeated;
				leftSide += this.MultiTotalCreations;
				leftSide += this.MultiTotalGodsDefeated;
				leftSide += this.MultiTotalRebirths;
				leftSide += this.MultiTotalAchievements;
				leftSide += this.MultiTotalUpgrades;
				leftSide += this.MultiHighestGodDefeated;
				leftSide += this.MultiMostClonesDefeated;
				leftSide += this.MultiTotalMoneySpent;
				leftSide += this.MultiTotalShadowClonesCreated;
				leftSide += this.MultiTotalShadowClonesDied;
				leftSide += this.MultiTBSScore;
				leftSide += this.MultiUBsDefeated;
				leftSide += this.MultiTotalMight;
				leftSide += this.MultiTotalGPsUsed;
				leftSide += this.MultiGodlyShootScore;
				leftSide += this.MultiGodlyShootScoreBoss;
				leftSide += this.MultiTotalPowersurge;
				leftSide += this.MultiUUChallenge;
				leftSide += this.MultiDBChallenge;
				leftSide += this.MultiUBChallenge;
				leftSide += this.MultiUAChallenge;
				leftSide += this.MultiPetGrowth;
				leftSide += this.MultiAchievementChallenge;
				leftSide += this.Multi1KChallenge;
				leftSide += this.MultiAfkyClonesKilled;
				leftSide += this.MultiAfkyGodPower;
				leftSide += this.MultiNoRbChallenge;
				leftSide += this.MultiUPChallenge;
				return leftSide + this.MultiBHChallenge;
			}
		}

		public CDouble StatisticRebirthMultiplier
		{
			get
			{
				if (this.statisticRebirthMultiplier == null)
				{
					this.statisticRebirthMultiplier = this.StatisticRebirthMultiplierBase;
					this.statisticRebirthMultiplier *= this.MultiMonumentsCreated + 1;
				}
				this.countMulti++;
				if (this.countMulti > 60)
				{
					this.countMulti = 0;
					this.statisticRebirthMultiplier = this.StatisticRebirthMultiplierBase;
					this.statisticRebirthMultiplier *= this.MultiMonumentsCreated + 1;
				}
				CDouble cDouble = this.statisticRebirthMultiplier * App.State.PremiumBoni.StatisticMulti;
				if (cDouble > App.State.Multiplier.RebirthMulti)
				{
					cDouble = App.State.Multiplier.RebirthMulti;
				}
				return cDouble;
			}
		}

		public bool CanStartAChallenge()
		{
			return this.HighestGodDefeated >= 28 && !this.HasStartedUltimateBaalChallenge && !this.HasStartedDoubleRebirthChallenge && !this.HasStartedUniverseChallenge && !this.HasStartedArtyChallenge && !this.HasStarted1kChallenge && !this.HasStartedAchievementChallenge && !this.HasStartedNoRbChallenge && !this.HasStartedBlackHoleChallenge && !this.HasStartedUltimatePetChallenge;
		}

		public void CalculateTotalPetGrowth(List<Pet> pets)
		{
			this.TotalPetGrowth = 0;
			foreach (Pet current in pets)
			{
				if (current.IsUnlocked)
				{
					this.TotalPetGrowth += current.BattleGrowth + current.MysticGrowth + current.PhysicalGrowth;
				}
			}
		}

		public void CalcuRandomDivider()
		{
			this.RandomDividerLastRebirth = (double)UnityEngine.Random.Range((float)this.RandomDividerLastRebirth.Double + 100f, (float)this.RandomDividerLastRebirth.Double + 2000f);
			if (this.RandomDividerLastRebirth < 1)
			{
				this.RandomDividerLastRebirth = 1;
			}
		}

		public CDouble AvgTimeLastRebirths()
		{
			if (this.Last5RebirthTimes.Count == 0)
			{
				return 99999;
			}
			CDouble cDouble = 0;
			foreach (CDouble current in this.Last5RebirthTimes)
			{
				cDouble += current;
			}
			cDouble /= this.Last5RebirthTimes.Count;
			return cDouble;
		}

		public CDouble ApplyTimeMulti(CDouble baseMulti)
		{
			long num = this.TimePlayedSinceRebirth / 1000L;
			if (num == 0L)
			{
				return 1;
			}
			if (num < 3600L)
			{
				baseMulti = baseMulti * num / 7200;
				if (num < 1800L)
				{
					baseMulti /= 2;
				}
				if (num < 900L)
				{
					baseMulti /= 2;
				}
				if (num < 720L)
				{
					baseMulti /= 2;
				}
				if (num < 600L)
				{
					baseMulti /= 2;
				}
				if (num < 480L)
				{
					baseMulti /= 2;
				}
				if (num < 360L)
				{
					baseMulti /= 2;
				}
				if (num < 300L)
				{
					baseMulti /= 3;
				}
				if (num < 240L)
				{
					baseMulti /= 3;
				}
				if (num < 180L)
				{
					baseMulti /= 3;
				}
				if (num < 120L)
				{
					baseMulti /= 3;
				}
				if (this.AvgTimeLastRebirths() < 900)
				{
					if (this.RandomDividerLastRebirth < 1)
					{
						this.RandomDividerLastRebirth = 1;
					}
					baseMulti /= this.RandomDividerLastRebirth;
				}
				if (baseMulti < 1)
				{
					baseMulti = 1;
				}
				return baseMulti;
			}
			long value = (num - 3600L) / 1800L + 101L;
			baseMulti = baseMulti * value / 100;
			if (baseMulti < 1)
			{
				baseMulti = 1;
			}
			return baseMulti;
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.TimePlayed);
			Conv.AppendValue(stringBuilder, "b", this.TimeOffline);
			Conv.AppendValue(stringBuilder, "c", this.TotalTrainingLevels.Serialize());
			Conv.AppendValue(stringBuilder, "d", this.TotalSkillLevels.Serialize());
			Conv.AppendValue(stringBuilder, "e", this.TotalEnemiesDefeated.Serialize());
			Conv.AppendValue(stringBuilder, "f", this.TotalCreations.Serialize());
			Conv.AppendValue(stringBuilder, "g", this.TotalShadowClonesCreated.Serialize());
			Conv.AppendValue(stringBuilder, "h", this.TotalShadowClonesDied.Serialize());
			Conv.AppendValue(stringBuilder, "i", this.TotalGodsDefeated.Serialize());
			Conv.AppendValue(stringBuilder, "j", this.TotalMoneySpent.Serialize());
			Conv.AppendValue(stringBuilder, "k", this.TotalRebirths.Serialize());
			Conv.AppendValue(stringBuilder, "l", this.MonumentsCreated.Serialize());
			Conv.AppendValue(stringBuilder, "m", this.TotalAchievements.Serialize());
			Conv.AppendValue(stringBuilder, "n", this.HighestGodDefeated.Serialize());
			Conv.AppendValue(stringBuilder, "o", this.TotalUpgrades.Serialize());
			Conv.AppendValue(stringBuilder, "p", this.MostDefeatedShadowClones.Serialize());
			Conv.AppendValue(stringBuilder, "q", this.TimePlayedSinceRebirth);
			Conv.AppendValue(stringBuilder, "r", this.TBSScore.Serialize());
			Conv.AppendValue(stringBuilder, "s", this.UBsDefeated.Serialize());
			Conv.AppendValue(stringBuilder, "t", this.GodlyShootScore.Serialize());
			Conv.AppendValue(stringBuilder, "u", this.TotalPowersurge.Serialize());
			Conv.AppendValue(stringBuilder, "v", this.HasStartedDoubleRebirthChallenge.ToString());
			Conv.AppendValue(stringBuilder, "w", this.DoubleRebirthChallengesFinished.Serialize());
			Conv.AppendValue(stringBuilder, "x", this.TimeAfterDRCStarted.Serialize());
			Conv.AppendValue(stringBuilder, "y", this.FastestDRCallenge.Serialize());
			Conv.AppendValue(stringBuilder, "z", this.HasStartedUltimateBaalChallenge.ToString());
			Conv.AppendValue(stringBuilder, "A", this.UltimateBaalChallengesFinished.Serialize());
			Conv.AppendValue(stringBuilder, "B", this.TimeAfterUBCStarted.Serialize());
			Conv.AppendValue(stringBuilder, "C", this.FastestUBCallenge.Serialize());
			Conv.AppendValue(stringBuilder, "D", this.PremiumStatsBeforeUBCChallenge);
			Conv.AppendValue(stringBuilder, "E", this.AbsoluteMaxClonesBeforeUBCChallenge.Serialize());
			Conv.AppendValue(stringBuilder, "F", this.MaxClonesBeforeUBCChallenge.Serialize());
			Conv.AppendValue(stringBuilder, "G", this.HasStartedUniverseChallenge.ToString());
			Conv.AppendValue(stringBuilder, "H", this.UniverseChallengesFinished.Serialize());
			Conv.AppendValue(stringBuilder, "I", this.TimeAfterUUCStarted.Serialize());
			Conv.AppendValue(stringBuilder, "J", this.FastestUUCallenge.Serialize());
			Conv.AppendValue(stringBuilder, "K", this.HasStartedArtyChallenge.ToString());
			Conv.AppendValue(stringBuilder, "L", this.ArtyChallengesFinished.Serialize());
			Conv.AppendValue(stringBuilder, "M", this.TimeAfterUACStarted.Serialize());
			Conv.AppendValue(stringBuilder, "N", this.FastestUACallenge.Serialize());
			Conv.AppendValue(stringBuilder, "O", this.RebirthsAfterUAC.Serialize());
			Conv.AppendValue(stringBuilder, "P", this.SkillUsageCountBeforeUAC);
			Conv.AppendValue(stringBuilder, "Q", this.RebirthsAfterUBC.Serialize());
			Conv.AppendValue(stringBuilder, "R", this.HighestGodInUAC.Serialize());
			Conv.AppendValue(stringBuilder, "S", this.MinRebirthsAfterUAC.Serialize());
			Conv.AppendValue(stringBuilder, "T", this.MinRebirthsAfterUBC.Serialize());
			Conv.AppendValue(stringBuilder, "U", this.CountRebirthsInUBC.ToString());
			Conv.AppendValue(stringBuilder, "W", this.RandomDividerLastRebirth.Serialize());
			Conv.AppendList<CDouble>(stringBuilder, this.Last5RebirthTimes, "X");
			Conv.AppendValue(stringBuilder, NS.n1.Nr(), this.HasStartedAchievementChallenge.ToString());
			Conv.AppendValue(stringBuilder, NS.n2.Nr(), this.AchievementChallengesFinished.Serialize());
			Conv.AppendValue(stringBuilder, NS.n3.Nr(), this.HasStarted1kChallenge.ToString());
			Conv.AppendValue(stringBuilder, NS.n4.Nr(), this.OnekChallengesFinished.Serialize());
			Conv.AppendValue(stringBuilder, NS.n6.Nr(), this.TimeAfter1KCStarted.Serialize());
			Conv.AppendValue(stringBuilder, NS.n7.Nr(), this.Fastest1KCCallenge.Serialize());
			Conv.AppendValue(stringBuilder, NS.n8.Nr(), this.CreatorBeaten.ToString());
			Conv.AppendValue(stringBuilder, NS.n9.Nr(), this.GodlyShootScoreBoss.Serialize());
			Conv.AppendList<SteamAndroidAchievement>(stringBuilder, this.ReachedAndroidAchievements, NS.n10.Nr());
			Conv.AppendValue(stringBuilder, NS.n11.Nr(), this.GPFromBlackHole.Serialize());
			Conv.AppendValue(stringBuilder, NS.n12.Nr(), this.GPFromBlackHoleUpgrade.Serialize());
			Conv.AppendValue(stringBuilder, NS.n13.Nr(), this.AfkyClonesKilled.Serialize());
			Conv.AppendValue(stringBuilder, NS.n14.Nr(), this.BlackHoleGPTimer.Serialize());
			Conv.AppendValue(stringBuilder, NS.n15.Nr(), this.AfkyGodPower.Serialize());
			Conv.AppendValue(stringBuilder, NS.n16.Nr(), this.HasStartedNoRbChallenge.ToString());
			Conv.AppendValue(stringBuilder, NS.n17.Nr(), this.NoRbChallengesFinished.Serialize());
			Conv.AppendValue(stringBuilder, NS.n18.Nr(), this.TimeAfterNoRbStarted.Serialize());
			Conv.AppendValue(stringBuilder, NS.n19.Nr(), this.FastestNoRbCCallenge.Serialize());
			Conv.AppendValue(stringBuilder, NS.n20.Nr(), this.HasReceivedPresent.ToString());
			Conv.AppendValue(stringBuilder, NS.n21.Nr(), this.GodDefeatedBeforeRebirth);
			Conv.AppendValue(stringBuilder, NS.n22.Nr(), this.TimeUntilNextChocolate);
			Conv.AppendValue(stringBuilder, NS.n23.Nr(), this.PresentCount.Serialize());
			Conv.AppendValue(stringBuilder, NS.n24.Nr(), this.HasStartedBlackHoleChallenge.ToString());
			Conv.AppendValue(stringBuilder, NS.n25.Nr(), this.BlackHoleChallengesFinished.Serialize());
			Conv.AppendValue(stringBuilder, NS.n26.Nr(), this.TimeAfterBHCStarted.Serialize());
			Conv.AppendValue(stringBuilder, NS.n27.Nr(), this.FastestBHCallenge.Serialize());
			Conv.AppendValue(stringBuilder, NS.n28.Nr(), this.HasStartedUltimatePetChallenge.ToString());
			Conv.AppendValue(stringBuilder, NS.n29.Nr(), this.UltimatePetChallengesFinished.Serialize());
			Conv.AppendValue(stringBuilder, NS.n30.Nr(), this.TimeAfterUPCStarted.Serialize());
			Conv.AppendValue(stringBuilder, NS.n31.Nr(), this.FastestUPCallenge.Serialize());
			return Conv.ToBase64(stringBuilder.ToString(), "Statistic");
		}

		internal static Statistic FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("Statistic.FromString with empty value!");
				return new Statistic();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "Statistic");
			Statistic statistic = new Statistic();
			statistic.TimePlayed = Conv.getLongFromParts(parts, "a");
			statistic.TimeOffline = Conv.getLongFromParts(parts, "b");
			statistic.TotalTrainingLevels = new CDouble(Conv.getStringFromParts(parts, "c"));
			statistic.TotalSkillLevels = new CDouble(Conv.getStringFromParts(parts, "d"));
			statistic.TotalEnemiesDefeated = new CDouble(Conv.getStringFromParts(parts, "e"));
			statistic.TotalCreations = new CDouble(Conv.getStringFromParts(parts, "f"));
			statistic.TotalShadowClonesCreated = new CDouble(Conv.getStringFromParts(parts, "g"));
			statistic.TotalShadowClonesDied = new CDouble(Conv.getStringFromParts(parts, "h"));
			statistic.TotalGodsDefeated = new CDouble(Conv.getStringFromParts(parts, "i"));
			statistic.TotalMoneySpent = new CDouble(Conv.getStringFromParts(parts, "j"));
			statistic.TotalRebirths = Conv.getCDoubleFromParts(parts, "k", false);
			statistic.MonumentsCreated = Conv.getCDoubleFromParts(parts, "l", false);
			statistic.TotalAchievements = Conv.getCDoubleFromParts(parts, "m", false);
			statistic.HighestGodDefeated = Conv.getCDoubleFromParts(parts, "n", false);
			statistic.TotalUpgrades = Conv.getCDoubleFromParts(parts, "o", false);
			statistic.MostDefeatedShadowClones = Conv.getCDoubleFromParts(parts, "p", false);
			statistic.TimePlayedSinceRebirth = Conv.getLongFromParts(parts, "q");
			statistic.TBSScore = Conv.getCDoubleFromParts(parts, "r", false);
			statistic.UBsDefeated = Conv.getCDoubleFromParts(parts, "s", false);
			statistic.GodlyShootScore = Conv.getCDoubleFromParts(parts, "t", false);
			statistic.TotalPowersurge = Conv.getCDoubleFromParts(parts, "u", false);
			statistic.HasStartedDoubleRebirthChallenge = Conv.getStringFromParts(parts, "v").ToLower().Equals("true");
			statistic.DoubleRebirthChallengesFinished = Conv.getCDoubleFromParts(parts, "w", false);
			statistic.TimeAfterDRCStarted = Conv.getCDoubleFromParts(parts, "x", false);
			statistic.FastestDRCallenge = Conv.getCDoubleFromParts(parts, "y", false);
			statistic.HasStartedUltimateBaalChallenge = Conv.getStringFromParts(parts, "z").ToLower().Equals("true");
			statistic.UltimateBaalChallengesFinished = Conv.getCDoubleFromParts(parts, "A", false);
			statistic.TimeAfterUBCStarted = Conv.getCDoubleFromParts(parts, "B", false);
			statistic.FastestUBCallenge = Conv.getCDoubleFromParts(parts, "C", false);
			statistic.PremiumStatsBeforeUBCChallenge = Conv.getStringFromParts(parts, "D");
			statistic.AbsoluteMaxClonesBeforeUBCChallenge = Conv.getCDoubleFromParts(parts, "E", false);
			statistic.MaxClonesBeforeUBCChallenge = Conv.getCDoubleFromParts(parts, "F", false);
			statistic.HasStartedUniverseChallenge = Conv.getStringFromParts(parts, "G").ToLower().Equals("true");
			statistic.UniverseChallengesFinished = Conv.getCDoubleFromParts(parts, "H", false);
			statistic.TimeAfterUUCStarted = Conv.getCDoubleFromParts(parts, "I", false);
			statistic.FastestUUCallenge = Conv.getCDoubleFromParts(parts, "J", false);
			statistic.HasStartedArtyChallenge = Conv.getStringFromParts(parts, "K").ToLower().Equals("true");
			statistic.ArtyChallengesFinished = Conv.getCDoubleFromParts(parts, "L", false);
			statistic.TimeAfterUACStarted = Conv.getCDoubleFromParts(parts, "M", false);
			statistic.FastestUACallenge = Conv.getCDoubleFromParts(parts, "N", false);
			statistic.RebirthsAfterUAC = Conv.getCDoubleFromParts(parts, "O", false);
			statistic.SkillUsageCountBeforeUAC = Conv.getStringFromParts(parts, "P");
			statistic.RebirthsAfterUBC = Conv.getCDoubleFromParts(parts, "Q", false);
			statistic.HighestGodInUAC = Conv.getCDoubleFromParts(parts, "R", false);
			statistic.MinRebirthsAfterUAC = Conv.getCDoubleFromParts(parts, "S", false);
			statistic.MinRebirthsAfterUBC = Conv.getCDoubleFromParts(parts, "T", false);
			statistic.CountRebirthsInUBC = Conv.getStringFromParts(parts, "U").ToLower().Equals("true");
			statistic.RandomDividerLastRebirth = Conv.getCDoubleFromParts(parts, "W", false);
			string stringFromParts = Conv.getStringFromParts(parts, "X");
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
					statistic.Last5RebirthTimes.Add(new CDouble(text));
				}
			}
			statistic.HasStartedAchievementChallenge = Conv.getStringFromParts(parts, NS.n1.Nr()).ToLower().Equals("true");
			statistic.AchievementChallengesFinished = Conv.getCDoubleFromParts(parts, NS.n2.Nr(), false);
			statistic.HasStarted1kChallenge = Conv.getStringFromParts(parts, NS.n3.Nr()).ToLower().Equals("true");
			statistic.OnekChallengesFinished = Conv.getCDoubleFromParts(parts, NS.n4.Nr(), false);
			statistic.TimeAfter1KCStarted = Conv.getCDoubleFromParts(parts, NS.n6.Nr(), false);
			statistic.Fastest1KCCallenge = Conv.getCDoubleFromParts(parts, NS.n7.Nr(), false);
			statistic.CreatorBeaten = Conv.getStringFromParts(parts, NS.n8.Nr()).ToLower().Equals("true");
			statistic.GodlyShootScoreBoss = Conv.getCDoubleFromParts(parts, NS.n9.Nr(), false);
			stringFromParts = Conv.getStringFromParts(parts, NS.n10.Nr());
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array3 = array;
			for (int j = 0; j < array3.Length; j++)
			{
				string value = array3[j];
				if (!string.IsNullOrEmpty(value))
				{
					statistic.ReachedAndroidAchievements.Add((SteamAndroidAchievement)Conv.StringToInt(value));
				}
			}
			statistic.GPFromBlackHole = Conv.getCDoubleFromParts(parts, NS.n11.Nr(), false);
			statistic.GPFromBlackHoleUpgrade = Conv.getCDoubleFromParts(parts, NS.n12.Nr(), false);
			statistic.AfkyClonesKilled = Conv.getCDoubleFromParts(parts, NS.n13.Nr(), false);
			statistic.BlackHoleGPTimer = Conv.getCDoubleFromParts(parts, NS.n14.Nr(), false);
			statistic.AfkyGodPower = Conv.getCDoubleFromParts(parts, NS.n15.Nr(), false);
			statistic.HasStartedNoRbChallenge = Conv.getStringFromParts(parts, NS.n16.Nr()).ToLower().Equals("true");
			statistic.NoRbChallengesFinished = Conv.getCDoubleFromParts(parts, NS.n17.Nr(), false);
			statistic.TimeAfterNoRbStarted = Conv.getCDoubleFromParts(parts, NS.n18.Nr(), false);
			statistic.FastestNoRbCCallenge = Conv.getCDoubleFromParts(parts, NS.n19.Nr(), false);
			statistic.HasReceivedPresent = Conv.getStringFromParts(parts, NS.n20.Nr()).ToLower().Equals("true");
			statistic.GodDefeatedBeforeRebirth = Conv.getStringFromParts(parts, NS.n21.Nr());
			statistic.TimeUntilNextChocolate = Conv.getLongFromParts(parts, NS.n22.Nr());
			statistic.PresentCount = Conv.getCDoubleFromParts(parts, NS.n23.Nr(), false);
			statistic.HasStartedBlackHoleChallenge = Conv.getStringFromParts(parts, NS.n24.Nr()).ToLower().Equals("true");
			statistic.BlackHoleChallengesFinished = Conv.getCDoubleFromParts(parts, NS.n25.Nr(), false);
			statistic.TimeAfterBHCStarted = Conv.getCDoubleFromParts(parts, NS.n26.Nr(), false);
			statistic.FastestBHCallenge = Conv.getCDoubleFromParts(parts, NS.n27.Nr(), false);
			statistic.HasStartedUltimatePetChallenge = Conv.getStringFromParts(parts, NS.n28.Nr()).ToLower().Equals("true");
			statistic.UltimatePetChallengesFinished = Conv.getCDoubleFromParts(parts, NS.n29.Nr(), false);
			statistic.TimeAfterUPCStarted = Conv.getCDoubleFromParts(parts, NS.n30.Nr(), false);
			statistic.FastestUPCallenge = Conv.getCDoubleFromParts(parts, NS.n31.Nr(), false);
			return statistic;
		}
	}
}
