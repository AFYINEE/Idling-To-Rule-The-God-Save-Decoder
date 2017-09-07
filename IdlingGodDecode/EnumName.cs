using Assets.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Helper
{
    public class EnumName
    {
        public static string Name(Training.TrainingType enumValue)
        {
            switch (enumValue)
            {
                case Training.TrainingType.running:
                    return "Running";
                case Training.TrainingType.sit_ups:
                    return "Sit ups";
                case Training.TrainingType.push_ups:
                    return "Push ups";
                case Training.TrainingType.swimming:
                    return "Swimming";
                case Training.TrainingType.long_jumps:
                    return "Long jumps";
                case Training.TrainingType.shadow_boxing:
                    return "Shadow boxing";
                case Training.TrainingType.jump_rope:
                    return "Jump rope";
                case Training.TrainingType.climb_mountains:
                    return "Climb mountains";
                case Training.TrainingType.run_in_water:
                    return "Run in water";
                case Training.TrainingType.meditate:
                    return "Meditate";
                case Training.TrainingType.throw_spears:
                    return "Throw spears";
                case Training.TrainingType.smash_rocks:
                    return "Smash rocks";
                case Training.TrainingType.run_with_weights:
                    return "Run with weights";
                case Training.TrainingType.walk_on_tightropes:
                    return "Walk on tightropes";
                case Training.TrainingType.swimm_with_weights:
                    return "Swim with weights";
                case Training.TrainingType.dive_with_sharks:
                    return "Dive with sharks";
                case Training.TrainingType.jump_on_trees:
                    return "Jump on trees";
                case Training.TrainingType.walk_on_water:
                    return "Walk on water";
                case Training.TrainingType.walk_10_gravity:
                    return "Walk with 10x gravity";
                case Training.TrainingType.run_50_gravity:
                    return "Run with 50x gravity";
                case Training.TrainingType.move_mountains:
                    return "Move mountains";
                case Training.TrainingType.learn_to_fly:
                    return "Learn to fly";
                case Training.TrainingType.fly_around_the_world:
                    return "Fly around the world";
                case Training.TrainingType.carry_mountains:
                    return "Carry Mountains";
                case Training.TrainingType.fly_to_the_moon:
                    return "Fly to the moon";
                case Training.TrainingType.fly_around_the_universe:
                    return "Fly around the universe";
                case Training.TrainingType.smash_meteorites:
                    return "Smash meteorites";
                case Training.TrainingType.train_on_another_dimension:
                    return "Train on dimension X";
                default:
                    return "Unknown";
            }
        }

        public static string Name(Skill.SkillType enumValue)
        {
            switch (enumValue)
            {
                case Skill.SkillType.double_punch:
                    return "Double punch";
                case Skill.SkillType.high_kick:
                    return "High kick";
                case Skill.SkillType.dodge:
                    return "Dodge";
                case Skill.SkillType.shadow_fist:
                    return "Shadow fist";
                case Skill.SkillType.focused_breathing:
                    return "Focused breathing";
                case Skill.SkillType.raging_fist:
                    return "Raging fist";
                case Skill.SkillType.defensive_aura:
                    return "Defensive aura";
                case Skill.SkillType.misdirection:
                    return "Misdirection";
                case Skill.SkillType.whirling_foot:
                    return "Whirling foot";
                case Skill.SkillType.invisible_hand:
                    return "Invisible hand";
                case Skill.SkillType.dragon_fist:
                    return "Dragon fist";
                case Skill.SkillType.offense_aura:
                    return "Offense aura";
                case Skill.SkillType.elemental_manipulation:
                    return "Elemental manipulation";
                case Skill.SkillType.earth_armor:
                    return "Earth armor";
                case Skill.SkillType.ice_wall:
                    return "Ice wall";
                case Skill.SkillType.clairvoyance:
                    return "Clairvoyance";
                case Skill.SkillType.aura_ball:
                    return "Aura ball";
                case Skill.SkillType.mystic_mode:
                    return "Mystic mode";
                case Skill.SkillType.a_108_star_fist:
                    return "108 fists of destiny";
                case Skill.SkillType.big_bang:
                    return "Big bang";
                case Skill.SkillType.god_speed:
                    return "God speed";
                case Skill.SkillType.teleport:
                    return "Teleport";
                case Skill.SkillType.transformation_aura:
                    return "Transformation aura";
                case Skill.SkillType.gear_eyes:
                    return "Gear eyes";
                case Skill.SkillType.reflection_barrier:
                    return "Reflection barrier";
                case Skill.SkillType.ionioi_hero_summon:
                    return "Ionioi hero summon";
                case Skill.SkillType.unlimited_creation_works:
                    return "Unlimited creation works";
                case Skill.SkillType.time_manipulation:
                    return "Time manipulation";
                default:
                    return "Unknown";
            }
        }

        public static string Name(Fight.FightType enumValue)
        {
            switch (enumValue)
            {
                case Fight.FightType.slimy:
                    return "Slimy";
                case Fight.FightType.frog:
                    return "Frog";
                case Fight.FightType.bunny:
                    return "Bunny";
                case Fight.FightType.gobiln:
                    return "Goblin";
                case Fight.FightType.wolf:
                    return "Wolf";
                case Fight.FightType.kobold:
                    return "Kobold";
                case Fight.FightType.big_food:
                    return "Big burger";
                case Fight.FightType.skeleton:
                    return "Skeleton";
                case Fight.FightType.zombie:
                    return "Zombie";
                case Fight.FightType.harpy:
                    return "Harpy";
                case Fight.FightType.orc:
                    return "Orc";
                case Fight.FightType.mummy:
                    return "Mummy";
                case Fight.FightType.fighting_turtle:
                    return "Fighting turtle";
                case Fight.FightType.ape:
                    return "Ape";
                case Fight.FightType.salamander:
                    return "Salamander";
                case Fight.FightType.golem:
                    return "Golem";
                case Fight.FightType.dullahan:
                    return "Dullahan";
                case Fight.FightType.succubus:
                    return "Succubus";
                case Fight.FightType.minotaurus:
                    return "Minotaurus";
                case Fight.FightType.devil:
                    return "Devil";
                case Fight.FightType.gargoyle:
                    return "Gargoyle";
                case Fight.FightType.demon:
                    return "Demon";
                case Fight.FightType.vampire:
                    return "Vampire";
                case Fight.FightType.lamia:
                    return "Lamia";
                case Fight.FightType.dragon:
                    return "Dragon";
                case Fight.FightType.behemoth:
                    return "Behemoth";
                case Fight.FightType.valkyrie:
                    return "Valkyrie";
                case Fight.FightType.nine_tailed_fox:
                    return "Nine tailed fox";
                case Fight.FightType.genbu:
                    return "Genbu";
                case Fight.FightType.byakko:
                    return "Byakko";
                case Fight.FightType.suzaku:
                    return "Suzaku";
                case Fight.FightType.seiryuu:
                    return "Seiryuu";
                case Fight.FightType.godzilla:
                    return "Godzilla";
                case Fight.FightType.monster_queen:
                    return "Monster Queen";
                default:
                    return "Unknown";
            }
        }

        public static string Name(Creation.CreationType enumValue)
        {
            switch (enumValue)
            {
                case Creation.CreationType.Shadow_clone:
                    return "Shadow clone";
                case Creation.CreationType.Light:
                    return "Light";
                case Creation.CreationType.Stone:
                    return "Stone";
                case Creation.CreationType.Soil:
                    return "Soil";
                case Creation.CreationType.Air:
                    return "Air";
                case Creation.CreationType.Water:
                    return "Water";
                case Creation.CreationType.Plant:
                    return "Plant";
                case Creation.CreationType.Tree:
                    return "Tree";
                case Creation.CreationType.Fish:
                    return "Fish";
                case Creation.CreationType.Animal:
                    return "Animal";
                case Creation.CreationType.Human:
                    return "Human";
                case Creation.CreationType.River:
                    return "River";
                case Creation.CreationType.Mountain:
                    return "Mountain";
                case Creation.CreationType.Forest:
                    return "Forest";
                case Creation.CreationType.Village:
                    return "Village";
                case Creation.CreationType.Town:
                    return "Town";
                case Creation.CreationType.Ocean:
                    return "Ocean";
                case Creation.CreationType.Nation:
                    return "Nation";
                case Creation.CreationType.Continent:
                    return "Continent";
                case Creation.CreationType.Weather:
                    return "Weather";
                case Creation.CreationType.Sky:
                    return "Sky";
                case Creation.CreationType.Night:
                    return "Night";
                case Creation.CreationType.Moon:
                    return "Moon";
                case Creation.CreationType.Planet:
                    return "Planet";
                case Creation.CreationType.Earthlike_planet:
                    return "Earthlike planet";
                case Creation.CreationType.Sun:
                    return "Sun";
                case Creation.CreationType.Solar_system:
                    return "Solar system";
                case Creation.CreationType.Galaxy:
                    return "Galaxy";
                case Creation.CreationType.Universe:
                    return "Universe";
                default:
                    return string.Empty;
            }
        }

        public static string Name(God.GodType enumValue)
        {
            switch (enumValue)
            {
                case God.GodType.Hyperion:
                    return "Hyperion";
                case God.GodType.Itztli:
                    return "Itztli";
                case God.GodType.Gaia:
                    return "Gaia";
                case God.GodType.Shu:
                    return "Shu";
                case God.GodType.Suijin:
                    return "Suijin";
                case God.GodType.Gefion:
                    return "Gefion";
                case God.GodType.Hathor:
                    return "Hathor";
                case God.GodType.Pontus:
                    return "Pontus";
                case God.GodType.Diana:
                    return "Diana";
                case God.GodType.Izanagi:
                    return "Izanagi";
                case God.GodType.Nephthys:
                    return "Nephthys";
                case God.GodType.Cybele:
                    return "Cybele";
                case God.GodType.Artemis:
                    return "Artemis";
                case God.GodType.Eros:
                    return "Eros";
                case God.GodType.Freya:
                    return "Freya";
                case God.GodType.Poseidon:
                    return "Poseidon";
                case God.GodType.Laima:
                    return "Laima";
                case God.GodType.Athena:
                    return "Athena";
                case God.GodType.Susano_o:
                    return "Susano o";
                case God.GodType.Zeus:
                    return "Zeus";
                case God.GodType.Nyx:
                    return "Nyx";
                case God.GodType.Luna:
                    return "Luna";
                case God.GodType.Jupiter:
                    return "Jupiter";
                case God.GodType.Odin:
                    return "Odin";
                case God.GodType.Amaterasu:
                    return "Amaterasu";
                case God.GodType.Coatlicue:
                    return "Coatlicue";
                case God.GodType.Chronos:
                    return "Chronos";
                case God.GodType.Tyrant_Overlord_Baal:
                    return "Tyrant Overlord Baal";
                default:
                    return string.Empty;
            }
        }

        internal static string Name(Monument.MonumentType enumValue)
        {
            switch (enumValue)
            {
                case Monument.MonumentType.mighty_statue:
                    return "Mighty Statue";
                case Monument.MonumentType.mystic_garden:
                    return "Mystic Garden";
                case Monument.MonumentType.tomb_of_god:
                    return "Tomb of Gods";
                case Monument.MonumentType.everlasting_lighthouse:
                    return "Everlasting Lighthouse";
                case Monument.MonumentType.godly_statue:
                    return "Godly Statue";
                case Monument.MonumentType.pyramids_of_power:
                    return "Pyramids of Power";
                case Monument.MonumentType.temple_of_god:
                    return "Temple of God";
                case Monument.MonumentType.black_hole:
                    return "Black Hole";
                default:
                    return string.Empty;
            }
        }

        internal static string Title(bool isFemale, God.GodType godType)
        {
            if (isFemale)
            {
                switch (godType)
                {
                    case God.GodType.Hyperion:
                        return "Powerless Goddess";
                    case God.GodType.Itztli:
                        return "Weak Goddess";
                    case God.GodType.Gaia:
                        return "Beginner Goddess";
                    case God.GodType.Shu:
                        return "Struggling Goddess";
                    case God.GodType.Suijin:
                        return "Rookie Goddess";
                    case God.GodType.Gefion:
                        return "Not so powerless Goddess";
                    case God.GodType.Hathor:
                        return "10. Class Goddess";
                    case God.GodType.Pontus:
                        return "Healthy Goddess";
                    case God.GodType.Diana:
                        return "Hard working Goddess";
                    case God.GodType.Izanagi:
                        return "Serious Goddess";
                    case God.GodType.Nephthys:
                        return "Talented Goddess";
                    case God.GodType.Cybele:
                        return "Skillful Goddess";
                    case God.GodType.Artemis:
                        return "Advanced Goddess";
                    case God.GodType.Eros:
                        return "Established Goddess";
                    case God.GodType.Freya:
                        return "5. Class Goddess";
                    case God.GodType.Poseidon:
                        return "Well-known Goddess";
                    case God.GodType.Laima:
                        return "Powerful Goddess";
                    case God.GodType.Athena:
                        return "Superior Goddess";
                    case God.GodType.Susano_o:
                        return "Magnificent Goddess";
                    case God.GodType.Zeus:
                        return "Ideal Goddess";
                    case God.GodType.Nyx:
                        return "1. Class Goddess";
                    case God.GodType.Luna:
                        return "Mighty Goddess";
                    case God.GodType.Jupiter:
                        return "Genius Goddess";
                    case God.GodType.Odin:
                        return "Legendary Goddess";
                    case God.GodType.Amaterasu:
                        return "Queen of Gods";
                    case God.GodType.Coatlicue:
                        return "Almighty Goddess";
                    case God.GodType.Chronos:
                        return "Almighty Goddess of Eternity";
                    case God.GodType.Tyrant_Overlord_Baal:
                        return "Strongest Entity in the Universe?";
                }
            }
            else
            {
                switch (godType)
                {
                    case God.GodType.Hyperion:
                        return "Powerless God";
                    case God.GodType.Itztli:
                        return "Weak God";
                    case God.GodType.Gaia:
                        return "Beginner God";
                    case God.GodType.Shu:
                        return "Struggling God";
                    case God.GodType.Suijin:
                        return "Rookie God";
                    case God.GodType.Gefion:
                        return "Not so powerless God";
                    case God.GodType.Hathor:
                        return "10. Class God";
                    case God.GodType.Pontus:
                        return "Healthy God";
                    case God.GodType.Diana:
                        return "Hard working God";
                    case God.GodType.Izanagi:
                        return "Serious God";
                    case God.GodType.Nephthys:
                        return "Talented God";
                    case God.GodType.Cybele:
                        return "Skillful God";
                    case God.GodType.Artemis:
                        return "Advanced God";
                    case God.GodType.Eros:
                        return "Established God";
                    case God.GodType.Freya:
                        return "5. Class God";
                    case God.GodType.Poseidon:
                        return "Well-known God";
                    case God.GodType.Laima:
                        return "Powerful God";
                    case God.GodType.Athena:
                        return "Superior God";
                    case God.GodType.Susano_o:
                        return "Magnificent God";
                    case God.GodType.Zeus:
                        return "Ideal God";
                    case God.GodType.Nyx:
                        return "1. Class God";
                    case God.GodType.Luna:
                        return "Mighty God";
                    case God.GodType.Jupiter:
                        return "Genius God";
                    case God.GodType.Odin:
                        return "Legendary God";
                    case God.GodType.Amaterasu:
                        return "King of Gods";
                    case God.GodType.Coatlicue:
                        return "Almighty God";
                    case God.GodType.Chronos:
                        return "Almighty God of Eternity";
                    case God.GodType.Tyrant_Overlord_Baal:
                        return "Strongest Entity in the Universe?";
                }
            }
            return string.Empty;
        }

        public static string Name(Might.MightType enumValue)
        {
            switch (enumValue)
            {
                case Might.MightType.physical_hp:
                    return "Physical HP +";
                case Might.MightType.physical_attack:
                    return "Physical Attack +";
                case Might.MightType.mystic_def:
                    return "Mystic Defense +";
                case Might.MightType.mystic_regen:
                    return "Mystic Regen +";
                case Might.MightType.battle:
                    return "Battle Might +";
                case Might.MightType.autofill_gen:
                    return "Clones on Divinity +";
                case Might.MightType.planet_power:
                    return "Clones on Planet +";
                case Might.MightType.powersurge_speed:
                    return "Powersurge +";
                case Might.MightType.focused_breathing:
                    return "Focused Breathing +";
                case Might.MightType.defensive_aura:
                    return "Defensive Aura +";
                case Might.MightType.offense_aura:
                    return "Offensive Aura +";
                case Might.MightType.elemental_manipulation:
                    return "Elemental Manipulation +";
                case Might.MightType.mystic_mode:
                    return "Mystic Mode +";
                case Might.MightType.transformation_aura:
                    return "Transformation Aura +";
                default:
                    return string.Empty;
            }
        }

        public static string Description(PetType enumValue)
        {
            switch (enumValue)
            {
                case PetType.Mouse:
                    return "A cute little mouse. Not to confuse with a computer mouse. But don't underestimate it! This mouse can steal cheese faster than you can even look! You need to beat Hyperion to get it.";
                case PetType.Rabbit:
                    return "Not to be confused with a bunny on Gaia. The name is different, it starts out even weaker! But it will become strong. Probably. Defeat P.Baal V5 to get it.";
                case PetType.Cat:
                    return "Last survivor of the planet Catomaro. Somehow it ended up as P.Baal V10's pet. If you beat him, you might catch the cat!";
                case PetType.Dog:
                    return "P.Baal V15's fighting Dog. It is totally loyal to its owner. At least as long as the owner feeds it.";
                case PetType.Fairy:
                    return "A small fairy searching for trouble all the time. It likes to hide planets from P.Baal V20. It usually takes him days to find them again!";
                case PetType.Dragon:
                    return "You might think that is the 'Dragon' monster. If you did, you are sooo wrong about that! It is not a puny monster but a cute little baby dragon! It is also P.Baal V25's pet.";
                case PetType.Doughnut:
                    return "It smells sweet and looks delicious. It can talk and if you believe what it says, you will die on the spot if you eat it! \nNo one tried to find out if it tells the truth yet, though.";
                case PetType.Eagle:
                    return "A space eagle from the 42nd dimension in the fourth galaxy, where it escaped from a supernova just before the galaxy was wiped out. So it is pretty fast.";
                case PetType.Phoenix:
                    return "A really big bird. Flames surround it and it will revive after it is killed without fail. But it is also pretty strong, so that only happens once in a while.";
                case PetType.Squirrel:
                    return "It likes to run around in Tyrant Overlord Baals' garden. He doesn't let anyone else into his garden, so you need to defeat him to get it.";
                case PetType.Turtle:
                    return "It is never in a hurry, never stressed and takes everything slowly. A really patient pet, just the way you need to be to beat the Ultimate Arty Challenge.";
                case PetType.Penguin:
                    return "Once it tried to go as a P.Baal but it failed miserably. \nA real P.Baal knocked it out while trying to walk towards him. Or more like the air surrounding him was enough to knock it out.";
                case PetType.Cupid:
                    return "Eros' little pet. It likes to shoot humans through the heart and talks about something called 'love' all the time. It also likes to eat chocolate a lot.";
                case PetType.Camel:
                    return "Can stay alive without water for a long period of time. It has achieved a lot, but doesn't believe in gods.";
                case PetType.Goat:
                    return "Likes to feed on grass all the time and wants everything for himself. It is also really peaceful, no killing allowed!";
                case PetType.Mole:
                    return "It is hard to see because it usually hides in underground. It is a big fan of Jacky Lee and has seen all his movies. It believes that attack is the best defense.";
                case PetType.Octopus:
                    return "Just like the shark its home is in the oceans. It looks weaker than a shark, but that is just to fool their predators to prey on them. P.Baal V40 liked to watch it and took it away.";
                case PetType.Pegasus:
                    return "It was once a horse which wanted to fly and kept jumping on trees. After falling to the ground millions of times, its skin became blue and it grew wings.";
                case PetType.Robot:
                    return "A robot which looks like a human in armor and can fly. Someone back on Earth invented it, then it escaped to this universe and never came backk.";
                case PetType.Shark:
                    return "It likes to swim in salty water and eats everything it finds in there. P.Baal V35 found that funny and kept it as his pet.";
                case PetType.Slime:
                    return "P.Baal V50 liked to experiment with slimies a lot and evolved one of them into ever stronger versions. This slime is the result.";
                case PetType.Snake:
                    return "P.Baal V30 once had a girlfriend with snakes on her head. His girlfriend is gone now, but one of the snakes is still there. Since then he kept it as his pet.";
                case PetType.Ufo:
                    return "It flies through outer space from planet to planet without ever landing once. Nobody will believe you if you say you saw it flying around.";
                case PetType.Wizard:
                    return "He has read a lot of books about spells and such. In his younger years, he never understood the way to cast the spells. Now he has become really old and can cast them with his finger snaps.";
                default:
                    return string.Empty;
            }
        }

        public static string Description(Campaigns campaign)
        {
            switch (campaign)
            {
                case Campaigns.Growth:
                    return "Increases the growth of the weakest pet who takes part. The growth gain is higher with higher growth of the other pets.";
                case Campaigns.Divinity:
                    return "You can earn divinity with this campaign. The higher the stats of the pets who take part, the more you will receive.";
                case Campaigns.Food:
                    return "You can find pet food here. Anything from puny food to mighty food. \nThe stats of your pets don't matter here.";
                case Campaigns.Item:
                    return "Search for pet stones. The higher the stats of the pets who take part, the more you will receive. You also have a chance to find rare items like godly liquid, chakra pill or a lucky draw!";
                case Campaigns.Level:
                    return "If your pets are bored of fighting clones, this is the right campaign for them. You will gain levels. The higher the growth and stats of the pets, the more levels you gain.";
                case Campaigns.Multiplier:
                    return "Increases the pet multiplier and even the cap of the pet rebirth multiplier.";
                case Campaigns.GodPower:
                    return "Your pets have a chance to find one or more god power. The higher the total stats of the pets, the higher the chance to find it.";
                default:
                    return string.Empty;
            }
        }

        public static string Name(KeyCode enumValue)
        {
            switch (enumValue)
            {
                case KeyCode.A:
                    return "A";
                case KeyCode.B:
                    return "B";
                case KeyCode.C:
                    return "C";
                case KeyCode.D:
                    return "D";
                case KeyCode.E:
                    return "E";
                case KeyCode.F:
                    return "F";
                case KeyCode.G:
                    return "G";
                case KeyCode.H:
                    return "H";
                case KeyCode.I:
                    return "I";
                case KeyCode.J:
                    return "J";
                case KeyCode.K:
                    return "K";
                case KeyCode.L:
                    return "L";
                case KeyCode.M:
                    return "M";
                case KeyCode.N:
                    return "N";
                case KeyCode.O:
                    return "O";
                case KeyCode.P:
                    return "P";
                case KeyCode.Q:
                    return "Q";
                case KeyCode.R:
                    return "R";
                case KeyCode.S:
                    return "S";
                case KeyCode.T:
                    return "T";
                case KeyCode.U:
                    return "U";
                case KeyCode.V:
                    return "V";
                case KeyCode.W:
                    return "W";
                case KeyCode.X:
                    return "X";
                case KeyCode.Y:
                    return "Y";
                case KeyCode.Z:
                    return "Z";
                default:
                    switch (enumValue)
                    {
                        case KeyCode.Alpha0:
                            return "0";
                        case KeyCode.Alpha1:
                            return "1";
                        case KeyCode.Alpha2:
                            return "2";
                        case KeyCode.Alpha3:
                            return "3";
                        case KeyCode.Alpha4:
                            return "4";
                        case KeyCode.Alpha5:
                            return "5";
                        case KeyCode.Alpha6:
                            return "6";
                        case KeyCode.Alpha7:
                            return "7";
                        case KeyCode.Alpha8:
                            return "8";
                        case KeyCode.Alpha9:
                            return "9";
                        default:
                            switch (enumValue)
                            {
                                case KeyCode.Keypad0:
                                    return "0";
                                case KeyCode.Keypad1:
                                    return "1";
                                case KeyCode.Keypad2:
                                    return "2";
                                case KeyCode.Keypad3:
                                    return "3";
                                case KeyCode.Keypad4:
                                    return "4";
                                case KeyCode.Keypad5:
                                    return "5";
                                case KeyCode.Keypad6:
                                    return "6";
                                case KeyCode.Keypad7:
                                    return "7";
                                case KeyCode.Keypad8:
                                    return "8";
                                case KeyCode.Keypad9:
                                    return "9";
                                default:
                                    if (enumValue != KeyCode.None)
                                    {
                                        return enumValue.ToString();
                                    }
                                    return "Unknown";
                            }
                            break;
                    }
                    break;
            }
        }
    }
}
