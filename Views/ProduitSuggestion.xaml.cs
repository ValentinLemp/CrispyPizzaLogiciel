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
    /// Logique d'interaction pour ProduitSuggestion.xaml
    /// </summary>
    public partial class ProduitSuggestion : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        BaseDeDonnée sql = new BaseDeDonnée();

        DataGrid tableauSuggestions = new DataGrid();
        List<DataRow> listeSuggestions = new List<DataRow>();

        ComboBox cbTypeProduit = new ComboBox();
        TextBox txtLibelle = new TextBox();
        TextBox txtDescription = new TextBox();
        TextBox txtPrix = new TextBox();
        TextBox txtPrixLarge = new TextBox();
        TextBlock lblPrixLarge = new TextBlock();

        int idModification = -1;

        public ProduitSuggestion()
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
            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(320) });

            TopBar();
            Tableau();
            PanelFormulaire();
            ChargerSuggestions();
        }

        public void TopBar()
        {
            Grid topBar = new Grid();
            topBar.Background = Brushes.DarkSlateGray;
            Grid.SetRow(topBar, 0);
            Grid.SetColumnSpan(topBar, 2);

            TextBlock titre = new TextBlock();
            titre.Text = "SUGGESTIONS";
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
            retour.Margin = new Thickness(0, 0, 15, 0);
            retour.Click += new RoutedEventHandler(Click_Retour);
            topBar.Children.Add(retour);

            grdMain.Children.Add(topBar);
        }

        public void Tableau()
        {
            tableauSuggestions.AutoGenerateColumns = false;
            tableauSuggestions.IsReadOnly = true;
            tableauSuggestions.SelectionMode = DataGridSelectionMode.Single;
            tableauSuggestions.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            tableauSuggestions.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            tableauSuggestions.Margin = new Thickness(10, 10, 10, 10);
            tableauSuggestions.Background = Brushes.Transparent;
            tableauSuggestions.RowBackground = Brushes.Transparent;
            tableauSuggestions.AlternatingRowBackground = Brushes.Transparent;
            tableauSuggestions.Foreground = Brushes.White;
            tableauSuggestions.GridLinesVisibility = DataGridGridLinesVisibility.None;
            tableauSuggestions.BorderThickness = new Thickness(0);
            tableauSuggestions.HeadersVisibility = DataGridHeadersVisibility.Column;
            tableauSuggestions.FontSize = 15;
            tableauSuggestions.RowHeight = 30;
            tableauSuggestions.ColumnHeaderHeight = 30;

            Style headerStyle = new Style(typeof(DataGridColumnHeader));
            headerStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.LightBlue));
            headerStyle.Setters.Add(new Setter(ForegroundProperty, Brushes.Black));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.FontWeightProperty, FontWeights.Bold));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.PaddingProperty, new Thickness(5)));
            tableauSuggestions.ColumnHeaderStyle = headerStyle;

            tableauSuggestions.Columns.Add(new DataGridTextColumn { Header = "Libellé", Binding = new Binding("Libelle"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
            tableauSuggestions.Columns.Add(new DataGridTextColumn { Header = "Type", Binding = new Binding("Type"), Width = 80 });
            tableauSuggestions.Columns.Add(new DataGridTextColumn { Header = "Prix", Binding = new Binding("Prix"), Width = 70 });
            tableauSuggestions.Columns.Add(new DataGridTextColumn { Header = "Prix L.", Binding = new Binding("PrixLarge"), Width = 70 });

            tableauSuggestions.SelectionChanged += Tableau_SelectionChanged;

            Grid.SetColumn(tableauSuggestions, 0);
            Grid.SetRow(tableauSuggestions, 1);
            grdMain.Children.Add(tableauSuggestions);
        }

        public void PanelFormulaire()
        {
            ScrollViewer scroll = new ScrollViewer();
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scroll.Margin = new Thickness(0, 5, 5, 5);
            Grid.SetColumn(scroll, 1);
            Grid.SetRow(scroll, 1);

            StackPanel panel = new StackPanel();
            panel.Background = Brushes.White;
            panel.Margin = new Thickness(10);

            // Type produit
            TextBlock lblType = new TextBlock();
            lblType.Text = "Type de produit :";
            lblType.FontSize = 14;
            lblType.FontWeight = FontWeights.Bold;
            lblType.Margin = new Thickness(15, 15, 15, 5);
            panel.Children.Add(lblType);

            cbTypeProduit.Width = 200;
            cbTypeProduit.Height = 28;
            cbTypeProduit.FontSize = 14;
            cbTypeProduit.HorizontalAlignment = HorizontalAlignment.Left;
            cbTypeProduit.Margin = new Thickness(15, 0, 15, 10);
            cbTypeProduit.Items.Add(new ComboBoxItem { Content = "Pizza", Tag = 1 });
            cbTypeProduit.Items.Add(new ComboBoxItem { Content = "Pâte", Tag = 2 });
            cbTypeProduit.Items.Add(new ComboBoxItem { Content = "Salade", Tag = 3 });
            cbTypeProduit.Items.Add(new ComboBoxItem { Content = "Pain", Tag = 4 });
            cbTypeProduit.SelectedIndex = 0;
            cbTypeProduit.SelectionChanged += CbType_Changed;
            panel.Children.Add(cbTypeProduit);

            // Libellé
            TextBlock lblLibelle = new TextBlock();
            lblLibelle.Text = "Libellé :";
            lblLibelle.FontSize = 14;
            lblLibelle.FontWeight = FontWeights.Bold;
            lblLibelle.Margin = new Thickness(15, 0, 15, 5);
            panel.Children.Add(lblLibelle);

            txtLibelle.Width = 250;
            txtLibelle.Height = 28;
            txtLibelle.FontSize = 14;
            txtLibelle.HorizontalAlignment = HorizontalAlignment.Left;
            txtLibelle.Margin = new Thickness(15, 0, 15, 10);
            panel.Children.Add(txtLibelle);

            // Description (ingrédients)
            TextBlock lblDesc = new TextBlock();
            lblDesc.Text = "Description (ingrédients, séparés par ,) :";
            lblDesc.FontSize = 14;
            lblDesc.FontWeight = FontWeights.Bold;
            lblDesc.Margin = new Thickness(15, 0, 15, 5);
            panel.Children.Add(lblDesc);

            txtDescription.Width = 250;
            txtDescription.Height = 60;
            txtDescription.FontSize = 14;
            txtDescription.TextWrapping = TextWrapping.Wrap;
            txtDescription.AcceptsReturn = false;
            txtDescription.HorizontalAlignment = HorizontalAlignment.Left;
            txtDescription.Margin = new Thickness(15, 0, 15, 10);
            panel.Children.Add(txtDescription);

            // Prix
            TextBlock lblPrix = new TextBlock();
            lblPrix.Text = "Prix :";
            lblPrix.FontSize = 14;
            lblPrix.FontWeight = FontWeights.Bold;
            lblPrix.Margin = new Thickness(15, 0, 15, 5);
            panel.Children.Add(lblPrix);

            txtPrix.Width = 120;
            txtPrix.Height = 28;
            txtPrix.FontSize = 14;
            txtPrix.HorizontalAlignment = HorizontalAlignment.Left;
            txtPrix.Margin = new Thickness(15, 0, 15, 10);
            panel.Children.Add(txtPrix);

            // Prix Large (visible seulement si pizza)
            lblPrixLarge.Text = "Prix Large :";
            lblPrixLarge.FontSize = 14;
            lblPrixLarge.FontWeight = FontWeights.Bold;
            lblPrixLarge.Margin = new Thickness(15, 0, 15, 5);
            panel.Children.Add(lblPrixLarge);

            txtPrixLarge.Width = 120;
            txtPrixLarge.Height = 28;
            txtPrixLarge.FontSize = 14;
            txtPrixLarge.HorizontalAlignment = HorizontalAlignment.Left;
            txtPrixLarge.Margin = new Thickness(15, 0, 15, 15);
            panel.Children.Add(txtPrixLarge);

            // Boutons
            WrapPanel wrapBtns = new WrapPanel();
            wrapBtns.HorizontalAlignment = HorizontalAlignment.Center;
            wrapBtns.Margin = new Thickness(0, 0, 0, 10);

            Button btnAjouter = new Button();
            btnAjouter.Content = "Ajouter";
            btnAjouter.FontSize = 14;
            btnAjouter.FontWeight = FontWeights.Bold;
            btnAjouter.Width = 85;
            btnAjouter.Height = 35;
            btnAjouter.Background = Brushes.LimeGreen;
            btnAjouter.Margin = new Thickness(5);
            btnAjouter.Click += new RoutedEventHandler(Click_Ajouter);
            wrapBtns.Children.Add(btnAjouter);

            Button btnModifier = new Button();
            btnModifier.Content = "Modifier";
            btnModifier.FontSize = 14;
            btnModifier.FontWeight = FontWeights.Bold;
            btnModifier.Width = 85;
            btnModifier.Height = 35;
            btnModifier.Background = Brushes.Orange;
            btnModifier.Margin = new Thickness(5);
            btnModifier.Click += new RoutedEventHandler(Click_Modifier);
            wrapBtns.Children.Add(btnModifier);

            Button btnSupprimer = new Button();
            btnSupprimer.Content = "Supprimer";
            btnSupprimer.FontSize = 14;
            btnSupprimer.FontWeight = FontWeights.Bold;
            btnSupprimer.Width = 85;
            btnSupprimer.Height = 35;
            btnSupprimer.Background = Brushes.Red;
            btnSupprimer.Foreground = Brushes.White;
            btnSupprimer.Margin = new Thickness(5);
            btnSupprimer.Click += new RoutedEventHandler(Click_Supprimer);
            wrapBtns.Children.Add(btnSupprimer);

            panel.Children.Add(wrapBtns);

            scroll.Content = panel;
            grdMain.Children.Add(scroll);
        }

        private string GetNomType(int idType)
        {
            switch (idType)
            {
                case 1: return "Pizza";
                case 2: return "Pâte";
                case 3: return "Salade";
                case 4: return "Pain";
                default: return "";
            }
        }

        public void ChargerSuggestions()
        {
            sql.AllSuggestions(out DataTable dataT, out int nbrRow);

            listeSuggestions.Clear();
            List<object> lignes = new List<object>();

            for (int i = 0; i < nbrRow; i++)
            {
                listeSuggestions.Add(dataT.Rows[i]);

                string prixLarge = dataT.Rows[i]["prixLarge"] != DBNull.Value
                    ? Convert.ToDouble(dataT.Rows[i]["prixLarge"]).ToString("0.00") + " €"
                    : "-";

                lignes.Add(new
                {
                    Libelle = dataT.Rows[i]["libelle"].ToString(),
                    Type = GetNomType(Convert.ToInt32(dataT.Rows[i]["idTypeProduit"])),
                    Prix = Convert.ToDouble(dataT.Rows[i]["prix"]).ToString("0.00") + " €",
                    PrixLarge = prixLarge
                });
            }

            tableauSuggestions.ItemsSource = null;
            tableauSuggestions.ItemsSource = lignes;
        }

        private void Tableau_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tableauSuggestions.SelectedIndex < 0) return;

            DataRow row = listeSuggestions[tableauSuggestions.SelectedIndex];
            idModification = Convert.ToInt32(row["id"]);

            txtLibelle.Text = row["libelle"].ToString();
            txtDescription.Text = row["description"].ToString();
            txtPrix.Text = Convert.ToDouble(row["prix"]).ToString("0.00");

            int idType = Convert.ToInt32(row["idTypeProduit"]);
            for (int i = 0; i < cbTypeProduit.Items.Count; i++)
            {
                ComboBoxItem item = (ComboBoxItem)cbTypeProduit.Items[i];
                if ((int)item.Tag == idType)
                {
                    cbTypeProduit.SelectedIndex = i;
                    break;
                }
            }

            if (row["prixLarge"] != DBNull.Value)
                txtPrixLarge.Text = Convert.ToDouble(row["prixLarge"]).ToString("0.00");
            else
                txtPrixLarge.Text = "";
        }

        private void CbType_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (cbTypeProduit.SelectedItem == null) return;
            int idType = (int)((ComboBoxItem)cbTypeProduit.SelectedItem).Tag;
            bool estPizza = idType == 1;
            lblPrixLarge.Visibility = estPizza ? Visibility.Visible : Visibility.Collapsed;
            txtPrixLarge.Visibility = estPizza ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ViderChamps()
        {
            txtLibelle.Text = "";
            txtDescription.Text = "";
            txtPrix.Text = "";
            txtPrixLarge.Text = "";
            cbTypeProduit.SelectedIndex = 0;
            idModification = -1;
            tableauSuggestions.SelectedIndex = -1;
        }

        private void Click_Ajouter(object sender, RoutedEventArgs e)
        {
            if (txtLibelle.Text.Trim() == "" || txtPrix.Text.Trim() == "")
            {
                MessageBox.Show("Veuillez remplir le libellé et le prix.");
                return;
            }

            if (!decimal.TryParse(txtPrix.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal prix))
            {
                MessageBox.Show("Prix invalide.");
                return;
            }

            int idType = (int)((ComboBoxItem)cbTypeProduit.SelectedItem).Tag;
            decimal? prixLarge = null;

            if (idType == 1)
            {
                if (txtPrixLarge.Text.Trim() == "")
                {
                    MessageBox.Show("Le prix large est obligatoire pour une pizza.");
                    return;
                }
                if (!decimal.TryParse(txtPrixLarge.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal pl))
                {
                    MessageBox.Show("Prix large invalide.");
                    return;
                }
                prixLarge = pl;
            }

            sql.AjouterSuggestion(txtLibelle.Text.Trim(), txtDescription.Text.Trim(), prix, prixLarge, idType);
            MessageBox.Show("Suggestion ajoutée !");
            ViderChamps();
            ChargerSuggestions();
        }

        private void Click_Modifier(object sender, RoutedEventArgs e)
        {
            if (idModification == -1)
            {
                MessageBox.Show("Veuillez sélectionner une suggestion à modifier.");
                return;
            }

            if (txtLibelle.Text.Trim() == "" || txtPrix.Text.Trim() == "")
            {
                MessageBox.Show("Veuillez remplir le libellé et le prix.");
                return;
            }

            if (!decimal.TryParse(txtPrix.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal prix))
            {
                MessageBox.Show("Prix invalide.");
                return;
            }

            int idType = (int)((ComboBoxItem)cbTypeProduit.SelectedItem).Tag;
            decimal? prixLarge = null;

            if (idType == 1)
            {
                if (txtPrixLarge.Text.Trim() == "")
                {
                    MessageBox.Show("Le prix large est obligatoire pour une pizza.");
                    return;
                }
                if (!decimal.TryParse(txtPrixLarge.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal pl))
                {
                    MessageBox.Show("Prix large invalide.");
                    return;
                }
                prixLarge = pl;
            }

            sql.ModifierSuggestion(idModification, txtLibelle.Text.Trim(), txtDescription.Text.Trim(), prix, prixLarge, idType);
            MessageBox.Show("Suggestion modifiée !");
            ViderChamps();
            ChargerSuggestions();
        }

        private void Click_Supprimer(object sender, RoutedEventArgs e)
        {
            if (idModification == -1)
            {
                MessageBox.Show("Veuillez sélectionner une suggestion à supprimer.");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer cette suggestion ?",
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;

            sql.SupprimerSuggestion(idModification);
            MessageBox.Show("Suggestion supprimée !");
            ViderChamps();
            ChargerSuggestions();
        }

        public void Click_Retour(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new HomeAdmin();
        }
    }
}
