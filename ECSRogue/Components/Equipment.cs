using System.Collections.Generic;
using ECSRogue.Data;
using ECSRogue.Managers.Entities;

namespace ECSRogue.Components
{
    /// <summary>
    ///     Represents the equipment of an Entity. Equipment are entities that have a slot that matches the owning entity and
    ///     can be equipped. Revise this!!!
    /// </summary>
    public class Equipment : Component
    {
        //Slot : Entity. Entity is null if no item is equipped
        public List<EquipmentSlot> equipment = new List<EquipmentSlot>();

        public Equipment(List<string> equipmentSlots)
        {
            foreach (var slot in equipmentSlots) equipment.Add(new EquipmentSlot(slot));
        }

        public Equipment(Equipment equipment)
        {
            this.equipment = equipment.equipment;
        }

        public override object Clone()
        {
            return new Equipment(this);
        }
    }

    //Repersentation of an equipment slot
    public class EquipmentSlot
    {
        public Entity entity = null;

        public EquipmentSlot(string slot)
        {
            this.slot = slot;
        }

        public string slot { get; }
    }
}