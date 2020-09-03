using System;
using ECSRogue.Components;
using ECSRogue.Managers.Entities;
using Microsoft.Xna.Framework;

namespace ECSRogue.Helpers.CollisionDetectionHelper
{
    internal class TileBasedCollisionDetection : ICollisionDetection
    {
        //refactor to not care about components
        public bool CheckCollision(Entity collidingEntity, Entity collidedEntity)
        {
            if (collidingEntity.Id == collidedEntity.Id) return false;

            Vector2 collidingEntityVelocity;
            Vector2 collidingEntityPosition;
            Vector2 collidedEntityPosition;
            try
            {
                collidingEntityVelocity = collidingEntity.GetComponent<Velocity>().velocity;
                collidingEntityPosition = collidingEntity.GetComponent<Position>().position;
                collidedEntityPosition = collidedEntity.GetComponent<Position>().position;
            }
            catch (Exception)
            {
                Console.WriteLine("Entity does not have a Position and velocity component");
                throw;
            }

            var collidingEntityFinalPosition = new Vector2();
            collidingEntityFinalPosition.X = collidingEntityVelocity.X + collidingEntityPosition.X;
            collidingEntityFinalPosition.Y = collidingEntityVelocity.Y + collidingEntityPosition.Y;

            if (collidingEntityFinalPosition.X == collidedEntityPosition.X &&
                collidingEntityFinalPosition.Y == collidedEntityPosition.Y) return true;

            return false;
        }

        private static void RectangleIntersects(Rectangle a, Rectangle b)
        {
        }
    }
}