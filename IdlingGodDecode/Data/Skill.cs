using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Data
{
	public class Skill : TrainingBase
	{
		public enum SkillType
		{
			double_punch,
			high_kick,
			dodge,
			shadow_fist,
			focused_breathing,
			raging_fist,
			defensive_aura,
			misdirection,
			whirling_foot,
			invisible_hand,
			dragon_fist,
			offense_aura,
			elemental_manipulation,
			earth_armor,
			ice_wall,
			clairvoyance,
			aura_ball,
			mystic_mode,
			a_108_star_fist,
			big_bang,
			god_speed,
			teleport,
			transformation_aura,
			gear_eyes,
			reflection_barrier,
			ionioi_hero_summon,
			unlimited_creation_works,
			time_manipulation
		}

		public Skill.SkillType TypeEnum;

		public SkillExtension Extension;

		private int minLevel = 500;

		private string desc = string.Empty;

		public int CapCount = 1;

		public bool IsAvailable
		{
			get
			{
				int num = this.EnumValue - 1;
				return num == -1 || App.State.AllSkills[num].Level > this.minLevel;
			}
		}

		public string Name
		{
			get
			{
				if (!this.IsAvailable)
				{
					return "???";
				}
				return EnumName.Name(this.TypeEnum);
			}
		}

		public string Description
		{
			get
			{
				if (!this.IsAvailable)
				{
					return "You still need to think about this skill.";
				}
				if (!string.IsNullOrEmpty(this.desc) && !this.ShouldUpdateText)
				{
					return this.desc;
				}
				switch (this.TypeEnum)
				{
				case Skill.SkillType.double_punch:
					this.desc = "You throw a punch not once, but twice! With half as much power.";
					break;
				case Skill.SkillType.high_kick:
					this.desc = "With a high kick you you can finally kick where your punches didn't have any effect.";
					break;
				case Skill.SkillType.dodge:
					this.desc = "It's important to learn how to dodge right, so you don't get hit as often.";
					break;
				case Skill.SkillType.shadow_fist:
					this.desc = "It's a fist as fast as your shadow!";
					break;
				case Skill.SkillType.focused_breathing:
					this.desc = "Now you can learn how to breathe. If you focus right, you can even forget about your surroundings!";
					break;
				case Skill.SkillType.raging_fist:
					this.desc = "Thats a powerful fist with all your rage. But it's kinda hard to hit with it.";
					break;
				case Skill.SkillType.defensive_aura:
					this.desc = "You learn how to build an aura around your body to decrease the damage you take.";
					break;
				case Skill.SkillType.misdirection:
					this.desc = "If done right, your enemy will always lose focus on you.";
					break;
				case Skill.SkillType.whirling_foot:
					this.desc = "You kick with your foot like a propeller while trying to hit the enemy.";
					break;
				case Skill.SkillType.invisible_hand:
					this.desc = "A really fast punch so it looks like your hand is invisible.";
					break;
				case Skill.SkillType.dragon_fist:
					this.desc = "Thats a really strong uppercut which can even hit a dragon.";
					break;
				case Skill.SkillType.offense_aura:
					this.desc = "After mastering your defensive aura, you can even use it to increase your offensive power.";
					break;
				case Skill.SkillType.elemental_manipulation:
					this.desc = "You learn how to manipulate the elements like earth, water, wind and fire.";
					break;
				case Skill.SkillType.earth_armor:
					this.desc = "With a good understanding of the earth element you can build an armor of earth around your defensive aura.";
					break;
				case Skill.SkillType.ice_wall:
					this.desc = "As for your water elemental power, you can now create an wall of ice. So the enemy will always freeze his hand, if you do it right.";
					break;
				case Skill.SkillType.clairvoyance:
					this.desc = "The wind will tell you what the enemy might do even before he knows it himself!";
					break;
				case Skill.SkillType.aura_ball:
					this.desc = "Thats your power of fire combined with your offense aura. You create a ball of fire aura and toss it to your enemy.";
					break;
				case Skill.SkillType.mystic_mode:
					this.desc = "After some deep concentration you will be able to enter your mystic mode. In this mode everything becomes clearer and all your powers are increased.";
					break;
				case Skill.SkillType.a_108_star_fist:
					this.desc = "You know of 108 stars. So you feel like it's your destiny to create a skill where you punch your enemy in a 108 star shaped matter.";
					break;
				case Skill.SkillType.big_bang:
					this.desc = "In theory you can create a really big blast with this. It's also really loud, so it's called bang instead of blast.";
					break;
				case Skill.SkillType.god_speed:
					this.desc = "You infuse yourself with the speed of lightning and call this the speed of god. Cause that's who you are!";
					break;
				case Skill.SkillType.teleport:
					this.desc = "After mastering your god speed, you are so fast, you don't even need to go all distances. Now you can skip parts of it.";
					break;
				case Skill.SkillType.transformation_aura:
					this.desc = "That's a higher level of your mystic mode. You shape your aura around your mystic mode which somehow causes your hair to become yellow and spiky.";
					break;
				case Skill.SkillType.gear_eyes:
					this.desc = "With eyes like this you can command everyone who looks into your eyes to do everything you want!";
					break;
				case Skill.SkillType.reflection_barrier:
					this.desc = "The perfect barrier. Just sit down and wait for your enemies to attack you only to let them feel their own attack themselves.";
					break;
				case Skill.SkillType.ionioi_hero_summon:
					this.desc = "A funny name but really powerful. With this you call the heroes in your mind to let them attack everyone who stands in your way!";
					break;
				case Skill.SkillType.unlimited_creation_works:
					this.desc = "You create another dimension where you can call all your creations with just thinking about them.";
					break;
				case Skill.SkillType.time_manipulation:
					this.desc = "Probably the most powerful ability of all. If something went wrong just manipulate the time to go back and try again.";
					break;
				}
				this.CapCount = base.DurationInMS(1) / 30 + 1;
				this.desc = string.Concat(new object[]
				{
					this.desc,
					"\n\nCapped at: ",
					this.CapCount,
					" clones",
					this.PowerGainInSecText
				});
				this.ShouldUpdateText = false;
				return this.desc;
			}
		}

		public string PowerGainInSecText
		{
			get
			{
				int count = 1;
				return "lowerText" + this.PowerGainInSec(count).ToGuiText(true) + " Mystic / s (with 1 Clone)";
			}
		}

		public Skill(Skill.SkillType type)
		{
			this.TypeEnum = type;
			this.EnumValue = (int)type;
			this.Extension = new SkillExtension(this.EnumValue);
			this.minLevel = this.EnumValue * 500 - 1;
		}

		internal static List<Skill> Initial()
		{
			return new List<Skill>
			{
				new Skill(Skill.SkillType.double_punch),
				new Skill(Skill.SkillType.high_kick),
				new Skill(Skill.SkillType.dodge),
				new Skill(Skill.SkillType.shadow_fist),
				new Skill(Skill.SkillType.focused_breathing),
				new Skill(Skill.SkillType.raging_fist),
				new Skill(Skill.SkillType.defensive_aura),
				new Skill(Skill.SkillType.misdirection),
				new Skill(Skill.SkillType.whirling_foot),
				new Skill(Skill.SkillType.invisible_hand),
				new Skill(Skill.SkillType.dragon_fist),
				new Skill(Skill.SkillType.offense_aura),
				new Skill(Skill.SkillType.elemental_manipulation),
				new Skill(Skill.SkillType.earth_armor),
				new Skill(Skill.SkillType.ice_wall),
				new Skill(Skill.SkillType.clairvoyance),
				new Skill(Skill.SkillType.aura_ball),
				new Skill(Skill.SkillType.mystic_mode),
				new Skill(Skill.SkillType.a_108_star_fist),
				new Skill(Skill.SkillType.big_bang),
				new Skill(Skill.SkillType.god_speed),
				new Skill(Skill.SkillType.teleport),
				new Skill(Skill.SkillType.transformation_aura),
				new Skill(Skill.SkillType.gear_eyes),
				new Skill(Skill.SkillType.reflection_barrier),
				new Skill(Skill.SkillType.ionioi_hero_summon),
				new Skill(Skill.SkillType.unlimited_creation_works),
				new Skill(Skill.SkillType.time_manipulation)
			};
		}

		public CDouble PowerGainInSec(int count)
		{
			int num = base.DurationInMS(count) + 1;
			if (num < 30)
			{
				num = 30;
			}
			int num2 = num % 30;
			if (num2 != 0)
			{
				num = num - num2 + 30;
			}
			return base.PowerGain * App.State.Multiplier.CurrentMultiMystic * 1000 / num;
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.EnumValue);
			Conv.AppendValue(stringBuilder, "b", this.Level.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.ShadowCloneCount.Serialize());
			Conv.AppendValue(stringBuilder, "d", this.CurrentDuration);
			Conv.AppendValue(stringBuilder, "e", this.Extension.Serialize());
			return Conv.ToBase64(stringBuilder.ToString(), "Skills");
		}

		internal static Skill FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("Skill.FromString with empty value!");
				return new Skill(Skill.SkillType.double_punch);
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "Skill");
			return new Skill((Skill.SkillType)Conv.getIntFromParts(parts, "a"))
			{
				Level = new CDouble(Conv.getStringFromParts(parts, "b")),
				ShadowCloneCount = Conv.getCDoubleFromParts(parts, "c", false),
				CurrentDuration = Conv.getLongFromParts(parts, "d"),
				Extension = SkillExtension.FromString(Conv.getStringFromParts(parts, "e"))
			};
		}
	}
}
