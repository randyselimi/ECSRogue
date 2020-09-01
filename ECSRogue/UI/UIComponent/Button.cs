using System;
using System.Collections.Generic;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.UI.UIComponent
{
    internal class Button : UIComponent
    {
        //extract into interface
        public delegate void ButtonClickedHandler(object source, UIEventArgs args);

        public Button(Vector2 dimensions, UIPosition relativePosition, Color color, Vector2? offset = null) : base(
            dimensions, relativePosition, color, offset)
        {
        }

        public event ButtonClickedHandler onClick;

        public void ClickButton()
        {
        }

        public void OnButtonClicked(object source, UIEventArgs args)
        {
            onClick?.Invoke(source, args);
        }
    }

    /// <summary>
    ///     Event args used to communicate between UI and Game logic
    /// </summary>
    public class UIEventArgs : EventArgs
    {
        public UIEventArgs(List<IEvent> eventQueue)
        {
            this.eventQueue = eventQueue;
        }

        public List<IEvent> eventQueue { get; set; }
    }
}