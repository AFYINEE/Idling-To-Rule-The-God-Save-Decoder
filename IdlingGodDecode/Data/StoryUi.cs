using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class StoryUi : GuiBase
	{
		private static StoryUi Instance = new StoryUi();

		private Vector2 scrollPosition = Vector2.zero;

		private string shownTitle = string.Empty;

		private string shownStory = string.Empty;

		private int shownIndex = -1;

		private static string infoText = string.Empty;

		private const string Intro = "Intro";

		private const string Chapter1 = "Chapter 1: Hyperion beaten";

		private const string Chapter2 = "Chapter 2: My first Rebirth";

		private const string Chapter3 = "Chapter 3: Itztli";

		private const string Chapter4 = "Chapter 4: Bunnies, Goblins and Gaia";

		private const string Chapter5 = "Chapter 5 Part 1: The biggest mountain";

		private const string Chapter5_1 = "Chapter 5 Part 2: The fight vs Shu";

		private const string Chapter6 = "Chapter 6: Suijin in the desert";

		private const string Chapter7 = "Chapter 7 Part 1: The big burgers";

		private const string Chapter7_1 = "Chapter 7 Part 2: The fight vs Gefion";

		private const string Chapter8 = "Chapter 8 Part 1: Finding Hathor";

		private const string Chapter8_1 = "Chapter 8 Part 2: Fighting Hathor";

		private const string Chapter9 = "Chapter 9 Part 1: Zombies in the Tomb";

		private const string Chapter9_1 = "Chapter 9 Part 2: Pontus and ???";

		private const string Chapter10 = "Chapter 10 Part 1: Time to start anew";

		private const string Chapter10_2 = "Chapter 10 Part 2: Diana and her Tomb";

		private const string Chapter11 = "Chapter 11 Part 1: The first monuments";

		private const string Chapter11_2 = "Chapter 11 Part 2: Orcs, humans and Izanagi";

		private const string Chapter12 = "Chapter 12 Part 1: Pyramids and bandages";

		private const string Chapter12_2 = "Chapter 12 Part 2: Nephthys the river goddess";

		private const string Chapter13 = "Chapter 13 Part 1: Fighting for a pizza";

		private const string Chapter13_2 = "Chapter 13 Part 2: The mountain goddess";

		private const string Chapter14 = "Chapter 14 Part 1: The glowing apes";

		private const string Chapter14_2 = "Chapter 14 Part 2: The revenge";

		private static List<KeyValuePair<string, string>> UnlockedStoryParts = new List<KeyValuePair<string, string>>();

		private static List<KeyValuePair<string, string>> AllStoryParts = new List<KeyValuePair<string, string>>
		{
			new KeyValuePair<string, string>("Intro", "00_Intro"),
			new KeyValuePair<string, string>("Chapter 1: Hyperion beaten", "01_Hyperion"),
			new KeyValuePair<string, string>("Chapter 2: My first Rebirth", "02_Rebirth"),
			new KeyValuePair<string, string>("Chapter 3: Itztli", "03_Itztli"),
			new KeyValuePair<string, string>("Chapter 4: Bunnies, Goblins and Gaia", "04_Gaia"),
			new KeyValuePair<string, string>("Chapter 5 Part 1: The biggest mountain", "05_Shu_part1"),
			new KeyValuePair<string, string>("Chapter 5 Part 2: The fight vs Shu", "05_Shu_part2"),
			new KeyValuePair<string, string>("Chapter 6: Suijin in the desert", "06_Suijin"),
			new KeyValuePair<string, string>("Chapter 7 Part 1: The big burgers", "07_Gefion_part1"),
			new KeyValuePair<string, string>("Chapter 7 Part 2: The fight vs Gefion", "07_Gefion_part2"),
			new KeyValuePair<string, string>("Chapter 8 Part 1: Finding Hathor", "08_Hathor_part1"),
			new KeyValuePair<string, string>("Chapter 8 Part 2: Fighting Hathor", "08_Hathor_part2"),
			new KeyValuePair<string, string>("Chapter 9 Part 1: Zombies in the Tomb", "09_Pontus_part1"),
			new KeyValuePair<string, string>("Chapter 9 Part 2: Pontus and ???", "09_Pontus_part2"),
			new KeyValuePair<string, string>("Chapter 10 Part 1: Time to start anew", "10_Diana_part1"),
			new KeyValuePair<string, string>("Chapter 10 Part 2: Diana and her Tomb", "10_Diana_part2"),
			new KeyValuePair<string, string>("Chapter 11 Part 1: The first monuments", "11_Izanagi_part1"),
			new KeyValuePair<string, string>("Chapter 11 Part 2: Orcs, humans and Izanagi", "11_Izanagi_part2"),
			new KeyValuePair<string, string>("Chapter 12 Part 1: Pyramids and bandages", "12_Nephthys_part1"),
			new KeyValuePair<string, string>("Chapter 12 Part 2: Nephthys the river goddess", "12_Nephthys_part2"),
			new KeyValuePair<string, string>("Chapter 13 Part 1: Fighting for a pizza", "13_Cybele_part1"),
			new KeyValuePair<string, string>("Chapter 13 Part 2: The mountain goddess", "13_Cybele_part2"),
			new KeyValuePair<string, string>("Chapter 14 Part 1: The glowing apes", "14_Artemis_part1"),
			new KeyValuePair<string, string>("Chapter 14 Part 2: The revenge", "14_Artemis_part2")
		};

		public static void SetUnlockedStoryParts(GameState state)
		{
			StoryUi.UnlockedStoryParts = new List<KeyValuePair<string, string>>();
			StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Intro")));
			if (state.Statistic.HighestGodDefeated > 0)
			{
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 1: Hyperion beaten")));
			}
			if (state.Statistic.TotalRebirths > 0)
			{
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 2: My first Rebirth")));
			}
			if (state.Statistic.HighestGodDefeated > 1)
			{
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 3: Itztli")));
			}
			if (state.Statistic.HighestGodDefeated > 2)
			{
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 4: Bunnies, Goblins and Gaia")));
			}
			if (state.Statistic.HighestGodDefeated > 3)
			{
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 5 Part 1: The biggest mountain")));
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 5 Part 2: The fight vs Shu")));
			}
			if (state.Statistic.HighestGodDefeated > 4)
			{
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 6: Suijin in the desert")));
			}
			if (state.Statistic.HighestGodDefeated > 5)
			{
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 7 Part 1: The big burgers")));
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 7 Part 2: The fight vs Gefion")));
			}
			if (state.Statistic.HighestGodDefeated > 6)
			{
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 8 Part 1: Finding Hathor")));
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 8 Part 2: Fighting Hathor")));
			}
			if (state.Statistic.HighestGodDefeated > 7)
			{
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 9 Part 1: Zombies in the Tomb")));
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 9 Part 2: Pontus and ???")));
			}
			if (state.Statistic.HighestGodDefeated > 8)
			{
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 10 Part 1: Time to start anew")));
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 10 Part 2: Diana and her Tomb")));
			}
			if (state.Statistic.HighestGodDefeated > 9)
			{
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 11 Part 1: The first monuments")));
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 11 Part 2: Orcs, humans and Izanagi")));
			}
			if (state.Statistic.HighestGodDefeated > 10)
			{
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 12 Part 1: Pyramids and bandages")));
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 12 Part 2: Nephthys the river goddess")));
			}
			if (state.Statistic.HighestGodDefeated > 11)
			{
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 13 Part 1: Fighting for a pizza")));
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 13 Part 2: The mountain goddess")));
			}
			if (state.Statistic.HighestGodDefeated > 12)
			{
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 14 Part 1: The glowing apes")));
				StoryUi.UnlockedStoryParts.Add(StoryUi.AllStoryParts.FirstOrDefault((KeyValuePair<string, string> x) => x.Key.Equals("Chapter 14 Part 2: The revenge")));
			}
			if (StoryUi.AllStoryParts.Count == StoryUi.UnlockedStoryParts.Count)
			{
				StoryUi.infoText = "To be continued!";
			}
		}

		public static void Show()
		{
			StoryUi.Instance.show();
		}

		private void show()
		{
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = GuiBase.FontSize(16);
			GUIStyle style2 = GUI.skin.GetStyle("Button");
			style2.fontSize = GuiBase.FontSize(14);
			GUI.BeginGroup(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)));
			GUI.Box(new Rect(GuiBase.Width(0f), GuiBase.Height(0f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			int num = (int)style.CalcHeight(new GUIContent(this.shownStory), GuiBase.Width(590f));
			if (!string.IsNullOrEmpty(this.shownStory))
			{
				this.scrollPosition = GuiBase.TouchScrollView(new Rect(GuiBase.Width(0f), GuiBase.Height(20f), GuiBase.Width(640f), GuiBase.Height(400f)), this.scrollPosition, new Rect(0f, GuiBase.Height(15f), GuiBase.Width(620f), (float)num + GuiBase.Height(60f)));
				style.fontStyle = FontStyle.Bold;
				style.alignment = TextAnchor.UpperCenter;
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(10f), GuiBase.Width(590f), GuiBase.Height(30f)), this.shownTitle);
				style.fontStyle = FontStyle.Normal;
				style.alignment = TextAnchor.UpperLeft;
				GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(50f), GuiBase.Width(590f), (float)num + GuiBase.Height(60f)), this.shownStory);
				GUI.EndScrollView();
				if (GUI.Button(new Rect(GuiBase.Width(320f), GuiBase.Height(440f), GuiBase.Width(100f), GuiBase.Height(30f)), "Back"))
				{
					this.shownTitle = string.Empty;
					this.shownStory = string.Empty;
					this.shownIndex = -1;
				}
				if (this.shownIndex > 0 && GUI.Button(new Rect(GuiBase.Width(430f), GuiBase.Height(440f), GuiBase.Width(100f), GuiBase.Height(30f)), "Previous"))
				{
					this.shownIndex--;
					this.scrollPosition = Vector2.zero;
					this.shownTitle = StoryUi.UnlockedStoryParts[this.shownIndex].Key;
					TextAsset textAsset = Resources.Load("Story/" + StoryUi.UnlockedStoryParts[this.shownIndex].Value) as TextAsset;
					this.shownStory = textAsset.text;
				}
				if (StoryUi.UnlockedStoryParts.Count > this.shownIndex + 1 && GUI.Button(new Rect(GuiBase.Width(540f), GuiBase.Height(440f), GuiBase.Width(100f), GuiBase.Height(30f)), "Next"))
				{
					this.shownIndex++;
					this.scrollPosition = Vector2.zero;
					this.shownTitle = StoryUi.UnlockedStoryParts[this.shownIndex].Key;
					TextAsset textAsset2 = Resources.Load("Story/" + StoryUi.UnlockedStoryParts[this.shownIndex].Value) as TextAsset;
					this.shownStory = textAsset2.text;
				}
			}
			else
			{
				int num2 = 20;
				for (int i = 0; i < StoryUi.UnlockedStoryParts.Count; i++)
				{
					KeyValuePair<string, string> part = StoryUi.UnlockedStoryParts[i];
					if (i % 2 == 0)
					{
						this.AddStoryPartButton(part, num2, 20, i);
					}
					else
					{
						num2 = this.AddStoryPartButton(part, num2, 320, i);
					}
				}
				if (!string.IsNullOrEmpty(StoryUi.infoText))
				{
					GUI.Label(new Rect(GuiBase.Width(40f), GuiBase.Height((float)num2), GuiBase.Width(590f), GuiBase.Height(30f)), StoryUi.infoText);
				}
			}
			GUI.EndGroup();
			style2.fontSize = GuiBase.FontSize(16);
		}

		private int AddStoryPartButton(KeyValuePair<string, string> part, int marginTop, int marginLeft, int index)
		{
			if (GUI.Button(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(280f), GuiBase.Height(30f)), part.Key.Replace("Chapter ", string.Empty).Replace(" Part ", "-")))
			{
				this.shownIndex = index;
				this.scrollPosition = Vector2.zero;
				this.shownTitle = part.Key;
				TextAsset textAsset = Resources.Load("Story/" + part.Value) as TextAsset;
				this.shownStory = textAsset.text;
			}
			return marginTop + 35;
		}
	}
}
