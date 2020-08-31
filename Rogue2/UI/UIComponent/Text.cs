using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Rogue2.UI
{
    class Text : UIComponent
    {
        public string displayText = "test";
        public SpriteFont spriteFont;

        public Text(SpriteFont spriteFont, UIPosition relativePosition, Color color, Vector2? offset = null) : base(relativePosition, color, offset)
        {
            this.spriteFont = spriteFont;
            Vector2 stringSize = this.spriteFont.MeasureString("Test");
        }

        public override void Draw(Texture2D rectangleSprite, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, displayText, new Vector2(absolutePosition.X, absolutePosition.Y), Color.Black);

            base.Draw(rectangleSprite, spriteBatch);
        }
    }
}
