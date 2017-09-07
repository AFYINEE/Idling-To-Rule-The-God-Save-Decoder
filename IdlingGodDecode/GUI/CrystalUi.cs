using Assets.Scripts.Data;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class CrystalUi : AreaRightUi
	{
		public static CrystalUi Instance = new CrystalUi();

		private bool showInfo;

		private string[] toolbarStringsPlanet = new string[]
		{
			"UBs",
			"UBs V2",
			"Crystal"
		};

		private bool showAll = true;

		public static void Show()
		{
			CrystalUi.Instance.Show(true);
		}

		public override bool Init()
		{
			return false;
		}

		protected override void ShowLabels(int marginTop, GUIStyle labelStyle)
		{
			this.SyncScrollbars = false;
			labelStyle.alignment = TextAnchor.MiddleLeft;
			labelStyle.fontStyle = FontStyle.Bold;
			labelStyle.fontSize = GuiBase.FontSize(18);
			PlanetUi.ToolbarIntPlanet = GUI.Toolbar(new Rect(GuiBase.Width(320f), GuiBase.Height(45f), GuiBase.Width(330f), GuiBase.Height(25f)), PlanetUi.ToolbarIntPlanet, this.toolbarStringsPlanet);
			marginTop += 5;
			string text = "Info";
			if (this.showInfo)
			{
				text = "Back";
			}
			if (GUI.Button(new Rect(GuiBase.Width(560f), GuiBase.Height((float)marginTop), GuiBase.Width(80f), GuiBase.Height(30f)), text))
			{
				this.showInfo = !this.showInfo;
			}
			GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(500f), GuiBase.Height(30f)), "Crystal Factory");
			labelStyle.fontSize = GuiBase.FontSize(16);
			labelStyle.fontStyle = FontStyle.Normal;
		}

		protected override void ShowScrollViewElements(int marginTop, GUIStyle labelStyle)
		{
			if (this.showInfo)
			{
				labelStyle.alignment = TextAnchor.UpperLeft;
				string text = "\r\nYou can adjust defender clones who will defend against incoming ultimate beings. This fight is just like if you attack them by yourself, but you won't lose hp.\r\nIf you win, you will receive Energy. If you lose, they will steal level 1 crystals (1 - 5 or each kind depending on the UB who attacks) and energy. With energy you can upgrade your modules. If you have upgraded modules, you can adjust shadow clones and they will produce crystals every 10 minutes with the same kind of the module.\r\nYour modules will still generate crystals when you are offline, but UBs won't attack you.\r\n\r\nThey will always produce level 1 crystals which can be upgraded if you have enough of them. You will lose crystals depending on a percentage basis which increases by 5% after every upgrade.\r\nWhen you upgrade crystals, there are two buttons. 'Upgrade Opt' will upgrade your crystals so you won't lose any crystal. For example an ultimate crystal starts at 60%. That means if you have have 8 crystals, only the 'Upgrade All' button is available.  After clicking it, you will lose 4 crystals because 60% of 8 is 4.8 and the number is rounded down.\r\nIf you have at least 10, the 'Upgrade Opt' will appear and would upgrade 6 out of 10 with 4 crystals lost. If you have 11, it would upgrade still 10 with 4 lost and it keeps 1 left over.\r\n\r\nIf you equip crystals, you will get a boost which is shown in their tooltip. You can only equip one of a kind, and the highest grade will be equipped. If you upgrade a crystal of the same kind later to a higher grade, you need to unequip your old one and equip it again for the higher grade to take effect.\r\nYou can only equip 2 different crystals at the beginning. This can be increased if you buy a slot upgrade until at most 6 slots. There is also a very rare chance to receive a slot upgrade with a lucky draw.\r\nWhen you rebirth, all your equipped crystals will give you crystal power depending on their grade. Your crystal power increases various stats which is shown in the god power page.\r\n";
				this.scrollViewHeight = (int)labelStyle.CalcHeight(new GUIContent(text), GuiBase.Width(600f));
				GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)(marginTop - 2)), GuiBase.Width(600f), (float)this.scrollViewHeight), text, labelStyle);
				return;
			}
			double percent = 1.0;
			if (App.State.Ext.Factory.TimeUntilAttack > 0L)
			{
				percent = 1.0 - (double)App.State.Ext.Factory.TimeUntilAttack / 3600000.0;
			}
			string str = string.Empty;
			if (App.State.PremiumBoni.CrystalBonusDefender > 0)
			{
				str = "\nDefender clones will only take " + (100 - App.State.PremiumBoni.CrystalBonusDefender) + " % damage because of equipped crystals.";
			}
			GuiBase.CreateProgressBar(marginTop, percent, "Defender Clones", "Ultimate beings will attack your crystal factory 10 minutes after they become available to fight in the UBs-Tab.\nIf two are available at the same time, there will be a pause of 10 minutes after each fight.\nIf your clones defeat them, you will get the same boost as if you would fight them yourself.\nIf you lose, they will steal some of your energy or level 1 crystals!" + str, GuiBase.progressBg, GuiBase.progressFgRed);
			if (App.State.Ext.Factory.DefenderFightsText.Length > 0 && GUI.Button(new Rect(GuiBase.Width(260f), GuiBase.Height((float)marginTop), GuiBase.Width(60f), GuiBase.Height(30f)), new GUIContent("Log", "Shows a short log of the last UB battles vs your defender clones.")))
			{
				GuiBase.ShowBigMessage(App.State.Ext.Factory.DefenderFightsText.ToString());
				App.State.Ext.Factory.DefenderFightsText = new StringBuilder();
			}
			labelStyle.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(GuiBase.Width(340f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), App.State.Ext.Factory.DefenderClones.CommaFormatted, labelStyle);
			labelStyle.alignment = TextAnchor.UpperLeft;
			if (GUI.Button(new Rect(GuiBase.Width(450f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+"))
			{
				App.State.Ext.Factory.AddCloneCount(App.State.GameSettings.ClonesToAddCount);
			}
			if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "-"))
			{
				App.State.Ext.Factory.RemoveCloneCount(App.State.GameSettings.ClonesToAddCount);
			}
			marginTop += 35;
			if (App.State.Statistic.HasStarted1kChallenge)
			{
				GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(500f), GuiBase.Height(30f)), "UBs won't attack in 1KC.", labelStyle);
			}
			else
			{
				GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(500f), GuiBase.Height(30f)), "Next UB attack in: " + Conv.MsToGuiText(App.State.Ext.Factory.TimeUntilAttack, true), labelStyle);
			}
			App.State.GameSettings.AutofillDefenders = GUI.Toggle(new Rect(GuiBase.Width(340f), GuiBase.Height((float)(marginTop + 5)), GuiBase.Width(100f), GuiBase.Height(30f)), App.State.GameSettings.AutofillDefenders, new GUIContent("Autofill", "Automatically fills up your defender clones if you have enough idle clones up to this number"));
			int num = (App.State.Clones.MaxShadowClones - App.State.GameSettings.SavedClonesForFight).ToInt();
			if (App.CurrentPlattform == Plattform.Android)
			{
				GUIStyle textField = Gui.ChosenSkin.textField;
				if (GUI.Button(new Rect(GuiBase.Width(480f), GuiBase.Height((float)(marginTop + 3)), GuiBase.Width(100f), GuiBase.Height(25f)), App.State.GameSettings.MaxDefenderClones.ToString(), textField))
				{
					base.ShowNumberInput("Maximum number of clones to autofill", App.State.GameSettings.MaxDefenderClones, num, delegate(CDouble x)
					{
						App.State.GameSettings.MaxDefenderClones = x.ToInt();
					});
				}
			}
			else
			{
				string s = GUI.TextField(new Rect(GuiBase.Width(480f), GuiBase.Height((float)(marginTop + 5)), GuiBase.Width(100f), GuiBase.Height(25f)), App.State.GameSettings.MaxDefenderClones.ToString());
				int.TryParse(s, out App.State.GameSettings.MaxDefenderClones);
				if (App.State.GameSettings.MaxDefenderClones > num)
				{
					App.State.GameSettings.MaxDefenderClones = num;
				}
				if (App.State.GameSettings.MaxDefenderClones < 0)
				{
					App.State.GameSettings.MaxDefenderClones = 0;
				}
			}
			marginTop += 40;
			labelStyle.fontSize = GuiBase.FontSize(18);
			labelStyle.fontStyle = FontStyle.Bold;
			GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(200f), GuiBase.Height(30f)), new GUIContent("Energy", "Each UB you defeat will give you energy. You can use this energy to upgrade your modules."));
			GUI.Label(new Rect(GuiBase.Width(320f), GuiBase.Height((float)marginTop), GuiBase.Width(200f), GuiBase.Height(30f)), new GUIContent(App.State.Ext.Factory.Energy.GuiText, string.Empty));
			labelStyle.fontStyle = FontStyle.Normal;
			labelStyle.fontSize = GuiBase.FontSize(16);
			marginTop += 40;
			labelStyle.alignment = TextAnchor.MiddleCenter;
			labelStyle.fontStyle = FontStyle.Bold;
			GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(185f), GuiBase.Height(30f)), new GUIContent("Modules", "You can produce crystals with each module. The count of the produced crystal to be produced is the same as the module level."));
			GUI.Label(new Rect(GuiBase.Width(227f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Level", "A module can only create level 1 crystals. For each level one crystal is produced for each progressbar filled.\nEach level also increases the upgrade chance of crystals of this kind by 1%. This increase is capped at 25% for god or ultimate modules, no cap for the others. The chance can't go lower than 5%, or higher is 95%"));
			GUI.Label(new Rect(GuiBase.Width(340f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Clones", "Adjusted clones will fill the module constantly with light, air and water so the module can produce crystals.\nThe first 4 modules need 10k clones,the ultimate module needs 30k, and the god module 50k for every level.\nClicking + or - will fill in all you need or remove all clones."));
			App.State.GameSettings.AutoBuyForCrystal = GUI.Toggle(new Rect(GuiBase.Width(450f), GuiBase.Height((float)(marginTop + 4)), GuiBase.Width(300f), GuiBase.Height(25f)), App.State.GameSettings.AutoBuyForCrystal, new GUIContent("Autobuy", "If this is on, and you have defeated Nephthys, the game will automatically buy the creations you need to produce crystals."));
			labelStyle.fontStyle = FontStyle.Normal;
			marginTop += 35;
			foreach (FactoryModule current in App.State.Ext.Factory.AllModules)
			{
				if (current.Crystals.Count > 0)
				{
				}
				GuiBase.CreateProgressBar(marginTop, (double)current.CurrentDuration / (double)current.BaseDuration, current.Name + " Module", current.InfoText, GuiBase.progressBg, GuiBase.progressFgGreen);
				GUI.Label(new Rect(GuiBase.Width(227f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), string.Empty + current.LevelText, labelStyle);
				if (current.Level < current.MaxLevel && GUI.Button(new Rect(GuiBase.Width(225f), GuiBase.Height((float)marginTop), GuiBase.Width(35f), GuiBase.Height(30f)), "+"))
				{
					current.ChangeLevel(true);
					current.AddNeededClones();
				}
				if (current.Level > 1 && GUI.Button(new Rect(GuiBase.Width(300f), GuiBase.Height((float)marginTop), GuiBase.Width(35f), GuiBase.Height(30f)), "-"))
				{
					current.ChangeLevel(false);
					current.AddNeededClones();
				}
				if (current.MaxLevel > 0)
				{
					GUI.Label(new Rect(GuiBase.Width(340f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), current.ShadowClones.CommaFormatted, labelStyle);
					if (GUI.Button(new Rect(GuiBase.Width(450f), GuiBase.Height((float)marginTop), GuiBase.Width(35f), GuiBase.Height(30f)), "+"))
					{
						current.AddNeededClones();
					}
					if (GUI.Button(new Rect(GuiBase.Width(495f), GuiBase.Height((float)marginTop), GuiBase.Width(35f), GuiBase.Height(30f)), "-"))
					{
						current.RemoveAllClones();
					}
				}
				else
				{
					GUI.Label(new Rect(GuiBase.Width(365f), GuiBase.Height((float)marginTop), GuiBase.Width(150f), GuiBase.Height(30f)), "Please upgrade first", labelStyle);
				}
				if (GUI.Button(new Rect(GuiBase.Width(540f), GuiBase.Height((float)marginTop), GuiBase.Width(80f), GuiBase.Height(30f)), new GUIContent("Upgrade", "You need " + current.UpgradeCost.GuiText + " Energy to upgrade this module.")))
				{
					if (current.Upgrade(ref App.State.Ext.Factory.Energy))
					{
						GuiBase.ShowToast(current.Name + " has now a max level of " + current.MaxLevel.GuiText + "!");
					}
					else
					{
						GuiBase.ShowToast("You don't have enough energy!");
					}
				}
				marginTop += 35;
			}
			marginTop += 35;
			GUI.Label(new Rect(GuiBase.Width(450f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Show All", "If this is off, it will show only the crystals with the highest grade."), labelStyle);
			this.showAll = GUI.Toggle(new Rect(GuiBase.Width(543f), GuiBase.Height((float)(marginTop + 4)), GuiBase.Width(60f), GuiBase.Height(30f)), this.showAll, string.Empty);
			using (List<FactoryModule>.Enumerator enumerator2 = App.State.Ext.Factory.AllModules.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					FactoryModule module = enumerator2.Current;
					Crystal crystal = App.State.Ext.Factory.EquippedCrystals.FirstOrDefault((Crystal x) => x.Type == module.Type);
					if (module.Crystals.Count > 0 || crystal != null)
					{
						labelStyle.alignment = TextAnchor.UpperLeft;
						GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(200f), GuiBase.Height(30f)), new GUIContent(module.Type + " Crystals"), labelStyle);
						labelStyle.alignment = TextAnchor.MiddleCenter;
						if (crystal == null)
						{
							if (GUI.Button(new Rect(GuiBase.Width(230f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Equip", "Equip the highest grade of your " + module.Type + " Crystals.")))
							{
								if (SpecialFightUi.IsFighting)
								{
									GuiBase.ShowToast("Please finish your special fight first!");
								}
								else
								{
									App.State.PremiumBoni.MaxCrystals.Round();
									if (App.State.PremiumBoni.MaxCrystals <= App.State.Ext.Factory.EquippedCrystals.Count)
									{
										GuiBase.ShowToast("You can only equip " + App.State.PremiumBoni.MaxCrystals + " crystals at once!");
									}
									else
									{
										Crystal crystal2 = module.Crystals[module.Crystals.Count - 1];
										crystal2.Count.Round();
										if (crystal2.Count == 0)
										{
											GuiBase.ShowToast("You don't have any crystals you can equip...");
										}
										else
										{
											if (crystal2.Count > 1)
											{
												crystal2.Count -= 1;
											}
											else
											{
												module.Crystals.RemoveAt(module.Crystals.Count - 1);
											}
											App.State.Ext.Factory.EquippedCrystals.Add(crystal2);
										}
									}
									App.State.PremiumBoni.CheckCrystalBonus(App.State);
									if (App.State.GameSettings.MaxAfterEquipCrystal)
									{
										App.State.GameSettings.CreationToCreateCount = App.State.PremiumBoni.CreationCountBoni(true).ToInt() + 1;
									}
								}
							}
						}
						else if (GUI.Button(new Rect(GuiBase.Width(230f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), new GUIContent("Unequip", "Equip the highest grade of your " + module.Type + " Crystals.")))
						{
							App.State.Ext.Factory.EquippedCrystals.Remove(crystal);
							module.AddCrystal(crystal, 1);
						}
						marginTop += 35;
					}
					for (int i = 0; i < module.Crystals.Count; i++)
					{
						if (i < module.Crystals.Count)
						{
							if (this.showAll || i >= module.Crystals.Count - 1)
							{
								Crystal crystal3 = module.Crystals[i];
								if (crystal3.Count > 0)
								{
									GUI.Label(new Rect(GuiBase.Width(60f), GuiBase.Height((float)marginTop), GuiBase.Width(50f), GuiBase.Height(30f)), new GUIContent(crystal3.Image, crystal3.Description));
									GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height((float)marginTop), GuiBase.Width(50f), GuiBase.Height(30f)), string.Empty + crystal3.Level.GuiText + "*", labelStyle);
									GUI.Label(new Rect(GuiBase.Width(90f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), " x " + crystal3.Count.GuiText, labelStyle);
									int maxLevel = crystal3.MaxLevel;
									if (crystal3.Level < maxLevel)
									{
										CDouble cDouble = crystal3.GetOptimalCount(App.State, module.MaxLevel);
										string text2 = string.Empty;
										CDouble cDouble2 = crystal3.UpgradeChance(App.State, module.MaxLevel, true);
										CDouble cDouble3 = crystal3.UpgradeChance(App.State, module.MaxLevel, false);
										CDouble countKeepLeftovers = crystal3.GetCountKeepLeftovers(App.State, module.MaxLevel, crystal3.Count);
										CDouble crystalsAfterUpgrade = crystal3.GetCrystalsAfterUpgrade(App.State, module.MaxLevel, cDouble);
										CDouble crystalsAfterUpgrade2 = crystal3.GetCrystalsAfterUpgrade(App.State, module.MaxLevel, countKeepLeftovers);
										if (cDouble2.ToInt() > cDouble3)
										{
											text2 = cDouble3.GuiText + " (capped out of your total chance of " + cDouble2.GuiText + ")";
										}
										else
										{
											text2 = cDouble3.GuiText;
										}
										labelStyle.alignment = TextAnchor.MiddleLeft;
										GUI.Label(new Rect(GuiBase.Width(200f), GuiBase.Height((float)marginTop), GuiBase.Width(150f), GuiBase.Height(30f)), " upgrade: " + cDouble3.GuiText + "%", labelStyle);
										if (cDouble <= crystal3.Count)
										{
											if (GUI.Button(new Rect(GuiBase.Width(370f), GuiBase.Height((float)marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), new GUIContent("Upgrade Opt", string.Concat(new object[]
											{
												"If you upgrade your grade ",
												crystal3.Level,
												" ",
												crystal3.Type,
												" crystals to the next grade, ",
												text2,
												" % of your crystals can be upgraded, the others are lost. This upgrades your crystals with losing the least amount and keeps a left over. You will use ",
												cDouble,
												" crystals which will be upgraded to ",
												crystalsAfterUpgrade.GuiText,
												" of the next grade.\nMaxed with grade ",
												maxLevel,
												".\n"
											}))))
											{
												module.UpgradeCrystal(App.State, crystal3, module.MaxLevel, cDouble);
											}
											GUI.Label(new Rect(GuiBase.Width(310f), GuiBase.Height((float)marginTop), GuiBase.Width(210f), GuiBase.Height(30f)), "(" + crystalsAfterUpgrade.GuiText + ")", labelStyle);
										}
										else if (countKeepLeftovers <= crystal3.Count && countKeepLeftovers > 0)
										{
											if (GUI.Button(new Rect(GuiBase.Width(370f), GuiBase.Height((float)marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), new GUIContent("Upgrade Keep", string.Concat(new object[]
											{
												"If you upgrade your grade ",
												crystal3.Level,
												" ",
												crystal3.Type,
												" crystals to the next grade, ",
												text2,
												" % of your crystals can be upgraded, the others are lost. This upgrades your crystals similar to 'Upgrade All', keeps the left over instead of wasting it. You will use ",
												countKeepLeftovers,
												" crystals which will be upgraded to ",
												crystalsAfterUpgrade2.GuiText,
												" of the next grade.\nMaxed with grade ",
												maxLevel,
												".\n"
											}))))
											{
												module.UpgradeCrystal(App.State, crystal3, module.MaxLevel, countKeepLeftovers);
											}
											GUI.Label(new Rect(GuiBase.Width(310f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), "(" + crystalsAfterUpgrade2.GuiText + ")", labelStyle);
										}
										labelStyle.alignment = TextAnchor.MiddleCenter;
										CDouble crystalsAfterUpgrade3 = crystal3.GetCrystalsAfterUpgrade(App.State, module.MaxLevel, crystal3.Count);
										if (crystalsAfterUpgrade3 >= 1)
										{
											if (GUI.Button(new Rect(GuiBase.Width(490f), GuiBase.Height((float)marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), new GUIContent("Upgrade All", string.Concat(new object[]
											{
												"If you upgrade your grade ",
												crystal3.Level,
												" ",
												crystal3.Type,
												" crystals to the next grade, ",
												text2,
												" % of your crystals can be upgraded, the others are lost. This will use up all your crystals of this grade and keeps no left over. You will use ",
												crystal3.Count.GuiText,
												" crystals which will be upgraded to ",
												crystalsAfterUpgrade3,
												" of the next grade.\nMaxed with grade ",
												maxLevel,
												".\n"
											}))))
											{
												module.UpgradeCrystal(App.State, crystal3, module.MaxLevel, crystal3.Count);
											}
										}
										else
										{
											GUI.Label(new Rect(GuiBase.Width(370f), GuiBase.Height((float)marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent("Not enough crystals.", string.Concat(new object[]
											{
												cDouble3,
												" % of the crystals can be upgraded, the others are lost.\nYou need to have at least ",
												crystal3.GetMinimumNeeded(App.State, module.MaxLevel),
												" grade ",
												crystal3.Level,
												" ",
												crystal3.Type,
												" crystals to be able to upgrade them."
											})), labelStyle);
										}
									}
									marginTop += 35;
								}
							}
						}
					}
				}
			}
			this.scrollViewHeight = marginTop - 140;
		}
	}
}
