using System;
using SwinGameSDK;
using Color = System.Drawing.Color;
using System.Collections.Generic;

namespace MyGame
{
	public class Inventory
	{
		private List<ItemStack> _items = new List<ItemStack>();
		private int _size;

		/// <summary>
		/// Gets or sets the <see cref="MyGame.ItemStack"/> at the specified index
		/// in the <see cref="MyGame.Inventory"/>.
		/// </summary>
		/// <param name="i">The index.</param>
		public ItemStack this[int i] {
			get {
				return _items [i];
			}
			set {
				_items[i] = value;
			}
		}

		/// <summary>
		/// Put the specified <see cref="MyGame.ItemStack"/> in the <see cref="MyGame.Inventory"/>.
		/// </summary>
		/// <param name="IS">ItemStack.</param>
		public void Put(ItemStack IS) {
			foreach (ItemStack i in _items)
			{
				if (i.Item == IS.Item)
				{
					i.Count += IS.Count;
					return;
				}
			}
			if (_items.Count < _size)
			{
				_items.Add (IS);
			}
		}

		public void Draw(int x,int y,int rowCount, int columnCount) {
			for (int iy = 0; iy < columnCount; iy++)
			{
				for (int ix = 0; ix < rowCount; ix++)
				{
					SwinGame.FillRectangle (SwinGame.RGBColor (128, 128, 128), x + (ix * 64) + (ix * 10), y + (iy * 64) + (iy * 10), 64, 64);
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MyGame.Inventory"/> class with
		/// the specified size.
		/// </summary>
		/// <param name="size">Size.</param>
		public Inventory (int size) {
			_size = size;
		}
	}

}

