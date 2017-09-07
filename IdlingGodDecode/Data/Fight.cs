using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class Fight : TrainingBase
	{
		public enum FightType
		{
			slimy,
			frog,
			bunny,
			gobiln,
			wolf,
			kobold,
			big_food,
			skeleton,
			zombie,
			harpy,
			orc,
			mummy,
			fighting_turtle,
			ape,
			salamander,
			golem,
			dullahan,
			succubus,
			minotaurus,
			devil,
			gargoyle,
			demon,
			vampire,
			lamia,
			dragon,
			behemoth,
			valkyrie,
			nine_tailed_fox,
			genbu,
			byakko,
			suzaku,
			seiryuu,
			godzilla,
			monster_queen
		}

		public Fight.FightType TypeEnum;

		private CDouble currentHealth;

		private string name;

		private string powerGainText = string.Empty;

		public int savedCount;

		private CDouble HP1 = new CDouble("1000");

		private CDouble HP2 = new CDouble("10000");

		private CDouble HP3 = new CDouble("150000");

		private CDouble HP4 = new CDouble("3500000");

		private CDouble HP5 = new CDouble("65000000");

		private CDouble HP6 = new CDouble("1000000000");

		private CDouble HP7 = new CDouble("50000000000");

		private CDouble HP8 = new CDouble("1000000000000");

		private CDouble HP9 = new CDouble("100000000000000");

		private CDouble HP10 = new CDouble("10000000000000000");

		private CDouble HP11 = new CDouble("1000000000000000000");

		private CDouble HP12 = new CDouble("100000000000000000000");

		private CDouble HP13 = new CDouble("10000000000000000000000");

		private CDouble HP14 = new CDouble("1000000000000000000000000");

		private CDouble HP15 = new CDouble("100000000000000000000000000");

		private CDouble HP16 = new CDouble("10000000000000000000000000000");

		private CDouble HP17 = new CDouble("1000000000000000000000000000000");

		private CDouble HP18 = new CDouble("100000000000000000000000000000000");

		private CDouble HP19 = new CDouble("10000000000000000000000000000000000");

		private CDouble HP20 = new CDouble("1000000000000000000000000000000000000");

		private CDouble HP21 = new CDouble("100000000000000000000000000000000000000");

		private CDouble HP22 = new CDouble("10000000000000000000000000000000000000000");

		private CDouble HP23 = new CDouble("1000000000000000000000000000000000000000000");

		private CDouble HP24 = new CDouble("100000000000000000000000000000000000000000000");

		private CDouble HP25 = new CDouble("10000000000000000000000000000000000000000000000");

		private CDouble HP26 = new CDouble("1000000000000000000000000000000000000000000000000");

		private CDouble HP27 = new CDouble("100000000000000000000000000000000000000000000000000");

		private CDouble HP28 = new CDouble("20000000000000000000000000000000000000000000000000000");

		private CDouble HP29 = new CDouble("4000000000000000000000000000000000000000000000000000000");

		private CDouble HP30 = new CDouble("800000000000000000000000000000000000000000000000000000000");

		private CDouble HP31 = new CDouble("160000000000000000000000000000000000000000000000000000000000");

		private CDouble HP32 = new CDouble("32000000000000000000000000000000000000000000000000000000000000");

		private CDouble HP33 = new CDouble("6400000000000000000000000000000000000000000000000000000000000000");

		private CDouble HP34 = new CDouble("1280000000000000000000000000000000000000000000000000000000000000000");

		private CDouble attack;

		private CDouble defense;

		public CDouble maxHealth;

		private CDouble moneyGain;

		private CDouble powergain;

		public CDouble CurrentHealth
		{
			get
			{
				if (this.currentHealth < 0)
				{
					return 0;
				}
				if (this.currentHealth > this.MaxHealth)
				{
					return this.MaxHealth;
				}
				return this.currentHealth;
			}
			set
			{
				this.currentHealth = value;
			}
		}

		public bool IsAvailable
		{
			get
			{
				int num = this.EnumValue - 1;
				return num == -1 || App.State.AllFights[num].Level > 0;
			}
		}

		public string Name
		{
			get
			{
				if (!this.IsAvailable)
				{
					return "???";
				}
				if (this.name != null)
				{
					return this.name;
				}
				this.name = EnumName.Name(this.TypeEnum);
				return this.name;
			}
		}

		public string Description
		{
			get
			{
				if (!this.IsAvailable)
				{
					return "You need to win the previous fight first.";
				}
				switch (this.TypeEnum)
				{
				case Fight.FightType.slimy:
					return "A slime like monster. It looks really weak, but you need to start somewhere.\n(Start with 200+ Clones, 20k+ Physical and Mystic)" + this.PowerGainInSecText;
				case Fight.FightType.frog:
					return "It's a frog. It can croak and jump." + this.PowerGainInSecText;
				case Fight.FightType.bunny:
					return "A little bunny. You can see it eating a carrot. It still looks quite happy." + this.PowerGainInSecText;
				case Fight.FightType.gobiln:
					return "A small green creature. Kinda creepy with a club in his hand." + this.PowerGainInSecText;
				case Fight.FightType.wolf:
					return "A furious wolf. He might try to eat you. But shadow clones are not healthy for him." + this.PowerGainInSecText;
				case Fight.FightType.kobold:
					return "It looks kinda like a mix out of the goblin and the wolf. It's also stronger than both!" + this.PowerGainInSecText;
				case Fight.FightType.big_food:
					return "Looks like a big delicious burger. But somehow it's attacking and trying to eat you!" + this.PowerGainInSecText;
				case Fight.FightType.skeleton:
					return "Some walking bones. Somehow they can walk and attack you and will always get up after going down." + this.PowerGainInSecText;
				case Fight.FightType.zombie:
					return "It's green and can talk. It only shouts braaaaains though." + this.PowerGainInSecText;
				case Fight.FightType.harpy:
					return "A flying birdlike creature, but its quite big for a bird. It is fast and hard to hit." + this.PowerGainInSecText;
				case Fight.FightType.orc:
					return "Some bigger and uglier goblin. It looks less creepy but way stronger." + this.PowerGainInSecText;
				case Fight.FightType.mummy:
					return "An upgraded version of the zombie! It has bandages now and will get up again after dying." + this.PowerGainInSecText;
				case Fight.FightType.fighting_turtle:
					return "A humanlike turtle with a sword in each hand? It's also much faster than a normal turtle and has a bigger armor on his back." + this.PowerGainInSecText;
				case Fight.FightType.ape:
					return "A talking ape. He doesn't want to be called an ape. He says he is a sayajin?" + this.PowerGainInSecText;
				case Fight.FightType.salamander:
					return "A lizard like creature. It really likes to play with fire and throws fireballs at you." + this.PowerGainInSecText;
				case Fight.FightType.golem:
					return "Walking stones? It really hurts to get hit from it. It also hurts to hit it." + this.PowerGainInSecText;
				case Fight.FightType.dullahan:
					return "Some headless knight? It has really shiny and durable armor." + this.PowerGainInSecText;
				case Fight.FightType.succubus:
					return "Be careful it might seduce you and this would end your idling!" + this.PowerGainInSecText;
				case Fight.FightType.minotaurus:
					return "A humanlike taurus but really big with an even bigger axe in his hand. One hit might cut down a mountain." + this.PowerGainInSecText;
				case Fight.FightType.devil:
					return "It comes right from hell trying to pull you down to hell." + this.PowerGainInSecText;
				case Fight.FightType.gargoyle:
					return "A flying creature with a body out of stone. Don't ask why it can fly." + this.PowerGainInSecText;
				case Fight.FightType.demon:
					return "A stronger devil. It's said lucifer directly ordered it to bring you down to hell." + this.PowerGainInSecText;
				case Fight.FightType.vampire:
					return "Don't try to fight it at night. It's almost invincible at night and tries to suck your blood." + this.PowerGainInSecText;
				case Fight.FightType.lamia:
					return "Just a girl... Combined with a snake, really strong and eats humans for breakfeast after skinning and cutting them." + this.PowerGainInSecText;
				case Fight.FightType.dragon:
					return "A really big lizard like creature who can fly and breath fire. It's skin is harder than stone." + this.PowerGainInSecText;
				case Fight.FightType.behemoth:
					return "One of the biggest creatures on earth. It causes a lot of earthquakes when walking." + this.PowerGainInSecText;
				case Fight.FightType.valkyrie:
					return "A flying demon which looks like a girl. Also called angel of death." + this.PowerGainInSecText;
				case Fight.FightType.nine_tailed_fox:
					return "Thats only a fox... With nine tails, bigger than a whale, faster than sound and just standing near it causes you to freeze in awe." + this.PowerGainInSecText;
				case Fight.FightType.genbu:
					return "A big tortoise. Really slow but it is black! The armor is almost impenetrable and it has a fast attacking snake." + this.PowerGainInSecText;
				case Fight.FightType.byakko:
					return "Not a normal tiger. It is white, fast and strong." + this.PowerGainInSecText;
				case Fight.FightType.suzaku:
					return "A red bird coming from the south and trying to rule the sky." + this.PowerGainInSecText;
				case Fight.FightType.seiryuu:
					return "A special blue dragon. Way stronger than a normal dragon." + this.PowerGainInSecText;
				case Fight.FightType.godzilla:
					return "Bigger than even a behemoth! Some name it the king of all monsters. Even gods fear it. That is why it has 'god' in its name." + this.PowerGainInSecText;
				case Fight.FightType.monster_queen:
					return "No other monster compares to her. She has the power of all other monsters at once!" + this.PowerGainInSecText;
				default:
					return "Unknown";
				}
			}
		}

		private string PowerGainInSecText
		{
			get
			{
				if (!string.IsNullOrEmpty(this.powerGainText) && !this.ShouldUpdateText)
				{
					return this.powerGainText;
				}
				int value = 1;
				this.powerGainText = string.Concat(new string[]
				{
					"lowerTextAttack: ",
					this.Attack.ToGuiText(true),
					"\n",
					this.PowerGainInSec(value).ToGuiText(true),
					" Battle / s (with 1 Clone)"
				});
				this.ShouldUpdateText = false;
				return this.powerGainText;
			}
		}

		public CDouble Attack
		{
			get
			{
				if (this.attack != null)
				{
					return this.attack;
				}
				switch (this.EnumValue)
				{
				case 0:
					this.attack = this.HP1;
					break;
				case 1:
					this.attack = this.HP2;
					break;
				case 2:
					this.attack = this.HP3;
					break;
				case 3:
					this.attack = this.HP4;
					break;
				case 4:
					this.attack = this.HP5;
					break;
				case 5:
					this.attack = this.HP6;
					break;
				case 6:
					this.attack = this.HP7;
					break;
				case 7:
					this.attack = this.HP8;
					break;
				case 8:
					this.attack = this.HP9;
					break;
				case 9:
					this.attack = this.HP10;
					break;
				case 10:
					this.attack = this.HP11;
					break;
				case 11:
					this.attack = this.HP12;
					break;
				case 12:
					this.attack = this.HP13;
					break;
				case 13:
					this.attack = this.HP14;
					break;
				case 14:
					this.attack = this.HP15;
					break;
				case 15:
					this.attack = this.HP16;
					break;
				case 16:
					this.attack = this.HP17;
					break;
				case 17:
					this.attack = this.HP18;
					break;
				case 18:
					this.attack = this.HP19;
					break;
				case 19:
					this.attack = this.HP20;
					break;
				case 20:
					this.attack = this.HP21;
					break;
				case 21:
					this.attack = this.HP22;
					break;
				case 22:
					this.attack = this.HP23;
					break;
				case 23:
					this.attack = this.HP24;
					break;
				case 24:
					this.attack = this.HP25;
					break;
				case 25:
					this.attack = this.HP26;
					break;
				case 26:
					this.attack = this.HP27;
					break;
				case 27:
					this.attack = this.HP28;
					break;
				case 28:
					this.attack = this.HP29;
					break;
				case 29:
					this.attack = this.HP30;
					break;
				case 30:
					this.attack = this.HP31;
					break;
				case 31:
					this.attack = this.HP32;
					break;
				case 32:
					this.attack = this.HP33;
					break;
				case 33:
					this.attack = this.HP34;
					break;
				}
				return this.attack;
			}
		}

		public CDouble Defense
		{
			get
			{
				if (this.defense != null)
				{
					return this.defense;
				}
				this.defense = this.Attack / 8 * 5;
				return this.defense;
			}
		}

		public CDouble MaxHealth
		{
			get
			{
				if (this.maxHealth != null)
				{
					return this.maxHealth;
				}
				this.maxHealth = this.Attack * 10;
				return this.maxHealth;
			}
		}

		public CDouble MoneyGain
		{
			get
			{
				if (this.moneyGain != null)
				{
					return this.moneyGain;
				}
				CDouble result = 0;
				switch (this.EnumValue)
				{
				case 0:
					result = 1;
					break;
				case 1:
					result = 2;
					break;
				case 2:
					result = 4;
					break;
				case 3:
					result = 8;
					break;
				case 4:
					result = 16;
					break;
				case 5:
					result = 32;
					break;
				case 6:
					result = 64;
					break;
				case 7:
					result = 128;
					break;
				case 8:
					result = 256;
					break;
				case 9:
					result = 512;
					break;
				case 10:
					result = 1024;
					break;
				case 11:
					result = 2048;
					break;
				case 12:
					result = 4096;
					break;
				case 13:
					result = 8192;
					break;
				case 14:
					result = 16384;
					break;
				case 15:
					result = 32768;
					break;
				case 16:
					result = 65536;
					break;
				case 17:
					result = 131072;
					break;
				case 18:
					result = 262144;
					break;
				case 19:
					result = 524288;
					break;
				case 20:
					result = 1048576;
					break;
				case 21:
					result = 2097152;
					break;
				case 22:
					result = 4194304;
					break;
				case 23:
					result = 8388608;
					break;
				case 24:
					result = 16777216;
					break;
				case 25:
					result = 33554432;
					break;
				case 26:
					result = 67108864;
					break;
				case 27:
					result = 134217728;
					break;
				case 28:
					result = 268435456;
					break;
				case 29:
					result = 536870912;
					break;
				case 30:
					result = 1073741824;
					break;
				case 31:
					result = 2147483648u;
					break;
				case 32:
					result = 4294967296L;
					break;
				case 33:
					result = 8589934592L;
					break;
				}
				this.moneyGain = result;
				return result;
			}
		}

		public new CDouble PowerGain
		{
			get
			{
				if (this.powergain != null)
				{
					return this.powergain;
				}
				switch (this.EnumValue)
				{
				case 0:
					this.powergain = 1;
					break;
				case 1:
					this.powergain = 3;
					break;
				case 2:
					this.powergain = 5;
					break;
				case 3:
					this.powergain = 7;
					break;
				case 4:
					this.powergain = 10;
					break;
				case 5:
					this.powergain = 16;
					break;
				case 6:
					this.powergain = 28;
					break;
				case 7:
					this.powergain = 40;
					break;
				case 8:
					this.powergain = 60;
					break;
				case 9:
					this.powergain = 90;
					break;
				case 10:
					this.powergain = 135;
					break;
				case 11:
					this.powergain = 200;
					break;
				case 12:
					this.powergain = 300;
					break;
				case 13:
					this.powergain = 450;
					break;
				case 14:
					this.powergain = 650;
					break;
				case 15:
					this.powergain = 880;
					break;
				case 16:
					this.powergain = 1250;
					break;
				case 17:
					this.powergain = 1700;
					break;
				case 18:
					this.powergain = 2300;
					break;
				case 19:
					this.powergain = 3200;
					break;
				case 20:
					this.powergain = 4200;
					break;
				case 21:
					this.powergain = 5500;
					break;
				case 22:
					this.powergain = 7400;
					break;
				case 23:
					this.powergain = 10000;
					break;
				case 24:
					this.powergain = 13000;
					break;
				case 25:
					this.powergain = 16000;
					break;
				case 26:
					this.powergain = 22000;
					break;
				case 27:
					this.powergain = 26000;
					break;
				case 28:
					this.powergain = 28000;
					break;
				case 29:
					this.powergain = 30000;
					break;
				case 30:
					this.powergain = 32000;
					break;
				case 31:
					this.powergain = 36000;
					break;
				case 32:
					this.powergain = 38000;
					break;
				case 33:
					this.powergain = 40000;
					break;
				}
				return this.powergain;
			}
		}

		public Fight(Fight.FightType type)
		{
			this.TypeEnum = type;
			this.EnumValue = (int)type;
			this.CurrentHealth = this.MaxHealth;
		}

		internal static List<Fight> Initial()
		{
			return new List<Fight>
			{
				new Fight(Fight.FightType.slimy),
				new Fight(Fight.FightType.frog),
				new Fight(Fight.FightType.bunny),
				new Fight(Fight.FightType.gobiln),
				new Fight(Fight.FightType.wolf),
				new Fight(Fight.FightType.kobold),
				new Fight(Fight.FightType.big_food),
				new Fight(Fight.FightType.skeleton),
				new Fight(Fight.FightType.zombie),
				new Fight(Fight.FightType.harpy),
				new Fight(Fight.FightType.orc),
				new Fight(Fight.FightType.mummy),
				new Fight(Fight.FightType.fighting_turtle),
				new Fight(Fight.FightType.ape),
				new Fight(Fight.FightType.salamander),
				new Fight(Fight.FightType.golem),
				new Fight(Fight.FightType.dullahan),
				new Fight(Fight.FightType.succubus),
				new Fight(Fight.FightType.minotaurus),
				new Fight(Fight.FightType.devil),
				new Fight(Fight.FightType.gargoyle),
				new Fight(Fight.FightType.demon),
				new Fight(Fight.FightType.vampire),
				new Fight(Fight.FightType.lamia),
				new Fight(Fight.FightType.dragon),
				new Fight(Fight.FightType.behemoth),
				new Fight(Fight.FightType.valkyrie),
				new Fight(Fight.FightType.nine_tailed_fox),
				new Fight(Fight.FightType.genbu),
				new Fight(Fight.FightType.byakko),
				new Fight(Fight.FightType.suzaku),
				new Fight(Fight.FightType.seiryuu),
				new Fight(Fight.FightType.godzilla),
				new Fight(Fight.FightType.monster_queen)
			};
		}

		public new void AddCloneCount(CDouble count)
		{
			base.AddCloneCount(count);
			this.savedCount = this.ShadowCloneCount.ToInt();
			if (App.State != null)
			{
				App.State.DivinityGainSec(false);
			}
		}

		public new void RemoveCloneCount(CDouble count)
		{
			base.RemoveCloneCount(count);
			this.savedCount = this.ShadowCloneCount.ToInt();
			if (App.State != null)
			{
				App.State.DivinityGainSec(false);
			}
		}

		public CDouble PowerGainInSec(CDouble clones)
		{
			CDouble leftSide = (App.State.CloneAttack + 10) * clones;
			CDouble cDouble = 30 * (leftSide - this.Defense) / 5000 + 1;
			if (cDouble <= 0)
			{
				return new CDouble();
			}
			double num;
			if (cDouble >= this.MaxHealth)
			{
				num = 33.333;
			}
			else
			{
				double num2 = (double)((this.MaxHealth * 64 / cDouble).ToInt() / 64 + 1);
				double num3 = num2 * 30.0 / 1000.0;
				num = 1.0 / num3;
			}
			long value = (long)(num * 1000.0);
			return value * App.State.Multiplier.CurrentMultiBattle * this.PowerGain / 1000;
		}

		public void GetAttacked(CDouble attackPower, long millisecs)
		{
			GameState state = App.State;
			if (state == null)
			{
				return;
			}
			CDouble cDouble = millisecs * (attackPower - this.Defense) / 5000 + 1;
			if (cDouble > 0)
			{
				this.CurrentHealth -= cDouble;
			}
			if (this.CurrentHealth <= 0)
			{
				CDouble leftSide = this.MoneyGain;
				if (state.PremiumBoni.CrystalBonusDivinity > 0)
				{
					leftSide = leftSide * (100 + state.PremiumBoni.CrystalBonusDivinity) / 100;
				}
				state.Money += this.MoneyGain;
				this.Level = ++this.Level;
				Statistic expr_F2 = state.Statistic;
				expr_F2.TotalEnemiesDefeated = ++expr_F2.TotalEnemiesDefeated;
				state.BattlePowerBase += this.PowerGain;
				this.CurrentHealth = this.MaxHealth;
			}
		}

		public new void UpdateDuration(long millisecs)
		{
			this.RecoverHealth(millisecs);
			if (this.ShadowCloneCount == 0)
			{
				return;
			}
			CDouble attackPower = (App.State.CloneAttack + 10) * this.ShadowCloneCount;
			CDouble leftSide = millisecs * (this.Attack - App.State.CloneDefense) / 5000;
			if (leftSide > 0 && App.State.CloneMaxHealth > 0)
			{
				int num = (leftSide * 100 / App.State.CloneMaxHealth).ToInt();
				if (num > 100 || UnityEngine.Random.Range(0, 100) <= num)
				{
					base.RemoveCloneCount(1);
					ShadowClone expr_E9 = App.State.Clones;
					expr_E9.Count = --expr_E9.Count;
					ShadowClone expr_103 = App.State.Clones;
					expr_103.TotalClonesKilled = ++expr_103.TotalClonesKilled;
					Statistic expr_11D = App.State.Statistic;
					expr_11D.TotalShadowClonesDied = ++expr_11D.TotalShadowClonesDied;
					if (App.State.GameSettings.AutoAddClones)
					{
						if (this.savedCount < this.ShadowCloneCount)
						{
							this.savedCount = this.ShadowCloneCount.ToInt();
						}
						base.AddCloneCount(this.savedCount - this.ShadowCloneCount);
					}
				}
			}
			this.GetAttacked(attackPower, millisecs);
			if (App.State.GameSettings.NextFightIf1Cloned && this.ShadowCloneCount > 1 && App.State.CloneAttack + 10 > this.maxHealth * 170 && this.TypeEnum != Fight.FightType.monster_queen)
			{
				bool flag = false;
				foreach (Might current in App.State.AllMights)
				{
					if (current.DurationLeft > 0L)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					Fight fight = App.State.AllFights.FirstOrDefault((Fight x) => x.TypeEnum == this.TypeEnum + 1);
					if (fight != null)
					{
						int value = this.ShadowCloneCount.ToInt() - 1;
						this.RemoveCloneCount(value);
						fight.AddCloneCount(value);
					}
				}
			}
		}

		public void RecoverHealth(long millisecs)
		{
			if (this.CurrentHealth == this.MaxHealth)
			{
				return;
			}
			this.CurrentHealth += this.Defense / 5000 * millisecs + 1;
		}

		public double getPercentOfHP()
		{
			if (this.CurrentHealth == this.MaxHealth && this.ShadowCloneCount > 0)
			{
				return 0.0;
			}
			return (this.CurrentHealth / this.MaxHealth).Double;
		}

		public string Serialize()
		{
			return base.Serialize("Fight");
		}

		internal static Fight FromString(string base64String)
		{
			TrainingBase trainingBase = TrainingBase.FromString(base64String, "Fight");
			return new Fight((Fight.FightType)trainingBase.EnumValue)
			{
				Level = trainingBase.Level,
				ShadowCloneCount = trainingBase.ShadowCloneCount,
				CurrentDuration = trainingBase.CurrentDuration
			};
		}
	}
}
