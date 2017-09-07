using Assets.Scripts.Helper;
using System;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class SkillExtension
	{
		internal string desc;

		internal long CoolDownBase;

		internal long GodSpeedModeDuration;

		internal long GearEyesDuration;

		internal bool DoubleUp;

		internal bool DamageBlock;

		internal bool DamageReflect;

		internal int Hitcount;

		internal int Hitchance;

		internal int HitchanceBonus;

		internal int DamagePercent;

		internal int HealPercent;

		internal int DodgeChance;

		internal int CounterChance;

		internal int DamageDecreasePercent;

		internal long DamageDecreaseDuration;

		internal int DamageIncreasePercent;

		internal long DamageIncreaseDuration;

		public int SkillId;

		public long UsageCount;

		public long CoolDownCurrent;

		public KeyCode KeyPress;

		public string Description
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(this.desc);
				if (!string.IsNullOrEmpty(this.desc))
				{
					stringBuilder.Append("\n");
				}
				int num = this.DamagePercent;
				int num2 = this.Hitcount;
				if (this.SkillId == 26)
				{
					int num3 = 0;
					foreach (Creation current in App.State.AllCreations)
					{
						if (current.CanBuy)
						{
							num3++;
						}
					}
					num = this.DamagePercent * num3;
				}
				else if (this.SkillId == 25)
				{
					num2 = App.State.Statistic.HighestGodDefeated.ToInt();
				}
				if (num != 0 && num2 != 0)
				{
					stringBuilder.Append("Base Damage: ").Append(num2).Append(" x ").Append(num).Append(".\n");
				}
				if (this.Hitchance != 0)
				{
					stringBuilder.Append("Hit chance: ").Append(this.Hitchance).Append(" %. \n");
				}
				if (this.HitchanceBonus != 0)
				{
					stringBuilder.Append("Hit chance Modifier: ").Append(this.HitchanceBonus).Append(" %. \n");
				}
				if (this.HealPercent != 0)
				{
					stringBuilder.Append("Heal yourself for ").Append(this.HealPercent).Append(" %. \n");
				}
				if (this.DodgeChance != 0)
				{
					stringBuilder.Append("Dodge next attack: ").Append(this.DodgeChance).Append(" %. \n");
				}
				if (this.CounterChance != 0)
				{
					stringBuilder.Append("Counter next attack: ").Append(this.CounterChance).Append(" %. \n");
				}
				if (this.DamageIncreasePercent != 0)
				{
					stringBuilder.Append("Increases the damage you do by ").Append(this.DamageIncreasePercent).Append(" % for ").Append(this.DamageIncreaseDuration).Append(" ms.\n");
				}
				if (this.DamageDecreasePercent != 0)
				{
					stringBuilder.Append("Decreases the damage you take by ").Append(this.DamageDecreasePercent).Append(" % for ").Append(this.DamageDecreaseDuration).Append(" ms.\n");
				}
				if (this.DoubleUp)
				{
					stringBuilder.Append("Doubles the power of your next attack skill.\n");
				}
				if (this.DamageBlock)
				{
					stringBuilder.Append("Nullfies the next enemy attack.\n");
				}
				if (this.DamageReflect)
				{
					stringBuilder.Append("Reflects the next enemy attack.\n");
				}
				stringBuilder.Append("Cooldown: ").Append(this.CoolDownBase).Append(" ms.\n");
				stringBuilder.Append("Usage count: ").Append(this.UsageCount).Append(".\n");
				return stringBuilder.ToString();
			}
		}

		public SkillExtension(int skillId)
		{
			this.SkillId = skillId;
			switch (skillId)
			{
			case 0:
				this.DamagePercent = 50;
				this.Hitchance = 50;
				this.Hitcount = 2;
				this.CoolDownBase = 1000L;
				this.KeyPress = KeyCode.Q;
				this.desc = "One - two and hit or miss the enemy.";
				break;
			case 1:
				this.DamagePercent = 80;
				this.Hitchance = 75;
				this.Hitcount = 1;
				this.CoolDownBase = 1000L;
				this.KeyPress = KeyCode.W;
				this.desc = "Kicks for more damage!";
				break;
			case 2:
				this.DodgeChance = 60;
				this.CoolDownBase = 7000L;
				this.desc = "Maybe you are lucky.";
				this.KeyPress = KeyCode.E;
				break;
			case 3:
				this.DamagePercent = 75;
				this.Hitchance = 100;
				this.Hitcount = 1;
				this.CoolDownBase = 2500L;
				this.KeyPress = KeyCode.R;
				this.desc = "So fast the enemy can't dodge.";
				break;
			case 4:
				this.HealPercent = 15;
				this.CoolDownBase = 16000L;
				this.desc = "Your only healing skill.";
				this.KeyPress = KeyCode.A;
				break;
			case 5:
				this.DamagePercent = 500;
				this.HealPercent = -5;
				this.Hitchance = 25;
				this.Hitcount = 1;
				this.CoolDownBase = 7500L;
				this.KeyPress = KeyCode.S;
				this.desc = "Really powerful but...";
				break;
			case 6:
				this.DamageDecreasePercent = 20;
				this.DamageDecreaseDuration = 15000L;
				this.CoolDownBase = 15000L;
				this.KeyPress = KeyCode.D;
				this.desc = string.Empty;
				break;
			case 7:
				this.DodgeChance = 75;
				this.HitchanceBonus = 50;
				this.CoolDownBase = 20000L;
				this.KeyPress = KeyCode.F;
				this.desc = "Now even Raging fist makes sense.";
				break;
			case 8:
				this.DamagePercent = 45;
				this.Hitchance = 60;
				this.Hitcount = 3;
				this.CoolDownBase = 3000L;
				this.KeyPress = KeyCode.Y;
				this.desc = "Whirl your foot for 3 hits.";
				break;
			case 9:
				this.DamagePercent = 45;
				this.Hitchance = 100;
				this.Hitcount = 2;
				this.CoolDownBase = 3000L;
				this.KeyPress = KeyCode.X;
				this.desc = "Even faster than shadow fist and hits twice!";
				break;
			case 10:
				this.DamagePercent = 250;
				this.Hitchance = 30;
				this.Hitcount = 1;
				this.CoolDownBase = 4000L;
				this.KeyPress = KeyCode.C;
				this.desc = "Fist of the dragon. Hits really hard but easy to dodge.";
				break;
			case 11:
				this.DamageIncreasePercent = 30;
				this.DamageIncreaseDuration = 60000L;
				this.CoolDownBase = 40000L;
				this.KeyPress = KeyCode.V;
				this.desc = string.Empty;
				break;
			case 12:
				this.DoubleUp = true;
				this.CoolDownBase = 25000L;
				this.KeyPress = KeyCode.T;
				this.desc = string.Empty;
				break;
			case 13:
				this.DamageDecreasePercent = 30;
				this.DamageDecreaseDuration = 20000L;
				this.CoolDownBase = 30000L;
				this.KeyPress = KeyCode.Z;
				this.desc = string.Empty;
				break;
			case 14:
				this.CoolDownBase = 12000L;
				this.DamageBlock = true;
				this.KeyPress = KeyCode.U;
				this.desc = string.Empty;
				break;
			case 15:
				this.DodgeChance = 50;
				this.CounterChance = 50;
				this.CoolDownBase = 15000L;
				this.KeyPress = KeyCode.I;
				this.desc = string.Empty;
				break;
			case 16:
				this.DamagePercent = 150;
				this.Hitchance = 75;
				this.Hitcount = 1;
				this.CoolDownBase = 9001L;
				this.KeyPress = KeyCode.G;
				this.desc = "Tosses an aura ball for huge damage to the enemy.";
				break;
			case 17:
				this.DamageIncreasePercent = 50;
				this.DamageIncreaseDuration = 15000L;
				this.DamageDecreasePercent = 50;
				this.DamageDecreaseDuration = 15000L;
				this.CoolDownBase = 45000L;
				this.KeyPress = KeyCode.H;
				this.desc = string.Empty;
				break;
			case 18:
				this.DamagePercent = 5;
				this.Hitchance = 50;
				this.Hitcount = 108;
				this.CoolDownBase = 35000L;
				this.KeyPress = KeyCode.J;
				this.desc = "Hits the enemy for 108 times in less then a second!";
				break;
			case 19:
				this.DamagePercent = 666;
				this.HealPercent = -10;
				this.Hitchance = 99;
				this.Hitcount = 1;
				this.CoolDownBase = 59999L;
				this.KeyPress = KeyCode.K;
				this.desc = "It does huge damage and looks like a bomb exploding.";
				break;
			case 20:
				this.GodSpeedModeDuration = 5000L;
				this.CoolDownBase = 12000L;
				this.KeyPress = KeyCode.B;
				this.desc = "The enemy moves in slow motion for the next 5 seconds. (Halves enemy attack speed)";
				break;
			case 21:
				this.DodgeChance = 100;
				this.CoolDownBase = 7000L;
				this.KeyPress = KeyCode.N;
				this.desc = "Teleport away just before the enemy hits you.";
				break;
			case 22:
				this.DamageIncreasePercent = 100;
				this.DamageIncreaseDuration = 15000L;
				this.DamageDecreasePercent = 50;
				this.DamageDecreaseDuration = 15000L;
				this.CoolDownBase = 45000L;
				this.KeyPress = KeyCode.M;
				this.desc = string.Empty;
				break;
			case 23:
				this.GearEyesDuration = 3000L;
				this.CoolDownBase = 13000L;
				this.KeyPress = KeyCode.Keypad1;
				this.desc = "Commands the enemy to do nothing for the next 3 seconds.";
				break;
			case 24:
				this.DamageReflect = true;
				this.CoolDownBase = 12000L;
				this.KeyPress = KeyCode.Keypad2;
				this.desc = string.Empty;
				break;
			case 25:
				this.DamagePercent = 30;
				this.Hitchance = 75;
				this.Hitcount = 1;
				this.CoolDownBase = 25000L;
				this.KeyPress = KeyCode.Keypad3;
				this.desc = "Summons every god you have defeated so far.";
				break;
			case 26:
				this.DamagePercent = 30;
				this.Hitchance = 80;
				this.Hitcount = 1;
				this.CoolDownBase = 25000L;
				this.KeyPress = KeyCode.Keypad4;
				this.desc = "Creates your highest created creation and throws it to the enemy.";
				break;
			case 27:
				this.CoolDownBase = 15000L;
				this.KeyPress = KeyCode.Keypad5;
				this.desc = "Reverts time for the last 3 enemy attacks as if they didn't even happen!";
				break;
			}
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.SkillId.ToString());
			Conv.AppendValue(stringBuilder, "b", this.UsageCount.ToString());
			Conv.AppendValue(stringBuilder, "c", (int)this.KeyPress);
			return Conv.ToBase64(stringBuilder.ToString(), "SkillExtension");
		}

		internal static SkillExtension FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("SkillExtension.FromString with empty value!");
				return new SkillExtension(0);
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "SkillExtension");
			SkillExtension skillExtension = new SkillExtension(Conv.getIntFromParts(parts, "a"));
			skillExtension.UsageCount = Conv.getLongFromParts(parts, "b");
			KeyCode intFromParts = (KeyCode)Conv.getIntFromParts(parts, "c");
			if (intFromParts != KeyCode.None)
			{
				skillExtension.KeyPress = intFromParts;
			}
			return skillExtension;
		}
	}
}
