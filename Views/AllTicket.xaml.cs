using CrispyLo.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace CrispyLo.Views
{
    public partial class AllTicket : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        BaseDeDonnée sql = new BaseDeDonnée();

        DataGrid tableauTickets = new DataGrid();
        DatePicker datePicker = new DatePicker();
        List<DataRow> listeTickets = new List<DataRow>();

        public AllTicket()
        {
            InitializeComponent();
            PrepareInitialize();
        }

        public void PrepareInitialize()
        {
            this.Background = Brushes.DimGray;

            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(55) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(55) });

            TopBar();
            BarreDate();
            Tableau();
            BarreBoutons();

            datePicker.SelectedDate = DateTime.Today;
            ChargerTickets();
        }

        public void TopBar()
        {
            Grid topBar = new Grid();
            topBar.Background = Brushes.DarkSlateGray;
            Grid.SetRow(topBar, 0);

            TextBlock titre = new TextBlock();
            titre.Text = "HISTORIQUE TICKETS";
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
            retour.Click += (s, e) => PagePrincipale.Main.Content = new HomeAdmin();
            topBar.Children.Add(retour);

            grdMain.Children.Add(topBar);
        }

        public void BarreDate()
        {
            StackPanel barre = new StackPanel();
            barre.Orientation = Orientation.Horizontal;
            barre.VerticalAlignment = VerticalAlignment.Center;
            barre.Margin = new Thickness(15, 0, 0, 0);
            Grid.SetRow(barre, 1);

            TextBlock lbl = new TextBlock();
            lbl.Text = "Date :";
            lbl.FontSize = 15;
            lbl.FontWeight = FontWeights.Bold;
            lbl.Foreground = Brushes.White;
            lbl.VerticalAlignment = VerticalAlignment.Center;
            lbl.Margin = new Thickness(0, 0, 10, 0);
            barre.Children.Add(lbl);

            datePicker.Width = 150;
            datePicker.Height = 30;
            datePicker.FontSize = 14;
            datePicker.SelectedDateChanged += (s, e) => ChargerTickets();
            barre.Children.Add(datePicker);

            grdMain.Children.Add(barre);
        }

        public void Tableau()
        {
            tableauTickets.AutoGenerateColumns = false;
            tableauTickets.IsReadOnly = true;
            tableauTickets.SelectionMode = DataGridSelectionMode.Single;
            tableauTickets.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            tableauTickets.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            tableauTickets.Margin = new Thickness(10, 5, 10, 5);
            tableauTickets.Background = Brushes.Transparent;
            tableauTickets.RowBackground = Brushes.Transparent;
            tableauTickets.AlternatingRowBackground = Brushes.Transparent;
            tableauTickets.Foreground = Brushes.White;
            tableauTickets.GridLinesVisibility = DataGridGridLinesVisibility.None;
            tableauTickets.BorderThickness = new Thickness(0);
            tableauTickets.HeadersVisibility = DataGridHeadersVisibility.Column;
            tableauTickets.FontSize = 14;
            tableauTickets.RowHeight = 30;
            tableauTickets.ColumnHeaderHeight = 30;

            Style headerStyle = new Style(typeof(DataGridColumnHeader));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.BackgroundProperty, Brushes.LightBlue));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.ForegroundProperty, Brushes.Black));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.FontWeightProperty, FontWeights.Bold));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.PaddingProperty, new Thickness(5)));
            tableauTickets.ColumnHeaderStyle = headerStyle;

            tableauTickets.Columns.Add(new DataGridTextColumn { Header = "N°", Binding = new Binding("Id"), Width = 50 });
            tableauTickets.Columns.Add(new DataGridTextColumn { Header = "Type", Binding = new Binding("Type"), Width = 90 });
            tableauTickets.Columns.Add(new DataGridTextColumn { Header = "Date", Binding = new Binding("Date"), Width = 130 });
            tableauTickets.Columns.Add(new DataGridTextColumn { Header = "Total HT", Binding = new Binding("TotalHT"), Width = 85 });
            tableauTickets.Columns.Add(new DataGridTextColumn { Header = "Total TVA", Binding = new Binding("TotalTVA"), Width = 85 });
            tableauTickets.Columns.Add(new DataGridTextColumn { Header = "Total TTC", Binding = new Binding("TotalTTC"), Width = 85 });
            tableauTickets.Columns.Add(new DataGridTextColumn { Header = "Ticket réf", Binding = new Binding("TicketRef"), Width = 80 });
            tableauTickets.Columns.Add(new DataGridTextColumn { Header = "Signature", Binding = new Binding("Signature"), Width = 160 });
            tableauTickets.Columns.Add(new DataGridTextColumn { Header = "Signature préc.", Binding = new Binding("SignaturePrec"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });

            tableauTickets.LoadingRow += (s, e) =>
            {
                dynamic item = e.Row.DataContext;
                string type = item.TypeRaw;
                if (type == "annulation" || type == "annule")
                    e.Row.Foreground = Brushes.Salmon;
                else
                    e.Row.Foreground = Brushes.White;
            };

            Grid.SetRow(tableauTickets, 2);
            grdMain.Children.Add(tableauTickets);
        }

        public void BarreBoutons()
        {
            StackPanel barre = new StackPanel();
            barre.Orientation = Orientation.Horizontal;
            barre.HorizontalAlignment = HorizontalAlignment.Center;
            barre.VerticalAlignment = VerticalAlignment.Center;
            barre.Margin = new Thickness(0, 5, 0, 5);
            Grid.SetRow(barre, 3);

            Button btnImprimer = new Button();
            btnImprimer.Content = "🧾 Imprimer ticket";
            btnImprimer.FontSize = 14;
            btnImprimer.FontWeight = FontWeights.Bold;
            btnImprimer.Width = 180;
            btnImprimer.Height = 38;
            btnImprimer.Margin = new Thickness(0, 0, 20, 0);
            btnImprimer.Click += Click_ImprimerTicket;
            barre.Children.Add(btnImprimer);

            Button btnImprimerJournee = new Button();
            btnImprimerJournee.Content = "🖨️ Imprimer la journée";
            btnImprimerJournee.FontSize = 14;
            btnImprimerJournee.FontWeight = FontWeights.Bold;
            btnImprimerJournee.Width = 200;
            btnImprimerJournee.Height = 38;
            btnImprimerJournee.Click += Click_ImprimerJournee;
            barre.Children.Add(btnImprimerJournee);

            grdMain.Children.Add(barre);
        }

        public void ChargerTickets()
        {
            if (datePicker.SelectedDate == null) return;

            DateTime date = datePicker.SelectedDate.Value;
            sql.AllTicketByDate(date, out DataTable dataT, out int nbrRow);

            listeTickets.Clear();
            List<object> lignes = new List<object>();

            for (int i = 0; i < nbrRow; i++)
            {
                listeTickets.Add(dataT.Rows[i]);

                int id = Convert.ToInt32(dataT.Rows[i]["id"]);
                string type = dataT.Rows[i]["type"].ToString();
                DateTime dateVal = Convert.ToDateTime(dataT.Rows[i]["dateValidation"]);
                decimal ht = Convert.ToDecimal(dataT.Rows[i]["total_ht"]);
                decimal tva = Convert.ToDecimal(dataT.Rows[i]["total_tva"]);
                decimal ttc = Convert.ToDecimal(dataT.Rows[i]["total_ttc"]);
                string signature = dataT.Rows[i]["signature"]?.ToString() ?? "";
                string signaturePrec = dataT.Rows[i]["signature_prec"]?.ToString() ?? "";
                int ticketRef = Convert.ToInt32(dataT.Rows[i]["ticket_ref"]);

                lignes.Add(new
                {
                    Id = id,
                    Type = type,
                    TypeRaw = type,
                    Date = dateVal.ToString("dd/MM/yyyy HH:mm"),
                    TotalHT = ht.ToString("0.00") + " €",
                    TotalTVA = tva.ToString("0.00") + " €",
                    TotalTTC = ttc.ToString("0.00") + " €",
                    Signature = signature,
                    SignaturePrec = signaturePrec,
                    TicketRef = ticketRef == -1 ? "" : ticketRef.ToString()
                });
            }

            tableauTickets.ItemsSource = null;
            tableauTickets.ItemsSource = lignes;
        }

        private void Click_ImprimerTicket(object sender, RoutedEventArgs e)
        {
            if (tableauTickets.SelectedIndex < 0) return;

            DataRow row = listeTickets[tableauTickets.SelectedIndex];
            int idTicket = Convert.ToInt32(row["id"]);
            ImpressionTicket imp = new ImpressionTicket();
            imp.ImpressionTicketVente(idTicket);
        }

        private void Click_ImprimerJournee(object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate == null) return;

            if (listeTickets.Count == 0)
            {
                MessageBox.Show("Aucun ticket pour cette journée.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                $"Imprimer les {listeTickets.Count} tickets de la journée ?",
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            ImpressionTicket imp = new ImpressionTicket();
            imp.ImpressionTousTickets(listeTickets);
        }
    }
}
