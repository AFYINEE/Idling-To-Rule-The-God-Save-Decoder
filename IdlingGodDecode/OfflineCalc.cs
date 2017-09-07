using Assets.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Helper
{
	internal class OfflineCalc : MonoBehaviour
	{
		public static long TimeOffline;

		private long ServerTime;

		private GameState State;

		public static bool IsCalculating;

		public void Update()
		{
			if (!App.EnableOfflineCalc)
			{
				return;
			}
			bool flag = !App.OfflineStatsChecked && !OfflineCalc.IsCalculating;
			if ((App.ServerTime != 0L && App.State.TimeStampGameClosed != 0L && flag) )//|| (UpdateStats.CheckOfflineForCalc && flag))
			{
				this.State = App.State;
				this.ServerTime = App.ServerTime;
				long num = this.ServerTime - this.State.TimeStampGameClosed - 10L;
				if (this.State.TimeStampGameClosed <= 0L)
				{
					num = 0L;
				}
				if (OfflineCalc.TimeOffline > 0L)
				{
					num = OfflineCalc.TimeOffline;
					OfflineCalc.TimeOffline = 0L;
				}
				num -= 10L;
				if (num <= 0L)
				{
					if (this.State.TimeStampGameClosedOfflineMS == 0L)
					{
						return;
					}
                    long num2 = 1; // UpdateStats.CurrentTimeMillis();
					long timeStampGameClosedOfflineMS = this.State.TimeStampGameClosedOfflineMS;
					num = (num2 - timeStampGameClosedOfflineMS) / 1000L;
					if (num <= 0L)
					{
						return;
					}
					Log.Info("Failed to get ServerTime, offline time is used: " + num);
				}
				else
				{
					Log.Info("TimeOffline: " + num);
				}
				OfflineCalc.IsCalculating = true;
				//base.StartCoroutine(this.Calculate(num));
			}
		}

		private void CalcTrainingSkills<T>(long timeMS, StringBuilder infoBuilder, List<T> elements, int stopAtCount, bool isNextOn)
		{
			int num = 0;
			long num2 = 0L;
			List<TrainingBase> list = new List<TrainingBase>();
			foreach (T current in elements)
			{
				if (current is TrainingBase)
				{
					list.Add(current as TrainingBase);
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				TrainingBase trainingBase = list[i];
				bool flag = false;
				if (trainingBase is Skill)
				{
					flag = ((Skill)trainingBase).IsAvailable;
				}
				if (trainingBase is Training)
				{
					flag = ((Training)trainingBase).IsAvailable;
				}
				if (num > 0 && flag)
				{
					if (trainingBase.ShadowCloneCount > 0)
					{
						num2 = 0L;
					}
					trainingBase.AddCloneCount(num);
					num = 0;
				}
				long num3 = 0L;
				if (trainingBase is Skill && ((Skill)trainingBase).IsAvailable && trainingBase.DurationInMS(1) > 30)
				{
					num3 = timeMS / 300000L;
					if (num3 > 10000L)
					{
						num3 = 10000L;
					}
					((Skill)trainingBase).Extension.UsageCount += num3;
				}
				if (trainingBase.ShadowCloneCount > 0)
				{
					int num4 = trainingBase.ShadowCloneCount.ToInt();
					int num5 = trainingBase.DurationInMS(num4);
					if (num5 < 30)
					{
						num5 = 30;
					}
					long num6 = timeMS;
					CDouble cDouble = 0;
					if (num2 > 0L)
					{
						num6 = num2;
						num2 = 0L;
					}
					cDouble = num6 / (long)num5;
					if (cDouble > 0)
					{
						bool flag2 = num4 <= this.State.GameSettings.TrainIgnoreCount && this.State.GameSettings.IgnoreCloneCountOn;
						if (!flag2 && !this.State.GameSettings.UseStopAt && stopAtCount <= trainingBase.Level + cDouble && isNextOn)
						{
							CDouble cDouble2 = stopAtCount - trainingBase.Level;
							if (this.State.PremiumBoni.ImprovedNextAt)
							{
								cDouble2 = (trainingBase.EnumValue + 1) * 500 - trainingBase.Level;
								if (stopAtCount - trainingBase.Level > cDouble2)
								{
									cDouble2 = stopAtCount - trainingBase.Level;
								}
							}
							if (cDouble2 > 0)
							{
								num6 -= (cDouble2 * num5).ToLong();
								num2 = (num5 * (cDouble - cDouble2)).ToLong();
								int num7 = this.State.GameSettings.TrainIgnoreCount;
								if (!this.State.GameSettings.IgnoreCloneCountOn)
								{
									num7 = 0;
								}
								num = trainingBase.ShadowCloneCount.ToInt() - num7;
								if (num2 > 0L)
								{
									int num8 = trainingBase.ShadowCloneCount.ToInt() - num;
									num5 = trainingBase.DurationInMS(num8);
									if (num5 < 30)
									{
										num5 = 30;
									}
									if (num8 > 0)
									{
										cDouble = cDouble2 + num6 / (long)num5;
									}
									else
									{
										cDouble = cDouble2;
									}
									trainingBase.RemoveCloneCount(num);
								}
								else
								{
									num = 0;
								}
							}
						}
						else if (!flag2 && isNextOn && stopAtCount <= trainingBase.Level + cDouble && (this.State.GameSettings.UseStopAt || trainingBase.EnumValue != 27))
						{
							cDouble = stopAtCount - trainingBase.Level.ToInt();
							if (cDouble < 0)
							{
								cDouble = 0;
							}
						}
						cDouble.Round();
						trainingBase.Level += cDouble;
						string value = string.Empty;
						if (trainingBase is Training)
						{
							value = ((Training)trainingBase).Name;
							this.State.PhysicalPowerBase += trainingBase.PowerGain * cDouble;
							this.State.Statistic.TotalTrainingLevels += cDouble;
						}
						else if (trainingBase is Skill)
						{
							value = ((Skill)trainingBase).Name;
							this.State.MysticPowerBase += trainingBase.PowerGain * cDouble;
							this.State.Statistic.TotalSkillLevels += cDouble;
						}
						infoBuilder.Append("- gained ").Append(cDouble).Append(" levels in ' ").Append(value).Append(" '\n");
						if (num3 > 0L)
						{
							infoBuilder.Append("- ").Append(value).Append(" was also used ").Append(num3).Append(" times to reduce the cap").Append("\n");
						}
					}
				}
			}
		}

		private void CalcCreations(long timeMS, StringBuilder infoBuilder)
		{
			CDouble cDouble = this.State.CreationSpeed(timeMS) / 3L;
			long num = timeMS;
			bool flag = false;
			if ((this.State.GameSettings.LastCreation != null && this.State.GameSettings.LastCreation.TypeEnum == Creation.CreationType.Shadow_clone) || (this.State.GameSettings.CreateShadowClonesIfNotMax && this.State.Clones.Count < this.State.Clones.MaxShadowClones))
			{
				flag = true;
			}
			if (flag)
			{
				Creation creation = this.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Shadow_clone);
				Creation.UpdateDurationMulti(this.State);
				CDouble cDouble2 = cDouble / creation.DurationInMS * (this.State.PremiumBoni.CreationCountBoni(true) + 1);
				if (cDouble2 > timeMS / 30L * (this.State.PremiumBoni.CreationCountBoni(true) + 1))
				{
					cDouble2 = timeMS / 30L * (this.State.PremiumBoni.CreationCountBoni(true) + 1);
				}
				if (cDouble2 > 0)
				{
					CDouble cDouble3 = timeMS / cDouble2;
					bool flag2 = this.State.GameSettings.LastCreation != null && this.State.GameSettings.LastCreation.TypeEnum == Creation.CreationType.Shadow_clone;
					if (cDouble2 + creation.Count > this.State.Clones.MaxShadowClones && this.State.GameSettings.CreateShadowClonesIfNotMax && !flag2)
					{
						cDouble2 = this.State.Clones.MaxShadowClones - creation.Count;
						num -= cDouble2.ToLong() * cDouble3.ToLong();
					}
					creation.TotalCreated += cDouble2;
					this.State.Clones.TotalClonesCreated += cDouble2;
					this.State.Statistic.TotalShadowClonesCreated += cDouble2;
					if (cDouble2 > 0)
					{
						infoBuilder.Append("- created ").Append(cDouble2.GuiText).Append(" Shadow Clones\n");
					}
					if (this.State.Clones.Count + cDouble2 > this.State.Clones.MaxShadowClones)
					{
						cDouble2 = this.State.Clones.MaxShadowClones - this.State.Clones.Count;
					}
					this.State.Clones.Count = this.State.Clones.Count + cDouble2.ToInt();
					if (this.State.Clones.Count < this.State.Clones.MaxShadowClones)
					{
						num = 0L;
					}
				}
			}
			if (this.State.GameSettings.LastCreation != null && this.State.GameSettings.LastCreation.TypeEnum != Creation.CreationType.Shadow_clone && App.CurrentPlattform == Plattform.Android)
			{
				while (num > 0L)
				{
					cDouble = this.State.CreationSpeed(num) / 3L;
					Creation creation2 = this.State.GameSettings.LastCreation;
					CDouble cDouble4 = creation2.DurationInMS;
					if (cDouble4 < 30)
					{
						cDouble4 = 30;
					}
					CDouble cDouble5 = cDouble / cDouble4;
					if (cDouble5 < 1)
					{
						creation2.currentDuration += cDouble.ToLong();
						break;
					}
					cDouble5.Round();
					cDouble5 *= this.State.PremiumBoni.CreationCountBoni(true) + 1;
					List<CreationCost> list = CreationCost.RequiredCreations(creation2.TypeEnum, cDouble5.ToLong(), true);
					bool flag3 = true;
					foreach (CreationCost current in list)
					{
						if (!CreationCost.HasCreations(this.State, current, this.State.GameSettings.AutoBuyCreations))
						{
							flag3 = false;
						}
					}
					if (!flag3)
					{
						cDouble5 = this.State.GameSettings.CreationToCreateCount;
						if (cDouble5 == 0)
						{
							cDouble5 = 1;
						}
						list = CreationCost.RequiredCreations(creation2.TypeEnum, cDouble5.ToLong(), true);
						flag3 = true;
						foreach (CreationCost current2 in list)
						{
							if (!CreationCost.HasCreations(this.State, current2, this.State.GameSettings.AutoBuyCreations))
							{
								flag3 = false;
							}
						}
						if (flag3)
						{
							bool flag4 = true;
							while (flag4)
							{
								cDouble5 *= 2;
								list = CreationCost.RequiredCreations(creation2.TypeEnum, cDouble5.ToLong(), true);
								foreach (CreationCost current3 in list)
								{
									if (!CreationCost.HasCreations(this.State, current3, this.State.GameSettings.AutoBuyCreations))
									{
										flag4 = false;
										cDouble5 /= 2;
										break;
									}
								}
							}
						}
					}
					if (!flag3)
					{
						infoBuilder.Append("- you failed to create " + creation2.Name + " because of missing prerequisites or divinity, so you created stones instead.\n");
						list = new List<CreationCost>();
						creation2 = this.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Stone);
						cDouble4 = creation2.DurationInMS;
						if (cDouble4 < 30)
						{
							cDouble4 = 30;
						}
						cDouble5 = cDouble / cDouble4 * (this.State.PremiumBoni.CreationCountBoni(true) + 1);
					}
					CDouble cDouble6 = cDouble5 + creation2.Count;
					CDouble cDouble7 = -1;
					if (creation2.NextAtCount > 0)
					{
						if (this.State.GameSettings.CreationsNextAtMode == 1)
						{
							cDouble7 = creation2.NextAtCount - creation2.Count;
						}
						else if (this.State.GameSettings.CreationsNextAtMode == 2)
						{
							cDouble7 = creation2.NextAtCount - creation2.TotalCreated;
						}
					}
					if (cDouble7 != -1 && cDouble5 > cDouble7 && flag3)
					{
						if (cDouble7 > 0)
						{
							cDouble5 = cDouble7;
							CDouble cDouble8 = (cDouble5 * cDouble4 / (this.State.PremiumBoni.CreationCountBoni(true) + 1)).ToLong();
							cDouble8 = cDouble8 / this.State.PremiumBoni.CreationDopingDivider / (100 + this.State.PremiumBoni.CreatingSpeedUpPercent(true)) * 100;
							num -= cDouble8.ToLong();
						}
						else
						{
							cDouble5 = 0;
						}
					}
					else
					{
						num = 0L;
					}
					if (num > 0L && this.State.GameSettings.LastCreation.TypeEnum != Creation.CreationType.Universe)
					{
						Creation creation3 = this.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == this.State.GameSettings.LastCreation.TypeEnum + 1);
						if (!creation3.GodToDefeat.IsDefeated)
						{
							break;
						}
						this.State.GameSettings.LastCreation = creation3;
					}
					else
					{
						num = 0L;
					}
					if (cDouble5 > 1)
					{
						list = CreationCost.RequiredCreations(creation2.TypeEnum, cDouble5.ToLong(), true);
						using (List<CreationCost>.Enumerator enumerator4 = list.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								CreationCost cost = enumerator4.Current;
								Creation creation4 = this.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
								creation4.Count -= cost.CountNeeded;
								if (creation4.Count < 0)
								{
									Log.Info(string.Concat(new object[]
									{
										creation4.Name,
										" had ",
										creation4.Count,
										"!"
									}));
									creation4.Count = 0;
								}
							}
						}
						cDouble5.Floor();
						creation2.Count += cDouble5;
						creation2.TotalCreated += cDouble5;
						this.State.CheckForAchievement(creation2);
						this.State.CheckForAchievement(creation2);
						this.State.CheckForAchievement(creation2);
						infoBuilder.Append("- you created ").Append(cDouble5.GuiText).Append(" " + creation2.Name + "\n\n");
					}
				}
				num = 0L;
			}
			if (this.State.Statistic.HasStartedUniverseChallenge)
			{
				Creation creation5 = this.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Universe);
				creation5.FinishUUC(this.State);
			}
		}

		private void CalcMonuments(long timeMS, StringBuilder infoBuilder)
		{
			foreach (Monument current in this.State.AllMonuments)
			{
				if (current.ShadowCloneCount > 0)
				{
					int num = current.ShadowCloneCount.ToInt();
					CDouble cDouble = timeMS * (long)num * (long)this.State.PremiumBoni.MonumentBuildTimeDivider * (100 + this.State.PremiumBoni.BuildingSpeedUpPercent(true)) / 100;
					long num2 = current.DurationInMS(1);
					long num3 = num2 / (current.Level.ToLong() + 1L);
					if (num2 < 30L)
					{
						num2 = 30L;
					}
					if (num3 < 30L)
					{
						num3 = 30L;
					}
					CDouble rightSide = current.Level * (num2 - num3) / 2;
					CDouble leftSide = cDouble + rightSide;
					CDouble cDouble2 = Math.Sqrt((leftSide / num3).Double) * 1.414215;
					cDouble2.Floor();
					if (cDouble2 < current.Level)
					{
						cDouble2 = current.Level;
					}
					if (cDouble2 > current.StopAt && current.StopAt != 0)
					{
						cDouble2 = current.StopAt;
					}
					if (this.State.GameSettings.StopMonumentBuilding && cDouble2 > current.Level)
					{
						cDouble2 = current.Level + 1;
					}
					int num4 = (cDouble2 - current.Level).ToInt();
					if (num4 > 20000)
					{
						num4 = 20000;
					}
					CDouble cDouble3 = 1;
					if (num4 == 0)
					{
						current.CurrentDuration += cDouble.ToLong();
					}
					else
					{
						bool flag = false;
						CDouble cDouble4 = cDouble2;
						if ((num4 + current.Level).ToInt() == current.StopAt && current.StopAt != 0)
						{
							current.CurrentDuration = 0L;
							cDouble4 = --cDouble4;
						}
						if (num4 > 1)
						{
							cDouble3 = cDouble4 * cDouble4 / 2 + cDouble4 / 2;
							cDouble3 = cDouble3 - current.Level * current.Level / 2 + current.Level / 2;
							foreach (CreationCost current2 in current.RequiredCreations(cDouble3))
							{
								if (!CreationCost.HasCreations(this.State, current2, this.State.GameSettings.AutoBuyCreationsForMonuments))
								{
									flag = true;
									cDouble = 0;
									break;
								}
							}
							while (flag)
							{
								flag = false;
								num4 /= 2;
								cDouble2 = num4 + current.Level;
								if (num4 < 1)
								{
									break;
								}
								cDouble3 = cDouble2 * cDouble2 / 2 + cDouble2 / 2;
								cDouble3 = cDouble3 - current.Level * current.Level / 2 + current.Level / 2;
								foreach (CreationCost current3 in current.RequiredCreations(cDouble3))
								{
									if (!CreationCost.HasCreations(this.State, current3, this.State.GameSettings.AutoBuyCreationsForMonuments))
									{
										flag = true;
										cDouble = 0;
										break;
									}
								}
							}
						}
						if (!flag)
						{
							if (num4 > 1)
							{
								using (List<CreationCost>.Enumerator enumerator4 = current.RequiredCreations(cDouble3).GetEnumerator())
								{
									while (enumerator4.MoveNext())
									{
										CreationCost cost = enumerator4.Current;
										Creation creation = this.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
										creation.Count -= cost.CountNeeded;
										if (creation.count < 0)
										{
											creation.count = 0;
										}
									}
								}
							}
							current.Level += num4;
							this.State.Statistic.MonumentsCreated += num4;
							current.AddStatBoni();
							if (this.State.GameSettings.StopMonumentBuilding)
							{
								this.State.Clones.RemoveUsedShadowClones(current.ShadowCloneCount);
								current.ShadowCloneCount = 0;
								current.CurrentDuration = 0L;
								cDouble = 0;
							}
							if (current.TypeEnum == Monument.MonumentType.temple_of_god)
							{
								this.State.Generator.IsAvailable = true;
							}
						}
					}
					if (current.CurrentDuration < 0L)
					{
						current.CurrentDuration = 0L;
					}
					if (num4 > 0)
					{
						infoBuilder.Append("- monument: ").Append(current.Name).Append(" was built ").Append(num4).Append(" x\n");
					}
				}
				if (current.Upgrade.ShadowCloneCount > 0)
				{
					int num5 = current.Upgrade.ShadowCloneCount.ToInt();
					CDouble cDouble5 = timeMS * (long)num5 * (long)this.State.PremiumBoni.MonumentBuildTimeDivider * (100 + this.State.PremiumBoni.BuildingSpeedUpPercent(true)) / 100;
					int num6 = 0;
					while (cDouble5 > 0)
					{
						long value = current.Upgrade.DurationInMS(1) - current.Upgrade.CurrentDuration;
						if (cDouble5 >= value && current.Upgrade.Level >= current.Upgrade.StopAt && current.Upgrade.StopAt != 0)
						{
							this.State.Clones.RemoveUsedShadowClones(current.Upgrade.ShadowCloneCount);
							current.Upgrade.ShadowCloneCount = 0;
							current.Upgrade.CurrentDuration = 0L;
							cDouble5 = 0;
						}
						else if (cDouble5 >= value)
						{
							cDouble5 -= value;
							current.Upgrade.CurrentDuration = 0L;
							bool flag2 = false;
							if (!current.Upgrade.IsPaid)
							{
								foreach (CreationCost current4 in current.Upgrade.RequiredCreations)
								{
									if (!CreationCost.HasCreations(this.State, current4, this.State.GameSettings.AutoBuyCreationsForMonuments))
									{
										flag2 = true;
										cDouble5 = 0;
										break;
									}
								}
							}
							if (!flag2)
							{
								if (!current.Upgrade.IsPaid)
								{
									using (List<CreationCost>.Enumerator enumerator6 = current.Upgrade.RequiredCreations.GetEnumerator())
									{
										while (enumerator6.MoveNext())
										{
											CreationCost cost = enumerator6.Current;
											Creation creation2 = this.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
											creation2.Count -= cost.CountNeeded;
											if (creation2.count < 0)
											{
												creation2.count = 0;
											}
										}
									}
								}
								else
								{
									current.Upgrade.IsPaid = false;
								}
								num6++;
								current.AddUpgradeLevel();
								if (this.State.GameSettings.StopMonumentBuilding)
								{
									this.State.Clones.RemoveUsedShadowClones(current.Upgrade.ShadowCloneCount);
									current.Upgrade.ShadowCloneCount = 0;
									current.Upgrade.CurrentDuration = 0L;
									cDouble5 = 0;
								}
							}
						}
						else
						{
							current.Upgrade.CurrentDuration += cDouble5.ToLong();
							cDouble5 = 0;
						}
					}
					if (num6 > 0)
					{
						infoBuilder.Append("- upgrade for ").Append(current.Name).Append(" was built ").Append(num6).Append(" x\n");
					}
				}
			}
		}

		private void CalcMight(long timeMS, StringBuilder infoBuilder)
		{
			long num = 0L;
			if (this.State.PremiumBoni.TotalMightIsUnlocked)
			{
				using (List<Might>.Enumerator enumerator = this.State.AllMights.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Might might = enumerator.Current;
						if (might.ShadowCloneCount > 0)
						{
							long value = timeMS;
							if (num > 0L)
							{
								value = num;
								num = 0L;
							}
							int num2 = might.ShadowCloneCount.ToInt();
							long num3 = might.DurationInMS(1, this.State);
							long num4 = num3 / (might.Level.ToLong() + 1L);
							if (num3 < 30L)
							{
								num3 = 30L;
							}
							if (num4 < 30L)
							{
								num4 = 30L;
							}
							CDouble rightSide = might.Level * (num3 - num4) / 2 + might.CurrentDuration;
							CDouble leftSide = value * might.ShadowCloneCount + rightSide;
							CDouble cDouble = Math.Sqrt((leftSide / num4).Double) * 1.414215;
							int num5 = (cDouble.Floored - might.Level).ToInt();
							if (num5 > 0)
							{
								if (might.NextAt > 0 && might.Level + num5 >= might.NextAt)
								{
									int num6 = num5;
									num5 = 0;
									if (might.Level < might.NextAt)
									{
										num5 = might.NextAt - might.Level.ToInt();
									}
									num6 -= num5;
									num = (num4 * might.Level.ToLong() * (long)num6 + num4 * (might.Level.ToLong() + (long)num5 + (long)num6) * (long)num6) / 2L / (long)num2;
									might.RemoveCloneCount(num2);
									Might might2 = this.State.AllMights.FirstOrDefault((Might x) => x.TypeEnum == might.TypeEnum + 1);
									if (might2 == null)
									{
										might2 = this.State.AllMights[0];
									}
									might2.AddCloneCount(num2);
								}
								if (num5 > 0)
								{
									might.CurrentDuration = (num3 * (cDouble - cDouble.Floored)).ToLong();
									might.Level += num5;
									this.State.PremiumBoni.TotalMight += (long)num5;
									infoBuilder.Append("- gained ").Append(num5).Append(" levels in ' ").Append(might.Name).Append(" '\n");
								}
							}
							if (num5 == 0 && might.Level < might.NextAt)
							{
								might.CurrentDuration += timeMS * (long)num2;
							}
						}
						if (might.DurationLeft > 0L)
						{
							might.DurationLeft -= timeMS;
							if (might.DurationLeft <= 0L)
							{
								might.DurationLeft = 0L;
								might.UseCoolDown = 3600000L;
							}
						}
						if (might.UseCoolDown > 0L)
						{
							might.UseCoolDown -= timeMS;
							if (might.UseCoolDown <= 0L)
							{
								might.UseCoolDown = 0L;
							}
						}
					}
				}
			}
		}

		private void CalcDivGen(long timeMS, StringBuilder infoBuilder)
		{
			if (this.State.Generator.ShadowCloneCount > 0 && !this.State.Generator.IsBuilt)
			{
				int num = this.State.Generator.ShadowCloneCount.ToInt();
				CDouble cDouble = timeMS * (long)num * (long)this.State.PremiumBoni.MonumentBuildTimeDivider * (100 + this.State.PremiumBoni.BuildingSpeedUpPercent(true)) / 100;
				long value = this.State.Generator.DurationInMS(1) - this.State.Generator.CurrentDuration;
				if (cDouble >= value)
				{
					this.State.Generator.CurrentDuration = 0L;
					this.State.Generator.IsPaid = false;
					this.State.Generator.IsBuilt = true;
					this.State.Clones.RemoveUsedShadowClones(num);
					this.State.Generator.ShadowCloneCount = 0;
					infoBuilder.Append("- divinity generator was built\n");
				}
				else
				{
					this.State.Generator.CurrentDuration += cDouble.ToLong();
					cDouble = 0;
				}
			}
			foreach (GeneratorUpgrade current in this.State.Generator.Upgrades)
			{
				if (current.ShadowCloneCount > 0)
				{
					int num2 = current.ShadowCloneCount.ToInt();
					CDouble cDouble2 = timeMS * (long)num2 * (long)this.State.PremiumBoni.MonumentBuildTimeDivider * (100 + this.State.PremiumBoni.BuildingSpeedUpPercent(true)) / 100;
					int num3 = 0;
					while (cDouble2 > 0)
					{
						if (current.StopAt != 0 && current.StopAt <= current.Level)
						{
							this.State.Clones.RemoveUsedShadowClones(current.ShadowCloneCount);
							current.ShadowCloneCount = 0;
							cDouble2 = 0;
						}
						else
						{
							long value2 = current.DurationInMS(1) - current.CurrentDuration;
							if (cDouble2 >= value2)
							{
								cDouble2 -= value2;
								current.CurrentDuration = 0L;
								bool flag = false;
								if (!current.IsPaid)
								{
									foreach (CreationCost current2 in current.RequiredCreations)
									{
										if (!CreationCost.HasCreations(this.State, current2, this.State.GameSettings.AutoBuyCreationsForMonuments))
										{
											flag = true;
											current.CurrentDuration = current.DurationInMS(1);
											cDouble2 = 0;
											break;
										}
									}
								}
								if (!flag)
								{
									if (!current.IsPaid)
									{
										using (List<CreationCost>.Enumerator enumerator3 = current.RequiredCreations.GetEnumerator())
										{
											while (enumerator3.MoveNext())
											{
												CreationCost cost = enumerator3.Current;
												Creation creation = this.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
												creation.Count -= cost.CountNeeded;
											}
										}
									}
									else
									{
										current.IsPaid = false;
									}
									num3++;
									GeneratorUpgrade expr_3BE = current;
									expr_3BE.Level = ++expr_3BE.Level;
									if (this.State.GameSettings.StopDivinityGenBuilding)
									{
										this.State.Clones.RemoveUsedShadowClones(current.ShadowCloneCount);
										current.ShadowCloneCount = 0;
										current.CurrentDuration = 0L;
										cDouble2 = 0;
									}
								}
							}
							else
							{
								current.CurrentDuration += cDouble2.ToLong();
								cDouble2 = 0;
							}
						}
					}
					if (num3 > 0)
					{
						infoBuilder.Append("- upgrade: ").Append(current.Name).Append(" was built ").Append(num3).Append(" x\n");
					}
				}
			}
			infoBuilder.Append("After your break, you feel refreshed and your creation speed is tripled for the next ").Append(Conv.MsToGuiText(this.State.CreatingSpeedBoniDuration, true)).Append("!");
		}

        //[DebuggerHidden]
        //private IEnumerator Calculate(long timeOfflineSec)
        //{
        //    OfflineCalc.<Calculate>c__Iterator0 <Calculate>c__Iterator = new OfflineCalc.<Calculate>c__Iterator0();
        //    <Calculate>c__Iterator.timeOfflineSec = timeOfflineSec;
        //    <Calculate>c__Iterator.$this = this;
        //    return <Calculate>c__Iterator;
        //}

		private void CalcDivGained(StringBuilder infoBuilder, long timeMS)
		{
			bool flag = false;
			CDouble cDouble = new CDouble();
			foreach (Fight current in this.State.AllFights)
			{
				if (current.ShadowCloneCount > 0)
				{
					int value = current.ShadowCloneCount.ToInt();
					CDouble leftSide = (this.State.CloneAttack + 10) * value;
					CDouble cDouble2 = 30 * (leftSide - current.Defense) / 5000 + 1;
					if (cDouble2 > 0)
					{
						double num;
						if (cDouble2 >= current.MaxHealth)
						{
							num = 33.33333333;
						}
						else
						{
							double num2 = (double)(current.MaxHealth * 64 / cDouble2).ToInt() / 64.0;
							double num3 = num2 * 30.0 / 1000.0;
							num = 1.0 / num3;
						}
						long value2 = (long)((double)(timeMS / 1000L) * num);
						current.Level += value2;
						this.State.BattlePowerBase += current.PowerGain * value2;
						this.State.Statistic.TotalEnemiesDefeated += value2;
						infoBuilder.Append("- defeated ").Append(value2).Append(" x ").Append(current.Name).Append("\n");
						cDouble += current.MoneyGain * value2;
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.State.Money += cDouble;
				infoBuilder.Append("\n- divinity gained: ").Append(cDouble.ToGuiText(true)).Append(" \n");
			}
			if (this.State.Generator.FilledCapacity > 0)
			{
				CDouble cDouble3 = this.State.Generator.ConvertSec * timeMS / 1000;
				CDouble rightSide = 0;
				CDouble rightSide2 = timeMS * (long)this.State.ClonesDifGenMod / 20L;
				CDouble cDouble4 = this.State.Generator.ShadowCloneCount * rightSide2;
				Creation creation = this.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Stone);
				if (cDouble4 > creation.Count)
				{
					cDouble4 = creation.Count;
				}
				CDouble cDouble5 = cDouble4 * creation.BuyCost;
				CDouble leftSide2 = 0;
				if (cDouble5 >= cDouble3)
				{
					leftSide2 = cDouble5 - cDouble3;
					cDouble5 = cDouble3;
					cDouble4 -= leftSide2 / creation.BuyCost;
				}
				else
				{
					rightSide = cDouble3 - cDouble5;
				}
				creation.Count -= cDouble4;
				if (this.State.Generator.FilledCapacity >= rightSide)
				{
					this.State.Generator.FilledCapacity -= rightSide;
				}
				else
				{
					rightSide = this.State.Generator.FilledCapacity;
					this.State.Generator.FilledCapacity = 0;
				}
				cDouble3 = cDouble5 + rightSide;
				CDouble cDouble6 = cDouble3 * this.State.Generator.DivinityEachCapacity;
				if (leftSide2 > 0)
				{
					int num4 = this.State.Generator.ShadowCloneCount.ToInt() - this.State.Generator.GetBreakEvenWorker(creation.BuyCost);
					CDouble leftSide3 = num4 / 5000;
					cDouble6 = cDouble6 * (leftSide3 + 100) / 100;
				}
				string value3 = string.Empty;
				if (cDouble4 > 0)
				{
					value3 = " and used up " + cDouble4.ToGuiText(true) + " stones.";
				}
				infoBuilder.Append("- generated divinity: ").Append(cDouble6.ToGuiText(true)).Append(value3).Append(" \n\n");
				this.State.Money += cDouble6;
			}
		}
	}
}
