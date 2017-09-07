using Assets.Scripts.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class BattleState
	{
		public enum BattleType
		{
			endless,
			jacky,
			cthulhu,
			doppel,
			developer,
			gods
		}

		internal EnemyData Enemies = new EnemyData();

		internal long GodSpeedModeDuration;

		internal long GearEyesDuration;

		internal List<DamageChange> DamageIncreases = new List<DamageChange>();

		internal List<DamageChange> DamageDecreases = new List<DamageChange>();

		internal List<EnemyData.Enemy> AllEnemies = new List<EnemyData.Enemy>();

		internal bool DoubleUp;

		internal bool DamageBlock;

		internal bool DamageReflect;

		internal int HitchanceBonus;

		internal int DodgeChance;

		internal int CounterChance;

		internal long totalFightTime;

		internal CDouble PlayerHp = 0;

		internal CDouble PlayerMaxHP = 0;

		internal EnemyData.Enemy CurrentEnemy = new EnemyData.Enemy("???", string.Empty, 0, 0, EnemyData.EnemyType.constantDamage, new List<EnemyData.SpecialAttackChance>());

		internal CDouble TotalPlayerDamage = 0;

		internal string SkillUseText = string.Empty;

		internal string EnemyDamageText = string.Empty;

		internal List<string> FightingLog = new List<string>();

		private BattleState.BattleType currentBattleType;

		internal int DefeatedEnemyCount;

		internal bool IsFighting;

		internal bool IsBattleFinished;

		public string BattleRewardText = string.Empty;

		internal CDouble[] last3EnemyAttacks = new CDouble[3];

		private int currentIntexOfEnemyAttacks;

		public static bool JackyLeeDefeated;

		private long enemyAttackTimer;

		private long totalFightTimeMilliSeconds;

		internal int DamageIncrease
		{
			get
			{
				int num = 100;
				foreach (DamageChange current in this.DamageIncreases)
				{
					num += current.ValuePercent;
				}
				return num;
			}
		}

		internal int DamageDecrease
		{
			get
			{
				int num = 100;
				foreach (DamageChange current in this.DamageDecreases)
				{
					num = num * (100 - current.ValuePercent) / 100;
				}
				return num;
			}
		}

		public string FightTimeString
		{
			get
			{
				return this.totalFightTimeMilliSeconds / 1000L + " sec - ";
			}
		}

		private long enemyAttackTime
		{
			get
			{
				if (this.GodSpeedModeDuration > 0L)
				{
					return 2000L;
				}
				return 1000L;
			}
		}

		public void StartFight(BattleState.BattleType type)
		{
			this.AllEnemies = new List<EnemyData.Enemy>();
			switch (type)
			{
			case BattleState.BattleType.endless:
				this.AllEnemies.Add(this.Enemies.Endless);
				break;
			case BattleState.BattleType.jacky:
				this.AllEnemies.Add(this.Enemies.Jacky);
				break;
			case BattleState.BattleType.cthulhu:
				this.AllEnemies.Add(this.Enemies.Cthulhu);
				break;
			case BattleState.BattleType.doppel:
				this.AllEnemies.Add(this.Enemies.Doppelganger);
				break;
			case BattleState.BattleType.developer:
				this.AllEnemies.Add(this.Enemies.Developer);
				this.AllEnemies.Add(this.Enemies.Creator);
				break;
			case BattleState.BattleType.gods:
				foreach (Creation current in App.State.AllCreations)
				{
					if (current.TypeEnum != Creation.CreationType.Shadow_clone)
					{
						God godToDefeat = current.GodToDefeat;
						List<EnemyData.SpecialAttackChance> list = new List<EnemyData.SpecialAttackChance>();
						list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.doubleDamage, 10));
						list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.tripleDamage, 3));
						list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.fiveDamage, 2));
						list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.tenDamage, 1));
						EnemyData.Enemy item = new EnemyData.Enemy(godToDefeat.Name, string.Empty, godToDefeat.MaxHealth, godToDefeat.Attack, EnemyData.EnemyType.constantDamage, list);
						this.AllEnemies.Add(item);
					}
				}
				break;
			}
			this.currentBattleType = type;
			this.IsFighting = true;
			this.IsBattleFinished = false;
			this.totalFightTime = 0L;
			this.PlayerHp = App.State.MaxHealth;
			this.PlayerMaxHP = App.State.MaxHealth;
			this.CurrentEnemy = this.AllEnemies[0];
			this.AllEnemies.RemoveAt(0);
			this.EnemyDamageText = string.Empty;
			this.SkillUseText = string.Empty;
			this.FightingLog = new List<string>();
			this.totalFightTimeMilliSeconds = 0L;
			this.enemyAttackTimer = 0L;
			this.DefeatedEnemyCount = 0;
			this.BattleRewardText = string.Empty;
			this.last3EnemyAttacks = new CDouble[3];
			this.currentIntexOfEnemyAttacks = 0;
			this.GodSpeedModeDuration = 0L;
			this.GearEyesDuration = 0L;
			this.DoubleUp = false;
			this.DamageBlock = false;
			this.DamageReflect = false;
			this.HitchanceBonus = 0;
			this.DodgeChance = 0;
			this.CounterChance = 0;
			this.DamageIncreases = new List<DamageChange>();
			this.DamageDecreases = new List<DamageChange>();
			this.TotalPlayerDamage = 0;
		}

		private CDouble randomize(CDouble baseValue)
		{
			int num = UnityEngine.Random.Range(0, 20);
			return baseValue * (90 + num) / 100;
		}

		public void UseSkill(Skill skill, bool usedKey = false)
		{
			int num = 500;
			if (usedKey)
			{
				num = 800;
			}
			foreach (Skill current in App.State.AllSkills)
			{
				if (current.Extension.CoolDownCurrent < (long)num)
				{
					current.Extension.CoolDownCurrent = (long)num;
				}
			}
			SkillExtension extension = skill.Extension;
			extension.CoolDownCurrent = extension.CoolDownBase;
			extension.UsageCount += 1L;
			if (skill.TypeEnum == Skill.SkillType.time_manipulation)
			{
				this.CurrentEnemy.TimeManipulate();
				CDouble cDouble = new CDouble();
				StringBuilder stringBuilder = new StringBuilder("You use ").Append(skill.Name).Append(" and the damages ");
				for (int i = 0; i < 3; i++)
				{
					if (this.last3EnemyAttacks[i] != null)
					{
						cDouble += this.last3EnemyAttacks[i];
						stringBuilder.Append(this.last3EnemyAttacks[i].ToGuiText(true));
						if (i == 0)
						{
							stringBuilder.Append(", ");
						}
						if (i == 1)
						{
							stringBuilder.Append(" and ");
						}
					}
				}
				this.PlayerHp += cDouble;
				this.SkillUseText = this.FightTimeString + stringBuilder.ToString() + " never occurred.";
				this.FightingLog.Add(this.SkillUseText);
				return;
			}
			if (extension.DamagePercent > 0)
			{
				int num2 = extension.DamagePercent;
				int num3 = extension.Hitcount;
				if (skill.TypeEnum == Skill.SkillType.unlimited_creation_works)
				{
					int num4 = 0;
					foreach (Creation current2 in App.State.AllCreations)
					{
						if (current2.CanBuy)
						{
							num4++;
						}
					}
					num2 *= num4;
				}
				else if (skill.TypeEnum == Skill.SkillType.ionioi_hero_summon)
				{
					num3 = App.State.Statistic.HighestGodDefeated.ToInt();
				}
				if (this.DoubleUp)
				{
					this.DoubleUp = false;
					num2 *= 2;
				}
				StringBuilder stringBuilder2 = new StringBuilder("You attack ").Append(this.CurrentEnemy.Name).Append(" with ").Append(skill.Name);
				int num5 = 0;
				for (int j = 0; j < num3; j++)
				{
					int num6 = UnityEngine.Random.Range(0, 100);
					if (extension.Hitchance + this.HitchanceBonus > num6)
					{
						num5++;
					}
				}
				this.HitchanceBonus = 0;
				if (num5 > 0)
				{
					CDouble cDouble2 = skill.Level;
					if (cDouble2 > 100000)
					{
						int num7 = cDouble2.ToInt() - 100000;
						if (num7 > 1100000)
						{
							cDouble2 = 150000 + (num7 - 1100000) / 100;
						}
						else
						{
							cDouble2 = 100000 + num7 / 20;
						}
					}
					CDouble cDouble3 = this.randomize(cDouble2 * num2 * this.DamageIncrease * App.State.Attack / 1000000000 * num5);
					Log.Info("playerDamage: " + cDouble3.ToGuiText(true));
					stringBuilder2.Append(" and hit ").Append(num5).Append(" times for ").Append(cDouble3.ToGuiText(true)).Append(" damage total.");
					this.TotalPlayerDamage += cDouble3;
					this.SkillUseText = this.FightTimeString + stringBuilder2.ToString();
					if (extension.HealPercent != 0)
					{
						StringBuilder stringBuilder3 = new StringBuilder();
						CDouble cDouble4 = extension.HealPercent * App.State.MaxHealth / 100;
						if (this.PlayerHp + cDouble4 > this.PlayerMaxHP)
						{
							cDouble4 = this.PlayerMaxHP - this.PlayerHp;
						}
						this.PlayerHp += cDouble4;
						if (cDouble4 > 0)
						{
							stringBuilder3.Append("\nYou heal yourself for " + cDouble4.ToGuiText(true)).Append(".");
						}
						else
						{
							stringBuilder3.Append("\nYou damage yourself for " + (cDouble4 * -1).ToGuiText(true)).Append(".");
						}
						this.SkillUseText += stringBuilder3.ToString();
					}
					this.FightingLog.Add(this.SkillUseText);
					if (this.PlayerHp <= 0)
					{
						GuiBase.ShowToast("You lose!");
						this.IsFighting = false;
						this.IsBattleFinished = true;
						this.GetReward(false);
						return;
					}
					if (this.CurrentEnemy.DoDamage(cDouble3))
					{
						this.SkillUseText = "You defeated " + this.CurrentEnemy.Name + "!";
						if (this.currentBattleType == BattleState.BattleType.developer)
						{
							this.SkillUseText = this.SkillUseText + " " + this.CurrentEnemy.Name + " evolved into ";
						}
						this.DefeatedEnemyCount++;
						Log.Info("AllEnemies.Count: " + this.AllEnemies.Count);
						if (this.AllEnemies.Count > 0)
						{
							this.CurrentEnemy = this.AllEnemies[0];
							this.AllEnemies.RemoveAt(0);
							if (this.currentBattleType == BattleState.BattleType.developer)
							{
								this.SkillUseText = this.SkillUseText + this.CurrentEnemy.Name + "!";
							}
						}
						else if (this.currentBattleType == BattleState.BattleType.gods)
						{
							CDouble cDouble5 = new CDouble("99999999999999999999999999999999999999999999999999999999000");
							int num8 = 1;
							for (int k = 0; k < this.DefeatedEnemyCount - 28; k++)
							{
								num8++;
								cDouble5 *= 100;
							}
							string name = "P. Baal";
							if (num8 > 1)
							{
								name = "P. Baal v " + num8;
							}
							List<EnemyData.SpecialAttackChance> list = new List<EnemyData.SpecialAttackChance>();
							list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.doubleDamage, 20));
							list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.tripleDamage, 10));
							list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.fiveDamage, 5));
							list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.tenDamage, 3));
							this.CurrentEnemy = new EnemyData.Enemy(name, string.Empty, cDouble5 * 1000, cDouble5, EnemyData.EnemyType.constantDamage, list);
						}
						else if (this.currentBattleType == BattleState.BattleType.endless)
						{
							string name2 = this.DefeatedEnemyCount + 1 + " Shadow Clones";
							List<EnemyData.SpecialAttackChance> list2 = new List<EnemyData.SpecialAttackChance>();
							list2.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.doubleDamage, 2 + this.DefeatedEnemyCount / 2));
							list2.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.tripleDamage, 1 + this.DefeatedEnemyCount / 3));
							list2.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.fiveDamage, 1 + this.DefeatedEnemyCount / 5));
							list2.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.tenDamage, 1 + this.DefeatedEnemyCount / 10));
							CDouble hp = App.State.PowerLevel * 5 * (this.DefeatedEnemyCount + 1);
							CDouble dam = App.State.PowerLevel / 300 * (this.DefeatedEnemyCount + 1);
							this.CurrentEnemy = new EnemyData.Enemy(name2, string.Empty, hp, dam, EnemyData.EnemyType.constantDamage, list2);
						}
						else
						{
							GuiBase.ShowToast("You win!");
							this.IsFighting = false;
							this.IsBattleFinished = true;
							this.GetReward(true);
						}
						this.FightingLog.Add(this.SkillUseText);
					}
				}
				else
				{
					stringBuilder2.Append(" and your attack misses!");
					this.SkillUseText = this.FightTimeString + stringBuilder2.ToString();
					this.FightingLog.Add(this.SkillUseText);
				}
			}
			else
			{
				StringBuilder stringBuilder4 = new StringBuilder(this.FightTimeString + "You use " + skill.Name).Append(" ");
				if (extension.DamageBlock)
				{
					this.DamageBlock = true;
					stringBuilder4.Append("and the next incoming damage will be blocked.");
				}
				if (extension.DamageReflect)
				{
					this.DamageReflect = true;
					stringBuilder4.Append("and the next incoming damage will be reflected.");
				}
				if (extension.DoubleUp)
				{
					this.DoubleUp = true;
					stringBuilder4.Append("and the next damage you do will be doubled.");
				}
				if (extension.GodSpeedModeDuration > 0L)
				{
					this.GodSpeedModeDuration = extension.GodSpeedModeDuration;
					stringBuilder4.Append("and you enter god speed mode for " + extension.GodSpeedModeDuration / 1000L + " seconds.");
				}
				if (extension.GearEyesDuration > 0L)
				{
					this.GearEyesDuration = extension.GearEyesDuration;
					stringBuilder4.Append("and the enemy is stopped for " + extension.GearEyesDuration / 1000L + " seconds.");
				}
				if (extension.HealPercent != 0)
				{
					CDouble cDouble6 = extension.HealPercent * App.State.MaxHealth / 100;
					if (this.PlayerHp + cDouble6 > this.PlayerMaxHP)
					{
						cDouble6 = this.PlayerMaxHP - this.PlayerHp;
					}
					this.PlayerHp += cDouble6;
					if (cDouble6 > 0)
					{
						stringBuilder4.Append("and you heal yourself for " + cDouble6.ToGuiText(true)).Append(".");
					}
					else
					{
						stringBuilder4.Append("and you damage yourself for " + (cDouble6 * -1).ToGuiText(true)).Append(".");
					}
				}
				if (extension.DodgeChance > 0)
				{
					this.DodgeChance = extension.DodgeChance;
					stringBuilder4.Append("and have a ").Append(extension.DodgeChance).Append(" % chance to dodge the next attack.");
				}
				if (extension.HitchanceBonus > 0)
				{
					this.HitchanceBonus = extension.HitchanceBonus;
					stringBuilder4.Append("and your next attack skill will have a ").Append(this.HitchanceBonus).Append(" % higher chance to hit.");
				}
				if (extension.CounterChance > 0)
				{
					this.CounterChance = extension.CounterChance;
					stringBuilder4.Append("and you have a ").Append(extension.CounterChance).Append(" % chance to counter the next attack.");
				}
				if (extension.DamageIncreaseDuration > 0L)
				{
					stringBuilder4.Append("and your damage is increased by ").Append(extension.DamageIncreasePercent).Append(" % for ").Append(extension.DamageIncreaseDuration / 1000L).Append(" seconds.");
					this.DamageIncreases.Add(new DamageChange(extension.DamageIncreasePercent, extension.DamageIncreaseDuration));
				}
				if (extension.DamageDecreaseDuration > 0L)
				{
					stringBuilder4.Append("and the damage you receive is decreased by ").Append(extension.DamageDecreasePercent).Append(" % for ").Append(extension.DamageDecreaseDuration / 1000L).Append(" seconds.");
					this.DamageDecreases.Add(new DamageChange(extension.DamageDecreasePercent, extension.DamageDecreaseDuration));
				}
				this.SkillUseText = stringBuilder4.ToString();
				this.SkillUseText = this.SkillUseText.Replace('.', ' ').TrimEnd(new char[0]) + ".";
				this.FightingLog.Add(this.SkillUseText);
			}
		}

		public void GetReward(bool won)
		{
			bool flag = won;
			CDouble cDouble = 33;
			foreach (Fight current in App.State.AllFights)
			{
				if (current.IsAvailable)
				{
					cDouble *= 2;
				}
			}
			if (App.State.Generator != null && App.State.Generator.IsBuilt)
			{
				cDouble += App.State.Generator.DivinitySec;
			}
			cDouble *= 2;
			if (this.currentBattleType == BattleState.BattleType.endless && this.DefeatedEnemyCount > 0)
			{
				int num = 0;
				for (int i = 0; i < this.DefeatedEnemyCount; i++)
				{
					num += i * 5;
				}
				cDouble *= num;
				if (this.DefeatedEnemyCount > App.State.Statistic.MostDefeatedShadowClones)
				{
					App.State.Statistic.MostDefeatedShadowClones = this.DefeatedEnemyCount;
					Leaderboards.SubmitStat(LeaderBoardType.MostClonesDefeated, App.State.Statistic.MostDefeatedShadowClones.ToInt(), false);
				}
				flag = true;
			}
			else if (this.currentBattleType == BattleState.BattleType.gods && this.DefeatedEnemyCount > 0)
			{
				cDouble = cDouble * this.DefeatedEnemyCount * 2;
				flag = true;
			}
			else if (this.currentBattleType == BattleState.BattleType.jacky)
			{
				cDouble *= 100;
				if (App.State.AllMights.First((Might x) => x.TypeEnum == Might.MightType.physical_attack).Level >= 100 && won)
				{
					Pet pet = App.State.Ext.AllPets.First((Pet x) => x.TypeEnum == PetType.Mole);
					if (!pet.IsUnlocked)
					{
						BattleState.JackyLeeDefeated = true;
						pet.Unlock();
						GuiBase.ShowToast("Contratulations, you just unlocked " + pet.Name + "!");
					}
				}
			}
			else if (this.currentBattleType == BattleState.BattleType.cthulhu)
			{
				cDouble *= 150;
			}
			else if (this.currentBattleType == BattleState.BattleType.doppel)
			{
				cDouble *= 250;
			}
			else if (this.currentBattleType == BattleState.BattleType.developer && this.DefeatedEnemyCount > 0)
			{
				if (this.DefeatedEnemyCount == 1)
				{
					cDouble *= 300;
				}
				else
				{
					cDouble *= 1000;
					App.State.Statistic.CreatorBeaten = true;
				}
				flag = true;
			}
			if (flag)
			{
				App.State.Money += cDouble;
				this.BattleRewardText = "You received " + cDouble.ToGuiText(true) + " Divinity!";
			}
			else
			{
				this.BattleRewardText = "You lost and got nothing.";
			}
			this.ResetSkillCoolDowns(false);
		}

		public void ResetSkillCoolDowns(bool fleed)
		{
			foreach (Skill current in App.State.AllSkills)
			{
				if (current.Extension.CoolDownCurrent >= 500L && fleed)
				{
					current.Extension.UsageCount -= 1L;
				}
				current.Extension.CoolDownCurrent = 0L;
			}
		}

		public void UpdateData(long timeDifference)
		{
			if (!this.IsFighting)
			{
				return;
			}
			this.totalFightTime += timeDifference;
			if (this.GearEyesDuration <= 0L)
			{
				this.enemyAttackTimer += timeDifference;
			}
			this.totalFightTimeMilliSeconds += timeDifference;
			if (this.enemyAttackTimer >= this.enemyAttackTime)
			{
				this.enemyAttackTimer = 0L;
				CDouble cDouble = this.randomize(this.CurrentEnemy.GetDamage(this.PlayerHp) * this.DamageDecrease / 100);
				bool flag = false;
				if (this.DodgeChance > 0)
				{
					int num = UnityEngine.Random.Range(0, 100);
					if (num < this.DodgeChance)
					{
						flag = true;
					}
					this.DodgeChance = 0;
				}
				bool flag2 = false;
				if (this.CounterChance > 0)
				{
					int num2 = UnityEngine.Random.Range(0, 100);
					if (num2 < this.CounterChance)
					{
						flag2 = true;
					}
					this.CounterChance = 0;
				}
				if (this.currentBattleType == BattleState.BattleType.endless && this.DefeatedEnemyCount > 0)
				{
					this.CurrentEnemy.AttackName = this.CurrentEnemy.AttackName.Replace("attacks", "attack");
					this.CurrentEnemy.AttackName = this.CurrentEnemy.AttackName.Replace("uses", "use");
				}
				if (flag)
				{
					if (this.currentBattleType == BattleState.BattleType.endless && this.DefeatedEnemyCount > 0)
					{
						this.EnemyDamageText = this.CurrentEnemy.Name + this.CurrentEnemy.AttackName + "and they miss!";
					}
					else
					{
						this.EnemyDamageText = this.CurrentEnemy.Name + this.CurrentEnemy.AttackName + "and misses!";
					}
				}
				else if (this.DamageBlock)
				{
					this.DamageBlock = false;
					this.EnemyDamageText = this.CurrentEnemy.Name + this.CurrentEnemy.AttackName + "but the damage is nullfied!";
				}
				else if (this.DamageReflect)
				{
					this.DamageReflect = false;
					this.CurrentEnemy.DoDamage(cDouble);
					this.EnemyDamageText = string.Concat(new string[]
					{
						this.CurrentEnemy.Name,
						this.CurrentEnemy.AttackName,
						"for ",
						cDouble.ToGuiText(true),
						" damage but the damage is reflected back!"
					});
				}
				else
				{
					this.PlayerHp -= cDouble;
					this.EnemyDamageText = string.Concat(new string[]
					{
						this.CurrentEnemy.Name,
						this.CurrentEnemy.AttackName,
						"for ",
						cDouble.ToGuiText(true),
						" damage."
					});
					if (flag2)
					{
						CDouble cDouble2 = this.randomize(this.DamageIncrease * App.State.Attack / 100);
						this.CurrentEnemy.DoDamage(cDouble2);
						this.EnemyDamageText = this.EnemyDamageText + " You counter for " + cDouble2.ToGuiText(true) + " back!";
					}
				}
				if (this.PlayerHp <= 0)
				{
					GuiBase.ShowToast("You lose!");
					this.IsFighting = false;
					this.IsBattleFinished = true;
					this.GetReward(false);
				}
				this.EnemyDamageText = this.FightTimeString + this.EnemyDamageText;
				this.FightingLog.Add(this.EnemyDamageText);
				this.CurrentEnemy.IncreaseDamage();
				this.last3EnemyAttacks[this.currentIntexOfEnemyAttacks] = cDouble;
				this.currentIntexOfEnemyAttacks++;
				this.currentIntexOfEnemyAttacks %= 3;
			}
			foreach (Skill current in App.State.AllSkills)
			{
				current.Extension.CoolDownCurrent -= timeDifference;
				if (current.Extension.CoolDownCurrent < 0L)
				{
					current.Extension.CoolDownCurrent = 0L;
				}
			}
			List<DamageChange> list = new List<DamageChange>();
			foreach (DamageChange current2 in this.DamageIncreases)
			{
				current2.Duration -= timeDifference;
				if (current2.Duration > 0L)
				{
					list.Add(current2);
				}
			}
			this.DamageIncreases = list;
			List<DamageChange> list2 = new List<DamageChange>();
			foreach (DamageChange current3 in this.DamageDecreases)
			{
				current3.Duration -= timeDifference;
				if (current3.Duration > 0L)
				{
					list2.Add(current3);
				}
			}
			this.DamageDecreases = list2;
			this.GodSpeedModeDuration -= timeDifference;
			if (this.GodSpeedModeDuration <= 0L)
			{
				this.GodSpeedModeDuration = 0L;
			}
			this.GearEyesDuration -= timeDifference;
			if (this.GearEyesDuration <= 0L)
			{
				this.GearEyesDuration = 0L;
			}
		}
	}
}
