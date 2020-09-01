using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Managers.Events;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    /// <summary>
    ///     Handles validation, equipping, and unequipping of items for entity
    /// </summary>
    internal class EquipmentSystem : GameSystem
    {
        public EquipmentSystem()
        {
            entityFilter.AddComponentToFilter<Equipment>();
        }

        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            for (var i = 0; i < eventQueue.Count; i++)
                if (eventQueue[i].GetType() == typeof(GameEvent))
                {
                    var gameEvent = (GameEvent) eventQueue[i];

                    //Entity will attempt to equip item to valid slot
                    if (gameEvent.eventType == "Equip")
                    {
                        var source = gameEvent.entities[0];
                        var equipment = gameEvent.entities[1];

                        var sourceEquipment = source.GetComponent<Equipment>();
                        var equipmentSlot = equipment.GetComponent<Slot>();

                        //If Entity does not have the ability to equip items or the equipment has no valid slot, invalid operation. return.
                        if (sourceEquipment == null || equipmentSlot == null) continue;


                        foreach (var slot in sourceEquipment.equipment)
                            //Check if Entity has a valid slot matching the slot of the item
                            if (slot.slot == equipmentSlot.equipmentSlot)
                                //If so, equip item to slot
                                slot.entity = equipment;
                    }
                }
        }
    }
}