using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CrispyLo.Classes;

namespace CrispyLo.Views.Info
{
    /// <summary>
    /// Logique d'interaction pour Remise.xaml
    /// </summary>
    public partial class Remise : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        int Index = 0;
        Produit p;
        public Remise(int index)
        {
            InitializeComponent();
            PrepareInitialize(index);
        }

        public void PrepareInitialize(int index)
        {
            p = PagePrincipale.Panier[index];
            this.Width = 400;
            this.Height = 500;
            texte.Text = p.Prix.ToString();
            Index = index;
        }

        public void Click_Gratuit(object sender, RoutedEventArgs e)
        {
            Produit p = PagePrincipale.Panier[Index];
            texte.Text = "0";
        }

        public void Click_Fidélité(object sender, RoutedEventArgs e)
        {
            Produit p = PagePrincipale.Panier[Index];
            if (p.Prix > 11)
            {
                decimal result = p.Prix - 12;
                texte.Text = result.ToString();
            }
        }
        public void Click_Prix(object sender, RoutedEventArgs e)
        {
            texte.IsReadOnly = false;
        }

        public void Click_Leave(object sender, RoutedEventArgs e)
        {
            p.Prix = decimal.Parse(texte.Text);
            PagePrincipale.MainInfo.Visibility = Visibility.Collapsed;
        }

        public void Click_GratuitTotal(object sender, RoutedEventArgs e)
        {
            foreach (Produit pAll in PagePrincipale.Panier)
            {
                pAll.Prix = 0;
                foreach (Ingredient ing in pAll.Ingredients)
                {
                    ing.Prix = 0;
                }
            }
            texte.Text = "0";
        }
    }
}
