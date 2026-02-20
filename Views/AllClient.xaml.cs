using CrispyLo.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Logique d'interaction pour AllClient.xaml
    /// </summary>
    public partial class AllClient : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        BaseDeDonnée sql = new BaseDeDonnée();
        DataGrid tableauClients = new DataGrid();

        TextBox txtNom = new TextBox();
        TextBox txtPrenom = new TextBox();
        TextBox txtTelephone = new TextBox();
        TextBox txtRue = new TextBox();
        TextBox txtNumeroRue = new TextBox();
        TextBox txtCodePostal = new TextBox();
        TextBox txtLocalite = new TextBox();

        Button btnSauvegarder = new Button();
        int idClientSelectionne = -1;

        public AllClient()
        {
            InitializeComponent();
            PrepareInitialize();
        }

        public void PrepareInitialize()
        {
            //Grille Colonne
            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(320) });

            //Grille Ligne
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            Tableau();
            BtnRetour();
            PanelDroit();
            ChargerClients();
            AffichageBas();
        }

        public void Tableau()
        {
            tableauClients.AutoGenerateColumns = false;
            tableauClients.IsReadOnly = true;
            tableauClients.SelectionMode = DataGridSelectionMode.Single;
            tableauClients.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            tableauClients.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            tableauClients.Margin = new Thickness(10, 0, 10, 10);
            tableauClients.Background = Brushes.Transparent;
            tableauClients.RowBackground = Brushes.Transparent;
            tableauClients.AlternatingRowBackground = Brushes.Transparent;
            tableauClients.Foreground = Brushes.White;
            tableauClients.GridLinesVisibility = DataGridGridLinesVisibility.None;
            tableauClients.BorderThickness = new Thickness(0);
            tableauClients.HeadersVisibility = DataGridHeadersVisibility.Column;
            tableauClients.FontSize = 16;
            tableauClients.RowHeight = 30;
            tableauClients.ColumnHeaderHeight = 30;
            Style headerStyle = new Style(typeof(DataGridColumnHeader));
            headerStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.LightBlue));
            headerStyle.Setters.Add(new Setter(ForegroundProperty, Brushes.Black));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.FontWeightProperty, FontWeights.Bold));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.PaddingProperty, new Thickness(5)));
            tableauClients.ColumnHeaderStyle = headerStyle;

            tableauClients.Columns.Add(new DataGridTextColumn { Header = "N°", Binding = new Binding("Id"), Width = 50 });
            tableauClients.Columns.Add(new DataGridTextColumn { Header = "Nom", Binding = new Binding("Nom"), Width = 120 });
            tableauClients.Columns.Add(new DataGridTextColumn { Header = "Prénom", Binding = new Binding("Prenom"), Width = 120 });
            tableauClients.Columns.Add(new DataGridTextColumn { Header = "Téléphone", Binding = new Binding("Telephone"), Width = 130 });
            tableauClients.Columns.Add(new DataGridTextColumn { Header = "Rue", Binding = new Binding("Rue"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
            tableauClients.Columns.Add(new DataGridTextColumn { Header = "N°", Binding = new Binding("NumeroRue"), Width = 60 });
            tableauClients.Columns.Add(new DataGridTextColumn { Header = "CP", Binding = new Binding("CodePostal"), Width = 70 });
            tableauClients.Columns.Add(new DataGridTextColumn { Header = "Localité", Binding = new Binding("Localite"), Width = 120 });

            tableauClients.SelectionChanged += TableauClients_SelectionChanged;

            Grid.SetColumn(tableauClients, 0);
            Grid.SetRow(tableauClients, 1);
            grdMain.Children.Add(tableauClients);
        }
        public void BtnRetour()
        {
            Button retour = new Button();
            retour.Content = "🔙";
            retour.FontWeight = FontWeights.Bold;
            retour.FontSize = 20;
            retour.Foreground = Brushes.White;
            retour.Background = Brushes.Red;
            retour.Width = 35;
            retour.Height = 35;
            retour.Margin = new Thickness(10);
            retour.HorizontalAlignment = HorizontalAlignment.Right;
            retour.VerticalAlignment = VerticalAlignment.Top;
            retour.Click += new RoutedEventHandler(Click_Retour);
            Grid.SetColumn(retour, 1);
            Grid.SetRow(retour, 0);
            grdMain.Children.Add(retour);
        }
        public void PanelDroit()
        {
            StackPanel panel = new StackPanel();
            panel.Margin = new Thickness(10);

            btnSauvegarder.Content = "💾";
            btnSauvegarder.FontWeight = FontWeights.Bold;
            btnSauvegarder.FontSize = 20;
            btnSauvegarder.Foreground = Brushes.White;
            btnSauvegarder.Background = Brushes.Green;
            btnSauvegarder.Width = 35;
            btnSauvegarder.Height = 35;
            btnSauvegarder.HorizontalAlignment = HorizontalAlignment.Right;
            btnSauvegarder.Margin = new Thickness(0, 0, 0, 10);
            btnSauvegarder.Visibility = Visibility.Collapsed;
            btnSauvegarder.Click += new RoutedEventHandler(Click_Sauvegarder);
            panel.Children.Add(btnSauvegarder);

            panel.Children.Add(CreerLabelTextBox("Nom", txtNom));
            panel.Children.Add(CreerLabelTextBox("Prénom", txtPrenom));
            panel.Children.Add(CreerLabelTextBox("Téléphone", txtTelephone));
            panel.Children.Add(CreerLabelTextBox("Rue", txtRue));
            panel.Children.Add(CreerLabelTextBox("N° Rue", txtNumeroRue));
            panel.Children.Add(CreerLabelTextBox("Code Postal", txtCodePostal));
            panel.Children.Add(CreerLabelTextBox("Localité", txtLocalite));

            Grid.SetColumn(panel, 1);
            Grid.SetRow(panel, 1);
            grdMain.Children.Add(panel);
        }

        private StackPanel CreerLabelTextBox(string labelText, TextBox textBox)
        {
            StackPanel sp = new StackPanel();
            sp.Margin = new Thickness(0, 0, 0, 10);

            Label label = new Label();
            label.Content = labelText;
            label.Foreground = Brushes.White;
            label.FontSize = 14;
            label.Padding = new Thickness(0, 0, 0, 2);

            textBox.IsReadOnly = true;
            textBox.FontSize = 14;
            textBox.Height = 30;
            textBox.Padding = new Thickness(5, 3, 5, 3);

            sp.Children.Add(label);
            sp.Children.Add(textBox);
            return sp;
        }

        public void ChargerClients()
        {
            DataTable dataT;
            int nbrRow;
            sql.AllClient(out dataT, out nbrRow);

            var listeClients = new List<object>();
            for (int i = 0; i < nbrRow; i++)
            {
                sql.AdresseByClient((int)dataT.Rows[i]["idAdresse"], out DataTable dataA);
                listeClients.Add(new
                {
                    Id = dataT.Rows[i]["id"].ToString(),
                    Nom = dataT.Rows[i]["nom"].ToString(),
                    Prenom = dataT.Rows[i]["prenom"].ToString(),
                    Telephone = dataT.Rows[i]["numero"].ToString(),
                    Rue = dataA.Rows[0]["nomRue"].ToString(),
                    NumeroRue = dataA.Rows[0]["numeroRue"].ToString(),
                    CodePostal = dataA.Rows[0]["codePostal"].ToString(),
                    Localite = dataA.Rows[0]["localite"].ToString()
                });
            }
            tableauClients.ItemsSource = listeClients;
        }

        private void TableauClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tableauClients.SelectedItem == null) return;

            // Si on était en mode modification, on annule
            SetReadOnly(true);
            btnSauvegarder.Visibility = Visibility.Collapsed;

            dynamic client = tableauClients.SelectedItem;
            idClientSelectionne = int.Parse(client.Id);
            txtNom.Text = client.Nom;
            txtPrenom.Text = client.Prenom;
            txtTelephone.Text = client.Telephone;
            txtRue.Text = client.Rue;
            txtNumeroRue.Text = client.NumeroRue;
            txtCodePostal.Text = client.CodePostal;
            txtLocalite.Text = client.Localite;
        }

        public void Click_Retour(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new Home();
        }

        public void AffichageBas()
        {
            WrapPanel wrap = new WrapPanel();
            wrap.Background = Brushes.LightGray;
            wrap.VerticalAlignment = VerticalAlignment.Bottom;
            wrap.HorizontalAlignment = HorizontalAlignment.Stretch;
            wrap.Margin = new Thickness(0, 0, 5, 120);
            Grid.SetColumn(wrap, 1);
            Grid.SetRow(wrap, 1);

            Button supprimer = new Button();
            Button modifier = new Button();

            TextBlock txtSupprimer = new TextBlock();
            txtSupprimer.Text = "🧾\n❌";
            txtSupprimer.TextAlignment = TextAlignment.Center;
            supprimer.Content = txtSupprimer;
            supprimer.FontWeight = FontWeights.Bold;
            supprimer.Height = 75;
            supprimer.Width = 125;
            supprimer.FontSize = 20;
            supprimer.Margin = new Thickness(30, 10, 5, 10);
            supprimer.Foreground = Brushes.Red;
            supprimer.HorizontalContentAlignment = HorizontalAlignment.Center;
            supprimer.VerticalContentAlignment = VerticalAlignment.Center;
            supprimer.Background = Brushes.Transparent;
            supprimer.Click += new RoutedEventHandler(Click_Supprimer);

            TextBlock txtModifier = new TextBlock();
            txtModifier.Text = "📝\nmodifier";
            txtModifier.TextAlignment = TextAlignment.Center;
            modifier.Content = txtModifier;
            modifier.FontWeight = FontWeights.Bold;
            modifier.Height = 75;
            modifier.Width = 125;
            modifier.FontSize = 20;
            modifier.Margin = new Thickness(15, 10, 5, 10);
            modifier.HorizontalContentAlignment = HorizontalAlignment.Center;
            modifier.VerticalContentAlignment = VerticalAlignment.Center;
            modifier.Background = Brushes.Transparent;
            modifier.Click += new RoutedEventHandler(Click_Modifier);

            wrap.Children.Add(modifier);
            wrap.Children.Add(supprimer);

            grdMain.Children.Add(wrap);
        }

        public void Click_Supprimer(object sender, RoutedEventArgs e)
        {
            if (idClientSelectionne == -1) return;

            if (sql.ClientEstLieACommande(idClientSelectionne))
            {
                MessageBox.Show("Ce client est lié à une ou plusieurs commandes, impossible de le supprimer.");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer ce client ?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                sql.SupprimerClient(idClientSelectionne);
                idClientSelectionne = -1;
                txtNom.Text = "";
                txtPrenom.Text = "";
                txtTelephone.Text = "";
                txtRue.Text = "";
                txtNumeroRue.Text = "";
                txtCodePostal.Text = "";
                txtLocalite.Text = "";
                ChargerClients();
            }
        }

        public void Click_Modifier(object sender, RoutedEventArgs e)
        {
            if (idClientSelectionne == -1) return;

            SetReadOnly(false);
            btnSauvegarder.Visibility = Visibility.Visible;
        }

        public void Click_Sauvegarder(object sender, RoutedEventArgs e)
        {
            if (idClientSelectionne == -1) return;

            string nom = txtNom.Text.Trim();
            string prenom = txtPrenom.Text.Trim();
            string telephone = txtTelephone.Text.Trim();
            string rue = txtRue.Text.Trim();
            string numeroRue = txtNumeroRue.Text.Trim();
            string codePostal = txtCodePostal.Text.Trim();
            string localite = txtLocalite.Text.Trim();

            if (nom == "" || telephone == "")
            {
                MessageBox.Show("Le nom et le téléphone sont obligatoires.");
                return;
            }

            sql.UpdateClient(idClientSelectionne, nom, prenom, telephone);

            // Récupérer l'idAdresse du client pour update l'adresse
            sql.ClientById(idClientSelectionne, out DataTable dataClient);
            if (dataClient.Rows.Count > 0)
            {
                int idAdresse = Convert.ToInt32(dataClient.Rows[0]["idAdresse"]);
                sql.UpdateAdresse(idAdresse, double.Parse(codePostal), rue, numeroRue, localite);
            }

            SetReadOnly(true);
            btnSauvegarder.Visibility = Visibility.Collapsed;
            ChargerClients();
            MessageBox.Show("Client modifié avec succès !");
        }

        private void SetReadOnly(bool readOnly)
        {
            txtNom.IsReadOnly = readOnly;
            txtPrenom.IsReadOnly = readOnly;
            txtTelephone.IsReadOnly = readOnly;
            txtRue.IsReadOnly = readOnly;
            txtNumeroRue.IsReadOnly = readOnly;
            txtCodePostal.IsReadOnly = readOnly;
            txtLocalite.IsReadOnly = readOnly;
        }
    }
}
