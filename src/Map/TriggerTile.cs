using System;
using System.IO;
using SwinGameSDK;

namespace MyGame
{
	public class TriggerTile : Tile
	{
		private Map _triggerMap;

		/// <summary>
		/// Gets or sets the trigger map; the map that is loaded when
		/// the tile is entered.
		/// <remarks>The player's <see cref="MapX"/> and <see cref="MapY"/> must be the same as this
		/// tile's <see cref="MapX"/> and <see cref="MapY"/></remarks>
		/// </summary>
		/// <value>The trigger map.</value>
		public Map TriggerMap {
			get {
				return _triggerMap;
			}
			set {
				_triggerMap = value;
			}
		}

		public TriggerTile(String expression) : base(expression) {
			_triggerMap = new Map(Data.Split (new Char[] { ':' })[2]);
		}

		public TriggerTile (TileType type, bool solid, string triggerPath) : base(type, solid) {
			_triggerMap = new Map (triggerPath);
		}

		public TriggerTile (TileType type, string triggerPath) : this(type, false, triggerPath) {
		}
	}
}

