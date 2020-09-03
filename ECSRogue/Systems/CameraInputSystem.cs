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
                var position = new Vector2();
                if (Keyboard.GetState().IsKeyDown(Keys.Left)) position.X += 10;
                if (Keyboard.GetState().IsKeyDown(Keys.Right)) position.X -= 10;
                if (Keyboard.GetState().IsKeyDown(Keys.Up)) position.Y += 10;
                if (Keyboard.GetState().IsKeyDown(Keys.Down)) position.Y -= 10;
                entity.GetComponent<Position>().position += position;
            }
        }
    }
}