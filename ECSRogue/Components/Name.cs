using System.Collections.Generic;
using ECSRogue.Data;

namespace ECSRogue.Components
{
    internal class Name : Component, IParameterizedComponent
    {
        public string NameSingular;
        public string NamePlural;
        public string Description;

        public Name(List<ComponentData> datas)
        {
            InitializeFromDefinition(datas);
        }

        public Name(Name name)
        {
            NameSingular = name.NameSingular;
        }

        public override object Clone()
        {
            var clone = new Name(this);
            return clone;
        }

        public void InitializeFromDefinition(List<ComponentData> datas)
        {
            foreach (ComponentData componentData in datas)
            {
                if (componentData.Id == "NameSingular")
                {
                    NameSingular = (string)componentData.Data;
                }
                if (componentData.Id == "NamePlural")
                {
                    NamePlural = (string)componentData.Data;
                }
                if (componentData.Id == "Description")
                {
                    Description = (string)componentData.Data;
                }
            }
        }
    }
}