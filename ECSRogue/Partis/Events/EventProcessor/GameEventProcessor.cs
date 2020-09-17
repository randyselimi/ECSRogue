using System.Collections.Generic;

namespace ECSRogue.Managers.Events.EventProcessor
{
    internal class GameEventProcessor : IEventProcessor
    {
        public void Process(EventManager manager)
        {
            foreach (var gameEvent in manager.GetEvents<GameEvent>())
            {
                manager.RemoveEvent(gameEvent);
            }
        }
    }
}