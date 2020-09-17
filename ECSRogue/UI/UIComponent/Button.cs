using System;
using System.Collections.Generic;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;

namespace ECSRogue.UI.UIComponent
{
    internal class Button : UIComponent
    {
        //extract into interface
        public delegate void ButtonClickedHandler(object source, PartisInstance instance);

        public Button(Vector2 dimensions, UIPosition relativePosition, Color color, Vector2? offset = null) : base(
            dimensions, relativePosition, color, offset)
        {
        }

        public event ButtonClickedHandler onClick;

        public void ClickButton()
        {
        }

        public void OnButtonClicked(object source, PartisInstance instance)
        {
            onClick?.Invoke(source, instance);
        }
    }
}