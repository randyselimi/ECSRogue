using System.Collections.Generic;
using ECSRogue.Components;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.Data
{
    public struct EntityTemplate
    {
        public string name;
        public List<Component> components;

        public EntityTemplate(string name, List<Component> components)
        {
            this.name = name;
            this.components = components;
        }
    }

    public static class EntityTemplates
    {
        public static Dictionary<string, EntityTemplate> entityTemplates = new Dictionary<string, EntityTemplate>();

        //This is a temporary implementation. Eventually entity templates will be parsed at runtime from XAML files. Allows for easy and extensible content generation

        public static void Load(ContentManager contentManager)
        {
            //Will eventually be defined in external file
            var humanoidEquipmentSlots = new List<string> {"Head", "Chest", "Legs", "Wieldable"};

            var entityTemplate = new EntityTemplate("wall",
                new List<Component>
                {
                    new Name("Wall"),  new IsActive(), new Collideable(), new Position(), new Wall(),
                    new Sprite(contentManager.Load<Texture2D>("Wall"), .5f)
                });
            entityTemplates.Add(entityTemplate.name, entityTemplate);

            entityTemplate = new EntityTemplate("rock",
                new List<Component>
                {
                    new Name("Rock"), new IsActive(), new Collideable(), new Position(), new Wall(),
                    new Sprite(contentManager.Load<Texture2D>("Rock"), .5f)
                });
            entityTemplates.Add(entityTemplate.name, entityTemplate);

            entityTemplate = new EntityTemplate("floor",
                new List<Component>
                {
                    new Name("Floor"), new IsActive(),  new Floor(), new Position(),
                    new Sprite(contentManager.Load<Texture2D>("Floor"), 0f)
                });
            entityTemplates.Add(entityTemplate.name, entityTemplate);

            entityTemplate = new EntityTemplate("sword",
                new List<Component>
                {
                    new Slot("Wieldable"), new IsActive(), new Name("Sword"), new Carryable(), new Wieldable("weapon"), new Position(),
                    new Damage(10), new Sprite(contentManager.Load<Texture2D>("Sword"), .25f)
                });
            entityTemplates.Add(entityTemplate.name, entityTemplate);

            entityTemplate = new EntityTemplate("player",
                new List<Component>
                {
                    new Turn(), new IsActive(), new Equipment(humanoidEquipmentSlots), new Name("Test Name"), new Collideable(),
                    new Position(), new Sprite(contentManager.Load<Texture2D>("Player"), 1), new Player(),
                    new Velocity(), new Health(1000), new Inventory()
                });
            entityTemplates.Add(entityTemplate.name, entityTemplate);

            entityTemplate = new EntityTemplate("spear",
                new List<Component>
                {
                    new Slot("Wieldable"), new IsActive(), new Name("Spear"), new Carryable(), new Wieldable("weapon"), new Position(),
                    new Damage(6), new Sprite(contentManager.Load<Texture2D>("Spear"), .25f)
                });
            entityTemplates.Add(entityTemplate.name, entityTemplate);

            entityTemplate = new EntityTemplate("monster",
                new List<Component>
                {
                    new MonsterAI(), new Turn(), new IsActive(), new Equipment(humanoidEquipmentSlots), new Name("Monster"),
                    new Collideable(), new Position(), new Sprite(contentManager.Load<Texture2D>("Zombie"), 1),
                    new Velocity(), new Health(6), new Inventory()
                });
            entityTemplates.Add(entityTemplate.name, entityTemplate);

            entityTemplate = new EntityTemplate("camera", new List<Component> {new Position(), new Camera()});
            entityTemplates.Add(entityTemplate.name, entityTemplate);

            entityTemplate = new EntityTemplate("door",
                new List<Component>
                {
                    new Name("Door"), new IsActive(), new Collideable(), new Position(), new Door(),
                    new Sprite(contentManager.Load<Texture2D>("Door"), 1)
                });
            entityTemplates.Add(entityTemplate.name, entityTemplate);

            //entityPreset = new EntityTemplate("debugtile", new List<Component> { new Position(), new Sprite(contentManager.Load<Texture2D>("DebugTile"), 0) });
            //entityTemplates.Add(entityPreset.name, entityPreset);
        }
    }
}