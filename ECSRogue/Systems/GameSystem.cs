using System.Collections.Generic;
using ECSRogue.Helpers.EntityFilterHelper;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    public abstract class GameSystem
    {
        public GameSystem()
        {
            filteredEntities = new Dictionary<int, Entity>();
            entityFilter = entityFilter;
        }

        public Dictionary<int, Entity> filteredEntities { get; set; }
        public EntityFilter entityFilter { get; protected set; } = new EntityFilter();

        private void Add(Entity entity)
        {
            filteredEntities.Add(entity.ID, entity);
        }

        private void Remove(int ID)
        {
            filteredEntities.Remove(ID);
        }

        private void Remove(Entity entity)
        {
            Remove(entity.ID);
        }

        public abstract void Update(GameTime gameTime, List<IEvent> eventQueue);
    }
}