using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Rogue2.Components
{
    public abstract class Component : ICloneable
    {
        public abstract object Clone();
    }
}
