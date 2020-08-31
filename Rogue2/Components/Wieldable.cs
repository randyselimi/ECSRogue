using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    class Wieldable : Component
    {
        public string slot;

        public Wieldable()
        {
        }
        public Wieldable(string slot)
        {
            this.slot = slot;
        }
        public Wieldable(Wieldable wieldable)
        {
            this.slot = wieldable.slot;
        }

        public override object Clone()
        {
            return new Wieldable(this);
        }
    }
}
