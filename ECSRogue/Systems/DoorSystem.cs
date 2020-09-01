using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    internal class DoorSystem : GameSystem
    {
        public DoorSystem()
        {
            entityFilter.AddComponentToFilter<Door>();
        }

        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            for (var i = 0; i < eventQueue.Count; i++)
                if (eventQueue[i].GetType() == typeof(GameEvent))
                {
                    var gameEvent = (GameEvent) eventQueue[i];

                    if (gameEvent.eventType == "Collision")
                        if (filteredEntities.ContainsValue(gameEvent.entities[1]))
                            gameEvent.entities[1].alive = false;
                }
        }
    }
}