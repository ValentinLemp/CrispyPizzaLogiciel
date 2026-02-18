using CrispyLo.Views.Info;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrispyLo.Views
{
    /// <summary>
    /// Logique d'interaction pour HomeAdmin.xaml
    /// </summary>
    public partial class HomeAdmin : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;

        public HomeAdmin()
        {
            InitializeComponent();
            PrepareInitialize();
        }

        public void PrepareInitialize()
        {
            this.Background = Brushes.DimGray;

            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            // ---- Top bar ----
            Grid topBar = new Grid();
            topBar.Background = Brushes.DarkSlateGray;
            Grid.SetRow(topBar, 0);

            TextBlock titre = new TextBlock();
            titre.Text = "ADMINISTRATION";
            titre.FontSize = 26;
            titre.FontWeight = FontWeights.Bold;
            titre.Foreground = Brushes.White;
            titre.HorizontalAlignment = HorizontalAlignment.Center;
            titre.VerticalAlignment = VerticalAlignment.Center;
            topBar.Children.Add(titre);

            Button leaveAdmin = new Button();
            leaveAdmin.Content = "Quitter Admin";
            leaveAdmin.FontWeight = FontWeights.Bold;
            leaveAdmin.FontSize = 13;
            leaveAdmin.Width = 140;
            leaveAdmin.Height = 35;
            leaveAdmin.HorizontalAlignment = HorizontalAlignment.Right;
            leaveAdmin.VerticalAlignment = VerticalAlignment.Center;
            leaveAdmin.Background = Brushes.Red;
            leaveAdmin.Foreground = Brushes.White;
            leaveAdmin.Margin = new Thickness(0, 0, 15, 0);
            leaveAdmin.Click += new RoutedEventHandler(Click_Leave);
            topBar.Children.Add(leaveAdmin);

            grdMain.Children.Add(topBar);

            // ---- Boutons ----
            WrapPanel wrap = new WrapPanel();
            wrap.VerticalAlignment = VerticalAlignment.Center;
            wrap.HorizontalAlignment = HorizontalAlignment.Center;
            wrap.Background = Brushes.Transparent;
            wrap.MaxWidth = 700;
            Grid.SetRow(wrap, 1);

            wrap.Children.Add(CreerBouton("🏠\nAccueil", Brushes.SteelBlue, Click_Home));
            wrap.Children.Add(CreerBouton("🔑\nMot de passe", Brushes.SlateBlue, Click_mdpUti));
            wrap.Children.Add(CreerBouton("⭐\nSuggestions", Brushes.Goldenrod, Click_Suggestion));
            wrap.Children.Add(CreerBouton("📋\nClôture", Brushes.Teal, Click_Cloture));
            wrap.Children.Add(CreerBouton("📜\nHistorique\nClôture", Brushes.DarkCyan, Click_HistClot));
            wrap.Children.Add(CreerBouton("📦\nCommandes", Brushes.IndianRed, Click_AllCommande));
            wrap.Children.Add(CreerBouton("👥\nClients", Brushes.MediumSeaGreen, Click_AllClient));

            grdMain.Children.Add(wrap);
        }

        private Button CreerBouton(string text, Brush couleur, RoutedEventHandler click)
        {
            Button btn = new Button();
            TextBlock txt = new TextBlock();
            txt.Text = text;
            txt.TextAlignment = TextAlignment.Center;
            txt.FontSize = 18;
            txt.FontWeight = FontWeights.Bold;
            txt.Foreground = Brushes.White;
            btn.Content = txt;
            btn.Width = 160;
            btn.Height = 140;
            btn.Margin = new Thickness(10);
            btn.Background = couleur;
            btn.BorderThickness = new Thickness(0);
            btn.Click += click;
            return btn;
        }

        public void Click_Leave(object sender, RoutedEventArgs e)
        {
            PagePrincipale.EstAdmin = false;
            PagePrincipale.Main.Content = new Home();
        }

        public void Click_Home(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new Home();
        }

        public void Click_mdpUti(object sender, RoutedEventArgs e)
        {
            PagePrincipale.MainInfo.Content = new modifierMDP();
            PagePrincipale.MainInfo.Visibility = Visibility.Visible;
        }

        public void Click_Cloture(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new Cloture();
        }

        public void Click_HistClot(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new HistoriqueCloture();
        }

        public void Click_AllCommande(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new AllCommande();
        }
        public void Click_AllClient(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new AllClient();
        }

        public void Click_Suggestion(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new ProduitSuggestion();
        }
    }
}
