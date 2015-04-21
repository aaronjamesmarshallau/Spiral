using System;
using SwinGameSDK;
using Color = System.Drawing.Color;

namespace MyGame
{
	public class InventoryGameState : GameState
	{
		private Player _player;
		private Bitmap _gui;

		public override void Entering() {

		}

		public override void Leaving() {
			_gui.Dispose ();
		}

		public override void Draw() {
			SwinGame.FillRectangle (Color.FromArgb (128, 0, 0, 0), 0, 0, SwinGame.ScreenWidth (), SwinGame.ScreenHeight ());
			_gui.Draw (SwinGame.ScreenWidth () / 2 - _gui.Width / 2, SwinGame.ScreenHeight () / 2 - _gui.Height / 2);
			_player.Inventory.Draw (SwinGame.ScreenWidth() / 2 + 10, SwinGame.ScreenHeight() / 2 - 140, 4, 4);
			//_player.Equipment.Draw ();
		}

		private void HandleInput() {
			if (SwinGame.KeyTyped (KeyCode.vk_i) || SwinGame.KeyTyped(KeyCode.vk_ESCAPE))
			{
				GSM.Return ();
			}
		}

		public override void Update() {
			HandleInput ();
		}

		public override void Returning(GameState gs) {

		}

		public override void DrawPaused() {

		}

		public override void UpdatePaused() {

		}

		public InventoryGameState (GameStateManager gsm, Player p) : base(gsm)
		{
			_player = p;
			_gui = new Bitmap("inventorygui.png");
		}
	}
}

