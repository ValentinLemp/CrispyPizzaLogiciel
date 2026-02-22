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
using CrispyLo.Modules;
using System.Data;

namespace CrispyLo.Views.Info
{
    /// <summary>
    /// Logique d'interaction pour modifierMDP.xaml
    /// </summary>
    public partial class modifierMDP : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        BaseDeDonnée sql = new BaseDeDonnée();

        Boolean estAdmin = false;
        Boolean estLogiciel = false;

        TextBlock textChoix = new TextBlock();

        TextBox mdpActuel = new TextBox();
        TextBox mdpNouv = new TextBox();

        WrapPanel wrapBtn = new WrapPanel();
        StackPanel panel = new StackPanel();

        public modifierMDP()
        {
            InitializeComponent();
            PrepareInitialize();
        }

        public void PrepareInitialize()
        {
            this.Width = 600;
            this.Height = 300;
            this.Background = Brushes.LightSlateGray;

            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            // ---- Titre + bouton retour ----
            Grid topBar = new Grid();
            Grid.SetRow(topBar, 0);

            TextBlock titre = new TextBlock();
            titre.Text = "Modifier mot de passe";
            titre.FontSize = 26;
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
            retour.Margin = new Thickness(0, 0, 10, 0);
            retour.Click += new RoutedEventHandler(Click_Leave);
            topBar.Children.Add(retour);

            grdMain.Children.Add(topBar);

            BtnChoix();
            Encodage();
        }

        public void BtnChoix()
        {
            wrapBtn.HorizontalAlignment = HorizontalAlignment.Center;
            wrapBtn.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(wrapBtn, 1);

            Button logiciel = new Button();
            logiciel.Content = "Logiciel";
            logiciel.Height = 50;
            logiciel.Width = 150;
            logiciel.FontSize = 20;
            logiciel.FontWeight = FontWeights.Bold;
            logiciel.Background = Brushes.FloralWhite;
            logiciel.Click += new RoutedEventHandler(Click_Bouton);
            logiciel.Margin = new Thickness(10);
            wrapBtn.Children.Add(logiciel);

            Button admin = new Button();
            admin.Content = "Admin";
            admin.Height = 50;
            admin.Width = 150;
            admin.FontSize = 20;
            admin.FontWeight = FontWeights.Bold;
            admin.Background = Brushes.FloralWhite;
            admin.Click += new RoutedEventHandler(Click_Bouton);
            admin.Margin = new Thickness(10);
            wrapBtn.Children.Add(admin);

            grdMain.Children.Add(wrapBtn);
        }

        public void Encodage()
        {
            panel.HorizontalAlignment = HorizontalAlignment.Center;
            panel.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(panel, 1);

            textChoix.FontSize = 20;
            textChoix.FontWeight = FontWeights.Bold;
            textChoix.Foreground = Brushes.White;
            textChoix.HorizontalAlignment = HorizontalAlignment.Center;
            textChoix.Margin = new Thickness(0, 0, 0, 15);
            panel.Children.Add(textChoix);

            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Mot de passe actuel :";
            textBlock.FontSize = 14;
            textBlock.Foreground = Brushes.White;
            textBlock.Margin = new Thickness(0, 0, 0, 5);
            panel.Children.Add(textBlock);

            mdpActuel.Height = 28;
            mdpActuel.Width = 250;
            mdpActuel.FontSize = 14;
            mdpActuel.MaxLength = 5;
            mdpActuel.HorizontalAlignment = HorizontalAlignment.Center;
            mdpActuel.Margin = new Thickness(0, 0, 0, 15);
            mdpActuel.PreviewTextInput += Chiffres_Only;
            panel.Children.Add(mdpActuel);

            TextBlock textBlock1 = new TextBlock();
            textBlock1.Text = "Nouveau mot de passe :";
            textBlock1.FontSize = 14;
            textBlock1.Foreground = Brushes.White;
            textBlock1.Margin = new Thickness(0, 0, 0, 5);
            panel.Children.Add(textBlock1);

            mdpNouv.Height = 28;
            mdpNouv.Width = 250;
            mdpNouv.FontSize = 14;
            mdpNouv.MaxLength = 5;
            mdpNouv.HorizontalAlignment = HorizontalAlignment.Center;
            mdpNouv.Margin = new Thickness(0, 0, 0, 20);
            mdpNouv.PreviewTextInput += Chiffres_Only;
            panel.Children.Add(mdpNouv);

            Button valider = new Button();
            valider.Content = "Valider";
            valider.Height = 35;
            valider.Width = 100;
            valider.FontSize = 16;
            valider.FontWeight = FontWeights.Bold;
            valider.Background = Brushes.FloralWhite;
            valider.HorizontalAlignment = HorizontalAlignment.Center;
            valider.Click += new RoutedEventHandler(Click_Valider);
            panel.Children.Add(valider);

            panel.Visibility = Visibility.Collapsed;
            grdMain.Children.Add(panel);
        }

        private void Chiffres_Only(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

        public void Click_Bouton(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Content.ToString() == "Logiciel")
            {
                estLogiciel = true;
                textChoix.Text = "Logiciel";
            }
            if (btn.Content.ToString() == "Admin")
            {
                estAdmin = true;
                textChoix.Text = "Admin";
            }
            panel.Visibility = Visibility.Visible;
            wrapBtn.Visibility = Visibility.Collapsed;
        }

        public void Click_Leave(object sender, RoutedEventArgs e)
        {
            if (estLogiciel || estAdmin)
            {
                estLogiciel = false;
                estAdmin = false;
                panel.Visibility = Visibility.Collapsed;
                wrapBtn.Visibility = Visibility.Visible;
            }
            else
            {
                PagePrincipale.MainInfo.Visibility = Visibility.Collapsed;
            }
        }

        public void Click_Valider(object sender, RoutedEventArgs e)
        {
            sql.VerificationMDP(out DataTable data, out int nbrRow);
            int i;
            for (i = 0;i < nbrRow; i++)
            {
                if (data.Rows[i]["motdepasse"].ToString() == mdpActuel.Text && (bool)data.Rows[i]["modeAdmin"] == estAdmin)
                {
                    break;
                }
            }
            if (i == nbrRow)
            {
                MessageBox.Show("Mot de passe actuel introuvable");
                return;
            }
            else
            {
                sql.ModificationMDPById(data.Rows[i]["id"], mdpNouv.Text);
                string type = estAdmin ? "Admin" : "Logiciel";
                sql.InsererJournal("Admin", $"Modification mot de passe {type}");
                estLogiciel = false;
                estAdmin = false;
                panel.Visibility = Visibility.Collapsed;
                wrapBtn.Visibility = Visibility.Visible;
                mdpActuel.Text = "";
                mdpNouv.Text = "";

            }
            
        }
    }
}
