using Assets.Scripts.Data;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class CreatingUi : GuiBase
	{
		public static CreatingUi Instance = new CreatingUi();

		public static Texture2D imageAchievement;

		public static Texture2D imageCreatingAchievement;

		public static Texture2D imageCreatingAchievementDisable;

		public bool IsAchievement;

		private bool chooseCreationIsOpen;

		private Creation creationToShow;

		private Vector2 scrollPosition = Vector2.zero;

		private Vector2 scrollPositionAllCreations = Vector2.zero;

		private bool showAutoBuyOptions;

		private static bool scrollBarsToZero = false;

		private int creationCount = App.State.GameSettings.CreationToCreateCount;

		public string countToBuy = "1";

		private string[] toolbarNextAt = new string[]
		{
			"Off",
			"1",
			"2"
		};

		private TouchScreenKeyboard keyboard;

		private int svHeight;

		private int toggleNumber;

		private string[] toolbarLeftStrings = new string[]
		{
			"1",
			"2",
			"5",
			"25",
			"75"
		};

		private Vector2 scrollPosAutobuy = Vector2.zero;

		private int marginTop = 125;

		public bool ChooseCreationIsOpen
		{
			get
			{
				if (App.State != null)
				{
					return App.State.GameSettings.ChooseCreationIsOpen;
				}
				return this.chooseCreationIsOpen;
			}
			set
			{
				App.State.GameSettings.ChooseCreationIsOpen = value;
			}
		}

		public string CreationInput
		{
			get
			{
				return this.creationCount.ToString();
			}
			set
			{
				int num = App.State.PremiumBoni.CreationCountBoni(true).ToInt() + 1;
				int num2 = 0;
				int.TryParse(value, out num2);
				if (num2 > 0 && num2 <= num)
				{
					App.State.GameSettings.CreationToCreateCount = num2;
				}
			}
		}

		public void Init()
		{
			CreatingUi.imageAchievement = (Texture2D)Resources.Load("Gui/achievement", typeof(Texture2D));
			CreatingUi.imageAchievement = (Texture2D)Resources.Load("Gui/achiev_creation_reached", typeof(Texture2D));
			CreatingUi.imageAchievement = (Texture2D)Resources.Load("Gui/achiev_creation", typeof(Texture2D));
			if (App.State != null)
			{
				this.ChooseCreationIsOpen = App.State.GameSettings.ChooseCreationIsOpen;
			}
		}

		public static void Show(bool isAchievement)
		{
			CreatingUi.Instance.IsAchievement = isAchievement;
			CreatingUi.Instance.Show();
		}

		private void Show()
		{
			if (CreatingUi.scrollBarsToZero)
			{
				this.scrollPosition = Vector2.zero;
				CreatingUi.scrollBarsToZero = false;
			}
			if (this.IsAchievement)
			{
				GuiBase.ShowAchievements(App.State.CreatingAchievements, "creating");
				return;
			}
			GUIStyle style = GUI.skin.GetStyle("Label");
			if (this.showAutoBuyOptions)
			{
				this.ShowAutoBuyOptions();
			}
			else if (this.creationToShow != null)
			{
				this.ShowBuyCreations();
			}
			else
			{
				style.fontSize = GuiBase.FontSize(16);
				GUI.Box(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
				GUI.BeginGroup(new Rect(GuiBase.Width(280f), GuiBase.Height(110f), GuiBase.Width(670f), GuiBase.Height(480f)));
				int num = 20;
				GUI.Label(new Rect(GuiBase.Width(345f), GuiBase.Height((float)(num - 7)), GuiBase.Width(200f), GuiBase.Height(30f)), new GUIContent("Create Clones if not max", "If this is on, shadow clones will be created instead of the selected creation if they are not maxed already."));
				App.State.GameSettings.CreateShadowClonesIfNotMax = GUI.Toggle(new Rect(GuiBase.Width(580f), GuiBase.Height((float)(num - 5)), GuiBase.Width(70f), GuiBase.Height(30f)), App.State.GameSettings.CreateShadowClonesIfNotMax, new GUIContent(string.Empty));
				if (App.State.IsBuyUnlocked)
				{
					string text = "If this is on, missing creations will be bought automatically if you have enough divinity.";
					if (App.State.PremiumBoni.AutoBuyCostReduction < 20)
					{
						text = string.Concat(new object[]
						{
							text,
							"\nBut beware: there is an additional ",
							20 - App.State.PremiumBoni.AutoBuyCostReduction,
							"% transaction fee!"
						});
					}
					App.State.GameSettings.AutoBuyCreations = GUI.Toggle(new Rect(GuiBase.Width(580f), GuiBase.Height((float)(num + 22)), GuiBase.Width(70f), GuiBase.Height(30f)), App.State.GameSettings.AutoBuyCreations, new GUIContent(string.Empty));
					if (GUI.Button(new Rect(GuiBase.Width(345f), GuiBase.Height((float)(num + 20)), GuiBase.Width(220f), GuiBase.Height(30f)), new GUIContent("Auto buy missing creations", text + "\nClick this button for more advanced settings.")))
					{
						this.showAutoBuyOptions = true;
					}
				}
				num += 50;
				if (App.State.PremiumBoni.CreationCountBoni(true) > 0)
				{
					num -= 25;
					if (App.State.GameSettings.CreationToCreateCount == 0)
					{
						App.State.GameSettings.CreationToCreateCount = 1;
					}
					int num2 = App.State.PremiumBoni.CreationCountBoni(true).ToInt() + 1;
					GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height(15f), GuiBase.Width(155f), GuiBase.Height(30f)), new GUIContent("Creations to create: ", "Cost of resources to create one creation with 1 = 100%, 2 = 195% ... 10 = 775%, 11 = 825%, 12 = 875%..., Creation/s is 100% + 5% for each more than one."));
					if (App.State.IsCrystalFactoryAvailable)
					{
						App.State.GameSettings.MaxAfterEquipCrystal = GUI.Toggle(new Rect(GuiBase.Width(240f), GuiBase.Height((float)(num - 25)), GuiBase.Width(70f), GuiBase.Height(30f)), App.State.GameSettings.MaxAfterEquipCrystal, new GUIContent(string.Empty, "If this is on, your creation count will be adjusted to your maximum count after equipping crystals."));
					}
					if (App.CurrentPlattform == Plattform.Android)
					{
						GUIStyle textField = Gui.ChosenSkin.textField;
						if (GUI.Button(new Rect(GuiBase.Width(185f), GuiBase.Height(15f), GuiBase.Width(45f), GuiBase.Height(25f)), App.State.GameSettings.CreationToCreateCount.ToString(), textField))
						{
							base.ShowNumberInput("Creations to create (Max = " + (App.State.PremiumBoni.CreationCountBoni(true).ToInt() + 1) + ")", App.State.GameSettings.CreationToCreateCount, App.State.PremiumBoni.CreationCountBoni(true).ToInt() + 1, delegate(CDouble x)
							{
								App.State.GameSettings.CreationToCreateCount = x.ToInt();
							});
						}
					}
					else
					{
						this.CreationInput = GUI.TextField(new Rect(GuiBase.Width(185f), GuiBase.Height(15f), GuiBase.Width(45f), GuiBase.Height(25f)), App.State.GameSettings.CreationToCreateCount.ToString());
					}
					Rect position = new Rect(GuiBase.Width(30f), GuiBase.Height(50f), GuiBase.Width(200f), GuiBase.Height(30f));
					if (position.Contains(Event.current.mousePosition) && this.creationCount != App.State.GameSettings.CreationToCreateCount)
					{
						this.creationCount = App.State.GameSettings.CreationToCreateCount;
						foreach (Creation current in App.State.AllCreations)
						{
							current.InitSubItemCost(0);
						}
					}
					App.State.GameSettings.CreationToCreateCount = (int)GUI.HorizontalSlider(position, (float)App.State.GameSettings.CreationToCreateCount, 1f, (float)num2);
					num += 25;
				}
				style.alignment = TextAnchor.UpperCenter;
				style.fontStyle = FontStyle.Bold;
				GUI.Label(new Rect(GuiBase.Width(25f), GuiBase.Height((float)num), GuiBase.Width(200f), GuiBase.Height(30f)), "You are creating:");
				if (App.State.CreatingSpeedBoniDuration > 0L)
				{
					GUI.Label(new Rect(GuiBase.Width(235f), GuiBase.Height((float)num), GuiBase.Width(400f), GuiBase.Height(30f)), "300% creation speed for the next " + Conv.MsToGuiText(App.State.CreatingSpeedBoniDuration, true) + "!");
				}
				style.fontStyle = FontStyle.Normal;
				num += 35;
				Creation activeCreation = App.State.GameSettings.LastCreation;
				if (activeCreation == null)
				{
					activeCreation = App.State.AllCreations.FirstOrDefault((Creation x) => x.IsActive);
				}
				if (activeCreation == null)
				{
					activeCreation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Shadow_clone);
				}
				GuiBase.CreateProgressBar(num, activeCreation.getPercent(), activeCreation.Name, activeCreation.Description, GuiBase.progressBg, GuiBase.progressFgBlue);
				style.alignment = TextAnchor.UpperLeft;
				GUI.Label(new Rect(GuiBase.Width(255f), GuiBase.Height((float)num), GuiBase.Width(200f), GuiBase.Height(30f)), "You have: " + activeCreation.Count.ToGuiText(true));
				string text2 = "Change creation";
				if (this.ChooseCreationIsOpen)
				{
					text2 = "More Info";
				}
				if (GUI.Button(new Rect(GuiBase.Width(485f), GuiBase.Height((float)(num + 10)), GuiBase.Width(140f), GuiBase.Height(30f)), text2))
				{
					this.ChooseCreationIsOpen = !this.ChooseCreationIsOpen;
					if (this.ChooseCreationIsOpen)
					{
						this.scrollPosition = this.scrollPositionAllCreations;
					}
					else
					{
						this.scrollPositionAllCreations = this.scrollPosition;
						this.scrollPosition = Vector2.zero;
					}
				}
				num += 25;
				GUI.Label(new Rect(GuiBase.Width(255f), GuiBase.Height((float)num), GuiBase.Width(250f), GuiBase.Height(30f)), "You created: " + activeCreation.TotalCreated.ToGuiText(true));
				num += 35;
				List<CreationCost> list = CreationCost.RequiredCreations(activeCreation.TypeEnum, 0L, false);
				int num3 = 290;
				if (this.ChooseCreationIsOpen)
				{
					style.fontStyle = FontStyle.Bold;
					GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)num), GuiBase.Width(150f), GuiBase.Height(30f)), "Creation");
					GUI.Label(new Rect(GuiBase.Width(160f), GuiBase.Height((float)num), GuiBase.Width(80f), GuiBase.Height(30f)), "Count", style);
					GUI.Label(new Rect(GuiBase.Width(290f), GuiBase.Height((float)num), GuiBase.Width(80f), GuiBase.Height(30f)), "Next At", style);
					GUI.Label(new Rect(GuiBase.Width(375f), GuiBase.Height((float)num), GuiBase.Width(150f), GuiBase.Height(30f)), "Achieve.", style);
					style.fontStyle = FontStyle.Normal;
					GUI.Label(new Rect(GuiBase.Width(475f), GuiBase.Height((float)num), GuiBase.Width(40f), GuiBase.Height(30f)), new GUIContent("N A", "Off will ignore the next at. 1 will go to the next creation if the current number reached the count. 2 will go to the next creation if you created the count by yourself."), style);
					App.State.GameSettings.CreationsNextAtMode = GUI.Toolbar(new Rect(GuiBase.Width(510f), GuiBase.Height((float)(num - 5)), GuiBase.Width(135f), GuiBase.Height(30f)), App.State.GameSettings.CreationsNextAtMode, this.toolbarNextAt);
					num += 35;
					num3 -= 35;
				}
				this.scrollPosition = GuiBase.TouchScrollView(new Rect(GuiBase.Width(0f), GuiBase.Height((float)num), GuiBase.Width(660f), GuiBase.Height((float)num3)), this.scrollPosition, new Rect(0f, GuiBase.Height((float)num), GuiBase.Width(620f), GuiBase.Height((float)this.svHeight)));
				Achievement first = null;
				if (this.ChooseCreationIsOpen)
				{
					for (int i = 0; i < App.State.AllCreations.Count; i++)
					{
						Creation creation = App.State.AllCreations[i];
						if (creation.IsActive)
						{
							style.fontStyle = FontStyle.Bold;
						}
						GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)num), GuiBase.Width(145f), GuiBase.Height(30f)), new GUIContent(creation.Name, creation.Description), style);
						GUI.Label(new Rect(GuiBase.Width(160f), GuiBase.Height((float)num), GuiBase.Width(150f), GuiBase.Height(35f)), creation.Count.ToGuiText(true), style);
						if (App.CurrentPlattform == Plattform.Android && creation.TypeEnum != Creation.CreationType.Shadow_clone)
						{
							GUIStyle textField2 = Gui.ChosenSkin.textField;
							if (GUI.Button(new Rect(GuiBase.Width(290f), GuiBase.Height((float)num), GuiBase.Width(70f), GuiBase.Height(25f)), creation.NextAtCount + string.Empty, textField2))
							{
								base.ShowNumberInput("Next at for " + creation.Name, creation.NextAtCount, 2147483647, delegate(CDouble x)
								{
									creation.NextAtCount = x.ToInt();
								});
							}
						}
						else if (creation.TypeEnum != Creation.CreationType.Shadow_clone)
						{
							creation.NextAtString = GUI.TextField(new Rect(GuiBase.Width(290f), GuiBase.Height((float)num), GuiBase.Width(70f), GuiBase.Height(25f)), creation.NextAtCount.ToString());
						}
						if (!creation.GodToDefeat.IsDefeated)
						{
							GUI.Label(new Rect(GuiBase.Width(365f), GuiBase.Height((float)num), GuiBase.Width(300f), GuiBase.Height(30f)), "Defeat " + creation.GodToDefeat.Name + " to unlock", style);
						}
						else
						{
							first = App.State.CreatingAchievements.FirstOrDefault((Achievement x) => x.IntEnum == (int)creation.TypeEnum);
							if (first != null)
							{
								this.AddAchievementImg(first, 370, num, 25);
								Achievement achievement = App.State.CreatingAchievements.FirstOrDefault((Achievement x) => x.Id == first.Id + 1);
								this.AddAchievementImg(achievement, 405, num, 25);
								Achievement achievement2 = App.State.CreatingAchievements.FirstOrDefault((Achievement x) => x.Id == first.Id + 2);
								this.AddAchievementImg(achievement2, 440, num, 25);
							}
							if (App.State.IsBuyUnlocked && creation.CanBuy && creation.TypeEnum != Creation.CreationType.Shadow_clone && GUI.Button(new Rect(GuiBase.Width(565f), GuiBase.Height((float)num), GuiBase.Width(60f), GuiBase.Height(25f)), "Buy"))
							{
								this.creationToShow = creation;
							}
							if (GUI.Button(new Rect(GuiBase.Width(475f), GuiBase.Height((float)num), GuiBase.Width(80f), GuiBase.Height(25f)), "Create"))
							{
								bool flag = false;
								foreach (Creation current2 in App.State.AllCreations)
								{
									if (!current2.GodToDefeat.IsDefeated && current2.IsActive)
									{
										flag = true;
									}
								}
								if (App.State.PrinnyBaal.IsFighting)
								{
									flag = true;
								}
								if (flag)
								{
									GuiBase.ShowToast("You can't create something while you fight a god!");
									App.State.GameSettings.LastCreation = creation;
								}
								else
								{
									foreach (Creation current3 in App.State.AllCreations)
									{
										current3.IsActive = false;
									}
									creation.IsActive = true;
									App.State.GameSettings.LastCreation = creation;
									foreach (UltimateBeingV2 current4 in App.State.HomePlanet.UltimateBeingsV2)
									{
										current4.isCreating = false;
									}
								}
							}
						}
						if (creation.IsActive)
						{
							style.fontStyle = FontStyle.Normal;
						}
						num += 30;
					}
				}
				else
				{
					first = App.State.CreatingAchievements.FirstOrDefault((Achievement x) => x.IntEnum == (int)activeCreation.TypeEnum);
					if (first != null)
					{
						GUI.Label(new Rect(GuiBase.Width(35f), GuiBase.Height((float)(num + 10)), GuiBase.Width(200f), GuiBase.Height(50f)), "Achievements for " + activeCreation.Name + ":");
						this.AddAchievementImg(first, 255, num, 40);
						Achievement achievement3 = App.State.CreatingAchievements.FirstOrDefault((Achievement x) => x.Id == first.Id + 1);
						this.AddAchievementImg(achievement3, 315, num, 40);
						Achievement achievement4 = App.State.CreatingAchievements.FirstOrDefault((Achievement x) => x.Id == first.Id + 2);
						this.AddAchievementImg(achievement4, 375, num, 40);
						num += 70;
					}
					if (list.Count > 0)
					{
						style.fontStyle = FontStyle.Bold;
						int num4 = App.State.GameSettings.CreationToCreateCount;
						if (num4 == 0)
						{
							num4 = 1;
						}
						GUI.Label(new Rect(GuiBase.Width(35f), GuiBase.Height((float)num), GuiBase.Width(210f), GuiBase.Height(50f)), string.Concat(new object[]
						{
							"Creations for ",
							num4,
							" x ",
							activeCreation.Name
						}));
						if (activeCreation.Name.Length > 8)
						{
							num += 20;
						}
						style.alignment = TextAnchor.UpperCenter;
						GUI.Label(new Rect(GuiBase.Width(210f), GuiBase.Height((float)num), GuiBase.Width(200f), GuiBase.Height(30f)), " you need");
						GUI.Label(new Rect(GuiBase.Width(335f), GuiBase.Height((float)num), GuiBase.Width(200f), GuiBase.Height(30f)), " you have");
						style.fontStyle = FontStyle.Normal;
						num += 30;
						using (List<CreationCost>.Enumerator enumerator5 = list.GetEnumerator())
						{
							while (enumerator5.MoveNext())
							{
								CreationCost requirement = enumerator5.Current;
								Creation creation3 = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == requirement.TypeEnum);
								GuiBase.CreateProgressBar(num, creation3.getPercent(), EnumName.Name(requirement.TypeEnum), creation3.Description, GuiBase.progressBg, GuiBase.progressFgBlue);
								GUI.Label(new Rect(GuiBase.Width(210f), GuiBase.Height((float)(num + 5)), GuiBase.Width(200f), GuiBase.Height(30f)), requirement.CountNeeded.ToGuiText(true));
								GUI.Label(new Rect(GuiBase.Width(335f), GuiBase.Height((float)(num + 5)), GuiBase.Width(200f), GuiBase.Height(30f)), creation3.Count.ToGuiText(true));
								if (App.State.IsBuyUnlocked && creation3.CanBuy && creation3.TypeEnum != Creation.CreationType.Shadow_clone && GUI.Button(new Rect(GuiBase.Width(545f), GuiBase.Height((float)num), GuiBase.Width(80f), GuiBase.Height(30f)), "Buy"))
								{
									this.creationToShow = creation3;
								}
								num += 35;
							}
						}
						if (activeCreation.SubItemCreationCost.Count > 0)
						{
							num += 10;
							style.alignment = TextAnchor.UpperLeft;
							style.fontStyle = FontStyle.Bold;
							GUI.Label(new Rect(GuiBase.Width(35f), GuiBase.Height((float)num), GuiBase.Width(200f), GuiBase.Height(30f)), new GUIContent("Prerequisite Creations", "All creations needed to create the creations you need to create one." + activeCreation.Name));
							style.alignment = TextAnchor.UpperCenter;
							style.fontStyle = FontStyle.Normal;
							num += 30;
							using (List<CreationCost>.Enumerator enumerator6 = activeCreation.SubItemCreationCost.GetEnumerator())
							{
								while (enumerator6.MoveNext())
								{
									CreationCost requirement = enumerator6.Current;
									Creation creation2 = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == requirement.TypeEnum);
									GuiBase.CreateProgressBar(num, creation2.getPercent(), EnumName.Name(requirement.TypeEnum), creation2.Description, GuiBase.progressBg, GuiBase.progressFgBlue);
									GUI.Label(new Rect(GuiBase.Width(210f), GuiBase.Height((float)(num + 5)), GuiBase.Width(200f), GuiBase.Height(30f)), requirement.CountNeeded.ToGuiText(true));
									GUI.Label(new Rect(GuiBase.Width(335f), GuiBase.Height((float)(num + 5)), GuiBase.Width(200f), GuiBase.Height(30f)), creation2.Count.ToGuiText(true));
									if (App.State.IsBuyUnlocked && creation2.CanBuy && creation2.TypeEnum != Creation.CreationType.Shadow_clone && GUI.Button(new Rect(GuiBase.Width(545f), GuiBase.Height((float)num), GuiBase.Width(80f), GuiBase.Height(30f)), "Buy"))
									{
										this.creationToShow = creation2;
									}
									num += 35;
								}
							}
						}
					}
				}
				this.svHeight = num - 150;
				GUI.EndScrollView();
				GUI.EndGroup();
			}
		}

		private void AddAchievementImg(Achievement achievement, int marginLeft, int marginTop, int width)
		{
			if (achievement == null)
			{
				return;
			}
			if (achievement.Reached)
			{
				GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width((float)width), GuiBase.Height((float)width)), new GUIContent(string.Empty, achievement.ImageReached, achievement.Description));
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width((float)width), GuiBase.Height((float)width)), new GUIContent(string.Empty, achievement.Image, achievement.Description));
			}
		}

		private void ShowBuyCreations()
		{
			GUIStyle style = GUI.skin.GetStyle("TextField");
			GUIStyle style2 = GUI.skin.GetStyle("Label");
			GUIStyle style3 = GUI.skin.GetStyle("Button");
			style.fontSize = GuiBase.FontSize(16);
			style3.fontSize = GuiBase.FontSize(16);
			style.alignment = TextAnchor.MiddleCenter;
			Rect rect = new Rect(GuiBase.Width(365f), GuiBase.Height(360f), GuiBase.Width(500f), GuiBase.Height(150f));
			GUI.Box(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			style2.fontSize = GuiBase.FontSize(16);
			style2.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(rect.xMin, rect.yMin + GuiBase.Height(10f), GuiBase.Width(450f), GuiBase.Height(30f)), new GUIContent(string.Empty, "Hello my dear god. Please click twice on the white box and then put a big number into it.\nOnly numbers though! Then finalize with the 'Buy' button. You can ignore the 'Cancel' button"));
			if (string.IsNullOrEmpty(this.countToBuy))
			{
				this.countToBuy = string.Empty;
			}
			this.countToBuy = GUI.TextField(new Rect(rect.xMin + GuiBase.Width(30f), rect.yMin + GuiBase.Height(45f), GuiBase.Width(270f), GuiBase.Height(30f)), this.countToBuy);
			if (GUI.Button(new Rect(rect.xMin + GuiBase.Width(400f), rect.yMin + GuiBase.Height(45f), GuiBase.Width(75f), GuiBase.Height(30f)), "Cancel"))
			{
				this.creationToShow = null;
				return;
			}
			try
			{
				if (!string.IsNullOrEmpty(this.countToBuy) && this.countToBuy.StartsWith("-"))
				{
					GuiBase.ShowToast("I only sell my creations to you. I won't buy them.");
					this.countToBuy = string.Empty;
					return;
				}
				CDouble cDouble = new CDouble(this.countToBuy);
				cDouble.Round();
				this.countToBuy = cDouble.ToString();
				style2.alignment = TextAnchor.UpperLeft;
				GUI.Label(new Rect(rect.xMin + GuiBase.Width(35f), rect.yMin + GuiBase.Height(85f), GuiBase.Width(450f), GuiBase.Height(30f)), "Divinity cost: " + (this.creationToShow.BuyCost * cDouble).ToGuiText(true));
				GUI.Label(new Rect(rect.xMin + GuiBase.Width(35f), rect.yMin + GuiBase.Height(115f), GuiBase.Width(450f), GuiBase.Height(30f)), "You have: " + App.State.Money.ToGuiText(true) + " divinity");
				GUI.Label(new Rect(rect.xMin + GuiBase.Width(35f), rect.yMin + GuiBase.Height(145f), GuiBase.Width(450f), GuiBase.Height(30f)), "You have: " + this.creationToShow.Count.ToGuiText(true) + " " + this.creationToShow.Name.ToLower());
				style2.alignment = TextAnchor.MiddleCenter;
				if (GUI.Button(new Rect(rect.xMin + GuiBase.Width(315f), rect.yMin + GuiBase.Height(45f), GuiBase.Width(75f), GuiBase.Height(30f)), "Buy"))
				{
					CDouble cDouble2 = this.creationToShow.BuyCost * cDouble;
					App.State.Money.Round();
					cDouble2.Round();
					if (App.State.Money >= cDouble2)
					{
						App.State.Money -= cDouble2;
						this.creationToShow.Count += cDouble;
						this.creationToShow = null;
						App.State.Statistic.TotalMoneySpent += cDouble2;
					}
					else
					{
						GuiBase.ShowToast("Sorry you are way too poor to afford that.\nPlease save up a little more and then come back.");
					}
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				this.countToBuy = string.Empty;
				GuiBase.ShowToast("Only numbers are allowed! Don't ask why. It's just hard to count money with letters or some weird characters.");
			}
			GUI.BeginGroup(new Rect(GuiBase.Width(280f), GuiBase.Height(120f), GuiBase.Width(670f), GuiBase.Height(480f)));
			if (this.creationToShow != null)
			{
				style2.fontStyle = FontStyle.Bold;
				GUI.Label(new Rect(GuiBase.Width(10f), GuiBase.Height(10f), GuiBase.Width(660f), GuiBase.Height(50f)), "I can sell you a " + this.creationToShow.Name + ". How many do you want to buy? \nPress a button or input a number.");
				style2.fontStyle = FontStyle.Normal;
			}
			this.toggleNumber = GUI.Toolbar(new Rect(GuiBase.Width(55f), GuiBase.Height(70f), GuiBase.Width(245f), GuiBase.Height(35f)), this.toggleNumber, this.toolbarLeftStrings);
			int value = 1;
			if (this.toggleNumber == 1)
			{
				value = 2;
			}
			else if (this.toggleNumber == 2)
			{
				value = 5;
			}
			else if (this.toggleNumber == 3)
			{
				value = 25;
			}
			else if (this.toggleNumber == 4)
			{
				value = 75;
			}
			CDouble cDouble3 = new CDouble("1") * value;
			if (GUI.Button(new Rect(GuiBase.Width(55f), GuiBase.Height(120f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble3.ToGuiText(true)))
			{
				this.BuyCount(cDouble3);
			}
			CDouble cDouble4 = new CDouble("10") * value;
			if (GUI.Button(new Rect(GuiBase.Width(200f), GuiBase.Height(120f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble4.ToGuiText(true)))
			{
				this.BuyCount(cDouble4);
			}
			CDouble cDouble5 = new CDouble("100") * value;
			if (GUI.Button(new Rect(GuiBase.Width(345f), GuiBase.Height(120f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble5.ToGuiText(true)))
			{
				this.BuyCount(cDouble5);
			}
			CDouble cDouble6 = new CDouble("1000") * value;
			if (GUI.Button(new Rect(GuiBase.Width(490f), GuiBase.Height(120f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble6.ToGuiText(true)))
			{
				this.BuyCount(cDouble6);
			}
			CDouble cDouble7 = new CDouble("10000") * value;
			if (GUI.Button(new Rect(GuiBase.Width(55f), GuiBase.Height(155f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble7.ToGuiText(true)))
			{
				this.BuyCount(cDouble7);
			}
			CDouble cDouble8 = new CDouble("100000") * value;
			if (GUI.Button(new Rect(GuiBase.Width(200f), GuiBase.Height(155f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble8.ToGuiText(true)))
			{
				this.BuyCount(cDouble8);
			}
			CDouble cDouble9 = new CDouble("1000000") * value;
			if (GUI.Button(new Rect(GuiBase.Width(345f), GuiBase.Height(155f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble9.ToGuiText(true)))
			{
				this.BuyCount(cDouble9);
			}
			CDouble cDouble10 = new CDouble("10000000") * value;
			if (GUI.Button(new Rect(GuiBase.Width(490f), GuiBase.Height(155f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble10.ToGuiText(true)))
			{
				this.BuyCount(cDouble10);
			}
			CDouble cDouble11 = new CDouble("100000000") * value;
			if (GUI.Button(new Rect(GuiBase.Width(55f), GuiBase.Height(190f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble11.ToGuiText(true)))
			{
				this.BuyCount(cDouble11);
			}
			CDouble cDouble12 = new CDouble("1000000000") * value;
			if (GUI.Button(new Rect(GuiBase.Width(200f), GuiBase.Height(190f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble12.ToGuiText(true)))
			{
				this.BuyCount(cDouble12);
			}
			CDouble cDouble13 = new CDouble("10000000000") * value;
			if (GUI.Button(new Rect(GuiBase.Width(345f), GuiBase.Height(190f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble13.ToGuiText(true)))
			{
				this.BuyCount(cDouble13);
			}
			CDouble cDouble14 = new CDouble("100000000000") * value;
			if (GUI.Button(new Rect(GuiBase.Width(490f), GuiBase.Height(190f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble14.ToGuiText(true)))
			{
				this.BuyCount(cDouble14);
			}
			CDouble cDouble15 = new CDouble("1000000000000") * value;
			if (GUI.Button(new Rect(GuiBase.Width(55f), GuiBase.Height(225f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble15.ToGuiText(true)))
			{
				this.BuyCount(cDouble15);
			}
			CDouble cDouble16 = new CDouble("10000000000000") * value;
			if (GUI.Button(new Rect(GuiBase.Width(200f), GuiBase.Height(225f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble16.ToGuiText(true)))
			{
				this.BuyCount(cDouble16);
			}
			CDouble cDouble17 = new CDouble("100000000000000") * value;
			if (GUI.Button(new Rect(GuiBase.Width(345f), GuiBase.Height(225f), GuiBase.Width(140f), GuiBase.Height(25f)), cDouble17.ToGuiText(true)))
			{
				this.BuyCount(cDouble17);
			}
			if (GUI.Button(new Rect(GuiBase.Width(490f), GuiBase.Height(225f), GuiBase.Width(140f), GuiBase.Height(25f)), "MAX"))
			{
				CDouble buyCost = this.creationToShow.BuyCost;
				if (buyCost == 0)
				{
					return;
				}
				CDouble cDouble18 = App.State.Money / this.creationToShow.BuyCost;
				cDouble18.Value = Math.Floor(cDouble18.Value);
				this.BuyCount(cDouble18);
			}
			GUI.EndGroup();
		}

		private void BuyCount(CDouble count)
		{
			this.countToBuy = count.Serialize();
		}

		internal static void ScrollbarToZero()
		{
			CreatingUi.scrollBarsToZero = true;
		}

		private void ShowAutoBuyOptions()
		{
			GUIStyle style = GUI.skin.GetStyle("TextField");
			GUIStyle style2 = GUI.skin.GetStyle("Label");
			GUIStyle style3 = GUI.skin.GetStyle("Button");
			style.fontSize = GuiBase.FontSize(16);
			style3.fontSize = GuiBase.FontSize(16);
			GUI.Box(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			style2.fontSize = GuiBase.FontSize(16);
			style2.alignment = TextAnchor.UpperLeft;
			int num = 330;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height(125f), GuiBase.Width(400f), GuiBase.Height(80f)), "Select which creations you want to autobuy. \nEven if a setting here is on, it won't autobuy them if the main setting on the creations page is off.");
			if (GUI.Button(new Rect(GuiBase.Width(800f), GuiBase.Height(125f), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Back")))
			{
				this.showAutoBuyOptions = false;
			}
			this.scrollPosAutobuy = GuiBase.TouchScrollView(new Rect(GuiBase.Width((float)num), GuiBase.Height(205f), GuiBase.Width(560f), GuiBase.Height(350f)), this.scrollPosAutobuy, new Rect(0f, GuiBase.Height(0f), GuiBase.Width(520f), GuiBase.Height((float)this.marginTop)));
			num = 0;
			this.marginTop = 0;
			foreach (Creation current in App.State.AllCreations)
			{
				if (current.TypeEnum != Creation.CreationType.Shadow_clone)
				{
					GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.marginTop), GuiBase.Width(450f), GuiBase.Height(30f)), current.Name);
					current.AutoBuy = GUI.Toggle(new Rect(GuiBase.Width((float)(num + 170)), GuiBase.Height((float)(this.marginTop + 2)), GuiBase.Width(70f), GuiBase.Height(30f)), current.AutoBuy, new GUIContent(string.Empty));
					this.marginTop += 35;
					if (current.TypeEnum == Creation.CreationType.Village)
					{
						this.marginTop = 0;
						num = 270;
					}
				}
			}
			GUI.EndScrollView();
			style.alignment = TextAnchor.UpperCenter;
		}
	}
}
