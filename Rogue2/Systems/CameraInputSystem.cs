using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rogue2.Components;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System.Collections.Generic;

namespace Rogue2.Systems
{
    class CameraInputSystem : GameSystem
    {
        public CameraInputSystem()
        {
            entityFilter.AddComponentToFilter<Position>();
            entityFilter.AddComponentToFilter<Camera>();
        }

        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            foreach (Entity entity in filteredEntities.Values)
            {
                
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    entity.GetComponent<Position>().position.X += 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    entity.GetComponent<Position>().position.X -= 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    entity.GetComponent<Position>().position.Y += 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    entity.GetComponent<Position>().position.Y -= 10;
                }
            }
        }
    }
}
