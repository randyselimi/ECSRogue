using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    class Sprite : Component
    {
        public Texture2D texture2D;
        public float height;
        public bool render = true;
        public Sprite(Texture2D texture2D, float height)
        {
            this.texture2D = texture2D;
            this.height = height;
        }
        public Sprite(Sprite sprite)
        {
            this.texture2D = sprite.texture2D;
            this.height = sprite.height;
        }
        public override object Clone()
        {
            Sprite clone = new Sprite(this);
            return clone;
        }
    }
}
