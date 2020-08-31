using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    class Player : Component
    {
        public Player()
        {
        }
        public Player(Player player)
        {
        }
        public override object Clone()
        {
            Player clone = new Player(this);
            return clone;
        }
    }
}
