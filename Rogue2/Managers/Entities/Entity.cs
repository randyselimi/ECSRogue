using Rogue2.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Managers.Entities
{
    public class Entity
    {
        public int ID { get; }
        public bool active = true;
        public bool alive = true;

        public Dictionary<Type, Component> components { get; set; }

        public Entity(int ID)
        {
            components = new Dictionary<Type, Component>();
            this.ID = ID;
        }

        public T GetComponent<T>() where T : Component
        {
            Component componentToGet = null;
            components.TryGetValue(typeof(T), out componentToGet);
            return (T)componentToGet;
        }

        public bool HasComponent<T>() where T : Component
        {
            if (GetComponent<T>() != null)
            {
                return true;
            }

            return false;
        }
    }
}
