using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    class Velocity : Component
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
            Velocity clone = new Velocity(this);
            return clone;
        }
    }
}
