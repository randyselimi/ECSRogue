using System.Collections.Generic;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;

namespace ECSRogue.Systems
{
    public enum SystemTypes
    {
        InputSystem,
        UpdateSystem,
        RenderSystem
    }
    public abstract class GameSystem
    {
        public GameSystem(SystemTypes systemType)
        {
            this.SystemType = systemType;
        }

        //Temporary solution
        public SystemTypes SystemType { get; set; }

        public abstract void Update(PartisInstance instance);
    }
}