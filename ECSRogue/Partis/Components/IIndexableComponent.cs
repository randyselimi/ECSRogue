using System;
using System.Collections.Generic;
using System.Text;
using ECSRogue.Managers;

namespace ECSRogue.Components
{
    /// <summary>
    /// Allows for component to be indexed by its value type
    /// </summary>
    public interface IIndexableComponent
    {
        public void OnComponentUpdated(object source, ComponentUpdatedEventArgs args);
        public event EventHandler<ComponentUpdatedEventArgs> ComponentUpdated;
        public object GetIndexValue();
    }

    public class ComponentUpdatedEventArgs : EventArgs
    {
        public object previous;
        public object current;
        public int id;

        public ComponentUpdatedEventArgs(object previous, object current, int id)
        {
            this.previous = previous;
            this.current = current;
            this.id = id;
        }
    }
}
