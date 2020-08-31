using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    class Floor : Component
    {
        public Floor()
        {

        }
        public Floor(Floor floor)
        {
        }

        public override object Clone()
        {
            return new Floor(this);
        }
    }
}
