using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    internal class DamageSystem : GameSystem
    {
        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            for (var i = 0; i < eventQueue.Count; i++)
                if (eventQueue[i].GetType() == typeof(GameEvent))
                {
                    var gameEvent = (GameEvent) eventQueue[i];
                    if (gameEvent.eventType == "Recieve_Damage")
                    {
                        var damageReciever = gameEvent.entities[0];

                        damageReciever.GetComponent<Health>().healthPoints -= (int) gameEvent.arguments[0];

                        if (damageReciever.GetComponent<Health>().healthPoints <= 0) damageReciever.alive = false;
                    }
                }
        }
    }
}