using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue2.Components;
using Rogue2.Data;
using Rogue2.Factories;
using Rogue2.Handlers;
using Rogue2.Helpers.CollisionDetectionHelper;
using Rogue2.Managers;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Levels;
using Rogue2.Managers.Rendering;
using Rogue2.Managers.Rendering.RenderComponent;
using Rogue2.Systems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rogue2
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Managers.GameInstance gameInstance;
        Random random = new Random();
        Level testLevel;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            EntityPresets.Load(Content);
            //Testing Logic
            gameInstance = new Managers.GameInstance(GraphicsDevice, Content);
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            List<IRenderComponent> renderComponents = new List<IRenderComponent>() { new EntityRenderComponent(), gameInstance.GetManager<UIManager>().renderComponent};

            gameInstance.GetManager<HandlerManager>().InitalizeHandler(new RenderHandler(_spriteBatch, renderComponents));
            gameInstance.GetManager<HandlerManager>().InitalizeHandler(new InputHandler());

            gameInstance.GetManager<SystemManager>().AddRenderSystem(new RenderSystem(gameInstance.GetManager<HandlerManager>().GetHandler<RenderHandler>()));
            gameInstance.GetManager<SystemManager>().AddInputSystem(new MovementSystem());
            gameInstance.GetManager<SystemManager>().AddInputSystem(new CameraInputSystem());
            gameInstance.GetManager<SystemManager>().AddUpdateSystem(new PhysicsSystem(new TileBasedCollisionDetection()));
            gameInstance.GetManager<SystemManager>().AddUpdateSystem(new CombatSystem());
            gameInstance.GetManager<SystemManager>().AddUpdateSystem(new EquipmentSystem());
            gameInstance.GetManager<SystemManager>().AddUpdateSystem(new InventorySystem());
            gameInstance.GetManager<SystemManager>().AddUpdateSystem(new DoorSystem());
            gameInstance.GetManager<SystemManager>().AddUpdateSystem(new DamageSystem());
            gameInstance.GetManager<SystemManager>().AddUpdateSystem(new MonsterAISystem(gameInstance.GetManager<EntityManager>()));



            EntityFactory PlayerFactory = new EntityFactory(EntityPresets.entityPresets["player"]);

            DungeonLevelFactory testLevelFactory = new DungeonLevelFactory(gameInstance.GetManager<EntityManager>(), random);

            testLevel = testLevelFactory.GenerateLevel(45, 45);

            EntityFactory MonsterFactory = new EntityFactory(EntityPresets.entityPresets["monster"]);

            var spawnPositions = testLevel.levelFloorTiles.Where(x => testLevel.GetTilesByPosition(x.Key) == null).ToList();
            
            for (int i = 0; i < 5; i++)
            {
                Entity monster = gameInstance.GetManager<EntityManager>().CreateEntity(MonsterFactory);
                var spawnPosition = spawnPositions[random.Next(0, spawnPositions.Count)];
                monster.GetComponent<Position>().position = spawnPosition.Value.GetComponent<Position>().position;
                spawnPositions.Remove(spawnPosition);
            }

            EntityFactory SwordFactory = new EntityFactory(EntityPresets.entityPresets["sword"]);

            for (int i = 0; i < 3; i++)
            {
                Entity sword = gameInstance.GetManager<EntityManager>().CreateEntity(SwordFactory);
                var spawnPosition = spawnPositions[random.Next(0, spawnPositions.Count)];
                sword.GetComponent<Position>().position = spawnPosition.Value.GetComponent<Position>().position;
                spawnPositions.Remove(spawnPosition);
            }

            EntityFactory SpearFactory = new EntityFactory(EntityPresets.entityPresets["spear"]);

            for (int i = 0; i < 3; i++)
            {
                Entity spear = gameInstance.GetManager<EntityManager>().CreateEntity(SpearFactory);
                var spawnPosition = spawnPositions[random.Next(0, spawnPositions.Count)];
                spear.GetComponent<Position>().position = spawnPosition.Value.GetComponent<Position>().position;
                spawnPositions.Remove(spawnPosition);
            }

            Entity player = gameInstance.GetManager<EntityManager>().CreateEntity(PlayerFactory);
            var playerSpawnPosition = spawnPositions[random.Next(0, spawnPositions.Count)];
            player.GetComponent<Position>().position = playerSpawnPosition.Value.GetComponent<Position>().position;

            EntityFactory CameraFactory = new EntityFactory(EntityPresets.entityPresets["camera"]);

            gameInstance.GetManager<EntityManager>().CreateEntity(CameraFactory).GetComponent<Position>().position = new Vector2(0, 0);

            gameInstance.GetManager<UIManager>().camera = gameInstance.GetManager<EntityManager>().GetEntitiesByComponent<Camera>().FirstOrDefault();
            gameInstance.GetManager<UIManager>().player = gameInstance.GetManager<EntityManager>().GetEntitiesByComponent<Player>().FirstOrDefault();



            gameInstance.GetManager<UIManager>().CreateDefaultUI();

            gameInstance.GetManager<HandlerManager>().GetHandler<InputHandler>().player = gameInstance.GetManager<EntityManager>().GetEntitiesByComponent<Player>();
            base.Initialize();
        }

        protected override void LoadContent()
        {


            

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            

            //List<Component> componentsToAdd = new List<Component> { new Position(new Vector2(random.Next(0, GraphicsDeviceManager.DefaultBackBufferWidth), random.Next(0, GraphicsDeviceManager.DefaultBackBufferHeight))), new Sprite(Content.Load<Texture2D>("Floor")) };
            //gameInstance.entityManager.AddEntity(componentsToAdd);

            //componentsToAdd = new List<Component> { new Position(new Vector2(random.Next(0, GraphicsDeviceManager.DefaultBackBufferWidth), random.Next(0, GraphicsDeviceManager.DefaultBackBufferHeight))), new Sprite(Content.Load<Texture2D>("Floor")) };
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
