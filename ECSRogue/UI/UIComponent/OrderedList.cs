using Microsoft.Xna.Framework;

namespace ECSRogue.UI.UIComponent
{
    internal class OrderedList : UIComponent
    {
        private readonly Vector2 childrenSpacing;

        public OrderedList(Vector2 childrenSpacing, Vector2 dimensions, UIPosition relativePosition, Color color,
            Vector2? offset = null) : base(dimensions, relativePosition, color, offset)
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