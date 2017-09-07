using Assets.Scripts.Data;
using Assets.Scripts.Gui;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using yIRC;

namespace Assets.Scripts.Helper
{
	public class ChatRoom : MonoBehaviour, IrcClient.IListConsumer<ChannelInfo>
	{
		private class TextAndColor
		{
			public string Text = string.Empty;

			public Color TextColor = Color.black;

			public TextAndColor(Color color, string text)
			{
				this.Text = text;
				this.TextColor = color;
			}
		}

		public string serverName = "irc.rizon.net";

		public string userName = string.Empty;

		private string userId = string.Empty;

		public string channel = "#ItRtG";

		public int port = 6667;

		public static IrcClient irc = null;

		public GUISkin skin;

		public static bool IsShowing = false;

		private static bool IsNotStarted = true;

		private static bool IsChannelChoosen = false;

		private static bool focusInput = false;

		private bool isIdentified;

		private int[] unreadTextChannels = new int[0];

		private int activeChannel;

		private string inputString = string.Empty;

		private static string Topic = string.Empty;

		public static bool NewMessageAvailable = false;

		private bool isDev;

		private Texture2D whiteTexture;

		private Texture2D chatTexture;

		private Color chatColor = new Color(0.9f, 0.9f, 0.9f, 1f);

		private bool firstFocusAfterConnect;

		private Dictionary<string, string> channelsText = new Dictionary<string, string>();

		private Dictionary<string, string> closedChannelsText = new Dictionary<string, string>();

		private Dictionary<string, Vector2> channelsScrolls = new Dictionary<string, Vector2>();

		public static bool UpdateText = false;

		private string nextFrameChannel = string.Empty;

		private Vector2 usersScroll = Vector2.zero;

		private int marginTopUsers;

		private string nameOfChatPartner = string.Empty;

		private List<ChannelUser> sortedUsers = new List<ChannelUser>();

		private GUIStyle buttonStyle;

		private GUIStyle chatStyle;

		private GUIStyle labelStyle;

		private GUIStyle fieldStyle;

		private static List<string> mutedUsers = new List<string>();

		private static List<ChatRoom.TextAndColor> chatText = new List<ChatRoom.TextAndColor>();

		private static string chatTextString = string.Empty;

		private static int chatTextLines = 0;

		private int selectedChannel;

		private string DateTimeString
		{
			get
			{
				string text = "[";
				if (DateTime.Now.Hour < 10)
				{
					text = text + "0" + DateTime.Now.Hour;
				}
				else
				{
					text += DateTime.Now.Hour.ToString();
				}
				text += ":";
				if (DateTime.Now.Minute < 10)
				{
					text = text + "0" + DateTime.Now.Minute;
				}
				else
				{
					text += DateTime.Now.Minute.ToString();
				}
				return text + "] ";
			}
		}

		public static float Height(float px)
		{
			return px * App.HeightMulti;
		}

		public static float Width(float px)
		{
			return px * App.WidthMulti;
		}

		public static int FontSize(int px)
		{
			return (int)((float)px * App.HeightMulti);
		}

		public void Start()
		{
			if (App.CurrentPlattform == Plattform.Android)
			{
				return;
			}
			this.channelsText["SERVER"] = string.Empty;
			this.chatTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
			this.chatTexture.SetPixel(0, 0, this.chatColor);
			this.chatTexture.Apply();
			this.whiteTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
			this.whiteTexture.SetPixel(0, 0, Color.white);
			this.whiteTexture.Apply();
		}

		public static void Show()
		{
			ChatRoom.UpdateText = true;
			ChatRoom.IsShowing = true;
			ChatRoom.focusInput = true;
		}

