using System;
using System.Collections.Generic;
using System.Text;
using ECSRogue.Data;
using ECSRogue.Factories;
using ECSRogue.Factories.EntityFactory;
using ECSRogue.Managers;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.Managers.Events.EventProcessor;
using ECSRogue.Systems;

namespace ECSRogue.Partis
{
    public class PartisInstance
    {
        private EntityManager entityManager = new EntityManager(new EntityFactory(), new ComponentFactory());
        private SystemManager systemManager = new SystemManager();
        private EventManager eventManager = new EventManager(new List<IEventProcessor>()
            {new GameEventProcessor(), new UIEventProcessor()});

        //Entities
        public Dictionary<int, Entity>.ValueCollection GetEntitiesByIndex(IComponentIndexer indexer)
        {
            return entityManager.indexManager.GetEntitiesByIndex(indexer).Values;
        }
        public Dictionary<int, Entity>.ValueCollection GetEntitiesByIndexes(params IComponentIndexer[] indexers)
        {
            return entityManager.indexManager.GetEntitiesByIndexes(indexers).Values;
        }
        public Entity CreateEntity(string entityDefinition)
        {
            return entityManager.CreateEntity(entityDefinition);
        }
        public void RemoveEntity(int entityId)
        {
            entityManager.RemoveEntity(entityId);
        }

        //Systems
        public void AddSystem(GameSystem gameSystemToAdd)
        {
            systemManager.AddSystem(gameSystemToAdd);
        }

        //Events
        public List<T> GetEvents<T>() where T : IEvent
        {
            return eventManager.GetEvents<T>();
        }
        public void AddEvent(IEvent newEvent)
        {
            eventManager.AddEvent(newEvent);
        }

        public void Initialize()
        {
        }

        public void Load(Dictionary<string, EntityDefinition> entityDefinitions)
        {
            entityManager.Load(entityDefinitions);
        }
        public void Update()
        {
            entityManager.Update();
            systemManager.Update(this);
            eventManager.Update();
        }

    }
}
