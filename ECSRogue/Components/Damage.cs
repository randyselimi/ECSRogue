﻿using System.Collections.Generic;
using ECSRogue.Data;

namespace ECSRogue.Components
{
    internal class Damage : Component, IParameterizedComponent
    {
        public int damageValue;

        public Damage(List<ComponentData> datas)
        {
            InitializeFromDefinition(datas);
        }

        public Damage(int damageValue)
        {
            this.damageValue = damageValue;
        }

        public Damage(Damage damage)
        {
            damageValue = damage.damageValue;
        }

        public override object Clone()
        {
            return new Damage(this);
        }
        public void InitializeFromDefinition(List<ComponentData> datas)
        {
            foreach (var data in datas)
            {
                if (data.Id == "Damage")
                {
                    damageValue = int.Parse((string)data.Data);
                }
            }
        }
    }
}