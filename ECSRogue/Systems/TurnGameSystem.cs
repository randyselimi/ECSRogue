using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using ECSRogue.Systems;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers
{
    internal class TurnSystem : GameSystem
    {
        public TurnSystem() : base(SystemTypes.UpdateSystem)
        {
        }

        public int currentTurn { get; private set; }

        public override void Update(PartisInstance instance)
        {
            var turnAbidingEntities = instance.GetEntitiesByIndex(new TypeIndexer(typeof(Turn)));

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