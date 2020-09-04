using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Managers;
using ECSRogue.Managers.Entities;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers
{
    //TODO: Names are terrible. Must be revised.
    //public class IndexManager
    //{
    //    private Dictionary<Type, ComponentIndex> indices = new Dictionary<Type, ComponentIndex>();

    //    public IndexManager()
    //    {
    //        CreateIndice(typeof(Position), new PositionIndex());
    //    }

    //    public TValue GetIndice<TKey, TValue>() 
    //        where TKey : Component
    //        where TValue : ComponentIndex
    //    {
    //        if (!indices.ContainsKey(typeof(TKey)))
    //        {

    //        }
    //        return (TValue)indices[typeof(TKey)];
    //    }

    //    public void CreateIndice(Type componentType, ComponentIndex indiceToAdd)
    //    {
    //        indices.Add(componentType, indiceToAdd);
    //    }
    //    public void OnComponentUpdated(object source, ComponentUpdatedEventArgs args)
    //    {
    //        if (indices.ContainsKey(source.GetType()))
    //        {
    //            indices[source.GetType()].OnComponentUpdated(source, args);
    //        }
    //    }
    //    public void OnComponentAdded(object source, ComponentAddedEventArgs args)
    //    {
    //        Type t = args.component.GetType();
    //        if (indices.ContainsKey(t))
    //        {

    //            indices[args.component.GetType()].OnComponentAdded(source, args);
    //        }
    //    }
    //}

    #region Indexers

    public interface IComponentIndexer
    {
        Type key { get; set; }
        object index { get; set; }
    }

    public class TypeIndexer : IComponentIndexer
    {
        public Type key { get; set; }
        public object index { get; set; }

        public TypeIndexer(Type index)
        {
            key = typeof(Type);
            this.index = index;
        }
    }
    public class PositionIndexer : IComponentIndexer
    {
        public Type key { get; set; }
        public object index { get; set; }

        public PositionIndexer(Vector2 index)
        {
            key = typeof(Position);
            this.index = index;
        }
    }
    public class IsActiveIndexer : IComponentIndexer
    {
        public Type key { get; set; }
        public object index { get; set; }

        public IsActiveIndexer(bool index)
        {
            key = typeof(IsActive);
            this.index = index;
        }
    }

    #endregion

    public class EntityIndexManager
    {
        private Dictionary<Type, EntityIndex> indices = new Dictionary<Type, EntityIndex>();

        public EntityIndexManager()
        {
            CreateIndice(typeof(Position), new PositionIndex());
            CreateIndice(typeof(Type), new TypeIndex());
            CreateIndice(typeof(IsActive), new IsActiveIndex());
        }

        public EntityIndex GetEntityIndexer(IComponentIndexer indexer)
        {
            return indices[indexer.key];
        }

        public Dictionary<int, Entity> GetEntitiesByIndex(IComponentIndexer indexer)
        {
            return GetEntityIndexer(indexer).GetEntitiesByIndex(indexer.index);
        }
        public Dictionary<int, Entity> GetEntitiesByIndexes(List<IComponentIndexer> indexers)
        {
            var entities = new Dictionary<int, Entity>();
            for (int currentIndex = 0; currentIndex < indexers.Count; currentIndex++)
            {
                if (currentIndex == 0)
                {
                    entities = GetEntityIndexer(indexers[currentIndex]).GetEntitiesByIndex(indexers[currentIndex].index);
                }

                else
                {
                    entities = GetEntityIndexer(indexers[currentIndex])
                        .GetEntitiesByIndexAndKeys(indexers[currentIndex].index, entities.Keys);
                }
            }

            return entities;
        }

        public void CreateIndice(Type test, EntityIndex index)
        {
            indices.Add(test, index);
        }

        public void OnComponentUpdated(object source, ComponentUpdatedEventArgs args)
        {
            if (indices.ContainsKey(source.GetType()))
            {
                indices[source.GetType()].OnComponentUpdated(source, args);
            }
        }
        public void OnComponentAdded(object source, ComponentAddedEventArgs args)
        {
            var type = args.component.GetType();

            foreach (var entityIndex in indices.Values)
            {
                entityIndex.OnComponentAdded(source, args);
            }
        }
        public void OnComponentRemoved()
        {

        }
    }



    public abstract class EntityIndex
    {
        public Type component { get; }
        public Dictionary<object, Dictionary<int, Entity>> EntitySubIndexes { get; set; } = new Dictionary<object, Dictionary<int, Entity>>();
        public Dictionary<int, Entity> GetEntitiesByIndex(object index)
        {
            return EntitySubIndexes[index];
        }
        public Dictionary<int, Entity> GetEntitiesByIndexAndKeys(object index, Dictionary<int, Entity>.KeyCollection keys)
        {
            var set = EntitySubIndexes[index];
            var subset = new Dictionary<int, Entity>();

            foreach (var key in keys)
            {
                if (set.ContainsKey(key))
                {
                    subset.Add(key, set[key]);
                }
            }

            return subset;
        }

        protected void AddEntityByIndex(object index, Entity entity)
        {
            if (!EntitySubIndexes.ContainsKey(index))
            {
                EntitySubIndexes.Add(index, new Dictionary<int, Entity>());
            }

            EntitySubIndexes[index].Add(entity.Id, entity);
        }
        //Implement Removal Behavior
        public void RemoveEntityByIndex()
        {

        }
        public void SwapEntityByIndex(object previous, object current, int id)
        {
            var swap = GetEntitiesByIndex(previous)[id];
            GetEntitiesByIndex(previous).Remove(id);
            AddEntityByIndex(current, swap);
        }


        public void OnComponentUpdated(object source, ComponentUpdatedEventArgs args)
        {
            if (args.previous != args.current)
            {
                SwapEntityByIndex(args.previous, args.current, args.id);
            }


        }
        public abstract void OnComponentAdded(object source, ComponentAddedEventArgs args);
        public abstract void OnComponentRemoved();

    }

    public class TypeIndex : EntityIndex
    {
        public override void OnComponentAdded(object source, ComponentAddedEventArgs args)
        {
            AddEntityByIndex(args.component.GetType(), args.entity);
        }

        public override void OnComponentRemoved()
        {
            throw new NotImplementedException();
        }
    }
    public class PositionIndex : EntityIndex
    {
        public override void OnComponentAdded(object source, ComponentAddedEventArgs args)
        {
            if (args.component is Position)
            {
                AddEntityByIndex((args.component as Position).position, args.entity);
            }
        }
        public override void OnComponentRemoved()
        {
            throw new System.NotImplementedException();
        }
    }

    class IsActiveIndex : EntityIndex
    {
        public override void OnComponentAdded(object source, ComponentAddedEventArgs args)
        {
            if (args.component is IsActive)
            {
                AddEntityByIndex((args.component as IsActive).isActive, args.entity);
            }
        }
        public override void OnComponentRemoved()
        {
            throw new System.NotImplementedException();
        }
    }
}