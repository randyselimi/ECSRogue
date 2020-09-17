using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Managers;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    //Ideally, keyboard input would be transformed into a game input (the same as the AI) and both be processed in the same way
    internal class MovementSystem : GameSystem
    {
        public MovementSystem() : base(SystemTypes.UpdateSystem)
        {
        }

        public override void Update(PartisInstance instance)
        {
            foreach (var gameEvent in instance.GetEvents<GameEvent>())
            {
                if (gameEvent.EventType == GameEvents.Move)
                {
                    var moveEvent = gameEvent as MoveEvent;
                    var movingEntity = moveEvent.MovingEntity;
                    movingEntity.GetComponent<Velocity>().velocity = moveEvent.MovingPosition;
                    movingEntity.GetComponent<Turn>().takenTurn = true;

                }
            }
        }
    }
}