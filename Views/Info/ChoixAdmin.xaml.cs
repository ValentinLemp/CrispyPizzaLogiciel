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
using static System.Net.Mime.MediaTypeNames;

namespace CrispyLo.Views.Info
{
    /// <summary>
    /// Logique d'interaction pour ChoixAdmin.xaml
    /// </summary>
    public partial class ChoixAdmin : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        BaseDeDonnée sql = new BaseDeDonnée();

        TextBox text = new TextBox();
        public ChoixAdmin()
        {
            InitializeComponent();
            PrepareInitialize();
        }

        public void PrepareInitialize()
        {
            StackPanel panel = new StackPanel();
            Button valider = new Button();

            this.Width = 600;
            this.Height = 300;
            this.Background = Brushes.LightSlateGray;

            text.FontSize = 40;
            text.FontWeight = FontWeights.Bold;
            text.MaxLength = 5;
            text.VerticalContentAlignment = VerticalAlignment.Center;
            text.HorizontalContentAlignment = HorizontalAlignment.Center;
            text.Width = 300;
            text.Height = 80;
            text.Margin = new Thickness(0,0,0,20);
            text.BorderBrush = Brushes.Black;

            valider.Content = "Valider";
            valider.Height = 80;
            valider.Width = 300;
            valider.FontSize = 20;
            valider.FontWeight = FontWeights.Bold;
            valider.Background = Brushes.Yellow;
            valider.Click += new RoutedEventHandler(Validation_Click);

            panel.VerticalAlignment = VerticalAlignment.Center;
            panel.HorizontalAlignment = HorizontalAlignment.Center;
            panel.Children.Add(text);
            panel.Children.Add(valider);

            grdMain.Children.Add(panel);
        }

        public void Validation_Click(object sender, RoutedEventArgs e)
        {
            sql.VerificationMDP(out DataTable data, out int nbrRow);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i]["motdepasse"].ToString() == text.Text && (bool)data.Rows[i]["modeAdmin"] == true && (int)data.Rows[i]["idRestaurant"] == PagePrincipale.Restaurent)
                {
                    PagePrincipale.MainInfo.Visibility = Visibility.Collapsed;
                    PagePrincipale.Main.Content = new HomeAdmin();
                    PagePrincipale.EstAdmin = true;
                }
            }
            text.Text = "";
        }

        public void Click_Leave(object sender, RoutedEventArgs e)
        {
            PagePrincipale.MainInfo.Visibility = Visibility.Collapsed;
        }
    }
}
