using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Data
{
	public class Training : TrainingBase
	{
		public enum TrainingType
		{
			running,
			sit_ups,
			push_ups,
			swimming,
			long_jumps,
			shadow_boxing,
			jump_rope,
			climb_mountains,
			run_in_water,
			meditate,
			throw_spears,
			smash_rocks,
			run_with_weights,
			walk_on_tightropes,
			swimm_with_weights,
			dive_with_sharks,
			jump_on_trees,
			walk_on_water,
			walk_10_gravity,
			run_50_gravity,
			move_mountains,
			learn_to_fly,
			fly_around_the_world,
			carry_mountains,
			fly_to_the_moon,
			fly_around_the_universe,
			smash_meteorites,
			train_on_another_dimension
		}

		public Training.TrainingType TypeEnum;

		private int minLevel = 500;

		private string desc = string.Empty;

		public int CapCount = 1;

		public bool IsAvailable
		{
			get
			{
				int num = this.EnumValue - 1;
				return num == -1 || App.State.AllTrainings[num].Level > this.minLevel;
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
					return "You still need to think about this training.";
				}
				if (!string.IsNullOrEmpty(this.desc) && !this.ShouldUpdateText)
				{
					return this.desc;
				}
				switch (this.TypeEnum)
				{
				case Training.TrainingType.running:
					this.desc = "Running is always good to improve your stamina and health. So run a lot!";
					break;
				case Training.TrainingType.sit_ups:
					this.desc = "Train your stomach muscles to withstand more blows.";
					break;
				case Training.TrainingType.push_ups:
					this.desc = "Strong arms are good to defend against attacks before they hit your body.";
					break;
				case Training.TrainingType.swimming:
					this.desc = "Water has more resistance than air. So it might improve your stamina more.";
					break;
				case Training.TrainingType.long_jumps:
					this.desc = "Jump long and a lot so you might be able to dodge faster.";
					break;
				case Training.TrainingType.shadow_boxing:
					this.desc = "Try to hit your shadow!";
					break;
				case Training.TrainingType.jump_rope:
					this.desc = "Jumping increases your leg power.";
					break;
				case Training.TrainingType.climb_mountains:
					this.desc = "And now try to reach the highest peak in less than a day!";
					break;
				case Training.TrainingType.run_in_water:
					this.desc = "Try to run as fast in water as on the ground.";
					break;
				case Training.TrainingType.meditate:
					this.desc = "And now meditate to let your brain think you are stronger.";
					break;
				case Training.TrainingType.throw_spears:
					this.desc = "Throw hard enough to pierce a mountain!";
					break;
				case Training.TrainingType.smash_rocks:
					this.desc = "That's how you make sand. Create a desert now.";
					break;
				case Training.TrainingType.run_with_weights:
					this.desc = "Use osmium bigger than your body as a weight and then run faster then ever.";
					break;
				case Training.TrainingType.walk_on_tightropes:
					this.desc = "A better balance is good to balance the damage you get. Maybe.";
					break;
				case Training.TrainingType.swimm_with_weights:
					this.desc = "And now take your osmium with you into the water and swim to the next continent.";
					break;
				case Training.TrainingType.dive_with_sharks:
					this.desc = "Try to go deeper and faster than they do.";
					break;
				case Training.TrainingType.jump_on_trees:
					this.desc = "Now jump from tree to tree until you reach the next country without touching the ground.";
					break;
				case Training.TrainingType.walk_on_water:
					this.desc = "Go fast enough to not sink even one millimeter.";
					break;
				case Training.TrainingType.walk_10_gravity:
					this.desc = "Go on until you can walk like you are on normal gravity.";
					break;
				case Training.TrainingType.run_50_gravity:
					this.desc = "And now the same with running until you feel like 50 times the normal gravity is nothing.";
					break;
				case Training.TrainingType.move_mountains:
					this.desc = "Move them with only your pinkie.";
					break;
				case Training.TrainingType.learn_to_fly:
					this.desc = "Now run fast enough until you can fly.";
					break;
				case Training.TrainingType.fly_around_the_world:
					this.desc = "When you finish your lap, you might go for another one.";
					break;
				case Training.TrainingType.carry_mountains:
					this.desc = "Moving them is too easy for you now, so carry them on your back while doing your normal training.";
					break;
				case Training.TrainingType.fly_to_the_moon:
					this.desc = "Please move the moon a bit to the right while you are at it.";
					break;
				case Training.TrainingType.fly_around_the_universe:
					this.desc = "Once to the end of the universe and back sounds easy right?";
					break;
				case Training.TrainingType.smash_meteorites:
					this.desc = "Throw a big punch at them while they hit you at full speed.";
					break;
				case Training.TrainingType.train_on_another_dimension:
					this.desc = "Nothing more to do in this world, but there are a lot of other dimensions where things aren't as easy!";
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
				return "lowerText" + this.PowerGainInSec(count).ToGuiText(true) + " Physical / s (with 1 Clone)";
			}
		}

		public Training(Training.TrainingType type)
		{
			this.TypeEnum = type;
			this.EnumValue = (int)type;
			this.minLevel = this.EnumValue * 500 - 1;
		}

		internal static List<Training> Initial()
		{
			return new List<Training>
			{
				new Training(Training.TrainingType.running),
				new Training(Training.TrainingType.sit_ups),
				new Training(Training.TrainingType.push_ups),
				new Training(Training.TrainingType.swimming),
				new Training(Training.TrainingType.long_jumps),
				new Training(Training.TrainingType.shadow_boxing),
				new Training(Training.TrainingType.jump_rope),
				new Training(Training.TrainingType.climb_mountains),
				new Training(Training.TrainingType.run_in_water),
				new Training(Training.TrainingType.meditate),
				new Training(Training.TrainingType.throw_spears),
				new Training(Training.TrainingType.smash_rocks),
				new Training(Training.TrainingType.run_with_weights),
				new Training(Training.TrainingType.walk_on_tightropes),
				new Training(Training.TrainingType.swimm_with_weights),
				new Training(Training.TrainingType.dive_with_sharks),
				new Training(Training.TrainingType.jump_on_trees),
				new Training(Training.TrainingType.walk_on_water),
				new Training(Training.TrainingType.walk_10_gravity),
				new Training(Training.TrainingType.run_50_gravity),
				new Training(Training.TrainingType.move_mountains),
				new Training(Training.TrainingType.learn_to_fly),
				new Training(Training.TrainingType.fly_around_the_world),
				new Training(Training.TrainingType.carry_mountains),
				new Training(Training.TrainingType.fly_to_the_moon),
				new Training(Training.TrainingType.fly_around_the_universe),
				new Training(Training.TrainingType.smash_meteorites),
				new Training(Training.TrainingType.train_on_another_dimension)
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
			return base.PowerGain * App.State.Multiplier.CurrentMultiPhysical * 1000 / num;
		}

		public string Serialize()
		{
			return base.Serialize("Training");
		}

		internal static Training FromString(string base64String)
		{
			TrainingBase trainingBase = TrainingBase.FromString(base64String, "Training");
			return new Training((Training.TrainingType)trainingBase.EnumValue)
			{
				Level = trainingBase.Level,
				ShadowCloneCount = trainingBase.ShadowCloneCount,
				CurrentDuration = trainingBase.CurrentDuration
			};
		}
	}
}
