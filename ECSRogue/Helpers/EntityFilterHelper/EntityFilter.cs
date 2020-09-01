using System;
using System.Collections.Generic;
using ECSRogue.Components;

namespace ECSRogue.Helpers.EntityFilterHelper
{
    public class EntityFilter
    {
        public EntityFilter()
        {
            ComponentsToFilter = new List<Type>();
        }

        public List<Type> ComponentsToFilter { get; }

        public void AddComponentToFilter<T>() where T : Component
        {
            ComponentsToFilter.Add(typeof(T));
        }
    }
}