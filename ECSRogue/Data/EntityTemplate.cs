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

    public static class EntityPresets
    {
        public static Dictionary<string, EntityTemplate> entityPresets = new Dictionary<string, EntityTemplate>();

        //This is a temporary implementation. Eventually entity templates will be parsed at runtime from XAML files. Allows for easy and extensible content generation

        public static void Load(ContentManager contentManager)
        {
            //Will eventually be defined in external file
            var humanoidEquipmentSlots = new List<string> {"Head", "Chest", "Legs", "Wieldable"};

            var entityPreset = new EntityTemplate("wall",
                new List<Component>
                {
                    new Name("Wall"), new Collideable(), new Position(), new Wall(),
                    new Sprite(contentManager.Load<Texture2D>("Wall"), .5f)
                });
            entityPresets.Add(entityPreset.name, entityPreset);

            entityPreset = new EntityTemplate("rock",
                new List<Component>
                {
                    new Name("Rock"), new Collideable(), new Position(), new Wall(),
                    new Sprite(contentManager.Load<Texture2D>("Rock"), .5f)
                });
            entityPresets.Add(entityPreset.name, entityPreset);

            entityPreset = new EntityTemplate("floor",
                new List<Component>
                {
                    new Name("Floor"), new Floor(), new Position(),
                    new Sprite(contentManager.Load<Texture2D>("Floor"), 0f)
                });
            entityPresets.Add(entityPreset.name, entityPreset);

            entityPreset = new EntityTemplate("sword",
                new List<Component>
                {
                    new Slot("Wieldable"), new Name("Sword"), new Carryable(), new Wieldable("weapon"), new Position(),
                    new Damage(10), new Sprite(contentManager.Load<Texture2D>("Sword"), .25f)
                });
            entityPresets.Add(entityPreset.name, entityPreset);

            entityPreset = new EntityTemplate("player",
                new List<Component>
                {
                    new Turn(), new Equipment(humanoidEquipmentSlots), new Name("Test Name"), new Collideable(),
                    new Position(), new Sprite(contentManager.Load<Texture2D>("Player"), 1), new Player(),
                    new Velocity(), new Health(20), new Inventory()
                });
            entityPresets.Add(entityPreset.name, entityPreset);

            entityPreset = new EntityTemplate("spear",
                new List<Component>
                {
                    new Slot("Wieldable"), new Name("Spear"), new Carryable(), new Wieldable("weapon"), new Position(),
                    new Damage(6), new Sprite(contentManager.Load<Texture2D>("Spear"), .25f)
                });
            entityPresets.Add(entityPreset.name, entityPreset);

            entityPreset = new EntityTemplate("monster",
                new List<Component>
                {
                    new MonsterAI(), new Turn(), new Equipment(humanoidEquipmentSlots), new Name("Monster"),
                    new Collideable(), new Position(), new Sprite(contentManager.Load<Texture2D>("Zombie"), 1),
                    new Velocity(), new Health(6), new Inventory()
                });
            entityPresets.Add(entityPreset.name, entityPreset);

            entityPreset = new EntityTemplate("camera", new List<Component> {new Position(), new Camera()});
            entityPresets.Add(entityPreset.name, entityPreset);

            entityPreset = new EntityTemplate("door",
                new List<Component>
                {
                    new Name("Door"), new Collideable(), new Position(), new Door(),
                    new Sprite(contentManager.Load<Texture2D>("Door"), 1)
                });
            entityPresets.Add(entityPreset.name, entityPreset);

            //entityPreset = new EntityTemplate("debugtile", new List<Component> { new Position(), new Sprite(contentManager.Load<Texture2D>("DebugTile"), 0) });
            //entityPresets.Add(entityPreset.name, entityPreset);
        }
    }
}