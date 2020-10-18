using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using ECSRogue.UI.UIComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECSRogue.UI.UIElement
{
    class GameLog : UIElement
    {
        private readonly Box logContainer;
        private readonly OrderedList logMessageHolder;
        //Decide how this should be handled
        private readonly LogManager logManager;
        private int currentFetchIndex = 0;
        List<Text> logMessages = new List<Text>();
        SpriteFont font;

        public GameLog(LogManager logManager, Vector2 screenPosition, Vector2 screenDimensions, SpriteFont spriteFont)
        {
            this.logManager = logManager;
            font = spriteFont;
            //TODO refactor assignemnt of base componenet to be required
            logContainer = new Box(new Vector2(400, 800), UIPosition.TopRight, Color.White);
            baseComponent = logContainer;
            logContainer.absolutePosition = logContainer.GetAbsolutePosition(screenPosition, screenDimensions);

            logMessageHolder = new OrderedList(new Vector2(0, 23), baseComponent.dimensions, UIPosition.TopLeft, Color.Black);
            baseComponent.AddChild(logMessageHolder);
        }

        public override void Update(Vector2 screenOffset)
        {
            base.Update(screenOffset);

            List<string> newMessages = logManager.GetNewMessages(currentFetchIndex);
            currentFetchIndex += newMessages.Count;

            foreach (var message in newMessages)
            {
                Text logMessage = new Text(message, font, UIPosition.TopLeft, Color.Black);
                logMessageHolder.AddChild(logMessage);
            }
            
        }
    }
}
