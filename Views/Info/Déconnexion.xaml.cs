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
    /// Logique d'interaction pour Déconnexion.xaml
    /// </summary>
    public partial class Déconnexion : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;

        public Déconnexion()
        {
            InitializeComponent();
            PrepareInitialize();
        }

        public void PrepareInitialize()
        {
            this.Width = 600;
            this.Height = 300;
            this.Background = Brushes.LightSlateGray;

            Button yes = new Button();
            Button no = new Button();
            TextBlock text = new TextBlock();

            text.Text = "Voulez-vous vraiment vous déconnecter";
            text.FontWeight = FontWeights.Bold;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            Grid.SetColumnSpan(text, 2);
            Grid.SetRow(text, 0);

            yes.Content = "Oui";
            yes.FontWeight  = FontWeights.Bold;
            yes.Height = 40;
            yes.Width = 80;
            yes.Background = Brushes.Red;
            yes.Click += new RoutedEventHandler(Click_yes);
            yes.HorizontalAlignment = HorizontalAlignment.Center;
            yes.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(yes, 0);
            Grid.SetRow(yes, 1);

            no.Content = "Non";
            no.FontWeight = FontWeights.Bold;
            no.Height = 40;
            no.Width = 80;
            no.Click += new RoutedEventHandler(Click_no);
            no.VerticalAlignment = VerticalAlignment.Center;
            no.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumn(no, 1);
            Grid.SetRow(no, 1);

            grdMain.Children.Add(text);
            grdMain.Children.Add(yes);
            grdMain.Children.Add(no);
        }

        public void Click_yes(object sender, RoutedEventArgs e)
        {
            PagePrincipale.MainInfo.Visibility = Visibility.Collapsed;
            PagePrincipale.Main.Content = new Login();
        }

        public void Click_no(object sender, RoutedEventArgs e) 
        {
            PagePrincipale.MainInfo.Visibility = Visibility.Collapsed;

        }
    }
}
