using System;
using SwinGameSDK;
using Color = System.Drawing.Color;

namespace MyGame
{
	public abstract class Item
	{
		private string _name;
		private string _description;
		private Bitmap _image;

		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}

		public string Description {
			get {
				return _description;
			}
			set {
				_description = value;
			}
		}

		public Bitmap Image {
			get {
				return _image;
			}
			set {
				_image = value;
			}
		}

		public virtual void Draw(int x, int y) {
			_image.Draw (x, y);
		}

		public Item(string name, string description) {
			_name = name;
			_description = description;
		}

		public Item(string name, string description, Bitmap image) : this(name, description) {
			_image = image;
		}

	}


}

