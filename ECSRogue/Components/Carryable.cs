using ECSRogue.Data;

namespace ECSRogue.Components
{
    internal class Carryable : Component
    {
        public Carryable()
        {
        }

        public Carryable(Carryable carryable)
        {
        }

        public override object Clone()
        {
            return new Carryable(this);
        }

    }
}