using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public class King : Piece
    {
        public King(Vector2 position, int playerIndex) : base(position, playerIndex) { }

        public override void FindFieldInRange()
        {
            FieldsInRange = new List<FieldInRange>(8);
            List<Vector2> possiblePositions = new List<Vector2>(8);

            // Left column of blocks
            possiblePositions.Add(new Vector2(Position.X - 1, Position.Y - 1));
            possiblePositions.Add(new Vector2(Position.X - 1, Position.Y));
            possiblePositions.Add(new Vector2(Position.X - 1, Position.Y + 1));
            // Middle column of blocks
            possiblePositions.Add(new Vector2(Position.X, Position.Y - 1));
            possiblePositions.Add(new Vector2(Position.X, Position.Y + 1));
            // Right column of blocks
            possiblePositions.Add(new Vector2(Position.X + 1, Position.Y - 1));
            possiblePositions.Add(new Vector2(Position.X + 1, Position.Y));
            possiblePositions.Add(new Vector2(Position.X + 1, Position.Y + 1));

            foreach (Vector2 pos in possiblePositions)
            { 
                AddFieldInRange(pos); 
            }        
        }
    }

    public class Queen : Piece
    {
        public Queen(Vector2 position, int playerIndex) : base(position, playerIndex) { }

        public override void FindFieldInRange()
        {
            FieldsInRange = new List<FieldInRange>(27);

            FindFieldsInRange_Horizontal();
            FindFieldsInRange_Vertical();
            FindFieldsInRange_Diagonal();
        }
    }

    public class Castle : Piece
    {
        public Castle(Vector2 position, int playerIndex) : base(position, playerIndex) { }

        public override void FindFieldInRange()
        {
            FieldsInRange = new List<FieldInRange>(14);

            FindFieldsInRange_Horizontal();
            FindFieldsInRange_Vertical();
        }
    }

    public class Bishop : Piece
    {
        public Bishop(Vector2 position, int playerIndex) : base(position, playerIndex) { }

        public override void FindFieldInRange()
        {
            FieldsInRange = new List<FieldInRange>(13);

            FindFieldsInRange_Diagonal();
        }
    }

    public class Knight : Piece
    {
        public Knight(Vector2 position, int playerIndex) : base(position, playerIndex) { }

        public override void FindFieldInRange()
        {
            FieldsInRange = new List<FieldInRange>(8);
            List<Vector2> possiblePositions = new List<Vector2>(8);

            // Top-left quarter
            possiblePositions.Add(new Vector2(Position.X - 2, Position.Y - 1));
            possiblePositions.Add(new Vector2(Position.X - 1, Position.Y - 2));
            // Top-right quarter
            possiblePositions.Add(new Vector2(Position.X + 1, Position.Y - 2));
            possiblePositions.Add(new Vector2(Position.X + 2, Position.Y - 1));
            // Bottom-right quarter
            possiblePositions.Add(new Vector2(Position.X + 2, Position.Y + 1));
            possiblePositions.Add(new Vector2(Position.X + 1, Position.Y + 2));
            // Bottom-left quarter
            possiblePositions.Add(new Vector2(Position.X - 1, Position.Y + 2));
            possiblePositions.Add(new Vector2(Position.X - 2, Position.Y + 1));

            foreach (Vector2 pos in possiblePositions)
            {
                AddFieldInRange(pos);
            }
        }
    }

    public class Pawn : Piece
    {
        public Pawn(Vector2 position, int playerIndex) : base(position, playerIndex) { }

        public override void FindFieldInRange()
        {
            FieldsInRange = new List<FieldInRange>(3);
            List<Vector2> possiblePositions = new List<Vector2>(3);
            Vector2 topBlockPosition;

            // Top direction
            if (PlayerIndex == 1)
            {
                // Top-left block
                possiblePositions.Add(new Vector2(Position.X - 1, Position.Y - 1));
                // Top block
                topBlockPosition = new Vector2(Position.X, Position.Y - 1);
                // Top-right block
                possiblePositions.Add(new Vector2(Position.X + 1, Position.Y - 1));
            }
            else // Bottom direction
            {
                // Top-left block
                possiblePositions.Add(new Vector2(Position.X - 1, Position.Y + 1));
                // Top block
                topBlockPosition = new Vector2(Position.X, Position.Y + 1);
                // Top-right block
                possiblePositions.Add(new Vector2(Position.X + 1, Position.Y + 1));
            }

            foreach (Vector2 pos in possiblePositions)
            {
                AddFieldInRange(pos);
            }
            AddFieldInRange_Pawn(topBlockPosition);
        }
    }
}
