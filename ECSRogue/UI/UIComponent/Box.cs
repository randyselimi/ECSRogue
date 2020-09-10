using ECSRogue.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.UI.UIComponent
{
    internal class Box : UIComponent
    {
        private readonly SpriteDefinition definition;

        public Box(Vector2 dimensions, UIPosition relativePosition, Color color, Vector2? offset = null) : base(
            dimensions, relativePosition, color, offset)
        {
        }

        public Box(SpriteDefinition definition, Vector2 dimensions, UIPosition relativePosition, Vector2? offset = null) : this(
            dimensions, relativePosition, Color.White, offset)
        {
            this.definition = definition;
        }

        public override void Draw(Texture2D rectangleSprite, SpriteBatch spriteBatch)
        {
            if (definition != null)
                spriteBatch.Draw(definition.spriteSheet, absolutePosition, definition.boundsRectangle, color, 0, new Vector2(0, 0), 1, SpriteEffects.None,
                    0);

            else
                spriteBatch.Draw(rectangleSprite,
                    new Rectangle((int) absolutePosition.X, (int) absolutePosition.Y, (int) dimensions.X,
                        (int) dimensions.Y), color);

            base.Draw(rectangleSprite, spriteBatch);
        }
    }
}