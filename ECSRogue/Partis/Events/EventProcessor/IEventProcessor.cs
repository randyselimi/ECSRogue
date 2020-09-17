using System.Collections.Generic;

namespace ECSRogue.Managers.Events.EventProcessor
{
    public interface IEventProcessor
    {
        /// <summary>
        ///     Processes events in eventQueue for corresponding type. Removes events depending on type-specific logic
        /// </summary>
        /// <param name="eventQueue"></param>
        public void Process(EventManager manager);
    }
}