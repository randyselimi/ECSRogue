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
                    string attackLogMessage = "";
                    var attackEvent = gameEvent as AttackEvent;

                    var attacker = attackEvent.AttackingEntity;
                    var defender = instance.GetEntitiesByIndexes(new PositionIndexer(attackEvent.AttackingPosition), new LevelIndexer(attackEvent.AttackingLevel), new TypeIndexer(typeof(Health))).SingleOrDefault();

                    //I know this is a slow and inefficient process
                    attackLogMessage += attacker.GetComponent<Name>().NameSingular + " attacks ";
                    if (defender != null)
                    {
                        int damage = CalculateDamageDealt(attacker);
                        instance.AddEvent(new DamageEvent(defender, damage));
                        attackLogMessage += "a " + defender.GetComponent<Name>().NameSingular + " for " + damage + " points of damage";
                    }
                    else { attackLogMessage += "the air"; }

                    attacker.GetComponent<Turn>().takenTurn = true;

                    instance.AddEvent(new LogEvent(attackLogMessage));
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