using System;
using System.Collections.Generic;
using ECSRogue.Components;

namespace ECSRogue.Managers.Entities
{
    public class Entity
    {
        public bool active = true;
        public bool alive = true;

        public Entity(int ID)
        {
            components = new Dictionary<Type, Component>();
            this.ID = ID;
        }

        public int ID { get; }

        public Dictionary<Type, Component> components { get; set; }

        public T GetComponent<T>() where T : Component
        {
            Component componentToGet = null;
            components.TryGetValue(typeof(T), out componentToGet);
            return (T) componentToGet;
        }

        public bool HasComponent<T>() where T : Component
        {
            if (GetComponent<T>() != null) return true;

            return false;
        }
    }
}