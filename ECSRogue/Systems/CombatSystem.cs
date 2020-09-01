using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    internal class CombatSystem : GameSystem
    {
        public CombatSystem()
        {
            entityFilter.AddComponentToFilter<Health>();
            entityFilter.AddComponentToFilter<Position>();
        }

        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            for (var i = 0; i < eventQueue.Count; i++)
                if (eventQueue[i].GetType() == typeof(GameEvent))
                {
                    var gameEvent = (GameEvent) eventQueue[i];

                    if (gameEvent.eventType == "Attack_Left")
                    {
                        var defender = filteredEntities.Values.FirstOrDefault(x =>
                            x.GetComponent<Position>().position - new Vector2(-1, 0) ==
                            gameEvent.entities[0].GetComponent<Position>().position);
                        var attacker = gameEvent.entities[0];

                        if (defender != null)
                        {
                            var damage = CalculateDamageDealt(attacker);
                            eventQueue.Add(new GameEvent("Recieve_Damage", new List<Entity> {defender, attacker},
                                damage));
                        }

                        attacker.GetComponent<Turn>().takenTurn = true;
                    }

                    if (gameEvent.eventType == "Attack_Right")
                    {
                        var defender = filteredEntities.Values.FirstOrDefault(x =>
                            x.GetComponent<Position>().position - new Vector2(1, 0) ==
                            gameEvent.entities[0].GetComponent<Position>().position);
                        var attacker = gameEvent.entities[0];

                        if (defender != null)
                        {
                            var damage = CalculateDamageDealt(attacker);
                            eventQueue.Add(new GameEvent("Recieve_Damage", new List<Entity> {defender, attacker},
                                damage));
                        }

                        attacker.GetComponent<Turn>().takenTurn = true;
                    }


                    if (gameEvent.eventType == "Attack_Up")
                    {
                        var defender = filteredEntities.Values.FirstOrDefault(x =>
                            x.GetComponent<Position>().position - new Vector2(0, -1) ==
                            gameEvent.entities[0].GetComponent<Position>().position);
                        var attacker = gameEvent.entities[0];

                        if (defender != null)
                        {
                            var damage = CalculateDamageDealt(attacker);
                            eventQueue.Add(new GameEvent("Recieve_Damage", new List<Entity> {defender, attacker},
                                damage));
                        }

                        attacker.GetComponent<Turn>().takenTurn = true;
                    }

                    if (gameEvent.eventType == "Attack_Down")
                    {
                        var defender = filteredEntities.Values.FirstOrDefault(x =>
                            x.GetComponent<Position>().position - new Vector2(0, 1) ==
                            gameEvent.entities[0].GetComponent<Position>().position);
                        var attacker = gameEvent.entities[0];

                        if (defender != null)
                        {
                            var damage = CalculateDamageDealt(attacker);
                            eventQueue.Add(new GameEvent("Recieve_Damage", new List<Entity> {defender, attacker},
                                damage));
                        }

                        attacker.GetComponent<Turn>().takenTurn = true;
                    }
                }
        }

        public int CalculateDamageDealt(Entity damageDealer)
        {
            var totalDamage = 0;

            var equippedWeapon = damageDealer.GetComponent<Equipment>().equipment.Where(x => x.slot == "Wieldable")
                .FirstOrDefault().entity;

            if (equippedWeapon != null)
                totalDamage += equippedWeapon.GetComponent<Damage>().damageValue;

            else
                totalDamage += 2;

            return totalDamage;
        }
    }
}