using NUnit.Framework;
using System;

namespace MyGame
{
	[TestFixture ()]
	public class GameStateTests
	{
		[Test ()]
		public void TestAddState ()
		{
			GameStateManager gsm = new GameStateManager ();
			Assert.AreEqual (null, gsm.CurrentState, "TEST::ADD1");

			GameStateManager.LoadConfig ();

			MenuGameState mgs = new MenuGameState (gsm);

			gsm.AddState (mgs);
			Assert.AreEqual (mgs, gsm.CurrentState, "TEST::ADD2");
		}

		[Test ()]
		public void TestSwapState()
		{
			GameStateManager gsm = new GameStateManager ();

			GameStateManager.LoadConfig ();

			MenuGameState mgs = new MenuGameState (gsm);
			OptionMenuState oms = new OptionMenuState (gsm);

			gsm.AddState (mgs);

			Assert.AreEqual (mgs, gsm.CurrentState, "TEST::SWAP1");

			gsm.SwapState (oms);

			Assert.AreEqual (oms, gsm.CurrentState, "TEST::SWAP2");
		}

		[Test ()]
		public void TestReturn()
		{
			GameStateManager gsm = new GameStateManager ();

			GameStateManager.LoadConfig ();

			MenuGameState mgs = new MenuGameState (gsm);
			OptionMenuState oms = new OptionMenuState (gsm);

			gsm.AddState (mgs);
			gsm.AddState (oms);

			Assert.AreEqual (oms, gsm.CurrentState, "TEST::RETURN1");

			gsm.Return ();

			Assert.AreEqual (mgs, gsm.CurrentState, "TEST::RETURN2");
		}

		[Test ()]
		public void ChangeConfigValue()
		{
			GameStateManager gsm = new GameStateManager ();

			GameStateManager.LoadConfig ();

			Assert.AreEqual (false, GameStateManager.PlayMusic, "TEST::CONFIG1");

			GameStateManager.PlayMusic = !GameStateManager.PlayMusic;

			Assert.AreNotEqual (false, GameStateManager.PlayMusic, "TEST::CONFIG2");

			GameStateManager.PlayMusic = false;
			GameStateManager.SaveConfig ();
		}
	}
}

