using System;
using SwinGameSDK;

namespace MyGame
{
	public abstract class Armor : Item, Wearable
	{
		private int _defenseRating;



		public int DefenseRating {
			get {
				return DefenseRating;
			}
			set {
				DefenseRating = value;
			}
		}

		public Armor (string name, string description) : base(name, description) { }

		public Armor (string name, string description, Bitmap image) : base(name, description, image) {	}
	}
}

