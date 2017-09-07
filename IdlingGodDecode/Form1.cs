using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assets.Scripts.Data;
using Assets.Scripts.Save;

namespace IdlingGodDecode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string DecompressString(string compressedText)
        {
            return Encoding.UTF8.GetString(CLZF2.Decompress(Convert.FromBase64String(compressedText)));
        }
        private void BtnDecode_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                GameState gs = Storage.FromCompressedString(richTextBox1.Text);
                String str = "";
                foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(gs))
                {
                    string name=descriptor.Name;
                    object value=descriptor.GetValue(gs);
                    //Console.WriteLine("{0}={1}",name,value);
                    str += "{" + name + "}={" + value + "}\n";
                }
                richTextBox1.Text = str;

            }
        }


            //public void CheckForCheats()
            //{
            //  CDouble cdouble1 = (CDouble) 0;
            //  bool flag = false;
            //  foreach (Fight allFight in this.AllFights)
            //  {
            //    allFight.ShadowCloneCount.Round();
            //    if (allFight.ShadowCloneCount < (CDouble) 0)
            //      flag = true;
            //    cdouble1 += allFight.ShadowCloneCount;
            //  }
            //  foreach (Training allTraining in this.AllTrainings)
            //  {
            //    allTraining.ShadowCloneCount.Round();
            //    if (allTraining.ShadowCloneCount < (CDouble) 0)
            //      flag = true;
            //    cdouble1 += allTraining.ShadowCloneCount;
            //    if (allTraining.CurrentDuration < 0L)
            //      allTraining.CurrentDuration = 0L;
            //  }
            //  foreach (Skill allSkill in this.AllSkills)
            //  {
            //    allSkill.ShadowCloneCount.Round();
            //    if (allSkill.ShadowCloneCount < (CDouble) 0)
            //      flag = true;
            //    cdouble1 += allSkill.ShadowCloneCount;
            //    if (allSkill.Extension.SkillId != allSkill.EnumValue)
            //      allSkill.Extension = new SkillExtension(allSkill.EnumValue);
            //    if (allSkill.CurrentDuration < 0L)
            //      allSkill.CurrentDuration = 0L;
            //    if (allSkill.Extension.UsageCount < 0L)
            //      allSkill.Extension.UsageCount = 0L;
            //  }
            //  foreach (Monument allMonument in this.AllMonuments)
            //  {
            //    allMonument.ShadowCloneCount.Round();
            //    allMonument.Upgrade.ShadowCloneCount.Round();
            //    if (allMonument.ShadowCloneCount < (CDouble) 0)
            //      flag = true;
            //    if (allMonument.Upgrade.ShadowCloneCount < (CDouble) 0)
            //      flag = true;
            //    cdouble1 += allMonument.ShadowCloneCount;
            //    cdouble1 += allMonument.Upgrade.ShadowCloneCount;
            //  }
            //  foreach (Might allMight in this.AllMights)
            //  {
            //    allMight.ShadowCloneCount.Round();
            //    if (allMight.ShadowCloneCount < (CDouble) 0)
            //      flag = true;
            //    cdouble1 += allMight.ShadowCloneCount;
            //  }
            //  this.HomePlanet.RoudClones();
            //  if (this.HomePlanet.ShadowCloneCount < (CDouble) 0)
            //    flag = true;
            //  CDouble cdouble2 = cdouble1 + this.HomePlanet.ShadowCloneCount;
            //  this.Generator.ShadowCloneCount.Round();
            //  if (this.Generator.ShadowCloneCount < (CDouble) 0)
            //    flag = true;
            //  CDouble cdouble3 = cdouble2 + this.Generator.ShadowCloneCount;
            //  foreach (GeneratorUpgrade upgrade in this.Generator.Upgrades)
            //  {
            //    upgrade.ShadowCloneCount.Round();
            //    if (upgrade.ShadowCloneCount < (CDouble) 0)
            //      flag = true;
            //    cdouble3 += upgrade.ShadowCloneCount;
            //  }
            //  foreach (Pet allPet in this.Ext.AllPets)
            //  {
            //    if (allPet.IsUnlocked)
            //    {
            //      allPet.ShadowCloneCount.Round();
            //      if (allPet.ShadowCloneCount < (CDouble) 0)
            //        flag = true;
            //      cdouble3 += (CDouble) allPet.ShadowCloneCount.ToInt();
            //    }
            //  }
            //  CDouble cdouble4 = cdouble3 + this.Ext.Factory.DefenderClones;
            //  foreach (FactoryModule allModule in this.Ext.Factory.AllModules)
            //    cdouble4 += allModule.ShadowClones;
            //  cdouble4.Round();
            //  if (cdouble4.ToInt() > this.Clones.MaxShadowClones.ToInt() || flag)
            //    this.RemoveAllClones(false);
            //  if (cdouble4 < (CDouble) 0 || cdouble4 > (CDouble) this.Clones.AbsoluteMaximum || this.Statistic.MonumentsCreated > (CDouble) 999999999)
            //  {
            //    this.PossibleCheater = true;
            //    this.ShouldSubmitScore = false;
            //  }
            //  int num1 = (int) ((this.Statistic.TimePlayed / 1000L + this.Statistic.TimeOffline) / 3600L / 24L);
            //  if (this.Statistic.TotalRebirths < (CDouble) 100 && this.Statistic.MonumentsCreated > (CDouble) 2500 * (this.Statistic.TotalRebirths + (CDouble) num1))
            //    this.PossibleCheater = true;
            //  if (this.Statistic.TotalRebirths < (CDouble) 500 && this.Statistic.MonumentsCreated > (CDouble) 5000 * (this.Statistic.TotalRebirths + (CDouble) num1))
            //    this.PossibleCheater = true;
            //  string[] strArray = (string[]) null;
            //  if (!string.IsNullOrEmpty(this.PremiumBoni.SteamPurchasedOrderIds))
            //    strArray = this.PremiumBoni.SteamPurchasedOrderIds.Split(',');
            //  int totalItemsBought = this.PremiumBoni.TotalItemsBought;
            //  if (strArray != null)
            //    totalItemsBought += strArray.Length;
            //  int num2 = num1;
            //  if (totalItemsBought > 0)
            //  {
            //    num2 = num2 * 2 + totalItemsBought * 50;
            //    this.PossibleCheater = false;
            //  }
            //  int num3 = this.PremiumBoni.GPFromLuckyDraws.ToInt();
            //  if (num3 > 5 * num2 + 200)
            //    num3 = 5 * num2 + 200;
            //  this.Clones.InUse = this.Clones.MaxShadowClones - (this.Clones.MaxShadowClones - (CDouble) cdouble4.ToInt());
            //  int num4 = 20 + this.Statistic.TotalGodsDefeated.ToInt() + this.HomePlanet.TotalGainedGodPower + this.Statistic.ArtyChallengesFinished.ToInt() * 200 + this.Statistic.UltimateBaalChallengesFinished.ToInt() * 100 + this.Statistic.DoubleRebirthChallengesFinished.ToInt() * 10 + this.Statistic.GPFromBlackHole.ToInt() + this.Statistic.GPFromBlackHoleUpgrade.ToInt() + num3 + this.PremiumBoni.GodPowerBought.ToInt() + 25 + this.PremiumBoni.GodPowerFromCrystals.ToInt() + this.PremiumBoni.GodPowerFromPets.ToInt();
            //  if (this.PremiumBoni.TotalItemsBought > 0)
            //    num4 = num4 + this.PremiumBoni.TotalItemsBought * 150 + 150;
            //  if (this.PossibleCheater || this.PremiumBoni.CalculateGPSpent(this) + this.PremiumBoni.GodPower > num4 || (this.Clones.InUse > this.Clones.MaxShadowClones || this.Clones.InUse < (CDouble) 0))
            //  {
            //    this.PossibleCheater = true;
            //    this.ShouldSubmitScore = false;
            //    if (!(this.Clones.InUse < (CDouble) 0))
            //      return;
            //    this.Clones.InUse = (CDouble) 0;
            //  }
            //  else
            //    this.PossibleCheater = false;
            //}
        }
    }

