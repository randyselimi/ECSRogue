using System;
using System.Collections.Generic;
using ECSRogue.Data;

namespace ECSRogue.Components
{
    /// <summary>
    ///     Represents a slot this entity is able to be equipped to
    ///     TODO: possible create an enum for this?
    /// </summary>
    public class Slot : Component, IParameterizedComponent
    {
        public string equipmentSlot;

        public Slot(List<ComponentData> datas)
        {
            InitializeFromDefinition(datas);
        }

        public Slot(Slot slot)
        {
            equipmentSlot = slot.equipmentSlot;
        }

        public override object Clone()
        {
            return new Slot(this);
        }

        public void InitializeFromDefinition(List<ComponentData> datas)
        {
            foreach (var componentData in datas)
            {
                if (componentData.Id == "Slot")
                {
                    equipmentSlot = (string) componentData.Data;
                }
            }
        }
    }
}