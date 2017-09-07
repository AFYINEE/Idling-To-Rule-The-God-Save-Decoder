using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data
{
	public class State2
	{
		public List<Pet> AllPets = new List<Pet>();

		public CDouble PunyFood = 0;

		public CDouble StrongFood = 0;

		public CDouble MightyFood = 0;

		public CDouble Chocolate = 0;

		public int PetIdInAvatar;

		public bool FacebookClicked;

		public bool TwitterClicked;

		public List<float> RandomValues = new List<float>();

		public int CurrentRandomNumber;

		public LuckyDraw Lucky = new LuckyDraw();

		public bool ImportedSaveFromKongToSteam;

		public Currency SteamCurrency;

		public string SteamCountry = string.Empty;

		public int KongConvertId;

		public CDouble AdPoints = 0;

		public int AdsWatched;

		public int TotalAdsWatched;

		public AfkyGame AfkGame = new AfkyGame();

		public bool RateDialogShown;

		public CrystalFactory Factory = new CrystalFactory();

		public List<PetCampaign> AllCampaigns = new List<PetCampaign>();

		public CDouble PetStones = 0;

		public CDouble PetStonesSpent = 0;

		public CDouble PetPowerMultiGods = 1;

		public CDouble PetPowerMultiCampaigns = 1;

		public long TimeSinceSteam = 5184000000L;

		private const long OneDayMS = 86400000L;

		private const long OneMonthMs = 2592000000L;

		public CDouble PetPowerMultiMonuments()
		{
			if (App.State == null)
			{
				return 1;
			}
			CDouble leftSide = 2;
			foreach (Monument current in App.State.AllMonuments)
			{
				leftSide += current.TotalMultiPhysical;
				leftSide += current.TotalMultiBattle;
				leftSide += current.TotalMultiMystic;
				leftSide += current.TotalMultiCreating;
			}
			return leftSide / 2;
		}

		public CDouble GetTotalPetPower(bool withBoost = true)
		{
			CDouble cDouble = 0;
			foreach (Pet current in this.AllPets)
			{
				if (current.CurrentHealth > 0 && !current.IsInCampaign)
				{
					cDouble += current.GetTotalStats();
				}
			}
			if (App.State != null)
			{
				cDouble = cDouble * (100 + App.State.UnleashAttackBoni) / 100;
				cDouble = cDouble * (100 + App.State.UnleashDefenseBoni) / 100;
			}
			if (withBoost)
			{
				return cDouble * this.PetPowerMultiGods * this.PetPowerMultiCampaigns * this.PetPowerMultiMonuments();
			}
			return cDouble;
		}

		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Conv.AppendList<Pet>(stringBuilder, this.AllPets, "b");
			Conv.AppendValue(stringBuilder, "c", this.PunyFood.Serialize());
			Conv.AppendValue(stringBuilder, "d", this.StrongFood.Serialize());
			Conv.AppendValue(stringBuilder, "e", this.MightyFood.Serialize());
			Conv.AppendValue(stringBuilder, "f", this.PetIdInAvatar);
			Conv.AppendList<float>(stringBuilder, this.RandomValues, "g");
			Conv.AppendValue(stringBuilder, "h", this.CurrentRandomNumber);
			Conv.AppendValue(stringBuilder, "i", this.FacebookClicked.ToString());
			Conv.AppendValue(stringBuilder, "j", this.TwitterClicked.ToString());
			Conv.AppendValue(stringBuilder, "k", this.Lucky.Serialize());
			Conv.AppendValue(stringBuilder, "l", this.ImportedSaveFromKongToSteam.ToString());
			Conv.AppendValue(stringBuilder, "m", (int)this.SteamCurrency);
			Conv.AppendValue(stringBuilder, "n", this.SteamCountry);
			Conv.AppendValue(stringBuilder, "o", this.KongConvertId);
			Conv.AppendValue(stringBuilder, "p", this.TimeSinceSteam);
			Conv.AppendValue(stringBuilder, "q", this.AdPoints.Serialize());
			Conv.AppendValue(stringBuilder, "r", this.AdsWatched);
			Conv.AppendValue(stringBuilder, "s", this.TotalAdsWatched);
			Conv.AppendValue(stringBuilder, "t", this.AfkGame.Serialize());
			Conv.AppendValue(stringBuilder, "u", this.RateDialogShown.ToString());
			Conv.AppendValue(stringBuilder, "v", this.Chocolate.Serialize());
			Conv.AppendValue(stringBuilder, "w", this.Factory.Serialize());
			Conv.AppendList<PetCampaign>(stringBuilder, this.AllCampaigns, "x");
			Conv.AppendValue(stringBuilder, "y", this.PetStones.Serialize());
			Conv.AppendValue(stringBuilder, "z", this.PetStonesSpent.Serialize());
			Conv.AppendValue(stringBuilder, "A", this.PetPowerMultiGods.Serialize());
			Conv.AppendValue(stringBuilder, "B", this.PetPowerMultiCampaigns.Serialize());
			return Conv.ToBase64(stringBuilder.ToString(), "State2");
		}

		internal static State2 Deserialize(string base64String)
		{
			if (string.IsNullOrEmpty(base64String))
			{
				Log.Error("State2.FromString with empty value!");
				return new State2();
			}
			string[] parts = Conv.StringPartsFromBase64(base64String, "State2");
			State2 state = new State2();
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
					state.AllPets.Add(Pet.Deserialize(text));
				}
			}
			Pet.CheckIfAllAdded(ref state.AllPets);
			state.PunyFood = Conv.getCDoubleFromParts(parts, "c", false);
			state.StrongFood = Conv.getCDoubleFromParts(parts, "d", false);
			state.MightyFood = Conv.getCDoubleFromParts(parts, "e", false);
			state.PetIdInAvatar = Conv.getIntFromParts(parts, "f");
			stringFromParts = Conv.getStringFromParts(parts, "g");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array3 = array;
			for (int j = 0; j < array3.Length; j++)
			{
				string value = array3[j];
				if (!string.IsNullOrEmpty(value))
				{
					state.RandomValues.Add(Conv.StringToFloat(value));
				}
			}
			if (state.RandomValues == null || state.RandomValues.Count == 0)
			{
				state.RandomizeNumbers();
			}
			state.CurrentRandomNumber = Conv.getIntFromParts(parts, "h");
			state.FacebookClicked = Conv.getStringFromParts(parts, "i").ToLower().Equals("true");
			state.TwitterClicked = Conv.getStringFromParts(parts, "j").ToLower().Equals("true");
			state.Lucky = LuckyDraw.Deserialize(Conv.getStringFromParts(parts, "k"));
			state.ImportedSaveFromKongToSteam = Conv.getStringFromParts(parts, "l").ToLower().Equals("true");
			state.SteamCurrency = (Currency)Conv.getIntFromParts(parts, "m");
			state.SteamCountry = Conv.getStringFromParts(parts, "n");
			state.KongConvertId = Conv.getIntFromParts(parts, "o");
			state.TimeSinceSteam = Conv.getLongFromParts(parts, "p");
			state.AdPoints = Conv.getCDoubleFromParts(parts, "q", false);
			state.AdsWatched = Conv.getIntFromParts(parts, "r");
			state.TotalAdsWatched = Conv.getIntFromParts(parts, "s");
			state.AfkGame = AfkyGame.FromString(Conv.getStringFromParts(parts, "t"));
			state.RateDialogShown = Conv.getStringFromParts(parts, "u").ToLower().Equals("true");
			state.Chocolate = Conv.getCDoubleFromParts(parts, "v", false);
			state.Factory = CrystalFactory.Deserialize(Conv.getStringFromParts(parts, "w"));
			stringFromParts = Conv.getStringFromParts(parts, "x");
			array = stringFromParts.Split(new char[]
			{
				'&'
			});
			string[] array4 = array;
			for (int k = 0; k < array4.Length; k++)
			{
				string text2 = array4[k];
				if (!string.IsNullOrEmpty(text2))
				{
					state.AllCampaigns.Add(PetCampaign.Deserialize(text2));
				}
			}
			if (state.AllCampaigns.Count == 0)
			{
				state.AllCampaigns = PetCampaign.Initial;
			}
			state.PetStones = Conv.getCDoubleFromParts(parts, "y", false);
			state.PetStonesSpent = Conv.getCDoubleFromParts(parts, "z", false);
			if (state.RandomValues == null || state.RandomValues.Count == 0)
			{
				state.RandomizeNumbers();
			}
			if (state.TimeSinceSteam < 5184000000L)
			{
				state.TimeSinceSteam = 5184000000L;
			}
			state.PetPowerMultiGods = Conv.getCDoubleFromParts(parts, "A", false);
			state.PetPowerMultiCampaigns = Conv.getCDoubleFromParts(parts, "B", false);
			return state;
		}

		public void RandomizeNumbers()
		{
			this.RandomValues = new List<float>();
			for (int i = 0; i < 100; i++)
			{
				this.RandomValues.Add(UnityEngine.Random.Range(0f, 0.9999f));
			}
		}

		public static int RandomInt(int min, int max, State2 state = null)
		{
			if (state == null && App.State != null)
			{
				state = App.State.Ext;
			}
			if (state == null)
			{
				return UnityEngine.Random.Range(min, max);
			}
			if (state.RandomValues.Count == 0)
			{
				state.RandomizeNumbers();
			}
			while (state.RandomValues[0] > 1f)
			{
				state.RandomValues[0] = state.RandomValues[0] / 10f;
			}
			int num = max - min;
			int num2 = (int)((float)num * state.RandomValues[0]);
			state.RandomValues.RemoveAt(0);
			state.RandomValues.Add(UnityEngine.Random.Range(0f, 0.9999f));
			return num2 + min;
		}
	}
}
