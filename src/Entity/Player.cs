using System;
using SwinGameSDK;
using Color = System.Drawing.Color;

namespace MyGame
{
	public class Player : LivingEntity
	{
		private System.Timers.Timer _tmr;
		private int _movementSpeed;
		private Map _currentMap;
		private Bitmap _charset;
		private int _charsetX;
		private int _charsetY;
		private int _animationSpeed = 100;
		private Inventory _inventory;

		private const int CHAR_CELL_WIDTH = 64;
		private const int CHAR_CELL_HEIGHT = 86;

		/// <summary>
		/// Gets the inventory.
		/// </summary>
		/// <value>The inventory.</value>
		public Inventory Inventory {
			get {
				return _inventory;
			}
		}

		/// <summary>
		/// Gets or sets the Charset Bitmap.
		/// </summary>
		/// <value>The char set.</value>
		public Bitmap CharSet {
			get {
				return _charset;
			}
			set {
				_charset = value;
			}
		}

		/// <summary>
		/// Gets or sets the movement speed.
		/// </summary>
		/// <value>The movement speed.</value>
		public int MovementSpeed {
			get {
				return _movementSpeed;
			}
			set {
				_movementSpeed = value;
			}
		}

		public void ToIdle() {
			Moving = false;
			_animationSpeed = 400;
		}

		public void ToMove() {
			Moving = true;
			_animationSpeed = 100;
		}

		private void IncrementCharsetX(object sender, EventArgs e) {
			_charsetX++;
			if (_charsetX > 3) {
				_charsetX = 0;
			}
		}

		private void UpdateAnimation() {
			switch (Direction)
			{
			case Direction.Up:
				if (SwinGame.KeyDown (GameStateManager.Controls["UpKey"]))
				{
					_charsetY = 0;
				}
				else
				{
					_charsetY = 1;
				}
				break;
			case Direction.Left:
				if (SwinGame.KeyDown (GameStateManager.Controls["LeftKey"]))
				{
					_charsetY = 6;
				}
				else
				{
					_charsetY = 7;
				}
				break;
			case Direction.Down:
				if (SwinGame.KeyDown (GameStateManager.Controls["DownKey"]))
				{
					_charsetY = 4;
				}
				else
				{
					_charsetY = 5;
				}
				break;
			case Direction.Right:
				if (SwinGame.KeyDown (GameStateManager.Controls["RightKey"]))
				{
					_charsetY = 2;
				}
				else
				{
					_charsetY = 3;
				}
				break;
			}
			if (_tmr.Interval != _animationSpeed)
			{
				_tmr.Interval = _animationSpeed;
			}
		}

		private void Move ()
		{
			if (Moving)
			{
				if (this.Direction == Direction.Up) //Up movement, decreasing y
				{
					if (!_currentMap.IsSolidAt (MapX, MapY - 1) && (!_currentMap.IsSolidAt (MapX + 1, MapY - 1) || OffsetX == 0)) //Testing for Tile to the top, and to the bottom (if offset isn't 0)
					{
						if (OffsetY < _movementSpeed)
						{
							MapY--;
							int remainder = _movementSpeed - OffsetY;
							OffsetY = Tile.TILE_SIZE - remainder; //Must decrease offset as well as decrease map position
						}
						else
						{
							OffsetY -= _movementSpeed;
						}
					}
					else
					{
						if (OffsetY != 0)
						{
							if (OffsetY - _movementSpeed < 0)
							{
								OffsetY = 0;
							}
							else
							{
								OffsetY -= _movementSpeed;
							}
						}
					}
				}
				else if (this.Direction == Direction.Left) //Left movement, decreasing x
				{
					if (!_currentMap.IsSolidAt (MapX - 1, MapY) && (!_currentMap.IsSolidAt (MapX - 1, MapY + 1) || OffsetY == 0)) //Testing for Tile to the left, and to the left-bottom (if offset isn't 0)
					{ 
						if (OffsetX < _movementSpeed)
						{
							MapX--;
							int remainder = _movementSpeed - OffsetX;
							OffsetX = Tile.TILE_SIZE - remainder;
						}
						else
						{
							OffsetX -= _movementSpeed;
						}
					}
					else
					{
						if (OffsetX != 0)
						{
							if (OffsetX - _movementSpeed < 0)
							{
								OffsetX = 0;
							}
							else
							{
								OffsetX -= _movementSpeed;
							}
						}
					}
				}
				else if (this.Direction == Direction.Down) //Down movement, increasing y
				{
					if (!_currentMap.IsSolidAt (MapX, MapY + 1) && (!_currentMap.IsSolidAt (MapX + 1, MapY + 1) || OffsetX == 0))
					{
						if (OffsetY > (Tile.TILE_SIZE - _movementSpeed))
						{
							MapY++;
							int remainder = _movementSpeed - (Tile.TILE_SIZE - OffsetY);
							OffsetY = remainder;
						}
						else
						{
							OffsetY += _movementSpeed;
						}
					}
					else
					{
						OffsetY = 0;
					}
				}
				else if (this.Direction == Direction.Right) //Right movement, increasing x
				{
					if (!_currentMap.IsSolidAt (MapX + 1, MapY) && (!_currentMap.IsSolidAt (MapX + 1, MapY + 1) || OffsetY == 0))
					{
						if (OffsetX > (Tile.TILE_SIZE - _movementSpeed))
						{
							MapX++;
							int remainder = _movementSpeed - (Tile.TILE_SIZE - OffsetX);
							OffsetX = remainder;
						}
						else
						{
							OffsetX += _movementSpeed;
						}
					}
					else
					{
						OffsetX = 0;
					}
				}
			}
		}

		public void Update() {
			//Movement
			Move ();

			//Animation
			UpdateAnimation();
		}

		private void DrawChar() {
			SwinGame.DrawBitmapPart (_charset, CHAR_CELL_WIDTH * _charsetX, CHAR_CELL_HEIGHT * _charsetY, CHAR_CELL_WIDTH, CHAR_CELL_HEIGHT, Map.PLAYER_TILE_OFFSET_X * Tile.TILE_SIZE, Map.PLAYER_TILE_OFFSET_Y * Tile.TILE_SIZE + (64 - CHAR_CELL_HEIGHT));
		}

		private void DrawEquipment() {

		}

		public void Draw() {
			//Character
			DrawChar ();

			//Equipment
			DrawEquipment ();
		}

		public Player (string name, Map currentMap, int mapX, int mapY) : base(name, mapX, mapY)
		{
			Name = name;
			_movementSpeed = 4;
			_currentMap = currentMap;
			Direction = Direction.Down;
			_charset = SwinGame.LoadBitmap ("playerclothed.png");
			_inventory = new Inventory (16);
			_tmr = new System.Timers.Timer (_animationSpeed);
			_tmr.Elapsed += new System.Timers.ElapsedEventHandler (IncrementCharsetX );
			_tmr.Start ();
		}
	}
}

