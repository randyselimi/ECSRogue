using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue2.Components;
using Rogue2.Handlers;
using Rogue2.Managers;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using Rogue2.Managers.Rendering.RenderComponent;
using Rogue2.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rogue2.Managers
{

    class UIManager : IManager
    {
        //Extremely disjointed. Very coupled system
        Dictionary<Type, UIElement> UIElements = new Dictionary<Type, UIElement>();

        public UIRenderComponent renderComponent;

        Vector2 screenPosition;
        Vector2 screenDimensions;

        EntityManager entityManager;

        Vector2 previousScreenPosition = new Vector2(0, 0);

        public Entity camera;
        public Entity player;

        List<SpriteFont> spriteFonts = new List<SpriteFont>();

        public UIManager(GraphicsDevice graphicsDevice, EntityManager entityManager, Vector2 screenDimensions, List<SpriteFont> spriteFonts)
        {
            renderComponent = new UIRenderComponent(graphicsDevice);
            this.entityManager = entityManager;

            this.screenPosition = new Vector2(0);
            this.screenDimensions = screenDimensions;

            this.spriteFonts = spriteFonts;
        }
        //Lots of logic in this should be encapsulated and extracted from this update function
        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue)
        {
            Vector2 newScreenPosition = camera.GetComponent<Position>().position;

            Vector2 screenOffset = previousScreenPosition - newScreenPosition;
            previousScreenPosition = newScreenPosition;

            screenPosition += screenOffset;
            foreach (var element in UIElements.Values)
            {
                element.Update(screenOffset, eventQueue);
                renderComponent.AddToDrawQueue(element);
            }

            for (int i = 0; i < eventQueue.Count; i++)
            {
                if (eventQueue[i].GetType() == typeof(UIEvent))
                {
                    UIEvent uiEvent = (UIEvent)eventQueue[i];

                    if (uiEvent.eventType == "Left_Mouse_Button_Pressed")
                    {
                        //very inefficient
                        List<Button> inputComponents = new List<Button>();
                        foreach (var UIElement in UIElements.Values)
                        {
                            inputComponents = UIElement.baseComponent.GetChildren<Button>();

                            foreach (var component in inputComponents)
                            {
                                Rectangle collisionBox = new Rectangle((int)component.absolutePosition.X, (int)component.absolutePosition.Y, (int)component.dimensions.X, (int)component.dimensions.Y);
                                if (collisionBox.Contains((int)uiEvent.arguments[0] - newScreenPosition.X, (int)uiEvent.arguments[1] - newScreenPosition.Y))
                                {
                                    component.OnButtonClicked(component, new UIEventArgs(eventQueue));
                                    uiEvent.handled = true;
                                }
                            }
                        }
                    }

                    if (uiEvent.eventType == "I_Pressed")
                    {
                        //temporary measure.
                        AddUIElement(new InventoryMenu(entityManager.GetEntitiesByComponent<Player>().FirstOrDefault(), screenPosition, screenDimensions, spriteFonts[0]));
                    }

                    if (uiEvent.eventType == "Escape_Pressed")
                    {
                        //temporary measure.
                        RemoveUIElement<InventoryMenu>();
                    }
                }            
            }
        }

        public void CreateDefaultUI()
        {
            AddUIElement(new StatusBar(entityManager.GetEntitiesByComponent<Player>().FirstOrDefault(), screenPosition, screenDimensions, spriteFonts[0]));
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
            if (UIElements.ContainsKey(typeof(T)))
            {
                UIElements.Remove(typeof(T));
            }
        }


    }
}
