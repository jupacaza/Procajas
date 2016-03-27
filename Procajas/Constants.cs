using Procajas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procajas
{
    public class Constants
    {        
        public static readonly List<TypeValueDescription> AdminOperationList = new List<TypeValueDescription>()
        {
            new TypeValueDescription { Value = AdminOperations.Create, Description = Properties.Resources.createText },
            new TypeValueDescription { Value = AdminOperations.Delete, Description = Properties.Resources.deleteText },
        };

        public static readonly List<TypeValueDescription> AdminItemTypeList = new List<TypeValueDescription>()
        {
            new TypeValueDescription { Value = AdminItemTypes.Material, Description = Properties.Resources.materialText },
            new TypeValueDescription { Value = AdminItemTypes.Process, Description = Properties.Resources.processText },
            new TypeValueDescription { Value = AdminItemTypes.Location, Description = Properties.Resources.locationText },
        };
    }
}
