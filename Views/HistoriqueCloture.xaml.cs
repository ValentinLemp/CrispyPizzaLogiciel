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
    /// Logique d'interaction pour HistoriqueCloture.xaml
    /// </summary>
    public partial class HistoriqueCloture : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        BaseDeDonnée sql = new BaseDeDonnée();

        DataGrid tableauClotures = new DataGrid();
        StackPanel panelDetail = new StackPanel();

        TextBlock valDate = new TextBlock();
        TextBlock valArgentRest = new TextBlock();
        TextBlock valArgentNet = new TextBlock();
        TextBlock valArgentCarte = new TextBlock();
        TextBlock valArgentLiquide = new TextBlock();
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

        List<DataRow> listeClotures = new List<DataRow>();

        public HistoriqueCloture()
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
            PanelResume();
            ChargerClotures();
        }

        public void TopBar()
        {
            Grid topBar = new Grid();
            topBar.Background = Brushes.DarkSlateGray;
            Grid.SetRow(topBar, 0);
            Grid.SetColumnSpan(topBar, 2);

            TextBlock titre = new TextBlock();
            titre.Text = "HISTORIQUE CLÔTURE";
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
            tableauClotures.AutoGenerateColumns = false;
            tableauClotures.IsReadOnly = true;
            tableauClotures.SelectionMode = DataGridSelectionMode.Single;
            tableauClotures.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            tableauClotures.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            tableauClotures.Margin = new Thickness(10, 10, 10, 10);
            tableauClotures.Background = Brushes.Transparent;
            tableauClotures.RowBackground = Brushes.Transparent;
            tableauClotures.AlternatingRowBackground = Brushes.Transparent;
            tableauClotures.Foreground = Brushes.White;
            tableauClotures.GridLinesVisibility = DataGridGridLinesVisibility.None;
            tableauClotures.BorderThickness = new Thickness(0);
            tableauClotures.HeadersVisibility = DataGridHeadersVisibility.Column;
            tableauClotures.FontSize = 15;
            tableauClotures.RowHeight = 30;
            tableauClotures.ColumnHeaderHeight = 30;

            Style headerStyle = new Style(typeof(DataGridColumnHeader));
            headerStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.LightBlue));
            headerStyle.Setters.Add(new Setter(ForegroundProperty, Brushes.Black));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.FontWeightProperty, FontWeights.Bold));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.PaddingProperty, new Thickness(5)));
            tableauClotures.ColumnHeaderStyle = headerStyle;

            tableauClotures.Columns.Add(new DataGridTextColumn { Header = "N°", Binding = new Binding("Numero"), Width = 50 });
            tableauClotures.Columns.Add(new DataGridTextColumn { Header = "Date", Binding = new Binding("Date"), Width = 100 });
            tableauClotures.Columns.Add(new DataGridTextColumn { Header = "Restaurant", Binding = new Binding("ArgentRest"), Width = 90 });
            tableauClotures.Columns.Add(new DataGridTextColumn { Header = "Internet", Binding = new Binding("ArgentNet"), Width = 85 });
            tableauClotures.Columns.Add(new DataGridTextColumn { Header = "Carte", Binding = new Binding("ArgentCarte"), Width = 85 });
            tableauClotures.Columns.Add(new DataGridTextColumn { Header = "Liquide", Binding = new Binding("ArgentLiquide"), Width = 85 });
            tableauClotures.Columns.Add(new DataGridTextColumn { Header = "Différence", Binding = new Binding("Difference"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });

            tableauClotures.LoadingRow += TableauClotures_LoadingRow;

            tableauClotures.SelectionChanged += Tableau_SelectionChanged;

            Grid.SetColumn(tableauClotures, 0);
            Grid.SetRow(tableauClotures, 1);
            grdMain.Children.Add(tableauClotures);
        }

        public void PanelResume()
        {
            ScrollViewer scroll = new ScrollViewer();
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scroll.Margin = new Thickness(0, 5, 5, 5);
            Grid.SetColumn(scroll, 1);
            Grid.SetRow(scroll, 1);

            panelDetail.Background = Brushes.White;
            panelDetail.Margin = new Thickness(10);

            // ---- Date ----
            valDate.Text = "";
            valDate.FontSize = 18;
            valDate.FontWeight = FontWeights.Bold;
            valDate.HorizontalAlignment = HorizontalAlignment.Center;
            valDate.Margin = new Thickness(0, 10, 0, 15);
            panelDetail.Children.Add(valDate);

            // ---- Argent ----
            TextBlock titreArgent = new TextBlock();
            titreArgent.Text = "Argent";
            titreArgent.FontSize = 16;
            titreArgent.FontWeight = FontWeights.Bold;
            titreArgent.Margin = new Thickness(15, 0, 0, 8);
            panelDetail.Children.Add(titreArgent);

            panelDetail.Children.Add(CreerLigneDetail("Restaurant :", valArgentRest));
            panelDetail.Children.Add(CreerLigneDetail("Internet :", valArgentNet));
            panelDetail.Children.Add(CreerLigneDetail("Carte :", valArgentCarte));
            panelDetail.Children.Add(CreerLigneDetail("Liquide :", valArgentLiquide));
            panelDetail.Children.Add(CreerLigneDetail("Différence :", valDifference));

            // ---- Séparateur ----
            Border sep = new Border();
            sep.BorderBrush = Brushes.LightGray;
            sep.BorderThickness = new Thickness(0, 1, 0, 0);
            sep.Margin = new Thickness(15, 10, 15, 10);
            panelDetail.Children.Add(sep);

            // ---- Produits ----
            TextBlock titreProduits = new TextBlock();
            titreProduits.Text = "Produits vendus";
            titreProduits.FontSize = 16;
            titreProduits.FontWeight = FontWeights.Bold;
            titreProduits.Margin = new Thickness(15, 0, 0, 8);
            panelDetail.Children.Add(titreProduits);

            panelDetail.Children.Add(CreerLigneDetail("Maxi Pizza :", valMaxiPizza));
            panelDetail.Children.Add(CreerLigneDetail("Pizza :", valPizza));
            panelDetail.Children.Add(CreerLigneDetail("Pâte :", valPate));
            panelDetail.Children.Add(CreerLigneDetail("Pain :", valPain));
            panelDetail.Children.Add(CreerLigneDetail("Salade :", valSalade));
            panelDetail.Children.Add(CreerLigneDetail("Snacking :", valSnacking));
            panelDetail.Children.Add(CreerLigneDetail("Boisson :", valBoisson));
            panelDetail.Children.Add(CreerLigneDetail("Dessert :", valDessert));
            panelDetail.Children.Add(CreerLigneDetail("Dipy :", valDipy));

            // ---- Bouton Imprimer ----
            Button btnImprimer = new Button();
            btnImprimer.Content = "🧾 Imprimer";
            btnImprimer.FontSize = 16;
            btnImprimer.FontWeight = FontWeights.Bold;
            btnImprimer.Width = 150;
            btnImprimer.Height = 40;
            btnImprimer.Margin = new Thickness(0, 20, 0, 10);
            btnImprimer.HorizontalAlignment = HorizontalAlignment.Center;
            btnImprimer.Click += new RoutedEventHandler(Click_Imprimer);
            panelDetail.Children.Add(btnImprimer);

            scroll.Content = panelDetail;
            grdMain.Children.Add(scroll);
        }

        private WrapPanel CreerLigneDetail(string label, TextBlock valeur)
        {
            WrapPanel ligne = new WrapPanel();
            ligne.Margin = new Thickness(15, 0, 15, 5);

            TextBlock lbl = new TextBlock();
            lbl.Text = label;
            lbl.FontSize = 14;
            lbl.FontWeight = FontWeights.Bold;
            lbl.Width = 110;
            lbl.VerticalAlignment = VerticalAlignment.Center;
            ligne.Children.Add(lbl);

            valeur.Text = "-";
            valeur.FontSize = 14;
            valeur.FontWeight = FontWeights.Bold;
            valeur.VerticalAlignment = VerticalAlignment.Center;
            ligne.Children.Add(valeur);

            return ligne;
        }

        public void ChargerClotures()
        {
            sql.AllClotureByRestaurant(out DataTable dataT, out int nbrRow);

            listeClotures.Clear();
            List<object> lignes = new List<object>();

            for (int i = 0; i < nbrRow; i++)
            {
                listeClotures.Add(dataT.Rows[i]);

                DateTime date = Convert.ToDateTime(dataT.Rows[i]["date"]);
                double argentRest = Convert.ToDouble(dataT.Rows[i]["argentRestaurant"]);
                double argentNet = Convert.ToDouble(dataT.Rows[i]["argentInternet"]);
                double argentCarte = Convert.ToDouble(dataT.Rows[i]["argentCarte"]);
                double argentLiquide = Convert.ToDouble(dataT.Rows[i]["argentLiquide"]);
                double diff = Convert.ToDouble(dataT.Rows[i]["argentDifference"]);

                int numero = Convert.ToInt32(dataT.Rows[i]["numeroCloture"]);

                lignes.Add(new
                {
                    Numero = numero,
                    Date = date.ToString("dd/MM/yyyy"),
                    ArgentRest = argentRest.ToString("0.00") + " €",
                    ArgentNet = argentNet.ToString("0.00") + " €",
                    ArgentCarte = argentCarte.ToString("0.00") + " €",
                    ArgentLiquide = argentLiquide.ToString("0.00") + " €",
                    Difference = diff.ToString("0.00") + " €",
                    DiffValue = diff
                });
            }

            tableauClotures.ItemsSource = null;
            tableauClotures.ItemsSource = lignes;
        }

        private void Tableau_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tableauClotures.SelectedIndex < 0) return;

            DataRow row = listeClotures[tableauClotures.SelectedIndex];

            DateTime date = Convert.ToDateTime(row["date"]);
            valDate.Text = date.ToString("dd/MM/yyyy");

            double argentRest = Convert.ToDouble(row["argentRestaurant"]);
            double argentNet = Convert.ToDouble(row["argentInternet"]);
            double diff = Convert.ToDouble(row["argentDifference"]);

            double argentCarte = Convert.ToDouble(row["argentCarte"]);
            double argentLiquide = Convert.ToDouble(row["argentLiquide"]);

            valArgentRest.Text = argentRest.ToString("0.00") + " €";
            valArgentNet.Text = argentNet.ToString("0.00") + " €";
            valArgentCarte.Text = argentCarte.ToString("0.00") + " €";
            valArgentLiquide.Text = argentLiquide.ToString("0.00") + " €";
            valDifference.Text = diff.ToString("0.00") + " €";

            if (diff < 0) valDifference.Foreground = Brushes.Red;
            else if (diff > 0) valDifference.Foreground = Brushes.Green;
            else valDifference.Foreground = Brushes.Black;

            valMaxiPizza.Text = row["nbMaxiPizza"].ToString();
            valPizza.Text = row["nbPizza"].ToString();
            valPate.Text = row["nbPâte"].ToString();
            valPain.Text = row["nbPain"].ToString();
            valSalade.Text = row["nbSalade"].ToString();
            valSnacking.Text = row["nbSnacking"].ToString();
            valBoisson.Text = row["nbBoisson"].ToString();
            valDessert.Text = row["nbDessert"].ToString();
            valDipy.Text = row["nbDipy"].ToString();
        }

        private void TableauClotures_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            dynamic item = e.Row.DataContext;
            double diff = item.DiffValue;

            if (diff < 0)
                e.Row.Foreground = Brushes.Red;
            else if (diff > 0)
                e.Row.Foreground = Brushes.LimeGreen;
            else
                e.Row.Foreground = Brushes.White;
        }

        public void Click_Imprimer(object sender, RoutedEventArgs e)
        {
            if (tableauClotures.SelectedItem == null) return;

            // ici code imprimer
        }

        public void Click_Retour(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new HomeAdmin();
        }
    }
}
