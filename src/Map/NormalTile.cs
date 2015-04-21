using System;

namespace MyGame
{
	public class NormalTile : Tile
	{
		public NormalTile (TileType type) : base(type) { }

		public NormalTile (string expression) : base(expression) { }

		public NormalTile (TileType type, bool solid) : base(type, solid) { }
	}
}

