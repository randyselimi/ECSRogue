using Microsoft.Xna.Framework;
using Rogue2.Components;
using Rogue2.Helpers.CollisionDetectionHelper;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System.Collections.Generic;
using System.Linq;

namespace Rogue2.Systems
{
    class PhysicsSystem : GameSystem
    {
        ICollisionDetection collisionDetection { get; set; }
        double timeSinceUpdate = 0;
        public PhysicsSystem(ICollisionDetection collisionDetection)
        {
            this.collisionDetection = collisionDetection;

            entityFilter.AddComponentToFilter<Position>();
            entityFilter.AddComponentToFilter<Collideable>();
        }

        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            timeSinceUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;


            foreach (Entity movingEntity in filteredEntities.Values.Where(x => x.GetComponent<Velocity>() != null))
            {
                bool collided = false;
                foreach (Entity entity in filteredEntities.Values)
                {
                    if (collisionDetection.CheckCollision(movingEntity, entity))
                    {
                        collided = true;
                        eventQueue.Add(new GameEvent("Collision", new List<Entity>() { movingEntity, entity }));
                    }
                }
                if (collided == false)
                {
                    movingEntity.GetComponent<Position>().position = Vector2.Add(movingEntity.GetComponent<Position>().position, movingEntity.GetComponent<Velocity>().velocity);
                }

                movingEntity.GetComponent<Velocity>().velocity = new Vector2(0);
            }
        }
    }
}
