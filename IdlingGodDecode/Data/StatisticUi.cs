using Assets.Scripts.Data;
using Assets.Scripts.Helper;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class StatisticUi : GuiBase
	{
		private static StatisticUi Instance = new StatisticUi();

		private Vector2 scrollPosition = Vector2.zero;

		private int scrollHeight;

		public static void Show()
		{
			StatisticUi.Instance.show();
		}

		private void show()
		{
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = GuiBase.FontSize(16);
			GUI.BeginGroup(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)));
			GUI.Box(new Rect(GuiBase.Width(0f), GuiBase.Height(0f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			this.scrollPosition = GuiBase.TouchScrollView(new Rect(GuiBase.Width(0f), GuiBase.Height(15f), GuiBase.Width(640f), GuiBase.Height(460f)), this.scrollPosition, new Rect(0f, GuiBase.Height(15f), GuiBase.Width(620f), GuiBase.Height((float)this.scrollHeight)));
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(10f), GuiBase.Width(600f), GuiBase.Height(50f)), new GUIContent("Some stats of your game. All multis will apply when rebirthing and can't be higher than your god multiplier."));
			style.fontStyle = FontStyle.Normal;
			int num = 70;
			if (UpdateStats.TimeLeftForEvent > 0L)
			{
				num = this.AddLine(num, "Time left until next chocolate", Conv.MsToGuiText(App.State.Statistic.TimeUntilNextChocolate, true), "This event will end in: " + Conv.MsToGuiText(UpdateStats.TimeLeftForEvent, false), 30);
			}
			num = this.AddLine(num, "Time played since rebirth", Conv.MsToGuiText(App.State.Statistic.TimePlayedSinceRebirth, true), "You need 1 hour to reach 100% rebirth multiplier. After that every 30 minutes played will increase your total rebirth multiplier by 1%. This time will count both offline and online time.", 30);
			num = this.AddLine(num, "Time online", Conv.MsToGuiText(App.State.Statistic.TimePlayed, true) + this.Multiboni(App.State.Statistic.MultiTimePlayed), "The total time the game was open. Every 10 minutes will increase your statistic multi by 1.", 30);
			num = this.AddLine(num, "Time offline", Conv.MsToGuiText(App.State.Statistic.TimeOffline, false) + this.Multiboni(App.State.Statistic.MultiTimeOffline), "The time you were offline after you started to play this game. Every 40 minutes will increase your statistic multi by 1. To prevent cheating, the time only works if you are online before you close the game.", 30);
			num += 20;
			num = this.AddLine(num, "Rebirths", App.State.Statistic.TotalRebirths.Serialize() + this.Multiboni(App.State.Statistic.MultiTotalRebirths), "The number of times you pushed the Rebirth button. Each rebirth increases your statistic multi by 1000.", 30);
			num = this.AddLine(num, "Training levels", App.State.Statistic.TotalTrainingLevels.ToGuiText(true) + this.Multiboni(App.State.Statistic.MultiTotalTrainingLevels), "The total number of levels you gained by trainings. Every 10,000 levels increases your statistic multi by 1.", 30);
			num = this.AddLine(num, "Skill levels", App.State.Statistic.TotalSkillLevels.ToGuiText(true) + this.Multiboni(App.State.Statistic.MultiTotalSkillLevels), "The total number of levels you gained by learning skills. Every 10,000 levels increases your statistic multi by 1.", 30);
			num = this.AddLine(num, "Enemies defeated", App.State.Statistic.TotalEnemiesDefeated.ToGuiText(true) + this.Multiboni(App.State.Statistic.MultiTotalEnemiesDefeated), "The total number of enemies you defeated. Every 200,000 enemies you defeat increases your statistic multi by 1.", 30);
			num = this.AddLine(num, "Creations", App.State.Statistic.TotalCreations.ToGuiText(true) + this.Multiboni(App.State.Statistic.MultiTotalCreations), "The total number of things you created. Every 1000 creations increases your statistic multi by 1.", 30);
			num = this.AddLine(num, "Shadow clones created", App.State.Statistic.TotalShadowClonesCreated.ToGuiText(true) + this.Multiboni(App.State.Statistic.MultiTotalShadowClonesCreated), "All the shadow clones you created. Every 1000 clones created reduces their attack divider by 1 (after rebirth, min divider 50).  Every 2000 increases your statistic multi by 1.", 30);
			num = this.AddLine(num, "Shadow clones died", App.State.Statistic.TotalShadowClonesDied.ToGuiText(true) + this.Multiboni(App.State.Statistic.MultiTotalShadowClonesDied), "All shadow clones who died fighting enemies. Every 500 clones who died reduces their defense and hp divider by 1 (after rebirth, min divider 50).  Every 500 increases your statistic multi by 1.", 30);
			num += 20;
			num = this.AddLine(num, "Achievements", App.State.Statistic.TotalAchievements.GuiText + this.Multiboni(App.State.Statistic.MultiTotalAchievements), "The total number of achievements you got. Each achievement will increase your statistic multi by 5.", 30);
			num = this.AddLine(num, "Gods defeated", App.State.Statistic.TotalGodsDefeated.ToGuiText(true) + this.Multiboni(App.State.Statistic.MultiTotalGodsDefeated), "The total number of gods you defeated. Each defeated god increases your statistic multi by 50.", 30);
			num = this.AddLine(num, "Divinity used", App.State.Statistic.TotalMoneySpent.ToGuiText(true) + this.Multiboni(App.State.Statistic.MultiTotalMoneySpent), "The total divinity you traded in for creations. The more you trade in, the higher your statistic multi.", 30);
			num = this.AddLine(num, "Upgrades", App.State.Statistic.TotalUpgrades.GuiText + this.Multiboni(App.State.Statistic.MultiTotalUpgrades), "The number of times you did upgrade a monument. Each upgrade increases your statistic multi by 5. Upgrading is available after defeating Freya.", 30);
			string str = string.Empty;
			if (App.State.Statistic.HighestGodDefeated > 28)
			{
				if (App.State.Statistic.HighestGodDefeated == 29)
				{
					str = "P. Baal";
				}
				else
				{
					str = "P. Baal v " + (App.State.Statistic.HighestGodDefeated - 28);
				}
			}
			else
			{
				str = EnumName.Name((God.GodType)App.State.Statistic.HighestGodDefeated.ToInt());
			}
			string str2 = string.Empty;
			if (!string.IsNullOrEmpty(App.State.Statistic.GodDefeatedBeforeRebirth))
			{
				str2 = "\nOn your last rebirth, you defeated " + App.State.Statistic.GodDefeatedBeforeRebirth;
			}
			num = this.AddLine(num, "Strongest god defeated", str + this.Multiboni(App.State.Statistic.MultiHighestGodDefeated), "The strongest god you have defeated. Each higher tier increases your statistic multi by 50 (5000 after Baal)." + str2, 30);
			num += 20;
			num = this.AddLine(num, "Ultimate Beings defeated", App.State.Statistic.UBsDefeated + this.Multiboni(App.State.Statistic.MultiUBsDefeated), "How many Ultimate Beings you have defeated. Each one increases your statistic multi by 40.", 30);
			num = this.AddLine(num, "Total Powersurge levels", App.State.Statistic.TotalPowersurge.ToGuiText(true) + this.Multiboni(App.State.Statistic.MultiTotalPowersurge), "How many times you upgraded a level in Powersurge. Each level increases your statistic multi by 1.", 30);
			num = this.AddLine(num, "Most shadow clones defeated", App.State.Statistic.MostDefeatedShadowClones + this.Multiboni(App.State.Statistic.MultiMostClonesDefeated), "The most shadow clones you have defeated in one special fight. Increases your statistic multi by 500 x defeated count x defeated count.", 30);
			num = this.AddLine(num, "TBS Score", App.State.Statistic.TBSScore + this.Multiboni(App.State.Statistic.MultiTBSScore), "Your score in the TBS - Game. Increases your statistic multi by 100 x score.", 30);
			if (App.CurrentPlattform != Plattform.Android)
			{
				num = this.AddLine(num, "Highscore Godly Shoot", App.State.Statistic.GodlyShootScore.ToInt() + this.Multiboni(App.State.Statistic.MultiGodlyShootScore), "The highest score you reached in 'Godly Shoot'. Each one increases your statistic multi by 500.", 30);
				num = this.AddLine(num, "Highscore Godly Shoot Boss", App.State.Statistic.GodlyShootScoreBoss.ToInt() + this.Multiboni(App.State.Statistic.MultiGodlyShootScoreBoss), "The highest score you reached in 'Godly Shoot' in Boss mode. Each one increases your statistic multi by 250.", 30);
			}
			num = this.AddLine(num, "AFK Clones killed", App.State.Statistic.AfkyClonesKilled.GuiText + this.Multiboni(App.State.Statistic.MultiAfkyClonesKilled), "The total number of clones your afky God has killed. Increases your statistic multi by 1 every 1000. Capped at 25 million.", 30);
			num = this.AddLine(num, "Highest Afky God Power", App.State.Statistic.AfkyGodPower.GuiText + this.Multiboni(App.State.Statistic.MultiAfkyGodPower), "The highest Power your Afky God ever had. Increases your statistic multi by 1 every 10. Capped at 25 million.", 30);
			num += 20;
			CDouble cDouble = App.State.PremiumBoni.TotalMight;
			num = this.AddLine(num, "Total Might gained", cDouble.GuiText + this.Multiboni(App.State.Statistic.MultiTotalMight), "How many times you upgraded a level in the Might-Tab. Each one increases your statistic multi by 10.", 30);
			CDouble cDouble2 = App.State.PremiumBoni.TotalGodPowerSpent;
			num = this.AddLine(num, "Total God Power spent", cDouble2.GuiText + this.Multiboni(App.State.Statistic.MultiTotalGPsUsed), "How many God Power you have spent. Each one increases your statistic multi by 20.\nGP spent for permanent upgrades: " + App.State.PremiumBoni.PermanentGPSpent, 30);
			num = this.AddLine(num, "Total Pet Growth", App.State.Statistic.TotalPetGrowth.GuiText + this.Multiboni(App.State.Statistic.MultiPetGrowth), "The total combined growth of all the pets you own.", 30);
			num += 20;
			num = this.AddLine(num, "All Achievements Challenges", App.State.Statistic.AchievementChallengesFinished + this.Multiboni(App.State.Statistic.MultiAchievementChallenge), "How often you finished an All Achievements Challenge. You will receive 750,000 statistic multi and your achievements will increase your multipliers by 1% more for each one. \nIt will also reduce the levels for trainings and fights you need to reach an achievement by 1% (Maxed at 50%).", 30);
			num = this.AddLine(num, "Double Rebirth Challenges", App.State.Statistic.DoubleRebirthChallengesFinished + this.Multiboni(App.State.Statistic.MultiDBChallenge), "How often you have defeated Baal after a Double Rebirth Challenge. You will receive 500,000 statistic multi and your might will level up one time (up to a max of 50) for each one.", 30);
			string right = Conv.MsToGuiText(App.State.Statistic.FastestDRCallenge.ToLong(), true);
			if (App.State.Statistic.FastestDRCallenge <= 0)
			{
				right = "No challenge finished.";
			}
			string str3 = string.Empty;
			if (App.State.Statistic.HasStartedDoubleRebirthChallenge)
			{
				str3 = "\nTime since you started the challenge: " + Conv.MsToGuiText(App.State.Statistic.TimeAfterDRCStarted.ToLong(), true);
			}
			num = this.AddLine(num, "Fastest DRC", right, "The fastest time you completed a 'Double Rebirth Challenge'" + str3, 30);
			num = this.AddLine(num, "1K Challenges", App.State.Statistic.OnekChallengesFinished + this.Multiboni(App.State.Statistic.Multi1KChallenge), "How often you have defeated Baal in a 1K Challenge. You will receive 1.5 million statistic multi and your clones will level up might 5% faster for each one. (level 40 is max)", 30);
			string right2 = Conv.MsToGuiText(App.State.Statistic.Fastest1KCCallenge.ToLong(), true);
			if (App.State.Statistic.Fastest1KCCallenge <= 0)
			{
				right2 = "No challenge finished.";
			}
			str3 = string.Empty;
			if (App.State.Statistic.HasStarted1kChallenge)
			{
				str3 = "\nTime since you started the challenge: " + Conv.MsToGuiText(App.State.Statistic.TimeAfter1KCStarted.ToLong(), true);
			}
			num = this.AddLine(num, "Fastest 1KC", right2, "The fastest time you completed a '1K Challenge'" + str3, 30);
			num = this.AddLine(num, "No Rebirth Challenges", App.State.Statistic.NoRbChallengesFinished + this.Multiboni(App.State.Statistic.MultiNoRbChallenge), "How often you have defeated Baal in a No Rebirth Challenge. You will receive 10 million statistic multi and you can fight Ultimate Beings 1% faster. (20% is max)", 30);
			string right3 = Conv.MsToGuiText(App.State.Statistic.FastestNoRbCCallenge.ToLong(), true);
			if (App.State.Statistic.FastestNoRbCCallenge <= 0)
			{
				right3 = "No challenge finished.";
			}
			if (App.State.Statistic.HasStartedNoRbChallenge)
			{
				str3 = "\nTime since you started the challenge: " + Conv.MsToGuiText(App.State.Statistic.TimeAfterNoRbStarted.ToLong(), true);
			}
			num = this.AddLine(num, "Fastest No Rebirth Challenge", right3, "The fastest time you completed a 'No Rebirth Challenge'" + str3, 30);
			num += 20;
			num = this.AddLine(num, "Ultimate Universe Challenges", App.State.Statistic.UniverseChallengesFinished + this.Multiboni(App.State.Statistic.MultiUUChallenge), "How often you finished an 'Ultimate Universe Challenge'. Your Planet level will increase by 1 for each finished Challenge (level 50 is max).", 30);
			string right4 = Conv.MsToGuiText(App.State.Statistic.FastestUUCallenge.ToLong(), true);
			if (App.State.Statistic.FastestUUCallenge <= 0)
			{
				right4 = "No challenge finished.";
			}
			str3 = string.Empty;
			if (App.State.Statistic.HasStartedUniverseChallenge)
			{
				str3 = "\nTime since you started the challenge: " + Conv.MsToGuiText(App.State.Statistic.TimeAfterUUCStarted.ToLong(), true);
			}
			num = this.AddLine(num, "Fastest UUC", right4, "The fastest time you completed an 'Ultimate Universe Challenge'" + str3, 30);
			num = this.AddLine(num, "Ultimate Pet Challenges", App.State.Statistic.UltimatePetChallengesFinished + this.Multiboni(App.State.Statistic.MultiUPChallenge), "How often you beat Baal with your pets. You will receive 2 million statistic multi and receive 5% more rewards from pet campaigns (maxed at 20).", 30);
			string right5 = Conv.MsToGuiText(App.State.Statistic.FastestUPCallenge.ToLong(), true);
			if (App.State.Statistic.FastestUPCallenge <= 0)
			{
				right5 = "No challenge finished.";
			}
			str3 = string.Empty;
			if (App.State.Statistic.HasStartedUltimatePetChallenge)
			{
				str3 = "\nTime since you started the challenge: " + Conv.MsToGuiText(App.State.Statistic.TimeAfterUPCStarted.ToLong(), true);
			}
			num = this.AddLine(num, "Fastest UPC", right5, "The fastest time you completed an 'Ultimate Pet Challenge'" + str3, 30);
			num = this.AddLine(num, "Black Holes Challenges", App.State.Statistic.BlackHoleChallengesFinished + this.Multiboni(App.State.Statistic.MultiBHChallenge), "How often you have finished the challenge. You will receive 750,000 statistic multi.\nFor each challenge finished, blackholes are 2% cheaper to build and each blackhole up to the number of BHCs completed provides an additional 5% chance for 1 GP each hour.\nCan't go higher than 200% for 2 additional GP each hour.", 30);
			string right6 = Conv.MsToGuiText(App.State.Statistic.FastestBHCallenge.ToLong(), true);
			if (App.State.Statistic.FastestBHCallenge <= 0)
			{
				right6 = "No challenge finished.";
			}
			str3 = string.Empty;
			if (App.State.Statistic.HasStartedBlackHoleChallenge)
			{
				str3 = "\nTime since you started the challenge: " + Conv.MsToGuiText(App.State.Statistic.TimeAfterBHCStarted.ToLong(), true);
			}
			num = this.AddLine(num, "Fastest BHC", right6, "The fastest time you completed a 'Black Hole Challenge'" + str3, 30);
			num += 20;
			num = this.AddLine(num, "Ultimate Baal Challenges", App.State.Statistic.UltimateBaalChallengesFinished + this.Multiboni(App.State.Statistic.MultiUBChallenge), "How often you beat Baal after an Ultimate Baal Challenge. You will receive 5 million statistic multi and P.Baals will grow weaker by 1% for each one (50% is minimum).", 30);
			string right7 = Conv.MsToGuiText(App.State.Statistic.FastestUBCallenge.ToLong(), true);
			if (App.State.Statistic.FastestUBCallenge <= 0)
			{
				right7 = "No challenge finished.";
			}
			str3 = string.Empty;
			if (App.State.Statistic.HasStartedUltimateBaalChallenge)
			{
				str3 = string.Concat(new object[]
				{
					"\nTime since you started the challenge: ",
					Conv.MsToGuiText(App.State.Statistic.TimeAfterUBCStarted.ToLong(), true),
					", currently you are on the ",
					App.State.Statistic.RebirthsAfterUBC,
					". rebirth."
				});
			}
			num = this.AddLine(num, "Fastest UBC", right7, "The fastest time you completed a 'Ultimate Baal Challenge'" + str3, 30);
			string right8 = "-";
			if (App.State.Statistic.MinRebirthsAfterUBC > 0)
			{
				right8 = App.State.Statistic.MinRebirthsAfterUBC.ToGuiText(true);
			}
			num = this.AddLine(num, "Minimum Rebirths used for UBC", right8, "Can you defeat baal in one rebirth?", 30);
			num = this.AddLine(num, "Ultimate Arty Challenges", App.State.Statistic.ArtyChallengesFinished + this.Multiboni(App.State.Statistic.MultiUAChallenge), "How often you beat Baal after an Ultimate Arty Challenge within 4 rebirths. You will receive 20 million statistic multi and P.Baals will grow weaker by 2% for each one (50% is minimum).", 30);
			string right9 = Conv.MsToGuiText(App.State.Statistic.FastestUACallenge.ToLong(), true);
			if (App.State.Statistic.FastestUACallenge <= 0)
			{
				right9 = "No challenge finished.";
			}
			str3 = string.Empty;
			if (App.State.Statistic.HasStartedUltimateBaalChallenge)
			{
				str3 = string.Concat(new object[]
				{
					"\nTime since you started the challenge: ",
					Conv.MsToGuiText(App.State.Statistic.TimeAfterUACStarted.ToLong(), true),
					", currently you are on the ",
					App.State.Statistic.RebirthsAfterUAC,
					". rebirth."
				});
			}
			num = this.AddLine(num, "Fastest UAC", right9, "The fastest time you completed an 'Ultimate Arty Challenge'" + str3, 30);
			right8 = "-";
			if (App.State.Statistic.MinRebirthsAfterUAC > 0)
			{
				right8 = App.State.Statistic.MinRebirthsAfterUAC.ToGuiText(true);
			}
			num = this.AddLine(num, "Minimum Rebirths used for UAC", right8, "Can you defeat baal in one rebirth?", 30);
			num += 20;
			num = this.AddLine(num, "Monuments created", App.State.Statistic.MonumentsCreated.GuiText, "The number of monuments you created. They work as a multiplier for all other statistic multiplier. Monuments are unlocked after defeating Diana.", 30);
			num = this.AddLine(num, "Total Multiplier ", string.Concat(new object[]
			{
				App.State.Statistic.StatisticRebirthMultiplierBase.ToGuiText(true),
				" x ",
				App.State.Statistic.MultiMonumentsCreated + 1,
				" = ",
				(App.State.Statistic.StatisticRebirthMultiplierBase * (App.State.Statistic.MultiMonumentsCreated + 1)).ToGuiText(true)
			}), "Total multiplier out of all statistics. Also multiplied with " + App.State.PremiumBoni.StatisticMulti.ToGuiText(true) + " from god power purchases but it is capped at: " + App.State.Multiplier.RebirthMulti.ToGuiText(true), 50);
			num += 25;
			GUI.EndScrollView();
			GUI.EndGroup();
			this.scrollHeight = num;
		}

		private string Multiboni(CDouble value)
		{
			return " (" + value.ToGuiText(true) + ")";
		}

		private int AddLine(int marginTop, string left, string right, string info, int height = 30)
		{
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)marginTop), GuiBase.Width(300f), GuiBase.Height((float)height)), new GUIContent(left, info));
			GUI.Label(new Rect(GuiBase.Width(320f), GuiBase.Height((float)marginTop), GuiBase.Width(300f), GuiBase.Height((float)height)), new GUIContent(right, info));
			marginTop += 23;
			return marginTop;
		}
	}
}
