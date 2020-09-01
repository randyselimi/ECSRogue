namespace ECSRogue.Components
{
    internal class Stats : Component
    {
        private readonly int agility;
        private readonly int intelligence;
        private readonly int strength;

        private Stats()
        {
        }

        private Stats(Stats stats)
        {
            strength = stats.strength;
            agility = stats.agility;
            intelligence = stats.intelligence;
        }

        public override object Clone()
        {
            return new Stats(this);
        }
    }
}