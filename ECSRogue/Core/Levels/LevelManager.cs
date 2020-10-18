using System;
using System.Collections.Generic;
using ECSRogue.Factories.LevelFactory;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers.Levels
{
    public class LevelManager
    {
        private int currentLevel = 0;
        public Dictionary<int, Level> levels { get; } = new Dictionary<int, Level>();

        public void GenerateLevel(int levelId, ILevelFactory levelFactory, PartisInstance instance)
        {
            levels.Add(levelId, levelFactory.GenerateLevel(levelId, 30, 30, instance));
        }

        public Level GetCurrentLevel()
        {
            return levels[currentLevel];
        }

        public void ChangeCurrentLevel(int newLevel, PartisInstance instance)
        {
            if (!levels.ContainsKey(newLevel))
            {
                GenerateLevel(newLevel, new DungeonLevelFactory(new Random()), instance);
            }

            currentLevel = newLevel;
        }
    }
}