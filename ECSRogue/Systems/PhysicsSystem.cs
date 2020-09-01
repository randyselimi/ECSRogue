using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Helpers.CollisionDetectionHelper;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    internal class PhysicsSystem : GameSystem
    {
        private EntityManager _entityManager;
        private double timeSinceUpdate;

        public PhysicsSystem(ICollisionDetection collisionDetection, EntityManager entityManager)
        {
            this.collisionDetection = collisionDetection;
            _entityManager = entityManager;

            entityFilter.AddComponentToFilter<Position>();
        }

        private ICollisionDetection collisionDetection { get; }

        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            timeSinceUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
            var iterations = 0;

            foreach (var movingEntity in filteredEntities.Values.Where(x => x.HasComponent<Velocity>()))
            {
                if (movingEntity.GetComponent<Velocity>().velocity == Vector2.Zero) continue;

                var collided = false;

                foreach (var entity in filteredEntities.Values.Where(x => x.HasComponent<Collideable>()))
                    if (collisionDetection.CheckCollision(movingEntity, entity))
                    {
                        collided = true;

                        eventQueue.Add(new GameEvent("Collision", new List<Entity> {movingEntity, entity}));
                    }

                if (collided == false)
                    movingEntity.GetComponent<Position>().position = Vector2.Add(
                        movingEntity.GetComponent<Position>().position, movingEntity.GetComponent<Velocity>().velocity);

                movingEntity.GetComponent<Velocity>().velocity = new Vector2(0);
                iterations++;
            }
        }
    }
}