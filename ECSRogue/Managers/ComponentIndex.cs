using System;
using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Managers;
using ECSRogue.Managers.Entities;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers
{
    //TODO: Names are terrible. Must be revised.
    public class IndexManager
    {
        private Dictionary<Type, ComponentIndex> indices = new Dictionary<Type, ComponentIndex>();

        public IndexManager()
        {
            CreateIndice(typeof(Position), new PositionIndex());
        }

        public TValue GetIndice<TKey, TValue>() 
            where TKey : Component
            where TValue : ComponentIndex
        {
            if (!indices.ContainsKey(typeof(TKey)))
            {
                
            }
            return (TValue)indices[typeof(TKey)];
        }

        public void CreateIndice(Type componentType, ComponentIndex indiceToAdd)
        {
            indices.Add(componentType, indiceToAdd);
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
            Type t = args.component.GetType();
            if (indices.ContainsKey(t))
            {

                indices[args.component.GetType()].OnComponentAdded(source, args);
            }
        }
    }

    public abstract class ComponentIndex
    {
        private Dictionary<object, Dictionary<int, Entity>> indexedComponents { get; set; } = new Dictionary<object, Dictionary<int, Entity>>();
        private Type componentType;
        protected Dictionary<int, Entity> GetEntityByIndex(object key)
        {
            return indexedComponents[key];
        }

        protected void AddEntityByIndex(object key, Entity entity)
        {
            if (!indexedComponents.ContainsKey(key))
            {
                indexedComponents.Add(key, new Dictionary<int, Entity>());
            }

            indexedComponents[key].Add(entity.Id, entity);
        }

        public abstract void OnComponentUpdated(object source, ComponentUpdatedEventArgs args);
        public abstract void OnComponentAdded(object source, ComponentAddedEventArgs args);
        public abstract void OnComponentRemoved();
    }

    public class PositionIndex : ComponentIndex
    {
        public Dictionary<int, Entity> GetEntityByIndex(Vector2 index)
        {
            return base.GetEntityByIndex(index);
        }

        public override void OnComponentUpdated(object source, ComponentUpdatedEventArgs args)
        {
            Component component = (Component) source;
            Vector2 previous = (Vector2) args.previous;
            Vector2 current = (Vector2) args.current;

            if (previous == current)
            {
                return;
            }

            Entity temp = GetEntityByIndex(previous)[component.Id];
            GetEntityByIndex(previous).Remove(component.Id);
            AddEntityByIndex(current, temp);
        }

        public override void OnComponentAdded(object source, ComponentAddedEventArgs args)
        {
            Position component = (Position)args.component;
            AddEntityByIndex(component.position, args.entity);
        }

        public override void OnComponentRemoved()
        {
            throw new System.NotImplementedException();
        }
    }
}