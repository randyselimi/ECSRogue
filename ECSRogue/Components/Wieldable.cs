namespace ECSRogue.Components
{
    internal class Wieldable : Component
    {
        public string slot;

        public Wieldable()
        {
        }

        public Wieldable(string slot)
        {
            this.slot = slot;
        }

        public Wieldable(Wieldable wieldable)
        {
            slot = wieldable.slot;
        }

        public override object Clone()
        {
            return new Wieldable(this);
        }
    }
}