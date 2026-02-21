using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrispyLo.Classes
{
    public class Ingredient
    {
        private int id;
        private string libelle;
        private decimal prix;

        public int Id { get; set; }
        public string Libelle { get; set; }
        public decimal Prix { get; set; }
        public Boolean EstSupplement { get; set; }
    }
}
