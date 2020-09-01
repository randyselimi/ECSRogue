namespace ECSRogue.Components
{
    internal class Name : Component
    {
        public string entityName;

        public Name(string entityName)
        {
            this.entityName = entityName;
        }

        public Name(Name name)
        {
            entityName = name.entityName;
        }

        public override object Clone()
        {
            var clone = new Name(this);
            return clone;
        }
    }
}