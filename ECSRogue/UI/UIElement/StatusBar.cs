using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.UI.UIComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.UI.UIElement
{
    internal class StatusBar : UIElement
    {
        private readonly Box box;
        private readonly Entity entity;
        private readonly Text equippedWeapon;
        private readonly Text health;

        private readonly Text name;

        public StatusBar(Entity entity, Vector2 screenPosition, Vector2 screenDimensions, SpriteFont spriteFont)
        {
            this.entity = entity;
            box = new Box(new Vector2(200, 100), UIPosition.BottomLeft, Color.White);
            baseComponent = box;
            box.absolutePosition = box.GetAbsolutePosition(screenPosition, screenDimensions);

            var orderedList =
                new OrderedList(new Vector2(0, 40), box.dimensions, UIPosition.TopLeft, Color.Transparent);
            baseComponent.AddChild(orderedList);


            name = new Text("Randy", spriteFont, UIPosition.TopLeft, Color.Black);
            orderedList.AddChild(name);

            health = new Text("HP: ", spriteFont, UIPosition.TopLeft, Color.Black);
            orderedList.AddChild(health);

            equippedWeapon = new Text("None", spriteFont, UIPosition.TopLeft, Color.Black);
            orderedList.AddChild(equippedWeapon);
        }

        public override void Update(Vector2 screenOffset)
        {
            base.Update(screenOffset);
            health.displayText = "HP: " + entity.GetComponent<Health>().healthPoints;
            var equipmentname = "None";

            //NOTE: This logic should probably be encapsulated at some point
            if (entity.GetComponent<Equipment>().equipment.Where(x => x.slot == "Wieldable").FirstOrDefault().entity !=
                null)
            {
                var e = entity.GetComponent<Equipment>().equipment.Where(x => x.slot == "Wieldable").FirstOrDefault()
                    .entity;
                equipmentname = e.GetComponent<Name>().NameSingular;
            }

            equippedWeapon.displayText = "Weapon: " + equipmentname;
        }
    }
}