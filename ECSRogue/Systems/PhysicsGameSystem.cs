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
    internal class PhysicsSystem : GameSystem
    {
        public PhysicsSystem() : base(SystemTypes.UpdateSystem)
        {
        }

        public override void Update(PartisInstance instance)
        {
            foreach (var movingEntity in instance.GetEntitiesByIndexes(new TypeIndexer(typeof(Velocity)), new TypeIndexer(typeof(Collideable))))
            {
                //If the entity has a velocity of zero, then it's not moving so continue
                if (movingEntity.GetComponent<Velocity>().velocity == Vector2.Zero) continue;

                //New Position of moving entity will be the sum of its Position and velocity vectors
                Vector2 newPosition = Vector2.Add(
                    movingEntity.GetComponent<Position>().position, movingEntity.GetComponent<Velocity>().velocity);

                //But first check if there are collideable entities at that new Position
                var collidingEntity = instance.GetEntitiesByIndexes(new LevelIndexer(movingEntity.GetComponent<LevelPosition>().CurrentLevel), new PositionIndexer(newPosition), new TypeIndexer(typeof(Collideable)))
                    .FirstOrDefault();
                if (collidingEntity != null)
                {
                    //Create a collision event, registering the moving entity and the collided entity
                    instance.AddEvent(new CollisionEvent(movingEntity, collidingEntity));
                }
                else
                {
                    movingEntity.GetComponent<Position>().position = newPosition;
                }

                movingEntity.GetComponent<Velocity>().velocity = new Vector2(0);
            }
        }
    }
}