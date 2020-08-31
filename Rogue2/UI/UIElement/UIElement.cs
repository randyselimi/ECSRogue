using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue2.Managers.Events;
using System.Collections.Generic;

namespace Rogue2.UI
{
    public abstract class UIElement
    {
        public UIComponent baseComponent { get; set; }

        public virtual void Draw(Texture2D rectangleSprite, SpriteBatch spriteBatch)
        {
            baseComponent.Draw(rectangleSprite, spriteBatch);
        }

        public virtual void Update(Vector2 screenOffset, List<IEvent> eventQueue)
        {
            baseComponent.Update(screenOffset);
        }
    }
}
