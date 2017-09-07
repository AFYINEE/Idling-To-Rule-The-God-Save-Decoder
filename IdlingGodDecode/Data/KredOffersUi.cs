using Assets.Scripts.Data;
using Assets.Scripts.Helper;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class KredOffersUi : GuiBase
	{
		private enum Reward
		{
			physical,
			mystic,
			battle,
			creating,
			all,
			div1,
			div2,
			creatingSpeed,
			puny,
			strong,
			mighty,
			lucky
		}

		public static KredOffersUi Instance = new KredOffersUi();

		private static Texture2D imgChakraPill;

		private static Texture2D imgGodlyLiquid;

		private static Texture2D imgChakraPillV2;

		private static Texture2D imgGodlyLiquidV2;

		private static Texture2D imgPetToken;

		private static Texture2D imgShadowSummon;

		private static Texture2D kreds;

		private static Texture2D godPower;

		private static Texture2D luckyDraw;

		private static Texture2D dailyPack;

		private static bool ShowAds = false;

		private static Vector2 scrollPosition = Vector2.zero;

		private static int marginTop = 20;

		public void Init()
		{
			KredOffersUi.imgChakraPill = (Texture2D)Resources.Load("Gui/chakra_pill", typeof(Texture2D));
			KredOffersUi.imgGodlyLiquid = (Texture2D)Resources.Load("Gui/godly_liquid", typeof(Texture2D));
			KredOffersUi.imgChakraPillV2 = (Texture2D)Resources.Load("Gui/chakra_pill_v2", typeof(Texture2D));
			KredOffersUi.imgGodlyLiquidV2 = (Texture2D)Resources.Load("Gui/godly_liquid_v2", typeof(Texture2D));
			KredOffersUi.imgShadowSummon = (Texture2D)Resources.Load("Gui/shadow_clones", typeof(Texture2D));
			KredOffersUi.imgPetToken = (Texture2D)Resources.Load("Gui/pet_token", typeof(Texture2D));
			KredOffersUi.kreds = (Texture2D)Resources.Load("Gui/kreds", typeof(Texture2D));
			KredOffersUi.godPower = (Texture2D)Resources.Load("Gui/godpower", typeof(Texture2D));
			KredOffersUi.luckyDraw = (Texture2D)Resources.Load("Gui/lucky", typeof(Texture2D));
			KredOffersUi.dailyPack = (Texture2D)Resources.Load("Gui/daily", typeof(Texture2D));
		}

		public static void Show()
		{
			if (KredOffersUi.ShowAds)
			{
				KredOffersUi.ShowAdUi();
				return;
			}
			GUIStyle style = GUI.skin.GetStyle("Label");
			GUIStyle style2 = GUI.skin.GetStyle("Button");
			style2.fontSize = GuiBase.FontSize(16);
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = GuiBase.FontSize(15);
			GUI.Box(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			if (Kongregate.IsWaitingForPurchase)
			{
				GUI.Label(new Rect(GuiBase.Width(310f), GuiBase.Height(130f), GuiBase.Width(630f), GuiBase.Height(70f)), "Waiting for the result...");
				return;
			}
			KredOffersUi.scrollPosition = GuiBase.TouchScrollView(new Rect(GuiBase.Width(280f), GuiBase.Height(120f), GuiBase.Width(660f), GuiBase.Height(430f)), KredOffersUi.scrollPosition, new Rect(0f, GuiBase.Height(20f), GuiBase.Width(620f), GuiBase.Height((float)KredOffersUi.marginTop)));
			KredOffersUi.marginTop = 25;
			GUI.Label(new Rect(GuiBase.Width(25f), GuiBase.Height((float)(KredOffersUi.marginTop + 5)), GuiBase.Width(400f), GuiBase.Height(30f)), "Want some rewards without spending Kreds?");
			if (GUI.Button(new Rect(GuiBase.Width(445f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Click here")))
			{
				KredOffersUi.ShowAds = true;
			}
			KredOffersUi.marginTop += 55;
			if (!App.State.PremiumBoni.HasPurchasedGodPowerOnce)
			{
				style.fontStyle = FontStyle.Bold;
				style.fontSize = GuiBase.FontSize(18);
				GUI.Label(new Rect(GuiBase.Width(25f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(630f), GuiBase.Height(70f)), "For your first 'God Power' purchase you will receive twice the amount!");
				style.fontSize = GuiBase.FontSize(15);
				style.fontStyle = FontStyle.Normal;
				KredOffersUi.marginTop += 35;
			}
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(300f)));
			KredOffersUi.AddHeader("God Power", 20, style);
			int num = App.State.PremiumBoni.GodPowerPurchaseBonusPercent();
			if (num > 0)
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(120f)), "The more you play and the more you purchase, the more God Power you will receive from purchases.\nCurrent bonus: " + num + "%\nYou will receive what is shown on the buttons, even if the purchase dialog afterwards shows less.");
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "Gain the power of gods!\nWith god power you can buy various permanent bonuses in the god power page.");
			}
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(170f), GuiBase.Width(150f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.GodPower);
			GUI.Label(new Rect(GuiBase.Width(150f), GuiBase.Height(162f), GuiBase.Width(40f), GuiBase.Height(40f)), KredOffersUi.godPower);
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(60f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			int num2 = 5 * (num + 100) / 100;
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy " + num2 + " for 25")))
			{
				Kongregate.PurchaseItem(Premium.ID_GOD_POWER5);
			}
			num2 = 25 * (num + 100) / 100;
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(100f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(100f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy " + num2 + " for 110")))
			{
				Kongregate.PurchaseItem(Premium.ID_GOD_POWER25);
			}
			num2 = 50 * (num + 100) / 100;
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(140f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(140f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy " + num2 + " for 210")))
			{
				Kongregate.PurchaseItem("jeahitsmine");
			}
			num2 = 100 * (num + 100) / 100;
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(180f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(180f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy " + num2 + " for 399")))
			{
				Kongregate.PurchaseItem(Premium.ID_GOD_POWER100);
			}
			KredOffersUi.marginTop += 225;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(250f)));
			KredOffersUi.AddHeader("Lucky Draws", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(55f)), "Try out your luck!");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(185f), GuiBase.Width(400f), GuiBase.Height(35f)), "You have: " + App.State.PremiumBoni.LuckyDraws.GuiText);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(210f), GuiBase.Width(400f), GuiBase.Height(35f)), "You can open up at most 50 a day.");
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(60f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 1 for 10")))
			{
				Kongregate.PurchaseItem(Premium.ID_LUCKY_DRAW);
			}
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(100f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(100f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 10 for 90")))
			{
				Kongregate.PurchaseItem(Premium.ID_LUCKY_DRAW10);
			}
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(140f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(140f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 50 for 398")))
			{
				Kongregate.PurchaseItem(Premium.ID_LUCKY_DRAW50);
			}
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(180f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.luckyDraw);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(180f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Open one")))
			{
				GuiBase.ShowToast(App.State.PremiumBoni.UseLuckyDraw());
			}
			if (App.State.PremiumBoni.TimeForNextLuckyDraw < 0)
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(95f), GuiBase.Width(200f), GuiBase.Height(25f)), "Get your free draw now!");
				if (GUI.Button(new Rect(GuiBase.Width(240f), GuiBase.Height(100f), GuiBase.Width(130f), GuiBase.Height(32f)), new GUIContent("Get free draw!")))
				{
					App.State.PremiumBoni.GetFreeLuckyDraw();
				}
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(95f), GuiBase.Width(200f), GuiBase.Height(50f)), "Time until your free draw is ready: " + Conv.MsToGuiText(App.State.PremiumBoni.TimeForNextLuckyDraw.ToLong(), true));
			}
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(140f), GuiBase.Width(450f), GuiBase.Height(50f)), App.State.Ext.Lucky.LastDrawText);
			KredOffersUi.marginTop += 240;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(220f)));
			KredOffersUi.AddHeader("Daily Packs", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(55f)), "Get one additional Lucky Draw and 2 God Power once a day for 15 or 30 days!");
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(60f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 15 for 50")))
			{
				Kongregate.PurchaseItem(Premium.ID_DAILY_PACK_15);
			}
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(100f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(100f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 30 for 90")))
			{
				Kongregate.PurchaseItem(Premium.ID_DAILY_PACK);
			}
			int num3 = 110;
			if (App.State.PremiumBoni.DailyPackDaysLeft > 0)
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(150f), GuiBase.Width(400f), GuiBase.Height(30f)), "Packs Left: " + App.State.PremiumBoni.DailyPackDaysLeft.GuiText);
				if (App.State.PremiumBoni.TimeForNextDailyPack <= 0L)
				{
					GUI.Label(new Rect(GuiBase.Width(580f), GuiBase.Height(150f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.dailyPack);
					if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(150f), GuiBase.Width(120f), GuiBase.Height(32f)), new GUIContent("Open Pack")))
					{
						App.State.PremiumBoni.GetDailyPack();
					}
				}
				else
				{
					GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(110f), GuiBase.Width(260f), GuiBase.Height(50f)), "Time until the next pack arrives: " + Conv.MsToGuiText(App.State.PremiumBoni.TimeForNextDailyPack, true));
				}
				num3 = 180;
			}
			KredOffersUi.marginTop += num3;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(300f)));
			KredOffersUi.AddHeader("Pet Token", 20, style);
			int num4 = 60;
			if (!App.State.PremiumBoni.BoughtLimitedPetToken)
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num4), GuiBase.Width(400f), GuiBase.Height(100f)), "Limited Offer. You can only buy this once!");
				GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height((float)num4), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
				if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height((float)num4), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 1 for 49")))
				{
					Kongregate.PurchaseItem(Premium.ID_PET_TOKEN_LIMITED);
				}
				KredOffersUi.marginTop += 40;
				num4 += 40;
			}
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num4), GuiBase.Width(400f), GuiBase.Height(100f)), "Unlock special Pets!\nYou can see the pets in Fight -> Pets.");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)(num4 + 70)), GuiBase.Width(150f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.PetToken.GuiText);
			GUI.Label(new Rect(GuiBase.Width(150f), GuiBase.Height((float)(num4 + 62)), GuiBase.Width(50f), GuiBase.Height(50f)), KredOffersUi.imgPetToken);
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height((float)num4), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height((float)num4), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 1 for 150")))
			{
				Kongregate.PurchaseItem(Premium.ID_PET_TOKEN);
			}
			num4 += 40;
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height((float)num4), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height((float)num4), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 3 for 399")))
			{
				Kongregate.PurchaseItem(Premium.ID_PET_TOKEN3);
			}
			KredOffersUi.marginTop += 180;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(300f)));
			KredOffersUi.AddHeader("Improved Crystal Upgrade", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "Adds 25% to the starting upgrade chance for all crystals (can't go higher than 95%)!\nYou can get this only once.");
			if (App.State.PremiumBoni.HasCrystalImprovement)
			{
				GUI.Label(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "You own this already!");
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(60f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
				if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy for 135")))
				{
					Kongregate.PurchaseItem(Premium.ID_CRYSTAL_CHANCE);
				}
			}
			KredOffersUi.marginTop += 120;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(300f)));
			KredOffersUi.AddHeader("Crystal Equip Slot", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "Adds one more equip slot for your crystals!\n");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(83f), GuiBase.Width(400f), GuiBase.Height(100f)), "Currently you can equip " + App.State.PremiumBoni.MaxCrystals.GuiText + " crystals at once.");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(106f), GuiBase.Width(400f), GuiBase.Height(100f)), "You can buy up to " + (6 - App.State.PremiumBoni.MaxCrystals).GuiText + " more slots.");
			if (App.State.PremiumBoni.MaxCrystals >= 6)
			{
				GUI.Label(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "Your slots are maxed!");
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(60f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
				if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy for 90")))
				{
					Kongregate.PurchaseItem(Premium.ID_CRYSTAL_SLOT);
				}
			}
			KredOffersUi.marginTop += 135;
			GUI.EndGroup();
			if (App.State.PremiumBoni.ImprovedNextAt)
			{
				GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(300f)));
				KredOffersUi.AddHeader("Improved Next At for challenges", 20, style);
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "If you buy this, improved next at will also work in challenges!\n");
				if (App.State.PremiumBoni.ChallengeImprovedNextAt)
				{
					GUI.Label(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "You own this already!");
				}
				else
				{
					GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(60f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
					if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy for 175")))
					{
						Kongregate.PurchaseItem(Premium.ID_CHALLENGE_NEXT_AT);
					}
				}
				KredOffersUi.marginTop += 100;
				GUI.EndGroup();
			}
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(300f)));
			KredOffersUi.AddHeader("Refrigerator", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "Keeps your food save. If you buy this, you won't lose your pet food after rebirthing!\n");
			if (App.State.PremiumBoni.PetFoodAfterRebirth)
			{
				GUI.Label(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "You own this already!");
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(60f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
				if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy for 175")))
				{
					Kongregate.PurchaseItem(Premium.ID_FOOD_AFTER_REBIRTH);
				}
			}
			KredOffersUi.marginTop += 110;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(300f)));
			KredOffersUi.AddHeader("Unlimited Gender Change", 20, style);
			if (App.State.PremiumBoni.HasUnlimitedGenderChange)
			{
				string str = "male";
				if (App.State.Avatar.IsFemale)
				{
					str = "female";
				}
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(390f), GuiBase.Height(100f)), "Change your gender. Your current gender is " + str);
				if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(140f), GuiBase.Height(28f)), new GUIContent("Change")))
				{
					App.State.Avatar.GenderChosen = false;
					HeroImage.Init(true);
				}
				KredOffersUi.marginTop += 20;
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(100f), GuiBase.Width(390f), GuiBase.Height(100f)), "Change your name. Your current name is " + App.State.AvatarName);
				if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(100f), GuiBase.Width(140f), GuiBase.Height(28f)), new GUIContent("Change")))
				{
					App.State.ChangeAvatarName = true;
				}
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "If you buy this, you can change your name and gender for free as often as you want!\n");
				GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(60f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
				if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy for 149")))
				{
					Kongregate.PurchaseItem(Premium.ID_UGC);
				}
			}
			KredOffersUi.marginTop += 150;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(180f)));
			KredOffersUi.AddHeader("Ultimate shadow summon", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(55f)), "Creating shadow clones is too slow? With this you will max them instantly!");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(140f), GuiBase.Width(400f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.UltimateShadowSummonCount);
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(60f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 1 for 15")))
			{
				Kongregate.PurchaseItem(Premium.ID_SHADOW_SUMMON);
			}
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(100f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(100f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 10 for 120")))
			{
				Kongregate.PurchaseItem(Premium.ID_SHADOW_SUMMON10);
			}
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(140f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.imgShadowSummon);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(140f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Use 1")))
			{
				App.State.PremiumBoni.UseUltimateShadowSummon();
			}
			KredOffersUi.marginTop += 170;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(180f)));
			KredOffersUi.AddHeader("Godly liquid", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(55f)), "Doubles your speed when creating creations (and clones) for 90 minutes. (Multiplies with godly liquid V2)");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(110f), GuiBase.Width(400f), GuiBase.Height(30f)), "Duration left: " + Conv.MsToGuiText(App.State.PremiumBoni.GodlyLiquidDuration, true));
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(140f), GuiBase.Width(400f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.GodlyLiquidCount);
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(60f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 1 for 10")))
			{
				Kongregate.PurchaseItem("nightmare");
			}
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(100f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(100f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 10 for 90")))
			{
				Kongregate.PurchaseItem("myfirstid");
			}
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(140f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.imgGodlyLiquid);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(140f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Use 1")))
			{
				App.State.PremiumBoni.UseGodlyLiquid();
			}
			KredOffersUi.marginTop += 170;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(180f)));
			KredOffersUi.AddHeader("Godly liquid V2", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(55f)), "Doubles your speed when creating creations (and clones) until your next rebirth.");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(110f), GuiBase.Width(400f), GuiBase.Height(30f)), "Is in use: " + App.State.PremiumBoni.GodlyLiquidV2InUse);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(140f), GuiBase.Width(400f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.GodlyLiquidV2Count);
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(60f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 1 for 33")))
			{
				Kongregate.PurchaseItem(Premium.ID_GODLY_LIQUID_REBIRTH);
			}
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(100f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(100f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 10 for 299")))
			{
				Kongregate.PurchaseItem(Premium.ID_GODLY_LIQUID_REBIRTH10);
			}
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(140f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.imgGodlyLiquidV2);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(140f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Use 1")))
			{
				App.State.PremiumBoni.UseGodlyLiquidRebirth();
			}
			KredOffersUi.marginTop += 170;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(180f)));
			KredOffersUi.AddHeader("Chakra pill", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(65f)), "Increases your chakra to let your clones build faster.\nDoubles the speed to build monuments and upgrades for 90 minutes. (Multiplies with chakra pill V2)");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(120f), GuiBase.Width(400f), GuiBase.Height(30f)), "Duration left: " + Conv.MsToGuiText(App.State.PremiumBoni.ChakraPillDuration, true));
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(150f), GuiBase.Width(400f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.ChakraPillCount);
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(70f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(70f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 1 for 10")))
			{
				Kongregate.PurchaseItem("xzp");
			}
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(110f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(110f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 10 for 90")))
			{
				Kongregate.PurchaseItem("helloworld");
			}
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(150f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.imgChakraPill);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(150f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Use 1")))
			{
				App.State.PremiumBoni.UseChakraPill();
			}
			KredOffersUi.marginTop += 180;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)KredOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(180f)));
			KredOffersUi.AddHeader("Chakra pill V2", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(65f)), "Increases your chakra to let your clones build faster.\nDoubles the speed to build monuments and upgrades until your next rebirth.");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(120f), GuiBase.Width(400f), GuiBase.Height(30f)), "Is in use: " + App.State.PremiumBoni.ChakraPillV2InUse);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(150f), GuiBase.Width(400f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.ChakraPillV2Count);
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(70f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(70f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 1 for 33")))
			{
				Kongregate.PurchaseItem(Premium.ID_CHAKRA_PILL_REBIRTH);
			}
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(110f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.kreds);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(110f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Buy 10 for 299")))
			{
				Kongregate.PurchaseItem(Premium.ID_CHAKRA_PILL_REBIRTH10);
			}
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height(150f), GuiBase.Width(32f), GuiBase.Height(32f)), KredOffersUi.imgChakraPillV2);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(150f), GuiBase.Width(140f), GuiBase.Height(32f)), new GUIContent("Use 1")))
			{
				App.State.PremiumBoni.UseChakraPillRebirth();
			}
			KredOffersUi.marginTop += 180;
			GUI.EndGroup();
			GUI.EndScrollView();
		}

		private static void AddHeader(string text, int marginTop, GUIStyle labelStyle)
		{
			labelStyle.fontStyle = FontStyle.Bold;
			labelStyle.alignment = TextAnchor.UpperCenter;
			labelStyle.fontSize = GuiBase.FontSize(18);
			GUI.Label(new Rect(0f, GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(30f)), text);
			labelStyle.fontSize = GuiBase.FontSize(15);
			labelStyle.alignment = TextAnchor.UpperLeft;
			labelStyle.fontStyle = FontStyle.Normal;
		}

		private static void ShowAdUi()
		{
			GUIStyle style = GUI.skin.GetStyle("Label");
			GUIStyle style2 = GUI.skin.GetStyle("Button");
			style2.fontSize = GuiBase.FontSize(16);
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = GuiBase.FontSize(16);
			GUI.Box(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			int num = 125;
			int num2 = 305;
			if (GUI.Button(new Rect(GuiBase.Width((float)(num2 + 420)), GuiBase.Height((float)num), GuiBase.Width(140f), GuiBase.Height(28f)), new GUIContent("Back")))
			{
				KredOffersUi.ShowAds = false;
			}
			GUI.Label(new Rect(GuiBase.Width((float)num2), GuiBase.Height((float)(num + 5)), GuiBase.Width(400f), GuiBase.Height(60f)), "For every Ad you watch, you will receive 1 Ad point. You can spend Ad points for various benefits below.");
			bool flag = App.State.Ext.AdsWatched >= 20;
			if (!App.CanShowAds || flag)
			{
				num += 45;
				GUI.Label(new Rect(GuiBase.Width((float)num2), GuiBase.Height((float)(num + 5)), GuiBase.Width(500f), GuiBase.Height(60f)), "You currently have " + App.State.Ext.AdPoints.ToInt() + " Ad points.\nCurrently there are no Ads available. Please come back later.");
			}
			else
			{
				num += 45;
				if (GUI.Button(new Rect(GuiBase.Width((float)(num2 + 420)), GuiBase.Height((float)num), GuiBase.Width(140f), GuiBase.Height(28f)), new GUIContent("Watch Ad")))
				{
					Kongregate.ShowIncentivizedAd();
				}
				GUI.Label(new Rect(GuiBase.Width((float)num2), GuiBase.Height((float)(num + 5)), GuiBase.Width(400f), GuiBase.Height(60f)), "You currently have " + App.State.Ext.AdPoints.ToInt() + " Ad points.");
			}
			num += 50;
			if (!App.State.Statistic.HasStartedUltimateBaalChallenge && !App.State.Statistic.HasStartedArtyChallenge)
			{
				if (App.State.Multiplier.DrawMultiPhysical < 10000000000L)
				{
					KredOffersUi.addSpendPointsButton(ref num, KredOffersUi.Reward.physical, "Increase your Physical by 10% until you rebirth", 1, "Maxed at 100 billion and not useable in UBC / UAC");
				}
				if (App.State.Multiplier.DrawMultiMystic < 10000000000L)
				{
					KredOffersUi.addSpendPointsButton(ref num, KredOffersUi.Reward.mystic, "Increase your Mystic by 10% until you rebirth", 1, "Maxed at 100 billion and not useable in UBC / UAC");
				}
				if (App.State.Multiplier.DrawMultiBattle < 10000000000L)
				{
					KredOffersUi.addSpendPointsButton(ref num, KredOffersUi.Reward.battle, "Increase your Battle by 10% until you rebirth", 1, "Maxed at 100 billion and not useable in UBC / UAC");
				}
				if (App.State.Multiplier.DrawMultiCreating < 10000000000L)
				{
					KredOffersUi.addSpendPointsButton(ref num, KredOffersUi.Reward.creating, "Increase your Creating by 10% until you rebirth", 1, "Maxed at 100 billion and not useable in UBC / UAC");
				}
				if (App.State.Multiplier.DrawMultiPhysical < 10000000000L && App.State.Multiplier.DrawMultiMystic < 10000000000L && App.State.Multiplier.DrawMultiBattle < 10000000000L && App.State.Multiplier.DrawMultiCreating < 10000000000L)
				{
					KredOffersUi.addSpendPointsButton(ref num, KredOffersUi.Reward.all, "Increase all 4 stats by 10% until you rebirth", 3, "Maxed at 100 billion");
				}
			}
			CDouble cDouble = App.State.DivinityGainSec(true) * 1200;
			KredOffersUi.addSpendPointsButton(ref num, KredOffersUi.Reward.div1, "Get " + cDouble.GuiText + " Divinity", 1, string.Empty);
			KredOffersUi.addSpendPointsButton(ref num, KredOffersUi.Reward.div2, "Get " + (cDouble * 2.5).GuiText + " Divinity", 2, string.Empty);
			KredOffersUi.addSpendPointsButton(ref num, KredOffersUi.Reward.creatingSpeed, "Increase your creating speed to 300% for 10 minutes", 2, string.Empty);
			KredOffersUi.addSpendPointsButton(ref num, KredOffersUi.Reward.puny, "Get one Puny Food", 2, string.Empty);
			KredOffersUi.addSpendPointsButton(ref num, KredOffersUi.Reward.strong, "Get one Strong Food", 4, string.Empty);
			KredOffersUi.addSpendPointsButton(ref num, KredOffersUi.Reward.mighty, "Get one Mighty Food", 6, string.Empty);
			KredOffersUi.addSpendPointsButton(ref num, KredOffersUi.Reward.lucky, "Get one Lucky Draw", 10, string.Empty);
		}

		private static void addSpendPointsButton(ref int marginTop, KredOffersUi.Reward reward, string text, CDouble pointCost, string description = "")
		{
			GUI.Label(new Rect(GuiBase.Width(305f), GuiBase.Height((float)(marginTop + 5)), GuiBase.Width(400f), GuiBase.Height(30f)), new GUIContent(text, description));
			App.State.Ext.AdPoints.Round();
			if (GUI.Button(new Rect(GuiBase.Width(725f), GuiBase.Height((float)marginTop), GuiBase.Width(140f), GuiBase.Height(28f)), new GUIContent("Buy for " + pointCost.ToInt())))
			{
				if (App.State.Ext.AdPoints >= pointCost)
				{
					switch (reward)
					{
					case KredOffersUi.Reward.physical:
						App.State.Multiplier.DrawMultiPhysical = App.State.Multiplier.DrawMultiPhysical * 1.1;
						GuiBase.ShowToast("Your Physical was increased by 10%!");
						break;
					case KredOffersUi.Reward.mystic:
						App.State.Multiplier.DrawMultiMystic = App.State.Multiplier.DrawMultiMystic * 1.1;
						GuiBase.ShowToast("Your Mystic was increased by 10%!");
						break;
					case KredOffersUi.Reward.battle:
						App.State.Multiplier.DrawMultiBattle = App.State.Multiplier.DrawMultiBattle * 1.1;
						GuiBase.ShowToast("Your Battle was increased by 10%!");
						break;
					case KredOffersUi.Reward.creating:
						App.State.Multiplier.DrawMultiCreating = App.State.Multiplier.DrawMultiCreating * 1.1;
						GuiBase.ShowToast("Your Creating was increased by 10%!");
						break;
					case KredOffersUi.Reward.all:
						App.State.Multiplier.DrawMultiPhysical = App.State.Multiplier.DrawMultiPhysical * 1.1;
						App.State.Multiplier.DrawMultiMystic = App.State.Multiplier.DrawMultiMystic * 1.1;
						App.State.Multiplier.DrawMultiCreating = App.State.Multiplier.DrawMultiCreating * 1.1;
						App.State.Multiplier.DrawMultiBattle = App.State.Multiplier.DrawMultiBattle * 1.1;
						GuiBase.ShowToast("All 4 stats were increased by 10%!");
						break;
					case KredOffersUi.Reward.div1:
					{
						CDouble cDouble = App.State.DivinityGainSec(true) * 1200;
						App.State.Money += cDouble;
						GuiBase.ShowToast("You received " + cDouble.GuiText + " divinity!");
						break;
					}
					case KredOffersUi.Reward.div2:
					{
						CDouble cDouble = App.State.DivinityGainSec(true) * 1200 * 2.5;
						App.State.Money += cDouble;
						GuiBase.ShowToast("You received " + cDouble.GuiText + " divinity!");
						break;
					}
					case KredOffersUi.Reward.creatingSpeed:
						App.State.CreatingSpeedBoniDuration += 600000L;
						GuiBase.ShowToast("Your creating speed is increased to 300% for another 10 minutes!");
						break;
					case KredOffersUi.Reward.puny:
					{
						State2 expr_3A8 = App.State.Ext;
						expr_3A8.PunyFood = ++expr_3A8.PunyFood;
						GuiBase.ShowToast("You received one Puny Food!");
						break;
					}
					case KredOffersUi.Reward.strong:
					{
						State2 expr_3D1 = App.State.Ext;
						expr_3D1.StrongFood = ++expr_3D1.StrongFood;
						GuiBase.ShowToast("You received one Strong Food!");
						break;
					}
					case KredOffersUi.Reward.mighty:
					{
						State2 expr_3FA = App.State.Ext;
						expr_3FA.MightyFood = ++expr_3FA.MightyFood;
						GuiBase.ShowToast("You received one Mighty Food!");
						break;
					}
					case KredOffersUi.Reward.lucky:
					{
						Premium expr_423 = App.State.PremiumBoni;
						expr_423.LuckyDraws = ++expr_423.LuckyDraws;
						GuiBase.ShowToast("You received one Lucky Draw!");
						break;
					}
					}
					App.State.Ext.AdPoints -= pointCost;
				}
				else
				{
					GuiBase.ShowToast("You don't have enough Ad points!");
				}
			}
			marginTop += 30;
		}
	}
}
