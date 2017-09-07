using Assets.Scripts.Helper;
using System;
using System.Globalization;
using System.Text;

namespace Assets.Scripts.Data
{
	public class CDouble
	{
		private double ACModifier = 54648.4;

		private double value;

		private static string[] NumberNames = new string[]
		{
			"million",
			"billion",
			"trillion",
			"quadrillion",
			"quintillion",
			"sextillion",
			"septillion",
			"octillion",
			"nonillion",
			"decillion",
			"undecillion",
			"duodecillion",
			"tredecillion",
			"quattuordecillion",
			"quindecillion",
			"sexdecillion",
			"septendecillion",
			"octodecillion",
			"novemdecillion",
			"vigintillion",
			"unvigintillion",
			"duovigintillion",
			"trevigintillion",
			"quattuorvigintillion",
			"quinvigintillion",
			"sexvigintillion",
			"septenvigintillion",
			"octovigintillion",
			"novemvigintillion",
			"trigintillion",
			"untrigintillion",
			"duotrigintillion",
			"trestrigintillion",
			"quattuortrigintillion",
			"quinquatrigintillion",
			"sestrigintillion",
			"septentrigintillion",
			"octotrigintillion",
			"noventrigintillion",
			"quadragintillion",
			"unquadragintillion",
			"duoquadragintillion",
			"trequadragintillion",
			"quattuorquadragintillion",
			"quinquadragintillion",
			"sexquadragintillion",
			"septquadragintillion",
			"octoquadragintillion",
			"novemquadragintillion",
			"quinquagintillion ",
			"unquinquagintillion ",
			"duoquinquagintillion",
			"trequinquagintillion ",
			"quattuorquinquagintillion ",
			"quinquinquagintillion ",
			"sexquinquagintillion ",
			"septquinquagintillion",
			"octoquinquagintillion ",
			"novemquinquagintillion ",
			"sexagintillion",
			"unsexagintillion",
			"duosexagintillion",
			"tresexagintillion",
			"quattuorsexagintillion",
			"quinsexagintillion",
			"sexsexagintillion",
			"septsexagintillion",
			"octosexagintillion",
			"novemsexagintillion",
			"septuagintillion ",
			"unseptuagintillion ",
			"duoseptuagintillion ",
			"treseptuagintillion ",
			"quattuorseptuagintillion ",
			"quinseptuagintillion ",
			"sexseptuagintillion ",
			"septseptuagintillion",
			"octoseptuagintillion ",
			"novemseptuagintillion ",
			"octogintillion",
			"unoctogintillion",
			"duooctogintillion",
			"treoctogintillion",
			"quattuoroctogintillion",
			"quinoctogintillion",
			"sexoctogintillion",
			"septoctogintillion",
			"octooctogintillion",
			"novemoctogintillion",
			"nonagintillion ",
			"unnonagintillion ",
			"duononagintillion ",
			"trenonagintillion ",
			"quattuornonagintillion ",
			"quinnonagintillion ",
			"sexnonagintillion ",
			"septnonagintillion",
			"octononagintillion ",
			"novemnonagintillion",
			"centillion"
		};

		public double Value
		{
			get
			{
				return this.value - this.ACModifier;
			}
			set
			{
				this.value = value + this.ACModifier;
			}
		}

		public double Double
		{
			get
			{
				return this.Value;
			}
		}

		public CDouble Rounded
		{
			get
			{
				return Math.Round(this.Value);
			}
		}

		public CDouble Floored
		{
			get
			{
				return Math.Floor(this.Value);
			}
		}

		public string NumberText
		{
			get
			{
				return string.Format("{0:0}", this.Value);
			}
		}

		public string CommaFormatted
		{
			get
			{
				return this.Value.ToString("N0");
			}
		}

		public string GuiText
		{
			get
			{
				return this.ToGuiText(true);
			}
		}

		public CDouble()
		{
			this.Value = 0.0;
		}

		public CDouble(double number)
		{
			this.Value = number;
		}

