using Assets.Scripts.Helper;
using System;
using System.Text;

namespace Assets.Scripts.Data
{
	public class Settings
	{
		public bool AutoAddClones = true;

		public int StopClonesAtTrainings = 100000;

		public int StopClonesAtSkills = 100000;

		public bool IsStopAtOnSkills;

		public bool IsStopAtOnTrainings;

		public bool StopMonumentBuilding;

		public bool StopDivinityGenBuilding;

		public bool CreateShadowClonesIfNotMax;

		public Creation LastCreation;

		public bool SyncScrollbars = true;

		public bool ShowAchievementPopups = true;

		public int Framerate = 30;

		public bool AutoBuyCreations;

		public bool AutoBuyCreationsForMonuments;

		public bool AutoBuyCreationsForDivGen;

		public bool AutoBuyCreationsForMonumentsBeforeRebirth;

		public bool UseExponentNumbers;

		public bool AutoBuyForCrystal;

		public bool HideMaxedChallenges;

		public bool UseStopAt;

		public int TooltipMode;

		public int UIStyle;

		public string CustomColor = string.Empty;

		public string CustomBackground = "0.25,0.25,0.25";

		public bool ShowToolTipsOnTop;

		public int CreationToCreateCount;

		public int ClonesToAddCount = 100;

		public bool AutoFightIsOn = true;

		public bool ShowToolTipsOnRightClick;

		public bool IgnoreCloneCountOn;

		public int TrainIgnoreCount;

		public bool TBSEyesIsMirrored = true;

		public bool NextFightIf1Cloned;

		public bool AchievementsOnTop;

		public bool SoundOn;

		public int SavedClonesForFight;

		public bool StickyClones;

		public int ProgressbarType;

		public bool SpecialFightSkillsSorted;

		public bool CreationsNextAtCreated;

		public int CreationsNextAtMode;

		public long DivGenCreatiosToAdd = 10000L;

		public bool ChooseCreationIsOpen;

		public bool AvaScaled;

		public int PetDistribution;

		public int MaxDefenderClones;

		public bool AutofillDefenders;

		public int FontType;

		public bool MaxAfterEquipCrystal;

		public Growth LastSelectedGrowth = Growth.All;

		public int LastHoursForCampaigns = 1;

