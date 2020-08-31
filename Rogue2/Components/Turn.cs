using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    /// <summary>
    /// Represents an entity that abides by the turn system and whether or not the entity has taken its turn
    /// </summary>
    class Turn : Component
    {
        public bool takenTurn = false;
        public Turn()
        {

        }
        Turn(Turn turn)
        {
        }
        public override object Clone()
        {
            return new Turn(this);
        }
    }
}
