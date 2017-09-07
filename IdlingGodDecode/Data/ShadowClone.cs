using Assets.Scripts.Helper;
using System;
using System.Text;

namespace Assets.Scripts.Data
{
	public class ShadowClone
	{
		public CDouble MaxCloneModifier = 0;

		public int absoluteMaximum = 99999;

		public CDouble TotalClonesCreated = 0;

		public CDouble TotalClonesKilled = 0;

		public CDouble maxShadowClones = 1000;

		public CDouble count = 0;

		public CDouble InUse = 0;

		public int AbsoluteMaximum
		{
			get
			{
				return this.absoluteMaximum - this.MaxCloneModifier.ToInt();
			}
			set
			{
				this.absoluteMaximum = value + this.MaxCloneModifier.ToInt();
			}
		}

		public int MaxShadowClonesRebirth
		{
			get
			{
				if (App.State != null && App.State.Statistic.HasStarted1kChallenge)
				{
					return 1000;
				}
				CDouble cDouble = this.MaxShadowClones + (this.TotalClonesCreated + this.TotalClonesKilled) / 20;
				if (cDouble > this.AbsoluteMaximum)
				{
					cDouble = this.AbsoluteMaximum;
				}
				return cDouble.ToInt();
			}
		}

		public CDouble MaxShadowClones
		{
			get
			{
				if (App.State != null && App.State.Statistic.HasStarted1kChallenge)
				{
					return 1000;
				}
				return this.maxShadowClones.ToInt();
			}
			set
			{
				this.maxShadowClones = value;
			}
		}

		public CDouble Count
		{
			get
			{
				if (this.count < 0)
				{
					return 0;
				}
				if (this.count > this.MaxShadowClones)
				{
					return this.MaxShadowClones.ToInt();
				}
				return this.count.ToInt();
			}
			set
			{
				this.count = value;
			}
		}

		public ShadowClone()
		{
			this.maxShadowClones = 1000;
		}

		public int IdleClones()
		{
			return (this.Count - this.InUse).ToInt();
		}

		public bool UseShadowClones(CDouble numberToUse)
		{
			if (this.Count >= numberToUse + this.InUse)
			{
				this.InUse += numberToUse;
				return true;
			}
			Log.Error("UseShadowClones failed: " + numberToUse);
			return false;
		}

		public bool RemoveUsedShadowClones(CDouble numberToRemove)
		{
			if (this.InUse >= numberToRemove)
			{
				this.InUse -= numberToRemove;
				return true;
			}
			Log.Error("RemoveUsedShadowClones failed: " + numberToRemove);
			return false;
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendValue(stringBuilder, "a", this.count.Serialize());
			Conv.AppendValue(stringBuilder, "b", this.InUse.Serialize());
			Conv.AppendValue(stringBuilder, "c", this.TotalClonesCreated.Serialize());
			Conv.AppendValue(stringBuilder, "d", this.TotalClonesKilled.Serialize());
			Conv.AppendValue(stringBuilder, "e", this.maxShadowClones.Serialize());
			Conv.AppendValue(stringBuilder, "f", this.AbsoluteMaximum);
			Conv.AppendValue(stringBuilder, "g", this.MaxCloneModifier.Serialize());
			return Conv.ToBase64(stringBuilder.ToString(), "ShadowClone");
		}

		internal static ShadowClone FromString(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("ShadowClone.FromString with empty value!");
				return new ShadowClone();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "ShadowClone");
			ShadowClone shadowClone = new ShadowClone();
			shadowClone.MaxCloneModifier = Conv.getCDoubleFromParts(parts, "g", false);
			shadowClone.count = Conv.getCDoubleFromParts(parts, "a", false);
			shadowClone.InUse = Conv.getCDoubleFromParts(parts, "b", false);
			shadowClone.TotalClonesCreated = Conv.getCDoubleFromParts(parts, "c", false);
			shadowClone.TotalClonesKilled = Conv.getCDoubleFromParts(parts, "d", false);
			shadowClone.AbsoluteMaximum = Conv.getIntFromParts(parts, "f");
			if (shadowClone.AbsoluteMaximum < 99999)
			{
				shadowClone.AbsoluteMaximum = 99999;
			}
			shadowClone.maxShadowClones = Conv.getCDoubleFromParts(parts, "e", false);
			return shadowClone;
		}

		internal string ToGuiText()
		{
			return "Clones: \n" + Conv.AddCommaSeparator(this.IdleClones()) + " / " + Conv.AddCommaSeparator(this.Count.ToInt());
		}
	}
}
