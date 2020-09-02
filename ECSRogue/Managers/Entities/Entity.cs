using System;
using System.Collections.Generic;
using System.Security.Claims;
using ECSRogue.Components;

namespace ECSRogue.Managers.Entities
{
    public class Entity
    {
        public bool active = true;
        public bool alive = true;

        private readonly EntityManager _entityManager;

        public Entity(int id, EntityManager entityManager)
        {
            //components = new Dictionary<Type, Component>();
            this.Id = id;
            this._entityManager = entityManager;
        }

        public int Id { get; }

        //public Dictionary<Type, Component> components { get; set; }

        public T GetComponent<T>() where T : Component
        {
            return (T)_entityManager.GetComponent(this, typeof(T));
        }

        public bool HasComponent<T>() where T : Component
        {
            return _entityManager.HasComponent(this, typeof(T));
        }

        public bool HasComponents(List<Type> componentList)
        {
            return _entityManager.HasComponents(this, componentList);
        }
    }
}