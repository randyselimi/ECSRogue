using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.Components
{
    internal class Sprite : Component
    {
        public float height;
        public bool render = true;
        public Texture2D texture2D;

        public Sprite(Texture2D texture2D, float height)
        {
            this.texture2D = texture2D;
            this.height = height;
        }

        public Sprite(Sprite sprite)
        {
            texture2D = sprite.texture2D;
            height = sprite.height;
        }

        public override object Clone()
        {
            var clone = new Sprite(this);
            return clone;
        }
    }
}