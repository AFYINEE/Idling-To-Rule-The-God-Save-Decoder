using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
	public class FactoryModule
	{
		public ModuleType Type;

		public CDouble Level = 0;

		public CDouble MaxLevel = 0;

		public CDouble ShadowClones = 0;

		public long CurrentDuration;

		public List<Crystal> Crystals = new List<Crystal>();

		public readonly long BaseDuration = 600000L;

		public bool ShouldUpdateText = true;

		private string levelText;

		public string InfoText = string.Empty;

		private bool hasEnoughCreations;

		public string Name
		{
			get
			{
				return this.Type.ToString();
			}
		}

		public string LevelText
		{
			get
			{
				if (this.levelText == null || this.ShouldUpdateText)
				{
					this.levelText = this.Level.ToGuiText(true);
				}
				return this.levelText;
			}
		}

		public CDouble UpgradeCost
		{
			get
			{
				if (this.Type == ModuleType.God)
				{
					return 500 + 250 * this.MaxLevel;
				}
				if (this.Type == ModuleType.Ultimate)
				{
					return 300 + 150 * this.MaxLevel;
				}
				return 100 + 50 * this.MaxLevel;
			}
		}

		public CDouble ClonesNeeded
		{
			get
			{
				CDouble cDouble = this.Level * 10000;
				if (this.Type == ModuleType.Ultimate)
				{
					cDouble *= 3;
				}
				else if (this.Type == ModuleType.God)
				{
					cDouble *= 5;
				}
				return cDouble;
			}
		}

		public List<CreationCost> RequiredCreations
		{
			get
			{
				List<CreationCost> list = new List<CreationCost>();
				int num = 2000;
				if (this.Type == ModuleType.Ultimate)
				{
					num *= 3;
				}
				if (this.Type == ModuleType.God)
				{
					num *= 5;
				}
				list.Add(new CreationCost(num, Creation.CreationType.Light));
				list.Add(new CreationCost(num, Creation.CreationType.Air));
				list.Add(new CreationCost(num, Creation.CreationType.Water));
				return list;
			}
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", (int)this.Type);
			Conv.AppendValue(stringBuilder, "b", this.MaxLevel.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.ShadowClones.Serialize());
			Conv.AppendValue(stringBuilder, "d", this.CurrentDuration);
			Conv.AppendList<Crystal>(stringBuilder, this.Crystals, "e");
			Conv.AppendValue(stringBuilder, "f", this.Level.Serialize());
			return Conv.ToBase64(stringBuilder.ToString(), "FactoryModule");
		}

		internal static FactoryModule Deserialize(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("FactoryModule.FromString with empty value!");
				return new FactoryModule();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "FactoryModule");
			FactoryModule factoryModule = new FactoryModule();
			factoryModule.Type = (ModuleType)Conv.getIntFromParts(parts, "a");
			factoryModule.MaxLevel = new CDouble(Conv.getStringFromParts(parts, "b"));
			factoryModule.ShadowClones = Conv.getCDoubleFromParts(parts, "c", false);
			factoryModule.CurrentDuration = Conv.getLongFromParts(parts, "d");
			factoryModule.Crystals = new List<Crystal>();
			string stringFromParts = Conv.getStringFromParts(parts, "e");
			string[] array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (!string.IsNullOrEmpty(text))
				{
					factoryModule.Crystals.Add(Crystal.Deserialize(text));
				}
			}
			factoryModule.Level = new CDouble(Conv.getStringFromParts(parts, "f"));
			return factoryModule;
		}

		public void UpdateInfoText()
		{
			this.InfoText = "Produces " + this.Type + " Crystals. ";
			switch (this.Type)
			{
			case ModuleType.Physical:
				this.InfoText += "They can increase your physical and building speed.";
				break;
			case ModuleType.Mystic:
				this.InfoText += "They can increase your mystic and decrease the damage your defender clones will take.";
				break;
			case ModuleType.Battle:
				this.InfoText += "They can increase your battle and divinity production.";
				break;
			case ModuleType.Creation:
				this.InfoText += "They can increase your creation and creation count.";
				break;
			case ModuleType.Ultimate:
				this.InfoText += "They are like all 4 crystals above together.";
				break;
			case ModuleType.God:
				this.InfoText += "They can produce god power over time.";
				break;
			}
			if (this.Level > 0)
			{
				this.InfoText = this.InfoText + "\nYour current max level is " + this.MaxLevel.GuiText;
			}
			if (this.CurrentDuration != this.BaseDuration)
			{
				this.InfoText += "lowerTextYou still need:\n";
			}
			else
			{
				this.InfoText += "lowerTextYou need a total of:\n";
			}
			foreach (CreationCost current in this.RequiredCreations)
			{
				if (this.CurrentDuration == this.BaseDuration)
				{
					this.InfoText = string.Concat(new string[]
					{
						this.InfoText,
						(current.CountNeeded * this.BaseDuration * this.Level).GuiText,
						" ",
						current.TypeEnum.ToString(),
						"\n"
					});
				}
				else
				{
					this.InfoText = string.Concat(new string[]
					{
						this.InfoText,
						(current.CountNeeded * (this.BaseDuration - this.CurrentDuration) * this.Level).GuiText,
						" ",
						current.TypeEnum.ToString(),
						"\n"
					});
				}
			}
			this.InfoText = this.InfoText + "Time until finish: " + Conv.MsToGuiText(this.BaseDuration - this.CurrentDuration, true);
			if (this.ShadowClones == 0)
			{
				this.InfoText += "\nNo clones adjusted, so the production stands still.";
			}
			else if (this.ShadowClones > 0 && !this.hasEnoughCreations)
			{
				this.InfoText += "\nYou don't have enough creations, so the production stands still!";
			}
		}

		public string UpdateDuration(long ms, GameState state)
		{
			this.UpdateInfoText();
			if (this.ShadowClones > 0)
			{
				bool flag = true;
				using (List<CreationCost>.Enumerator enumerator = this.RequiredCreations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CreationCost cost = enumerator.Current;
						Creation creation = state.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
						CDouble leftSide = ms * cost.CountNeeded * this.Level;
						if (leftSide > creation.Count)
						{
							if (state.IsBuyUnlocked && state.GameSettings.AutoBuyForCrystal && creation.CanBuy)
							{
								CDouble leftSide2 = cost.CountNeeded * this.Level * (this.BaseDuration - this.CurrentDuration);
								CDouble cDouble = leftSide2 - creation.Count;
								CDouble rightSide = cDouble * creation.BuyCost * (120 - state.PremiumBoni.AutoBuyCostReduction) / 100;
								if (state.Money >= rightSide)
								{
									state.Money -= rightSide;
									creation.count += cDouble;
									state.Statistic.TotalMoneySpent += rightSide;
								}
								else
								{
									CDouble cDouble2 = leftSide - creation.Count;
									CDouble rightSide2 = cDouble2 * creation.BuyCost * (120 - state.PremiumBoni.AutoBuyCostReduction) / 100;
									if (state.Money >= rightSide2)
									{
										state.Money -= rightSide2;
										creation.count += cDouble2;
										state.Statistic.TotalMoneySpent += rightSide2;
									}
									else
									{
										flag = false;
									}
								}
							}
							else
							{
								flag = false;
							}
						}
					}
				}
				this.hasEnoughCreations = flag;
				if (flag)
				{
					using (List<CreationCost>.Enumerator enumerator2 = this.RequiredCreations.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							CreationCost cost = enumerator2.Current;
							Creation creation2 = state.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
							CDouble rightSide3 = ms * cost.CountNeeded * this.Level;
							creation2.count -= rightSide3;
						}
					}
					this.CurrentDuration += ms;
					double num = (double)this.CurrentDuration / (double)this.BaseDuration;
					int num2 = (int)num;
					if (num2 > 0)
					{
						this.CurrentDuration = (long)((num - (double)num2) * (double)this.BaseDuration);
						Crystal crystal = this.Crystals.FirstOrDefault((Crystal x) => x.Level == 1);
						if (crystal == null)
						{
							this.Crystals.Add(new Crystal
							{
								Type = this.Type,
								Level = 1,
								Count = this.Level * num2
							});
						}
						else
						{
							crystal.Count += this.Level * num2;
						}
						this.Crystals = (from x in this.Crystals
						orderby x.Level.ToInt()
						select x).ToList<Crystal>();
						return string.Concat(new object[]
						{
							"- ",
							this.Level * num2,
							" x ",
							this.Type.ToString(),
							" Crystal\n"
						});
					}
				}
				return string.Empty;
			}
			return string.Empty;
		}

		public void UpgradeCrystal(GameState state, Crystal crystalToUpgrade, CDouble moduleLevel, CDouble count)
		{
			Crystal newCrystal = crystalToUpgrade.Upgrade(state, moduleLevel, count);
			if (newCrystal.Count > 0)
			{
				Crystal crystal = this.Crystals.FirstOrDefault((Crystal x) => x.Level == newCrystal.Level);
				if (crystal == null)
				{
					this.Crystals.Add(newCrystal);
				}
				else
				{
					crystal.Count += newCrystal.Count;
				}
			}
			List<Crystal> list = new List<Crystal>();
			foreach (Crystal current in this.Crystals)
			{
				if (current.Count > 0)
				{
					list.Add(current);
				}
			}
			this.Crystals = (from x in list
			orderby x.Level.ToInt()
			select x).ToList<Crystal>();
		}

		public void AddCrystal(Crystal crystal, CDouble count)
		{
			Crystal crystal2 = this.Crystals.FirstOrDefault((Crystal x) => x.Level == crystal.Level);
			if (crystal2 == null)
			{
				crystal.Count = 1;
				this.Crystals.Add(crystal);
			}
			else
			{
				crystal2.Count += count;
			}
			this.Crystals = (from x in this.Crystals
			orderby x.Level.ToInt()
			select x).ToList<Crystal>();
		}

		public bool Upgrade(ref CDouble availableEnergy)
		{
			availableEnergy.Round();
			if (availableEnergy >= this.UpgradeCost)
			{
				availableEnergy -= this.UpgradeCost;
				this.MaxLevel = ++this.MaxLevel;
				return true;
			}
			return false;
		}

		public void ChangeLevel(bool increase)
		{
			CDouble level = this.Level;
			this.MaxLevel.Round();
			this.Level.Round();
			if (increase && this.MaxLevel > this.Level)
			{
				this.Level = ++this.Level;
			}
			else if (!increase && this.Level > 1)
			{
				this.Level = --this.Level;
			}
			CDouble leftSide = level / this.Level;
			this.CurrentDuration = (leftSide * this.CurrentDuration).ToLong();
		}

		public void AddNeededClones()
		{
			this.RemoveAllClones();
			CDouble clonesNeeded = this.ClonesNeeded;
			CDouble availableClones = App.State.GetAvailableClones(false);
			if (availableClones >= clonesNeeded)
			{
				App.State.Clones.UseShadowClones(clonesNeeded);
				this.ShadowClones += clonesNeeded;
				this.ShouldUpdateText = true;
			}
			else
			{
				GuiBase.ShowToast("You need " + clonesNeeded.GuiText + " clones!");
			}
		}

		public void RemoveAllClones()
		{
			App.State.Clones.RemoveUsedShadowClones(this.ShadowClones);
			this.ShadowClones = 0;
			this.ShouldUpdateText = true;
		}
	}
}
