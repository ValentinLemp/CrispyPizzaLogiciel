using CrispyLo.Modules;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Threading;

namespace CrispyLo.Views.ComInternet
{
    /// <summary>
    /// Logique d'interaction pour RechercheCommande.xaml
    /// </summary>
    public partial class RechercheCommande : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        BaseDeDonnée sql = new BaseDeDonnée();
        Grid barreNotif;
        TextBlock txtNotif;
        DispatcherTimer timerClignotement;
        DispatcherTimer timerRecherche;
        bool estRouge = true;
        DataTable commandesNonVues;
        int indexCommande = 0;

        public RechercheCommande()
        {
            InitializeComponent();
            PrepareInitialize();
        }

        public void PrepareInitialize()
        {
            // ---- Barre de notification ----
            barreNotif = new Grid();
            barreNotif.Height = 50;
            barreNotif.VerticalAlignment = VerticalAlignment.Top;
            barreNotif.Background = Brushes.Red;
            barreNotif.Visibility = Visibility.Collapsed;

            txtNotif = new TextBlock();
            txtNotif.Text = "NOUVELLE COMMANDE INTERNET";
            txtNotif.FontSize = 20;
            txtNotif.FontWeight = FontWeights.Bold;
            txtNotif.Foreground = Brushes.White;
            txtNotif.HorizontalAlignment = HorizontalAlignment.Center;
            txtNotif.VerticalAlignment = VerticalAlignment.Center;

            Button btnConfirmer = new Button();
            btnConfirmer.Content = "Confirmer";
            btnConfirmer.FontSize = 14;
            btnConfirmer.FontWeight = FontWeights.Bold;
            btnConfirmer.Width = 120;
            btnConfirmer.Height = 35;
            btnConfirmer.HorizontalAlignment = HorizontalAlignment.Right;
            btnConfirmer.VerticalAlignment = VerticalAlignment.Center;
            btnConfirmer.Margin = new Thickness(0, 0, 15, 0);
            btnConfirmer.Background = Brushes.White;
            btnConfirmer.Foreground = Brushes.Black;
            btnConfirmer.Click += Click_Confirmer;

            barreNotif.Children.Add(txtNotif);
            barreNotif.Children.Add(btnConfirmer);

            grdMain.Children.Add(barreNotif);

            // ---- Timer clignotement (1 sec) ----
            timerClignotement = new DispatcherTimer();
            timerClignotement.Interval = TimeSpan.FromSeconds(1);
            timerClignotement.Tick += (s, e) =>
            {
                if (estRouge)
                    barreNotif.Background = Brushes.OrangeRed;
                else
                    barreNotif.Background = Brushes.Red;
                estRouge = !estRouge;
            };

            // ---- Timer recherche nouvelles commandes (5 sec) ----
            timerRecherche = new DispatcherTimer();
            timerRecherche.Interval = TimeSpan.FromSeconds(5);
            timerRecherche.Tick += (s, e) => VerifierNouvellesCommandes();
            timerRecherche.Start();
        }

        private void VerifierNouvellesCommandes()
        {
            if (barreNotif.Visibility == Visibility.Visible) return;

            try
            {
                int nbrRow;
                sql.CommandesNonVues(PagePrincipale.Restaurent, out commandesNonVues, out nbrRow);
                if (nbrRow > 0)
                {
                    indexCommande = 0;
                    MajTexteNotif();
                    barreNotif.Visibility = Visibility.Visible;
                    PagePrincipale.RechercheCommande.Visibility = Visibility.Visible;
                    timerClignotement.Start();
                }
            }
            catch { }
        }

        private void MajTexteNotif()
        {
            int total = commandesNonVues.Rows.Count;
            txtNotif.Text = "NOUVELLE COMMANDE INTERNET (" + (indexCommande + 1) + "/" + total + ")";
        }

        private void Click_Confirmer(object sender, RoutedEventArgs e)
        {
            if (commandesNonVues == null || commandesNonVues.Rows.Count == 0) return;

            try
            {
                int idCommande = Convert.ToInt32(commandesNonVues.Rows[indexCommande]["id"]);
                // ici code imprimer avec commandesNonVues.Rows[indexCommande]
                sql.MarquerCommandeVue(idCommande);
            }
            catch { }

            indexCommande++;

            if (indexCommande < commandesNonVues.Rows.Count)
            {
                MajTexteNotif();
            }
            else
            {
                timerClignotement.Stop();
                barreNotif.Visibility = Visibility.Collapsed;
                PagePrincipale.RechercheCommande.Visibility = Visibility.Collapsed;
                indexCommande = 0;
            }
        }
    }
}
