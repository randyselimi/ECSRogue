using System;
using System.Collections.Generic;
using ECSRogue.Data;
using ECSRogue.Managers.Entities;

namespace ECSRogue.Components
{
    /// <summary>
    ///     Represents the equipment of an Entity. Equipment are entities that have a slot that matches the owning entity and
    ///     can be equipped. Revise this!!!
    /// </summary>
    public class Equipment : Component, IParameterizedComponent
    {
        //Slot : Entity. Entity is null if no item is equipped
        public List<EquipmentSlot> equipment = new List<EquipmentSlot>();

        public Equipment(List<ComponentData> datas)
        {
            InitializeFromDefinition(datas);
        }

        public Equipment(Equipment equipment)
        {
            this.equipment = equipment.equipment;
        }

        public override object Clone()
        {
            return new Equipment(this);
        }

        public void InitializeFromDefinition(List<ComponentData> datas)
        {
            foreach (var componentData in datas)
            {
                if (componentData.Id == "Slot")
                {
                    equipment.Add(new EquipmentSlot((string)componentData.Data));
                }
            }
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