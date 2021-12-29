using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    static class Chess
    {
        static public List<Piece> Pieces { get; set; }

        static public Dictionary<string, Rectangle> SourceRectangle { get; private set; }

        static public Point Margin { get; private set; }

        static public Point SizeOfField { get; private set; }

        static private Point SizeOfFieldNoborder;

        static public Piece HoverPiece { get; private set; }

        static public Piece SelectedPiece { get; private set; }

        static public int CurrentPlayerIndex { get; private set; }
        static public Texture2D ChessBoardTexture { get; set; }
        static public Texture2D ChessPiecesTexture { get; set; }
        static public Texture2D SelectedFieldTexture1 { get; set; }
        static public Texture2D SelectedFieldTexture2 { get; set; }
        static public Texture2D FieldInRangeTexture1 { get; set; }
        static public Texture2D FieldInRangeTexture2 { get; set; }

        static private float hoverTimeCounter;
        static private Piece previousHoverPiece;

        // The duration of the hovering over the chess piece effect (in seconds).
        static private float hoverDuration;

        // value 0 black texture
        // value 0.5 darker colored texture
        // value 1 original color to the texture
        static private float hover_minColorMultiplier;

        /// <summary>
        /// Initializes various variables such as the chess pieces.
        /// </summary>
        static public void Initialize()
        {
            CurrentPlayerIndex = 1;
            Margin = new Point(41, 41);
            SizeOfFieldNoborder = new Point(80, 80);
            SizeOfField = new Point(82, 82);
            hoverDuration = 0.25f; // seconds
            hover_minColorMultiplier = 0.1f;

            // Fill up the source dictionary
            SourceRectangle = new Dictionary<string, Rectangle>(6);
            SourceRectangle["King"] = new Rectangle(0, 0, 64, 64);
            SourceRectangle["Queen"] = new Rectangle(64, 0, 64, 64);
            SourceRectangle["Castle"] = new Rectangle(128, 0, 64, 64);
            SourceRectangle["Knight"] = new Rectangle(192, 0, 64, 64);
            SourceRectangle["Bishop"] = new Rectangle(256, 0, 64, 64);
            SourceRectangle["Pawn"] = new Rectangle(320, 0, 64, 64);

            // Initialize the chess pieces positions on the chess board
            Pieces = new List<Piece>(32);

            // Player 1 has white chess pieces and is on the bottom side
            // Player 2 has black chess pieces and is on the top side

            // Setup the chess board...
            // Create the chess pieces and add them to the chess board
            for (int playerIndex = 1; playerIndex <= 2; playerIndex++)  //Foreach Player
            {
                int row, pawnsRow;

                if (playerIndex == 1)
                { row = 8; pawnsRow = 7; }
                else
                { row = 1; pawnsRow = 2; }

                Pieces.Add(new Castle(new Point(1, row), playerIndex));
                Pieces.Add(new Knight(new Point(2, row), playerIndex));
                Pieces.Add(new Bishop(new Point(3, row), playerIndex));
                Pieces.Add(new Queen(new Point(4, row), playerIndex));
                Pieces.Add(new King(new Point(5, row), playerIndex));
                Pieces.Add(new Bishop(new Point(6, row), playerIndex));
                Pieces.Add(new Knight(new Point(7, row), playerIndex));
                Pieces.Add(new Castle(new Point(8, row), playerIndex));
                for (int i = 1; i <= 8; i++)
                {
                    Pieces.Add(new Pawn(new Point(i, pawnsRow), playerIndex));
                }
            }
        }

        /// <summary>
        /// Updates the chess game logic.
        /// </summary>
        static public void Update()
        {
            // Reset and re-calculate HoverPiece
            HoverPiece = null;
            Point hoverBlockPosition = Util.CoordinatesToPosition(InputHandler.Position);
            foreach (Piece piece in Pieces)
            {
                if (piece.Position == hoverBlockPosition)
                {
                    HoverPiece = piece;
                    break;
                }
            }

            // Reset hoverTimeCounter
            if (previousHoverPiece != HoverPiece)
            {
                hoverTimeCounter = 0;
            }

            if (InputHandler.Released)
            {
                // If we haven't selected a chess piece
                if (SelectedPiece == null)
                {
                    // If the user selected a chess block which contains an allied piece
                    if (HoverPiece != null && HoverPiece.PlayerIndex == CurrentPlayerIndex)
                    {
                        // Update the selected piece, and find its reachable blocks
                        SelectedPiece = HoverPiece;
                        SelectedPiece.FindFieldInRange();
                    }
                }
                else // If we have selected a chess piece
                {
                    // True if the user clicked on a reachable block
                    bool clickedOnFieldInRange = false;

                    foreach (FieldInRange block in SelectedPiece.FieldsInRange)
                    {
                        // Check if the user selected a reachable block (from the selected chess piece) 
                        if (block.Position == hoverBlockPosition)
                        {
                            clickedOnFieldInRange = true;

                            // Move the selected piece
                            SelectedPiece.Position = hoverBlockPosition;
                            // Unselect the piece
                            SelectedPiece = null;
                            // Change the current player
                            if (CurrentPlayerIndex == 1)
                                CurrentPlayerIndex = 2;
                            else
                                CurrentPlayerIndex = 1;

                            // Remove the enemy piece from the pieces list
                            if (HoverPiece != null)
                                Pieces.Remove(HoverPiece);

                            break;
                        }
                    }

                    // If the user didn't select a reachable block
                    if (!clickedOnFieldInRange)
                    {
                        // If the user either selected an empty chess block or an enemy chess piece
                        if (HoverPiece == null || HoverPiece.PlayerIndex != CurrentPlayerIndex)
                        {
                            SelectedPiece = null;
                        }
                        else // If the user selected an ally chess piece
                        {
                            // Update the selected piece and find its reachable blocks
                            SelectedPiece = HoverPiece;
                            SelectedPiece.FindFieldInRange();
                        }
                    }
                }
            }

            previousHoverPiece = HoverPiece;
        }

        static public void DrawChessBoard(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ChessBoardTexture, new Rectangle(0, 0, ChessBoardTexture.Width, ChessBoardTexture.Height), Color.White);
        }

        /// <summary>
        /// Draws the chess pieces.
        /// </summary>
        static public void DrawChessPieces(SpriteBatch spriteBatch)
        {
            foreach (Piece piece in Pieces)
            {
                // Get the position of the chess piece in screen coordinates
                Vector2 position = Util.PositionToCoordinates(piece.Position);

                // Get the proper sourceRectangle according to the player's index
                // Player 1 has white chess pieces and is on the bottom side
                // Player 2 has black chess pieces and is on the top side
                Rectangle sourceRectangle = SourceRectangle[piece.GetType().Name];
                if (piece.PlayerIndex == 1)
                { sourceRectangle.Y += sourceRectangle.Height; }

                Vector2 origin = sourceRectangle.GetHalfSize() - SizeOfField.ToVector2().GetHalfSize();

                spriteBatch.Draw(ChessPiecesTexture, position, sourceRectangle, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
            }
        }

        static public void DrawHoverPiece(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (SelectedPiece != null || HoverPiece == null || HoverPiece.PlayerIndex != CurrentPlayerIndex)
            { return; }

            if (hoverTimeCounter < hoverDuration)
            { hoverTimeCounter += (float)gameTime.ElapsedGameTime.TotalSeconds; }

            float colorMultiplier = (hoverTimeCounter / hoverDuration) * (1 - hover_minColorMultiplier) + hover_minColorMultiplier;

            Vector2 position = Util.PositionToCoordinates(HoverPiece.Position);

            Texture2D texture;
            if (CurrentPlayerIndex == 1)
            { texture = SelectedFieldTexture1; }
            else
            { texture = SelectedFieldTexture2; }
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, SizeOfFieldNoborder.X, SizeOfFieldNoborder.Y), Color.White * colorMultiplier);
        }

        static public void DrawSelectedPiece(SpriteBatch spriteBatch)
        {
            if (SelectedPiece == null)
                return;

            // Get the position of the selected piece, to screen coordinates
            Vector2 position = Util.PositionToCoordinates(SelectedPiece.Position);

            Texture2D texture;
            if (CurrentPlayerIndex == 1)
            { texture = SelectedFieldTexture1; }
            else
            { texture = SelectedFieldTexture2; }
        }

        static public void DrawFieldsInRange(SpriteBatch spriteBatch)
        {
            if (SelectedPiece == null)
                return;

            foreach (FieldInRange block in SelectedPiece.FieldsInRange)
            {
                // Get the position of the reachable block, to screen coordinates
                Vector2 position = Util.PositionToCoordinates(block.Position);

                Texture2D texture;
                if (CurrentPlayerIndex == 1)
                    texture = FieldInRangeTexture1;
                else
                    texture = FieldInRangeTexture2;
                spriteBatch.Draw(texture, position, new Rectangle(0, 0, SizeOfFieldNoborder.X, SizeOfFieldNoborder.Y), Color.White);
            }

        }
    }
}
