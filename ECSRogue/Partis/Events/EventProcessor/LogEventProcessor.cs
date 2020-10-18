using System.Collections.Generic;

namespace ECSRogue.Managers.Events.EventProcessor
{
    internal class LogEventProcessor : IEventProcessor
    {
        public void Process(EventManager manager)
        {
            foreach (var logEvent in manager.GetEvents<LogEvent>())
            {
                if (logEvent.lifetime == 1)
                {
                    manager.RemoveEvent(logEvent);
                }
                else logEvent.lifetime++;
            }
        }
    }
}