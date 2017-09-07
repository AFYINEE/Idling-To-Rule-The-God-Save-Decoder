using Assets.Scripts.Data;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class PetUi : GuiBase
	{
		private enum ValueType
		{
			Count,
			Physical,
			Mystic,
			Battle
		}

		public static PetUi Instance = new PetUi();

		private Texture2D GpImage;

		private Texture2D FreeFood;

		private Texture2D PunyFood;

		private Texture2D StrongFood;

		private Texture2D MightyFood;

		private Texture2D Chocolate;

		private Texture2D PetStones;

		private Texture2D HotSauce;

		private Texture2D SweetSauce;

		private Texture2D SourSauce;

		private Texture2D Mayo;

		private Vector2 scrollPosition = Vector2.zero;

		private int SvHeight;

		private int MarginTop;

		private Pet VisitedPet;

		private List<Pet> AllUnlockedPets = new List<Pet>();

		private List<Pet> AllPets = new List<Pet>();

		private int VisitedIndex;

		private FoodType FoodToBuy;

		private bool RenamePet;

		private bool CreateClones;

		private bool ShowTrades;

		public static int ToolbarInt = 0;

		private string[] toolbarStrings = new string[]
		{
			"Pets",
			"Campaigns"
		};

		private PetCampaign CampaignToStart;

		private List<Pet> AvailablePets = new List<Pet>();

		private int oldDuration = 1;

		public static int DurationHours = 1;

		public static int SelectedGrowth = 0;

		private string[] toolbarSelectedGrowth = new string[]
		{
			"Physical",
			"Mystic",
			"Battle",
			"All"
		};

		private string[] trades = new string[]
		{
			"Strong Food",
			"Mighty Food",
			"Premium"
		};

		private string[] trades2 = new string[]
		{
			"Strong Food",
			"Mighty Food",
			"Premium",
			"Auto Select"
		};

		private int tradeId;

		private string[] FoodNames = new string[]
		{
			"Free Food",
			"Puny Food",
			" Strong Food",
			"Mighty Food",
			"Chocolate"
		};

		private int ToolbarFood;

		private string[] SauceNames = new string[]
		{
			"None",
			"Hot Sauce",
			" Sweet Sauce",
			"Sour Sauce",
			"Mayonaise"
		};

		private int ToolbarSauces;

		private string[] BuyCount = new string[]
		{
			"1",
			"2",
			"5",
			"10",
			"20",
			"50"
		};

		private int ToolbarCount;

		private bool FeedPet;

		private string[] ChangeValue = new string[]
		{
			"1",
			"5",
			"10",
			"50",
			"100",
			"500",
			"1,000",
			"5,000",
			"10,000",
			"50,000",
			"100,000",
			"500,000",
			"1 million",
			"5 million"
		};

		private int ToolbarValue;

		private CDouble count = 0;

		private CDouble physical = 0;

		private CDouble mystic = 0;

		private CDouble battle = 0;

		private CDouble cloneExp = 0;

		public void Init()
		{
			this.GpImage = (Texture2D)Resources.Load("Gui/godpower", typeof(Texture2D));
			this.FreeFood = (Texture2D)Resources.Load("Gui/pets/food_free", typeof(Texture2D));
			this.PunyFood = (Texture2D)Resources.Load("Gui/pets/food_puny", typeof(Texture2D));
			this.StrongFood = (Texture2D)Resources.Load("Gui/pets/food_strong", typeof(Texture2D));
			this.MightyFood = (Texture2D)Resources.Load("Gui/pets/food_mighty", typeof(Texture2D));
			this.Chocolate = (Texture2D)Resources.Load("Gui/pets/food_choco", typeof(Texture2D));
			this.PetStones = (Texture2D)Resources.Load("Gui/pets/pet_stones", typeof(Texture2D));
			this.HotSauce = (Texture2D)Resources.Load("Gui/pets/sauce_hot", typeof(Texture2D));
			this.SweetSauce = (Texture2D)Resources.Load("Gui/pets/sauce_sweet", typeof(Texture2D));
			this.SourSauce = (Texture2D)Resources.Load("Gui/pets/sauce_sour", typeof(Texture2D));
			this.Mayo = (Texture2D)Resources.Load("Gui/pets/sauce_mayo", typeof(Texture2D));
			this.AllUnlockedPets = new List<Pet>();
			this.AllPets = new List<Pet>();
			foreach (Pet current in App.State.Ext.AllPets)
			{
				current.Image = (Texture2D)Resources.Load(current.ImagePath, typeof(Texture2D));
				current.CalculateValues();
				this.AllPets.Add(current);
				if (current.IsUnlocked)
				{
					this.AllUnlockedPets.Add(current);
				}
			}
			this.AllUnlockedPets = (from o in this.AllUnlockedPets
			orderby o.SortValue
			select o).ToList<Pet>();
			this.AllPets = (from o in this.AllPets
			orderby !o.IsUnlocked
			select o).ThenBy((Pet c) => c.SortValue).ToList<Pet>();
			this.VisitedPet = null;
			this.FoodToBuy = FoodType.None;
			this.RenamePet = false;
			this.CreateClones = false;
		}

		public void Show()
		{
			if (App.State == null)
			{
				return;
			}
			GUI.BeginGroup(new Rect(GuiBase.Width(280f), GuiBase.Height(110f), GuiBase.Width(670f), GuiBase.Height(480f)));
			GUI.Box(new Rect(GuiBase.Width(10f), GuiBase.Height(0f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.fontSize = GuiBase.FontSize(16);
			style.alignment = TextAnchor.UpperLeft;
			if (this.ShowTrades)
			{
				this.showTrades();
				GUI.EndGroup();
				return;
			}
			if (this.FoodToBuy != FoodType.None)
			{
				this.showFood();
				GUI.EndGroup();
				return;
			}
			if (this.VisitedPet == null)
			{
				style.alignment = TextAnchor.UpperCenter;
				this.MarginTop = 20;
				int num = 30;
				PetUi.ToolbarInt = GUI.Toolbar(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(360f), GuiBase.Height(25f)), PetUi.ToolbarInt, this.toolbarStrings);
				this.MarginTop += 40;
				this.scrollPosition = GuiBase.TouchScrollView(new Rect(GuiBase.Width(0f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(430f), GuiBase.Height(400f)), this.scrollPosition, new Rect(0f, GuiBase.Height((float)this.MarginTop), GuiBase.Width(400f), GuiBase.Height((float)this.SvHeight)));
				this.SvHeight = 0;
				if (PetUi.ToolbarInt == 0)
				{
					this.ShowPets(num, style);
				}
				else
				{
					this.ShowCampaigns(num, style);
				}
				GUI.EndScrollView();
				style.alignment = TextAnchor.UpperLeft;
				this.MarginTop = 20;
				num = 455;
				if (GUI.Button(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(95f), GuiBase.Height(30f)), new GUIContent("Distribute", "Distributes all your idle clones to your unlocked pets who are not in a campaign. The stats of the clones are the same as they where when you created them in the first place")))
				{
					int num2 = 0;
					foreach (Pet current in App.State.Ext.AllPets)
					{
						if (current.IsUnlocked)
						{
							if (!current.IsInCampaign)
							{
								num2++;
							}
							current.RemoveCloneCount(current.ShadowCloneCount.ToInt());
						}
					}
					if (num2 > 0)
					{
						int num3 = App.State.Clones.IdleClones();
						if (App.State.GameSettings.PetDistribution != 0 && App.State.GameSettings.PetDistribution < num3)
						{
							num3 = App.State.GameSettings.PetDistribution;
						}
						num3 /= num2;
						foreach (Pet current2 in App.State.Ext.AllPets)
						{
							if (current2.IsUnlocked && !current2.IsInCampaign)
							{
								current2.AddCloneCount(num3);
							}
						}
					}
				}
				if (GUI.Button(new Rect(GuiBase.Width((float)(num + 100)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(95f), GuiBase.Height(30f)), new GUIContent("Reclaim", "Reclaims all clones fighting pets and sets them to idle.")))
				{
					foreach (Pet current3 in App.State.Ext.AllPets)
					{
						if (current3.IsUnlocked)
						{
							current3.RemoveCloneCount(current3.ShadowCloneCount.ToInt());
						}
					}
				}
				this.MarginTop += 40;
				if (App.State.PremiumBoni.HasPetHalfStats && GUI.Button(new Rect(GuiBase.Width((float)(num + 100)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(95f), GuiBase.Height(30f)), new GUIContent("Half Stats", "This will add half the stats of your pets (battle for mystic and physical, mystic for battle) to the clones fighting them for all pets.")))
				{
					foreach (Pet current4 in App.State.Ext.AllPets)
					{
						if (current4.IsUnlocked)
						{
							current4.CreateClones(0, current4.Battle / 2, current4.Battle / 2, current4.Mystic / 2);
						}
					}
				}
				if (App.CurrentPlattform == Plattform.Android)
				{
					GUIStyle textField = Gui.ChosenSkin.textField;
					if (GUI.Button(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(95f), GuiBase.Height(25f)), App.State.GameSettings.PetDistribution.ToString(), textField))
					{
						base.ShowNumberInput("Clones to distribute for pets. 0 = all available clones.", App.State.GameSettings.PetDistribution, App.State.Clones.AbsoluteMaximum, delegate(CDouble x)
						{
							App.State.GameSettings.PetDistribution = x.ToInt();
						});
					}
				}
				else
				{
					string value = App.State.GameSettings.PetDistribution.ToString();
					value = GUI.TextField(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(95f), GuiBase.Height(25f)), App.State.GameSettings.PetDistribution.ToString());
					GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(95f), GuiBase.Height(25f)), new GUIContent(string.Empty, "Clones to distribute for pets. 0 = all available clones."));
					int num4 = Conv.StringToInt(value);
					if (num4 >= 0)
					{
						App.State.GameSettings.PetDistribution = num4;
					}
				}
				this.MarginTop += 35;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Pet Stones", "With pet stones you can upgrade puny food to strong food and strong food to mighty food. If you have a lot of them, you can also trade them for a pet token!"));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 100)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), App.State.Ext.PetStones.ToGuiText(true));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 20)), GuiBase.Height((float)(this.MarginTop + 20)), GuiBase.Width(50f), GuiBase.Height(50f)), this.PetStones);
				if (GUI.Button(new Rect(GuiBase.Width((float)(num + 120)), GuiBase.Height((float)(this.MarginTop + 30)), GuiBase.Width(55f), GuiBase.Height(30f)), "Trade"))
				{
					this.ShowTrades = true;
				}
				this.MarginTop += 65;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Puny Food", "Some basic food. Pets don't really like it but they rather eat this than starve to death. Each one costs 1 Baal power or 1 God Power.lowerTextIncreases each of the three growth stats by 2, if the feeding bar is below 10% or by 1, if the feeding bar is between 10% and 50%, or by 0.5, if the feeding bar is above 50%."));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 100)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), App.State.Ext.PunyFood.ToGuiText(true));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 20)), GuiBase.Height((float)(this.MarginTop + 20)), GuiBase.Width(50f), GuiBase.Height(50f)), this.PunyFood);
				if (GUI.Button(new Rect(GuiBase.Width((float)(num + 120)), GuiBase.Height((float)(this.MarginTop + 30)), GuiBase.Width(55f), GuiBase.Height(30f)), "Buy"))
				{
					this.FoodToBuy = FoodType.Puny;
				}
				this.MarginTop += 65;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Strong Food", "The better kind of food. Most pets like it. Each one costs 2 Baal power or 2 God Power.lowerTextIncreases each of the three growth stats by 3.5, if the feeding bar is below 10% or by 2, if the feeding bar is between 10% and 50%, or by 1, if the feeding bar is above 50%."));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 100)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), App.State.Ext.StrongFood.ToGuiText(true));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 20)), GuiBase.Height((float)(this.MarginTop + 20)), GuiBase.Width(50f), GuiBase.Height(50f)), this.StrongFood);
				if (GUI.Button(new Rect(GuiBase.Width((float)(num + 120)), GuiBase.Height((float)(this.MarginTop + 30)), GuiBase.Width(55f), GuiBase.Height(30f)), "Buy"))
				{
					this.FoodToBuy = FoodType.Strong;
				}
				this.MarginTop += 65;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Mighty Food", "Mighty food for mighty pets! Every pet dreams about it. It tastes absolutely mightyfull!. Each one costs 3 Baal power or 3 God Power.lowerTextIncreases each of the three growth stats by 5, if the feeding bar is below 10% or by 3, if the feeding bar is between 10% and 50%, or by 1.5, if the feeding bar is above 50%."));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 100)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), App.State.Ext.MightyFood.ToGuiText(true));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 20)), GuiBase.Height((float)(this.MarginTop + 20)), GuiBase.Width(50f), GuiBase.Height(50f)), this.MightyFood);
				if (GUI.Button(new Rect(GuiBase.Width((float)(num + 120)), GuiBase.Height((float)(this.MarginTop + 30)), GuiBase.Width(55f), GuiBase.Height(30f)), "Buy"))
				{
					this.FoodToBuy = FoodType.Mighty;
				}
				style.fontStyle = FontStyle.Bold;
				this.MarginTop += 40;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)(this.MarginTop + 30)), GuiBase.Width(150f), GuiBase.Height(25f)), new GUIContent("Pet Multiplier", "Your physical, mystic and battle multipliers are multiplied with the pet multiplier.\nEach stat of all pets adds 0.01% to your own stats. With only the mouse at first it might seem little, but later on you will get ten thousands of %!"));
				this.MarginTop += 30;
				if (App.State.Statistic.HasStartedArtyChallenge || App.State.Statistic.HasStartedUltimateBaalChallenge)
				{
					GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)(this.MarginTop + 30)), GuiBase.Width(200f), GuiBase.Height(50f)), "While in UBC or UAC, pets won't give multipier.");
					style.fontStyle = FontStyle.Normal;
				}
				else
				{
					GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)(this.MarginTop + 30)), GuiBase.Width(150f), GuiBase.Height(50f)), "Physical:");
					GUI.Label(new Rect(GuiBase.Width((float)(num + 90)), GuiBase.Height((float)(this.MarginTop + 30)), GuiBase.Width(150f), GuiBase.Height(50f)), new GUIContent(App.State.Multiplier.PetMultiPhysical.ToGuiText(false) + " %", string.Concat(new string[]
					{
						"From pet stats ",
						App.State.Multiplier.PetMultiPhysicalBase.ToGuiText(false),
						" % * ",
						App.State.Multiplier.PetCampainBoost.GuiText,
						"% from campaigns.\nRebirth multi from stats ",
						App.State.Multiplier.PetMultiPhysicalRebirthBase.GuiText,
						"% + ",
						App.State.Multiplier.PetCampainBoostRebirth.GuiText,
						" % from campaigns."
					})));
					this.MarginTop += 25;
					GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)(this.MarginTop + 30)), GuiBase.Width(150f), GuiBase.Height(50f)), "Mystic:");
					GUI.Label(new Rect(GuiBase.Width((float)(num + 90)), GuiBase.Height((float)(this.MarginTop + 30)), GuiBase.Width(150f), GuiBase.Height(50f)), new GUIContent(App.State.Multiplier.PetMultiMystic.ToGuiText(false) + " %", string.Concat(new string[]
					{
						"From pet stats ",
						App.State.Multiplier.PetMultiMysticBase.ToGuiText(false),
						" % * ",
						App.State.Multiplier.PetCampainBoost.GuiText,
						"% from campaigns.\nRebirth multi from stats ",
						App.State.Multiplier.PetMultiMysticRebirthBase.GuiText,
						"% + ",
						App.State.Multiplier.PetCampainBoostRebirth.GuiText,
						" % from campaigns."
					})));
					this.MarginTop += 25;
					GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)(this.MarginTop + 30)), GuiBase.Width(150f), GuiBase.Height(50f)), "Battle:");
					GUI.Label(new Rect(GuiBase.Width((float)(num + 90)), GuiBase.Height((float)(this.MarginTop + 30)), GuiBase.Width(150f), GuiBase.Height(50f)), new GUIContent(App.State.Multiplier.PetMultiBattle.ToGuiText(false) + " %", string.Concat(new string[]
					{
						"From pet stats ",
						App.State.Multiplier.PetMultiBattleBase.ToGuiText(false),
						" % * ",
						App.State.Multiplier.PetCampainBoost.GuiText,
						"% from campaigns.\nRebirth multi from stats ",
						App.State.Multiplier.PetMultiBattleRebirthBase.GuiText,
						"% + ",
						App.State.Multiplier.PetCampainBoostRebirth.GuiText,
						" % from campaigns."
					})));
					style.fontStyle = FontStyle.Normal;
					this.MarginTop += 25;
				}
				GUI.EndGroup();
				return;
			}
			if (this.CreateClones)
			{
				this.createClones();
				GUI.EndGroup();
				return;
			}
			if (this.FeedPet)
			{
				this.feedPet();
				GUI.EndGroup();
				return;
			}
			this.showPet(style);
			GUI.EndGroup();
		}

		public static int GetPetTop(PetType type, int marginTop)
		{
			int num = marginTop;
			if (type == PetType.Mouse)
			{
				num -= 8;
			}
			if (type == PetType.Cupid)
			{
				num += 10;
			}
			if (type == PetType.Squirrel)
			{
				num -= 3;
			}
			if (type == PetType.Rabbit)
			{
				num -= 3;
			}
			if (type == PetType.Cat)
			{
				num = num;
			}
			if (type == PetType.Dog)
			{
				num = num;
			}
			if (type == PetType.Fairy)
			{
				num += 15;
			}
			if (type == PetType.Dragon)
			{
				num = num;
			}
			if (type == PetType.Snake)
			{
				num += 5;
			}
			if (type == PetType.Shark)
			{
				num += 10;
			}
			if (type == PetType.Octopus)
			{
				num = num;
			}
			if (type == PetType.Slime)
			{
				num += 5;
			}
			if (type == PetType.Mole)
			{
				num += 5;
			}
			if (type == PetType.Camel)
			{
				num += 3;
			}
			if (type == PetType.Goat)
			{
				num = num;
			}
			if (type == PetType.Turtle)
			{
				num -= 3;
			}
			if (type == PetType.Doughnut)
			{
				num -= 5;
			}
			if (type == PetType.Eagle)
			{
				num += 20;
			}
			if (type == PetType.Penguin)
			{
				num += 2;
			}
			if (type == PetType.Phoenix)
			{
				num += 8;
			}
			if (type == PetType.Wizard)
			{
				num += 5;
			}
			if (type == PetType.Pegasus)
			{
				num += 7;
			}
			if (type == PetType.Ufo)
			{
				num += 10;
			}
			if (type == PetType.Robot)
			{
				num += 5;
			}
			return num;
		}

		private void ShowPets(int marginLeft, GUIStyle labelStyle)
		{
			foreach (Pet current in this.AllPets)
			{
				GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)PetUi.GetPetTop(current.TypeEnum, this.MarginTop - 20)), GuiBase.Width(60f), GuiBase.Height(60f)), current.Image);
				if (!current.IsUnlocked)
				{
					GUI.Label(new Rect(GuiBase.Width((float)(marginLeft + 70)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent(current.Name, current.Description));
					if (GUI.Button(new Rect(GuiBase.Width((float)(marginLeft + 180)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Unlock"))
					{
						current.Unlock();
					}
				}
				else
				{
					if (current.CurrentHealth <= 0 || current.ShadowCloneCount < 1)
					{
						GuiBase.FontColorProgressbar = Color.red;
					}
					string text = current.Name;
					if (current.IsInCampaign)
					{
						GuiBase.FontColorProgressbar = Color.yellow;
						text += " (C)";
					}
					GuiBase.CreateProgressBar(marginLeft + 70, this.MarginTop, 185f, 31f, current.FullnessPercent.Double, text, current.Description, GuiBase.progressBg, GuiBase.progressFgBlue);
					if (GUI.Button(new Rect(GuiBase.Width((float)(marginLeft + 280)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(23f)), "Visit"))
					{
						this.VisitedPet = current;
						for (int i = 0; i < this.AllUnlockedPets.Count; i++)
						{
							if (this.AllUnlockedPets[i].TypeEnum == this.VisitedPet.TypeEnum)
							{
								this.VisitedIndex = i;
							}
						}
					}
					labelStyle.fontSize = GuiBase.FontSize(12);
					GUI.Label(new Rect(GuiBase.Width((float)(marginLeft + 280)), GuiBase.Height((float)(this.MarginTop + 20)), GuiBase.Width(80f), GuiBase.Height(20f)), new GUIContent(current.ShadowCloneCount.GuiText, "The number of clones who are fighting " + current.Name + "."));
					labelStyle.fontSize = GuiBase.FontSize(16);
					GuiBase.FontColorProgressbar = Color.white;
				}
				this.SvHeight += 50;
				this.MarginTop += 50;
			}
		}

		private void ShowCampaigns(int marginLeft, GUIStyle labelStyle)
		{
			if (this.CampaignToStart != null)
			{
				this.StartCampaign(marginLeft, labelStyle);
			}
			else
			{
				this.MarginTop += 20;
				foreach (PetCampaign current in App.State.Ext.AllCampaigns)
				{
					PetCampaign campaignToChoose = current;
					if (current.PetsInCampaign.Count > 0)
					{
						this.MarginTop += 10;
						for (int i = 0; i < current.PetsInCampaign.Count; i++)
						{
							Pet pet = current.PetsInCampaign[i];
							GUI.Label(new Rect(GuiBase.Width((float)(marginLeft + i * 30)), GuiBase.Height((float)PetUi.GetPetTop(pet.TypeEnum, this.MarginTop - 30)), GuiBase.Width(40f), GuiBase.Height(40f)), new GUIContent(pet.Image, pet.GetCampaignDescription()));
						}
						this.MarginTop += 10;
					}
					Texture2D fgImage = GuiBase.progressFgBlue;
					if (current.PetsInCampaign.Count > 0 && current.CurrentDuration >= current.TotalDuration)
					{
						fgImage = GuiBase.progressFgGreen;
						if (GUI.Button(new Rect(GuiBase.Width((float)(marginLeft + 280)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Result"))
						{
							string textToShow = campaignToChoose.CalculateResult(App.State);
							GuiBase.ShowBigMessage(textToShow);
						}
					}
					else if (current.PetsInCampaign.Count > 0)
					{
						if (GUI.Button(new Rect(GuiBase.Width((float)(marginLeft + 280)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Cancel"))
						{
							GuiBase.ShowDialog("Cancel Campaign", "Do you want to cancel the campaign? This means you won't get any rewards.", delegate
							{
								campaignToChoose.Cancel();
							}, delegate
							{
							}, "Yes", "No", false, false);
						}
					}
					else
					{
						fgImage = GuiBase.progressFgRed;
						if (GUI.Button(new Rect(GuiBase.Width((float)(marginLeft + 280)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Select"))
						{
							PetUi.DurationHours = App.State.GameSettings.LastHoursForCampaigns;
							this.oldDuration = 0;
							if ((campaignToChoose.Type == Campaigns.Divinity || campaignToChoose.Type == Campaigns.GodPower) && App.State.Statistic.HasStartedArtyChallenge)
							{
								GuiBase.ShowToast("Sorry, this campaign is not available in the arty challenge.");
							}
							else
							{
								this.CampaignToStart = campaignToChoose;
								this.AvailablePets = new List<Pet>();
								foreach (Pet current2 in App.State.Ext.AllPets)
								{
									if (current2.IsUnlocked && !current2.IsInCampaign)
									{
										current2.GetCampaignValue(this.CampaignToStart.Type, PetUi.DurationHours, App.State);
										current2.SetCampaignInfo();
										this.AvailablePets.Add(current2);
									}
								}
								if (this.AvailablePets.Count == 0)
								{
									GuiBase.ShowToast("All of your pets are already in a campaign. Please wait until they are back.");
									this.CampaignToStart = null;
								}
								else
								{
									this.AvailablePets = (from x in this.AvailablePets
									orderby x.GetTotalStats().Double descending
									select x).ToList<Pet>();
								}
							}
						}
					}
					GuiBase.CreateProgressBar(marginLeft, this.MarginTop, 185f, 31f, current.CurrentDuration.Double / current.TotalDuration.Double, current.Type.ToString().Replace("GodPower", "God Power"), current.Description, GuiBase.progressBg, fgImage);
					this.MarginTop += 50;
				}
				this.SvHeight = this.MarginTop - 50;
			}
		}

		private void StartCampaign(int marginLeft, GUIStyle labelStyle)
		{
			labelStyle.alignment = TextAnchor.UpperLeft;
			this.MarginTop = this.MarginTop;
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)this.MarginTop), GuiBase.Width(360f), GuiBase.Height(60f)), new GUIContent(this.CampaignToStart.Type.ToString() + " Campaign"));
			this.MarginTop += 35;
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)this.MarginTop), GuiBase.Width(360f), GuiBase.Height(120f)), new GUIContent(this.CampaignToStart.Description));
			this.MarginTop += 100;
			App.State.GameSettings.LastHoursForCampaigns = (int)GUI.HorizontalSlider(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(30f)), (float)App.State.GameSettings.LastHoursForCampaigns, 1f, 12f);
			PetUi.DurationHours = App.State.GameSettings.LastHoursForCampaigns;
			if (PetUi.DurationHours <= 0)
			{
				PetUi.DurationHours = 1;
			}
			this.MarginTop += 25;
			string arg = " hour";
			if (PetUi.DurationHours > 1)
			{
				arg = " hours";
			}
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)this.MarginTop), GuiBase.Width(160f), GuiBase.Height(60f)), new GUIContent("Duration: " + PetUi.DurationHours + arg, "The longer the duration, the more rewards you might get."));
			if (App.State.PremiumBoni.HasAutoSelectPets)
			{
				if (GUI.Button(new Rect(GuiBase.Width((float)(marginLeft + 180)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), new GUIContent("Select", "Autoselects pets for this campaign. This selects all available pets (up to 10) and favours pets who are good at this campaign.For the growth campaign it also selects the pet with the lowest growth.")))
				{
					this.AutoSelect();
				}
				if (GUI.Button(new Rect(GuiBase.Width((float)(marginLeft + 280)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), new GUIContent("Auto", "Autoselects pets for this campaign, and starts it. This selects all available pets (up to 10) and favours pets who are good at this campaign.For the growth campaign it also selects the pet with the lowest growth.")))
				{
					this.AutoStart();
				}
			}
			if (this.CampaignToStart == null)
			{
				return;
			}
			if (this.CampaignToStart.Type == Campaigns.Growth)
			{
				this.MarginTop += 40;
				GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)this.MarginTop), GuiBase.Width(160f), GuiBase.Height(60f)), new GUIContent("Growth gain", "After the campaign, your weakest pet will receive growth. Please choose which one should be gained."));
				this.MarginTop += 30;
				App.State.GameSettings.LastSelectedGrowth = (Growth)GUI.Toolbar(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)this.MarginTop), GuiBase.Width(360f), GuiBase.Height(25f)), (int)App.State.GameSettings.LastSelectedGrowth, this.toolbarSelectedGrowth);
				this.CampaignToStart.SelectedGrowth = App.State.GameSettings.LastSelectedGrowth;
			}
			this.MarginTop += 40;
			string text = "Please select at least one of your pets.";
			if (this.CampaignToStart != null && this.CampaignToStart.Type == Campaigns.Growth)
			{
				text = "Please select at least two of your pets.";
			}
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)this.MarginTop), GuiBase.Width(360f), GuiBase.Height(30f)), new GUIContent(text));
			this.MarginTop += 65;
			int num = 0;
			foreach (Pet current in this.AvailablePets)
			{
				Pet pet = current;
				bool flag = num % 2 == 1;
				int num2 = marginLeft;
				if (flag)
				{
					num2 += 200;
				}
				string str = string.Empty;
				if (string.IsNullOrEmpty(current.DescGrowthCampaign) || this.oldDuration != PetUi.DurationHours)
				{
					current.GetCampaignValue(this.CampaignToStart.Type, PetUi.DurationHours, App.State);
				}
				str = current.GetResultDescForCampagin(this.CampaignToStart.Type);
				GUI.Label(new Rect(GuiBase.Width((float)num2), GuiBase.Height((float)(PetUi.GetPetTop(current.TypeEnum, this.MarginTop) - 20)), GuiBase.Width(60f), GuiBase.Height(60f)), new GUIContent(current.Image, current.GetCampaignDescription() + "\n" + str));
				if (current.IsInCampaign)
				{
					if (GUI.Button(new Rect(GuiBase.Width((float)(num2 + 80)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Remove"))
					{
						pet.IsInCampaign = false;
					}
				}
				else if (GUI.Button(new Rect(GuiBase.Width((float)(num2 + 80)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Add"))
				{
					pet.IsInCampaign = true;
				}
				if (flag)
				{
					this.MarginTop += 55;
				}
				num++;
			}
			this.oldDuration = PetUi.DurationHours;
			if (this.AvailablePets.Count % 2 == 1)
			{
				this.MarginTop += 55;
			}
			if (GUI.Button(new Rect(GuiBase.Width((float)(marginLeft + 160)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Cancel"))
			{
				foreach (Pet current2 in this.AvailablePets)
				{
					current2.IsInCampaign = false;
				}
				this.CampaignToStart = null;
				this.AvailablePets = new List<Pet>();
			}
			if (GUI.Button(new Rect(GuiBase.Width((float)(marginLeft + 280)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Start"))
			{
				this.StartCampaign();
			}
			this.SvHeight = this.MarginTop;
		}

		private void AutoSelect()
		{
			int num = 0;
			foreach (Pet current in this.AvailablePets)
			{
				current.IsInCampaign = false;
			}
			if (this.CampaignToStart.Type == Campaigns.Growth)
			{
				CDouble cDouble = 0;
				Pet pet = null;
				foreach (Pet current2 in this.AvailablePets)
				{
					if (cDouble == 0 || current2.GetTotalGrowth() < cDouble)
					{
						cDouble = current2.GetTotalGrowth();
						pet = current2;
					}
				}
				num++;
				pet.IsInCampaign = true;
			}
			foreach (Pet current3 in this.AvailablePets)
			{
				List<CampaignBoost> campaignBoost = current3.GetCampaignBoost();
				foreach (CampaignBoost current4 in campaignBoost)
				{
					if (current4.Type == this.CampaignToStart.Type && current4.Value > 0 && num < 10 && !current3.IsInCampaign)
					{
						num++;
						current3.IsInCampaign = true;
					}
				}
			}
			foreach (Pet current5 in this.AvailablePets)
			{
				List<CampaignBoost> campaignBoost2 = current5.GetCampaignBoost();
				bool flag = false;
				foreach (CampaignBoost current6 in campaignBoost2)
				{
					if (current6.Type == this.CampaignToStart.Type && current6.Value < 0)
					{
						flag = true;
					}
				}
				if (!flag && num < 10 && !current5.IsInCampaign)
				{
					num++;
					current5.IsInCampaign = true;
				}
			}
			foreach (Pet current7 in this.AvailablePets)
			{
				if (num < 10 && !current7.IsInCampaign)
				{
					num++;
					current7.IsInCampaign = true;
				}
			}
		}

		private void AutoStart()
		{
			this.AutoSelect();
			this.StartCampaign();
		}

		private void StartCampaign()
		{
			List<Pet> list = new List<Pet>();
			foreach (Pet current in this.AvailablePets)
			{
				if (current.IsInCampaign)
				{
					list.Add(current);
				}
			}
			if (list.Count == 0)
			{
				GuiBase.ShowToast("Please select at least one pet!");
			}
			else if (list.Count < 2 && this.CampaignToStart.Type == Campaigns.Growth)
			{
				GuiBase.ShowToast("For the growth campaign, you need at least two pets!");
				App.State.GameSettings.LastSelectedGrowth = this.CampaignToStart.SelectedGrowth;
			}
			else if (list.Count > 10)
			{
				GuiBase.ShowToast("You can't take more than 10 pets into one campaign!");
			}
			else
			{
				App.State.GameSettings.LastHoursForCampaigns = PetUi.DurationHours;
				this.CampaignToStart.Start(App.State, PetUi.DurationHours, list);
				this.CampaignToStart = null;
				this.AvailablePets = new List<Pet>();
			}
		}

		private void showTrades()
		{
			string[] texts = this.trades;
			if (!App.State.PremiumBoni.HasAutoSelectPets)
			{
				texts = this.trades2;
			}
			this.MarginTop = 30;
			int num = 40;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(550f), GuiBase.Height(50f)), "What do you want to trade?");
			this.MarginTop += 60;
			this.tradeId = GUI.Toolbar(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(450f), GuiBase.Height(30f)), this.tradeId, texts);
			this.MarginTop += 60;
			num += 5;
			GameState state = App.State;
			if (this.tradeId == 0)
			{
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(50f)), "2 x ");
				GUI.Label(new Rect(GuiBase.Width((float)(num + 35)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.PunyFood);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 110)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(50f)), "+ 50 x ");
				GUI.Label(new Rect(GuiBase.Width((float)(num + 160)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.PetStones);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 220)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(50f)), " = ");
				GUI.Label(new Rect(GuiBase.Width((float)(num + 260)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.StrongFood);
				CDouble cDouble = 2;
				CDouble cDouble2 = 50;
				CDouble cDouble3 = 1;
				CDouble cDouble4 = 2;
				CDouble cDouble5 = 50;
				CDouble cDouble6 = 1;
				bool flag = false;
				this.MarginTop += 60;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(150f), GuiBase.Height(50f)), "You have " + state.Ext.PetStones.GuiText);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 120)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.PetStones);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 180)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(50f)), " and " + state.Ext.PunyFood.GuiText);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 250)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.PunyFood);
				this.MarginTop += 10;
				for (int i = 0; i < 5; i++)
				{
					int value = 2 * i;
					if (i == 3)
					{
						value = 9;
					}
					if (i == 4)
					{
						int num2 = (state.Ext.PetStones / cDouble2).Floor().ToInt();
						int num3 = (state.Ext.PunyFood / cDouble).Floor().ToInt();
						if (num2 < num3)
						{
							num3 = num2;
						}
						value = num3 - 1;
					}
					cDouble4 = cDouble + value * cDouble;
					cDouble5 = cDouble2 + value * cDouble2;
					cDouble6 = cDouble3 + value * cDouble3;
					if (state.Ext.PunyFood >= cDouble4 && state.Ext.PetStones >= cDouble5)
					{
						flag = true;
						this.MarginTop += 40;
						GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(450f), GuiBase.Height(50f)), string.Concat(new object[]
						{
							"Trade ",
							cDouble4.GuiText,
							" puny food and ",
							cDouble5,
							" pet stones for ",
							cDouble6,
							" strong food?"
						}));
						if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Trade"))
						{
							GuiBase.ShowToast(string.Concat(new object[]
							{
								"You traded ",
								cDouble4.GuiText,
								" puny food and ",
								cDouble5.GuiText,
								" pet stones for ",
								cDouble6,
								" strong food!"
							}));
							state.Ext.PetStones -= cDouble5;
							state.Ext.PunyFood -= cDouble4;
							state.Ext.StrongFood += cDouble6;
							this.ShowTrades = false;
						}
					}
				}
				if (!flag)
				{
					this.MarginTop += 60;
					GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(450f), GuiBase.Height(50f)), "You don't have enough puny food or pet stones for the trade...");
				}
				this.MarginTop += 40;
			}
			else if (this.tradeId == 1)
			{
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(50f)), "2 x ");
				GUI.Label(new Rect(GuiBase.Width((float)(num + 35)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.StrongFood);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 110)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(50f)), "+ 50 x ");
				GUI.Label(new Rect(GuiBase.Width((float)(num + 160)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.PetStones);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 220)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(50f)), " = ");
				GUI.Label(new Rect(GuiBase.Width((float)(num + 260)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.MightyFood);
				CDouble cDouble7 = 2;
				CDouble cDouble8 = 50;
				CDouble cDouble9 = 1;
				CDouble cDouble10 = 2;
				CDouble cDouble11 = 50;
				CDouble cDouble12 = 1;
				bool flag2 = false;
				this.MarginTop += 60;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(150f), GuiBase.Height(50f)), "You have " + state.Ext.PetStones.GuiText);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 120)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.PetStones);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 180)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(50f)), " and " + state.Ext.StrongFood.GuiText);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 250)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.StrongFood);
				this.MarginTop += 10;
				for (int j = 0; j < 5; j++)
				{
					int value2 = 2 * j;
					if (j == 3)
					{
						value2 = 9;
					}
					if (j == 4)
					{
						int num4 = (state.Ext.PetStones / cDouble8).Floor().ToInt();
						int num5 = (state.Ext.StrongFood / cDouble7).Floor().ToInt();
						if (num4 < num5)
						{
							num5 = num4;
						}
						value2 = num5 - 1;
					}
					cDouble10 = cDouble7 + value2 * cDouble7;
					cDouble11 = cDouble8 + value2 * cDouble8;
					cDouble12 = cDouble9 + value2 * cDouble9;
					if (state.Ext.StrongFood >= cDouble10 && state.Ext.PetStones >= cDouble11)
					{
						flag2 = true;
						this.MarginTop += 40;
						GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(450f), GuiBase.Height(50f)), string.Concat(new object[]
						{
							"Trade ",
							cDouble10.GuiText,
							" strong food and ",
							cDouble11,
							" pet stones for ",
							cDouble12,
							" mighty food?"
						}));
						if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Trade"))
						{
							GuiBase.ShowToast(string.Concat(new object[]
							{
								"You traded ",
								cDouble10.GuiText,
								" strong food and ",
								cDouble11.GuiText,
								" pet stones for ",
								cDouble12,
								" mighty food!"
							}));
							state.Ext.PetStones -= cDouble11;
							state.Ext.StrongFood -= cDouble10;
							state.Ext.MightyFood += cDouble12;
							this.ShowTrades = false;
						}
					}
				}
				if (!flag2)
				{
					this.MarginTop += 60;
					GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(450f), GuiBase.Height(50f)), "You don't have enough strong food or pet stones for the trade...");
				}
				this.MarginTop += 40;
			}
			else if (this.tradeId == 2)
			{
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(150f), GuiBase.Height(50f)), "You have " + state.Ext.PetStones.GuiText);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 140)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.PetStones);
				this.MarginTop += 60;
				CDouble cDouble13 = 300000;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(150f), GuiBase.Height(50f)), "Trade " + cDouble13.CommaFormatted);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 115)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.PetStones);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 175)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(450f), GuiBase.Height(50f)), " for one pet token?");
				if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Trade"))
				{
					if (state.Ext.PetStones > cDouble13)
					{
						GuiBase.ShowToast("You traded " + cDouble13.GuiText + " pet stones for one pet token!");
						state.Ext.PetStonesSpent += cDouble13;
						state.Ext.PetStones -= cDouble13;
						Premium expr_DEB = state.PremiumBoni;
						expr_DEB.PetToken = ++expr_DEB.PetToken;
						this.ShowTrades = false;
					}
					else
					{
						GuiBase.ShowToast("You don't have enough pet stones!");
					}
				}
				if (App.State.IsCrystalFactoryAvailable && !state.PremiumBoni.HasCrystalImprovement)
				{
					this.MarginTop += 60;
					GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(150f), GuiBase.Height(50f)), "Trade " + cDouble13.CommaFormatted);
					GUI.Label(new Rect(GuiBase.Width((float)(num + 115)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.PetStones);
					GUI.Label(new Rect(GuiBase.Width((float)(num + 175)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(450f), GuiBase.Height(50f)), " for improved crystal upgrade?");
					GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)(this.MarginTop + 22)), GuiBase.Width(500f), GuiBase.Height(50f)), "This is the same as the one you can purchase. You can't get both.");
					if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Trade"))
					{
						if (state.Ext.PetStones > cDouble13)
						{
							GuiBase.ShowToast("You traded " + cDouble13.GuiText + " pet stones for 'Improved Crystal Upgrade'!");
							state.Ext.PetStonesSpent += cDouble13;
							state.Ext.PetStones -= cDouble13;
							state.PremiumBoni.HasCrystalImprovement = true;
							this.ShowTrades = false;
						}
						else
						{
							GuiBase.ShowToast("You don't have enough pet stones!");
						}
					}
				}
				if (App.State.IsCrystalFactoryAvailable && state.PremiumBoni.MaxCrystals < 6)
				{
					CDouble cDouble14 = 250000;
					this.MarginTop += 60;
					GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(150f), GuiBase.Height(50f)), "Trade " + cDouble14.CommaFormatted);
					GUI.Label(new Rect(GuiBase.Width((float)(num + 115)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.PetStones);
					GUI.Label(new Rect(GuiBase.Width((float)(num + 175)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(450f), GuiBase.Height(50f)), " for one crystal slot?");
					GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)(this.MarginTop + 22)), GuiBase.Width(500f), GuiBase.Height(50f)), "The max slots are 6. You currently have " + App.State.PremiumBoni.MaxCrystals.GuiText + " slots.");
					if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Trade"))
					{
						if (state.Ext.PetStones > cDouble14)
						{
							GuiBase.ShowToast("You traded " + cDouble14.GuiText + " pet stones for one crystal slot!");
							state.Ext.PetStonesSpent += cDouble14;
							state.Ext.PetStones -= cDouble14;
							Premium expr_11F2 = state.PremiumBoni;
							expr_11F2.MaxCrystals = ++expr_11F2.MaxCrystals;
							this.ShowTrades = false;
						}
						else
						{
							GuiBase.ShowToast("You don't have enough pet stones!");
						}
					}
				}
				this.MarginTop += 60;
			}
			else if (this.tradeId == 3)
			{
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(150f), GuiBase.Height(50f)), "You have " + state.Ext.PetStones.GuiText);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 140)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.PetStones);
				this.MarginTop += 60;
				CDouble cDouble15 = 5000;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(150f), GuiBase.Height(50f)), "You need " + cDouble15.CommaFormatted);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 140)), GuiBase.Height((float)(this.MarginTop - 10)), GuiBase.Width(100f), GuiBase.Height(50f)), this.PetStones);
				GUI.Label(new Rect(GuiBase.Width((float)(num + 200)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(250f), GuiBase.Height(30f)), " for the auto select-button.");
				this.MarginTop += 30;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(550f), GuiBase.Height(50f)), "The auto select-button is a convenient way to send pets on campaigns. It auto selects up to 10 pets and favors their strenghts.");
				if (state.Ext.PetStones > cDouble15)
				{
					this.MarginTop += 60;
					GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(450f), GuiBase.Height(50f)), "Trade " + cDouble15.GuiText + " pet stones for the auto button?");
					if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Trade"))
					{
						GuiBase.ShowToast("You traded " + cDouble15.GuiText + " pet stones for the auto button!");
						state.Ext.PetStonesSpent += cDouble15;
						state.Ext.PetStones -= cDouble15;
						state.PremiumBoni.HasAutoSelectPets = true;
						this.ShowTrades = false;
					}
				}
				this.MarginTop += 60;
			}
			if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height(150f), GuiBase.Width(80f), GuiBase.Height(30f)), "Cancel"))
			{
				this.ShowTrades = false;
			}
		}

		private void showFood()
		{
			this.MarginTop = 30;
			int num = 40;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(550f), GuiBase.Height(50f)), "How many " + this.FoodNames[(int)this.FoodToBuy] + " do you want to buy?\nBe careful, you will lose your food after rebirthing!");
			this.MarginTop += 60;
			this.ToolbarCount = GUI.Toolbar(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(400f), GuiBase.Height(30f)), this.ToolbarCount, this.BuyCount);
			int num2 = 0;
			CDouble cDouble = 0;
			int.TryParse(this.BuyCount[this.ToolbarCount], out num2);
			cDouble = num2 * (int)this.FoodToBuy;
			if (num2 == 5)
			{
				cDouble *= 0.949;
			}
			if (num2 == 10)
			{
				cDouble *= 0.899;
			}
			if (num2 == 20)
			{
				cDouble *= 0.849;
			}
			if (num2 == 50)
			{
				cDouble *= 0.799;
			}
			CDouble cDouble2 = cDouble * 0.75;
			if (cDouble2 < 1)
			{
				cDouble2 = 1;
			}
			this.MarginTop += 50;
			GUI.Label(new Rect(GuiBase.Width((float)(num + 40)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(50f)), string.Concat(new object[]
			{
				"Baal Power Cost: ",
				cDouble.ToNextInt(),
				"\nYou have: ",
				App.State.HomePlanet.BaalPower,
				string.Empty
			}));
			if (GUI.Button(new Rect(GuiBase.Width((float)(num + 220)), GuiBase.Height((float)(this.MarginTop + 5)), GuiBase.Width(80f), GuiBase.Height(30f)), "Buy"))
			{
				if (App.State.HomePlanet.BaalPower >= cDouble.ToNextInt())
				{
					this.buyFood(this.FoodToBuy, num2, cDouble.ToNextInt(), true);
				}
				else
				{
					GuiBase.ShowToast("You don't have enough Baal Power!");
				}
			}
			this.MarginTop += 50;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)(this.MarginTop + 5)), GuiBase.Width(35f), GuiBase.Height(35f)), this.GpImage);
			GUI.Label(new Rect(GuiBase.Width((float)(num + 40)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(50f)), string.Concat(new object[]
			{
				"God Power Cost: ",
				cDouble2.ToNextInt(),
				"\nYou have: ",
				App.State.PremiumBoni.GodPower,
				string.Empty
			}));
			if (GUI.Button(new Rect(GuiBase.Width((float)(num + 220)), GuiBase.Height((float)(this.MarginTop + 5)), GuiBase.Width(80f), GuiBase.Height(30f)), "Buy"))
			{
				if (App.State.PremiumBoni.GodPower >= cDouble2.ToNextInt())
				{
					this.buyFood(this.FoodToBuy, num2, cDouble2.ToNextInt(), false);
				}
				else
				{
					GuiBase.ShowToast("You don't have enough God Power!");
				}
			}
			if (GUI.Button(new Rect(GuiBase.Width(400f), GuiBase.Height((float)(this.MarginTop + 5)), GuiBase.Width(80f), GuiBase.Height(30f)), "Cancel"))
			{
				this.FoodToBuy = FoodType.None;
			}
		}

		private void buyFood(FoodType type, int count, CDouble cost, bool isBaalPower)
		{
			string text = this.FoodNames[(int)this.FoodToBuy];
			string text2 = " God";
			if (isBaalPower)
			{
				text2 = " Baal";
			}
			GuiBase.ShowDialog("Buy " + text, string.Concat(new object[]
			{
				"Do you want to spend ",
				cost.ToInt(),
				text2,
				" power to buy ",
				count,
				" x ",
				text
			}), delegate
			{
				int num = cost.ToInt();
				if (isBaalPower && App.State.HomePlanet.BaalPower < num)
				{
					GuiBase.ShowToast("You don't have enough Baal Power!");
				}
				else if (!isBaalPower && App.State.PremiumBoni.GodPower < num)
				{
					GuiBase.ShowToast("You don't have enough God Power!");
				}
				else
				{
					if (isBaalPower)
					{
						App.State.HomePlanet.BaalPower -= num;
					}
					else
					{
						GodPowerUi.SpendGodPower(num);
					}
					if (type == FoodType.Puny)
					{
						App.State.Ext.PunyFood += count;
					}
					else if (type == FoodType.Strong)
					{
						App.State.Ext.StrongFood += count;
					}
					else if (type == FoodType.Mighty)
					{
						App.State.Ext.MightyFood += count;
					}
					this.FoodToBuy = FoodType.None;
				}
			}, delegate
			{
			}, "Yes", "No", false, false);
		}

		private void feedPet()
		{
			this.MarginTop = 65;
			int num = 40;
			this.ToolbarFood = GUI.Toolbar(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(600f), GuiBase.Height(30f)), this.ToolbarFood, this.FoodNames);
			string text = this.FoodNames[this.ToolbarFood];
			FoodType toolbarFood = (FoodType)this.ToolbarFood;
			int num2 = 0;
			Texture2D image = this.FreeFood;
			if (toolbarFood == FoodType.Puny)
			{
				image = this.PunyFood;
				num2 = App.State.Ext.PunyFood.ToInt();
			}
			else if (toolbarFood == FoodType.Strong)
			{
				image = this.StrongFood;
				num2 = App.State.Ext.StrongFood.ToInt();
			}
			else if (toolbarFood == FoodType.Mighty)
			{
				image = this.MightyFood;
				num2 = App.State.Ext.MightyFood.ToInt();
			}
			else if (toolbarFood == FoodType.Chocolate)
			{
				image = this.Chocolate;
				num2 = App.State.Ext.Chocolate.ToInt();
			}
			string text2 = "Which food do you want to feed?";
			if (this.ToolbarFood > 0)
			{
				text2 = string.Concat(new object[]
				{
					text2,
					" (You have ",
					num2,
					" x ",
					text,
					".)"
				});
			}
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height(30f), GuiBase.Width(550f), GuiBase.Height(50f)), text2);
			this.MarginTop += 45;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(350f), GuiBase.Height(50f)), "Which sauce do you want to use?");
			this.MarginTop += 35;
			this.ToolbarSauces = GUI.Toolbar(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(600f), GuiBase.Height(30f)), this.ToolbarSauces, this.SauceNames);
			SauceType toolbarSauces = (SauceType)this.ToolbarSauces;
			Texture2D image2 = null;
			if (toolbarSauces == SauceType.Hot)
			{
				image2 = this.HotSauce;
			}
			else if (toolbarSauces == SauceType.Sweet)
			{
				image2 = this.SweetSauce;
			}
			else if (toolbarSauces == SauceType.Sour)
			{
				image2 = this.SourSauce;
			}
			else if (toolbarSauces == SauceType.Mayonaise)
			{
				image2 = this.Mayo;
			}
			this.MarginTop += 40;
			GUI.Label(new Rect(GuiBase.Width(420f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(100f)), image2);
			GUI.Label(new Rect(GuiBase.Width(500f), GuiBase.Height((float)(this.MarginTop + 30)), GuiBase.Width(100f), GuiBase.Height(100f)), image);
			this.MarginTop += 40;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(380f), GuiBase.Height(75f)), this.VisitedPet.GetFoodDescription(toolbarFood, toolbarSauces));
			this.MarginTop += 90;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)(this.MarginTop + 3)), GuiBase.Width(550f), GuiBase.Height(25f)), "Currently the feeding bar is at: " + (this.VisitedPet.FullnessPercent * 100).GuiText + " %");
			if (GUI.Button(new Rect(GuiBase.Width((float)(num + 350)), GuiBase.Height((float)(this.MarginTop + 5)), GuiBase.Width(80f), GuiBase.Height(30f)), "Feed"))
			{
				if (this.VisitedPet.Feed(toolbarFood, toolbarSauces, true))
				{
					this.FeedPet = false;
				}
				else
				{
					GuiBase.ShowToast("You don't have enough " + text + "!");
				}
			}
			if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)(this.MarginTop + 5)), GuiBase.Width(80f), GuiBase.Height(30f)), "Cancel"))
			{
				this.FeedPet = false;
			}
			this.MarginTop += 40;
			if (GUI.Button(new Rect(GuiBase.Width((float)(num + 350)), GuiBase.Height((float)(this.MarginTop + 5)), GuiBase.Width(80f), GuiBase.Height(30f)), new GUIContent("Feed All", "This will feed all unlocked pets who are not in a campaign with the selected food and sauce. If you don't have enough of the selected food, free food will be chosen.")))
			{
				this.FeedAll(toolbarFood, toolbarSauces, text, false);
			}
			if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)(this.MarginTop + 5)), GuiBase.Width(80f), GuiBase.Height(30f)), new GUIContent("Feed All", "This will feed all unlocked pets who are not in a campaign with the selected food and sauce. If you don't have enough of the selected food, nothing will be fed.")))
			{
				this.FeedAll(toolbarFood, toolbarSauces, text, true);
			}
		}

		private void FeedAll(FoodType food, SauceType sauce, string foodName, bool onlyCurrentFood = false)
		{
			int num = 0;
			int num2 = 0;
			foreach (Pet current in App.State.Ext.AllPets)
			{
				if (current.IsUnlocked && current.FullnessPercent < 75 && !current.IsInCampaign)
				{
					if (!current.Feed(food, sauce, false))
					{
						if (!onlyCurrentFood)
						{
							current.Feed(FoodType.None, sauce, true);
						}
						num2++;
					}
					else
					{
						num++;
					}
				}
			}
			string text = string.Concat(new object[]
			{
				"You successfully fed ",
				num,
				" of your pets with ",
				foodName,
				"!"
			});
			if (num2 > 0 && !onlyCurrentFood)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\n",
					num2,
					" of your pets were fed with free food."
				});
			}
			GuiBase.ShowToast(text);
			this.FeedPet = false;
		}

		private void browseRenamePets(GUIStyle labelStyle)
		{
			if (this.AllUnlockedPets.Count > 2)
			{
				int num = this.VisitedIndex;
				if (num > 0)
				{
					num--;
				}
				else
				{
					num = this.AllUnlockedPets.Count - 1;
				}
				Pet pet = this.AllUnlockedPets[num];
				if (GUI.Button(new Rect(GuiBase.Width(250f), GuiBase.Height(20f), GuiBase.Width(130f), GuiBase.Height(30f)), pet.Name))
				{
					this.VisitedIndex = num;
					this.VisitedPet = this.AllUnlockedPets[num];
				}
			}
			if (this.AllUnlockedPets.Count > 1)
			{
				int num2 = this.VisitedIndex;
				if (num2 < this.AllUnlockedPets.Count - 1)
				{
					num2++;
				}
				else
				{
					num2 = 0;
				}
				Pet pet2 = this.AllUnlockedPets[num2];
				if (GUI.Button(new Rect(GuiBase.Width(400f), GuiBase.Height(20f), GuiBase.Width(130f), GuiBase.Height(30f)), pet2.Name))
				{
					this.VisitedIndex = num2;
					this.VisitedPet = this.AllUnlockedPets[num2];
				}
			}
			this.MarginTop = 18;
			string text = "Rename";
			if (this.RenamePet)
			{
				text = "Save";
				this.VisitedPet.Name = GUI.TextField(new Rect(GuiBase.Width(45f), GuiBase.Height((float)(this.MarginTop + 78)), GuiBase.Width(150f), GuiBase.Height(25f)), this.VisitedPet.Name);
				if (this.VisitedPet.Name.Length > 15)
				{
					this.VisitedPet.Name = this.VisitedPet.Name.Substring(0, 15);
				}
			}
			else
			{
				labelStyle.fontStyle = FontStyle.Bold;
				labelStyle.fontSize = GuiBase.FontSize(18);
				GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height((float)(this.MarginTop + 75)), GuiBase.Width(150f), GuiBase.Height(30f)), this.VisitedPet.Name);
				labelStyle.fontStyle = FontStyle.Normal;
				labelStyle.fontSize = GuiBase.FontSize(16);
			}
			if (GUI.Button(new Rect(GuiBase.Width(40f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(120f), GuiBase.Height(30f)), text))
			{
				this.RenamePet = !this.RenamePet;
			}
		}

		private void showPet(GUIStyle labelStyle)
		{
			if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height(20f), GuiBase.Width(80f), GuiBase.Height(30f)), "Back"))
			{
				this.VisitedPet = null;
				return;
			}
			this.browseRenamePets(labelStyle);
			this.MarginTop = 95;
			labelStyle.alignment = TextAnchor.UpperCenter;
			GuiBase.CreateProgressBar(225, this.MarginTop, 185f, 31f, this.VisitedPet.FullnessPercent.Double, "Hunger", string.Concat(new string[]
			{
				"You can feed ",
				this.VisitedPet.Name,
				", if the feeding bar is at below 50% for a reduced growth gain or if the bar is below 10% for the full gain. \nIf the bar is empty, ",
				this.VisitedPet.Name,
				" will lose all it's hp and won't contribute to your stats anymore!lowerTextCurrent %: ",
				(this.VisitedPet.FullnessPercent * 100).ToGuiText(false)
			}), GuiBase.progressBg, GuiBase.progressFgBlue);
			this.MarginTop += 35;
			GuiBase.CreateProgressBar(225, this.MarginTop, 185f, 31f, this.VisitedPet.Exp.Double / this.VisitedPet.ExpNeeded.Double, "Experience", "Current Exp: " + this.VisitedPet.Exp.GuiText + "\nExp to next Level: " + this.VisitedPet.ExpNeeded.GuiText, GuiBase.progressBg, GuiBase.progressFgGreen);
			if (!this.VisitedPet.IsInCampaign && GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height((float)(this.MarginTop + 1)), GuiBase.Width(80f), GuiBase.Height(30f)), "Feed"))
			{
				if (this.VisitedPet.CanFeed)
				{
					this.FeedPet = true;
				}
				else
				{
					GuiBase.ShowToast("The feeding bar has to be below 75% until you can feed " + this.VisitedPet.Name + "!");
				}
			}
			GUI.Label(new Rect(GuiBase.Width(240f), GuiBase.Height((float)(this.MarginTop + 35)), GuiBase.Width(150f), GuiBase.Height(150f)), this.VisitedPet.Image);
			labelStyle.fontSize = GuiBase.FontSize(14);
			labelStyle.alignment = TextAnchor.UpperLeft;
			this.MarginTop = 140;
			GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), new GUIContent("Level: ", "Every time " + this.VisitedPet.Name + " levels up, physical, mystic and battle will increase depending on their growth."));
			GUI.Label(new Rect(GuiBase.Width(140f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), this.VisitedPet.Level.ToGuiText(true));
			this.MarginTop += 30;
			GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(25f)), new GUIContent("Physical: ", "Each physical increases 10 Hp for " + this.VisitedPet.Name + " and adds 0.01 to the pet physical multiplier."));
			GUI.Label(new Rect(GuiBase.Width(140f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), this.VisitedPet.Physical.GuiText);
			this.MarginTop += 25;
			GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(25f)), new GUIContent("Mystic: ", "Each mystic increases defense by 0.5 and the hp recover for " + this.VisitedPet.Name + " and adds 0.01 to the pet mystic multiplier."));
			GUI.Label(new Rect(GuiBase.Width(140f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), this.VisitedPet.Mystic.GuiText);
			this.MarginTop += 25;
			GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(25f)), new GUIContent("Battle: ", "Each battle increases 1 attack for " + this.VisitedPet.Name + " and adds 0.01 to the pet battle multiplier."));
			GUI.Label(new Rect(GuiBase.Width(140f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), this.VisitedPet.Battle.GuiText);
			this.MarginTop += 40;
			GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), new GUIContent("Total growth: ", "The combined growth of  " + this.VisitedPet.Name + ". The combined growth of all pets together is a highscore in the statistics page.\nThe stats of a pet are: growth + (Level - 1) * 0.1 * growth. Every 100 levels the increase rises 0.1 * growth up to a total increase of 1 * growth."));
			GUI.Label(new Rect(GuiBase.Width(140f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), (this.VisitedPet.PhysicalGrowth + this.VisitedPet.MysticGrowth + this.VisitedPet.BattleGrowth).ToGuiText(false));
			this.MarginTop += 30;
			GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(25f)), new GUIContent("P growth: "));
			if (this.VisitedPet.PhysicalGrowth > 1000 && this.VisitedPet.PhysicalGrowth != this.VisitedPet.PhysicalGrowth.Rounded)
			{
				GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(25f)), new GUIContent("P growth: ", "Exact value: " + this.VisitedPet.PhysicalGrowth.Value));
			}
			GUI.Label(new Rect(GuiBase.Width(140f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), this.VisitedPet.PhysicalGrowth.ToGuiText(false));
			this.MarginTop += 25;
			GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(25f)), new GUIContent("M growth: "));
			if (this.VisitedPet.MysticGrowth > 1000 && this.VisitedPet.MysticGrowth != this.VisitedPet.MysticGrowth.Rounded)
			{
				GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(25f)), new GUIContent("M growth: ", "Exact value: " + this.VisitedPet.MysticGrowth.Value));
			}
			GUI.Label(new Rect(GuiBase.Width(140f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), this.VisitedPet.MysticGrowth.ToGuiText(false));
			this.MarginTop += 25;
			GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(25f)), new GUIContent("B growth: "));
			if (this.VisitedPet.BattleGrowth > 1000 && this.VisitedPet.BattleGrowth != this.VisitedPet.BattleGrowth.Rounded)
			{
				GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(25f)), new GUIContent("B growth: ", "Exact value: " + this.VisitedPet.BattleGrowth.Value));
			}
			GUI.Label(new Rect(GuiBase.Width(140f), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), this.VisitedPet.BattleGrowth.ToGuiText(false));
			this.MarginTop = 160;
			int num = 440;
			if (this.VisitedPet.IsInCampaign)
			{
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(100f)), new GUIContent(this.VisitedPet.Name + " is currently in a campaign. It can't eat or fight clones."));
			}
			else
			{
				if (App.State.PremiumBoni.PetToken > 0 && GUI.Button(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)(this.MarginTop + 10)), GuiBase.Width(120f), GuiBase.Height(30f)), new GUIContent("Use Token", "You can use a pet token to increase the physical growth, mystic growth and battle growth by 100 each.")))
				{
					GuiBase.ShowDialog("Use 1 Pet Token", "Do you want to use 1 pet token to increase physical growth, mystic growth and battle growth by 100 each?", delegate
					{
						Premium expr_0A = App.State.PremiumBoni;
						expr_0A.PetToken = --expr_0A.PetToken;
						this.VisitedPet.PhysicalGrowth += 100;
						this.VisitedPet.MysticGrowth += 100;
						this.VisitedPet.BattleGrowth += 100;
						this.VisitedPet.CalculateValues();
						GuiBase.ShowToast("Physical, mystic and battle growth for " + this.VisitedPet.Name + " is increased by 100!");
					}, delegate
					{
					}, "Yes", "No", false, false);
				}
				this.MarginTop += 50;
				if (GUI.Button(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(120f), GuiBase.Height(30f)), "Create Clones"))
				{
					this.physical = this.VisitedPet.ClonePhysical;
					this.mystic = this.VisitedPet.CloneMystic;
					this.battle = this.VisitedPet.CloneBattle;
					this.cloneExp = Pet.CalcCloneExp(this.physical, this.mystic, this.battle);
					this.CreateClones = true;
					return;
				}
				this.MarginTop += 60;
				labelStyle.fontSize = GuiBase.FontSize(16);
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(150f), GuiBase.Height(25f)), new GUIContent("Custom Clones", "The stats of the clones " + this.VisitedPet.Name + " will fight."));
				this.MarginTop += 35;
				labelStyle.fontSize = GuiBase.FontSize(14);
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), new GUIContent("Count: ", "The number of clones are left to fight your pet. If there are zero, the pet will do nothing."));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 90)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(100f), GuiBase.Height(25f)), this.VisitedPet.ShadowCloneCount.ToGuiText(true));
				this.MarginTop += 25;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(130f), GuiBase.Height(25f)), new GUIContent("Experience: ", "The amount of experience " + this.VisitedPet.Name + " will receive, for each clone defeated"));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 90)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(112000f), GuiBase.Height(25f)), this.VisitedPet.CloneExp.GuiText);
				this.MarginTop += 25;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(100f)), new GUIContent("Physical: "));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 90)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(130f), GuiBase.Height(25f)), this.VisitedPet.ClonePhysical.GuiText);
				this.MarginTop += 25;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(100f)), new GUIContent("Mystic: "));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 90)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(130f), GuiBase.Height(25f)), this.VisitedPet.CloneMystic.GuiText);
				this.MarginTop += 25;
				GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(100f)), new GUIContent("Battle: "));
				GUI.Label(new Rect(GuiBase.Width((float)(num + 90)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(130f), GuiBase.Height(25f)), this.VisitedPet.CloneBattle.GuiText);
			}
			labelStyle.fontSize = GuiBase.FontSize(16);
			labelStyle.alignment = TextAnchor.UpperCenter;
			this.MarginTop = 325;
			num = 225;
			string text = string.Empty;
			if (this.VisitedPet.CurrentHealth <= 0)
			{
				text = "\nTime until recovery from zero health: " + Conv.MsToGuiText(this.VisitedPet.ZeroHealthTime, true);
			}
			GuiBase.CreateProgressBar(num, this.MarginTop, 185f, 31f, this.VisitedPet.getPercentOfHP(), "HP " + this.VisitedPet.CurrentHealth.GuiText, string.Concat(new string[]
			{
				"Your health of ",
				this.VisitedPet.Name,
				". If the health reaches zero, ",
				this.VisitedPet.Name,
				" will need 1 minute to recover from that.lowerTextMax HP: ",
				this.VisitedPet.MaxHealth.ToGuiText(true),
				"\nHP Recover / s: ",
				this.VisitedPet.HpRecoverSec.ToGuiText(true),
				text
			}), GuiBase.progressBg, GuiBase.progressFgRed);
			this.MarginTop += 40;
			labelStyle.fontSize = GuiBase.FontSize(30);
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(185f), GuiBase.Height(100f)), new GUIContent("VS"));
			labelStyle.fontSize = GuiBase.FontSize(16);
			this.MarginTop += 50;
			GuiBase.CreateProgressBar(num, this.MarginTop, 185f, 31f, this.VisitedPet.getPercentOfHPClone(), "HP " + this.VisitedPet.CloneCurrentHealth.GuiText, string.Concat(new string[]
			{
				"The health of the Clone ",
				this.VisitedPet.Name,
				" fights against. lowerTextMax HP: ",
				this.VisitedPet.CloneMaxHealth.ToGuiText(true),
				"\nHP Recover / s: ",
				this.VisitedPet.HpRecoverSecClone.ToGuiText(true)
			}), GuiBase.progressBg, GuiBase.progressFgRed);
		}

		private void createClones()
		{
			if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height(20f), GuiBase.Width(80f), GuiBase.Height(30f)), "Back"))
			{
				this.CreateClones = false;
				return;
			}
			int num = 40;
			this.MarginTop = 25;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(500f), GuiBase.Height(100f)), new GUIContent("Create Shadow Clones with stats slightly lower than " + this.VisitedPet.Name + ".\nThen it will fight the Shadow Clones to gain experience and level up."));
			this.MarginTop += 60;
			this.ToolbarValue = GUI.SelectionGrid(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(590f), GuiBase.Height(65f)), this.ToolbarValue, this.ChangeValue, 7);
			this.MarginTop += 40;
			if (this.count == 0)
			{
				this.count = this.VisitedPet.ShadowCloneCount;
			}
			int value = 0;
			int.TryParse(this.ChangeValue[this.ToolbarValue].Replace(",", string.Empty).Replace(" million", "000000"), out value);
			this.MarginTop += 50;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(100f)), new GUIContent("Count: "));
			string cou = this.addTextField(num + 100, this.MarginTop, "Number of clones to create", 0, this.count, PetUi.ValueType.Count);
			this.addButtons(this.count, value, delegate(CDouble newValue)
			{
				cou = newValue.NumberText;
			});
			this.MarginTop += 40;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(100f)), new GUIContent("Physical: "));
			string phys = this.addTextField(num + 100, this.MarginTop, "Value for Physical of the clone", 0, this.physical, PetUi.ValueType.Physical);
			this.addButtons(this.physical, value, delegate(CDouble newValue)
			{
				phys = newValue.NumberText;
			});
			this.MarginTop += 40;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(100f)), new GUIContent("Mystic: "));
			string mys = this.addTextField(num + 100, this.MarginTop, "Value for Mystic of the clone", 0, this.mystic, PetUi.ValueType.Mystic);
			this.addButtons(this.mystic, value, delegate(CDouble newValue)
			{
				mys = newValue.NumberText;
			});
			this.MarginTop += 40;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(100f)), new GUIContent("Battle: "));
			string bat = this.addTextField(num + 100, this.MarginTop, "Value for Battle of the clone", 0, this.battle, PetUi.ValueType.Battle);
			this.addButtons(this.battle, value, delegate(CDouble newValue)
			{
				bat = newValue.NumberText;
			});
			this.count = Conv.StringToDouble(cou, true, false);
			this.physical = Conv.StringToDouble(phys, true, false);
			this.mystic = Conv.StringToDouble(mys, true, false);
			this.battle = Conv.StringToDouble(bat, true, false);
			if (this.count > App.State.Clones.IdleClones() + this.VisitedPet.ShadowCloneCount)
			{
				this.count = App.State.Clones.IdleClones() + this.VisitedPet.ShadowCloneCount;
				GuiBase.ShowToast("You can't use more clones than your idle clones!");
			}
			if (this.physical > App.State.CloneMaxHealth / 10)
			{
				this.physical = App.State.CloneMaxHealth / 10;
				GuiBase.ShowToast("The value can't be higher than the the physical of your normal shadow clones!");
			}
			if (this.mystic > App.State.CloneDefense)
			{
				this.mystic = App.State.CloneDefense;
				GuiBase.ShowToast("The value can't be higher than the the mystic of your normal shadow clones!");
			}
			if (this.battle > App.State.CloneAttack)
			{
				this.battle = App.State.CloneAttack;
				GuiBase.ShowToast("The value can't be higher than the the battle of your normal shadow clones!");
			}
			this.cloneExp = Pet.CalcCloneExp(this.physical, this.mystic, this.battle);
			this.MarginTop += 50;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(100f)), new GUIContent("Experience: "));
			GUI.Label(new Rect(GuiBase.Width((float)(num + 100)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(100f)), this.cloneExp.GuiText);
			this.MarginTop += 50;
			GUI.Label(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(100f)), new GUIContent("Pet Stats"));
			GUI.Label(new Rect(GuiBase.Width((float)(num + 100)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(100f)), "Physical:\nMystic:\nBattle:");
			GUI.Label(new Rect(GuiBase.Width((float)(num + 200)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(200f), GuiBase.Height(100f)), string.Concat(new string[]
			{
				this.VisitedPet.Physical.GuiText,
				"\n",
				this.VisitedPet.Mystic.GuiText,
				"\n",
				this.VisitedPet.Battle.GuiText
			}));
			if (GUI.Button(new Rect(GuiBase.Width((float)(num + 320)), GuiBase.Height((float)(this.MarginTop - 45)), GuiBase.Width(120f), GuiBase.Height(30f)), new GUIContent("Create Clones", "This will create new clones with the given stats and replaces all clones currently fighting the pet.\nYou need to have enough idle clone to be able to do that!")))
			{
				if (this.count > App.State.Clones.IdleClones() + this.VisitedPet.ShadowCloneCount)
				{
					this.count = App.State.Clones.IdleClones() + this.VisitedPet.ShadowCloneCount;
				}
				this.VisitedPet.CreateClones(this.count, this.physical, this.mystic, this.battle);
				this.CreateClones = false;
				return;
			}
			if (GUI.Button(new Rect(GuiBase.Width((float)(num + 470)), GuiBase.Height((float)(this.MarginTop - 45)), GuiBase.Width(120f), GuiBase.Height(30f)), new GUIContent("Stats for all", "Adds the same clone stats to all Pets you own.")))
			{
				foreach (Pet current in App.State.Ext.AllPets)
				{
					if (current.IsUnlocked)
					{
						current.CreateClones(0, this.physical, this.mystic, this.battle);
					}
				}
				this.CreateClones = false;
				return;
			}
			if (App.State.PremiumBoni.HasPetHalfStats)
			{
				if (GUI.Button(new Rect(GuiBase.Width((float)(num + 320)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(120f), GuiBase.Height(30f)), new GUIContent("Half stats", "Adds half of your pets stats (battle for mystic and physical, mystic for battle) as clone stats.")))
				{
					this.VisitedPet.CreateClones(0, this.VisitedPet.Battle / 2, this.VisitedPet.Battle / 2, this.VisitedPet.Mystic / 2);
					this.CreateClones = false;
					return;
				}
				if (GUI.Button(new Rect(GuiBase.Width((float)(num + 470)), GuiBase.Height((float)this.MarginTop), GuiBase.Width(120f), GuiBase.Height(30f)), new GUIContent("Half for all", "Adds half of your pets stats as clone stats (battle for mystic and physical, mystic for battle) for all pets.")))
				{
					foreach (Pet current2 in App.State.Ext.AllPets)
					{
						if (current2.IsUnlocked)
						{
							current2.CreateClones(0, current2.Battle / 2, current2.Battle / 2, current2.Mystic / 2);
						}
					}
					this.CreateClones = false;
					return;
				}
			}
		}

		private string addTextField(int marginLeft, int marginTop, string text, int maxCount, CDouble number, PetUi.ValueType type)
		{
			if (App.CurrentPlattform == Plattform.Android)
			{
				GUIStyle textField = Gui.ChosenSkin.textField;
				if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(140f), GuiBase.Height(25f)), number + string.Empty, textField))
				{
					base.ShowNumberInput(text, number, maxCount, delegate(CDouble x)
					{
						if (type == PetUi.ValueType.Count)
						{
							this.count = x;
						}
						if (type == PetUi.ValueType.Physical)
						{
							this.physical = x;
						}
						if (type == PetUi.ValueType.Battle)
						{
							this.battle = x;
						}
						if (type == PetUi.ValueType.Mystic)
						{
							this.mystic = x;
						}
					});
				}
				return number.NumberText;
			}
			return GUI.TextField(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(140f), GuiBase.Height(25f)), number.NumberText);
		}

		private void addButtons(CDouble valueToChange, CDouble value, Action<CDouble> onPressed)
		{
			int num = 300;
			if (GUI.Button(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(40f), GuiBase.Height(30f)), new GUIContent("+")))
			{
				valueToChange += value;
				onPressed(valueToChange);
			}
			num += 60;
			if (GUI.Button(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(40f), GuiBase.Height(30f)), new GUIContent("-")))
			{
				valueToChange -= value;
				if (valueToChange < 1)
				{
					valueToChange = 1;
				}
				onPressed(valueToChange);
			}
			num += 60;
			if (GUI.Button(new Rect(GuiBase.Width((float)num), GuiBase.Height((float)this.MarginTop), GuiBase.Width(50f), GuiBase.Height(30f)), new GUIContent("Set")))
			{
				valueToChange = value;
				onPressed(value);
			}
		}
	}
}
