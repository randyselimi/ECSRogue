namespace ECSRogue.Components
{
    /// <summary>
    ///     Represents an entity that abides by the turn system and whether or not the entity has taken its turn
    /// </summary>
    internal class Turn : Component
    {
        public bool takenTurn = false;

        public Turn()
        {
        }

        private Turn(Turn turn)
        {
        }

        public override object Clone()
        {
            return new Turn(this);
        }
    }
}