namespace ECSRogue.Components
{
    /// <summary>
    ///     Represents a slot this entity is able to be equipped to
    ///     TODO: possible create an enum for this?
    /// </summary>
    public class Slot : Component
    {
        public string equipmentSlot;

        public Slot(string equipmentSlot)
        {
            this.equipmentSlot = equipmentSlot;
        }

        public Slot(Slot slot)
        {
            equipmentSlot = slot.equipmentSlot;
        }

        public override object Clone()
        {
            return new Slot(this);
        }
    }
}