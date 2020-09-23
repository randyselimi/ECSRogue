using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Data;
using ECSRogue.Handlers.Rendering;
using ECSRogue.Handlers.Rendering.RenderComponent;
using ECSRogue.Managers;
using ECSRogue.Managers.Events;
using ECSRogue.Managers.Levels;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ECSRogue.Systems
{
    public class RenderSystem : GameSystem
    {
        private LevelManager levelManager;
        public RenderSystem(RenderHandler renderHandler, LevelManager levelManager) : base(SystemTypes.RenderSystem)
        {
            this.levelManager = levelManager;
            EntityRenderComponent = (EntityRenderProcessor) renderHandler.GetRenderProcessor<EntityRenderProcessor>();
        }

        private EntityRenderProcessor EntityRenderComponent { get; }

        public override void Update(PartisInstance instance)
        {
            foreach (var entity in instance.GetEntitiesByIndexes(new TypeIndexer(typeof(Sprite)), new LevelIndexer(levelManager.GetCurrentLevel().Id)))
            {
                SpriteDefinition definition = entity.GetComponent<Sprite>().sprite;

                    if (entity.GetComponent<Sprite>().render == true)
                    {
                        EntityRenderComponent.AddToDrawQueue(definition.spriteSheet, 
                            Vector2.Multiply(entity.GetComponent<Position>().position, 35), Color.White, entity.GetComponent<Sprite>().height,
                            definition.boundsRectangle);
                    }
            }
        }
    }
}