using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    class Health : Component
    {
        public int healthPoints;

        public Health()
        {
        }
        public Health(int healthPoints)
        {
            this.healthPoints = healthPoints;
        }
        public Health(Health health)
        {
            this.healthPoints = health.healthPoints;
        }
        public override object Clone()
        {
            return new Health(this);
        }
    }
}