		private void ChooseChannel()
		{
			GUI.Box(new Rect(10f, 10f, (float)(Screen.width - 20), (float)(Screen.height - 20)), string.Empty);
			GUI.Label(new Rect(ChatRoom.Width(230f), ChatRoom.Height(130f), ChatRoom.Width(560f), ChatRoom.Height(170f)), "This chat is based to IRC and connected to Rizon IRC.\nPlease select the channel where you want to connect to.\nThe default channel for this game is #ItRtG.\nIf the channel is too full, you can use #ItRtG2, #ItRtG3... or something different. If you want to change the channel, click 'Quit Chat' and then choose a different channel.");
			this.channel = GUI.TextField(new Rect(ChatRoom.Width(230f), ChatRoom.Height(270f), ChatRoom.Width(100f), ChatRoom.Height(30f)), this.channel);
			if (!this.channel.StartsWith("#"))
			{
				this.channel = "#" + this.channel;
			}
			if (GUI.Button(new Rect(ChatRoom.Width(340f), ChatRoom.Height(270f), ChatRoom.Width(130f), ChatRoom.Height(30f)), "Connect"))
			{
				ChatRoom.IsNotStarted = true;
				ChatRoom.IsChannelChoosen = true;
			}
			if (GUI.Button(new Rect(ChatRoom.Width(480f), ChatRoom.Height(270f), ChatRoom.Width(130f), ChatRoom.Height(30f)), "Cancel"))
			{
				ChatRoom.IsShowing = false;
			}
		}

		private void OnGUI()
		{
			if (!ChatRoom.IsShowing)
			{
				return;
			}
			ChatRoom.NewMessageAvailable = false;
			if (string.IsNullOrEmpty(this.userName))
			{
				this.userName = App.State.AvatarName;
			}
			if ("76561198288630083".Equals(App.State.SteamId) || "2365000".Equals(App.State.KongUserId))
			{
				this.isDev = true;
			}
			base.GetComponent<Camera>().backgroundColor = SettingsUi.Instance.BGColor;
			this.skin = Gui.Gui.ChosenSkin;
			GUI.skin = this.skin;
			if (ChatRoom.IsNotStarted)
			{
				this.ChooseChannel();
				if (ChatRoom.IsChannelChoosen)
				{
					ChatRoom.IsNotStarted = false;
					this.Connect();
					this.buttonStyle = GUI.skin.GetStyle("Button");
					this.chatStyle = GUI.skin.GetStyle("TextArea");
					this.labelStyle = GUI.skin.GetStyle("Label");
					this.fieldStyle = GUI.skin.GetStyle("TextField");
				}
			}
			if (ChatRoom.irc != null && ChatRoom.irc.IsConnected)
			{
				if (this.isDev && !this.isIdentified)
				{
					ChatRoom.irc.SendMessage(SendType.Message, "NickServ", "IDENTIFY k3934Dl456hj");
					this.isIdentified = true;
				}
				this.DoChatWindow();
				if (this.firstFocusAfterConnect)
				{
					ChatRoom.focusInput = true;
					this.firstFocusAfterConnect = false;
				}
			}
			else if (ChatRoom.irc != null && !ChatRoom.irc.IsConnected)
			{
				this.ChooseChannel();
				GUI.Label(new Rect(ChatRoom.Width(230f), ChatRoom.Height(100f), ChatRoom.Width(560f), ChatRoom.Height(30f)), "Failed to connect, please try again.");
			}
			if (Input.GetKeyDown(KeyCode.F3))
			{
				ChatRoom.IsShowing = false;
			}
			TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
			textEditor.Copy();
		}

		public void Add(ChannelInfo entry)
		{
			this.Log(string.Format("Channel entry: {0}", entry.Channel));
		}

		public void Clear()
		{
		}

		public void Finished()
		{
		}

