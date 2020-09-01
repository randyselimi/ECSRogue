using Microsoft.Xna.Framework;

namespace ECSRogue.Components
{
    internal class Velocity : Component
    {
        public Vector2 velocity;

        public Velocity()
        {
        }

        public Velocity(Vector2 velocity)
        {
            this.velocity = velocity;
        }

        public Velocity(Velocity velocity)
        {
            this.velocity = velocity.velocity;
        }

        public override object Clone()
        {
            var clone = new Velocity(this);
            return clone;
        }
    }
}