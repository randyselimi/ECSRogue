using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue2.Components;
using Rogue2.Handlers;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using Rogue2.Managers.Rendering;
using System;
using System.Collections.Generic;

namespace Rogue2.Managers
{
    /// <summary>
    /// Handles rendering of all game objects including entities and UI. Different aspects of game are drawn by RenderComponents which are mangaged by the RenderManager
    /// </summary>
    public class RenderHandler : Handler
    {
        SpriteBatch spriteBatch;
        Dictionary<Type, IRenderComponent> renderComponents = new Dictionary<Type, IRenderComponent>();
        public RenderHandler(SpriteBatch spriteBatch, List<IRenderComponent> renderComponents)
        {
            this.spriteBatch = spriteBatch;

            foreach (var renderComponent in renderComponents)
            {
                this.renderComponents.Add(renderComponent.GetType(), renderComponent);
            }
        }
        void BeginRender(SpriteSortMode spriteSortMode)
        {
            Matrix? translationMatrix = null;
            foreach (Entity entity in entityManager.GetEntitiesByComponent<Camera>())
            {
                translationMatrix = Matrix.CreateTranslation(new Vector3(entity.GetComponent<Position>().position, 0));
            }

            spriteBatch.Begin(spriteSortMode, transformMatrix: translationMatrix);
        }
        void EndRender()
        {
            spriteBatch.End();
        }
        void Clear()
        {
            spriteBatch.GraphicsDevice.Clear(Color.Black);
        }
        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            Clear();

            foreach (var component in renderComponents)
            {
                BeginRender(component.Value.spriteSortMode);
                component.Value.Draw(spriteBatch);
                EndRender();
            }
        }

        public IRenderComponent GetRenderComponent<T>() where T : IRenderComponent
        {
            return renderComponents[typeof(T)];
        }
    }

}
