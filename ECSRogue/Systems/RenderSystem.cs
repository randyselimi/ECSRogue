using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Handlers.Rendering;
using ECSRogue.Handlers.Rendering.RenderComponent;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    internal class RenderSystem : GameSystem
    {
        public RenderSystem(RenderHandler renderHandler)
        {
            entityRenderComponent = (EntityRenderComponent) renderHandler.GetRenderComponent<EntityRenderComponent>();

            entityFilter.AddComponentToFilter<Position>();
            entityFilter.AddComponentToFilter<Sprite>();
        }

        private EntityRenderComponent entityRenderComponent { get; }

        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            foreach (var entity in filteredEntities.Values)
                if (entity.GetComponent<Sprite>().render)
                    entityRenderComponent.AddToDrawQueue(entity.GetComponent<Sprite>().texture2D,
                        Vector2.Multiply(entity.GetComponent<Position>().position,
                            entity.GetComponent<Sprite>().texture2D.Width), Color.White,
                        entity.GetComponent<Sprite>().height);
        }
    }
}