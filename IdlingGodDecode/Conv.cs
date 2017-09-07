using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Helper
{
    public class Conv
    {
        public const string VALUE1 = "a";

        public const string VALUE2 = "b";

        public const string VALUE3 = "c";

        public const string VALUE4 = "d";

        public const string VALUE5 = "e";

        public const string VALUE6 = "f";

        public const string VALUE7 = "g";

        public const string VALUE8 = "h";

        public const string VALUE9 = "i";

        public const string VALUE10 = "j";

        public const string VALUE11 = "k";

        public const string VALUE12 = "l";

        public const string VALUE13 = "m";

        public const string VALUE14 = "n";

        public const string VALUE15 = "o";

        public const string VALUE16 = "p";

        public const string VALUE17 = "q";

        public const string VALUE18 = "r";

        public const string VALUE19 = "s";

        public const string VALUE20 = "t";

        public const string VALUE21 = "u";

        public const string VALUE22 = "v";

        public const string VALUE23 = "w";

        public const string VALUE24 = "x";

        public const string VALUE25 = "y";

        public const string VALUE26 = "z";

        public const string VALUE27 = "A";

        public const string VALUE28 = "B";

        public const string VALUE29 = "C";

        public const string VALUE30 = "D";

        public const string VALUE31 = "E";

        public const string VALUE32 = "F";

        public const string VALUE33 = "G";

        public const string VALUE34 = "H";

        public const string VALUE35 = "I";

        public const string VALUE36 = "J";

        public const string VALUE37 = "K";

        public const string VALUE38 = "L";

        public const string VALUE39 = "M";

        public const string VALUE40 = "N";

        public const string VALUE41 = "O";

        public const string VALUE42 = "P";

        public const string VALUE43 = "Q";

        public const string VALUE44 = "R";

        public const string VALUE45 = "S";

        public const string VALUE46 = "T";

        public const string VALUE47 = "U";

        public const string VALUE48 = "V";

        public const string VALUE49 = "W";

        public const string VALUE50 = "X";

        public const string VALUE51 = "Y";

        public const string VALUE52 = "Z";

        public const char SeperatorName = ':';

        public const char SeperatorClassMember = ';';

        public const char SeperatorList = '&';

        public static int StringToInt(string value)
        {
            int result = 0;
            int.TryParse(value, out result);
            return result;
        }

        public static long StringToLong(string value)
        {
            long result = 0L;
            long.TryParse(value, out result);
            return result;
        }

        public static string AddCommaSeparator(int value)
        {
            return string.Format("{0:n0}", value);
        }

        public static void AppendValue(StringBuilder builder, string valueName, string value)
        {
            builder.Append(valueName).Append(':').Append(value).Append(';');
        }

        public static void AppendValue(StringBuilder builder, string valueName, int value)
        {
            builder.Append(valueName).Append(':').Append(value).Append(';');
        }

        public static void AppendValue(StringBuilder builder, string valueName, long value)
        {
            builder.Append(valueName).Append(':').Append(value).Append(';');
        }

        public static void AppendList<T>(StringBuilder builder, List<T> listToSave, string nameOfList)
        {
            builder.Append(nameOfList + ':');
            foreach (object obj in listToSave)
            {
                string value = string.Empty;
                if (obj is ClothingPart)
                {
                    value = ((ClothingPart)obj).Serialize();
                }
                else if (obj is GeneratorUpgrade)
                {
                    value = ((GeneratorUpgrade)obj).Serialize();
                }
                else if (obj is Training)
                {
                    value = ((Training)obj).Serialize();
                }
                else if (obj is Skill)
                {
                    value = ((Skill)obj).Serialize();
                }
                else if (obj is Fight)
                {
                    value = ((Fight)obj).Serialize();
                }
                else if (obj is Creation)
                {
                    value = ((Creation)obj).Serialize();
                }
                else if (obj is AchievementId)
                {
                    value = ((AchievementId)obj).Serialize();
                }
                else if (obj is Monument)
                {
                    value = ((Monument)obj).Serialize();
                }
                else if (obj is MonumentUpgrade)
                {
                    value = ((MonumentUpgrade)obj).Serialize();
                }
                else if (obj is Might)
                {
                    value = ((Might)obj).Serialize();
                }
                else if (obj is UltimateBeing)
                {
                    value = ((UltimateBeing)obj).Serialize();
                }
                else if (obj is UltimateBeingV2)
                {
                    value = ((UltimateBeingV2)obj).Serialize();
                }
                else if (obj is Pet)
                {
                    value = ((Pet)obj).Serialize();
                }
                else if (obj is float)
                {
                    value = ((float)obj).ToString();
                }
                else if (obj is int)
                {
                    value = ((int)obj).ToString();
                }
                else if (obj is CDouble)
                {
                    value = ((CDouble)obj).Serialize();
                }
                else if (obj is LuckyDrawCount)
                {
                    value = ((LuckyDrawCount)obj).Serialize();
                }
                else if (obj is SteamAndroidAchievement)
                {
                    value = ((int)obj).ToString();
                }
                else if (obj is FactoryModule)
                {
                    value = ((FactoryModule)obj).Serialize();
                }
                else if (obj is Crystal)
                {
                    value = ((Crystal)obj).Serialize();
                }
                else if (obj is PetCampaign)
                {
                    value = ((PetCampaign)obj).Serialize();
                }
                else
                {
                    if (!(obj is PetType))
                    {
                        throw new NotSupportedException();
                    }
                    value = ((int)obj).ToString();
                }
                builder.Append(value).Append('&');
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(';');
        }

        public static string getStringFromParts(string[] parts, string partname)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                string text = parts[i];
                if (text.StartsWith(partname))
                {
                    string[] array = text.Split(new char[]
					{
						':'
					});
                    if (array.Length == 2)
                    {
                        return array[1];
                    }
                }
            }
            return string.Empty;
        }

        public static int getIntFromParts(string[] parts, string partname)
        {
            return Conv.StringToInt(Conv.getStringFromParts(parts, partname));
        }

        public static float StringToFloat(string value)
        {
            float result = 0f;
            float.TryParse(value, out result);
            return result;
        }

        public static double StringToDoubleRuntime(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0.0;
            }
            double result = 0.0;
            double.TryParse(value, out result);
            return result;
        }

        public static double StringToDouble(string value, bool allowNegative = true, bool addDegbug = false)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0.0;
            }
            double num = 0.0;
            double.TryParse(value, out num);
            if (value.Contains(","))
            {
                value = value.Replace(",", ".");
            }
            else
            {
                value = value.Replace(".", ",");
            }
            double num2 = 0.0;
            double.TryParse(value, out num2);
            double num3 = num2;
            if (num3 == 0.0 || (num3 > num && num != 0.0))
            {
                num3 = num;
            }
            if (num3 < 0.0 && !allowNegative)
            {
                num3 = 0.0;
            }
            return num3;
        }

        public static float getFloatFromParts(string[] parts, string partname)
        {
            return Conv.StringToFloat(Conv.getStringFromParts(parts, partname));
        }

        public static double getDoubleFromParts(string[] parts, string partname)
        {
            return Conv.StringToDouble(Conv.getStringFromParts(parts, partname), true, false);
        }

        public static double RoundToOneFourth(double value)
        {
            double num = Math.Round(value * 4.0, MidpointRounding.AwayFromZero);
            return num / 4.0;
        }

        internal static CDouble getCDoubleFromParts(string[] parts, string partname, bool round = false)
        {
            string stringFromParts = Conv.getStringFromParts(parts, partname);
            CDouble cDouble = new CDouble(stringFromParts);
            if (round)
            {
                cDouble.Round();
            }
            return cDouble;
        }

        internal static long getLongFromParts(string[] parts, string partname)
        {
            string stringFromParts = Conv.getStringFromParts(parts, partname);
            long result = 0L;
            long.TryParse(stringFromParts, out result);
            return result;
        }

        internal static ulong getUlongFromParts(string[] parts, string partname)
        {
            string stringFromParts = Conv.getStringFromParts(parts, partname);
            ulong result = 0uL;
            ulong.TryParse(stringFromParts, out result);
            return result;
        }

        public static string ToBase64(string plainText, string logName = "")
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(bytes);
        }

        public static string FromBase64(string base64EncodedData)
        {
            byte[] bytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string[] StringPartsFromBase64(string base64String, string logName = "")
        {
            string text = Conv.FromBase64(base64String);
            return text.Split(new char[]
			{
				';'
			});
        }

        public static Color StringToColor(string colorString)
        {
            if (string.IsNullOrEmpty(colorString))
            {
                return default(Color);
            }
            string[] array = colorString.Split(new char[]
			{
				','
			});
            if (array.Length != 3)
            {
                return default(Color);
            }
            float r = 0f;
            float.TryParse(array[0], out r);
            float g = 0f;
            float.TryParse(array[1], out g);
            float b = 0f;
            float.TryParse(array[2], out b);
            return new Color(r, g, b);
        }

        public static string ColorToString(Color color)
        {
            return string.Concat(new object[]
			{
				color.r,
				",",
				color.g,
				",",
				color.b
			});
        }

        public static string MsToGuiText(long time, bool isMs)
        {
            if (time < 0L)
            {
                return "0 sec";
            }
            int num = 0;
            int num2;
            if (isMs)
            {
                num = (int)(time % 1000L);
                num2 = (int)(time / 1000L);
            }
            else
            {
                num2 = (int)time;
            }
            int num3 = num2 / 60;
            int num4 = num3 / 60;
            int num5 = num4 / 24;
            int num6 = num3 % 60;
            string text = string.Empty + num6;
            if (num6 < 10)
            {
                text = "0" + num6;
            }
            int num7 = num2 % 60;
            string text2 = string.Empty + num7;
            if (num2 < 10)
            {
                string text3 = string.Empty;
                long num8 = (long)(num / 10);
                if (num8 < 10L)
                {
                    text3 = "0";
                }
                return string.Concat(new object[]
				{
					text2,
					".",
					text3,
					num / 10,
					" sec"
				});
            }
            if (num2 < 60)
            {
                return text2 + " sec";
            }
            if (num7 < 10)
            {
                text2 = "0" + num7;
            }
            if (num3 < 60)
            {
                return text + ":" + text2 + " minutes";
            }
            if (num5 > 0)
            {
                return string.Concat(new object[]
				{
					num5,
					" days, ",
					num4 % 24,
					":",
					text,
					":",
					text2,
					" hours"
				});
            }
            return string.Concat(new object[]
			{
				num4 % 24,
				":",
				text,
				":",
				text2,
				" hours"
			});
        }

        public static string MsToGuiSec(long time)
        {
            return (int)(time / 1000L) + " sec";
        }

        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int num = Source.LastIndexOf(Find);
            if (num == -1)
            {
                return Source;
            }
            return Source.Remove(num, Find.Length).Insert(num, Replace);
        }
    }
}
