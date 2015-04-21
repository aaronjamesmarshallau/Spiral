using System;
using SwinGameSDK;

namespace MyGame
{
	public class Weapon : Item
	{
		private int _attackRating;
		private int _durability;

		/// <summary>
		/// Gets or sets the attack rating.
		/// </summary>
		/// <value>The attack rating.</value>
		public int AttackRating {
			get {
				return _attackRating;
			}
			set {
				_attackRating = value;
			}
		}

		/// <summary>
		/// Gets or sets the durability.
		/// </summary>
		/// <value>The durability.</value>
		public int Durability {
			get {
				return _durability;
			}
			set {
				_durability = value;
			}
		}

		public Weapon (string name, string description) : base(name, description) {	}

		public Weapon (string name, string description, Bitmap image) : base(name, description, image) { }
	}

}

