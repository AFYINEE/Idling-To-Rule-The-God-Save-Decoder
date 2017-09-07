using Assets.Scripts.Data;
using Assets.Scripts.Gui;
using Assets.Scripts.Helper;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Save
{
	public class Storage
	{
		public const string AutoSave = "AutoSave";

		public const string KredSave = "KredSave";

		public const string ManualSave = "ManualSave.txt";

		public static string GetMD5Hash(string stringValue)
		{
			MD5 mD = MD5.Create();
			byte[] array = mD.ComputeHash(Encoding.UTF8.GetBytes(stringValue));
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		public static string SaveGameState(GameState state, string filename)
		{
			if (string.IsNullOrEmpty(filename))
			{
				filename = "AutoSave";
			}
			string text = Storage.ToCompressedString(state);
			try
			{
				if (App.CurrentPlattform == Plattform.Steam)
				{
					if (!filename.EndsWith(".txt"))
					{
						filename += ".txt";
					}
					string text2 = Application.dataPath + "\\Saves\\";
					if (!"ManualSave.txt".Equals(filename))
					{
						text2 = Application.persistentDataPath + "\\ItRtG_Steam_\\";
					}
					if (!Directory.Exists(text2))
					{
						Directory.CreateDirectory(text2);
					}
					Log.Info("Game saved to: " + text2 + filename);
					File.WriteAllText(text2 + filename, text);
				}
				else
				{
					PlayerPrefs.SetString(filename, text);
					PlayerPrefs.Save();
				}
			}
			catch (IOException ex)
			{
				GuiBase.ShowBigMessage("Failed so save the game!\n" + ex.StackTrace);
			}
			return text;
		}

		public static void SaveText(string text, string filename)
		{
			string text2 = Application.dataPath + "\\Saves\\";
			if (!Directory.Exists(text2))
			{
				Directory.CreateDirectory(text2);
			}
			File.WriteAllText(text2 + filename, text);
		}

		public static string ToCompressedString(GameState state)
		{
			if (state == null)
			{
				return string.Empty;
			}
			string text = state.Serialize();
			string str = Vertifier.GenerateVertifierFromGameState(state);
			text = text + "-77-" + str;
			return Storage.CompressString(text);
		}

		public static GameState FromCompressedString(string gamestateString)
		{
			GameState result;
			try
			{
				if (string.IsNullOrEmpty(gamestateString))
				{
					result = new GameState(true, 0);
				}
				else
				{
					string text = Storage.DecompressString(gamestateString);
					string[] array = text.Split(new string[]
					{
						"-77-"
					}, StringSplitOptions.None);
					if (array.Length == 2)
					{
						string base64String = array[0];
						GameState gameState = GameState.FromString(base64String);
						result = gameState;
					}
					else
					{
						Log.Error("Something went wrong" + array.Length + "new gamestate is returned");
						result = new GameState(true, 0);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex.StackTrace + "\nSomething went wrong new gamestate is returned");
				result = new GameState(true, 0);
			}
			return result;
		}

		public static GameState LoadGameState(string filename)
		{
			if (App.CurrentPlattform == Plattform.Steam)
			{
				if (string.IsNullOrEmpty(filename))
				{
					return null;
				}
				string str = Application.dataPath + "\\Saves\\";
				if (!"ManualSave.txt".Equals(filename))
				{
					str = Application.persistentDataPath + "\\ItRtG_Steam_\\";
				}
				if (!filename.Contains(".txt"))
				{
					filename += ".txt";
				}
				string text = str + filename;
				GameState gameState = Storage.FromCompressedString(Storage.LoadFile(text));
				if (gameState == null || (gameState.Clones.Count == 0 && gameState.Statistic.HighestGodDefeated == 0))
				{
					text = Application.persistentDataPath + "\\ItRtG_Steam_\\" + filename;
					gameState = Storage.FromCompressedString(Storage.LoadFile(text));
				}
				if (gameState == null || (gameState.Clones.Count == 0 && gameState.Statistic.HighestGodDefeated == 0))
				{
					text = Application.dataPath + "\\Saves\\" + filename;
					gameState = Storage.FromCompressedString(Storage.LoadFile(text));
				}
				Log.Info("Game loaded from: " + text);
				if (App.State != null && !App.State.SteamId.Equals(gameState.SteamId) && App.State.KongUserIdLong == 0L)
				{
					Log.Error("This save is not a valid save!");
					GuiBase.ShowToast("This save is not a valid save!");
					Storage.SaveGameState(gameState, "SaveWrongSteamId");
					return null;
				}
				if (string.IsNullOrEmpty(gameState.SteamId) && !filename.Equals("AutoSave.txt"))
				{
					Log.Error("This save is not a valid save!");
					GuiBase.ShowToast("This save is not a valid save!");
					Storage.SaveGameState(gameState, "SaveInvalidSteamId");
					return null;
				}
				return gameState;
			}
			else
			{
				if (PlayerPrefs.HasKey(filename))
				{
					return Storage.FromCompressedString(PlayerPrefs.GetString(filename));
				}
				return null;
			}
		}

		private static string LoadFile(string fileName)
		{
			string result;
			try
			{
				string text = string.Empty;
				StreamReader streamReader = new StreamReader(fileName, Encoding.Default);
				using (streamReader)
				{
					string text2;
					do
					{
						text2 = streamReader.ReadLine();
						text += text2;
						if (text2 != null)
						{
						}
					}
					while (text2 != null);
					streamReader.Close();
					result = text;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("{0}\n", ex.Message);
				result = string.Empty;
			}
			return result;
		}

		public static string CompressString(string text)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			byte[] inArray = CLZF2.Compress(bytes);
			return Convert.ToBase64String(inArray);
		}

		public static string DecompressString(string compressedText)
		{
			byte[] inputBytes = Convert.FromBase64String(compressedText);
			byte[] bytes = CLZF2.Decompress(inputBytes);
			return Encoding.UTF8.GetString(bytes);
		}
	}
}
