using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    public class Door : Component
    {
        public Door()
        {

        }

        public Door(Door door)
        {

        }
        public override object Clone()
        {
            return new Door(this);
        }
    }
}
