using Assets.Scripts.Data;
using Assets.Scripts.Helper;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	internal class SteamOffersUi : GuiBase
	{
		public static SteamOffersUi Instance = new SteamOffersUi();

		private static Texture2D imgChakraPill;

		private static Texture2D imgGodlyLiquid;

		private static Texture2D imgChakraPillV2;

		private static Texture2D imgGodlyLiquidV2;

		private static Texture2D imgPetToken;

		private static Texture2D imgShadowSummon;

		private static Texture2D godPower;

		private static Texture2D luckyDraw;

		private static Texture2D dailyPack;

		private static Vector2 scrollPosition = Vector2.zero;

		private static int marginTop = 20;

		public void Init()
		{
			SteamOffersUi.imgChakraPill = (Texture2D)Resources.Load("Gui/chakra_pill", typeof(Texture2D));
			SteamOffersUi.imgGodlyLiquid = (Texture2D)Resources.Load("Gui/godly_liquid", typeof(Texture2D));
			SteamOffersUi.imgChakraPillV2 = (Texture2D)Resources.Load("Gui/chakra_pill_v2", typeof(Texture2D));
			SteamOffersUi.imgGodlyLiquidV2 = (Texture2D)Resources.Load("Gui/godly_liquid_v2", typeof(Texture2D));
			SteamOffersUi.imgShadowSummon = (Texture2D)Resources.Load("Gui/shadow_clones", typeof(Texture2D));
			SteamOffersUi.imgPetToken = (Texture2D)Resources.Load("Gui/pet_token", typeof(Texture2D));
			SteamOffersUi.godPower = (Texture2D)Resources.Load("Gui/godpower", typeof(Texture2D));
			SteamOffersUi.luckyDraw = (Texture2D)Resources.Load("Gui/lucky", typeof(Texture2D));
			SteamOffersUi.dailyPack = (Texture2D)Resources.Load("Gui/daily", typeof(Texture2D));
		}

		private static void addPurchaseButton(int left, int top, Purchase purchaseId, int count)
		{
			if (GUI.Button(new Rect(GuiBase.Width((float)left), GuiBase.Height((float)top), GuiBase.Width(180f), GuiBase.Height(32f)), new GUIContent(string.Concat(new object[]
			{
				"Buy ",
				count,
				" for ",
				//Purchases.GetPriceText(purchaseId)
			}))))
			{
				SteamHelper.TimerSinceStarted = UpdateStats.CurrentTimeMillis();
				SteamHelper.PurchaseItem(purchaseId);
			}
		}

		public static void Show()
		{
			GUIStyle style = GUI.skin.GetStyle("Label");
			GUIStyle style2 = GUI.skin.GetStyle("Button");
			style2.fontSize = GuiBase.FontSize(16);
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = GuiBase.FontSize(15);
			GUI.Box(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			if (SteamHelper.IsWaitingForPurchase || App.State.PremiumBoni.ItemIdToPurchase != Purchase.None)
			{
				GUI.Label(new Rect(GuiBase.Width(310f), GuiBase.Height(130f), GuiBase.Width(630f), GuiBase.Height(70f)), "Waiting for the result...");
				return;
			}
			SteamOffersUi.scrollPosition = GuiBase.TouchScrollView(new Rect(GuiBase.Width(280f), GuiBase.Height(120f), GuiBase.Width(660f), GuiBase.Height(430f)), SteamOffersUi.scrollPosition, new Rect(0f, GuiBase.Height(20f), GuiBase.Width(620f), GuiBase.Height((float)SteamOffersUi.marginTop)));
			SteamOffersUi.marginTop = 20;
			if (!App.State.PremiumBoni.HasPurchasedGodPowerOnce)
			{
				style.fontStyle = FontStyle.Bold;
				style.fontSize = GuiBase.FontSize(18);
				GUI.Label(new Rect(GuiBase.Width(25f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(630f), GuiBase.Height(70f)), "For your first 'God Power' purchase you will receive twice the amount!");
				style.fontSize = GuiBase.FontSize(15);
				style.fontStyle = FontStyle.Normal;
				SteamOffersUi.marginTop += 40;
			}
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(300f)));
			SteamOffersUi.AddHeader("God Power", 20, style);
			int num = App.State.PremiumBoni.GodPowerPurchaseBonusPercent();
			if (num > 0)
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(120f)), "The more you play and the more you purchase, the more God Power you will receive from purchases.\nCurrent bonus: " + num + "%\nYou will receive what is shown on the buttons, even if the purchase dialog afterwards shows less.");
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "Gain the power of gods!\nWith god power you can buy various permanent bonuses in the god power page.");
			}
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(180f), GuiBase.Width(150f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.GodPower);
			GUI.Label(new Rect(GuiBase.Width(150f), GuiBase.Height(162f), GuiBase.Width(40f), GuiBase.Height(40f)), SteamOffersUi.godPower);
			int count = 5 * (num + 100) / 100;
			SteamOffersUi.addPurchaseButton(440, 60, Purchase.GodPower5, count);
			count = 25 * (num + 100) / 100;
			SteamOffersUi.addPurchaseButton(440, 100, Purchase.GodPower25, count);
			count = 50 * (num + 100) / 100;
			SteamOffersUi.addPurchaseButton(440, 140, Purchase.GodPower50, count);
			count = 100 * (num + 100) / 100;
			SteamOffersUi.addPurchaseButton(440, 180, Purchase.GodPower100, count);
			SteamOffersUi.marginTop += 225;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(260f)));
			SteamOffersUi.AddHeader("Lucky Draws", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(55f)), "Try out your luck!");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(185f), GuiBase.Width(400f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.LuckyDraws.GuiText);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(210f), GuiBase.Width(400f), GuiBase.Height(35f)), "You can open up at most 50 a day.");
			SteamOffersUi.addPurchaseButton(440, 60, Purchase.LuckyDraw, 1);
			SteamOffersUi.addPurchaseButton(440, 100, Purchase.LuckyDraw10, 10);
			SteamOffersUi.addPurchaseButton(440, 140, Purchase.LuckyDraw50, 50);
			GUI.Label(new Rect(GuiBase.Width(580f), GuiBase.Height(180f), GuiBase.Width(32f), GuiBase.Height(32f)), SteamOffersUi.luckyDraw);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(180f), GuiBase.Width(120f), GuiBase.Height(32f)), new GUIContent("Open one")))
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
			SteamOffersUi.marginTop += 220;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(220f)));
			SteamOffersUi.AddHeader("Daily Packs", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(55f)), "Get one additional Lucky Draw and 2 God Power once a day for 15 or 30 days!");
			SteamOffersUi.addPurchaseButton(440, 60, Purchase.DailyPack15, 15);
			SteamOffersUi.addPurchaseButton(440, 100, Purchase.DailyPack, 30);
			int num2 = 110;
			if (App.State.PremiumBoni.DailyPackDaysLeft > 0)
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(150f), GuiBase.Width(400f), GuiBase.Height(30f)), "Packs Left: " + App.State.PremiumBoni.DailyPackDaysLeft.GuiText);
				if (App.State.PremiumBoni.TimeForNextDailyPack < 0L)
				{
					GUI.Label(new Rect(GuiBase.Width(580f), GuiBase.Height(150f), GuiBase.Width(32f), GuiBase.Height(32f)), SteamOffersUi.dailyPack);
					if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(150f), GuiBase.Width(120f), GuiBase.Height(32f)), new GUIContent("Open Pack")))
					{
						App.State.PremiumBoni.GetDailyPack();
					}
				}
				else
				{
					GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(110f), GuiBase.Width(260f), GuiBase.Height(50f)), "Time until the next pack arrives: " + Conv.MsToGuiText(App.State.PremiumBoni.TimeForNextDailyPack, true));
				}
				num2 = 180;
			}
			SteamOffersUi.marginTop += num2;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(300f)));
			SteamOffersUi.AddHeader("Pet Token", 20, style);
			int num3 = 60;
			if (!App.State.PremiumBoni.BoughtLimitedPetToken)
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num3), GuiBase.Width(400f), GuiBase.Height(100f)), "Limited Offer. You can only buy this once!");
				SteamOffersUi.addPurchaseButton(440, num3, Purchase.PetTokenLimited, 1);
				SteamOffersUi.marginTop += 40;
				num3 += 40;
			}
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num3), GuiBase.Width(400f), GuiBase.Height(100f)), "Unlock special Pets!\nYou can see the pets in Fight -> Pets.");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)(num3 + 70)), GuiBase.Width(150f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.PetToken.GuiText);
			GUI.Label(new Rect(GuiBase.Width(150f), GuiBase.Height((float)(num3 + 62)), GuiBase.Width(50f), GuiBase.Height(50f)), SteamOffersUi.imgPetToken);
			SteamOffersUi.addPurchaseButton(440, num3, Purchase.PetToken, 1);
			num3 += 40;
			SteamOffersUi.addPurchaseButton(440, num3, Purchase.PetToken3, 3);
			SteamOffersUi.marginTop += 180;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(300f)));
			SteamOffersUi.AddHeader("Improved Crystal Upgrade", 20, style);
			num3 = 60;
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "Adds 25% to the starting upgrade chance for all crystals (can't go higher than 95%)!\nYou can get this only once.");
			if (App.State.PremiumBoni.HasCrystalImprovement)
			{
				GUI.Label(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "You own this already!");
			}
			else
			{
				SteamOffersUi.addPurchaseButton(440, num3, Purchase.CrystalUpgradeChance, 1);
			}
			SteamOffersUi.marginTop += 120;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(300f)));
			SteamOffersUi.AddHeader("Crystal Equip Slot", 20, style);
			num3 = 60;
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "Adds one more equip slot for your crystals!\n");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(83f), GuiBase.Width(400f), GuiBase.Height(100f)), "Currently you can equip " + App.State.PremiumBoni.MaxCrystals.GuiText + " crystals at once.");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(106f), GuiBase.Width(400f), GuiBase.Height(100f)), "You can buy up to " + (6 - App.State.PremiumBoni.MaxCrystals).GuiText + " more slots.");
			if (App.State.PremiumBoni.MaxCrystals >= 6)
			{
				GUI.Label(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "Your slots are maxed!");
			}
			else
			{
				SteamOffersUi.addPurchaseButton(440, num3, Purchase.CrystalSlot, 1);
			}
			SteamOffersUi.marginTop += 135;
			GUI.EndGroup();
			if (App.State.PremiumBoni.ImprovedNextAt || App.State.Statistic.HasStartedArtyChallenge || App.State.Statistic.HasStartedUltimateBaalChallenge)
			{
				num3 = 60;
				GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(300f)));
				SteamOffersUi.AddHeader("Improved Next At for challenges", 20, style);
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "If you buy this, improved next at will also work in challenges!\n");
				if (App.State.PremiumBoni.ChallengeImprovedNextAt)
				{
					GUI.Label(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "You own this already!");
				}
				else
				{
					SteamOffersUi.addPurchaseButton(440, num3, Purchase.ChallengeNext, 1);
				}
				SteamOffersUi.marginTop += 100;
				GUI.EndGroup();
			}
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(300f)));
			SteamOffersUi.AddHeader("Refrigerator", 20, style);
			num3 = 60;
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "Keeps your food safe. If you buy this, you won't lose your pet food after rebirthing!\n");
			if (App.State.PremiumBoni.PetFoodAfterRebirth)
			{
				GUI.Label(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "You own this already!");
			}
			else
			{
				SteamOffersUi.addPurchaseButton(440, num3, Purchase.FoodRebirth, 1);
			}
			SteamOffersUi.marginTop += 120;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(300f)));
			SteamOffersUi.AddHeader("Unlimited Gender Change", 20, style);
			if (App.State.PremiumBoni.HasUnlimitedGenderChange)
			{
				string str = "male";
				if (App.State.Avatar.IsFemale)
				{
					str = "female";
				}
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(390f), GuiBase.Height(100f)), "Change your gender. Your current gender is " + str);
				if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(60f), GuiBase.Width(120f), GuiBase.Height(28f)), new GUIContent("Change")))
				{
					App.State.Avatar.GenderChosen = false;
					HeroImage.Init(true);
				}
				SteamOffersUi.marginTop += 20;
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(100f), GuiBase.Width(390f), GuiBase.Height(100f)), "Change your name. Your current name is " + App.State.AvatarName);
				if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(100f), GuiBase.Width(120f), GuiBase.Height(28f)), new GUIContent("Change")))
				{
					App.State.ChangeAvatarName = true;
				}
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(100f)), "If you buy this, you can change your name and gender for free as often as you want!\n");
				SteamOffersUi.addPurchaseButton(440, 60, Purchase.GenderChange, 1);
			}
			SteamOffersUi.marginTop += 150;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(185f)));
			SteamOffersUi.AddHeader("Ultimate shadow summon", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(55f)), "Creating shadow clones is too slow? With this you will max them instantly!");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(140f), GuiBase.Width(400f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.UltimateShadowSummonCount);
			SteamOffersUi.addPurchaseButton(440, 60, Purchase.ShadowSummon, 1);
			SteamOffersUi.addPurchaseButton(440, 100, Purchase.ShadowSummon10, 10);
			GUI.Label(new Rect(GuiBase.Width(580f), GuiBase.Height(140f), GuiBase.Width(32f), GuiBase.Height(32f)), SteamOffersUi.imgShadowSummon);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(140f), GuiBase.Width(120f), GuiBase.Height(32f)), new GUIContent("Use 1")))
			{
				App.State.PremiumBoni.UseUltimateShadowSummon();
			}
			SteamOffersUi.marginTop += 170;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(185f)));
			SteamOffersUi.AddHeader("Godly liquid", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(55f)), "Doubles your speed when creating creations (and clones) for 90 minutes. (Multiplies with godly liquid V2)");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(110f), GuiBase.Width(400f), GuiBase.Height(30f)), "Duration left: " + Conv.MsToGuiText(App.State.PremiumBoni.GodlyLiquidDuration, true));
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(140f), GuiBase.Width(400f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.GodlyLiquidCount);
			SteamOffersUi.addPurchaseButton(440, 60, Purchase.GodlyLiquid, 1);
			SteamOffersUi.addPurchaseButton(440, 100, Purchase.GodlyLiquid10, 10);
			GUI.Label(new Rect(GuiBase.Width(580f), GuiBase.Height(140f), GuiBase.Width(32f), GuiBase.Height(32f)), SteamOffersUi.imgGodlyLiquid);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(140f), GuiBase.Width(120f), GuiBase.Height(32f)), new GUIContent("Use 1")))
			{
				App.State.PremiumBoni.UseGodlyLiquid();
			}
			SteamOffersUi.marginTop += 170;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(185f)));
			SteamOffersUi.AddHeader("Godly liquid V2", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(55f)), "Doubles your speed when creating creations (and clones) until your next rebirth.");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(110f), GuiBase.Width(400f), GuiBase.Height(30f)), "Is in use: " + App.State.PremiumBoni.GodlyLiquidV2InUse);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(140f), GuiBase.Width(400f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.GodlyLiquidV2Count);
			SteamOffersUi.addPurchaseButton(440, 60, Purchase.GodlyLiquidRebirth, 1);
			SteamOffersUi.addPurchaseButton(440, 100, Purchase.GodlyLiquidRebirth10, 10);
			GUI.Label(new Rect(GuiBase.Width(580f), GuiBase.Height(140f), GuiBase.Width(32f), GuiBase.Height(32f)), SteamOffersUi.imgGodlyLiquidV2);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(140f), GuiBase.Width(120f), GuiBase.Height(32f)), new GUIContent("Use 1")))
			{
				App.State.PremiumBoni.UseGodlyLiquidRebirth();
			}
			SteamOffersUi.marginTop += 170;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(185f)));
			SteamOffersUi.AddHeader("Chakra pill", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(65f)), "Increases your chakra to let your clones build faster.\nDoubles the speed to build monuments and upgrades for 90 minutes. (Multiplies with chakra pill V2)");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(120f), GuiBase.Width(400f), GuiBase.Height(30f)), "Duration left: " + Conv.MsToGuiText(App.State.PremiumBoni.ChakraPillDuration, true));
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(150f), GuiBase.Width(400f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.ChakraPillCount);
			SteamOffersUi.addPurchaseButton(440, 60, Purchase.ChakraPill, 1);
			SteamOffersUi.addPurchaseButton(440, 100, Purchase.ChakraPill10, 10);
			GUI.Label(new Rect(GuiBase.Width(580f), GuiBase.Height(150f), GuiBase.Width(32f), GuiBase.Height(32f)), SteamOffersUi.imgChakraPill);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(150f), GuiBase.Width(120f), GuiBase.Height(32f)), new GUIContent("Use 1")))
			{
				App.State.PremiumBoni.UseChakraPill();
			}
			SteamOffersUi.marginTop += 180;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(GuiBase.Width(5f), GuiBase.Height((float)SteamOffersUi.marginTop), GuiBase.Width(670f), GuiBase.Height(185f)));
			SteamOffersUi.AddHeader("Chakra pill V2", 20, style);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(60f), GuiBase.Width(400f), GuiBase.Height(65f)), "Increases your chakra to let your clones build faster.\nDoubles the speed to build monuments and upgrades until your next rebirth.");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(120f), GuiBase.Width(400f), GuiBase.Height(30f)), "Is in use: " + App.State.PremiumBoni.ChakraPillV2InUse);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(150f), GuiBase.Width(400f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.ChakraPillV2Count);
			SteamOffersUi.addPurchaseButton(440, 60, Purchase.ChakraPillRebirth, 1);
			SteamOffersUi.addPurchaseButton(440, 100, Purchase.ChakraPillRebirth10, 10);
			GUI.Label(new Rect(GuiBase.Width(580f), GuiBase.Height(150f), GuiBase.Width(32f), GuiBase.Height(32f)), SteamOffersUi.imgChakraPillV2);
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(150f), GuiBase.Width(120f), GuiBase.Height(32f)), new GUIContent("Use 1")))
			{
				App.State.PremiumBoni.UseChakraPillRebirth();
			}
			SteamOffersUi.marginTop += 180;
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
	}
}
