using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public class King : Piece
    {
        public King(Point position, int playerIndex) : base(position, playerIndex) { }

        public override void FindFieldInRange()
        {
            FieldsInRange = new List<FieldInRange>(8);
            List<Point> possiblePositions = new List<Point>(8);

            // Left column of blocks
            possiblePositions.Add(new Point(Position.X - 1, Position.Y - 1));
            possiblePositions.Add(new Point(Position.X - 1, Position.Y));
            possiblePositions.Add(new Point(Position.X - 1, Position.Y + 1));
            // Middle column of blocks
            possiblePositions.Add(new Point(Position.X, Position.Y - 1));
            possiblePositions.Add(new Point(Position.X, Position.Y + 1));
            // Right column of blocks
            possiblePositions.Add(new Point(Position.X + 1, Position.Y - 1));
            possiblePositions.Add(new Point(Position.X + 1, Position.Y));
            possiblePositions.Add(new Point(Position.X + 1, Position.Y + 1));

            foreach (Point pos in possiblePositions)
            { 
                AddFieldInRange(pos); 
            }        
        }
    }

    public class Queen : Piece
    {
        public Queen(Point position, int playerIndex) : base(position, playerIndex) { }

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
        public Castle(Point position, int playerIndex) : base(position, playerIndex) { }

        public override void FindFieldInRange()
        {
            FieldsInRange = new List<FieldInRange>(14);

            FindFieldsInRange_Horizontal();
            FindFieldsInRange_Vertical();
        }
    }

    public class Bishop : Piece
    {
        public Bishop(Point position, int playerIndex) : base(position, playerIndex) { }

        public override void FindFieldInRange()
        {
            FieldsInRange = new List<FieldInRange>(13);

            FindFieldsInRange_Diagonal();
        }
    }

    public class Knight : Piece
    {
        public Knight(Point position, int playerIndex) : base(position, playerIndex) { }

        public override void FindFieldInRange()
        {
            FieldsInRange = new List<FieldInRange>(8);
            List<Point> possiblePositions = new List<Point>(8);

            // Top-left quarter
            possiblePositions.Add(new Point(Position.X - 2, Position.Y - 1));
            possiblePositions.Add(new Point(Position.X - 1, Position.Y - 2));
            // Top-right quarter
            possiblePositions.Add(new Point(Position.X + 1, Position.Y - 2));
            possiblePositions.Add(new Point(Position.X + 2, Position.Y - 1));
            // Bottom-right quarter
            possiblePositions.Add(new Point(Position.X + 2, Position.Y + 1));
            possiblePositions.Add(new Point(Position.X + 1, Position.Y + 2));
            // Bottom-left quarter
            possiblePositions.Add(new Point(Position.X - 1, Position.Y + 2));
            possiblePositions.Add(new Point(Position.X - 2, Position.Y + 1));

            foreach (Point pos in possiblePositions)
            {
                AddFieldInRange(pos);
            }
        }
    }

    public class Pawn : Piece
    {
        public Pawn(Point position, int playerIndex) : base(position, playerIndex) { }

        public override void FindFieldInRange()
        {
            FieldsInRange = new List<FieldInRange>(3);
            List<Point> possiblePositions = new List<Point>(3);
            Point topBlockPosition;

            // Top direction
            if (PlayerIndex == 1)
            {
                // Top-left block
                possiblePositions.Add(new Point(Position.X - 1, Position.Y - 1));
                // Top block
                topBlockPosition = new Point(Position.X, Position.Y - 1);
                // Top-right block
                possiblePositions.Add(new Point(Position.X + 1, Position.Y - 1));
            }
            else // Bottom direction
            {
                // Top-left block
                possiblePositions.Add(new Point(Position.X - 1, Position.Y + 1));
                // Top block
                topBlockPosition = new Point(Position.X, Position.Y + 1);
                // Top-right block
                possiblePositions.Add(new Point(Position.X + 1, Position.Y + 1));
            }

            foreach (Point pos in possiblePositions)
            {
                AddFieldInRange(pos);
            }
            AddFieldInRange_Pawn(topBlockPosition);
        }
    }
}
