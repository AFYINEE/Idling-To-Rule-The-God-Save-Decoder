using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class EnemyData
	{
		public enum EnemyType
		{
			constantDamage,
			doubleUp
		}

		public enum SpecialAttack
		{
			doubleDamage,
			tripleDamage,
			fiveDamage,
			tenDamage,
			Percent50,
			Percent99
		}

		public class SpecialAttackChance
		{
			public EnemyData.SpecialAttack Type;

			public int PercentChance;

			public string Name;

			public SpecialAttackChance(EnemyData.SpecialAttack type, int chance)
			{
				this.Type = type;
				this.PercentChance = chance;
				switch (type)
				{
				case EnemyData.SpecialAttack.doubleDamage:
					this.Name = "Power Attack";
					break;
				case EnemyData.SpecialAttack.tripleDamage:
					this.Name = "Triple Hit";
					break;
				case EnemyData.SpecialAttack.fiveDamage:
					this.Name = "Special Attack";
					break;
				case EnemyData.SpecialAttack.tenDamage:
					this.Name = "Mighty Attack";
					break;
				case EnemyData.SpecialAttack.Percent50:
					this.Name = "Ultimate Attack";
					break;
				case EnemyData.SpecialAttack.Percent99:
					this.Name = "Supernova";
					break;
				}
			}
		}

		public class Enemy
		{
			internal string Name;

			internal string Description;

			internal CDouble HPMax;

			internal CDouble HP;

			internal string AttackName;

			internal CDouble damage;

			internal EnemyData.EnemyType TypeEnum;

			internal List<EnemyData.SpecialAttackChance> SpecialAttacks;

			public Enemy(string name, string desc, CDouble hp, CDouble dam, EnemyData.EnemyType type, List<EnemyData.SpecialAttackChance> specialAttacks)
			{
				this.Name = name;
				this.Description = desc;
				this.HPMax = hp;
				this.HP = hp;
				this.damage = dam;
				this.TypeEnum = type;
				this.SpecialAttacks = specialAttacks;
			}

			internal CDouble GetDamage(CDouble playerHp)
			{
				this.AttackName = " attacks you ";
				using (List<EnemyData.SpecialAttackChance>.Enumerator enumerator = this.SpecialAttacks.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EnemyData.SpecialAttackChance current = enumerator.Current;
						int num = UnityEngine.Random.Range(0, 100);
						if (num < current.PercentChance)
						{
							this.AttackName = " uses " + current.Name + " ";
							switch (current.Type)
							{
							case EnemyData.SpecialAttack.doubleDamage:
							{
								CDouble result = this.damage * 2;
								return result;
							}
							case EnemyData.SpecialAttack.tripleDamage:
							{
								CDouble result = this.damage * 3;
								return result;
							}
							case EnemyData.SpecialAttack.fiveDamage:
							{
								CDouble result = this.damage * 5;
								return result;
							}
							case EnemyData.SpecialAttack.tenDamage:
							{
								CDouble result = this.damage * 10;
								return result;
							}
							case EnemyData.SpecialAttack.Percent50:
							{
								CDouble result = playerHp / 2;
								return result;
							}
							case EnemyData.SpecialAttack.Percent99:
							{
								CDouble result = playerHp * 99 / 100;
								return result;
							}
							default:
								goto IL_123;
							}
						}
					}
					IL_123:;
				}
				return this.damage;
			}

			public void IncreaseDamage()
			{
				EnemyData.EnemyType typeEnum = this.TypeEnum;
				if (typeEnum == EnemyData.EnemyType.doubleUp)
				{
					this.damage *= 2;
				}
			}

			public void TimeManipulate()
			{
				EnemyData.EnemyType typeEnum = this.TypeEnum;
				if (typeEnum == EnemyData.EnemyType.doubleUp)
				{
					this.damage /= 8;
				}
			}

			public bool DoDamage(CDouble damage)
			{
				this.HP -= damage;
				return this.HP <= 0;
			}
		}

		public EnemyData.Enemy Endless
		{
			get
			{
				List<EnemyData.SpecialAttackChance> list = new List<EnemyData.SpecialAttackChance>();
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.doubleDamage, 2));
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.tripleDamage, 1));
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.fiveDamage, 1));
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.tenDamage, 1));
				CDouble hp = App.State.PowerLevel * 5;
				CDouble dam = App.State.PowerLevel / 300;
				return new EnemyData.Enemy("Shadow Clones", "Clones of yourself but they use a different and constant divider. One is quite easy but in numbers they can become quite strong.", hp, dam, EnemyData.EnemyType.constantDamage, list);
			}
		}

		public EnemyData.Enemy Jacky
		{
			get
			{
				List<EnemyData.SpecialAttackChance> list = new List<EnemyData.SpecialAttackChance>();
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.doubleDamage, 10));
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.tripleDamage, 5));
				return new EnemyData.Enemy("Jacky Lee", "Two humans are combined into one. Can they beat a god like yourself?", App.State.PowerLevel * 100, App.State.PowerLevel / 35, EnemyData.EnemyType.constantDamage, list);
			}
		}

		public EnemyData.Enemy Cthulhu
		{
			get
			{
				List<EnemyData.SpecialAttackChance> list = new List<EnemyData.SpecialAttackChance>();
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.doubleDamage, 10));
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.tripleDamage, 5));
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.fiveDamage, 5));
				return new EnemyData.Enemy("Cthulhu", "The first real challenge. You might need 100k of most skills to be victorious.", App.State.PowerLevel * 150, App.State.PowerLevel / 20, EnemyData.EnemyType.constantDamage, list);
			}
		}

		public EnemyData.Enemy Doppelganger
		{
			get
			{
				List<EnemyData.SpecialAttackChance> list = new List<EnemyData.SpecialAttackChance>();
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.doubleDamage, 10));
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.tripleDamage, 10));
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.fiveDamage, 10));
				return new EnemyData.Enemy("Doppelganger", "Looks just like yourself and it might be even stronger than you!\nAll your skill levels might need to be above 100k to have a chance.", App.State.PowerLevel * 250, App.State.PowerLevel / 15, EnemyData.EnemyType.constantDamage, list);
			}
		}

		public EnemyData.Enemy Developer
		{
			get
			{
				List<EnemyData.SpecialAttackChance> list = new List<EnemyData.SpecialAttackChance>();
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.Percent50, 3));
				return new EnemyData.Enemy("D. Evelope", "Starts out weak but becomes constantly stronger.", App.State.PowerLevel * 250, 1, EnemyData.EnemyType.doubleUp, list);
			}
		}

		public EnemyData.Enemy Creator
		{
			get
			{
				List<EnemyData.SpecialAttackChance> list = new List<EnemyData.SpecialAttackChance>();
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.Percent50, 5));
				list.Add(new EnemyData.SpecialAttackChance(EnemyData.SpecialAttack.Percent99, 3));
				return new EnemyData.Enemy("D. Creator", string.Empty, App.State.PowerLevel * 500, 100, EnemyData.EnemyType.doubleUp, list);
			}
		}
	}
}
