using System;
using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Data;
using ECSRogue.Factories.EntityFactory;
using ECSRogue.Factories.LevelFactory;
using ECSRogue.Handlers.Input;
using ECSRogue.Handlers.Rendering;
using ECSRogue.Handlers.Rendering.RenderComponent;
using ECSRogue.Managers;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Levels;
using ECSRogue.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private GameInstance gameInstance;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Testing Logic
            gameInstance = new GameInstance(GraphicsDevice, Content);
            gameInstance.Initialize();

            //inputHandler.player =
            //    gameInstance.GetManager<EntityManager>().GetEntitiesByComponent<Player>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            gameInstance.LoadContent();
            // TODO: use this.Content to load your game content here

        }

        protected override void Update(GameTime gameTime)
        {
            gameInstance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}