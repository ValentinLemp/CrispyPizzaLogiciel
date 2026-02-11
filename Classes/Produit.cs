using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrispyLo.Classes
{
    public class Produit
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public double Prix { get; set; }
        public Boolean EstLarge { get; set; }
        public string TypePate { get; set; }
        public int IdTypeProduit { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Note {get; set;}
        public double prixTotal => (Prix + (TypePate != null && (TypePate == "tagliatelle" || TypePate == "tortellini") ? 1 : 0) + Ingredients.Sum(i => (i.EstSupplement ? i.Prix : 0)));
    }
}
