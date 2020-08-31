using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue2.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Managers.Rendering.RenderComponent
{
    class UIRenderComponent : IRenderComponent
    {
        Queue<UIElement> drawQueue = new Queue<UIElement>();

        public SpriteSortMode spriteSortMode { get; set; } = SpriteSortMode.Immediate;
        Texture2D rectangleSprite;
        public GraphicsDevice graphicsDevice;
        public UIRenderComponent(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            rectangleSprite = new Texture2D(graphicsDevice, 1, 1);
            rectangleSprite.SetData(new[] { Color.White });
        }
        public void AddToDrawQueue(UIElement uiElement)
        {
            drawQueue.Enqueue(uiElement);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            while (drawQueue.Count != 0)
            {
                UIElement current = drawQueue.Dequeue();
                current.Draw(rectangleSprite, spriteBatch);
            }
        }
    }
}