		public void Connect()
		{
			if (ChatRoom.irc != null && ChatRoom.irc.IsConnected)
			{
				ChatRoom.irc.Disconnect();
			}
			ChatRoom.irc = new IrcClient();
			ChatRoom.irc.Encoding = Encoding.UTF8;
			ChatRoom.irc.SendDelay = 200;
			ChatRoom.irc.ActiveChannelSyncing = true;
			ChatRoom.irc.OnError += new ErrorEventHandler(this.irc_OnError);
			ChatRoom.irc.OnRawMessage += new IrcEventHandler(this.irc_OnRawMessage);
			ChatRoom.irc.OnQueryMessage += new IrcEventHandler(this.irc_OnQueryMessage);
			ChatRoom.irc.RegisterChannelListConsumer(this);
			try
			{
				ChatRoom.irc.Connect(this.serverName, this.port);
			}
			catch (ConnectionException ex)
			{
				this.Log("Couldn't connect! Reason: " + ex.Message);
			}
			try
			{
				this.userId = App.State.AvatarName + UnityEngine.Random.Range(0, 10000);
				if (string.IsNullOrEmpty(this.userName))
				{
					this.userName = "Player1";
				}
				if (string.IsNullOrEmpty(this.userId))
				{
					this.userId = "Player" + UnityEngine.Random.Range(0, 1000);
				}
				if (this.isDev)
				{
					this.userName = "Ryu82";
					ChatRoom.irc.Login(this.userName, this.userId, 0, this.userName);
				}
				else
				{
					if ("Ryu82".Equals(this.userName))
					{
						this.userName = "RyuClone";
					}
					ChatRoom.irc.Login(this.userName, this.userId, 0, this.userName);
				}
				ChatRoom.irc.Join(this.channel);
				ChatRoom.irc.Topic("#ItRtG", Priority.High);
				this.firstFocusAfterConnect = true;
			}
			catch (ConnectionException ex2)
			{
				this.Log(ex2.Message);
			}
			catch (Exception ex3)
			{
				this.Log("Error occurred! Message: " + ex3.Message);
				this.Log("Exception: " + ex3.StackTrace);
			}
		}

		private void Update()
		{
			if (App.CurrentPlattform == Plattform.Android)
			{
				return;
			}
			if (ChatRoom.irc != null && ChatRoom.irc.IsConnected)
			{
				ChatRoom.irc.ListenOnce(false);
			}
			if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || !Input.GetMouseButton(2))
			{
			}
			if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
			{
				GUI.FocusControl("Input");
			}
		}

		private void Log(string message)
		{
			Dictionary<string, string> dictionary;
			(dictionary = this.channelsText)["SERVER"] = dictionary["SERVER"] + message;
			UnityEngine.Debug.Log(message);
		}

		private void irc_OnRawMessage(object sender, IrcEventArgs e)
		{
			string text = (!string.IsNullOrEmpty(e.Data.Channel) && e.Data.Type == ReceiveType.ChannelMessage) ? e.Data.Channel : "SERVER";
			string text2 = (!string.IsNullOrEmpty(e.Data.Nick)) ? string.Format(this.DateTimeString + "{0}: {1}", e.Data.Nick, e.Data.Message) : ("* " + e.Data.Message);
			Dictionary<string, string> dictionary;
			if (!this.channelsText.ContainsKey(text))
			{
				this.channelsText[text] = string.Empty;
			}
			else
			{
				string key;
				(dictionary = this.channelsText)[key = text] = dictionary[key] + "\r\n";
			}
			string key2;
			(dictionary = this.channelsText)[key2 = text] = dictionary[key2] + text2;
			if (this.channelsScrolls.ContainsKey(text))
			{
				this.channelsScrolls[text] = new Vector2(0f, 3.40282347E+38f);
			}
			if (!"SERVER".Equals(text))
			{
				if (!ChatRoom.IsShowing)
				{
					ChatRoom.NewMessageAvailable = true;
				}
				else
				{
					ChatRoom.UpdateText = true;
				}
			}
			List<string> list = new List<string>();
			list.AddRange(ChatRoom.irc.GetChannels());
			for (int i = 0; i < list.Count; i++)
			{
				if (!string.IsNullOrEmpty(e.Data.Nick) && e.Data.Nick.Equals(list[i]) && this.unreadTextChannels.Length > i)
				{
					this.unreadTextChannels[i] = 1;
				}
			}
			if (e.Data.Type == ReceiveType.Topic)
			{
				ChatRoom.Topic = text2;
				if (!string.IsNullOrEmpty(ChatRoom.Topic))
				{
					ChatRoom.Topic = "Topic: " + ChatRoom.Topic.Substring(1);
				}
			}
		}

