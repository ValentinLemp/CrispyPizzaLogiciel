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
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Page
    {

        //---------------------
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        BaseDeDonnée sql = new BaseDeDonnée();
        //---------------------

        StackPanel panel = new StackPanel();
        TextBox text = new TextBox();
        Button valider = new Button();

        public Login()
        {
            InitializeComponent();
            PrepareInitialize();
        }

        public void PrepareInitialize()
        {
            Thickness mythickness = new Thickness();

            mythickness.Bottom = 20;

            text.FontSize = 40;
            text.FontWeight = FontWeights.Bold;
            text.MaxLength =  5;
            text.VerticalContentAlignment = VerticalAlignment.Center;
            text.HorizontalContentAlignment = HorizontalAlignment.Center;
            text.Width = 300;
            text.Height = 80;
            text.Margin = mythickness;
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
                if (data.Rows[i]["motdepasse"].ToString() == text.Text && (bool)data.Rows[i]["modeAdmin"] == false)
                {
                    PagePrincipale.Main.Content = new Home();
                    PagePrincipale.Restaurent = (int)data.Rows[i]["id"];
                }
            }
            text.Text = "";
        }
    }
}
