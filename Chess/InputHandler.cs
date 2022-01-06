using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    static class InputHandler
    {
        static public Vector2 Position { get { return position / ResolutionUtil.Scale; } }
        static public Boolean Released { get; private set; }
        static public Boolean Pressed { get; private set; }
        static private Vector2 position;

        static private MouseState PreviousMouseState;
        static private KeyboardState PreviousKeyboardState;



        /// <summary>
        /// Updates the inputs.
        /// </summary>
        static public void Update(Game game, GraphicsDeviceManager graphics)
        {
            // Reset old states
            Pressed = false;
            Released = false;

            // Get new states
            KeyboardState currentKeyboardState = Keyboard.GetState();
            MouseState currentMouseState = Mouse.GetState();

            // Position
            position = new Vector2(currentMouseState.X, currentMouseState.Y);
            // Pressed
            if (currentMouseState.LeftButton == ButtonState.Pressed)
                Pressed = true;
            // Released
            else if (PreviousMouseState.LeftButton == ButtonState.Pressed)
                Released = true;
            // Esc - exit game
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
                game.Exit();
            // F11 - fullscreen game
            if (PreviousKeyboardState.IsKeyDown(Keys.F11) && !currentKeyboardState.IsKeyDown(Keys.F11))
            {
                if (game.Window.IsBorderless == true)
                {
                    game.Window.Position = new Point((ResolutionUtil.ScreenWidth - ResolutionUtil.GameWidth) / 2, ((ResolutionUtil.ScreenHeight - ResolutionUtil.GameHeight) / 2) - 40);
                    game.Window.IsBorderless = false;
                    graphics.PreferredBackBufferWidth = ResolutionUtil.GameWidth;
                    graphics.PreferredBackBufferHeight = ResolutionUtil.GameHeight;
                    graphics.ApplyChanges();
                }
                else
                {
                    game.Window.Position = new Point(0, 0);
                    game.Window.IsBorderless = true;
                    graphics.PreferredBackBufferWidth = ResolutionUtil.ScreenWidth;
                    graphics.PreferredBackBufferHeight = ResolutionUtil.ScreenHeight;
                    graphics.ApplyChanges();
                }
            }

            PreviousMouseState = currentMouseState;
            PreviousKeyboardState = currentKeyboardState;
        }
    }
}
