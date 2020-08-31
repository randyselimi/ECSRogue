﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue2.Managers.Events;
using System.Collections.Generic;
using System.Linq;

namespace Rogue2.UI
{
    class DebugMenu : UIElement
    {
        Box container;
        OrderedList debugList;
        Text debugText1;
        Text debugText2;

        public DebugMenu(Vector2 screenPosition, Vector2 screenDimensions, SpriteFont spriteFont)
        {

            container = new Box(new Vector2(500, 200), UIPosition.TopLeft, Color.White);
            baseComponent = container;
            container.absolutePosition = container.GetAbsolutePosition(screenPosition, screenDimensions);

            debugList = new OrderedList(new Vector2(0, 40), baseComponent.dimensions, UIPosition.TopLeft, Color.Black);
            baseComponent.AddChild(debugList);

            debugText1 = new Text(spriteFont, UIPosition.TopLeft, Color.Black);
            debugText2 = new Text(spriteFont, UIPosition.TopLeft, Color.Black);
            debugList.AddChild(debugText1);
            debugList.AddChild(debugText2);


        }
        public override void Update(Vector2 screenOffset, List<IEvent> eventQueue)
        {
            base.Update(screenOffset, eventQueue);

            var uiEvents = eventQueue.Where(x => x.GetType() == typeof(UIEvent)).ToList();

            for (int i = 0; i < uiEvents.Count; i++)
            {
                UIEvent uiEvent = (UIEvent)uiEvents[i];

                debugText1.displayText = "Previous Keyboard Input: " + uiEvent.arguments[0];
                debugText2.displayText = "Current Keyboard Input: " + uiEvent.arguments[1];
            }
            
        }
    }
}

