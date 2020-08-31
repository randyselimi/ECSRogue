using Microsoft.Xna.Framework;
using Rogue2.Managers;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System.Collections.Generic;

namespace Rogue2.Handlers
{
    abstract public class Handler
    {
        public EntityManager entityManager;
        public SystemManager systemManager;

        public Handler()
        {
        }

        abstract public void Update(GameTime gameTime, List<IEvent> eventQueue);
    }
}
