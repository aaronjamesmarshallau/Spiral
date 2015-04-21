using System;
using SwinGameSDK;
using Color = System.Drawing.Color;

namespace MyGame
{
	public class InputGameState : GameState
	{
		private InputBox _inputBox;
		private string _return;

		public string Return {
			get {
				return _return;
			}
		}

		public override void Returning(GameState returnFrom) {

		}

		public override void Entering() {

		}

		public override void Leaving() {

		}

		public override void Draw() {
			SwinGame.FillRectangle (Color.FromArgb(127, 0,0,0), 0, 0, SwinGame.ScreenWidth(), SwinGame.ScreenHeight());
			_inputBox.Draw ();
		}

		public override void DrawPaused() {

		}

		public override void Update() {
			_inputBox.Update ();
			if (_inputBox.Response == ButtonResponse.Ok)
			{
				_return = _inputBox.Value;
				GSM.Return ();
			}
		}

		public override void UpdatePaused() {

		}

		public InputGameState (GameStateManager gsm, string prompt) : base(gsm)
		{
			_inputBox = new InputBox (prompt);
		}
	}

}

