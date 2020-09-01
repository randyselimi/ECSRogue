namespace ECSRogue.Components
{
    internal class Damage : Component
    {
        public int damageValue;

        public Damage()
        {
        }

        public Damage(int damageValue)
        {
            this.damageValue = damageValue;
        }

        public Damage(Damage damage)
        {
            damageValue = damage.damageValue;
        }

        public override object Clone()
        {
            return new Damage(this);
        }
    }
}