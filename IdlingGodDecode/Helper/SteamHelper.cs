using Assets.Scripts.Data;
using Steamworks;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Helper
{
	internal class SteamHelper : MonoBehaviour
	{
		private class Achievement_t
		{
			public SteamAndroidAchievement m_eAchievementID;

			public string m_strName;

			public string m_strDescription;

			public bool m_bAchieved;

			public Achievement_t(SteamAndroidAchievement achievementID, string name, string desc)
			{
				this.m_eAchievementID = achievementID;
				this.m_strName = name;
				this.m_strDescription = desc;
				this.m_bAchieved = false;
			}
		}

		private static SteamHelper.Achievement_t[] m_Achievements = new SteamHelper.Achievement_t[]
		{
			new SteamHelper.Achievement_t(SteamAndroidAchievement.LightCreator, "Light Creator", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.FirstMonument, "First Monument", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.BaalSlayer, "Baal Slayer", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.UniverseCreator, "Creator of the Universe", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.BestBurger, "The best burger", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.QueenSlayer, "Queen Slayer", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.SlayerOfCreator, "Slayer of the creator?", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.DivinestBeing, "The divinest Being", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.EternalRebirther, "Eternal Rebirther", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.PetLover, "Pet Lover", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.MasterOfTBS, "Master of TBS", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.SlayerOfUltimate, "Slayer of the Ultimate", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.ITRTG, "ITRTG!", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.PlanetEaterV2, "Planet Eater Returns! And is defeated!", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.ITRTGV2, "ITRTG V2!", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.UUC, "Ultimate Universe Challenge complete", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.TAC, "All Achievements Challenge complete", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.UBC, "Ultimate Baal Challenge complete", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.UAC, "Ultimate Arty Challenge complete", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.OnekC, "1k Challenge complete", string.Empty),
			new SteamHelper.Achievement_t(SteamAndroidAchievement.DRC, "Double Rebirth Challenge complete", string.Empty)
		};

		private CGameID m_GameID;

		private bool m_bRequestedStats;

		protected Callback<UserStatsReceived_t> m_UserStatsReceived;

		protected Callback<UserAchievementStored_t> m_UserAchievementStored;

		protected Callback<MicroTxnAuthorizationResponse_t> m_MicroTxnAuthorizationResponse;

		private bool IsInitialized;

		private bool CheckForResult;

		private bool IsWaitingForResult;

		private bool IsWaitingForCurrency;

		private string BaseUrl = "https://www.shugasu.com/itrtg/";

		public static long TimerSinceStarted = 0L;

		public static bool IsQuitting = false;

		private bool CheckAgain = true;

		public static bool IsWaitingForPurchase
		{
			get;
			private set;
		}

		private void Start()
		{
			if (App.CurrentPlattform != Plattform.Steam)
			{
				return;
			}
			base.StartCoroutine("LookForTransactions");
		}

		private void OnEnable()
		{
			if (App.CurrentPlattform != Plattform.Steam)
			{
				return;
			}
			this.m_UserStatsReceived = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.OnUserStatsReceived));
			this.m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(new Callback<UserAchievementStored_t>.DispatchDelegate(this.OnAchievementStored));
			this.m_MicroTxnAuthorizationResponse = Callback<MicroTxnAuthorizationResponse_t>.Create(new Callback<MicroTxnAuthorizationResponse_t>.DispatchDelegate(this.OnPurchaseResult));
			this.m_bRequestedStats = false;
			this.Init();
		}

		private void Init()
		{
			if (this.IsInitialized || !SteamManager.Initialized || App.State == null)
			{
				return;
			}
			Log.Info("Steam Init!");
			this.m_GameID = new CGameID(SteamUtils.GetAppID());
			SteamHelper.InitSteamId();
			this.CheckForResult = true;
			App.InitialLeaderboardSubmit();
			this.IsInitialized = true;
		}

		public static void InitSteamId()
		{
			if (!SteamManager.Initialized || App.State == null)
			{
				Log.Info("Steam not initialized!");
				return;
			}
			bool flag = false;
			string text = SteamUser.GetSteamID().ToString();
			if (!string.IsNullOrEmpty(App.State.SteamId) && !App.State.SteamId.Equals(text))
			{
				flag = true;
			}
			App.State.SteamName = SteamFriends.GetPersonaName();
			App.State.SteamId = text;
			if (string.IsNullOrEmpty(App.State.AvatarName) || "Guest".Equals(App.State.AvatarName))
			{
				App.State.AvatarName = App.State.SteamName;
			}
			if (flag)
			{
				//UpdateStats.SaveToServer(UpdateStats.ServerSaveType.Silent);
			}
		}

		public static void PurchaseItem(Purchase id)
		{
			App.State.PremiumBoni.ItemIdToPurchase = id;
		}

		private void Reset()
		{
			SteamHelper.TimerSinceStarted = 0L;
			App.State.PremiumBoni.ItemIdToPurchase = Purchase.None;
			App.State.PremiumBoni.OrderIdLastSteamPurchase = 0uL;
			SteamHelper.IsWaitingForPurchase = false;
			this.IsWaitingForResult = false;
			this.IsWaitingForCurrency = false;
			this.CheckForResult = false;
		}

        //[DebuggerHidden]
        //private IEnumerator LookForTransactions()
        //{
        //    SteamHelper.<LookForTransactions>c__Iterator0 <LookForTransactions>c__Iterator = new SteamHelper.<LookForTransactions>c__Iterator0();
        //    <LookForTransactions>c__Iterator.$this = this;
        //    return <LookForTransactions>c__Iterator;
        //}

		private void Update()
		{
			if (App.CurrentPlattform != Plattform.Steam || !SteamManager.Initialized)
			{
				return;
			}
			this.Init();
			if (!this.m_bRequestedStats)
			{
				if (!SteamManager.Initialized)
				{
					this.m_bRequestedStats = true;
					return;
				}
				bool bRequestedStats = SteamUserStats.RequestCurrentStats();
				this.m_bRequestedStats = bRequestedStats;
			}
		}

		public static void CheckAchievements()
		{
			if (App.State == null || App.State.Ext.ImportedSaveFromKongToSteam || App.IsTimeTooOldForSteam || SteamHelper.IsQuitting || !SteamManager.Initialized)
			{
				return;
			}
			SteamHelper.Achievement_t[] achievements = SteamHelper.m_Achievements;
			for (int i = 0; i < achievements.Length; i++)
			{
				SteamHelper.Achievement_t achievement_t = achievements[i];
				if (!achievement_t.m_bAchieved)
				{
					switch (achievement_t.m_eAchievementID)
					{
					case SteamAndroidAchievement.LightCreator:
						if (App.State.Statistic.HighestGodDefeated > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.UniverseCreator:
						if (App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Universe).Count > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.FirstMonument:
						if (App.State.Statistic.MonumentsCreated > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.BaalSlayer:
						if (App.State.Statistic.HighestGodDefeated > 27)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.BestBurger:
						if (App.State.AllFights.FirstOrDefault((Fight x) => x.TypeEnum == Fight.FightType.big_food).Level > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.QueenSlayer:
						if (App.State.AllFights.FirstOrDefault((Fight x) => x.TypeEnum == Fight.FightType.monster_queen).Level > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.SlayerOfCreator:
						if (App.State.Statistic.CreatorBeaten)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.DivinestBeing:
					{
						bool flag = true;
						foreach (GeneratorUpgrade current in App.State.Generator.Upgrades)
						{
							if (current.Level < 33)
							{
								flag = false;
							}
						}
						if (flag)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					}
					case SteamAndroidAchievement.EternalRebirther:
						if (App.State.Statistic.TotalRebirths > 99)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.PetLover:
						if (App.State.Statistic.TotalPetGrowth > 30000)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.MasterOfTBS:
						if (App.State.Statistic.TBSScore > 1000)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.SlayerOfUltimate:
						if (App.State.HomePlanet.UltimateBeings.FirstOrDefault((UltimateBeing x) => x.Tier == 1).TimesDefeated > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.ITRTG:
						if (App.State.HomePlanet.UltimateBeings.FirstOrDefault((UltimateBeing x) => x.Tier == 5).TimesDefeated > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.PlanetEaterV2:
						if (App.State.HomePlanet.UltimateBeingsV2.FirstOrDefault((UltimateBeingV2 x) => x.Tier == 1).TimesDefeated > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.ITRTGV2:
						if (App.State.HomePlanet.UltimateBeingsV2.FirstOrDefault((UltimateBeingV2 x) => x.Tier == 5).TimesDefeated > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.UUC:
						if (App.State.Statistic.UniverseChallengesFinished > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.TAC:
						if (App.State.Statistic.AchievementChallengesFinished > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.UBC:
						if (App.State.Statistic.UltimateBaalChallengesFinished > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.UAC:
						if (App.State.Statistic.ArtyChallengesFinished > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.OnekC:
						if (App.State.Statistic.OnekChallengesFinished > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					case SteamAndroidAchievement.DRC:
						if (App.State.Statistic.DoubleRebirthChallengesFinished > 0)
						{
							SteamHelper.UnlockAchievement(achievement_t);
						}
						break;
					}
				}
			}
		}

		private void OnGUI()
		{
			if (App.CurrentPlattform != Plattform.Steam || !SteamManager.Initialized)
			{
				return;
			}
		}

		private static void UnlockAchievement(SteamHelper.Achievement_t achievement)
		{
			if (!App.State.Ext.ImportedSaveFromKongToSteam && SteamManager.Initialized)
			{
				achievement.m_bAchieved = true;
				SteamUserStats.SetAchievement(achievement.m_eAchievementID.ToString());
			}
		}

		private void OnUserStatsReceived(UserStatsReceived_t pCallback)
		{
			if (!SteamManager.Initialized)
			{
				return;
			}
			if ((ulong)this.m_GameID == pCallback.m_nGameID)
			{
				if (pCallback.m_eResult == EResult.k_EResultOK)
				{
					Log.Info("Received stats and achievements from Steam\n");
					SteamHelper.Achievement_t[] achievements = SteamHelper.m_Achievements;
					for (int i = 0; i < achievements.Length; i++)
					{
						SteamHelper.Achievement_t achievement_t = achievements[i];
						bool achievement = SteamUserStats.GetAchievement(achievement_t.m_eAchievementID.ToString(), out achievement_t.m_bAchieved);
						if (achievement)
						{
							achievement_t.m_strName = SteamUserStats.GetAchievementDisplayAttribute(achievement_t.m_eAchievementID.ToString(), "name");
							achievement_t.m_strDescription = SteamUserStats.GetAchievementDisplayAttribute(achievement_t.m_eAchievementID.ToString(), "desc");
						}
						else
						{
							Log.Error("SteamUserStats.GetAchievement failed for Achievement " + achievement_t.m_eAchievementID + "\nIs it registered in the Steam Partner site?");
						}
					}
				}
				else
				{
					Log.Info("RequestStats - failed, " + pCallback.m_eResult);
				}
			}
		}

		private void OnAchievementStored(UserAchievementStored_t pCallback)
		{
			if ((ulong)this.m_GameID == pCallback.m_nGameID)
			{
				if (pCallback.m_nMaxProgress == 0u)
				{
					Log.Info("Achievement '" + pCallback.m_rgchAchievementName + "' unlocked!");
				}
				else
				{
					Log.Info(string.Concat(new object[]
					{
						"Achievement '",
						pCallback.m_rgchAchievementName,
						"' progress callback, (",
						pCallback.m_nCurProgress,
						",",
						pCallback.m_nMaxProgress,
						")"
					}));
				}
			}
		}

		private void OnPurchaseResult(MicroTxnAuthorizationResponse_t pCallback)
		{
			this.CheckForResult = true;
		}
	}
}
