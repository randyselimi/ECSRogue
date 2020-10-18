using System.Collections.Generic;
using ECSRogue.Managers.Entities;
using Microsoft.Xna.Framework;
using Pathfinding;

namespace ECSRogue.Managers.Levels
{
    public class Level
    {
        public int Id { get; }
        public Dictionary<Vector2, Entity> levelFloorTiles = new Dictionary<Vector2, Entity>();
        public List<Rectangle> levelRooms = new List<Rectangle>();
        public Dictionary<Vector2, Entity> levelTiles = new Dictionary<Vector2, Entity>();
        public int maxHeight;
        public int maxWidth;
        public NodeMap nodeMap;

        public Level(int id, int maxWidth = 100, int maxHeight = 100)
        {
            Id = id;
            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;

            nodeMap = new NodeMap(maxWidth, maxHeight);
        }

        public Entity GetTilesByPosition(Vector2 position)
        {
            Entity tile;
            levelTiles.TryGetValue(position, out tile);
            return tile;
        }

        public Entity GetFloorTilesByPosition(Vector2 position)
        {
            Entity floorTile;
            levelFloorTiles.TryGetValue(position, out floorTile);
            return floorTile;
        }

        public List<Entity> GetAllTilesByPosition(Vector2 position)
        {
            var allTiles = new List<Entity>();
            allTiles.Add(GetTilesByPosition(position));
            allTiles.Add(GetFloorTilesByPosition(position));

            return allTiles;
        }
    }

    //public class Room
    //{
    //    public Rectangle roomRectangle;
    //    public List<Entity> wallEntity = new List<Entity>();
    //    public List<Entity> floorEntity = new List<Entity>();
    //    public List<Entity> doorEntity = new List<Entity>();

    //    public Room(int x, int y, int width, int height)
    //    {
    //        roomRectangle = new Rectangle(x, y, width, height);
    //    }
    //}
}