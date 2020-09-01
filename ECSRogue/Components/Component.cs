using System;

namespace ECSRogue.Components
{
    public abstract class Component : ICloneable
    {
        public abstract object Clone();
    }
}