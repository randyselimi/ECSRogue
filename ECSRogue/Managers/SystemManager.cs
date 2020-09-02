using System.Collections.Generic;
using ECSRogue.Helpers.EntityFilterHelper;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.Systems;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers
{
    public class SystemManager : IManager
    {
        //Order of updating: input, update, render
        //Can defintely handle this better
        private readonly List<GameSystem> inputSystems;
        private readonly List<GameSystem> renderSystems;
        private readonly List<GameSystem> systems;
        private readonly List<GameSystem> updateSystems;

        private readonly EntityManager entityManager;
        public SystemManager(EntityManager entityManager)
        {
            this.entityManager = entityManager;
            systems = new List<GameSystem>();
            inputSystems = new List<GameSystem>();
            updateSystems = new List<GameSystem>();
            renderSystems = new List<GameSystem>();
        }

        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue)
        {
            foreach (var system in inputSystems) system.Update(gameTime, eventQueue);

            foreach (var system in updateSystems) system.Update(gameTime, eventQueue);

            foreach (var system in renderSystems) system.Update(gameTime, eventQueue);
        }

        private void AddSystem(GameSystem systemToAdd, List<GameSystem> systemList)
        {
            systems.Add(systemToAdd);
            systemList.Add(systemToAdd);
        }

        public void AddInputSystem(GameSystem systemToAdd)
        {
            AddSystem(systemToAdd, inputSystems);
        }

        public void AddUpdateSystem(GameSystem systemToAdd)
        {
            AddSystem(systemToAdd, updateSystems);
        }

        public void AddRenderSystem(GameSystem systemToAdd)
        {
            AddSystem(systemToAdd, renderSystems);
        }

        public void OnEntityAdded(object source, EntityAddedEventArgs e)
        {
            foreach (var system in systems)
                if (e.entity.HasComponents(system.entityFilter.ComponentsToFilter))
                    system.filteredEntities.Add(e.entity.Id, e.entity);
        }

        public void OnEntityRemoved(object source, EntityRemovedEventArgs e)
        {
            foreach (var system in systems) system.filteredEntities.Remove(e.ID);
        }
    }
}