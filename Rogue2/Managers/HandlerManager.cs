using Microsoft.Xna.Framework;
using Rogue2.Handlers;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System;
using System.Collections.Generic;

namespace Rogue2.Managers
{
    class HandlerManager : IManager
    {
        SystemManager systemManager;
        EntityManager entityManager;
        private Dictionary<Type, Handler> handlers;

        public HandlerManager(SystemManager systemManager, EntityManager entityManager)
        {
            this.systemManager = systemManager;
            this.entityManager = entityManager;

            handlers = new Dictionary<Type, Handler>();
        }
        public T GetHandler<T>() where T : Handler
        {
            Handler handlerToGet;
            handlers.TryGetValue(typeof(T), out handlerToGet);
            return (T)handlerToGet;
        }
        public void InitalizeHandler(Handler handlerToInitalize)
        {
            handlerToInitalize.entityManager = entityManager;
            handlerToInitalize.systemManager = systemManager;

            handlers.Add(handlerToInitalize.GetType(), handlerToInitalize);
        }
        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue)
        {
            foreach (Handler handler in handlers.Values)
            {
                handler.Update(gameTime, eventQueue);
            }
        }
    }
}
