using Assets.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class GodPowerUi : GuiBase
	{
		public static GodPowerUi Instance = new GodPowerUi();

		private static Texture2D godPower;

		private static Vector2 scrollPosition = Vector2.zero;

		private static int marginTop = 0;

		public void Init()
		{
			GodPowerUi.godPower = (Texture2D)Resources.Load("Gui/godpower", typeof(Texture2D));
		}

		private static void addTextField(ref CDouble value, int godPowerleft)
		{
			int num = value.ToInt();
			string s = GUI.TextField(new Rect(GuiBase.Width(180f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(60f), GuiBase.Height(25f)), value.ToString());
			int num2 = 0;
			int.TryParse(s, out num2);
			if (num2 >= 0 && num2 <= godPowerleft + num)
			{
				value = num2;
			}
		}

		public static void Show()
		{
			GUIStyle style = GUI.skin.GetStyle("Label");
			GUIStyle style2 = GUI.skin.GetStyle("Button");
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = GuiBase.FontSize(16);
			style2.fontSize = GuiBase.FontSize(16);
			App.State.PremiumBoni.CheckIfGPIsAdjusted();
			GUI.Box(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			GodPowerUi.scrollPosition = GuiBase.TouchScrollView(new Rect(GuiBase.Width(280f), GuiBase.Height(120f), GuiBase.Width(660f), GuiBase.Height(430f)), GodPowerUi.scrollPosition, new Rect(0f, GuiBase.Height(20f), GuiBase.Width(620f), GuiBase.Height((float)GodPowerUi.marginTop)));
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(30f), GuiBase.Width(400f), GuiBase.Height(30f)), "Here you can buy various offers with your god power.");
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(68f), GuiBase.Width(150f), GuiBase.Height(30f)), "You have: " + App.State.PremiumBoni.GodPower);
			GUI.Label(new Rect(GuiBase.Width(150f), GuiBase.Height(60f), GuiBase.Width(40f), GuiBase.Height(40f)), GodPowerUi.godPower);
			GodPowerUi.marginTop = 105;
			string tooltip = string.Empty;
			if (App.State.Statistic.UltimateBaalChallengesFinished > 0 || App.State.Statistic.ArtyChallengesFinished > 0)
			{
				tooltip = "The value in brackets is the total boost with your crystal power included.";
			}
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(600f), GuiBase.Height(30f)), new GUIContent("You can adjust unused god power for additional stat multiplier.", tooltip));
			GodPowerUi.marginTop += 30;
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent("Bonus Physical: "));
			GUI.Label(new Rect(GuiBase.Width(250f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent(" % (" + (App.State.PremiumBoni.GpBoniPhysical * (1 + App.State.PremiumBoni.CrystalPower / 1000)).GuiText + ")"));
			App.State.PremiumBoni.GpBoniPhysical = (int)GUI.HorizontalSlider(new Rect(GuiBase.Width(340f), GuiBase.Height((float)(GodPowerUi.marginTop + 2)), GuiBase.Width(200f), GuiBase.Height(30f)), (float)App.State.PremiumBoni.GpBoniPhysical.ToInt(), 0f, (float)App.State.PremiumBoni.MaxBoniPhysical);
			GodPowerUi.addTextField(ref App.State.PremiumBoni.GpBoniPhysical, App.State.PremiumBoni.GodPower);
			GodPowerUi.marginTop += 30;
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent("Bonus Mystic: "));
			GUI.Label(new Rect(GuiBase.Width(250f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent(" % (" + (App.State.PremiumBoni.GpBoniMystic * (1 + App.State.PremiumBoni.CrystalPower / 1000)).GuiText + ")"));
			App.State.PremiumBoni.GpBoniMystic = (int)GUI.HorizontalSlider(new Rect(GuiBase.Width(340f), GuiBase.Height((float)(GodPowerUi.marginTop + 2)), GuiBase.Width(200f), GuiBase.Height(30f)), (float)App.State.PremiumBoni.GpBoniMystic.ToInt(), 0f, (float)App.State.PremiumBoni.MaxBoniMystic);
			GodPowerUi.addTextField(ref App.State.PremiumBoni.GpBoniMystic, App.State.PremiumBoni.GodPower);
			GodPowerUi.marginTop += 30;
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent("Bonus Battle: "));
			GUI.Label(new Rect(GuiBase.Width(250f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent(" % (" + (App.State.PremiumBoni.GpBoniBattle * (1 + App.State.PremiumBoni.CrystalPower / 1000)).GuiText + ")"));
			App.State.PremiumBoni.GpBoniBattle = (int)GUI.HorizontalSlider(new Rect(GuiBase.Width(340f), GuiBase.Height((float)(GodPowerUi.marginTop + 2)), GuiBase.Width(200f), GuiBase.Height(30f)), (float)App.State.PremiumBoni.GpBoniBattle.ToInt(), 0f, (float)App.State.PremiumBoni.MaxBoniBattle);
			GodPowerUi.addTextField(ref App.State.PremiumBoni.GpBoniBattle, App.State.PremiumBoni.GodPower);
			GodPowerUi.marginTop += 30;
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent("Bonus Creating: "));
			GUI.Label(new Rect(GuiBase.Width(250f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent(" % (" + (App.State.PremiumBoni.GpBoniCreating * (1 + App.State.PremiumBoni.CrystalPower / 1000)).GuiText + ")"));
			App.State.PremiumBoni.GpBoniCreating = (int)GUI.HorizontalSlider(new Rect(GuiBase.Width(340f), GuiBase.Height((float)(GodPowerUi.marginTop + 2)), GuiBase.Width(200f), GuiBase.Height(30f)), (float)App.State.PremiumBoni.GpBoniCreating.ToInt(), 0f, (float)App.State.PremiumBoni.MaxBoniCreating);
			GodPowerUi.addTextField(ref App.State.PremiumBoni.GpBoniCreating, App.State.PremiumBoni.GodPower);
			GodPowerUi.marginTop += 35;
			style.fontStyle = FontStyle.Bold;
			if (App.State.PremiumBoni.CrystalPower > 0 || App.State.PremiumBoni.CrystalBonusBuilding > 0)
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(450f), GuiBase.Height(30f)), new GUIContent(string.Concat(new object[]
				{
					"Your current creating speed multiplier: ",
					App.State.PremiumBoni.CreatingSpeedUpPercent(false),
					" % (",
					App.State.PremiumBoni.CreatingSpeedUpPercent(true),
					")"
				}), string.Concat(new object[]
				{
					"The higher your multiplier, the faster you can create things.\n",
					App.State.PremiumBoni.CreatingSpeedUpPercent(false),
					" from god power and ",
					App.State.PremiumBoni.CrystalPower / 2,
					" from crystal power."
				})));
				string text = ".";
				if (App.State.PremiumBoni.CrystalBonusBuilding > 0)
				{
					text = ", increased by " + App.State.PremiumBoni.CrystalBonusBuilding.GuiText + " % from equipped crystals.";
				}
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)(GodPowerUi.marginTop + 28)), GuiBase.Width(450f), GuiBase.Height(30f)), new GUIContent(string.Concat(new object[]
				{
					"Your current building speed multiplier: ",
					App.State.PremiumBoni.BuildingSpeedUpPercent(false),
					" % (",
					App.State.PremiumBoni.BuildingSpeedUpPercent(true),
					")"
				}), string.Concat(new object[]
				{
					"The higher your multiplier, the faster you can build monuments and upgrades.\n",
					App.State.PremiumBoni.BuildingSpeedUpPercent(false),
					" from god power and ",
					App.State.PremiumBoni.CrystalPower / 2,
					" from crystal power",
					text
				})));
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(450f), GuiBase.Height(30f)), new GUIContent("Your current creating speed multiplier: " + App.State.PremiumBoni.CreatingSpeedUpPercent(true) + " %", "The higher your multiplier, the faster you can create things.\n"));
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)(GodPowerUi.marginTop + 28)), GuiBase.Width(450f), GuiBase.Height(30f)), new GUIContent("Your current building speed multiplier: " + App.State.PremiumBoni.BuildingSpeedUpPercent(true) + " %", "The higher your multiplier, the faster you can build monuments and upgrades."));
			}
			if (App.State.Statistic.UltimateBaalChallengesFinished > 0 || App.State.Statistic.ArtyChallengesFinished > 0)
			{
				GodPowerUi.marginTop += 30;
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)(GodPowerUi.marginTop + 28)), GuiBase.Width(400f), GuiBase.Height(30f)), new GUIContent("Crystal Power: " + App.State.PremiumBoni.CrystalPower.GuiText, "If you have crystals equipped when you rebirth, you will receive 1 crystal power for each grade (2 for ultimate crystals, 3 for god crystals).\nEach crystal power will add 0.5% to your creating speed, building speed multiplier, and increases the effectiveness of unused godpower adjusted by 0.1%."));
			}
			if (App.State.Statistic.HighestGodDefeated >= 28 && !App.State.Statistic.HasStartedArtyChallenge)
			{
				GodPowerUi.marginTop += 61;
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(600f), GuiBase.Height(30f)), new GUIContent("Your current chance to lose levels on a miss in the TBS - Game: " + (100 - App.State.PremiumBoni.TbsMissreduction) + " %", "The lower the chance, the less likely you will lose a level when you miss on the TBS - Game. (20% is minimum)"));
				GodPowerUi.marginTop += 28;
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(600f), GuiBase.Height(30f)), new GUIContent("The progress from the TBS - Game you keep after rebirthing: " + App.State.PremiumBoni.TbsProgressAfterRebirth + " %", "After rebirthing you will keep the shown % of your levels in the TBS - Game. (80% is maximum)"));
				GodPowerUi.marginTop += 28;
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(600f), GuiBase.Height(30f)), new GUIContent("Chance for double points in TBS: " + App.State.PremiumBoni.TbsDoublePoints + " %", "If you hit the white bar in TBS, this is your chance to get twice as much points. (100% is maximum)"));
				GodPowerUi.marginTop += 28;
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(600f), GuiBase.Height(30f)), new GUIContent("Extra Pixels for the white area: " + App.State.PremiumBoni.TbsExtraPixels, "The current (and also the minimum) width of the white area is increased by " + App.State.PremiumBoni.TbsExtraPixels + " pixels. (3 is maximum)"));
				GodPowerUi.marginTop -= 20;
			}
			style.fontStyle = FontStyle.Normal;
			GodPowerUi.marginTop += 70;
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase creating speed permanently by 2%", " increases your creating speed permanently by 2%.", GodPowerUi.marginTop, 1, style, delegate
			{
				App.State.PremiumBoni.AddCreationSpeed(2);
				GuiBase.ShowToast("Your creating speed multiplier is now increased by 2%!");
			});
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase creating speed permanently by 5%", " increases your creating speed permanently by 5%.", GodPowerUi.marginTop, 2, style, delegate
			{
				App.State.PremiumBoni.AddCreationSpeed(5);
				GuiBase.ShowToast("Your creating speed multiplier is now increased by 5%!");
			});
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase creating speed permanently by 15%", " increases your creating speed permanently by 15%.", GodPowerUi.marginTop, 5, style, delegate
			{
				App.State.PremiumBoni.AddCreationSpeed(15);
				GuiBase.ShowToast("Your creating speed multiplier is now increased by 15%!");
			});
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase creating speed permanently by 35%", " increases your creating speed permanently by 35%.", GodPowerUi.marginTop, 10, style, delegate
			{
				App.State.PremiumBoni.AddCreationSpeed(35);
				GuiBase.ShowToast("Your creating speed multiplier is now increased by 35%!");
			});
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase creating speed permanently by 350%", " increases your creating speed permanently by 350%.", GodPowerUi.marginTop, 100, style, delegate
			{
				App.State.PremiumBoni.AddCreationSpeed(350);
				GuiBase.ShowToast("Your creating speed multiplier is now increased by 350%!");
			});
			GodPowerUi.marginTop += 20;
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase building speed permanently by 2%", " increases your building speed permanently by 2%.", GodPowerUi.marginTop, 1, style, delegate
			{
				App.State.PremiumBoni.AddBuildingSpeed(2);
				GuiBase.ShowToast("Your building speed multiplier is now increased by 2%!");
			});
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase building speed permanently by 5%", " increases your building speed permanently by 5%.", GodPowerUi.marginTop, 2, style, delegate
			{
				App.State.PremiumBoni.AddBuildingSpeed(5);
				GuiBase.ShowToast("Your building speed multiplier is now increased by 5%!");
			});
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase building speed permanently by 15%", " increases your building speed permanently by 15%.", GodPowerUi.marginTop, 5, style, delegate
			{
				App.State.PremiumBoni.AddBuildingSpeed(15);
				GuiBase.ShowToast("Your building speed multiplier is now increased by 15%!");
			});
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase building speed permanently by 35%", " increases your building speed permanently by 35%.", GodPowerUi.marginTop, 10, style, delegate
			{
				App.State.PremiumBoni.AddBuildingSpeed(35);
				GuiBase.ShowToast("Your building speed multiplier is now increased by 35%!");
			});
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase building speed permanently by 350%", " increases your building speed permanently by 350%.", GodPowerUi.marginTop, 100, style, delegate
			{
				App.State.PremiumBoni.AddBuildingSpeed(350);
				GuiBase.ShowToast("Your building speed multiplier is now increased by 350%!");
			});
			GodPowerUi.marginTop += 20;
			GodPowerUi.marginTop = GodPowerUi.AddItem("Double your statistic multi (currently: " + App.State.PremiumBoni.StatisticMulti.GuiText + " x)", "your statistic multi will double. (It still can't be higher than your god multi, so only buy it, if you feel like your god multi outruns your statistic multi by far)", GodPowerUi.marginTop, 50, style, delegate
			{
				App.State.PremiumBoni.StatisticMulti = App.State.PremiumBoni.StatisticMulti * 2;
				GuiBase.ShowToast("Your statistic multi is now twice as high!");
			});
			GodPowerUi.marginTop += 20;
			GodPowerUi.marginTop = GodPowerUi.AddItem(string.Concat(new object[]
			{
				"Increase the count of creations per bar by one. \nYou need to be able to afford it or nothing will be created.\nCurrently you can create up to ",
				App.State.PremiumBoni.CreationCountBoni(true).ToInt() + 1,
				" (",
				App.State.PremiumBoni.CreationCountBoni(false).ToInt() + 1,
				" from god power) at once."
			}), "increases the count you create for each progressbar in 'Create' by one.", GodPowerUi.marginTop, 50, style, delegate
			{
				App.State.PremiumBoni.AddCreationCountBoni(1);
				GuiBase.ShowToast("You can now create more items for each progress!");
			});
			GodPowerUi.marginTop += 100;
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase your maximum Clones by 1 instantly.\nAlso increases your soft cap by 1.", "increases your maximum clones by 1.", GodPowerUi.marginTop, 1, style, delegate
			{
				App.State.Clones.MaxShadowClones += 1;
				App.State.Clones.AbsoluteMaximum++;
				GuiBase.ShowToast("Your maximum clones are increased by 1!");
			});
			GodPowerUi.marginTop += 25;
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase your maximum Clones by 1000 instantly.\nAlso increases your soft cap by 1000.", "increases your maximum clones by 1000.", GodPowerUi.marginTop, 3, style, delegate
			{
				App.State.Clones.MaxShadowClones += 1000;
				App.State.Clones.AbsoluteMaximum += 1000;
				GuiBase.ShowToast("Your maximum clones are increased by 1000!");
			});
			GodPowerUi.marginTop += 25;
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase your maximum Clones by 10000 instantly.\nAlso increases your soft cap by 10000.", "increases your maximum clones by 10000.", GodPowerUi.marginTop, 25, style, delegate
			{
				App.State.Clones.MaxShadowClones += 10000;
				App.State.Clones.AbsoluteMaximum += 10000;
				GuiBase.ShowToast("Your maximum clones are increased by 10000!");
			});
			GodPowerUi.marginTop += 25;
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase your maximum Clones by 100,000 instantly.\nAlso increases your soft cap by 100,000.", "increases your maximum clones by 100,000.", GodPowerUi.marginTop, 250, style, delegate
			{
				App.State.Clones.MaxShadowClones += 100000;
				App.State.Clones.AbsoluteMaximum += 100000;
				GuiBase.ShowToast("Your maximum clones are increased by 100,000!");
			});
			if (App.State.Statistic.HighestGodDefeated >= 28)
			{
				if (App.State.PremiumBoni.TbsMissreduction < 80)
				{
					GodPowerUi.marginTop += 25;
					GodPowerUi.marginTop = GodPowerUi.AddItem("Reduce the chance to lose a level on the TBS - Game when you miss by 5%.", "decreases the chance to lose points after a miss in the TBS - Game by 5%. (20% is minimum).", GodPowerUi.marginTop, 10, style, delegate
					{
						App.State.PremiumBoni.TbsMissreduction += 5;
						GuiBase.ShowToast("The chance to lose a level if you miss in the TBS - Game is reduced by 5%!");
					});
				}
				if (App.State.PremiumBoni.TbsProgressAfterRebirth < 80)
				{
					GodPowerUi.marginTop += 25;
					GodPowerUi.marginTop = GodPowerUi.AddItem("Keep 10% of your levels from the TBS - Game even after rebirthing.", "you will Keep 10% more of your levels from the TBS - Game after rebirthing. (80% is maximum)", GodPowerUi.marginTop, 10, style, delegate
					{
						App.State.PremiumBoni.TbsProgressAfterRebirth += 10;
						GuiBase.ShowToast("You keep 10% more of your levels after rebirthing!");
					});
				}
				if (App.State.PremiumBoni.TbsDoublePoints < 100)
				{
					GodPowerUi.marginTop += 25;
					GodPowerUi.marginTop = GodPowerUi.AddItem("Increase the chance to score twice as much points in TBS by 5%.", "your chance to score double points in TBS after a hit will increase by 5 %", GodPowerUi.marginTop, 5, style, delegate
					{
						App.State.PremiumBoni.TbsDoublePoints += 5;
						GuiBase.ShowToast("Your chance to score double points in TBS is increased by 5%!");
					});
				}
				if (App.State.PremiumBoni.TbsExtraPixels < 3)
				{
					GodPowerUi.marginTop += 25;
					GodPowerUi.marginTop = GodPowerUi.AddItem("Increase the white area for TBS by 1 pixel.", "the current (and also the minimum) width of the white area in the TBS-Game will be increased by 1 pixel", GodPowerUi.marginTop, 33, style, delegate
					{
						App.State.PremiumBoni.TbsExtraPixels++;
						GuiBase.ShowToast("The current (and also the minimum) width of the white area in TBS is increased by 1 pixel!");
					});
				}
			}
			string str = "male";
			if (App.State.Avatar.IsFemale)
			{
				str = "female";
			}
			GodPowerUi.marginTop += 20;
			if (!App.State.PremiumBoni.HasUnlimitedGenderChange)
			{
				GodPowerUi.marginTop = GodPowerUi.AddItem("Change your gender. Your current gender is " + str, "you can choose your gender again.", GodPowerUi.marginTop, 20, style, delegate
				{
					App.State.Avatar.GenderChosen = false;
					App.State.GameSettings.AvaScaled = false;
					HeroImage.Init(true);
				});
				GodPowerUi.marginTop += 30;
				GodPowerUi.marginTop = GodPowerUi.AddItem("Change your name. Your current name is " + App.State.AvatarName, "you can rename your character.", GodPowerUi.marginTop, 20, style, delegate
				{
					App.State.ChangeAvatarName = true;
				});
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(390f), GuiBase.Height(100f)), "Change your gender. Your current gender is " + str);
				if (GUI.Button(new Rect(GuiBase.Width(420f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(140f), GuiBase.Height(28f)), new GUIContent("Change")))
				{
					App.State.Avatar.GenderChosen = false;
					App.State.GameSettings.AvaScaled = false;
					HeroImage.Init(true);
				}
				GodPowerUi.marginTop += 40;
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(390f), GuiBase.Height(100f)), "Change your name. Your current name is " + App.State.AvatarName);
				if (GUI.Button(new Rect(GuiBase.Width(420f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(140f), GuiBase.Height(28f)), new GUIContent("Change")))
				{
					App.State.ChangeAvatarName = true;
				}
			}
			GodPowerUi.marginTop += 40;
			if (App.State.PremiumBoni.AutoBuyCostReduction < 20)
			{
				GodPowerUi.marginTop += 10;
				GodPowerUi.marginTop = GodPowerUi.AddItem("Decrease Auto buy cost. Currently you pay " + (120 - App.State.PremiumBoni.AutoBuyCostReduction) + " %.", "decreases the cost for auto buy by 1%.", GodPowerUi.marginTop, 3, style, delegate
				{
					App.State.PremiumBoni.AutoBuyCostReduction++;
				});
			}
			if (!App.State.PremiumBoni.HasPetHalfStats)
			{
				GodPowerUi.marginTop += 20;
				GodPowerUi.marginTop = GodPowerUi.AddItem("Pet half stats. This will unlock a button to create clones with half the stats of your pets.", "adds a button 'Half stats' and 'Half stats for all' to the clone creation screen of your pets.", GodPowerUi.marginTop, 100, style, delegate
				{
					App.State.PremiumBoni.HasPetHalfStats = true;
					GuiBase.ShowToast("You can now adjust the stats of your clones with one button! It might not be the most effective way but its fast and easy.");
				});
				GodPowerUi.marginTop += 20;
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(550f), GuiBase.Height(50f)), "Pet half stats is already bought");
				GodPowerUi.marginTop += 20;
			}
			if (!App.State.PremiumBoni.ImprovedNextAt)
			{
				GodPowerUi.marginTop += 20;
				GodPowerUi.marginTop = GodPowerUi.AddItem("Improved 'Next at'. With this your clones will stay at the current skill until a next one is available!", "the 'Next at'-option won't remove clones if there is no skill unlocked.", GodPowerUi.marginTop, 99, style, delegate
				{
					App.State.PremiumBoni.ImprovedNextAt = true;
					GuiBase.ShowToast("Now you can say good bye to micromanaging skills. Just have 'Next at' on with a low value until all skills / trainings are unlocked.");
				});
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(550f), GuiBase.Height(50f)), "Improved 'Next at' is already bought");
				GodPowerUi.marginTop += 20;
			}
			if (!App.State.PremiumBoni.CanShowAlerts)
			{
				GodPowerUi.marginTop += 20;
				GodPowerUi.marginTop = GodPowerUi.AddItem("Show an alert button if your divinity generator is empty or you can feed your pets.", "a button will show up below the achievement toggle, if your divinity generator is empty or your pets can be fed.", GodPowerUi.marginTop, 50, style, delegate
				{
					App.State.PremiumBoni.CanShowAlerts = true;
					GuiBase.ShowToast("Now a button will be shown, if your divinity generator is empty or your pets can be fed.");
				});
			}
			else
			{
				GodPowerUi.marginTop += 20;
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(550f), GuiBase.Height(50f)), "The alert button is already bought");
				GodPowerUi.marginTop += 20;
			}
			GodPowerUi.marginTop += 35;
			style.fontStyle = FontStyle.Bold;
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)GodPowerUi.marginTop), GuiBase.Width(550f), GuiBase.Height(50f)), "The offers below are temporary and lost when you rebirth.");
			style.fontStyle = FontStyle.Normal;
			GodPowerUi.marginTop += 35;
			CDouble newClones = App.State.Clones.MaxShadowClones - App.State.Clones.Count;
			if (newClones > 99999)
			{
				newClones = 99999;
			}
			GodPowerUi.marginTop = GodPowerUi.AddItem("Create up to 99999 Shadow Clones at once! \nThis can't be higher than your maximum clones.", " will let you create " + newClones + " shadow clones at once.", GodPowerUi.marginTop, 3, style, delegate
			{
				App.State.Clones.Count += newClones;
				App.State.Clones.TotalClonesCreated += newClones;
				App.State.Statistic.TotalShadowClonesCreated += newClones;
				if (App.State.Clones.Count > App.State.Clones.MaxShadowClones)
				{
					App.State.Clones.Count = App.State.Clones.MaxShadowClones;
				}
				GuiBase.ShowToast("You created " + newClones + " shadow clones at once!");
			});
			GodPowerUi.marginTop += 20;
			CDouble divinityToGet = App.State.DivinityGainSec(true) * 3600;
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase your 'Divinity' by " + divinityToGet.ToGuiText(true), " will increase your current 'Divinity' by " + divinityToGet.ToGuiText(true), GodPowerUi.marginTop, 1, style, delegate
			{
				App.State.Money += divinityToGet;
				GuiBase.ShowToast("Your 'Divinity' is increased by " + divinityToGet.ToGuiText(true));
			});
			CDouble divinityV2 = divinityToGet * 4;
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase your 'Divinity' by " + divinityV2.ToGuiText(true), " will increase your current 'Divinity' by " + divinityV2.ToGuiText(true), GodPowerUi.marginTop, 3, style, delegate
			{
				App.State.Money += divinityV2;
				GuiBase.ShowToast("Your 'Divinity' is increased by " + divinityV2.ToGuiText(true));
			});
			CDouble divinityV3 = divinityToGet * 8;
			GodPowerUi.marginTop = GodPowerUi.AddItem("Increase your 'Divinity' by " + divinityV3.ToGuiText(true), " will increase your current 'Divinity' by " + divinityV3.ToGuiText(true), GodPowerUi.marginTop, 5, style, delegate
			{
				App.State.Money += divinityV3;
				GuiBase.ShowToast("Your 'Divinity' is increased by " + divinityV3.ToGuiText(true));
			});
			GUI.EndScrollView();
		}

		public static bool SpendGodPower(CDouble cdCount)
		{
			int num = cdCount.ToInt();
			if (App.State.PremiumBoni.GodPower >= num)
			{
				CDouble cDouble = num;
				CDouble cDouble2 = App.State.PremiumBoni.GodPower - App.State.PremiumBoni.GpBoniPhysical - App.State.PremiumBoni.GpBoniMystic - App.State.PremiumBoni.GpBoniBattle - App.State.PremiumBoni.GpBoniCreating;
				cDouble2.Round();
				if (cDouble2 > 0)
				{
					cDouble = num - cDouble2;
					if (cDouble < 0)
					{
						cDouble = 0;
					}
				}
				App.State.PremiumBoni.GpBoniPhysical -= cDouble;
				cDouble = 0;
				if (App.State.PremiumBoni.GpBoniPhysical < 0)
				{
					cDouble = App.State.PremiumBoni.GpBoniPhysical * -1;
					App.State.PremiumBoni.GpBoniPhysical = 0;
				}
				App.State.PremiumBoni.GpBoniMystic -= cDouble;
				cDouble = 0;
				if (App.State.PremiumBoni.GpBoniMystic < 0)
				{
					cDouble = App.State.PremiumBoni.GpBoniMystic * -1;
					App.State.PremiumBoni.GpBoniMystic = 0;
				}
				App.State.PremiumBoni.GpBoniBattle -= cDouble;
				cDouble = 0;
				if (App.State.PremiumBoni.GpBoniBattle < 0)
				{
					cDouble = App.State.PremiumBoni.GpBoniBattle * -1;
					App.State.PremiumBoni.GpBoniBattle = 0;
				}
				App.State.PremiumBoni.GpBoniCreating -= cDouble;
				cDouble = 0;
				if (App.State.PremiumBoni.GpBoniCreating < 0)
				{
					cDouble = App.State.PremiumBoni.GpBoniCreating * -1;
					App.State.PremiumBoni.GpBoniCreating = 0;
				}
				App.State.PremiumBoni.TotalGodPowerSpent += num;
				App.State.PremiumBoni.GodPower -= num;
				App.State.PremiumBoni.permanentGPSpent = 0;
				return true;
			}
			GuiBase.ShowToast("You don't have enough god power!");
			return false;
		}

		private static int AddItem(string text, string dialogText, int marginTop, int cost, GUIStyle labelStyle, Action result)
		{
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)marginTop), GuiBase.Width(390f), GuiBase.Height(130f)), text);
			GUI.Label(new Rect(GuiBase.Width(590f), GuiBase.Height((float)marginTop), GuiBase.Width(32f), GuiBase.Height(32f)), GodPowerUi.godPower);
			if (GUI.Button(new Rect(GuiBase.Width(420f), GuiBase.Height((float)marginTop), GuiBase.Width(140f), GuiBase.Height(28f)), new GUIContent("Get it for " + cost)))
			{
				GuiBase.ShowDialog("Are you sure?", string.Concat(new object[]
				{
					"This will use up ",
					cost,
					" god power and ",
					dialogText
				}), delegate
				{
					if (GodPowerUi.SpendGodPower(cost))
					{
						result();
						App.SaveGameState();
					}
				}, delegate
				{
				}, "Yes", "No", false, false);
			}
			return marginTop + 30;
		}
	}
}
