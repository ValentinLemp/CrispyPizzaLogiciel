using CrispyLo.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        TextBlock totalCalculette = new TextBlock();
        double[] valeurs = { 0.05, 0.10, 0.20, 0.50, 1, 2, 5, 10, 20, 50, 100 };
        TextBlock[] quantites;

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
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(220) });

            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            TopBar();
            PanelArgent();
            PanelProduits();
            Calculette();
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

        public void Calculette()
        {
            StackPanel panelCalc = new StackPanel();
            panelCalc.Background = Brushes.DarkSlateGray;
            panelCalc.Margin = new Thickness(5, 5, 5, 5);
            Grid.SetRow(panelCalc, 2);
            Grid.SetColumnSpan(panelCalc, 2);

            string[] labels = { "0.05", "0.10", "0.20", "0.50", "1€", "2€", "5€", "10€", "20€", "50€", "100€" };
            quantites = new TextBlock[valeurs.Length];

            // Ligne des labels
            WrapPanel ligneLabels = new WrapPanel();
            ligneLabels.HorizontalAlignment = HorizontalAlignment.Center;
            ligneLabels.Margin = new Thickness(0, 8, 0, 0);
            for (int i = 0; i < labels.Length; i++)
            {
                TextBlock lbl = new TextBlock();
                lbl.Text = labels[i];
                lbl.FontSize = 13;
                lbl.FontWeight = FontWeights.Bold;
                lbl.Foreground = Brushes.White;
                lbl.Width = 55;
                lbl.TextAlignment = TextAlignment.Center;
                lbl.Margin = new Thickness(2, 0, 2, 0);
                ligneLabels.Children.Add(lbl);
            }
            panelCalc.Children.Add(ligneLabels);

            // Ligne des boutons +
            WrapPanel lignePlus = new WrapPanel();
            lignePlus.HorizontalAlignment = HorizontalAlignment.Center;
            for (int i = 0; i < valeurs.Length; i++)
            {
                Button btnPlus = new Button();
                btnPlus.Content = "+";
                btnPlus.FontWeight = FontWeights.Bold;
                btnPlus.FontSize = 14;
                btnPlus.Width = 55;
                btnPlus.Height = 28;
                btnPlus.Margin = new Thickness(2);
                btnPlus.Background = Brushes.LimeGreen;
                btnPlus.Tag = i;
                btnPlus.Click += Click_CalcPlus;
                lignePlus.Children.Add(btnPlus);
            }
            panelCalc.Children.Add(lignePlus);

            // Ligne des quantités
            WrapPanel ligneQte = new WrapPanel();
            ligneQte.HorizontalAlignment = HorizontalAlignment.Center;
            for (int i = 0; i < valeurs.Length; i++)
            {
                quantites[i] = new TextBlock();
                quantites[i].Text = "0";
                quantites[i].FontSize = 16;
                quantites[i].FontWeight = FontWeights.Bold;
                quantites[i].Foreground = Brushes.Yellow;
                quantites[i].Width = 55;
                quantites[i].TextAlignment = TextAlignment.Center;
                quantites[i].Margin = new Thickness(2, 2, 2, 2);
                ligneQte.Children.Add(quantites[i]);
            }
            panelCalc.Children.Add(ligneQte);

            // Ligne des boutons -
            WrapPanel ligneMoins = new WrapPanel();
            ligneMoins.HorizontalAlignment = HorizontalAlignment.Center;
            for (int i = 0; i < valeurs.Length; i++)
            {
                Button btnMoins = new Button();
                btnMoins.Content = "-";
                btnMoins.FontWeight = FontWeights.Bold;
                btnMoins.FontSize = 14;
                btnMoins.Width = 55;
                btnMoins.Height = 28;
                btnMoins.Margin = new Thickness(2);
                btnMoins.Background = Brushes.Salmon;
                btnMoins.Tag = i;
                btnMoins.Click += Click_CalcMoins;
                ligneMoins.Children.Add(btnMoins);
            }
            panelCalc.Children.Add(ligneMoins);

            // Ligne total + bouton reset
            WrapPanel ligneBas = new WrapPanel();
            ligneBas.HorizontalAlignment = HorizontalAlignment.Center;
            ligneBas.Margin = new Thickness(0, 5, 0, 5);

            TextBlock lblTotal = new TextBlock();
            lblTotal.Text = "Total :";
            lblTotal.FontSize = 18;
            lblTotal.FontWeight = FontWeights.Bold;
            lblTotal.Foreground = Brushes.White;
            lblTotal.VerticalAlignment = VerticalAlignment.Center;
            lblTotal.Margin = new Thickness(0, 0, 10, 0);
            ligneBas.Children.Add(lblTotal);

            totalCalculette.Text = "0.00 €";
            totalCalculette.FontSize = 20;
            totalCalculette.FontWeight = FontWeights.Bold;
            totalCalculette.Foreground = Brushes.Yellow;
            totalCalculette.VerticalAlignment = VerticalAlignment.Center;
            totalCalculette.Margin = new Thickness(0, 0, 30, 0);
            ligneBas.Children.Add(totalCalculette);

            Button btnReset = new Button();
            btnReset.Content = "Remettre à 0";
            btnReset.FontWeight = FontWeights.Bold;
            btnReset.FontSize = 13;
            btnReset.Width = 110;
            btnReset.Height = 30;
            btnReset.Background = Brushes.Orange;
            btnReset.Click += Click_CalcReset;
            ligneBas.Children.Add(btnReset);

            panelCalc.Children.Add(ligneBas);
            grdMain.Children.Add(panelCalc);
        }

        private void Click_CalcPlus(object sender, RoutedEventArgs e)
        {
            int index = (int)((Button)sender).Tag;
            int qte = int.Parse(quantites[index].Text) + 1;
            quantites[index].Text = qte.ToString();
            MajTotalCalculette();
        }

        private void Click_CalcMoins(object sender, RoutedEventArgs e)
        {
            int index = (int)((Button)sender).Tag;
            int qte = int.Parse(quantites[index].Text);
            if (qte > 0) qte--;
            quantites[index].Text = qte.ToString();
            MajTotalCalculette();
        }

        private void Click_CalcReset(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < quantites.Length; i++)
                quantites[i].Text = "0";
            MajTotalCalculette();
        }

        private void MajTotalCalculette()
        {
            double total = 0;
            for (int i = 0; i < valeurs.Length; i++)
                total += valeurs[i] * int.Parse(quantites[i].Text);
            totalCalculette.Text = total.ToString("0.00") + " €";
        }

        public void ChargerDonnees()
        {
            DateTime aujourdhui = DateTime.Now;

            // ---- Totaux argent par statut ----
            decimal totalRest = sql.TotalCommandeByDateAndStatut(aujourdhui, 1);
            decimal totalNet = sql.TotalCommandeByDateAndStatut(aujourdhui, 2);

            valTotalRestaurant.Text = totalRest.ToString("0.00") + " €";
            valTotalInternet.Text = totalNet.ToString("0.00") + " €";

            // Auto-remplir internet (va direct sur le compte)
            txtInternet.Text = totalNet.ToString("0.00");

            // ---- Comptage produits vendus ----
            sql.CompteProduitsByDate(aujourdhui, out DataTable dataP);

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

            decimal argentRest = 0, argentNet = 0;
            decimal.TryParse(valTotalRestaurant.Text.Replace(" €", "").Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out argentRest);
            decimal.TryParse(valTotalInternet.Text.Replace(" €", "").Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out argentNet);

            decimal argentCarte = 0, argentLiquide = 0;
            decimal.TryParse(txtCarte.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out argentCarte);
            decimal.TryParse(txtLiquide.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out argentLiquide);

            decimal argentDiff = 0;
            string diffText = valDifference.Text.Replace(" €", "").Replace("+", "").Replace(",", ".");
            decimal.TryParse(diffText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out argentDiff);

            string signaturePrec = sql.DernierSignature();
            string signature = Traitement.GenererSignature(idCom, date, totalCommande, signaturePrec);

            sql.InsererCloture(aujourdhui, argentRest, argentNet, argentCarte, argentLiquide, argentDiff,
                int.Parse(valMaxiPizza.Text), int.Parse(valPizza.Text),
                int.Parse(valPate.Text), int.Parse(valPain.Text),
                int.Parse(valSalade.Text), int.Parse(valSnacking.Text),
                int.Parse(valBoisson.Text), int.Parse(valDessert.Text),
                int.Parse(valDipy.Text), signature, signaturePrec);
            
            sql.CloturerCommandes(aujourdhui);
            MessageBox.Show("Clôture effectuée avec succès !");
            PagePrincipale.Main.Content = new HomeAdmin();
        }

        public void Click_Retour(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new HomeAdmin();
        }
    }
}
