using System.Collections.Generic;

namespace ECSRogue.Managers.Events.EventProcessor
{
    internal class GameEventProcessor : IEventProcessor
    {
        public void Process(List<IEvent> eventQueue)
        {
            for (var i = 0; i < eventQueue.Count; i++)
                if (eventQueue[i].GetType() == typeof(GameEvent))
                {
                    var gameEvent = (GameEvent) eventQueue[i];

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