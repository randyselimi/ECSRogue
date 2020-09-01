namespace ECSRogue.Components
{
    internal class Wall : Component
    {
        public Wall()
        {
        }

        public Wall(Wall wall)
        {
        }

        public override object Clone()
        {
            return new Wall(this);
        }
    }
}