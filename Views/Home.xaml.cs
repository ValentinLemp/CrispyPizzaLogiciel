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
using Org.BouncyCastle.Utilities.IO;


namespace CrispyLo.Views
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    /// 
    //🔊 🔇
    public partial class Home : Page
    {
        //---------------------
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        ControlTemplate radioStyle = VisuelCSS.CreateButtonStyleTemplate();
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
        WrapPanel suggestion = new WrapPanel();

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

        RadioButton ajoutIng = new RadioButton();
        RadioButton supprIng = new RadioButton();
        RadioButton ingViande = new RadioButton();
        RadioButton ingFromage = new RadioButton();
        RadioButton ingLégume = new RadioButton();
        RadioButton ingPoisson = new RadioButton();
        RadioButton ingHuilCreme = new RadioButton();
        RadioButton ingSauce = new RadioButton();

        StackPanel panier = new StackPanel();

        TextBlock affichePrix = new TextBlock();


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
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(144) });

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
                btnCatalogue.Background = Brushes.Chartreuse;
                btnCatalogue.Foreground = Brushes.White;
                btnCatalogue.Margin = new Thickness(5, 0, 0, 0);
                btnCatalogue.Click += new RoutedEventHandler(Click_catalogue);
                if (data.Rows[i]["libelle"].ToString() == "pizza")
                {
                    btnCatalogue.Margin = new Thickness(5, 1, 0, 0);
                }

                selectionAffichage.Children.Add(btnCatalogue);
            }
            Button btnSCatalogue = new Button();
            btnSCatalogue.Content = "SUGGESTION";
            btnSCatalogue.Height = 70;
            btnSCatalogue.Width = 210;
            btnSCatalogue.HorizontalAlignment = HorizontalAlignment.Left;
            btnSCatalogue.FontWeight = FontWeights.Bold;
            btnSCatalogue.FontSize = 20;
            btnSCatalogue.Background = Brushes.Chartreuse;
            btnSCatalogue.Foreground = Brushes.White;
            btnSCatalogue.Click += new RoutedEventHandler(Click_catalogue);
            btnSCatalogue.Margin = new Thickness(5, 0, 0, 0);
            selectionAffichage.Children.Add(btnSCatalogue);


            Button btnIngrédient = new Button();
            btnIngrédient.Content = "INGREDIENT";
            btnIngrédient.Height = 70;
            btnIngrédient.Width = 210;
            btnIngrédient.HorizontalAlignment = HorizontalAlignment.Left;
            btnIngrédient.FontWeight = FontWeights.Bold;
            btnIngrédient.FontSize = 20;
            btnIngrédient.Background = Brushes.Chartreuse;
            btnIngrédient.Foreground = Brushes.White;
            btnIngrédient.Click += new RoutedEventHandler(Click_catalogue);
            btnIngrédient.Margin = new Thickness(5, 0, 0, 0);

            selectionAffichage.Children.Add(btnIngrédient);
            grdMain.Children.Add(selectionAffichage);


            Button btnDéconnexion = new Button();
            btnDéconnexion.Content = "X";
            btnDéconnexion.Foreground = Brushes.White;
            btnDéconnexion.Background = Brushes.Red;
            btnDéconnexion.FontWeight = FontWeights.Black;
            btnDéconnexion.Click += new RoutedEventHandler(Click_Exit);
            btnDéconnexion.Height = 25;
            btnDéconnexion.Width = 25;
            btnDéconnexion.VerticalAlignment = VerticalAlignment.Top;
            btnDéconnexion.HorizontalAlignment = HorizontalAlignment.Left;
            btnDéconnexion.Margin = new Thickness(5, 5, 0, 0);
            Grid.SetColumn(btnDéconnexion, 0);
            Grid.SetRow(btnDéconnexion, 0);
            grdMain.Children.Add(btnDéconnexion);

            // ---- Boutons son ----
            StackPanel panelSon = new StackPanel();
            panelSon.Orientation = Orientation.Horizontal;
            panelSon.VerticalAlignment = VerticalAlignment.Top;
            panelSon.HorizontalAlignment = HorizontalAlignment.Left;
            panelSon.Margin = new Thickness(35, 5, 0, 0);
            Grid.SetColumn(panelSon, 0);
            Grid.SetRow(panelSon, 0);

            Button btnVolUp = new Button();
            btnVolUp.Content = "+";
            btnVolUp.Width = 25;
            btnVolUp.Height = 25;
            btnVolUp.FontWeight = FontWeights.Bold;
            btnVolUp.Click += (s, ev) =>
            {
                if (PagePrincipale.SonNotification.Volume < 1.0)
                    PagePrincipale.SonNotification.Volume = Math.Min(1.0, PagePrincipale.SonNotification.Volume + 0.2);
                PagePrincipale.SonNotification.Stop();
                PagePrincipale.SonNotification.Position = TimeSpan.Zero;
                PagePrincipale.SonNotification.Play();
            };

            Button btnMute = new Button();
            btnMute.Content = "🔊";
            btnMute.Width = 25;
            btnMute.Height = 25;
            btnMute.Click += (s, ev) =>
            {
                PagePrincipale.SonNotification.IsMuted = !PagePrincipale.SonNotification.IsMuted;
                btnMute.Content = PagePrincipale.SonNotification.IsMuted ? "🔇" : "🔊";
                if (!PagePrincipale.SonNotification.IsMuted)
                {
                    PagePrincipale.SonNotification.Stop();
                    PagePrincipale.SonNotification.Position = TimeSpan.Zero;
                    PagePrincipale.SonNotification.Play();
                }
            };

            Button btnVolDown = new Button();
            btnVolDown.Content = "-";
            btnVolDown.Width = 25;
            btnVolDown.Height = 25;
            btnVolDown.FontWeight = FontWeights.Bold;
            btnVolDown.Click += (s, ev) =>
            {
                if (PagePrincipale.SonNotification.Volume > 0.0)
                    PagePrincipale.SonNotification.Volume = Math.Max(0.0, PagePrincipale.SonNotification.Volume - 0.2);
                PagePrincipale.SonNotification.Stop();
                PagePrincipale.SonNotification.Position = TimeSpan.Zero;
                PagePrincipale.SonNotification.Play();
            };

            panelSon.Children.Add(btnVolUp);
            panelSon.Children.Add(btnMute);
            panelSon.Children.Add(btnVolDown);
            grdMain.Children.Add(panelSon);

            //-------------------------------------------------------------------------------------------------------
            //------------------------------------------Création Vue Milieu------------------------------------------ 
            WrapPanel taillePizza = new WrapPanel();
            WrapPanel typePate = new WrapPanel();
            WrapPanel choixSuggestion = new WrapPanel();
            WrapPanel pain = new WrapPanel();
            WrapPanel snacking = new WrapPanel();
            WrapPanel salade = new WrapPanel();
            WrapPanel dipy = new WrapPanel();
            WrapPanel dessert = new WrapPanel();
            WrapPanel boisson = new WrapPanel();
            WrapPanel choixIngrédient = new WrapPanel();

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

            choixIngrédient.Visibility = Visibility.Collapsed;
            Grid.SetColumn(choixIngrédient, 1);
            Grid.SetRow(choixIngrédient, 0);

            choixSuggestion.Visibility = Visibility.Collapsed;
            Grid.SetColumn(choixSuggestion, 1);
            Grid.SetRow(choixSuggestion, 0);


            //All Bouton salade
            sql.AllProduitByTypeProduit(3, out data, out nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btn = new Button();
                btn.Content = VisuelCSS.RemplisageBouton(false, false, data.Rows[i]["libelle"].ToString(), (double)data.Rows[i]["prix"]);
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
                btn.Content = VisuelCSS.RemplisageBouton(false, false, data.Rows[i]["libelle"].ToString(), (double)data.Rows[i]["prix"]);
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
                btn.Content = VisuelCSS.RemplisageBouton(false, false, nom, (double)data.Rows[i]["prix"]);
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
                btn.Content = VisuelCSS.RemplisageBouton(false, false, nom, (double)data.Rows[i]["prix"]);
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
                btn.Content = VisuelCSS.RemplisageBouton(false, false, nom, (double)data.Rows[i]["prix"]);
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
                btn.Content = VisuelCSS.RemplisageBouton(false, false, data.Rows[i]["libelle"].ToString(), (double)data.Rows[i]["prix"]);
                btn.Tag = data.Rows[i]["id"];
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Gold;
                btn.Margin = new Thickness(2);
                btn.Click += new RoutedEventHandler(Click_Produit);
                dipy.Children.Add(btn);
            }


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
            radioTailleMédium.Margin = new Thickness(200, 5, 5, 5);
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

            radioPateSpaghetti.Content = "SPAGHETTI";
            radioPateSpaghetti.FontWeight = FontWeights.Bold;
            radioPateSpaghetti.GroupName = "pate";
            radioPateSpaghetti.Checked += Pate_Checked;
            radioPateSpaghetti.Margin = new Thickness(5);
            radioPateSpaghetti.Template = radioStyle;
            radioPateSpaghetti.Width = 100;
            radioPateSpaghetti.Height = 40;

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

            ingViande.Content = "VIANDES";
            ingViande.FontWeight = FontWeights.Bold;
            ingViande.GroupName = "typeIngredient";
            ingViande.Checked += Ingredient_Checked;
            ingViande.Margin = new Thickness(5);
            ingViande.Template = radioStyle;
            ingViande.IsChecked = true;
            ingViande.Width = 100;
            ingViande.Height = 40;

            ingFromage.Content = "FROMAGES";
            ingFromage.FontWeight = FontWeights.Bold;
            ingFromage.GroupName = "typeIngredient";
            ingFromage.Checked += Ingredient_Checked;
            ingFromage.Margin = new Thickness(5);
            ingFromage.Template = radioStyle;
            ingFromage.Width = 100;
            ingFromage.Height = 40;

            ingLégume.Content = "LÉGUMES";
            ingLégume.FontWeight = FontWeights.Bold;
            ingLégume.GroupName = "typeIngredient";
            ingLégume.Checked += Ingredient_Checked;
            ingLégume.Margin = new Thickness(5);
            ingLégume.Template = radioStyle;
            ingLégume.Width = 100;
            ingLégume.Height = 40;

            ingPoisson.Content = "POISSONS";
            ingPoisson.FontWeight = FontWeights.Bold;
            ingPoisson.GroupName = "typeIngredient";
            ingPoisson.Checked += Ingredient_Checked;
            ingPoisson.Margin = new Thickness(5);
            ingPoisson.Template = radioStyle;
            ingPoisson.Width = 100;
            ingPoisson.Height = 40;

            ingHuilCreme.Content = "HUILE/CRÈME";
            ingHuilCreme.FontWeight = FontWeights.Bold;
            ingHuilCreme.GroupName = "typeIngredient";
            ingHuilCreme.Checked += Ingredient_Checked;
            ingHuilCreme.Margin = new Thickness(5);
            ingHuilCreme.Template = radioStyle;
            ingHuilCreme.Width = 100;
            ingHuilCreme.Height = 40;

            ingSauce.Content = "SAUCES";
            ingSauce.FontWeight = FontWeights.Bold;
            ingSauce.GroupName = "typeIngredient";
            ingSauce.Checked += Ingredient_Checked;
            ingSauce.Margin = new Thickness(5);
            ingSauce.Template = radioStyle;
            ingSauce.Width = 100;
            ingSauce.Height = 40;

            ajoutIng.Content = "AJOUTER";
            ajoutIng.FontWeight = FontWeights.Bold;
            ajoutIng.GroupName = "choixIngredient";
            ajoutIng.Checked += Ingredient_Checked;
            ajoutIng.Margin = new Thickness(100, 5, 5, 5);
            ajoutIng.Template = radioStyle;
            ajoutIng.IsChecked = true;
            ajoutIng.Width = 100;
            ajoutIng.Height = 40;

            supprIng.Content = "SUPPRIMER";
            supprIng.FontWeight = FontWeights.Bold;
            supprIng.GroupName = "choixIngredient";
            supprIng.Checked += Ingredient_Checked;
            supprIng.Margin = new Thickness(5);
            supprIng.Template = radioStyle;
            supprIng.Width = 100;
            supprIng.Height = 40;

            choixIngrédient.Children.Add(ingViande);
            choixIngrédient.Children.Add(ingFromage);
            choixIngrédient.Children.Add(ingLégume);
            choixIngrédient.Children.Add(ingPoisson);
            choixIngrédient.Children.Add(ingHuilCreme);
            choixIngrédient.Children.Add(ingSauce);
            choixIngrédient.Children.Add(ajoutIng);
            choixIngrédient.Children.Add(supprIng);
            //-------------------------------------------------------------------------------------------------------

            //------------------------------------------Création Vue Droite------------------------------------------
            StackPanel actionHaut = new StackPanel();
            WrapPanel actionBas = new WrapPanel();
            WrapPanel commander = new WrapPanel();

            Button supprimer = new Button();
            Button vider = new Button();
            Button remise = new Button();
            Button calculatrice = new Button();
            Button note = new Button();

            Button commande = new Button();

            supprimer.Content = "❌";
            supprimer.Background = Brushes.Transparent;
            supprimer.Height = 50;
            supprimer.Width = 50;
            supprimer.FontSize = 20;
            supprimer.Foreground = Brushes.Black;
            supprimer.Margin = new Thickness(15,5,5,5);
            supprimer.Click += new RoutedEventHandler(Click_Supprimer);
            supprimer.FontWeight = FontWeights.Bold;

            vider.Content = "🗑️";
            vider.Background = Brushes.Transparent;
            vider.Height = 50;
            vider.Width = 50;
            vider.FontSize = 20;
            vider.Foreground = Brushes.Black;
            vider.Margin = new Thickness(5);
            vider.Click += new RoutedEventHandler(Click_Vider);
            vider.FontWeight = FontWeights.Bold;

            remise.Content = "🏷️";
            remise.Background = Brushes.Transparent;
            remise.Height = 50;
            remise.Width = 50;
            remise.FontSize = 25;
            remise.Foreground = Brushes.Black;
            remise.Margin = new Thickness(5);
            remise.Click += new RoutedEventHandler(Click_Remise);
            remise.FontWeight = FontWeights.Bold;

            calculatrice.Content = "🧮";
            calculatrice.Background = Brushes.Transparent;
            calculatrice.Height = 50;
            calculatrice.Width = 50;
            calculatrice.FontSize = 25;
            calculatrice.Margin = new Thickness(5);
            calculatrice.Foreground = Brushes.Black;
            calculatrice.Click += new RoutedEventHandler(Click_Calculette);
            calculatrice.FontWeight = FontWeights.Bold;

            note.Content = "🗒️";
            note.Background = Brushes.Transparent;
            note.Height = 50;
            note.Width = 50;
            note.FontSize = 20;
            note.Foreground = Brushes.Black;
            note.Margin = new Thickness(5);
            note.Click += new RoutedEventHandler(Click_Note);
            note.FontWeight = FontWeights.Bold;

            actionBas.VerticalAlignment = VerticalAlignment.Top;
            actionBas.HorizontalAlignment = HorizontalAlignment.Stretch;
            actionBas.Margin = new Thickness(0, 0, 5, 0);
            actionBas.Background = Brushes.White;
            Grid.SetColumn(actionHaut, 2);
            Grid.SetRow(actionHaut, 0);

            Grid.SetColumn(actionBas, 2);
            Grid.SetRow(actionBas, 2);

            actionBas.Children.Add(supprimer);
            actionBas.Children.Add(vider);
            actionBas.Children.Add(remise);
            actionBas.Children.Add(note);
            actionBas.Children.Add(calculatrice);

            grdMain.Children.Add(actionBas);

            panier.Background = Brushes.White;
            ScrollViewer scrollPanier = new ScrollViewer();
            scrollPanier.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollPanier.Content = panier;
            scrollPanier.Margin = new Thickness(0, 0, 5, 0);
            Grid.SetColumn(scrollPanier, 2);
            Grid.SetRow(scrollPanier, 1);
            grdMain.Children.Add(scrollPanier);

            commander.Background = Brushes.LightGray;
            commander.Margin = new Thickness(0, 10, 5, 5);
            Grid.SetColumn(commander, 2);
            Grid.SetRow(commander, 3);

            commande.Content = "🛒";
            commande.Height = 55;
            commande.Width = 85;
            commande.Background = Brushes.Transparent;
            commande.Margin = new Thickness(40, 35, 0, 0);
            commande.VerticalAlignment = VerticalAlignment.Center;
            commande.FontSize = 40;
            commande.FontWeight = FontWeights.Bold;
            commande.Click += new RoutedEventHandler(Click_Commande);

            affichePrix.Text = "0 €";
            affichePrix.Height = 60;
            affichePrix.Width = 140;
            affichePrix.Background = Brushes.Transparent;
            affichePrix.Margin = new Thickness(40, 35, 5, 0);
            affichePrix.VerticalAlignment = VerticalAlignment.Center;
            affichePrix.FontSize = 40;
            affichePrix.TextAlignment = TextAlignment.Right;

            commander.Children.Add(commande);
            commander.Children.Add(affichePrix);
            grdMain.Children.Add(commander);

            //action du haut droite
            WrapPanel wrap = new WrapPanel();
            wrap.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(wrap, 2);
            Grid.SetRow(wrap, 0);

            Button btnAdmin = new Button();
            btnAdmin.Content = "Admin";
            btnAdmin.FontWeight = FontWeights.Bold;
            btnAdmin.FontSize = 15;
            btnAdmin.Foreground = Brushes.White;
            btnAdmin.Background = Brushes.Red;
            btnAdmin.Width = 70;
            btnAdmin.Height = 35;
            btnAdmin.Click += new RoutedEventHandler(Click_Admin);
            if (PagePrincipale.EstAdmin)
            {
                btnAdmin.Background = Brushes.Green;
            }

            wrap.Children.Add(btnAdmin);

            Button allCommande = new Button();
            allCommande.Content = "🗃️";
            allCommande.FontSize = 20;
            allCommande.Height = 40;
            allCommande.Width = 40;
            allCommande.Background = Brushes.DodgerBlue;
            allCommande.Foreground = Brushes.White;
            allCommande.Margin = new Thickness(20, 0, 0, 0);
            allCommande.Click += new RoutedEventHandler(Click_AllCommande);
            wrap.Children.Add(allCommande);

            Button allClient = new Button();
            allClient.Content = "👤";
            allClient.FontSize = 20;
            allClient.Height = 40;
            allClient.Width = 40;
            allClient.Background = Brushes.DodgerBlue;
            allClient.Foreground = Brushes.White;
            allClient.Margin = new Thickness(10, 0, 0, 0);
            allClient.Click += new RoutedEventHandler(Click_allClient);
            wrap.Children.Add(allClient);

            Button retourCommande = new Button();
            retourCommande.Visibility = Visibility.Hidden;
            retourCommande.Content = "🔙";
            retourCommande.FontWeight = FontWeights.Bold;
            retourCommande.FontSize = 20;
            retourCommande.Foreground = Brushes.White;
            retourCommande.Background = Brushes.Red;
            retourCommande.Width = 35;
            retourCommande.Height = 35;
            retourCommande.Margin = new Thickness(45,10,10,10);
            retourCommande.HorizontalAlignment = HorizontalAlignment.Right;
            retourCommande.VerticalAlignment = VerticalAlignment.Top;
            retourCommande.Click += new RoutedEventHandler(Click_AllCommande);
            wrap.Children.Add(retourCommande);

            grdMain.Children.Add(wrap);

            if (PagePrincipale.IdCommande != -1)
            {
                retourCommande.Visibility = Visibility.Visible;
                allCommande.Visibility = Visibility.Collapsed;
                allClient.Visibility = Visibility.Collapsed;
                btnAdmin.Visibility = Visibility.Collapsed;
            }
            //-------------------------------------------------------------------------------------------------------

            //---------------------------------------------Création catalogue--------------------------------------------
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
            catalogueWrap.Add("CHOIXINGREDIENT", choixIngrédient);

            foreach (var wrapCat in catalogueWrap.Values)
            {
                grdMain.Children.Add(wrapCat);
            }

            if (PagePrincipale.Panier.Count > 0)
            {
                affichePanier();
            }
            //-------------------------------------------------------------------------------------------------------
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
            string nom = "";
            if ((int)data.Rows[0]["idTypeProduit"] == 1)
            {
                if (radioTailleLarge.IsChecked == true)
                {
                    prix = (double)data.Rows[0]["prixLarge"];
                    nom = "maxi ";
                }
            }
            if ((int)data.Rows[0]["idTypeProduit"] == 2)
            {
                if (radioPateRigatoni.IsChecked == true)
                {
                    pate = "Rigatoni ";
                    nom = "Rigatoni ";
                }
                else if (radioPateSpaghetti.IsChecked == true)
                {
                    pate = "Spaghetti ";
                    nom = "Spaghetti ";
                }
                else if (radioPateTagliatelle.IsChecked == true)
                {
                    pate = "Tagliatelle ";
                    nom = "Tagliatelle ";
                    prix += 1;
                }
                else if(radioPateTortellini.IsChecked == true)
                {
                    pate = "Tortellini ";
                    nom = "Tortellini ";
                    prix += 1;
                }
            }
            nom += data.Rows[0]["libelle"].ToString();
            if ((int)data.Rows[0]["idTypeProduit"] == 5 || nom == "Moelleux au chocolat")
            {
                nom += " " + data.Rows[0]["quantite"] + "pc";
            }
            PagePrincipale.Panier.Add(new Produit
            {
                Id = (int)data.Rows[0]["id"],
                Libelle = nom,
                Prix = prix,
                EstLarge = (bool)radioTailleLarge.IsChecked,
                TypePate = pate,
                IdTypeProduit = (int)data.Rows[0]["idTypeProduit"],
                Ingredients = new List<Ingredient>(),
                Note = ""
            });
            affichePanier();
            RadioButton radiobtn = (RadioButton)panier.Children[panier.Children.Count - 1];
            radiobtn.IsChecked = true;

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
            else if (categori == "SUGGESTION")
            {
                catalogueWrap["CHOIXSUGGESTION"].Visibility = Visibility.Visible;
            }
            else if (categori == "INGREDIENT")
            {
                catalogueWrap["CHOIXINGREDIENT"].Visibility = Visibility.Visible;
                Ingredient_Checked(null, null);
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
            string prix = "prix";
            Boolean estLarge = false;
            int id = 0;
            if (radioTailleLarge.IsChecked == true)
            {
                estLarge = true;
                prix = "prixLarge";
            }

            if (allPizza.IsChecked == true)
            {
                sql.AllProduitByTypeProduit(1, out DataTable data, out int nbrRow);
                for (int i = 0; i < nbrRow; i++)
                {
                    Button btn = new Button();
                    btn.Content = VisuelCSS.RemplisageBouton(true, estLarge, data.Rows[i]["libelle"].ToString(), (double)data.Rows[i][prix]);
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
                id = 1;
            }
            else if (spécialPizza.IsChecked == true)
            {
                id = 2;
            }
            else if (blanchePizza.IsChecked == true)
            {
                id = 3;
            }
            else if (merPizza.IsChecked == true)
            {
                id = 4;
            }
            if (id != 0)
            {
                sql.AllPizzaByTypeProduit(id, out DataTable data, out int nbrRow);
                for (int i = 0; i < nbrRow; i++)
                {
                    Button btn = new Button();
                    btn.Content = VisuelCSS.RemplisageBouton(true, false, data.Rows[i]["libelle"].ToString(), (double)data.Rows[i][prix]);
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
            string nom = rb.Content.ToString() + " ";

            if (rb.Content == "TORTELLINI")
            {
                string libelle = "Tomate crème";
                for (int i = 0; i < 2; i++)
                {
                    sql.ProduitByLibelle(libelle, out DataTable data);
                    Button btn = new Button();
                    btn.Content = VisuelCSS.RemplisageBouton(false, false, nom + data.Rows[0]["libelle"].ToString(), (double)data.Rows[0]["prix"] + 1);
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
                btn.Content = VisuelCSS.RemplisageBouton(false, false, data.Rows[0]["libelle"].ToString(), (double)data.Rows[0]["prix"]);
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
                    btn.Content = VisuelCSS.RemplisageBouton(false, false, nom + data.Rows[i]["libelle"].ToString(), (double)data.Rows[i]["prix"] + prix);
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

        public void Suggestion_Checked(object sender, RoutedEventArgs e)
        {

            sql.AllProduitByEstSuggestion(out DataTable data, out int nbrRow);
            if (data != null)
            {
                suggestion.Children.Clear();
                for (int i = 0; i < nbrRow; i++)
                {
                    string phrase = "";
                    Boolean estLarge = false;

                    double prix = (double)data.Rows[i]["prix"];
                    if (suggestionLarge.IsChecked == true && (int)data.Rows[i]["idTypeProduit"] == 1)
                    {
                        estLarge = true;
                        prix = (double)data.Rows[i]["prixLarge"];
                    }
                    else if (suggestionRigatoni.IsChecked == true && (int)data.Rows[i]["idTypeProduit"] == 2)
                    {
                        phrase = "rigatoni ";
                    }
                    else if (suggestionSpaghetti.IsChecked == true && (int)data.Rows[i]["idTypeProduit"] == 2)
                    {
                        phrase = "spaghetti ";
                    }
                    else if (suggestionTagliatelle.IsChecked == true && (int)data.Rows[i]["idTypeProduit"] == 2)
                    {
                        phrase = "tagliatelle ";
                        prix = (double)data.Rows[i]["prix"] + 1;
                    }
                    Button btn = new Button();
                    btn.Content = VisuelCSS.RemplisageBouton(false, estLarge, phrase + data.Rows[i]["libelle"].ToString(), prix);
                    btn.Tag = data.Rows[i]["id"];
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Gold;
                    btn.Margin = new Thickness(2);
                    btn.Click += new RoutedEventHandler(Click_Produit);
                    suggestion.Children.Add(btn);
                }
            }
        }
        public void affichePanier()
        {
            if (PagePrincipale.Panier.Count == 0)
            {
                affichePrix.Text = "0 €";
            }
            else
            {
                affichePrix.Text = Traitement.prixTotal() + " €";
            }
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

                RadioButton rb = new RadioButton();
                rb.Content = grille;
                rb.Height = 25;
                rb.Tag = i;
                rb.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                rb.GroupName = "panier";
                rb.Click += new RoutedEventHandler(Ingredient_Checked);
                rb.Template = VisuelCSS.CreatePanierRadioTemplate();

                panier.Children.Add(rb);

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

                    grilleIng.Width = 316;
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


        public void Ingredient_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rdtbtn = panier.Children.OfType<RadioButton>().FirstOrDefault(rb => rb.IsChecked == true);

            if (PagePrincipale.Panier.Count != 0 && rdtbtn != null)
            {
                ingrédient.Children.Clear();

                int index = (int)rdtbtn.Tag;
                Produit p = PagePrincipale.Panier[index];
                if (p.IdTypeProduit == 1 || p.IdTypeProduit == 2 || p.IdTypeProduit == 3)
                {
                    string prix = "prixMedium";
                    if (p.EstLarge == true)
                    {
                        prix = "prixLarge";
                    }

                    if (ajoutIng.IsChecked == true)
                    {
                        string libelle = "forPizza";
                        if (p.IdTypeProduit == 2) { libelle = "forPate"; }
                        else if (p.IdTypeProduit == 3) { libelle = "forSalade"; }
                        int id = 0;
                        if (ingViande.IsChecked == true) { id = 1; }
                        else if (ingFromage.IsChecked == true) { id = 2; }
                        else if (ingLégume.IsChecked == true) { id = 3; }
                        else if (ingPoisson.IsChecked == true) { id = 4; }
                        else if (ingHuilCreme.IsChecked == true) { id = 5; }
                        else if (ingSauce.IsChecked == true) { id = 6; }

                        sql.AllIngredientByCategorieIng(id, libelle, out DataTable data, out int nbrRow);
                        for (int i = 0; i < nbrRow; i++)
                        {
                            Button btn = new Button();
                            btn.Background = Brushes.Gold;

                            foreach (Ingredient ing in p.Ingredients)
                            {
                                if (ing.Libelle == data.Rows[i]["libelle"].ToString() && ing.EstSupplement == true)
                                {
                                    btn.Background = Brushes.LightGreen;
                                    break;
                                }
                            }
                            btn.Content = VisuelCSS.RemplisageBouton(false, false, data.Rows[i]["libelle"].ToString(), (double)data.Rows[i][prix]);
                            btn.Tag = data.Rows[i]["id"];
                            btn.Width = 100;
                            btn.Height = 100;
                            btn.Margin = new Thickness(2);
                            btn.Click += new RoutedEventHandler(Click_IngredientPlus);
                            ingrédient.Children.Add(btn);
                        }
                    }
                    else
                    {
                        sql.ProduitById(p.Id, out DataTable data);
                        string[] tabIng = Traitement.descToIngredient(data.Rows[0]["description"].ToString());
                        for (int i = 0; i < tabIng.Length; i++)
                        {
                            int prixIng = 0;
                            Button btn = new Button();
                            btn.Background = Brushes.Gold;
                            foreach (Ingredient ing in p.Ingredients)
                            {
                                if (ing.Libelle == tabIng[i] && ing.EstSupplement == false)
                                {
                                    prixIng = -1;
                                    btn.Background = Brushes.Crimson;
                                    break;
                                }
                            }
                            btn.Content = VisuelCSS.RemplisageBouton(false, false, tabIng[i], prixIng);
                            btn.Width = 100;
                            btn.Height = 100;
                            btn.Tag = tabIng[i];
                            btn.Margin = new Thickness(2);
                            btn.Click += new RoutedEventHandler(Click_IngredientMoins);
                            ingrédient.Children.Add(btn);
                        }
                    }
                }

            }
        }

        public void Click_IngredientMoins(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            RadioButton rdtbtn = panier.Children.OfType<RadioButton>().FirstOrDefault(rb => rb.IsChecked == true);
            int index = (int)rdtbtn.Tag;
            Produit p = PagePrincipale.Panier[index];
            if (btn.Background == Brushes.Gold)
            {
                btn.Background = Brushes.Crimson;
                btn.Content = VisuelCSS.RemplisageBouton(false, false, btn.Tag.ToString(), -1);
                p.Ingredients.Add(new Ingredient
                {
                    Id = 0,
                    Libelle = btn.Tag.ToString(),
                    Prix = 0,
                    EstSupplement = false
                });
            }
            else
            {
                btn.Background = Brushes.Gold;
                btn.Content = VisuelCSS.RemplisageBouton(false, false, btn.Tag.ToString(), 0);
                foreach (Ingredient ing in p.Ingredients)
                {
                    if (ing.Libelle == btn.Tag.ToString())
                    {
                        p.Ingredients.Remove(ing);
                        break;
                    }
                }
            }
            affichePanier();
            foreach (RadioButton radio in panier.Children.OfType<RadioButton>())
            {
                if ((int)radio.Tag == (int)rdtbtn.Tag)
                {
                    radio.IsChecked = true;
                    break;
                }
            }
        }
        public void Click_IngredientPlus(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            RadioButton rdtbtn = panier.Children.OfType<RadioButton>().FirstOrDefault(rb => rb.IsChecked == true);
            int index = (int)rdtbtn.Tag;
            Produit p = PagePrincipale.Panier[index];
            sql.IngredientById((int)btn.Tag, out DataTable data);

            if (btn.Background == Brushes.Gold)
            {
                btn.Background = Brushes.LightGreen;
                double prix = (double)data.Rows[0]["prixMedium"];
                if (p.EstLarge) { prix = (double)data.Rows[0]["prixLarge"]; }
                p.Ingredients.Add(new Ingredient
                {
                    Id = (int)data.Rows[0]["id"],
                    Libelle = data.Rows[0]["libelle"].ToString(),
                    Prix = prix,
                    EstSupplement = true
                });
            }
            else
            {
                btn.Background = Brushes.Gold;
                foreach (Ingredient ing in p.Ingredients)
                {
                    if (ing.Id == (int)btn.Tag)
                    {
                        p.Ingredients.Remove(ing);
                        break;
                    }
                }
            }
            affichePanier();
            foreach (RadioButton radio in panier.Children.OfType<RadioButton>())
            {
                if ((int)radio.Tag == (int)rdtbtn.Tag)
                {
                    radio.IsChecked = true;
                    break;
                }
            }
        }

        public void Click_Supprimer(object sender, RoutedEventArgs e)
        {
            RadioButton rdtbtn = panier.Children.OfType<RadioButton>().FirstOrDefault(rb => rb.IsChecked == true);
            if (rdtbtn != null)
            {
                int index = (int)rdtbtn.Tag;
                PagePrincipale.Panier.RemoveAt(index);
                affichePanier();
            }
        }

        public void Click_Vider(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Panier.Clear();
            panier.Children.Clear();
            affichePrix.Text = "0 €";
        }

        public async void Click_Remise(object sender, RoutedEventArgs e)
        {
            RadioButton rdtbtn = panier.Children.OfType<RadioButton>().FirstOrDefault(rb => rb.IsChecked == true);
            if (rdtbtn != null)
            {
                PagePrincipale.MainInfo.Content = new Remise((int)rdtbtn.Tag);
                PagePrincipale.MainInfo.Visibility = Visibility.Visible;

                var tcs = new TaskCompletionSource<bool>();
                void handler(object s, DependencyPropertyChangedEventArgs args)
                {
                    if (PagePrincipale.MainInfo.Visibility != Visibility.Visible)
                        tcs.SetResult(true);
                }
                PagePrincipale.MainInfo.IsVisibleChanged += handler;
                await tcs.Task;
                PagePrincipale.MainInfo.IsVisibleChanged -= handler;

                affichePanier();
            }
        }

        public void Click_Calculette(object sender, RoutedEventArgs e)
        {
            PagePrincipale.MainInfo.Content = new Calculette(Traitement.prixTotal());
            PagePrincipale.MainInfo.Visibility = Visibility.Visible;
        }

        public async void Click_Note(object sender, RoutedEventArgs e)
        {
            RadioButton rdtbtn = panier.Children.OfType<RadioButton>().FirstOrDefault(rb => rb.IsChecked == true);
            if (rdtbtn != null)
            {
                int index = (int)rdtbtn.Tag;
                PagePrincipale.MainInfo.Content = new WriteNote(index);
                PagePrincipale.MainInfo.Visibility = Visibility.Visible;

                var tcs = new TaskCompletionSource<bool>();
                void handler(object s, DependencyPropertyChangedEventArgs args)
                {
                    if (PagePrincipale.MainInfo.Visibility != Visibility.Visible)
                        tcs.SetResult(true);
                }
                PagePrincipale.MainInfo.IsVisibleChanged += handler;
                await tcs.Task;
                PagePrincipale.MainInfo.IsVisibleChanged -= handler;

                affichePanier();
            }
        }

        public void Click_Commande(object sender, RoutedEventArgs e)
        {
 
            PagePrincipale.Main.Content = new NewCommande();
        }

        public void Click_AllCommande(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new AllCommande();
        }

        public void Click_allClient(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Main.Content = new AllClient();
        }

        public void Click_Admin(object sender, RoutedEventArgs e)
        {
            if (PagePrincipale.EstAdmin)
            {
                PagePrincipale.Main.Content = new HomeAdmin();
            }
            else
            {
                PagePrincipale.MainInfo.Content = new ChoixAdmin();
                PagePrincipale.MainInfo.Visibility = Visibility.Visible;
            }
        }
    }
}
