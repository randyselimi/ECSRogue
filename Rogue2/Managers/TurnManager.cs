using Microsoft.Xna.Framework;
using Rogue2.Components;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System.Collections.Generic;

namespace Rogue2.Managers
{
    class TurnManager : IManager
    {
        public int currentTurn { get; private set; } = 0;

        EntityManager entityManager;

        public TurnManager(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue)
        {
            List<Entity> turnAbidingEntities = entityManager.GetEntitiesByComponent<Turn>();

            bool allTurnsTaken = true;

            //Check if every turn abiding entity has taken their turn
            foreach (var turnAbidingEntity in turnAbidingEntities)
            {
                if (turnAbidingEntity.GetComponent<Turn>().takenTurn == false)
                {
                    allTurnsTaken = false;
                }
            }

            //If all turn abiding entities have taken their turn, advance current turn and set taken turn to false
            if (allTurnsTaken == true)
            {
                currentTurn++;
                foreach (var turnAbidingEntity in turnAbidingEntities)
                {
                    turnAbidingEntity.GetComponent<Turn>().takenTurn = false;
                }
            }
        }
    }
}
