namespace ECSRogue.Components
{
    public class Collideable : Component
    {
        public Collideable()
        {
        }

        public Collideable(Collideable collideable)
        {
        }

        public override object Clone()
        {
            var clone = new Collideable(this);
            return clone;
        }
    }
}