using Assets.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	internal class NavigationMenu : GuiBase
	{
		private static NavigationMenu Instance = new NavigationMenu();

		public static void Show()
		{
			NavigationMenu.Instance.show();
		}

		private void show()
		{
			GUIStyle style = GUI.skin.GetStyle("Label");
			GUI.BeginGroup(new Rect(GuiBase.Width(280f), GuiBase.Height(110f), GuiBase.Width(670f), GuiBase.Height(480f)));
			GUI.Box(new Rect(GuiBase.Width(10f), GuiBase.Height(0f), GuiBase.Width(660f), GuiBase.Height(480f)), string.Empty);
			style.fontSize = GuiBase.FontSize(18);
			style.fontStyle = FontStyle.Bold;
			style.alignment = TextAnchor.UpperCenter;
			GUI.Label(new Rect(GuiBase.Width(30f), GuiBase.Height(20f), GuiBase.Width(660f), GuiBase.Height(40f)), "Keyboard Shortcuts");
			style.fontSize = GuiBase.FontSize(16);
			style.fontStyle = FontStyle.Normal;
			style.alignment = TextAnchor.UpperLeft;
			int num = 50;
			this.AddInfoLabel("F1 : Show Shortcuts", ref num, 30);
			this.AddInfoLabel("F2 : Show FAQ", ref num, 30);
			this.AddInfoLabel("F3 : Open the Chat", ref num, 30);
			this.AddInfoLabel("1 : Show Create", ref num, 30);
			this.AddInfoLabel("2 : Show Monuments", ref num, 30);
			this.AddInfoLabel("3 : Show Divinity", ref num, 30);
			this.AddInfoLabel("4 : Show Planet", ref num, 30);
			this.AddInfoLabel("5 : Show Physical", ref num, 30);
			this.AddInfoLabel("6 : Show Skills", ref num, 30);
			this.AddInfoLabel("7 : Show Might", ref num, 30);
			this.AddInfoLabel("8 : Show TBS", ref num, 30);
			this.AddInfoLabel("9 : Show Monster", ref num, 30);
			this.AddInfoLabel("0 : Show Gods", ref num, 30);
			num = 50;
			this.AddInfoLabel("Shift + 1 : Show Special", ref num, 300);
			this.AddInfoLabel("Shift + 2 : Show Statistics", ref num, 300);
			this.AddInfoLabel("Shift + 3 : Show Settings", ref num, 300);
			this.AddInfoLabel("Shift + 4 : Show Story", ref num, 300);
			this.AddInfoLabel("Shift + 5 : Show Pets", ref num, 300);
			if (App.CurrentPlattform == Plattform.Steam)
			{
				this.AddInfoLabel("Esc: Change resolution or quit game", ref num, 300);
			}
			this.AddInfoLabel("Some well-known code: ???", ref num, 300);
			GUI.Label(new Rect(GuiBase.Width(300f), GuiBase.Height((float)num), GuiBase.Width(360f), GuiBase.Height(70f)), "Shift + r : Remove all shadow clones from Monument, Might, Divinity and Planet");
			num += 50;
			GUI.Label(new Rect(GuiBase.Width(300f), GuiBase.Height((float)num), GuiBase.Width(360f), GuiBase.Height(70f)), "Shift + a : Set Clones to add / remove to 'Max'");
			num += 30;
			GUI.Label(new Rect(GuiBase.Width(300f), GuiBase.Height((float)num), GuiBase.Width(360f), GuiBase.Height(70f)), "Right click a number button: reduce the current number by that count.");
			num += 50;
			GUI.Label(new Rect(GuiBase.Width(300f), GuiBase.Height((float)num), GuiBase.Width(360f), GuiBase.Height(70f)), "Right click + at physical / skills: add 28 clones");
			num += 50;
			GUI.EndGroup();
		}

		private void AddInfoLabel(string info, ref int marginTop, int marginLeft = 30)
		{
			GUI.Label(new Rect(GuiBase.Width((float)marginLeft), GuiBase.Height((float)marginTop), GuiBase.Width(300f), GuiBase.Height(30f)), info);
			marginTop += 25;
		}
	}
}
