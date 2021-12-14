using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public class FieldInRange
    {
        public Vector2 Position { get; set; }

        public enum Type { Empty, HasEnemyPiece}

        public Type type { get; set; }

        public FieldInRange(Vector2 position)
        {
            Position = position;
            type = Type.Empty;
        }

        public FieldInRange(Vector2 position, Type type)
        {
            Position = position;
            this.type = type;
        }
    }
}
