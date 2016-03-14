using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procajas
{
    public class Constants
    {
        // TODO: Finish Process List
        public enum Processes
        {
            IMP,
            SUA,
            COR,
            ABU
        }

        public static readonly List<string> ProcessList = new List<string>()
        {
            Processes.IMP.ToString(),
            Processes.SUA.ToString(),
            Processes.COR.ToString(),
            Processes.ABU.ToString()
        };

    }
}
