namespace ECSRogue.Components
{
    internal class Player : Component
    {
        public Player()
        {
        }

        public Player(Player player)
        {
        }

        public override object Clone()
        {
            var clone = new Player(this);
            return clone;
        }

    }
}