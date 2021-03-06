﻿using System;
using System.Collections.Generic;
using System.Text;
using ECSRogue.Data;
using ECSRogue.Managers;

namespace ECSRogue.Components
{
    class IsActive : Component, IIndexableComponent
    {
        private bool is_active = true;
        public bool isActive
        {
            get => is_active;

            set
            {
                OnComponentUpdated(this, new ComponentUpdatedEventArgs(is_active, value, Id));
                is_active = value;
            }
        }

        public IsActive()
        {
        }

        public IsActive(IsActive clone)
        {
        }

        public override object Clone()
        {
            return new IsActive(this);
        }
        public void OnComponentUpdated(object source, ComponentUpdatedEventArgs args)
        {
            EventHandler<ComponentUpdatedEventArgs> handler = ComponentUpdated;

            handler?.Invoke(source, args);
        }

        public event EventHandler<ComponentUpdatedEventArgs> ComponentUpdated;
        public object GetIndexValue()
        {
            return isActive;
        }
    }
}
