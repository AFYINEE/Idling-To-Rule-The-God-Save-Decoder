using Assets.Scripts.Helper;
using System;
using System.Text;

namespace Assets.Scripts.Data
{
	public class PlayerKredProblems
	{
		public bool Ryu82HasReceived;

		public bool SlayurHasReceived;

		public bool RebosteroHasReceived;

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "b", this.Ryu82HasReceived.ToString());
			Conv.AppendValue(stringBuilder, "f", this.SlayurHasReceived.ToString());
			Conv.AppendValue(stringBuilder, "g", this.RebosteroHasReceived.ToString());
			return Conv.ToBase64(stringBuilder.ToString(), "Settings");
		}

		internal static PlayerKredProblems FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				return new PlayerKredProblems();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "Settings");
			return new PlayerKredProblems
			{
				Ryu82HasReceived = Conv.getStringFromParts(parts, "b").ToLower().Equals("true"),
				SlayurHasReceived = Conv.getStringFromParts(parts, "f").ToLower().Equals("true"),
				RebosteroHasReceived = Conv.getStringFromParts(parts, "g").ToLower().Equals("true")
			};
		}

		internal int Check()
		{
			App.State.KredProblems.Ryu82HasReceived = false;
			string text = App.State.KongUserName;
			if (!string.IsNullOrEmpty(text))
			{
				text = text.ToLower();
			}
			return 0;
		}
	}
}
