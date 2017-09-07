using Assets.Scripts.Helper;
using System;
using System.Text;

namespace Assets.Scripts.Data
{
	public class AchievementId
	{
		public int Id;

		public bool IsReached;

		public AchievementId()
		{
		}

		public AchievementId(int id, bool isReached)
		{
			this.Id = id;
			this.IsReached = isReached;
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.IsReached.ToString());
			Conv.AppendValue(stringBuilder, "b", this.Id);
			return Conv.ToBase64(stringBuilder.ToString(), "AchievementId");
		}

		internal static AchievementId FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("AchievementId with empty value!");
				return null;
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "AchievementId");
			return new AchievementId
			{
				IsReached = Conv.getStringFromParts(parts, "a").ToLower().Equals("true"),
				Id = Conv.getIntFromParts(parts, "b")
			};
		}
	}
}
