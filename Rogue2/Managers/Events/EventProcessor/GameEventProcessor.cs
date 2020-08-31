using Rogue2.Managers.Events;
using System.Collections.Generic;

namespace Rogue2.Handlers.Events.EventProcessor
{
    class GameEventProcessor : IEventProcessor
    {
        public void Process(List<IEvent> eventQueue)
        {
            for (int i = 0; i < eventQueue.Count; i++)
            {
                if (eventQueue[i].GetType() == typeof(GameEvent))
                {
                    GameEvent gameEvent = (GameEvent)eventQueue[i];

                    gameEvent.lifetime++;

                    //unneccesary
                    if (gameEvent.lifetime >= 1)
                    {
                        eventQueue.Remove(eventQueue[i]);                       
                        i--;
                    }
                 }
            }
        }
    }
}
