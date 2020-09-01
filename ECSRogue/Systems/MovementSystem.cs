using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    //Ideally, keyboard input would be transformed into a game input (the same as the AI) and both be processed in the same way
    internal class MovementSystem : GameSystem
    {
        public MovementSystem()
        {
            entityFilter.AddComponentToFilter<Position>();
            entityFilter.AddComponentToFilter<Velocity>();
            entityFilter.AddComponentToFilter<Sprite>();
        }

        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            for (var i = 0; i < eventQueue.Count; i++)
            {
                var deltaVelocity = new Vector2();
                if (eventQueue[i].GetType() == typeof(GameEvent))
                {
                    var gameEvent = (GameEvent) eventQueue[i];
                    var movingEntity = gameEvent.entities[0];

                    movingEntity.GetComponent<Velocity>().velocity = Vector2.Zero;

                    if (gameEvent.eventType == "Move_Up")
                    {
                        deltaVelocity.Y = -1;
                        movingEntity.GetComponent<Turn>().takenTurn = true;
                    }
                    else if (gameEvent.eventType == "Move_Down")
                    {
                        deltaVelocity.Y = 1;
                        movingEntity.GetComponent<Turn>().takenTurn = true;
                    }
                    else if (gameEvent.eventType == "Move_Left")
                    {
                        deltaVelocity.X = -1;
                        movingEntity.GetComponent<Turn>().takenTurn = true;
                    }
                    else if (gameEvent.eventType == "Move_Right")
                    {
                        deltaVelocity.X = 1;
                        movingEntity.GetComponent<Turn>().takenTurn = true;
                    }

                    movingEntity.GetComponent<Velocity>().velocity = deltaVelocity;
                }
            }
        }
    }
}