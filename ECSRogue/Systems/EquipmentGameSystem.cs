using System.Collections.Generic;
using ECSRogue.Components;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    /// <summary>
    ///     Handles validation, equipping, and unequipping of items for entity
    /// </summary>
    internal class EquipmentSystem : GameSystem
    {
        public EquipmentSystem() : base(SystemTypes.UpdateSystem)
        {
        }

        public override void Update(PartisInstance instance)
        {
            foreach (var gameEvent in instance.GetEvents<GameEvent>())
            {
                //Entity will attempt to equip item to valid slot
                    if (gameEvent.EventType == GameEvents.Equip)
                    {
                        var equipEvent = gameEvent as EquipEvent;
                        var equippingEntity = equipEvent.EquippingEntity;
                        var equippedEntity = equipEvent.EquippedEntity;

                        var sourceEquipment = equippingEntity.GetComponent<Equipment>();
                        var equipmentSlot = equippedEntity.GetComponent<Slot>();

                        //If Entity does not have the ability to equip items or the equipment has no valid slot, invalid operation. return.
                        if (sourceEquipment == null || equipmentSlot == null) continue;


                        foreach (var slot in sourceEquipment.equipment)
                            //Check if Entity has a valid slot matching the slot of the item
                            if (slot.slot == equipmentSlot.equipmentSlot)
                                //If so, equip item to slot
                                slot.entity = equippedEntity;
                    }
                }
        }
    }
}