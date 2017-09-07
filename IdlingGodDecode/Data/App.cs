using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using Assets.Scripts.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class App
	{
		public const string GameName = "Idling to rule the Gods";

		public const string VersionDate = "Date: 2017-08-09";

		public const string VersionName = "Version: 2.13.740";

		public const string GameVersion = "2.13.740";

		public static float HeightMulti = (float)Screen.height / 600f;

		public static float WidthMulti = (float)Screen.width / 960f;

		public static GameState State;

		public static long ServerTime;

		public static bool OfflineStatsChecked = false;

		public static bool UserIdIsCorrect = true;

		public static bool CanShowAds = false;

		public static bool AdOpened = false;

		public static bool GetOnlineSave = false;

		public static bool EnableOfflineCalc = true;

		public static bool SyncWithOnlineSave = true;

		public const int Modifier = -36546;

		public static string ServerSave = string.Empty;

		public static Plattform CurrentPlattform = Plattform.Android;

		public static bool IsTimeTooOldForSteam
		{
			get
			{
				if (App.State == null)
				{
					return true;
				}
				long num = App.State.Statistic.TimePlayed + App.State.Statistic.TimeOffline * 1000L;
				return num > App.State.Ext.TimeSinceSteam;
			}
		}

		public static void UpdateResolutionInfo()
		{
			App.HeightMulti = (float)Screen.height / 600f;
			App.WidthMulti = (float)Screen.width / 960f;
		}

		internal static void Init()
		{
			App.OfflineStatsChecked = false;
			App.LoadGameState();
			App.State.CheckForCheats();
			App.State.InitAchievementNames();
			App.State.Multiplier.RecalculateMonumentMultis(App.State);
			if (App.CurrentPlattform == Plattform.Steam)
			{
				Leaderboards.Instance.Init();
			}
			foreach (Creation current in App.State.AllCreations)
			{
				current.InitSubItemCost(0);
			}
			int num = UnityEngine.Random.Range(10, 50);
			int godPower = App.State.PremiumBoni.GodPower;
			CDouble uBMultiplier = App.State.HomePlanet.UBMultiplier;
			CDouble powerSurgeMultiplier = App.State.HomePlanet.PowerSurgeMultiplier;
			CDouble shadowCloneCount = App.State.HomePlanet.ShadowCloneCount;
			int baalPower = App.State.HomePlanet.BaalPower;
			CDouble statisticMulti = App.State.PremiumBoni.StatisticMulti;
			int tbsExtraPixels = App.State.PremiumBoni.TbsExtraPixels;
			int tbsDoublePoints = App.State.PremiumBoni.TbsDoublePoints;
			long totalMight = App.State.PremiumBoni.TotalMight;
			App.State.HomePlanet.PlanetMultiplierMod = num + 56;
			App.State.PremiumBoni.GodPowerMod = num;
			App.State.PremiumBoni.GodPower = godPower;
			App.State.PremiumBoni.TotalMight = totalMight;
			App.State.PremiumBoni.StatisticMulti = statisticMulti;
			App.State.PremiumBoni.TbsExtraPixels = tbsExtraPixels;
			App.State.PremiumBoni.TbsDoublePoints = tbsDoublePoints;
			App.State.HomePlanet.UBMultiplier = uBMultiplier;
			App.State.HomePlanet.PowerSurgeMultiplier = powerSurgeMultiplier;
			App.State.HomePlanet.ShadowCloneCount = shadowCloneCount;
			App.State.HomePlanet.BaalPower = baalPower;
			num = UnityEngine.Random.Range(10, 50);
			int absoluteMaximum = App.State.Clones.AbsoluteMaximum;
			App.State.Clones.MaxCloneModifier = num;
			App.State.Clones.AbsoluteMaximum = absoluteMaximum;
			App.LoadOnlineAfterInit();
		}

		public static void LoadOnlineAfterInit()
		{
			if (!App.GetOnlineSave || !App.SyncWithOnlineSave)
			{
				return;
			}
			bool flag = false;
			if (App.CurrentPlattform == Plattform.Kongregate && Kongregate.APILoaded)
			{
				App.State.KongUserId = Kongregate.UserId;
				App.State.KongUserName = Kongregate.Username;
				flag = true;
			}
			if (App.CurrentPlattform == Plattform.Steam && SteamManager.Initialized)
			{
				SteamHelper.InitSteamId();
				flag = true;
			}
			if (App.CurrentPlattform == Plattform.Android && Social.localUser.authenticated)
			{
				flag = true;
			}
			if (App.GetOnlineSave && flag)
			{
				App.GetOnlineSave = false;
				UpdateStats.LoadFromServer(UpdateStats.ServerSaveType.SaveToCompare);
			}
		}

		public static void OpenWebsite(string url)
		{
			if (App.CurrentPlattform == Plattform.Kongregate)
			{
				Application.ExternalEval("window.open('" + url + "','_blank')");
			}
			else
			{
				Application.OpenURL(url);
			}
		}

		internal static void ConnectToKongregate()
		{
			if (App.CurrentPlattform == Plattform.Kongregate && Kongregate.APILoaded)
			{
				Kongregate.SubmitStat("initialized", 1, true);
				Kongregate.InitAdEventListener();
				if (!Kongregate.IsGuest)
				{
					if (string.IsNullOrEmpty(App.State.KongUserId) || string.IsNullOrEmpty(App.State.KongUserName) || "Guest".Equals(App.State.KongUserName))
					{
						App.State.KongUserName = Kongregate.Username;
						App.State.KongUserId = Kongregate.UserId;
						if (string.IsNullOrEmpty(App.State.AvatarName) || "Guest".Equals(App.State.AvatarName))
						{
							App.State.AvatarName = App.State.KongUserName;
						}
					}
					if (App.CheckForUserId())
					{
						App.InitialLeaderboardSubmit();
						Kongregate.CheckBoughtItems();
					}
				}
				App.LoadOnlineAfterInit();
			}
		}

		public static void InitialLeaderboardSubmit()
		{
			App.State.CheckForCheats();
			Leaderboards.SubmitStat(LeaderBoardType.Rebirths, App.State.Statistic.TotalRebirths.ToInt(), false);
			Leaderboards.SubmitStat(LeaderBoardType.GodsDefeated, App.State.Statistic.TotalGodsDefeated.ToInt(), false);
			Leaderboards.SubmitStat(LeaderBoardType.Achievements, App.State.Statistic.TotalAchievements.ToInt(), false);
			Leaderboards.SubmitStat(LeaderBoardType.Monuments, App.State.Statistic.MonumentsCreated.ToInt(), false);
			Leaderboards.SubmitStat(LeaderBoardType.HighestGodDefeated, App.State.Statistic.HighestGodDefeated.ToInt(), false);
			Leaderboards.SubmitStat(LeaderBoardType.MostClonesDefeated, App.State.Statistic.MostDefeatedShadowClones.ToInt(), false);
			Leaderboards.SubmitStat(LeaderBoardType.TBSScore, App.State.Statistic.TBSScore.ToInt(), false);
			Leaderboards.SubmitStat(LeaderBoardType.ScoreGodlyShoot, App.State.Statistic.GodlyShootScore.ToInt(), false);
			Leaderboards.SubmitStat(LeaderBoardType.GodlyShootScoreBoss, App.State.Statistic.GodlyShootScoreBoss.ToInt(), false);
			Leaderboards.SubmitStat(LeaderBoardType.ClonesCreated, (App.State.Statistic.TotalShadowClonesCreated / 1000).ToInt(), false);
			Leaderboards.SubmitStat(LeaderBoardType.AfkyClonesKilled, (App.State.Statistic.AfkyClonesKilled / 1000000).ToInt(), false);
			Leaderboards.SubmitStat(LeaderBoardType.AfkyGodPower, App.State.Statistic.AfkyGodPower.ToInt(), false);
			if (App.State.Statistic.FastestDRCallenge > 0)
			{
				Leaderboards.SubmitStat(LeaderBoardType.FastestDRCallenge, (int)(App.State.Statistic.FastestDRCallenge.ToLong() / 1000L), false);
			}
			if (App.State.Statistic.Fastest1KCCallenge > 0)
			{
				Leaderboards.SubmitStat(LeaderBoardType.Fastest1KCCallenge, (int)(App.State.Statistic.Fastest1KCCallenge.ToLong() / 1000L), false);
			}
			if (App.State.Statistic.FastestUBCallenge > 0)
			{
				Leaderboards.SubmitStat(LeaderBoardType.FastestUBCallenge, (int)(App.State.Statistic.FastestUBCallenge.ToLong() / 1000L), false);
			}
			if (App.State.Statistic.FastestUACallenge > 0)
			{
				Leaderboards.SubmitStat(LeaderBoardType.FastestUACallenge, (int)(App.State.Statistic.FastestUACallenge.ToLong() / 1000L), false);
			}
			if (App.State.Statistic.FastestUUCallenge > 0)
			{
				Leaderboards.SubmitStat(LeaderBoardType.FastestUUCallenge, (int)(App.State.Statistic.FastestUUCallenge.ToLong() / 1000L), false);
			}
			if (App.State.Statistic.FastestNoRbCCallenge > 0)
			{
				Leaderboards.SubmitStat(LeaderBoardType.FastestNRChallenge, (int)(App.State.Statistic.FastestNoRbCCallenge.ToLong() / 1000L), false);
			}
			if (App.State.Statistic.FastestUPCallenge > 0)
			{
				Leaderboards.SubmitStat(LeaderBoardType.FastestUPCallenge, (int)(App.State.Statistic.FastestUPCallenge.ToLong() / 1000L), false);
			}
			if (App.State.Statistic.FastestBHCallenge > 0)
			{
				Leaderboards.SubmitStat(LeaderBoardType.FastestBHCallenge, (int)(App.State.Statistic.FastestBHCallenge.ToLong() / 1000L), false);
			}
			Leaderboards.SubmitStat(LeaderBoardType.TotalPetGrowth, App.State.Statistic.TotalPetGrowth.ToInt(), false);
		}

		internal static bool CheckForUserId()
		{
			if (!Kongregate.APILoaded)
			{
				return true;
			}
			if (Kongregate.IsGuest)
			{
				return true;
			}
			if (Kongregate.UserId.Equals(App.State.KongUserId))
			{
				return true;
			}
			App.UserIdIsCorrect = false;
			return false;
		}

		internal static void LoadGameState()
		{
			try
			{
				App.State = Storage.LoadGameState("AutoSave");
			}
			catch (Exception ex)
			{
				Log.Error(ex.StackTrace + "\nBad exception on loading the game! Should not happen.");
			}
			if (App.State == null)
			{
				App.State = new GameState(true, 0);
			}
			App.GetOnlineSave = true;
			MainUi.Instance.Init(false);
		}

		internal static string SaveGameState()
		{
			return Storage.SaveGameState(App.State, "AutoSave");
		}

		public static void CheckRebirth(Action<bool> result, bool showDefaultDialog = true)
		{
			foreach (Creation current in App.State.AllCreations)
			{
				if (current.TypeEnum == Creation.CreationType.Light && !current.GodToDefeat.IsDefeated)
				{
					GuiBase.ShowToast("You need to defeat " + current.GodToDefeat.Name + " first to be able to rebirth!");
					result(false);
					return;
				}
			}
			if (App.State.Statistic.HasStartedArtyChallenge && App.State.Statistic.RebirthsAfterUAC > 5)
			{
				GuiBase.ShowDialog("Are you sure?", "If you rebirth now, you will cancel your Arty challenge without getting any rewards!", delegate
				{
					result(true);
				}, delegate
				{
					result(false);
				}, "Yes", "No", false, false);
			}
			else
			{
				App.CheckCampaigns(delegate(bool campaign)
				{
					if (campaign)
					{
						App.CheckBaalPower(delegate(bool baalpower)
						{
							if (baalpower)
							{
								App.CheckPets(delegate(bool pets)
								{
									if (pets)
									{
										if (showDefaultDialog)
										{
											GuiBase.ShowDialog("Are you sure?", "This will overwrite your current multiplier, even if your stats after rebirthing are lower.", delegate
											{
												result(true);
											}, delegate
											{
												result(false);
											}, "Yes", "No", false, false);
										}
										else
										{
											result(true);
										}
									}
									else
									{
										result(false);
									}
								});
							}
							else
							{
								result(false);
							}
						});
					}
				});
			}
		}

		public static void CheckBaalPower(Action<bool> result)
		{
			if (App.State.HomePlanet.BaalPower > 3 && App.State.PremiumBoni.PetFoodAfterRebirth)
			{
				GuiBase.ShowDialog("Are you sure?", "You still have " + App.State.HomePlanet.BaalPower + " baal power left.", delegate
				{
					result(true);
				}, delegate
				{
					result(false);
				}, "Yes", "No", false, false);
			}
			else
			{
				result(true);
			}
		}

		public static void CheckCampaigns(Action<bool> result)
		{
			bool flag = false;
			foreach (PetCampaign current in App.State.Ext.AllCampaigns)
			{
				if (current.TotalDuration > 0)
				{
					GuiBase.ShowDialog("Are you sure?", "There are still pet campaigns running.", delegate
					{
						result(true);
					}, delegate
					{
						result(false);
					}, "Yes", "No", false, false);
					flag = true;
				}
			}
			if (!flag)
			{
				result(true);
			}
		}

		private static void CheckPets(Action<bool> result)
		{
			foreach (Pet current in App.State.Ext.AllPets)
			{
				if (current.CanFeed && current.IsUnlocked)
				{
					GuiBase.ShowDialog("Are you sure?", "Your pets are hungry!", delegate
					{
						result(true);
					}, delegate
					{
						result(false);
					}, "Yes", "No", false, false);
					return;
				}
			}
			result(true);
		}

		internal static void Rebirth()
		{
			Monument monument3 = App.State.AllMonuments.FirstOrDefault((Monument x) => x.TypeEnum == Monument.MonumentType.black_hole);
			if (monument3 != null && monument3.Upgrade.Level > 0)
			{
				int num = monument3.Upgrade.Level.ToInt();
				if (num > 50)
				{
					num = 50;
				}
				App.State.PremiumBoni.GodPower += num;
				App.State.Statistic.GPFromBlackHoleUpgrade += num;
				GuiBase.ShowBigMessage("You received " + num + " God Power from your black hole upgrades!");
			}
			if (App.State.Statistic.HasStartedNoRbChallenge)
			{
				GuiBase.ShowToast("Because of your rebirth, you lost your No Rebirth Challenge!");
				App.State.Statistic.HasStartedNoRbChallenge = false;
			}
			App.State.Statistic.Last5RebirthTimes.Add(App.State.Statistic.TimePlayedSinceRebirth);
			if (App.State.Statistic.Last5RebirthTimes.Count > 5)
			{
				App.State.Statistic.Last5RebirthTimes.RemoveAt(0);
			}
			bool shouldSubmitScore = App.State.ShouldSubmitScore;
			CDouble cDouble = App.State.Statistic.ApplyTimeMulti(App.State.Multiplier.RebirthMulti * App.State.Statistic.StatisticRebirthMultiplier);
			if (App.State.Statistic.AvgTimeLastRebirths() < 900 || App.State.Statistic.RandomDividerLastRebirth < 1)
			{
				App.State.Statistic.CalcuRandomDivider();
			}
			else
			{
				App.State.Statistic.RandomDividerLastRebirth = 1;
			}
			bool flag = false;
			foreach (Creation current in App.State.AllCreations)
			{
				if (current.GodToDefeat.IsDefeated)
				{
					App.State.Statistic.GodDefeatedBeforeRebirth = current.GodToDefeat.Name;
					if (current.TypeEnum == Creation.CreationType.Universe)
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				App.State.Statistic.GodDefeatedBeforeRebirth = "P. Baal v " + (App.State.PrinnyBaal.Level - 1);
			}
			string base64String = App.State.Statistic.Serialize();
			CDouble cDouble2 = App.State.Ext.AfkGame.ExpMulti;
			CDouble cDouble3 = App.State.Ext.AfkGame.Power.Level / 10000000;
			if (cDouble3 > 1)
			{
				cDouble3 = 1;
			}
			cDouble2 += cDouble3;
			CDouble achievementMultiPhysical = App.State.Multiplier.MultiBoniPhysicalRebirth * cDouble;
			CDouble achievementMultiMystic = App.State.Multiplier.MultiBoniMysticRebirth * cDouble;
			CDouble achievementMultiBattle = App.State.Multiplier.MultiBoniBattleRebirth * cDouble;
			CDouble achievementMultiCreating = App.State.Multiplier.MultiBoniCreatingRebirth * cDouble;
			int maxShadowClonesRebirth = App.State.Clones.MaxShadowClonesRebirth;
			int absoluteMaximum = App.State.Clones.AbsoluteMaximum;
			Premium premiumBoni = App.State.PremiumBoni;
			AvatarOptions avatar = App.State.Avatar;
			List<Monument> allMonuments = App.State.AllMonuments;
			List<GeneratorUpgrade> upgrades = App.State.Generator.Upgrades;
			PlayerKredProblems kredProblems = App.State.KredProblems;
			Planet homePlanet = App.State.HomePlanet;
			long creatingSpeedBoniDuration = App.State.CreatingSpeedBoniDuration;
			string avatarName = App.State.AvatarName;
			List<float> randomValues = App.State.Ext.RandomValues;
			int currentRandomNumber = App.State.Ext.CurrentRandomNumber;
			Critical crits = App.State.Crits;
			crits.Eyes = crits.Eyes * App.State.PremiumBoni.TbsProgressAfterRebirth / 100;
			crits.Feet = crits.Feet * App.State.PremiumBoni.TbsProgressAfterRebirth / 100;
			crits.Mouth = crits.Mouth * App.State.PremiumBoni.TbsProgressAfterRebirth / 100;
			crits.Tail = crits.Tail * App.State.PremiumBoni.TbsProgressAfterRebirth / 100;
			crits.Wings = crits.Wings * App.State.PremiumBoni.TbsProgressAfterRebirth / 100;
			crits.EyesNoMirror = crits.EyesNoMirror * App.State.PremiumBoni.TbsProgressAfterRebirth / 100;
			List<Creation> allCreations = App.State.AllCreations;
			Settings gameSettings = App.State.GameSettings;
			string kongUserId = App.State.KongUserId;
			string kongUserName = App.State.KongUserName;
			string steamId = App.State.SteamId;
			string steamName = App.State.SteamName;
			string androidId = App.State.AndroidId;
			string androidName = App.State.AndroidName;
			bool isSocialDialogShown = App.State.IsSocialDialogShown;
			List<Skill> allSkills = App.State.AllSkills;
			List<Pet> allPets = App.State.Ext.AllPets;
			foreach (Pet current2 in allPets)
			{
				current2.FeedTimer = 43200000L;
				current2.Exp = 0;
				current2.Level = 1;
				current2.ShadowCloneCount = 0;
				current2.CloneBattle = 1;
				current2.CloneMystic = 1;
				current2.ClonePhysical = 1;
				current2.CalculateValues();
			}
			State2 ext = App.State.Ext;
			List<Might> allMights = App.State.AllMights;
			long timeStampGameClosed = App.State.TimeStampGameClosed;
			long timeStampGameClosedOfflineMS = App.State.TimeStampGameClosedOfflineMS;
			CDouble cDouble4 = 0;
			foreach (Crystal current3 in App.State.Ext.Factory.EquippedCrystals)
			{
				CDouble cDouble5 = current3.Level;
				if (current3.Type == ModuleType.Ultimate)
				{
					cDouble5 *= 2;
				}
				if (current3.Type == ModuleType.God)
				{
					cDouble5 *= 3;
				}
				cDouble4 += cDouble5;
			}
			App.State = new GameState(true, App.State.Statistic.AchievementChallengesFinished.ToInt());
			App.State.GameSettings.AutofillDefenders = false;
			App.State.TimeStampGameClosed = timeStampGameClosed;
			App.State.TimeStampGameClosedOfflineMS = timeStampGameClosedOfflineMS;
			App.State.Ext.AfkGame.InitExpCost();
			App.State.Ext.SteamCurrency = ext.SteamCurrency;
			App.State.Ext.SteamCountry = ext.SteamCountry;
			App.State.Ext.TimeSinceSteam = ext.TimeSinceSteam;
			App.State.Ext.AdPoints = ext.AdPoints;
			App.State.Ext.AdsWatched = ext.AdsWatched;
			App.State.Ext.TotalAdsWatched = ext.TotalAdsWatched;
			App.State.Ext.ImportedSaveFromKongToSteam = ext.ImportedSaveFromKongToSteam;
			App.State.Ext.KongConvertId = ext.KongConvertId;
			App.State.Ext.TwitterClicked = ext.TwitterClicked;
			App.State.Ext.FacebookClicked = ext.FacebookClicked;
			App.State.Ext.PetIdInAvatar = ext.PetIdInAvatar;
			App.State.Ext.Lucky = ext.Lucky;
			App.State.Ext.RateDialogShown = ext.RateDialogShown;
			App.State.Ext.PetStonesSpent = ext.PetStonesSpent;
			App.State.Ext.AfkGame.ExpMulti = cDouble2;
			App.State.KongUserId = kongUserId;
			App.State.KongUserName = kongUserName;
			App.State.SteamId = steamId;
			App.State.SteamName = steamName;
			App.State.AndroidId = androidId;
			App.State.AndroidName = androidName;
			App.State.IsTutorialShown = true;
			App.State.IsGuestMsgShown = true;
			App.State.IsSocialDialogShown = isSocialDialogShown;
			App.State.Multiplier.AchievementMultiPhysical = achievementMultiPhysical;
			App.State.Multiplier.AchievementMultiMystic = achievementMultiMystic;
			App.State.Multiplier.AchievementMultiBattle = achievementMultiBattle;
			App.State.Multiplier.AchievementMultiCreating = achievementMultiCreating;
			App.State.Clones.MaxShadowClones = maxShadowClonesRebirth;
			App.State.Clones.AbsoluteMaximum = absoluteMaximum;
			App.State.Multiplier.GodMultiFromRebirth = cDouble;
			App.State.Statistic = Statistic.FromString(base64String);
			Statistic expr_981 = App.State.Statistic;
			expr_981.TotalRebirths = ++expr_981.TotalRebirths;
			App.State.Statistic.TimePlayedSinceRebirth = 0L;
			App.State.Statistic.BlackHoleGPTimer = 0;
			App.State.Ext.AfkGame.Exp = App.State.Statistic.TotalRebirths * 30 * App.State.Ext.AfkGame.ExpMulti;
			App.State.AddMultisFromGod();
			App.State.KredProblems = kredProblems;
			App.State.Crits = crits;
			App.State.CreatingSpeedBoniDuration = creatingSpeedBoniDuration;
			int num2 = App.State.Statistic.TotalShadowClonesCreated.ToInt() / 1000;
			if (num2 > 950)
			{
				num2 = 950;
			}
			App.State.CloneAttackDivider = 1000 - num2;
			int num3 = App.State.Statistic.TotalShadowClonesDied.ToInt() / 500;
			if (num3 > 950)
			{
				num3 = 950;
			}
			App.State.CloneDefenseDivider = 1000 - num3;
			App.State.CloneHealthDivider = 1000 - num3;
			App.State.InitAchievementNames();
			App.State.PremiumBoni = premiumBoni;
			App.State.PremiumBoni.ChakraPillV2InUse = false;
			App.State.PremiumBoni.GodlyLiquidV2InUse = false;
			App.State.PremiumBoni.TotalMightIsUnlocked = false;
			App.State.PremiumBoni.CrystalGPTimeCurrent = 0;
			App.State.PremiumBoni.CrystalPower += cDouble4;
			App.State.ShouldSubmitScore = shouldSubmitScore;
			App.State.GameSettings = gameSettings;
			App.State.GameSettings.AutoBuyCreations = false;
			App.State.Avatar = avatar;
			App.State.Ext.RandomValues = randomValues;
			App.State.Ext.CurrentRandomNumber = currentRandomNumber;
			using (List<Creation>.Enumerator enumerator4 = App.State.AllCreations.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					Creation creation = enumerator4.Current;
					Creation creation3 = allCreations.FirstOrDefault((Creation x) => x.TypeEnum == creation.TypeEnum);
					if (creation3 != null)
					{
						creation.GodToDefeat.IsDefeatedForFirstTime = creation3.GodToDefeat.IsDefeatedForFirstTime;
						creation.GodToDefeat.IsDefeatedPetChallenge = creation3.GodToDefeat.IsDefeatedPetChallenge;
						creation.NextAtCount = creation3.NextAtCount;
						creation.AutoBuy = creation3.AutoBuy;
					}
				}
			}
			using (List<Skill>.Enumerator enumerator5 = App.State.AllSkills.GetEnumerator())
			{
				while (enumerator5.MoveNext())
				{
					Skill element = enumerator5.Current;
					Skill skill = allSkills.FirstOrDefault((Skill x) => x.EnumValue == element.EnumValue);
					if (skill != null)
					{
						element.Extension.KeyPress = skill.Extension.KeyPress;
						element.Extension.UsageCount = skill.Extension.UsageCount;
					}
				}
			}
			using (List<Monument>.Enumerator enumerator6 = App.State.AllMonuments.GetEnumerator())
			{
				while (enumerator6.MoveNext())
				{
					Monument monument = enumerator6.Current;
					Monument monument2 = allMonuments.FirstOrDefault((Monument x) => x.TypeEnum == monument.TypeEnum);
					if (monument2 != null)
					{
						monument.StopAtString = monument2.StopAtString;
						monument.Upgrade.StopAtString = monument2.Upgrade.StopAtString;
					}
				}
			}
			using (List<GeneratorUpgrade>.Enumerator enumerator7 = App.State.Generator.Upgrades.GetEnumerator())
			{
				while (enumerator7.MoveNext())
				{
					GeneratorUpgrade divGenUpgrade = enumerator7.Current;
					GeneratorUpgrade generatorUpgrade = upgrades.FirstOrDefault((GeneratorUpgrade x) => x.type == divGenUpgrade.type);
					if (generatorUpgrade != null)
					{
						divGenUpgrade.StopAt = generatorUpgrade.StopAt;
					}
				}
			}
			using (List<Might>.Enumerator enumerator8 = App.State.AllMights.GetEnumerator())
			{
				while (enumerator8.MoveNext())
				{
					Might might = enumerator8.Current;
					Might might2 = allMights.FirstOrDefault((Might x) => x.TypeEnum == might.TypeEnum);
					if (might2 != null)
					{
						might.NextAt = might2.NextAt;
					}
				}
			}
			App.State.Ext.PetPowerMultiCampaigns = ext.PetPowerMultiCampaigns;
			App.State.Ext.PetPowerMultiGods = ext.PetPowerMultiGods;
			App.State.Ext.PetStones = ext.PetStones;
			App.State.Ext.AllCampaigns = ext.AllCampaigns;
			foreach (PetCampaign current4 in App.State.Ext.AllCampaigns)
			{
				current4.Cancel();
			}
			App.State.AvatarName = avatarName;
			App.State.HomePlanet.IsCreated = homePlanet.IsCreated;
			App.State.HomePlanet.UpgradeLevel = homePlanet.UpgradeLevel;
			App.State.HomePlanet.UltimateBeings = UltimateBeing.Initial;
			App.State.HomePlanet.UltimateBeingsV2 = UltimateBeingV2.Initial;
			App.State.HomePlanet.UpgradeLevelArtyChallenge = homePlanet.UpgradeLevelArtyChallenge;
			if (App.State.Statistic.UltimateBaalChallengesFinished > 0 || App.State.Statistic.ArtyChallengesFinished > 0)
			{
				App.State.HomePlanet.UltimateBeingsV2[0].IsAvailable = true;
				App.State.HomePlanet.UltimateBeingsV2[0].TimesDefeated = homePlanet.UltimateBeingsV2[0].TimesDefeated;
				App.State.HomePlanet.UltimateBeingsV2[1].TimesDefeated = homePlanet.UltimateBeingsV2[1].TimesDefeated;
				App.State.HomePlanet.UltimateBeingsV2[2].TimesDefeated = homePlanet.UltimateBeingsV2[2].TimesDefeated;
				App.State.HomePlanet.UltimateBeingsV2[3].TimesDefeated = homePlanet.UltimateBeingsV2[3].TimesDefeated;
				App.State.HomePlanet.UltimateBeingsV2[4].TimesDefeated = homePlanet.UltimateBeingsV2[4].TimesDefeated;
			}
			App.State.HomePlanet.InitUBMultipliers();
			App.State.HomePlanet.TotalGainedGodPower = homePlanet.TotalGainedGodPower;
			using (List<UltimateBeing>.Enumerator enumerator10 = App.State.HomePlanet.UltimateBeings.GetEnumerator())
			{
				while (enumerator10.MoveNext())
				{
					UltimateBeing being = enumerator10.Current;
					UltimateBeing ultimateBeing = homePlanet.UltimateBeings.FirstOrDefault((UltimateBeing x) => x.Tier == being.Tier);
					being.IsAvailable = ultimateBeing.IsAvailable;
					being.ComeBack();
					being.HPPercent = 0.0;
				}
			}
			App.State.GameSettings.AutoBuyCreations = false;
			App.State.GameSettings.AutoBuyCreationsForMonumentsBeforeRebirth = App.State.GameSettings.AutoBuyCreationsForMonuments;
			App.State.GameSettings.AutoBuyCreationsForMonuments = false;
			foreach (UltimateBeingV2 current5 in App.State.HomePlanet.UltimateBeingsV2)
			{
				current5.IsDefeated = false;
			}
			App.State.Ext.AllPets = allPets;
			if (App.State.PremiumBoni.PetFoodAfterRebirth)
			{
				App.State.Ext.PunyFood = ext.PunyFood;
				App.State.Ext.MightyFood = ext.MightyFood;
				App.State.Ext.StrongFood = ext.StrongFood;
			}
			App.State.Ext.Chocolate = ext.Chocolate;
			App.State.Statistic.CalculateTotalPetGrowth(App.State.Ext.AllPets);
			App.SaveGameState();
			Creation creation2 = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Shadow_clone);
			App.State.GameSettings.LastCreation = creation2;
			if (creation2 != null)
			{
				creation2.IsActive = true;
			}
			if (App.State.Statistic.HasStartedUltimateBaalChallenge && App.State.Statistic.CountRebirthsInUBC)
			{
				Statistic expr_133F = App.State.Statistic;
				expr_133F.RebirthsAfterUBC = ++expr_133F.RebirthsAfterUBC;
			}
			if (App.State.Statistic.HasStartedArtyChallenge)
			{
				Statistic expr_136D = App.State.Statistic;
				expr_136D.RebirthsAfterUAC = ++expr_136D.RebirthsAfterUAC;
				if (App.State.Statistic.RebirthsAfterUAC > 5)
				{
					App.State.Statistic.HasStartedArtyChallenge = false;
					App.State.Statistic.RebirthsAfterUAC = 0;
					App.State.Statistic.TimeAfterUACStarted = 0;
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
					foreach (UltimateBeing current6 in App.State.HomePlanet.UltimateBeings)
					{
						if (current6.Tier <= App.State.HomePlanet.UpgradeLevel)
						{
							current6.IsAvailable = true;
						}
					}
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
					App.State.PremiumBoni.CheckIfGPIsAdjusted();
					App.SaveGameState();
					GuiBase.ShowToast("Your Challenge failed! You received back the God Power and God Power upgrades you had before the challenge.");
				}
			}
			Leaderboards.SubmitStat(LeaderBoardType.Rebirths, App.State.Statistic.TotalRebirths.ToInt(), false);
			SpecialFightUi.SortSkills();
			TBSUi.Reset();
			PlanetUi.Instance.Reset();
			if (!App.State.Statistic.HasStartedUltimateBaalChallenge && !App.State.Statistic.HasStartedArtyChallenge && App.State.Statistic.HighestGodDefeated > 3 && !App.State.IsSocialDialogShown)
			{
				MainUi.ShowSocialDialog = true;
			}
			MainUi.Instance.Init(true);
		}
	}
}
