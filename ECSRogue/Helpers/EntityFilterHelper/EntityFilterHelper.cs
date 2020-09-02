using System;
using ECSRogue.Components;
using ECSRogue.Managers.Entities;

namespace ECSRogue.Helpers.EntityFilterHelper
{
    //Do something with this
    public static class EntityFilterHelper
    {
        //Filters entity based on entityFilter. 
        
        //public static bool FilterEntity(EntityFilter entityFilter, Entity entityToTest)
        //{
        //    foreach (var component in entityFilter.ComponentsToFilter)
        //        if (entityToTest.HasComponent<>() == false)
        //            return false;

        //    return true;
        //}

        ////Tests if a single component in an EntityFilter is equal to the passed component
        //private static bool TestComponent(Type componentToTest, Entity entityToTest)
        //{
        //    foreach (var component in entityToTest.components.Values)
        //        if (componentToTest == component.GetType())
        //            return true;
        //    return false;
        //}

        //public static bool TestComponent<T>(Entity entityToTest) where T : Component
        //{
        //    foreach (var component in entityToTest.components.Values)
        //        if (typeof(T) == component.GetType())
        //            return true;
        //    return false;
        //}
    }
}