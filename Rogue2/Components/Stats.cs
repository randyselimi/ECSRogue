using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    class Stats : Component
    {
        int strength;
        int agility;
        int intelligence;

        Stats()
        {

        }
        Stats(Stats stats)
        {
            this.strength = stats.strength;
            this.agility = stats.agility;
            this.intelligence = stats.intelligence;
        }

        public override object Clone()
        {
            return new Stats(this);
        }
    }
}
