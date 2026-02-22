using CrispyLo.Classes;
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
    /// Logique d'interaction pour AllCommande.xaml
    /// </summary>
    public partial class AllCommande : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        ControlTemplate radioStyle = VisuelCSS.CreateButtonStyleTemplate();
        BaseDeDonnée sql = new BaseDeDonnée();
        ImpressionTicket impression = new ImpressionTicket();

        DataGrid tableauCommandes = new DataGrid();
        StackPanel panier = new StackPanel();
        RadioButton rdLivraison = new RadioButton();
        RadioButton rdEmporter = new RadioButton();
        DatePicker datePicker = new DatePicker();
        TextBlock prix = new TextBlock();

        public AllCommande()
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

            Filtres();
            Tableau();
            Panier();
            AfficheBas();
            ChargerCommandes();
        }

        public void Filtres()
        {
            WrapPanel panelFiltres = new WrapPanel();
            panelFiltres.VerticalAlignment = VerticalAlignment.Center;
            panelFiltres.Margin = new Thickness(10, 0, 0, 0);
            Grid.SetColumn(panelFiltres, 0);
            Grid.SetRow(panelFiltres, 0);

            rdLivraison.Content = "LIVRAISON";
            rdLivraison.FontWeight = FontWeights.Bold;
            rdLivraison.GroupName = "typeCommande";
            rdLivraison.Template = radioStyle;
            rdLivraison.Margin = new Thickness(5);
            rdLivraison.Width = 130;
            rdLivraison.Height = 40;
            rdLivraison.Checked += Filtre_Changed;

            rdEmporter.Content = "EMPORTER";
            rdEmporter.FontWeight = FontWeights.Bold;
            rdEmporter.GroupName = "typeCommande";
            rdEmporter.IsChecked = true;
            rdEmporter.Template = radioStyle;
            rdEmporter.Margin = new Thickness(5);
            rdEmporter.Width = 130;
            rdEmporter.Height = 40;
            rdEmporter.Checked += Filtre_Changed;

            datePicker.SelectedDate = DateTime.Now;
            datePicker.Width = 130;
            datePicker.Height = 50;
            datePicker.FontSize = 15;
            datePicker.Background = Brushes.Transparent;
            datePicker.BorderThickness = new Thickness(0);
            datePicker.Margin = new Thickness(20, 20, 5, 5);
            datePicker.SelectedDateChanged += DatePicker_Changed;

            panelFiltres.Children.Add(rdEmporter);
            panelFiltres.Children.Add(rdLivraison);
            panelFiltres.Children.Add(datePicker);
            grdMain.Children.Add(panelFiltres);
        }

        public void Tableau()
        {
            tableauCommandes.AutoGenerateColumns = false;
            tableauCommandes.IsReadOnly = true;
            tableauCommandes.SelectionMode = DataGridSelectionMode.Single;
            tableauCommandes.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            tableauCommandes.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            tableauCommandes.Margin = new Thickness(10, 0, 10, 10);
            tableauCommandes.Background = Brushes.Transparent;
            tableauCommandes.RowBackground = Brushes.Transparent;
            tableauCommandes.AlternatingRowBackground = Brushes.Transparent;
            tableauCommandes.Foreground = Brushes.White;
            tableauCommandes.GridLinesVisibility = DataGridGridLinesVisibility.None;
            tableauCommandes.BorderThickness = new Thickness(0);
            tableauCommandes.HeadersVisibility = DataGridHeadersVisibility.Column;
            tableauCommandes.FontSize = 16;
            tableauCommandes.RowHeight = 30;
            tableauCommandes.ColumnHeaderHeight = 30;
            Style headerStyle = new Style(typeof(DataGridColumnHeader));
            headerStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.LightBlue));
            headerStyle.Setters.Add(new Setter(ForegroundProperty, Brushes.Black));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.FontWeightProperty, FontWeights.Bold));
            headerStyle.Setters.Add(new Setter(DataGridColumnHeader.PaddingProperty, new Thickness(5)));
            tableauCommandes.ColumnHeaderStyle = headerStyle;

            tableauCommandes.Columns.Add(new DataGridTextColumn { Header = "N°", Binding = new Binding("Id"), Width = 50 });
            tableauCommandes.Columns.Add(new DataGridTextColumn { Header = "Date", Binding = new Binding("Date"), Width = 100 });
            tableauCommandes.Columns.Add(new DataGridTextColumn { Header = "Heure", Binding = new Binding("Heure"), Width = 70 });
            tableauCommandes.Columns.Add(new DataGridTextColumn { Header = "Prise Commande", Binding = new Binding("PriseCommande"), Width = 140 });
            tableauCommandes.Columns.Add(new DataGridTextColumn { Header = "Client", Binding = new Binding("Client"), Width = 140 });
            tableauCommandes.Columns.Add(new DataGridTextColumn { Header = "Adresse", Binding = new Binding("Adresse"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
            tableauCommandes.Columns.Add(new DataGridTextColumn { Header = "Montant", Binding = new Binding("Montant"), Width = 80 });

            tableauCommandes.SelectionChanged += TableauCommandes_SelectionChanged;

            Grid.SetColumn(tableauCommandes, 0);
            Grid.SetRow(tableauCommandes, 1);
            grdMain.Children.Add(tableauCommandes);
        }

        public void Panier()
        {
            panier.Background = Brushes.White;

            ScrollViewer scrollPanier = new ScrollViewer();
            scrollPanier.VerticalAlignment = VerticalAlignment.Top;
            scrollPanier.Height = 600;
            scrollPanier.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollPanier.Content = panier;
            scrollPanier.Margin = new Thickness(0, 0, 5, 0);
            Grid.SetColumn(scrollPanier, 1);
            Grid.SetRow(scrollPanier, 1);
            grdMain.Children.Add(scrollPanier);
        }

        public void AfficheBas()
        {
            StackPanel panel = new StackPanel();
            panel.Background = Brushes.LightGray;
            panel.VerticalAlignment = VerticalAlignment.Bottom;
            panel.HorizontalAlignment = HorizontalAlignment.Stretch;
            panel.Margin = new Thickness(0, 0, 5, 5);
            Grid.SetColumn(panel, 1);
            Grid.SetRow(panel, 1);

            WrapPanel wrap0 = new WrapPanel();
            wrap0.Background = Brushes.Transparent;

            Button supprimer = new Button();
            supprimer.Visibility = Visibility.Hidden;
            if (PagePrincipale.EstAdmin)
            { supprimer.Visibility = Visibility.Visible; }
            supprimer.Content = "supprimer";
            supprimer.Height = 40;
            supprimer.Width = 100;
            supprimer.FontWeight = FontWeights.Bold;
            supprimer.Margin = new Thickness(20,0,0,10);
            supprimer.Background = Brushes.Red;
            supprimer.Foreground = Brushes.White;
            supprimer.HorizontalAlignment = HorizontalAlignment.Left;
            supprimer.Click += new RoutedEventHandler(Click_Supprimer);
            wrap0.Children.Add(supprimer);

            prix.Height = 60;
            prix.Width = 140;
            prix.Background = Brushes.Transparent;
            prix.FontSize = 40;
            prix.TextAlignment = TextAlignment.Right;
            prix.Margin = new Thickness(40, 0, 15, 10);
            prix.HorizontalAlignment = HorizontalAlignment.Right;
            wrap0.Children.Add(prix);

            panel.Children.Add(wrap0);

            WrapPanel wrap = new WrapPanel();
            wrap.Background = Brushes.Transparent;

            Button imprimer = new Button();
            Button modifier = new Button();

            TextBlock txtImprimer = new TextBlock();
            txtImprimer.Text = "🧾\nimprimer";
            txtImprimer.TextAlignment = TextAlignment.Center;
            imprimer.Content = txtImprimer;
            imprimer.FontWeight = FontWeights.Bold;
            imprimer.Height = 75;
            imprimer.Width = 125;
            imprimer.FontSize = 20;
            imprimer.Margin = new Thickness(30, 5, 5, 5);
            imprimer.HorizontalContentAlignment = HorizontalAlignment.Center;
            imprimer.VerticalContentAlignment = VerticalAlignment.Center;
            imprimer.Background = Brushes.Transparent;
            imprimer.Click += new RoutedEventHandler(Click_Imprimer);

            TextBlock txtModifier = new TextBlock();
            txtModifier.Text = "📝\nmodifier";
            txtModifier.TextAlignment = TextAlignment.Center;
            modifier.Content = txtModifier;
            modifier.FontWeight = FontWeights.Bold;
            modifier.Height = 75;
            modifier.Width = 125;
            modifier.FontSize = 20;
            modifier.Margin = new Thickness(15, 5, 5, 5);
            modifier.HorizontalContentAlignment = HorizontalAlignment.Center;
            modifier.VerticalContentAlignment = VerticalAlignment.Center;
            modifier.Background = Brushes.Transparent;
            modifier.Click += new RoutedEventHandler(Click_Modifier);

            wrap.Children.Add(modifier);
            wrap.Children.Add(imprimer);
            panel.Children.Add(wrap);

            grdMain.Children.Add(panel);
        }
        public void ChargerCommandes()
        {
            if (datePicker.SelectedDate == null) return;

            bool estLivraison = rdLivraison.IsChecked == true;
            DateTime date = datePicker.SelectedDate.Value;

            sql.AllCommandeByDateAndType(date, estLivraison, out DataTable dataT, out int nbrRow);

            List<object> commandes = new List<object>();
            for (int i = 0; i < nbrRow; i++)
            {
                int idStatut = Convert.ToInt32(dataT.Rows[i]["idStatut"]);

                DateTime dateHeure = Convert.ToDateTime(dataT.Rows[i]["dateHeure"]);

                string adresse = "";
                if (dataT.Rows[i]["idClient"] != DBNull.Value)
                {
                    int idClient = Convert.ToInt32(dataT.Rows[i]["idClient"]);
                    sql.ClientById(idClient, out DataTable dataC);
                    if (dataC.Rows.Count > 0)
                    {
                        int idAdresse = Convert.ToInt32(dataC.Rows[0]["idAdresse"]);
                        sql.AdresseByClient(idAdresse, out DataTable dataAdr);
                        if (dataAdr.Rows.Count > 0)
                        {
                            adresse = dataAdr.Rows[0]["nomRue"].ToString() + " " +
                                      dataAdr.Rows[0]["numeroRue"].ToString() + " " +
                                      dataAdr.Rows[0]["localite"].ToString() + " " +
                                      dataAdr.Rows[0]["codePostal"].ToString();
                        }
                    }
                }

                string statut = "";
                {
                    sql.StatutById(idStatut, out DataTable dataStatut);
                    if (dataStatut.Rows.Count > 0)
                    {
                        statut = dataStatut.Rows[0]["libelle"].ToString();
                    }
                }

                commandes.Add(new
                {
                    Id = dataT.Rows[i]["id"],
                    Date = dateHeure.ToString("dd/MM/yyyy"),
                    Heure = dateHeure.ToString("HH:mm"),
                    Client = dataT.Rows[i]["nom"].ToString() + " " + dataT.Rows[i]["prenom"].ToString(),
                    Adresse = adresse,
                    Montant = dataT.Rows[i]["prixTotal"].ToString() + " €",
                    PriseCommande = statut,
                    EstCloture = Convert.ToBoolean(dataT.Rows[i]["estCloture"])
                });
            }

            tableauCommandes.ItemsSource = null;
            tableauCommandes.ItemsSource = commandes;
        }

        private void Filtre_Changed(object sender, RoutedEventArgs e)
        {
            ChargerCommandes();
        }

        private void DatePicker_Changed(object sender, SelectionChangedEventArgs e)
        {
            ChargerCommandes();
        }

        private void TableauCommandes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tableauCommandes.SelectedItem == null) return;

            dynamic commande = tableauCommandes.SelectedItem;
            PagePrincipale.IdCommande = Convert.ToInt32(commande.Id);


            RemplissageInfo();
            AfficherPanier();
        }

        public void RemplissageInfo()
        {
            PagePrincipale.Panier.Clear();

            sql.CommandeById(PagePrincipale.IdCommande, out DataTable dataCom);
            prix.Text = dataCom.Rows[0]["prixTotal"] + " €";

            sql.AllLigneByCommande(PagePrincipale.IdCommande, out DataTable dataLigne, out int nbrLigne);
            for (int ligne = 0; ligne < nbrLigne; ligne++)
            {
                string libelle = "";
                if ((bool)dataLigne.Rows[ligne]["tailleLarge"])
                {
                    libelle = "Maxi ";
                }
                libelle += dataLigne.Rows[ligne]["typePate"].ToString();

                sql.ProduitById((int)dataLigne.Rows[ligne]["idProduit"], out DataTable p);
                libelle += p.Rows[0]["libelle"].ToString();

                if ((int)p.Rows[0]["idTypeProduit"] == 5 || libelle == "Moelleux au chocolat")
                {
                    libelle += " " + p.Rows[0]["quantite"] + "pc";
                }

                PagePrincipale.Panier.Add(new Produit
                {
                    Id = (int)p.Rows[0]["id"],
                    Libelle = libelle.ToUpper(),
                    Prix = (decimal)dataLigne.Rows[ligne]["prixProduit"],
                    EstLarge = (bool)dataLigne.Rows[ligne]["tailleLarge"],
                    TypePate = dataLigne.Rows[ligne]["typePate"].ToString(),
                    IdTypeProduit = (int)p.Rows[0]["idTypeProduit"],
                    Note = dataLigne.Rows[ligne]["note"].ToString(),
                    Ingredients = new List<Ingredient>()
                });

                sql.AllIngredientByLigne((int)dataLigne.Rows[ligne]["id"], out DataTable dataIngredient, out int nbrIngredient);
                for (int i = 0; i < nbrIngredient; i++)
                {
                    string[] tabIng = Traitement.descToIngredient(dataIngredient.Rows[i]["ingredient"].ToString());
                    for (int j = 0; j < tabIng.Length; j++)
                    {
                        int id = 0;
                        decimal prix = 0;
                        if ((bool)dataIngredient.Rows[i]["ingredientEnPlus"])
                        {
                            sql.IngredientByLibelle(tabIng[j].Trim(), out DataTable data);
                            id = (int)data.Rows[0]["id"];
                            if ((bool)dataLigne.Rows[ligne]["tailleLarge"])
                            {
                                prix = (decimal)data.Rows[0]["prixLarge"];
                            }
                            else
                            {
                                prix = (decimal)data.Rows[0]["prixMedium"];
                            }
                        }

                        Ingredient ingredient = new Ingredient();
                        ingredient.Id = id;
                        ingredient.Libelle = tabIng[j];
                        ingredient.Prix = prix;
                        ingredient.EstSupplement = (bool)dataIngredient.Rows[i]["ingredientEnPlus"];
                        PagePrincipale.Panier[ligne].Ingredients.Add(ingredient);
                    }
                }
            }
        }

        public void AfficherPanier()
        {
            panier.Children.Clear();
            for (int i = 0; i < PagePrincipale.Panier.Count; i++)
            {
                Produit p = PagePrincipale.Panier[i];
                Grid grille = new Grid();
                grille.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grille.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                TextBlock textNom = new TextBlock();
                textNom.Text = " " + p.Libelle;
                textNom.FontWeight = FontWeights.Bold;
                textNom.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumn(textNom, 0);
                grille.Children.Add(textNom);

                TextBlock textPrix = new TextBlock();
                textPrix.Text = p.Prix.ToString() + "€ ";
                textPrix.FontWeight = FontWeights.Bold;
                textPrix.VerticalAlignment = VerticalAlignment.Center;
                textPrix.Margin = new Thickness(0, 0, 5, 0);
                Grid.SetColumn(textPrix, 1);
                grille.Children.Add(textPrix);
                panier.Children.Add(grille);

                foreach (Ingredient ing in p.Ingredients)
                {
                    Grid grilleIng = new Grid();
                    grilleIng.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    string libelle = "";
                    WrapPanel text = new WrapPanel();

                    if (ing.EstSupplement == false)
                    {
                        libelle += " --> SS ";
                        text.Background = Brushes.Coral;
                    }
                    else
                    {
                        libelle += " --> SUPP ";
                        text.Background = Brushes.LightGreen;
                        grilleIng.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                        TextBlock prixIng = new TextBlock();
                        prixIng.Text = ing.Prix + "€ ";
                        prixIng.FontWeight = FontWeights.Bold;
                        prixIng.VerticalAlignment = VerticalAlignment.Center;
                        prixIng.HorizontalAlignment = HorizontalAlignment.Right;
                        prixIng.Margin = new Thickness(0, 0, 5, 0);
                        Grid.SetColumn(prixIng, 1);
                        grilleIng.Children.Add(prixIng);
                    }
                    libelle += ing.Libelle;

                    TextBlock textIng = new TextBlock();
                    textIng.Text = " " + libelle;
                    textIng.FontWeight = FontWeights.Bold;
                    textIng.VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetColumn(textIng, 0);
                    grilleIng.Children.Add(textIng);

                    grilleIng.Width = panier.ActualWidth - 2;
                    text.Children.Add(grilleIng);
                    text.Height = 20;
                    text.HorizontalAlignment = HorizontalAlignment.Stretch;
                    text.Margin = new Thickness(1);

                    panier.Children.Add(text);
                }

                if (p.Note != "")
                {
                    TextBlock text = new TextBlock();
                    text.Text = PagePrincipale.Panier[i].Note;
                    text.FontWeight = FontWeights.Bold;
                    text.VerticalAlignment = VerticalAlignment.Center;
                    text.HorizontalAlignment = HorizontalAlignment.Stretch;
                    text.Background = Brushes.PeachPuff;
                    text.Margin = new Thickness(1);
                    text.Height = 20;

                    panier.Children.Add(text);
                }
            }
        }

        public void Click_Retour(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Panier.Clear();
            PagePrincipale.IdCommande = -1;
            PagePrincipale.Main.Content = new Home();
        }

        public void Click_Modifier(object sender, RoutedEventArgs e)
        {
            if (tableauCommandes.SelectedItem == null) return;
            dynamic commande = tableauCommandes.SelectedItem;
            if (commande.EstCloture)
            {
                MessageBox.Show("Cette commande est clôturée et ne peut pas être modifiée.", "Commande clôturée", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (PagePrincipale.IdCommande != -1)
            {
                PagePrincipale.Main.Content = new Home();
            }
        }

        public void Click_Supprimer(object sender, RoutedEventArgs e)
        {
            if (tableauCommandes.SelectedItem == null) return;
            dynamic cmd = tableauCommandes.SelectedItem;
            if (cmd.EstCloture)
            {
                MessageBox.Show("Cette commande est clôturée et ne peut pas être supprimée.", "Commande clôturée", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int idCommande = Convert.ToInt32(cmd.Id);

            MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer cette commande ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;

            decimal totalCommande = decimal.Parse(prix.Text.Replace(" €", "").Trim());
            decimal taux = 0.06m;
            decimal ht = Math.Round(totalCommande / (1 + taux), 2);
            decimal tvaVal = Math.Round(totalCommande - ht, 2);

            sql.TicketByCommande(idCommande, out DataTable dataT);
            sql.UpdateTicketidCom((int)dataT.Rows[0]["id"]);
            string signaturePrec = sql.DernierSignature();
            string signature = Traitement.GenererSignature(idCommande, datePicker.SelectedDate.Value, totalCommande, signaturePrec);
            sql.newTicket("annulation", DateTime.Now, -ht, -tvaVal, -totalCommande, signature, signaturePrec, (int)dataT.Rows[0]["id"], -1);

            sql.SupprimerCommande(idCommande);
            sql.InsererJournal("Admin", $"Annulation commande N°{idCommande} - {totalCommande:0.00}€");

            PagePrincipale.Panier.Clear();
            PagePrincipale.IdCommande = -1;
            panier.Children.Clear();
            prix.Text = "";
            ChargerCommandes();
        }

        public void Click_Imprimer(object sender, RoutedEventArgs e)
        {
            if (PagePrincipale.IdCommande != -1)
            {
                    impression.GeneralImpression(PagePrincipale.IdCommande);
            }
        }
    }
}
