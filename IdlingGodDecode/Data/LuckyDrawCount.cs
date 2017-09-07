using Assets.Scripts.Helper;
using System;
using System.Text;

namespace Assets.Scripts.Data
{
	public class LuckyDrawCount
	{
		public LuckyType Type;

		public int Count;

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", (int)this.Type);
			Conv.AppendValue(stringBuilder, "b", this.Count);
			return Conv.ToBase64(stringBuilder.ToString(), "LuckyDrawCount");
		}

		internal static LuckyDrawCount Deserialize(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("LuckyDrawCount.FromString with empty value!");
				return new LuckyDrawCount();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "LuckyDrawCount");
			return new LuckyDrawCount
			{
				Type = (LuckyType)Conv.getIntFromParts(parts, "a"),
				Count = Conv.getIntFromParts(parts, "b")
			};
		}
	}
}
