using System.Collections.Generic;
using ECSRogue.Factories.LevelFactory;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers.Levels
{
    internal class LevelManager : IManager
    {
        private readonly int currentFloor = 0;
        public Dictionary<int, Level> levels { get; } = new Dictionary<int, Level>();

        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue)
        {
        }

        public void GenerateLevel(ILevelFactory levelFactory)
        {
            levels.Add(levels.Count, levelFactory.GenerateLevel(30, 30));
        }

        public Level GetCurrentLevel()
        {
            return levels[currentFloor];
        }
    }
}