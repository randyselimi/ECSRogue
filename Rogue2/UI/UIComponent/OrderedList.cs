using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.UI
{
    class OrderedList : UIComponent
    {
        Vector2 childrenSpacing;
        public OrderedList(Vector2 childrenSpacing, Vector2 dimensions, UIPosition relativePosition, Color color, Vector2? offset = null) : base(dimensions, relativePosition, color, offset)
        {
            this.childrenSpacing = childrenSpacing;
        }



        public override void AddChild(UIComponent childToAdd)
        {
            base.AddChild(childToAdd);

            childToAdd.absolutePosition += childrenSpacing * new Vector2(children.Count - 1);
        }
    }
}
