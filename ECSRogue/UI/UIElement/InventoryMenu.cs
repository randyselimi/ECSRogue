using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.UI.UIComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.UI.UIElement
{
    internal class InventoryMenu : UIElement
    {
        private readonly Box container;
        private readonly Entity entity;
        private readonly OrderedList inventory;
        private List<Button> itembuttons = new List<Button>();

        private readonly Dictionary<Box, Entity> items = new Dictionary<Box, Entity>();
        private readonly Text label;


        public InventoryMenu(Entity entity, Vector2 screenPosition, Vector2 screenDimensions, SpriteFont spriteFont)
        {
            this.entity = entity;

            container = new Box(new Vector2(200, 200), UIPosition.Center, Color.White);
            baseComponent = container;
            container.absolutePosition = container.GetAbsolutePosition(screenPosition, screenDimensions);

            label = new Text(spriteFont, UIPosition.Top, Color.Black);
            label.displayText = "Inventory";
            baseComponent.AddChild(label);

            inventory = new OrderedList(new Vector2(0, 40), baseComponent.dimensions, UIPosition.TopLeft, Color.Black);
            baseComponent.AddChild(inventory);
        }

        public void OnButtonLeftClicked(object source, UIEventArgs args)
        {
            var clickedButton = (Button) source;
            args.eventQueue.Add(new GameEvent("Equip", new List<Entity> {entity, items[(Box) clickedButton.parent]}));
        }


        public override void Update(Vector2 screenOffset, List<IEvent> eventQueue)
        {
            base.Update(screenOffset, eventQueue);

            if (items.Count != entity.GetComponent<Inventory>().inventory.Count)
            {
                items.Clear();
                inventory.RemoveAllChildren();

                foreach (var item in entity.GetComponent<Inventory>().inventory)
                {
                    var itemBox = new Box(item.GetComponent<Sprite>().sprite, new Vector2(32, 32),
                        UIPosition.TopLeft);
                    var buttonToAdd = new Button(itemBox.dimensions, UIPosition.TopLeft, Color.Transparent);
                    buttonToAdd.onClick += OnButtonLeftClicked;
                    inventory.AddChild(itemBox);
                    itemBox.AddChild(buttonToAdd);
                    items.Add(itemBox, item);
                }
            }
        }
    }
}