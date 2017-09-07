using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
    public class Creation
    {
        public enum CreationType
        {
            Shadow_clone,
            Light,
            Stone,
            Soil,
            Air,
            Water,
            Plant,
            Tree,
            Fish,
            Animal,
            Human,
            River,
            Mountain,
            Forest,
            Village,
            Town,
            Ocean,
            Nation,
            Continent,
            Weather,
            Sky,
            Night,
            Moon,
            Planet,
            Earthlike_planet,
            Sun,
            Solar_system,
            Galaxy,
            Universe
        }

        public Creation.CreationType TypeEnum;

        public God GodToDefeat;

        public long currentDuration;

        public bool IsActive;

        public CDouble TotalCreated = new CDouble();

        public bool ShouldUpdateText = true;

        public bool CanBuy;

        public bool AutoBuy = true;

        public int NextAtCount;

        public CDouble count = 0;

        private string description = string.Empty;

        public List<CreationCost> SubItemCreationCost = new List<CreationCost>();

        private Creation oldActiveCreation;

        public CDouble CountToAdd = 0;

        private int pierceModi = 28;

        private const int sec = 1000;

        private const int min = 60000;

        private const int hour = 3600000;

        private const long day = 86400000L;

        private static int durationMulti = 100;

        public static bool UpdateText;

        private static string creatingSpeedText = string.Empty;

        public string NextAtString
        {
            get
            {
                return this.NextAtCount.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.StartsWith("-"))
                {
                    value = "0";
                }
                int.TryParse(value, out this.NextAtCount);
            }
        }

        public CDouble Count
        {
            get
            {
                if (this.TypeEnum == Creation.CreationType.Shadow_clone && App.State != null)
                {
                    return App.State.Clones.Count;
                }
                return this.count;
            }
            set
            {
                try
                {
                    if (this.TypeEnum == Creation.CreationType.Shadow_clone && App.State != null)
                    {
                        CDouble rightSide = App.State.PremiumBoni.CreationCountBoni(true) + 1;
                        App.State.Clones.Count = App.State.Clones.Count + rightSide;
                        App.State.Clones.TotalClonesCreated += rightSide;
                        App.State.Statistic.TotalShadowClonesCreated += rightSide;
                    }
                    else
                    {
                        this.count = value;
                    }
                }
                catch (Exception e)
                {

                }
            }
        }

        protected string PowerGainInSecText
        {
            get
            {
                int num = (App.State.PremiumBoni.CreationDopingDivider / (100 + App.State.PremiumBoni.CreatingSpeedUpPercent(true))).ToInt();
                if (num == 0)
                {
                    num = 1;
                }
                long num2 = 100L * this.DurationInMS / (long)num;
                if (num2 < 30L)
                {
                    num2 = 30L;
                }
                CDouble leftSide = this.CreatingPowerGain() * (100 + (App.State.GameSettings.CreationToCreateCount - 1) * 5) / 100;
                CDouble cDouble = leftSide * App.State.Multiplier.CurrentMultiCreating * 1000 / num2;
                return "lowerText" + cDouble.ToGuiText(true) + " Creation / sec.";
            }
        }

        public string Name
        {
            get
            {
                return EnumName.Name(this.TypeEnum);
            }
        }

        public string Description
        {
            get
            {
                if (!string.IsNullOrEmpty(this.description) && !this.ShouldUpdateText)
                {
                    return this.description;
                }
                if (!this.GodToDefeat.IsDefeated)
                {
                    this.description = this.GodToDefeat.Description;
                    return this.description;
                }
                int num = App.State.GameSettings.CreationToCreateCount;
                if (num == 0)
                {
                    num = 1;
                }
                StringBuilder stringBuilder = new StringBuilder("\nTo create " + num);
                stringBuilder.Append(" x ").Append(this.Name).Append(", you need:\n");
                foreach (CreationCost current in CreationCost.RequiredCreations(this.TypeEnum, 0L, false))
                {
                    stringBuilder.Append(current.CountNeeded.ToGuiText(true)).Append(" x ").Append(EnumName.Name(current.TypeEnum)).Append(", ");
                }
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
                switch (this.TypeEnum)
                {
                    case Creation.CreationType.Shadow_clone:
                        this.description = string.Concat(new object[]
					{
						"Shadow Clones can fight, train, and practice skills for you.\nYou just have to create them.\n",
						App.State.Clones.MaxShadowClones,
						" is your limit.",
						this.PowerGainInSecText
					});
                        break;
                    case Creation.CreationType.Light:
                        this.description = "Create a light to make the world brighter. You can't see without light. So it is quite a useful thing to create." + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Stone:
                        this.description = "Stones are one of the most important things in the world! Without stones there wouldn't be any deserts, mountains, meteorites and a few other things." + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Soil:
                        this.description = "Soil or earth. Now it's getting important. That's one of the foundations of all life." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Air:
                        this.description = "Not that important, if you want to suffocate. But you might not want that, so better create some air." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Water:
                        this.description = "Water is also kinda neat to have. Without it, no life would be possible." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Plant:
                        this.description = "Now you can create your first lifeform! Another foundation of other lives. They need to eat something." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Tree:
                        this.description = "You can't create that much air at once. Make your helper! (Not that they will produce air for 'you')" + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Fish:
                        this.description = "Your first meat! Now there is everything there to create sushi. But somehow fishes don't like it." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Animal:
                        this.description = "Create some animals to make it more lively. We don't want a boring world." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Human:
                        this.description = "Don't create too many of them, or they might destroy everything you have created till now." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.River:
                        this.description = "Some space to transport all your created water seems like a good idea, right?" + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Mountain:
                        this.description = "You might need some big stones for this. They are for... well create them anyway." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Forest:
                        this.description = "Even trees need some friends! So put some together, cause they can't walk by themselves." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Village:
                        this.description = "If you created a few too many humans, you might as well put them together." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Town:
                        this.description = "If you create a town, you might have already created too many humans..." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Ocean:
                        this.description = "Your rivers are not enough to prevent a planet from drying out. Create something bigger like an ocean!" + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Nation:
                        this.description = "Sooo... yep no humans needed, just some towns. " + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Continent:
                        this.description = "Your first continent! Now you can see what you have done with all your oceans." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Weather:
                        this.description = "Just make it random. So everyone has some fun with it." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Sky:
                        this.description = "Thats a lot of air here already. Now you can see the sky!" + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Night:
                        this.description = "A good way to get some sleep. You might need it already." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Moon:
                        this.description = "Now with the night, a moon sounds like a good idea. They will make a good combination." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Planet:
                        this.description = "Just some biiiig rock." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Earthlike_planet:
                        this.description = "Now you can put everything you have created on your big rock." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Sun:
                        this.description = "You just noticed your planet was kinda cold. A sun is to warm it all up a bit." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Solar_system:
                        this.description = "Just like trees, your planets also need some friends... maybe. So put them together." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Galaxy:
                        this.description = "You were bored, right? Creating a little galaxy is a good way to waste some time." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                    case Creation.CreationType.Universe:
                        this.description = "Just a bit more and you finally finished the game! Or not..." + stringBuilder.ToString() + this.PowerGainInSecText;
                        break;
                }
                CDouble rightSide = App.State.CreationSpeed(30L);
                CDouble cDouble = (this.DurationInMS - this.currentDuration) * 30L / rightSide;
                string text = "\nTime needed: " + Conv.MsToGuiText(cDouble.ToLong(), true);
                if (this.TypeEnum != Creation.CreationType.Shadow_clone)
                {
                    this.description = string.Concat(new string[]
					{
						"Received from: ",
						EnumName.Name(this.GodToDefeat.TypeEnum),
						"\n",
						this.description,
						text
					});
                }
                this.description = this.description + "\nYou created: " + this.TotalCreated.ToGuiText(true);
                this.ShouldUpdateText = false;
                return this.description;
            }
        }

        public CDouble BuyCost
        {
            get
            {
                CDouble result = 0;
                switch (this.TypeEnum)
                {
                    case Creation.CreationType.Shadow_clone:
                        return 0;
                    case Creation.CreationType.Light:
                        result = 1250;
                        break;
                    case Creation.CreationType.Stone:
                        result = 1250;
                        break;
                    case Creation.CreationType.Soil:
                        result = 4050;
                        break;
                    case Creation.CreationType.Air:
                        result = 8000;
                        break;
                    case Creation.CreationType.Water:
                        result = 28250;
                        break;
                    case Creation.CreationType.Plant:
                        result = 77000;
                        break;
                    case Creation.CreationType.Tree:
                        result = 122000;
                        break;
                    case Creation.CreationType.Fish:
                        result = 693800;
                        break;
                    case Creation.CreationType.Animal:
                        result = 3234650;
                        break;
                    case Creation.CreationType.Human:
                        result = 70671250;
                        break;
                    case Creation.CreationType.River:
                        result = 141366500;
                        break;
                    case Creation.CreationType.Mountain:
                        result = 250180000;
                        break;
                    case Creation.CreationType.Forest:
                        result = 1220310000;
                        break;
                    case Creation.CreationType.Village:
                        result = 15887686500L;
                        break;
                    case Creation.CreationType.Town:
                        result = 354580906500L;
                        break;
                    case Creation.CreationType.Ocean:
                        result = 1996984606500L;
                        break;
                    case Creation.CreationType.Nation:
                        result = 5746270637500L;
                        break;
                    case Creation.CreationType.Continent:
                        result = 30728339994000L;
                        break;
                    case Creation.CreationType.Weather:
                        result = 51538265776500L;
                        break;
                    case Creation.CreationType.Sky:
                        result = 75663269026500L;
                        break;
                    case Creation.CreationType.Night:
                        result = 151326542253000L;
                        break;
                    case Creation.CreationType.Moon:
                        result = 338826547653000L;
                        break;
                    case Creation.CreationType.Planet:
                        result = 713826558753000L;
                        break;
                    case Creation.CreationType.Earthlike_planet:
                        result = 2645576593753000L;
                        break;
                    case Creation.CreationType.Sun:
                        result = 12500000071998800L;
                        break;
                    case Creation.CreationType.Solar_system:
                        result = 199028233339041000L;
                        break;
                    case Creation.CreationType.Galaxy:
                        result = 995141169995203000L;
                        break;
                    case Creation.CreationType.Universe:
                        result = 4975705969976010000L;
                        break;
                }
                return result;
            }
        }

        public long DurationInMS
        {
            get
            {
                long num = 999999999L;
                switch (this.TypeEnum)
                {
                    case Creation.CreationType.Shadow_clone:
                        num = 1000L;
                        break;
                    case Creation.CreationType.Light:
                        num = 1000L;
                        break;
                    case Creation.CreationType.Stone:
                        num = 1000L;
                        break;
                    case Creation.CreationType.Soil:
                        num = 2000L;
                        break;
                    case Creation.CreationType.Air:
                        num = 4000L;
                        break;
                    case Creation.CreationType.Water:
                        num = 5000L;
                        break;
                    case Creation.CreationType.Plant:
                        num = 7000L;
                        break;
                    case Creation.CreationType.Tree:
                        num = 10000L;
                        break;
                    case Creation.CreationType.Fish:
                        num = 15000L;
                        break;
                    case Creation.CreationType.Animal:
                        num = 20000L;
                        break;
                    case Creation.CreationType.Human:
                        num = 30000L;
                        break;
                    case Creation.CreationType.River:
                        num = 60000L;
                        break;
                    case Creation.CreationType.Mountain:
                        num = 120000L;
                        break;
                    case Creation.CreationType.Forest:
                        num = 180000L;
                        break;
                    case Creation.CreationType.Village:
                        num = 240000L;
                        break;
                    case Creation.CreationType.Town:
                        num = 360000L;
                        break;
                    case Creation.CreationType.Ocean:
                        num = 600000L;
                        break;
                    case Creation.CreationType.Nation:
                        num = 720000L;
                        break;
                    case Creation.CreationType.Continent:
                        num = 900000L;
                        break;
                    case Creation.CreationType.Weather:
                        num = 1200000L;
                        break;
                    case Creation.CreationType.Sky:
                        num = 1500000L;
                        break;
                    case Creation.CreationType.Night:
                        num = 2100000L;
                        break;
                    case Creation.CreationType.Moon:
                        num = 2400000L;
                        break;
                    case Creation.CreationType.Planet:
                        num = 3600000L;
                        break;
                    case Creation.CreationType.Earthlike_planet:
                        num = 10800000L;
                        break;
                    case Creation.CreationType.Sun:
                        num = 21600000L;
                        break;
                    case Creation.CreationType.Solar_system:
                        num = 43200000L;
                        break;
                    case Creation.CreationType.Galaxy:
                        num = 86400000L;
                        break;
                    case Creation.CreationType.Universe:
                        num = 432000000L;
                        break;
                }
                return num * (long)Creation.durationMulti / 100L;
            }
        }

        public Creation(Creation.CreationType type, God godToDefeat)
        {
            this.GodToDefeat = godToDefeat;
            this.TypeEnum = type;
            this.Count = 0;
            this.InitSubItemCost(0);
        }

        internal static List<Creation> Initial()
        {
            return new List<Creation>
			{
				new Creation(Creation.CreationType.Shadow_clone, new God(God.GodType.None)),
				new Creation(Creation.CreationType.Light, new God(God.GodType.Hyperion)),
				new Creation(Creation.CreationType.Stone, new God(God.GodType.Itztli)),
				new Creation(Creation.CreationType.Soil, new God(God.GodType.Gaia)),
				new Creation(Creation.CreationType.Air, new God(God.GodType.Shu)),
				new Creation(Creation.CreationType.Water, new God(God.GodType.Suijin)),
				new Creation(Creation.CreationType.Plant, new God(God.GodType.Gefion)),
				new Creation(Creation.CreationType.Tree, new God(God.GodType.Hathor)),
				new Creation(Creation.CreationType.Fish, new God(God.GodType.Pontus)),
				new Creation(Creation.CreationType.Animal, new God(God.GodType.Diana)),
				new Creation(Creation.CreationType.Human, new God(God.GodType.Izanagi)),
				new Creation(Creation.CreationType.River, new God(God.GodType.Nephthys)),
				new Creation(Creation.CreationType.Mountain, new God(God.GodType.Cybele)),
				new Creation(Creation.CreationType.Forest, new God(God.GodType.Artemis)),
				new Creation(Creation.CreationType.Village, new God(God.GodType.Eros)),
				new Creation(Creation.CreationType.Town, new God(God.GodType.Freya)),
				new Creation(Creation.CreationType.Ocean, new God(God.GodType.Poseidon)),
				new Creation(Creation.CreationType.Nation, new God(God.GodType.Laima)),
				new Creation(Creation.CreationType.Continent, new God(God.GodType.Athena)),
				new Creation(Creation.CreationType.Weather, new God(God.GodType.Susano_o)),
				new Creation(Creation.CreationType.Sky, new God(God.GodType.Zeus)),
				new Creation(Creation.CreationType.Night, new God(God.GodType.Nyx)),
				new Creation(Creation.CreationType.Moon, new God(God.GodType.Luna)),
				new Creation(Creation.CreationType.Planet, new God(God.GodType.Jupiter)),
				new Creation(Creation.CreationType.Earthlike_planet, new God(God.GodType.Odin)),
				new Creation(Creation.CreationType.Sun, new God(God.GodType.Amaterasu)),
				new Creation(Creation.CreationType.Solar_system, new God(God.GodType.Coatlicue)),
				new Creation(Creation.CreationType.Galaxy, new God(God.GodType.Chronos)),
				new Creation(Creation.CreationType.Universe, new God(God.GodType.Tyrant_Overlord_Baal))
			};
        }

        public CDouble CreatingPowerGain()
        {
            switch (this.TypeEnum)
            {
                case Creation.CreationType.Shadow_clone:
                    return 50;
                case Creation.CreationType.Light:
                    return 1000;
                case Creation.CreationType.Stone:
                    return 1200;
                case Creation.CreationType.Soil:
                    return 2880;
                case Creation.CreationType.Air:
                    return 6048;
                case Creation.CreationType.Water:
                    return 10368;
                case Creation.CreationType.Plant:
                    return 18662;
                case Creation.CreationType.Tree:
                    return 29860;
                case Creation.CreationType.Fish:
                    return 53748;
                case Creation.CreationType.Animal:
                    return 85996;
                case Creation.CreationType.Human:
                    return 154793;
                case Creation.CreationType.River:
                    return 371504;
                case Creation.CreationType.Mountain:
                    return 818708;
                case Creation.CreationType.Forest:
                    return 1337415;
                case Creation.CreationType.Village:
                    return 2567837;
                case Creation.CreationType.Town:
                    return 4622106;
                case Creation.CreationType.Ocean:
                    return 9244213;
                case Creation.CreationType.Nation:
                    return 13866319;
                case Creation.CreationType.Continent:
                    return 19967500;
                case Creation.CreationType.Weather:
                    return 27954500;
                case Creation.CreationType.Sky:
                    return 38337600;
                case Creation.CreationType.Night:
                    return 57506400;
                case Creation.CreationType.Moon:
                    return 82809216;
                case Creation.CreationType.Planet:
                    return 198742118;
                case Creation.CreationType.Earthlike_planet:
                    return 715471625;
                case Creation.CreationType.Sun:
                    return 1717131900;
                case Creation.CreationType.Solar_system:
                    return 4121116559u;
                case Creation.CreationType.Galaxy:
                    return 9890679742L;
                case Creation.CreationType.Universe:
                    return 58540854915L;
                default:
                    return 0;
            }
        }

        public void InitSubItemCost(int countAdded = 0)
        {
            List<CreationCost> list = new List<CreationCost>();
            foreach (CreationCost current in CreationCost.RequiredCreations(this.TypeEnum, (long)countAdded, false))
            {
                list.AddRange(this.GetCost(current, countAdded));
            }
            this.SubItemCreationCost = new List<CreationCost>();
            using (List<CreationCost>.Enumerator enumerator2 = list.GetEnumerator())
            {
                while (enumerator2.MoveNext())
                {
                    CreationCost cost = enumerator2.Current;
                    CreationCost creationCost = this.SubItemCreationCost.FirstOrDefault((CreationCost x) => x.TypeEnum == cost.TypeEnum);
                    if (creationCost == null)
                    {
                        this.SubItemCreationCost.Add(cost);
                    }
                    else
                    {
                        creationCost.CountNeeded = cost.CountNeeded;
                    }
                }
            }
            //this.SubItemCreationCost = (from o in this.SubItemCreationCost
            //                            orderby (int)(o.TypeEnum * (Creation.CreationType)(-1))
            //                            select o).ToList<CreationCost>();
        }

        private List<CreationCost> GetCost(CreationCost creationCost, int creationCount = 0)
        {
            List<CreationCost> list = new List<CreationCost>();
            try
            {
                if (creationCount == 0 && App.State != null && App.State.GameSettings.CreationToCreateCount > 0)
                {
                    creationCount = App.State.GameSettings.CreationToCreateCount;
                }
                if (creationCount == 0)
                {
                    creationCount = 1;
                }
                foreach (CreationCost current in CreationCost.RequiredCreations(creationCost.TypeEnum, (long)creationCount, false))
                {
                    current.CountNeeded = current.CountNeeded * creationCost.CountNeeded / creationCount;
                    list.Add(current);
                    list.AddRange(this.GetCost(current, 0));
                }
            }
            catch(Exception ex)
            {

            }
            return list;

        }

        private int CheckCountNeeded()
        {
            if (App.State.GameSettings.CreationToCreateCount > App.State.PremiumBoni.CreationCountBoni(true) + 1)
            {
                App.State.GameSettings.CreationToCreateCount = App.State.PremiumBoni.CreationCountBoni(true).ToInt() + 1;
            }
            CDouble cDouble = App.State.GameSettings.CreationToCreateCount;
            if (App.State.GameSettings.CreationsNextAtMode == 2 && this.NextAtCount > 0 && this.TotalCreated + cDouble >= this.NextAtCount && App.State.GameSettings.LastCreation == this)
            {
                cDouble = this.NextAtCount - this.TotalCreated;
            }
            if (App.State.GameSettings.CreationsNextAtMode == 1 && this.NextAtCount > 0 && this.Count + cDouble >= this.NextAtCount && App.State.GameSettings.LastCreation == this)
            {
                cDouble = this.NextAtCount - this.Count;
            }
            if (App.State.GameSettings.LastCreation != null && App.State.GameSettings.LastCreation != this)
            {
                List<CreationCost> source = CreationCost.RequiredCreations(App.State.GameSettings.LastCreation.TypeEnum, (long)App.State.GameSettings.LastCreation.CountToAdd.ToInt(), false);
                CreationCost creationCost = source.FirstOrDefault((CreationCost x) => x.TypeEnum == this.TypeEnum);
                if (creationCost != null)
                {
                    cDouble = creationCost.CountNeeded;
                }
                if (cDouble > App.State.GameSettings.CreationToCreateCount)
                {
                    cDouble = App.State.GameSettings.CreationToCreateCount;
                }
                if (!this.AutoBuy && cDouble < App.State.GameSettings.CreationToCreateCount)
                {
                    cDouble = App.State.GameSettings.CreationToCreateCount;
                }
            }
            if (cDouble <= 0)
            {
                cDouble = 1;
            }
            if (this.TypeEnum == Creation.CreationType.Shadow_clone || this.TypeEnum == Creation.CreationType.Light || this.TypeEnum == Creation.CreationType.Stone)
            {
                cDouble = App.State.PremiumBoni.CreationCountBoni(true) + 1;
            }
            this.CountToAdd = cDouble;
            return cDouble.ToInt();
        }

        private bool CheckPrerequesiteCost(List<CreationCost> requiredCreations)
        {
            bool flag = false;
            Creation creation = null;
            for (int i = 0; i < requiredCreations.Count; i++)
            {
                CreationCost cost = requiredCreations[i];
                Creation creation2 = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
                if (cost.CountNeeded > creation2.Count)
                {
                    if (App.State.IsBuyUnlocked && App.State.GameSettings.AutoBuyCreations && creation2.CanBuy && creation2.AutoBuy)
                    {
                        CDouble cDouble = cost.CountNeeded - creation2.Count;
                        CDouble rightSide = cDouble * creation2.BuyCost * (120 - App.State.PremiumBoni.AutoBuyCostReduction) / 100;
                        if (App.State.Money >= rightSide)
                        {
                            App.State.Money -= rightSide;
                            creation2.count += cDouble;
                            App.State.Statistic.TotalMoneySpent += rightSide;
                        }
                        else
                        {
                            flag = true;
                            creation = creation2;
                        }
                    }
                    else
                    {
                        flag = true;
                        creation = creation2;
                    }
                }
            }
            if (flag)
            {
                if (creation != null)
                {
                    creation.IsActive = true;
                }
                this.IsActive = false;
            }
            return flag;
        }

        public void UpdateDuration(long ms)
        {
            if (App.State == null)
            {
                return;
            }
            if (!this.GodToDefeat.IsDefeated)
            {
                this.GodToDefeat.RecoverHealth(ms);
            }
            if (!this.IsActive)
            {
                return;
            }
            if (!this.GodToDefeat.IsDefeated)
            {
                this.FightGod(ms);
                if (this.GodToDefeat.IsDefeated)
                {
                    this.description = string.Empty;
                }
                return;
            }
            bool flag = this.NextAtCount != 0 && this.NextAtCount <= this.Count;
            if (App.State.GameSettings.CreationsNextAtMode == 2)
            {
                flag = (this.NextAtCount != 0 && this.NextAtCount <= this.TotalCreated);
            }
            if ((App.State.GameSettings.LastCreation != null && App.State.GameSettings.LastCreation.TypeEnum != this.TypeEnum) || App.State.GameSettings.CreationsNextAtMode == 0)
            {
                flag = false;
            }
            if (flag && this.TypeEnum != Creation.CreationType.Shadow_clone)
            {
                foreach (Creation current in App.State.AllCreations)
                {
                    current.IsActive = false;
                }
                Creation lastCreation = this;
                Creation creation = App.State.AllCreations.First((Creation x) => x.TypeEnum == Creation.CreationType.Shadow_clone);
                if (this.TypeEnum == Creation.CreationType.Universe)
                {
                    lastCreation = creation;
                    creation.IsActive = true;
                }
                else
                {
                    Creation creation2 = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == this.TypeEnum + 1);
                    if (creation2 != null && creation2.GodToDefeat.IsDefeated)
                    {
                        lastCreation = creation2;
                        creation2.IsActive = true;
                    }
                    else
                    {
                        bool flag2 = false;
                        foreach (Creation current2 in App.State.AllCreations)
                        {
                            if (current2.GodToDefeat.IsDefeated && current2.NextAtCount == 0 && current2.TypeEnum != Creation.CreationType.Shadow_clone)
                            {
                                lastCreation = current2;
                                current2.IsActive = true;
                                flag2 = true;
                                break;
                            }
                        }
                        if (!flag2)
                        {
                            lastCreation = creation;
                            creation.IsActive = true;
                        }
                    }
                }
                App.State.GameSettings.LastCreation = lastCreation;
                return;
            }
            if (App.State.GameSettings.CreateShadowClonesIfNotMax && App.State.Clones.Count < App.State.Clones.MaxShadowClones && this.TypeEnum != Creation.CreationType.Shadow_clone)
            {
                this.oldActiveCreation = this;
                Creation creation3 = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == Creation.CreationType.Shadow_clone);
                creation3.IsActive = true;
                return;
            }
            if (this.oldActiveCreation != null)
            {
                this.IsActive = false;
                this.oldActiveCreation.IsActive = true;
                this.oldActiveCreation = null;
            }
            int num = this.CheckCountNeeded();
            List<CreationCost> list = CreationCost.RequiredCreations(this.TypeEnum, (long)num, false);
            if (this.CheckPrerequesiteCost(list))
            {
                return;
            }
            this.currentDuration += App.State.CreationSpeed(ms);
            long durationInMS = this.DurationInMS;
            if (this.currentDuration > durationInMS)
            {
                this.currentDuration -= durationInMS;
                if (this.currentDuration > durationInMS)
                {
                    this.currentDuration = durationInMS;
                }
                using (List<CreationCost>.Enumerator enumerator3 = list.GetEnumerator())
                {
                    while (enumerator3.MoveNext())
                    {
                        CreationCost cost = enumerator3.Current;
                        Creation creation4 = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
                        creation4.Count -= cost.CountNeeded;
                    }
                }
                CDouble rightSide = this.CreatingPowerGain() * (100 + (num - 1) * 5) / 100;
                App.State.CreatingPowerBase += rightSide;
                this.CanBuy = true;
                this.Count += num;
                this.TotalCreated += num;
                App.State.CheckForAchievement(this);
                if (this.TypeEnum != Creation.CreationType.Shadow_clone)
                {
                    App.State.Statistic.TotalCreations += num;
                }
                this.FinishUUC(App.State);
                if (App.State.GameSettings.LastCreation != null && App.State.GameSettings.LastCreation.TypeEnum != this.TypeEnum)
                {
                    Creation creation5 = App.State.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == App.State.GameSettings.LastCreation.TypeEnum);
                    this.IsActive = false;
                    creation5.IsActive = true;
                }
            }
        }

        public void FinishUUC(GameState state)
        {
            if (this.TypeEnum == Creation.CreationType.Universe && state.Statistic.HasStartedUniverseChallenge && this.count > 0)
            {
                state.Statistic.HasStartedUniverseChallenge = false;
                if (state.Statistic.FastestUUCallenge <= 0 || state.Statistic.FastestUUCallenge > state.Statistic.TimeAfterUUCStarted)
                {
                    state.Statistic.FastestUUCallenge = state.Statistic.TimeAfterUUCStarted;
                    Leaderboards.SubmitStat(LeaderBoardType.FastestUUCallenge, (int)(state.Statistic.FastestUUCallenge.ToLong() / 1000L), false);
                }
                state.Statistic.TimeAfterUUCStarted = 0;
                Statistic expr_C6 = state.Statistic;
                expr_C6.UniverseChallengesFinished = ++expr_C6.UniverseChallengesFinished;
                if (state.HomePlanet.UpgradeLevel < 50)
                {
                    Planet expr_F8 = state.HomePlanet;
                    expr_F8.UpgradeLevel = ++expr_F8.UpgradeLevel;
                    GuiBase.ShowToast("Ultimate Universe Challenge finished! Your Planet level increased by 1.");
                }
                else
                {
                    GuiBase.ShowToast("Ultimate Universe Challenge finished! But your Planet level is already maxed.");
                }
            }
        }

        private void FightGod(long ms)
        {
            this.pierceModi--;
            bool isPierce = false;
            if (this.pierceModi < (int)this.TypeEnum)
            {
                isPierce = true;
                this.pierceModi = 28;
            }
            if (App.State.GetAttacked(this.GodToDefeat.Attack, ms, isPierce))
            {
                this.IsActive = false;
                GodUi.EnableCreating();
            }
            else if (App.State.Statistic.HasStartedUltimatePetChallenge)
            {
                CDouble cDouble = App.State.Ext.GetTotalPetPower(true) / 3;
                this.GodToDefeat.GetAttacked(cDouble, ms);
                CDouble leftSide = (this.GodToDefeat.Attack - cDouble) / 5000 * ms;
                CDouble rightSide = leftSide / App.State.Ext.PetPowerMultiCampaigns / App.State.Ext.PetPowerMultiGods;
                if (cDouble == 0)
                {
                    this.IsActive = false;
                    GodUi.EnableCreating();
                }
                foreach (Pet current in App.State.Ext.AllPets)
                {
                    if (current.CurrentHealth > 0 && !current.IsInCampaign)
                    {
                        current.CurrentHealth -= rightSide;
                        if (current.CurrentHealth < 0)
                        {
                            current.CurrentHealth = 0;
                            current.ZeroHealthTime = 60000L;
                        }
                        break;
                    }
                }
                if (this.GodToDefeat.IsDefeated && !this.GodToDefeat.IsDefeatedPetChallenge)
                {
                    this.GodToDefeat.IsDefeatedPetChallenge = true;
                    App.State.Ext.PetPowerMultiGods = App.State.Ext.PetPowerMultiGods * 4;
                }
            }
            else
            {
                this.GodToDefeat.GetAttacked(App.State.Attack, ms);
            }
            if (this.GodToDefeat.IsDefeated)
            {
                this.IsActive = false;
                GodUi.EnableCreating();
            }
        }

        public double getPercent()
        {
            return (double)this.currentDuration / (double)this.DurationInMS;
        }

        public CDouble TotalDuration(GameState state)
        {
            CDouble cDouble = this.DurationInMS;
            using (List<CreationCost>.Enumerator enumerator = CreationCost.RequiredCreations(this.TypeEnum, 1L, false).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    CreationCost cost = enumerator.Current;
                    Creation creation = state.AllCreations.FirstOrDefault((Creation x) => x.TypeEnum == cost.TypeEnum);
                    if (creation != null)
                    {
                        cDouble += cost.CountNeeded * creation.TotalDuration(state);
                    }
                }
            }
            return cDouble;
        }

        public static void UpdateDurationMulti(GameState state)
        {
            CDouble leftSide = state.CreatingPower;
            int num = 0;
            while (leftSide > 10 && num < 90)
            {
                num += 2;
                leftSide /= 8;
            }
            Creation.durationMulti = 100 - num;
        }

        public static string CurrentCreationSpeedText(GameState state)
        {
            if (!string.IsNullOrEmpty(Creation.creatingSpeedText) && !Creation.UpdateText)
            {
                return Creation.creatingSpeedText;
            }
            CDouble leftSide = state.CreatingPower;
            int num = 0;
            while (leftSide > 10 && num < 90)
            {
                num += 2;
                leftSide /= 8;
            }
            num = 100 - num;
            Creation.creatingSpeedText = (100.0 / (double)num * (double)state.PremiumBoni.CreationDopingDivider * (100 + state.PremiumBoni.CreatingSpeedUpPercent(true))).GuiText + " %";
            return Creation.creatingSpeedText;
        }

        public string Serialize()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Conv.AppendValue(stringBuilder, "a", (int)this.TypeEnum);
            Conv.AppendValue(stringBuilder, "b", this.GodToDefeat.Serialize());
            Conv.AppendValue(stringBuilder, "d", this.Count.Serialize());
            Conv.AppendValue(stringBuilder, "e", this.currentDuration.ToString());
            Conv.AppendValue(stringBuilder, "f", this.IsActive.ToString());
            Conv.AppendValue(stringBuilder, "g", this.TotalCreated.Serialize());
            Conv.AppendValue(stringBuilder, "h", this.CanBuy.ToString());
            Conv.AppendValue(stringBuilder, "i", this.NextAtCount);
            Conv.AppendValue(stringBuilder, "j", this.AutoBuy.ToString());
            return Conv.ToBase64(stringBuilder.ToString(), "Creation");
        }

        internal static Creation FromString(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                Log.Error("Creation.FromString with empty value!");
                return null;
            }
            string[] parts = Conv.StringPartsFromBase64(base64String, "Creation");
            Creation creation = new Creation((Creation.CreationType)Conv.getIntFromParts(parts, "a"), God.FromString(Conv.getStringFromParts(parts, "b")));
            creation.Count = new CDouble(Conv.getStringFromParts(parts, "d"));
            creation.Count.Round();
            creation.currentDuration = (long)Conv.getIntFromParts(parts, "e");
            creation.IsActive = Conv.getStringFromParts(parts, "f").ToLower().Equals("true");
            creation.TotalCreated = new CDouble(Conv.getStringFromParts(parts, "g"));
            creation.CanBuy = Conv.getStringFromParts(parts, "h").ToLower().Equals("true");
            creation.NextAtCount = Conv.getIntFromParts(parts, "i");
            string text = Conv.getStringFromParts(parts, "j").ToLower();
            creation.AutoBuy = (string.IsNullOrEmpty(text) || text.Equals("true"));
            if (creation.TotalCreated > 0)
            {
                creation.CanBuy = true;
            }
            return creation;
        }
    }
}
