using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.Data
{
    /// <summary>
    /// Represents a sprite from a spritesheet
    /// </summary>
    public class SpriteDefinition
    {
        public Texture2D spriteSheet { get; }
        public string spriteId { get; }
        public Rectangle boundsRectangle;

        public SpriteDefinition(Texture2D spriteSheet, string spriteId, Rectangle rectangle)
        {
            this.spriteSheet = spriteSheet;
            this.spriteId = spriteId;
            boundsRectangle = rectangle;
        }
    }

    public class SpriteAtlas
    {
        Dictionary<string, SpriteDefinition> spriteDefinitions = new Dictionary<string, SpriteDefinition>();

        public void AddDefinition(SpriteDefinition input)
        {
            spriteDefinitions.Add(input.spriteId, input);
        }

        public SpriteDefinition GetDefinition(string Id)
        {
            return spriteDefinitions[Id];
        }
    }

    public class SpriteDefinitionLoader
    {
        private ContentManager manager;
        private int Sprite_Size = 35;

        public SpriteDefinitionLoader(ContentManager manager)
        {
            this.manager = manager;
        }

        public SpriteAtlas LoadSpriteDefinitions(string spriteSheet)
        {

            XmlDocument document = new XmlDocument();
            document.Load("SpriteDefinitions.xml");

            SpriteAtlas atlas = new SpriteAtlas();

            XmlNode root = document.DocumentElement;
            XmlNodeList sheetList = root.SelectNodes("/SpriteDefinitions/SpriteSheet");

            foreach (XmlNode sheetNode in sheetList)
            {
                Texture2D texture = manager.Load<Texture2D>(sheetNode.Attributes[0].Value);
                int xOffset = int.Parse(sheetNode.SelectSingleNode("Offset/X").InnerText);
                int yOffset = int.Parse(sheetNode.SelectSingleNode("Offset/Y").InnerText);

                //Vector2 bounds = new Vector2(texture.Width / (Sprite_Size + xOffset), texture.Height / (Sprite_Size + yOffset));
                XmlNodeList spriteList = sheetNode.SelectNodes("Sprites/*");

                foreach (XmlNode spriteNode in spriteList)
                {
                    Rectangle spriteRectangle = new Rectangle();
                    int xPos = int.Parse(spriteNode.SelectSingleNode("Position/X").InnerText);
                    int yPos = int.Parse(spriteNode.SelectSingleNode("Position/Y").InnerText);

                    spriteRectangle.Height = Sprite_Size;
                    spriteRectangle.Width = Sprite_Size;

                    spriteRectangle.X = xPos * Sprite_Size + xOffset - 1 + xOffset * xPos;
                    spriteRectangle.Y = yPos * Sprite_Size + yOffset - 1 + yOffset * yPos;

                    SpriteDefinition definition = new SpriteDefinition(texture, spriteNode.Attributes[0].Value, spriteRectangle);
                    atlas.AddDefinition(definition);
                }
            }

            return atlas;
        }
    }
}
