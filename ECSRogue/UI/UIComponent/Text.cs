﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.UI.UIComponent
{
    internal class Text : UIComponent
    {
        public string displayText = "test";
        public SpriteFont spriteFont;

        public Text(string text, SpriteFont spriteFont, UIPosition relativePosition, Color color, Vector2? offset = null) : base(
            relativePosition, color, offset)
        {
            this.spriteFont = spriteFont;
            displayText = text;
            dimensions = spriteFont.MeasureString(displayText);
        }

        public override void Draw(Texture2D rectangleSprite, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, displayText, new Vector2(absolutePosition.X, absolutePosition.Y),
                Color.Black);

            base.Draw(rectangleSprite, spriteBatch);
        }
    }
}