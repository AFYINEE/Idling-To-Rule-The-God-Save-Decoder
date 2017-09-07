using Assets.Scripts.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class BattleUBV2
	{
		public static BattleUBV2 Instance = new BattleUBV2();

		public bool IsFighting;

		public UltimateBeingV2 Being;

		public List<SkillUB2> PlayerSkills = new List<SkillUB2>();

		public CDouble PlayerHp = 0;

		public CDouble PlayerEnergy = 0;

		public CDouble TurnCount = 1;

		internal CDouble GodSpeedModeDuration = 0;

		internal bool TurnUsed;

		internal DamageChange MysticMode;

		internal DamageChange TransformationAura;

		internal List<EnemyData.Enemy> AllEnemies = new List<EnemyData.Enemy>();

		internal bool DoubleUp;

		internal bool DamageBlock;

		internal bool DamageReflect;

		internal CDouble HitchanceBonus = 0;

		internal CDouble DodgeChance = 0;

		internal CDouble CounterChance = 0;

		internal string InfoText = string.Empty;

		public List<SkillUB2> LastUsedSkills = new List<SkillUB2>();

		internal CDouble Damage
		{
			get
			{
				CDouble cDouble = 100 + App.State.AllMights.FirstOrDefault((Might x) => x.TypeEnum == Might.MightType.physical_attack).Level;
				if (this.MysticMode != null && this.MysticMode.Duration > 0L)
				{
					cDouble = cDouble * (100 + this.MysticMode.ValuePercent) / 100;
				}
				if (this.TransformationAura != null && this.TransformationAura.Duration > 0L)
				{
					cDouble = cDouble * (100 + this.TransformationAura.ValuePercent) / 100;
				}
				return cDouble;
			}
		}

		internal CDouble DamageReduction
		{
			get
			{
				CDouble cDouble = 100 - App.State.AllMights.FirstOrDefault((Might x) => x.TypeEnum == Might.MightType.mystic_def).Level / 10;
				if (cDouble < 25)
				{
					cDouble = 25;
				}
				if (this.MysticMode != null && this.MysticMode.Duration > 0L)
				{
					cDouble *= 0.75;
				}
				if (this.TransformationAura != null && this.TransformationAura.Duration > 0L)
				{
					cDouble *= 0.5;
				}
				return 100 - cDouble;
			}
		}

		public void start(UltimateBeingV2 ub)
		{
			this.TurnCount = 1;
			this.Being = ub;
			this.Being.DamageReduction = 100;
			this.LastUsedSkills = new List<SkillUB2>();
			this.PlayerHp = App.State.CurrentHealth;
			this.PlayerEnergy = 1000;
			this.IsFighting = true;
			this.DoubleUp = false;
			this.DamageBlock = false;
			this.DamageReflect = false;
			this.HitchanceBonus = 0;
			this.DodgeChance = 0;
			this.CounterChance = 0;
			this.MysticMode = null;
			this.TransformationAura = null;
			this.GodSpeedModeDuration = 0;
			this.Being.PowerUp = false;
			this.InfoText = string.Empty;
			this.PlayerSkills = new List<SkillUB2>();
			this.PlayerSkills.Add(new SkillUB2
			{
				Name = "Mystic Mode",
				Desc = "50% more attack and 25% damage reduction for 6 turns",
				TypeEnum = SkillTypeUBV2.MysticMode,
				EnergyCost = 30,
				BuffDuration = 6,
				Buff = 50
			});
			this.PlayerSkills.Add(new SkillUB2
			{
				Name = "Transformation aura",
				Desc = "100 % more attack and 50% damage reduction for 5 turns",
				TypeEnum = SkillTypeUBV2.TransformationAura,
				EnergyCost = 80,
				BuffDuration = 5,
				Buff = 100
			});
			this.PlayerSkills.Add(new SkillUB2
			{
				Name = "God Speed",
				Desc = "Do 2 actions in one turn for the next 4 turns",
				TypeEnum = SkillTypeUBV2.GodSpeed,
				EnergyCost = 150,
				IncreaseActionsDuration = 8
			});
			this.PlayerSkills.Add(new SkillUB2
			{
				Name = "Elemental manipulation",
				Desc = "Doubles the damage of the next attack",
				TypeEnum = SkillTypeUBV2.ElementalManipulation,
				EnergyCost = 30,
				DoubleDamage = true
			});
			this.PlayerSkills.Add(new SkillUB2
			{
				Name = "Reflection barrier",
				Desc = "Reflect the next attack",
				TypeEnum = SkillTypeUBV2.ReflectionBarrier,
				EnergyCost = 200,
				ReflectDamage = true
			});
			this.PlayerSkills.Add(new SkillUB2
			{
				Name = "Clairvoyance",
				Desc = "65% chance to dodge and counter the next attack",
				TypeEnum = SkillTypeUBV2.Clairvoyance,
				EnergyCost = 100,
				DodgeCounterChance = 65
			});
			this.PlayerSkills.Add(new SkillUB2
			{
				Name = "Focused breathing",
				Desc = "Recover 15% of hp",
				TypeEnum = SkillTypeUBV2.FocusedBreathing,
				EnergyCost = -60,
				HealPerc = 15
			});
			this.PlayerSkills.Add(new SkillUB2
			{
				Name = "Aura Ball",
				Desc = "Attack with an aura ball",
				TypeEnum = SkillTypeUBV2.AuraBall,
				EnergyCost = 20,
				Damage = 2
			});
			this.PlayerSkills.Add(new SkillUB2
			{
				Name = "Big Bang",
				Desc = "Attack with big bang, twice as much damage as aura ball",
				TypeEnum = SkillTypeUBV2.BigBang,
				EnergyCost = 40,
				Damage = 4,
				HealPerc = -10
			});
			this.PlayerSkills.Add(new SkillUB2
			{
				Name = "lonioi hero summon",
				Desc = "Damage depending on your highest god defeated",
				TypeEnum = SkillTypeUBV2.IonioiHeroSummon,
				EnergyCost = App.State.Statistic.HighestGodDefeated * 1.8,
				Damage = App.State.Statistic.HighestGodDefeated / 10
			});
			Creation creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == ub.CreationNeeded);
			this.PlayerSkills.Add(new SkillUB2
			{
				Name = "Give " + creation.Name,
				Desc = "Gives away one " + creation.Name,
				TypeEnum = SkillTypeUBV2.GiveBaseCreation,
				EnergyCost = -20
			});
			this.PlayerSkills.Add(new SkillUB2
			{
				Name = "Give " + ub.CreationName,
				Desc = "Gives away one " + ub.CreationName,
				TypeEnum = SkillTypeUBV2.GiveModifiedCreation,
				EnergyCost = -20
			});
			foreach (SkillUB2 current in this.PlayerSkills)
			{
				if (current.EnergyCost > 0)
				{
					current.Desc = current.Desc + "\nEnergy cost: " + current.EnergyCost.ToGuiText(true);
				}
				else if (current.EnergyCost < 0)
				{
					current.Desc = current.Desc + "\nRecovers " + current.EnergyCost.ToGuiText(true) + " energy";
				}
				if (current.HealPerc < 0)
				{
					current.Desc = current.Desc + "\nReduces your hp by " + (current.HealPerc * -1).ToGuiText(true) + " %";
				}
				if (current.Damage > 0)
				{
					current.Desc = current.Desc + "\nBase Damage: " + current.Damage.ToGuiText(false);
				}
			}
		}

		public void NextTurn(SkillUB2 skillUsed)
		{
			if (this.GodSpeedModeDuration > 0 && !this.TurnUsed)
			{
				this.TurnUsed = true;
				this.InfoText = this.InfoText + "\nYou used " + skillUsed.Name;
			}
			else
			{
				this.InfoText = "Turn " + this.TurnCount.ToGuiText(true) + "\nYou used " + skillUsed.Name;
				this.TurnUsed = false;
				this.TurnCount = ++this.TurnCount;
			}
			this.PlayerEnergy += 10;
			if (skillUsed.EnergyCost + 10 >= this.PlayerEnergy)
			{
				this.InfoText = string.Concat(new string[]
				{
					"Turn ",
					this.TurnCount.ToGuiText(true),
					"\nYou didin't have enough energy to use ",
					skillUsed.Name,
					" and wasted a turn."
				});
				this.UBAttack(false);
				return;
			}
			this.PlayerEnergy -= skillUsed.EnergyCost;
			if (this.PlayerEnergy > 1000)
			{
				this.PlayerEnergy = 1000;
			}
			if (this.MysticMode != null)
			{
				this.MysticMode.Duration -= 1L;
			}
			if (this.TransformationAura != null)
			{
				this.TransformationAura.Duration -= 1L;
			}
			if (skillUsed.TypeEnum == SkillTypeUBV2.MysticMode)
			{
				this.MysticMode = new DamageChange(skillUsed.Buff.ToInt(), (long)skillUsed.BuffDuration.ToInt());
			}
			if (skillUsed.TypeEnum == SkillTypeUBV2.TransformationAura)
			{
				this.TransformationAura = new DamageChange(skillUsed.Buff.ToInt(), (long)skillUsed.BuffDuration.ToInt());
			}
			this.GodSpeedModeDuration = --this.GodSpeedModeDuration;
			if (this.GodSpeedModeDuration < 0)
			{
				this.GodSpeedModeDuration = 0;
			}
			if (skillUsed.IncreaseActionsDuration > 0)
			{
				this.GodSpeedModeDuration = skillUsed.IncreaseActionsDuration;
			}
			if (skillUsed.DoubleDamage)
			{
				this.DoubleUp = true;
			}
			if (skillUsed.ReflectDamage)
			{
				this.DamageReflect = true;
			}
			if (skillUsed.DodgeCounterChance > 0)
			{
				this.DodgeChance = skillUsed.DodgeCounterChance;
				this.CounterChance = skillUsed.DodgeCounterChance;
			}
			if (skillUsed.HealPerc != 0)
			{
				this.PlayerHp += App.State.MaxHealth * skillUsed.HealPerc / 100;
				if (this.PlayerHp > App.State.MaxHealth)
				{
					this.PlayerHp = App.State.MaxHealth;
				}
			}
			this.Fight(skillUsed, this.TurnUsed);
		}

		public void Fight(SkillUB2 skillUsed, bool playerSecondAttack)
		{
			string str = string.Empty;
			int num = 0;
			int num2 = 0;
			foreach (SkillUB2 current in this.LastUsedSkills)
			{
				if (current.TypeEnum == SkillTypeUBV2.GiveBaseCreation)
				{
					num++;
				}
				if (current.TypeEnum == SkillTypeUBV2.GiveModifiedCreation)
				{
					num2++;
				}
			}
			this.LastUsedSkills.Add(skillUsed);
			if (skillUsed.TypeEnum == SkillTypeUBV2.GiveBaseCreation)
			{
				Creation creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == this.Being.CreationNeeded);
				if (creation.Count < 1)
				{
					this.LastUsedSkills.RemoveAt(this.LastUsedSkills.Count - 1);
					str = "You did not have any " + creation.Name + "!";
					this.UBAttack(playerSecondAttack);
				}
				else
				{
					if ((this.LastUsedSkills.Count > 2 && this.LastUsedSkills[this.LastUsedSkills.Count - 2].TypeEnum == SkillTypeUBV2.GiveBaseCreation) || (this.LastUsedSkills.Count > 2 && this.LastUsedSkills[this.LastUsedSkills.Count - 2].TypeEnum == SkillTypeUBV2.GiveModifiedCreation))
					{
						this.LastUsedSkills.RemoveAt(this.LastUsedSkills.Count - 1);
						str = this.Being.Name + " destroyed your " + creation.Name + "! He grew tired of it.";
						this.UBAttack(playerSecondAttack);
					}
					else
					{
						this.Being.DamageReduction += 3;
						if (this.Being.DamageReduction > 100)
						{
							this.Being.DamageReduction = 100;
						}
						str = this.Being.Name + " ate your " + creation.Name + ". His damage reduction was increased by 3%";
					}
					Creation expr_20B = creation;
					expr_20B.Count = --expr_20B.Count;
				}
			}
			else if (skillUsed.TypeEnum == SkillTypeUBV2.GiveModifiedCreation)
			{
				if (this.Being.CreationCount < 1)
				{
					this.LastUsedSkills.RemoveAt(this.LastUsedSkills.Count - 1);
					str = "You did not have any " + this.Being.CreationName + "!";
					this.UBAttack(playerSecondAttack);
				}
				else
				{
					if (num2 > num * 2)
					{
						this.LastUsedSkills.RemoveAt(this.LastUsedSkills.Count - 1);
						str = this.Being.Name + " destroyed your " + this.Being.CreationName + "! He was suspicious of it!";
						this.UBAttack(playerSecondAttack);
					}
					else if ((this.LastUsedSkills.Count > 2 && this.LastUsedSkills[this.LastUsedSkills.Count - 2].TypeEnum == SkillTypeUBV2.GiveBaseCreation) || (this.LastUsedSkills.Count > 2 && this.LastUsedSkills[this.LastUsedSkills.Count - 2].TypeEnum == SkillTypeUBV2.GiveModifiedCreation))
					{
						this.LastUsedSkills.RemoveAt(this.LastUsedSkills.Count - 1);
						str = this.Being.Name + " destroyed your " + this.Being.CreationName + "! He grew tired of it.";
						this.UBAttack(playerSecondAttack);
					}
					else
					{
						this.Being.DamageReduction -= 5;
						if (this.Being.DamageReduction < 0)
						{
							this.Being.DamageReduction = 0;
						}
						str = this.Being.Name + " ate your " + this.Being.CreationName + ". His damage reduction was reduced by 5%!";
					}
					UltimateBeingV2 expr_402 = this.Being;
					expr_402.CreationCount = --expr_402.CreationCount;
				}
			}
			else if (skillUsed.TypeEnum == SkillTypeUBV2.AuraBall || skillUsed.TypeEnum == SkillTypeUBV2.IonioiHeroSummon || skillUsed.TypeEnum == SkillTypeUBV2.BigBang)
			{
				CDouble cDouble = skillUsed.Damage * (100 - this.Being.DamageReduction) / 100;
				if (this.DoubleUp)
				{
					cDouble *= 2;
					this.DoubleUp = false;
				}
				cDouble = cDouble * this.Damage / 100;
				cDouble = (double)UnityEngine.Random.Range((float)cDouble.ToInt() * 0.9f, (float)cDouble.ToInt() * 1.1f);
				cDouble /= 0.5 + (double)(this.Being.Tier / 2);
				this.Being.HPPercent -= cDouble;
				this.InfoText = this.InfoText + " and caused " + cDouble.ToGuiText(true) + " % damage.";
				this.UBAttack(playerSecondAttack);
			}
			else
			{
				this.UBAttack(playerSecondAttack);
			}
			this.InfoText = this.InfoText + "\n" + str;
		}

		private void UBAttack(bool playerSecondAttack)
		{
			if (playerSecondAttack)
			{
				return;
			}
			int num = UnityEngine.Random.Range(0, 100);
			if (this.Being.DamageReduction < 75 && num > 80)
			{
				this.Being.DamageReduction += 10;
				this.InfoText = this.InfoText + "\n" + this.Being.Name + " concentrated his power. His damage reduction increased by 10%";
			}
			else if (this.Being.HPPercent < 15 && num > 67)
			{
				this.Being.HPPercent += 10;
				this.InfoText = this.InfoText + "\n" + this.Being.Name + " healed himself for 10% of his hp.";
			}
			else if (num > 85 && !this.Being.PowerUp)
			{
				this.Being.PowerUp = true;
				this.InfoText = this.InfoText + "\n" + this.Being.Name + " concentrated his power.";
			}
			else
			{
				string text = this.Being.SkillName1;
				CDouble rightSide = 1;
				if (num > 50)
				{
					if (num > 80)
					{
						rightSide = 2.5;
						text = this.Being.SkillName3;
					}
					else
					{
						rightSide = 1.5;
						text = this.Being.SkillName2;
					}
				}
				if (this.Being.PowerUp)
				{
					rightSide = 2;
					this.Being.PowerUp = false;
				}
				CDouble cDouble = this.Being.BaseCloneDamage / 100 * (100 - this.DamageReduction) * rightSide;
				cDouble = (double)UnityEngine.Random.Range((float)cDouble.ToInt() * 0.9f, (float)cDouble.ToInt() * 1.1f);
				CDouble cDouble2 = this.Being.BaseDamage / 100 * (100 - this.DamageReduction) * rightSide;
				if (!this.DamageReflect)
				{
					if (num < this.DodgeChance)
					{
						CDouble cDouble3 = 5 * (100 - this.Being.DamageReduction) / 100;
						if (this.DoubleUp)
						{
							cDouble3 *= 2;
							this.DoubleUp = false;
						}
						this.DodgeChance = 0;
						this.CounterChance = 0;
						cDouble3 = cDouble3 * this.Damage / 100;
						cDouble3 = (double)UnityEngine.Random.Range((float)cDouble3.ToInt() * 0.9f, (float)cDouble3.ToInt() * 1.1f);
						this.Being.HPPercent -= cDouble3;
						this.InfoText = string.Concat(new string[]
						{
							this.InfoText,
							"\n",
							this.Being.Name,
							" used ",
							text,
							" and you dodged the attack! \nYou countered for ",
							cDouble3.ToGuiText(true)
						});
					}
					else
					{
						if (cDouble > App.State.HomePlanet.ShadowCloneCount)
						{
							cDouble = App.State.HomePlanet.ShadowCloneCount;
							cDouble2 *= 2;
						}
						else
						{
							cDouble2 *= 0.5;
						}
						App.State.HomePlanet.ShadowCloneCount -= cDouble.ToInt();
						App.State.Clones.RemoveUsedShadowClones(cDouble.ToInt());
						App.State.Clones.Count -= cDouble.ToInt();
						App.State.Clones.TotalClonesKilled += cDouble.ToInt();
						App.State.Statistic.TotalShadowClonesDied += cDouble;
						this.PlayerHp -= App.State.MaxHealth * cDouble2 / 100;
						this.InfoText = string.Concat(new string[]
						{
							this.InfoText,
							"\n",
							this.Being.Name,
							" used ",
							text,
							" and killed ",
							cDouble.ToGuiText(true),
							" of your clones!\nYou also lost ",
							cDouble2.ToGuiText(true),
							" % of your hp."
						});
					}
				}
				else
				{
					cDouble2 = cDouble2 * (100 - this.Being.DamageReduction) / 250;
					if (cDouble2 > 5)
					{
						cDouble2 = 5;
					}
					this.Being.HPPercent -= cDouble2;
					this.DamageReflect = false;
					this.InfoText = string.Concat(new string[]
					{
						this.InfoText,
						"\n",
						this.Being.Name,
						" used ",
						text,
						" but the damage is reflected back!\n",
						this.Being.Name,
						" took ",
						cDouble2.ToGuiText(true),
						" % damage."
					});
				}
			}
			if (this.PlayerHp <= 0 || App.State.HomePlanet.ShadowCloneCount <= 0)
			{
				App.State.CurrentHealth = 0;
				GuiBase.ShowToast("You lost the fight!");
				this.IsFighting = false;
			}
			if (this.Being.HPPercent <= 0)
			{
				this.Being.IsDefeated = true;
				this.Being.isCreating = false;
				this.Being.TimesDefeated++;
				CDouble multiplier = this.Being.GetMultiplier(App.State.HomePlanet.UBMultiplier);
				App.State.HomePlanet.UBMultiplier += multiplier;
				App.State.PremiumBoni.GodPower += this.Being.Tier * 10;
				App.State.HomePlanet.TotalGainedGodPower += this.Being.Tier * 10;
				GuiBase.ShowToast(string.Concat(new object[]
				{
					"You won the fight and earned ",
					this.Being.Tier * 10,
					" God Power!\nYour planet multiplier also increased by ",
					multiplier.ToGuiText(true)
				}));
				if (this.Being.Tier < 5)
				{
					App.State.HomePlanet.UltimateBeingsV2[this.Being.Tier].IsAvailable = true;
				}
				App.State.PremiumBoni.CheckGP = true;
				this.IsFighting = false;
				HeroImage.SetTitle();
			}
		}
	}
}
