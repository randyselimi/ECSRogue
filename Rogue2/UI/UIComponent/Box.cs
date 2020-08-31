﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue2.Managers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Xml.XPath;

namespace Rogue2.UI
{
    class Box : UIComponent
    {
        Texture2D texture;
        public Box(Vector2 dimensions, UIPosition relativePosition, Color color, Vector2? offset = null) : base(dimensions, relativePosition, color, offset)
        {

        }

        public Box(Texture2D texture, Vector2 dimensions, UIPosition relativePosition, Vector2? offset = null) : this(dimensions, relativePosition, Color.White, offset)
        {
            this.texture = texture;
        }

        public override void Draw(Texture2D rectangleSprite, SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, absolutePosition, null, color, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);

            }

            else 
            {
                spriteBatch.Draw(rectangleSprite, new Rectangle((int)absolutePosition.X, (int)absolutePosition.Y, (int)dimensions.X, (int)dimensions.Y), color);
            } 

            base.Draw(rectangleSprite, spriteBatch);

        }
    }
}
