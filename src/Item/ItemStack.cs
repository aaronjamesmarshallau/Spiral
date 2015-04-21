using System;
using SwinGameSDK;
using Color = System.Drawing.Color;
using System.Collections.Generic;

namespace MyGame
{
	public class ItemStack
	{
		private Item _item;
		private int _count;

		/// <summary>
		/// Returns the item for this ItemStack. Read-only.
		/// </summary>
		/// <value>The item.</value>
		public Item Item {
			get {
				return _item;
			}
		}

		/// <summary>
		/// Gets or sets the number of Items in the ItemStack.
		/// </summary>
		/// <value>The count.</value>
		public int Count {
			get {
				return _count;
			}
			set {
				_count = value;
			}
		}

		private void Draw() {

		}

		public ItemStack(Item item, int count) {
			_item = item;
			_count = count;
		}
	}


}

