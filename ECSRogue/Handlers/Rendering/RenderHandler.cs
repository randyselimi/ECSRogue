using System;
using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Handlers.Rendering.RenderComponent;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.Handlers.Rendering
{
    /// <summary>
    ///     Handles rendering of all game objects including entities and UI. Different aspects of game are drawn by
    ///     RenderComponents which are mangaged by the RenderManager
    /// </summary>
    public class RenderHandler : Handler
    {
        private readonly Dictionary<Type, IRenderComponent> renderComponents = new Dictionary<Type, IRenderComponent>();
        private readonly SpriteBatch spriteBatch;

        public RenderHandler(SpriteBatch spriteBatch, List<IRenderComponent> renderComponents)
        {
            this.spriteBatch = spriteBatch;

            foreach (var renderComponent in renderComponents)
                this.renderComponents.Add(renderComponent.GetType(), renderComponent);
        }

        private void BeginRender(SpriteSortMode spriteSortMode)
        {
            Matrix? translationMatrix = null;
            foreach (var entity in entityManager.GetEntitiesByComponent<Camera>())
                translationMatrix = Matrix.CreateTranslation(new Vector3(entity.GetComponent<Position>().position, 0));

            spriteBatch.Begin(spriteSortMode, transformMatrix: translationMatrix);
        }

        private void EndRender()
        {
            spriteBatch.End();
        }

        private void Clear()
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