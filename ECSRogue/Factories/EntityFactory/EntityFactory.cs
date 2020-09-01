using ECSRogue.Components;
using ECSRogue.Data;
using ECSRogue.Managers.Entities;

namespace ECSRogue.Factories.EntityFactory
{
    public class EntityFactory
    {
        private EntityTemplate entityTemplate;

        public EntityFactory(EntityTemplate entityTemplate)
        {
            this.entityTemplate = entityTemplate;
        }

        public Entity CreateEntity(int ID)
        {
            var entityToCreate = new Entity(ID);

            foreach (var component in entityTemplate.components)
                entityToCreate.components.Add(component.GetType(), (Component) component.Clone());

            return entityToCreate;
        }
    }
}