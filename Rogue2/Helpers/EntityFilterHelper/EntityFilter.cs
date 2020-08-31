using Rogue2.Components;
using System;
using System.Collections.Generic;

namespace Rogue2.Helpers.EntityFilterHelper
{
    public class EntityFilter
    {
        public List<Type> ComponentsToFilter { get; private set; }
        public EntityFilter()
        {
            ComponentsToFilter = new List<Type>();
        }

        public void AddComponentToFilter<T>() where T : Component
        {
            ComponentsToFilter.Add(typeof(T));
        }
    }
}
