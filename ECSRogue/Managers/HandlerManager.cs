using System;
using System.Collections.Generic;
using ECSRogue.Handlers;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers
{
    internal class HandlerManager : IManager
    {
        private readonly EntityManager entityManager;
        private readonly Dictionary<Type, Handler> handlers;
        private readonly SystemManager systemManager;

        public HandlerManager(SystemManager systemManager, EntityManager entityManager)
        {
            this.systemManager = systemManager;
            this.entityManager = entityManager;

            handlers = new Dictionary<Type, Handler>();
        }

        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue)
        {
            foreach (var handler in handlers.Values) handler.Update(gameTime, eventQueue);
        }

        public T GetHandler<T>() where T : Handler
        {
            Handler handlerToGet;
            handlers.TryGetValue(typeof(T), out handlerToGet);
            return (T) handlerToGet;
        }

        public void InitalizeHandler(Handler handlerToInitalize)
        {
            handlerToInitalize.entityManager = entityManager;
            handlerToInitalize.systemManager = systemManager;

            handlers.Add(handlerToInitalize.GetType(), handlerToInitalize);
        }
    }
}