using System.Collections.Generic;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using ECSRogue.Systems;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers
{
    public class SystemManager
    {
        //Order of updating: input, update, render
        //Can defintely handle this better
        private readonly List<GameSystem> inputSystems;
        private readonly List<GameSystem> renderSystems;
        private readonly List<GameSystem> systems;
        private readonly List<GameSystem> updateSystems;

        public SystemManager()
        {
            systems = new List<GameSystem>();
            inputSystems = new List<GameSystem>();
            updateSystems = new List<GameSystem>();
            renderSystems = new List<GameSystem>();
        }

        public void Update(PartisInstance instance)
        {
            foreach (var system in inputSystems) system.Update(instance);

            foreach (var system in updateSystems) system.Update(instance);

            foreach (var system in renderSystems) system.Update(instance);
        }

        private void AddSystem(GameSystem gameSystemToAdd, List<GameSystem> systemList)
        {
            systems.Add(gameSystemToAdd);
            systemList.Add(gameSystemToAdd);
        }

        public void AddSystem(GameSystem gameSystemToAdd)
        {
            if (gameSystemToAdd.SystemType == SystemTypes.InputSystem)
            {
                AddInputSystem(gameSystemToAdd);
            }
            else if (gameSystemToAdd.SystemType == SystemTypes.UpdateSystem)
            {
                AddUpdateSystem(gameSystemToAdd);
            }
            else if (gameSystemToAdd.SystemType == SystemTypes.RenderSystem)
            {
                AddRenderSystem(gameSystemToAdd);
            }
        }

        public void AddInputSystem(GameSystem gameSystemToAdd)
        {
            AddSystem(gameSystemToAdd, inputSystems);
        }

        public void AddUpdateSystem(GameSystem gameSystemToAdd)
        {
            AddSystem(gameSystemToAdd, updateSystems);
        }

        public void AddRenderSystem(GameSystem gameSystemToAdd)
        {
            AddSystem(gameSystemToAdd, renderSystems);
        }

        //public void OnEntityAdded(object source, EntityAddedEventArgs e)
        //{
        //    foreach (var system in systems)
        //        if (e.entity.HasComponents(system.entityFilter.ComponentsToFilter))
        //            system.filteredEntities.Add(e.entity.Id, e.entity);
        //}

        //public void OnEntityRemoved(object source, EntityRemovedEventArgs e)
        //{
        //    foreach (var system in systems) system.filteredEntities.Remove(e.ID);
        //}
    }
}