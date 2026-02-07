using CrispyLo.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrispyLo.Modules
{
    static class Traitement
    {

        public static string[] descToIngredient(string description)
        {
            description = description.Replace("(", "").Replace(")", "");
            return description.Split(", ");
        }

        public static double prixTotal()
        {
            MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;

            double montantTotal = 0;
            foreach (Produit produit in PagePrincipale.Panier)
            {
                montantTotal += produit.prixTotal;
            }

            return montantTotal;
        }
    }
}
