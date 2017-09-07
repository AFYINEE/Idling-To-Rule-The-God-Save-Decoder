using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class Achievement
	{
		public class Pair
		{
			public int Id;

			public Achievement Achieve;

			public Pair(int id, Achievement achievement)
			{
				this.Id = id;
				this.Achieve = achievement;
			}
		}

		public int Id;

		public bool Reached;

		public bool ShowRealName;

		public string nonReachedName;

		private string description;

		public Texture2D Image;

		public Texture2D ImageReached;

		public int IntEnum;

		public CDouble CountNeeded;

		private string descriptionMultiText;

		public CDouble multiplierBoni = new CDouble();

		public CDouble multiplierBoniRebirth = new CDouble();

		private static Dictionary<int, Achievement> AllTrainingAchievements = new Dictionary<int, Achievement>();

		private static readonly Texture2D imageTraining = (Texture2D)Resources.Load("Gui/achiev_training", typeof(Texture2D));

		private static readonly Texture2D imageTrainingReached = (Texture2D)Resources.Load("Gui/achiev_training_reached", typeof(Texture2D));

		private static Dictionary<int, Achievement> AllSkillAchievements = new Dictionary<int, Achievement>();

		private static readonly Texture2D imageSkill = (Texture2D)Resources.Load("Gui/achiev_skill", typeof(Texture2D));

		private static readonly Texture2D imageSkillReached = (Texture2D)Resources.Load("Gui/achiev_skill_reached", typeof(Texture2D));

		private static Dictionary<int, Achievement> AllFightAchievements = new Dictionary<int, Achievement>();

		private static readonly Texture2D imageFight = (Texture2D)Resources.Load("Gui/achiev_fight", typeof(Texture2D));

		private static readonly Texture2D imageFightReached = (Texture2D)Resources.Load("Gui/achiev_fight_reached", typeof(Texture2D));

		private static Dictionary<int, Achievement> AllCreationAchievements = new Dictionary<int, Achievement>();

		private static readonly Texture2D imageCreation = (Texture2D)Resources.Load("Gui/achiev_creation", typeof(Texture2D));

		private static readonly Texture2D imageCreationReached = (Texture2D)Resources.Load("Gui/achiev_creation_reached", typeof(Texture2D));

		public string NonReachedName
		{
			get
			{
				return this.nonReachedName + this.descriptionMultiText;
			}
			private set
			{
				this.nonReachedName = value;
			}
		}

		public string Description
		{
			get
			{
				return this.description + this.descriptionMultiText;
			}
			private set
			{
				this.description = value;
			}
		}

		public CDouble MultiplierBoni
		{
			get
			{
				return this.multiplierBoni;
			}
			set
			{
				this.multiplierBoni = value;
				this.descriptionMultiText = this.MultiplierBoni.ToGuiText(true) + " (now), " + this.MultiplierBoniRebirth.ToGuiText(true) + " (rebirth)";
			}
		}

		public CDouble MultiplierBoniRebirth
		{
			get
			{
				return this.multiplierBoniRebirth;
			}
			set
			{
				this.multiplierBoniRebirth = value;
				this.descriptionMultiText = this.MultiplierBoni.ToGuiText(true) + " (now), " + this.MultiplierBoniRebirth.ToGuiText(true) + " (rebirth)";
			}
		}

		public Achievement(int id, int intEnum, Texture2D image, Texture2D imageReached, CDouble countNeeded, CDouble multiBoni, CDouble multiBoniRebirth, string description, string nonReachedName)
		{
			this.Id = id;
			this.IntEnum = intEnum;
			this.CountNeeded = countNeeded;
			this.MultiplierBoni = multiBoni;
			this.MultiplierBoniRebirth = multiBoniRebirth;
			this.Image = image;
			this.ImageReached = imageReached;
			this.NonReachedName = nonReachedName;
			this.Description = description;
		}

		public static void InitAchievements(int AACsFinished)
		{
			int num = 1000 - AACsFinished * 10;
			if (num < 500)
			{
				num = 500;
			}
			int num2 = num * 10;
			int value = num2 * 25;
			int num3 = num2 * 10;
			int num4 = num3 * 5;
			int num5 = num4 * 4;
			int value2 = num5 * 5;
			Achievement.AllTrainingAchievements = new Dictionary<int, Achievement>();
			Achievement.TrainingAchievement(1, Training.TrainingType.running, num, 1, 1);
			Achievement.TrainingAchievement(2, Training.TrainingType.running, num2, 2, 1);
			Achievement.TrainingAchievement(3, Training.TrainingType.running, num3, 3, 2);
			Achievement.TrainingAchievement(4, Training.TrainingType.sit_ups, num, 1, 1);
			Achievement.TrainingAchievement(5, Training.TrainingType.sit_ups, num2, 2, 1);
			Achievement.TrainingAchievement(6, Training.TrainingType.sit_ups, num3, 3, 2);
			Achievement.TrainingAchievement(7, Training.TrainingType.push_ups, num, 1, 1);
			Achievement.TrainingAchievement(8, Training.TrainingType.push_ups, num2, 2, 1);
			Achievement.TrainingAchievement(9, Training.TrainingType.push_ups, num3, 3, 2);
			Achievement.TrainingAchievement(10, Training.TrainingType.swimming, num, 1, 1);
			Achievement.TrainingAchievement(11, Training.TrainingType.swimming, num2, 2, 1);
			Achievement.TrainingAchievement(12, Training.TrainingType.swimming, num3, 3, 2);
			Achievement.TrainingAchievement(13, Training.TrainingType.long_jumps, num, 1, 1);
			Achievement.TrainingAchievement(14, Training.TrainingType.long_jumps, num2, 2, 1);
			Achievement.TrainingAchievement(15, Training.TrainingType.long_jumps, num3, 3, 2);
			Achievement.TrainingAchievement(16, Training.TrainingType.shadow_boxing, num, 1, 1);
			Achievement.TrainingAchievement(17, Training.TrainingType.shadow_boxing, num2, 2, 1);
			Achievement.TrainingAchievement(18, Training.TrainingType.shadow_boxing, num3, 3, 2);
			Achievement.TrainingAchievement(19, Training.TrainingType.jump_rope, num, 1, 1);
			Achievement.TrainingAchievement(20, Training.TrainingType.jump_rope, num2, 2, 1);
			Achievement.TrainingAchievement(21, Training.TrainingType.jump_rope, num3, 3, 2);
			Achievement.TrainingAchievement(22, Training.TrainingType.climb_mountains, num, 1, 1);
			Achievement.TrainingAchievement(23, Training.TrainingType.climb_mountains, num2, 2, 1);
			Achievement.TrainingAchievement(24, Training.TrainingType.climb_mountains, num3, 3, 2);
			Achievement.TrainingAchievement(25, Training.TrainingType.run_in_water, num, 1, 1);
			Achievement.TrainingAchievement(26, Training.TrainingType.run_in_water, num2, 2, 1);
			Achievement.TrainingAchievement(27, Training.TrainingType.run_in_water, num3, 3, 2);
			Achievement.TrainingAchievement(28, Training.TrainingType.meditate, num, 1, 1);
			Achievement.TrainingAchievement(29, Training.TrainingType.meditate, num2, 2, 1);
			Achievement.TrainingAchievement(30, Training.TrainingType.meditate, num3, 3, 2);
			Achievement.TrainingAchievement(31, Training.TrainingType.throw_spears, num, 1, 2);
			Achievement.TrainingAchievement(32, Training.TrainingType.throw_spears, num2, 2, 3);
			Achievement.TrainingAchievement(33, Training.TrainingType.throw_spears, num3, 3, 4);
			Achievement.TrainingAchievement(34, Training.TrainingType.smash_rocks, num, 1, 2);
			Achievement.TrainingAchievement(35, Training.TrainingType.smash_rocks, num2, 2, 3);
			Achievement.TrainingAchievement(36, Training.TrainingType.smash_rocks, num3, 3, 4);
			Achievement.TrainingAchievement(37, Training.TrainingType.run_with_weights, num, 1, 2);
			Achievement.TrainingAchievement(38, Training.TrainingType.run_with_weights, num2, 2, 3);
			Achievement.TrainingAchievement(39, Training.TrainingType.run_with_weights, num3, 3, 4);
			Achievement.TrainingAchievement(40, Training.TrainingType.walk_on_tightropes, num, 1, 2);
			Achievement.TrainingAchievement(41, Training.TrainingType.walk_on_tightropes, num2, 2, 3);
			Achievement.TrainingAchievement(42, Training.TrainingType.walk_on_tightropes, num3, 3, 4);
			Achievement.TrainingAchievement(43, Training.TrainingType.swimm_with_weights, num, 1, 3);
			Achievement.TrainingAchievement(44, Training.TrainingType.swimm_with_weights, num2, 2, 4);
			Achievement.TrainingAchievement(45, Training.TrainingType.swimm_with_weights, num3, 3, 6);
			Achievement.TrainingAchievement(46, Training.TrainingType.dive_with_sharks, num, 1, 3);
			Achievement.TrainingAchievement(47, Training.TrainingType.dive_with_sharks, num2, 2, 4);
			Achievement.TrainingAchievement(48, Training.TrainingType.dive_with_sharks, num3, 3, 6);
			Achievement.TrainingAchievement(49, Training.TrainingType.jump_on_trees, num, 1, 3);
			Achievement.TrainingAchievement(50, Training.TrainingType.jump_on_trees, num2, 2, 4);
			Achievement.TrainingAchievement(51, Training.TrainingType.jump_on_trees, num3, 3, 6);
			Achievement.TrainingAchievement(52, Training.TrainingType.walk_on_water, num, 1, 4);
			Achievement.TrainingAchievement(53, Training.TrainingType.walk_on_water, num2, 2, 6);
			Achievement.TrainingAchievement(54, Training.TrainingType.walk_on_water, num3, 3, 8);
			Achievement.TrainingAchievement(55, Training.TrainingType.walk_10_gravity, num, 1, 4);
			Achievement.TrainingAchievement(56, Training.TrainingType.walk_10_gravity, num2, 2, 6);
			Achievement.TrainingAchievement(57, Training.TrainingType.walk_10_gravity, num3, 3, 8);
			Achievement.TrainingAchievement(58, Training.TrainingType.run_50_gravity, num, 1, 5);
			Achievement.TrainingAchievement(59, Training.TrainingType.run_50_gravity, num2, 2, 7);
			Achievement.TrainingAchievement(60, Training.TrainingType.run_50_gravity, num3, 3, 10);
			Achievement.TrainingAchievement(61, Training.TrainingType.move_mountains, num, 1, 6);
			Achievement.TrainingAchievement(62, Training.TrainingType.move_mountains, num2, 2, 9);
			Achievement.TrainingAchievement(63, Training.TrainingType.move_mountains, num3, 3, 12);
			Achievement.TrainingAchievement(64, Training.TrainingType.learn_to_fly, num, 1, 7);
			Achievement.TrainingAchievement(65, Training.TrainingType.learn_to_fly, num2, 2, 10);
			Achievement.TrainingAchievement(66, Training.TrainingType.learn_to_fly, num3, 3, 14);
			Achievement.TrainingAchievement(67, Training.TrainingType.fly_around_the_world, num, 1, 8);
			Achievement.TrainingAchievement(68, Training.TrainingType.fly_around_the_world, num2, 2, 12);
			Achievement.TrainingAchievement(69, Training.TrainingType.fly_around_the_world, num3, 3, 16);
			Achievement.TrainingAchievement(70, Training.TrainingType.carry_mountains, num, 1, 10);
			Achievement.TrainingAchievement(71, Training.TrainingType.carry_mountains, num2, 2, 15);
			Achievement.TrainingAchievement(72, Training.TrainingType.carry_mountains, num3, 3, 20);
			Achievement.TrainingAchievement(73, Training.TrainingType.fly_to_the_moon, num, 1, 12);
			Achievement.TrainingAchievement(74, Training.TrainingType.fly_to_the_moon, num2, 2, 18);
			Achievement.TrainingAchievement(75, Training.TrainingType.fly_to_the_moon, num3, 3, 24);
			Achievement.TrainingAchievement(76, Training.TrainingType.fly_around_the_universe, num, 1, 14);
			Achievement.TrainingAchievement(77, Training.TrainingType.fly_around_the_universe, num2, 2, 21);
			Achievement.TrainingAchievement(78, Training.TrainingType.fly_around_the_universe, num3, 3, 28);
			Achievement.TrainingAchievement(79, Training.TrainingType.smash_meteorites, num, 1, 15);
			Achievement.TrainingAchievement(80, Training.TrainingType.smash_meteorites, num2, 2, 23);
			Achievement.TrainingAchievement(81, Training.TrainingType.smash_meteorites, num3, 3, 30);
			Achievement.TrainingAchievement(82, Training.TrainingType.train_on_another_dimension, num, 1, 20);
			Achievement.TrainingAchievement(83, Training.TrainingType.train_on_another_dimension, num2, 2, 30);
			Achievement.TrainingAchievement(84, Training.TrainingType.train_on_another_dimension, num3, 3, 40);
			Achievement.TrainingAchievement(85, Training.TrainingType.running, num4, 5, 4);
			Achievement.TrainingAchievement(86, Training.TrainingType.sit_ups, num4, 5, 4);
			Achievement.TrainingAchievement(87, Training.TrainingType.push_ups, num4, 5, 4);
			Achievement.TrainingAchievement(88, Training.TrainingType.swimming, num4, 5, 4);
			Achievement.TrainingAchievement(89, Training.TrainingType.long_jumps, num4, 5, 4);
			Achievement.TrainingAchievement(90, Training.TrainingType.shadow_boxing, num4, 5, 4);
			Achievement.TrainingAchievement(91, Training.TrainingType.jump_rope, num4, 5, 4);
			Achievement.TrainingAchievement(92, Training.TrainingType.climb_mountains, num4, 5, 4);
			Achievement.TrainingAchievement(93, Training.TrainingType.run_in_water, num4, 5, 4);
			Achievement.TrainingAchievement(94, Training.TrainingType.meditate, num4, 5, 4);
			Achievement.TrainingAchievement(95, Training.TrainingType.throw_spears, num4, 5, 4);
			Achievement.TrainingAchievement(96, Training.TrainingType.smash_rocks, num4, 5, 8);
			Achievement.TrainingAchievement(97, Training.TrainingType.run_with_weights, num4, 5, 8);
			Achievement.TrainingAchievement(98, Training.TrainingType.walk_on_tightropes, num4, 5, 8);
			Achievement.TrainingAchievement(99, Training.TrainingType.swimm_with_weights, num4, 5, 8);
			Achievement.TrainingAchievement(100, Training.TrainingType.dive_with_sharks, num4, 5, 12);
			Achievement.TrainingAchievement(101, Training.TrainingType.jump_on_trees, num4, 5, 12);
			Achievement.TrainingAchievement(102, Training.TrainingType.walk_on_water, num4, 5, 16);
			Achievement.TrainingAchievement(103, Training.TrainingType.walk_10_gravity, num4, 5, 16);
			Achievement.TrainingAchievement(104, Training.TrainingType.run_50_gravity, num4, 5, 20);
			Achievement.TrainingAchievement(105, Training.TrainingType.move_mountains, num4, 5, 24);
			Achievement.TrainingAchievement(106, Training.TrainingType.learn_to_fly, num4, 5, 28);
			Achievement.TrainingAchievement(107, Training.TrainingType.fly_around_the_world, num4, 5, 32);
			Achievement.TrainingAchievement(108, Training.TrainingType.carry_mountains, num4, 5, 40);
			Achievement.TrainingAchievement(109, Training.TrainingType.fly_to_the_moon, num4, 5, 48);
			Achievement.TrainingAchievement(110, Training.TrainingType.fly_around_the_universe, num4, 5, 56);
			Achievement.TrainingAchievement(111, Training.TrainingType.smash_meteorites, num4, 5, 60);
			Achievement.TrainingAchievement(112, Training.TrainingType.train_on_another_dimension, num4, 5, 80);
			Achievement.TrainingAchievement(113, Training.TrainingType.running, num5, 5, 8);
			Achievement.TrainingAchievement(114, Training.TrainingType.sit_ups, num5, 5, 8);
			Achievement.TrainingAchievement(115, Training.TrainingType.push_ups, num5, 5, 8);
			Achievement.TrainingAchievement(116, Training.TrainingType.swimming, num5, 5, 8);
			Achievement.TrainingAchievement(117, Training.TrainingType.long_jumps, num5, 5, 8);
			Achievement.TrainingAchievement(118, Training.TrainingType.shadow_boxing, num5, 5, 8);
			Achievement.TrainingAchievement(119, Training.TrainingType.jump_rope, num5, 5, 8);
			Achievement.TrainingAchievement(120, Training.TrainingType.climb_mountains, num5, 5, 8);
			Achievement.TrainingAchievement(121, Training.TrainingType.run_in_water, num5, 5, 8);
			Achievement.TrainingAchievement(122, Training.TrainingType.meditate, num5, 5, 8);
			Achievement.TrainingAchievement(123, Training.TrainingType.throw_spears, num5, 5, 8);
			Achievement.TrainingAchievement(124, Training.TrainingType.smash_rocks, num5, 5, 16);
			Achievement.TrainingAchievement(125, Training.TrainingType.run_with_weights, num5, 5, 16);
			Achievement.TrainingAchievement(126, Training.TrainingType.walk_on_tightropes, num5, 5, 16);
			Achievement.TrainingAchievement(127, Training.TrainingType.swimm_with_weights, num5, 5, 16);
			Achievement.TrainingAchievement(128, Training.TrainingType.dive_with_sharks, num5, 5, 24);
			Achievement.TrainingAchievement(129, Training.TrainingType.jump_on_trees, num5, 5, 24);
			Achievement.TrainingAchievement(130, Training.TrainingType.walk_on_water, num5, 5, 32);
			Achievement.TrainingAchievement(131, Training.TrainingType.walk_10_gravity, num5, 5, 32);
			Achievement.TrainingAchievement(132, Training.TrainingType.run_50_gravity, num5, 5, 40);
			Achievement.TrainingAchievement(133, Training.TrainingType.move_mountains, num5, 5, 48);
			Achievement.TrainingAchievement(134, Training.TrainingType.learn_to_fly, num5, 5, 56);
			Achievement.TrainingAchievement(135, Training.TrainingType.fly_around_the_world, num5, 5, 64);
			Achievement.TrainingAchievement(136, Training.TrainingType.carry_mountains, num5, 5, 80);
			Achievement.TrainingAchievement(137, Training.TrainingType.fly_to_the_moon, num5, 5, 96);
			Achievement.TrainingAchievement(138, Training.TrainingType.fly_around_the_universe, num5, 5, 112);
			Achievement.TrainingAchievement(139, Training.TrainingType.smash_meteorites, num5, 5, 120);
			Achievement.TrainingAchievement(140, Training.TrainingType.train_on_another_dimension, num5, 5, 160);
			Achievement.TrainingAchievement(141, Training.TrainingType.running, value2, 5, 8);
			Achievement.TrainingAchievement(142, Training.TrainingType.sit_ups, value2, 5, 8);
			Achievement.TrainingAchievement(143, Training.TrainingType.push_ups, value2, 5, 8);
			Achievement.TrainingAchievement(144, Training.TrainingType.swimming, value2, 5, 8);
			Achievement.TrainingAchievement(145, Training.TrainingType.long_jumps, value2, 5, 8);
			Achievement.TrainingAchievement(146, Training.TrainingType.shadow_boxing, value2, 5, 8);
			Achievement.TrainingAchievement(147, Training.TrainingType.jump_rope, value2, 5, 8);
			Achievement.TrainingAchievement(148, Training.TrainingType.climb_mountains, value2, 5, 8);
			Achievement.TrainingAchievement(149, Training.TrainingType.run_in_water, value2, 5, 8);
			Achievement.TrainingAchievement(150, Training.TrainingType.meditate, value2, 5, 8);
			Achievement.TrainingAchievement(151, Training.TrainingType.throw_spears, value2, 5, 8);
			Achievement.TrainingAchievement(152, Training.TrainingType.smash_rocks, value2, 5, 16);
			Achievement.TrainingAchievement(153, Training.TrainingType.run_with_weights, value2, 5, 16);
			Achievement.TrainingAchievement(154, Training.TrainingType.walk_on_tightropes, value2, 5, 16);
			Achievement.TrainingAchievement(155, Training.TrainingType.swimm_with_weights, value2, 5, 16);
			Achievement.TrainingAchievement(156, Training.TrainingType.dive_with_sharks, value2, 5, 24);
			Achievement.TrainingAchievement(157, Training.TrainingType.jump_on_trees, value2, 5, 24);
			Achievement.TrainingAchievement(158, Training.TrainingType.walk_on_water, value2, 5, 32);
			Achievement.TrainingAchievement(159, Training.TrainingType.walk_10_gravity, value2, 5, 32);
			Achievement.TrainingAchievement(160, Training.TrainingType.run_50_gravity, value2, 5, 40);
			Achievement.TrainingAchievement(161, Training.TrainingType.move_mountains, value2, 5, 48);
			Achievement.TrainingAchievement(162, Training.TrainingType.learn_to_fly, value2, 5, 56);
			Achievement.TrainingAchievement(163, Training.TrainingType.fly_around_the_world, value2, 5, 64);
			Achievement.TrainingAchievement(164, Training.TrainingType.carry_mountains, value2, 5, 80);
			Achievement.TrainingAchievement(165, Training.TrainingType.fly_to_the_moon, value2, 5, 96);
			Achievement.TrainingAchievement(166, Training.TrainingType.fly_around_the_universe, value2, 5, 112);
			Achievement.TrainingAchievement(167, Training.TrainingType.smash_meteorites, value2, 5, 120);
			Achievement.TrainingAchievement(168, Training.TrainingType.train_on_another_dimension, value2, 5, 160);
			Achievement.AllSkillAchievements = new Dictionary<int, Achievement>();
			Achievement.SkillAchievement(1, Skill.SkillType.double_punch, num, 1, 1);
			Achievement.SkillAchievement(2, Skill.SkillType.double_punch, num2, 2, 1);
			Achievement.SkillAchievement(3, Skill.SkillType.double_punch, num3, 3, 2);
			Achievement.SkillAchievement(4, Skill.SkillType.high_kick, num, 1, 1);
			Achievement.SkillAchievement(5, Skill.SkillType.high_kick, num2, 2, 1);
			Achievement.SkillAchievement(6, Skill.SkillType.high_kick, num3, 3, 2);
			Achievement.SkillAchievement(7, Skill.SkillType.dodge, num, 1, 1);
			Achievement.SkillAchievement(8, Skill.SkillType.dodge, num2, 2, 1);
			Achievement.SkillAchievement(9, Skill.SkillType.dodge, num3, 3, 2);
			Achievement.SkillAchievement(10, Skill.SkillType.shadow_fist, num, 1, 1);
			Achievement.SkillAchievement(11, Skill.SkillType.shadow_fist, num2, 2, 1);
			Achievement.SkillAchievement(12, Skill.SkillType.shadow_fist, num3, 3, 2);
			Achievement.SkillAchievement(13, Skill.SkillType.focused_breathing, num, 1, 1);
			Achievement.SkillAchievement(14, Skill.SkillType.focused_breathing, num2, 2, 1);
			Achievement.SkillAchievement(15, Skill.SkillType.focused_breathing, num3, 3, 2);
			Achievement.SkillAchievement(16, Skill.SkillType.raging_fist, num, 1, 1);
			Achievement.SkillAchievement(17, Skill.SkillType.raging_fist, num2, 2, 1);
			Achievement.SkillAchievement(18, Skill.SkillType.raging_fist, num3, 3, 2);
			Achievement.SkillAchievement(19, Skill.SkillType.defensive_aura, num, 1, 1);
			Achievement.SkillAchievement(20, Skill.SkillType.defensive_aura, num2, 2, 1);
			Achievement.SkillAchievement(21, Skill.SkillType.defensive_aura, num3, 3, 2);
			Achievement.SkillAchievement(22, Skill.SkillType.misdirection, num, 1, 1);
			Achievement.SkillAchievement(23, Skill.SkillType.misdirection, num2, 2, 1);
			Achievement.SkillAchievement(24, Skill.SkillType.misdirection, num3, 3, 2);
			Achievement.SkillAchievement(25, Skill.SkillType.whirling_foot, num, 1, 1);
			Achievement.SkillAchievement(26, Skill.SkillType.whirling_foot, num2, 2, 1);
			Achievement.SkillAchievement(27, Skill.SkillType.whirling_foot, num3, 3, 2);
			Achievement.SkillAchievement(28, Skill.SkillType.invisible_hand, num, 1, 1);
			Achievement.SkillAchievement(29, Skill.SkillType.invisible_hand, num2, 2, 1);
			Achievement.SkillAchievement(30, Skill.SkillType.invisible_hand, num3, 3, 2);
			Achievement.SkillAchievement(31, Skill.SkillType.dragon_fist, num, 1, 2);
			Achievement.SkillAchievement(32, Skill.SkillType.dragon_fist, num2, 2, 3);
			Achievement.SkillAchievement(33, Skill.SkillType.dragon_fist, num3, 3, 4);
			Achievement.SkillAchievement(34, Skill.SkillType.offense_aura, num, 1, 2);
			Achievement.SkillAchievement(35, Skill.SkillType.offense_aura, num2, 2, 3);
			Achievement.SkillAchievement(36, Skill.SkillType.offense_aura, num3, 3, 4);
			Achievement.SkillAchievement(37, Skill.SkillType.elemental_manipulation, num, 1, 2);
			Achievement.SkillAchievement(38, Skill.SkillType.elemental_manipulation, num2, 2, 3);
			Achievement.SkillAchievement(39, Skill.SkillType.elemental_manipulation, num3, 3, 4);
			Achievement.SkillAchievement(40, Skill.SkillType.earth_armor, num, 1, 2);
			Achievement.SkillAchievement(41, Skill.SkillType.earth_armor, num2, 2, 3);
			Achievement.SkillAchievement(42, Skill.SkillType.earth_armor, num3, 3, 4);
			Achievement.SkillAchievement(43, Skill.SkillType.ice_wall, num, 1, 3);
			Achievement.SkillAchievement(44, Skill.SkillType.ice_wall, num2, 2, 4);
			Achievement.SkillAchievement(45, Skill.SkillType.ice_wall, num3, 3, 6);
			Achievement.SkillAchievement(46, Skill.SkillType.clairvoyance, num, 1, 3);
			Achievement.SkillAchievement(47, Skill.SkillType.clairvoyance, num2, 2, 4);
			Achievement.SkillAchievement(48, Skill.SkillType.clairvoyance, num3, 3, 6);
			Achievement.SkillAchievement(49, Skill.SkillType.aura_ball, num, 1, 3);
			Achievement.SkillAchievement(50, Skill.SkillType.aura_ball, num2, 2, 4);
			Achievement.SkillAchievement(51, Skill.SkillType.aura_ball, num3, 3, 6);
			Achievement.SkillAchievement(52, Skill.SkillType.mystic_mode, num, 1, 4);
			Achievement.SkillAchievement(53, Skill.SkillType.mystic_mode, num2, 2, 6);
			Achievement.SkillAchievement(54, Skill.SkillType.mystic_mode, num3, 3, 8);
			Achievement.SkillAchievement(55, Skill.SkillType.a_108_star_fist, num, 1, 4);
			Achievement.SkillAchievement(56, Skill.SkillType.a_108_star_fist, num2, 2, 6);
			Achievement.SkillAchievement(57, Skill.SkillType.a_108_star_fist, num3, 3, 8);
			Achievement.SkillAchievement(58, Skill.SkillType.big_bang, num, 1, 5);
			Achievement.SkillAchievement(59, Skill.SkillType.big_bang, num2, 2, 7);
			Achievement.SkillAchievement(60, Skill.SkillType.big_bang, num3, 3, 10);
			Achievement.SkillAchievement(61, Skill.SkillType.god_speed, num, 1, 6);
			Achievement.SkillAchievement(62, Skill.SkillType.god_speed, num2, 2, 9);
			Achievement.SkillAchievement(63, Skill.SkillType.god_speed, num3, 3, 12);
			Achievement.SkillAchievement(64, Skill.SkillType.teleport, num, 1, 7);
			Achievement.SkillAchievement(65, Skill.SkillType.teleport, num2, 2, 10);
			Achievement.SkillAchievement(66, Skill.SkillType.teleport, num3, 3, 14);
			Achievement.SkillAchievement(67, Skill.SkillType.transformation_aura, num, 1, 8);
			Achievement.SkillAchievement(68, Skill.SkillType.transformation_aura, num2, 2, 12);
			Achievement.SkillAchievement(69, Skill.SkillType.transformation_aura, num3, 3, 16);
			Achievement.SkillAchievement(70, Skill.SkillType.gear_eyes, num, 1, 10);
			Achievement.SkillAchievement(71, Skill.SkillType.gear_eyes, num2, 2, 15);
			Achievement.SkillAchievement(72, Skill.SkillType.gear_eyes, num3, 3, 20);
			Achievement.SkillAchievement(73, Skill.SkillType.reflection_barrier, num, 1, 12);
			Achievement.SkillAchievement(74, Skill.SkillType.reflection_barrier, num2, 2, 18);
			Achievement.SkillAchievement(75, Skill.SkillType.reflection_barrier, num3, 3, 24);
			Achievement.SkillAchievement(76, Skill.SkillType.ionioi_hero_summon, num, 1, 14);
			Achievement.SkillAchievement(77, Skill.SkillType.ionioi_hero_summon, num2, 2, 21);
			Achievement.SkillAchievement(78, Skill.SkillType.ionioi_hero_summon, num3, 3, 28);
			Achievement.SkillAchievement(79, Skill.SkillType.unlimited_creation_works, num, 1, 15);
			Achievement.SkillAchievement(80, Skill.SkillType.unlimited_creation_works, num2, 2, 23);
			Achievement.SkillAchievement(81, Skill.SkillType.unlimited_creation_works, num3, 3, 30);
			Achievement.SkillAchievement(82, Skill.SkillType.time_manipulation, num, 1, 20);
			Achievement.SkillAchievement(83, Skill.SkillType.time_manipulation, num2, 2, 30);
			Achievement.SkillAchievement(84, Skill.SkillType.time_manipulation, num3, 3, 40);
			Achievement.SkillAchievement(85, Skill.SkillType.double_punch, num4, 5, 4);
			Achievement.SkillAchievement(86, Skill.SkillType.high_kick, num4, 5, 4);
			Achievement.SkillAchievement(87, Skill.SkillType.dodge, num4, 5, 4);
			Achievement.SkillAchievement(88, Skill.SkillType.shadow_fist, num4, 5, 4);
			Achievement.SkillAchievement(89, Skill.SkillType.focused_breathing, num4, 5, 4);
			Achievement.SkillAchievement(90, Skill.SkillType.raging_fist, num4, 5, 4);
			Achievement.SkillAchievement(91, Skill.SkillType.defensive_aura, num4, 5, 4);
			Achievement.SkillAchievement(92, Skill.SkillType.misdirection, num4, 5, 4);
			Achievement.SkillAchievement(93, Skill.SkillType.whirling_foot, num4, 5, 4);
			Achievement.SkillAchievement(94, Skill.SkillType.invisible_hand, num4, 5, 4);
			Achievement.SkillAchievement(95, Skill.SkillType.dragon_fist, num4, 5, 4);
			Achievement.SkillAchievement(96, Skill.SkillType.offense_aura, num4, 5, 8);
			Achievement.SkillAchievement(97, Skill.SkillType.elemental_manipulation, num4, 5, 8);
			Achievement.SkillAchievement(98, Skill.SkillType.earth_armor, num4, 5, 8);
			Achievement.SkillAchievement(99, Skill.SkillType.ice_wall, num4, 5, 8);
			Achievement.SkillAchievement(100, Skill.SkillType.clairvoyance, num4, 5, 12);
			Achievement.SkillAchievement(101, Skill.SkillType.aura_ball, num4, 5, 12);
			Achievement.SkillAchievement(102, Skill.SkillType.mystic_mode, num4, 5, 16);
			Achievement.SkillAchievement(103, Skill.SkillType.a_108_star_fist, num4, 5, 16);
			Achievement.SkillAchievement(104, Skill.SkillType.big_bang, num4, 5, 20);
			Achievement.SkillAchievement(105, Skill.SkillType.god_speed, num4, 5, 24);
			Achievement.SkillAchievement(106, Skill.SkillType.teleport, num4, 5, 28);
			Achievement.SkillAchievement(107, Skill.SkillType.transformation_aura, num4, 5, 32);
			Achievement.SkillAchievement(108, Skill.SkillType.gear_eyes, num4, 5, 40);
			Achievement.SkillAchievement(109, Skill.SkillType.reflection_barrier, num4, 5, 48);
			Achievement.SkillAchievement(110, Skill.SkillType.ionioi_hero_summon, num4, 5, 56);
			Achievement.SkillAchievement(111, Skill.SkillType.unlimited_creation_works, num4, 5, 60);
			Achievement.SkillAchievement(112, Skill.SkillType.time_manipulation, num4, 5, 80);
			Achievement.SkillAchievement(113, Skill.SkillType.double_punch, num5, 5, 8);
			Achievement.SkillAchievement(114, Skill.SkillType.high_kick, num5, 5, 8);
			Achievement.SkillAchievement(115, Skill.SkillType.dodge, num5, 5, 8);
			Achievement.SkillAchievement(116, Skill.SkillType.shadow_fist, num5, 5, 8);
			Achievement.SkillAchievement(117, Skill.SkillType.focused_breathing, num5, 5, 8);
			Achievement.SkillAchievement(118, Skill.SkillType.raging_fist, num5, 5, 8);
			Achievement.SkillAchievement(119, Skill.SkillType.defensive_aura, num5, 5, 8);
			Achievement.SkillAchievement(120, Skill.SkillType.misdirection, num5, 5, 8);
			Achievement.SkillAchievement(121, Skill.SkillType.whirling_foot, num5, 5, 8);
			Achievement.SkillAchievement(122, Skill.SkillType.invisible_hand, num5, 5, 8);
			Achievement.SkillAchievement(123, Skill.SkillType.dragon_fist, num5, 5, 8);
			Achievement.SkillAchievement(124, Skill.SkillType.offense_aura, num5, 5, 16);
			Achievement.SkillAchievement(125, Skill.SkillType.elemental_manipulation, num5, 5, 16);
			Achievement.SkillAchievement(126, Skill.SkillType.earth_armor, num5, 5, 16);
			Achievement.SkillAchievement(127, Skill.SkillType.ice_wall, num5, 5, 16);
			Achievement.SkillAchievement(128, Skill.SkillType.clairvoyance, num5, 5, 24);
			Achievement.SkillAchievement(129, Skill.SkillType.aura_ball, num5, 5, 24);
			Achievement.SkillAchievement(130, Skill.SkillType.mystic_mode, num5, 5, 32);
			Achievement.SkillAchievement(131, Skill.SkillType.a_108_star_fist, num5, 5, 32);
			Achievement.SkillAchievement(132, Skill.SkillType.big_bang, num5, 5, 40);
			Achievement.SkillAchievement(133, Skill.SkillType.god_speed, num5, 5, 48);
			Achievement.SkillAchievement(134, Skill.SkillType.teleport, num5, 5, 56);
			Achievement.SkillAchievement(135, Skill.SkillType.transformation_aura, num5, 5, 64);
			Achievement.SkillAchievement(136, Skill.SkillType.gear_eyes, num5, 5, 80);
			Achievement.SkillAchievement(137, Skill.SkillType.reflection_barrier, num5, 5, 96);
			Achievement.SkillAchievement(138, Skill.SkillType.ionioi_hero_summon, num5, 5, 112);
			Achievement.SkillAchievement(139, Skill.SkillType.unlimited_creation_works, num5, 5, 120);
			Achievement.SkillAchievement(140, Skill.SkillType.time_manipulation, num5, 5, 160);
			Achievement.SkillAchievement(141, Skill.SkillType.double_punch, value2, 5, 8);
			Achievement.SkillAchievement(142, Skill.SkillType.high_kick, value2, 5, 8);
			Achievement.SkillAchievement(143, Skill.SkillType.dodge, value2, 5, 8);
			Achievement.SkillAchievement(144, Skill.SkillType.shadow_fist, value2, 5, 8);
			Achievement.SkillAchievement(145, Skill.SkillType.focused_breathing, value2, 5, 8);
			Achievement.SkillAchievement(146, Skill.SkillType.raging_fist, value2, 5, 8);
			Achievement.SkillAchievement(147, Skill.SkillType.defensive_aura, value2, 5, 8);
			Achievement.SkillAchievement(148, Skill.SkillType.misdirection, value2, 5, 8);
			Achievement.SkillAchievement(149, Skill.SkillType.whirling_foot, value2, 5, 8);
			Achievement.SkillAchievement(150, Skill.SkillType.invisible_hand, value2, 5, 8);
			Achievement.SkillAchievement(151, Skill.SkillType.dragon_fist, value2, 5, 8);
			Achievement.SkillAchievement(152, Skill.SkillType.offense_aura, value2, 5, 16);
			Achievement.SkillAchievement(153, Skill.SkillType.elemental_manipulation, value2, 5, 16);
			Achievement.SkillAchievement(154, Skill.SkillType.earth_armor, value2, 5, 16);
			Achievement.SkillAchievement(155, Skill.SkillType.ice_wall, value2, 5, 16);
			Achievement.SkillAchievement(156, Skill.SkillType.clairvoyance, value2, 5, 24);
			Achievement.SkillAchievement(157, Skill.SkillType.aura_ball, value2, 5, 24);
			Achievement.SkillAchievement(158, Skill.SkillType.mystic_mode, value2, 5, 32);
			Achievement.SkillAchievement(159, Skill.SkillType.a_108_star_fist, value2, 5, 32);
			Achievement.SkillAchievement(160, Skill.SkillType.big_bang, value2, 5, 40);
			Achievement.SkillAchievement(161, Skill.SkillType.god_speed, value2, 5, 48);
			Achievement.SkillAchievement(162, Skill.SkillType.teleport, value2, 5, 56);
			Achievement.SkillAchievement(163, Skill.SkillType.transformation_aura, value2, 5, 64);
			Achievement.SkillAchievement(164, Skill.SkillType.gear_eyes, value2, 5, 80);
			Achievement.SkillAchievement(165, Skill.SkillType.reflection_barrier, value2, 5, 96);
			Achievement.SkillAchievement(166, Skill.SkillType.ionioi_hero_summon, value2, 5, 112);
			Achievement.SkillAchievement(167, Skill.SkillType.unlimited_creation_works, value2, 5, 120);
			Achievement.SkillAchievement(168, Skill.SkillType.time_manipulation, value2, 5, 160);
			Achievement.AllFightAchievements = new Dictionary<int, Achievement>();
			Achievement.FightAchievement(1, Fight.FightType.slimy, num, 1, 1);
			Achievement.FightAchievement(2, Fight.FightType.slimy, value, 2, 1);
			Achievement.FightAchievement(3, Fight.FightType.slimy, num5, 3, 2);
			Achievement.FightAchievement(4, Fight.FightType.frog, num, 1, 1);
			Achievement.FightAchievement(5, Fight.FightType.frog, value, 2, 1);
			Achievement.FightAchievement(6, Fight.FightType.frog, num5, 3, 2);
			Achievement.FightAchievement(7, Fight.FightType.bunny, num, 1, 1);
			Achievement.FightAchievement(8, Fight.FightType.bunny, value, 2, 1);
			Achievement.FightAchievement(9, Fight.FightType.bunny, num5, 3, 2);
			Achievement.FightAchievement(10, Fight.FightType.gobiln, num, 1, 1);
			Achievement.FightAchievement(11, Fight.FightType.gobiln, value, 2, 1);
			Achievement.FightAchievement(12, Fight.FightType.gobiln, num5, 3, 2);
			Achievement.FightAchievement(13, Fight.FightType.wolf, num, 1, 1);
			Achievement.FightAchievement(14, Fight.FightType.wolf, value, 2, 1);
			Achievement.FightAchievement(15, Fight.FightType.wolf, num5, 3, 2);
			Achievement.FightAchievement(16, Fight.FightType.kobold, num, 1, 1);
			Achievement.FightAchievement(17, Fight.FightType.kobold, value, 2, 1);
			Achievement.FightAchievement(18, Fight.FightType.kobold, num5, 3, 2);
			Achievement.FightAchievement(19, Fight.FightType.big_food, num, 1, 1);
			Achievement.FightAchievement(20, Fight.FightType.big_food, value, 2, 1);
			Achievement.FightAchievement(21, Fight.FightType.big_food, num5, 3, 2);
			Achievement.FightAchievement(22, Fight.FightType.skeleton, num, 1, 1);
			Achievement.FightAchievement(23, Fight.FightType.skeleton, value, 2, 1);
			Achievement.FightAchievement(24, Fight.FightType.skeleton, num5, 3, 2);
			Achievement.FightAchievement(25, Fight.FightType.zombie, num, 1, 1);
			Achievement.FightAchievement(26, Fight.FightType.zombie, value, 2, 1);
			Achievement.FightAchievement(27, Fight.FightType.zombie, num5, 3, 2);
			Achievement.FightAchievement(28, Fight.FightType.harpy, num, 1, 1);
			Achievement.FightAchievement(29, Fight.FightType.harpy, value, 2, 1);
			Achievement.FightAchievement(30, Fight.FightType.harpy, num5, 3, 2);
			Achievement.FightAchievement(31, Fight.FightType.orc, num, 1, 2);
			Achievement.FightAchievement(32, Fight.FightType.orc, value, 2, 3);
			Achievement.FightAchievement(33, Fight.FightType.orc, num5, 3, 4);
			Achievement.FightAchievement(34, Fight.FightType.mummy, num, 1, 2);
			Achievement.FightAchievement(35, Fight.FightType.mummy, value, 2, 3);
			Achievement.FightAchievement(36, Fight.FightType.mummy, num5, 3, 4);
			Achievement.FightAchievement(37, Fight.FightType.fighting_turtle, num, 1, 2);
			Achievement.FightAchievement(38, Fight.FightType.fighting_turtle, value, 2, 3);
			Achievement.FightAchievement(39, Fight.FightType.fighting_turtle, num5, 3, 4);
			Achievement.FightAchievement(40, Fight.FightType.ape, num, 1, 2);
			Achievement.FightAchievement(41, Fight.FightType.ape, value, 2, 3);
			Achievement.FightAchievement(42, Fight.FightType.ape, num5, 3, 4);
			Achievement.FightAchievement(43, Fight.FightType.salamander, num, 1, 3);
			Achievement.FightAchievement(44, Fight.FightType.salamander, value, 2, 4);
			Achievement.FightAchievement(45, Fight.FightType.salamander, num5, 3, 6);
			Achievement.FightAchievement(46, Fight.FightType.golem, num, 1, 3);
			Achievement.FightAchievement(47, Fight.FightType.golem, value, 2, 4);
			Achievement.FightAchievement(48, Fight.FightType.golem, num5, 3, 6);
			Achievement.FightAchievement(49, Fight.FightType.dullahan, num, 1, 3);
			Achievement.FightAchievement(50, Fight.FightType.dullahan, value, 2, 4);
			Achievement.FightAchievement(51, Fight.FightType.dullahan, num5, 3, 6);
			Achievement.FightAchievement(52, Fight.FightType.succubus, num, 1, 4);
			Achievement.FightAchievement(53, Fight.FightType.succubus, value, 2, 6);
			Achievement.FightAchievement(54, Fight.FightType.succubus, num5, 3, 8);
			Achievement.FightAchievement(55, Fight.FightType.minotaurus, num, 1, 4);
			Achievement.FightAchievement(56, Fight.FightType.minotaurus, value, 2, 6);
			Achievement.FightAchievement(57, Fight.FightType.minotaurus, num5, 3, 8);
			Achievement.FightAchievement(58, Fight.FightType.devil, num, 1, 5);
			Achievement.FightAchievement(59, Fight.FightType.devil, value, 2, 7);
			Achievement.FightAchievement(60, Fight.FightType.devil, num5, 3, 10);
			Achievement.FightAchievement(61, Fight.FightType.gargoyle, num, 1, 6);
			Achievement.FightAchievement(62, Fight.FightType.gargoyle, value, 2, 9);
			Achievement.FightAchievement(63, Fight.FightType.gargoyle, num5, 3, 12);
			Achievement.FightAchievement(64, Fight.FightType.demon, num, 1, 7);
			Achievement.FightAchievement(65, Fight.FightType.demon, value, 2, 10);
			Achievement.FightAchievement(66, Fight.FightType.demon, num5, 3, 14);
			Achievement.FightAchievement(67, Fight.FightType.vampire, num, 1, 8);
			Achievement.FightAchievement(68, Fight.FightType.vampire, value, 2, 12);
			Achievement.FightAchievement(69, Fight.FightType.vampire, num5, 3, 16);
			Achievement.FightAchievement(70, Fight.FightType.lamia, num, 1, 10);
			Achievement.FightAchievement(71, Fight.FightType.lamia, value, 2, 15);
			Achievement.FightAchievement(72, Fight.FightType.lamia, num5, 3, 20);
			Achievement.FightAchievement(73, Fight.FightType.dragon, num, 1, 12);
			Achievement.FightAchievement(74, Fight.FightType.dragon, value, 2, 18);
			Achievement.FightAchievement(75, Fight.FightType.dragon, num5, 3, 24);
			Achievement.FightAchievement(76, Fight.FightType.behemoth, num, 1, 14);
			Achievement.FightAchievement(77, Fight.FightType.behemoth, value, 2, 21);
			Achievement.FightAchievement(78, Fight.FightType.behemoth, num5, 3, 28);
			Achievement.FightAchievement(79, Fight.FightType.valkyrie, num, 1, 15);
			Achievement.FightAchievement(80, Fight.FightType.valkyrie, value, 2, 23);
			Achievement.FightAchievement(81, Fight.FightType.valkyrie, num5, 3, 30);
			Achievement.FightAchievement(82, Fight.FightType.nine_tailed_fox, num, 1, 20);
			Achievement.FightAchievement(83, Fight.FightType.nine_tailed_fox, value, 2, 30);
			Achievement.FightAchievement(84, Fight.FightType.nine_tailed_fox, num5, 3, 40);
			Achievement.FightAchievement(85, Fight.FightType.genbu, num, 1, 20);
			Achievement.FightAchievement(86, Fight.FightType.genbu, value, 2, 30);
			Achievement.FightAchievement(87, Fight.FightType.genbu, num5, 3, 40);
			Achievement.FightAchievement(88, Fight.FightType.byakko, num, 1, 20);
			Achievement.FightAchievement(89, Fight.FightType.byakko, value, 2, 30);
			Achievement.FightAchievement(90, Fight.FightType.byakko, num5, 3, 40);
			Achievement.FightAchievement(91, Fight.FightType.suzaku, num, 1, 20);
			Achievement.FightAchievement(92, Fight.FightType.suzaku, value, 2, 30);
			Achievement.FightAchievement(93, Fight.FightType.suzaku, num5, 3, 40);
			Achievement.FightAchievement(94, Fight.FightType.seiryuu, num, 1, 20);
			Achievement.FightAchievement(95, Fight.FightType.seiryuu, value, 2, 30);
			Achievement.FightAchievement(96, Fight.FightType.seiryuu, num5, 3, 40);
			Achievement.FightAchievement(97, Fight.FightType.godzilla, num, 1, 20);
			Achievement.FightAchievement(98, Fight.FightType.godzilla, value, 2, 30);
			Achievement.FightAchievement(99, Fight.FightType.godzilla, num5, 3, 40);
			Achievement.FightAchievement(100, Fight.FightType.monster_queen, num, 1, 20);
			Achievement.FightAchievement(101, Fight.FightType.monster_queen, value, 2, 30);
			Achievement.FightAchievement(102, Fight.FightType.monster_queen, num5, 3, 40);
			Achievement.FightAchievement(103, Fight.FightType.slimy, value2, 5, 8);
			Achievement.FightAchievement(104, Fight.FightType.frog, value2, 5, 8);
			Achievement.FightAchievement(105, Fight.FightType.bunny, value2, 5, 8);
			Achievement.FightAchievement(106, Fight.FightType.gobiln, value2, 5, 8);
			Achievement.FightAchievement(107, Fight.FightType.wolf, value2, 5, 8);
			Achievement.FightAchievement(108, Fight.FightType.kobold, value2, 5, 8);
			Achievement.FightAchievement(109, Fight.FightType.big_food, value2, 5, 8);
			Achievement.FightAchievement(110, Fight.FightType.skeleton, value2, 5, 8);
			Achievement.FightAchievement(111, Fight.FightType.zombie, value2, 5, 8);
			Achievement.FightAchievement(112, Fight.FightType.harpy, value2, 5, 8);
			Achievement.FightAchievement(113, Fight.FightType.orc, value2, 5, 8);
			Achievement.FightAchievement(114, Fight.FightType.mummy, value2, 5, 16);
			Achievement.FightAchievement(115, Fight.FightType.fighting_turtle, value2, 5, 16);
			Achievement.FightAchievement(116, Fight.FightType.ape, value2, 5, 16);
			Achievement.FightAchievement(117, Fight.FightType.salamander, value2, 5, 16);
			Achievement.FightAchievement(118, Fight.FightType.golem, value2, 5, 24);
			Achievement.FightAchievement(119, Fight.FightType.dullahan, value2, 5, 24);
			Achievement.FightAchievement(120, Fight.FightType.succubus, value2, 5, 32);
			Achievement.FightAchievement(121, Fight.FightType.minotaurus, value2, 5, 32);
			Achievement.FightAchievement(122, Fight.FightType.devil, value2, 5, 40);
			Achievement.FightAchievement(123, Fight.FightType.gargoyle, value2, 5, 48);
			Achievement.FightAchievement(124, Fight.FightType.demon, value2, 5, 56);
			Achievement.FightAchievement(125, Fight.FightType.vampire, value2, 5, 64);
			Achievement.FightAchievement(126, Fight.FightType.lamia, value2, 5, 80);
			Achievement.FightAchievement(127, Fight.FightType.dragon, value2, 5, 96);
			Achievement.FightAchievement(128, Fight.FightType.behemoth, value2, 5, 112);
			Achievement.FightAchievement(129, Fight.FightType.valkyrie, value2, 5, 120);
			Achievement.FightAchievement(130, Fight.FightType.nine_tailed_fox, value2, 5, 130);
			Achievement.FightAchievement(131, Fight.FightType.genbu, value2, 5, 140);
			Achievement.FightAchievement(132, Fight.FightType.byakko, value2, 5, 150);
			Achievement.FightAchievement(133, Fight.FightType.suzaku, value2, 5, 160);
			Achievement.FightAchievement(134, Fight.FightType.seiryuu, value2, 5, 170);
			Achievement.FightAchievement(135, Fight.FightType.godzilla, value2, 5, 180);
			Achievement.FightAchievement(136, Fight.FightType.monster_queen, value2, 5, 200);
			Achievement.AllCreationAchievements = new Dictionary<int, Achievement>();
			Achievement.CreationAchievement(1, Creation.CreationType.Light, 500, 1, 1);
			Achievement.CreationAchievement(2, Creation.CreationType.Light, 3000, 2, 1);
			Achievement.CreationAchievement(3, Creation.CreationType.Light, 12000, 3, 2);
			Achievement.CreationAchievement(4, Creation.CreationType.Stone, 500, 1, 1);
			Achievement.CreationAchievement(5, Creation.CreationType.Stone, 3000, 2, 1);
			Achievement.CreationAchievement(6, Creation.CreationType.Stone, 12000, 3, 2);
			Achievement.CreationAchievement(7, Creation.CreationType.Soil, 250, 1, 1);
			Achievement.CreationAchievement(8, Creation.CreationType.Soil, 1500, 2, 1);
			Achievement.CreationAchievement(9, Creation.CreationType.Soil, 6000, 3, 2);
			Achievement.CreationAchievement(10, Creation.CreationType.Air, 150, 1, 1);
			Achievement.CreationAchievement(11, Creation.CreationType.Air, 1000, 2, 1);
			Achievement.CreationAchievement(12, Creation.CreationType.Air, 4000, 3, 2);
			Achievement.CreationAchievement(13, Creation.CreationType.Water, 100, 1, 1);
			Achievement.CreationAchievement(14, Creation.CreationType.Water, 750, 2, 1);
			Achievement.CreationAchievement(15, Creation.CreationType.Water, 3000, 3, 2);
			Achievement.CreationAchievement(16, Creation.CreationType.Plant, 75, 1, 1);
			Achievement.CreationAchievement(17, Creation.CreationType.Plant, 500, 2, 1);
			Achievement.CreationAchievement(18, Creation.CreationType.Plant, 2000, 3, 2);
			Achievement.CreationAchievement(19, Creation.CreationType.Tree, 60, 1, 1);
			Achievement.CreationAchievement(20, Creation.CreationType.Tree, 400, 2, 1);
			Achievement.CreationAchievement(21, Creation.CreationType.Tree, 1600, 3, 2);
			Achievement.CreationAchievement(22, Creation.CreationType.Fish, 55, 1, 1);
			Achievement.CreationAchievement(23, Creation.CreationType.Fish, 300, 2, 1);
			Achievement.CreationAchievement(24, Creation.CreationType.Fish, 1200, 3, 2);
			Achievement.CreationAchievement(25, Creation.CreationType.Animal, 50, 1, 1);
			Achievement.CreationAchievement(26, Creation.CreationType.Animal, 250, 2, 1);
			Achievement.CreationAchievement(27, Creation.CreationType.Animal, 1000, 3, 2);
			Achievement.CreationAchievement(28, Creation.CreationType.Human, 25, 1, 1);
			Achievement.CreationAchievement(29, Creation.CreationType.Human, 150, 2, 1);
			Achievement.CreationAchievement(30, Creation.CreationType.Human, 600, 3, 2);
			Achievement.CreationAchievement(31, Creation.CreationType.River, 10, 1, 2);
			Achievement.CreationAchievement(32, Creation.CreationType.River, 60, 2, 3);
			Achievement.CreationAchievement(33, Creation.CreationType.River, 240, 3, 4);
			Achievement.CreationAchievement(34, Creation.CreationType.Mountain, 8, 1, 2);
			Achievement.CreationAchievement(35, Creation.CreationType.Mountain, 45, 2, 3);
			Achievement.CreationAchievement(36, Creation.CreationType.Mountain, 180, 3, 4);
			Achievement.CreationAchievement(37, Creation.CreationType.Forest, 5, 1, 2);
			Achievement.CreationAchievement(38, Creation.CreationType.Forest, 30, 2, 3);
			Achievement.CreationAchievement(39, Creation.CreationType.Forest, 120, 3, 4);
			Achievement.CreationAchievement(40, Creation.CreationType.Village, 4, 1, 2);
			Achievement.CreationAchievement(41, Creation.CreationType.Village, 24, 2, 3);
			Achievement.CreationAchievement(42, Creation.CreationType.Village, 90, 3, 4);
			Achievement.CreationAchievement(43, Creation.CreationType.Town, 3, 1, 3);
			Achievement.CreationAchievement(44, Creation.CreationType.Town, 15, 2, 4);
			Achievement.CreationAchievement(45, Creation.CreationType.Town, 60, 3, 6);
			Achievement.CreationAchievement(46, Creation.CreationType.Ocean, 2, 1, 3);
			Achievement.CreationAchievement(47, Creation.CreationType.Ocean, 10, 2, 4);
			Achievement.CreationAchievement(48, Creation.CreationType.Ocean, 40, 3, 6);
			Achievement.CreationAchievement(49, Creation.CreationType.Nation, 2, 1, 3);
			Achievement.CreationAchievement(50, Creation.CreationType.Nation, 6, 2, 4);
			Achievement.CreationAchievement(51, Creation.CreationType.Nation, 24, 3, 6);
			Achievement.CreationAchievement(52, Creation.CreationType.Continent, 1, 1, 4);
			Achievement.CreationAchievement(53, Creation.CreationType.Continent, 6, 2, 6);
			Achievement.CreationAchievement(54, Creation.CreationType.Continent, 20, 3, 8);
			Achievement.CreationAchievement(55, Creation.CreationType.Weather, 1, 1, 4);
			Achievement.CreationAchievement(56, Creation.CreationType.Weather, 6, 2, 6);
			Achievement.CreationAchievement(57, Creation.CreationType.Weather, 18, 3, 8);
			Achievement.CreationAchievement(58, Creation.CreationType.Sky, 1, 1, 5);
			Achievement.CreationAchievement(59, Creation.CreationType.Sky, 5, 2, 7);
			Achievement.CreationAchievement(60, Creation.CreationType.Sky, 15, 3, 10);
			Achievement.CreationAchievement(61, Creation.CreationType.Night, 1, 1, 6);
			Achievement.CreationAchievement(62, Creation.CreationType.Night, 4, 2, 9);
			Achievement.CreationAchievement(63, Creation.CreationType.Night, 12, 3, 12);
			Achievement.CreationAchievement(64, Creation.CreationType.Moon, 1, 1, 7);
			Achievement.CreationAchievement(65, Creation.CreationType.Moon, 3, 2, 10);
			Achievement.CreationAchievement(66, Creation.CreationType.Moon, 10, 3, 14);
			Achievement.CreationAchievement(67, Creation.CreationType.Planet, 1, 1, 8);
			Achievement.CreationAchievement(68, Creation.CreationType.Planet, 3, 2, 12);
			Achievement.CreationAchievement(69, Creation.CreationType.Planet, 8, 3, 16);
			Achievement.CreationAchievement(70, Creation.CreationType.Earthlike_planet, 1, 1, 10);
			Achievement.CreationAchievement(71, Creation.CreationType.Earthlike_planet, 3, 2, 15);
			Achievement.CreationAchievement(72, Creation.CreationType.Earthlike_planet, 6, 3, 20);
			Achievement.CreationAchievement(73, Creation.CreationType.Sun, 1, 1, 12);
			Achievement.CreationAchievement(74, Creation.CreationType.Sun, 3, 2, 18);
			Achievement.CreationAchievement(75, Creation.CreationType.Sun, 5, 3, 24);
			Achievement.CreationAchievement(76, Creation.CreationType.Solar_system, 1, 1, 14);
			Achievement.CreationAchievement(77, Creation.CreationType.Solar_system, 3, 2, 21);
			Achievement.CreationAchievement(78, Creation.CreationType.Solar_system, 5, 3, 28);
			Achievement.CreationAchievement(79, Creation.CreationType.Galaxy, 1, 1, 15);
			Achievement.CreationAchievement(80, Creation.CreationType.Galaxy, 2, 2, 23);
			Achievement.CreationAchievement(81, Creation.CreationType.Galaxy, 4, 3, 30);
			Achievement.CreationAchievement(82, Creation.CreationType.Universe, 1, 1, 20);
			Achievement.CreationAchievement(83, Creation.CreationType.Universe, 2, 2, 30);
			Achievement.CreationAchievement(84, Creation.CreationType.Universe, 3, 3, 40);
		}

		private static void TrainingAchievement(int id, Training.TrainingType enumType, CDouble countNeeded, int multiBoni, int multiBoniRebirth)
		{
			string str = " until Level " + countNeeded.ToGuiText(true) + ".\nPhysical Multi: ";
			string text = EnumName.Name(enumType) + str;
			string text2 = "???" + str;
			int num = (int)((int)enumType / (int)Training.TrainingType.throw_spears + 1);
			Achievement.AllTrainingAchievements.Add(id, new Achievement(id, (int)enumType, Achievement.imageTraining, Achievement.imageTrainingReached, countNeeded, multiBoni * num, multiBoniRebirth * num, text, text2));
		}

		public static List<Achievement> InitialTrainingAchievements()
		{
			return (from x in Achievement.AllTrainingAchievements
			select x.Value).ToList<Achievement>();
		}

		public static List<Achievement> TrainingAchievementsFromIdList(List<AchievementId> idList)
		{
			return Achievement.AchievementsFromIdList(idList, Achievement.AllTrainingAchievements, Achievement.InitialTrainingAchievements());
		}

		private static void SkillAchievement(int id, Skill.SkillType enumType, CDouble countNeeded, int multiBoni, int multiBoniRebirth)
		{
			string str = " until Level " + countNeeded.ToGuiText(true) + ".\nMystic Multi: ";
			string text = EnumName.Name(enumType) + str;
			string text2 = "???" + str;
            int num = (int)((int)enumType / (int)Skill.SkillType.dragon_fist + 1);
			Achievement.AllSkillAchievements.Add(id, new Achievement(id, (int)enumType, Achievement.imageSkill, Achievement.imageSkillReached, countNeeded, multiBoni * num, multiBoniRebirth * num, text, text2));
		}

		public static List<Achievement> InitialSkillAchievements()
		{
			return (from x in Achievement.AllSkillAchievements
			select x.Value).ToList<Achievement>();
		}

		public static List<Achievement> SkillAchievementsFromIdList(List<AchievementId> idList)
		{
			return Achievement.AchievementsFromIdList(idList, Achievement.AllSkillAchievements, Achievement.InitialSkillAchievements());
		}

		private static void FightAchievement(int id, Fight.FightType enumType, CDouble countNeeded, int multiBoni, int multiBoniRebirth)
		{
			string str = "Defeat " + countNeeded.ToGuiText(true) + " x ";
			string text = str + EnumName.Name(enumType) + ".\nBattle Multi: ";
			string text2 = str + "???.\nBattle Multi: ";
			Achievement.AllFightAchievements.Add(id, new Achievement(id, (int)enumType, Achievement.imageFight, Achievement.imageFightReached, countNeeded, multiBoni, multiBoniRebirth, text, text2));
		}

		public static List<Achievement> InitialFightAchievements()
		{
			return (from x in Achievement.AllFightAchievements
			select x.Value).ToList<Achievement>();
		}

		public static List<Achievement> FightAchievementsFromIdList(List<AchievementId> idList)
		{
			return Achievement.AchievementsFromIdList(idList, Achievement.AllFightAchievements, Achievement.InitialFightAchievements());
		}

		private static void CreationAchievement(int id, Creation.CreationType enumType, CDouble countNeeded, int multiBoni, int multiBoniRebirth)
		{
			string text = string.Concat(new string[]
			{
				"Create ",
				countNeeded.ToGuiText(true),
				" x ",
				EnumName.Name(enumType),
				".\nCreation Multi: "
			});
            int num = (int)((int)enumType * (int)Creation.CreationType.Soil);
			Achievement.AllCreationAchievements.Add(id, new Achievement(id, (int)enumType, Achievement.imageCreation, Achievement.imageCreationReached, countNeeded, multiBoni * num, multiBoniRebirth * num, text, text));
		}

		public static List<Achievement> InitialCreationAchievements()
		{
			return (from x in Achievement.AllCreationAchievements
			select x.Value).ToList<Achievement>();
		}

		public static List<Achievement> CreationAchievementsFromIdList(List<AchievementId> idList)
		{
			return Achievement.AchievementsFromIdList(idList, Achievement.AllCreationAchievements, Achievement.InitialCreationAchievements());
		}

		private static List<Achievement> AchievementsFromIdList(List<AchievementId> idList, Dictionary<int, Achievement> allAchievements, List<Achievement> initialAchievements)
		{
			List<Achievement> list = new List<Achievement>();
			foreach (AchievementId current in idList)
			{
				Achievement achievement2 = null;
				allAchievements.TryGetValue(current.Id, out achievement2);
				if (achievement2 != null)
				{
					achievement2.Reached = current.IsReached;
					list.Add(achievement2);
				}
				else
				{
					Log.Error("Achievement is null! " + current.Id);
				}
			}
			List<Achievement> list2 = new List<Achievement>();
			using (List<Achievement>.Enumerator enumerator2 = initialAchievements.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Achievement achievement = enumerator2.Current;
					if (list.FirstOrDefault((Achievement x) => x.Id == achievement.Id) == null)
					{
						list2.Add(achievement);
					}
				}
			}
			list.AddRange(list2);
			return list;
		}

		public static List<AchievementId> AchievementsToIdList(List<Achievement> achievements)
		{
			List<AchievementId> list = new List<AchievementId>();
			foreach (Achievement current in achievements)
			{
				list.Add(new AchievementId(current.Id, current.Reached));
			}
			return list;
		}
	}
}
