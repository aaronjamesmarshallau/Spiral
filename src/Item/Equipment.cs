using System;

namespace MyGame
{
	public class Equipment
	{
		private Head _head;
		private Chest _chest;
		private Feet _feet;
		private Weapon _weapon;


		public Head Head {
			get {
				return _head;
			}
			set {
				_head = value;
			}
		}

		public Chest Chest {
			get {
				return _chest;
			}
			set {
				_chest = value;
			}
		}

		public Feet Feet {
			get {
				return _feet;
			}
			set {
				_feet = value;
			}
		}

		public Weapon Weapon {
			get {
				return _weapon;
			}
			set {
				_weapon = value;
			}
		}

		public void Draw() {

		}

		public Equipment (Head head, Chest chest, Feet feet, Weapon weapon)
		{
			_head = head;
			_chest = chest;
			_feet = feet;
		}

		public Equipment () : this (null, null, null, null) {

		}
	}
}

