using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Managers.Rendering
{
    public interface IRenderComponent
    {
        public SpriteSortMode spriteSortMode { get; set; }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
