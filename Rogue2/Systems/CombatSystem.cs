using Microsoft.Xna.Framework;
using Rogue2.Components;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System.Collections.Generic;
using System.Linq;

namespace Rogue2.Systems
{
    class CombatSystem : GameSystem
    {
        public CombatSystem()
        {
            entityFilter.AddComponentToFilter<Health>();
            entityFilter.AddComponentToFilter<Position>();
        }
        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            for (int i = 0; i < eventQueue.Count; i++)
            {
                if (eventQueue[i].GetType() == typeof(GameEvent))
                {
                    GameEvent gameEvent = (GameEvent)eventQueue[i];

                    if (gameEvent.eventType == "Attack_Left")
                    {
                        Entity defender = filteredEntities.Values.FirstOrDefault(x => x.GetComponent<Position>().position - new Vector2(-1, 0) == gameEvent.entities[0].GetComponent<Position>().position);
                        Entity attacker = gameEvent.entities[0];

                        if (defender != null)
                        {
                            int damage = CalculateDamageDealt(attacker);
                            eventQueue.Add(new GameEvent("Recieve_Damage", new List<Entity>() { defender, attacker }, damage));
                        }
                    }

                    if (gameEvent.eventType == "Attack_Right")
                    {
                        Entity defender = filteredEntities.Values.FirstOrDefault(x => x.GetComponent<Position>().position - new Vector2(1, 0) == gameEvent.entities[0].GetComponent<Position>().position);
                        Entity attacker = gameEvent.entities[0];

                        if (defender != null)
                        {
                            int damage = CalculateDamageDealt(attacker);
                            eventQueue.Add(new GameEvent("Recieve_Damage", new List<Entity>() { defender, attacker }, damage));
                        }
                    }


                    if (gameEvent.eventType == "Attack_Up")
                    {
                        Entity defender = filteredEntities.Values.FirstOrDefault(x => x.GetComponent<Position>().position - new Vector2(0, -1) == gameEvent.entities[0].GetComponent<Position>().position);
                        Entity attacker = gameEvent.entities[0];

                        if (defender != null)
                        {
                            int damage = CalculateDamageDealt(attacker);
                            eventQueue.Add(new GameEvent("Recieve_Damage", new List<Entity>() { defender, attacker }, damage));
                        }
                    }

                    if (gameEvent.eventType == "Attack_Down")
                    {
                        Entity defender = filteredEntities.Values.FirstOrDefault(x => x.GetComponent<Position>().position - new Vector2(0, 1) == gameEvent.entities[0].GetComponent<Position>().position);
                        Entity attacker = gameEvent.entities[0];

                        if (defender != null)
                        {
                            int damage = CalculateDamageDealt(attacker);
                            eventQueue.Add(new GameEvent("Recieve_Damage", new List<Entity>() { defender, attacker }, damage));
                        }
                    }
                }
                
            }
        }
         
        public int CalculateDamageDealt(Entity damageDealer)
        {
            int totalDamage = 0;

            Entity equippedWeapon = damageDealer.GetComponent<Equipment>().equipment.Where(x => x.slot == "Wieldable").FirstOrDefault().entity;
            
            if (equippedWeapon != null)
            {
                totalDamage += equippedWeapon.GetComponent<Damage>().damageValue;
            }

            else
            {
                totalDamage += 2;
            }

            return totalDamage;
        }
    }
}
