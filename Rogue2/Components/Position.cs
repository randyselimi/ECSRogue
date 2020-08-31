using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    //Refactor. have a isGlobalPositionflag
    /// <summary>
    /// Position component used for free-floating game objects not bound to the tile system, such as cameras and ui elements
    /// Coordinate corresponds to global
    /// </summary>
    class Position : Component
    {
        public Vector2 position;
        public Position()
        {

        }
        public Position(Vector2 position)
        {
            this.position = position;
        }
        public Position(Position position)
        {
            this.position = position.position;
        }
        public override object Clone()
        {
            Position clone = new Position(this);
            return clone;
        }
    }
}
