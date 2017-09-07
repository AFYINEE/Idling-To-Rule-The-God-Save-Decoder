using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
	public class GameState
	{
		public BattleState Battle = new BattleState();

		public long TimeStampGameClosedOfflineMS;

		public bool IsMonumentUnlocked;

		public bool IsBuyUnlocked;

		public bool IsUpgradeUnlocked;

		public string KongUserId = string.Empty;

		public string KongUserName = string.Empty;

		public string AndroidName = string.Empty;

		public string AndroidId = string.Empty;

		public string SteamId = string.Empty;

		public string SteamName = string.Empty;

		public bool IsBlackListed;

		private CDouble currentHealth;

		public int UnleashRegenBoni;

		public int UnleashAttackBoni;

		public int UnleashDefenseBoni;

		public int PhysicalHPMod = 100;

		public int PhysicalAttackMod = 25;

		private string physicalInfo = string.Empty;

		private string physicalInfoDraw = string.Empty;

		public int MysticDefMod = 100;

		public int MysticRegenMod = 100;

		private string mysticInfo = string.Empty;

		private string mysticInfoDraw = string.Empty;

		public int BattleAttackMod = 100;

		private string battlePowerInfo = string.Empty;

		private string battlePowerInfoDraw = string.Empty;

		private string creatringInfo = string.Empty;

		private string attackInfo = string.Empty;

		public int ClonesDifGenMod = 100;

		public int ClonesPlanetMod = 10;

		public int PowerSurgeMod = 100;

		private const int countValue = 100;

		private CDouble physicalPower;

		private CDouble mysticPower;

		private CDouble battlePower;

		private CDouble creatingPower;

		private CDouble powerLevel;

		private CDouble attack;

		private CDouble defense;

		private CDouble maxHealth;

		private CDouble HpRecover = new CDouble();

		public CDouble HpRecoverSec = new CDouble();

		private int recoverUpdateCount;

		public CDouble DivGainSecFights = 0;

		public string DivGainText = string.Empty;

		private bool UpdateMaxHealth;

		private bool UpdateAttack;

		private bool UpdatePhysical;

		private bool UpdateMystic;

		private bool UpdateBattle;

		private bool UpdateCreating;

		private bool UpdateDefense;

		private bool UpdatePowerLevel;

		public bool IsCrystalFactoryAvailable
		{
			get
			{
				return this.HomePlanet.IsCreated && (this.Statistic.UltimateBaalChallengesFinished > 0 || this.Statistic.ArtyChallengesFinished > 0);
			}
		}

		public bool PossibleCheater
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}

		public string TitleGod
		{
			get;
			set;
		}

		public CDouble Money
		{
			get;
			set;
		}

		public List<Training> AllTrainings
		{
			get;
			set;
		}

		public List<Skill> AllSkills
		{
			get;
			set;
		}

		public List<Fight> AllFights
		{
			get;
			set;
		}

		public List<Creation> AllCreations
		{
			get;
			set;
		}

		public List<Monument> AllMonuments
		{
			get;
			set;
		}

		public ShadowClone Clones
		{
			get;
			set;
		}

		public PBaal PrinnyBaal
		{
			get;
			set;
		}

		public AvatarOptions Avatar
		{
			get;
			set;
		}

		public Critical Crits
		{
			get;
			set;
		}

		public Planet HomePlanet
		{
			get;
			set;
		}

		public List<Might> AllMights
		{
			get;
			set;
		}

		public long TimeStampGameClosed
		{
			get;
			set;
		}

		public Premium PremiumBoni
		{
			get;
			set;
		}

		public bool ShouldSubmitScore
		{
			get;
			set;
		}

		public bool IsTutorialShown
		{
			get;
			set;
		}

		public bool IsGuestMsgShown
		{
			get;
			set;
		}

		public bool IsSocialDialogShown
		{
			get;
			set;
		}

		public Settings GameSettings
		{
			get;
			set;
		}

		public PlayerKredProblems KredProblems
		{
			get;
			set;
		}

		public long KongUserIdLong
		{
			get
			{
				return Conv.StringToLong(this.KongUserId);
			}
		}

		public long SteamIdLong
		{
			get
			{
				return Conv.StringToLong(this.SteamId);
			}
		}

		public string AvatarName
		{
			get;
			set;
		}

		public bool ChangeAvatarName
		{
			get;
			set;
		}

		public State2 Ext
		{
			get;
			set;
		}

		public int CloneAttackDivider
		{
			get;
			set;
		}

		public int CloneDefenseDivider
		{
			get;
			set;
		}

		public int CloneHealthDivider
		{
			get;
			set;
		}

		public CDouble CloneAttack
		{
			get
			{
				return this.Attack / this.CloneAttackDivider;
			}
		}

		public CDouble CloneDefense
		{
			get
			{
				return this.Defense / this.CloneDefenseDivider;
			}
		}

		public CDouble CloneMaxHealth
		{
			get
			{
				return this.MaxHealth / this.CloneHealthDivider;
			}
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

		public List<Achievement> TrainingAchievements
		{
			get;
			set;
		}

		public List<Achievement> SkillAchievements
		{
			get;
			set;
		}

		public List<Achievement> FightingAchievements
		{
			get;
			set;
		}

		public List<Achievement> CreatingAchievements
		{
			get;
			set;
		}

		public Multi Multiplier
		{
			get;
			set;
		}

		public Statistic Statistic
		{
			get;
			set;
		}

		public Statistic ChallengeStatistic
		{
			get;
			set;
		}

		public CDouble PhysicalPowerBase
		{
			get;
			set;
		}

		public CDouble MysticPowerBase
		{
			get;
			set;
		}

		public CDouble BattlePowerBase
		{
			get;
			set;
		}

		public CDouble CreatingPowerBase
		{
			get;
			set;
		}

		public CDouble OldCreatingPower
		{
			get;
			set;
		}

		public long CreatingSpeedBoniDuration
		{
			get;
			set;
		}

		public DivinityGenerator Generator
		{
			get;
			set;
		}

		public string PhysicalDescription
		{
			get
			{
				return "Physical power increases your hp and the damage you do.\n(Each Physical gives " + this.physicalInfo + ")\nIncreases when training. " + this.physicalInfoDraw;
			}
		}

		public string MysticDescription
		{
			get
			{
				return "Mystic power reduces the damage you receive and increases the speed of your hp recovery.\n(Each Mystic power gives " + this.mysticInfo + " Defensive power)\nIncreases when training skills. " + this.mysticInfoDraw;
			}
		}

		public string BattleDescription
		{
			get
			{
				return "Battle power increases the damage you do.\n(Each Battle power gives " + this.battlePowerInfo + " Attack Power)\nIncreases when fighting enemies. " + this.battlePowerInfoDraw;
			}
		}

		public string CreatingDescription
		{
			get
			{
				return "Higher creating power lets you create things faster\nCurrent creation speed: " + this.creatringInfo;
			}
		}

		public string AttackDescription
		{
			get
			{
				return "Your attack power = " + this.attackInfo;
			}
		}

		public CDouble PhysicalPower
		{
			get
			{
				if (this.physicalPower == null || this.UpdatePhysical)
				{
					this.UpdatePhysical = false;
					CDouble cDouble = this.PremiumBoni.GpBoniPhysical.ToInt();
					cDouble *= 1 + this.PremiumBoni.CrystalPower / 1000;
					this.physicalPower = this.PhysicalPowerBase * this.Multiplier.CurrentMultiPhysical * (100 + cDouble) * (100 + this.PremiumBoni.CrystalBonusPhysical) / 10000;
					this.physicalPower = this.AdditionalMultis(this.physicalPower);
				}
				return this.physicalPower;
			}
		}

		public CDouble MysticPower
		{
			get
			{
				if (this.mysticPower == null || this.UpdateMystic)
				{
					this.UpdateMystic = false;
					CDouble cDouble = this.PremiumBoni.GpBoniMystic.ToInt();
					cDouble *= 1 + this.PremiumBoni.CrystalPower / 1000;
					this.mysticPower = this.MysticPowerBase * this.Multiplier.CurrentMultiMystic * (100 + cDouble) * (100 + this.PremiumBoni.CrystalBonusMystic) / 10000;
					this.mysticPower = this.AdditionalMultis(this.mysticPower);
					if (this.UnleashDefenseBoni > 0)
					{
						this.mysticPower = this.mysticPower * this.UnleashDefenseBoni / 100;
					}
				}
				return this.mysticPower;
			}
		}

		public CDouble BattlePower
		{
			get
			{
				if (this.battlePower == null || this.UpdateBattle)
				{
					this.UpdateBattle = false;
					CDouble cDouble = this.PremiumBoni.GpBoniBattle.ToInt();
					cDouble *= 1 + this.PremiumBoni.CrystalPower / 1000;
					this.battlePower = this.BattlePowerBase * this.Multiplier.CurrentMultiBattle * (100 + cDouble) * (100 + this.PremiumBoni.CrystalBonusBattle) / 10000;
					this.battlePower = this.AdditionalMultis(this.battlePower);
				}
				return this.battlePower;
			}
		}

		public CDouble CreatingPower
		{
			get
			{
				if (this.creatingPower == null || this.UpdateCreating)
				{
					this.UpdateCreating = false;
					CDouble cDouble = this.PremiumBoni.GpBoniCreating.ToInt();
					cDouble *= 1 + this.PremiumBoni.CrystalPower / 1000;
					this.creatingPower = this.CreatingPowerBase * this.Multiplier.CurrentMultiCreating * (100 + cDouble) * (100 + this.PremiumBoni.CrystalBonusCreation) / 10000;
					this.creatingPower = this.AdditionalMultis(this.creatingPower);
				}
				return this.creatingPower;
			}
		}

		public CDouble PowerLevel
		{
			get
			{
				if (this.powerLevel == null || this.UpdatePowerLevel)
				{
					this.UpdatePowerLevel = false;
					CDouble cDouble = 1 + this.Crits.CriticalPercent(this.GameSettings.TBSEyesIsMirrored) / 100 * (this.Crits.CriticalDamage - 100) / 100;
					if (cDouble < 1)
					{
						cDouble = 1;
					}
					this.powerLevel = (this.MysticPower + this.Attack + this.MaxHealth / 10) * cDouble;
				}
				return this.powerLevel;
			}
		}

		public CDouble Attack
		{
			get
			{
				if (this.attack == null || this.UpdateAttack)
				{
					this.UpdateAttack = false;
					this.attack = this.BattlePower * this.BattleAttackMod / 100 + this.PhysicalPower * this.PhysicalAttackMod / 100 + this.CreatingPower / 2;
					if (this.UnleashAttackBoni > 0)
					{
						this.attack = this.attack * this.UnleashAttackBoni / 100;
					}
				}
				return this.attack;
			}
		}

		public CDouble Defense
		{
			get
			{
				if (this.defense == null || this.UpdateDefense)
				{
					this.UpdateDefense = false;
					this.defense = this.MysticPower * this.MysticDefMod / 100;
				}
				return this.defense;
			}
		}

		public CDouble MaxHealth
		{
			get
			{
				if (this.maxHealth == null || this.UpdateMaxHealth)
				{
					this.UpdateMaxHealth = false;
					this.maxHealth = this.PhysicalPower * this.PhysicalHPMod / 10 + this.CreatingPower * 5;
				}
				if (this.maxHealth == 0)
				{
					return 1;
				}
				return this.maxHealth;
			}
		}

		public GameState(bool initialValues, int achievementChallenges = 0)
		{
			this.Money = new CDouble();
			this.PhysicalPowerBase = new CDouble();
			this.MysticPowerBase = new CDouble();
			this.BattlePowerBase = new CDouble();
			this.CreatingPowerBase = new CDouble();
			this.CurrentHealth = new CDouble();
			this.Statistic = new Statistic();
			this.ChallengeStatistic = new Statistic();
			this.Title = "God in Training";
			this.PremiumBoni = new Premium();
			this.Clones = new ShadowClone();
			this.Generator = new DivinityGenerator();
			this.GameSettings = new Settings();
			this.KredProblems = new PlayerKredProblems();
			this.PrinnyBaal = new PBaal();
			this.Avatar = new AvatarOptions();
			this.Crits = new Critical();
			this.Ext = new State2();
			this.CloneAttackDivider = 1000;
			this.CloneDefenseDivider = 1000;
			this.CloneHealthDivider = 1000;
			this.PremiumBoni.StatisticMulti = 1;
			this.Multiplier = new Multi();
			this.HomePlanet = new Planet();
			if (initialValues)
			{
                try
                {
                    Achievement.InitAchievements(achievementChallenges);
                }
                catch (Exception e)
                {
                    
                }
				this.AllTrainings = Training.Initial();
				this.AllCreations = Creation.Initial();
				this.AllSkills = Skill.Initial();
				this.AllFights = Fight.Initial();
				this.AllMonuments = Monument.Initial();
				this.Generator.Upgrades = GeneratorUpgrade.Initial();
				this.AllMights = Might.Initial();
				Creation creation = this.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Shadow_clone);
				if (creation != null)
				{
					creation.IsActive = true;
				}
				this.TrainingAchievements = Achievement.InitialTrainingAchievements();
				this.SkillAchievements = Achievement.InitialSkillAchievements();
				this.FightingAchievements = Achievement.InitialFightAchievements();
				this.CreatingAchievements = Achievement.InitialCreationAchievements();
				this.ShouldSubmitScore = true;
				this.HomePlanet.InitUBMultipliers();
				this.Ext.AllPets = Pet.InitialPets();
			}
			else
			{
				this.AllTrainings = new List<Training>();
				this.AllCreations = new List<Creation>();
				this.AllSkills = new List<Skill>();
				this.AllFights = new List<Fight>();
				this.AllMonuments = new List<Monument>();
				this.AllMights = new List<Might>();
				this.TrainingAchievements = new List<Achievement>();
				this.SkillAchievements = new List<Achievement>();
				this.FightingAchievements = new List<Achievement>();
				this.CreatingAchievements = new List<Achievement>();
				this.Ext.AllPets = new List<Pet>();
			}
			if (!this.PremiumBoni.HasObtainedFreeGodPower && this.PremiumBoni.GodPower == 0)
			{
				this.PremiumBoni.HasObtainedFreeGodPower = true;
				this.PremiumBoni.GodPower += 10;
				this.PremiumBoni.CheckIfGPIsAdjusted();
			}
		}

		public CDouble AdditionalMultis(CDouble baseValue)
		{
			CDouble cDouble = baseValue * (100 + this.HomePlanet.PlanetMultiplier) / 100;
			if (this.PremiumBoni.TotalMightIsUnlocked)
			{
				cDouble = cDouble * (100L + this.PremiumBoni.TotalMight / 4L) / 100;
			}
			return cDouble;
		}

		public void CheckMightBoni()
		{
			this.UnleashRegenBoni = 0;
			this.UnleashAttackBoni = 0;
			this.UnleashDefenseBoni = 0;
			foreach (Might current in this.AllMights)
			{
				if (current.DurationLeft > 0L)
				{
					this.UnleashRegenBoni = this.MultiplyUnleash(this.UnleashRegenBoni, current.UnleashRegenBoni);
					this.UnleashAttackBoni = this.MultiplyUnleash(this.UnleashAttackBoni, current.UnleashAttackBoni);
					this.UnleashDefenseBoni = this.MultiplyUnleash(this.UnleashDefenseBoni, current.UnleashDefenseBoni);
				}
				long num = current.Level.ToLong();
				if (current.TypeEnum == Might.MightType.physical_hp)
				{
					this.PhysicalHPMod = (int)(100L + num);
				}
				else if (current.TypeEnum == Might.MightType.physical_attack)
				{
					this.PhysicalAttackMod = (int)(25L + num);
				}
				else if (current.TypeEnum == Might.MightType.mystic_def)
				{
					this.MysticDefMod = (int)(100L + num);
				}
				else if (current.TypeEnum == Might.MightType.mystic_regen)
				{
					this.MysticRegenMod = (int)(100L + num);
				}
				else if (current.TypeEnum == Might.MightType.battle)
				{
					this.BattleAttackMod = (int)(100L + num * 2L);
				}
				else if (current.TypeEnum == Might.MightType.autofill_gen)
				{
					this.ClonesDifGenMod = (int)(100L + num);
				}
				else if (current.TypeEnum == Might.MightType.planet_power)
				{
					this.ClonesPlanetMod = (int)(100L + num);
				}
				else if (current.TypeEnum == Might.MightType.powersurge_speed)
				{
					this.PowerSurgeMod = (int)(100L + num);
				}
			}
			double num2 = (double)this.PhysicalAttackMod / 100.0;
			this.physicalInfo = ((double)this.PhysicalHPMod / 10.0).ToString("0.00") + " x HP + " + num2.ToString("0.00") + " x Attack Power";
			this.physicalInfoDraw = string.Empty;
			if (this.Multiplier.DrawMultiPhysical > 1)
			{
				this.physicalInfoDraw = "\nMulti from Lucky Draw: " + this.Multiplier.DrawMultiPhysical.ToGuiText(true);
			}
			if (this.PremiumBoni.CrystalBonusPhysical > 0)
			{
				this.physicalInfoDraw = string.Concat(new object[]
				{
					this.physicalInfoDraw,
					"\nBonus from equipped Crystals: ",
					this.PremiumBoni.CrystalBonusPhysical,
					"%."
				});
			}
			this.mysticInfo = ((double)this.MysticDefMod / 100.0).ToString("0.00") + " x ";
			this.mysticInfoDraw = string.Empty;
			if (this.Multiplier.DrawMultiMystic > 1)
			{
				this.mysticInfoDraw = "\nMulti from Lucky Draw: " + this.Multiplier.DrawMultiMystic.ToGuiText(true);
			}
			if (this.PremiumBoni.CrystalBonusMystic > 0)
			{
				this.mysticInfoDraw = string.Concat(new object[]
				{
					this.mysticInfoDraw,
					"\nBonus from equipped Crystals: ",
					this.PremiumBoni.CrystalBonusMystic,
					"%."
				});
			}
			double num3 = (double)this.BattleAttackMod / 100.0;
			this.battlePowerInfo = num3.ToString("0.00") + " x ";
			this.battlePowerInfoDraw = string.Empty;
			if (this.Multiplier.DrawMultiBattle > 1)
			{
				this.battlePowerInfoDraw = "\nMulti from Lucky Draw: " + this.Multiplier.DrawMultiBattle.ToGuiText(true);
			}
			if (this.PremiumBoni.CrystalBonusBattle > 0)
			{
				this.battlePowerInfoDraw = string.Concat(new object[]
				{
					this.battlePowerInfoDraw,
					"\nBonus from equipped Crystals: ",
					this.PremiumBoni.CrystalBonusBattle,
					"%."
				});
			}
			this.creatringInfo = Creation.CurrentCreationSpeedText(this) + "\n(1 Creation = 5 HP and 1/2 Attack Power)\nIncreases when creating things. ";
			if (this.Multiplier.DrawMultiCreating > 1)
			{
				this.creatringInfo = this.creatringInfo + "\nMulti from Lucky Draw: " + this.Multiplier.DrawMultiCreating.ToGuiText(true);
			}
			if (this.PremiumBoni.CrystalBonusCreation > 0)
			{
				this.creatringInfo = string.Concat(new object[]
				{
					this.creatringInfo,
					"\nBonus from equipped Crystals: ",
					this.PremiumBoni.CrystalBonusCreation,
					"%."
				});
			}
			this.attackInfo = "Physical x " + num2.ToString("0.00") + " + Creating x 0.5 + Battle x " + num3.ToString("0.00");
		}

		private int MultiplyUnleash(int first, int second)
		{
			if (first == 0 && second > 0)
			{
				first = 100 + second;
			}
			else if (first > 0 && second > 0)
			{
				first = first * (100 + second) / 100;
			}
			return first;
		}

		public bool GetAttacked(CDouble attackPower, long millisecs, bool isPierce)
		{
			CDouble cDouble = (attackPower - this.Defense) / 5000 * millisecs;
			if (isPierce)
			{
				cDouble = attackPower / 1000 * millisecs;
			}
			if (cDouble > 0)
			{
				this.CurrentHealth -= cDouble;
			}
			return this.CurrentHealth <= 0;
		}

		public void RecoverHealth(long millisecs)
		{
			this.recoverUpdateCount++;
			if (this.recoverUpdateCount > 30)
			{
				this.recoverUpdateCount = 0;
				this.HpRecover = this.MysticPower * this.MysticRegenMod / 2000000 * millisecs + 1;
				if (this.UnleashRegenBoni > 0)
				{
					this.HpRecover = this.HpRecover * this.UnleashRegenBoni / 100;
				}
				this.HpRecoverSec = this.HpRecover * 33;
			}
			this.CurrentHealth += this.HpRecover;
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

		public void RemoveAllClones(bool onlyBuildings)
		{
			foreach (Might current in this.AllMights)
			{
				current.RemoveCloneCount(current.ShadowCloneCount.ToInt());
				if (current.ShadowCloneCount < 0)
				{
					current.ShadowCloneCount = 0;
				}
			}
			foreach (Monument current2 in this.AllMonuments)
			{
				current2.RemoveCloneCount(current2.ShadowCloneCount);
				current2.Upgrade.RemoveCloneCount(current2.Upgrade.ShadowCloneCount);
				if (current2.ShadowCloneCount < 0)
				{
					current2.ShadowCloneCount = 0;
				}
				if (current2.Upgrade.ShadowCloneCount < 0)
				{
					current2.Upgrade.ShadowCloneCount = 0;
				}
			}
			foreach (GeneratorUpgrade current3 in this.Generator.Upgrades)
			{
				current3.RemoveCloneCount(current3.ShadowCloneCount);
				if (current3.ShadowCloneCount < 0)
				{
					current3.ShadowCloneCount = 0;
				}
			}
			this.Generator.RemoveCloneCount(this.Generator.ShadowCloneCount);
			this.HomePlanet.RemoveCloneCount(this.HomePlanet.ShadowCloneCount);
			if (this.Generator.ShadowCloneCount < 0)
			{
				this.Generator.ShadowCloneCount = 0;
			}
			if (this.HomePlanet.ShadowCloneCount < 0)
			{
				this.HomePlanet.ShadowCloneCount = 0;
			}
			this.Ext.Factory.RemoveCloneCount(this.Ext.Factory.DefenderClones.ToInt());
			if (this.Ext.Factory.DefenderClones < 0)
			{
				this.Ext.Factory.DefenderClones = 0;
			}
			foreach (FactoryModule current4 in this.Ext.Factory.AllModules)
			{
				current4.RemoveAllClones();
				if (current4.ShadowClones < 0)
				{
					current4.ShadowClones = 0;
				}
			}
			if (onlyBuildings)
			{
				return;
			}
			foreach (Fight current5 in this.AllFights)
			{
				current5.RemoveCloneCount(current5.ShadowCloneCount);
				if (current5.ShadowCloneCount < 0)
				{
					current5.ShadowCloneCount = 0;
				}
			}
			foreach (Training current6 in this.AllTrainings)
			{
				current6.RemoveCloneCount(current6.ShadowCloneCount);
				if (current6.ShadowCloneCount < 0)
				{
					current6.ShadowCloneCount = 0;
				}
			}
			foreach (Skill current7 in this.AllSkills)
			{
				current7.RemoveCloneCount(current7.ShadowCloneCount);
				if (current7.ShadowCloneCount < 0)
				{
					current7.ShadowCloneCount = 0;
				}
			}
			foreach (Pet current8 in this.Ext.AllPets)
			{
				if (current8.IsUnlocked)
				{
					current8.RemoveCloneCount(current8.ShadowCloneCount.ToInt());
					if (current8.ShadowCloneCount < 0)
					{
						current8.ShadowCloneCount = 0;
					}
				}
			}
			this.Clones.InUse = 0;
			if (this.Clones.Count < 0)
			{
				this.Clones.Count = 0;
			}
		}

		public void CheckForCheats()
		{
			CDouble cDouble = 0;
			bool flag = false;
			foreach (Fight current in this.AllFights)
			{
				current.ShadowCloneCount.Round();
				if (current.ShadowCloneCount < 0)
				{
					flag = true;
				}
				cDouble += current.ShadowCloneCount;
			}
			foreach (Training current2 in this.AllTrainings)
			{
				current2.ShadowCloneCount.Round();
				if (current2.ShadowCloneCount < 0)
				{
					flag = true;
				}
				cDouble += current2.ShadowCloneCount;
				if (current2.CurrentDuration < 0L)
				{
					current2.CurrentDuration = 0L;
				}
			}
			foreach (Skill current3 in this.AllSkills)
			{
				current3.ShadowCloneCount.Round();
				if (current3.ShadowCloneCount < 0)
				{
					flag = true;
				}
				cDouble += current3.ShadowCloneCount;
				if (current3.Extension.SkillId != current3.EnumValue)
				{
					current3.Extension = new SkillExtension(current3.EnumValue);
				}
				if (current3.CurrentDuration < 0L)
				{
					current3.CurrentDuration = 0L;
				}
				if (current3.Extension.UsageCount < 0L)
				{
					current3.Extension.UsageCount = 0L;
				}
			}
			foreach (Monument current4 in this.AllMonuments)
			{
				current4.ShadowCloneCount.Round();
				current4.Upgrade.ShadowCloneCount.Round();
				if (current4.ShadowCloneCount < 0)
				{
					flag = true;
				}
				if (current4.Upgrade.ShadowCloneCount < 0)
				{
					flag = true;
				}
				cDouble += current4.ShadowCloneCount;
				cDouble += current4.Upgrade.ShadowCloneCount;
			}
			foreach (Might current5 in this.AllMights)
			{
				current5.ShadowCloneCount.Round();
				if (current5.ShadowCloneCount < 0)
				{
					flag = true;
				}
				cDouble += current5.ShadowCloneCount;
			}
			this.HomePlanet.RoudClones();
			if (this.HomePlanet.ShadowCloneCount < 0)
			{
				flag = true;
			}
			cDouble += this.HomePlanet.ShadowCloneCount;
			this.Generator.ShadowCloneCount.Round();
			if (this.Generator.ShadowCloneCount < 0)
			{
				flag = true;
			}
			cDouble += this.Generator.ShadowCloneCount;
			foreach (GeneratorUpgrade current6 in this.Generator.Upgrades)
			{
				current6.ShadowCloneCount.Round();
				if (current6.ShadowCloneCount < 0)
				{
					flag = true;
				}
				cDouble += current6.ShadowCloneCount;
			}
			foreach (Pet current7 in this.Ext.AllPets)
			{
				if (current7.IsUnlocked)
				{
					current7.ShadowCloneCount.Round();
					if (current7.ShadowCloneCount < 0)
					{
						flag = true;
					}
					cDouble += current7.ShadowCloneCount.ToInt();
				}
			}
			cDouble += this.Ext.Factory.DefenderClones;
			foreach (FactoryModule current8 in this.Ext.Factory.AllModules)
			{
				cDouble += current8.ShadowClones;
			}
			cDouble.Round();
			if (cDouble.ToInt() > this.Clones.MaxShadowClones.ToInt() || flag)
			{
				this.RemoveAllClones(false);
			}
			if (cDouble < 0 || cDouble > this.Clones.AbsoluteMaximum || this.Statistic.MonumentsCreated > 999999999)
			{
				this.PossibleCheater = true;
				this.ShouldSubmitScore = false;
			}
			int num = (int)((this.Statistic.TimePlayed / 1000L + this.Statistic.TimeOffline) / 3600L / 24L);
			if (this.Statistic.TotalRebirths < 100 && this.Statistic.MonumentsCreated > 2500 * (this.Statistic.TotalRebirths + num))
			{
				this.PossibleCheater = true;
			}
			if (this.Statistic.TotalRebirths < 500 && this.Statistic.MonumentsCreated > 5000 * (this.Statistic.TotalRebirths + num))
			{
				this.PossibleCheater = true;
			}
			string[] array = null;
			if (!string.IsNullOrEmpty(this.PremiumBoni.SteamPurchasedOrderIds))
			{
				array = this.PremiumBoni.SteamPurchasedOrderIds.Split(new char[]
				{
					','
				});
			}
			int num2 = this.PremiumBoni.TotalItemsBought;
			if (array != null)
			{
				num2 += array.Length;
			}
			int num3 = num;
			if (num2 > 0)
			{
				num3 = num3 * 2 + num2 * 50;
				this.PossibleCheater = false;
			}
			int num4 = this.PremiumBoni.GPFromLuckyDraws.ToInt();
			if (num4 > 5 * num3 + 200)
			{
				num4 = 5 * num3 + 200;
			}
			this.Clones.InUse = this.Clones.MaxShadowClones - (this.Clones.MaxShadowClones - cDouble.ToInt());
			int num5 = 20 + this.Statistic.TotalGodsDefeated.ToInt() + this.HomePlanet.TotalGainedGodPower + this.Statistic.ArtyChallengesFinished.ToInt() * 200 + this.Statistic.UltimateBaalChallengesFinished.ToInt() * 100 + this.Statistic.DoubleRebirthChallengesFinished.ToInt() * 10 + this.Statistic.GPFromBlackHole.ToInt() + this.Statistic.GPFromBlackHoleUpgrade.ToInt();
			num5 = num5 + num4 + this.PremiumBoni.GodPowerBought.ToInt() + 25 + this.PremiumBoni.GodPowerFromCrystals.ToInt() + this.PremiumBoni.GodPowerFromPets.ToInt();
			if (this.PremiumBoni.TotalItemsBought > 0)
			{
				num5 = num5 + this.PremiumBoni.TotalItemsBought * 150 + 150;
			}
			int num6 = this.PremiumBoni.CalculateGPSpent(this) + this.PremiumBoni.GodPower;
			if (this.PossibleCheater || num6 > num5 || this.Clones.InUse > this.Clones.MaxShadowClones || this.Clones.InUse < 0)
			{
				this.PossibleCheater = true;
				this.ShouldSubmitScore = false;
				if (this.Clones.InUse < 0)
				{
					this.Clones.InUse = 0;
				}
			}
			else
			{
				this.PossibleCheater = false;
			}
		}

		public CDouble GetAvailableClones(bool isFight = false)
		{
			CDouble cDouble = this.GameSettings.SavedClonesForFight;
			if (isFight)
			{
				cDouble = 0;
			}
			else
			{
				int value = (this.Clones.AbsoluteMaximum - this.Clones.Count).ToInt();
				cDouble -= value;
				if (cDouble < 0)
				{
					cDouble = 0;
				}
				cDouble.Round();
				foreach (Fight current in this.AllFights)
				{
					cDouble -= current.ShadowCloneCount;
				}
				foreach (Pet current2 in this.Ext.AllPets)
				{
					cDouble -= current2.ShadowCloneCount;
				}
			}
			if (cDouble < 0)
			{
				cDouble = 0;
			}
			return this.Clones.IdleClones() - cDouble;
		}

		public long CreationSpeed(long ms)
		{
			if (this.CreatingSpeedBoniDuration > 0L)
			{
				ms *= 3L;
			}
			return (ms * (long)this.PremiumBoni.CreationDopingDivider * (100 + this.PremiumBoni.CreatingSpeedUpPercent(true)) / 100).ToLong();
		}

		public CDouble DivinityGainSec(bool withDivGen = true)
		{
			CDouble cDouble = 1;
			foreach (Fight current in this.AllFights)
			{
				if (current.ShadowCloneCount > 0)
				{
					cDouble += current.MoneyGain;
				}
			}
			cDouble = cDouble * 1000 / 30;
			this.DivGainSecFights = cDouble;
			this.DivGainText = "\nYou gain " + this.DivGainSecFights.GuiText + " divinity / s if all fights are capped.";
			if (this.PremiumBoni.CrystalBonusDivinity > 0)
			{
				this.DivGainText = this.DivGainText + "\n" + (this.DivGainSecFights * (100 + this.PremiumBoni.CrystalBonusDivinity) / 100).GuiText + " total with the bonus of your equipped crystals.";
			}
			if (withDivGen && this.Generator != null && this.Generator.IsBuilt)
			{
				cDouble += this.Generator.DivinitySec;
			}
			return cDouble;
		}

		public void UpdateAllInfoTexts()
		{
			foreach (Training current in this.AllTrainings)
			{
				current.ShouldUpdateText = true;
			}
			foreach (Skill current2 in this.AllSkills)
			{
				current2.ShouldUpdateText = true;
			}
			foreach (Fight current3 in this.AllFights)
			{
				current3.ShouldUpdateText = true;
			}
			foreach (Creation current4 in this.AllCreations)
			{
				current4.ShouldUpdateText = true;
			}
			foreach (Monument current5 in this.AllMonuments)
			{
				current5.ShouldUpdateText = true;
			}
			this.Generator.ShouldUpdateText = true;
			foreach (GeneratorUpgrade current6 in this.Generator.Upgrades)
			{
				current6.ShouldUpdateText = true;
			}
			Creation.UpdateText = true;
			this.UpdateMaxHealth = true;
			this.UpdateDefense = true;
			this.UpdateAttack = true;
			this.UpdatePhysical = true;
			this.UpdateMystic = true;
			this.UpdateBattle = true;
			this.UpdatePowerLevel = true;
			this.UpdateCreating = true;
		}

		public void CheckMight()
		{
			bool flag = true;
			bool flag2 = false;
			foreach (Might current in this.AllMights)
			{
				if (current.ShadowCloneCount > 0)
				{
					flag2 = true;
				}
				if (current.NextAt == 0 || current.NextAt > current.Level)
				{
					flag = false;
				}
			}
			if (flag && flag2)
			{
				foreach (Might current2 in this.AllMights)
				{
					current2.RemoveCloneCount(current2.ShadowCloneCount.ToInt());
				}
			}
		}

		public void CheckIfAchievementChallengeFinished()
		{
			bool flag = true;
			foreach (Achievement current in this.TrainingAchievements)
			{
				if (!current.Reached)
				{
					flag = false;
					break;
				}
			}
			foreach (Achievement current2 in this.SkillAchievements)
			{
				if (!current2.Reached)
				{
					flag = false;
					break;
				}
			}
			foreach (Achievement current3 in this.FightingAchievements)
			{
				if (!current3.Reached)
				{
					flag = false;
					break;
				}
			}
			bool flag2 = flag;
			foreach (Achievement current4 in this.CreatingAchievements)
			{
				if (!current4.Reached)
				{
					flag = false;
					break;
				}
			}
			if (flag2)
			{
				Pet pet = App.State.Ext.AllPets.First((Pet x) => x.TypeEnum == PetType.Camel);
				if (!pet.IsUnlocked)
				{
					bool flag3 = false;
					foreach (Creation current5 in App.State.AllCreations)
					{
						if (current5.GodToDefeat.IsDefeated && current5.TypeEnum != Creation.CreationType.Shadow_clone)
						{
							flag3 = true;
							break;
						}
					}
					if (!flag3)
					{
						pet.Unlock();
					}
				}
			}
			Pet pet2 = App.State.Ext.AllPets.FirstOrDefault((Pet x) => x.TypeEnum == PetType.Goat);
			if (!pet2.IsUnlocked && Pet.CheckGoatUnlock())
			{
				pet2.Unlock();
			}
			if (!this.Statistic.HasStartedAchievementChallenge)
			{
				return;
			}
			if (flag)
			{
				Statistic expr_25B = this.Statistic;
				expr_25B.AchievementChallengesFinished = ++expr_25B.AchievementChallengesFinished;
				this.Statistic.HasStartedAchievementChallenge = false;
				GuiBase.ShowToast("You finished your Achievement Challenge! Your achievements will now increase your multiplier by 1% more.");
			}
		}

		public void CheckForAchievements()
		{
			using (List<Training>.Enumerator enumerator = this.AllTrainings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Training training = enumerator.Current;
					Achievement achievement = this.TrainingAchievements.FirstOrDefault((Achievement x) => x.IntEnum == training.EnumValue && !x.Reached);
					if (achievement != null)
					{
						achievement.CountNeeded.Round();
						training.Level.Round();
						if (achievement.CountNeeded <= training.Level)
						{
							Log.Info("Achievement reached: " + achievement.Description);
							GuiBase.ShowAchievementReached(achievement);
							achievement.Reached = true;
							achievement.ShowRealName = true;
							this.Multiplier.AchievementMultiPhysical += achievement.MultiplierBoni;
							this.Multiplier.AchievementMultiPhysicalRebirth += achievement.MultiplierBoniRebirth;
							Statistic expr_DF = this.Statistic;
							expr_DF.TotalAchievements = ++expr_DF.TotalAchievements;
							for (achievement = this.TrainingAchievements.FirstOrDefault((Achievement x) => x.IntEnum == training.EnumValue && !x.ShowRealName); achievement != null; achievement = this.TrainingAchievements.FirstOrDefault((Achievement x) => x.IntEnum == training.EnumValue && !x.ShowRealName))
							{
								achievement.ShowRealName = true;
							}
						}
					}
				}
			}
			using (List<Skill>.Enumerator enumerator2 = this.AllSkills.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Skill skill = enumerator2.Current;
					Achievement achievement2 = this.SkillAchievements.FirstOrDefault((Achievement x) => x.IntEnum == skill.EnumValue && !x.Reached);
					if (achievement2 != null)
					{
						achievement2.CountNeeded.Round();
						skill.Level.Round();
						if (achievement2.CountNeeded <= skill.Level)
						{
							Log.Info("Achievement reached: " + achievement2.Description);
							GuiBase.ShowAchievementReached(achievement2);
							achievement2.Reached = true;
							achievement2.ShowRealName = true;
							this.Multiplier.AchievementMultiMystic += achievement2.MultiplierBoni;
							this.Multiplier.AchievementMultiMysticRebirth += achievement2.MultiplierBoniRebirth;
							Statistic expr_23E = this.Statistic;
							expr_23E.TotalAchievements = ++expr_23E.TotalAchievements;
							for (achievement2 = this.SkillAchievements.FirstOrDefault((Achievement x) => x.IntEnum == skill.EnumValue && !x.ShowRealName); achievement2 != null; achievement2 = this.SkillAchievements.FirstOrDefault((Achievement x) => x.IntEnum == skill.EnumValue && !x.ShowRealName))
							{
								achievement2.ShowRealName = true;
							}
						}
					}
				}
			}
			using (List<Fight>.Enumerator enumerator3 = this.AllFights.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					Fight fight = enumerator3.Current;
					Achievement achievement3 = this.FightingAchievements.FirstOrDefault((Achievement x) => x.IntEnum == fight.EnumValue && !x.Reached);
					if (achievement3 != null)
					{
						achievement3.CountNeeded.Round();
						fight.Level.Round();
						if (achievement3.CountNeeded <= fight.Level)
						{
							Log.Info("Achievement reached: " + achievement3.Description);
							GuiBase.ShowAchievementReached(achievement3);
							achievement3.Reached = true;
							achievement3.ShowRealName = true;
							this.Multiplier.AchievementMultiBattle += achievement3.MultiplierBoni;
							this.Multiplier.AchievementMultiBattleRebirth += achievement3.MultiplierBoniRebirth;
							Statistic expr_3A4 = this.Statistic;
							expr_3A4.TotalAchievements = ++expr_3A4.TotalAchievements;
							for (achievement3 = this.FightingAchievements.FirstOrDefault((Achievement x) => x.IntEnum == fight.EnumValue && !x.ShowRealName); achievement3 != null; achievement3 = this.FightingAchievements.FirstOrDefault((Achievement x) => x.IntEnum == fight.EnumValue && !x.ShowRealName))
							{
								achievement3.ShowRealName = true;
							}
						}
					}
				}
			}
		}

		public void RecalculateAchievementMultis()
		{
			CDouble achievementMultiPhysicalRebirth = this.Multiplier.AchievementMultiPhysicalRebirth;
			CDouble achievementMultiMysticRebirth = this.Multiplier.AchievementMultiMysticRebirth;
			CDouble achievementMultiBattleRebirth = this.Multiplier.AchievementMultiBattleRebirth;
			this.Multiplier.AchievementMultiPhysicalRebirth = 1;
			this.Multiplier.AchievementMultiMysticRebirth = 1;
			this.Multiplier.AchievementMultiBattleRebirth = 50;
			using (List<Training>.Enumerator enumerator = this.AllTrainings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Training training = enumerator.Current;
					List<Achievement> list = this.TrainingAchievements.FindAll((Achievement x) => x.IntEnum == training.EnumValue);
					if (list.Count > 0)
					{
						foreach (Achievement current in list)
						{
							current.CountNeeded.Round();
							training.Level.Round();
							if (current.CountNeeded <= training.Level)
							{
								current.Reached = true;
								this.Multiplier.AchievementMultiPhysicalRebirth += current.MultiplierBoniRebirth;
							}
						}
					}
				}
			}
			using (List<Skill>.Enumerator enumerator3 = this.AllSkills.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					Skill skill = enumerator3.Current;
					List<Achievement> list2 = this.SkillAchievements.FindAll((Achievement x) => x.IntEnum == skill.EnumValue);
					if (list2.Count > 0)
					{
						foreach (Achievement current2 in list2)
						{
							current2.CountNeeded.Round();
							skill.Level.Round();
							if (current2.CountNeeded <= skill.Level)
							{
								current2.Reached = true;
								this.Multiplier.AchievementMultiMysticRebirth += current2.MultiplierBoniRebirth;
							}
						}
					}
				}
			}
			using (List<Fight>.Enumerator enumerator5 = this.AllFights.GetEnumerator())
			{
				while (enumerator5.MoveNext())
				{
					Fight fight = enumerator5.Current;
					List<Achievement> list3 = this.FightingAchievements.FindAll((Achievement x) => x.IntEnum == fight.EnumValue);
					if (list3.Count > 0)
					{
						foreach (Achievement current3 in list3)
						{
							current3.CountNeeded.Round();
							fight.Level.Round();
							if (current3.CountNeeded <= fight.Level)
							{
								current3.Reached = true;
								this.Multiplier.AchievementMultiBattleRebirth += current3.MultiplierBoniRebirth;
							}
						}
					}
				}
			}
			if (achievementMultiPhysicalRebirth > this.Multiplier.AchievementMultiPhysicalRebirth)
			{
				Log.Info(string.Concat(new object[]
				{
					"Phys Before: ",
					achievementMultiPhysicalRebirth,
					" - after: ",
					this.Multiplier.AchievementMultiPhysicalRebirth
				}));
			}
			if (achievementMultiMysticRebirth > this.Multiplier.AchievementMultiMysticRebirth)
			{
				Log.Info(string.Concat(new object[]
				{
					"Mys Before: ",
					achievementMultiMysticRebirth,
					" - after: ",
					this.Multiplier.AchievementMultiMysticRebirth
				}));
			}
			if (achievementMultiBattleRebirth > this.Multiplier.AchievementMultiBattleRebirth)
			{
				Log.Info(string.Concat(new object[]
				{
					"Battle Before: ",
					achievementMultiBattleRebirth,
					" - after: ",
					this.Multiplier.AchievementMultiBattleRebirth
				}));
			}
		}

		public void CheckForAchievement(Creation item)
		{
			Achievement achievement = this.CreatingAchievements.FirstOrDefault((Achievement x) => x.IntEnum == (int)item.TypeEnum && !x.Reached);
			if (achievement == null)
			{
				return;
			}
			achievement.CountNeeded.Round();
			item.TotalCreated.Round();
			if (achievement.CountNeeded <= item.TotalCreated)
			{
				Log.Info("Achievement reached: " + achievement.Description);
				GuiBase.ShowAchievementReached(achievement);
				achievement.Reached = true;
				achievement.ShowRealName = true;
				this.Multiplier.AchievementMultiCreating += achievement.MultiplierBoni;
				this.Multiplier.AchievementMultiCreatingRebirth += achievement.MultiplierBoniRebirth;
				Statistic expr_C9 = this.Statistic;
				expr_C9.TotalAchievements = ++expr_C9.TotalAchievements;
				item.ShouldUpdateText = true;
				this.CheckForAchievement(item);
			}
		}

		public void InitAchievementNames()
		{
			using (List<Achievement>.Enumerator enumerator = this.TrainingAchievements.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Achievement achievement = enumerator.Current;
					TrainingBase trainingBase = this.AllTrainings.FirstOrDefault((Training x) => x.EnumValue == achievement.IntEnum);
					if (trainingBase != null)
					{
						if (trainingBase.Level > 0)
						{
							achievement.ShowRealName = true;
						}
						if (achievement.CountNeeded <= trainingBase.Level)
						{
							achievement.Reached = true;
						}
					}
					achievement.MultiplierBoni = achievement.MultiplierBoni;
				}
			}
			using (List<Achievement>.Enumerator enumerator2 = this.SkillAchievements.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Achievement achievement = enumerator2.Current;
					TrainingBase trainingBase2 = this.AllSkills.FirstOrDefault((Skill x) => x.EnumValue == achievement.IntEnum);
					if (trainingBase2 != null)
					{
						if (trainingBase2.Level > 0)
						{
							achievement.ShowRealName = true;
						}
						if (achievement.CountNeeded <= trainingBase2.Level)
						{
							achievement.Reached = true;
						}
					}
					achievement.MultiplierBoni = achievement.MultiplierBoni;
				}
			}
			using (List<Achievement>.Enumerator enumerator3 = this.FightingAchievements.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					Achievement achievement = enumerator3.Current;
					TrainingBase trainingBase3 = this.AllFights.FirstOrDefault((Fight x) => x.EnumValue == achievement.IntEnum);
					if (trainingBase3 != null)
					{
						if (trainingBase3.Level > 0)
						{
							achievement.ShowRealName = true;
						}
						if (achievement.CountNeeded <= trainingBase3.Level)
						{
							achievement.Reached = true;
						}
					}
					achievement.MultiplierBoni = achievement.MultiplierBoni;
				}
			}
			foreach (Achievement current in this.CreatingAchievements)
			{
				current.MultiplierBoni = current.MultiplierBoni;
			}
		}

		public void AddMultisFromGod()
		{
			if (this.Multiplier.GodMultiFromRebirth == 0)
			{
				return;
			}
			CDouble cDouble = this.Multiplier.GodMultiFromRebirth;
			if (this.Statistic.AchievementChallengesFinished > 0)
			{
				CDouble leftSide = this.Statistic.AchievementChallengesFinished.ToNextInt();
				if (leftSide > 50)
				{
					leftSide = 50;
				}
				cDouble += leftSide * 0.01;
			}
			foreach (Achievement current in this.TrainingAchievements)
			{
				if (current.MultiplierBoni > 20)
				{
					return;
				}
				current.MultiplierBoni *= cDouble;
				current.MultiplierBoniRebirth *= cDouble;
			}
			foreach (Achievement current2 in this.SkillAchievements)
			{
				current2.MultiplierBoni *= cDouble;
				current2.MultiplierBoniRebirth *= cDouble;
			}
			foreach (Achievement current3 in this.FightingAchievements)
			{
				current3.MultiplierBoni *= cDouble;
				current3.MultiplierBoniRebirth *= cDouble;
			}
			foreach (Achievement current4 in this.CreatingAchievements)
			{
				current4.MultiplierBoni *= cDouble;
				current4.MultiplierBoniRebirth *= cDouble;
			}
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.Money.Serialize());
			Conv.AppendValue(stringBuilder, "b", this.CurrentHealth.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.TimeStampGameClosed);
			Conv.AppendValue(stringBuilder, "d", this.CreatingPower.Serialize());
			Conv.AppendValue(stringBuilder, "e", this.Clones.Serialize());
			Conv.AppendValue(stringBuilder, "g", this.Title);
			Conv.AppendList<Training>(stringBuilder, this.AllTrainings, "h");
			Conv.AppendList<Creation>(stringBuilder, this.AllCreations, "i");
			Conv.AppendList<Skill>(stringBuilder, this.AllSkills, "j");
			Conv.AppendList<Fight>(stringBuilder, this.AllFights, "k");
			Conv.AppendValue(stringBuilder, "l", this.CloneAttackDivider);
			Conv.AppendValue(stringBuilder, "m", this.CloneDefenseDivider);
			Conv.AppendValue(stringBuilder, "n", this.CloneHealthDivider);
			Conv.AppendValue(stringBuilder, "o", this.Multiplier.Serialize());
			Conv.AppendValue(stringBuilder, "p", this.PremiumBoni.Serialize());
			Conv.AppendValue(stringBuilder, "q", this.ShouldSubmitScore.ToString());
			Conv.AppendValue(stringBuilder, "r", this.KongUserId);
			Conv.AppendValue(stringBuilder, "s", this.KongUserName);
			Conv.AppendValue(stringBuilder, "t", this.IsTutorialShown.ToString());
			Conv.AppendValue(stringBuilder, "u", this.IsGuestMsgShown.ToString());
			Conv.AppendValue(stringBuilder, "x", this.Statistic.Serialize());
			List<AchievementId> listToSave = Achievement.AchievementsToIdList(this.TrainingAchievements);
			Conv.AppendList<AchievementId>(stringBuilder, listToSave, "y");
			listToSave = Achievement.AchievementsToIdList(this.SkillAchievements);
			Conv.AppendList<AchievementId>(stringBuilder, listToSave, "z");
			listToSave = Achievement.AchievementsToIdList(this.FightingAchievements);
			Conv.AppendList<AchievementId>(stringBuilder, listToSave, "A");
			listToSave = Achievement.AchievementsToIdList(this.CreatingAchievements);
			Conv.AppendList<AchievementId>(stringBuilder, listToSave, "B");
			Conv.AppendValue(stringBuilder, "C", this.IsMonumentUnlocked.ToString());
			Conv.AppendList<Monument>(stringBuilder, this.AllMonuments, "D");
			Conv.AppendValue(stringBuilder, "E", this.IsBuyUnlocked.ToString());
			Conv.AppendValue(stringBuilder, "F", this.PhysicalPowerBase.Serialize());
			Conv.AppendValue(stringBuilder, "G", this.MysticPowerBase.Serialize());
			Conv.AppendValue(stringBuilder, "H", this.BattlePowerBase.Serialize());
			Conv.AppendValue(stringBuilder, "I", this.CreatingPowerBase.Serialize());
			Conv.AppendValue(stringBuilder, "J", this.IsUpgradeUnlocked.ToString());
			Conv.AppendValue(stringBuilder, "K", this.Generator.Serialize());
			Conv.AppendValue(stringBuilder, "N", this.TitleGod);
			Conv.AppendValue(stringBuilder, "O", this.GameSettings.Serialize());
			Conv.AppendValue(stringBuilder, "P", this.PrinnyBaal.Serialize());
			Conv.AppendValue(stringBuilder, "Q", this.Avatar.Serialize());
			Conv.AppendValue(stringBuilder, "R", this.KredProblems.Serialize());
			Conv.AppendValue(stringBuilder, "S", this.Crits.Serialize());
			Conv.AppendValue(stringBuilder, "T", this.HomePlanet.Serialize());
			Conv.AppendValue(stringBuilder, "U", this.CreatingSpeedBoniDuration);
			Conv.AppendList<Might>(stringBuilder, this.AllMights, "V");
			Conv.AppendValue(stringBuilder, "W", this.AvatarName);
			Conv.AppendValue(stringBuilder, "X", this.Ext.Serialize());
			Conv.AppendValue(stringBuilder, "Y", this.IsBlackListed.ToString());
			Conv.AppendValue(stringBuilder, "Z", this.IsSocialDialogShown.ToString());
			Conv.AppendValue(stringBuilder, NS.n1.Nr(), this.SteamId);
			Conv.AppendValue(stringBuilder, NS.n2.Nr(), this.SteamName);
			Conv.AppendValue(stringBuilder, NS.n3.Nr(), this.AndroidId);
			Conv.AppendValue(stringBuilder, NS.n4.Nr(), this.AndroidName);
			Conv.AppendValue(stringBuilder, NS.n5.Nr(), this.TimeStampGameClosedOfflineMS);
			return Conv.ToBase64(stringBuilder.ToString(), "GameState");
		}

		internal static GameState FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				return new GameState(true, 0);
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "GameState");
			GameState state = new GameState(false, 0);
			state.OldCreatingPower = new CDouble(Conv.getStringFromParts(parts, "d"));
			state.PhysicalPowerBase = new CDouble(Conv.getStringFromParts(parts, "F"));
			state.MysticPowerBase = new CDouble(Conv.getStringFromParts(parts, "G"));
			state.BattlePowerBase = new CDouble(Conv.getStringFromParts(parts, "H"));
			state.CreatingPowerBase = new CDouble(Conv.getStringFromParts(parts, "I"));
			state.Multiplier = Multi.FromString(Conv.getStringFromParts(parts, "o"));
			state.Money = new CDouble(Conv.getStringFromParts(parts, "a"));
			state.TimeStampGameClosed = Conv.getLongFromParts(parts, "c");
			state.CurrentHealth = new CDouble(Conv.getStringFromParts(parts, "b"));
			state.Clones = ShadowClone.FromString(Conv.getStringFromParts(parts, "e"));
			state.Title = Conv.getStringFromParts(parts, "g");
			state.CloneAttackDivider = Conv.getIntFromParts(parts, "l");
			if (state.CloneAttackDivider == 0)
			{
				state.CloneAttackDivider = 1000;
			}
			state.CloneDefenseDivider = Conv.getIntFromParts(parts, "m");
			if (state.CloneDefenseDivider == 0)
			{
				state.CloneDefenseDivider = 1000;
			}
			state.CloneHealthDivider = Conv.getIntFromParts(parts, "n");
			if (state.CloneHealthDivider == 0)
			{
				state.CloneHealthDivider = 1000;
			}
			state.PremiumBoni = Premium.FromString(Conv.getStringFromParts(parts, "p"));
			state.ShouldSubmitScore = Conv.getStringFromParts(parts, "q").ToLower().Equals("true");
			state.KongUserId = Conv.getStringFromParts(parts, "r");
			state.KongUserName = Conv.getStringFromParts(parts, "s");
			state.IsTutorialShown = Conv.getStringFromParts(parts, "t").ToLower().Equals("true");
			state.IsGuestMsgShown = Conv.getStringFromParts(parts, "u").ToLower().Equals("true");
			state.Statistic = Statistic.FromString(Conv.getStringFromParts(parts, "x"));
			string stringFromParts = Conv.getStringFromParts(parts, "h");
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
					state.AllTrainings.Add(Training.FromString(text));
				}
			}
			if (state.AllTrainings.Count == 0)
			{
				state.AllTrainings = Training.Initial();
			}
			stringFromParts = Conv.getStringFromParts(parts, "i");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array3 = array;
			for (int j = 0; j < array3.Length; j++)
			{
				string text2 = array3[j];
				if (!string.IsNullOrEmpty(text2))
				{
					state.AllCreations.Add(Creation.FromString(text2));
				}
			}
			if (state.AllCreations.Count == 0)
			{
				state.AllCreations = Creation.Initial();
			}
			stringFromParts = Conv.getStringFromParts(parts, "j");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array4 = array;
			for (int k = 0; k < array4.Length; k++)
			{
				string text3 = array4[k];
				if (!string.IsNullOrEmpty(text3))
				{
					state.AllSkills.Add(Skill.FromString(text3));
				}
			}
			if (state.AllSkills.Count == 0)
			{
				state.AllSkills = Skill.Initial();
			}
			stringFromParts = Conv.getStringFromParts(parts, "k");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array5 = array;
			for (int l = 0; l < array5.Length; l++)
			{
				string text4 = array5[l];
				if (!string.IsNullOrEmpty(text4))
				{
					state.AllFights.Add(Fight.FromString(text4));
				}
			}
			if (state.AllFights.Count == 0)
			{
				state.AllFights = Fight.Initial();
			}
			if (state.AllFights.Count == 28)
			{
				state.AllFights.Add(new Fight(Fight.FightType.genbu));
				state.AllFights.Add(new Fight(Fight.FightType.byakko));
				state.AllFights.Add(new Fight(Fight.FightType.suzaku));
				state.AllFights.Add(new Fight(Fight.FightType.seiryuu));
				state.AllFights.Add(new Fight(Fight.FightType.godzilla));
				state.AllFights.Add(new Fight(Fight.FightType.monster_queen));
			}
			stringFromParts = Conv.getStringFromParts(parts, "D");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array6 = array;
			for (int m = 0; m < array6.Length; m++)
			{
				string text5 = array6[m];
				if (!string.IsNullOrEmpty(text5))
				{
					state.AllMonuments.Add(Monument.FromString(text5));
				}
			}
			if (state.AllMonuments.Count == 0)
			{
				state.AllMonuments = Monument.Initial();
			}
			if (state.AllMonuments.Count == 7)
			{
				state.AllMonuments.Add(new Monument(Monument.MonumentType.black_hole));
			}
			Achievement.InitAchievements(state.Statistic.AchievementChallengesFinished.ToInt());
			stringFromParts = Conv.getStringFromParts(parts, "y");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			List<AchievementId> list = new List<AchievementId>();
			string[] array7 = array;
			for (int n = 0; n < array7.Length; n++)
			{
				string text6 = array7[n];
				if (!string.IsNullOrEmpty(text6))
				{
					list.Add(AchievementId.FromString(text6));
				}
			}
			state.TrainingAchievements = Achievement.TrainingAchievementsFromIdList(list);
			stringFromParts = Conv.getStringFromParts(parts, "z");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			list = new List<AchievementId>();
			string[] array8 = array;
			for (int num = 0; num < array8.Length; num++)
			{
				string text7 = array8[num];
				if (!string.IsNullOrEmpty(text7))
				{
					list.Add(AchievementId.FromString(text7));
				}
			}
			state.SkillAchievements = Achievement.SkillAchievementsFromIdList(list);
			stringFromParts = Conv.getStringFromParts(parts, "A");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			list = new List<AchievementId>();
			string[] array9 = array;
			for (int num2 = 0; num2 < array9.Length; num2++)
			{
				string text8 = array9[num2];
				if (!string.IsNullOrEmpty(text8))
				{
					list.Add(AchievementId.FromString(text8));
				}
			}
			state.FightingAchievements = Achievement.FightAchievementsFromIdList(list);
			stringFromParts = Conv.getStringFromParts(parts, "B");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			list = new List<AchievementId>();
			string[] array10 = array;
			for (int num3 = 0; num3 < array10.Length; num3++)
			{
				string text9 = array10[num3];
				if (!string.IsNullOrEmpty(text9))
				{
					list.Add(AchievementId.FromString(text9));
				}
			}
			state.CreatingAchievements = Achievement.CreationAchievementsFromIdList(list);
			state.AddMultisFromGod();
			state.IsMonumentUnlocked = Conv.getStringFromParts(parts, "C").ToLower().Equals("true");
			state.IsBuyUnlocked = Conv.getStringFromParts(parts, "E").ToLower().Equals("true");
			state.IsUpgradeUnlocked = Conv.getStringFromParts(parts, "J").ToLower().Equals("true");
			state.Generator = DivinityGenerator.FromString(Conv.getStringFromParts(parts, "K"));
			state.TitleGod = Conv.getStringFromParts(parts, "N");
			state.GameSettings = Settings.FromString(Conv.getStringFromParts(parts, "O"));
			state.PrinnyBaal = PBaal.FromString(Conv.getStringFromParts(parts, "P"));
			if (state.GameSettings.LastCreation == null)
			{
				state.GameSettings.LastCreation = state.AllCreations[0];
			}
			state.Avatar = AvatarOptions.FromString(Conv.getStringFromParts(parts, "Q"));
			state.KredProblems = PlayerKredProblems.FromString(Conv.getStringFromParts(parts, "R"));
			state.Crits = Critical.FromString(Conv.getStringFromParts(parts, "S"));
			state.HomePlanet = Planet.FromString(Conv.getStringFromParts(parts, "T"));
			if (state.HomePlanet.IsCreated && state.HomePlanet.UpgradeLevel > 1 && state.HomePlanet.TotalGainedGodPower == 0)
			{
				state.HomePlanet.TotalGainedGodPower = (int)(state.Statistic.UBsDefeated.Double * 0.4);
				state.HomePlanet.ShadowCloneCount.Round();
			}
			if (state.Statistic.TotalPowersurge < state.HomePlanet.PowerSurgeMultiplier)
			{
				state.Statistic.TotalPowersurge = state.HomePlanet.PowerSurgeMultiplier;
			}
			if (state.Statistic.UltimateBaalChallengesFinished > 0 || state.Statistic.ArtyChallengesFinished > 0)
			{
				state.HomePlanet.UltimateBeingsV2[0].IsAvailable = true;
				foreach (UltimateBeing current in state.HomePlanet.UltimateBeings)
				{
					if (state.HomePlanet.UpgradeLevel > 4)
					{
						current.IsAvailable = true;
					}
				}
			}
			state.CreatingSpeedBoniDuration = Conv.getLongFromParts(parts, "U");
			if (state.PremiumBoni.StatisticMulti <= 0)
			{
				state.PremiumBoni.StatisticMulti = 1;
			}
			stringFromParts = Conv.getStringFromParts(parts, "V");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array11 = array;
			for (int num4 = 0; num4 < array11.Length; num4++)
			{
				string text10 = array11[num4];
				if (!string.IsNullOrEmpty(text10))
				{
					state.AllMights.Add(Might.FromString(text10));
				}
			}
			if (state.AllMights.Count == 0)
			{
				state.AllMights = Might.Initial();
			}
			state.AvatarName = Conv.getStringFromParts(parts, "W");
			if (string.IsNullOrEmpty(state.AvatarName))
			{
				state.AvatarName = state.KongUserName;
			}
			state.Ext = State2.Deserialize(Conv.getStringFromParts(parts, "X"));
			state.IsBlackListed = Conv.getStringFromParts(parts, "Y").ToLower().Equals("true");
			state.IsSocialDialogShown = Conv.getStringFromParts(parts, "Z").ToLower().Equals("true");
			state.SteamId = Conv.getStringFromParts(parts, NS.n1.Nr());
			state.SteamName = Conv.getStringFromParts(parts, NS.n2.Nr());
			state.AndroidId = Conv.getStringFromParts(parts, NS.n3.Nr());
			state.AndroidName = Conv.getStringFromParts(parts, NS.n4.Nr());
			state.TimeStampGameClosedOfflineMS = Conv.getLongFromParts(parts, NS.n5.Nr());
			state.Statistic.CalculateTotalPetGrowth(state.Ext.AllPets);
			if (state.CreatingPowerBase < 0)
			{
				state.CreatingPowerBase = 0;
			}
			try
			{
				if (state.SteamId == null)
				{
					state.SteamId = string.Empty;
				}
				if (state.SteamName == null)
				{
					state.SteamName = string.Empty;
				}
				if (state.KongUserId == null)
				{
					state.KongUserId = string.Empty;
				}
				if (state.KongUserName == null)
				{
					state.KongUserName = string.Empty;
				}
				if (state.KongUserId != null)
				{
					state.KongUserId = state.KongUserId.Trim();
				}
				int num5 = state.PremiumBoni.CalculateGPSpent(state);
				if (num5 > state.PremiumBoni.TotalGodPowerSpent)
				{
					state.PremiumBoni.TotalGodPowerSpent = num5;
				}
				StoryUi.SetUnlockedStoryParts(state);
				if (state.Statistic.UBsDefeated == 0)
				{
					foreach (UltimateBeing current2 in state.HomePlanet.UltimateBeings)
					{
						state.Statistic.UBsDefeated += current2.TimesDefeated;
					}
				}
				state.HomePlanet.InitUBMultipliers();
				state.Battle = new BattleState();
				if (state.Statistic.TotalRebirths == 0)
				{
					state.Statistic.TimePlayedSinceRebirth = state.Statistic.TimePlayed;
				}
				Creation creation = state.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Universe);
				if (creation != null && creation.GodToDefeat.IsDefeated)
				{
					state.PrinnyBaal.IsUnlocked = true;
				}
				if (state.GameSettings.LastCreation != null)
				{
					state.GameSettings.LastCreation = state.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == state.GameSettings.LastCreation.TypeEnum);
				}
				if (state.Clones.MaxShadowClones < state.Clones.IdleClones())
				{
					state.Clones.Count = state.Clones.MaxShadowClones;
				}
				if (state.GameSettings.Framerate == 0)
				{
					state.GameSettings.Framerate = 30;
				}
				if (!state.Generator.IsAvailable)
				{
					Monument monument = state.AllMonuments.FirstOrDefault((Monument x) => x.TypeEnum == Monument.MonumentType.temple_of_god);
					if (monument != null && monument.Level > 0)
					{
						state.Generator.IsAvailable = true;
					}
				}
				state.PremiumBoni.CheckIfGPIsAdjusted();
				foreach (Pet current3 in state.Ext.AllPets)
				{
					if (current3.ZeroHealthTime > 60000L)
					{
						current3.ZeroHealthTime = 60000L;
					}
					current3.ShadowCloneCount.Round();
					current3.BattleGrowth = Conv.RoundToOneFourth(current3.BattleGrowth.Double);
					current3.MysticGrowth = Conv.RoundToOneFourth(current3.MysticGrowth.Double);
					current3.PhysicalGrowth = Conv.RoundToOneFourth(current3.PhysicalGrowth.Double);
					if (current3.ShadowCloneCount < 0)
					{
						current3.ShadowCloneCount = 0;
					}
					current3.CalculateValues();
				}
				state.Multiplier.UpdatePetMultis(state);
				foreach (Might current4 in state.AllMights)
				{
					if (current4.TypeEnum == Might.MightType.physical_hp)
					{
						state.PhysicalHPMod = (100 + current4.Level).ToInt();
					}
					current4.ShadowCloneCount.Round();
				}
				state.CurrentHealth = new CDouble(Conv.getStringFromParts(parts, "b"));
				foreach (ClothingPart current5 in state.Avatar.ClothingParts)
				{
					if (current5.PermanentGPCost == 0 && current5.GodDefeatedTierNeeded <= state.Statistic.HighestGodDefeated)
					{
						current5.IsPermanentUnlocked = true;
					}
				}
				if (state.Statistic.HasStartedArtyChallenge)
				{
					state.Crits = new Critical();
				}
				if (state.Statistic.HasStartedUltimateBaalChallenge || state.Statistic.HasStartedArtyChallenge)
				{
					Premium premium = Premium.FromString(state.Statistic.PremiumStatsBeforeUBCChallenge);
					if (state.PremiumBoni.TotalItemsBought < premium.TotalItemsBought)
					{
						state.PremiumBoni.TotalItemsBought = premium.TotalItemsBought;
					}
				}
				state.PremiumBoni.CheckCrystalBonus(state);
				state.DivinityGainSec(false);
				foreach (PetCampaign current6 in state.Ext.AllCampaigns)
				{
					current6.InitPetsInCampaign(state);
					if (current6.Type == Campaigns.GodPower && state.PremiumBoni.GodPowerFromPets < current6.CampaingsFinished * 5)
					{
						state.PremiumBoni.GodPowerFromPets = current6.CampaingsFinished * 5;
					}
				}
				foreach (Monument current7 in state.AllMonuments)
				{
					if (!state.IsUpgradeUnlocked)
					{
						current7.Upgrade.Level = 0;
					}
				}
				if ("Ryu82".Equals(state.SteamName) && "bushackan".Equals(state.KongUserName))
				{
					state.SteamName = string.Empty;
					state.SteamId = string.Empty;
				}
				App.OfflineStatsChecked = false;
			}
			catch (Exception ex)
			{
				Log.Error(string.Empty + ex.StackTrace);
			}
			return state;
		}
	}
}
