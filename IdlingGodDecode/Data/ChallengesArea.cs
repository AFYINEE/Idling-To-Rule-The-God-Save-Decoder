using Assets.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	internal class ChallengesArea : GuiBase
	{
		public static ChallengesArea Instance = new ChallengesArea();

		public bool isOpen;

		private Vector2 scrollPosition = Vector2.zero;

		private int marginTop = 35;

		public void Show(GUIStyle labelStyle)
		{
			labelStyle.fontSize = 16;
			labelStyle.fontStyle = FontStyle.Normal;
			GUIStyle style = GUI.skin.GetStyle("Button");
			style.fontSize = GuiBase.FontSize(15);
			GUI.BeginGroup(new Rect(GuiBase.Width(9f), GuiBase.Height(45f), GuiBase.Width(265f), GuiBase.Height(490f)));
			GUI.Label(new Rect(GuiBase.Width(0f), GuiBase.Height(0f), GuiBase.Width(245f), GuiBase.Height(50f)), new GUIContent("Challenges"));
			App.State.GameSettings.HideMaxedChallenges = GUI.Toggle(new Rect(GuiBase.Width(110f), GuiBase.Height(5f), GuiBase.Width(260f), GuiBase.Height(25f)), App.State.GameSettings.HideMaxedChallenges, new GUIContent("Hide maxed", "If this is on, maxed challenges will be hidden."));
			this.scrollPosition = GuiBase.TouchScrollView(new Rect(GuiBase.Width(0f), GuiBase.Height(35f), GuiBase.Width(245f), GuiBase.Height(310f)), this.scrollPosition, new Rect(0f, GuiBase.Height(0f), GuiBase.Width(225f), GuiBase.Height((float)(this.marginTop + 30))));
			this.marginTop = 0;
			bool flag = App.State.GameSettings.HideMaxedChallenges && App.State.HomePlanet.UpgradeLevel >= 50;
			if (App.State.HomePlanet.UpgradeLevel >= 5 && !flag && GUI.Button(new Rect(GuiBase.Width(0f), GuiBase.Height((float)this.marginTop), GuiBase.Width(225f), GuiBase.Height(30f)), new GUIContent("Ultimate Universe Challenge", "This is almost like a normal rebirth. You will lose nothing and your time until you create one universe is recorded. If you create a universe, your Planet Level will be increased by one (This means your Powersurge speed will increase).")))
			{
				App.CheckRebirth(delegate(bool x)
				{
					if (x)
					{
						App.Rebirth();
						App.State.Statistic.HasStartedUniverseChallenge = true;
						App.State.Statistic.TimeAfterUUCStarted = 0;
					}
				}, true);
			}
			bool flag2 = App.State.GameSettings.HideMaxedChallenges && App.State.Statistic.AchievementChallengesFinished >= 50;
			if (App.State.Statistic.UniverseChallengesFinished > 0 && !flag2)
			{
				this.marginTop += 40;
				if (GUI.Button(new Rect(GuiBase.Width(0f), GuiBase.Height((float)this.marginTop), GuiBase.Width(225f), GuiBase.Height(30f)), new GUIContent("All Achievements Challenge", "This is almost like a normal rebirth. You will lose nothing and when you get all achievements available, your achievements will provide 1% more multiplier permanently and it will lower the requirement to get an training or fight achievement by 1% (maxed at 50).")))
				{
					App.CheckRebirth(delegate(bool x)
					{
						if (x)
						{
							App.Rebirth();
							App.State.Statistic.HasStartedAchievementChallenge = true;
						}
					}, true);
				}
			}
			bool flag3 = App.State.GameSettings.HideMaxedChallenges && App.State.Statistic.BlackHoleChallengesFinished >= 40;
			if (App.State.Statistic.AchievementChallengesFinished > 0 && !flag3)
			{
				this.marginTop += 40;
				if (GUI.Button(new Rect(GuiBase.Width(0f), GuiBase.Height((float)this.marginTop), GuiBase.Width(225f), GuiBase.Height(30f)), new GUIContent("Black Hole Challenge", "This is like a normal rebirth. It ends after building one black hole with one upgrade. For each challenge you have finished, the cost for building black holes and upgrades will be decreased by 2%.\nIt also adds a chance for passive god power gain of 5% for each black hole build and challenge finished. Maxed at 40 challenges. \n With 40 challenges finished, you will only pay 20% of the orginal cost and 40 black holes built will give you 3 god power every hour.")))
				{
					App.CheckRebirth(delegate(bool x)
					{
						if (x)
						{
							App.Rebirth();
							App.State.Statistic.HasStartedBlackHoleChallenge = true;
						}
					}, true);
				}
			}
			bool flag4 = App.State.GameSettings.HideMaxedChallenges && App.State.Statistic.UltimatePetChallengesFinished >= 20;
			if (App.State.Statistic.TotalPetGrowth > 10000 && !flag4)
			{
				this.marginTop += 40;
				if (GUI.Button(new Rect(GuiBase.Width(0f), GuiBase.Height((float)this.marginTop), GuiBase.Width(225f), GuiBase.Height(30f)), new GUIContent("Ultimate Pet Challenge", "This is like a normal rebirth but you can't fight gods by yourself. Your pets will fight the gods instead. The god page will show your total pet power for this run and this will be used to fight gods.\nThe challenge is finished when your pets defeat baal. While the challenge is active, your pets can find pet pills in item campaigns which will boost their multiplier until the challenge is over.\nEach challenge finished will increase all rewards from pet campaigns by 5%. Maxed at 100%")))
				{
					App.CheckRebirth(delegate(bool x)
					{
						if (x)
						{
							App.Rebirth();
							App.State.Statistic.HasStartedUltimatePetChallenge = true;
							App.State.Ext.PetPowerMultiCampaigns = 1;
							App.State.Ext.PetPowerMultiGods = 1;
							foreach (Creation current in App.State.AllCreations)
							{
								current.GodToDefeat.IsDefeatedPetChallenge = false;
							}
						}
					}, true);
				}
			}
			if (!App.State.GameSettings.HideMaxedChallenges || !(App.State.Statistic.DoubleRebirthChallengesFinished >= 50))
			{
				this.marginTop += 40;
				if (GUI.Button(new Rect(GuiBase.Width(0f), GuiBase.Height((float)this.marginTop), GuiBase.Width(225f), GuiBase.Height(30f)), new GUIContent("Double Rebirth Challenge", "This is an extra challenge that works like doing two rebirths in a row, resetting your multipliers to their starting values. ONLY recommended if you have a lot of God Power and want a challenge. If you beat Baal again after starting this challenge, you will receive an extra reward.\nAfter starting this challenge, you will gain 1 God Power for defeating the earlier gods for the first time once more.")))
				{
					App.CheckRebirth(delegate(bool x)
					{
						if (x)
						{
							GuiBase.ShowDialog("Are you sure?", "This is an extra challenge which will reward you with 10 God Power and 500,000 statistic multi after you defeat Baal again. \nIt will also level up might for each finished challenge by 1 level as soon as you unlock it.\nThis will revert your current stats to the default values!", delegate
							{
								GuiBase.ShowDialog("Are you really sure?", "This is no normal rebirth and it will take quite a bit of time to go back to your current progress.", delegate
								{
								}, delegate
								{
									App.Rebirth();
									App.Rebirth();
									App.State.Statistic.HasStartedDoubleRebirthChallenge = true;
									App.State.Statistic.TimeAfterDRCStarted = 0;
									foreach (Creation current in App.State.AllCreations)
									{
										current.GodToDefeat.IsDefeatedForFirstTime = false;
									}
								}, "No", "Yes", false, false);
							}, delegate
							{
							}, "Yes", "No", false, false);
						}
					}, false);
				}
			}
			bool flag5 = App.State.GameSettings.HideMaxedChallenges && App.State.Statistic.OnekChallengesFinished >= 40;
			if (App.State.Statistic.DoubleRebirthChallengesFinished > 1 && !flag5)
			{
				this.marginTop += 40;
				if (GUI.Button(new Rect(GuiBase.Width(0f), GuiBase.Height((float)this.marginTop), GuiBase.Width(225f), GuiBase.Height(30f)), new GUIContent("1k Challenge", "This is an extra challenge that works like doing two rebirths in a row, resetting your multipliers to their starting values. Compared to the Double Rebirth Challenge, you will only have 1000 Clones which can't be increased until you finish this challenge (defeat Baal). If you beat Baal again after starting this challenge, you will receive an extra reward.")))
				{
					App.CheckRebirth(delegate(bool x)
					{
						if (x)
						{
							GuiBase.ShowDialog("Are you sure?", "This is an extra challenge which will reward you with 20 God Power and 750,000 statistic multi after you defeat Baal again. \nIt will also increase the speed, your clones will level up might by 5%.\nThis will revert your current stats to the default values!", delegate
							{
								GuiBase.ShowDialog("Are you really sure?", "This is no normal rebirth and it will take quite a bit of time to go back to your current progress.", delegate
								{
								}, delegate
								{
									App.Rebirth();
									App.Rebirth();
									App.State.Statistic.HasStarted1kChallenge = true;
									App.State.Statistic.TimeAfter1KCStarted = 0;
									foreach (Creation current in App.State.AllCreations)
									{
										current.GodToDefeat.IsDefeatedForFirstTime = false;
									}
								}, "No", "Yes", false, false);
							}, delegate
							{
							}, "Yes", "No", false, false);
						}
					}, false);
				}
			}
			bool flag6 = App.State.GameSettings.HideMaxedChallenges && App.State.Statistic.NoRbChallengesFinished >= 20;
			if (App.State.Statistic.OnekChallengesFinished > 0 && !flag6)
			{
				this.marginTop += 40;
				if (GUI.Button(new Rect(GuiBase.Width(0f), GuiBase.Height((float)this.marginTop), GuiBase.Width(225f), GuiBase.Height(30f)), new GUIContent("No Rebirth Challenge", "This is an extra challenge that works like doing two rebirths in a row, resetting your multipliers to their starting values. Compared to the Double Rebirth Challenge, you can't rebirth until you finish this challenge (defeat Baal). Rebirthing once will cancel this challenge. If you beat Baal again after starting this challenge, you will receive an extra reward.")))
				{
					App.CheckRebirth(delegate(bool x)
					{
						if (x)
						{
							GuiBase.ShowDialog("Are you sure?", "This is an extra challenge which will reward you with 250 God Power and 5,000,000 statistic multi after you defeat Baal again. \nUltimate Beings will also appear 1% faster. (Maxed at 20%)\nThis will revert your current stats to the default values!", delegate
							{
								GuiBase.ShowDialog("Are you really sure?", "This is no normal rebirth and it will take quite a bit of time to go back to your current progress.", delegate
								{
								}, delegate
								{
									App.Rebirth();
									App.Rebirth();
									App.State.Statistic.HasStartedNoRbChallenge = true;
									App.State.Statistic.TimeAfterNoRbStarted = 0;
								}, "No", "Yes", false, false);
							}, delegate
							{
							}, "Yes", "No", false, false);
						}
					}, false);
				}
			}
			if (!App.State.GameSettings.HideMaxedChallenges || !(App.State.Statistic.UltimateBaalChallengesFinished + App.State.Statistic.ArtyChallengesFinished * 2 >= 50))
			{
				this.marginTop += 40;
				if (GUI.Button(new Rect(GuiBase.Width(0f), GuiBase.Height((float)this.marginTop), GuiBase.Width(225f), GuiBase.Height(30f)), new GUIContent("Ultimate Baal Challenge", "This is similar to the double rebirth challenge, but your current God Power and upgrades will also be locked (defeat Baal again to regain everything that you had before). Max Shadow Clones will be reverted to 1000. Your total might will revert to 0.\nYour reward for beating this challenge will be much bigger than that for the regular double rebirth challenge. This might be the better option if you don't have much God Power and want a challenge.")))
				{
					App.CheckRebirth(delegate(bool x)
					{
						if (x)
						{
							GuiBase.ShowDialog("Are you sure?", "This is an extra challenge which will reward you with 100 God Power and 5 million statistic multi after you defeat Baal again. This will revert your current stats to the default values and also remove your God Power and God Power Upgrades. P.Baals will also become weaker by 1% until 50% for each finished challenge.", delegate
							{
								UpdateStats.SaveToServer(UpdateStats.ServerSaveType.UBChallengeSave);
								GuiBase.ShowDialog("Are you really sure?", "This is no normal rebirth and it will take quite a bit of time to go back to your current progress.", delegate
								{
								}, delegate
								{
									this.StartUltimateBaalChallenge();
								}, "No", "Yes", false, false);
							}, delegate
							{
							}, "Yes", "No", false, false);
						}
					}, false);
				}
			}
			if (!App.State.GameSettings.HideMaxedChallenges || !(App.State.Statistic.ArtyChallengesFinished >= 2))
			{
				this.marginTop += 40;
				if (GUI.Button(new Rect(GuiBase.Width(0f), GuiBase.Height((float)this.marginTop), GuiBase.Width(225f), GuiBase.Height(30f)), new GUIContent("Ultimate Arty Challenge", "This isn't worth it, so I'd pass on it. It is similar to anUltimate Baal Challenge (UBC), but you will also lose your training caps, unlocked TBS, need to unlock your Planet again and you have to defeat Baal within 5 rebirths after taking this challenge (The rebirth used for starting this challenge counts as one!).\nOnce you do a 6th rebirth the challenge will chancel, and you will receive no reward. \nIf you finish this challenge, you will receive twice the rewards than you would get from an UBC.")))
				{
					App.CheckRebirth(delegate(bool x)
					{
						if (x)
						{
							GuiBase.ShowDialog("Are you sure?", "This is an extra challenge which will reward you with 200 God Power and 10 million statistic multi if you defeat Baal within 4 rebirths. The first time, you will receive the turtle pet and the second time a free pet token. No extra reward after that!\nBeating it faster than in 2000000 seconds ~23 days is almost impossible without cheating, so also no extra reward for that.\nThis will revert your current stats to the default values and also remove your God Power and God Power Upgrades. P.Baals will also become weaker by 2% until 50% for each finished challenge.", delegate
							{
								UpdateStats.SaveToServer(UpdateStats.ServerSaveType.UAChallengeSave);
								GuiBase.ShowDialog("Are you really sure?", "This is no normal rebirth and it will take quite a bit of time to go back to your current progress.", delegate
								{
								}, delegate
								{
									this.StartArtyChallenge();
								}, "No", "Yes", false, false);
							}, delegate
							{
							}, "Yes", "No", false, false);
						}
					}, false);
				}
			}
			GUI.EndScrollView();
			GUI.EndGroup();
		}

		private void StartUltimateBaalChallenge()
		{
			App.Rebirth();
			App.Rebirth();
			App.State.Statistic.HasStartedUltimateBaalChallenge = true;
			App.State.Statistic.TimeAfterUBCStarted = 0;
			App.State.Statistic.PremiumStatsBeforeUBCChallenge = App.State.PremiumBoni.Serialize();
			Premium premiumBoni = App.State.PremiumBoni;
			App.State.PremiumBoni = new Premium();
			App.State.PremiumBoni.AddPremiumWhenStartChallenge(premiumBoni);
			App.State.Statistic.AbsoluteMaxClonesBeforeUBCChallenge = App.State.Clones.AbsoluteMaximum;
			App.State.Statistic.MaxClonesBeforeUBCChallenge = App.State.Clones.MaxShadowClones;
			App.State.Clones.AbsoluteMaximum = 99999;
			App.State.Clones.MaxShadowClones = 1000;
			App.State.Statistic.RebirthsAfterUBC = 1;
			App.State.Statistic.CountRebirthsInUBC = true;
			foreach (Creation current in App.State.AllCreations)
			{
				current.GodToDefeat.IsDefeatedForFirstTime = false;
			}
		}

		private void StartArtyChallenge()
		{
			App.Rebirth();
			App.Rebirth();
			App.State.Statistic.HasStartedArtyChallenge = true;
			App.State.Statistic.TimeAfterUACStarted = 0;
			App.State.Statistic.HighestGodInUAC = 0;
			App.State.Statistic.PremiumStatsBeforeUBCChallenge = App.State.PremiumBoni.Serialize();
			Premium premiumBoni = App.State.PremiumBoni;
			App.State.PremiumBoni = new Premium();
			App.State.PremiumBoni.AddPremiumWhenStartChallenge(premiumBoni);
			App.State.Statistic.AbsoluteMaxClonesBeforeUBCChallenge = App.State.Clones.AbsoluteMaximum;
			App.State.Statistic.MaxClonesBeforeUBCChallenge = App.State.Clones.MaxShadowClones;
			App.State.Clones.AbsoluteMaximum = 99999;
			App.State.Clones.MaxShadowClones = 1000;
			App.State.Statistic.RebirthsAfterUAC = 1;
			App.State.HomePlanet.UpgradeLevelArtyChallenge = App.State.HomePlanet.UpgradeLevel;
			App.State.HomePlanet.UpgradeLevel = 0;
			App.State.Crits = new Critical();
			foreach (UltimateBeing current in App.State.HomePlanet.UltimateBeings)
			{
				current.IsAvailable = false;
			}
			foreach (Creation current2 in App.State.AllCreations)
			{
				current2.GodToDefeat.IsDefeatedForFirstTime = false;
			}
			string text = string.Empty;
			foreach (Skill current3 in App.State.AllSkills)
			{
				text = text + current3.Extension.UsageCount.ToString() + ",";
				current3.Extension.UsageCount = 0L;
			}
			App.State.Statistic.SkillUsageCountBeforeUAC = text;
			App.State.HomePlanet.IsCreated = false;
		}
	}
}
