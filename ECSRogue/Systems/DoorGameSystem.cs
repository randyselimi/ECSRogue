using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    internal class DoorSystem : GameSystem
    {
        public DoorSystem() : base(SystemTypes.UpdateSystem)
        {
        }

        public override void Update(PartisInstance instance)
        {
            foreach (var gameEvent in instance.GetEvents<GameEvent>())
            {
                //Entity will attempt to equip item to valid slot
                if (gameEvent.EventType == GameEvents.Collide)
                {
                    var collisionEvent = gameEvent as CollisionEvent;
                    if (collisionEvent.CollidedEntity.HasComponent<Door>())
                        collisionEvent.CollidedEntity.alive = false;
                }
            }
        }
    }
}