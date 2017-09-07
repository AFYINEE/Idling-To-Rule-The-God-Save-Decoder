using Assets.Scripts.Helper;
using System;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
	public class TrainingBase
	{
		public CDouble Level = 0;

		public CDouble ShadowCloneCount = 0;

		public int EnumValue;

		public long CurrentDuration;

		public bool ShouldUpdateText = true;

		private int countText;

		private string levelText;

		public string LevelText
		{
			get
			{
				this.countText++;
				if (this.levelText == null || this.countText > 5)
				{
					this.countText = 0;
					this.levelText = this.Level.ToGuiText(true);
				}
				return this.levelText;
			}
		}

		public CDouble PowerGain
		{
			get
			{
				switch (this.EnumValue)
				{
				case 0:
					return 1;
				case 1:
					return 3;
				case 2:
					return 5;
				case 3:
					return 7;
				case 4:
					return 10;
				case 5:
					return 17;
				case 6:
					return 30;
				case 7:
					return 47;
				case 8:
					return 73;
				case 9:
					return 114;
				case 10:
					return 173;
				case 11:
					return 253;
				case 12:
					return 374;
				case 13:
					return 535;
				case 14:
					return 758;
				case 15:
					return 1048;
				case 16:
					return 1424;
				case 17:
					return 1930;
				case 18:
					return 2582;
				case 19:
					return 3450;
				case 20:
					return 4601;
				case 21:
					return 6073;
				case 22:
					return 8005;
				case 23:
					return 10600;
				case 24:
					return 13991;
				case 25:
					return 18411;
				case 26:
					return 25185;
				case 27:
					return 35716;
				default:
					return new CDouble();
				}
			}
		}

		public void AddCloneCount(CDouble count)
		{
			CDouble availableClones = App.State.GetAvailableClones(this is Fight);
			if (count > availableClones)
			{
				count = availableClones;
			}
			if (count < 0)
			{
				count = 0;
			}
			App.State.Clones.UseShadowClones(count);
			this.ShadowCloneCount += count;
			this.ShadowCloneCount.Round();
			this.ShouldUpdateText = true;
		}

		public void RemoveCloneCount(CDouble count)
		{
			count.Round();
			this.ShadowCloneCount.Round();
			if (this.ShadowCloneCount <= count)
			{
				count = this.ShadowCloneCount;
			}
			this.ShadowCloneCount -= count;
			this.ShadowCloneCount.Round();
			App.State.Clones.RemoveUsedShadowClones(count);
			this.ShouldUpdateText = true;
		}

		public double getPercent()
		{
			return (double)this.CurrentDuration / (double)this.DurationInMS(1);
		}

		public void UpdateDuration(long ms)
		{
			if (this.ShadowCloneCount == 0)
			{
				return;
			}
			this.CurrentDuration += ms * (long)this.ShadowCloneCount.ToInt();
			int num = this.DurationInMS(1);
			if (this.CurrentDuration > (long)num)
			{
				this.CurrentDuration = 0L;
				CDouble cDouble = 0;
				if (App.State.GameSettings.IgnoreCloneCountOn)
				{
					cDouble = App.State.GameSettings.TrainIgnoreCount;
				}
				if (cDouble > this.ShadowCloneCount)
				{
					cDouble = this.ShadowCloneCount;
				}
				bool flag = true;
				int value = App.State.GameSettings.StopClonesAtSkills;
				bool flag2 = App.State.GameSettings.IsStopAtOnSkills;
				if (this is Training)
				{
					value = App.State.GameSettings.StopClonesAtTrainings;
					flag2 = App.State.GameSettings.IsStopAtOnTrainings;
				}
				if (this.ShadowCloneCount > cDouble && this.Level >= value && flag2)
				{
					flag = false;
					CDouble cDouble2 = this.ShadowCloneCount;
					if (!App.State.GameSettings.UseStopAt)
					{
						if (this.checkNext() || !App.State.PremiumBoni.ImprovedNextAt)
						{
							cDouble2 -= cDouble;
							this.RemoveCloneCount(cDouble2);
							if (this is Training)
							{
								Training training = App.State.AllTrainings.FirstOrDefault((Training x) => x.EnumValue == this.EnumValue + 1);
								if (training != null && training.IsAvailable)
								{
									training.AddCloneCount(cDouble2);
								}
							}
							else if (this is Skill)
							{
								Skill skill = App.State.AllSkills.FirstOrDefault((Skill x) => x.EnumValue == this.EnumValue + 1);
								if (skill != null && skill.IsAvailable)
								{
									skill.AddCloneCount(cDouble2);
								}
							}
						}
						else
						{
							flag = true;
						}
					}
					else
					{
						cDouble2 -= cDouble;
						this.RemoveCloneCount(cDouble2);
					}
				}
				if (flag)
				{
					this.Level = ++this.Level;
					if (this is Training)
					{
						App.State.PhysicalPowerBase += this.PowerGain;
						Statistic expr_255 = App.State.Statistic;
						expr_255.TotalTrainingLevels = ++expr_255.TotalTrainingLevels;
					}
					else if (this is Skill)
					{
						App.State.MysticPowerBase += this.PowerGain;
						Statistic expr_29A = App.State.Statistic;
						expr_29A.TotalSkillLevels = ++expr_29A.TotalSkillLevels;
					}
				}
			}
		}

		private bool checkNext()
		{
			if (this is Training)
			{
				Training training = App.State.AllTrainings.FirstOrDefault((Training x) => x.EnumValue == this.EnumValue + 1);
				if (training != null && training.IsAvailable)
				{
					return true;
				}
			}
			else if (this is Skill)
			{
				Skill skill = App.State.AllSkills.FirstOrDefault((Skill x) => x.EnumValue == this.EnumValue + 1);
				if (skill != null && skill.IsAvailable)
				{
					return true;
				}
			}
			return false;
		}

		public int DurationInMS(int shadowCloneCount)
		{
			int num = 999999999;
			switch (this.EnumValue)
			{
			case 0:
				num = 5000;
				break;
			case 1:
				num = 10000;
				break;
			case 2:
				num = 15000;
				break;
			case 3:
				num = 20000;
				break;
			case 4:
				num = 25000;
				break;
			case 5:
				num = 35000;
				break;
			case 6:
				num = 50000;
				break;
			case 7:
				num = 65000;
				break;
			case 8:
				num = 85000;
				break;
			case 9:
				num = 110000;
				break;
			case 10:
				num = 140000;
				break;
			case 11:
				num = 170000;
				break;
			case 12:
				num = 210000;
				break;
			case 13:
				num = 250000;
				break;
			case 14:
				num = 295000;
				break;
			case 15:
				num = 340000;
				break;
			case 16:
				num = 385000;
				break;
			case 17:
				num = 435000;
				break;
			case 18:
				num = 485000;
				break;
			case 19:
				num = 540000;
				break;
			case 20:
				num = 600000;
				break;
			case 21:
				num = 660000;
				break;
			case 22:
				num = 725000;
				break;
			case 23:
				num = 800000;
				break;
			case 24:
				num = 880000;
				break;
			case 25:
				num = 965000;
				break;
			case 26:
				num = 1100000;
				break;
			case 27:
				num = 1300000;
				break;
			}
			if (App.State != null)
			{
				Skill skill = App.State.AllSkills.FirstOrDefault((Skill x) => x.EnumValue == this.EnumValue);
				if (skill != null)
				{
					SkillExtension extension = skill.Extension;
					num -= (int)extension.UsageCount * 10;
				}
				if (num <= 29)
				{
					num = 29;
				}
			}
			if (shadowCloneCount == 0)
			{
				return num;
			}
			return num / shadowCloneCount;
		}

		public string Serialize(string debugName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.EnumValue);
			Conv.AppendValue(stringBuilder, "b", this.Level.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.ShadowCloneCount.Serialize());
			Conv.AppendValue(stringBuilder, "d", this.CurrentDuration);
			return Conv.ToBase64(stringBuilder.ToString(), debugName);
		}

		internal static TrainingBase FromString(string base64String, string debugName)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error(debugName + ".FromString with empty value!");
				return new TrainingBase();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, debugName);
			return new TrainingBase
			{
				EnumValue = Conv.getIntFromParts(parts, "a"),
				Level = new CDouble(Conv.getStringFromParts(parts, "b")),
				ShadowCloneCount = Conv.getCDoubleFromParts(parts, "c", false),
				CurrentDuration = Conv.getLongFromParts(parts, "d")
			};
		}
	}
}
