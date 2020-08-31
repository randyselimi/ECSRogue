using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    class Damage : Component
    {
        public int damageValue;

        public Damage()
        {
        }
        public Damage(int damageValue)
        {
            this.damageValue = damageValue;
        }
        public Damage(Damage damage)
        {
            this.damageValue = damage.damageValue;
        }
        public override object Clone()
        {
            return new Damage(this);
        }
    }
}