		public bool SyncTrainingSkill;

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.StopClonesAtTrainings);
			Conv.AppendValue(stringBuilder, "b", this.StopClonesAtSkills);
			Conv.AppendValue(stringBuilder, "c", this.AutoAddClones.ToString());
			Conv.AppendValue(stringBuilder, "d", this.IsStopAtOnSkills.ToString());
			Conv.AppendValue(stringBuilder, "e", this.IsStopAtOnTrainings.ToString());
			if (this.LastCreation != null)
			{
				Conv.AppendValue(stringBuilder, "g", this.LastCreation.Serialize());
			}
			Conv.AppendValue(stringBuilder, "i", this.SyncScrollbars.ToString());
			Conv.AppendValue(stringBuilder, "j", this.ShowAchievementPopups.ToString());
			Conv.AppendValue(stringBuilder, "k", this.Framerate);
			Conv.AppendValue(stringBuilder, "l", this.AutoBuyCreations.ToString());
			Conv.AppendValue(stringBuilder, "m", this.AutoBuyCreationsForMonuments.ToString());
			Conv.AppendValue(stringBuilder, "n", this.UseStopAt.ToString());
			Conv.AppendValue(stringBuilder, "o", this.TooltipMode);
			Conv.AppendValue(stringBuilder, "p", this.UseExponentNumbers.ToString());
			Conv.AppendValue(stringBuilder, "q", this.UIStyle);
			Conv.AppendValue(stringBuilder, "r", this.CustomColor);
			Conv.AppendValue(stringBuilder, "s", this.StopMonumentBuilding.ToString());
			Conv.AppendValue(stringBuilder, "t", this.ShowToolTipsOnTop.ToString());
			Conv.AppendValue(stringBuilder, "u", this.CreationToCreateCount);
			Conv.AppendValue(stringBuilder, "v", this.AutoFightIsOn.ToString());
			Conv.AppendValue(stringBuilder, "w", this.ShowToolTipsOnRightClick.ToString());
			Conv.AppendValue(stringBuilder, "x", this.IgnoreCloneCountOn.ToString());
			Conv.AppendValue(stringBuilder, "y", this.TrainIgnoreCount);
			Conv.AppendValue(stringBuilder, "z", this.CreateShadowClonesIfNotMax.ToString());
			Conv.AppendValue(stringBuilder, "A", this.TBSEyesIsMirrored.ToString());
			Conv.AppendValue(stringBuilder, "D", this.ClonesToAddCount);
			Conv.AppendValue(stringBuilder, "E", this.NextFightIf1Cloned.ToString());
			Conv.AppendValue(stringBuilder, "F", this.AchievementsOnTop.ToString());
			Conv.AppendValue(stringBuilder, "G", this.SoundOn.ToString());
			Conv.AppendValue(stringBuilder, "H", this.SavedClonesForFight);
			Conv.AppendValue(stringBuilder, "I", this.StickyClones.ToString());
			Conv.AppendValue(stringBuilder, "J", this.CustomBackground);
			Conv.AppendValue(stringBuilder, "K", this.ProgressbarType);
			Conv.AppendValue(stringBuilder, "L", this.SpecialFightSkillsSorted.ToString());
			Conv.AppendValue(stringBuilder, "M", this.CreationsNextAtCreated.ToString());
			Conv.AppendValue(stringBuilder, "N", this.StopDivinityGenBuilding.ToString());
			Conv.AppendValue(stringBuilder, "O", this.CreationsNextAtMode);
			Conv.AppendValue(stringBuilder, "P", this.DivGenCreatiosToAdd);
			Conv.AppendValue(stringBuilder, "Q", this.ChooseCreationIsOpen.ToString());
			Conv.AppendValue(stringBuilder, "R", this.AvaScaled.ToString());
			Conv.AppendValue(stringBuilder, "S", this.PetDistribution);
			Conv.AppendValue(stringBuilder, "T", this.AutoBuyForCrystal.ToString());
			Conv.AppendValue(stringBuilder, "U", this.MaxDefenderClones);
			Conv.AppendValue(stringBuilder, "V", this.AutofillDefenders.ToString());
			Conv.AppendValue(stringBuilder, "W", this.FontType);
			Conv.AppendValue(stringBuilder, "X", this.MaxAfterEquipCrystal.ToString());
			Conv.AppendValue(stringBuilder, "Y", this.AutoBuyCreationsForDivGen.ToString());
			Conv.AppendValue(stringBuilder, "Z", (int)this.LastSelectedGrowth);
			Conv.AppendValue(stringBuilder, NS.n1.Nr(), this.LastHoursForCampaigns);
			Conv.AppendValue(stringBuilder, NS.n2.Nr(), this.SyncTrainingSkill.ToString());
			Conv.AppendValue(stringBuilder, NS.n3.Nr(), this.HideMaxedChallenges.ToString());
			return Conv.ToBase64(stringBuilder.ToString(), "Settings");
		}

		internal static Settings FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				return new Settings();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "Settings");
			Settings settings = new Settings();
			settings.StopClonesAtTrainings = Conv.getIntFromParts(parts, "a");
			settings.StopClonesAtSkills = Conv.getIntFromParts(parts, "b");
			settings.AutoAddClones = Conv.getStringFromParts(parts, "c").ToLower().Equals("true");
			settings.IsStopAtOnSkills = Conv.getStringFromParts(parts, "d").ToLower().Equals("true");
			settings.IsStopAtOnTrainings = Conv.getStringFromParts(parts, "e").ToLower().Equals("true");
			settings.LastCreation = Creation.FromString(Conv.getStringFromParts(parts, "g"));
			settings.SyncScrollbars = Conv.getStringFromParts(parts, "i").ToLower().Equals("true");
			settings.ShowAchievementPopups = Conv.getStringFromParts(parts, "j").ToLower().Equals("true");
			settings.Framerate = Conv.getIntFromParts(parts, "k");
			settings.AutoBuyCreations = Conv.getStringFromParts(parts, "l").ToLower().Equals("true");
			settings.AutoBuyCreationsForMonuments = Conv.getStringFromParts(parts, "m").ToLower().Equals("true");
			settings.UseStopAt = Conv.getStringFromParts(parts, "n").ToLower().Equals("true");
			settings.TooltipMode = Conv.getIntFromParts(parts, "o");
			settings.UseExponentNumbers = Conv.getStringFromParts(parts, "p").ToLower().Equals("true");
			settings.UIStyle = Conv.getIntFromParts(parts, "q");
			settings.CustomColor = Conv.getStringFromParts(parts, "r");
			settings.StopMonumentBuilding = Conv.getStringFromParts(parts, "s").ToLower().Equals("true");
			settings.ShowToolTipsOnTop = Conv.getStringFromParts(parts, "t").ToLower().Equals("true");
			settings.CreationToCreateCount = Conv.getIntFromParts(parts, "u");
			settings.AutoFightIsOn = Conv.getStringFromParts(parts, "v").ToLower().Equals("true");
			settings.ShowToolTipsOnRightClick = Conv.getStringFromParts(parts, "w").ToLower().Equals("true");
			settings.IgnoreCloneCountOn = Conv.getStringFromParts(parts, "x").ToLower().Equals("true");
			settings.TrainIgnoreCount = Conv.getIntFromParts(parts, "y");
			settings.CreateShadowClonesIfNotMax = Conv.getStringFromParts(parts, "z").ToLower().Equals("true");
			settings.TBSEyesIsMirrored = Conv.getStringFromParts(parts, "A").ToLower().Equals("true");
			settings.ClonesToAddCount = Conv.getIntFromParts(parts, "D");
			settings.NextFightIf1Cloned = Conv.getStringFromParts(parts, "E").ToLower().Equals("true");
			settings.AchievementsOnTop = Conv.getStringFromParts(parts, "F").ToLower().Equals("true");
			settings.SoundOn = Conv.getStringFromParts(parts, "G").ToLower().Equals("true");
			settings.SavedClonesForFight = Conv.getIntFromParts(parts, "H");
			settings.StickyClones = Conv.getStringFromParts(parts, "I").ToLower().Equals("true");
			settings.CustomBackground = Conv.getStringFromParts(parts, "J");
			settings.ProgressbarType = Conv.getIntFromParts(parts, "K");
			settings.SpecialFightSkillsSorted = Conv.getStringFromParts(parts, "L").ToLower().Equals("true");
			settings.CreationsNextAtCreated = Conv.getStringFromParts(parts, "M").ToLower().Equals("true");
			settings.StopDivinityGenBuilding = Conv.getStringFromParts(parts, "N").ToLower().Equals("true");
			settings.CreationsNextAtMode = Conv.getIntFromParts(parts, "O");
			settings.DivGenCreatiosToAdd = Conv.getLongFromParts(parts, "P");
			settings.ChooseCreationIsOpen = Conv.getStringFromParts(parts, "Q").ToLower().Equals("true");
			settings.AvaScaled = Conv.getStringFromParts(parts, "R").ToLower().Equals("true");
			settings.PetDistribution = Conv.getIntFromParts(parts, "S");
			settings.AutoBuyForCrystal = Conv.getStringFromParts(parts, "T").ToLower().Equals("true");
			settings.MaxDefenderClones = Conv.getIntFromParts(parts, "U");
			settings.AutofillDefenders = Conv.getStringFromParts(parts, "V").ToLower().Equals("true");
			settings.FontType = Conv.getIntFromParts(parts, "W");
			settings.MaxAfterEquipCrystal = Conv.getStringFromParts(parts, "X").ToLower().Equals("true");
			settings.AutoBuyCreationsForDivGen = Conv.getStringFromParts(parts, "Y").ToLower().Equals("true");
			settings.LastSelectedGrowth = (Growth)Conv.getIntFromParts(parts, "Z");
			settings.LastHoursForCampaigns = Conv.getIntFromParts(parts, NS.n1.Nr());
			settings.SyncTrainingSkill = Conv.getStringFromParts(parts, NS.n2.Nr()).ToLower().Equals("true");
			settings.HideMaxedChallenges = Conv.getStringFromParts(parts, NS.n3.Nr()).ToLower().Equals("true");
			if (settings.TrainIgnoreCount == 0)
			{
				settings.TrainIgnoreCount = 1;
			}
			return settings;
		}
	}
}
