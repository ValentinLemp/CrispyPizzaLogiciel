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

namespace CrispyLo.Views
{
    /// <summary>
    /// Logique d'interaction pour Cloture.xaml
    /// </summary>
    public partial class Cloture : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        BaseDeDonnée sql = new BaseDeDonnée();

        TextBlock valTotalRestaurant = new TextBlock();
        TextBlock valTotalInternet = new TextBlock();

        TextBox txtCarte = new TextBox();
        TextBox txtLiquide = new TextBox();
        TextBox txtInternet = new TextBox();

        TextBlock valDifference = new TextBlock();

        TextBlock valMaxiPizza = new TextBlock();
        TextBlock valPizza = new TextBlock();
        TextBlock valPate = new TextBlock();
        TextBlock valPain = new TextBlock();
        TextBlock valSalade = new TextBlock();
        TextBlock valSnacking = new TextBlock();
        TextBlock valBoisson = new TextBlock();
        TextBlock valDessert = new TextBlock();
        TextBlock valDipy = new TextBlock();

        public Cloture()
        {
            InitializeComponent();
            PrepareInitialize();
        }

        public void PrepareInitialize()
        {
            this.Background = Brushes.DimGray;

            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            TopBar();
            PanelArgent();
            PanelProduits();
            ChargerDonnees();
        }

        public void TopBar()
        {
            Grid topBar = new Grid();
            topBar.Background = Brushes.DarkSlateGray;
            Grid.SetRow(topBar, 0);
            Grid.SetColumnSpan(topBar, 2);

            TextBlock titre = new TextBlock();
            titre.Text = "CLÔTURE";
            titre.FontSize = 28;
            titre.FontWeight = FontWeights.Bold;
            titre.Foreground = Brushes.White;
            titre.HorizontalAlignment = HorizontalAlignment.Center;
            titre.VerticalAlignment = VerticalAlignment.Center;
            topBar.Children.Add(titre);

            Button retour = new Button();
            retour.Content = "🔙";
            retour.FontWeight = FontWeights.Black;
            retour.FontSize = 18;
            retour.Foreground = Brushes.White;
            retour.Background = Brushes.Red;
            retour.Width = 35;
            retour.Height = 35;
            retour.HorizontalAlignment = HorizontalAlignment.Right;
            retour.VerticalAlignment = VerticalAlignment.Center;
            retour.Margin = new Thickness(0, 0, 15, 0);
            retour.Click += new RoutedEventHandler(Click_Retour);
            topBar.Children.Add(retour);

            grdMain.Children.Add(topBar);
        }

