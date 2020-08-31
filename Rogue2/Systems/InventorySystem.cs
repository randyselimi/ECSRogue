using Microsoft.Xna.Framework;
using Rogue2.Components;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System.Collections.Generic;
using System.Linq;

namespace Rogue2.Systems
{
    class InventorySystem : GameSystem
    {
        public InventorySystem()
        {
            entityFilter.AddComponentToFilter<Carryable>();
            entityFilter.AddComponentToFilter<Sprite>();
            entityFilter.AddComponentToFilter<Position>();
        }
        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            for (int i = 0; i < eventQueue.Count; i++)
            {
                if (eventQueue[i].GetType() == typeof(GameEvent))
                {
                    GameEvent gameEvent = (GameEvent)eventQueue[i];

                    if (gameEvent.eventType == "Pickup")
                    {
                        Entity source = gameEvent.entities[0];

                        //check if source has inventory

                        if (!source.HasComponent<Inventory>())
                        {
                            continue;
                        }

                        Entity carryable = filteredEntities.Values.FirstOrDefault(x => x.GetComponent<Position>().position == source.GetComponent<Position>().position);

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
}
