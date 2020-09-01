using System;
using System.Collections.Generic;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.Managers.Events.EventProcessor;
using ECSRogue.Managers.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.Managers
{
    public class GameInstance
    {
        private readonly Dictionary<Type, IManager> managers = new Dictionary<Type, IManager>();

        public GameInstance(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            var systemManager = new SystemManager();
            var entityManager = new EntityManager();

            var handlerManager = new HandlerManager(systemManager, entityManager);
            var turnManager = new TurnManager(entityManager);
            var eventManager = new EventManager(new List<IEventProcessor>
                {new GameEventProcessor(), new UIEventProcessor()});
            var uiManager = new UIManager(graphicsDevice, entityManager,
                new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height),
                new List<SpriteFont> {contentManager.Load<SpriteFont>("spritefont")});
            var levelManager = new LevelManager();

            entityManager.EntityAdded += systemManager.OnEntityAdded;
            entityManager.EntityRemoved += systemManager.OnEntityRemoved;

            managers.Add(eventManager.GetType(), eventManager);
            managers.Add(uiManager.GetType(), uiManager);
            managers.Add(handlerManager.GetType(), handlerManager);
            managers.Add(systemManager.GetType(), systemManager);
            managers.Add(entityManager.GetType(), entityManager);
            managers.Add(turnManager.GetType(), turnManager);
            managers.Add(levelManager.GetType(), levelManager);
        }

        public void Update(GameTime gameTime)
        {
            var currentTurn = GetManager<TurnManager>().currentTurn;
            var eventQueue = GetManager<EventManager>().eventQueue;

            foreach (var manager in managers.Values) manager.Update(gameTime, currentTurn, eventQueue);
        }

        public T GetManager<T>() where T : IManager
        {
            return (T) managers[typeof(T)];
        }
    }
}