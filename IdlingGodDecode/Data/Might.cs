using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
	public class Might
	{
		public enum MightType
		{
			physical_hp,
			physical_attack,
			mystic_def,
			mystic_regen,
			battle,
			autofill_gen,
			planet_power,
			powersurge_speed,
			focused_breathing,
			defensive_aura,
			offense_aura,
			elemental_manipulation,
			mystic_mode,
			transformation_aura
		}

		public Might.MightType TypeEnum;

		public bool IsUsable;

		public CDouble Level = 0;

		public CDouble ShadowCloneCount = 0;

		public long CurrentDuration;

		public int UseDurationBase;

		public long UseCoolDown;

		public long DurationLeft;

		public int NextAt;

		public int UnleashRegenBoni;

		public int UnleashAttackBoni;

		public int UnleashDefenseBoni;

		public bool ShouldUpdateText = true;

		private string desc = string.Empty;

		public long UseDuration
		{
			get
			{
				return (long)this.UseDurationBase + this.Level.ToLong();
			}
		}

		public string Name
		{
			get
			{
				return EnumName.Name(this.TypeEnum);
			}
		}

		public string Description
		{
			get
			{
				if (!string.IsNullOrEmpty(this.desc) && !this.ShouldUpdateText)
				{
					return this.desc;
				}
				int value = 100;
				switch (this.TypeEnum)
				{
				case Might.MightType.physical_hp:
					this.desc = "Each level increases the hp physical provides by 1%";
					break;
				case Might.MightType.physical_attack:
					this.desc = "Each level increases the attack physical provides by 1%";
					value = 25;
					break;
				case Might.MightType.mystic_def:
					this.desc = "Each level increases the defense mystic provides by 1%";
					break;
				case Might.MightType.mystic_regen:
					this.desc = "Each level increases the hp-recover speed mystic provides by 1%";
					break;
				case Might.MightType.battle:
					this.desc = "Each level increases the attack battle provides by 2%";
					break;
				case Might.MightType.autofill_gen:
					this.desc = "Each level increases the speed your clones can refill the divinity generator by 1% ";
					break;
				case Might.MightType.planet_power:
					this.desc = "Each level increases the attack and defense of your clones when fighting ultimate beings by 1%";
					break;
				case Might.MightType.powersurge_speed:
					this.desc = "Each level increases the speed of powersurge by 1%";
					break;
				case Might.MightType.focused_breathing:
					this.desc = "Each level increases the duration of the unleash by 1 second.";
					break;
				case Might.MightType.defensive_aura:
					this.desc = "Each level increases the duration of the unleash by 1 second.";
					break;
				case Might.MightType.offense_aura:
					this.desc = "Each level increases the duration of the unleash by 1 second.";
					break;
				case Might.MightType.elemental_manipulation:
					this.desc = "Each level increases the duration of the unleash by 1 second.";
					break;
				case Might.MightType.mystic_mode:
					this.desc = "Each level increases the duration of the unleash by 1 second.";
					break;
				case Might.MightType.transformation_aura:
					this.desc = "Each level increases the duration of the unleash by 1 second.";
					break;
				}
				this.desc += "lowerText";
				CDouble leftSide = this.Level;
				if (this.TypeEnum == Might.MightType.battle)
				{
					leftSide *= 2;
				}
				if (this.IsUsable)
				{
					this.desc = string.Concat(new object[]
					{
						this.desc,
						"Current Duration: ",
						leftSide + this.UseDurationBase,
						" sec"
					});
				}
				else
				{
					this.desc = string.Concat(new object[]
					{
						this.desc,
						"Total : ",
						leftSide + value,
						"%"
					});
				}
				this.desc = this.desc + "\n" + this.DurationInfo;
				this.ShouldUpdateText = false;
				return this.desc;
			}
		}

		public string UnleashDesc
		{
			get
			{
				string text = "Unleash the power of " + this.Name.Replace("+", string.Empty) + "to increase ";
				StringBuilder stringBuilder = new StringBuilder();
				if (this.UnleashAttackBoni > 0)
				{
					stringBuilder.Append("Attack by ").Append(this.UnleashAttackBoni).Append(" %");
				}
				if (this.UnleashDefenseBoni > 0)
				{
					stringBuilder.Append(", Mystic by ").Append(this.UnleashDefenseBoni).Append(" %");
				}
				if (this.UnleashRegenBoni > 0)
				{
					stringBuilder.Append(", HP Recover by ").Append(this.UnleashRegenBoni).Append(" %");
				}
				string text2 = stringBuilder.ToString();
				if (text2.StartsWith(", "))
				{
					text2 = text2.Substring(2);
				}
				return string.Concat(new object[]
				{
					text,
					text2,
					" for ",
					this.UseDuration,
					" seconds."
				});
			}
		}

		private string DurationInfo
		{
			get
			{
				int num = this.ShadowCloneCount.ToInt();
				if (num == 0)
				{
					num = 1000;
				}
				long time = (this.DurationInMS(1, App.State) - this.CurrentDuration) / (long)num;
				return "Time to level up: " + Conv.MsToGuiText(time, true) + string.Format(" ({0} Clones)", num);
			}
		}

		public Might(Might.MightType type)
		{
			this.TypeEnum = type;
		}

		internal static List<Might> Initial()
		{
			return new List<Might>
			{
				new Might(Might.MightType.physical_hp),
				new Might(Might.MightType.physical_attack),
				new Might(Might.MightType.mystic_def),
				new Might(Might.MightType.mystic_regen),
				new Might(Might.MightType.battle),
				new Might(Might.MightType.autofill_gen),
				new Might(Might.MightType.planet_power),
				new Might(Might.MightType.powersurge_speed),
				new Might(Might.MightType.focused_breathing)
				{
					IsUsable = true,
					UseDurationBase = 30,
					UnleashRegenBoni = 100
				},
				new Might(Might.MightType.defensive_aura)
				{
					IsUsable = true,
					UseDurationBase = 25,
					UnleashDefenseBoni = 100
				},
				new Might(Might.MightType.offense_aura)
				{
					IsUsable = true,
					UseDurationBase = 20,
					UnleashAttackBoni = 100
				},
				new Might(Might.MightType.elemental_manipulation)
				{
					IsUsable = true,
					UseDurationBase = 15,
					UnleashAttackBoni = 100,
					UnleashDefenseBoni = 100
				},
				new Might(Might.MightType.mystic_mode)
				{
					IsUsable = true,
					UseDurationBase = 10,
					UnleashAttackBoni = 150,
					UnleashDefenseBoni = 150
				},
				new Might(Might.MightType.transformation_aura)
				{
					IsUsable = true,
					UseDurationBase = 5,
					UnleashAttackBoni = 200,
					UnleashDefenseBoni = 200,
					UnleashRegenBoni = 200
				}
			};
		}

		public void AddCloneCount(CDouble count)
		{
			CDouble availableClones = App.State.GetAvailableClones(false);
			if (count > availableClones)
			{
				count = availableClones;
			}
			if (count < 0)
			{
				count = 0;
			}
			App.State.Clones.UseShadowClones(count);
			this.ShadowCloneCount += count;
			this.ShadowCloneCount.Round();
			this.ShouldUpdateText = true;
		}

		public void RemoveCloneCount(int count)
		{
			if (this.ShadowCloneCount <= count)
			{
				count = this.ShadowCloneCount.ToInt();
			}
			this.ShadowCloneCount -= count;
			this.ShadowCloneCount.Round();
			App.State.Clones.RemoveUsedShadowClones(count);
			this.ShouldUpdateText = true;
		}

		public double getPercent()
		{
			return (double)this.CurrentDuration / (double)this.DurationInMS(1, App.State);
		}

		public void Unleash()
		{
			this.DurationLeft = this.UseDuration * 1000L;
		}

		public void UpdateDuration(long ms)
		{
			if (this.DurationLeft > 0L)
			{
				this.DurationLeft -= ms;
				if (this.DurationLeft <= 0L)
				{
					this.DurationLeft = 0L;
					this.UseCoolDown = 3600000L;
				}
			}
			else if (this.UseCoolDown > 0L)
			{
				this.UseCoolDown -= ms;
				if (this.UseCoolDown <= 0L)
				{
					this.UseCoolDown = 0L;
				}
			}
			if (this.ShadowCloneCount == 0)
			{
				return;
			}
			if (this.NextAt > 0 && this.Level >= this.NextAt)
			{
				int value = this.ShadowCloneCount.ToInt();
				this.RemoveCloneCount(this.ShadowCloneCount.ToInt());
				Might might = App.State.AllMights.FirstOrDefault((Might x) => x.TypeEnum == this.TypeEnum + 1);
				if (might == null)
				{
					might = App.State.AllMights[0];
				}
				might.AddCloneCount(value);
				return;
			}
			this.CurrentDuration += ms * (long)this.ShadowCloneCount.ToInt();
			long num = this.DurationInMS(1, App.State);
			if (this.CurrentDuration > num)
			{
				this.CurrentDuration = 0L;
				this.Level = ++this.Level;
				App.State.PremiumBoni.TotalMight += 1L;
			}
		}

		public long DurationInMS(int shadowCloneCount, GameState state)
		{
			double num = 1000000000.0;
			if (shadowCloneCount == 0)
			{
				return (long)num;
			}
			switch (this.TypeEnum)
			{
			case Might.MightType.focused_breathing:
				num = 500000000.0;
				break;
			case Might.MightType.defensive_aura:
				num = 750000000.0;
				break;
			case Might.MightType.offense_aura:
				num = 1000000000.0;
				break;
			case Might.MightType.elemental_manipulation:
				num = 1500000000.0;
				break;
			case Might.MightType.mystic_mode:
				num = 2000000000.0;
				break;
			case Might.MightType.transformation_aura:
				num = 2500000000.0;
				break;
			}
			CDouble cDouble = shadowCloneCount;
			if (state != null && state.Statistic.OnekChallengesFinished > 0)
			{
				CDouble leftSide = state.Statistic.OnekChallengesFinished;
				if (leftSide > 40)
				{
					leftSide = 40;
				}
				cDouble = cDouble * (100 + leftSide * 5) / 100;
			}
			return (num / cDouble * (1 + this.Level)).ToLong();
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", (int)this.TypeEnum);
			Conv.AppendValue(stringBuilder, "b", this.Level.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.ShadowCloneCount.Serialize());
			Conv.AppendValue(stringBuilder, "d", this.CurrentDuration);
			Conv.AppendValue(stringBuilder, "e", this.IsUsable.ToString());
			Conv.AppendValue(stringBuilder, "g", this.UseDurationBase);
			Conv.AppendValue(stringBuilder, "h", this.UseCoolDown);
			Conv.AppendValue(stringBuilder, "i", this.UnleashRegenBoni);
			Conv.AppendValue(stringBuilder, "j", this.UnleashAttackBoni);
			Conv.AppendValue(stringBuilder, "k", this.UnleashDefenseBoni);
			Conv.AppendValue(stringBuilder, "l", this.DurationLeft);
			Conv.AppendValue(stringBuilder, "m", this.NextAt);
			return Conv.ToBase64(stringBuilder.ToString(), "Might");
		}

		internal static Might FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("Might.FromString with empty value!");
				return new Might(Might.MightType.autofill_gen);
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "Might");
			return new Might((Might.MightType)Conv.getIntFromParts(parts, "a"))
			{
				Level = Conv.getCDoubleFromParts(parts, "b", false),
				ShadowCloneCount = Conv.getCDoubleFromParts(parts, "c", false),
				CurrentDuration = Conv.getLongFromParts(parts, "d"),
				IsUsable = Conv.getStringFromParts(parts, "e").ToLower().Equals("true"),
				UseDurationBase = Conv.getIntFromParts(parts, "g"),
				UseCoolDown = Conv.getLongFromParts(parts, "h"),
				UnleashRegenBoni = Conv.getIntFromParts(parts, "i"),
				UnleashAttackBoni = Conv.getIntFromParts(parts, "j"),
				UnleashDefenseBoni = Conv.getIntFromParts(parts, "k"),
				DurationLeft = Conv.getLongFromParts(parts, "l"),
				NextAt = Conv.getIntFromParts(parts, "m")
			};
		}
	}
}
