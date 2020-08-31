using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    class Carryable : Component
    {
        public Carryable()
        {

        }
        public Carryable(Carryable carryable)
        {
            
        }
        public override object Clone()
        {
            return new Carryable(this);
        }
    }
}
