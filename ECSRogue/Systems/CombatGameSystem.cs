using System;
using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Managers;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    internal class CombatSystem : GameSystem
    {
        public CombatSystem() : base(SystemTypes.UpdateSystem)
        {
        }

        public override void Update(PartisInstance instance)
        {
            foreach (var gameEvent in instance.GetEvents<GameEvent>())
            {
                if (gameEvent.EventType == GameEvents.Attack)
                {
                    var attackEvent = gameEvent as AttackEvent;

                    var attacker = attackEvent.AttackingEntity;
                    var defender = instance.GetEntitiesByIndexes(new PositionIndexer(attackEvent.AttackingPosition), new LevelIndexer(attackEvent.AttackingLevel), new TypeIndexer(typeof(Health))).SingleOrDefault();

                    if (defender != null)
                    {
                        int damage = CalculateDamageDealt(attacker);
                        instance.AddEvent(new DamageEvent(defender, damage));
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