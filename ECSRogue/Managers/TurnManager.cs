﻿using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers
{
    internal class TurnManager : IManager
    {
        private readonly EntityManager entityManager;

        public TurnManager(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        public int currentTurn { get; private set; }

        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue)
        {
            var turnAbidingEntities = entityManager.GetEntitiesByComponent<Turn>();

            var allTurnsTaken = true;

            //Check if every turn abiding entity has taken their turn
            foreach (var turnAbidingEntity in turnAbidingEntities)
                if (turnAbidingEntity.GetComponent<Turn>().takenTurn == false)
                    allTurnsTaken = false;

            //If all turn abiding entities have taken their turn, advance current turn and set taken turn to false
            if (allTurnsTaken)
            {
                currentTurn++;
                foreach (var turnAbidingEntity in turnAbidingEntities)
                    turnAbidingEntity.GetComponent<Turn>().takenTurn = false;
            }
        }
    }
}