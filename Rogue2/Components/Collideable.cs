using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    public class Collideable : Component
    {
        public Collideable()
        {
        }
        public Collideable(Collideable collideable)
        {
        }
        public override object Clone()
        {
            Collideable clone = new Collideable(this);
            return clone;
        }
    }
}
