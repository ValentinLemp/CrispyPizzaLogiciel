using CrispyLo.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CrispyLo.Views.Info
{
    /// <summary>
    /// Logique d'interaction pour Calculette.xaml
    /// </summary>
    public partial class Calculette : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        public Calculette(int index)
        {
            InitializeComponent();
            PrepareInitialize(index);
        }

        public void PrepareInitialize(int index)
        {
            this.Height = 450;
            this.Width = 365;
            Produit p = PagePrincipale.Panier[index];
            prixCommande.Text = p.Prix + " €";
            prixCommande.Tag = p.Prix;
            argentReçu.Tag = 0.0;
        }

        public void Click_Montant(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            argentReçu.Tag = (double)argentReçu.Tag + double.Parse(btn.Content.ToString());
            argentReçu.Text = argentReçu.Tag.ToString();
        }

        public void Click_Clear(object sender, RoutedEventArgs e) 
        {
            argentReçu.Tag = 0.0;
            argentReçu.Text = "";
        }

        public void Click_Leave(object sender, RoutedEventArgs e)
        { 
            PagePrincipale.MainInfo.Visibility = Visibility.Collapsed;
        }

        public void ReçuChanger(object sender, RoutedEventArgs e)
        {
            rendre.Text = (double)argentReçu.Tag - (double)prixCommande.Tag + " €";
        }
    }
}
