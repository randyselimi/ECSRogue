using System;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Data;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Levels;
using Microsoft.Xna.Framework;
using Pathfinding;

namespace ECSRogue.Factories.LevelFactory
{
    internal class DungeonLevelFactory : ILevelFactory
    {
        private const int MapSize = 100;

        private readonly Random random;

        public DungeonLevelFactory(EntityManager entityManager, Random random)
        {
            this.entityManager = entityManager;
            this.random = random;
        }

        private EntityManager entityManager { get; }

        public Level GenerateLevel(int maxWidth, int maxHeight)
        {
            var numberOfAttempts = 10000;
            var generatedLevel = new Level(maxWidth, maxHeight);

            //Step one, create rooms that fall within level bounds and do not intersect other rooms
            for (var i = 0; i < numberOfAttempts; i++) CreateRoom(generatedLevel, 10, 10);

            foreach (var room in generatedLevel.levelRooms) CreateCorridor(room, generatedLevel);


            FillInLevel(generatedLevel);

            return generatedLevel;
        }

        public void CreateRoom(Level generatedLevel, int roomMaxWidth, int roomMaxHeight)
        {
            //minimum room size is 5
            if (roomMaxHeight < 5) roomMaxHeight = 5;
            if (roomMaxWidth < 5) roomMaxWidth = 5;

            //if (roomMinDoors >= roomMaxHeight = roomMaxWidth - 4)
            //{
            //    doorAmountMax = room.wallEntity.Count - 4;
            //}
            //if (roomMinDoors >= roomMaxDoors)
            //{
            //    doorAmountMin = doorAmountMax - 1;
            //}

            var roomPosition = new Vector2(random.Next(0, generatedLevel.maxWidth - roomMaxWidth),
                random.Next(0, generatedLevel.maxHeight - roomMaxHeight));
            var roomDimensions = new Vector2(random.Next(5, roomMaxWidth), random.Next(5, roomMaxHeight));
            var generatedRoom = new Rectangle((int) roomPosition.X, (int) roomPosition.Y, (int) roomDimensions.X,
                (int) roomDimensions.Y);

            var validPlacement = true;
            var bufferRectangle = new Rectangle((int) roomPosition.X, (int) roomPosition.Y, (int) roomDimensions.X,
                (int) roomDimensions.Y);
            bufferRectangle.Inflate(3, 3);
            foreach (var room in generatedLevel.levelRooms)
                if (bufferRectangle.Intersects(room))
                    validPlacement = false;
            if (!validPlacement) return;

            for (var y = 0; y <= roomDimensions.Y; y++)
            for (var x = 0; x <= roomDimensions.X; x++)
                if (y == 0 || y == roomDimensions.Y
                           || x == 0 && (y != 0 || y != roomDimensions.Y)
                           || x == roomDimensions.X && (y != 0 || y != roomDimensions.Y))
                {
                    var newWall = entityManager.CreateEntity("Stone_Wall");
                    newWall.GetComponent<Position>().position = new Vector2(x + roomPosition.X, y + roomPosition.Y);
                    generatedLevel.levelTiles.Add(new Vector2(x + roomPosition.X, y + roomPosition.Y), newWall);
                }

                else
                {
                    var newFloor = entityManager.CreateEntity("Stone_Floor");
                    newFloor.GetComponent<Position>().position = new Vector2(x + roomPosition.X, y + roomPosition.Y);
                    generatedLevel.levelFloorTiles.Add(new Vector2(x + roomPosition.X, y + roomPosition.Y), newFloor);
                }

            generatedLevel.levelRooms.Add(generatedRoom);
        }

        private void CreateCorridor(Rectangle startingRoom, Level level)
        {
            float CalculateLevelGenerationWeightValue(int x, int y)
            {
                var position = new Vector2(x, y);

                var tiles = level.GetAllTilesByPosition(position);

                float weight = 0;

                if (tiles.Exists(x => x != null && x.HasComponent<Wall>()))
                    weight += 50;
                else if (tiles.Exists(x => x != null && x.HasComponent<Floor>()))
                    weight += 0;
                else if (tiles.Exists(x => x != null && x.HasComponent<Door>()))
                    weight += 0;
                else
                    weight += 10;

                return weight;
            }

            // select a random room other than the selected room to end corridor end
            var validRooms = level.levelRooms.Where(x => x != startingRoom).ToList();

            var roomToPathTo = validRooms[random.Next(0, validRooms.Count)];

            //Vector2 startPosition = new Vector2(startingRoom.X + startingRoom.Width * random.Next(0, 2), startingRoom.Y + startingRoom.Height * random.Next(0, 2));
            //Vector2 endPosition = new Vector2(roomToPathTo.X + roomToPathTo.Width * random.Next(0, 2), roomToPathTo.Y + roomToPathTo.Height * random.Next(0, 2));

            var startPosition = new Vector2(random.Next(startingRoom.X + 1, startingRoom.X + startingRoom.Width),
                random.Next(startingRoom.Y + 1, startingRoom.Y + startingRoom.Height));
            var endPosition = new Vector2(random.Next(roomToPathTo.X + 1, roomToPathTo.X + roomToPathTo.Width),
                random.Next(roomToPathTo.Y + 1, roomToPathTo.Y + roomToPathTo.Height));

            var pathfinder = new Pathfinder(CalculateLevelGenerationWeightValue);

            var path = pathfinder.GetPath((int) startPosition.X, (int) startPosition.Y, (int) endPosition.X,
                (int) endPosition.Y, level.nodeMap);

            while (path.Count != 0)
            {
                var pop = path.Pop();
                var position = new Vector2(pop[0], pop[1]);

                var entities = level.GetAllTilesByPosition(position);

                if (entities.Where(x => x != null).ToList().Count == 0)
                    if (!level.levelFloorTiles.ContainsKey(position))
                    {
                        var newFloor = entityManager.CreateEntity("Stone_Floor");
                        newFloor.GetComponent<Position>().position = position;
                        level.levelFloorTiles.TryAdd(position, newFloor);
                    }

                foreach (var entity in entities)
                    if (entity != null && entity.HasComponent<Wall>())
                    {
                        var newDoor = entityManager.CreateEntity("Wooden_Door");
                        newDoor.GetComponent<Position>().position = position;
                        entityManager.RemoveEntity(entity.Id);
                        level.levelTiles.Remove(position);
                        level.levelTiles.Add(position, newDoor);
                    }

                //Entity newDebugTile = entityManager.CreateEntity(debugTileFactory);
                //newDebugTile.GetComponent<Position>().Position = Position;
            }
        }

        private void FillInLevel(Level level)
        {
            //fill in void tiles
            for (var y = 0; y < level.maxHeight; y++)
            for (var x = 0; x < level.maxWidth; x++)
            {
                var position = new Vector2(x, y);
                if (level.GetAllTilesByPosition(position).Where(x => x != null).ToList().Count == 0)
                {
                    var newRock = entityManager.CreateEntity("Rock_Wall");
                    newRock.GetComponent<Position>().position = position;
                    level.levelTiles.Add(position, newRock);
                }

                if (!level.levelFloorTiles.ContainsKey(position))
                {
                    var newFloor = entityManager.CreateEntity("Stone_Floor");
                    newFloor.GetComponent<Position>().position = position;
                    level.levelFloorTiles.TryAdd(position, newFloor);
                }
            }
        }
    }
}