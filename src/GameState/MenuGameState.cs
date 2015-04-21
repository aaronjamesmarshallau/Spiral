using System;
using SwinGameSDK;
using Color = System.Drawing.Color;
using System.IO;

namespace MyGame
{
	public class MenuGameState : GameState
	{
		private Bitmap _backgroundImage;
		private Bitmap _logo;
		private Bitmap[] _soundIcons = new Bitmap[4];
		private float _backgroundX;
		private Font _font;
		private Music _music;
		private string[,] _menuOptions = new string[4,2] {{"Start Game", "0"}, {"Continue", "0"}, {"Options", "0"}, {"Exit Game", "0"}};

		public override void Entering() {
			if (GameStateManager.PlayMusic)
			{
				_music.Play ();
			}
		}

		public override void Returning(GameState returnFrom) {
			if (returnFrom is InputGameState)
			{
				InputGameState rf = (InputGameState)returnFrom;
				GSM.SwapState(new PlayingGameState(GSM, "ferring_town", rf.Return));
			}
		}

		public override void Leaving() {
			SwinGame.StopMusic ();
			SwinGame.FreeMusic (_music);
		}

		#region Drawing

		private void DrawBackground() {
			if (_backgroundX < -(_backgroundImage.Width) + 800)
			{
				SwinGame.DrawBitmap (_backgroundImage, _backgroundX + _backgroundImage.Width, 0);
			}
			SwinGame.DrawBitmap (_backgroundImage, _backgroundX, 0);
		}

		private void DrawLogo() {
			SwinGame.DrawBitmap (_logo, SwinGame.ScreenWidth () - 200, 0);
			SwinGame.DrawText ("Spiral", Color.Black, SwinGame.LoadFont ("squaredeal.ttf", 64), SwinGame.ScreenWidth () - (200 + SwinGame.TextWidth (SwinGame.LoadFont ("squaredeal.ttf", 64), "Spiral")), 20);
		}

		private void DrawSoundIcon() {
			if (GameStateManager.PlayMusic)
			{
				if (SwinGame.PointInRect (SwinGame.MousePosition (), 10, 10, SwinGame.BitmapWidth (_soundIcons [0]), SwinGame.BitmapHeight (_soundIcons [0])))
				{
					SwinGame.DrawBitmap (_soundIcons [2], 10, 10);
				}
				else
				{
					SwinGame.DrawBitmap (_soundIcons [0], 10, 10);
				}
			}
			else
			{
				if (SwinGame.PointInRect (SwinGame.MousePosition (), 10, 10, SwinGame.BitmapWidth (_soundIcons [0]), SwinGame.BitmapHeight (_soundIcons [0])))
				{
					SwinGame.DrawBitmap (_soundIcons [3], 10, 10);
				}
				else
				{
					SwinGame.DrawBitmap (_soundIcons [1], 10, 10);
				}
			}
		}

		private void DrawMenu() {
			float menuX;
			float menuY;

			for (int i = 0; i < _menuOptions.GetLength(0); i++)
			{
				menuY = 475 + SwinGame.TextHeight(_font, _menuOptions[i,0]) * i;
				menuX = SwinGame.ScreenWidth() - SwinGame.TextWidth (_font, _menuOptions[i,0]) - 10;
				if (_menuOptions[i, 1] == "1")
				{
					SwinGame.DrawText (_menuOptions [i,0], Color.LightBlue, _font, menuX, menuY);
				}
				else
				{
					SwinGame.DrawText (_menuOptions [i,0], Color.White, _font, menuX, menuY);
				}
			}
		}

		public override void Draw() {
			//Background
			DrawBackground ();

			//Logo
			DrawLogo ();

			//Mute icon
			DrawSoundIcon ();
					
			//Menu Options
			DrawMenu ();
		}

		public override void DrawPaused () {
			Draw ();
		}

		#endregion

		#region Updating

		private void UpdateBackground() {
			if (_backgroundX > -(_backgroundImage.Width))
			{
				_backgroundX--;
			} else {
				_backgroundX = 0;
			}
		}

		private void UpdateMusic() {
			if (!SwinGame.MusicPlaying () && GameStateManager.PlayMusic)
			{
				_music.Play ();
			}
			else if (!GameStateManager.PlayMusic)
			{
				SwinGame.StopMusic ();
			}
		}

		private void UpdateMenu() {
			float menuX;
			float menuY;

			for (int i = 0; i < _menuOptions.GetLength(0); i++)
			{
				menuY = 475 + SwinGame.TextHeight (_font, _menuOptions [i,0]) * i;
				menuX = SwinGame.ScreenWidth () - SwinGame.TextWidth (_font, _menuOptions [i,0]) - 10;

				if (SwinGame.PointInRect (SwinGame.MousePosition (), menuX, menuY, SwinGame.TextWidth (_font, _menuOptions [i, 0]), SwinGame.TextHeight (_font, _menuOptions [i, 0])))
				{
					_menuOptions [i, 1] = "1";
				}
				else
				{
					_menuOptions [i, 1] = "0";
				}

				if (SwinGame.MouseClicked (MouseButton.LeftButton))
				{
					if (_menuOptions [i, 1] == "1")
					{
						switch (_menuOptions [i, 0])
						{
						case "Start Game":
							InputGameState igs = new InputGameState (GSM, "Please enter your character's name!");
							GSM.AddState (igs);
							break;
						case "Continue":
							break;
						case "Options":
							break;
						case "Exit Game":
							GSM.Return ();
							break;
						default:
							//do something
							break;
						}
					}
				} 

			}
		}

		private void UpdateSound () {
			if (SwinGame.PointInRect(SwinGame.MousePosition(), 10, 10, SwinGame.BitmapWidth(_soundIcons[0]) + 10, SwinGame.BitmapHeight(_soundIcons[0]) + 10) && SwinGame.MouseClicked(MouseButton.LeftButton))
			{
				GameStateManager.PlayMusic = !GameStateManager.PlayMusic;
			}
		}

		public override void Update() {
			UpdateBackground ();

			UpdateMusic ();

			UpdateMenu ();

			UpdateSound ();
		}

		public override void UpdatePaused() {
			UpdateBackground ();
		}

		#endregion

		public MenuGameState (GameStateManager gsm, string backgroundPath) : base (gsm)
		{
			_backgroundImage = SwinGame.LoadBitmap (backgroundPath);
			_logo = SwinGame.LoadBitmap ("logo200x200.png");
			_music = SwinGame.LoadMusic ("menu.mp3");
			_font = SwinGame.LoadFont ("squaredeal.ttf", 28);
			_soundIcons [0] = SwinGame.LoadBitmap ("sound.png");
			_soundIcons [1] = SwinGame.LoadBitmap ("mute.png");
			_soundIcons [2] = SwinGame.LoadBitmap ("soundSelected.png");
			_soundIcons [3] = SwinGame.LoadBitmap ("muteSelected.png");
		}

		public MenuGameState (GameStateManager gsm) : this(gsm, "background.png") {}
	}
}

