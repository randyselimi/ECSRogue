using Microsoft.Xna.Framework;
using Rogue2.Components;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System.Collections.Generic;

namespace Rogue2.Systems
{
    class DamageSystem : GameSystem
    {
        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            for (int i = 0; i < eventQueue.Count; i++)
            {
                if (eventQueue[i].GetType() == typeof(GameEvent))
                {
                    GameEvent gameEvent = (GameEvent)eventQueue[i];
                    if (gameEvent.eventType == "Recieve_Damage")
                    {
                        Entity damageReciever = gameEvent.entities[0];

                        damageReciever.GetComponent<Health>().healthPoints -= (int)gameEvent.arguments[0];

                        if (damageReciever.GetComponent<Health>().healthPoints <= 0)
                        {
                            damageReciever.alive = false;
                        }
                    }
                }

            }
        }
    }
}
