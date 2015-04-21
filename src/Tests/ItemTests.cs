using NUnit.Framework;
using System;

namespace MyGame
{
	[TestFixture ()]
	public class ItemTests
	{
		[Test ()]
		public void WeaponCreateTest ()
		{
			Weapon tWep = new Weapon ("Glowing Sword of Awesome", "An awesome ultra fantastic fantasmo sword of amazing stuff");

			Assert.AreEqual ("Glowing Sword of Awesome", tWep.Name);
			Assert.AreEqual ("An awesome ultra fantastic fantasmo sword of amazing stuff", tWep.Description);
		}

		[Test ()]
		public void ItemStackTest ()
		{
			Weapon tWep = new Weapon ("Glowing Sword of Awesome", "An awesome ultra fantastic fantasmo sword of amazing stuff");
			ItemStack its = new ItemStack (tWep, 10);

			Assert.AreEqual (tWep, its.Item);

			Assert.AreEqual (10, its.Count);

			its.Count--;

			Assert.AreEqual (9, its.Count);
		}

		[Test ()]
		public void InventoryPutTest () 
		{
			Weapon tWep = new Weapon ("Glowing Sword of Awesome", "description");
			Inventory inv = new Inventory (16);
			ItemStack its = new ItemStack (tWep, 1);

			inv.Put (its);

			Assert.AreEqual(inv[0], its);
		}
	}
}

