using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ECSRogue.Systems
{
    internal class CameraInputSystem : GameSystem
    {
        public CameraInputSystem()
        {
            entityFilter.AddComponentToFilter<Position>();
            entityFilter.AddComponentToFilter<Camera>();
        }

        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            foreach (var entity in filteredEntities.Values)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left)) entity.GetComponent<Position>().position.X += 10;
                if (Keyboard.GetState().IsKeyDown(Keys.Right)) entity.GetComponent<Position>().position.X -= 10;
                if (Keyboard.GetState().IsKeyDown(Keys.Up)) entity.GetComponent<Position>().position.Y += 10;
                if (Keyboard.GetState().IsKeyDown(Keys.Down)) entity.GetComponent<Position>().position.Y -= 10;
            }
        }
    }
}