using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Managers.Rendering.RenderComponent
{
    class EntityRenderComponent : IRenderComponent
    {
        Queue<DrawData> drawQueue = new Queue<DrawData>();
        public SpriteSortMode spriteSortMode { get; set; } = SpriteSortMode.FrontToBack;


        public void AddToDrawQueue(Texture2D texture2D, Vector2 position, Color color, float height)
        {
            drawQueue.Enqueue(new DrawData(texture2D, position, color, height));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            while (drawQueue.Count != 0)
            {
                DrawData current = drawQueue.Dequeue();
                spriteBatch.Draw(current.texture2D, current.position, null, current.color, 0, new Vector2(0, 0), 1, SpriteEffects.None, current.height);
            }
        }

        public class DrawData
        {
            public Texture2D texture2D;
            public Vector2 position;
            public Color color;
            public float height;

            public DrawData(Texture2D texture2D, Vector2 position, Color color, float height)
            {
                this.texture2D = texture2D;
                this.position = position;
                this.color = color;
                this.height = height;
            }
        }
    }
}
