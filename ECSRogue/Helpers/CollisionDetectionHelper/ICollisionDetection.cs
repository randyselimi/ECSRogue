using ECSRogue.Managers.Entities;

namespace ECSRogue.Helpers.CollisionDetectionHelper
{
    internal interface ICollisionDetection
    {
        bool CheckCollision(Entity collidingEntity, Entity collidedEntity);
    }
}