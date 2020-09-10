using System;
using System.Collections.Generic;
using System.Text;
using ECSRogue.Data;

namespace ECSRogue.Components
{
    interface IXmlParameterComponent
    {
        public void InitializeFromDefinition(List<ComponentData> datas);
    }
}