        public void PanelArgent()
        {
            StackPanel panelGauche = new StackPanel();
            panelGauche.Margin = new Thickness(30, 25, 20, 20);
            panelGauche.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(panelGauche, 1);
            Grid.SetColumn(panelGauche, 0);

            // ---- Total Restaurant ----
            TextBlock lblRestaurant = new TextBlock();
            lblRestaurant.Text = "Total Restaurant";
            lblRestaurant.FontSize = 18;
            lblRestaurant.FontWeight = FontWeights.Bold;
            lblRestaurant.Foreground = Brushes.White;
            lblRestaurant.Margin = new Thickness(0, 0, 0, 5);
            panelGauche.Children.Add(lblRestaurant);

            valTotalRestaurant.Text = "0.00 €";
            valTotalRestaurant.FontSize = 24;
            valTotalRestaurant.FontWeight = FontWeights.Bold;
            valTotalRestaurant.Foreground = Brushes.LightGreen;
            valTotalRestaurant.Margin = new Thickness(0, 0, 0, 20);
            panelGauche.Children.Add(valTotalRestaurant);

            // ---- Total Internet ----
            TextBlock lblInternet = new TextBlock();
            lblInternet.Text = "Total Internet";
            lblInternet.FontSize = 18;
            lblInternet.FontWeight = FontWeights.Bold;
            lblInternet.Foreground = Brushes.White;
            lblInternet.Margin = new Thickness(0, 0, 0, 5);
            panelGauche.Children.Add(lblInternet);

            valTotalInternet.Text = "0.00 €";
            valTotalInternet.FontSize = 24;
            valTotalInternet.FontWeight = FontWeights.Bold;
            valTotalInternet.Foreground = Brushes.LightSkyBlue;
            valTotalInternet.Margin = new Thickness(0, 0, 0, 25);
            panelGauche.Children.Add(valTotalInternet);

            // ---- Séparateur ----
            Border separateur = new Border();
            separateur.BorderBrush = Brushes.Gray;
            separateur.BorderThickness = new Thickness(0, 1, 0, 0);
            separateur.Margin = new Thickness(0, 0, 0, 25);
            panelGauche.Children.Add(separateur);

            // ---- Titre Argent récolté ----
            TextBlock lblRecolte = new TextBlock();
            lblRecolte.Text = "Argent récolté";
            lblRecolte.FontSize = 18;
            lblRecolte.FontWeight = FontWeights.Bold;
            lblRecolte.Foreground = Brushes.White;
            lblRecolte.Margin = new Thickness(0, 0, 0, 15);
            panelGauche.Children.Add(lblRecolte);

            // Par carte
            panelGauche.Children.Add(CreerLigneEncodage("Par carte :", txtCarte));

            // Par liquide
            panelGauche.Children.Add(CreerLigneEncodage("Par liquide :", txtLiquide));

            // Par internet
            panelGauche.Children.Add(CreerLigneEncodage("Par internet :", txtInternet));

            // ---- Séparateur ----
            Border separateur2 = new Border();
            separateur2.BorderBrush = Brushes.Gray;
            separateur2.BorderThickness = new Thickness(0, 1, 0, 0);
            separateur2.Margin = new Thickness(0, 5, 0, 15);
            panelGauche.Children.Add(separateur2);

            // ---- Différence ----
            TextBlock lblDiff = new TextBlock();
            lblDiff.Text = "Différence :";
            lblDiff.FontSize = 18;
            lblDiff.FontWeight = FontWeights.Bold;
            lblDiff.Foreground = Brushes.White;
            lblDiff.Margin = new Thickness(0, 0, 0, 5);
            panelGauche.Children.Add(lblDiff);

            valDifference.Text = "0.00 €";
            valDifference.FontSize = 26;
            valDifference.FontWeight = FontWeights.Bold;
            valDifference.Foreground = Brushes.White;
            valDifference.Margin = new Thickness(0, 0, 0, 25);
            panelGauche.Children.Add(valDifference);

            // ---- Bouton Clôturer ----
            Button btnCloturer = new Button();
            btnCloturer.Content = "CLÔTURER";
            btnCloturer.FontSize = 18;
            btnCloturer.FontWeight = FontWeights.Bold;
            btnCloturer.Foreground = Brushes.White;
            btnCloturer.Background = Brushes.DarkRed;
            btnCloturer.Width = 200;
            btnCloturer.Height = 45;
            btnCloturer.HorizontalAlignment = HorizontalAlignment.Left;
            btnCloturer.Click += new RoutedEventHandler(Click_Cloturer);
            panelGauche.Children.Add(btnCloturer);

            grdMain.Children.Add(panelGauche);
        }

        private WrapPanel CreerLigneEncodage(string label, TextBox textBox)
        {
            WrapPanel ligne = new WrapPanel();
            ligne.Margin = new Thickness(0, 0, 0, 10);

            TextBlock lbl = new TextBlock();
            lbl.Text = label;
            lbl.FontSize = 15;
            lbl.FontWeight = FontWeights.Bold;
            lbl.Foreground = Brushes.White;
            lbl.Width = 120;
            lbl.VerticalAlignment = VerticalAlignment.Center;
            ligne.Children.Add(lbl);

            textBox.Width = 150;
            textBox.Height = 28;
            textBox.FontSize = 15;
            textBox.PreviewTextInput += Chiffres_Only;
            textBox.TextChanged += (s, ev) => CalculerDifference();
            ligne.Children.Add(textBox);

            TextBlock euro = new TextBlock();
            euro.Text = " €";
            euro.FontSize = 15;
            euro.FontWeight = FontWeights.Bold;
            euro.Foreground = Brushes.White;
            euro.VerticalAlignment = VerticalAlignment.Center;
            ligne.Children.Add(euro);

            return ligne;
        }

