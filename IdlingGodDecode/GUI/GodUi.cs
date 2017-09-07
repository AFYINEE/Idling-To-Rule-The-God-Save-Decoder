using Assets.Scripts.Data;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class GodUi : GuiBase
	{
		public static GodUi Instance = new GodUi();

		private Vector2 scrollPosition = Vector2.zero;

		private static bool scrollBarsToZero = false;

		private int marginTop = 20;

		public string countToBuy = "1";

		private static UltimateBeingV2 createdEvilCreation = null;

		public void Show()
		{
			if (GodUi.scrollBarsToZero)
			{
				this.scrollPosition = Vector2.zero;
				GodUi.scrollBarsToZero = false;
			}
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = GuiBase.FontSize(18);
			GUI.BeginGroup(new Rect(GuiBase.Width(280f), GuiBase.Height(110f), GuiBase.Width(670f), GuiBase.Height(480f)));
			GUI.Box(new Rect(GuiBase.Width(10f), GuiBase.Height(0f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			style.alignment = TextAnchor.UpperCenter;
			int num = 20;
			int num2 = 400;
			if (App.State.Statistic.HasStartedUltimatePetChallenge)
			{
				GUI.Label(new Rect(GuiBase.Width(0f), GuiBase.Height((float)num), GuiBase.Width(550f), GuiBase.Height(30f)), new GUIContent("Pet Power: " + App.State.Ext.GetTotalPetPower(false).ToGuiText(true), "The power of all pets who are available to fight gods combined."), style);
				GUI.Label(new Rect(GuiBase.Width(0f), GuiBase.Height((float)(num + 25)), GuiBase.Width(550f), GuiBase.Height(30f)), new GUIContent("Multi from gods: " + App.State.Ext.PetPowerMultiGods.ToGuiText(true), "This is a multiplier to the pet power and will multiply with 4 for each god defeated. This won't reset after rebirthing."), style);
				GUI.Label(new Rect(GuiBase.Width(0f), GuiBase.Height((float)(num + 50)), GuiBase.Width(550f), GuiBase.Height(30f)), new GUIContent("Multi from pet pills: " + App.State.Ext.PetPowerMultiCampaigns.ToGuiText(true), "You can find Pet Pills in item campaigns for additional multipliers. This won't reset after rebirthing."), style);
				GUI.Label(new Rect(GuiBase.Width(0f), GuiBase.Height((float)(num + 75)), GuiBase.Width(550f), GuiBase.Height(30f)), new GUIContent("Multi from monuments: " + App.State.Ext.PetPowerMultiMonuments().ToGuiText(true), "Your monuments will even give multipliers to your pets! Sadly, all of this is lost after rebirthing."), style);
				GUI.Label(new Rect(GuiBase.Width(0f), GuiBase.Height((float)(num + 100)), GuiBase.Width(550f), GuiBase.Height(30f)), new GUIContent("Total Pet Power: " + App.State.Ext.GetTotalPetPower(true).ToGuiText(true), "The total power your pets have to fight the gods. The damage you do is (total power / 3 - god defense) / 5000 * milliseconds. \nCriticals from TBS are also possible."), style);
				num += 115;
				int num3 = 20;
				int num4 = num;
				int num5 = 0;
				foreach (Pet current in App.State.Ext.AllPets)
				{
					if (current.CurrentHealth > 0 && !current.IsInCampaign)
					{
						GUI.Label(new Rect(GuiBase.Width((float)num3), GuiBase.Height((float)PetUi.GetPetTop(current.TypeEnum, num4)), GuiBase.Width(40f), GuiBase.Height(40f)), new GUIContent(current.Image, "Pets who are available for fighting against the gods. Pets with no health or who are in campaigns can't participate."));
						num5++;
						num4 += 5;
						if (num5 > App.State.Ext.AllPets.Count / 2)
						{
							num4 -= 10;
						}
						num3 += 20;
					}
				}
				num += 120;
				num2 -= 220;
			}
			GUIStyle style2 = GUI.skin.GetStyle("Button");
			style2.fontSize = GuiBase.FontSize(18);
			style.fontStyle = FontStyle.Bold;
			GUI.Label(new Rect(GuiBase.Width(25f), GuiBase.Height((float)num), GuiBase.Width(200f), GuiBase.Height(30f)), "God");
			GUI.Label(new Rect(GuiBase.Width(210f), GuiBase.Height((float)num), GuiBase.Width(200f), GuiBase.Height(30f)), "Unlocks", style);
			style.fontStyle = FontStyle.Normal;
			num += 35;
			if (App.State.PremiumBoni.TotalMightIsUnlocked && GUI.Button(new Rect(GuiBase.Width(520f), GuiBase.Height(10f), GuiBase.Width(140f), GuiBase.Height(30f)), "Unleash Might"))
			{
				if (SpecialFightUi.IsFighting)
				{
					GuiBase.ShowToast("Please finish your special fight first°");
				}
				else
				{
					int num6 = 0;
					foreach (Might current2 in App.State.AllMights)
					{
						if (current2.IsUsable && current2.UseCoolDown == 0L && current2.DurationLeft <= 0L)
						{
							current2.Unleash();
							num6++;
						}
					}
					if (num6 > 0)
					{
						GuiBase.ShowToast("Multi unleash!");
					}
					else
					{
						GuiBase.ShowToast("Skills are on cooldown, please wait a bit.");
					}
				}
			}
			this.scrollPosition = GuiBase.TouchScrollView(new Rect(GuiBase.Width(0f), GuiBase.Height((float)num), GuiBase.Width(650f), GuiBase.Height((float)num2)), this.scrollPosition, new Rect(0f, GuiBase.Height((float)num), GuiBase.Width(620f), GuiBase.Height((float)(this.marginTop - num))));
			if (this.marginTop < 390)
			{
				GUIStyle style3 = GUI.skin.GetStyle("scrollview");
				style3.normal.background = null;
			}
			this.marginTop = num;
			bool flag = true;
			for (int i = 0; i < App.State.AllCreations.Count; i++)
			{
				Creation creation = App.State.AllCreations[i];
				if (!creation.GodToDefeat.IsDefeated)
				{
					flag = false;
				}
			}
			if (App.State.PrinnyBaal.IsUnlocked && flag)
			{
				style.fontSize = GuiBase.FontSize(16);
				GuiBase.CreateProgressBar(this.marginTop, App.State.PrinnyBaal.getPercentOfHP(), App.State.PrinnyBaal.Name, App.State.PrinnyBaal.Description, GuiBase.progressBg, GuiBase.progressFgRed);
				if (App.State.PrinnyBaal.IsFighting)
				{
					GUI.Toggle(new Rect(GuiBase.Width(430f), GuiBase.Height((float)this.marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), true, "Fighting...", GUI.skin.GetStyle("Button"));
					style.alignment = TextAnchor.MiddleLeft;
					GUI.Label(new Rect(GuiBase.Width(35f), GuiBase.Height((float)(this.marginTop + 35)), GuiBase.Width(400f), GuiBase.Height(30f)), "Your damage /s: " + App.State.PrinnyBaal.DamageSec.ToGuiText(true), style);
					style.alignment = TextAnchor.UpperCenter;
					if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height((float)this.marginTop), GuiBase.Width(70f), GuiBase.Height(30f)), "Stop"))
					{
						GodUi.EnableCreating();
					}
					this.marginTop += 35;
				}
				else if (GUI.Button(new Rect(GuiBase.Width(430f), GuiBase.Height((float)this.marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), "Fight"))
				{
					App.State.PrinnyBaal.IsFighting = true;
					GodUi.DisableCreation();
				}
			}
			else
			{
				for (int j = 0; j < App.State.AllCreations.Count; j++)
				{
					Creation creation2 = App.State.AllCreations[j];
					if (!creation2.GodToDefeat.IsDefeated)
					{
						style.fontSize = GuiBase.FontSize(16);
						GuiBase.CreateProgressBar(this.marginTop, creation2.GodToDefeat.getPercentOfHP(), creation2.GodToDefeat.Name, creation2.Description, GuiBase.progressBg, GuiBase.progressFgRed);
						GUI.Label(new Rect(GuiBase.Width(210f), GuiBase.Height((float)this.marginTop), GuiBase.Width(200f), GuiBase.Height(30f)), creation2.Name, style);
						if (creation2.IsActive)
						{
							GUI.Toggle(new Rect(GuiBase.Width(430f), GuiBase.Height((float)this.marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), true, "Fighting...", GUI.skin.GetStyle("Button"));
							style.alignment = TextAnchor.MiddleLeft;
							GUI.Label(new Rect(GuiBase.Width(35f), GuiBase.Height((float)(this.marginTop + 35)), GuiBase.Width(400f), GuiBase.Height(30f)), "Your damage /s: " + creation2.GodToDefeat.DamageSec.ToGuiText(true), style);
							style.alignment = TextAnchor.UpperCenter;
							if (GUI.Button(new Rect(GuiBase.Width(550f), GuiBase.Height((float)this.marginTop), GuiBase.Width(70f), GuiBase.Height(30f)), "Stop"))
							{
								GodUi.EnableCreating();
							}
							this.marginTop += 35;
						}
						else if (GUI.Button(new Rect(GuiBase.Width(430f), GuiBase.Height((float)this.marginTop), GuiBase.Width(110f), GuiBase.Height(30f)), "Fight"))
						{
							bool flag2 = true;
							foreach (Creation current3 in App.State.AllCreations)
							{
								if (current3.TypeEnum < creation2.TypeEnum && !current3.GodToDefeat.IsDefeated)
								{
									flag2 = false;
								}
							}
							if (!flag2)
							{
								GuiBase.ShowToast("You need to defeat all previous gods first!");
							}
							else
							{
								App.State.PrinnyBaal.IsFighting = false;
								bool isActive = creation2.IsActive;
								GodUi.DisableCreation();
								creation2.IsActive = !isActive;
							}
						}
						this.marginTop += 35;
					}
				}
			}
			GUI.EndScrollView();
			GUI.EndGroup();
		}

		public static void DisableCreation()
		{
			foreach (Creation current in App.State.AllCreations)
			{
				current.IsActive = false;
			}
			foreach (UltimateBeingV2 current2 in App.State.HomePlanet.UltimateBeingsV2)
			{
				if (current2.isCreating)
				{
					GodUi.createdEvilCreation = current2;
				}
				current2.isCreating = false;
			}
		}

		public static void EnableCreating()
		{
			bool flag = false;
			if (GodUi.createdEvilCreation != null)
			{
				foreach (UltimateBeingV2 current in App.State.HomePlanet.UltimateBeingsV2)
				{
					if (current.Name.Equals(GodUi.createdEvilCreation.Name))
					{
						current.isCreating = true;
						flag = true;
					}
				}
				GodUi.createdEvilCreation = null;
			}
			foreach (Creation current2 in App.State.AllCreations)
			{
				current2.IsActive = false;
			}
			if (!flag)
			{
				Creation creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Shadow_clone);
				if (App.State.GameSettings.LastCreation != null)
				{
					creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == App.State.GameSettings.LastCreation.TypeEnum);
				}
				if (!creation.GodToDefeat.IsDefeated)
				{
					creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Shadow_clone);
				}
				if (creation != null)
				{
					creation.IsActive = true;
				}
			}
			App.State.PrinnyBaal.IsFighting = false;
		}

		internal static void ScrollbarToZero()
		{
			GodUi.scrollBarsToZero = true;
		}
	}
}
