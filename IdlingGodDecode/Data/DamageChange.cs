using System;

namespace Assets.Scripts.Data
{
	public class DamageChange
	{
		internal long Duration;

		internal int ValuePercent;

		public DamageChange(int valuePercent, long duration)
		{
			this.ValuePercent = valuePercent;
			this.Duration = duration;
		}
	}
}
