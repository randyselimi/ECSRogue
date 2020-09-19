using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using ECSRogue.Components;
using ECSRogue.Managers;
using ECSRogue.Managers.Entities;
using Microsoft.Xna.Framework;
using Component = ECSRogue.Components.Component;

namespace ECSRogue.Managers
{
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

    public class LevelIndexer : IComponentIndexer
    {
        public Type key { get; set; }
        public object index { get; set; }

        public LevelIndexer(int index)
        {
            key = typeof(LevelPosition);
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
        private Dictionary<Type, EntityIndex> indexes = new Dictionary<Type, EntityIndex>();

        public EntityIndexManager()
        {
            //Add default entityindexer of type Type
            indexes.Add(typeof(Type), new EntityIndex());

            //Use CreateIndex to create custom indexes from components that implement IIndexable
            CreateIndex<Position>();
            CreateIndex<IsActive>();
            CreateIndex<LevelPosition>();
        }

        public EntityIndex GetEntityIndexer(IComponentIndexer indexer)
        {
            return indexes[indexer.key];
        }
        public Dictionary<int, Entity> GetEntitiesByIndex(IComponentIndexer indexer)
        {
            return GetEntityIndexer(indexer).GetEntitiesByIndex(indexer.index);
        }
        public Dictionary<int, Entity> GetEntitiesByIndexes(params IComponentIndexer[] indexers)
        {
            var entities = new Dictionary<int, Entity>();
            for (int currentIndex = 0; currentIndex < indexers.Length; currentIndex++)
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

        public void CreateIndex<T>() where T : IIndexableComponent
        {
            indexes.Add(typeof(T), new EntityIndex());
        }

        public void OnComponentUpdated(object source, ComponentUpdatedEventArgs args)
        {
            if (indexes.ContainsKey(source.GetType()))
            {
                indexes[source.GetType()].OnComponentUpdated(source, args);
            }
        }
        public void OnComponentAdded(object source, ComponentAddedEventArgs args)
        {
            //Add every component to TypeIndex
            indexes[typeof(Type)].OnComponentAdded(source, args);

            //If the component also implements IIndexable then pass it to its respective index
            if (args.component is IIndexableComponent indexableComponent)
            {
                indexes[args.component.GetType()].OnComponentAdded(source, args);
            }
        }
        public void OnComponentRemoved(object source, ComponentRemovedEventArgs args)
        {
            //Remove component from TypeIndex
            indexes[typeof(Type)].OnComponentRemoved(source, args);

            //If the component also implements IIndexable then remove it from its respective index
            if (args.component is IIndexableComponent indexableComponent)
            {
                indexes[args.component.GetType()].OnComponentRemoved(source, args);
            }
        }
    }



    public class EntityIndex
    {
        public Dictionary<object, Dictionary<int, Entity>> EntitySubIndexes { get; set; } = new Dictionary<object, Dictionary<int, Entity>>();

        public Dictionary<int, Entity> GetEntitiesByIndex(object index)
        {
            EntitySubIndexes.TryGetValue(index, out var value);

            return value;
        }
        public Dictionary<int, Entity> GetEntitiesByIndexAndKeys(object index, Dictionary<int, Entity>.KeyCollection keys)
        {
            var set = EntitySubIndexes[index];
            var subset = new Dictionary<int, Entity>();

            if (keys != null)
            {
                foreach (var key in keys)
                {
                    if (set.ContainsKey(key))
                    {
                        subset.Add(key, set[key]);
                    }
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
        public void RemoveEntityByIndex(object index, int entityId)
        {
            if (EntitySubIndexes.ContainsKey(index))
            {
                EntitySubIndexes[index].Remove(entityId);
            }
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
        public void OnComponentAdded(object source, ComponentAddedEventArgs args)
        {
            //This is some hacky logic. Basically if an object implements IIndexable, get its index value; otherwise, get its type for TypeIndex. Probably should split entity indexes into two types
            AddEntityByIndex(args.component is IIndexableComponent component ? component.GetIndexValue() : args.component.GetType(), args.entity);
        }
        public void OnComponentRemoved(object source, ComponentRemovedEventArgs args)
        {
            RemoveEntityByIndex(args.component is IIndexableComponent component ? component.GetIndexValue() : args.component.GetType(), args.entityId);
        }
    }
}