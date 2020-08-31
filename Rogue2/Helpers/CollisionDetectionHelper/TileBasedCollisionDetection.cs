using Microsoft.Xna.Framework;
using Rogue2.Components;
using Rogue2.Managers.Entities;
using System;

namespace Rogue2.Helpers.CollisionDetectionHelper
{
    class TileBasedCollisionDetection : ICollisionDetection
    {
        //refactor to not care about components
        public bool CheckCollision(Entity collidingEntity, Entity collidedEntity)
        {
            if (collidingEntity.ID == collidedEntity.ID)
            {
                return false;
            }

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
                Console.WriteLine("Entity does not have a position and velocity component");
                throw;
            }

            Vector2 collidingEntityFinalPosition = new Vector2();
            collidingEntityFinalPosition.X = collidingEntityVelocity.X + collidingEntityPosition.X;
            collidingEntityFinalPosition.Y = collidingEntityVelocity.Y + collidingEntityPosition.Y;

            if (collidingEntityFinalPosition.X == collidedEntityPosition.X && collidingEntityFinalPosition.Y == collidedEntityPosition.Y)
            {
                return true;
            }

            return false;
        }

        static void RectangleIntersects(Rectangle a, Rectangle b)
        {

        }
    }
}
