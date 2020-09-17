using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.Handlers.Rendering.RenderComponent
{
    internal class EntityRenderProcessor : IRenderProcessor
    {
        private readonly Queue<DrawData> drawQueue = new Queue<DrawData>();
        public SpriteSortMode spriteSortMode { get; set; } = SpriteSortMode.FrontToBack;

        public void Draw(SpriteBatch spriteBatch)
        {
            while (drawQueue.Count != 0)
            {
                var current = drawQueue.Dequeue();
                spriteBatch.Draw(current.texture2D, current.position, current.sourceRectangle, Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, current.height);
            }
        }


        public void AddToDrawQueue(Texture2D texture2D, Vector2 position, Color color, float height, Rectangle sourceRectangle)
        {
            drawQueue.Enqueue(new DrawData(texture2D, position, color, height, sourceRectangle));
        }

        public class DrawData
        {
            public Color color;
            public float height;
            public Vector2 position;
            public Texture2D texture2D;
            public Rectangle sourceRectangle;

            public DrawData(Texture2D texture2D, Vector2 position, Color color, float height, Rectangle sourceRectangle)
            {
                this.texture2D = texture2D;
                this.position = position;
                this.color = color;
                this.height = height;
                this.sourceRectangle = sourceRectangle;
            }
        }
    }
}