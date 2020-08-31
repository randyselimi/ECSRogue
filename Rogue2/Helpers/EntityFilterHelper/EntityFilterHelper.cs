using Rogue2.Components;
using Rogue2.Managers.Entities;
using System;

namespace Rogue2.Helpers.EntityFilterHelper
{
    //Do something with this
    public static class EntityFilterHelper
    {

        //Filters entity based on entityFilter. 
        public static bool FilterEntity(EntityFilter entityFilter, Entity entityToTest)
        {
            foreach (Type component in entityFilter.ComponentsToFilter)
            {
                if (TestComponent(component, entityToTest) == false)
                {
                    return false;
                }
            }

            return true;
        }

        //Tests if a single component in an EntityFilter is equal to the passed component
        static bool TestComponent(Type componentToTest, Entity entityToTest)
        {
            foreach (var component in entityToTest.components.Values)
            {
                if (componentToTest == component.GetType())
                {
                    return true;
                }
            }
            return false;
        }

        public static bool TestComponent<T>(Entity entityToTest) where T : Component
        {
            foreach (var component in entityToTest.components.Values)
            {
                if (typeof(T) == component.GetType())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