        public void PanelProduits()
        {
            StackPanel panelDroite = new StackPanel();
            panelDroite.Margin = new Thickness(20, 25, 30, 20);
            panelDroite.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(panelDroite, 1);
            Grid.SetColumn(panelDroite, 1);

            TextBlock titreProduits = new TextBlock();
            titreProduits.Text = "Produits vendus";
            titreProduits.FontSize = 20;
            titreProduits.FontWeight = FontWeights.Bold;
            titreProduits.Foreground = Brushes.White;
            titreProduits.Margin = new Thickness(0, 0, 0, 20);
            panelDroite.Children.Add(titreProduits);

            panelDroite.Children.Add(CreerLigneProduit("Maxi Pizza :", valMaxiPizza));
            panelDroite.Children.Add(CreerLigneProduit("Pizza :", valPizza));
            panelDroite.Children.Add(CreerLigneProduit("Pâte :", valPate));
            panelDroite.Children.Add(CreerLigneProduit("Pain :", valPain));
            panelDroite.Children.Add(CreerLigneProduit("Salade :", valSalade));
            panelDroite.Children.Add(CreerLigneProduit("Snacking :", valSnacking));
            panelDroite.Children.Add(CreerLigneProduit("Boisson :", valBoisson));
            panelDroite.Children.Add(CreerLigneProduit("Dessert :", valDessert));
            panelDroite.Children.Add(CreerLigneProduit("Dipy :", valDipy));

            grdMain.Children.Add(panelDroite);
        }

        private WrapPanel CreerLigneProduit(string label, TextBlock valeur)
        {
            WrapPanel ligne = new WrapPanel();
            ligne.Margin = new Thickness(0, 0, 0, 12);

            TextBlock lbl = new TextBlock();
            lbl.Text = label;
            lbl.FontSize = 16;
            lbl.FontWeight = FontWeights.Bold;
            lbl.Foreground = Brushes.White;
            lbl.Width = 130;
            lbl.VerticalAlignment = VerticalAlignment.Center;
            ligne.Children.Add(lbl);

            valeur.Text = "0";
            valeur.FontSize = 20;
            valeur.FontWeight = FontWeights.Bold;
            valeur.Foreground = Brushes.Yellow;
            valeur.VerticalAlignment = VerticalAlignment.Center;
            ligne.Children.Add(valeur);

            return ligne;
        }

        public void ChargerDonnees()
        {
            DateTime aujourdhui = DateTime.Now;
            int idRest = PagePrincipale.Restaurent;

            // ---- Totaux argent par statut ----
            double totalRest = sql.TotalCommandeByDateAndStatut(aujourdhui, 1, idRest);
            double totalNet = sql.TotalCommandeByDateAndStatut(aujourdhui, 2, idRest);

            valTotalRestaurant.Text = totalRest.ToString("0.00") + " €";
            valTotalInternet.Text = totalNet.ToString("0.00") + " €";

            // Auto-remplir internet (va direct sur le compte)
            txtInternet.Text = totalNet.ToString("0.00");

            // ---- Comptage produits vendus ----
            sql.CompteProduitsByDate(aujourdhui, idRest, out DataTable dataP);

            int nbMaxiPizza = 0, nbPizza = 0, nbPate = 0, nbPain = 0;
            int nbSalade = 0, nbSnacking = 0, nbBoisson = 0, nbDessert = 0, nbDipy = 0;

            for (int i = 0; i < dataP.Rows.Count; i++)
            {
                int typeProduit = Convert.ToInt32(dataP.Rows[i]["idTypeProduit"]);
                bool estLarge = Convert.ToBoolean(dataP.Rows[i]["tailleLarge"]);
                int nombre = Convert.ToInt32(dataP.Rows[i]["nombre"]);

                switch (typeProduit)
                {
                    case 1: // Pizza
                        if (estLarge) nbMaxiPizza += nombre;
                        else nbPizza += nombre;
                        break;
                    case 2: nbPate += nombre; break;
                    case 3: nbSalade += nombre; break;
                    case 4: nbPain += nombre; break;
                    case 5: nbSnacking += nombre; break;
                    case 6: nbDessert += nombre; break;
                    case 7: nbBoisson += nombre; break;
                    case 8: nbDipy += nombre; break;
                }
            }

            valMaxiPizza.Text = nbMaxiPizza.ToString();
            valPizza.Text = nbPizza.ToString();
            valPate.Text = nbPate.ToString();
            valPain.Text = nbPain.ToString();
            valSalade.Text = nbSalade.ToString();
            valSnacking.Text = nbSnacking.ToString();
            valBoisson.Text = nbBoisson.ToString();
            valDessert.Text = nbDessert.ToString();
            valDipy.Text = nbDipy.ToString();

            CalculerDifference();
        }

