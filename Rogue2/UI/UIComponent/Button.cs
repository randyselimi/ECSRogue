using Microsoft.Xna.Framework;
using Rogue2.Managers.Events;
using System;
using System.Collections.Generic;

namespace Rogue2.UI
{

    class Button : UIComponent
    {
        //extract into interface
        public delegate void ButtonClickedHandler(object source, UIEventArgs args);
        public Button(Vector2 dimensions, UIPosition relativePosition, Color color, Vector2? offset = null) : base(dimensions, relativePosition, color, offset)
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
    /// Event args used to communicate between UI and Game logic
    /// </summary>
    public class UIEventArgs : EventArgs
    {
        public List<IEvent> eventQueue { get; set; }

        public UIEventArgs(List<IEvent> eventQueue)
        {
            this.eventQueue = eventQueue;
        }
    }
}