		public CDouble(long number)
		{
			this.Value = (double)number;
		}

		public CDouble(ulong number)
		{
			this.Value = number;
		}

		public CDouble(string digits)
		{
			this.Value = Conv.StringToDouble(digits, true, false);
		}

		public CDouble(string digits, bool noConvert)
		{
			this.Value = Conv.StringToDoubleRuntime(digits);
		}

		public static implicit operator CDouble(long value)
		{
			return new CDouble(value);
		}

		public static implicit operator CDouble(double value)
		{
			return new CDouble(value);
		}

		public static implicit operator CDouble(ulong value)
		{
			return new CDouble(value);
		}

		public static implicit operator CDouble(int value)
		{
			return new CDouble((long)value);
		}

		public static implicit operator CDouble(uint value)
		{
			return new CDouble((ulong)value);
		}

		public static CDouble operator +(CDouble leftSide, CDouble rightSide)
		{
			return new CDouble(leftSide.Value + rightSide.Value);
		}

		public static CDouble operator ++(CDouble leftSide)
		{
			return leftSide + 1;
		}

		public static CDouble operator -(CDouble leftSide, CDouble rightSide)
		{
			return new CDouble(leftSide.Value - rightSide.Value);
		}

		public static CDouble operator --(CDouble leftSide)
		{
			return leftSide - 1;
		}

		public static CDouble operator *(CDouble leftSide, CDouble rightSide)
		{
			if (object.ReferenceEquals(leftSide, null))
			{
				return 0;
			}
			if (object.ReferenceEquals(rightSide, null))
			{
				return 0;
			}
			return new CDouble(leftSide.Value * rightSide.Value);
		}

		public static CDouble operator /(CDouble leftSide, CDouble rightSide)
		{
			if (leftSide == null)
			{
				return 0;
			}
			if (rightSide == null)
			{
				throw new DivideByZeroException();
			}
			if (rightSide.Value == 0.0)
			{
				throw new DivideByZeroException();
			}
			return new CDouble(leftSide.Value / rightSide.Value);
		}

		public int CompareTo(CDouble value)
		{
			return CDouble.Compare(this, value);
		}

		public static int Compare(CDouble leftSide, CDouble rightSide)
		{
			if (object.ReferenceEquals(leftSide, rightSide))
			{
				return 0;
			}
			if (leftSide > rightSide)
			{
				return 1;
			}
			if (leftSide == rightSide)
			{
				return 0;
			}
			return -1;
		}

		public static bool operator ==(CDouble leftSide, CDouble rightSide)
		{
			return object.ReferenceEquals(leftSide, rightSide) || (!object.ReferenceEquals(leftSide, null) && !object.ReferenceEquals(rightSide, null) && leftSide.Equals(rightSide));
		}

		public static bool operator !=(CDouble leftSide, CDouble rightSide)
		{
			return !(leftSide == rightSide);
		}

		public static bool operator >(CDouble leftSide, CDouble rightSide)
		{
			return !object.ReferenceEquals(leftSide, null) && (object.ReferenceEquals(rightSide, null) || leftSide.Value > rightSide.Value);
		}

		public static bool operator <(CDouble leftSide, CDouble rightSide)
		{
			return object.ReferenceEquals(leftSide, null) || (!object.ReferenceEquals(rightSide, null) && leftSide.Value < rightSide.Value);
		}

		public static bool operator >=(CDouble leftSide, CDouble rightSide)
		{
			return CDouble.Compare(leftSide, rightSide) >= 0;
		}

