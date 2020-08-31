using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    class Weight : Component
    {
        int weight;

        Weight()
        {
        }
        Weight(Weight weight)
        {
            this.weight = weight.weight;
        }
        public override object Clone()
        {
            return new Weight(this);
        }
    }
}
