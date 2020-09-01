namespace ECSRogue.Components
{
    internal class Health : Component
    {
        public int healthPoints;

        public Health()
        {
        }

        public Health(int healthPoints)
        {
            this.healthPoints = healthPoints;
        }

        public Health(Health health)
        {
            healthPoints = health.healthPoints;
        }

        public override object Clone()
        {
            return new Health(this);
        }
    }
}