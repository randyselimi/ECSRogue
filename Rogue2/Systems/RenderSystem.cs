using Microsoft.Xna.Framework;
using Rogue2.Components;
using Rogue2.Managers;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using Rogue2.Managers.Rendering.RenderComponent;
using System.Collections.Generic;

namespace Rogue2.Systems
{
    class RenderSystem : GameSystem
    {
        EntityRenderComponent entityRenderComponent { get; set; }

        public RenderSystem(RenderHandler renderHandler)
        {
            this.entityRenderComponent = (EntityRenderComponent)renderHandler.GetRenderComponent<EntityRenderComponent>();

            entityFilter.AddComponentToFilter<Position>();
            entityFilter.AddComponentToFilter<Sprite>();
        }

        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            foreach (Entity entity in filteredEntities.Values)
            {
                if (entity.GetComponent<Sprite>().render == true)
                {
                    entityRenderComponent.AddToDrawQueue(entity.GetComponent<Sprite>().texture2D, Vector2.Multiply(entity.GetComponent<Position>().position, entity.GetComponent<Sprite>().texture2D.Width), Color.White, entity.GetComponent<Sprite>().height);
                }
            }
        }
    }
}
