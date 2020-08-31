using Microsoft.Xna.Framework;
using Rogue2.Components;
using Rogue2.Managers.Events;
using System.Collections.Generic;

namespace Rogue2.Systems
{
    class DoorSystem : GameSystem
    {
        public DoorSystem()
        {
            entityFilter.AddComponentToFilter<Door>();
        }
        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            for (int i = 0; i < eventQueue.Count; i++)
            {
                if (eventQueue[i].GetType() == typeof(GameEvent))
                {
                    GameEvent gameEvent = (GameEvent)eventQueue[i];

                    if (gameEvent.eventType == "Collision")
                    {
                        if (filteredEntities.ContainsValue(gameEvent.entities[1]))
                        {
                            gameEvent.entities[1].alive = false;
                        }

                    }
                }
            }
        }
    }
}
