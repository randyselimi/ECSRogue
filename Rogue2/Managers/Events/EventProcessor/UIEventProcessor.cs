using Rogue2.Managers.Events;
using System.Collections.Generic;

namespace Rogue2.Handlers.Events.EventProcessor
{
    class UIEventProcessor : IEventProcessor
    {
        public void Process(List<IEvent> eventQueue)
        {
            for (int i = 0; i < eventQueue.Count; i++)
            {
                if (eventQueue[i].GetType() == typeof(UIEvent))
                {
                    UIEvent uiEvent = (UIEvent)eventQueue[i];
                    if (uiEvent.lifetime >= 1)
                    {
                        eventQueue.Remove(eventQueue[i]);
                        i--;
                        continue;
                    }
                    else
                    {
                        uiEvent.lifetime++;
                    }
                }

            }
        }
    }
}
