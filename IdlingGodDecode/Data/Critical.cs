using Assets.Scripts.Helper;
using System;
using System.Text;

namespace Assets.Scripts.Data
{
	public class Critical
	{
		public int ValueMod = 96;

		private int eyes;

		private int eyesNoMirror;

		private int mouth;

		private int wings;

		private int tail;

		private int feet;

		public int Eyes
		{
			get
			{
				return this.eyes - this.ValueMod;
			}
			set
			{
				this.eyes = value + this.ValueMod;
			}
		}

		public int EyesNoMirror
		{
			get
			{
				return this.eyesNoMirror - this.ValueMod;
			}
			set
			{
				this.eyesNoMirror = value + this.ValueMod;
			}
		}

		public int Mouth
		{
			get
			{
				return this.mouth - this.ValueMod;
			}
			set
			{
				this.mouth = value + this.ValueMod;
			}
		}

		public int Wings
		{
			get
			{
				return this.wings - this.ValueMod;
			}
			set
			{
				this.wings = value + this.ValueMod;
			}
		}

		public int Tail
		{
			get
			{
				return this.tail - this.ValueMod;
			}
			set
			{
				this.tail = value + this.ValueMod;
			}
		}

		public int Feet
		{
			get
			{
				return this.feet - this.ValueMod;
			}
			set
			{
				this.feet = value + this.ValueMod;
			}
		}

		public CDouble CriticalDamage
		{
			get
			{
				if (App.State != null && App.State.Statistic.HasStartedArtyChallenge)
				{
					return 100;
				}
				int num = this.Mouth + this.Wings + this.Tail + this.Feet + 100;
				if (num > 1000)
				{
					num = 1000;
				}
				return num;
			}
		}

		public Critical()
		{
			int num = 0;
			this.EyesNoMirror = num;
			num = num;
			this.Feet = num;
			num = num;
			this.Tail = num;
			num = num;
			this.Wings = num;
			num = num;
			this.Mouth = num;
			this.Eyes = num;
		}

		public int Score(bool mirroredEyes)
		{
			if (!mirroredEyes)
			{
				return this.Mouth + this.Wings + this.Tail + this.Feet + this.EyesNoMirror;
			}
			return this.Mouth + this.Wings + this.Tail + this.Feet + this.Eyes * 4;
		}

		public CDouble CriticalPercent(bool mirroredEyes)
		{
			int num = this.Eyes;
			if (!mirroredEyes)
			{
				num = (int)((double)this.EyesNoMirror / 2.5);
			}
			if (num > 100)
			{
				num = 100;
			}
			return num;
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.ValueMod.ToString());
			Conv.AppendValue(stringBuilder, "b", this.Eyes.ToString());
			Conv.AppendValue(stringBuilder, "c", this.Mouth.ToString());
			Conv.AppendValue(stringBuilder, "d", this.Wings.ToString());
			Conv.AppendValue(stringBuilder, "e", this.Tail.ToString());
			Conv.AppendValue(stringBuilder, "f", this.Feet.ToString());
			Conv.AppendValue(stringBuilder, "g", this.EyesNoMirror.ToString());
			return Conv.ToBase64(stringBuilder.ToString(), "Critical");
		}

		internal static Critical FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				return new Critical();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "Critical");
			return new Critical
			{
				ValueMod = Conv.getIntFromParts(parts, "a"),
				Eyes = Conv.getIntFromParts(parts, "b"),
				Mouth = Conv.getIntFromParts(parts, "c"),
				Wings = Conv.getIntFromParts(parts, "d"),
				Tail = Conv.getIntFromParts(parts, "e"),
				Feet = Conv.getIntFromParts(parts, "f"),
				EyesNoMirror = Conv.getIntFromParts(parts, "g")
			};
		}
	}
}
