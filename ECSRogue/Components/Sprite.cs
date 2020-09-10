using System.Collections.Generic;
using System.Linq;
using ECSRogue.Data;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.Components
{
    internal class Sprite : Component, IXmlParameterComponent, IContentComponent
    {
        public float height;
        public bool render = true;
        private string spriteId;
        public SpriteDefinition sprite;

        public Sprite(List<ComponentData> datas)
        {
            InitializeFromDefinition(datas);
        }

        public Sprite(Sprite sprite)
        {
            this.sprite = sprite.sprite;
            height = sprite.height;
        }

        public override object Clone()
        {
            var clone = new Sprite(this);
            return clone;
        }
        public void InitializeFromDefinition(List<ComponentData> datas)
        {
            foreach (ComponentData data in datas)
            {
                if (data.Id == "Name")
                {
                    spriteId = (string) data.Data;
                }

                if (data.Id == "Height")
                {
                    height = float.Parse((string)data.Data);
                }
            }
        }

        public void LoadContent(SpriteAtlas atlas)
        {
            sprite = atlas.GetDefinition(spriteId);
        }
    }
}