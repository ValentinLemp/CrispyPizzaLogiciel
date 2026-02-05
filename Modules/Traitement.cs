using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrispyLo.Modules
{
    internal class Traitement
    {
        public static string[] descToIngredient(string description)
        {
            description = description.Replace("(", "").Replace(")", "");
            return description.Split(", ");
        }
    }
}
