using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.Handlers.Rendering.RenderComponent
{
    public interface IRenderProcessor
    {
        public SpriteSortMode spriteSortMode { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}