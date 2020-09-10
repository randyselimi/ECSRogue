using System.Collections.Generic;
using ECSRogue.Data;

namespace ECSRogue.Components
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