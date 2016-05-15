using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static bool ValidatePositiveInt(string sValue, out int iValue)
        {
            if (!int.TryParse(sValue, out iValue) || iValue < 0)
            {
                return false;
            }

            return true;
        }

        public static string GetDepartmentFromMaterial(string material)
        {
            if (material == null || material.Length < 3)
            {
                return null;
            }

            return material.Substring(0, 3).ToUpperInvariant();
        }

        public static bool ValidateMaterialName(string process, string material)
        {
            if (process == null || material == null || process.Length != 3 || material.Length < 3 || !material.Substring(0, 3).Equals(process, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }

        public static bool ValidateMaterialNameFormat(string material)
        {
            Regex rg = new Regex(@"^[A-Z0-9]{3}_[a-zA-Z0-9]*$");
            return rg.IsMatch(material);
        }

        public static bool ValidateProcessNameFormat(string process)
        {
            Regex rg = new Regex(@"^[A-Z0-9]{3}$");
            return rg.IsMatch(process);
        }
    }
}
