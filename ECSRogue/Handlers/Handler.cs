using System.Collections.Generic;
using ECSRogue.Managers;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Handlers
{
    public abstract class Handler
    {
        public EntityManager entityManager;
        public SystemManager systemManager;

        public abstract void Update(GameTime gameTime, List<IEvent> eventQueue);
    }
}