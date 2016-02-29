using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procajas
{
    public static class Utilities
    {
        public static bool ValidatePositiveDouble(string sValue, out double dValue)
        {
            if (!double.TryParse(sValue, out dValue) || dValue < 0)
            {
                return false;
            }

            return true;
        }
    }
}
