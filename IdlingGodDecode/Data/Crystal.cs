using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using System;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class Crystal
	{
		public ModuleType Type;

		public CDouble Level = 0;

		public CDouble Count = 0;

		public Texture2D image;

		public string EquipDescription = string.Empty;

		public Texture2D Image
		{
			get
			{
				if (this.image == null)
				{
					string path = "Gui/crystal_" + this.Type.ToString().ToLower();
					this.image = (Texture2D)Resources.Load(path, typeof(Texture2D));
				}
				return this.image;
			}
		}

		public int MaxLevel
		{
			get
			{
				if (this.Type == ModuleType.God)
				{
					return 15;
				}
				if (this.Type == ModuleType.Ultimate)
				{
					return 20;
				}
				return 30;
			}
		}

		public string Description
		{
			get
			{
				CDouble cDouble = this.Level * 100;
				CDouble cDouble2 = 27000 / this.Level;
				CDouble cDouble3 = this.Level * 3;
				if (cDouble3 > 95)
				{
					cDouble3 = 95;
				}
				string text = string.Empty;
				if (this.Type == ModuleType.Physical)
				{
					text = string.Concat(new object[]
					{
						"Increases your Physical by ",
						cDouble.GuiText,
						"%, and your building speed is increased by ",
						this.Level * 3,
						"%"
					});
				}
				if (this.Type == ModuleType.Mystic)
				{
					text = string.Concat(new object[]
					{
						"Increases your Mystic by ",
						cDouble.GuiText,
						"%, and your defender clones will take ",
						cDouble3,
						"% less damage from incoming UB attacks"
					});
				}
				if (this.Type == ModuleType.Battle)
				{
					text = string.Concat(new object[]
					{
						"Increases your Battle by ",
						cDouble.GuiText,
						"%, and the divinity you earn from monsters and the divinity generator is increased by ",
						this.Level * 3,
						"%"
					});
				}
				if (this.Type == ModuleType.Creation)
				{
					text = "Increases your Creation by " + cDouble.GuiText + "%, and your creation count is increased by " + this.Level.GuiText;
				}
				if (this.Type == ModuleType.Ultimate)
				{
					text = string.Concat(new object[]
					{
						"Increases your Physical, Mystic, Battle and Creation by ",
						cDouble.GuiText,
						"%, your building speed by ",
						this.Level * 3,
						"%, your defender clones will take ",
						cDouble3,
						"% less damage from incoming UB attacks, divinity you earn from monsters and the divinity generator is increased by ",
						this.Level * 3,
						"%, and your creation count is increased by ",
						this.Level.GuiText
					});
				}
				if (this.Type == ModuleType.God)
				{
					text = "You will earn one god power every " + Conv.MsToGuiText(cDouble2.ToLong(), false);
				}
				return string.Concat(new object[]
				{
					text,
					" if this crystal is equipped.\nThe maximum level is ",
					this.MaxLevel,
					"."
				});
			}
		}

		public void UpdateEquipDescription(GameState state)
		{
			this.EquipDescription = string.Concat(new object[]
			{
				this.Type,
				" Crystal grade ",
				this.Level.GuiText,
				"\n",
				this.Description.Replace(" if this crystal is equipped", string.Empty)
			});
			if (this.Type == ModuleType.God)
			{
				this.EquipDescription = this.EquipDescription + "lowerTextTime until your next god power: " + Conv.MsToGuiText(state.PremiumBoni.CrystalGPTimeCurrent.ToLong(), true) + "\nThe timer and the god power you will receive continues while you are offline, but the offline gain is capped at a total of 10 god power.";
			}
		}

		public CDouble UpgradeChance(GameState state, CDouble moduleLevel, bool maxChance = false)
		{
			if (moduleLevel > 25 && (this.Type == ModuleType.God || this.Type == ModuleType.Ultimate))
			{
				moduleLevel = 25;
			}
			CDouble cDouble = 105 - this.Level * 5 + moduleLevel;
			if (this.Type == ModuleType.Ultimate)
			{
				cDouble -= 40;
			}
			else if (this.Type == ModuleType.God)
			{
				cDouble -= 60;
			}
			if (state.PremiumBoni.HasCrystalImprovement)
			{
				cDouble += 25;
			}
			if (cDouble > 95 && !maxChance)
			{
				cDouble = 95;
			}
			if (cDouble < 5)
			{
				cDouble = 5;
			}
			return cDouble;
		}

		public int GetOptimalCount(GameState state, CDouble moduleLevel)
		{
			int num = this.UpgradeChance(state, moduleLevel, false).ToInt();
			int num2;
			if (num % 50 == 0)
			{
				num2 = 2;
			}
			else if (num % 25 == 0)
			{
				num2 = 4;
			}
			else if (num % 20 == 0)
			{
				num2 = 5;
			}
			else if (num % 10 == 0)
			{
				num2 = 10;
			}
			else if (num % 5 == 0)
			{
				num2 = 20;
			}
			else if (num % 4 == 0)
			{
				num2 = 25;
			}
			else if (num % 2 == 0)
			{
				num2 = 50;
			}
			else
			{
				num2 = 100;
			}
			int num3 = this.Count.ToInt() / num2 * num2;
			if (num3 > 0)
			{
				num2 = num3;
			}
			return num2;
		}

		public CDouble GetCrystalsAfterUpgrade(GameState state, CDouble moduleLevel, CDouble countToUse)
		{
			int value = this.UpgradeChance(state, moduleLevel, false).ToInt();
			return (countToUse * value / 100).Floor();
		}

		public CDouble GetMinimumNeeded(GameState state, CDouble moduleLevel)
		{
			int num = this.UpgradeChance(state, moduleLevel, false).ToInt();
			int num2 = 2;
			while (num2 * num / 100 == 0)
			{
				num2++;
			}
			return num2;
		}

		public CDouble GetCountKeepLeftovers(GameState state, CDouble moduleLevel, CDouble count)
		{
			int value = this.UpgradeChance(state, moduleLevel, false).ToInt();
			CDouble cDouble = (count * value / 100).Floor();
			cDouble = (cDouble / value * 100).Ceiling();
			if (cDouble > count)
			{
				cDouble = count;
			}
			return cDouble;
		}

		public Crystal Upgrade(GameState state, CDouble moduleLevel, CDouble count)
		{
			if (count > this.Count)
			{
				count = this.Count;
			}
			int num = this.UpgradeChance(state, moduleLevel, false).ToInt();
			CDouble crystalsAfterUpgrade = this.GetCrystalsAfterUpgrade(state, moduleLevel, count);
			this.Count -= count;
			this.Count.Round();
			if (crystalsAfterUpgrade == 0)
			{
				GuiBase.ShowToast("You failed to upgrade any crystal!");
			}
			return new Crystal
			{
				Type = this.Type,
				Count = crystalsAfterUpgrade,
				Level = this.Level + 1
			};
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", (int)this.Type);
			Conv.AppendValue(stringBuilder, "b", this.Level.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.Count.Serialize());
			return Conv.ToBase64(stringBuilder.ToString(), "Crystal");
		}

		internal static Crystal Deserialize(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("Crystal.FromString with empty value!");
				return new Crystal();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "Crystal");
			return new Crystal
			{
				Type = (ModuleType)Conv.getIntFromParts(parts, "a"),
				Level = new CDouble(Conv.getStringFromParts(parts, "b")),
				Count = Conv.getCDoubleFromParts(parts, "c", false)
			};
		}
	}
}
