using Assets.Scripts.Data;
//using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using Assets.Scripts.Save;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class UpdateStats : MonoBehaviour
	{
		public enum ServerSaveType
		{
			None,
			UserRequest,
			Automatic,
			UBChallengeSave,
			UAChallengeSave,
			UAChallengeSave2,
			Silent,
			SaveToCompare
		}

		public static int PetLowestHunger = 0;

		public static bool DivGenEmpty = false;

		public static bool CheckOfflineForCalc = false;

		private static long autoSaveTimer = 0L;

		private static long halfHourTimer = 0L;

		private long timeOnline;

		private WaitForSeconds FiveSeconds = new WaitForSeconds(5f);

		private WaitForSeconds HalfHour = new WaitForSeconds(1800f);

		public static long TimeLimitForEvent = 1487300000L;

		public static long TimeLeftForEvent = 0L;

		private static UpdateStats.ServerSaveType ServerSave = UpdateStats.ServerSaveType.None;

		private static long TimeSinceLastOnlineRequestMS = 60000L;

		public static long LastServerSaveTime = -1L;

		private static bool InitSaveOnline = false;

		private static bool InitLoadOnline = false;

		public static bool OverwriteKongSave = false;

		public static bool OverwriteSteamSave = false;

		public static bool OverwriteAndroidSave = false;

		private string BaseUrl = "https://www.shugasu.com/itrtg/";

		private const string BaseUrlDst = "http://dstgames.de/games/itrtg/";

		public static bool GetKongSaveAfterConnecting = false;

		public static bool GetSteamSaveAfterConnecting = false;

		public static bool GetAndroidSaveAfterConnecting = false;

		private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private long timerStack;

		private long lastUpdateStats = UpdateStats.CurrentTimeMillis();

		private long timerStack500;

		private long lastUpdateStats500;

		private int calculateCount;

		private int oldSubmittedCount;

		public static long TimeSinceLastOnlineRequest
		{
			get
			{
				return UpdateStats.TimeSinceLastOnlineRequestMS / 1000L;
			}
		}

		public static long NextOnlineSaveTime
		{
			get
			{
				return 900L - UpdateStats.autoSaveTimer;
			}
		}

		private void Start()
		{
			if (App.CurrentPlattform == Plattform.Android)
			{
				base.InvokeRepeating("AutoSave", 30f, 30f);
			}
			else
			{
				base.InvokeRepeating("AutoSave", 300f, 300f);
			}
			base.InvokeRepeating("UpdateAllStats", 1.5f, 0.029f);
			base.InvokeRepeating("UpdateSlower", 3f, 0.5f);
			base.InvokeRepeating("UpdateAchievementCount", 30f, 300f);
			//base.StartCoroutine(this.UpdateServerTime());
			//base.StartCoroutine(this.LookForBlacklisted());
		}

        //[DebuggerHidden]
        //internal IEnumerator UpdateServerTime()
        //{
        //    UpdateStats.<UpdateServerTime>c__Iterator0 <UpdateServerTime>c__Iterator = new UpdateStats.<UpdateServerTime>c__Iterator0();
        //    <UpdateServerTime>c__Iterator.$this = this;
        //    return <UpdateServerTime>c__Iterator;
        //}

		public static void SaveToServer(UpdateStats.ServerSaveType type)
		{
			UpdateStats.ServerSave = type;
			UpdateStats.InitSaveOnline = true;
			UpdateStats.InitLoadOnline = false;
			UpdateStats.autoSaveTimer = 0L;
		}

		public static void LoadFromServer(UpdateStats.ServerSaveType type)
		{
			UpdateStats.ServerSave = type;
			UpdateStats.InitLoadOnline = true;
			UpdateStats.InitSaveOnline = false;
		}

        //[DebuggerHidden]
        //private IEnumerator SaveToSteam(string compressedState)
        //{
        //    UpdateStats.<SaveToSteam>c__Iterator1 <SaveToSteam>c__Iterator = new UpdateStats.<SaveToSteam>c__Iterator1();
        //    <SaveToSteam>c__Iterator.compressedState = compressedState;
        //    <SaveToSteam>c__Iterator.$this = this;
        //    return <SaveToSteam>c__Iterator;
        //}

        //[DebuggerHidden]
        //private IEnumerator SaveToKong(string compressedState)
        //{
        //    UpdateStats.<SaveToKong>c__Iterator2 <SaveToKong>c__Iterator = new UpdateStats.<SaveToKong>c__Iterator2();
        //    <SaveToKong>c__Iterator.compressedState = compressedState;
        //    <SaveToKong>c__Iterator.$this = this;
        //    return <SaveToKong>c__Iterator;
        //}

        //[DebuggerHidden]
        //private IEnumerator SaveToAndroid(string compressedState)
        //{
        //    UpdateStats.<SaveToAndroid>c__Iterator3 <SaveToAndroid>c__Iterator = new UpdateStats.<SaveToAndroid>c__Iterator3();
        //    <SaveToAndroid>c__Iterator.compressedState = compressedState;
        //    <SaveToAndroid>c__Iterator.$this = this;
        //    return <SaveToAndroid>c__Iterator;
        //}

        //[DebuggerHidden]
        //private IEnumerator CheckSaveResult(WWW www)
        //{
        //    UpdateStats.<CheckSaveResult>c__Iterator4 <CheckSaveResult>c__Iterator = new UpdateStats.<CheckSaveResult>c__Iterator4();
        //    <CheckSaveResult>c__Iterator.www = www;
        //    return <CheckSaveResult>c__Iterator;
        //}

        //[DebuggerHidden]
        //private IEnumerator SaveToServer()
        //{
        //    UpdateStats.<SaveToServer>c__Iterator5 <SaveToServer>c__Iterator = new UpdateStats.<SaveToServer>c__Iterator5();
        //    <SaveToServer>c__Iterator.$this = this;
        //    return <SaveToServer>c__Iterator;
        //}

        //[DebuggerHidden]
        //private IEnumerator LoadFromServer()
        //{
        //    UpdateStats.<LoadFromServer>c__Iterator6 <LoadFromServer>c__Iterator = new UpdateStats.<LoadFromServer>c__Iterator6();
        //    <LoadFromServer>c__Iterator.$this = this;
        //    return <LoadFromServer>c__Iterator;
        //}

        //[DebuggerHidden]
        //private IEnumerator CheckVersion()
        //{
        //    UpdateStats.<CheckVersion>c__Iterator7 <CheckVersion>c__Iterator = new UpdateStats.<CheckVersion>c__Iterator7();
        //    <CheckVersion>c__Iterator.$this = this;
        //    return <CheckVersion>c__Iterator;
        //}

        //private bool loadSave(WWW www)
        //{
        //    if (www.error != null)
        //    {
        //        UnityEngine.Debug.Log(www.error);
        //        GuiBase.ShowToast("Failed to load gamestate from server!\n" + www.error);
        //        return false;
        //    }
        //    string[] array = www.text.Split(new char[]
        //    {
        //        ';'
        //    });
        //    if (array.Length == 2)
        //    {
        //        long num = 0L;
        //        long.TryParse(array[0], out num);
        //        UnityEngine.Debug.Log("timeOffline:" + num);
        //        GameState gameState = Storage.FromCompressedString(array[1]);
        //        if (UpdateStats.ServerSave != UpdateStats.ServerSaveType.SaveToCompare)
        //        {
        //            App.OfflineStatsChecked = true;
        //            OfflineCalc.TimeOffline = num;
        //        }
        //        if (UpdateStats.GetKongSaveAfterConnecting || UpdateStats.OverwriteKongSave)
        //        {
        //            if (gameState.KongUserName.Equals(ConnectArea.KongNameInput))
        //            {
        //                if (UpdateStats.GetKongSaveAfterConnecting)
        //                {
        //                    UpdateStats.GetKongSaveAfterConnecting = false;
        //                    gameState.Ext.ImportedSaveFromKongToSteam = true;
        //                    gameState.SteamName = App.State.SteamName;
        //                    gameState.SteamId = App.State.SteamId;
        //                    gameState.AndroidName = App.State.AndroidName;
        //                    gameState.AndroidId = App.State.AndroidId;
        //                    App.State = gameState;
        //                    App.State.InitAchievementNames();
        //                    MainUi.Instance.Init(false);
        //                    App.SaveGameState();
        //                    GuiBase.ShowBigMessage("Please go to Kongregate now and load the Online save. This will connect both online saves.");
        //                }
        //                else
        //                {
        //                    App.State.KongUserId = gameState.KongUserId;
        //                    App.State.KongUserName = gameState.KongUserName;
        //                }
        //                UpdateStats.SaveToServer(UpdateStats.ServerSaveType.Automatic);
        //            }
        //            else
        //            {
        //                GuiBase.ShowToast("The Kongregate Name doesn't match with the Kongregate Id! Please make sure, both are correct.");
        //            }
        //        }
        //        else if (UpdateStats.GetSteamSaveAfterConnecting || UpdateStats.OverwriteSteamSave)
        //        {
        //            if (gameState.SteamName.Equals(ConnectArea.SteamNameInput))
        //            {
        //                if (UpdateStats.GetSteamSaveAfterConnecting)
        //                {
        //                    UpdateStats.GetSteamSaveAfterConnecting = false;
        //                    gameState.KongUserName = App.State.KongUserName;
        //                    gameState.KongUserId = App.State.KongUserId;
        //                    gameState.AndroidName = App.State.AndroidName;
        //                    gameState.AndroidId = App.State.AndroidId;
        //                    App.State = gameState;
        //                    App.State.InitAchievementNames();
        //                    MainUi.Instance.Init(false);
        //                    App.SaveGameState();
        //                    GuiBase.ShowBigMessage("Please go to Steam now and load the Online save. This will connect both online saves.");
        //                }
        //                else
        //                {
        //                    App.State.SteamId = gameState.SteamId;
        //                    App.State.SteamName = gameState.SteamName;
        //                }
        //                UpdateStats.SaveToServer(UpdateStats.ServerSaveType.Automatic);
        //            }
        //            else
        //            {
        //                GuiBase.ShowToast("The Steam Name doesn't match with the Steam Id! Please make sure, both are correct.");
        //            }
        //        }
        //        else if (UpdateStats.GetAndroidSaveAfterConnecting || UpdateStats.OverwriteAndroidSave)
        //        {
        //            string strA = ConnectArea.AndroidNameInput.Trim();
        //            string strB = gameState.AndroidName.Trim();
        //            if (string.Compare(strA, strB) == 0)
        //            {
        //                if (UpdateStats.GetAndroidSaveAfterConnecting)
        //                {
        //                    UpdateStats.GetAndroidSaveAfterConnecting = false;
        //                    gameState.KongUserName = App.State.KongUserName;
        //                    gameState.KongUserId = App.State.KongUserId;
        //                    gameState.SteamName = App.State.SteamName;
        //                    gameState.SteamId = App.State.SteamId;
        //                    App.State = gameState;
        //                    App.State.InitAchievementNames();
        //                    MainUi.Instance.Init(false);
        //                    App.SaveGameState();
        //                    GuiBase.ShowBigMessage("Please go to Android now and load the Online save. This will connect both online saves.");
        //                }
        //                else
        //                {
        //                    App.State.AndroidId = gameState.AndroidId;
        //                    App.State.AndroidName = gameState.AndroidName;
        //                }
        //                UpdateStats.SaveToServer(UpdateStats.ServerSaveType.Automatic);
        //            }
        //            else
        //            {
        //                GuiBase.ShowToast("The Android Name doesn't match with the Android Id! Please make sure, both are correct.");
        //            }
        //        }
        //        else
        //        {
        //            bool flag = true;
        //            if (UpdateStats.ServerSave == UpdateStats.ServerSaveType.SaveToCompare && App.State != null)
        //            {
        //                long num2 = App.State.Statistic.TimePlayed + App.State.Statistic.TimeOffline;
        //                long num3 = gameState.Statistic.TimePlayed + gameState.Statistic.TimeOffline;
        //                if (num2 < num3)
        //                {
        //                    Log.Info("Current save is older than online save, online save is used.");
        //                }
        //                else
        //                {
        //                    flag = false;
        //                }
        //            }
        //            if (flag)
        //            {
        //                OfflineCalc.TimeOffline = num;
        //                App.State = gameState;
        //                App.State.InitAchievementNames();
        //                MainUi.Instance.Init(false);
        //                App.OfflineStatsChecked = false;
        //                App.SaveGameState();
        //            }
        //        }
        //        return true;
        //    }
        //    if (UpdateStats.GetKongSaveAfterConnecting)
        //    {
        //        GuiBase.ShowToast("Failed to connect to your Kongregate save. Please make sure, your Convert Id is right.");
        //    }
        //    return false;
        //}

        //[DebuggerHidden]
        //internal IEnumerator LookForBlacklisted()
        //{
        //    UpdateStats.<LookForBlacklisted>c__Iterator8 <LookForBlacklisted>c__Iterator = new UpdateStats.<LookForBlacklisted>c__Iterator8();
        //    <LookForBlacklisted>c__Iterator.$this = this;
        //    return <LookForBlacklisted>c__Iterator;
        //}

        //private void OnApplicationQuit()
        //{
        //    SteamHelper.IsQuitting = true;
        //    ChatRoom.Disconnect();
        //    if (App.State != null)
        //    {
        //        App.State.TimeStampGameClosed = App.ServerTime;
        //        UnityEngine.Debug.Log("servertime quit: " + App.ServerTime);
        //        App.SaveGameState();
        //    }
        //}

		private void OnDisable()
		{
			try
			{
				base.StopAllCoroutines();
			}
			catch (Exception)
			{
				UnityEngine.Debug.Log("Crash in StopAllCoroutines");
			}
		}

		private void AutoSave()
		{
			if (App.OfflineStatsChecked)
			{
				App.State.TimeStampGameClosed = App.ServerTime;
			}
			App.SaveGameState();
		}

		public static long CurrentTimeMillis()
		{
			return (long)(DateTime.UtcNow - UpdateStats.Jan1st1970).TotalMilliseconds;
		}

		private void UpdateAllStats()
		{
			long num = UpdateStats.CurrentTimeMillis() - this.lastUpdateStats;
			this.timerStack += num;
			this.lastUpdateStats = UpdateStats.CurrentTimeMillis();
			if (this.timerStack < 30L || OfflineCalc.IsCalculating)
			{
				return;
			}
			num = 30L;
			this.timerStack -= num;
			if (App.State == null)
			{
				return;
			}
			if (UpdateStats.TimeLeftForEvent > 0L)
			{
				App.State.Statistic.TimeUntilNextChocolate -= num;
				if (App.State.Statistic.TimeUntilNextChocolate < 0L)
				{
					App.State.Statistic.TimeUntilNextChocolate = 1200000L;
					State2 expr_B4 = App.State.Ext;
					expr_B4.Chocolate = ++expr_B4.Chocolate;
					GuiBase.ShowToast("You received 1 chocolate!");
				}
			}
			App.State.Ext.AfkGame.Update(num);
			foreach (Training current in App.State.AllTrainings)
			{
				current.UpdateDuration(num);
			}
			foreach (Skill current2 in App.State.AllSkills)
			{
				current2.UpdateDuration(num);
			}
			foreach (Fight current3 in App.State.AllFights)
			{
				current3.UpdateDuration(num);
			}
			foreach (Creation current4 in App.State.AllCreations)
			{
				current4.UpdateDuration(num);
			}
			foreach (Might current5 in App.State.AllMights)
			{
				current5.UpdateDuration(num);
			}
			App.State.RecoverHealth(num);
			App.State.PremiumBoni.UpdateDuration(num);
			App.State.Generator.UpdateDuration(num);
			App.State.PrinnyBaal.Fight(num);
			App.State.Battle.UpdateData(num);
			SpecialFightUi.UpdateAutoMode(num);
			App.State.CreatingSpeedBoniDuration -= num;
			if (App.State.CreatingSpeedBoniDuration < 0L)
			{
				App.State.CreatingSpeedBoniDuration = 0L;
			}
			if (!App.State.PremiumBoni.TotalMightIsUnlocked)
			{
				App.State.PremiumBoni.TotalMightIsUnlocked = (App.State.AllTrainings[App.State.AllTrainings.Count - 1].IsAvailable && App.State.AllSkills[App.State.AllSkills.Count - 1].IsAvailable);
				if (App.State.PremiumBoni.TotalMightIsUnlocked && !App.State.Statistic.HasStartedArtyChallenge && !App.State.Statistic.HasStartedUltimateBaalChallenge)
				{
					foreach (Might current6 in App.State.AllMights)
					{
						CDouble cDouble = App.State.Statistic.DoubleRebirthChallengesFinished;
						if (cDouble > 50)
						{
							cDouble = 50;
						}
						current6.Level += cDouble;
						App.State.PremiumBoni.TotalMight += (long)cDouble.ToInt();
					}
				}
			}
			foreach (Monument current7 in App.State.AllMonuments)
			{
				current7.UpdateDuration(num);
				if (App.State.Statistic.HasStartedBlackHoleChallenge && current7.TypeEnum == Monument.MonumentType.black_hole)
				{
					MonumentUpgrade upgrade = current7.Upgrade;
					if (current7.Level > 0 && upgrade.Level > 0)
					{
						App.State.Statistic.HasStartedBlackHoleChallenge = false;
						if (App.State.Statistic.FastestBHCallenge <= 0 || App.State.Statistic.FastestBHCallenge > App.State.Statistic.TimeAfterBHCStarted)
						{
							App.State.Statistic.FastestBHCallenge = App.State.Statistic.TimeAfterBHCStarted;
							Leaderboards.SubmitStat(LeaderBoardType.FastestBHCallenge, (int)(App.State.Statistic.FastestBHCallenge.ToLong() / 1000L), false);
						}
						App.State.Statistic.TimeAfterBHCStarted = 0;
						Statistic expr_542 = App.State.Statistic;
						expr_542.BlackHoleChallengesFinished = ++expr_542.BlackHoleChallengesFinished;
						App.SaveGameState();
						GuiBase.ShowToast("You finished your black hole challenge!");
					}
				}
			}
			foreach (Pet current8 in App.State.Ext.AllPets)
			{
				current8.UpdateDuration(num);
			}
			App.State.HomePlanet.UpdateDuration(num);
			if (HeroImage.ShouldInitRessources)
			{
				//base.StartCoroutine(this.InitAvatarImages());
			}
		}

        //[DebuggerHidden]
        //internal IEnumerator InitAvatarImages()
        //{
        //    return new UpdateStats.<InitAvatarImages>c__Iterator9();
        //}

        //private void UpdateSlower()
        //{
        //    if (App.State == null || OfflineCalc.IsCalculating)
        //    {
        //        return;
        //    }
        //    if (this.lastUpdateStats500 == 0L)
        //    {
        //        this.lastUpdateStats500 = UpdateStats.CurrentTimeMillis();
        //    }
        //    long num = UpdateStats.CurrentTimeMillis() - this.lastUpdateStats500;
        //    this.timerStack500 += num;
        //    this.lastUpdateStats500 = UpdateStats.CurrentTimeMillis();
        //    if (this.timerStack500 < 500L)
        //    {
        //        return;
        //    }
        //    if (App.CurrentPlattform == Plattform.Steam)
        //    {
        //        Leaderboards.Instance.HandleScoreUploads();
        //        SteamHelper.CheckAchievements();
        //    }
        //    num = 500L;
        //    UpdateStats.TimeSinceLastOnlineRequestMS += num;
        //    this.timerStack500 -= num;
        //    InfoArea.TimeSincePressed += (int)num;
        //    this.calculateCount++;
        //    if (UpdateStats.InitSaveOnline)
        //    {
        //        UpdateStats.InitSaveOnline = false;
        //        base.StartCoroutine(this.SaveToServer());
        //    }
        //    else if (UpdateStats.InitLoadOnline)
        //    {
        //        UpdateStats.InitLoadOnline = false;
        //        base.StartCoroutine(this.LoadFromServer());
        //    }
        //    foreach (PetCampaign current in App.State.Ext.AllCampaigns)
        //    {
        //        current.UpdateDuration(num);
        //    }
        //    MainUi.CheckAlertButton(App.State);
        //    App.State.UpdateAllInfoTexts();
        //    App.State.CheckIfAchievementChallengeFinished();
        //    Creation.UpdateDurationMulti(App.State);
        //    App.State.CheckMightBoni();
        //    App.State.Generator.UpdateCloneFillSpeedText();
        //    App.UpdateResolutionInfo();
        //    App.State.Multiplier.UpdatePetMultis(App.State);
        //    App.State.PremiumBoni.CheckGPBoniValues();
        //    App.State.PremiumBoni.CheckCrystalBonus(App.State);
        //    App.State.Ext.Factory.UpdateDuration(num, App.State, false);
        //    App.State.CheckMight();
        //    App.State.CheckForAchievements();
        //    if (App.State.IsCrystalFactoryAvailable && App.State.GameSettings.AutofillDefenders && App.State.GameSettings.MaxDefenderClones > 0 && App.State.Ext.Factory.DefenderClones.ToInt() != App.State.GameSettings.MaxDefenderClones)
        //    {
        //        App.State.Ext.Factory.RemoveCloneCount(App.State.Ext.Factory.DefenderClones);
        //        CDouble cDouble = App.State.GetAvailableClones(false);
        //        if (cDouble > App.State.GameSettings.MaxDefenderClones)
        //        {
        //            cDouble = App.State.GameSettings.MaxDefenderClones;
        //        }
        //        App.State.Ext.Factory.AddCloneCount(cDouble);
        //    }
        //    if (this.calculateCount > 100)
        //    {
        //        App.State.RecalculateAchievementMultis();
        //        this.calculateCount = 0;
        //    }
        //    UpdateStats.DivGenEmpty = (App.State.Generator.IsBuilt && App.State.Generator.FilledCapacity <= 0);
        //    App.State.CheckForCheats();
        //    UpdateStats.PetLowestHunger = 100;
        //    foreach (Pet current2 in App.State.Ext.AllPets)
        //    {
        //        if (current2.IsUnlocked)
        //        {
        //            current2.UpdateFeedTimerMultis();
        //            if (!current2.IsInCampaign)
        //            {
        //                if (current2.FeedTimer < 4320000L)
        //                {
        //                    UpdateStats.PetLowestHunger = 10;
        //                }
        //                else if (current2.FeedTimer < 21600000L)
        //                {
        //                    UpdateStats.PetLowestHunger = 50;
        //                }
        //                else if (current2.FeedTimer < 32400000L)
        //                {
        //                    UpdateStats.PetLowestHunger = 75;
        //                }
        //            }
        //        }
        //    }
        //    App.State.PremiumBoni.TimeForNextLuckyDraw -= num;
        //    if (App.State.PremiumBoni.TimeForNextDailyPack >= 0L)
        //    {
        //        App.State.PremiumBoni.TimeForNextDailyPack -= num;
        //    }
        //    App.State.Statistic.TimePlayed += num;
        //    App.State.Statistic.TimePlayedSinceRebirth += num;
        //    App.State.Ext.TimeSinceSteam += num;
        //    if (App.State.Statistic.HasStartedDoubleRebirthChallenge)
        //    {
        //        App.State.Statistic.TimeAfterDRCStarted += num;
        //    }
        //    if (App.State.Statistic.HasStartedUltimateBaalChallenge)
        //    {
        //        App.State.Statistic.TimeAfterUBCStarted += num;
        //    }
        //    if (App.State.Statistic.HasStartedArtyChallenge)
        //    {
        //        App.State.Statistic.TimeAfterUACStarted += num;
        //    }
        //    if (App.State.Statistic.HasStartedUniverseChallenge)
        //    {
        //        App.State.Statistic.TimeAfterUUCStarted += num;
        //    }
        //    if (App.State.Statistic.HasStarted1kChallenge)
        //    {
        //        App.State.Statistic.TimeAfter1KCStarted += num;
        //    }
        //    if (App.State.Statistic.HasStartedNoRbChallenge)
        //    {
        //        App.State.Statistic.TimeAfterNoRbStarted += num;
        //    }
        //    if (App.State.Statistic.HasStartedBlackHoleChallenge)
        //    {
        //        App.State.Statistic.TimeAfterBHCStarted += num;
        //    }
        //    if (App.State.Statistic.HasStartedUltimatePetChallenge)
        //    {
        //        App.State.Statistic.TimeAfterUPCStarted += num;
        //    }
        //    foreach (Creation current3 in App.State.AllCreations)
        //    {
        //        current3.count.Round();
        //    }
        //    Monument monument = App.State.AllMonuments.FirstOrDefault((Monument x) => x.TypeEnum == Monument.MonumentType.black_hole);
        //    if (monument != null && monument.Level > 0)
        //    {
        //        monument.ShouldUpdateText = true;
        //        App.State.Statistic.BlackHoleGPTimer += num;
        //        if (App.State.Statistic.BlackHoleGPTimer > 3600000)
        //        {
        //            App.State.Statistic.BlackHoleGPTimer = 0;
        //            int num2 = (monument.Level * 25).ToInt();
        //            if (UnityEngine.Random.Range(0, 100) < num2)
        //            {
        //                Statistic expr_770 = App.State.Statistic;
        //                expr_770.GPFromBlackHole = ++expr_770.GPFromBlackHole;
        //                App.State.PremiumBoni.GodPower++;
        //                App.State.PremiumBoni.CheckIfGPIsAdjusted();
        //            }
        //            if (App.State.Statistic.BlackHoleChallengesFinished > 0)
        //            {
        //                int num3 = (App.State.Statistic.BlackHoleChallengesFinished * 5).ToInt();
        //                if (monument.Level < App.State.Statistic.BlackHoleChallengesFinished)
        //                {
        //                    num3 = (monument.Level * 5).ToInt();
        //                }
        //                if (num3 > 100)
        //                {
        //                    Statistic expr_832 = App.State.Statistic;
        //                    expr_832.GPFromBlackHole = ++expr_832.GPFromBlackHole;
        //                    App.State.PremiumBoni.GodPower++;
        //                    num3 -= 100;
        //                }
        //                if (UnityEngine.Random.Range(0, 100) < num3)
        //                {
        //                    Statistic expr_879 = App.State.Statistic;
        //                    expr_879.GPFromBlackHole = ++expr_879.GPFromBlackHole;
        //                    App.State.PremiumBoni.GodPower++;
        //                }
        //                App.State.PremiumBoni.CheckIfGPIsAdjusted();
        //            }
        //        }
        //    }
        //    if (App.State.HomePlanet.UpgradeLevel > 50)
        //    {
        //        App.State.HomePlanet.UpgradeLevel = 50;
        //    }
        //}

		private void UpdateAchievementCount()
		{
			if (App.State == null)
			{
				return;
			}
			if (this.oldSubmittedCount < App.State.Statistic.TotalAchievements)
			{
				this.oldSubmittedCount = App.State.Statistic.TotalAchievements.ToInt();
				Leaderboards.SubmitStat(LeaderBoardType.Achievements, App.State.Statistic.TotalAchievements.ToInt(), false);
				Leaderboards.SubmitStat(LeaderBoardType.ClonesCreated, (App.State.Statistic.TotalShadowClonesCreated / 1000).ToInt(), false);
				Leaderboards.SubmitStat(LeaderBoardType.AfkyClonesKilled, (App.State.Statistic.AfkyClonesKilled / 1000000).ToInt(), false);
			}
		}
	}
}
