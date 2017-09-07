using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Data
{
	public class CreationCost
	{
		public CDouble CountNeeded;

		public Creation.CreationType TypeEnum;

		public CreationCost(CDouble count, Creation.CreationType type)
		{
			this.CountNeeded = count;
			this.TypeEnum = type;
		}

		public static bool HasCreations(GameState state, CreationCost cost, bool autobuy)
		{
			Creation creation = state.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
			if (!(cost.CountNeeded > creation.Count))
			{
				return true;
			}
			if (!autobuy || !creation.CanBuy)
			{
				return false;
			}
			CDouble cDouble = cost.CountNeeded - creation.Count;
			CDouble rightSide = cDouble * creation.BuyCost * (120 - state.PremiumBoni.AutoBuyCostReduction) / 100;
			if (state.Money >= rightSide)
			{
				state.Money -= rightSide;
				creation.count += cDouble;
				state.Statistic.TotalMoneySpent += rightSide;
				return true;
			}
			return false;
		}

		public static List<CreationCost> RequiredCreations(Creation.CreationType creationType, long count = 0L, bool offlineCalc = false)
		{
			List<CreationCost> list = new List<CreationCost>();
			switch (creationType)
			{
			case Creation.CreationType.Soil:
				list.Add(new CreationCost(1, Creation.CreationType.Stone));
				break;
			case Creation.CreationType.Air:
				list.Add(new CreationCost(2, Creation.CreationType.Light));
				break;
			case Creation.CreationType.Water:
				list.Add(new CreationCost(3, Creation.CreationType.Air));
				break;
			case Creation.CreationType.Plant:
				list.Add(new CreationCost(2, Creation.CreationType.Soil));
				list.Add(new CreationCost(2, Creation.CreationType.Water));
				break;
			case Creation.CreationType.Tree:
				list.Add(new CreationCost(5, Creation.CreationType.Soil));
				list.Add(new CreationCost(3, Creation.CreationType.Water));
				break;
			case Creation.CreationType.Fish:
				list.Add(new CreationCost(10, Creation.CreationType.Water));
				list.Add(new CreationCost(5, Creation.CreationType.Plant));
				break;
			case Creation.CreationType.Animal:
				list.Add(new CreationCost(15, Creation.CreationType.Water));
				list.Add(new CreationCost(9, Creation.CreationType.Plant));
				list.Add(new CreationCost(3, Creation.CreationType.Fish));
				break;
			case Creation.CreationType.Human:
				list.Add(new CreationCost(100, Creation.CreationType.Water));
				list.Add(new CreationCost(25, Creation.CreationType.Plant));
				list.Add(new CreationCost(25, Creation.CreationType.Fish));
				list.Add(new CreationCost(15, Creation.CreationType.Animal));
				break;
			case Creation.CreationType.River:
				list.Add(new CreationCost(5000, Creation.CreationType.Water));
				break;
			case Creation.CreationType.Mountain:
				list.Add(new CreationCost(200000, Creation.CreationType.Stone));
				break;
			case Creation.CreationType.Forest:
				list.Add(new CreationCost(10000, Creation.CreationType.Tree));
				break;
			case Creation.CreationType.Village:
				list.Add(new CreationCost(5000, Creation.CreationType.Stone));
				list.Add(new CreationCost(5000, Creation.CreationType.Plant));
				list.Add(new CreationCost(200, Creation.CreationType.Human));
				list.Add(new CreationCost(1, Creation.CreationType.River));
				list.Add(new CreationCost(1, Creation.CreationType.Forest));
				break;
			case Creation.CreationType.Town:
				list.Add(new CreationCost(250000, Creation.CreationType.Stone));
				list.Add(new CreationCost(10000, Creation.CreationType.Plant));
				list.Add(new CreationCost(5000, Creation.CreationType.Human));
				list.Add(new CreationCost(1, Creation.CreationType.River));
				break;
			case Creation.CreationType.Ocean:
				list.Add(new CreationCost(30000000, Creation.CreationType.Water));
				list.Add(new CreationCost(5000000, Creation.CreationType.Plant));
				list.Add(new CreationCost(1000000, Creation.CreationType.Fish));
				list.Add(new CreationCost(500, Creation.CreationType.River));
				break;
			case Creation.CreationType.Nation:
				list.Add(new CreationCost(1000000, Creation.CreationType.Plant));
				list.Add(new CreationCost(100000, Creation.CreationType.Animal));
				list.Add(new CreationCost(100, Creation.CreationType.River));
				list.Add(new CreationCost(3, Creation.CreationType.Mountain));
				list.Add(new CreationCost(10, Creation.CreationType.Forest));
				list.Add(new CreationCost(15, Creation.CreationType.Town));
				break;
			case Creation.CreationType.Continent:
				list.Add(new CreationCost(1, Creation.CreationType.Ocean));
				list.Add(new CreationCost(5, Creation.CreationType.Nation));
				break;
			case Creation.CreationType.Weather:
				list.Add(new CreationCost(1000000000, Creation.CreationType.Air));
				list.Add(new CreationCost(100000000, Creation.CreationType.Water));
				list.Add(new CreationCost(5, Creation.CreationType.Ocean));
				list.Add(new CreationCost(1, Creation.CreationType.Continent));
				break;
			case Creation.CreationType.Sky:
				list.Add(new CreationCost(100000000, Creation.CreationType.Light));
				list.Add(new CreationCost(3000000000u, Creation.CreationType.Air));
				list.Add(new CreationCost(1, Creation.CreationType.Weather));
				break;
			case Creation.CreationType.Night:
				list.Add(new CreationCost(2, Creation.CreationType.Sky));
				break;
			case Creation.CreationType.Moon:
				list.Add(new CreationCost(150000000000L, Creation.CreationType.Stone));
				list.Add(new CreationCost(1, Creation.CreationType.Night));
				break;
			case Creation.CreationType.Planet:
				list.Add(new CreationCost(300000000000L, Creation.CreationType.Stone));
				list.Add(new CreationCost(1, Creation.CreationType.Moon));
				break;
			case Creation.CreationType.Earthlike_planet:
				list.Add(new CreationCost(100000000000L, Creation.CreationType.Air));
				list.Add(new CreationCost(10000000000L, Creation.CreationType.Soil));
				list.Add(new CreationCost(25000000000L, Creation.CreationType.Water));
				list.Add(new CreationCost(5000000000L, Creation.CreationType.Plant));
				list.Add(new CreationCost(1, Creation.CreationType.Planet));
				break;
			case Creation.CreationType.Sun:
				list.Add(new CreationCost(9999999999999L, Creation.CreationType.Light));
				break;
			case Creation.CreationType.Solar_system:
				list.Add(new CreationCost(100, Creation.CreationType.Planet));
				list.Add(new CreationCost(1, Creation.CreationType.Earthlike_planet));
				list.Add(new CreationCost(10, Creation.CreationType.Sun));
				break;
			case Creation.CreationType.Galaxy:
				list.Add(new CreationCost(5, Creation.CreationType.Solar_system));
				break;
			case Creation.CreationType.Universe:
				list.Add(new CreationCost(5, Creation.CreationType.Galaxy));
				break;
			}
			long num = 1L;
            try
            {
                if ((App.State != null && App.State.GameSettings.CreationToCreateCount > 1) || offlineCalc)
                {
                    if (offlineCalc || (count > 0L && count < (long)App.State.GameSettings.CreationToCreateCount))
                    {
                        num = count;
                    }
                    else if (App.State.GameSettings.CreationToCreateCount > 0)
                    {
                        num = (long)App.State.GameSettings.CreationToCreateCount;
                    }
                    long num2 = 100L;
                    long num3 = num;
                    long num4 = 0L;
                    if (num > 12L)
                    {
                        num3 = 12L;
                        num4 = num - num3;
                    }
                    int num5 = 1;
                    while ((long)num5 < num3)
                    {
                        int num6 = 5 * num5;
                        if (num6 >= 50)
                        {
                            num6 = 50;
                        }
                        num2 += (long)(100 - num6);
                        num5++;
                    }
                    if (num4 > 0L)
                    {
                        num2 += num4 * 50L;
                    }
                    foreach (CreationCost current in list)
                    {
                        double num7 = current.CountNeeded.Double * (double)num2 / (double)(100L * num) * (double)num;
                        num7 = Math.Round(num7);
                        current.CountNeeded = new CDouble(Convert.ToDecimal(num7).ToString(), true);
                    }
                }
            }
                catch ( Exception e)
                {

                }
			return list;
		}
	}
}
