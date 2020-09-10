using System.Collections.Generic;
using System.Linq;
using ECSRogue.Data;

namespace ECSRogue.Components
{
    internal class Health : Component, IXmlParameterComponent
    {
        public int healthPoints;

        public Health(List<ComponentData> datas)
        {
            InitializeFromDefinition(datas);
        }

        public Health(Health health)
        {
            healthPoints = health.healthPoints;
        }

        public override object Clone()
        {
            return new Health(this);
        }

        public void InitializeFromDefinition(List<ComponentData> datas)
        {
            foreach (ComponentData data in datas)
            {
                if (data.Id == "Hp")
                {
                    healthPoints = (int)data.Data;
                }
            }
        }
    }
}