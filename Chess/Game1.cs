using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chess
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += delegate { ResolutionUtil.WasResized = true; };
            graphics.PreferredBackBufferWidth = 800; // window's width
            graphics.PreferredBackBufferHeight = 800; // window's height
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ResolutionUtil.Initialize(graphics);
            Chess.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Chess.ChessBoardTexture = Content.Load<Texture2D>("ChessBoard");
            Chess.ChessPiecesTexture = Content.Load<Texture2D>("ChessPieces");

            // Player 1 colors
            Chess.SelectedFieldTexture1 = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            Chess.SelectedFieldTexture1.SetData<Color>(new Color[] { new Color(170, 170, 170) * 0.6f });
            Chess.FieldInRangeTexture1= new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            Chess.FieldInRangeTexture1.SetData<Color>(new Color[] { new Color(170, 170, 170) * 0.6f });
            // Player 2 colors
            Chess.SelectedFieldTexture2 = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            Chess.SelectedFieldTexture2.SetData<Color>(new Color[] { new Color(171, 149, 89) * 0.6f });
            Chess.FieldInRangeTexture2 = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            Chess.FieldInRangeTexture2.SetData<Color>(new Color[] { new Color(171, 149, 89) * 0.6f });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHandler.Update(this, graphics);
            ResolutionUtil.Update(this, graphics);
            Chess.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, ResolutionUtil.ScaleMatrix);
            Chess.DrawChessBoard(spriteBatch);
            Chess.DrawChessPieces(spriteBatch);
            Chess.DrawHoverPiece(spriteBatch, gameTime);
            Chess.DrawSelectedPiece(spriteBatch);
            Chess.DrawFieldsInRange(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
