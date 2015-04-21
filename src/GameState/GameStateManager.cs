using System;
using System.IO;
using System.Collections.Generic;
using SwinGameSDK;
using Color = System.Drawing.Color;

namespace MyGame
{
	public class GameStateManager
	{
		private Stack<GameState> _currentStates = new Stack<GameState>();
		private static Dictionary<string, object> _config = new Dictionary<string, object>();
		private static Dictionary<string, KeyCode> _controls = new Dictionary<string, KeyCode>();
		private bool _exit = false;

		#region Config

		/// <summary>
		/// Gets the config.
		/// </summary>
		/// <value>The config.</value>
		public static Dictionary<string, object> Config {
			get {
				return _config;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the game is muted.
		/// </summary>
		/// <value><c>true</c> if play music; otherwise, <c>false</c>.</value>
		public static bool PlayMusic {
			get {
				return Convert.ToBoolean (_config ["PlayMusic"]);
			}
			set {
				_config ["PlayMusic"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the SFX volume.
		/// </summary>
		/// <value>The SFX volume.</value>
		public static int SFXVolume {
			get {
				return Convert.ToInt32 (_config ["SFXvol"]);
			}
			set {
				_config ["SFXvol"] = value;
			}
		}


		/// <summary>
		/// Gets or sets the music volume.
		/// </summary>
		/// <value>The music volume.</value>
		public static int MusicVolume {
			get {
				return Convert.ToInt32 (_config["MUSvol"]);
			}
			set {
				_config ["MUSvol"] = value;
			}
		}

		/// <summary>
		/// Loads the config.
		/// </summary>
		public static void LoadConfig() {
			if (!File.Exists ("./config/config.txt"))
			{
				File.Create ("./config/config.txt").Close ();
			}

			StreamReader sr = new StreamReader ("./config/config.txt");
			string line;
			string[] lineArr = new string[2];
			try {
				do {
					line = sr.ReadLine();
					lineArr = line.Split('=');
					_config.Add(lineArr[0], lineArr[1]);
				} while (!sr.EndOfStream);
				foreach(KeyValuePair<string, object> kvp in _config) {
					Console.WriteLine("Config item {0} loaded with value \"{1}\"", kvp.Key, kvp.Value);
				}
			}
			catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}
			finally {
				sr.Close ();
				sr.Dispose();
			}
		}

		public static void SaveConfig() {
			StreamWriter sw = new StreamWriter ("./config/config.txt");
			string line;
			try {
				foreach(KeyValuePair<string, object> kvp in _config) {
					line = kvp.Key + "=" + kvp.Value.ToString();
					sw.WriteLine(line);
				}
			}
			catch (Exception e) {
				Console.WriteLine (e.Message);
			}
			finally {
				sw.Close ();
				sw.Dispose ();
			}
		}

		#endregion

		#region Controls

		public static Dictionary<string, KeyCode> Controls {
			get {
				return _controls;
			}
		}

		/// <summary>
		/// Loads the Game's controls (at "./config/controls.txt")
		/// </summary>
		public static void LoadControls() {
			if (!File.Exists ("./config/controls.txt"))
			{
				File.Create ("./config/controls.txt");
			}

			StreamReader sr = new StreamReader ("./config/controls.txt");
			string line;
			string[] lineArr = new string[2];
			string key;
			KeyCode val;
			try {
				do {
					line = sr.ReadLine();
					lineArr = line.Split('=');
					key = lineArr[0];
					val = (KeyCode)Enum.Parse(typeof(KeyCode), lineArr[1]);
					Console.WriteLine("{0}: {1}", key, val);
					_controls.Add(key, val);
				} while (!sr.EndOfStream);
				foreach(KeyValuePair<string, KeyCode> kvp in _controls) {
					Console.WriteLine("Control {0} loaded with key \"{1}\"", kvp.Key, kvp.Value);
				}
			}
			catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}
			finally {
				sr.Close ();
				sr.Dispose();
			}
		}

		public static void SaveControls() {
			StreamWriter sw = new StreamWriter ("./config/controls.txt");
			string line;
			try {
				foreach(KeyValuePair<string, KeyCode> kvp in _controls) {
					line = kvp.Key + "=" + kvp.Value.ToString();
					sw.WriteLine(line);
				}
			}
			catch (Exception e) {
				Console.WriteLine (e.Message);
			}
			finally {
				sw.Close ();
				sw.Dispose ();
			}
		}

		#endregion

		/// <summary>
		/// Gets the current <see cref="GameState"/> .
		/// </summary>
		/// <value>Current GameState</value>
		public GameState CurrentState {
			get {
				if (_currentStates.Count > 0)
				{
					return _currentStates.Peek ();
				}
				return null;
			}
		}

		/// <summary>
		/// Adds a new <see cref="GameState"/> to the stack,
		/// triggering the <see cref="CurrentState"/> Leaving method
		/// and the <see cref="newState"/> Entering method.
		/// </summary>
		/// <param name="newState">The new <see cref="GameState"/> </param>
		public void AddState(GameState newState) {
			if (_currentStates.Count > 0)
			{
				_currentStates.Peek ().Leaving ();
			}
			_currentStates.Push(newState);
			_currentStates.Peek ().Entering ();
		}

		/// <summary>
		/// Exits the <see cref="CurrentState"/>, triggering the
		/// Leaving method, and adds the <see cref="newState"/>, 
		/// triggering the Entering method.
		/// </summary>
		/// <param name="newState">The new <see cref="GameState"/> </param>
		public void SwapState(GameState newState) {
			if (_currentStates.Count < 1)
			{
				AddState (newState);
				return;
			}
			else
			{
				_currentStates.Peek ().Leaving ();
				_currentStates.Pop ();
				_currentStates.Push (newState);
				_currentStates.Peek ().Entering ();
			}
		}

		/// <summary>
		/// Returns from the <see cref="CurrentState"/>  to the previous
		/// state. If no state exists, exits.
		/// </summary>
		public void Return() {
			GameState returnFrom;
			_currentStates.Peek ().Leaving ();
			returnFrom = _currentStates.Pop ();
			if (_currentStates.Count > 0)
			{
				_currentStates.Peek ().Returning (returnFrom);
			}
		}

		/// <summary>
		/// Starts the <see cref="GameState"/> loop.
		/// </summary>
		public void Start() {
			while ((_currentStates.Count > 0) && !(SwinGame.WindowCloseRequested()) && !_exit)
			{
				SwinGame.ClearScreen (Color.White);
				SwinGame.ProcessEvents ();

				foreach (GameState gs in _currentStates)
				{
					if (!(gs == _currentStates.Peek ()))
					{
						gs.DrawPaused ();
						gs.UpdatePaused ();
					}
				}

				_currentStates.Peek ().Draw ();
				_currentStates.Peek ().Update ();

				SwinGame.RefreshScreen (60);
			}
		}

		public void Exit() {
			_exit = true;
		}

		public GameStateManager ()
		{
		}
	}
}

