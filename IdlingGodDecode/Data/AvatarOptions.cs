using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
	public class AvatarOptions
	{
		public bool GenderChosen;

		public bool IsFemale;

		public bool HasLongHair;

		public bool HasBoughtLongHair;

		public bool HasBoughtHair3;

		public int HairStyle;

		public List<ClothingPart> ClothingParts = new List<ClothingPart>();

		public List<ClothingPart> ClothingPartsOld = new List<ClothingPart>();

		public AvatarOptions()
		{
			this.ClothingParts = AvatarOptions.InitialParts();
		}

		public static List<ClothingPart> InitialParts()
		{
			return new List<ClothingPart>
			{
				new ClothingPart(ClothingPartEnum.bodywhite, 0, 0, true, true),
				new ClothingPart(ClothingPartEnum.bodybrown, 0, 0, true, false),
				new ClothingPart(ClothingPartEnum.bodyblack, 0, 0, true, false),
				new ClothingPart(ClothingPartEnum.bodyblue, 10, 0, true, false),
				new ClothingPart(ClothingPartEnum.bodygreen, 10, 0, true, false),
				new ClothingPart(ClothingPartEnum.bodypurple, 10, 0, true, false),
				new ClothingPart(ClothingPartEnum.no_shoes, 0, 0, true, false),
				new ClothingPart(ClothingPartEnum.shoes1, 0, 0, true, true),
				new ClothingPart(ClothingPartEnum.shoes2, 0, 6, false, false),
				new ClothingPart(ClothingPartEnum.shoes3, 15, 0, false, false),
				new ClothingPart(ClothingPartEnum.shoes4, 0, 18, false, false),
				new ClothingPart(ClothingPartEnum.clothes1, 0, 0, true, true),
				new ClothingPart(ClothingPartEnum.clothes2, 0, 2, false, false),
				new ClothingPart(ClothingPartEnum.clothes3, 30, 0, false, false),
				new ClothingPart(ClothingPartEnum.clothes3extra, 20, 0, false, false),
				new ClothingPart(ClothingPartEnum.clothes4, 0, 12, false, false),
				new ClothingPart(ClothingPartEnum.clothes5, 0, 22, false, false),
				new ClothingPart(ClothingPartEnum.clothes6, 35, 0, false, false),
				new ClothingPart(ClothingPartEnum.shortHair, 0, 0, true, true),
				new ClothingPart(ClothingPartEnum.longHair, 25, 0, false, false),
				new ClothingPart(ClothingPartEnum.type3hair, 33, 0, false, false),
				new ClothingPart(ClothingPartEnum.hair1, 0, 0, true, true),
				new ClothingPart(ClothingPartEnum.hair2, 0, 3, false, false),
				new ClothingPart(ClothingPartEnum.hair3, 0, 10, false, false),
				new ClothingPart(ClothingPartEnum.hair4, 0, 16, false, false),
				new ClothingPart(ClothingPartEnum.hair5, 10, 0, false, false),
				new ClothingPart(ClothingPartEnum.hair6, 10, 0, false, false),
				new ClothingPart(ClothingPartEnum.hair7, 10, 0, false, false),
				new ClothingPart(ClothingPartEnum.hair8, 10, 0, false, false),
				new ClothingPart(ClothingPartEnum.hairGlow, 0, 12, false, false),
				new ClothingPart(ClothingPartEnum.wing1, 0, 20, false, false),
				new ClothingPart(ClothingPartEnum.wing2, 0, 21, false, false),
				new ClothingPart(ClothingPartEnum.wing3, 0, 26, false, false),
				new ClothingPart(ClothingPartEnum.wing4, 0, 27, false, false),
				new ClothingPart(ClothingPartEnum.wing5, 20, 0, false, false),
				new ClothingPart(ClothingPartEnum.wing6, 20, 0, false, false),
				new ClothingPart(ClothingPartEnum.wingsBlack, 20, 0, false, false),
				new ClothingPart(ClothingPartEnum.removeDarkness, 0, 1, false, false),
				new ClothingPart(ClothingPartEnum.extra1, 0, 7, false, false),
				new ClothingPart(ClothingPartEnum.extra2, 0, 8, false, false),
				new ClothingPart(ClothingPartEnum.extra3, 10, 0, false, false),
				new ClothingPart(ClothingPartEnum.extra4, 10, 0, false, false),
				new ClothingPart(ClothingPartEnum.extra5, 10, 0, false, false),
				new ClothingPart(ClothingPartEnum.extra6, 0, 35, false, false),
				new ClothingPart(ClothingPartEnum.extra7, 0, 23, false, false),
				new ClothingPart(ClothingPartEnum.extra8, 25, 0, false, false),
				new ClothingPart(ClothingPartEnum.aura1, 0, 8, false, false),
				new ClothingPart(ClothingPartEnum.aura2, 0, 18, false, false),
				new ClothingPart(ClothingPartEnum.aura3, 30, 0, false, false),
				new ClothingPart(ClothingPartEnum.arty_reward, 999999, 175, false, false)
			};
		}

		internal static void CheckIfAllAdded(ref List<ClothingPart> allParts)
		{
			if (allParts == null || allParts.Count == 0)
			{
				allParts = AvatarOptions.InitialParts();
				return;
			}
			List<ClothingPart> list = AvatarOptions.InitialParts();
			foreach (ClothingPart current in list)
			{
				AvatarOptions.AddIfMissing(ref allParts, list, current.Id);
			}
		}

		private static void AddIfMissing(ref List<ClothingPart> allParts, List<ClothingPart> initial, ClothingPartEnum missingParts)
		{
			if (allParts.FirstOrDefault((ClothingPart x) => x.Id == missingParts) == null)
			{
				ClothingPart item = initial.FirstOrDefault((ClothingPart x) => x.Id == missingParts);
				allParts.Add(item);
			}
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.GenderChosen.ToString());
			Conv.AppendValue(stringBuilder, "b", this.IsFemale.ToString());
			Conv.AppendList<ClothingPart>(stringBuilder, this.ClothingPartsOld, "c");
			Conv.AppendValue(stringBuilder, "d", this.HasLongHair.ToString());
			Conv.AppendValue(stringBuilder, "e", this.HasBoughtLongHair.ToString());
			Conv.AppendList<ClothingPart>(stringBuilder, this.ClothingParts, "f");
			Conv.AppendValue(stringBuilder, "g", this.HasBoughtHair3.ToString());
			Conv.AppendValue(stringBuilder, "h", this.HairStyle);
			return Conv.ToBase64(stringBuilder.ToString(), "AvatarOptions");
		}

		internal static AvatarOptions FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("AvatarOptions.FromString with empty value!");
				return new AvatarOptions
				{
					ClothingParts = AvatarOptions.InitialParts()
				};
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "AvatarOptions");
			AvatarOptions avatarOptions = new AvatarOptions();
			avatarOptions.GenderChosen = Conv.getStringFromParts(parts, "a").ToLower().Equals("true");
			avatarOptions.IsFemale = Conv.getStringFromParts(parts, "b").ToLower().Equals("true");
			string stringFromParts = Conv.getStringFromParts(parts, "c");
			string[] array = stringFromParts.Split(new char[]
			{
				'&'
			});
			List<ClothingPart> list = new List<ClothingPart>();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (!string.IsNullOrEmpty(text))
				{
					list.Add(ClothingPart.FromString(text));
				}
			}
			avatarOptions.ClothingPartsOld = list;
			avatarOptions.HasLongHair = Conv.getStringFromParts(parts, "d").ToLower().Equals("true");
			avatarOptions.HasBoughtLongHair = Conv.getStringFromParts(parts, "e").ToLower().Equals("true");
			stringFromParts = Conv.getStringFromParts(parts, "f");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			List<ClothingPart> list2 = new List<ClothingPart>();
			string[] array3 = array;
			for (int j = 0; j < array3.Length; j++)
			{
				string text2 = array3[j];
				if (!string.IsNullOrEmpty(text2))
				{
					list2.Add(ClothingPart.FromString(text2));
				}
			}
			avatarOptions.ClothingParts = list2;
			AvatarOptions.CheckIfAllAdded(ref avatarOptions.ClothingParts);
			avatarOptions.HasBoughtHair3 = Conv.getStringFromParts(parts, "g").ToLower().Equals("true");
			avatarOptions.HairStyle = Conv.getIntFromParts(parts, "h");
			return avatarOptions;
		}
	}
}
