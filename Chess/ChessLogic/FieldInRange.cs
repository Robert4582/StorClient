using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public class FieldInRange
    {
        public Point Position { get; set; }

        public enum Type { Empty, HasEnemyPiece}

        public Type type { get; set; }

        public FieldInRange(Point position)
        {
            Position = position;
            type = Type.Empty;
        }

        public FieldInRange(Point position, Type type)
        {
            Position = position;
            this.type = type;
        }
    }
}
