using System.Collections.Generic;
using ECSRogue.Factories.LevelFactory;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers.Levels
{
    internal class LevelManager
    {
        private readonly int currentFloor = 0;
        public Dictionary<int, Level> levels { get; } = new Dictionary<int, Level>();

        public void GenerateLevel(ILevelFactory levelFactory, PartisInstance instance)
        {
            levels.Add(levels.Count, levelFactory.GenerateLevel(30, 30, instance));
        }

        public Level GetCurrentLevel()
        {
            return levels[currentFloor];
        }
    }
}