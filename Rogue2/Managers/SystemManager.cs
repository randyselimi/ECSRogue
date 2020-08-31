using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Rogue2.Helpers.EntityFilterHelper;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using Rogue2.Systems;

namespace Rogue2.Managers
{
    public class SystemManager : IManager
    {
        List<GameSystem> systems;
        //Order of updating: input, update, render
        //Can defintely handle this better
        List<GameSystem> inputSystems;
        List<GameSystem> updateSystems;
        List<GameSystem> renderSystems;

        public SystemManager()
        {
            systems = new List<GameSystem>();
            inputSystems = new List<GameSystem>();
            updateSystems = new List<GameSystem>();
            renderSystems = new List<GameSystem>();
        }

        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue)
        {
            foreach (var system in inputSystems)
            {
                system.Update(gameTime, eventQueue);
            }

            foreach (var system in updateSystems)
            {
                system.Update(gameTime, eventQueue);
            }

            foreach (var system in renderSystems)
            {
                system.Update(gameTime, eventQueue);
            }
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
            foreach (GameSystem system in systems)
            {
                    if (EntityFilterHelper.FilterEntity(system.entityFilter, e.entity))
                    {
                        system.filteredEntities.Add(e.entity.ID, e.entity);
                    }
            }
        }

        public void OnEntityRemoved(object source, EntityRemovedEventArgs e)
        {
            foreach (GameSystem system in systems)
            {
                system.filteredEntities.Remove(e.ID);
            }
        }
    }
}
