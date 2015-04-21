using System;

namespace MyGame
{
	public abstract class LivingEntity
	{
		public event EventHandler EntityChangeX;
		public event EventHandler EntityChangeY;

		private bool _moving;
		private Direction _direction;
		private bool _isAttackable;
		private int _maxHealth;
		private int _currHealth;
		private string _name;
		private int _mapX;
		private int _mapY;
		private int _offsetX;
		private int _offsetY;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="MyGame.LivingEntity"/> is moving.
		/// </summary>
		/// <value><c>true</c> if moving; otherwise, <c>false</c>.</value>
		public bool Moving {
			get {
				return _moving;
			}
			set {
				_moving = value;
			}
		}

		/// <summary>
		/// Gets or sets the entity's direction.
		/// </summary>
		/// <value>The direction.</value>
		public Direction Direction {
			get {
				return _direction;
			}
			set {
				_direction = value;
			}
		}

		/// <summary>
		/// Gets or sets the tile offset in the X direction.
		/// </summary>
		/// <value>The offset x.</value>
		public int OffsetX {
			get {
				return _offsetX;
			}
			set {
				_offsetX = value;
			}
		}

		/// <summary>
		/// Gets or sets the tile offset in the Y direction.
		/// </summary>
		/// <value>The offset y.</value>
		public int OffsetY {
			get {
				return _offsetY;
			}
			set {
				_offsetY = value;
			}
		}

		/// <summary>
		/// Gets or sets the X coordinate on the Current Map.
		/// </summary>
		/// <value>The map x.</value>
		public int MapX {
			get {
				return _mapX;
			}
			set {
				_mapX = value;
				if (EntityChangeX != null)
				{
					EntityChangeX(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Gets or sets the Y coordinate on the Current Map.
		/// </summary>
		/// <value>The map y.</value>
		public int MapY {
			get {
				return _mapY;
			}
			set {
				_mapY = value;
				if (EntityChangeY != null)
				{
					EntityChangeY(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}

		/// <summary>
		/// Gets or sets the current health. If current health is set to greater than 
		/// the current <see cref="MaxHealth"/>, then the current health is set to
		/// the current <see cref="MaxHealth"/> value.
		/// </summary>
		/// <value>The current health.</value>
		public int CurrentHealth {
			get {
				return _currHealth;
			}
			set {
				_currHealth = value;
				if (_currHealth > _maxHealth)
				{
					_currHealth = _maxHealth;
				}
			}
		}

		/// <summary>
		/// Gets or sets the max health. If the max health is set to lesser than
		/// the current <see cref="CurrentHealth"/>, then the current health is set to
		/// the current <see cref="MaxHealth"/> value.
		/// </summary>
		/// <value>The max health.</value>
		public int MaxHealth {
			get {
				return _maxHealth;
			}
			set {
				_maxHealth = value;
				if (_maxHealth < _currHealth)
				{
					_currHealth = _maxHealth;
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is attackable.
		/// </summary>
		/// <value><c>true</c> if this instance is attackable; otherwise, <c>false</c>.</value>
		public bool IsAttackable {
			get {
				return _isAttackable;
			}
			set {
				_isAttackable = value;
			}
		}

		public void MoveTo(int mapX, int mapY) {

		}

		public LivingEntity (string name, int mapX, int mapY)
		{
			Name = name;
			MapX = mapX;
			MapY = mapY;
		}
	}
}

