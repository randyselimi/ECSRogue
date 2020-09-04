using System;
using System.Collections.Generic;
using System.Text;

namespace ECSRogue.Components
{
    class IsActive : Component
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
    }
}