		public void irc_OnQueryMessage(object sender, IrcEventArgs e)
		{
			Dictionary<string, string> dictionary;
			if (!this.channelsText.ContainsKey(e.Data.Nick))
			{
				this.channelsText[e.Data.Nick] = string.Empty;
			}
			else if (this.channelsText[e.Data.Nick].Length > 2)
			{
				string nick;
				(dictionary = this.channelsText)[nick = e.Data.Nick] = dictionary[nick] + "\r\n";
			}
			string str = string.Format(this.DateTimeString + "{0}: {1}", e.Data.Nick, e.Data.Message);
			string nick2;
			(dictionary = this.channelsText)[nick2 = e.Data.Nick] = dictionary[nick2] + str;
			if (this.channelsScrolls.ContainsKey(e.Data.Nick))
			{
				this.channelsScrolls[e.Data.Nick] = new Vector2(0f, 3.40282347E+38f);
			}
			if (!ChatRoom.IsShowing)
			{
				ChatRoom.NewMessageAvailable = true;
			}
			List<string> list = new List<string>();
			list.AddRange(ChatRoom.irc.GetChannels());
			for (int i = 0; i < list.Count; i++)
			{
				if (!string.IsNullOrEmpty(e.Data.Nick) && e.Data.Nick.Equals(list[i]) && this.unreadTextChannels.Length > i)
				{
					this.unreadTextChannels[i] = 1;
				}
			}
			ChatRoom.UpdateText = true;
		}

