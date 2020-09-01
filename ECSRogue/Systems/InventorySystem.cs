using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    internal class InventorySystem : GameSystem
    {
        public InventorySystem()
        {
            entityFilter.AddComponentToFilter<Carryable>();
            entityFilter.AddComponentToFilter<Sprite>();
            entityFilter.AddComponentToFilter<Position>();
        }

        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            for (var i = 0; i < eventQueue.Count; i++)
                if (eventQueue[i].GetType() == typeof(GameEvent))
                {
                    var gameEvent = (GameEvent) eventQueue[i];

                    if (gameEvent.eventType == "Pickup")
                    {
                        var source = gameEvent.entities[0];

                        //check if source has inventory

                        if (!source.HasComponent<Inventory>()) continue;

                        var carryable = filteredEntities.Values.FirstOrDefault(x =>
                            x.GetComponent<Position>().position == source.GetComponent<Position>().position);

                        if (carryable != null)
                        {
                            source.GetComponent<Inventory>().inventory.Add(carryable);
                            carryable.GetComponent<Sprite>().render = false;

                            source.GetComponent<Turn>().takenTurn = true;
                        }
                    }
                }
        }
    }
}