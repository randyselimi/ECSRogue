using System;
using System.Collections.Generic;
using ECSRogue.Data;

namespace ECSRogue.Components
{
    public abstract class Component : ICloneable
    {
        public delegate void ComponentUpdatedEventHandler(object source, ComponentUpdatedEventArgs args);

        public int Id { get; set; }
        public abstract object Clone();

        public void OnComponentUpdated(object source, ComponentUpdatedEventArgs args)
        {
            ComponentUpdated?.Invoke(source, args);
        }
        public event ComponentUpdatedEventHandler ComponentUpdated;
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