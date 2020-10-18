using System;
using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Handlers.Rendering.RenderComponent;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using ECSRogue.UI.UIComponent;
using ECSRogue.UI.UIElement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.Managers
{
    internal class UIHandler
    {
        //temporary
        public Entity camera;
        public Entity player;

        public UIRenderProcessor renderProcessor;

        private readonly Vector2 screenDimensions;
        private Vector2 previousScreenPosition = new Vector2(0);
        private Vector2 screenPosition = new Vector2(0);

        private List<SpriteFont> spriteFonts = new List<SpriteFont>();

        //Extremely disjointed. Very coupled system
        private readonly Dictionary<Type, UIElement> UIElements = new Dictionary<Type, UIElement>();

        public UIHandler(Vector2 screenDimensions)
        {
            this.screenDimensions = screenDimensions;
        }

        public void Initialize(GameInstance gameInstance, UIRenderProcessor processor, params SpriteFont[] spriteFonts)
        {
            camera = gameInstance.partisInstance.GetEntitiesByIndex(new TypeIndexer(typeof(Camera))).Single();
            player = gameInstance.partisInstance.GetEntitiesByIndex(new TypeIndexer(typeof(Player))).Single();

            this.spriteFonts = spriteFonts.ToList();

            renderProcessor = processor;

            CreateDefaultUI(gameInstance);
        }

        //Lots of logic in this should be encapsulated and extracted from this update function
        public void Update(PartisInstance instance)
        {
            var newScreenPosition = camera.GetComponent<Position>().position;

            var screenOffset = previousScreenPosition - newScreenPosition;
            previousScreenPosition = newScreenPosition;

            screenPosition += screenOffset;
            foreach (var element in UIElements.Values)
            {
                element.Update(screenOffset);
                renderProcessor.AddToDrawQueue(element);
            }

            foreach (var uiEvent in instance.GetEvents<UiEvent>())
            {

                if (uiEvent.EventType == UiEvents.LeftMouseClick)
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
                                component.OnButtonClicked(component, instance);
                                uiEvent.handled = true;
                            }
                        }
                    }
                }

                if (uiEvent.EventType == UiEvents.Inventory)
                {
                    //temporary measure. should close inventory menu if menu is already open
                    AddUIElement(new InventoryMenu(player,
                        screenPosition, screenDimensions, spriteFonts[0]));
                }

                if (uiEvent.EventType == UiEvents.Exit)
                {
                        //temporary measure.
                        RemoveUIElement<InventoryMenu>();
                }
            }
        }

        public void CreateDefaultUI(GameInstance gameInstance)
        {
            AddUIElement(new StatusBar(player, screenPosition,
                screenDimensions, spriteFonts[0]));
            AddUIElement(new GameLog(gameInstance.logManager, screenPosition, screenDimensions, spriteFonts[0]));
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