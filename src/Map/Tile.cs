using System;
using SwinGameSDK;
using Color = System.Drawing.Color;

namespace MyGame
{
	public abstract class Tile
	{
		private Bitmap _image;
		private bool _isSolid;
		private TileType _type;
		private string _data;

		/// <summary>
		/// More of a public constant, TILE_SIZE represents (in
		/// pixels) how large a tile is (for movement purposes).
		/// </summary>
		/// <value>The (somewhat constant) tile size.</value>
		public static int TILE_SIZE {
			get {
				return 64;
			}
		}

		/// <summary>
		/// Gets or sets the tile's type as a <see cref="TileType"/>.
		/// </summary>
		/// <value>The type.</value>
		public TileType Type {
			get {
				return _type;
			}
			set {
				_type = value;
			}
		}

		/// <summary>
		/// A string (of expressions) that represents the tile. Typically
		/// between 3 and 4 expressions represent the tile. The 4 expressions
		/// are as follows.
		/// </summary>
		/// <list type="bullet">
		/// <item>TileType (as int)*</item>
		/// <item>Is Solid (as int)*</item>
		/// </list>
		/// <value>The data.</value>
		public string Data {
			get {
				return _data;
			}
			set {
				_data = value;
				LoadFromExp (value);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this tile is solid.
		/// </summary>
		/// <value><c>true</c> if this instance is solid; otherwise, <c>false</c>.</value>
		public bool IsSolid {
			get {
				return _isSolid;
			}
		}

		public static Bitmap LoadTileImage(TileType type) {
			Bitmap image;
			image = SwinGame.LoadBitmap (type.ToString ("G").ToLower () + ".png");
			Console.WriteLine (type.ToString("G").ToLower()+".png");
			if (SwinGame.BitmapHeight (image) != TILE_SIZE)
			{
				float scale = TILE_SIZE / SwinGame.BitmapHeight (image);
				image = SwinGame.RotateScaleBitmap(image, 0.0f, scale);
			}
			return image;
		}

		/// <summary>
		/// Draw the current Tile at the specified x and y.'
		/// Is virtual, override for other uses.
		/// </summary>
		/// <param name="x">The x coordinate to draw at (not divided by 64).</param>
		/// <param name="y">The y coordinate to draw at (not divided by 64).</param>
		public virtual void Draw (int x, int y)
		{
			SwinGame.DrawBitmap (_image, x, y);
		}

		/// <summary>
		/// Loads the tile from a string of expressions. The expressions
		/// are as follows.
		/// </summary>
		/// <list type="bullet">
		/// <item>TileType (as int)*</item>
		/// <item>Is Solid (as int)*</item>
		/// </list>
		/// <param name="data">Data.</param>
		public virtual void LoadFromExp (string data)
		{
			string[] dataArr = new string[2];
			dataArr = data.Split (new Char[] { ':' });
			_type = (TileType) Convert.ToInt32(dataArr[0]);
			_isSolid = Convert.ToBoolean(Convert.ToInt32(dataArr [1]));	
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MyGame.Tile"/> class
		/// loading using the <see cref="expression"/> 
		/// </summary>
		/// <param name="expression">Expression.</param>
		public Tile (String expression) {
			_data = expression;
			LoadFromExp (expression);
			_image = LoadTileImage (_type);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MyGame.Tile"/> class
		/// using the given <see cref="TileType"/> .
		/// </summary>
		/// <param name="type">Type.</param>
		public Tile (TileType type) {
			_type = type;
			_isSolid = false;
			_image = LoadTileImage (_type);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MyGame.Tile"/> class
		/// using the <see cref="TileType"/> and the given <see cref="Boolean"/> <see cref="solid"/>   .
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="solid">If set to <c>true</c> solid.</param>
		public Tile (TileType type, bool solid) : this(type) {
			_isSolid = solid;
		}
	}


}

