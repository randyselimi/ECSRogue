using System.Collections.Generic;

namespace ECSRogue.Managers.Events.EventProcessor
{
    internal class UIEventProcessor : IEventProcessor
    {
        public void Process(List<IEvent> eventQueue)
        {
            for (var i = 0; i < eventQueue.Count; i++)
                if (eventQueue[i].GetType() == typeof(UIEvent))
                {
                    var uiEvent = (UIEvent) eventQueue[i];
                    if (uiEvent.lifetime >= 1)
                    {
                        eventQueue.Remove(eventQueue[i]);
                        i--;
                    }
                    else
                    {
                        uiEvent.lifetime++;
                    }
                }
        }
    }
}