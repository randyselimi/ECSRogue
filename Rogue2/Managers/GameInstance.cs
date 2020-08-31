using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rogue2.Handlers.Events.EventProcessor;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System;
using System.Collections.Generic;

namespace Rogue2.Managers
{
    public class GameInstance
    {
        private Dictionary<Type, IManager> managers = new Dictionary<Type, IManager>();

        public GameInstance(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            SystemManager systemManager = new SystemManager();
            EntityManager entityManager = new EntityManager();

            HandlerManager handlerManager = new HandlerManager(systemManager, entityManager);
            TurnManager turnManager = new TurnManager(entityManager);
            EventManager eventManager = new EventManager(new List<IEventProcessor>() { new GameEventProcessor(), new UIEventProcessor() });
            UIManager uiManager = new UIManager(graphicsDevice, entityManager, new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), new List<SpriteFont>() { contentManager.Load<SpriteFont>("spritefont") });

            entityManager.EntityAdded += systemManager.OnEntityAdded;
            entityManager.EntityRemoved += systemManager.OnEntityRemoved;

            managers.Add(eventManager.GetType(), eventManager);
            managers.Add(uiManager.GetType(), uiManager);
            managers.Add(handlerManager.GetType(), handlerManager);
            managers.Add(systemManager.GetType(), systemManager);
            managers.Add(entityManager.GetType(), entityManager);
            managers.Add(turnManager.GetType(), turnManager);

        }
        public void Update(GameTime gameTime)
        {
            int currentTurn = GetManager<TurnManager>().currentTurn;
            List<IEvent> eventQueue = GetManager<EventManager>().eventQueue;

            foreach (var manager in managers.Values)
            {
                manager.Update(gameTime, currentTurn, eventQueue);
            }
        }

        public T GetManager<T>() where T : IManager
        {
            return (T)managers[typeof(T)];
        }
    }
}
