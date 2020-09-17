using ECSRogue.Components;
using ECSRogue.Data;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers;

namespace ECSRogue.Factories.EntityFactory
{
    public class EntityFactory
    {
        public EntityFactory()
        {
        }

        public Entity CreateEntity(EntityManager entityManager, EntityDefinition entityDefinition, int id)
        {
            var entityToCreate = new Entity(id, entityManager);

            foreach (var component in entityDefinition.components)
                entityManager.CreateComponent(component, entityToCreate);

            return entityToCreate;
        }
    }
}