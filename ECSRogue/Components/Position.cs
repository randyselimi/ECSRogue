using System;
using System.Collections.Generic;
using ECSRogue.Data;
using ECSRogue.Managers;
using Microsoft.Xna.Framework;

namespace ECSRogue.Components
{
    public class Position : Component, IIndexableComponent
    {
        private Vector2 pos;
        public Vector2 position
        {
            get => pos;

            set
            {
                OnComponentUpdated(this, new ComponentUpdatedEventArgs(pos, value, Id));
                pos = value;
            }
        }
        public Position()
        {
        }
        public Position(Position position)
        {
            this.position = position.position;
        }
        public override object Clone()
        {
            var clone = new Position(this);
            return clone;
        }

        public void OnComponentUpdated(object source, ComponentUpdatedEventArgs args)
        {
            EventHandler<ComponentUpdatedEventArgs> handler = ComponentUpdated;

            handler?.Invoke(source, args);
        }
        public event EventHandler<ComponentUpdatedEventArgs> ComponentUpdated;

        public object GetIndexValue()
        {
            return position;
        }
    }
}