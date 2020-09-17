using System.Collections.Generic;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.UI.UIElement
{
    public abstract class UIElement
    {
        public UIComponent.UIComponent baseComponent { get; set; }

        public virtual void Draw(Texture2D rectangleSprite, SpriteBatch spriteBatch)
        {
            baseComponent.Draw(rectangleSprite, spriteBatch);
        }

        public virtual void Update(Vector2 screenOffset)
        {
            baseComponent.Update(screenOffset);
        }
    }
}