using Rogue2.Managers.Events;
using System.Collections.Generic;

namespace Rogue2.Handlers.Events.EventProcessor
{
    public interface IEventProcessor
    {
        /// <summary>
        /// Processes events in eventQueue for corresponding type. Removes events depending on type-specific logic
        /// </summary>
        /// <param name="eventQueue"></param>
        public void Process(List<IEvent> eventQueue);
    }
}
