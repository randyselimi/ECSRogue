using System;
using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Data;
using ECSRogue.Factories;
using ECSRogue.Factories.EntityFactory;
using ECSRogue.Factories.LevelFactory;
using ECSRogue.Handlers.Input;
using ECSRogue.Handlers.Rendering;
using ECSRogue.Handlers.Rendering.RenderComponent;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.Managers.Events.EventProcessor;
using ECSRogue.Managers.Levels;
using ECSRogue.Partis;
using ECSRogue.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.Managers
{
    public class GameInstance
    {
        public PartisInstance partisInstance;
        private InputHandler input;
        private RenderHandler render;
        private UIHandler ui;

        private LevelManager levelManager;
        public LogManager logManager;

        private GraphicsDevice graphicsDevice;
        private ContentManager contentManager;
        private SpriteBatch spriteBatch;

        private bool Test = false;

        public GameInstance(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            spriteBatch = new SpriteBatch(graphicsDevice);

            partisInstance = new PartisInstance();

            ui = new UIHandler(new Vector2(graphicsDevice.Viewport.Width,
                graphicsDevice.Viewport.Height));
            input = new InputHandler();
            render = new RenderHandler(spriteBatch);
        }

        public void Initialize()
        {
            levelManager = new LevelManager();
            logManager = new LogManager();

            //initialize partis
            partisInstance.Initialize();

            ContentDefinitionLoader s = new ContentDefinitionLoader(contentManager);
            EntityDefinitionLoader e = new EntityDefinitionLoader(s.LoadSpriteDefinitions("test"));

            partisInstance.Load(e.LoadEntityDefinitions("Test"));
            var player = partisInstance.CreateEntity("Player");
            var camera = partisInstance.CreateEntity("Camera");

            //initialize renderer
            render.Initialize(new List<IRenderProcessor>()
                {new EntityRenderProcessor(), new UIRenderProcessor(graphicsDevice)});

            partisInstance.AddSystem(new MonsterAISystem(levelManager));
            partisInstance.AddSystem(new MovementSystem());
            partisInstance.AddSystem(new CameraInputSystem());
            partisInstance.AddSystem(new PhysicsSystem());
            partisInstance.AddSystem(new CombatSystem());
            partisInstance.AddSystem(new EquipmentSystem());
            partisInstance.AddSystem(new InventorySystem());
            partisInstance.AddSystem(new DoorSystem());
            partisInstance.AddSystem(new DamageSystem());
            partisInstance.AddSystem(new RenderSystem(render, levelManager));
            partisInstance.AddSystem(new TurnSystem());
            partisInstance.AddSystem(new LevelChangeSystem(levelManager));

            //initialize ui
            ui.Initialize(this, render.GetRenderProcessor<UIRenderProcessor>(),
                contentManager.Load<SpriteFont>("spritefont"));


        }

        public void LoadContent()
        {
        }

        public void TestInstance()
        {
            levelManager.GenerateLevel(0, new DungeonLevelFactory(new Random()), partisInstance);
            //ui.CreateDefaultUI();
        }

        public void Update(GameTime gameTime)
        {
            if (Test == false)
            {
                TestInstance();
                Test = true;
            }

            logManager.Update(partisInstance);
            input.Update(gameTime, partisInstance);
            ui.Update(partisInstance);
            render.Draw(gameTime, partisInstance);
            partisInstance.Update();
        }
    }
}