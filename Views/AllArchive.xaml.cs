using CrispyLo.Modules;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace CrispyLo.Views
{
    public partial class AllArchive : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        BaseDeDonnée sql = new BaseDeDonnée();

        DataGrid tableauJournal = new DataGrid();
        DatePicker datePicker = new DatePicker();
        List<DataRow> listeJournal = new List<DataRow>();

        public AllArchive()
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

            datePicker.SelectedDate = DateTime.Today;
            ChargerJournal();
        }

        public void TopBar()
        {
            Grid topBar = new Grid();
            topBar.Background = Brushes.DarkSlateGray;
            Grid.SetRow(topBar, 0);

            TextBlock titre = new TextBlock();
            titre.Text = "JOURNAL DES ÉVÉNEMENTS";
            titre.FontSize = 24;
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
            datePicker.SelectedDateChanged += (s, e) => ChargerJournal();
            barre.Children.Add(datePicker);

            grdMain.Children.Add(barre);
        }

        public void Tableau()
        {
            tableauJournal.AutoGenerateColumns = false;
            tableauJournal.IsReadOnly = true;
            tableauJournal.SelectionMode = DataGridSelectionMode.Single;
            tableauJournal.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            tableauJournal.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            tableauJournal.Margin = new Thickness(10, 5, 10, 5);
            tableauJournal.Background = Brushes.Transparent;
            tableauJournal.RowBackground = Brushes.Transparent;
            tableauJournal.AlternatingRowBackground = Brushes.Transparent;
            tableauJournal.Foreground = Brushes.White;
            tableauJournal.GridLinesVisibility = DataGridGridLinesVisibility.None;
            tableauJournal.BorderThickness = new Thickness(0);
            tableauJournal.HeadersVisibility = DataGridHeadersVisibility.Column;
            tableauJournal.FontSize = 14;
            tableauJournal.RowHeight = 30;
            tableauJournal.ColumnHeaderHeight = 30;

            Style headerStyle = new Style(typeof(DataGridColumnHeader));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.BackgroundProperty, Brushes.DarkGoldenrod));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.ForegroundProperty, Brushes.White));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.FontWeightProperty, FontWeights.Bold));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.PaddingProperty, new Thickness(5)));
            tableauJournal.ColumnHeaderStyle = headerStyle;

            tableauJournal.Columns.Add(new DataGridTextColumn { Header = "Date & Heure", Binding = new Binding("DateHeure"), Width = 150 });
            tableauJournal.Columns.Add(new DataGridTextColumn { Header = "Utilisateur", Binding = new Binding("Utilisateur"), Width = 120 });
            tableauJournal.Columns.Add(new DataGridTextColumn { Header = "Action", Binding = new Binding("Action"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });

            Grid.SetRow(tableauJournal, 2);
            grdMain.Children.Add(tableauJournal);
        }

        public void ChargerJournal()
        {
            DataTable dataT;
            int nbrRow;

            if (datePicker.SelectedDate == null)
                sql.ToutJournal(out dataT, out nbrRow);
            else
                sql.JournalByDate(datePicker.SelectedDate.Value, out dataT, out nbrRow);

            listeJournal.Clear();
            List<object> lignes = new List<object>();

            for (int i = 0; i < nbrRow; i++)
            {
                listeJournal.Add(dataT.Rows[i]);
                DateTime dateHeure = Convert.ToDateTime(dataT.Rows[i]["date"]);

                lignes.Add(new
                {
                    DateHeure = dateHeure.ToString("dd/MM/yyyy HH:mm:ss"),
                    Utilisateur = dataT.Rows[i]["utilisateur"].ToString(),
                    Action = dataT.Rows[i]["action"].ToString()
                });
            }

            tableauJournal.ItemsSource = null;
            tableauJournal.ItemsSource = lignes;
        }
    }
}
