using System;
using SwinGameSDK;
using Color = System.Drawing.Color;

namespace MyGame
{
	public class InputField
	{ 
		private string _text;
		private int _x;
		private int _y;
		private int _width;
		private int _height;
		private Font _font;
		private int _maxLength;
		private bool _cursor;
		private System.Timers.Timer tmr;

		public int MaxLength {
			get {
				return _maxLength;
			}
			set {
				_maxLength = value;
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

		public int X {
			get {
				return _x;
			}
			set {
				_x = value;
			}
		}

		public int Height {
			get {
				return _height;
			}
			set {
				_height = value;
			}
		}

		public int Width {
			get {
				return _width;
			}
			set {
				_width = value;
			}
		}

		public Font Font {
			get {
				return _font;
			}
			set {
				_font = value;
			}
		}

		public string Text {
			get {
				return _text;
			}
			set {
				_text = value;
			}
		}

		public void Draw() {
			SwinGame.FillRectangle (Color.Black, _x, _y, _width, _height);
			SwinGame.DrawRectangle (Color.White, _x, _y, _width, _height);
			SwinGame.DrawText (_text, Color.White, _font, _x + 2, _y + 2);
			if (_cursor)
			{
				SwinGame.DrawLine (Color.White, _x + SwinGame.TextWidth (_font, _text) + 2, _y + 2, _x + SwinGame.TextWidth (_font, _text) + 2, _y + _height - 4);
			}
		}

		public void Update() {
			_text = SwinGame.TextReadAsASCII ();
			if (SwinGame.KeyDown(KeyCode.vk_RETURN))
			{
				SwinGame.EndReadingText ();
			}
		}

		private void TimerTick(object sender, EventArgs e) {
			_cursor = !_cursor;
		}

		public InputField (int x, int y, int width, int height) {
			_x = x;
			_y = y;
			_width = width;
			_height = height;
			_cursor = true;
			_maxLength = 28;
			_font = SwinGame.LoadFont ("arial.ttf", 16);
			tmr = new System.Timers.Timer (500);
			tmr.Elapsed += TimerTick;
			tmr.Start ();

			SwinGame.StartReadingText (Color.Transparent, _maxLength, _font, SwinGame.ScreenWidth(), SwinGame.ScreenHeight());
		}

	}



}

