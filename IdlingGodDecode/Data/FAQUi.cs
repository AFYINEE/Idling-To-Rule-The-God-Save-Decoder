using Assets.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class FAQUi : GuiBase
	{
		public static FAQUi Instance = new FAQUi();

		private Vector2 scrollPosition = Vector2.zero;

		public static void Show()
		{
			FAQUi.Instance.show();
		}

		public void show()
		{
			GUIStyle style = GUI.skin.GetStyle("Label");
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = GuiBase.FontSize(16);
			GUI.BeginGroup(new Rect(GuiBase.Width(290f), GuiBase.Height(110f), GuiBase.Width(660f), GuiBase.Height(480f)));
			GUI.Box(new Rect(GuiBase.Width(0f), GuiBase.Height(0f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(15f), GuiBase.Width(400f), GuiBase.Height(30f)), new GUIContent("For more in depth guides, please visit the forum"));
			if (GUI.Button(new Rect(GuiBase.Width(420f), GuiBase.Height(15f), GuiBase.Width(80f), GuiBase.Height(30f)), new GUIContent("Forum", "In the Sticky: Game Guides there are various guides for the game.")))
			{
				App.OpenWebsite("http://www.kongregate.com/forums/2874-idling-to-rule-the-gods/topics/609942-game-guides");
			}
			if (GUI.Button(new Rect(GuiBase.Width(520f), GuiBase.Height(15f), GuiBase.Width(80f), GuiBase.Height(30f)), new GUIContent("Wiki", "A wiki with info and tips about this game.")))
			{
				App.OpenWebsite("http://itrtg.wikia.com/wiki/Idling_to_Rule_the_Gods_Wiki");
			}
			TextAsset textAsset = Resources.Load("faq") as TextAsset;
			string text = textAsset.text;
			GUIStyle gUIStyle = new GUIStyle();
			gUIStyle.richText = true;
			int num = (int)style.CalcHeight(new GUIContent(text), GuiBase.Width(590f));
			this.scrollPosition = GuiBase.TouchScrollView(new Rect(GuiBase.Width(0f), GuiBase.Height(55f), GuiBase.Width(640f), GuiBase.Height(400f)), this.scrollPosition, new Rect(0f, GuiBase.Height(15f), GuiBase.Width(620f), (float)num + GuiBase.Height(60f)));
			style.fontStyle = FontStyle.Normal;
			style.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(GuiBase.Width(20f), GuiBase.Height(30f), GuiBase.Width(590f), (float)num + GuiBase.Height(60f)), text);
			GUI.EndScrollView();
			GUI.EndGroup();
		}
	}
}
