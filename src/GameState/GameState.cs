using System;
using System.Collections;

namespace MyGame
{
	public abstract class GameState
	{
		private GameStateManager _GSM;

		public GameStateManager GSM
		{
			get {
				return _GSM;
			}
		}

		public abstract void Returning (GameState returnFrom);

		public abstract void Leaving ();

		public abstract void Entering ();

		public abstract void Draw ();

		public abstract void DrawPaused();

		public abstract void Update();

		public abstract void UpdatePaused ();

		public GameState(GameStateManager gsm) {
			_GSM = gsm;
		}
	}

}

