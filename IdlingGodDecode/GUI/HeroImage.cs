using Assets.Scripts.Data;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class HeroImage : GuiBase
	{
		public enum CurrentSelections
		{
			Hair,
			Skin,
			Clothes,
			Shoes,
			Wings,
			Aura,
			Other,
			Pets
		}

		private static bool IsInitialized = false;

		private static Texture2D EmptyTexture;

		private static Texture2D maleBody;

		private static Texture2D maleClothesBlue;

		private static Texture2D maleShoesRed;

		private static Texture2D maleHeadBrown;

		private static Texture2D femaleBody;

		private static Texture2D femaleClothesWhite;

		private static Texture2D femaleShoesWhite;

		private static Texture2D femaleHeadBlack;

		private static Dictionary<ClothingPartEnum, ClothingPart> AllClothings = new Dictionary<ClothingPartEnum, ClothingPart>();

		private static List<ClothingPart> headParts = null;

		private static List<ClothingPart> skinParts = null;

		private static List<ClothingPart> clothingParts = null;

		private static List<ClothingPart> shoesParts = null;

		private static List<ClothingPart> hairLenght = null;

		private static List<Pet> unlockedPets = null;

		public static Pet ShownPet = null;

		public static bool ShouldInitRessources = false;

		private static bool allAvatarPartsFilled = false;

		private static int copyIndex = 0;

		private static List<Texture2D> AllAvatarParts = null;

		private static int ToolbarSelection = 0;

		private static string[] Selections = new string[]
		{
			"Hair",
			"Skin",
			"Clothes",
			"Shoes",
			"Wings",
			"Aura",
			"Other",
			"Pets"
		};

		private static int PetSelection = 0;

		private static string[] PetSelections = null;

		private static Vector2 petScrollPos = default(Vector2);

		private static string partName = string.Empty;

		public static ClothingPart DialogPart = null;

		private static string DialogText = string.Empty;

		public static void Init(bool force = false)
		{
			if (App.State == null || (HeroImage.IsInitialized && !force))
			{
				return;
			}
			if (App.State.Avatar.ClothingParts.Count == 0)
			{
				return;
			}
			if (HeroImage.EmptyTexture == null)
			{
				HeroImage.EmptyTexture = (Texture2D)Resources.Load("Gui/pets/invisible", typeof(Texture2D));
			}
			HeroImage.unlockedPets = null;
			HeroImage.PetSelections = null;
			if (!App.State.Avatar.GenderChosen)
			{
				HeroImage.maleBody = (Texture2D)Resources.Load("Gui/heroes/male_body_white", typeof(Texture2D));
				HeroImage.maleClothesBlue = (Texture2D)Resources.Load("Gui/heroes/male_clothes_blue", typeof(Texture2D));
				HeroImage.maleShoesRed = (Texture2D)Resources.Load("Gui/heroes/male_shoes_red", typeof(Texture2D));
				HeroImage.maleHeadBrown = (Texture2D)Resources.Load("Gui/heroes/male_head_brown", typeof(Texture2D));
				HeroImage.femaleBody = (Texture2D)Resources.Load("Gui/heroes/female_body_white", typeof(Texture2D));
				HeroImage.femaleClothesWhite = (Texture2D)Resources.Load("Gui/heroes/female_clothes_white", typeof(Texture2D));
				HeroImage.femaleShoesWhite = (Texture2D)Resources.Load("Gui/heroes/female_shoes_white", typeof(Texture2D));
				HeroImage.femaleHeadBlack = (Texture2D)Resources.Load("Gui/heroes/female_head_black", typeof(Texture2D));
			}
			HeroImage.ShouldInitRessources = true;
			foreach (ClothingPart current in App.State.Avatar.ClothingParts)
			{
				if (current.PermanentGPCost == 0 && current.GodDefeatedTierNeeded <= App.State.Statistic.HighestGodDefeated)
				{
					current.IsPermanentUnlocked = true;
				}
			}
			HeroImage.AllClothings = new Dictionary<ClothingPartEnum, ClothingPart>();
			HeroImage.headParts = new List<ClothingPart>();
			HeroImage.skinParts = new List<ClothingPart>();
			HeroImage.clothingParts = new List<ClothingPart>();
			HeroImage.shoesParts = new List<ClothingPart>();
			HeroImage.hairLenght = new List<ClothingPart>();
			foreach (ClothingPart current2 in App.State.Avatar.ClothingParts)
			{
				try
				{
					HeroImage.AllClothings.Add(current2.Id, current2);
				}
				catch (ArgumentException)
				{
					Log.Info(string.Concat(new object[]
					{
						"Key already in AllClothings: ",
						current2.Name,
						", ",
						current2.Id
					}));
				}
				if (current2.Id == ClothingPartEnum.hair1 || current2.Id == ClothingPartEnum.hair2 || current2.Id == ClothingPartEnum.hair3 || current2.Id == ClothingPartEnum.hair4 || current2.Id == ClothingPartEnum.hair5 || current2.Id == ClothingPartEnum.hair6 || current2.Id == ClothingPartEnum.hair7 || current2.Id == ClothingPartEnum.hair8)
				{
					HeroImage.headParts.Add(current2);
				}
				else if (current2.Id == ClothingPartEnum.clothes1 || current2.Id == ClothingPartEnum.clothes2 || current2.Id == ClothingPartEnum.clothes3 || current2.Id == ClothingPartEnum.clothes4 || current2.Id == ClothingPartEnum.clothes5 || current2.Id == ClothingPartEnum.clothes6)
				{
					HeroImage.clothingParts.Add(current2);
				}
				else if (current2.Id == ClothingPartEnum.shoes1 || current2.Id == ClothingPartEnum.shoes2 || current2.Id == ClothingPartEnum.shoes3 || current2.Id == ClothingPartEnum.shoes4 || current2.Id == ClothingPartEnum.no_shoes)
				{
					HeroImage.shoesParts.Add(current2);
				}
				else if (current2.Id == ClothingPartEnum.bodyblack || current2.Id == ClothingPartEnum.bodyblue || current2.Id == ClothingPartEnum.bodybrown || current2.Id == ClothingPartEnum.bodywhite || current2.Id == ClothingPartEnum.bodygreen || current2.Id == ClothingPartEnum.bodypurple)
				{
					HeroImage.skinParts.Add(current2);
				}
				else if (current2.Id == ClothingPartEnum.shortHair || current2.Id == ClothingPartEnum.longHair || current2.Id == ClothingPartEnum.type3hair)
				{
					HeroImage.hairLenght.Add(current2);
				}
				current2.PreviousValue = current2.IsSet;
			}
			HeroImage.IsInitialized = true;
		}

		public static void InitRessources()
		{
			if (!HeroImage.ShouldInitRessources)
			{
				return;
			}
			try
			{
				if (App.State.Avatar.IsFemale)
				{
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.clothes1).Image = (Texture2D)Resources.Load("Gui/heroes/female_clothes_white", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.clothes2).Image = (Texture2D)Resources.Load("Gui/heroes/female_clothes_red", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.shoes1).Image = (Texture2D)Resources.Load("Gui/heroes/female_shoes_white", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.shoes2).Image = (Texture2D)Resources.Load("Gui/heroes/female_shoes_red", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.shoes3).Image = (Texture2D)Resources.Load("Gui/heroes/female_shoes_big", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.shoes4).Image = (Texture2D)Resources.Load("Gui/heroes/female_shoes_sandals", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra1).Image = (Texture2D)Resources.Load("Gui/heroes/female_extra1", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra2).Image = (Texture2D)Resources.Load("Gui/heroes/female_extra2", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra3).Image = (Texture2D)Resources.Load("Gui/heroes/female_extra3", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra4).Image = (Texture2D)Resources.Load("Gui/heroes/female_extra4", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra5).Image = (Texture2D)Resources.Load("Gui/heroes/female_extra5", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra6).Image = (Texture2D)Resources.Load("Gui/heroes/female_extra6", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra7).Image = (Texture2D)Resources.Load("Gui/heroes/female_extra7", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra8).Image = (Texture2D)Resources.Load("Gui/heroes/female_extra8", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.wing1).Image = (Texture2D)Resources.Load("Gui/heroes/female_wing_left1", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.wing2).Image = (Texture2D)Resources.Load("Gui/heroes/female_wing_right1", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.aura1).Image = (Texture2D)Resources.Load("Gui/heroes/female_aura_white", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.aura2).Image = (Texture2D)Resources.Load("Gui/heroes/female_aura_blue", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.clothes3).Image = (Texture2D)Resources.Load("Gui/heroes/female_clothes2", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.clothes4).Image = (Texture2D)Resources.Load("Gui/heroes/female_clothes3", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.clothes5).Image = (Texture2D)Resources.Load("Gui/heroes/female_clothes_kimono", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.clothes6).Image = (Texture2D)Resources.Load("Gui/heroes/female_clothes_kimono_purple", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.clothes3extra).Image = (Texture2D)Resources.Load("Gui/heroes/female_clothes2_extra", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.bodywhite).Image = (Texture2D)Resources.Load("Gui/heroes/female_body_white", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.bodybrown).Image = (Texture2D)Resources.Load("Gui/heroes/female_body_brown", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.bodyblack).Image = (Texture2D)Resources.Load("Gui/heroes/female_body_black", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.bodyblue).Image = (Texture2D)Resources.Load("Gui/heroes/female_body_blue", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.bodygreen).Image = (Texture2D)Resources.Load("Gui/heroes/female_body_green", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.bodypurple).Image = (Texture2D)Resources.Load("Gui/heroes/female_body_purple", typeof(Texture2D));
				}
				else
				{
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.clothes1).Image = (Texture2D)Resources.Load("Gui/heroes/male_clothes_blue", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.clothes2).Image = (Texture2D)Resources.Load("Gui/heroes/male_clothes_red", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.shoes1).Image = (Texture2D)Resources.Load("Gui/heroes/male_shoes_white", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.shoes2).Image = (Texture2D)Resources.Load("Gui/heroes/male_shoes_red", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.shoes3).Image = (Texture2D)Resources.Load("Gui/heroes/male_shoes_big", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.shoes4).Image = (Texture2D)Resources.Load("Gui/heroes/male_shoes_sandals", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra1).Image = (Texture2D)Resources.Load("Gui/heroes/male_extra1", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra2).Image = (Texture2D)Resources.Load("Gui/heroes/male_extra2", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra3).Image = (Texture2D)Resources.Load("Gui/heroes/male_extra3", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra4).Image = (Texture2D)Resources.Load("Gui/heroes/male_extra4", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra5).Image = (Texture2D)Resources.Load("Gui/heroes/male_extra5", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra6).Image = (Texture2D)Resources.Load("Gui/heroes/male_extra6", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra7).Image = (Texture2D)Resources.Load("Gui/heroes/male_extra7", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.extra8).Image = (Texture2D)Resources.Load("Gui/heroes/male_extra8", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.wing1).Image = (Texture2D)Resources.Load("Gui/heroes/male_wing_left1", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.wing2).Image = (Texture2D)Resources.Load("Gui/heroes/male_wing_right1", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.aura1).Image = (Texture2D)Resources.Load("Gui/heroes/male_aura_white", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.aura2).Image = (Texture2D)Resources.Load("Gui/heroes/male_aura_blue", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.clothes3).Image = (Texture2D)Resources.Load("Gui/heroes/male_clothes2", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.clothes3extra).Image = (Texture2D)Resources.Load("Gui/heroes/male_clothes2_extra", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.clothes4).Image = (Texture2D)Resources.Load("Gui/heroes/male_clothes_white", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.clothes5).Image = (Texture2D)Resources.Load("Gui/heroes/male_clothes_toga", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.clothes6).Image = (Texture2D)Resources.Load("Gui/heroes/male_clothes_toga_blue", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.bodywhite).Image = (Texture2D)Resources.Load("Gui/heroes/male_body_white", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.bodybrown).Image = (Texture2D)Resources.Load("Gui/heroes/male_body_brown", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.bodyblack).Image = (Texture2D)Resources.Load("Gui/heroes/male_body_black", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.bodyblue).Image = (Texture2D)Resources.Load("Gui/heroes/male_body_blue", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.bodygreen).Image = (Texture2D)Resources.Load("Gui/heroes/male_body_green", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.bodypurple).Image = (Texture2D)Resources.Load("Gui/heroes/male_body_purple", typeof(Texture2D));
				}
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.aura3).Image = (Texture2D)Resources.Load("Gui/heroes/aura3", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.wingsBlack).Image = (Texture2D)Resources.Load("Gui/heroes/wings3", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.wing3).Image = (Texture2D)Resources.Load("Gui/heroes/male_wing_left2", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.wing4).Image = (Texture2D)Resources.Load("Gui/heroes/male_wing_right2", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.wing5).Image = (Texture2D)Resources.Load("Gui/heroes/male_wing_left3", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.wing6).Image = (Texture2D)Resources.Load("Gui/heroes/male_wing_right3", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.removeDarkness).Image = (Texture2D)Resources.Load("Gui/heroes/bg", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.arty_reward).Image = (Texture2D)Resources.Load("Gui/heroes/arty_was_here", typeof(Texture2D));
			}
			catch (NullReferenceException)
			{
				int num = 0;
				foreach (ClothingPart current in App.State.Avatar.ClothingParts)
				{
					if (current.IsPermanentUnlocked)
					{
						num += current.PermanentGPCost.ToInt();
					}
				}
				App.State.PremiumBoni.GodPower += num;
				GuiBase.ShowToast("Some error with the Avatar happened. Sorry for the Issue! All Avatar parts had to be reseted. If you bought anything with GP, you got the GP back.");
				HeroImage.Init(true);
			}
			HeroImage.ShouldInitRessources = false;
			HeroImage.AllAvatarParts = new List<Texture2D>();
			HeroImage.allAvatarPartsFilled = false;
		}

		public static void SetHairColors()
		{
			if (!HeroImage.ShouldInitRessources)
			{
				return;
			}
			if (App.State.Avatar.IsFemale)
			{
				if (App.State.Avatar.HairStyle == 0)
				{
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair1).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_black", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair2).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_red", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair3).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_brown", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair4).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_white", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair5).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_blue", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair6).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_blonde", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair7).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_green", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair8).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_purple", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hairGlow).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_glow", typeof(Texture2D));
				}
				else if (App.State.Avatar.HairStyle == 1)
				{
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair1).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_black2", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair2).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_red2", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair3).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_brown2", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair4).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_white2", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair5).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_blue2", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair6).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_blonde2", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair7).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_green2", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair8).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_purple2", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hairGlow).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_glow2", typeof(Texture2D));
				}
				else if (App.State.Avatar.HairStyle == 2)
				{
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair1).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_black3", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair2).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_red3", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair3).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_brown3", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair4).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_white3", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair5).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_blue3", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair6).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_blonde3", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair7).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_green3", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair8).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_purple3", typeof(Texture2D));
					App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hairGlow).Image = (Texture2D)Resources.Load("Gui/heroes/female_head_glow3", typeof(Texture2D));
				}
			}
			else if (App.State.Avatar.HairStyle == 0)
			{
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair1).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_brown", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair2).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_red", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair3).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_black", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair4).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_white", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair5).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_blue", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair6).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_blonde", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair7).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_green", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair8).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_purple", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hairGlow).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_glow", typeof(Texture2D));
			}
			else if (App.State.Avatar.HairStyle == 1)
			{
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair1).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_brown2", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair2).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_red2", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair3).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_black2", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair4).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_white2", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair5).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_blue2", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair6).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_blonde2", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair7).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_green2", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair8).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_purple2", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hairGlow).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_glow2", typeof(Texture2D));
			}
			else if (App.State.Avatar.HairStyle == 2)
			{
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair1).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_brown3", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair2).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_red3", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair3).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_black3", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair4).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_white3", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair5).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_blue3", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair6).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_blonde3", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair7).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_green3", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hair8).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_purple3", typeof(Texture2D));
				App.State.Avatar.ClothingParts.FirstOrDefault((ClothingPart x) => x.Id == ClothingPartEnum.hairGlow).Image = (Texture2D)Resources.Load("Gui/heroes/male_head_glow3", typeof(Texture2D));
			}
		}

		internal static void ShowChooseAvatar(int marginTop)
		{
			HeroImage.Init(false);
			int num = 285;
			int num2 = 180;
			int num3 = 220;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)marginTop), GuiBase.Width((float)num2), GuiBase.Height((float)num3)), HeroImage.femaleBody);
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)marginTop), GuiBase.Width((float)num2), GuiBase.Height((float)num3)), HeroImage.femaleClothesWhite);
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)marginTop), GuiBase.Width((float)num2), GuiBase.Height((float)num3)), HeroImage.femaleShoesWhite);
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)marginTop), GuiBase.Width((float)num2), GuiBase.Height((float)num3)), HeroImage.femaleHeadBlack);
			if (GUI.Button(new Rect(GuiBase.Width((float)(num + 70)), GuiBase.Height((float)(marginTop + 245)), GuiBase.Width(80f), GuiBase.Height(30f)), "Female"))
			{
				App.State.Avatar.IsFemale = true;
				App.State.Avatar.GenderChosen = true;
				HeroImage.IsInitialized = false;
				HeroImage.SetTitle();
			}
			num += 200;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)marginTop), GuiBase.Width((float)num2), GuiBase.Height((float)num3)), HeroImage.maleBody);
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)marginTop), GuiBase.Width((float)num2), GuiBase.Height((float)num3)), HeroImage.maleClothesBlue);
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)marginTop), GuiBase.Width((float)num2), GuiBase.Height((float)num3)), HeroImage.maleShoesRed);
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)marginTop), GuiBase.Width((float)num2), GuiBase.Height((float)num3)), HeroImage.maleHeadBrown);
			if (GUI.Button(new Rect(GuiBase.Width((float)(num + 70)), GuiBase.Height((float)(marginTop + 245)), GuiBase.Width(80f), GuiBase.Height(30f)), "Male"))
			{
				App.State.Avatar.IsFemale = false;
				App.State.Avatar.GenderChosen = true;
				HeroImage.IsInitialized = false;
				HeroImage.SetTitle();
			}
		}

		public static void SetTitle()
		{
			int num = -1;
			foreach (Creation current in App.State.AllCreations)
			{
				if (current.GodToDefeat.IsDefeated)
				{
					num++;
				}
			}
			if (num > 28)
			{
				num = 28;
			}
			if (num <= 0)
			{
				if (App.State.Avatar.IsFemale)
				{
					App.State.Title = "Goddess in Training";
				}
				else
				{
					App.State.Title = "God in Training";
				}
			}
			else
			{
				App.State.Title = EnumName.Title(App.State.Avatar.IsFemale, (God.GodType)num);
			}
		}

		private static void AddToGui(ClothingPart part, int left, int top, int width, int height)
		{
			if (part.Image == null)
			{
				return;
			}
			float num = App.WidthMulti;
			if (App.HeightMulti < num)
			{
				num = App.HeightMulti;
			}
			int num2 = (int)((float)width * num);
			int num3 = (int)((float)height * num);
			if (part.IsSet || part.IsPreview)
			{
				if (part.Id == ClothingPartEnum.arty_reward)
				{
					int num4 = 10;
					int num5 = 180;
					int num6 = (int)(60f * num);
					int num7 = (int)(83f * num);
					if (width > 300)
					{
						num4 = 20;
						num5 = 270;
						num6 = (int)(121f * num);
						num7 = (int)(166f * num);
					}
					GUI.DrawTexture(new Rect(GuiBase.Width((float)num4), GuiBase.Height((float)num5), (float)num6, (float)num7), part.Image);
				}
				else
				{
					GUI.DrawTexture(new Rect(GuiBase.Width((float)left), GuiBase.Height((float)top), (float)num2, (float)num3), part.Image);
					if (!HeroImage.allAvatarPartsFilled)
					{
						HeroImage.AllAvatarParts.Add(part.Image);
					}
				}
			}
		}

		internal static void ShowImage(int left = 25, int top = 47, int width = 180, int height = 220, bool isBigImage = false)
		{
			if (App.State.Avatar.ClothingParts.Count == 0)
			{
				return;
			}
			HeroImage.Init(false);
			HeroImage.CheckValues();
			if (HeroImage.AllAvatarParts == null)
			{
				HeroImage.AllAvatarParts = new List<Texture2D>();
			}
			int num = 10;
			int num2 = 65;
			if (isBigImage)
			{
				num = 25;
				num2 = 130;
			}
			if (!HeroImage.AllClothings[ClothingPartEnum.removeDarkness].IsSet && HeroImage.AllClothings[ClothingPartEnum.removeDarkness].Image != null)
			{
				GUI.DrawTexture(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)top), GuiBase.Width((float)(width + num2)), GuiBase.Height((float)height)), HeroImage.AllClothings[ClothingPartEnum.removeDarkness].Image);
			}
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.aura1], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.aura2], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.aura3], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.wing3], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.wing4], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.wingsBlack], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.wing5], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.wing6], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.clothes3extra], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.bodywhite], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.bodybrown], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.bodyblack], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.bodyblue], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.bodygreen], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.bodypurple], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.shoes1], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.shoes2], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.shoes3], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.shoes4], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.clothes1], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.clothes2], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.clothes4], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.clothes3], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.clothes5], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.clothes6], left, top, width, height);
			if (App.State.Avatar.IsFemale)
			{
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.extra1], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.extra3], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.extra4], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.clothes3extra], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.wing1], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.wing2], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair1], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair2], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair3], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair4], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair5], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair6], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair7], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair8], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hairGlow], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.extra2], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.extra6], left, top, width, height);
			}
			else
			{
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.wing1], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.wing2], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair1], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair2], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair3], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair4], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair5], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair6], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair7], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair8], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hairGlow], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.extra1], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.extra2], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.extra3], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.extra4], left, top, width, height);
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.extra6], left, top, width, height);
			}
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.extra8], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.extra7], left, top, width, height);
			if (HeroImage.AllClothings[ClothingPartEnum.hair2].IsPreview)
			{
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair2], left, top, width, height);
			}
			if (HeroImage.AllClothings[ClothingPartEnum.hair3].IsPreview)
			{
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair3], left, top, width, height);
			}
			if (HeroImage.AllClothings[ClothingPartEnum.hair4].IsPreview)
			{
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair4], left, top, width, height);
			}
			if (HeroImage.AllClothings[ClothingPartEnum.hair5].IsPreview)
			{
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair5], left, top, width, height);
			}
			if (HeroImage.AllClothings[ClothingPartEnum.hair6].IsPreview)
			{
				HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.hair6], left, top, width, height);
			}
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.extra5], left, top, width, height);
			HeroImage.AddToGui(HeroImage.AllClothings[ClothingPartEnum.arty_reward], left, top, width, height);
			HeroImage.allAvatarPartsFilled = true;
			HeroImage.InitAvatarForGodlyShot(HeroImage.AllAvatarParts);
			if (App.State.Ext.PetIdInAvatar > 0)
			{
				if (HeroImage.unlockedPets == null)
				{
					HeroImage.unlockedPets = new List<Pet>();
					foreach (Pet current in App.State.Ext.AllPets)
					{
						if (current.IsUnlocked)
						{
							HeroImage.unlockedPets.Add(current);
						}
					}
					HeroImage.unlockedPets = (from o in HeroImage.unlockedPets
					orderby o.SortValue
					select o).ToList<Pet>();
				}
				if (HeroImage.unlockedPets.Count >= App.State.Ext.PetIdInAvatar)
				{
					HeroImage.ShownPet = HeroImage.unlockedPets[App.State.Ext.PetIdInAvatar - 1];
					int num3 = width / 2;
					float num4 = (float)width * 0.9f;
					if (isBigImage)
					{
						num4 += 25f;
					}
					float num5 = App.WidthMulti;
					if (App.HeightMulti < num5)
					{
						num5 = App.HeightMulti;
					}
					int num6 = (int)((float)num3 * num5);
					GUI.DrawTexture(new Rect(GuiBase.Width(num4), GuiBase.Height((float)(top + height - num3)), (float)num6, (float)num6), HeroImage.ShownPet.Image);
					if (GUI.Button(new Rect(GuiBase.Width(num4), GuiBase.Height((float)(top + height - num3)), (float)num6, (float)num6), HeroImage.EmptyTexture, GUI.skin.GetStyle("Label")) && Event.current.button != 1)
					{
						MainUi.ShowPage(MainUi.Pages.Pets);
					}
				}
			}
			for (int i = 0; i < App.State.Ext.Factory.EquippedCrystals.Count; i++)
			{
				Crystal crystal = App.State.Ext.Factory.EquippedCrystals[i];
				Rect position = new Rect(GuiBase.Width((float)(left + 350 - i % 2 * 50)), GuiBase.Height((float)(top + i / 2 * 60)), GuiBase.Width(60f), GuiBase.Height(60f));
				if (!isBigImage)
				{
					position = new Rect(GuiBase.Width((float)(left + 200 - i % 2 * 30)), GuiBase.Height((float)(top + i / 2 * 35)), GuiBase.Width(30f), GuiBase.Height(30f));
				}
				if (GUI.Button(position, new GUIContent(crystal.Image, crystal.EquipDescription), GUI.skin.GetStyle("Label")) && Event.current.button != 1)
				{
					MainUi.ShowPage(MainUi.Pages.PlanetCrystal);
				}
			}
		}

		private static void InitAvatarForGodlyShot(List<Texture2D> allParts)
		{
			if (App.CurrentPlattform != Plattform.Android && allParts != null && allParts.Count > HeroImage.copyIndex)
			{
				if (HeroImage.copyIndex == 0)
				{
					AreaGodlyShoot.Instance.Avatar = HeroImage.CopyTexture(allParts[0]);
				}
				else
				{
					Texture2D texture2D = HeroImage.CopyTexture(allParts[HeroImage.copyIndex]);
					if (AreaGodlyShoot.Instance.Avatar != texture2D)
					{
						AreaGodlyShoot.Instance.Avatar = HeroImage.CombineTextures2(AreaGodlyShoot.Instance.Avatar, texture2D);
					}
				}
				HeroImage.copyIndex++;
			}
		}

		public static Texture2D CopyTexture(Texture2D baseTexture)
		{
			Color[] pixels = baseTexture.GetPixels();
			Texture2D texture2D = new Texture2D(baseTexture.width, baseTexture.height, TextureFormat.RGBA32, false);
			texture2D.SetPixels(pixels);
			texture2D.Apply(false);
			return texture2D;
		}

		public static Texture2D CombineTextures2(Texture2D background, Texture2D watermark)
		{
			int num = 0;
			int num2 = background.height - watermark.height;
			for (int i = num; i < background.width; i++)
			{
				for (int j = num2; j < background.height; j++)
				{
					Color pixel = background.GetPixel(i, j);
					Color pixel2 = watermark.GetPixel(i - num, j - num2);
					Color color = Color.Lerp(pixel, pixel2, pixel2.a / 1f);
					background.SetPixel(i, j, color);
				}
			}
			background.Apply();
			return background;
		}

		internal static void ShowComplete()
		{
			if (App.State == null)
			{
				return;
			}
			if (HeroImage.PetSelections == null)
			{
				HeroImage.unlockedPets = new List<Pet>();
				foreach (Pet current in App.State.Ext.AllPets)
				{
					if (current.IsUnlocked)
					{
						HeroImage.unlockedPets.Add(current);
					}
				}
				HeroImage.unlockedPets = (from o in HeroImage.unlockedPets
				orderby o.SortValue
				select o).ToList<Pet>();
				HeroImage.PetSelections = new string[HeroImage.unlockedPets.Count + 1];
				HeroImage.PetSelections[0] = "None";
				for (int i = 0; i < HeroImage.PetSelections.Length - 1; i++)
				{
					HeroImage.PetSelections[i + 1] = HeroImage.unlockedPets[i].Name;
				}
				HeroImage.PetSelection = App.State.Ext.PetIdInAvatar;
			}
			GUI.BeginGroup(new Rect(GuiBase.Width(280f), GuiBase.Height(110f), GuiBase.Width(670f), GuiBase.Height(480f)));
			GUI.Box(new Rect(GuiBase.Width(10f), GuiBase.Height(0f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.alignment = TextAnchor.UpperLeft;
			HeroImage.ShowImage(65, 50, 328, 400, true);
			bool isFemale = App.State.Avatar.IsFemale;
			string text = App.State.AvatarName;
			if (string.IsNullOrEmpty(text))
			{
				text = "Guest";
			}
			style.fontStyle = FontStyle.Bold;
			style.alignment = TextAnchor.UpperCenter;
			style.fontSize = GuiBase.FontSize(20);
			GUI.Label(new Rect(GuiBase.Width(0f), GuiBase.Height(10f), GuiBase.Width(500f), GuiBase.Height(130f)), text + ", " + App.State.Title);
			style.fontSize = GuiBase.FontSize(14);
			style.fontStyle = FontStyle.Normal;
			GUIStyle style2 = GUI.skin.GetStyle("Button");
			style2.alignment = TextAnchor.MiddleCenter;
			style2.fontSize = GuiBase.FontSize(14);
			HeroImage.ToolbarSelection = GUI.SelectionGrid(new Rect(GuiBase.Width(500f), GuiBase.Height(20f), GuiBase.Width(150f), GuiBase.Height(100f)), HeroImage.ToolbarSelection, HeroImage.Selections, 2);
			HeroImage.CurrentSelections toolbarSelection = (HeroImage.CurrentSelections)HeroImage.ToolbarSelection;
			int num = 140;
			switch (toolbarSelection)
			{
			case HeroImage.CurrentSelections.Hair:
			{
				num = HeroImage.AddToggles(HeroImage.AllClothings[ClothingPartEnum.shortHair], HeroImage.AllClothings[ClothingPartEnum.longHair], num, "Short", "Long", style2);
				string name = "Brown";
				string name2 = "Black";
				string name3 = "Spiky";
				if (App.State.Avatar.IsFemale)
				{
					name = "Black";
					name2 = "Brown";
					name3 = "Twin";
				}
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.type3hair], num, name3, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.hair1], num, name, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.hair2], num, "Red", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.hair3], num, name2, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.hair4], num, "White", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.hair5], num, "Blue", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.hair6], num, "Blonde", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.hair7], num, "Green", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.hair8], num, "Purple", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.hairGlow], num, "Hair glow", style2);
				break;
			}
			case HeroImage.CurrentSelections.Skin:
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.bodywhite], num, "White", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.bodybrown], num, "Brown", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.bodyblack], num, "Black", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.bodyblue], num, "Blue", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.bodygreen], num, "Green", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.bodypurple], num, "Purple", style2);
				break;
			case HeroImage.CurrentSelections.Clothes:
			{
				string name4 = "Blue";
				if (isFemale)
				{
					name4 = "White Dress";
				}
				string name5 = "Red";
				if (isFemale)
				{
					name5 = "Red Dress";
				}
				string name6 = "White";
				if (isFemale)
				{
					name6 = "Blue Dress";
				}
				string name7 = "Ancient";
				if (isFemale)
				{
					name7 = "Pink";
				}
				string name8 = "Cape";
				if (isFemale)
				{
					name8 = "Extra";
				}
				string name9 = "White Toga";
				if (isFemale)
				{
					name9 = "Red Kimono";
				}
				string name10 = "Blue Toga";
				if (isFemale)
				{
					name10 = "Purple Kimono";
				}
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.clothes1], num, name4, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.clothes2], num, name5, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.clothes4], num, name6, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.clothes3], num, name7, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.clothes5], num, name9, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.clothes6], num, name10, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.clothes3extra], num, name8, style2);
				break;
			}
			case HeroImage.CurrentSelections.Shoes:
			{
				string name11 = "Red Boots";
				if (isFemale)
				{
					name11 = "Pink Boots";
				}
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.shoes1], num, "Small White", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.shoes2], num, "Small Red", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.shoes3], num, name11, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.shoes4], num, "Sandals", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.no_shoes], num, "Bare Feet", style2);
				break;
			}
			case HeroImage.CurrentSelections.Wings:
				num = HeroImage.AddToggles(HeroImage.AllClothings[ClothingPartEnum.wing1], HeroImage.AllClothings[ClothingPartEnum.wing2], num, "Left 1", "Right 1", style2);
				num = HeroImage.AddToggles(HeroImage.AllClothings[ClothingPartEnum.wing3], HeroImage.AllClothings[ClothingPartEnum.wing4], num, "Left 2", "Right 2", style2);
				num = HeroImage.AddToggles(HeroImage.AllClothings[ClothingPartEnum.wing5], HeroImage.AllClothings[ClothingPartEnum.wing6], num, "Left 3", "Right 3", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.wingsBlack], num, "Dark Wings", style2);
				break;
			case HeroImage.CurrentSelections.Aura:
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.aura1], num, "Aura 1", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.aura2], num, "Aura 2", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.aura3], num, "Aura 3", style2);
				break;
			case HeroImage.CurrentSelections.Other:
			{
				string name12 = "Necklace";
				if (!isFemale)
				{
					name12 = "Galaxy";
				}
				string name13 = "Tiara";
				if (!isFemale)
				{
					name13 = "Burger";
				}
				string name14 = "Right Bracelet";
				if (!isFemale)
				{
					name14 = "Sun Glasses";
				}
				string name15 = "Left Bracelet";
				if (!isFemale)
				{
					name15 = "Santa Beard";
				}
				string name16 = "Santa Hat";
				string name17 = "Baal";
				string name18 = "Rose";
				if (!isFemale)
				{
					name18 = "Laurel Wreath";
				}
				string name19 = "Headband";
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.extra1], num, name12, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.extra2], num, name13, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.extra3], num, name14, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.extra4], num, name15, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.extra5], num, name16, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.extra6], num, name17, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.extra7], num, name18, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.extra8], num, name19, style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.removeDarkness], num, "Remove darkness", style2);
				num = HeroImage.AddToggle(HeroImage.AllClothings[ClothingPartEnum.arty_reward], num, "Arty was here", style2);
				break;
			}
			case HeroImage.CurrentSelections.Pets:
				HeroImage.petScrollPos = GuiBase.TouchScrollView(new Rect(GuiBase.Width(0f), GuiBase.Height((float)num), GuiBase.Width(650f), GuiBase.Height(330f)), HeroImage.petScrollPos, new Rect(0f, GuiBase.Height((float)num), GuiBase.Width(620f), GuiBase.Height((float)(HeroImage.PetSelections.Length * 30 + 10))));
				HeroImage.PetSelection = GUI.SelectionGrid(new Rect(GuiBase.Width(500f), GuiBase.Height((float)num), GuiBase.Width(130f), GuiBase.Height((float)(HeroImage.PetSelections.Length * 30))), HeroImage.PetSelection, HeroImage.PetSelections, 1);
				App.State.Ext.PetIdInAvatar = HeroImage.PetSelection;
				GUI.EndScrollView();
				break;
			}
			style2.normal.textColor = Gui.MainColor;
			GUI.EndGroup();
			if (HeroImage.DialogPart != null)
			{
				HeroImage.ShowDialog();
			}
		}

		private static void CheckValues()
		{
			HeroImage.CheckPartList(HeroImage.shoesParts);
			HeroImage.CheckPartList(HeroImage.clothingParts);
			HeroImage.CheckPartList(HeroImage.headParts);
			HeroImage.CheckPartList(HeroImage.skinParts);
			HeroImage.CheckPartList(HeroImage.hairLenght);
			int hairStyle = App.State.Avatar.HairStyle;
			if (HeroImage.hairLenght[0].IsSet)
			{
				App.State.Avatar.HairStyle = 0;
			}
			else if (HeroImage.hairLenght[1].IsSet)
			{
				App.State.Avatar.HairStyle = 1;
			}
			else if (HeroImage.hairLenght[2].IsSet)
			{
				App.State.Avatar.HairStyle = 2;
			}
			if (HeroImage.hairLenght[1].IsPreview)
			{
				App.State.Avatar.HairStyle = 1;
			}
			else if (HeroImage.hairLenght[2].IsPreview)
			{
				App.State.Avatar.HairStyle = 2;
			}
			if (hairStyle != App.State.Avatar.HairStyle)
			{
				App.State.GameSettings.AvaScaled = false;
				HeroImage.ShouldInitRessources = true;
				HeroImage.SetHairColors();
				HeroImage.ShouldInitRessources = false;
			}
		}

		private static void CheckPartList(List<ClothingPart> list)
		{
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			for (int i = 0; i < list.Count; i++)
			{
				ClothingPart clothingPart = list[i];
				if (clothingPart.IsPlayerSet != clothingPart.PreviousValue)
				{
					num = i;
					flag2 = true;
				}
				if (clothingPart.IsPlayerSet && clothingPart.IsPermanentUnlocked)
				{
					flag = true;
				}
			}
			if (flag2)
			{
				for (int j = 0; j < list.Count; j++)
				{
					ClothingPart clothingPart2 = list[j];
					if (j == num)
					{
						clothingPart2.IsPlayerSet = true;
					}
					else
					{
						clothingPart2.IsPlayerSet = false;
					}
					clothingPart2.PreviousValue = clothingPart2.IsPlayerSet;
					if (clothingPart2.IsPlayerSet && clothingPart2.IsPermanentUnlocked)
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				list[0].IsPlayerSet = true;
			}
		}

		private static int AddToggle(ClothingPart returnValue, int marginTop, string name, GUIStyle buttonStyle)
		{
			if (returnValue.Id == ClothingPartEnum.arty_reward)
			{
				if (App.State.Statistic.ArtyChallengesFinished < 1)
				{
					buttonStyle.normal.textColor = Color.red;
				}
				else
				{
					buttonStyle.normal.textColor = Gui.MainColor;
				}
			}
			else if (!string.IsNullOrEmpty(returnValue.NameOfMissing()))
			{
				buttonStyle.normal.textColor = Color.red;
			}
			else
			{
				buttonStyle.normal.textColor = Gui.MainColor;
			}
			returnValue.IsPlayerSet = GUI.Toggle(new Rect(GuiBase.Width(500f), GuiBase.Height((float)marginTop), GuiBase.Width(150f), GuiBase.Height(25f)), returnValue.IsPlayerSet, new GUIContent(name, string.Empty), buttonStyle);
			if (returnValue.IsPlayerSet && !returnValue.IsSet)
			{
				returnValue.IsPlayerSet = false;
				if (returnValue.Id == ClothingPartEnum.arty_reward)
				{
					HeroImage.DialogText = "You need to beat the 'Ultimate Arty Challenge' to unlock " + name + "!";
				}
				else
				{
					HeroImage.DialogText = HeroImage.NeededTextFromClothingPart(returnValue) + name + "!";
				}
				HeroImage.partName = name;
				HeroImage.DialogPart = returnValue;
			}
			return marginTop + 28;
		}

		private static int AddToggles(ClothingPart returnValue1, ClothingPart returnValue2, int marginTop, string name1, string name2, GUIStyle buttonStyle)
		{
			if (!string.IsNullOrEmpty(returnValue1.NameOfMissing()))
			{
				buttonStyle.normal.textColor = Color.red;
			}
			else
			{
				buttonStyle.normal.textColor = Gui.MainColor;
			}
			returnValue1.IsPlayerSet = GUI.Toggle(new Rect(GuiBase.Width(500f), GuiBase.Height((float)marginTop), GuiBase.Width(74f), GuiBase.Height(25f)), returnValue1.IsPlayerSet, new GUIContent(name1, string.Empty), buttonStyle);
			if (!string.IsNullOrEmpty(returnValue2.NameOfMissing()))
			{
				buttonStyle.normal.textColor = Color.red;
			}
			else
			{
				buttonStyle.normal.textColor = Gui.MainColor;
			}
			returnValue2.IsPlayerSet = GUI.Toggle(new Rect(GuiBase.Width(576f), GuiBase.Height((float)marginTop), GuiBase.Width(74f), GuiBase.Height(25f)), returnValue2.IsPlayerSet, new GUIContent(name2, string.Empty), buttonStyle);
			if (returnValue1.IsPlayerSet && !returnValue1.IsSet)
			{
				returnValue1.IsPlayerSet = false;
				HeroImage.DialogText = HeroImage.NeededTextFromClothingPart(returnValue1) + name1 + "!";
				HeroImage.partName = name1;
				HeroImage.DialogPart = returnValue1;
			}
			if (returnValue2.IsPlayerSet && !returnValue2.IsSet)
			{
				returnValue2.IsPlayerSet = false;
				HeroImage.DialogText = HeroImage.NeededTextFromClothingPart(returnValue2) + name2 + "!";
				HeroImage.partName = name2;
				HeroImage.DialogPart = returnValue2;
			}
			return marginTop + 28;
		}

		private static string NeededTextFromClothingPart(ClothingPart part)
		{
			if (part.GodDefeatedTierNeeded > 0)
			{
				return "You need to defeat " + part.NameOfMissing() + " to unlock ";
			}
			if (part.CreationTierNeeded > 0)
			{
				return "You need to create at least one " + part.NameOfMissing() + " to unlock ";
			}
			if (part.PermanentGPCost > 0)
			{
				return "You need to pay " + part.NameOfMissing() + " god power to unlock ";
			}
			return string.Empty;
		}

		private static void ShowDialog()
		{
			if (HeroImage.DialogPart.PermanentGPCost <= 0)
			{
				GuiBase.ShowToast(HeroImage.DialogText);
				HeroImage.DialogPart.IsPreview = false;
				HeroImage.DialogPart = null;
				return;
			}
			GUI.BeginGroup(new Rect(GuiBase.Width(370f), GuiBase.Height(180f), GuiBase.Width(350f), GuiBase.Height(240f)));
			GUI.Box(new Rect(GuiBase.Width(0f), GuiBase.Height(0f), GuiBase.Width(350f), GuiBase.Height(200f)), string.Empty);
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.alignment = TextAnchor.UpperCenter;
			style.fontSize = GuiBase.FontSize(16);
			style.fontStyle = FontStyle.Normal;
			GUIStyle style2 = GUI.skin.GetStyle("Button");
			style2.alignment = TextAnchor.MiddleCenter;
			style2.fontSize = GuiBase.FontSize(16);
			HeroImage.DialogPart.IsPreview = true;
			GUI.Label(new Rect(GuiBase.Width(15f), GuiBase.Height(20f), GuiBase.Width(320f), GuiBase.Height(90f)), HeroImage.DialogText + "\nDo you want to pay " + HeroImage.DialogPart.PermanentGPCost.GuiText + " god power?");
			if (GUI.Button(new Rect(GuiBase.Width(40f), GuiBase.Height(130f), GuiBase.Width(125f), GuiBase.Height(30f)), "Yes"))
			{
				HeroImage.DialogPart.IsPreview = false;
				if (GodPowerUi.SpendGodPower(HeroImage.DialogPart.PermanentGPCost))
				{
					HeroImage.DialogPart.IsPermanentUnlocked = true;
					HeroImage.DialogPart.IsPlayerSet = true;
					GuiBase.ShowToast(HeroImage.partName + " is now unlocked permanently!");
					App.SaveGameState();
				}
				HeroImage.DialogPart = null;
			}
			if (GUI.Button(new Rect(GuiBase.Width(180f), GuiBase.Height(130f), GuiBase.Width(125f), GuiBase.Height(30f)), "No"))
			{
				HeroImage.DialogPart.IsPreview = false;
				HeroImage.DialogPart = null;
			}
			GUI.EndGroup();
		}
	}
}
