using Microsoft.Xna.Framework;
using Rogue2.Helpers.EntityFilterHelper;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System.Collections.Generic;

namespace Rogue2.Systems
{
    public abstract class GameSystem
    {
        public Dictionary<int, Entity> filteredEntities { get; set; }
        public EntityFilter entityFilter { get; protected set; } = new EntityFilter();
        public GameSystem()
        {
            filteredEntities = new Dictionary<int, Entity>();
            this.entityFilter = entityFilter;
        }
        void Add(Entity entity)
        {
            filteredEntities.Add(entity.ID, entity);
        }
        void Remove(int ID)
        {
            filteredEntities.Remove(ID);
        }
        void Remove(Entity entity)
        {
            Remove(entity.ID);
        }
        public abstract void Update(GameTime gameTime, List<IEvent> eventQueue);
    }
}
