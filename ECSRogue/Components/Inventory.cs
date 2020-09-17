using System.Collections.Generic;
using ECSRogue.Data;
using ECSRogue.Managers.Entities;

namespace ECSRogue.Components
{
    /// <summary>
    ///     Contains references to entities that an entity has in their possession.
    /// </summary>
    internal class Inventory : Component
    {
        public List<Entity> inventory = new List<Entity>();

        public Inventory()
        {
        }

        private Inventory(Inventory inventory)
        {
        }

        public override object Clone()
        {
            return new Inventory(this);
        }
    }
}