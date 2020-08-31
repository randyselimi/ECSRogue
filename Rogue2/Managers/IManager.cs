using Microsoft.Xna.Framework;
using Rogue2.Managers.Events;
using System.Collections.Generic;

namespace Rogue2.Managers
{
    public interface IManager
    {
        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue);
    }
}
