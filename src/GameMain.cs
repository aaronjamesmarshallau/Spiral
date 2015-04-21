using System;
using System.IO;
using System.Reflection;
using SwinGameSDK;
using Color = System.Drawing.Color;

namespace MyGame
{
    public class GameMain
    {
        public static void Main()
        {
			GameStateManager gsm = new GameStateManager ();
			GameStateManager.LoadConfig ();
			GameStateManager.LoadControls ();
            //Start the audio system so sound can be played
            SwinGame.OpenAudio();
            
			//Open Graphics Window
            SwinGame.OpenGraphicsWindow("Spiral", 800, 600);

            //Run the game loop
			gsm.AddState (new MenuGameState (gsm));
			gsm.Start ();
            
            //End the audio
            SwinGame.CloseAudio();

            GameStateManager.SaveConfig ();
			GameStateManager.SaveControls ();
            
            //Close any resources we were using
            SwinGame.ReleaseAllResources();
        }
    }
}