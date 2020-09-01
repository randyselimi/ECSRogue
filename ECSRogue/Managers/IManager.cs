using System.Collections.Generic;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers
{
    public interface IManager
    {
        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue);
    }
}