using System;
using SwinGameSDK;

namespace MyGame
{
	public class OptionMenuState : GameState
	{
		public override void Returning (GameState returnFrom) {

		}

		public override void Entering() {

		}

		public override void Leaving() {

		}

		public override void Draw() {

		}

		public override void DrawPaused() {

		}

		public override void Update() {
			if (SwinGame.KeyTyped (KeyCode.vk_ESCAPE))
			{
				GSM.Return ();
			}
		}

		public override void UpdatePaused() {

		}

		public OptionMenuState (GameStateManager gsm) : base(gsm)
		{
			
		}
	}
}

