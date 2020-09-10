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
using ECSRogue.Helpers.CollisionDetectionHelper;
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
        private SpriteBatch _spriteBatch;
        private GameInstance gameInstance;
        private readonly Random random = new Random();
        private Level testLevel;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            SpriteDefinitionLoader s = new SpriteDefinitionLoader(Content);
            EntityDefinitionLoader e = new EntityDefinitionLoader(s.LoadSpriteDefinitions("s"));

            e.LoadEntityDefinitions("EntityDefinitions.xml");
            //Testing Logic
            gameInstance = new GameInstance(GraphicsDevice, Content);
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            var renderComponents = new List<IRenderComponent>
                {new EntityRenderComponent(), gameInstance.GetManager<UIManager>().renderComponent};

            gameInstance.GetManager<HandlerManager>()
                .InitalizeHandler(new RenderHandler(_spriteBatch, renderComponents));
            gameInstance.GetManager<HandlerManager>().InitalizeHandler(new InputHandler());

            gameInstance.GetManager<SystemManager>()
                .AddRenderSystem(
                    new RenderSystem(gameInstance.GetManager<HandlerManager>().GetHandler<RenderHandler>()));


            var monsterAiSystem = new MonsterAISystem(gameInstance.GetManager<EntityManager>(),
                gameInstance.GetManager<LevelManager>());

            gameInstance.GetManager<SystemManager>().AddInputSystem(monsterAiSystem);

            gameInstance.GetManager<SystemManager>().AddInputSystem(new MovementSystem());
            gameInstance.GetManager<SystemManager>().AddInputSystem(new CameraInputSystem());
            gameInstance.GetManager<SystemManager>().AddUpdateSystem(
                new PhysicsSystem(new TileBasedCollisionDetection(), gameInstance.GetManager<EntityManager>()));
            gameInstance.GetManager<SystemManager>().AddUpdateSystem(new CombatSystem());
            gameInstance.GetManager<SystemManager>().AddUpdateSystem(new EquipmentSystem());
            gameInstance.GetManager<SystemManager>().AddUpdateSystem(new InventorySystem());
            gameInstance.GetManager<SystemManager>().AddUpdateSystem(new DoorSystem());
            gameInstance.GetManager<SystemManager>().AddUpdateSystem(new DamageSystem());

           

            gameInstance.GetManager<LevelManager>()
                .GenerateLevel(new DungeonLevelFactory(gameInstance.GetManager<EntityManager>(), random));

            testLevel = gameInstance.GetManager<LevelManager>().GetCurrentLevel();

            var spawnPositions = testLevel.levelFloorTiles.Where(x => testLevel.GetTilesByPosition(x.Key) == null)
                .ToList();

            for (var i = 0; i < 5; i++)
            {
                var monster = gameInstance.GetManager<EntityManager>()
                    .CreateEntity("Goblin_Grunt");
                var spawnPosition = spawnPositions[random.Next(0, spawnPositions.Count)];
                monster.GetComponent<Position>().position =
                    spawnPosition.Value.GetComponent<Position>().position;
                spawnPositions.Remove(spawnPosition);
            }


            for (var i = 0; i < 3; i++)
            {
                var sword = gameInstance.GetManager<EntityManager>().CreateEntity("Iron_Longsword");
                var spawnPosition = spawnPositions[random.Next(0, spawnPositions.Count)];
                sword.GetComponent<Position>().position =
                    spawnPosition.Value.GetComponent<Position>().position;
                spawnPositions.Remove(spawnPosition);
            }


            for (var i = 0; i < 3; i++)
            {
                var spear = gameInstance.GetManager<EntityManager>().CreateEntity("Wooden_Club");
                var spawnPosition = spawnPositions[random.Next(0, spawnPositions.Count)];
                spear.GetComponent<Position>().position =
                    spawnPosition.Value.GetComponent<Position>().position;
                spawnPositions.Remove(spawnPosition);
            }

            var player = gameInstance.GetManager<EntityManager>().CreateEntity("Player");
            var playerSpawnPosition = spawnPositions[random.Next(0, spawnPositions.Count)];
            player.GetComponent<Position>().position =
                playerSpawnPosition.Value.GetComponent<Position>().position;


            gameInstance.GetManager<EntityManager>()
                .CreateEntity("Camera").GetComponent<Position>().position = new Vector2(0, 0);

            var camera = gameInstance.GetManager<EntityManager>()
                .GetEntitiesByComponent<Camera>().FirstOrDefault();
            gameInstance.GetManager<UIManager>().camera = camera;
            gameInstance.GetManager<UIManager>().player = gameInstance.GetManager<EntityManager>()
                .GetEntitiesByComponent<Player>().FirstOrDefault();

            gameInstance.GetManager<UIManager>().CreateDefaultUI();

            monsterAiSystem.player = player;

            gameInstance.GetManager<HandlerManager>().GetHandler<InputHandler>().player =
                gameInstance.GetManager<EntityManager>().GetEntitiesByComponent<Player>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here

        }

        protected override void Update(GameTime gameTime)
        {
            //List<Component> componentsToAdd = new List<Component> { new Position(new Vector2(random.Next(0, GraphicsDeviceManager.DefaultBackBufferWidth), random.Next(0, GraphicsDeviceManager.DefaultBackBufferHeight))), new Sprite(Content.LoadEntityDefinitions<Texture2D>("Floor")) };
            //gameInstance.entityManager.AddEntity(componentsToAdd);

            //componentsToAdd = new List<Component> { new Position(new Vector2(random.Next(0, GraphicsDeviceManager.DefaultBackBufferWidth), random.Next(0, GraphicsDeviceManager.DefaultBackBufferHeight))), new Sprite(Content.LoadEntityDefinitions<Texture2D>("Floor")) };
            //gameInstance.entityManager.AddEntity(componentsToAdd);

            //gameInstance.entityManager.RemoveEntity(random.Next(0, gameInstance.entityManager.ID));

            gameInstance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}