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
        ComboBox selectorResto = new ComboBox();
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

            selectorResto.Items.Add(new ComboBoxItem { Content = "Meux", Tag = 1 });
            selectorResto.Items.Add(new ComboBoxItem { Content = "Ramillies", Tag = 2 });
            selectorResto.SelectedIndex = -1;
            selectorResto.FontSize = 25;
            selectorResto.FontWeight = FontWeights.Bold;
            selectorResto.Width = 300;
            selectorResto.Height = 60;
            selectorResto.Margin = mythickness;

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
            panel.Children.Add(selectorResto);
            panel.Children.Add(text);
            panel.Children.Add(valider);

            grdMain.Children.Add(panel);
        }

        public void Validation_Click(object sender, RoutedEventArgs e)
        {
            if (selectorResto.SelectedItem == null)
            {
                text.Text = "";
                return;
            }

            PagePrincipale.Restaurant = (int)((ComboBoxItem)selectorResto.SelectedItem).Tag;

            if (PagePrincipale.Restaurant == 1)
            {
                PagePrincipale.ChemminDB = "Server=localhost;Database=crispy_pizza_meux;port=3306;User Id=root;password=root";
                sql.VerificationMDP(out DataTable data, out int nbrRow);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (data.Rows[i]["motdepasse"].ToString() == text.Text && (bool)data.Rows[i]["modeAdmin"] == false)
                    {
                        PagePrincipale.Main.Content = new Home();
                    }
                }
            }
            else if(PagePrincipale.Restaurant == 2)
            {
                PagePrincipale.ChemminDB = "Server=localhost;Database=crispy_pizza_ramillies;port=3306;User Id=root;password=root";
                sql.VerificationMDP(out DataTable data, out int nbrRow);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (data.Rows[i]["motdepasse"].ToString() == text.Text && (bool)data.Rows[i]["modeAdmin"] == false)
                    {
                        PagePrincipale.Main.Content = new Home();
                    }
                }
            }
            text.Text = "";

            
        }
    }
}
