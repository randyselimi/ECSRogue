using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Managers;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    internal class InventorySystem : GameSystem
    {
        public InventorySystem() : base(SystemTypes.UpdateSystem)
        {

        }

        public override void Update(PartisInstance instance)
        {
            foreach (var gameEvent in instance.GetEvents<GameEvent>())
            {
                if (gameEvent.EventType == GameEvents.Pickup)
                {
                    var pickupEvent = gameEvent as PickupEvent;

                    var pickingEntity = pickupEvent.PickingEntity;
                    //check if source has inventory

                    if (!pickingEntity.HasComponent<Inventory>()) continue;

                    var pickedEntity = instance.GetEntitiesByIndexes(new TypeIndexer(typeof(Carryable)),
                        new PositionIndexer(pickingEntity.GetComponent<Position>().position)).FirstOrDefault();

                    //this should be fixed. carriables still exist in tile they were picked up in. need to be made inactive or something
                    if (pickedEntity != null)
                    {
                        pickingEntity.GetComponent<Inventory>().inventory.Add(pickedEntity);
                        pickedEntity.GetComponent<Sprite>().render = false;

                        pickingEntity.GetComponent<Turn>().takenTurn = true;
                    }
                }
            }
        }
    }
}