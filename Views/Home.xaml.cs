using CrispyLo.Modules;
using CrispyLo.Views.Info;
using Org.BouncyCastle.Asn1.Pkcs;
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
//------
using System.Data;
using CrispyLo.Classes;


namespace CrispyLo.Views
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        //---------------------
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        BaseDeDonnée sql = new BaseDeDonnée();
        //---------------------
        public Home()
        {
            InitializeComponent();
            PrepareInitialize();
        }

        Dictionary<string, WrapPanel> catalogueWrap = new Dictionary<string, WrapPanel>();
        WrapPanel pizza = new WrapPanel();
        WrapPanel pate = new WrapPanel();
        WrapPanel ingrédient = new WrapPanel();

        RadioButton radioTailleLarge = new RadioButton();
        RadioButton allPizza = new RadioButton();
        RadioButton traditionellePizza = new RadioButton();
        RadioButton spécialPizza = new RadioButton();
        RadioButton blanchePizza = new RadioButton();
        RadioButton merPizza = new RadioButton();
        RadioButton radioPateTagliatelle = new RadioButton();
        RadioButton radioPateTortellini = new RadioButton();
        RadioButton radioPateRigatoni = new RadioButton();
        RadioButton radioPateSpaghetti = new RadioButton();
        RadioButton radioPateCanneloni = new RadioButton();
        RadioButton suggestionLarge = new RadioButton();
        RadioButton suggestionRigatoni = new RadioButton();
        RadioButton suggestionSpaghetti = new RadioButton();
        RadioButton suggestionTagliatelle = new RadioButton();

        public void PrepareInitialize()
        {
            this.Background = Brushes.DimGray;
            //Grille Colonne
            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(220) });
            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(320) });

            //Grille Ligne
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(124) });

            //------------------------------------------Création Vue Gauche------------------------------------------ 
            StackPanel selectionAffichage = new StackPanel();

            Grid.SetColumn(selectionAffichage, 0);
            Grid.SetRow(selectionAffichage, 1);
            Grid.SetRowSpan(selectionAffichage, 4);

            //Bouton catalogue
            sql.AllTypeProduit(out DataTable data, out int nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btnCatalogue = new Button();
                btnCatalogue.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                btnCatalogue.Height = 70;
                btnCatalogue.Width = 210;
                btnCatalogue.HorizontalAlignment = HorizontalAlignment.Left;
                btnCatalogue.FontWeight = FontWeights.Bold;
                btnCatalogue.FontSize = 20;
                btnCatalogue.Background = Brushes.LimeGreen;
                btnCatalogue.Foreground = Brushes.White;
                btnCatalogue.Click += new RoutedEventHandler(Click_catalogue);
                if (data.Rows[i]["libelle"].ToString() == "pizza")
                {
                    btnCatalogue.Margin = new Thickness(0, 1, 0, 0);
                }

                    selectionAffichage.Children.Add(btnCatalogue);
            }

            Button btnIngrédient = new Button();
            btnIngrédient.Content = "INGREDIENT";
            btnIngrédient.Height = 70;
            btnIngrédient.Width = 210;
            btnIngrédient.HorizontalAlignment = HorizontalAlignment.Left;
            btnIngrédient.FontWeight = FontWeights.Bold;
            btnIngrédient.FontSize = 20;
            btnIngrédient.Background = Brushes.LimeGreen;
            btnIngrédient.Foreground = Brushes.White;
            btnIngrédient.Click += new RoutedEventHandler(Click_catalogue);

            selectionAffichage.Children.Add(btnIngrédient);
            grdMain.Children.Add(selectionAffichage);


            Button btnDéconnexion = new Button();
            btnDéconnexion.Content = "X";
            btnDéconnexion.Background = Brushes.Red;
            btnDéconnexion.FontWeight = FontWeights.Bold;
            btnDéconnexion.Click += new RoutedEventHandler(Click_Exit);
            btnDéconnexion.Height = 40;
            btnDéconnexion.Width = 40;
            btnDéconnexion.VerticalAlignment = VerticalAlignment.Top;
            btnDéconnexion.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(btnDéconnexion, 0);
            Grid.SetRow(btnDéconnexion, 0);
            grdMain.Children.Add(btnDéconnexion);

            //-------------------------------------------------------------------------------------------------------
            //------------------------------------------Création Vue Milieu------------------------------------------ 
            WrapPanel taillePizza = new WrapPanel();
            WrapPanel typePate = new WrapPanel();
            WrapPanel suggestion = new WrapPanel();
            WrapPanel choixSuggestion = new WrapPanel();
            WrapPanel pain = new WrapPanel();
            WrapPanel snacking = new WrapPanel();
            WrapPanel salade = new WrapPanel();
            WrapPanel dipy = new WrapPanel();
            WrapPanel dessert = new WrapPanel();
            WrapPanel boisson = new WrapPanel();
            RadioButton suggestionMedium = new RadioButton();


            RadioButton radioTailleMédium = new RadioButton();

            Grid.SetColumn(pizza, 1);
            Grid.SetRow(pizza, 1);
            Grid.SetRowSpan(pizza, 3);

            Grid.SetColumn(taillePizza, 1); 
            Grid.SetRow(taillePizza, 0);

            pate.Visibility = Visibility.Collapsed;
            Grid.SetColumn(pate, 1);
            Grid.SetRow(pate, 1);
            Grid.SetRowSpan(pate, 3);

            typePate.Visibility = Visibility.Collapsed;
            Grid.SetColumn(typePate, 1);
            Grid.SetRow(typePate, 0);

            suggestion.Visibility = Visibility.Collapsed;
            Grid.SetColumn(suggestion, 1);
            Grid.SetRow(suggestion, 1);
            Grid.SetRowSpan(suggestion, 3);

            pain.Visibility = Visibility.Collapsed;
            Grid.SetColumn(pain, 1);
            Grid.SetRow(pain, 1);
            Grid.SetRowSpan(pain, 3);

            snacking.Visibility = Visibility.Collapsed;
            Grid.SetColumn(snacking, 1);
            Grid.SetRow(snacking, 1);
            Grid.SetRowSpan(snacking, 3);

            salade.Visibility = Visibility.Collapsed;
            Grid.SetColumn(salade, 1);
            Grid.SetRow(salade, 1);
            Grid.SetRowSpan(salade, 3);
            
            dipy.Visibility = Visibility.Collapsed;
            Grid.SetColumn(dipy, 1);
            Grid.SetRow(dipy, 1);
            Grid.SetRowSpan(dipy, 3);

            dessert.Visibility = Visibility.Collapsed;
            Grid.SetColumn(dessert, 1);
            Grid.SetRow(dessert, 1);
            Grid.SetRowSpan(dessert, 3);

            boisson.Visibility = Visibility.Collapsed;
            Grid.SetColumn(boisson, 1);
            Grid.SetRow(boisson, 1);
            Grid.SetRowSpan(boisson, 3);

            ingrédient.Visibility = Visibility.Collapsed;
            Grid.SetColumn(ingrédient, 1);
            Grid.SetRow(ingrédient, 1);
            Grid.SetRowSpan(ingrédient, 3);

            choixSuggestion.Visibility = Visibility.Collapsed;
            Grid.SetColumn(choixSuggestion, 1);
            Grid.SetRow(choixSuggestion, 0);


            //All Bouton salade
            sql.AllProduitByTypeProduit(3, out data, out nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btn = new Button();
                btn.Content = RemplisageBouton(false, data.Rows[i]["libelle"].ToString(), (double)data.Rows[i]["prix"]);
                btn.Tag = data.Rows[i]["id"];
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Gold;
                btn.Margin = new Thickness(2);
                btn.Click += new RoutedEventHandler(Click_Produit);
                salade.Children.Add(btn);
            }
            //All Bouton pain
            sql.AllProduitByTypeProduit(4, out data, out nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btn = new Button();
                btn.Content = RemplisageBouton(false, data.Rows[i]["libelle"].ToString(), (double)data.Rows[i]["prix"]);
                btn.Tag = data.Rows[i]["id"];
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Gold;
                btn.Margin = new Thickness(2);
                btn.Click += new RoutedEventHandler(Click_Produit);
                pain.Children.Add(btn);
            }
            //All Bouton snacking
            sql.AllProduitByTypeProduit(5, out data, out nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                string nom = data.Rows[i]["libelle"].ToString() + "\n" + data.Rows[i]["quantite"].ToString() + "pc";
                Button btn = new Button();
                btn.Content = RemplisageBouton(false, nom, (double)data.Rows[i]["prix"]);
                btn.Tag = data.Rows[i]["id"];
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Gold;
                btn.Margin = new Thickness(2);
                btn.Click += new RoutedEventHandler(Click_Produit);
                snacking.Children.Add(btn);
            }
            //All Bouton dessert
            sql.AllProduitByTypeProduit(6, out data, out nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                string nom;
                Button btn = new Button();
                if (data.Rows[i]["libelle"].ToString() == "Moelleux au chocolat")
                {
                    nom = data.Rows[i]["libelle"].ToString() + "\n" + data.Rows[i]["quantite"].ToString() + " pc";
                }
                else
                {
                    nom = data.Rows[i]["libelle"].ToString();
                }
                btn.Content = RemplisageBouton(false, nom, (double)data.Rows[i]["prix"]);
                btn.Tag = data.Rows[i]["id"];
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Gold;
                btn.Margin = new Thickness(2);
                btn.Click += new RoutedEventHandler(Click_Produit);
                dessert.Children.Add(btn);
            }
            //All Bouton boisson
            sql.AllProduitByTypeProduit(7, out data, out nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btn = new Button();
                string nom = data.Rows[i]["libelle"].ToString() + "\n" + data.Rows[i]["centilitre"].ToString();
                btn.Content = RemplisageBouton(false, nom, (double)data.Rows[i]["prix"]);
                btn.Tag = data.Rows[i]["id"];
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Gold;
                btn.Margin = new Thickness(2);
                btn.Click += new RoutedEventHandler(Click_Produit);
                boisson.Children.Add(btn);
            }
            //All Bouton dipy
            sql.AllProduitByTypeProduit(8, out data, out nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btn = new Button();
                btn.Content = RemplisageBouton(false, data.Rows[i]["libelle"].ToString(), (double)data.Rows[i]["prix"]);
                btn.Tag = data.Rows[i]["id"];
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Gold;
                btn.Margin = new Thickness(2);
                btn.Click += new RoutedEventHandler(Click_Produit);
                dipy.Children.Add(btn);
            }

            ControlTemplate radioStyle = VisuelCSS.CreateButtonStyleTemplate();

            //Pizza catégorie
            allPizza.Content = "ALL";
            allPizza.FontWeight = FontWeights.Bold;
            allPizza.GroupName = "pizza";
            allPizza.Checked += Pizza_Checked;
            allPizza.IsChecked = true;
            allPizza.Margin = new Thickness(5);
            allPizza.Template = radioStyle;
            allPizza.Width = 100;
            allPizza.Height = 40;

            traditionellePizza.Content = "TRADITIONELLE";
            traditionellePizza.FontWeight = FontWeights.Bold;
            traditionellePizza.GroupName = "pizza";
            traditionellePizza.Checked += Pizza_Checked;
            traditionellePizza.Margin = new Thickness(5);
            traditionellePizza.Template = radioStyle;
            traditionellePizza.Width = 100;
            traditionellePizza.Height = 40;

            spécialPizza.Content = "SPÉCIALE";
            spécialPizza.FontWeight = FontWeights.Bold;
            spécialPizza.GroupName = "pizza";
            spécialPizza.Checked += Pizza_Checked;
            spécialPizza.Margin = new Thickness(5);
            spécialPizza.Template = radioStyle;
            spécialPizza.Width = 100;
            spécialPizza.Height = 40;

            blanchePizza.Content = "BLANCHE";
            blanchePizza.FontWeight = FontWeights.Bold;
            blanchePizza.GroupName = "pizza";
            blanchePizza.Checked += Pizza_Checked;
            blanchePizza.Margin = new Thickness(5);
            blanchePizza.Template = radioStyle;
            blanchePizza.Width = 100;
            blanchePizza.Height = 40;

            merPizza.Content = "DE LA MER";
            merPizza.FontWeight = FontWeights.Bold;
            merPizza.GroupName = "pizza";
            merPizza.Checked += Pizza_Checked;
            merPizza.Margin = new Thickness(5);
            merPizza.Template = radioStyle;
            merPizza.Width = 100;
            merPizza.Height = 40;

            taillePizza.Children.Add(allPizza);
            taillePizza.Children.Add(traditionellePizza);
            taillePizza.Children.Add(spécialPizza);
            taillePizza.Children.Add(blanchePizza);
            taillePizza.Children.Add(merPizza);

            //Pizza taille
            radioTailleMédium.Content = "RÉGULAR";
            radioTailleMédium.FontWeight = FontWeights.Bold;
            radioTailleMédium.GroupName = "taillePizza";
            radioTailleMédium.Margin = new Thickness(200,5,5,5);
            radioTailleMédium.Template = radioStyle;
            radioTailleMédium.Width = 100;
            radioTailleMédium.Height = 40;
            radioTailleMédium.IsChecked = true;
            radioTailleMédium.HorizontalAlignment = HorizontalAlignment.Right;
            radioTailleMédium.Checked += Click_TaillePizza;

            radioTailleLarge.Content = "LARGE";
            radioTailleLarge.FontWeight = FontWeights.Bold;
            radioTailleLarge.GroupName = "taillePizza";
            radioTailleLarge.Margin = new Thickness(5);
            radioTailleLarge.Template = radioStyle;
            radioTailleLarge.Width = 100;
            radioTailleLarge.Height = 40;
            radioTailleLarge.HorizontalAlignment = HorizontalAlignment.Right;
            radioTailleLarge.Checked += Click_TaillePizza;

            taillePizza.Children.Add(radioTailleMédium);
            taillePizza.Children.Add(radioTailleLarge);

            //Type pate
            radioPateRigatoni.Content = "RIGATONI";
            radioPateRigatoni.FontWeight = FontWeights.Bold;
            radioPateRigatoni.GroupName = "pate";
            radioPateRigatoni.Checked += Pate_Checked;
            radioPateRigatoni.IsChecked = true;
            radioPateRigatoni.Margin = new Thickness(5);
            radioPateRigatoni.Template = radioStyle;
            radioPateRigatoni.Width = 100;
            radioPateRigatoni.Height = 40;

            radioPateTagliatelle.Content = "TAGLIATELLE";
            radioPateTagliatelle.FontWeight = FontWeights.Bold;
            radioPateTagliatelle.GroupName = "pate";
            radioPateTagliatelle.Checked += Pate_Checked;
            radioPateTagliatelle.Margin = new Thickness(5);
            radioPateTagliatelle.Template = radioStyle;
            radioPateTagliatelle.Width = 100;
            radioPateTagliatelle.Height = 40;

            radioPateSpaghetti .Content = "SPAGHETTI";
            radioPateSpaghetti .FontWeight = FontWeights.Bold;
            radioPateSpaghetti .GroupName = "pate";
            radioPateSpaghetti .Checked += Pate_Checked;
            radioPateSpaghetti .Margin = new Thickness(5);
            radioPateSpaghetti .Template = radioStyle;
            radioPateSpaghetti .Width = 100;
            radioPateSpaghetti .Height = 40;

            radioPateTortellini.Content = "TORTELLINI";
            radioPateTortellini.FontWeight = FontWeights.Bold;
            radioPateTortellini.GroupName = "pate";
            radioPateTortellini.Checked += Pate_Checked;
            radioPateTortellini.Margin = new Thickness(5);
            radioPateTortellini.Template = radioStyle;
            radioPateTortellini.Width = 100;
            radioPateTortellini.Height = 40;

            radioPateCanneloni.Content = "CANNELONI";
            radioPateCanneloni.FontWeight = FontWeights.Bold;
            radioPateCanneloni.GroupName = "pate";
            radioPateCanneloni.Checked += Pate_Checked;
            radioPateCanneloni.Margin = new Thickness(5);
            radioPateCanneloni.Template = radioStyle;
            radioPateCanneloni.Width = 100;
            radioPateCanneloni.Height = 40; 

            typePate.Children.Add(radioPateRigatoni);
            typePate.Children.Add(radioPateSpaghetti);
            typePate.Children.Add(radioPateTagliatelle);
            typePate.Children.Add(radioPateTortellini);
            typePate.Children.Add(radioPateCanneloni);

            //parametre suggestion
            suggestionMedium.Content = "RÉGULAR";
            suggestionMedium.FontWeight = FontWeights.Bold;
            suggestionMedium.GroupName = "suggestionPizza";
            suggestionMedium.Margin = new Thickness(200, 5, 5, 5);
            suggestionMedium.Template = radioStyle;
            suggestionMedium.Width = 100;
            suggestionMedium.Height = 40;
            suggestionMedium.IsChecked = true;
            suggestionMedium.HorizontalAlignment = HorizontalAlignment.Right;
            suggestionMedium.Checked += Suggestion_Checked;

            suggestionLarge.Content = "LARGE";
            suggestionLarge.FontWeight = FontWeights.Bold;
            suggestionLarge.GroupName = "suggestionPizza";
            suggestionLarge.Margin = new Thickness(5);
            suggestionLarge.Template = radioStyle;
            suggestionLarge.Width = 100;
            suggestionLarge.Height = 40;
            suggestionLarge.HorizontalAlignment = HorizontalAlignment.Right;
            suggestionLarge.Checked += Suggestion_Checked;

            suggestionRigatoni.Content = "RIGATONI";
            suggestionRigatoni.FontWeight = FontWeights.Bold;
            suggestionRigatoni.GroupName = "suggestionPate";
            suggestionRigatoni.Checked += Suggestion_Checked;
            suggestionRigatoni.IsChecked = true;
            suggestionRigatoni.Margin = new Thickness(5);
            suggestionRigatoni.Template = radioStyle;
            suggestionRigatoni.Width = 100;
            suggestionRigatoni.Height = 40;

            suggestionTagliatelle.Content = "TAGLIATELLE";
            suggestionTagliatelle.FontWeight = FontWeights.Bold;
            suggestionTagliatelle.GroupName = "suggestionPate";
            suggestionTagliatelle.Checked += Suggestion_Checked;
            suggestionTagliatelle.Margin = new Thickness(5);
            suggestionTagliatelle.Template = radioStyle;
            suggestionTagliatelle.Width = 100;
            suggestionTagliatelle.Height = 40;

            suggestionSpaghetti.Content = "SPAGHETTI";
            suggestionSpaghetti.FontWeight = FontWeights.Bold;
            suggestionSpaghetti.GroupName = "suggestionPate";
            suggestionSpaghetti.Checked += Suggestion_Checked;
            suggestionSpaghetti.Margin = new Thickness(5);
            suggestionSpaghetti.Template = radioStyle;
            suggestionSpaghetti.Width = 100;
            suggestionSpaghetti.Height = 40;

            choixSuggestion.Children.Add(suggestionRigatoni);
            choixSuggestion.Children.Add(suggestionSpaghetti);
            choixSuggestion.Children.Add(suggestionTagliatelle);
            choixSuggestion.Children.Add(suggestionMedium);
            choixSuggestion.Children.Add(suggestionLarge);
            //-------------------------------------------------------------------------------------------------------

            //------------------------------------------Création Vue Droite------------------------------------------
            StackPanel actionHaut = new StackPanel();
            StackPanel panier = new StackPanel();
            StackPanel actionBas = new StackPanel();

            Grid.SetColumn(actionHaut, 2);
            Grid.SetRow(actionHaut, 0);

            Grid.SetColumn(panier, 2);
            Grid.SetRow(panier, 1);

            Grid.SetColumn(actionBas, 2);
            Grid.SetRow(actionBas, 3);
            //-------------------------------------------------------------------------------------------------------

            //---------------------------------------------Création Ajout--------------------------------------------
            catalogueWrap.Add("PIZZA", pizza);
            catalogueWrap.Add("TAILLEPIZZA", taillePizza);
            catalogueWrap.Add("PÂTE", pate);
            catalogueWrap.Add("TYPEPATE", typePate);
            catalogueWrap.Add("SUGGESTION", suggestion);
            catalogueWrap.Add("CHOIXSUGGESTION", choixSuggestion);
            catalogueWrap.Add("PAIN", pain);
            catalogueWrap.Add("SALADE", salade);
            catalogueWrap.Add("DESSERT", dessert);
            catalogueWrap.Add("BOISSON", boisson);
            catalogueWrap.Add("DIPY", dipy);
            catalogueWrap.Add("SNACKING", snacking);
            catalogueWrap.Add("INGREDIENT", ingrédient);

            foreach (var wrap in catalogueWrap.Values)
            {
                grdMain.Children.Add(wrap);
            }
            //-------------------------------------------------------------------------------------------------------
            //faire un nouveau wrap panel avec les taille pizza et type patte et tout faire dans le click_Suggestion/ pizza pate pain salade
        }



        public void Click_Produit(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            DataTable data;
            double prix;
            string pate = "";
            string libelle;

            if (btn.Tag is int)
            {
                sql.ProduitById((int)btn.Tag, out data);
            }
            else
            {
                sql.ProduitByLibelle(btn.Tag.ToString(), out data);
            }

            libelle = data.Rows[0]["libelle"].ToString();
            if ((int)data.Rows[0]["idTypeProduit"] == 5 || data.Rows[0]["libelle"].ToString() == "moelleux au chocolat")
            {
                libelle = data.Rows[0]["libelle"].ToString() + " " + data.Rows[0]["quantite"].ToString() + "pc";
            }
            prix = (double)data.Rows[0]["prix"];
            if ((int)data.Rows[0]["idTypeProduit"] == 1)
            {
                if (radioTailleLarge.IsChecked == true)
                {
                    prix = (double)data.Rows[0]["prixLarge"];
                }
            }
            if ((int)data.Rows[0]["idTypeProduit"] == 2)
            {
                if (radioPateRigatoni.IsChecked == true)
                {
                    pate = "Rigatoni";
                }
                else if (radioPateSpaghetti.IsChecked == true)
                {
                    pate = "Spaghetti";
                }
                else if (radioPateTagliatelle.IsChecked == true)
                {
                    pate = "Tagliatelle";
                }
                else
                {
                    pate = "Tortellini";
                }
            }

            PagePrincipale.Panier.Add(new Produit
            {
                Id = (int)data.Rows[0]["id"],
                Libelle = data.Rows[0]["libelle"].ToString(),
                Prix = prix,
                EstLarge = (bool)radioTailleLarge.IsChecked,
                TypePate = pate,
                IdTypeProduit = (int)data.Rows[0]["idTypeProduit"]
            });
        }

        public void Click_catalogue(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string categori = btn.Content.ToString();
            foreach (var wrap in catalogueWrap.Values)
            {
                wrap.Visibility = Visibility.Collapsed;
            }
            if (categori == "PIZZA")
            {
                catalogueWrap["TAILLEPIZZA"].Visibility = Visibility.Visible;
            }
            else if (categori == "PÂTE")
            {
                catalogueWrap["TYPEPATE"].Visibility = Visibility.Visible;
            }
            else if(categori == "SUGGESTION")
            {
                catalogueWrap["CHOIXSUGGESTION"].Visibility = Visibility.Visible;
            }
            catalogueWrap[categori].Visibility = Visibility.Visible;

        }

        public void Click_Exit(object sender, RoutedEventArgs e)
        {
            PagePrincipale.MainInfo.Visibility = Visibility.Visible;
            PagePrincipale.MainInfo.Content = new Déconnexion();
        }

        public void Click_TaillePizza(object sender, RoutedEventArgs e)
        {
           Pizza_Checked(sender, e);
        }

        public void Pizza_Checked(object sender, RoutedEventArgs e)
        {
            pizza.Children.Clear();
            string maxi = "";
            string prix = "prix";
            if (radioTailleLarge.IsChecked == true)
            {
                maxi = "MAXI ";
                prix = "prixLarge";
            }
            if (allPizza.IsChecked == true)
            {
                sql.AllProduitByTypeProduit(1, out DataTable data, out int nbrRow);
                for (int i = 0; i < nbrRow; i++)
                {
                    Button btn = new Button();
                    btn.Content = RemplisageBouton(true, maxi + data.Rows[i]["libelle"].ToString(), (double)data.Rows[i][prix]);
                    btn.Tag = data.Rows[i]["id"];
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Gold;
                    btn.Margin = new Thickness(2);
                    btn.Click += new RoutedEventHandler(Click_Produit);
                    pizza.Children.Add(btn);
                }
            }
            else if (traditionellePizza.IsChecked == true)
            {
                sql.AllPizzaByTypeProduit(1, out DataTable data, out int nbrRow);
                for (int i = 0; i < nbrRow; i++)
                {
                    Button btn = new Button();
                    btn.Content = RemplisageBouton(true, maxi + data.Rows[i]["libelle"].ToString(), (double)data.Rows[i][prix]);
                    btn.Tag = data.Rows[i]["id"];
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Gold;
                    btn.Margin = new Thickness(2);
                    btn.Click += new RoutedEventHandler(Click_Produit);
                    pizza.Children.Add(btn);
                }
            }
            else if (spécialPizza.IsChecked == true)
            {
                sql.AllPizzaByTypeProduit(2, out DataTable data, out int nbrRow);
                for (int i = 0; i < nbrRow; i++)
                {
                    Button btn = new Button();
                    btn.Content = RemplisageBouton(true, maxi + data.Rows[i]["libelle"].ToString(), (double)data.Rows[i][prix]);
                    btn.Tag = data.Rows[i]["id"];
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Gold;
                    btn.FontWeight = FontWeights.Bold;
                    btn.Margin = new Thickness(2);
                    btn.Click += new RoutedEventHandler(Click_Produit);
                    pizza.Children.Add(btn);
                }
            }
            else if (blanchePizza.IsChecked == true)
            {
                sql.AllPizzaByTypeProduit(3, out DataTable data, out int nbrRow);
                for (int i = 0; i < nbrRow; i++)
                {
                    Button btn = new Button();
                    btn.Content = RemplisageBouton(true, maxi + data.Rows[i]["libelle"].ToString(), (double)data.Rows[i][prix]);
                    btn.Tag = data.Rows[i]["id"];
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Gold;
                    btn.Margin = new Thickness(2);
                    btn.Click += new RoutedEventHandler(Click_Produit);
                    pizza.Children.Add(btn);
                }
            }
            else if (merPizza.IsChecked == true)
            {
                sql.AllPizzaByTypeProduit(4, out DataTable data, out int nbrRow);
                for (int i = 0; i < nbrRow; i++)
                {
                    Button btn = new Button();
                    btn.Content = RemplisageBouton(true, maxi + data.Rows[i]["libelle"].ToString(), (double)data.Rows[i][prix]);
                    btn.Tag = data.Rows[i]["id"];
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Gold;
                    btn.Margin = new Thickness(2);
                    btn.Click += new RoutedEventHandler(Click_Produit);
                    pizza.Children.Add(btn);
                }
            }
            
        }

        public void Pate_Checked(object sender, RoutedEventArgs e)
        {
            pate.Children.Clear();
            RadioButton rb = (RadioButton)sender;
            string nom = rb.Content.ToString();
            if (rb.Content == "TORTELLINI")
            {
                string libelle = "Tomate crème";
                for (int i = 0; i < 2; i++)
                {
                    sql.ProduitByLibelle(libelle, out DataTable data);
                    Button btn = new Button();
                    btn.Content = RemplisageBouton(false, data.Rows[0]["libelle"].ToString(), (double)data.Rows[0]["prix"] + 1);
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Gold;
                    btn.Tag = libelle;
                    btn.Margin = new Thickness(2);
                    btn.Click += new RoutedEventHandler(Click_Produit);
                    pate.Children.Add(btn);
                    libelle = "Jambon crème";
                }
            }
            else if (rb.Content == "CANNELONI")
            {
                string libelle = "Canneloni ricota";
                sql.ProduitByLibelle(libelle, out DataTable data);
                Button btn = new Button();
                btn.Content = RemplisageBouton(false, data.Rows[0]["libelle"].ToString(), (double)data.Rows[0]["prix"]);
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Gold;
                btn.Tag = libelle;
                btn.Margin = new Thickness(2);
                btn.Click += new RoutedEventHandler(Click_Produit);
                pate.Children.Add(btn);
            }
            else
            {
                //All Bouton pate
                sql.AllProduitByTypeProduit(2, out DataTable data, out int nbrRow);
                for (int i = 0; i < nbrRow; i++)
                {
                    double prix = (radioPateTagliatelle.IsChecked == true ? 1 : 0);
                    Button btn = new Button();
                    btn.Content = RemplisageBouton(false, data.Rows[i]["libelle"].ToString(), (double)data.Rows[i]["prix"] + prix);
                    btn.Tag = data.Rows[i]["id"];
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Gold;
                    btn.Margin = new Thickness(2);
                    btn.Click += new RoutedEventHandler(Click_Produit);
                    pate.Children.Add(btn);
                }
            }
        }

        public Grid RemplisageBouton(Boolean pizza, string libelle, double prix)
        {
            Grid contenue = new Grid();
            contenue.HorizontalAlignment = HorizontalAlignment.Stretch;
            contenue.VerticalAlignment = VerticalAlignment.Stretch;
            contenue.Width = 100;
            contenue.Height = 100;

            if (pizza == true)
            {
                Image img = new Image();
                string src = "../images/" + libelle + ".png";
                img.Source = new BitmapImage(new Uri(src, UriKind.Relative));
                img.Stretch = Stretch.Fill;
                contenue.Children.Add(img);
            }

            TextBlock textNom = new TextBlock();
            textNom.Text = libelle.ToUpper();
            textNom.FontWeight = FontWeights.Bold;
            textNom.TextWrapping = TextWrapping.Wrap;
            textNom.HorizontalAlignment = HorizontalAlignment.Center;
            textNom.VerticalAlignment = VerticalAlignment.Center;
            textNom.TextAlignment = TextAlignment.Center;
            contenue.Children.Add(textNom);

            TextBlock textPrix = new TextBlock();
            textPrix.Text = prix.ToString();
            textPrix.Foreground = Brushes.White;
            textPrix.FontWeight = FontWeights.Bold;
            textPrix.VerticalAlignment = VerticalAlignment.Bottom;
            textPrix.HorizontalAlignment = HorizontalAlignment.Right;
            textPrix.Margin = new Thickness(0, 0, 5, 5);
            contenue.Children.Add(textPrix);

            return contenue;
        }

        public void Suggestion_Checked(object sender, RoutedEventArgs e)
        {
            string maxi = "";
            string prix = "prix";
            sql.AllProduitByTypeProduit(9, out DataTable data, out int nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btn = new Button();
                btn.Content = RemplisageBouton(false, maxi + data.Rows[i]["libelle"].ToString(), (double)data.Rows[i][prix]);
                btn.Tag = data.Rows[i]["id"];
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Gold;
                btn.Margin = new Thickness(2);
                btn.Click += new RoutedEventHandler(Click_Produit);
                pizza.Children.Add(btn);
            }
            //reagrder si c'est pizza afiiche comme ca etc et avec les chechek pour maxi et pate
        }
    }
}
