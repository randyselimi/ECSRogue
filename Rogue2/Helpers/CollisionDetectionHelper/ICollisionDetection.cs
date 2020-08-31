using Rogue2.Managers.Entities;

namespace Rogue2.Helpers.CollisionDetectionHelper
{
    interface ICollisionDetection
    {
        bool CheckCollision(Entity collidingEntity, Entity collidedEntity);
    }
}
