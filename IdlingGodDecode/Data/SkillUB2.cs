using System;

namespace Assets.Scripts.Data
{
	public class SkillUB2
	{
		public string Name = string.Empty;

		public string Desc = string.Empty;

		public SkillTypeUBV2 TypeEnum;

		public CDouble EnergyCost = 0;

		public CDouble HealPerc = 0;

		public CDouble Damage = 0;

		public CDouble Buff = 0;

		public CDouble BuffDuration = 0;

		public bool ReflectDamage;

		public bool DoubleDamage;

		public bool RevertLastAction;

		public CDouble IncreaseActionsDuration = 0;

		public CDouble DodgeCounterChance = 0;
	}
}
