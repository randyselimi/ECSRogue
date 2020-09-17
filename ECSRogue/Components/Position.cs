using System;
using System.Collections.Generic;
using ECSRogue.Data;
using Microsoft.Xna.Framework;

namespace ECSRogue.Components
{
    public class Position : Component
    {
        private Vector2 position1;

        public Vector2 position
        {
            get => position1;

            set
            {
                OnComponentUpdated(this, new ComponentUpdatedEventArgs(position1, value, Id));
                position1 = value;
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
    }
}