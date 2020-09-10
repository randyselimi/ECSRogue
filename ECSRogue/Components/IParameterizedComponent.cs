using System;
using System.Collections.Generic;
using System.Text;
using ECSRogue.Data;

namespace ECSRogue.Components
{
    interface IParameterizedComponent
    {
        public void InitializeFromDefinition(List<ComponentData> datas);
    }
}
