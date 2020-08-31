using Microsoft.Xna.Framework;
using Rogue2.Managers.Events;
using System.Collections.Generic;

namespace Rogue2.Managers.Levels
{
    class LevelManager : IManager
    {
        public Dictionary<int, Level> levels { get; private set; } = new Dictionary<int, Level>();

        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue)
        {
            
        }

        void GenerateLevel(ILevelFactory levelFactory)
        {
            levels.Add(levels.Count, levelFactory.GenerateLevel(100, 100));
        }
    }
}
