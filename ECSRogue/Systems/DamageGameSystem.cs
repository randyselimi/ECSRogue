using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Managers;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    internal class DamageSystem : GameSystem
    {
        public DamageSystem() : base(SystemTypes.UpdateSystem)
        {

        }
        public override void Update(PartisInstance instance) 
        {
            foreach (var gameEvent in instance.GetEvents<GameEvent>())
            {
                if (gameEvent.EventType == GameEvents.Damage)
                {
                    var damageEvent = gameEvent as DamageEvent;

                    var damagedEntity = damageEvent.DamagedEntity;

                    damagedEntity.GetComponent<Health>().healthPoints -= (int)damageEvent.DamageAmount;

                    //TODO this should be moved into its own system!
                    if (damagedEntity.GetComponent<Health>().healthPoints <= 0) damagedEntity.alive = false;
                    
                }
            }

        }
    }
}