using Assets.Scripts.Data;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class PlanetUi : AreaRightUi
	{
		public static PlanetUi Instance = new PlanetUi();

		private bool showV2;

		private BattleUBV2 Battle;

		private UltimateBeing selectedBeing;

		private bool showBaalPower;

		private bool showInfo;

		private bool isFighting;

		private string fightLog = string.Empty;

		private int bPAttackUsage;

		private int bPDefUsage;

		private bool useUpPowerSurge;

		public static int ToolbarIntPlanet = 0;

		private string[] toolbarStringsPlanet = new string[]
		{
			"UBs",
			"UBs V2",
			"Crystal"
		};

		private static bool scrollBarsToZero = false;

		private bool IsFightingV2
		{
			get
			{
				return this.Battle != null && this.Battle.IsFighting;
			}
		}

		public static void Show()
		{
			if (PlanetUi.ToolbarIntPlanet == 2)
			{
				CrystalUi.Show();
			}
			else
			{
				PlanetUi.Instance.Show(true);
			}
		}

		public override bool Init()
		{
			if (this.IsFightingV2)
			{
				this.FightUB();
				return true;
			}
			return false;
		}

		protected override void ShowLabels(int marginTop, GUIStyle labelStyle)
		{
			this.SyncScrollbars = false;
			labelStyle.alignment = TextAnchor.MiddleLeft;
			labelStyle.fontStyle = FontStyle.Normal;
			labelStyle.fontSize = GuiBase.FontSize(16);
			marginTop += 5;
			string text = "Info";
			if (this.isFighting || this.showBaalPower || this.showInfo)
			{
				text = "Back";
			}
			if (GUI.Button(new Rect(GuiBase.Width(560f), GuiBase.Height((float)marginTop), GuiBase.Width(80f), GuiBase.Height(30f)), text))
			{
				if (this.isFighting)
				{
					this.isFighting = false;
				}
				else if (this.showBaalPower)
				{
					this.showBaalPower = false;
				}
				else
				{
					this.showInfo = !this.showInfo;
				}
			}
			bool flag = App.State.Statistic.UltimateBaalChallengesFinished > 0 || (!this.showInfo && App.State.Statistic.ArtyChallengesFinished > 0);
			if (!App.State.HomePlanet.IsCreated)
			{
				flag = false;
			}
			if (App.State.HomePlanet.IsCreated)
			{
				if (App.State.Statistic.ArtyChallengesFinished > 0 || App.State.Statistic.UltimateBaalChallengesFinished > 0)
				{
					PlanetUi.ToolbarIntPlanet = GUI.Toolbar(new Rect(GuiBase.Width(320f), GuiBase.Height(45f), GuiBase.Width(330f), GuiBase.Height(25f)), PlanetUi.ToolbarIntPlanet, this.toolbarStringsPlanet);
					this.showV2 = (PlanetUi.ToolbarIntPlanet == 1 && flag);
				}
				else
				{
					GUI.Label(new Rect(GuiBase.Width(450f), GuiBase.Height(45f), GuiBase.Width(100f), GuiBase.Height(25f)), new GUIContent("???", "Finish at least one UBC or UAC to unlock"));
				}
			}
			if (!this.showInfo && !this.isFighting && !this.showBaalPower)
			{
				if (App.State.HomePlanet.UpgradeLevel < 5 || !App.State.HomePlanet.IsCreated)
				{
					string buttonText = "Unlock";
					if (App.State.HomePlanet.IsCreated)
					{
						buttonText = "Upgrade";
					}
					if (GUI.Button(new Rect(GuiBase.Width(320f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), buttonText))
					{
						int creationTier = 24 + App.State.HomePlanet.UpgradeLevel.ToInt();
						if (!App.State.HomePlanet.IsCreated)
						{
							creationTier = 23;
						}
						Creation creationNeeded = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == (Creation.CreationType)creationTier);
						if (creationNeeded == null || creationNeeded.Count == 0)
						{
							if (!App.State.HomePlanet.IsCreated)
							{
								GuiBase.ShowToast("You need to defeat Jupiter and create one Planet to unlock your home planet.");
							}
							else
							{
								GuiBase.ShowToast(string.Concat(new string[]
								{
									"You need 1 ",
									creationNeeded.Name,
									" to ",
									buttonText,
									" your Planet!"
								}));
							}
						}
						else
						{
							GuiBase.ShowDialog(buttonText + " Planet", string.Concat(new string[]
							{
								"Do you want to ",
								buttonText.ToLower(),
								" your Planet and use up 1 ",
								creationNeeded.Name,
								"?"
							}), delegate
							{
								App.State.HomePlanet.Upgrade(App.State);
								creationNeeded.Count -= 1;
								GuiBase.ShowToast("You successfully " + buttonText + "d your Planet by spending 1 " + creationNeeded.Name);
							}, delegate
							{
							}, "Yes", "No", false, false);
						}
					}
				}
				if (App.State.HomePlanet.IsCreated)
				{
					labelStyle.fontStyle = FontStyle.Bold;
					labelStyle.fontSize = GuiBase.FontSize(18);
					string text2 = string.Concat(new object[]
					{
						App.State.HomePlanet.Name,
						" (level ",
						App.State.HomePlanet.UpgradeLevel,
						")"
					});
					if (App.State.HomePlanet.UpgradeLevel == 50)
					{
						text2 = string.Concat(new object[]
						{
							App.State.HomePlanet.Name,
							" (level ",
							App.State.HomePlanet.UpgradeLevel,
							" MAX)"
						});
					}
					GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(500f), GuiBase.Height(30f)), text2, labelStyle);
					labelStyle.alignment = TextAnchor.UpperCenter;
					GUI.Label(new Rect(GuiBase.Width(15f), GuiBase.Height((float)(marginTop + 95)), GuiBase.Width(220f), GuiBase.Height(30f)), "Ultimate Being");
					if (!this.showV2)
					{
						GUI.Label(new Rect(GuiBase.Width(175f), GuiBase.Height((float)(marginTop + 95)), GuiBase.Width(220f), GuiBase.Height(30f)), "Revert time");
						GUI.Label(new Rect(GuiBase.Width(355f), GuiBase.Height((float)(marginTop + 95)), GuiBase.Width(200f), GuiBase.Height(30f)), "Multiplier increase", labelStyle);
					}
					else
					{
						GUI.Label(new Rect(GuiBase.Width(175f), GuiBase.Height((float)(marginTop + 95)), GuiBase.Width(220f), GuiBase.Height(30f)), "Count");
						GUI.Label(new Rect(GuiBase.Width(330f), GuiBase.Height((float)(marginTop + 95)), GuiBase.Width(100f), GuiBase.Height(30f)), "Stop At");
					}
				}
			}
			labelStyle.fontSize = GuiBase.FontSize(16);
		}

		public void Reset()
		{
			this.selectedBeing = null;
			this.showBaalPower = false;
			this.showInfo = false;
			this.isFighting = false;
			this.fightLog = string.Empty;
			this.showV2 = false;
			this.Battle = null;
		}

		protected override void ShowScrollViewElements(int marginTop, GUIStyle labelStyle)
		{
			if (PlanetUi.scrollBarsToZero)
			{
				base.SetScrollbarPosition(Vector2.zero);
				PlanetUi.scrollBarsToZero = false;
			}
			if (this.showInfo)
			{
				labelStyle.alignment = TextAnchor.UpperLeft;
				string text = "Here at your planet you get an extra multiplier. At first your clones can increase the multiplier with powersurge. If you upgrade your planet, you can increase the multiplier faster and fight ultimate beings.\nIf you defeat an ultimate being, your multiplier for physical, mystic, battle and creating will increase, however after each kill the effect will decrease. Tier 1 drops one GP every 5 times, Tier 2 drops once GP twice every 5 times up to once GP every kill for the highest UB! (The counter will reset after rebirthing)\nFighting the ultimate beings works like this: \nFirst you adjust clones, then you press the 'Fight' button and then the fight will be calculated.\nIn the fight some or all your clones might die, so take this into consideration!\nThe power of the ultimate beings is adjusted to your own power. So the number of clones you use is the most important!\nIt is advisable to use more than 100k clones for the first fight and more for the later ones.\nIf you lose a fight, the health the ultimate being lost will stay and it won't recover it's hp. However each ultimate being has a timer. When the timer reaches zero, it will be back to full health. If you killed it, it will also be back and you can fight it again.";
				this.scrollViewHeight = (int)labelStyle.CalcHeight(new GUIContent(text), GuiBase.Width(600f));
				GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)(marginTop - 2)), GuiBase.Width(600f), (float)this.scrollViewHeight), text, labelStyle);
				return;
			}
			if (this.showV2)
			{
				marginTop += 10;
				this.addPowerSurge(marginTop, labelStyle);
				marginTop += 80;
				this.showUBV2s(marginTop, labelStyle);
				return;
			}
			if (this.showBaalPower)
			{
				labelStyle.alignment = TextAnchor.UpperLeft;
				marginTop += 2;
				GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(50f)), "Here you can spend Baal Power to increase your chances to beat UBs. The increased Attack / Defense will last for one fight.", labelStyle);
				marginTop += 50;
				GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(340f)), "You have " + (App.State.HomePlanet.BaalPower - this.bPAttackUsage - this.bPDefUsage) + " Baal Power", labelStyle);
				marginTop += 50;
				GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(340f)), "Clone Attack: " + (App.State.ClonesPlanetMod + this.bPAttackUsage * 50) + " %", labelStyle);
				if (App.State.HomePlanet.BaalPower > this.bPAttackUsage + this.bPDefUsage && GUI.Button(new Rect(GuiBase.Width(300f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+"))
				{
					this.bPAttackUsage++;
				}
				if (this.bPAttackUsage > 0 && GUI.Button(new Rect(GuiBase.Width(350f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "-"))
				{
					this.bPAttackUsage--;
				}
				if (this.bPAttackUsage + this.bPDefUsage > App.State.HomePlanet.BaalPower)
				{
					this.bPAttackUsage = 0;
					this.bPDefUsage = 0;
				}
				marginTop += 50;
				GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height(340f)), "Clone Defense: " + (App.State.ClonesPlanetMod + this.bPDefUsage * 50) + " %", labelStyle);
				if (App.State.HomePlanet.BaalPower > this.bPAttackUsage + this.bPDefUsage && GUI.Button(new Rect(GuiBase.Width(300f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+"))
				{
					this.bPDefUsage++;
				}
				if (this.bPDefUsage > 0 && GUI.Button(new Rect(GuiBase.Width(350f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "-"))
				{
					this.bPDefUsage--;
				}
				marginTop += 50;
				int num = App.State.HomePlanet.PowerSurgeBoni / 2;
				this.useUpPowerSurge = GUI.Toggle(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(280f), GuiBase.Height(30f)), this.useUpPowerSurge, new GUIContent("Use Powersurge (" + num + " %)", "If this is on, your get additional % to Attack and Defense for this fight and the bonus from Powersurge will reset."));
				if (GUI.Button(new Rect(GuiBase.Width(560f), GuiBase.Height((float)marginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Fight"))
				{
					this.showBaalPower = false;
					if (this.useUpPowerSurge)
					{
						this.fightLog = this.selectedBeing.Fight(App.State, this.bPAttackUsage, this.bPDefUsage, num, App.State.HomePlanet.ShadowCloneCount, false);
						App.State.HomePlanet.PowerSurgeBoni = 0;
					}
					else
					{
						this.fightLog = this.selectedBeing.Fight(App.State, this.bPAttackUsage, this.bPDefUsage, 0, App.State.HomePlanet.ShadowCloneCount, false);
					}
					this.bPDefUsage = 0;
					this.bPAttackUsage = 0;
					this.isFighting = true;
				}
				return;
			}
			if (this.isFighting)
			{
				this.scrollViewHeight = this.fightLog.Split(new char[]
				{
					'\n'
				}).Length * 35 + 40;
				labelStyle.alignment = TextAnchor.UpperLeft;
				GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(600f), GuiBase.Height((float)(this.scrollViewHeight - 40))), this.fightLog, labelStyle);
				marginTop += this.scrollViewHeight - 40;
				return;
			}
			this.scrollViewHeight = 320;
			if (App.State.HomePlanet.IsCreated)
			{
				marginTop += 10;
				this.addPowerSurge(marginTop, labelStyle);
				marginTop += 80;
				bool flag = false;
				foreach (UltimateBeing current in App.State.HomePlanet.UltimateBeings)
				{
					labelStyle.fontSize = GuiBase.FontSize(16);
					if (current.IsAvailable)
					{
						GuiBase.CreateProgressBar(marginTop, current.HPPercent / 100.0, current.Name, current.Description, GuiBase.progressBg, GuiBase.progressFgBlue);
						GUI.Label(new Rect(GuiBase.Width(230f), GuiBase.Height((float)marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), string.Empty + Conv.MsToGuiText(current.TimeUntilComeBack, true), labelStyle);
						GUI.Label(new Rect(GuiBase.Width(355f), GuiBase.Height((float)marginTop), GuiBase.Width(200f), GuiBase.Height(30f)), string.Empty + current.NextMultiplier.ToGuiText(false) + " %", labelStyle);
						if (current.HPPercent > 0.0 && GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height((float)marginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Fight"))
						{
							if (App.State.HomePlanet.ShadowCloneCount > 0)
							{
								if (App.State.HomePlanet.BaalPower == 0 && App.State.HomePlanet.PowerSurgeBoni < 2)
								{
									this.fightLog = current.Fight(App.State, 0, 0, 0, App.State.HomePlanet.ShadowCloneCount, false);
									this.isFighting = true;
								}
								else
								{
									this.showBaalPower = true;
									this.selectedBeing = current;
								}
							}
							else
							{
								GuiBase.ShowToast("You need to add clones to powersurge before starting a fight!");
							}
						}
						if (current.Tier == 5 && current.TimesDefeated > 0)
						{
							flag = true;
						}
					}
					else
					{
						labelStyle.alignment = TextAnchor.UpperLeft;
						GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(450f), GuiBase.Height(30f)), "Your planet has to be level " + current.Tier + " to unlock this ultimate being.", labelStyle);
					}
					marginTop += 35;
				}
				labelStyle.alignment = TextAnchor.UpperLeft;
				GUI.Label(new Rect(GuiBase.Width(240f), GuiBase.Height((float)marginTop), GuiBase.Width(200f), GuiBase.Height(30f)), (App.State.HomePlanet.PlanetMultiplier + 100).ToGuiText(true) + " %", labelStyle);
				labelStyle.fontStyle = FontStyle.Bold;
				labelStyle.fontSize = GuiBase.FontSize(18);
				GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent("Planet Multiplier", string.Concat(new string[]
				{
					"This multiplier multiplies directly Physical, Mystic, Battle and Creating. 100% is the base value.lowerTextMulti from Powersurge: ",
					App.State.HomePlanet.PowerSurgeMultiplier.GuiText,
					" %\nMulti from Ultimate Beings: ",
					App.State.HomePlanet.UBMultiplier.ToGuiText(true),
					" %"
				})), labelStyle);
				labelStyle.fontStyle = FontStyle.Normal;
				marginTop += 30;
				if (flag)
				{
					CDouble cDouble = App.State.Statistic.MonumentsCreated;
					cDouble += App.State.Statistic.HighestGodDefeated * 10000;
					cDouble += App.State.Statistic.TotalAchievements * 10;
					cDouble += App.State.Statistic.TotalGodsDefeated * 100;
					cDouble += App.State.Statistic.MostDefeatedShadowClones * 5000;
					cDouble += App.State.Statistic.TBSScore * 100;
					cDouble += App.State.Statistic.TotalTrainingLevels / 100000;
					cDouble += App.State.Statistic.TotalSkillLevels / 100000;
					cDouble += App.State.Statistic.TotalEnemiesDefeated / 200000;
					cDouble += App.State.Statistic.TotalShadowClonesCreated / 5000;
					cDouble += App.State.Statistic.TotalCreations / 10000;
					cDouble += App.State.Clones.MaxShadowClones / 10;
					cDouble += App.State.Statistic.UBsDefeated * 250;
					cDouble += App.State.Statistic.TimePlayed / 50000L;
					cDouble += App.State.PremiumBoni.CrystalPower * 100;
					GUI.Label(new Rect(GuiBase.Width(240f), GuiBase.Height((float)marginTop), GuiBase.Width(150f), GuiBase.Height(30f)), string.Empty + cDouble.ToGuiText(true), labelStyle);
					labelStyle.fontStyle = FontStyle.Bold;
					labelStyle.fontSize = GuiBase.FontSize(18);
					GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)marginTop), GuiBase.Width(220f), GuiBase.Height(30f)), new GUIContent("Game Points", "Congratulation for reaching this far! This is calculated from your stats in this game. In Steam you can export the score and keep it. It might be useful in one of my next games.\nFor other plattforms it is just a score you can compare with others currently."), labelStyle);
					labelStyle.fontStyle = FontStyle.Normal;
					if (App.CurrentPlattform == Plattform.Steam && GUI.Button(new Rect(GuiBase.Width(450f), GuiBase.Height((float)marginTop), GuiBase.Width(180f), GuiBase.Height(30f)), "I win"))
					{
						string password = "Bvd2VyTGV2ZWwQTWluaW5nU3BlZWRMZXZlbBvd2VyTGV2ZWwQTWluaW5nU3BlZWRMZXZlbBZNaW5pbmdQcm9zcGVjdGlvbkxldmVsE1NtZWx0aW5nUHJvZ3Jlc3Npb24MU21lbHRpbmdUeXBlDlNtZWx0aAAD0AQAAx01K9WoR4D8JBAAAAAcAAACCAAAANQAAADUAAACKAAAAAP////+/TNZAAieccB+ZEkSA+jTtFgQQRBEAAAAdAAAABQAAAAAAAAAACqZABgAAAB8AAAAGAAAACQUAAAAJBgAAAAkHAAAAAQEBBQMAAAAESXRlbQUAAAAJaXRlbVZhbHVlBG5hbWUJZW5jaGFudGVkCHF1YW50aXR5BHBsdXMAAQAAAAYBBggCAAAA4XrgukdhEEIGCAAAAZXZlbBFTZWxsaW5nU3BlZWRMZXZlbBFTZWxsaW5nU3VwZXJRdWljaw5BBZNaW5pbmdQcm9zcGVjdGlvbkxldmVsE1NtZWx0aW5nUHJvZ3Jlc3Npb24MU21lbHRpbmdUeXBlDlNtZWx0aW5nQWN0aXZlD1NtZWx0aW5nVHlwZU1heBNTbWVsdGluZ01hbnVhbExldmVsF1NtZWx0aW5nQ3JpdENoYW5jZUxldmVsFlNtZWx0aW5nQ3JpdFBvd2VyTGV2ZWwSU21lbHRpbmdTcGVlZExldmVsEkZvcmdpbmdQcm9ncmVzc2lvbgtGb3JnaW5nVHlwZQ1Gb3JnaW5nQWN0aXZlDkZvcmdpbmdUeXBlTWF4EkZvcmdpbmdNYW51YWxMZXZlbBZGb3JnaW5nQ3JpdENoYW5jZUxldmVsFUZvcmdpbmdDcml0UG93ZXJMZXZlbBFGb3JnaW5nU3BlZWRMZXZlbBVFbmNoYW50aW5nUHJvZ3Jlc3Npb24ORW5jaGFudGluZ0l0ZW0ORW5jaGFudGluZ1R5cGURRW5jaGFudGluZ1R5cGVNYXgVRW5jaGFudGluZ01hbnVhbExldmVsGUVuY2hhbnRpbmdDcml0Q2hhbmNlTGV2ZWwYRW5jaGFudGluZ0NyaXRQb3dlckxldmVsFEVuY2hhbnRpbmdTcGVlZExldmVsElNlbGxpbmdQcm9ncmVzc2lvbgtTZWxsaW5nSXRlbQtTZWxsaW5nVHlwZRJTZWxsaW5nTWFudWFsTGV2ZWwWU2VsbGluZ0NyaXRDaGFuY2VMZXZlbBdTZWxsaW5nTmVnb2NpYXRpb25MZXZlbBFTZWxsaW5nU3BlZWRMZXZlbBFTZWxsaW5nU3VwZXJRdWljaw5B";
						StringBuilder stringBuilder = new StringBuilder();
						Conv.AppendValue(stringBuilder, "a", UnityEngine.Random.Range(0, 9999999).ToString());
						Conv.AppendValue(stringBuilder, "b", cDouble.ToInt().ToString());
						Conv.AppendValue(stringBuilder, "c", App.State.KongUserId.ToString());
						Conv.AppendValue(stringBuilder, "d", UnityEngine.Random.Range(0, 9999999).ToString());
						Conv.AppendValue(stringBuilder, "e", App.State.KongUserName.ToString());
						Conv.AppendValue(stringBuilder, "f", UnityEngine.Random.Range(0, 9999999).ToString());
						string plainText = Conv.ToBase64(stringBuilder.ToString(), string.Empty);
						string plainText2 = Vertifier.Encrypt(plainText, password);
						string text2 = Conv.ToBase64(plainText2, string.Empty);
						TextEditor textEditor = new TextEditor();
						textEditor.text = text2;
						textEditor.SelectAll();
						textEditor.Copy();
						GuiBase.ShowToast("Your data is copied to your clipboard. Please paste it to a text-editor and keep it save until it is of use in one of my next games!");
					}
				}
			}
		}

		private void addPowerSurge(int marginTop, GUIStyle labelStyle)
		{
			GuiBase.CreateProgressBar(marginTop, App.State.HomePlanet.Percent, "Powersurge", string.Concat(new object[]
			{
				"Adjust clones here to get more power!\nOne Shadow Clone generates (1 + planet level) power / hour. 100k power will increase your Stat Multiplier by 1.lowerText",
				App.State.HomePlanet.ProgressInfo,
				"\nPowerbonus for Fights: ",
				App.State.HomePlanet.PowerSurgeBoni / 2,
				" %"
			}), GuiBase.progressBg, GuiBase.progressFgGreen);
			GUI.Label(new Rect(GuiBase.Width(230f), GuiBase.Height((float)marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), new GUIContent("Clones", "All clones here will be also used for UB-Fights if you start a fight."), labelStyle);
			GUI.Label(new Rect(GuiBase.Width(340f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), string.Empty + App.State.HomePlanet.ShadowCloneCount.CommaFormatted, labelStyle);
			if (GUI.Button(new Rect(GuiBase.Width(450f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "+"))
			{
				App.State.HomePlanet.AddCloneCount(App.State.GameSettings.ClonesToAddCount);
			}
			if (GUI.Button(new Rect(GuiBase.Width(500f), GuiBase.Height((float)marginTop), GuiBase.Width(40f), GuiBase.Height(30f)), "-"))
			{
				App.State.HomePlanet.RemoveCloneCount(App.State.GameSettings.ClonesToAddCount);
			}
		}

		internal static void ScrollbarToZero()
		{
			PlanetUi.scrollBarsToZero = true;
		}

		private void showUBV2s(int marginTop, GUIStyle labelStyle)
		{
			using (List<UltimateBeingV2>.Enumerator enumerator = App.State.HomePlanet.UltimateBeingsV2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					UltimateBeingV2 being = enumerator.Current;
					if (!being.IsDefeated)
					{
						labelStyle.fontSize = GuiBase.FontSize(16);
						if (being.IsAvailable)
						{
							string text = being.Description;
							if (being.HPPercent < 100)
							{
								text = text + "\nHp left: " + being.HPPercent.ToGuiText(true) + " %";
							}
							GuiBase.CreateProgressBar(marginTop, being.HPPercent.Double / 100.0, being.Name, text, GuiBase.progressBg, GuiBase.progressFgRed);
							if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height((float)marginTop), GuiBase.Width(80f), GuiBase.Height(30f)), new GUIContent("Fight", string.Concat(new object[]
							{
								"If you win, you will receive ",
								being.Tier * 10,
								" God Power and your Multi from Ultimate Beings will increase by ",
								being.GetMultiplier(App.State.HomePlanet.UBMultiplier).ToGuiText(true),
								" %"
							}))))
							{
								if (App.State.CurrentHealth * 2 < App.State.MaxHealth || App.State.HomePlanet.ShadowCloneCount < 100000)
								{
									GuiBase.ShowToast("Your need at least 50% of your health and 100000 Clones on Powersurge to be able to start a fight.");
								}
								else
								{
									this.Battle = BattleUBV2.Instance;
									this.Battle.start(being);
								}
							}
							marginTop += 35;
							Creation creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == being.CreationNeeded);
							string str = string.Empty;
							if (creation != null)
							{
								str = "\n\nYou have " + creation.Count.GuiText + " x " + creation.Name;
							}
							GuiBase.CreateProgressBar(marginTop, being.CurrentCreationDuration.Double / being.CreationDuration.Double, being.CreationName, being.CreationDescription + str, GuiBase.progressBg, GuiBase.progressFgBlue);
							being.StopAtString = GUI.TextField(new Rect(GuiBase.Width(350f), GuiBase.Height((float)marginTop), GuiBase.Width(55f), GuiBase.Height(25f)), being.StopAtString);
							if (!being.isCreating)
							{
								if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height((float)marginTop), GuiBase.Width(80f), GuiBase.Height(30f)), new GUIContent("Create", "If you create a " + being.CreationName + ", you can't create anything else until you stop it.")))
								{
									being.isCreating = true;
									being.InitDuration(App.State);
									foreach (Creation current in App.State.AllCreations)
									{
										current.IsActive = false;
									}
								}
							}
							else if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height((float)marginTop), GuiBase.Width(80f), GuiBase.Height(30f)), "Stop"))
							{
								being.isCreating = false;
								being.GoBackToCreating();
							}
							GUI.Label(new Rect(GuiBase.Width(235f), GuiBase.Height((float)marginTop), GuiBase.Width(100f), GuiBase.Height(30f)), string.Empty + being.CreationCount.ToGuiText(true), labelStyle);
						}
						marginTop += 35;
					}
				}
			}
			labelStyle.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(GuiBase.Width(240f), GuiBase.Height((float)(marginTop - 5)), GuiBase.Width(200f), GuiBase.Height(30f)), (App.State.HomePlanet.PlanetMultiplier + 100).ToGuiText(true) + " %", labelStyle);
			labelStyle.fontStyle = FontStyle.Bold;
			labelStyle.fontSize = GuiBase.FontSize(18);
			GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height((float)(marginTop - 5)), GuiBase.Width(300f), GuiBase.Height(30f)), new GUIContent("Planet Multiplier", string.Concat(new string[]
			{
				"This multiplier multiplies directly Physical, Mystic, Battle and Creating. 100% is the base value.lowerTextMulti from Powersurge: ",
				App.State.HomePlanet.PowerSurgeMultiplier.ToGuiText(true),
				" %\nMulti from Ultimate Beings: ",
				App.State.HomePlanet.UBMultiplier.ToGuiText(true),
				" %"
			})), labelStyle);
			labelStyle.fontStyle = FontStyle.Normal;
		}

		private void FightUB()
		{
			int num = 10;
			int num2 = 10;
			GUIStyle style = GUI.skin.GetStyle("Label");
			GUI.BeginGroup(new Rect(GuiBase.Width(280f), GuiBase.Height(110f), GuiBase.Width(670f), GuiBase.Height(480f)));
			GUI.Box(new Rect(GuiBase.Width(10f), GuiBase.Height(0f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			string text = App.State.AvatarName;
			if (string.IsNullOrEmpty(text))
			{
				text = "Guest";
			}
			if (GUI.Button(new Rect(GuiBase.Width(20f), GuiBase.Height((float)num), GuiBase.Width(80f), GuiBase.Height(30f)), new GUIContent("Flee")))
			{
				this.Battle.IsFighting = false;
				GuiBase.ShowToast("You ran away! You gained a new title: Chicken God!");
				App.State.Title = "Chicken God";
				if (App.State.Avatar.IsFemale)
				{
					App.State.Title = "Chicken Goddess";
				}
				App.State.TitleGod = "nothing, but you were able to run away really fast.";
			}
			style.fontSize = GuiBase.FontSize(14);
			style.alignment = TextAnchor.UpperCenter;
			GUI.Label(new Rect(GuiBase.Width(385f), GuiBase.Height((float)(num + 35)), GuiBase.Width(185f), GuiBase.Height(30f)), "Damage Reduction: " + this.Battle.Being.DamageReduction.ToGuiText(true) + " %");
			style.fontStyle = FontStyle.Bold;
			style.fontSize = GuiBase.FontSize(16);
			GuiBase.CreateProgressBar(130, num + 1, 185f, 31f, this.Battle.PlayerHp.Double / App.State.MaxHealth.Double, text, this.Battle.PlayerHp.ToGuiText(true) + " / " + App.State.MaxHealth.ToGuiText(true), GuiBase.progressBg, GuiBase.progressFgRed);
			GuiBase.CreateProgressBar(130, num + 31, 185f, 31f, this.Battle.PlayerEnergy.Double / 1000.0, this.Battle.PlayerEnergy.ToGuiText(true) + " / 1000", "Your energy. You will recover 10 each turn.", GuiBase.progressBg, GuiBase.progressFgBlue);
			string str = this.Battle.Being.HPPercent.ToGuiText(false);
			if (this.Battle.Being.HPPercent < 0.01)
			{
				str = "Less than 0.01";
			}
			GuiBase.CreateProgressBar(385, num + 1, 185f, 31f, this.Battle.Being.HPPercent.Double / 100.0, this.Battle.Being.Name, "Health: " + str + " %", GuiBase.progressBg, GuiBase.progressFgRed);
			style.fontSize = GuiBase.FontSize(22);
			GUI.Label(new Rect(GuiBase.Width(325f), GuiBase.Height((float)num), GuiBase.Width(60f), GuiBase.Height(30f)), "VS");
			style.fontSize = GuiBase.FontSize(16);
			style.alignment = TextAnchor.UpperLeft;
			num += 80;
			GUI.Label(new Rect(GuiBase.Width((float)(num2 + 10)), GuiBase.Height((float)num), GuiBase.Width(150f), GuiBase.Height(45f)), "Current Turn: " + this.Battle.TurnCount);
			GUI.Label(new Rect(GuiBase.Width((float)(num2 + 160)), GuiBase.Height((float)num), GuiBase.Width(640f), GuiBase.Height(45f)), "Clones left: " + App.State.HomePlanet.ShadowCloneCount);
			style.fontStyle = FontStyle.Normal;
			num += 30;
			style.fontSize = GuiBase.FontSize(14);
			GUI.Label(new Rect(GuiBase.Width((float)(num2 + 430)), GuiBase.Height((float)num), GuiBase.Width(640f), GuiBase.Height(45f)), "Dodge Chance = " + this.Battle.DodgeChance + " %");
			num += 20;
			GUI.Label(new Rect(GuiBase.Width((float)(num2 + 10)), GuiBase.Height((float)num), GuiBase.Width(640f), GuiBase.Height(45f)), new GUIContent("Your Damage: " + this.Battle.Damage.ToGuiText(true) + " %", "The base damage multiplier is 100 + Physical Attack + training in 'Might'."));
			GUI.Label(new Rect(GuiBase.Width((float)(num2 + 220)), GuiBase.Height((float)num), GuiBase.Width(640f), GuiBase.Height(45f)), new GUIContent("Damage Reduction: " + this.Battle.DamageReduction.ToGuiText(true) + " %", "The base damage reduction is Mystic Defense + training in 'Might' / 10 with a maximum of 75%."));
			GUI.Label(new Rect(GuiBase.Width((float)(num2 + 430)), GuiBase.Height((float)num), GuiBase.Width(640f), GuiBase.Height(45f)), "Counter Chance = " + this.Battle.CounterChance + " %");
			num += 20;
			GUI.Label(new Rect(GuiBase.Width((float)(num2 + 430)), GuiBase.Height((float)num), GuiBase.Width(640f), GuiBase.Height(45f)), "Damage Reflect = " + this.Battle.DamageReflect);
			GUI.Label(new Rect(GuiBase.Width((float)(num2 + 220)), GuiBase.Height((float)num), GuiBase.Width(640f), GuiBase.Height(45f)), "God Speed: " + (this.Battle.GodSpeedModeDuration / 2).Floored + " turns");
			GUI.Label(new Rect(GuiBase.Width((float)(num2 + 10)), GuiBase.Height((float)num), GuiBase.Width(640f), GuiBase.Height(45f)), "Double Damage = " + this.Battle.DoubleUp);
			num += 40;
			GUI.Label(new Rect(GuiBase.Width((float)(num2 + 10)), GuiBase.Height((float)num), GuiBase.Width(640f), GuiBase.Height(130f)), this.Battle.InfoText);
			GUIStyle style2 = GUI.skin.GetStyle("Button");
			style2.fontSize = GuiBase.FontSize(13);
			num += 130;
			num2 = 20;
			int num3 = 0;
			foreach (SkillUB2 current in this.Battle.PlayerSkills)
			{
				if (GUI.Button(new Rect(GuiBase.Width((float)num2), GuiBase.Height((float)num), GuiBase.Width(152f), GuiBase.Height(30f)), new GUIContent(current.Name, current.Desc)))
				{
					this.Battle.NextTurn(current);
				}
				if (current.TypeEnum == SkillTypeUBV2.FocusedBreathing || current.TypeEnum == SkillTypeUBV2.IonioiHeroSummon)
				{
					num3++;
				}
				num2 += 161;
				num3++;
				if (num3 % 4 == 0)
				{
					num2 = 20;
					num += 35;
				}
			}
			GUI.EndGroup();
		}
	}
}
