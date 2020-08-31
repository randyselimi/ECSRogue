using Microsoft.Xna.Framework;
using Rogue2.Components;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System.Collections.Generic;
using System.Linq;

namespace Rogue2.Systems
{
    //Ideally, keyboard input would be transformed into a game input (the same as the AI) and both be processed in the same way
    class MovementSystem : GameSystem
    {

        public MovementSystem( )
        {
            entityFilter.AddComponentToFilter<Position>();
            entityFilter.AddComponentToFilter<Velocity>();
            entityFilter.AddComponentToFilter<Sprite>();
            entityFilter.AddComponentToFilter<Player>();
        }
        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            Vector2 deltaVelocity = new Vector2();


            for (int i = 0; i < eventQueue.Count; i++)
            {
                if (eventQueue[i].GetType() == typeof(GameEvent))
                {
                    GameEvent gameEvent = (GameEvent)eventQueue[i];

                    if (gameEvent.eventType == "Move_Up")
                    {
                        deltaVelocity.Y = -1;
                    }
                    else if (gameEvent.eventType == "Move_Down")
                    {
                        deltaVelocity.Y = 1;
                    }
                    else if (gameEvent.eventType == "Move_Left")
                    {
                        deltaVelocity.X = -1;
                    }
                    else if (gameEvent.eventType == "Move_Right")
                    {
                        deltaVelocity.X = 1;
                    }


                    foreach (Entity entity in gameEvent.entities.Where(x => filteredEntities.ContainsKey(x.ID)).ToList())
                    {                       
                        Velocity entityVelocity = entity.GetComponent<Velocity>();

                        entityVelocity.velocity.X = deltaVelocity.X;
                        entityVelocity.velocity.Y = deltaVelocity.Y;

                        if (deltaVelocity.X != 0 || deltaVelocity.Y != 0)
                        {
                            entity.GetComponent<Turn>().takenTurn = true;
                        }
                    }
                }
            }   
        }
    }
}
