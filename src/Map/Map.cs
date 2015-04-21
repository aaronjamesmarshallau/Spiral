using System;
using System.IO;
using SwinGameSDK;

namespace MyGame
{
	public class Map
	{
		private int _width;
		private int _height;
		private int _initialX = 0;
		private int _initialY = 0;
		private Tile[,] _tiles;
		private string _path;
		private string _name;
		private string _region;
		private Music _music;

		/// <summary>
		/// Gets or sets the music.
		/// </summary>
		/// <value>The music.</value>
		public Music Music {
			get {
				return _music;
			}
			set {
				_music = value;
			}
		}

		/// <summary>
		/// Gets the Player Tile Offset in the X direction
		/// </summary>
		/// <value>The Player Tile Offset in the X direction.</value>
		public static int PLAYER_TILE_OFFSET_X {
			get {
				return 6;
			}
		}

		/// <summary>
		/// Gets the Player Tile Offset in the Y direction
		/// </summary>
		/// <value>The Player Tile Offset in the Y direction.</value>
		public static int PLAYER_TILE_OFFSET_Y {
			get {
				return 4;
			}
		}

		/// <summary>
		/// Gets the Map Tile Render Count in the X direction.
		/// </summary>
		/// <value>The Map Tile Render Count in the X direction.</value>
		public static int MAP_TILE_RENDER_COUNT_X {
			get {
				return 14;
			}
		}

		/// <summary>
		/// Gets the Map Tile Render Count in the Y direction.
		/// </summary>
		/// <value>The Map Tile Render Count in the Y direction.</value>
		public static int MAP_TILE_RENDER_COUNT_Y {
			get {
				return 11;
			}
		}

		/// <summary>
		/// Gets the width of the map (in tiles).
		/// </summary>
		/// <value>The width.</value>
		public int Width {
			get {
				return _width;
			}
		}

		/// <summary>
		/// Gets the height of the map (in tiles).
		/// </summary>
		/// <value>The height.</value>
		public int Height {
			get {
				return _height;
			}
		}

		/// <summary>
		/// Gets the initial map x.
		/// </summary>
		/// <value>The initial map x.</value>
		public int InitialX {
			get {
				return _initialX;
			}
		}

		/// <summary>
		/// Gets the initial map y.
		/// </summary>
		/// <value>The initial map y.</value>
		public int InitialY {
			get {
				return _initialY;
			}
		}

		/// <summary>
		/// Gets the name of the map.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get {
				return _name;
			}
		}

		/// <summary>
		/// Gets the region that this map belongs to.
		/// </summary>
		/// <value>The region.</value>
		public string Region {
			get {
				return _region;
			}
		}

		public bool IsSolidAt(int mapX, int mapY) {
			return _tiles [mapX, mapY].IsSolid;
		}

		public void Update() {

		}

		public void Draw(int playerMapX, int playerMapY, int playerMapOffsetX, int playerMapOffsetY) {
			int drawingX;
			int drawingY;
			Tile dumby = new NormalTile ((TileType)3);
			for (int x = -PLAYER_TILE_OFFSET_X ; x < MAP_TILE_RENDER_COUNT_X - PLAYER_TILE_OFFSET_X; x++)
			{
				drawingX = playerMapX + x;
				for (int y = -PLAYER_TILE_OFFSET_Y; y < MAP_TILE_RENDER_COUNT_Y - PLAYER_TILE_OFFSET_Y; y++)
				{
					drawingY = playerMapY + y;
					//Console.WriteLine ("Drawing tile ({0}, {1})!", drawingX, drawingY);
					if ((drawingX > 0) && (drawingY > 0) && (drawingX < Width) && (drawingY < Height))
					{
						_tiles [drawingX, drawingY].Draw ((int) (x + PLAYER_TILE_OFFSET_X) * 64 - playerMapOffsetX, (int)(y + PLAYER_TILE_OFFSET_Y) * 64 - playerMapOffsetY);
					}
					else
					{
						dumby.Draw ((int) (x + PLAYER_TILE_OFFSET_X) * 64 - playerMapOffsetX, (int) (y + PLAYER_TILE_OFFSET_Y) * 64 - playerMapOffsetY);
					}

				}
			}
		}

		private int GetWidthFromFile() {
			int result = 0;
			StreamReader sr = new StreamReader (_path);
			string line;
			string[] tileInfo;
			for (int i = 0; i < 4; i++)
			{
				sr.ReadLine ();
			}

			try {
				line = sr.ReadLine ();
				tileInfo = line.Split (new Char[1] {','});
				foreach (String s in tileInfo) {
					string[] temp = s.Split(new Char[1] {':'});
					result += Convert.ToInt32(temp[1]);
				}
			} catch (Exception e) {
				Console.WriteLine ("An error occurred: {0}", e.Message);
			} finally {
				sr.Close ();
				sr.Dispose ();
			}
			return result;
		}

		private int GetHeightFromFile() {
			StreamReader sr = new StreamReader (_path);
			int i = 0;
			while (!sr.EndOfStream)
			{
				sr.ReadLine ();
				i++;
			}
			i -= 4; //Removes headers
			sr.Close ();
			sr.Dispose ();
			return i;
		}

		public void Load() {
			StreamReader sr = new StreamReader (_path);
			string line;
			string[] lineArray = new string[_width];
			string[] subArray = new string[4];
			int count = 0;

			_name = sr.ReadLine ();
			_region = sr.ReadLine ();
			_music = SwinGame.LoadMusic (sr.ReadLine ());

			line = sr.ReadLine ();
			line = line.Substring (1, line.Length - 2);
			if (_initialX == 0 && _initialY == 0)
			{
				_initialX = Convert.ToInt32 (line.Split (new Char[1] { ',' }) [0]);
				_initialY = Convert.ToInt32 (line.Split (new Char[1] { ',' }) [1]);
			
				_initialY--;
			}

			for (int x = 0; x < _width; x++)
			{
				line = sr.ReadLine ();
				count = 0;
				lineArray = line.Split (',');
				for (int y = 0; y < _height; y++)
				{
					subArray = lineArray [count].Split (':');
					for (int z = 0; z < Convert.ToInt32 (subArray[1]); z++)
					{
						Console.WriteLine ("Loading tile [{0}, {1}]", x, y + z);
						if(subArray.Length == 3) {
							_tiles [y + z, x] = new NormalTile (subArray [0] + ":" + subArray [2]);
						} else if(subArray.Length == 4) {
							_tiles [y + z, x] = new TriggerTile (subArray [0] + ":" + subArray [2] + ":" + subArray [3]);
						}
						Console.WriteLine ("Successfully loaded tile [{0}, {1}]", x, y + z);
					}
					y += Convert.ToInt32(subArray[1]) - 1;
					count++;
				}

			}
			sr.Close ();
			sr.Dispose ();
		}

		public Map (string fileName, int initialX, int initialY) : this(fileName) {
			_initialX = initialX;
			_initialY = initialY;
		}

		public Map (string fileName) {
			_path = fileName;
			_height = GetHeightFromFile ();
			_width = GetWidthFromFile ();
			_tiles = new Tile[_width, _height];
		}
	}

}

