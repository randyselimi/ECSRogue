using ECSRogue.Components;
using ECSRogue.Managers;
using ECSRogue.Managers.Entities;

namespace ECSRogue.Factories
{
    public class ComponentFactory
    {

        public ComponentFactory()
        {
        }

        public Component CreateComponent(Component component, Entity entity)
        {
            Component createdComponent = (Component)component.Clone();
            createdComponent.Id = entity.Id;

            return createdComponent;
        }
    }
}