using Assets.Scripts.Data;
using Assets.Scripts.Gui;
using System;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Helper
{
	public class Kongregate : MonoBehaviour
	{
		private static bool m_IsGuest;

		private static string m_Username = string.Empty;

		private static string m_UserId = string.Empty;

		private static string m_Items = string.Empty;

		private static string m_GameAuthToken = string.Empty;

		public static bool APILoaded;

		public static bool IsWaitingForPurchase;

		public static bool IsGuest
		{
			get
			{
				if (!Kongregate.m_IsGuest && string.IsNullOrEmpty(Kongregate.m_Username))
				{
					Kongregate.CallAPIFunction("kongregate.services.isGuest()", "SetIsGuest");
				}
				return Kongregate.m_IsGuest;
			}
		}

		public static string Username
		{
			get
			{
				if (string.IsNullOrEmpty(Kongregate.m_Username))
				{
					Kongregate.CallAPIFunction("kongregate.services.getUsername()", "SetUsername");
				}
				return Kongregate.m_Username;
			}
		}

		public static string UserId
		{
			get
			{
				if (string.IsNullOrEmpty(Kongregate.m_UserId))
				{
					Kongregate.CallAPIFunction("kongregate.services.getUserId()", "SetUserId");
				}
				return Kongregate.m_UserId;
			}
		}

		public static string GameToken
		{
			get
			{
				return Kongregate.m_GameAuthToken;
			}
		}

		private void Start()
		{
			Application.ExternalCall("getKongData", new object[0]);
		}

		public static void SetIsGuest(object returnValue)
		{
			if (bool.TryParse(returnValue.ToString(), out Kongregate.m_IsGuest) && Kongregate.m_IsGuest)
			{
				Kongregate.m_Username = string.Empty;
			}
		}

		public static void SetUsername(object returnValue)
		{
			Kongregate.m_Username = returnValue.ToString();
		}

		private static void SetUserId(object returnValue)
		{
			Kongregate.m_UserId = returnValue.ToString();
		}

		private void SetUserItems(object returnValue)
		{
			Kongregate.m_Items = returnValue.ToString();
			string[] array = (!string.IsNullOrEmpty(Kongregate.m_Items)) ? Kongregate.m_Items.Split(new char[]
			{
				','
			}) : new string[0];
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string arg = array2[i];
				stringBuilder.Append(num + ". " + arg).Append("\n");
				num++;
			}
			string text = string.Empty;
			if (array.Length > App.State.PremiumBoni.TotalItemsBought)
			{
				text = App.State.PremiumBoni.GetItemNameAndAddItem(App.State.PremiumBoni.ItemToPurchase, Purchase.None);
			}
			App.State.PremiumBoni.TotalItemsBought = array.Length;
			Kongregate.IsWaitingForPurchase = false;
			if (!string.IsNullOrEmpty(text))
			{
				App.SaveGameState();
				GuiBase.ShowBigMessage("Thank you for your purchase!\n" + text);
			}
			else
			{
				int num2 = App.State.KredProblems.Check();
				if (num2 > 0)
				{
					GuiBase.ShowBigMessage("Thank you for your purchase!\nYou received " + num2 + " god power.");
				}
			}
		}

		public static void CheckBoughtItems()
		{
			Kongregate.CallAPIFunction("getUserItems()");
		}

		public static void ShowSignIn()
		{
			Kongregate.CallAPIFunction("if(kongregate.services.isGuest()) kongregate.services.showSignInBox()");
		}

		public static void SubmitStat(string statistic, int value, bool force = false)
		{
			if (((App.CurrentPlattform == Plattform.Kongregate && App.State.ShouldSubmitScore) || (App.CurrentPlattform == Plattform.Kongregate && force)) && (force || !App.State.PossibleCheater))
			{
				Kongregate.CallAPIFunction(string.Format("kongregate.stats.submit('{0}', {1})", statistic, value));
			}
		}

		public static void PurchaseItem(string item)
		{
			if (Kongregate.IsGuest)
			{
				Kongregate.ShowSignIn();
			}
			else
			{
				Kongregate.IsWaitingForPurchase = true;
				App.State.PremiumBoni.ItemToPurchase = item;
				Log.Info("[Kongregate] Attempting purchase of " + item);
				Kongregate.CallAPIFunction(string.Format("purchaseItem('{0}')", item));
			}
		}

		public static void GetUserItems2()
		{
			Kongregate.CallAPIFunction("getUserItems2()");
		}

		private void SetAllUserItems(object returnValue)
		{
			Kongregate.m_Items = returnValue.ToString();
			string[] array = (!string.IsNullOrEmpty(Kongregate.m_Items)) ? Kongregate.m_Items.Split(new char[]
			{
				','
			}) : new string[0];
			if (array.Length == 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder("You got your bought items back:\n");
			int num = 1;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string itemId = array2[i];
				stringBuilder.Append(num + ". " + App.State.PremiumBoni.GetItemNameAndAddItem(itemId, Purchase.None)).Append("\n");
				num++;
			}
			string value = stringBuilder.ToString();
			if (!string.IsNullOrEmpty(value))
			{
				GuiBase.ShowBigMessage(stringBuilder.ToString());
			}
		}

		public static void InitAdEventListener()
		{
			Kongregate.CallAPIFunction("initAdEventListener()");
		}

		public static void ShowIncentivizedAd()
		{
			Kongregate.CallAPIFunction("showIncentivizedAd()");
		}

		private void AdsInitialized(object returnValue)
		{
		}

		private void SetAdsAvailable(object returnValue)
		{
			if (returnValue == null)
			{
				return;
			}
			string text = returnValue.ToString().ToLower().Trim();
			Log.Info("adsavailable: " + text);
			App.CanShowAds = "true".Equals(text);
		}

		private void SetAdOpened(object returnValue)
		{
			App.AdOpened = true;
		}

		private void SetAdCompleted(object returnValue)
		{
			App.AdOpened = false;
			State2 expr_10 = App.State.Ext;
			expr_10.AdPoints = ++expr_10.AdPoints;
			App.State.Ext.AdsWatched++;
			App.State.Ext.TotalAdsWatched++;
			GuiBase.ShowToast("Thank you, you received 1 Ad point!");
		}

		private void SetAdAbandoned(object returnValue)
		{
			App.AdOpened = false;
			GuiBase.ShowToast("The Ad was canceled, so sorry no reward.");
		}

		private static void CallAPIFunction(string functionCall)
		{
			Kongregate.CallAPIFunction(functionCall, null);
		}

		private void OnPurchaseResult(object returnValue)
		{
			Kongregate.CheckBoughtItems();
		}

		private static void CallAPIFunction(string functionCall, string callback)
		{
			if (Kongregate.APILoaded)
			{
				Application.ExternalEval(functionCall);
			}
		}

		public static void LogMessage(string message)
		{
			Log.Info(message);
		}

		private void OnKongregateAPILoaded(string userInfo)
		{
			Log.Info("[Kongregate] API Loaded");
			Kongregate.APILoaded = true;
			Kongregate.IsWaitingForPurchase = false;
			string[] array = userInfo.Split(new char[]
			{
				'|'
			});
			int num = int.Parse(array[0]);
			string username = array[1];
			string gameAuthToken = array[2];
			if (num == 0)
			{
				Kongregate.SetIsGuest(true);
			}
			else
			{
				Kongregate.SetUserId(array[0]);
				Kongregate.SetUsername(username);
				Kongregate.SetIsGuest(false);
				Kongregate.m_GameAuthToken = gameAuthToken;
			}
			App.ConnectToKongregate();
		}

		private void OnLogin(string userInfo)
		{
			string[] array = userInfo.Split(new char[]
			{
				'|'
			});
			Kongregate.SetUserId(array[0]);
			Kongregate.SetUsername(array[1]);
			Kongregate.SetIsGuest(false);
			Kongregate.m_GameAuthToken = array[2];
			if (App.State == null)
			{
				App.Init();
			}
			App.ConnectToKongregate();
		}
	}
}
