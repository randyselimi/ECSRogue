using System;
using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Handlers.Rendering.RenderComponent;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.UI.UIComponent;
using ECSRogue.UI.UIElement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.Managers
{
    internal class UIManager : IManager
    {
        public Entity camera;

        private readonly EntityManager entityManager;
        public Entity player;

        private Vector2 previousScreenPosition = new Vector2(0, 0);

        public UIRenderComponent renderComponent;
        private readonly Vector2 screenDimensions;

        private Vector2 screenPosition;

        private readonly List<SpriteFont> spriteFonts = new List<SpriteFont>();

        //Extremely disjointed. Very coupled system
        private readonly Dictionary<Type, UIElement> UIElements = new Dictionary<Type, UIElement>();

        public UIManager(GraphicsDevice graphicsDevice, EntityManager entityManager, Vector2 screenDimensions,
            List<SpriteFont> spriteFonts)
        {
            renderComponent = new UIRenderComponent(graphicsDevice);
            this.entityManager = entityManager;

            screenPosition = new Vector2(0);
            this.screenDimensions = screenDimensions;

            this.spriteFonts = spriteFonts;
        }

        //Lots of logic in this should be encapsulated and extracted from this update function
        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue)
        {
            var newScreenPosition = camera.GetComponent<Position>().position;

            var screenOffset = previousScreenPosition - newScreenPosition;
            previousScreenPosition = newScreenPosition;

            screenPosition += screenOffset;
            foreach (var element in UIElements.Values)
            {
                element.Update(screenOffset, eventQueue);
                renderComponent.AddToDrawQueue(element);
            }

            for (var i = 0; i < eventQueue.Count; i++)
                if (eventQueue[i].GetType() == typeof(UIEvent))
                {
                    var uiEvent = (UIEvent) eventQueue[i];

                    if (uiEvent.eventType == "Left_Mouse_Button_Pressed")
                    {
                        //very inefficient
                        var inputComponents = new List<Button>();
                        foreach (var UIElement in UIElements.Values)
                        {
                            inputComponents = UIElement.baseComponent.GetChildren<Button>();

                            foreach (var component in inputComponents)
                            {
                                var collisionBox = new Rectangle((int) component.absolutePosition.X,
                                    (int) component.absolutePosition.Y, (int) component.dimensions.X,
                                    (int) component.dimensions.Y);
                                if (collisionBox.Contains((int) uiEvent.arguments[0] - newScreenPosition.X,
                                    (int) uiEvent.arguments[1] - newScreenPosition.Y))
                                {
                                    component.OnButtonClicked(component, new UIEventArgs(eventQueue));
                                    uiEvent.handled = true;
                                }
                            }
                        }
                    }

                    if (uiEvent.eventType == "I_Pressed")
                        //temporary measure.
                        AddUIElement(new InventoryMenu(entityManager.GetEntitiesByComponent<Player>().FirstOrDefault(),
                            screenPosition, screenDimensions, spriteFonts[0]));

                    if (uiEvent.eventType == "Escape_Pressed")
                        //temporary measure.
                        RemoveUIElement<InventoryMenu>();
                }
        }

        public void CreateDefaultUI()
        {
            AddUIElement(new StatusBar(entityManager.GetEntitiesByComponent<Player>().FirstOrDefault(), screenPosition,
                screenDimensions, spriteFonts[0]));
        }

        //temporary implementation
        public void AddUIElement(UIElement elementToAdd)
        {
            UIElements.TryAdd(elementToAdd.GetType(), elementToAdd);
            //UIElements.Add(new StatusBar(entityManager.GetEntitiesByComponent<Player>().FirstOrDefault(), screenDimensions, spriteFont));
            //UIElements.Add(new InventoryMenu(entityManager.GetEntitiesByComponent<Player>().FirstOrDefault(), screenDimensions, spriteFont));
            //UIElements.Add(new DebugMenu(screenDimensions, spriteFont));
        }

        //Maybe don't use generics here if a need of runtime removal is needed
        public void RemoveUIElement<T>() where T : UIElement
        {
            if (UIElements.ContainsKey(typeof(T))) UIElements.Remove(typeof(T));
        }
    }
}