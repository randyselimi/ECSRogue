using System.Collections.Generic;
using ECSRogue.UI.UIElement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.Handlers.Rendering.RenderComponent
{
    internal class UIRenderComponent : IRenderComponent
    {
        private readonly Queue<UIElement> drawQueue = new Queue<UIElement>();
        public GraphicsDevice graphicsDevice;
        private readonly Texture2D rectangleSprite;

        public UIRenderComponent(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            rectangleSprite = new Texture2D(graphicsDevice, 1, 1);
            rectangleSprite.SetData(new[] {Color.White});
        }

        public SpriteSortMode spriteSortMode { get; set; } = SpriteSortMode.Immediate;

        public void Draw(SpriteBatch spriteBatch)
        {
            while (drawQueue.Count != 0)
            {
                var current = drawQueue.Dequeue();
                current.Draw(rectangleSprite, spriteBatch);
            }
        }

        public void AddToDrawQueue(UIElement uiElement)
        {
            drawQueue.Enqueue(uiElement);
        }
    }
}