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
        decimal montantTotal;
        public Calculette(decimal index)
        {
            InitializeComponent();
            PrepareInitialize(index);
        }

        public void PrepareInitialize(decimal index)
        {
            this.Height = 450;
            this.Width = 365;
            montantTotal = index;
            argentReçu.Tag = 0.0;
            prixCommande.Text = index.ToString();
            prixCommande.Tag = index;
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
            prixCommande.Tag = montantTotal;
            prixCommande.Text = montantTotal.ToString();
        }

        public void Click_Leave(object sender, RoutedEventArgs e)
        { 
            PagePrincipale.MainInfo.Visibility = Visibility.Collapsed;
        }

        public void ReçuChanger(object sender, TextChangedEventArgs e)
        {
            if (argentReçu.Text == "")
            {
                argentReçu.Tag = 0.0;
                affichePrix();
            }
            if (double.TryParse(argentReçu.Text, out double montant))
            {
                argentReçu.Tag = montant;
                affichePrix();
            }
        }

        public void prixCommandeChanger(object sender, TextChangedEventArgs e)
        {
            if (prixCommande.Text == "")
            {
                prixCommande.Tag = 0.0;
                affichePrix();
            }
            if (double.TryParse(prixCommande.Text, out double montant))
            {
                prixCommande.Tag = montant;
                affichePrix();
            }
        }

        public void affichePrix()
        {
            rendre.Text = (double)argentReçu.Tag - (double)prixCommande.Tag + " €";
        }
    }
}
