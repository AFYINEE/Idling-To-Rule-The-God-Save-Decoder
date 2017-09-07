using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
	public class UltimateBeingV2
	{
		public int StopAt;

		public string stopAtString = "0";

		public string Name = string.Empty;

		public Creation.CreationType CreationNeeded;

		public string CreationName = string.Empty;

		public string CreationDescriptionBase = string.Empty;

		public string CreationDescription = string.Empty;

		public CDouble CurrentCreationDuration = 0;

		public CDouble CreationDuration = 0;

		public CDouble CreationLoss = 0;

		public CDouble CreationCount = 0;

		public bool isCreating;

		public bool IsDefeated;

		public CDouble HPPercent
		{
			get;
			set;
		}

		public int TimesDefeated
		{
			get;
			set;
		}

		public int Tier
		{
			get;
			set;
		}

		public bool IsAvailable
		{
			get;
			set;
		}

		public CDouble DamageReduction
		{
			get;
			set;
		}

		public CDouble BaseDamage
		{
			get
			{
				return this.Tier * 20;
			}
		}

		public CDouble BaseCloneDamage
		{
			get
			{
				return this.Tier * 100000;
			}
		}

		public string StopAtString
		{
			get
			{
				return this.stopAtString;
			}
			set
			{
				try
				{
					if (!string.IsNullOrEmpty(value) && value.StartsWith("-"))
					{
						value = "0";
					}
					int.TryParse(value, out this.StopAt);
					this.stopAtString = this.StopAt.ToString();
				}
				catch (Exception)
				{
					this.stopAtString = "0";
					this.StopAt = 0;
				}
			}
		}

		public string SkillName1
		{
			get
			{
				if (this.Tier == 1)
				{
					return "Sweet Tongue";
				}
				if (this.Tier == 2)
				{
					return "Judge Laser";
				}
				if (this.Tier == 3)
				{
					return "Magmacalypse";
				}
				if (this.Tier == 4)
				{
					return "Atomification";
				}
				if (this.Tier == 5)
				{
					return "Lazy Punch";
				}
				return string.Empty;
			}
		}

		public string SkillName2
		{
			get
			{
				if (this.Tier == 1)
				{
					return "Big Hunger";
				}
				if (this.Tier == 2)
				{
					return "Gods Judgment";
				}
				if (this.Tier == 3)
				{
					return "Burning Hell";
				}
				if (this.Tier == 4)
				{
					return "Ragnarok";
				}
				if (this.Tier == 5)
				{
					return "Idle Rule";
				}
				return string.Empty;
			}
		}

		public string SkillName3
		{
			get
			{
				if (this.Tier == 1)
				{
					return "Mighty Stomp";
				}
				if (this.Tier == 2)
				{
					return "Final Judgement";
				}
				if (this.Tier == 3)
				{
					return "Supernova";
				}
				if (this.Tier == 4)
				{
					return "Divine Nothingness";
				}
				if (this.Tier == 5)
				{
					return "Ultra Epic Idle Finger Snap";
				}
				return string.Empty;
			}
		}

		public string Description
		{
			get
			{
				if (this.Tier == 1)
				{
					return "The real planet eater! It seems like until now you only defeated his shadow while he is in another dimension. \nNow you found a way into his dimension and can fight him. But be warned... his power is also a different dimension.";
				}
				if (this.Tier == 2)
				{
					return "The real Godly Tribunal! Just as the planet eater he is in a different dimension and a lot more powerful than the one you fought until now...";
				}
				if (this.Tier == 3)
				{
					return "You know it already. This is the real Living Sun! Stronger than anything you faced before!";
				}
				if (this.Tier == 4)
				{
					return "Not good. You thought, the living sun was hard? Try to fight the real God Above All now!";
				}
				if (this.Tier == 5)
				{
					return "Okay now everything is over. You just can't win this, so don't even try.";
				}
				return string.Empty;
			}
		}

		public static List<UltimateBeingV2> Initial
		{
			get
			{
				List<UltimateBeingV2> list = new List<UltimateBeingV2>();
				for (int i = 0; i < 5; i++)
				{
					list.Add(new UltimateBeingV2(i + 1));
				}
				return list;
			}
		}

		public bool PowerUp
		{
			get;
			internal set;
		}

		public UltimateBeingV2(int tier)
		{
			this.HPPercent = 100;
			this.Tier = tier;
			if (this.Tier == 1)
			{
				this.Name = "Planet Eater V2";
				this.CreationNeeded = Creation.CreationType.Planet;
				this.CreationName = "Poison Planet";
				this.CreationDescriptionBase = "It looks like a normal planet, but it is poisoned. Maybe you have some need for that...";
			}
			else if (this.Tier == 2)
			{
				this.Name = "Godly Tribunal V2";
				this.CreationNeeded = Creation.CreationType.Earthlike_planet;
				this.CreationName = "Draining Planet";
				this.CreationDescriptionBase = "Almost like a normal earthlike planet, except all the plants on this planet are draining plants. It drains the power of anybody who comes near it.";
			}
			else if (this.Tier == 3)
			{
				this.Name = "Living Sun V2";
				this.CreationNeeded = Creation.CreationType.Sun;
				this.CreationName = "Icy Sun";
				this.CreationDescriptionBase = "It just looks like a white star and is so cold, that the cold feels like it burns. It is even colder than the minimum temperature.";
			}
			else if (this.Tier == 4)
			{
				this.Name = "God Above All V2";
				this.CreationNeeded = Creation.CreationType.Solar_system;
				this.CreationName = "Trap System";
				this.CreationDescriptionBase = "All stars and planets in this solar system are set to explode as soon as you command it.";
			}
			else if (this.Tier == 5)
			{
				this.Name = "ITRTG V2";
				this.CreationNeeded = Creation.CreationType.Universe;
				this.CreationName = "Fakeverse";
				this.CreationDescriptionBase = "It looks just like a normal universe. Nobody but you might find out the difference. A good present to fool someone with it.";
			}
			this.CreationDescription = this.CreationDescriptionBase;
		}

		public void InitDuration(GameState state)
		{
			Creation.UpdateDurationMulti(state);
			Creation creation = state.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == this.CreationNeeded);
			this.CreationDuration = creation.DurationInMS;
			this.CreationLoss = creation.CreatingPowerGain() * 5;
		}

		public void UpdateDuration(long ms)
		{
			if (App.State == null)
			{
				return;
			}
			if (this.CreationDuration == 0)
			{
				this.InitDuration(App.State);
			}
			if (this.isCreating)
			{
				if (App.State.CreatingPowerBase <= 0)
				{
					GuiBase.ShowToast("You need more creating power to create a " + this.CreationName);
					this.isCreating = false;
					this.GoBackToCreating();
					return;
				}
				if (this.CurrentCreationDuration <= 0)
				{
					Creation creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == this.CreationNeeded);
					if (creation.count == 0)
					{
						this.isCreating = false;
						GuiBase.ShowToast("You need one " + creation.Name + " to create a " + this.CreationName);
						this.GoBackToCreating();
						return;
					}
					Creation expr_EB = creation;
					expr_EB.count = --expr_EB.count;
				}
				CDouble rightSide = App.State.CreationSpeed(ms);
				this.CurrentCreationDuration += rightSide;
				CDouble cDouble = (this.CreationDuration - this.CurrentCreationDuration) * 30 / rightSide;
				string text = "Time to finish: " + Conv.MsToGuiText(cDouble.ToLong(), true);
				CDouble cDouble2 = this.CreationLoss / this.CreationDuration * rightSide;
				App.State.CreatingPowerBase -= cDouble2;
				if (App.State.CreatingPowerBase < 0)
				{
					App.State.CreatingPowerBase = 0;
				}
				CDouble cDouble3 = cDouble2 * App.State.Multiplier.CurrentMultiCreating * (100 + App.State.PremiumBoni.GpBoniCreating);
				cDouble3 = App.State.AdditionalMultis(cDouble3);
				if (this.CreationDuration > 0 && this.CurrentCreationDuration > this.CreationDuration)
				{
					this.CurrentCreationDuration = 0;
					this.CreationCount = ++this.CreationCount;
					if (this.CreationCount >= this.StopAt && this.StopAt != 0)
					{
						this.isCreating = false;
						this.GoBackToCreating();
					}
				}
				this.CreationDescription = string.Concat(new string[]
				{
					this.CreationDescriptionBase,
					"\nYou will lose ",
					cDouble3.ToGuiText(true),
					" Creating every second while creating this.\n",
					text
				});
			}
			if (this.HPPercent < 100)
			{
				this.HPPercent += 1.67E-05 * (double)ms;
			}
		}

		public CDouble GetMultiplier(CDouble currentMultiplier)
		{
			return currentMultiplier * this.Tier;
		}

		internal void GoBackToCreating()
		{
			Creation creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Shadow_clone);
			foreach (Creation current in App.State.AllCreations)
			{
				current.IsActive = false;
			}
			if (App.State.GameSettings.LastCreation != null)
			{
				creation = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == App.State.GameSettings.LastCreation.TypeEnum);
			}
			if (creation != null)
			{
				creation.IsActive = true;
			}
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.HPPercent.Serialize());
			Conv.AppendValue(stringBuilder, "b", this.TimesDefeated.ToString());
			Conv.AppendValue(stringBuilder, "c", this.Tier.ToString());
			Conv.AppendValue(stringBuilder, "d", this.IsAvailable.ToString());
			Conv.AppendValue(stringBuilder, "e", this.CreationCount.Serialize());
			Conv.AppendValue(stringBuilder, "f", this.isCreating.ToString());
			Conv.AppendValue(stringBuilder, "g", this.CurrentCreationDuration.Serialize());
			Conv.AppendValue(stringBuilder, "h", this.IsDefeated.ToString());
			Conv.AppendValue(stringBuilder, "i", this.StopAt);
			return Conv.ToBase64(stringBuilder.ToString(), "UltimateBeingV2");
		}

		internal static UltimateBeingV2 FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				return new UltimateBeingV2(1);
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "UltimateBeingV2");
			UltimateBeingV2 ultimateBeingV = new UltimateBeingV2(Conv.getIntFromParts(parts, "c"));
			ultimateBeingV.HPPercent = Conv.getCDoubleFromParts(parts, "a", false);
			ultimateBeingV.TimesDefeated = Conv.getIntFromParts(parts, "b");
			ultimateBeingV.IsAvailable = Conv.getStringFromParts(parts, "d").ToLower().Equals("true");
			ultimateBeingV.CreationCount = Conv.getCDoubleFromParts(parts, "e", false);
			ultimateBeingV.isCreating = Conv.getStringFromParts(parts, "f").ToLower().Equals("true");
			ultimateBeingV.CurrentCreationDuration = Conv.getCDoubleFromParts(parts, "g", false);
			ultimateBeingV.IsDefeated = Conv.getStringFromParts(parts, "h").ToLower().Equals("true");
			ultimateBeingV.StopAt = Conv.getIntFromParts(parts, "i");
			ultimateBeingV.StopAtString = ultimateBeingV.StopAt.ToString();
			return ultimateBeingV;
		}
	}
}