        private void Chiffres_Only(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string texte = tb.Text + e.Text;
            e.Handled = !double.TryParse(texte.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _);
        }

        public void CalculerDifference()
        {
            double carte = 0, liquide = 0, internet = 0;
            double.TryParse(txtCarte.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out carte);
            double.TryParse(txtLiquide.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out liquide);
            double.TryParse(txtInternet.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out internet);

            double totalRecolte = carte + liquide + internet;

            double totalRestaurant = 0, totalNet = 0;
            double.TryParse(valTotalRestaurant.Text.Replace(" €", "").Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out totalRestaurant);
            double.TryParse(valTotalInternet.Text.Replace(" €", "").Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out totalNet);

            double totalCommandes = totalRestaurant + totalNet;
            double difference = totalRecolte - totalCommandes;

            if (difference < 0)
            {
                valDifference.Text = difference.ToString("0.00") + " €";
                valDifference.Foreground = Brushes.Red;
            }
            else if (difference > 0)
            {
                valDifference.Text = "+" + difference.ToString("0.00") + " €";
                valDifference.Foreground = Brushes.LimeGreen;
            }
            else
            {
                valDifference.Text = "0.00 €";
                valDifference.Foreground = Brushes.White;
            }
        }

        public void Click_Cloturer(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Voulez-vous vraiment clôturer la journée ?\nToutes les commandes du jour seront marquées comme clôturées.",
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            DateTime aujourdhui = DateTime.Now;
            int idRest = PagePrincipale.Restaurent;

            double argentRest = 0, argentNet = 0;
            double.TryParse(valTotalRestaurant.Text.Replace(" €", "").Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out argentRest);
            double.TryParse(valTotalInternet.Text.Replace(" €", "").Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out argentNet);

            double argentCarte = 0, argentLiquide = 0;
            double.TryParse(txtCarte.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out argentCarte);
            double.TryParse(txtLiquide.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out argentLiquide);

            double argentDiff = 0;
            string diffText = valDifference.Text.Replace(" €", "").Replace("+", "").Replace(",", ".");
            double.TryParse(diffText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out argentDiff);

            sql.InsererCloture(aujourdhui, argentRest, argentNet, argentCarte, argentLiquide, argentDiff,
                int.Parse(valMaxiPizza.Text), int.Parse(valPizza.Text),
                int.Parse(valPate.Text), int.Parse(valPain.Text),
                int.Parse(valSalade.Text), int.Parse(valSnacking.Text),
                int.Parse(valBoisson.Text), int.Parse(valDessert.Text),
                int.Parse(valDipy.Text), idRest);

            sql.CloturerCommandes(aujourdhui, idRest);
            MessageBox.Show("Clôture effectuée avec succès !");
            PagePrincipale.Main.Content = new HomeAdmin();
        }

        public void Click_Retour(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new HomeAdmin();
        }
    }
}
