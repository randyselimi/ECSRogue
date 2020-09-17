using System;
using System.Collections.Generic;
using System.Text;
using ECSRogue.Data;

namespace ECSRogue.Components
{
    public interface IContentComponent
    {
        public void LoadContent(SpriteAtlas atlas);
    }
}
