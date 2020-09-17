using System.Collections.Generic;

namespace ECSRogue.Managers.Events.EventProcessor
{
    internal class UIEventProcessor : IEventProcessor
    {
        public void Process(EventManager manager)
        {
            foreach (var uiEvent in manager.GetEvents<UiEvent>())
            {
                manager.RemoveEvent(uiEvent);
            }
        }
    }
}