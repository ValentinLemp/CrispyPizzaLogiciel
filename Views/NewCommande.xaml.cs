using CrispyLo.Classes;
using CrispyLo.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrispyLo.Views
{
    /// <summary>
    /// Logique d'interaction pour NewCommande.xaml
    /// </summary>
    public partial class NewCommande : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        ControlTemplate radioStyle = VisuelCSS.CreateButtonStyleTemplate();
        ImpressionTicket impression = new ImpressionTicket();
        BaseDeDonnée bd = new BaseDeDonnée();

        public NewCommande()
        {
            InitializeComponent();
            PrepareInitialize();

            if (PagePrincipale.IdCommande != -1)
            {
                RemplirChamps();
            }
        }

        public void RemplirChamps()
        {
            bd.CommandeById(PagePrincipale.IdCommande, out DataTable dataCom);
            if (dataCom.Rows.Count == 0) return;

            Nom.Text = dataCom.Rows[0]["nom"].ToString();
            Prenom.Text = dataCom.Rows[0]["prenom"].ToString();
            Commentaire.Text = dataCom.Rows[0]["commetaire"].ToString();

            DateTime dateHeure = Convert.ToDateTime(dataCom.Rows[0]["dateHeure"]);
            datePicker.SelectedDate = dateHeure.Date;
            Heures.Text = dateHeure.ToString("HH:mm");

            bool estLivraison = Convert.ToBoolean(dataCom.Rows[0]["estLivraison"]);
            if (estLivraison)
            {
                rdLivraison.IsChecked = true;
                rdEmporter.IsChecked = false;
            }
            else
            {
                rdEmporter.IsChecked = true;
                rdLivraison.IsChecked = false;
            }

            if (dataCom.Rows[0]["idClient"] != DBNull.Value)
            {
                int idClient = Convert.ToInt32(dataCom.Rows[0]["idClient"]);
                bd.ClientById(idClient, out DataTable dataClient);
                if (dataClient.Rows.Count > 0)
                {
                    Telephone.Text = dataClient.Rows[0]["numero"].ToString();
                    int idAdresse = Convert.ToInt32(dataClient.Rows[0]["idAdresse"]);
                    bd.AdresseByClient(idAdresse, out DataTable dataAdr);
                    if (dataAdr.Rows.Count > 0)
                    {
                        Adresse.Text = dataAdr.Rows[0]["nomRue"].ToString();
                        Numero.Text = dataAdr.Rows[0]["numeroRue"].ToString();
                        Localite.Text = dataAdr.Rows[0]["codePostal"].ToString();
                        Ville.Text = dataAdr.Rows[0]["localite"].ToString();
                    }
                }
            }
        }
        RadioButton rdLivraison = new RadioButton();
        RadioButton rdEmporter = new RadioButton();

        StackPanel panelGénéralHeure = new StackPanel();
        StackPanel panelGénéralRésumé = new StackPanel();

        StackPanel encodage = new StackPanel();
        ListBox suggestions = new ListBox();
        CancellationTokenSource cts;
        bool selectionEnCours = false;

        TextBox Commentaire = new TextBox();
        TextBox Ville = new TextBox();
        TextBox Localite = new TextBox();
        TextBox Numero = new TextBox();
        TextBox Adresse = new TextBox();
        TextBox Telephone = new TextBox();
        TextBox Nom = new TextBox();
        TextBox Prenom = new TextBox();

        DatePicker datePicker = new DatePicker();
        TextBox Heures = new TextBox();
        DataGrid tableauClients = new DataGrid();
        List<Client> listeClients = new List<Client>();

        public void PrepareInitialize()
        {
            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(76) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            StackPanel panelCouleur = new StackPanel();
            panelCouleur.Background = Brushes.White;
            Grid.SetRow(panelCouleur, 1);
            Grid.SetColumn(panelCouleur, 0);
            grdMain.Children.Add(panelCouleur);

            StackPanel panelC = new StackPanel();
            panelC.Background = Brushes.DarkGray;
            Grid.SetRow(panelC, 0);
            Grid.SetColumnSpan(panelC, 2);
            grdMain.Children.Add(panelC);

            StackPanel panelC1 = new StackPanel();
            panelC1.Background = Brushes.WhiteSmoke;
            Grid.SetRow(panelC1, 1);
            Grid.SetColumn(panelC1, 1);
            grdMain.Children.Add(panelC1);

            StackPanel panelC2 = new StackPanel();
            panelC2.Background = Brushes.Gainsboro;
            Grid.SetRow(panelC2, 2);
            Grid.SetColumnSpan(panelC2, 2);
            grdMain.Children.Add(panelC2);

            WrapPanel typeCommande = new WrapPanel();
            Grid.SetColumn(typeCommande, 0);
            Grid.SetRow(typeCommande, 0);

            encodage.Height = 260;
            encodage.VerticalAlignment = VerticalAlignment.Bottom;
            encodage.HorizontalAlignment = HorizontalAlignment.Stretch;
            Grid.SetColumn(encodage, 0);
            Grid.SetRow(encodage, 1);
            encodage.Background = Brushes.Transparent;

            rdEmporter.Content = "EMPORTER";
            rdEmporter.FontWeight = FontWeights.Bold;
            rdEmporter.GroupName = "typeCommande";
            rdEmporter.IsChecked = true;
            rdEmporter.Template = radioStyle;
            rdEmporter.Margin = new Thickness(100,10,10,10);
            rdEmporter.Width = 130;
            rdEmporter.Height = 50;

            rdLivraison.Content = "LIVRAISON";
            rdLivraison.FontWeight = FontWeights.Bold;
            rdLivraison.GroupName = "typeCommande";
            rdLivraison.Template = radioStyle;
            rdLivraison.Margin = new Thickness(10);
            rdLivraison.Width = 130;
            rdLivraison.Height = 50;

            typeCommande.Children.Add(rdEmporter);
            typeCommande.Children.Add(rdLivraison);

            WrapPanel panel = new WrapPanel();
            TextBlock nom = new TextBlock();

            nom.Text = "Nom :";
            nom.Width = 100;
            nom.Height = 25;
            nom.Margin = new Thickness(0, 0, 20, 0);
            nom.FontSize = 15;
            Nom.Width = 200;
            Nom.Height = 25;
            Nom.FontSize = 15;
            panel.Margin = new Thickness(20, 30, 0, 0);
            panel.Children.Add(nom);
            panel.Children.Add(Nom);
            Grid.SetColumn(panel, 0);
            Grid.SetRow(panel, 1);
            grdMain.Children.Add(panel);

            WrapPanel panel1 = new WrapPanel();
            TextBlock prenom = new TextBlock();

            prenom.Text = "Prénom :";
            prenom.Width = 100;
            prenom.Height = 25;
            prenom.Margin = new Thickness(0, 0, 20, 0);
            prenom.FontSize = 15;
            Prenom.Width = 200;
            Prenom.Height = 25;
            Prenom.FontSize = 15;
            panel1.Margin = new Thickness(20, 65, 0, 0);
            panel1.Children.Add(prenom);
            panel1.Children.Add(Prenom);
            Grid.SetColumn(panel1, 0);
            Grid.SetRow(panel1, 1);
            grdMain.Children.Add(panel1);

            WrapPanel panel2 = new WrapPanel();
            TextBlock telephone = new TextBlock();

            telephone.Text = "téléphone :";
            telephone.Width = 100;
            telephone.Height = 25;
            telephone.Margin = new Thickness(0, 0, 20, 0);
            telephone.FontSize = 15;
            Telephone.Width = 200;
            Telephone.Height = 25;
            Telephone.FontSize = 15;
            Telephone.TextChanged += Telephone_TextChanged;
            panel2.Margin = new Thickness(20, 15, 0, 0);
            panel2.Children.Add(telephone);
            panel2.Children.Add(Telephone);
            encodage.Children.Add(panel2);

            WrapPanel panel3 = new WrapPanel();
            TextBlock adresse = new TextBlock();

            adresse.Text = "Rue :";
            adresse.Width = 100;
            adresse.Height = 25;
            adresse.Margin = new Thickness(0, 0, 20, 0);
            adresse.FontSize = 15;
            Adresse.Width = 350;
            Adresse.Height = 25;
            Adresse.FontSize = 15;
            Adresse.Margin = new Thickness(0, 0, 30, 0);
            Adresse.TextChanged += Adresse_TextChanged;
            Adresse.KeyDown += Adresse_KeyDown;
            panel3.Margin = new Thickness(20, 15, 0, 0);
            panel3.Children.Add(adresse);
            panel3.Children.Add(Adresse);

            suggestions.Width = 350;
            suggestions.MaxHeight = 120;
            suggestions.Visibility = Visibility.Collapsed;
            suggestions.Margin = new Thickness(120, 0, 0, 0);
            suggestions.HorizontalAlignment = HorizontalAlignment.Left;
            suggestions.MouseDoubleClick += Suggestions_Click;

            TextBlock numero = new TextBlock();

            numero.Text = "Numéro :";
            numero.Width = 75;
            numero.Height = 25;
            numero.FontSize = 15;
            Numero.Width = 50;
            Numero.Height = 25;
            Numero.FontSize = 15;
            panel3.Children.Add(numero);
            panel3.Children.Add(Numero);
            encodage.Children.Add(panel3);
            encodage.Children.Add(suggestions);

            WrapPanel panel4 = new WrapPanel();
            TextBlock localite = new TextBlock();

            localite.Text = "Localité :";
            localite.Width = 100;
            localite.Height = 25;
            localite.Margin = new Thickness(0, 0, 20, 0);
            localite.FontSize = 15;
            Localite.Width = 200;
            Localite.Height = 25;
            Localite.FontSize = 15;
            Localite.Margin = new Thickness(0, 0, 55, 0);
            panel4.Margin = new Thickness(20, 15, 0, 0);
            panel4.Children.Add(localite);
            panel4.Children.Add(Localite);

            TextBlock ville = new TextBlock();

            ville.Text = "Ville :";
            ville.Width = 50;
            ville.Height = 25;
            ville.FontSize = 15;
            Ville.Width = 200;
            Ville.Height = 25;
            Ville.FontSize = 15;
            panel4.Children.Add(ville);
            panel4.Children.Add(Ville);
            encodage.Children.Add(panel4);

            StackPanel panel5 = new StackPanel();
            TextBlock commentaire = new TextBlock();

            commentaire.Text = "commentaire :";
            commentaire.Width = 100;
            commentaire.Height = 25;
            commentaire.FontSize = 15;
            commentaire.HorizontalAlignment = HorizontalAlignment.Left;
            commentaire.Margin = new Thickness(0, 10, 0, 0);
            Commentaire.Width = 500;
            Commentaire.Height = 80;
            Commentaire.FontSize = 15;
            Commentaire.Margin = new Thickness(0, 0, 0, 10);
            panel5.Children.Add(commentaire);
            panel5.Children.Add(Commentaire);
            panel5.VerticalAlignment = VerticalAlignment.Bottom;
            panel5.HorizontalAlignment = HorizontalAlignment.Left;
            panel5.Margin = new Thickness(20, 0, 0, 0);
            Grid.SetColumn(panel5, 0);
            Grid.SetRow(panel5, 1);
            encodage.Children.Add(panel5);

            grdMain.Children.Add(typeCommande);
            grdMain.Children.Add(encodage);

            Horloge();
            Heure();
            grdMain.Children.Add(panelGénéralHeure);
            Resume();
            TableauClient();

            Button retour = new Button();
            retour.Content = "🔙";
            retour.FontWeight = FontWeights.Bold;
            retour.FontSize = 20;
            retour.Foreground = Brushes.White;
            retour.Background = Brushes.Red;
            retour.Width = 35;
            retour.Height = 35;
            retour.Margin = new Thickness(17);
            retour.HorizontalAlignment = HorizontalAlignment.Right;
            retour.VerticalAlignment = VerticalAlignment.Top;
            retour.Click += new RoutedEventHandler(Click_Retour);
            Grid.SetColumn(retour, 1);
            Grid.SetRow(retour, 0);
            grdMain.Children.Add(retour);

            Button sauvegarder = new Button();
            sauvegarder.Content = "💾";
            sauvegarder.FontWeight = FontWeights.Bold;
            sauvegarder.FontSize = 20;
            sauvegarder.Foreground = Brushes.White;
            sauvegarder.Background = Brushes.Green;
            sauvegarder.Width = 35;
            sauvegarder.Height = 35;
            sauvegarder.Margin = new Thickness(50, 10, 10, 10);
            sauvegarder.HorizontalAlignment = HorizontalAlignment.Right;
            sauvegarder.VerticalAlignment = VerticalAlignment.Top;
            sauvegarder.Click += new RoutedEventHandler(Click_Sauvegarder);
            Grid.SetColumn(sauvegarder, 0);
            Grid.SetRow(sauvegarder, 1);
            grdMain.Children.Add(sauvegarder);
        }

        public void Horloge()
        {
            panelGénéralHeure.HorizontalAlignment = HorizontalAlignment.Left;
            panelGénéralHeure.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetColumn(panelGénéralHeure, 1);
            Grid.SetRow(panelGénéralHeure, 1);

            WrapPanel wrap = new WrapPanel();
            TextBlock date = new TextBlock();

            date.Text = "Date prévu : ";
            date.Width = 125;
            date.Height = 25;
            date.FontWeight = FontWeights.Bold;
            date.FontSize = 20;
            datePicker.SelectedDate = DateTime.Now;
            datePicker.Width = 115;
            datePicker.Height = 25;
            wrap.Margin = new Thickness(20,20,20,10);
            wrap.Children.Add(date);
            wrap.Children.Add(datePicker);
            panelGénéralHeure.Children.Add(wrap);
        }

        public void Heure()
        {
            WrapPanel wrap = new WrapPanel();
            TextBlock heure = new TextBlock();

            heure.Text = "Heure prévue :";
            heure.Width = 140;
            heure.Height = 25;
            heure.FontWeight = FontWeights.Bold;
            heure.FontSize = 20;
            heure.Margin = new Thickness(0, 0, 10, 0);
            Heures.Text = DateTime.Now.ToString("HH:mm");
            Heures.Width = 115;
            Heures.Height = 25;
            Heures.FontSize = 20;
            Heures.FontWeight= FontWeights.Bold;
            Heures.BorderThickness = new Thickness(0);
            Heures.Background = Brushes.Transparent;
            wrap.HorizontalAlignment = HorizontalAlignment.Right;
            wrap.VerticalAlignment = VerticalAlignment.Top;
            wrap.Margin = new Thickness(20,20,20,10);
            wrap.Children.Add(heure);
            wrap.Children.Add(Heures);
            panelGénéralHeure.Children.Add(wrap);

            StackPanel panel = new StackPanel();
            panel.HorizontalAlignment = HorizontalAlignment.Right;
            panel.Margin = new Thickness(0,0,80,0);

            WrapPanel ligne = new WrapPanel();
            Button btn = new Button();
            btn.Content = "11";
            btn.FontWeight = FontWeights.Bold;
            btn.Height = 60;
            btn.Width = 60;
            btn.Margin = new Thickness(3);
            btn.Click += new RoutedEventHandler(Click_Heure);
            ligne.Children.Add(btn);

            Button btn1 = new Button();
            btn1.Content = "12";
            btn1.FontWeight = FontWeights.Bold;
            btn1.Height = 60;
            btn1.Width = 60;
            btn1.Margin = new Thickness(3,3,15,3);
            btn1.Click += new RoutedEventHandler(Click_Heure);
            ligne.Children.Add(btn1);

            Button btn2 = new Button();
            btn2.Content = "00";
            btn2.FontWeight = FontWeights.Bold;
            btn2.Height = 60;
            btn2.Width = 60;
            btn2.Margin = new Thickness(3);
            btn2.Foreground = Brushes.Magenta;
            btn2.Click += new RoutedEventHandler(Click_Minute);
            ligne.Children.Add(btn2);

            panel.Children.Add(ligne);

            WrapPanel ligne1 = new WrapPanel();
            Button btn3 = new Button();
            btn3.Content = "13";
            btn3.FontWeight = FontWeights.Bold;
            btn3.Height = 60;
            btn3.Width = 60;
            btn3.Margin = new Thickness(3);
            btn3.Click += new RoutedEventHandler(Click_Heure);
            ligne1.Children.Add(btn3);

            Button btn4 = new Button();
            btn4.Content = "14";
            btn4.FontWeight = FontWeights.Bold;
            btn4.Height = 60;
            btn4.Width = 60;
            btn4.Margin = new Thickness(3, 3, 15, 3);
            btn4.Click += new RoutedEventHandler(Click_Heure);
            ligne1.Children.Add(btn4);

            Button btn5 = new Button();
            btn5.Content = "15";
            btn5.FontWeight = FontWeights.Bold;
            btn5.Height = 60;
            btn5.Width = 60;
            btn5.Margin = new Thickness(3);
            btn5.Foreground = Brushes.Magenta;
            btn5.Click += new RoutedEventHandler(Click_Minute);
            ligne1.Children.Add(btn5);

            panel.Children.Add(ligne1);

            WrapPanel ligne2 = new WrapPanel();
            Button btn6 = new Button();
            btn6.Content = "18";
            btn6.FontWeight = FontWeights.Bold;
            btn6.Height = 60;
            btn6.Width = 60;
            btn6.Margin = new Thickness(3);
            btn6.Click += new RoutedEventHandler(Click_Heure);
            ligne2.Children.Add(btn6);

            Button btn7 = new Button();
            btn7.Content = "19";
            btn7.FontWeight = FontWeights.Bold;
            btn7.Height = 60;
            btn7.Width = 60;
            btn7.Margin = new Thickness(3, 3, 15, 3);
            btn7.Click += new RoutedEventHandler(Click_Heure);
            ligne2.Children.Add(btn7);

            Button btn8 = new Button();
            btn8.Content = "30";
            btn8.FontWeight = FontWeights.Bold;
            btn8.Height = 60;
            btn8.Width = 60;
            btn8.Margin = new Thickness(3);
            btn8.Foreground = Brushes.Magenta;
            btn8.Click += new RoutedEventHandler(Click_Minute);
            ligne2.Children.Add(btn8);

            panel.Children.Add(ligne2);

            WrapPanel ligne3 = new WrapPanel();
            Button btn9 = new Button();
            btn9.Content = "20";
            btn9.FontWeight = FontWeights.Bold;
            btn9.Height = 60;
            btn9.Width = 60;
            btn9.Margin = new Thickness(3);
            btn9.Click += new RoutedEventHandler(Click_Heure);
            ligne3.Children.Add(btn9);

            Button btn10 = new Button();
            btn10.Content = "21";
            btn10.FontWeight = FontWeights.Bold;
            btn10.Height = 60;
            btn10.Width = 60;
            btn10.Margin = new Thickness(3, 3, 15, 3);
            btn10.Click += new RoutedEventHandler(Click_Heure);
            ligne3.Children.Add(btn10);

            Button btn11 = new Button();
            btn11.Content = "45";
            btn11.FontWeight = FontWeights.Bold;
            btn11.Height = 60;
            btn11.Width = 60;
            btn11.Margin = new Thickness(3,3,3,3);
            btn11.Foreground = Brushes.Magenta;
            btn11.Click += new RoutedEventHandler(Click_Minute);
            ligne3.Children.Add(btn11);

            panel.Children.Add(ligne3);

            panelGénéralHeure.Margin = new Thickness(0,0,0,10);
            panelGénéralHeure.Children.Add(panel);
        }

        public void Resume()
        {
            panelGénéralRésumé.Margin = new Thickness(0,20,200,0);
            panelGénéralRésumé.HorizontalAlignment = HorizontalAlignment.Right;
            panelGénéralRésumé.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetColumn(panelGénéralRésumé, 1);
            Grid.SetRow(panelGénéralRésumé, 1);

            TextBlock titre = new TextBlock();
            titre.Text = "Résumé";
            titre.FontWeight = FontWeights.Black;
            titre.FontSize = 20;
            titre.Background = Brushes.Transparent;
            panelGénéralRésumé.Children.Add(titre);
            Traitement.CalculRésumé(out string[] result);
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] != "")
                {
                    TextBlock phrase = new TextBlock();
                    phrase.Text = result[i];
                    phrase.FontWeight = FontWeights.Bold;
                    phrase.FontSize = 15;
                    panelGénéralRésumé.Children.Add(phrase);
                }
            }
            TextBlock montant = new TextBlock();
            montant.Text = Traitement.prixTotal().ToString() + " €";
            montant.FontSize = 15;
            montant.FontWeight = FontWeights.Bold;
            panelGénéralRésumé.Children.Add(montant);
            grdMain.Children.Add(panelGénéralRésumé);
        }
        private async void Adresse_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (selectionEnCours) return;

            if (cts != null) cts.Cancel();
            cts = new CancellationTokenSource();
            var token = cts.Token;

            string texte = Adresse.Text;
            if (texte.Length < 3)
            {
                suggestions.Visibility = Visibility.Collapsed;
                return;
            }

            try
            {
                await Task.Delay(400, token);
                if (token.IsCancellationRequested) return;

                List<Adresse> resultats = await Traitement.RechercherAdresse(texte);
                if (token.IsCancellationRequested) return;

                suggestions.Items.Clear();
                foreach (Adresse adr in resultats)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = adr.Affichage;
                    item.Tag = adr;
                    suggestions.Items.Add(item);
                }
                suggestions.Visibility = resultats.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (TaskCanceledException) { }
        }

        private void Adresse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && suggestions.Visibility == Visibility.Visible && suggestions.Items.Count > 0)
            {
                if (suggestions.SelectedIndex == -1)
                    suggestions.SelectedIndex = 0;
                SelectionnerAdresse((ListBoxItem)suggestions.SelectedItem);
                e.Handled = true;
            }
            else if (e.Key == Key.Down && suggestions.Visibility == Visibility.Visible)
            {
                suggestions.Focus();
                if (suggestions.Items.Count > 0)
                    suggestions.SelectedIndex = 0;
                e.Handled = true;
            }
        }

        private void Suggestions_Click(object sender, MouseButtonEventArgs e)
        {
            if (suggestions.SelectedItem != null)
                SelectionnerAdresse((ListBoxItem)suggestions.SelectedItem);
        }

        private void SelectionnerAdresse(ListBoxItem item)
        {
            Adresse adr = (Adresse)item.Tag;
            selectionEnCours = true;
            Adresse.Text = adr.Rue;
            Numero.Text = adr.Numero;
            Localite.Text = adr.CodePostal;
            Ville.Text = adr.Ville;
            selectionEnCours = false;
            suggestions.Visibility = Visibility.Collapsed;
        }

        public void Click_Retour(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new Home();
        }

        public void Click_Minute(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            DateTime heure = DateTime.Parse(Heures.Text);
            Heures.Text = heure.ToString("HH") + ":" + btn.Content.ToString();
        }
        public void Click_Heure(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            DateTime heure = DateTime.Parse(Heures.Text);
            Heures.Text = btn.Content.ToString() + ":" + heure.ToString("mm");
        }

        public void TableauClient()
        {
            StackPanel panelTableau = new StackPanel();
            panelTableau.Margin = new Thickness(20, 15, 20, 10);
            Grid.SetRow(panelTableau, 2);
            Grid.SetColumn(panelTableau, 0);
            Grid.SetColumnSpan(panelTableau, 2);

            tableauClients.AutoGenerateColumns = false;
            tableauClients.IsReadOnly = true;
            tableauClients.SelectionMode = DataGridSelectionMode.Single;
            tableauClients.MaxHeight = 200;
            tableauClients.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            tableauClients.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            tableauClients.Columns.Add(new DataGridTextColumn { Header = "Nom", Binding = new Binding("Nom"), Width = 120 });
            tableauClients.Columns.Add(new DataGridTextColumn { Header = "Prénom", Binding = new Binding("Prenom"), Width = 120 });
            tableauClients.Columns.Add(new DataGridTextColumn { Header = "Téléphone", Binding = new Binding("telephone"), Width = 130 });
            tableauClients.Columns.Add(new DataGridTextColumn { Header = "Rue", Binding = new Binding("Adresse.Rue"), Width = 200 });
            tableauClients.Columns.Add(new DataGridTextColumn { Header = "N°", Binding = new Binding("Adresse.Numero"), Width = 60 });
            tableauClients.Columns.Add(new DataGridTextColumn { Header = "CP", Binding = new Binding("Adresse.CodePostal"), Width = 70 });
            tableauClients.Columns.Add(new DataGridTextColumn { Header = "Localité", Binding = new Binding("Adresse.Ville"), Width = 120 });

            panelTableau.Children.Add(tableauClients);

            WrapPanel panelBoutons = new WrapPanel();
            panelBoutons.Margin = new Thickness(0, 10, 0, 0);

            Button btnSelectionner = new Button();
            btnSelectionner.Content = "Sélectionner";
            btnSelectionner.Width = 120;
            btnSelectionner.Height = 35;
            btnSelectionner.Margin = new Thickness(0, 0, 10, 0);
            btnSelectionner.FontWeight = FontWeights.Bold;
            btnSelectionner.Click += new RoutedEventHandler(Click_Selectionner);
            panelBoutons.Children.Add(btnSelectionner);

            Button btnSupprimer = new Button();
            btnSupprimer.Content = "Supprimer";
            btnSupprimer.Width = 120;
            btnSupprimer.Height = 35;
            btnSupprimer.FontWeight = FontWeights.Bold;
            btnSupprimer.Background = Brushes.Red;
            btnSupprimer.Foreground = Brushes.White;
            btnSupprimer.Click += new RoutedEventHandler(Click_Supprimer);
            panelBoutons.Children.Add(btnSupprimer);

            panelTableau.Children.Add(panelBoutons);
            grdMain.Children.Add(panelTableau);
        }

        private void Telephone_TextChanged(object sender, TextChangedEventArgs e)
        {
            string phone = Telephone.Text;
            if (phone.Length < 3)
            {
                listeClients.Clear();
                tableauClients.ItemsSource = null;
                return;
            }

            bd.AllClientByTelephone(phone, out DataTable dataT, out int nbrRow);

            listeClients.Clear();
            for (int i = 0; i < nbrRow; i++)
            {
                int idAdresse = Convert.ToInt32(dataT.Rows[i]["idAdresse"]);
                bd.AdresseByClient(idAdresse, out DataTable dataAdr);

                Client client = new Client();
                client.Id = Convert.ToInt32(dataT.Rows[i]["id"]);
                client.Nom = dataT.Rows[i]["nom"].ToString();
                client.Prenom = dataT.Rows[i]["prenom"].ToString();
                client.telephone = dataT.Rows[i]["numero"].ToString();
                client.Adresse = new Adresse();
                if (dataAdr.Rows.Count > 0)
                {
                    client.Adresse.Rue = dataAdr.Rows[0]["nomRue"].ToString();
                    client.Adresse.Numero = dataAdr.Rows[0]["numeroRue"].ToString();
                    client.Adresse.CodePostal = dataAdr.Rows[0]["codePostal"].ToString();
                    client.Adresse.Ville = dataAdr.Rows[0]["localite"].ToString();
                }
                listeClients.Add(client);
            }
            tableauClients.ItemsSource = null;
            tableauClients.ItemsSource = listeClients;
        }

        private void Click_Selectionner(object sender, RoutedEventArgs e)
        {
            if (tableauClients.SelectedItem == null) return;

            Client client = (Client)tableauClients.SelectedItem;
            selectionEnCours = true;
            Nom.Text = client.Nom;
            Prenom.Text = client.Prenom;
            Telephone.Text = client.telephone;
            if (client.Adresse != null)
            {
                Adresse.Text = client.Adresse.Rue;
                Numero.Text = client.Adresse.Numero;
                Localite.Text = client.Adresse.CodePostal;
                Ville.Text = client.Adresse.Ville;
            }
            selectionEnCours = false;
        }

        private void Click_Supprimer(object sender, RoutedEventArgs e)
        {
            if (tableauClients.SelectedItem == null) return;

            Client client = (Client)tableauClients.SelectedItem;
            bd.SupprimerClient(client.Id);

            listeClients.Remove(client);
            tableauClients.ItemsSource = null;
            tableauClients.ItemsSource = listeClients;
        }

        private void Click_Sauvegarder(object sender, RoutedEventArgs e)
        {
            if (PagePrincipale.Panier.Count != 0)
            {
                if (!TimeSpan.TryParseExact(Heures.Text, "hh\\:mm", null, out _))
                {
                    MessageBox.Show("Format heure pas bonne (HH:MM) !");
                    return;
                }
                bool livraison = (bool)rdLivraison.IsChecked;
                double prixTotal = Traitement.prixTotal();
                //int idRest = PagePrincipale.Restaurent;

                string nom = Nom.Text.Trim();
                string prenom = Prenom.Text.Trim() == "" ? "" : Prenom.Text.Trim();
                string telephone = Telephone.Text.Trim();
                string rue = Adresse.Text.Trim();
                string numero = Numero.Text.Trim();
                string codePostal = Localite.Text.Trim();
                string ville = Ville.Text.Trim();
                string commentaire = Commentaire.Text.Trim();
                DateTime date = datePicker.SelectedDate.Value.Date + TimeSpan.Parse(Heures.Text);

                if (PagePrincipale.IdCommande != -1)
                {
                    // ---- MODE MODIFICATION ----
                    int idCom = PagePrincipale.IdCommande;
                    bd.CommandeById(idCom, out DataTable dataCom);
                    int? ancienIdClient = dataCom.Rows[0]["idClient"] != DBNull.Value ? Convert.ToInt32(dataCom.Rows[0]["idClient"]) : (int?)null;

                    int? idClient = null;

                    if (livraison)
                    {
                        if (nom == "" || telephone == "" || rue == "" || numero == "" || codePostal == "" || ville == "")
                        {
                            MessageBox.Show("Veuillez remplir tous les champs obligatoires (Nom, Téléphone, Rue, Numéro, Localité, Ville).");
                            return;
                        }

                        if (ancienIdClient.HasValue)
                        {
                            // Client existait déjà → update client + adresse
                            bd.ClientById(ancienIdClient.Value, out DataTable dataClient);
                            int idAdresse = Convert.ToInt32(dataClient.Rows[0]["idAdresse"]);
                            bd.UpdateClient(ancienIdClient.Value, nom, prenom, telephone);
                            bd.UpdateAdresse(idAdresse, double.Parse(codePostal), rue, numero, ville);
                            idClient = ancienIdClient.Value;
                        }
                        else
                        {
                            // Passage emporter → livraison → créer client + adresse
                            int idAdresse = bd.AjouterAdresse(double.Parse(codePostal), rue, numero, ville);
                            idClient = bd.AjouterClient(nom, prenom, telephone, idAdresse);
                        }
                    }
                    else
                    {
                        if (nom == "")
                        {
                            MessageBox.Show("Veuillez remplir le champ nom");
                            return;
                        }
                        // Emporter → idClient = null (on ne supprime pas l'ancien client)
                        idClient = null;
                    }

                    bd.UpdateCommande(idCom, nom, prenom, livraison, date, prixTotal, commentaire, 1, idClient);

                    // Récupérer les anciennes lignes
                    bd.AllLigneByCommande(idCom, out DataTable dataLignes, out int nbrLignes);
                    List<int> anciensIdLignes = new List<int>();
                    for (int i = 0; i < nbrLignes; i++)
                    {
                        anciensIdLignes.Add(Convert.ToInt32(dataLignes.Rows[i]["id"]));
                    }

                    // Update les lignes existantes, ajouter les nouvelles
                    for (int i = 0; i < PagePrincipale.Panier.Count; i++)
                    {
                        if (i < anciensIdLignes.Count)
                        {
                            // Ligne existante → update + recréer ingrédients
                            Produit p = PagePrincipale.Panier[i];
                            bd.UpdateLigne(anciensIdLignes[i], p.Prix, p.EstLarge, p.TypePate, p.Note, p.Id);
                            bd.SupprimerIngredientsByLigne(anciensIdLignes[i]);
                            Traitement.ModificationLigneIngredient(anciensIdLignes[i], p);
                        }
                        else
                        {
                            // Nouvelle ligne → insert
                            Produit p = PagePrincipale.Panier[i];
                            int idLigne = bd.NewLigne(p.Prix, p.EstLarge, p.TypePate, p.Note, p.Id, idCom);
                            Traitement.ModificationLigneIngredient(idLigne, p);
                        }
                    }

                    // Supprimer les lignes en trop
                    for (int i = PagePrincipale.Panier.Count; i < anciensIdLignes.Count; i++)
                    {
                        bd.SupprimerLigne(anciensIdLignes[i]);
                    }
                    impression.GeneralImpression(PagePrincipale.IdCommande);
                    MessageBox.Show("Commande modifiée avec succès !");
                    PagePrincipale.IdCommande = -1;
                    PagePrincipale.Panier.Clear();
                    PagePrincipale.Main.Content = new Home();
                }
                else
                {
                    // ---- MODE CRÉATION ----
                    int newIdCommande = -1;
                    if (rdLivraison.IsChecked == true)
                    {
                        if (nom == "" || telephone == "" || rue == "" || numero == "" || codePostal == "" || ville == "")
                        {
                            MessageBox.Show("Veuillez remplir tous les champs obligatoires (Nom, Téléphone, Rue, Numéro, Localité, Ville).");
                            return;
                        }

                        int idAdresse = bd.AjouterAdresse(double.Parse(codePostal), rue, numero, ville);
                        int idClient = bd.AjouterClient(nom, prenom, telephone, idAdresse);
                        newIdCommande = bd.NewCommande(nom, prenom, livraison, date, prixTotal, commentaire, 1, idClient);
                        Traitement.CreationLigne(newIdCommande);
                    }
                    else if (rdLivraison.IsChecked == false)
                    {
                        if (nom == "")
                        {
                            MessageBox.Show("Veuillez remplir le champ nom");
                            return;
                        }

                        newIdCommande = bd.NewCommande(nom, prenom, livraison, date, prixTotal, commentaire, 1, null);
                        Traitement.CreationLigne(newIdCommande);
                    }
                    impression.GeneralImpression(newIdCommande);
                    MessageBox.Show("Commade sauvegarder avec succès !");
                    PagePrincipale.Panier.Clear();
                    PagePrincipale.Main.Content = new Home();
                }

            }
            else
            {
                MessageBox.Show("Le panier est vide !");
            }
        }
    }
}
