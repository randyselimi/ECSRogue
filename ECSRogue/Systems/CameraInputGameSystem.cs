using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Managers;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ECSRogue.Systems
{
    internal class CameraInputSystem : GameSystem
    {
        public CameraInputSystem() : base(SystemTypes.InputSystem)
        {
        }

        public override void Update(PartisInstance instance)
        {
            var camera = instance.GetEntitiesByIndex(new TypeIndexer(typeof(Camera))).Single();
            var position = new Vector2();
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) position.X += 10;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) position.X -= 10;
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) position.Y += 10;
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) position.Y -= 10;
            camera.GetComponent<Position>().position += position;
        }
    }
}