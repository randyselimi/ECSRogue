using Rogue2.Components;
using Rogue2.Data;
using Rogue2.Managers.Entities;

namespace Rogue2.Factories
{
    public class EntityFactory
    {
        EntityTemplate entityTemplate;
        public EntityFactory(EntityTemplate entityTemplate)
        {
            this.entityTemplate = entityTemplate;
        }        
        public Entity CreateEntity(int ID)
        {
            Entity entityToCreate = new Entity(ID);

            foreach (Component component in entityTemplate.components)
            {
                entityToCreate.components.Add(component.GetType(), (Component)component.Clone());
            }

            return entityToCreate;
        }
    }
}
