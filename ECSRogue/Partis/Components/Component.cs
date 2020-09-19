using System;
using System.Collections.Generic;
using ECSRogue.Data;

namespace ECSRogue.Components
{
    public abstract class Component : ICloneable
    {
        public int Id { get; set; }
        public abstract object Clone();
    }
}