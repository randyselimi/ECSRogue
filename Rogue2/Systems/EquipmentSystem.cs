using Microsoft.Xna.Framework;
using Rogue2.Components;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System.Collections.Generic;

namespace Rogue2.Systems
{
    /// <summary>
    /// Handles validation, equipping, and unequipping of items for entity
    /// </summary>
    class EquipmentSystem : GameSystem
    {
        public EquipmentSystem()
        {
            entityFilter.AddComponentToFilter<Equipment>();
        }

        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            for (int i = 0; i < eventQueue.Count; i++)
            {
                if (eventQueue[i].GetType() == typeof(GameEvent))
                {
                    GameEvent gameEvent = (GameEvent)eventQueue[i];

                    //Entity will attempt to equip item to valid slot
                    if (gameEvent.eventType == "Equip")
                    {                       
                        Entity source = gameEvent.entities[0];
                        Entity equipment = gameEvent.entities[1];

                        Equipment sourceEquipment = source.GetComponent<Equipment>();
                        Slot equipmentSlot = equipment.GetComponent<Slot>();

                        //If Entity does not have the ability to equip items or the equipment has no valid slot, invalid operation. return.
                        if (sourceEquipment == null || equipmentSlot == null)
                        {
                            continue;
                        }


                        foreach (var slot in sourceEquipment.equipment)
                        {
                            //Check if Entity has a valid slot matching the slot of the item
                            if (slot.slot == equipmentSlot.equipmentSlot)
                            {
                                //If so, equip item to slot
                                slot.entity = equipment;
                            }
                        }
                    }
                }
            }
        }
    }
}
