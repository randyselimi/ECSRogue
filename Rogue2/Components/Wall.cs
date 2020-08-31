using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    class Wall : Component
    {
        public Wall()
        {
        }
        public Wall(Wall wall)
        {

        }

        public override object Clone()
        {
            return new Wall(this);
        }
    }
}
