namespace ECSRogue.Components
{
    internal class Weight : Component
    {
        private readonly int weight;

        private Weight()
        {
        }

        private Weight(Weight weight)
        {
            this.weight = weight.weight;
        }

        public override object Clone()
        {
            return new Weight(this);
        }
    }
}