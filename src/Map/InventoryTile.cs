using System;

namespace MyGame
{
	public class InventoryTile : Tile
	{
		public InventoryTile (TileType type) : base(type) { }

		public InventoryTile (string expression) : base(expression) { }

		public InventoryTile (TileType type, bool solid) : base(type, solid) { }
	}
}

