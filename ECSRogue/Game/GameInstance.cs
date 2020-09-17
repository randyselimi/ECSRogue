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
        private PartisInstance instance;
        private InputHandler input;
        private RenderHandler render;
        private UIHandler ui;

        private LevelManager levelManager;

        private GraphicsDevice graphicsDevice;
        private ContentManager contentManager;
        private SpriteBatch spriteBatch;

        private bool Test = false;

        public GameInstance(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            spriteBatch = new SpriteBatch(graphicsDevice);

            instance = new PartisInstance();

            ui = new UIHandler(new Vector2(graphicsDevice.Viewport.Width,
                graphicsDevice.Viewport.Height));
            input = new InputHandler();
            render = new RenderHandler(spriteBatch);
        }

        public void Initialize()
        {
            levelManager = new LevelManager();

            //initialize partis
            instance.Initialize();

            ContentDefinitionLoader s = new ContentDefinitionLoader(contentManager);
            EntityDefinitionLoader e = new EntityDefinitionLoader(s.LoadSpriteDefinitions("test"));

            instance.Load(e.LoadEntityDefinitions("Test"));
            var player = instance.CreateEntity("Player");
            var camera = instance.CreateEntity("Camera");

            //initialize renderer
            render.Initialize(new List<IRenderProcessor>()
                {new EntityRenderProcessor(), new UIRenderProcessor(graphicsDevice)});

            instance.AddSystem(new MonsterAISystem(levelManager));
            instance.AddSystem(new MovementSystem());
            instance.AddSystem(new CameraInputSystem());
            instance.AddSystem(new PhysicsSystem());
            instance.AddSystem(new CombatSystem());
            instance.AddSystem(new EquipmentSystem());
            instance.AddSystem(new InventorySystem());
            instance.AddSystem(new DoorSystem());
            instance.AddSystem(new DamageSystem());
            instance.AddSystem(new RenderSystem(render));
            instance.AddSystem(new TurnSystem());

            //initialize ui
            ui.Initialize(instance, render.GetRenderProcessor<UIRenderProcessor>(),
                contentManager.Load<SpriteFont>("spritefont"));


        }

        public void LoadContent()
        {
        }

        public void TestInstance()
        {
            levelManager.GenerateLevel(new DungeonLevelFactory(new Random()), instance);
            //ui.CreateDefaultUI();
        }

        public void Update(GameTime gameTime)
        {
            if (Test == false)
            {
                TestInstance();
                Test = true;
            }


            input.Update(gameTime, instance);
            ui.Update(instance);
            render.Draw(gameTime, instance);
            instance.Update();
        }
    }
}