using System;
using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Factories.EntityFactory;
using ECSRogue.Helpers.EntityFilterHelper;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers.Entities
{
    public class EntityManager : IManager
    {
        public delegate void EntityAddedEventHandler(object source, EntityAddedEventArgs args);

        public delegate void EntityRemovedEventHandler(object source, EntityRemovedEventArgs args);

        public EntityManager()
        {
            ID = 0;
            entities = new Dictionary<int, Entity>();
        }

        //might not work
        public Dictionary<int, Entity> entities { get; }
        public int ID { get; private set; }

        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue)
        {
            foreach (var entity in entities.Values)
                if (entity.alive == false)
                    RemoveEntity(entity.ID);
        }

        public Entity CreateEntity(EntityFactory entityFactory)
        {
            var createdEntity = entityFactory.CreateEntity(ID++);

            entities.Add(createdEntity.ID, createdEntity);

            //figure out if passing the entity is a bad idea
            var args = new EntityAddedEventArgs();
            args.entity = createdEntity;

            OnEntityAdded(this, args);

            return createdEntity;
        }

        public void RemoveEntity(int ID)
        {
            if (entities.Remove(ID))
            {
                var args = new EntityRemovedEventArgs();
                args.ID = ID;

                OnEntityRemoved(this, args);
            }
        }

        public Entity GetEntityByID(int ID)
        {
            Entity entity = null;
            entities.TryGetValue(ID, out entity);

            return entity;
        }

        public List<Entity> GetEntitiesByComponent<T>() where T : Component
        {
            var entitiesWithComponent = new List<Entity>();

            foreach (var entity in entities.Values)
                if (EntityFilterHelper.TestComponent<T>(entity))
                    entitiesWithComponent.Add(entity);

            return entitiesWithComponent;
        }

        public event EntityAddedEventHandler EntityAdded;
        public event EntityRemovedEventHandler EntityRemoved;

        public void OnEntityAdded(object source, EntityAddedEventArgs args)
        {
            EntityAdded?.Invoke(source, args);
        }

        public void OnEntityRemoved(object source, EntityRemovedEventArgs args)
        {
            EntityRemoved?.Invoke(source, args);
        }
    }

    //combine into 1?
    public class EntityAddedEventArgs : EventArgs
    {
        public Entity entity { get; set; }
    }

    public class EntityRemovedEventArgs : EventArgs
    {
        public int ID { get; set; }
    }
}