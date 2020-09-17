using System;
using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Handlers.Rendering.RenderComponent;
using ECSRogue.Managers;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSRogue.Handlers.Rendering
{
    /// <summary>
    ///     Handles rendering of all game objects including entities and UI. Different aspects of game are drawn by
    ///     RenderComponents which are mangaged by the RenderManager
    /// </summary>
    public class RenderHandler
    {
        private readonly Dictionary<Type, IRenderProcessor> renderProcessors = new Dictionary<Type, IRenderProcessor>();
        private readonly SpriteBatch spriteBatch;

        public RenderHandler(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Initialize(List<IRenderProcessor> renderProcessors)
        {
            foreach (var renderProcessor in renderProcessors)
                this.renderProcessors.Add(renderProcessor.GetType(), renderProcessor);
        }

        private void BeginRender(SpriteSortMode spriteSortMode, PartisInstance instance)
        {
            Matrix? translationMatrix = null;
            var camera = instance.GetEntitiesByIndex(new TypeIndexer(typeof(Camera))).Single();
            translationMatrix = Matrix.CreateTranslation(new Vector3(camera.GetComponent<Position>().position, 0));

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

        public void Draw(GameTime gameTime, PartisInstance instance)
        {
            Clear();

            foreach (var component in renderProcessors)
            {
                BeginRender(component.Value.spriteSortMode, instance);
                component.Value.Draw(spriteBatch);
                EndRender();
            }
        }

        public T GetRenderProcessor<T>() where T : IRenderProcessor
        {
            return (T)renderProcessors[typeof(T)];
        }
    }
}