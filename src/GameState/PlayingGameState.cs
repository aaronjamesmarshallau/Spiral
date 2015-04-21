using System;
using SwinGameSDK;
using System.IO;
using System.Timers;
using Color = System.Drawing.Color;

namespace MyGame
{
	public class PlayingGameState : GameState
	{
		private bool _playMusic;
		private Map _currentMap;
		private Player _player;

		public override void Returning(GameState returnFrom) {
			if (_currentMap.Music != null)
			{
				_currentMap.Music.Play ();
			}
		}

		public override void Entering() {
			if (File.Exists ("./Resources/players/" + _player.Name + ".dat"))
			{
				LoadFrom ("./Resources/players/" + _player.Name + ".dat");
			}
			else
			{
				File.Create ("./Resources/players/" + _player.Name + ".dat").Close();
			}

			_playMusic = GameStateManager.PlayMusic;

			if (_currentMap != null)
			{
				_currentMap.Load ();
				if ((_currentMap.Music != null) && (_playMusic == true))
				{
					_currentMap.Music.Play ();
				}
				else
				{
					Console.WriteLine ("It's dun fucked");
				}
			}

			_player.MapX = _currentMap.InitialX;
			_player.MapY = _currentMap.InitialY;

			_player.Update ();

			
		}

		public override void Leaving() {
			if (SwinGame.MusicPlaying())
			{
				SwinGame.FadeMusicOut (1000);
			}
		}

		public override void Draw() {
			_currentMap.Draw (_player.MapX, _player.MapY, _player.OffsetX, _player.OffsetY);
			_player.Draw ();
		}

		private void HandleMovement() {
			//KeysDown
			if (SwinGame.KeyDown (GameStateManager.Controls["LeftKey"]))
			{
				_player.ToMove ();
				_player.Direction = Direction.Left;
			}
			else if (SwinGame.KeyDown (GameStateManager.Controls["RightKey"]))
			{
				_player.ToMove ();
				_player.Direction = Direction.Right;
			}
			if (SwinGame.KeyDown (GameStateManager.Controls["UpKey"]))
			{
				_player.ToMove ();
				_player.Direction = Direction.Up;
			}
			else if (SwinGame.KeyDown (GameStateManager.Controls["DownKey"]))
			{
				_player.ToMove ();
				_player.Direction = Direction.Down;
			}

			//KeysReleased
			if (SwinGame.KeyReleased (GameStateManager.Controls["LeftKey"]) && _player.Direction == Direction.Left)
			{
				_player.ToIdle ();
			}
			else if (SwinGame.KeyReleased (GameStateManager.Controls["RightKey"]) && _player.Direction == Direction.Right)
			{
				_player.ToIdle ();
			}
			if (SwinGame.KeyReleased (GameStateManager.Controls["UpKey"]) && _player.Direction == Direction.Up)
			{
				_player.ToIdle ();
			}
			else if (SwinGame.KeyReleased (GameStateManager.Controls["DownKey"]) && _player.Direction == Direction.Down)
			{
				_player.ToIdle ();
			}
		}

		public override void DrawPaused() {
			UpdatePaused ();
			Draw ();
		}

		public override void UpdatePaused() {

		}

		private void HandleInput () {
			if (SwinGame.KeyTyped (KeyCode.vk_ESCAPE))
			{
				GSM.AddState (new PauseGameState (GSM));
			}
			if (SwinGame.KeyTyped (KeyCode.vk_i))
			{
				GSM.AddState (new InventoryGameState (GSM, _player));
			}
		}

		public override void Update() {
			//Input
			HandleInput ();
			HandleMovement ();

			//Character
			_player.Update ();
		}

		public void LoadFrom(string fileName) {
			//throw new NotImplementedException ();
		}

		public PlayingGameState (GameStateManager gsm, string mapName, string playerName) : base(gsm)
		{
			_currentMap = new Map ("./Resources/maps/" + mapName + ".map");
			_playMusic = false;
			_player = new Player (playerName, _currentMap, _currentMap.InitialX, _currentMap.InitialY);
		}

		public PlayingGameState (GameStateManager gsm) : this(gsm, "ferring_town", null)
		{

		}
	}
}

