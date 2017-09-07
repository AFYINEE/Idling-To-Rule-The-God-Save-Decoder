using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
	public class CrystalFactory
	{
		public CDouble DefenderClones = 0;

		public List<FactoryModule> AllModules = new List<FactoryModule>();

		public CDouble Energy = 0;

		public long LastUBAttack;

		public List<Crystal> EquippedCrystals = new List<Crystal>();

		public long TimeUntilAttack;

		public StringBuilder DefenderFightsText = new StringBuilder();

		public CrystalFactory()
		{
			this.AllModules = this.Initial();
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.DefenderClones.Serialize());
			Conv.AppendList<FactoryModule>(stringBuilder, this.AllModules, "b");
			Conv.AppendValue(stringBuilder, "c", this.Energy.Serialize());
			Conv.AppendValue(stringBuilder, "d", this.LastUBAttack);
			Conv.AppendList<Crystal>(stringBuilder, this.EquippedCrystals, "e");
			return Conv.ToBase64(stringBuilder.ToString(), "CrystalFactory");
		}

		internal static CrystalFactory Deserialize(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("CrystalFactory.FromString with empty value!");
				return new CrystalFactory();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "CrystalFactory");
			CrystalFactory crystalFactory = new CrystalFactory();
			crystalFactory.DefenderClones = new CDouble(Conv.getStringFromParts(parts, "a"));
			crystalFactory.AllModules = new List<FactoryModule>();
			string stringFromParts = Conv.getStringFromParts(parts, "b");
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
					crystalFactory.AllModules.Add(FactoryModule.Deserialize(text));
				}
			}
			if (crystalFactory.AllModules.Count == 0)
			{
				crystalFactory.AllModules = crystalFactory.Initial();
			}
			crystalFactory.Energy = new CDouble(Conv.getStringFromParts(parts, "c"));
			crystalFactory.LastUBAttack = Conv.getLongFromParts(parts, "d");
			crystalFactory.EquippedCrystals = new List<Crystal>();
			stringFromParts = Conv.getStringFromParts(parts, "e");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array3 = array;
			for (int j = 0; j < array3.Length; j++)
			{
				string text2 = array3[j];
				if (!string.IsNullOrEmpty(text2))
				{
					crystalFactory.EquippedCrystals.Add(Crystal.Deserialize(text2));
				}
			}
			return crystalFactory;
		}

		private List<FactoryModule> Initial()
		{
			return new List<FactoryModule>
			{
				new FactoryModule
				{
					Type = ModuleType.Physical
				},
				new FactoryModule
				{
					Type = ModuleType.Mystic
				},
				new FactoryModule
				{
					Type = ModuleType.Battle
				},
				new FactoryModule
				{
					Type = ModuleType.Creation
				},
				new FactoryModule
				{
					Type = ModuleType.Ultimate
				},
				new FactoryModule
				{
					Type = ModuleType.God
				}
			};
		}

		public void AddCloneCount(CDouble count)
		{
			CDouble availableClones = App.State.GetAvailableClones(false);
			if (count > availableClones)
			{
				count = availableClones;
			}
			if (count < 0)
			{
				count = 0;
			}
			App.State.Clones.UseShadowClones(count);
			this.DefenderClones += count;
			this.DefenderClones.Round();
		}

		public void RemoveCloneCount(CDouble count)
		{
			if (this.DefenderClones <= count)
			{
				count = this.DefenderClones;
			}
			this.DefenderClones -= count;
			App.State.Clones.RemoveUsedShadowClones(count.ToInt());
			this.DefenderClones.Round();
		}

		public string UpdateDuration(long ms, GameState state, bool isOfflineCalc = false)
		{
			if (!state.HomePlanet.IsCreated || !state.IsCrystalFactoryAvailable)
			{
				return null;
			}
			this.LastUBAttack += ms;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (FactoryModule current in this.AllModules)
			{
				stringBuilder.Append(current.UpdateDuration(ms, state));
			}
			foreach (Crystal current2 in this.EquippedCrystals)
			{
				current2.UpdateEquipDescription(state);
			}
			if (!isOfflineCalc)
			{
				this.CheckForUbAttack();
			}
			return stringBuilder.ToString();
		}

		public void CheckForUbAttack()
		{
			if (App.State.Statistic.HasStarted1kChallenge)
			{
				return;
			}
			long num = 0L;
			long num2 = 600000L;
			if (App.State.Statistic.NoRbChallengesFinished > 0)
			{
				int num3 = App.State.Statistic.NoRbChallengesFinished.ToInt();
				if (num3 > 20)
				{
					num3 = 20;
				}
				num2 = num2 * (long)(100 - num3) / 100L;
			}
			UltimateBeing ultimateBeing = null;
			List<UltimateBeing> list = (from x in App.State.HomePlanet.UltimateBeings
			orderby x.Tier descending
			select x).ToList<UltimateBeing>();
			foreach (UltimateBeing current in list)
			{
				if (current.IsAvailable)
				{
					if (current.HPPercent > 0.0)
					{
						long num4 = current.TimeUntilComeBack + num2 - current.TimeUntilComeBackBase;
						if (num == 0L || num > num4)
						{
							num = num4;
						}
						ultimateBeing = current;
					}
					else if (num == 0L || num > current.TimeUntilComeBack + num2)
					{
						num = current.TimeUntilComeBack + num2;
					}
				}
			}
			if (this.LastUBAttack < num2 && num < num2 - this.LastUBAttack)
			{
				num = num2 - this.LastUBAttack;
			}
			if (num <= 0L && ultimateBeing != null)
			{
				string text = ultimateBeing.Fight(App.State, 0, 0, 0, this.DefenderClones, true);
				this.DefenderClones.Round();
				if (text.Contains("lost the fight"))
				{
					int value = ultimateBeing.Tier * 10;
					CDouble cDouble = this.Energy * value / 100;
					this.Energy -= cDouble;
					if (cDouble > 0)
					{
						text = text + "\nHe stole " + cDouble.GuiText + " energy!\n";
					}
					bool flag = false;
					string text2 = "He also stole ";
					if (cDouble > 0)
					{
						text2 = "He stole ";
					}
					foreach (FactoryModule current2 in this.AllModules)
					{
						Crystal crystal = current2.Crystals.FirstOrDefault((Crystal x) => x.Level == 1);
						if (crystal != null && crystal.Count > 0)
						{
							flag = true;
							CDouble cDouble2 = ultimateBeing.Tier;
							if (cDouble2 > crystal.Count)
							{
								cDouble2 = crystal.Count;
							}
							crystal.Count -= cDouble2;
							text2 = string.Concat(new object[]
							{
								text2,
								cDouble2.GuiText,
								" ",
								crystal.Type
							});
							if (cDouble2 == 1)
							{
								text2 += " Crystal, ";
							}
							else
							{
								text2 += " Crystals, ";
							}
						}
					}
					if (flag)
					{
						text2 = Conv.ReplaceLastOccurrence(text2, "Crystals, ", "Crystals of grade 1!");
						text2 = Conv.ReplaceLastOccurrence(text2, "Crystal, ", "Crystal of grade 1!");
						text += text2;
					}
					if (!flag && cDouble == 0)
					{
						text = ultimateBeing.Name + " tried to attack, but there was nothing of interest on your planet, so he left.\n\n";
					}
				}
				this.DefenderFightsText.Append(text + "\n\n");
				this.LastUBAttack = 0L;
			}
			this.TimeUntilAttack = num;
		}
	}
}
