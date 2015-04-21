using System;
using SwinGameSDK;
using Color = System.Drawing.Color;

namespace MyGame
{
	class InputBox
	{
		private int _x;
		private int _y;
		private int _width;
		private int _height;
		private string _prompt;
		private string _value;
		private Button _button;
		private InputField _input;
		private ButtonResponse _response;

		public string Value {
			get {
				return _value;
			}
		}

		public ButtonResponse Response {
			get {
				return _response;
			}
		}

		public int X {
			get {
				return _x;
			}
			set {
				_x = value;
			}
		}

		public int Y {
			get {
				return _y;
			}
			set {
				_y = value;
			}
		}

		private void LoadResources() {
			SwinGame.LoadFontNamed ("PromptText", "arial.ttf", 18);
		}

		public void Draw ()
		{
			SwinGame.FillRectangle(Color.Wheat, _x, _y, _width, _height);
			SwinGame.DrawRectangle (Color.DarkOrange, _x - 1, _y - 1, _width + 2, _height + 2);
			SwinGame.DrawRectangle (Color.DarkOrange, _x - 2, _y - 2, _width + 4, _height + 4);
			SwinGame.DrawRectangle (Color.DarkOrange, _x - 3, _y - 3, _width + 6, _height + 6);
			SwinGame.DrawRectangle (Color.DarkOrange, _x - 4, _y - 4, _width + 8, _height + 8);
			SwinGame.DrawRectangle (Color.DarkOrange, _x - 5, _y - 5, _width + 10, _height + 10);
			SwinGame.DrawTextLinesOnScreen (_prompt, Color.Black, Color.Transparent, "PromptText", FontAlignment.AlignCenter, _x + 20, _y + 20, _width - 40, _height - 80);
			_button.Draw ();
			_input.Draw ();
		}

		public void Update() {
			_button.Update ();
			_input.Update ();
			_value = _input.Text;
		}

		public void ButtonClicked(object sender, EventArgs e) {

			_response = ButtonResponse.Ok;
		}

		public InputBox(String prompt) {
			_prompt = prompt;
			_width = SwinGame.ScreenWidth() / 2;
			_height = SwinGame.ScreenHeight() / 3;
			_x = SwinGame.ScreenWidth() / 4;
			_y = SwinGame.ScreenHeight () / 3;
			_response = ButtonResponse.NotSet;
			_button = new Button ("btnOk", "OK", _x + _width / 2, _y + _height - 40);
			_button.X = _x + _width / 2 - _button.Width / 2;
			_button.Y = _x + _height - _button.Height / 2;
			_input = new InputField (_x + 30, _y + 100, _width - 60, 20);
			_button.ButtonClick += new EventHandler (ButtonClicked);
			LoadResources ();
		}
	}


}

