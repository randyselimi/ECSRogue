using System;
using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Data;
using ECSRogue.Factories;
using ECSRogue.Factories.EntityFactory;
using ECSRogue.Helpers.EntityFilterHelper;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers.Entities
{
    //TODO: This should be split up into mulitple classes
    public class EntityManager : IManager
    {
        public delegate void EntityAddedEventHandler(object source, EntityAddedEventArgs args);

        public delegate void EntityRemovedEventHandler(object source, EntityRemovedEventArgs args);

        public delegate void ComponentAddedEventHandler(object source, ComponentAddedEventArgs args);

        public delegate void ComponentRemovedEventHandler(object source, ComponentRemovedEventArgs args);

        private readonly EntityFactory entityFactory;

        /// <summary>
        /// Value of componentDictionary should be store 
        /// </summary>
        private Dictionary<Type, Dictionary<Entity, Component>> ComponentDictionary = new Dictionary<Type, Dictionary<Entity, Component>>();

        private readonly ComponentFactory componentFactory;
        //private readonly ComponentManager _componentManager;
        public EntityManager(EntityFactory entityFactory, ComponentFactory componentFactory)
        {
            this.entityFactory = entityFactory;
            this.componentFactory = componentFactory;
            Id = 0;
            Entities = new Dictionary<int, Entity>();

            indexManager = new EntityIndexManager();
            ComponentAdded += indexManager.OnComponentAdded;
        }

        //might not work
        public Dictionary<int, Entity> Entities { get; }
        public Dictionary<Type, Dictionary<int, Component>> Components { get; }
        public EntityIndexManager indexManager;
        public int Id { get; private set; }

        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue)
        {
            foreach (var entity in Entities.Values)
                if (entity.alive == false)
                    RemoveEntity(entity.Id);
        }

        public Entity CreateEntity(EntityTemplate entityTemplate)
        {
            var createdEntity = entityFactory.CreateEntity(this, entityTemplate, Id++);
            return AddEntity(createdEntity);
        }
        private Entity AddEntity(Entity entity)
        {
            Entities.Add(entity.Id, entity);
            //figure out if passing the entity is a bad idea
            var args = new EntityAddedEventArgs();
            args.entity = entity;

            OnEntityAdded(this, args);

            return entity;
        }
        public void RemoveEntity(int ID)
        {
            if (Entities.Remove(ID))
            {
                var args = new EntityRemovedEventArgs();
                args.ID = ID;

                OnEntityRemoved(this, args);
            }
        }
        public Entity GetEntityByID(int ID)
        {
            Entity entity = null;
            Entities.TryGetValue(ID, out entity);

            return entity;
        }


        public Dictionary<Entity, Component> GetComponents(Type t)
        {
            if (!ComponentDictionary.ContainsKey(t))
            {
                ComponentDictionary.Add(t, new Dictionary<Entity, Component>());
            }

            return ComponentDictionary[t];
        }
        public Component GetComponent(Entity entity, Type t)
        {
            if (GetComponents(t).ContainsKey(entity))
            {
                return ComponentDictionary[t][entity];
            }
            return null;
        }

        public bool HasComponent(Entity entity, Type t)
        {
            return GetComponent(entity, t) != null;
        }
        public bool HasComponents(Entity entity, List<Type> types)
        {
            bool hasComponents = true;
            foreach (var t in types)
            {
                if (!HasComponent(entity, t))
                {
                    hasComponents = false;
                }
            }

            return hasComponents;
        }
        public void CreateComponent(Component component, Entity entity)
        {
            AddComponent(componentFactory.CreateComponent(component, entity), entity);
        }
        public void AddComponent(Component component, Entity entity)
        {
            GetComponents(component.GetType()).Add(entity, component);
            component.ComponentUpdated += indexManager.OnComponentUpdated;
            OnComponentAdded(this, new ComponentAddedEventArgs(component, entity));
        }

        public List<Entity> GetEntitiesByComponent<T>() where T : Component
        {
            return ComponentDictionary[typeof(T)].Keys.ToList();
        }

        public event EntityAddedEventHandler EntityAdded;
        public event EntityRemovedEventHandler EntityRemoved;

        public event ComponentAddedEventHandler ComponentAdded;
        public event ComponentRemovedEventHandler ComponentRemoved;

        public void OnEntityAdded(object source, EntityAddedEventArgs args)
        {
            EntityAdded?.Invoke(source, args);
        }

        public void OnEntityRemoved(object source, EntityRemovedEventArgs args)
        {
            EntityRemoved?.Invoke(source, args);
        }

        public void OnComponentAdded(object source, ComponentAddedEventArgs args)
        {
            ComponentAdded?.Invoke(source, args);
        }

        public void OnComponentRemoved(object source, ComponentRemovedEventArgs args)
        {
            ComponentRemoved?.Invoke(source, args);
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

    public class ComponentAddedEventArgs : EventArgs
    {
        public Component component { get; set; }

        public Entity entity { get; set; }

        public ComponentAddedEventArgs(Component component, Entity entity)
        {
            this.component = component;
            this.entity = entity;
        }
    }

    public class ComponentRemovedEventArgs : EventArgs
    {
        public Component component { get; set; }

        public Entity entity { get; set; }

        public ComponentRemovedEventArgs(Component component, Entity entity)
        {
            this.component = component;
            this.entity = entity;
        }
    }
}