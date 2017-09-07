using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
	public class LuckyDraw
	{
		public LuckyType LastLuckyDraw;

		public List<LuckyDrawCount> DrawCounts = new List<LuckyDrawCount>();

		public string LastDrawText
		{
			get
			{
				return "Your last draw: \n" + LuckyDraw.DrawText(this.LastLuckyDraw);
			}
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", (int)this.LastLuckyDraw);
			Conv.AppendList<LuckyDrawCount>(stringBuilder, this.DrawCounts, "b");
			return Conv.ToBase64(stringBuilder.ToString(), "LuckyDraw");
		}

		internal static LuckyDraw Deserialize(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("LuckyDraw.FromString with empty value!");
				return new LuckyDraw();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "LuckyDraw");
			LuckyDraw luckyDraw = new LuckyDraw();
			luckyDraw.LastLuckyDraw = (LuckyType)Conv.getIntFromParts(parts, "a");
			string stringFromParts = Conv.getStringFromParts(parts, "b");
			string[] array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (!string.IsNullOrEmpty(text))
				{
					luckyDraw.DrawCounts.Add(LuckyDrawCount.Deserialize(text));
				}
			}
			return luckyDraw;
		}

		public LuckyType Draw()
		{
			if (App.State == null || App.State.PremiumBoni.LuckyDraws < 1)
			{
				return LuckyType.None;
			}
			List<LuckyType> list = new List<LuckyType>();
			this.AddDraw(ref list, LuckyType.GP2, 40);
			this.AddDraw(ref list, LuckyType.GP5, 10);
			this.AddDraw(ref list, LuckyType.GP10, 5);
			this.AddDraw(ref list, LuckyType.GP25, 2);
			this.AddDraw(ref list, LuckyType.GP50, 1);
			this.AddDraw(ref list, LuckyType.GP100, 1);
			this.AddDraw(ref list, LuckyType.Liquid1, 8);
			this.AddDraw(ref list, LuckyType.Liquid2, 4);
			this.AddDraw(ref list, LuckyType.Chakra1, 8);
			this.AddDraw(ref list, LuckyType.Chakra2, 4);
			this.AddDraw(ref list, LuckyType.PetToken, 2);
			this.AddDraw(ref list, LuckyType.LuckyDraw, 10);
			this.AddDraw(ref list, LuckyType.USS, 6);
			this.AddDraw(ref list, LuckyType.CrystalSlot, 1);
			this.AddDraw(ref list, LuckyType.Dall, 4);
			this.AddDraw(ref list, LuckyType.DPhysical, 15);
			this.AddDraw(ref list, LuckyType.DMystic, 15);
			this.AddDraw(ref list, LuckyType.DCreating, 15);
			this.AddDraw(ref list, LuckyType.DBattle, 15);
			this.AddDraw(ref list, LuckyType.DDiv, 15);
			this.AddDraw(ref list, LuckyType.Div1, 50);
			this.AddDraw(ref list, LuckyType.Div5, 15);
			this.AddDraw(ref list, LuckyType.Growth1, 55);
			this.AddDraw(ref list, LuckyType.Growth2, 25);
			this.AddDraw(ref list, LuckyType.Growth5, 5);
			this.AddDraw(ref list, LuckyType.MightyFood1, 25);
			this.AddDraw(ref list, LuckyType.MightyFood2, 10);
			this.AddDraw(ref list, LuckyType.MightyFood5, 5);
			this.AddDraw(ref list, LuckyType.GP1, 130);
			LuckyType drawing = LuckyType.GP5;
			if (this.LastLuckyDraw != LuckyType.None)
			{
				drawing = list[State2.RandomInt(0, list.Count, App.State.Ext)];
			}
			if ((drawing == LuckyType.CrystalSlot && App.State.PremiumBoni.MaxCrystals == 6) || (drawing == LuckyType.CrystalSlot && !App.State.IsCrystalFactoryAvailable))
			{
				drawing = LuckyType.GP10;
			}
			Premium expr_20D = App.State.PremiumBoni;
			expr_20D.LuckyDraws = --expr_20D.LuckyDraws;
			this.LastLuckyDraw = drawing;
			LuckyDrawCount luckyDrawCount = this.DrawCounts.FirstOrDefault((LuckyDrawCount x) => x.Type == drawing);
			if (luckyDrawCount == null)
			{
				luckyDrawCount = new LuckyDrawCount();
				luckyDrawCount.Type = drawing;
				this.DrawCounts.Add(luckyDrawCount);
			}
			luckyDrawCount.Count++;
			return drawing;
		}

		private void AddDraw(ref List<LuckyType> allDraws, LuckyType type, int count)
		{
			for (int i = 0; i < count; i++)
			{
				allDraws.Add(type);
			}
		}

		public static string DrawText(LuckyType type)
		{
			string result = string.Empty;
			switch (type)
			{
			case LuckyType.None:
				result = "There is no draw recorded yet.";
				break;
			case LuckyType.USS:
				result = "You received one Ultimate Shadow Summon!";
				break;
			case LuckyType.PetToken:
				result = "You received one Pet Token!";
				break;
			case LuckyType.GP1:
				result = "You received one God Power!";
				break;
			case LuckyType.GP2:
				result = "You received 2 God Power!";
				break;
			case LuckyType.GP5:
				result = "You received 5 God Power!";
				break;
			case LuckyType.GP10:
				result = "You received 10 God Power!";
				break;
			case LuckyType.GP25:
				result = "You received one 25 God Power!";
				break;
			case LuckyType.GP50:
				result = "You received one 50 God Power!";
				break;
			case LuckyType.GP100:
				result = "You received one 100 God Power!";
				break;
			case LuckyType.Liquid1:
				result = "You received one Godly Liquid!";
				break;
			case LuckyType.Liquid2:
				result = "You received one Godly Liquid V2!";
				break;
			case LuckyType.Chakra1:
				result = "You received one Chakra Pill!";
				break;
			case LuckyType.Chakra2:
				result = "You received one Chakra Pill V2!";
				break;
			case LuckyType.LuckyDraw:
				result = "You received two Lucky Draws!";
				break;
			case LuckyType.Dall:
				result = "Your four power stats are doubled until you rebirth!";
				break;
			case LuckyType.DPhysical:
				result = "Your physical is doubled until you rebirth!";
				break;
			case LuckyType.DMystic:
				result = "Your mystic is doubled until you rebirth!";
				break;
			case LuckyType.DCreating:
				result = "Your creating is doubled until you rebirth!";
				break;
			case LuckyType.DBattle:
				result = "Your battle is doubled until you rebirth!";
				break;
			case LuckyType.DDiv:
				result = "Your divinity is doubled!";
				break;
			case LuckyType.Div1:
				result = "You received one hour of Divinity!";
				break;
			case LuckyType.Div5:
				result = "You received five hours of Divinity!";
				break;
			case LuckyType.Growth1:
				result = "The growth of all the pets you own is increased by 1!";
				break;
			case LuckyType.Growth2:
				result = "The growth of all the pets you own is increased by 2!";
				break;
			case LuckyType.Growth5:
				result = "The growth of all the pets you own is increased by 5!";
				break;
			case LuckyType.MightyFood1:
				result = "You received 1 Mighty Food until you rebirth!";
				break;
			case LuckyType.MightyFood2:
				result = "You received 2 x Mighty Food until you rebirth!";
				break;
			case LuckyType.MightyFood5:
				result = "You received 5 x Mighty Food until you rebirth!";
				break;
			case LuckyType.CrystalSlot:
				result = "Your equip slots for crystals increased by 1!";
				break;
			}
			return result;
		}
	}
}