		public static bool operator <=(CDouble leftSide, CDouble rightSide)
		{
			return CDouble.Compare(leftSide, rightSide) <= 0;
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(obj, null))
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj is CDouble)
			{
				string text = this.Serialize();
				string text2 = ((CDouble)obj).Serialize();
				return text.Equals(text2);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		public string Serialize()
		{
			return this.Value.ToString();
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}

		public CDouble Floor()
		{
			this.Value = Math.Floor(this.Value);
			return this.Value;
		}

		public CDouble Ceiling()
		{
			this.Value = Math.Ceiling(this.Value);
			return this.Value;
		}

		public void Round()
		{
			this.Value = Math.Round(this.Value);
		}

		public int ToInt()
		{
			if (this.Value < 2147483647.0)
			{
				return (int)Math.Round(this.Value);
			}
			return 2147483647;
		}

		public long ToLong()
		{
			if (this.Value < 9.2233720368547758E+18)
			{
				return (long)Math.Round(this.Value);
			}
			return 2147483647L;
		}

		public int ToNextInt()
		{
			if (this.Value < 2147483647.0)
			{
				return (int)Math.Ceiling(this.Value);
			}
			return 2147483647;
		}

		public string ToGuiText(bool onlyWholeNumbers = true)
		{
			bool flag = false;
			if (App.State != null && App.State.GameSettings.UseExponentNumbers)
			{
				flag = true;
			}
			double num = this.Value;
			bool flag2 = num < 0.0;
			if (flag2)
			{
				num *= -1.0;
			}
			if (num < 1000.0)
			{
				string text = string.Empty;
				if (onlyWholeNumbers)
				{
					text = string.Format("{0:0}", num);
				}
				else
				{
					text = string.Format("{0:0.##}", num);
				}
				if (flag2)
				{
					text = "-" + text;
				}
				return text;
			}
			if (double.IsInfinity(num) || double.IsNaN(num))
			{
				return "∞";
			}
			string text2 = string.Format("{0:F0}", num);
			int num2 = text2.Length % 3;
			if (num2 == 0)
			{
				num2 = 3;
			}
			string text3 = text2.Substring(0, num2);
			string text4 = text2.Substring(num2, 3);
			if (string.IsNullOrEmpty(text3))
			{
				text3 = "0";
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (flag2)
			{
				stringBuilder.Append("-");
			}
			stringBuilder.Append(text3);
			if (text2.Length < 7)
			{
				stringBuilder.Append(",").Append(text4);
				return stringBuilder.ToString();
			}
			if (flag)
			{
				if (flag2)
				{
					return "-" + num.ToString("0.000 E+0");
				}
				return num.ToString("0.000 E+0");
			}
			else
			{
				int num3 = (text2.Length - 1) / 3 - 2;
				if (CDouble.NumberNames.Length < num3 + 1)
				{
					stringBuilder.Append(text4);
					stringBuilder.Insert(1, ".");
					stringBuilder.Append(" E+" + (text2.Length - 1));
					return stringBuilder.ToString();
				}
				if (CDouble.NumberNames.Length >= num3 + 1)
				{
					stringBuilder.Append(".").Append(text4);
					stringBuilder.Append(" ").Append(CDouble.NumberNames[num3]);
					return stringBuilder.ToString();
				}
				return "∞";
			}
		}

		public static int ToInt16(CDouble value)
		{
			if (object.ReferenceEquals(value, null))
			{
				return 0;
			}
			return (int)short.Parse(value.Serialize(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static uint ToUInt16(CDouble value)
		{
			if (object.ReferenceEquals(value, null))
			{
				return 0u;
			}
			return (uint)ushort.Parse(value.Serialize(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static int ToInt32(CDouble value)
		{
			if (object.ReferenceEquals(value, null))
			{
				return 0;
			}
			return int.Parse(value.Serialize(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static uint ToUInt32(CDouble value)
		{
			if (object.ReferenceEquals(value, null))
			{
				return 0u;
			}
			return uint.Parse(value.Serialize(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static long ToInt64(CDouble value)
		{
			if (object.ReferenceEquals(value, null))
			{
				return 0L;
			}
			return long.Parse(value.Serialize(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static ulong ToUInt64(CDouble value)
		{
			if (object.ReferenceEquals(value, null))
			{
				return 0uL;
			}
			return ulong.Parse(value.Serialize(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}
	}
}
