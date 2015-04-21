using System;
using SwinGameSDK;
using Color = System.Drawing.Color;

namespace MyGame
{
	public class PauseGameState : GameState
	{
		private Button _resume = new Button ("btnResume", "Resume", SwinGame.ScreenWidth () / 2 - 150, SwinGame.ScreenHeight () / 2 - 80, 300, 30);
		private Button _options = new Button ("btnOptions", "Options", SwinGame.ScreenWidth () / 2 - 150, SwinGame.ScreenHeight() / 2 - 40, 300, 30);
		private Button _stats = new Button ("btnStats", "Statistics", SwinGame.ScreenWidth () / 2 - 150, SwinGame.ScreenHeight() / 2, 300, 30);
		private Button _exit = new Button ("btnExit", "Exit", SwinGame.ScreenWidth () / 2 - 150, SwinGame.ScreenHeight() / 2 + 40, 300, 30);

		public override void Returning (GameState returnFrom) {
			Entering ();
		}

		public override void Entering() {

		}

		public override void Leaving() {

		}

		public override void Draw() {
			SwinGame.FillRectangle(Color.FromArgb(127, 0,0,0), 0, 0, SwinGame.ScreenWidth(), SwinGame.ScreenHeight());
			_resume.Draw ();
			_options.Draw ();
			_stats.Draw ();
			_exit.Draw ();
		}

		public override void DrawPaused () {
			Draw ();
		}

		public override void Update() {
			if (SwinGame.KeyTyped (KeyCode.vk_ESCAPE))
			{
				GSM.Return ();
			}
			_resume.Update ();
			_options.Update ();
			_stats.Update ();
			_exit.Update ();
		}

		public override void UpdatePaused() {

		}

		#region Buttons

		private void Resume_Click(object sender, EventArgs e) {
			GSM.Return ();
		}

		private void Options_Click(object sender, EventArgs e) {
			GSM.AddState (new OptionMenuState (GSM));
		}

		private void Stats_Click(object sender, EventArgs e) {
			//GSM.AddState (new StatisticsMenuState (GSM));
		}

		private void Exit_Click(object sender, EventArgs e) {
			GSM.Exit();
		}

		#endregion

		public PauseGameState (GameStateManager gsm) : base(gsm) {
			_resume.ButtonClick += new EventHandler (Resume_Click);
			_options.ButtonClick += new EventHandler (Options_Click);
			_stats.ButtonClick += new EventHandler (Stats_Click);
			_exit.ButtonClick += new EventHandler (Exit_Click);
		}
	}
}

