using System;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ECSRogue.Components
{
    //Refactor. have a isGlobalPositionflag
    /// <summary>
    ///     Position component used for free-floating game objects not bound to the tile system, such as cameras and ui
    ///     elements
    ///     Coordinate corresponds to global
    /// </summary>
    public class Position : Component
    {
        private Vector2 position1;

        public Vector2 position
        {
            get => position1;

            set
            {
                OnComponentUpdated(this, new ComponentUpdatedEventArgs(position1, value, Id));
                position1 = value;
            }
        }

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
            var clone = new Position(this);
            return clone;
        }

    }
}