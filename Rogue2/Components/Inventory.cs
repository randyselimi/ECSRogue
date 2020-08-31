using Rogue2.Managers.Entities;
using System.Collections.Generic;

namespace Rogue2.Components
{
    /// <summary>
    /// Contains references to entities that an entity has in their possession.
    /// </summary>
    class Inventory : Component
    {
        public List<Entity> inventory = new List<Entity>();

        public Inventory()
        {

        }

        Inventory(Inventory inventory)
        {

        }

        public override object Clone()
        {
            return new Inventory(this);
        }
    }
}
