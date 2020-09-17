using System.Collections.Generic;
using ECSRogue.Data;

namespace ECSRogue.Components
{
    internal class Floor : Component
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