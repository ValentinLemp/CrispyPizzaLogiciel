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

            //Grille Ligne
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            WrapPanel wrap = new WrapPanel();
            wrap.VerticalAlignment = VerticalAlignment.Center;
            wrap.HorizontalAlignment = HorizontalAlignment.Center;
            wrap.Background = Brushes.Transparent;
            Grid.SetRow(wrap, 0);
            Grid.SetRowSpan(wrap, 2);

            Button home = new Button();
            home.Content = "Home";
            home.FontWeight = FontWeights.Bold;
            home.FontSize = 20;
            home.Width = 200;
            home.Height = 200;
            home.Background = Brushes.CadetBlue;
            home.Click += new RoutedEventHandler(Click_Home);
            wrap.Children.Add(home);

            Button mtpUti = new Button();
            mtpUti.Content = "    Modifier\nmot de passe";
            mtpUti.FontWeight = FontWeights.Bold;
            mtpUti.FontSize = 20;
            mtpUti.Width = 200;
            mtpUti.Height = 200;
            mtpUti.Background = Brushes.CadetBlue;
            mtpUti.Click += new RoutedEventHandler(Click_mtpUti);
            wrap.Children.Add(mtpUti);

            Button cloture = new Button();
            cloture.Content = "Cloture";
            cloture.FontWeight = FontWeights.Bold;
            cloture.FontSize = 20;
            cloture.Width = 200;
            cloture.Height = 200;
            cloture.Background = Brushes.CadetBlue;
            cloture.Click += new RoutedEventHandler(Click_Cloture);
            wrap.Children.Add(cloture);

            Button histCloture = new Button();
            histCloture.Content = "Historique\n  Cloture";
            histCloture.FontWeight = FontWeights.Bold;
            histCloture.FontSize = 20;
            histCloture.Width = 200;
            histCloture.Height = 200;
            histCloture.Background = Brushes.CadetBlue;
            histCloture.Click += new RoutedEventHandler(Click_HistClot);
            wrap.Children.Add(histCloture);

            Button allCommande = new Button();
            allCommande.Content = "Liste Commande";
            allCommande.FontWeight = FontWeights.Bold;
            allCommande.FontSize = 20;
            allCommande.Width = 200;
            allCommande.Height = 200;
            allCommande.Background = Brushes.CadetBlue;
            allCommande.Click += new RoutedEventHandler(Click_AllCommande);
            wrap.Children.Add(allCommande);

            Button allClient = new Button();
            allClient.Content = "Liste Client";
            allClient.FontWeight = FontWeights.Bold;
            allClient.FontSize = 20;
            allClient.Width = 200;
            allClient.Height = 200;
            allClient.Background = Brushes.CadetBlue;
            allClient.Click += new RoutedEventHandler(Click_AllClient);
            wrap.Children.Add(allClient);

            grdMain.Children.Add(wrap);

            BtnModeAdmin();
        }

        public void BtnModeAdmin()
        {
            Button leaveAdmin = new Button();
            leaveAdmin.Content = "Quitter le mode Admin";
            leaveAdmin.FontWeight = FontWeights.Bold;
            leaveAdmin.FontSize = 15;
            leaveAdmin.Width = 200;
            leaveAdmin.Height = 30;
            leaveAdmin.VerticalAlignment = VerticalAlignment.Top;
            leaveAdmin.HorizontalAlignment = HorizontalAlignment.Right;
            leaveAdmin.Background = Brushes.Red;
            leaveAdmin.Foreground = Brushes.White;
            leaveAdmin.Margin = new Thickness(10);
            leaveAdmin.Click += new RoutedEventHandler(Click_Leave);
            grdMain.Children.Add(leaveAdmin);
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

        public void Click_mtpUti(object sender, RoutedEventArgs e)
        {
            //
        }

        public void Click_Cloture(object sender, RoutedEventArgs e)
        {
            //
        }

        public void Click_HistClot(object sender, RoutedEventArgs e)
        {
            //
        }

        public void Click_AllCommande(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new AllCommande();
        }
        public void Click_AllClient(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new AllClient();
        }
    }
}
