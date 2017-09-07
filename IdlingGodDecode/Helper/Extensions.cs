using System;

namespace Assets.Scripts.Helper
{
	public static class Extensions
	{
		public static string Nr(this NS value)
		{
			int num = (int)value;
			string text = num.ToString();
			if (text.Length == 1)
			{
				text = "00" + text;
			}
			if (text.Length == 2)
			{
				text = "0" + text;
			}
			return text;
		}
	}
}
