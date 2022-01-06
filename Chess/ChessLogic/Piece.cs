using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public abstract class Piece
    {
        public Point Position { get; set; }

        public int PlayerIndex { get; set; }

        public List<FieldInRange> FieldsInRange { get; set; }

        abstract public void FindFieldInRange();

        protected Piece(Point position, int playerIndex)
        {
            Position = position;
            PlayerIndex = playerIndex;
        }

        protected bool AddFieldInRange(Point pos)
        {
            bool fieldIsEmpty = true;
            bool fieldIsOutOfBounds = true;

            if (pos.X >= 1 && pos.Y >= 1 && pos.X <= 8 && pos.Y <= 8)
            {
                fieldIsOutOfBounds = false;

                foreach (Piece piece in Chess.Pieces)
                {
                    if (pos == piece.Position)
                    {
                        if (PlayerIndex != piece.PlayerIndex)
                        {
                            FieldsInRange.Add(new FieldInRange(pos, FieldInRange.Type.HasEnemyPiece));
                        }

                        fieldIsEmpty = false;
                        break;
                    }
                }
                if (fieldIsEmpty)
                {
                    FieldsInRange.Add(new FieldInRange(pos));
                }
            }
            return !fieldIsEmpty || fieldIsOutOfBounds;
        }

        protected void AddFieldInRange_Pawn(Point pos)
        {
            bool fieldIsEmpty = true;

            if (pos.X >= 1 && pos.Y >= 1 && pos.X <= 8 && pos.Y <= 8)
            {
                foreach (Piece piece in Chess.Pieces)
                {
                    if (pos == piece.Position)
                    {
                        fieldIsEmpty = false;
                        break;
                    }
                }

                if (fieldIsEmpty)
                {
                    FieldsInRange.Add(new FieldInRange(pos));
                }
            }
        }

        protected void FindFieldsInRange_Horizontal()
        {
            Point pos = Position;
            for (int i = 0; i < 7; i++)
            {
                //Right
                pos.X += 1;
                if (AddFieldInRange(pos))
                {
                    break;
                }
            }

            pos = Position;
            for (int i = 0; i < 7; i++)
            {
                //Left
                pos.X -= 1;
                if (AddFieldInRange(pos))
                {
                    break;
                }
            }
        }

        protected void FindFieldsInRange_Vertical()
        {
            Point pos = Position;
            for (int i = 0; i < 7; i++)
            {
                //Down
                pos.Y+= 1;
                if (AddFieldInRange(pos))
                {
                    break;
                }
            }

            pos = Position;
            for (int i = 0; i < 7; i++)
            {
                //Up
                pos.Y -= 1;
                if (AddFieldInRange(pos))
                {
                    break;
                }
            }
        }

        protected void FindFieldsInRange_Diagonal()
        {
            //Up-right direction
            Point pos = Position;
            for (int i = 0; i < 7; i++)
            {
                pos += new Point(1, -1);
                if (AddFieldInRange(pos))
                {
                    break;
                }
            }

            //Up-left direction
            pos = Position;
            for (int i = 0; i < 7; i++)
            {
                pos += new Point(-1, -1);
                if (AddFieldInRange(pos))
                {
                    break;
                }
            }

            //Down-right direction
            pos = Position;
            for (int i = 0; i < 7; i++)
            {
                pos += new Point(1, 1);
                if (AddFieldInRange(pos))
                {
                    break;
                }
            }

            //Down-left direction
            pos = Position;
            for (int i = 0; i < 7; i++)
            {
                pos += new Point(-1, 1);
                if (AddFieldInRange(pos))
                { 
                    break; 
                }
            }
        }
    }
}
