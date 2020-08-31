using Microsoft.Xna.Framework;
using Rogue2.Components;
using Rogue2.Data;
using Rogue2.Factories;
using Rogue2.Helpers.Pathfinding;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Levels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rogue2.Managers
{
    class DungeonLevelFactory : ILevelFactory
    {
        const int MapSize = 100;
        EntityManager entityManager { get; set; }
        EntityFactory wallFactory = new EntityFactory(EntityPresets.entityPresets["wall"]);
        EntityFactory rockFactory = new EntityFactory(EntityPresets.entityPresets["rock"]);
        EntityFactory doorFactory = new EntityFactory(EntityPresets.entityPresets["door"]);
        EntityFactory floorFactory = new EntityFactory(EntityPresets.entityPresets["floor"]);
        //EntityFactory debugTileFactory = new EntityFactory(EntityPresets.entityPresets["debugtile"]);

        Random random;

        public DungeonLevelFactory(EntityManager entityManager, Random random)
        {
            this.entityManager = entityManager;
            this.random = random;
        }
        public Level GenerateLevel(int maxWidth, int maxHeight)
        {
            int numberOfAttempts = 10000;
            Level generatedLevel = new Level(maxWidth, maxHeight);

            //Step one, create rooms that fall within level bounds and do not intersect other rooms
            for (int i = 0; i < numberOfAttempts; i++)
            {
                CreateRoom(generatedLevel, 10, 10);
            }

            foreach (Rectangle room in generatedLevel.levelRooms)
            {
                CreateCorridor(room, generatedLevel);
            }


            FillInLevel(generatedLevel);

            return generatedLevel;
        }

        public void CreateRoom(Level generatedLevel, int roomMaxWidth, int roomMaxHeight)
        {
            //minimum room size is 5
            if (roomMaxHeight < 5) { roomMaxHeight = 5; }
            if (roomMaxWidth < 5) { roomMaxWidth = 5; }

            //if (roomMinDoors >= roomMaxHeight = roomMaxWidth - 4)
            //{
            //    doorAmountMax = room.wallEntity.Count - 4;
            //}
            //if (roomMinDoors >= roomMaxDoors)
            //{
            //    doorAmountMin = doorAmountMax - 1;
            //}

            Vector2 roomPosition = new Vector2(random.Next(0, generatedLevel.maxWidth - roomMaxWidth), random.Next(0, generatedLevel.maxHeight - roomMaxHeight));
            Vector2 roomDimensions = new Vector2(random.Next(5, roomMaxWidth), random.Next(5, roomMaxHeight));
            Rectangle generatedRoom = new Rectangle((int)roomPosition.X, (int)roomPosition.Y, (int)roomDimensions.X, (int)roomDimensions.Y);

            bool validPlacement = true;
            Rectangle bufferRectangle = new Rectangle((int)roomPosition.X, (int)roomPosition.Y, (int)roomDimensions.X, (int)roomDimensions.Y);
            bufferRectangle.Inflate(3, 3);
            foreach (Rectangle room in generatedLevel.levelRooms)
            {
                if (bufferRectangle.Intersects(room))
                {
                    validPlacement = false;
                }

            }
            if (!validPlacement)
            {
                return;
            }

            for (int y = 0; y <= roomDimensions.Y; y++)
            {
                for (int x = 0; x <= roomDimensions.X; x++)
                {
                    if ((y == 0 || y == roomDimensions.Y)
                       || (x == 0 && (y != 0 || y != roomDimensions.Y))
                       || (x == roomDimensions.X && (y != 0 || y != roomDimensions.Y)))
                    {
                        Entity newWall = entityManager.CreateEntity(wallFactory);
                        newWall.GetComponent<Position>().position = new Vector2(x + roomPosition.X, y + roomPosition.Y);
                        generatedLevel.levelTiles.Add(new Vector2(x + roomPosition.X, y + roomPosition.Y), newWall);
                    }

                    else
                    {
                        Entity newFloor = entityManager.CreateEntity(floorFactory);
                        newFloor.GetComponent<Position>().position = new Vector2(x + roomPosition.X, y + roomPosition.Y);
                        generatedLevel.levelFloorTiles.Add(new Vector2(x + roomPosition.X, y + roomPosition.Y), newFloor);
                    }
                }
            }

            generatedLevel.levelRooms.Add(generatedRoom);
        }                                                                  

        void CreateCorridor(Rectangle startingRoom, Level level)
        {
            // select a random room other than the selected room to end corridor end
            List<Rectangle> validRooms = level.levelRooms.Where(x => x != startingRoom).ToList();

            Rectangle roomToPathTo = validRooms[random.Next(0, validRooms.Count)];

            //Vector2 startPosition = new Vector2(startingRoom.X + startingRoom.Width * random.Next(0, 2), startingRoom.Y + startingRoom.Height * random.Next(0, 2));
            //Vector2 endPosition = new Vector2(roomToPathTo.X + roomToPathTo.Width * random.Next(0, 2), roomToPathTo.Y + roomToPathTo.Height * random.Next(0, 2));

            Vector2 startPosition = new Vector2(random.Next(startingRoom.X + 1, startingRoom.X + startingRoom.Width), random.Next(startingRoom.Y + 1, startingRoom.Y + startingRoom.Height));
            Vector2 endPosition = new Vector2(random.Next(roomToPathTo.X + 1, roomToPathTo.X + roomToPathTo.Width), random.Next(roomToPathTo.Y + 1, roomToPathTo.Y + roomToPathTo.Height));

            Stack<Vector2> path = PathfindingAlgorithm.GetPath(startPosition, endPosition, new DungeonLevelPathfindingData(level));

            while (path.Count != 0)
            {
                Vector2 position = path.Pop();
            
                List<Entity> entities = level.GetAllTilesByPosition(position);

                if (entities.Where(x => x != null).ToList().Count == 0)
                {
                    if (!level.levelFloorTiles.ContainsKey(position))
                    {
                        Entity newFloor = entityManager.CreateEntity(floorFactory);
                        newFloor.GetComponent<Position>().position = position;
                        level.levelFloorTiles.TryAdd(position, newFloor);
                    }
                }

                foreach (var entity in entities)
                {
                    if (entity != null && entity.HasComponent<Wall>())
                    {
                        Entity newDoor = entityManager.CreateEntity(doorFactory);
                        newDoor.GetComponent<Position>().position = position;
                        entityManager.RemoveEntity(entity.ID);
                        level.levelTiles.Remove(position);
                        level.levelTiles.Add(position, newDoor);
                    }
                }

                //Entity newDebugTile = entityManager.CreateEntity(debugTileFactory);
                //newDebugTile.GetComponent<Position>().position = position;
            }

        }
        void FillInLevel(Level level)
        {
            //fill in void tiles
            for (int y = 0; y < level.maxHeight; y++)
            {
                for (int x = 0; x < level.maxWidth; x++)
                {
                    Vector2 position = new Vector2(x, y);
                    if (level.GetAllTilesByPosition(position).Where(x => x != null).ToList().Count == 0)
                    {
                        Entity newRock = entityManager.CreateEntity(rockFactory);
                        newRock.GetComponent<Position>().position = position;
                        level.levelTiles.Add(position, newRock);
                    }

                    if (!level.levelFloorTiles.ContainsKey(position))
                    {
                        Entity newFloor = entityManager.CreateEntity(floorFactory);
                        newFloor.GetComponent<Position>().position = position;
                        level.levelFloorTiles.TryAdd(position, newFloor);
                    }
                }
            }
        }

        public class DungeonLevelPathfindingData : IPathfindingData
        {
            public Level level { get; private set; }
            public DungeonLevelPathfindingData(Level level)
            {
                this.level = level;
            }
            public int CalculateWeightValue(Vector2 position)
            {

                List<Entity> tiles = level.GetAllTilesByPosition(position);

                int gvalue = 0;

                if (tiles.Exists(x => x != null && x.HasComponent<Wall>()))
                {
                    gvalue += 50;
                }
                else if (tiles.Exists(x => x != null && x.HasComponent<Floor>()))
                {
                    gvalue += 1;
                }
                else if (tiles.Exists(x => x != null && x.HasComponent<Door>()))
                {
                    gvalue += 1;
                }
                else
                {
                    gvalue += 10;
                }

                return gvalue;
            }
        }

    }
}



