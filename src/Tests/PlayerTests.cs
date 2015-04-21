using NUnit.Framework;
using System;

namespace MyGame
{
	[TestFixture ()]
	public class PlayerTests
	{
		[Test ()]
		public void PlayerInitTest () //I use a null map, because map loading doesn't work in NUnit...
		{
			Player p = new Player ("Aaron", null, 10, 10);

			Assert.AreEqual ("Aaron", p.Name, "TEST::PlayerName1");
			Assert.AreEqual (10, p.MapX, "TEST::PlayerX");
			Assert.AreEqual (10, p.MapY, "TEST::PlayerY");
		}

		[Test ()]
		public void PlayerIdleTest()
		{
			Player p = new Player ("Aaron", null, 10, 10);

			Assert.AreEqual (false, p.Moving, "TEST::MOVING1");

			p.ToMove ();

			Assert.AreEqual (true, p.Moving, "TEST::MOVING2");

			p.ToIdle ();

			Assert.AreEqual (false, p.Moving, "TEST::MOVING3");
		}

		/*[Test ()]
		public void PlayerMoveTest()
		{
			Player p = new Player ("Aaron", null, 10, 10);

			p.Direction = Direction.Up;
			p.ToMove ();
			Assert.AreEqual (10, p.MapY);
			p.Update ();
			Assert.AreEqual (9, p.MapY);
		} Because map loading doesn't work in NUnit, this can't be tested.*/
	}
}

