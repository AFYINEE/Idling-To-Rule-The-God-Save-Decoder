using Assets.Scripts.Data;
using Assets.Scripts.Helper;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class DivinityGeneratorUi : GuiBase
	{
		public static DivinityGeneratorUi Instance = new DivinityGeneratorUi();

		public static Vector2 ScrollPosition = Vector2.zero;

		protected int scrollViewHeight;

		public static bool AddIsOpen = false;

		private string cloneCountString = "999999999999999";

		private long secLeftTillEmpty;

		private int count = 30;

		protected long ClonesToAdd
		{
			get
			{
				if (!DivinityGeneratorUi.AddIsOpen)
				{
					return (long)App.State.GameSettings.ClonesToAddCount;
				}
				return App.State.GameSettings.DivGenCreatiosToAdd;
			}
		}

		protected string CloneCountString
		{
			get
			{
				if (!DivinityGeneratorUi.AddIsOpen)
				{
					return AreaRightUi.CloneCountString;
				}
				return this.cloneCountString;
			}
			set
			{
				try
				{
					if (!DivinityGeneratorUi.AddIsOpen)
					{
						AreaRightUi.CloneCountString = value;
					}
					else
					{
						if (!string.IsNullOrEmpty(value) && value.StartsWith("-"))
						{
							value = string.Empty;
						}
						long num = 0L;
						long.TryParse(value, out num);
						this.cloneCountString = num.ToString();
						if (num < 9223372036854775807L)
						{
							App.State.GameSettings.DivGenCreatiosToAdd = num;
						}
						else
						{
							this.cloneCountString = 9223372036854775807L.ToString();
							App.State.GameSettings.DivGenCreatiosToAdd = 9223372036854775807L;
						}
					}
				}
				catch (Exception)
				{
					this.cloneCountString = string.Empty;
				}
			}
		}

		protected void SetScrollbarPosition(Vector2 position)
		{
			DivinityGeneratorUi.ScrollPosition = position;
		}

		private void SetCountAfterButton(int count)
		{
			long num;
			if (Event.current.button == 1)
			{
				num = this.ClonesToAdd - (long)count;
			}
			else
			{
				num = (long)count;
			}
			if (num <= 0L)
			{
				num = 1L;
			}
			this.CloneCountString = num.ToString();
		}

		public void Show()
		{
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.fontSize = GuiBase.FontSize(16);
			if (!App.State.Generator.IsAvailable)
			{
				GUI.Box(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
				style.alignment = TextAnchor.MiddleCenter;
				GUI.Label(new Rect(GuiBase.Width(310f), GuiBase.Height(150f), GuiBase.Width(640f), GuiBase.Height(30f)), "You need to build a Temple of God to unlock the divinity generator.", style);
				return;
			}
			if (DivinityGeneratorUi.AddIsOpen)
			{
				this.scrollViewHeight = (int)GuiBase.Height(1000f);
			}
			else
			{
				this.scrollViewHeight = 0;
			}
			GUI.BeginGroup(new Rect(GuiBase.Width(280f), GuiBase.Height(110f), GuiBase.Width(670f), GuiBase.Height(480f)));
			GUI.Box(new Rect(GuiBase.Width(10f), GuiBase.Height(0f), GuiBase.Width(660f), GuiBase.Height(80f)), string.Empty);
			style.alignment = TextAnchor.UpperLeft;
			GUIStyle style2 = GUI.skin.GetStyle("TextField");
			style2.fontSize = GuiBase.FontSize(14);
			style2.alignment = TextAnchor.MiddleCenter;
			GUIContent content = new GUIContent("Clones to add / remove", "Click on the buttons for standard values or in the input field, then input the number of clones each click on + / - should add / remove.");
			if (DivinityGeneratorUi.AddIsOpen)
			{
				content = new GUIContent("Creations to add", "The number of creations you want to sacrifice and generate divinity out of it.");
			}
			GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height(11f), GuiBase.Width(285f), GuiBase.Height(30f)), content);
			int num = 210;
			int num2 = 65;
			if (DivinityGeneratorUi.AddIsOpen)
			{
				num = 170;
				num2 = 105;
			}
			if (App.CurrentPlattform == Plattform.Android)
			{
				GUIStyle textField = Gui.ChosenSkin.textField;
				if (GUI.Button(new Rect(GuiBase.Width((float)num), GuiBase.Height(10f), GuiBase.Width((float)num2), GuiBase.Height(25f)), this.CloneCountString, textField))
				{
					base.ShowNumberInput("Clones to add / remove", App.State.GameSettings.ClonesToAddCount, 9223372036854775807L, delegate(CDouble x)
					{
						this.CloneCountString = x.ToString();
					});
				}
			}
			else
			{
				this.CloneCountString = GUI.TextField(new Rect(GuiBase.Width((float)num), GuiBase.Height(10f), GuiBase.Width((float)num2), GuiBase.Height(25f)), this.CloneCountString);
			}
			if (GUI.Button(new Rect(GuiBase.Width(290f), GuiBase.Height(10f), GuiBase.Width(45f), GuiBase.Height(25f)), "1"))
			{
				this.SetCountAfterButton(1);
			}
			if (GUI.Button(new Rect(GuiBase.Width(340f), GuiBase.Height(10f), GuiBase.Width(45f), GuiBase.Height(25f)), "10"))
			{
				this.SetCountAfterButton(10);
			}
			if (GUI.Button(new Rect(GuiBase.Width(390f), GuiBase.Height(10f), GuiBase.Width(45f), GuiBase.Height(25f)), "100"))
			{
				this.SetCountAfterButton(100);
			}
			if (GUI.Button(new Rect(GuiBase.Width(440f), GuiBase.Height(10f), GuiBase.Width(45f), GuiBase.Height(25f)), "1K"))
			{
				this.SetCountAfterButton(1000);
			}
			if (GUI.Button(new Rect(GuiBase.Width(490f), GuiBase.Height(10f), GuiBase.Width(45f), GuiBase.Height(25f)), "10K"))
			{
				this.SetCountAfterButton(10000);
			}
			if (GUI.Button(new Rect(GuiBase.Width(540f), GuiBase.Height(10f), GuiBase.Width(50f), GuiBase.Height(25f)), "100K"))
			{
				this.SetCountAfterButton(100000);
			}
			if (GUI.Button(new Rect(GuiBase.Width(595f), GuiBase.Height(10f), GuiBase.Width(53f), GuiBase.Height(25f)), "MAX"))
			{
				if (!DivinityGeneratorUi.AddIsOpen)
				{
					this.CloneCountString = App.State.Clones.MaxShadowClones.ToString();
				}
				else
				{
					this.CloneCountString = "999999999999999";
				}
			}
			GUI.Box(new Rect(GuiBase.Width(10f), GuiBase.Height(85f), GuiBase.Width(660f), GuiBase.Height(395f)), string.Empty);
			int num3 = 90;
			this.ShowLabels(num3, style);
			style.fontStyle = FontStyle.Normal;
			num3 += 45;
			if (DivinityGeneratorUi.AddIsOpen)
			{
				num3 += 70;
				DivinityGeneratorUi.ScrollPosition = GuiBase.TouchScrollView(new Rect(GuiBase.Width(0f), GuiBase.Height((float)num3), GuiBase.Width(650f), GuiBase.Height(255f)), DivinityGeneratorUi.ScrollPosition, new Rect(0f, GuiBase.Height((float)num3), GuiBase.Width(620f), GuiBase.Height((float)this.scrollViewHeight)));
			}
			this.ShowScrollViewElements(num3, style);
			if (DivinityGeneratorUi.AddIsOpen)
			{
				GUI.EndScrollView();
			}
			GUI.EndGroup();
		}

		protected void ShowLabels(int marginTop, GUIStyle labelStyle)
		{
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
				GUI.Label(new Rect(GuiBase.Width(365f), GuiBase.Height(42f), GuiBase.Width(220f), GuiBase.Height(30f)), new GUIContent("Auto buy missing creations", text));
				App.State.GameSettings.AutoBuyCreationsForDivGen = GUI.Toggle(new Rect(GuiBase.Width(593f), GuiBase.Height(47f), GuiBase.Width(300f), GuiBase.Height(30f)), App.State.GameSettings.AutoBuyCreationsForDivGen, new GUIContent(string.Empty));
			}
			if (App.State.Generator.IsBuilt)
			{
				labelStyle.fontSize = GuiBase.FontSize(18);
				labelStyle.fontStyle = FontStyle.Bold;
				labelStyle.alignment = TextAnchor.MiddleCenter;
				GUI.Label(new Rect(GuiBase.Width(15f), GuiBase.Height((float)marginTop), GuiBase.Width(650f), GuiBase.Height(30f)), new GUIContent("Divinity Generator", "The divinity generator can convert your creations into divinity. You need to fill in creations for it to work."));
				marginTop += 45;
				labelStyle.fontSize = GuiBase.FontSize(16);
				labelStyle.fontStyle = FontStyle.Normal;
				labelStyle.alignment = TextAnchor.UpperLeft;
				GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(250f), GuiBase.Height(30f)), new GUIContent("Capacity / In use", "The total capacity and the capacity in use. The base will increase for each god defeated after Zeus (stays after rebirth)."));
				GUI.Label(new Rect(GuiBase.Width(200f), GuiBase.Height((float)marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), App.State.Generator.Capacity.ToGuiText(true) + " / " + App.State.Generator.FilledCapacity.ToGuiText(true));
				GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height(42f), GuiBase.Width(200f), GuiBase.Height(30f)), new GUIContent("Stop after finish", "If this is on, clones will be removed from generator upgrade when it is finished."));
				App.State.GameSettings.StopDivinityGenBuilding = GUI.Toggle(new Rect(GuiBase.Width(200f), GuiBase.Height(47f), GuiBase.Width(300f), GuiBase.Height(30f)), App.State.GameSettings.StopDivinityGenBuilding, new GUIContent(string.Empty));
				if (!DivinityGeneratorUi.AddIsOpen)
				{
					if (GUI.Button(new Rect(GuiBase.Width(540f), GuiBase.Height((float)marginTop), GuiBase.Width(50f), GuiBase.Height(30f)), new GUIContent("Add", "Add your creations to use them up for more divinity.")))
					{
						DivinityGeneratorUi.AddIsOpen = true;
					}
					marginTop += 25;
					GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), new GUIContent("Time till empty", "It takes that long until the capacity in use is 0. It does not take the worker clones into consideration!"));
					CDouble convertSec = App.State.Generator.ConvertSec;
					if (this.count == 30)
					{
						this.count = 0;
						this.secLeftTillEmpty = (long)(App.State.Generator.FilledCapacity * 1000 / App.State.Generator.ConvertSec).ToInt();
					}
					this.count++;
					GUI.Label(new Rect(GuiBase.Width(200f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), Conv.MsToGuiText(this.secLeftTillEmpty, true));
					marginTop += 25;
					GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), new GUIContent("Divinity each convert", "The number from Convert will be multiplied with this number for the divinity gain."));
					GUI.Label(new Rect(GuiBase.Width(200f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), App.State.Generator.DivinityEachCapacity.ToString());
					marginTop += 25;
					GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), new GUIContent("Converts / s", "Each second this number will be converted into divinity. The base will increase for each god defeated after Zeus (stays after rebirth)."));
					GUI.Label(new Rect(GuiBase.Width(200f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), convertSec.ToGuiText(true));
					marginTop += 25;
					GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), new GUIContent("Divinity / s", "The amount of divinity the generator will generate each second."));
					string text2 = App.State.Generator.DivinitySecWithWorker.ToGuiText(true);
					if (App.State.PremiumBoni.CrystalBonusDivinity > 0)
					{
						text2 = text2 + " (" + App.State.Generator.DivinitySecWithWorkerCrystal.GuiText + ")";
					}
					string tooltip = string.Empty;
					if (App.State.PremiumBoni.CrystalBonusDivinity > 0)
					{
						tooltip = App.State.PremiumBoni.CrystalBonusDivinity.GuiText + "% more income from equipped crystals";
					}
					GUI.Label(new Rect(GuiBase.Width(200f), GuiBase.Height((float)marginTop), GuiBase.Width(340f), GuiBase.Height(30f)), new GUIContent(text2, tooltip));
					marginTop += 25;
					GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), new GUIContent("Worker Clones", "You can adjust some clones to auto-fill your generator. They can only use stones to refill the generator, so be sure to have enough! (It is hard to carry things like light or oceans for them). One shadow clone can fill in " + App.State.Generator.CloneFillSpeedText + "\nIf you allocate more clones than the break even point, the leftover clones will fill the capacity of the divinity generator. If it is full, they will increase the divinity gain by 1% every 5000 clones (maxed at 400% additional bonus)."));
					GUI.Label(new Rect(GuiBase.Width(200f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), string.Empty + App.State.Generator.ShadowCloneCount.CommaFormatted);
					if (GUI.Button(new Rect(GuiBase.Width(470f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+"))
					{
						App.State.Generator.AddCloneCount((int)this.ClonesToAdd);
					}
					if (GUI.Button(new Rect(GuiBase.Width(530f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "-"))
					{
						App.State.Generator.RemoveCloneCount((int)this.ClonesToAdd);
					}
					if (GUI.Button(new Rect(GuiBase.Width(590f), GuiBase.Height((float)marginTop), GuiBase.Width(50f), GuiBase.Height(30f)), new GUIContent("CAP")))
					{
						App.State.Generator.UseBreakEvenWorkerCount();
					}
				}
				else
				{
					if (GUI.Button(new Rect(GuiBase.Width(540f), GuiBase.Height((float)marginTop), GuiBase.Width(60f), GuiBase.Height(30f)), "Close"))
					{
						DivinityGeneratorUi.AddIsOpen = false;
					}
					marginTop += 35;
					labelStyle.fontSize = GuiBase.FontSize(18);
					labelStyle.fontStyle = FontStyle.Bold;
					GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(150f), GuiBase.Height(30f)), "Creation");
					GUI.Label(new Rect(GuiBase.Width(180f), GuiBase.Height((float)marginTop), GuiBase.Width(150f), GuiBase.Height(30f)), "Count", labelStyle);
					GUI.Label(new Rect(GuiBase.Width(325f), GuiBase.Height((float)marginTop), GuiBase.Width(150f), GuiBase.Height(30f)), "Capacity", labelStyle);
					GUI.Label(new Rect(GuiBase.Width(480f), GuiBase.Height((float)marginTop), GuiBase.Width(150f), GuiBase.Height(30f)), "Add", labelStyle);
					labelStyle.fontSize = GuiBase.FontSize(16);
					labelStyle.fontStyle = FontStyle.Normal;
				}
			}
		}

		protected void ShowScrollViewElements(int marginTop, GUIStyle labelStyle)
		{
			DivinityGenerator generator = App.State.Generator;
			if (!App.State.Generator.IsBuilt)
			{
				labelStyle.fontSize = GuiBase.FontSize(16);
				labelStyle.alignment = TextAnchor.UpperCenter;
				GuiBase.CreateProgressBar(marginTop, generator.getPercent(), generator.Name, generator.Description + generator.MissingItems, GuiBase.progressBg, GuiBase.progressFgBlue);
				labelStyle.fontSize = GuiBase.FontSize(18);
				GUI.Label(new Rect(GuiBase.Width(230f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), string.Empty + generator.Level, labelStyle);
				GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), string.Empty + generator.ShadowCloneCount, labelStyle);
				if (GUI.Button(new Rect(GuiBase.Width(470f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+"))
				{
					generator.AddCloneCount((int)this.ClonesToAdd);
				}
				if (GUI.Button(new Rect(GuiBase.Width(540f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "-"))
				{
					generator.RemoveCloneCount((int)this.ClonesToAdd);
				}
			}
			else if (!DivinityGeneratorUi.AddIsOpen)
			{
				labelStyle.alignment = TextAnchor.UpperCenter;
				marginTop += 170;
				labelStyle.fontSize = GuiBase.FontSize(18);
				labelStyle.fontStyle = FontStyle.Bold;
				GUI.Label(new Rect(GuiBase.Width(15f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), "Upgrade");
				GUI.Label(new Rect(GuiBase.Width(210f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), "Level", labelStyle);
				GUI.Label(new Rect(GuiBase.Width(280f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), "Stop At", labelStyle);
				GUI.Label(new Rect(GuiBase.Width(370f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), "Clones", labelStyle);
				marginTop += 45;
				labelStyle.fontStyle = FontStyle.Normal;
				foreach (GeneratorUpgrade current in generator.Upgrades)
				{
					labelStyle.fontSize = GuiBase.FontSize(16);
					GuiBase.CreateProgressBar(marginTop, current.getPercent(), current.Name, current.Description + current.MissingItems, GuiBase.progressBg, GuiBase.progressFgGreen);
					labelStyle.fontSize = GuiBase.FontSize(18);
					GUI.Label(new Rect(GuiBase.Width(210f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), string.Empty + current.Level.CommaFormatted, labelStyle);
					GUI.Label(new Rect(GuiBase.Width(370f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), string.Empty + current.ShadowCloneCount.CommaFormatted, labelStyle);
					if (App.CurrentPlattform == Plattform.Android)
					{
						GeneratorUpgrade up = current;
						GUIStyle textField = Gui.ChosenSkin.textField;
						if (GUI.Button(new Rect(GuiBase.Width(305f), GuiBase.Height((float)marginTop), GuiBase.Width(55f), GuiBase.Height(25f)), up.StopAt + string.Empty, textField))
						{
							base.ShowNumberInput("Stop at for " + up.Name + " " + up.Name, up.StopAt, 2147483647, delegate(CDouble x)
							{
								up.StopAt = x.ToInt();
							});
						}
					}
					else
					{
						string s = GUI.TextField(new Rect(GuiBase.Width(305f), GuiBase.Height((float)marginTop), GuiBase.Width(55f), GuiBase.Height(25f)), current.StopAt.ToString());
						int.TryParse(s, out current.StopAt);
						if (current.StopAt < 0)
						{
							current.StopAt = 0;
						}
					}
					if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+"))
					{
						current.AddCloneCount((int)this.ClonesToAdd);
					}
					if (GUI.Button(new Rect(GuiBase.Width(560f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "-"))
					{
						current.RemoveCloneCount((int)this.ClonesToAdd);
					}
					marginTop += 35;
				}
			}
			else
			{
				for (int i = 1; i < App.State.AllCreations.Count; i++)
				{
					Creation creation = App.State.AllCreations[i];
					labelStyle.fontSize = GuiBase.FontSize(16);
					GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(160f), GuiBase.Height(30f)), new GUIContent(creation.Name, "Capacity for one: " + creation.BuyCost.ToGuiText(true)), labelStyle);
					GUI.Label(new Rect(GuiBase.Width(180f), GuiBase.Height((float)marginTop), GuiBase.Width(160f), GuiBase.Height(30f)), creation.Count.ToGuiText(true), labelStyle);
					CDouble cDouble = App.State.GameSettings.DivGenCreatiosToAdd;
					if (cDouble == 0)
					{
						cDouble = 10000;
					}
					if (cDouble > creation.Count)
					{
						cDouble = creation.Count;
					}
					CDouble cDouble2 = cDouble * creation.BuyCost;
					if (cDouble2 > App.State.Generator.FreeCapacity)
					{
						cDouble = App.State.Generator.FreeCapacity / creation.BuyCost;
						cDouble.Value = Math.Floor(cDouble.Value);
						cDouble2 = cDouble * creation.BuyCost;
					}
					GUI.Label(new Rect(GuiBase.Width(325f), GuiBase.Height((float)marginTop), GuiBase.Width(160f), GuiBase.Height(30f)), cDouble2.ToGuiText(true), labelStyle);
					GUIStyle style = GUI.skin.GetStyle("Button");
					style.fontSize = GuiBase.FontSize(16);
					if (GUI.Button(new Rect(GuiBase.Width(480f), GuiBase.Height((float)marginTop), GuiBase.Width(130f), GuiBase.Height(30f)), cDouble.ToGuiText(true)) && cDouble > 0)
					{
						App.State.Generator.FilledCapacity += cDouble2;
						creation.Count -= cDouble;
					}
					marginTop += 35;
				}
			}
		}
	}
}