		private void DoUpdateText()
		{
			if (ChatRoom.irc == null || !ChatRoom.irc.IsConnected)
			{
				return;
			}
			List<string> list = new List<string>();
			list.AddRange(ChatRoom.irc.GetChannels());
			if (list.Count == 0)
			{
				return;
			}
			foreach (KeyValuePair<string, string> current in this.channelsText)
			{
				if (current.Key != "SERVER" && list.IndexOf(current.Key) == -1)
				{
					list.Add(current.Key);
				}
			}
			if (this.nextFrameChannel != string.Empty)
			{
				this.activeChannel = list.IndexOf(this.nextFrameChannel);
				this.nextFrameChannel = string.Empty;
			}
			if (this.unreadTextChannels.Length != list.Count)
			{
				int[] array = this.unreadTextChannels;
				this.unreadTextChannels = new int[list.Count];
				for (int i = 0; i < array.Length; i++)
				{
					if (this.unreadTextChannels.Length > i)
					{
						this.unreadTextChannels[i] = array[i];
					}
				}
			}
			if (this.unreadTextChannels.Length > 0)
			{
				this.unreadTextChannels[this.activeChannel] = 0;
			}
			string text = (!this.channelsText.ContainsKey(list[this.activeChannel])) ? string.Empty : this.channelsText[list[this.activeChannel]];
			if (this.activeChannel == 0)
			{
				text = "Welcome to the chat. You are connected as: " + this.userName + ".\nPlease be nice to others and don't spam.\nYou can send messages with return or clicking the send button. \nYou can also message someone privately if you click on their name.\n\n" + text;
			}
			if (string.IsNullOrEmpty(text))
			{
				ChatRoom.chatTextString = string.Empty;
				ChatRoom.chatText = new List<ChatRoom.TextAndColor>();
				return;
			}
			if (text.Length > 16000)
			{
				text = text.Substring(text.Length - 16000, 16000);
			}
			ChatRoom.chatText = new List<ChatRoom.TextAndColor>();
			string[] array2 = text.Split(new char[]
			{
				'\n'
			});
			ChatRoom.chatTextLines = 0;
			int num = (int)this.chatStyle.CalcHeight(new GUIContent("base"), ChatRoom.Width(704f));
			for (int j = 0; j < array2.Length; j++)
			{
				bool flag = false;
				string[] array3 = array2[j].Split(new char[]
				{
					']'
				});
				if (array3.Length > 1)
				{
					string[] array4 = array3[1].Trim().Split(new char[]
					{
						':'
					});
					string text2 = string.Empty;
					if (array4.Length > 0)
					{
						text2 = array4[0].Trim();
					}
					foreach (string current2 in ChatRoom.mutedUsers)
					{
						if (text2.Equals(current2))
						{
							flag = true;
						}
					}
					if (!flag)
					{
						if (text2.StartsWith(this.userName))
						{
							ChatRoom.chatText.Add(new ChatRoom.TextAndColor(Color.blue, array2[j]));
						}
						else
						{
							ChatRoom.chatText.Add(new ChatRoom.TextAndColor(Color.black, array2[j]));
						}
					}
				}
				else
				{
					ChatRoom.chatText.Add(new ChatRoom.TextAndColor(Color.black, array2[j]));
				}
				if (!flag)
				{
					ChatRoom.chatTextLines++;
				}
				int num2 = (int)this.chatStyle.CalcHeight(new GUIContent(array2[j]), ChatRoom.Width(704f));
				if (num2 > num)
				{
					int num3 = num2 / num;
					ChatRoom.chatTextLines += num3;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ChatRoom.TextAndColor current3 in ChatRoom.chatText)
			{
				stringBuilder.Append(current3.Text + "\n");
			}
			if (stringBuilder.Length > 1)
			{
				ChatRoom.chatTextString = stringBuilder.ToString().Substring(0, stringBuilder.Length - 1);
			}
			ChatRoom.UpdateText = false;
		}

		private void DoChatWindow()
		{
			if (ChatRoom.UpdateText || ChatRoom.chatTextLines == 0)
			{
				this.DoUpdateText();
			}
			List<string> list = new List<string>();
			list.AddRange(ChatRoom.irc.GetChannels());
			GUILayout.BeginArea(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.skin.box);
			if (list.Count == 0)
			{
				this.labelStyle.normal.textColor = Gui.Gui.MainColor;
				GUI.Box(new Rect(10f, 10f, (float)(Screen.width - 20), (float)(Screen.height - 20)), string.Empty);
				GUI.Label(new Rect(ChatRoom.Width(200f), ChatRoom.Height(130f), ChatRoom.Width(560f), ChatRoom.Height(30f)), "Connecting to the server, please wait...");
				if (GUI.Button(new Rect(ChatRoom.Width(200f), ChatRoom.Height(160f), ChatRoom.Width(560f), ChatRoom.Height(25f)), "Back"))
				{
					ChatRoom.IsShowing = false;
				}
				GUILayout.EndArea();
				return;
			}
			this.chatStyle.fontSize = ChatRoom.FontSize(14);
			this.labelStyle.fontSize = ChatRoom.FontSize(14);
			this.buttonStyle.fontSize = ChatRoom.FontSize(14);
			this.fieldStyle.fontSize = ChatRoom.FontSize(14);
			this.fieldStyle.padding = new RectOffset((int)ChatRoom.Width(8f), 0, 0, 0);
			this.fieldStyle.alignment = TextAnchor.MiddleLeft;
			this.chatStyle.normal.textColor = Color.black;
			this.fieldStyle.normal.textColor = Color.black;
            this.buttonStyle.normal.textColor = Gui.Gui.MainColor;
			this.labelStyle.normal.textColor = Color.black;
			foreach (KeyValuePair<string, string> current in this.channelsText)
			{
				if (current.Key != "SERVER" && list.IndexOf(current.Key) == -1)
				{
					list.Add(current.Key);
				}
			}
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			if (this.nextFrameChannel != string.Empty)
			{
				this.activeChannel = list.IndexOf(this.nextFrameChannel);
				this.nextFrameChannel = string.Empty;
			}
			if (this.unreadTextChannels.Length != list.Count)
			{
				int[] array = this.unreadTextChannels;
				this.unreadTextChannels = new int[list.Count];
				for (int i = 0; i < array.Length; i++)
				{
					if (this.unreadTextChannels.Length > i)
					{
						this.unreadTextChannels[i] = array[i];
					}
				}
			}
			string[] texts = new string[list.Count];
			this.activeChannel = GUILayout.Toolbar(this.activeChannel, texts, new GUILayoutOption[0]);
			this.unreadTextChannels[this.activeChannel] = 0;
			if (this.selectedChannel != this.activeChannel)
			{
				this.selectedChannel = this.activeChannel;
				ChatRoom.UpdateText = true;
			}
			this.nameOfChatPartner = list[this.activeChannel];
			int num = 940;
			int num2 = num / list.Count;
			this.labelStyle.alignment = TextAnchor.UpperCenter;
			for (int j = 0; j < list.Count; j++)
			{
                this.labelStyle.normal.textColor = Gui.Gui.MainColor;
				if (this.unreadTextChannels[j] == 1)
				{
					this.labelStyle.normal.textColor = Color.green;
				}
				GUI.Label(new Rect(ChatRoom.Width((float)(10 + j * num2)), ChatRoom.Height(5f), ChatRoom.Width((float)num2), ChatRoom.Height(28f)), list[j]);
			}
			this.labelStyle.normal.textColor = Color.black;
			this.labelStyle.alignment = TextAnchor.UpperLeft;
			Vector2 scrollPosition = new Vector2(0f, 3.40282347E+38f);
			if (this.channelsScrolls.ContainsKey(list[this.activeChannel]))
			{
				scrollPosition = this.channelsScrolls[list[this.activeChannel]];
			}
			this.chatStyle.hover.background = this.whiteTexture;
			this.chatStyle.hover.textColor = Color.black;
			this.chatStyle.normal.background = this.whiteTexture;
			this.chatStyle.normal.textColor = Color.black;
			GUI.TextArea(new Rect(ChatRoom.Width(10f), ChatRoom.Height(40f), ChatRoom.Width(710f), ChatRoom.Height(25f)), ChatRoom.Topic);
			this.chatStyle.hover.textColor = this.chatColor;
			this.chatStyle.hover.background = this.chatTexture;
			this.chatStyle.normal.textColor = this.chatColor;
			this.chatStyle.normal.background = this.chatTexture;
			int num3 = (int)this.chatStyle.CalcHeight(new GUIContent(ChatRoom.chatTextString), ChatRoom.Width(704f));
			this.channelsScrolls[list[this.activeChannel]] = GUI.BeginScrollView(new Rect(ChatRoom.Width(10f), ChatRoom.Height(70f), ChatRoom.Width(745f), ChatRoom.Height(440f)), scrollPosition, new Rect(0f, ChatRoom.Height(70f), ChatRoom.Width(700f), (float)num3 + ChatRoom.Height(0f)));
			int num4 = 440;
			if (num3 > 440)
			{
				num4 = num3;
			}
			GUI.TextArea(new Rect(ChatRoom.Width(0f), ChatRoom.Height(70f), ChatRoom.Width(710f), ChatRoom.Height((float)num4)), ChatRoom.chatTextString);
			float num5 = ChatRoom.Height(70f);
			int num6 = (int)this.chatStyle.CalcHeight(new GUIContent("base"), ChatRoom.Width(704f));
			float num7 = 0f;
			if (ChatRoom.chatTextLines > 0)
			{
				num7 = (float)(num3 / ChatRoom.chatTextLines);
			}
			for (int k = 0; k < ChatRoom.chatText.Count; k++)
			{
				int num8 = (int)this.chatStyle.CalcHeight(new GUIContent(ChatRoom.chatText[k].Text), ChatRoom.Width(710f));
				this.labelStyle.normal.textColor = ChatRoom.chatText[k].TextColor;
				GUI.Label(new Rect(ChatRoom.Width(3f), num5, ChatRoom.Width(704f), (float)num8), ChatRoom.chatText[k].Text);
				num5 += num7;
				if (num8 > num6)
				{
					int num9 = num8 / num6;
					num5 += num7 * (float)num9;
				}
			}
			this.labelStyle.normal.textColor = Color.black;
			GUI.EndScrollView();
			bool flag = false;
			Channel channel = ChatRoom.irc.GetChannel(list[this.activeChannel]);
			if (channel != null)
			{
				if (channel.Users.Values.Count != 0)
				{
					this.usersScroll = GuiBase.TouchScrollView(new Rect(ChatRoom.Width(770f), ChatRoom.Height(40f), ChatRoom.Width(175f), ChatRoom.Height(500f)), this.usersScroll, new Rect(0f, ChatRoom.Height(40f), ChatRoom.Width(140f), ChatRoom.Height((float)this.marginTopUsers)));
					this.marginTopUsers = 70;
					if (this.sortedUsers.Count != channel.Users.Count)
					{
						this.sortedUsers = (from x in channel.Users
						orderby x.Value.Nick
						select x.Value).ToList<ChannelUser>();
					}
					foreach (ChannelUser current2 in this.sortedUsers)
					{
						if (current2.Realname != null && !current2.Realname.Equals(this.userId))
						{
							if (current2.IsIrcOp)
							{
								this.buttonStyle.normal.textColor = Color.green;
							}
							else
							{
                                this.buttonStyle.normal.textColor = Gui.Gui.MainColor;
							}
							if (GUI.Button(new Rect(ChatRoom.Width(0f), ChatRoom.Height((float)this.marginTopUsers), ChatRoom.Width(145f), ChatRoom.Height(25f)), current2.Nick))
							{
								if (this.closedChannelsText.ContainsKey(current2.Nick))
								{
									this.channelsText.Add(current2.Nick, this.closedChannelsText[current2.Nick]);
									this.closedChannelsText.Remove(current2.Nick);
									this.nextFrameChannel = current2.Nick;
								}
								else if (!this.channelsText.ContainsKey(current2.Nick))
								{
									this.channelsText[current2.Nick] = string.Empty;
									this.nextFrameChannel = current2.Nick;
								}
								if (list.Contains(current2.Nick))
								{
									this.activeChannel = list.IndexOf(current2.Nick);
								}
								this.nameOfChatPartner = current2.Nick;
								flag = true;
							}
							this.marginTopUsers += 26;
						}
						else if (current2.Realname != null)
						{
							this.userName = current2.Nick;
						}
					}
                    this.labelStyle.normal.textColor = Gui.Gui.MainColor;
					GUI.Label(new Rect(ChatRoom.Width(0f), ChatRoom.Height(40f), ChatRoom.Width(120f), ChatRoom.Height(35f)), this.userName);
					this.labelStyle.normal.textColor = Color.black;
					GUI.EndScrollView();
				}
			}
			else
			{
				if (GUI.Button(new Rect(ChatRoom.Width(770f), ChatRoom.Height(40f), ChatRoom.Width(145f), ChatRoom.Height(30f)), new GUIContent("Close")) && this.channelsText.ContainsKey(this.nameOfChatPartner))
				{
					this.closedChannelsText.Add(this.nameOfChatPartner, this.channelsText[this.nameOfChatPartner]);
					this.channelsText.Remove(this.nameOfChatPartner);
					this.nameOfChatPartner = string.Empty;
					this.activeChannel = 0;
					flag = true;
				}
				bool flag2 = ChatRoom.mutedUsers.Contains(this.nameOfChatPartner);
				string text = "Ignore";
				if (flag2)
				{
					text = "Clear ignore";
				}
				if (GUI.Button(new Rect(ChatRoom.Width(770f), ChatRoom.Height(80f), ChatRoom.Width(145f), ChatRoom.Height(30f)), new GUIContent(text)) && this.channelsText.ContainsKey(this.nameOfChatPartner))
				{
					if (!flag2)
					{
						ChatRoom.mutedUsers.Add(this.nameOfChatPartner);
					}
					else
					{
						ChatRoom.mutedUsers.Remove(this.nameOfChatPartner);
					}
				}
			}
			GUI.SetNextControlName("Input");
			this.inputString = GUI.TextField(new Rect(ChatRoom.Width(10f), ChatRoom.Height(520f), ChatRoom.Width(560f), ChatRoom.Height(30f)), this.inputString);
			if (ChatRoom.focusInput)
			{
				ChatRoom.focusInput = false;
				GUI.FocusControl("Input");
			}
			Event current3 = Event.current;
			bool flag3 = current3.keyCode == KeyCode.Return || current3.keyCode == KeyCode.KeypadEnter;
			if ((GUI.Button(new Rect(ChatRoom.Width(580f), ChatRoom.Height(520f), ChatRoom.Width(140f), ChatRoom.Height(30f)), "SEND") || flag3) && !string.IsNullOrEmpty(this.inputString) && this.inputString.Length > 1)
			{
				ChatRoom.irc.SendMessage(SendType.Message, list[this.activeChannel], this.inputString);
				Dictionary<string, string> dictionary;
				if (!this.channelsText.ContainsKey(list[this.activeChannel]))
				{
					this.channelsText[list[this.activeChannel]] = string.Empty;
				}
				else
				{
					string key;
					(dictionary = this.channelsText)[key = list[this.activeChannel]] = dictionary[key] + "\r\n";
				}
				Dictionary<string, string> expr_D89 = dictionary = this.channelsText;
				string key2;
				string expr_D98 = key2 = list[this.activeChannel];
				string text2 = dictionary[key2];
				expr_D89[expr_D98] = string.Concat(new string[]
				{
					text2,
					this.DateTimeString,
					this.userName,
					": ",
					this.inputString
				});
				this.inputString = string.Empty;
				this.channelsScrolls[list[this.activeChannel]] = new Vector2(0f, 3.40282347E+38f);
				ChatRoom.UpdateText = true;
			}
			if (GUI.Button(new Rect(ChatRoom.Width(10f), ChatRoom.Height(560f), ChatRoom.Width(800f), ChatRoom.Height(30f)), "Back (you will still stay in the chat)"))
			{
				ChatRoom.IsShowing = false;
			}
			if (GUI.Button(new Rect(ChatRoom.Width(820f), ChatRoom.Height(560f), ChatRoom.Width(120f), ChatRoom.Height(30f)), "Quit Chat"))
			{
				ChatRoom.IsShowing = false;
				ChatRoom.IsNotStarted = true;
				ChatRoom.IsChannelChoosen = false;
				ChatRoom.Disconnect();
			}
			GUILayout.EndVertical();
			GUILayout.EndArea();
			this.fieldStyle.alignment = TextAnchor.MiddleCenter;
			this.fieldStyle.padding = new RectOffset(0, 0, 0, 0);
			if (flag)
			{
				ChatRoom.focusInput = true;
			}
		}

		public static void Disconnect()
		{
			if (ChatRoom.irc != null && ChatRoom.irc.IsConnected)
			{
				ChatRoom.irc.Quit("Application close");
				ChatRoom.irc.Disconnect();
			}
		}

		private void irc_OnError(object sender, ErrorEventArgs e)
		{
			UnityEngine.Debug.LogError(e.ErrorMessage);
		}

        //[DebuggerHidden]
        //private IEnumerator StopIRC()
        //{
        //    return new <IEnumerator>ChatRoom.GetIterator();
        //}
	}
}
