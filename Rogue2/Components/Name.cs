using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    class Name : Component
    {
        public string entityName;
        public Name(string entityName)
        {
            this.entityName = entityName;
        }
        public Name(Name name)
        {
            this.entityName = name.entityName;
        }
        public override object Clone()
        {
            Name clone = new Name(this);
            return clone;
        }
    }
}
