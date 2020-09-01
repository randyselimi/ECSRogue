using Microsoft.Xna.Framework;

namespace ECSRogue.Components
{
    //Refactor. have a isGlobalPositionflag
    /// <summary>
    ///     Position component used for free-floating game objects not bound to the tile system, such as cameras and ui
    ///     elements
    ///     Coordinate corresponds to global
    /// </summary>
    internal class Position : Component
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
            var clone = new Position(this);
            return clone;
        }
    }
}