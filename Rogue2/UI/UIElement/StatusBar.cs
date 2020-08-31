using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue2.Components;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System.Collections.Generic;
using System.Linq;

namespace Rogue2.UI
{
    class StatusBar : UIElement
    {
        Entity entity;

        Text name;
        Text health;
        Text equippedWeapon;
        Box box;
        public StatusBar(Entity entity, Vector2 screenPosition, Vector2 screenDimensions, SpriteFont spriteFont)
        {
            this.entity = entity;
            box = new Box(new Vector2(200, 100), UIPosition.BottomLeft, Color.White);
            baseComponent = box;
            box.absolutePosition = box.GetAbsolutePosition(screenPosition, screenDimensions);

            OrderedList orderedList = new OrderedList(new Vector2(0, 40), box.dimensions, UIPosition.TopLeft, Color.Transparent);
            baseComponent.AddChild(orderedList);


            name = new Text(spriteFont, UIPosition.TopLeft, Color.Black);
            name.displayText = "Randy";
            orderedList.AddChild(name);

            health = new Text(spriteFont, UIPosition.TopLeft, Color.Black);
            orderedList.AddChild(health);

            equippedWeapon = new Text(spriteFont, UIPosition.TopLeft, Color.Black);
            orderedList.AddChild(equippedWeapon);
            
        }

        public override void Update(Vector2 screenOffset, List<IEvent> eventQueue)
        {
            base.Update(screenOffset, eventQueue);
            health.displayText = "HP: " + entity.GetComponent<Health>().healthPoints.ToString();
            string equipmentname = "None";

            //NOTE: This logic should probably be encapsulated at some point
            if (entity.GetComponent<Equipment>().equipment.Where(x => x.slot == "Wieldable").FirstOrDefault().entity != null)
            {
                Entity e = entity.GetComponent<Equipment>().equipment.Where(x => x.slot == "Wieldable").FirstOrDefault().entity;
                equipmentname = e.GetComponent<Name>().entityName;
            }
            equippedWeapon.displayText = "Weapon: " + equipmentname;
        }


    }
}
