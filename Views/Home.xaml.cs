using CrispyLo.Modules;
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
using CrispyLo.Views.Info;


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


        public void PrepareInitialize()
        {
            //Grille Colonne
            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(220) });
            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(320) });

            //Grille Ligne
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(124) });

            //Bordure
            Border bordS = new Border();
            bordS.BorderBrush = Brushes.Black;
            bordS.Background = Brushes.DarkKhaki;
            bordS.BorderThickness= new Thickness(0, 1, 2, 0);
            Grid.SetColumn(bordS, 0);
            Grid.SetRow(bordS, 1);
            Grid.SetRowSpan(bordS, 4);
            grdMain.Children.Add(bordS);

            Border bordT = new Border();
            bordT.BorderBrush = Brushes.Black;
            bordT.Background = Brushes.DarkKhaki;
            bordT.BorderThickness = new Thickness(1, 0, 1, 2);
            Grid.SetColumn(bordT, 1);
            Grid.SetRow(bordT, 0);
            grdMain.Children.Add(bordT);

            Border bordD = new Border();
            bordD.BorderBrush = Brushes.Black;
            bordD.Background = Brushes.DarkKhaki;
            bordD.BorderThickness = new Thickness(0, 0, 1, 1);
            Grid.SetColumn(bordD, 0);
            Grid.SetRow(bordD, 0);
            grdMain.Children.Add(bordD);

            Border bordA = new Border();
            bordA.BorderBrush = Brushes.Black;
            bordA.Background = Brushes.DarkKhaki;
            bordA.BorderThickness = new Thickness(1, 0, 0, 1);
            Grid.SetColumn(bordA, 2);
            Grid.SetRow(bordA, 0);
            grdMain.Children.Add(bordA);

            Border bordP = new Border();
            bordP.BorderBrush = Brushes.Black;
            bordP.Background = Brushes.White;
            bordP.BorderThickness = new Thickness(2, 1, 0, 1);
            Grid.SetColumn(bordP, 2);
            Grid.SetRow(bordP, 1);
            grdMain.Children.Add(bordP);

            Border bordAP = new Border();
            bordAP.BorderBrush = Brushes.Black;
            bordAP.Background = Brushes.DarkKhaki;
            bordAP.BorderThickness = new Thickness(2, 1, 0, 1);
            Grid.SetColumn(bordAP, 2);
            Grid.SetRow(bordAP, 2);
            grdMain.Children.Add(bordAP);

            Border bordC = new Border();
            bordC.BorderBrush = Brushes.Black;
            bordC.Background = Brushes.DarkKhaki;
            bordC.BorderThickness = new Thickness(2, 1, 0, 0);
            Grid.SetColumn(bordC, 2);
            Grid.SetRow(bordC, 3);
            grdMain.Children.Add(bordC);

            //------------------------------------------Création Vue Gauche------------------------------------------ 
            StackPanel selectionAffichage = new StackPanel();

            Grid.SetColumn(selectionAffichage, 0);
            Grid.SetRow(selectionAffichage, 1);
            Grid.SetRowSpan(selectionAffichage, 4);

            //Bouton catalogue
            sql.AllTypePrduit(out DataTable data, out int nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btnCatalogue = new Button();
                btnCatalogue.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                btnCatalogue.Height = 80;
                btnCatalogue.Width = 218;
                btnCatalogue.HorizontalAlignment = HorizontalAlignment.Left;
                btnCatalogue.FontWeight = FontWeights.Bold;
                btnCatalogue.FontSize = 20;
                btnCatalogue.Background = Brushes.Yellow;
                btnCatalogue.Click += new RoutedEventHandler(Click_catalogue);
                if (data.Rows[i]["libelle"].ToString() == "pizza")
                {
                    btnCatalogue.Margin = new Thickness(0, 1, 0, 0);
                }

                    selectionAffichage.Children.Add(btnCatalogue);
            }

            Button btnDéconnexion = new Button();
            btnDéconnexion.Content = "DÉCONNEXION";
            btnDéconnexion.Background = Brushes.Red;
            btnDéconnexion.FontWeight = FontWeights.Bold;
            btnDéconnexion.Click += new RoutedEventHandler(Click_Exit);
            btnDéconnexion.Height = 58;
            btnDéconnexion.Width = 219;
            btnDéconnexion.VerticalAlignment = VerticalAlignment.Top;
            btnDéconnexion.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(btnDéconnexion, 0);
            Grid.SetRow(btnDéconnexion, 0);
            //-------------------------------------------------------------------------------------------------------
            //------------------------------------------Création Vue Milieu------------------------------------------ 
            WrapPanel taillePizza = new WrapPanel();
            WrapPanel typePate = new WrapPanel();
            WrapPanel suggestion = new WrapPanel();
            WrapPanel pain = new WrapPanel();
            WrapPanel snacking = new WrapPanel();
            WrapPanel salade = new WrapPanel();
            WrapPanel dipy = new WrapPanel();
            WrapPanel dessert = new WrapPanel();
            WrapPanel boisson = new WrapPanel();
            WrapPanel ingrédient = new WrapPanel();

            RadioButton allPizza = new RadioButton();
            RadioButton traditionellePizza = new RadioButton();
            RadioButton spécialPizza = new RadioButton();
            RadioButton blanchePizza = new RadioButton();
            RadioButton merPizza = new RadioButton();
            RadioButton radioTailleMédium = new RadioButton();
            RadioButton radioTailleLarge = new RadioButton();
            RadioButton radioPateRigatoni = new RadioButton();
            RadioButton radioPateTagliatelle = new RadioButton();
            RadioButton radioPateSpaghetti  = new RadioButton();
            RadioButton radioPateTortellini = new RadioButton();

            //Bouton Droite Spécifique
            Button btnIngrédient = new Button();
            pizza.Background = Brushes.DarkKhaki;
            Grid.SetColumn(pizza, 1);
            Grid.SetRow(pizza, 1);
            Grid.SetRowSpan(pizza, 3);


            Grid.SetColumn(taillePizza, 1);
            Grid.SetRow(taillePizza, 0);

            pate.Background = Brushes.DarkKhaki;
            pate.Visibility = Visibility.Collapsed;
            Grid.SetColumn(pate, 1);
            Grid.SetRow(pate, 1);
            Grid.SetRowSpan(pate, 3);

            typePate.Visibility = Visibility.Collapsed;
            Grid.SetColumn(typePate, 1);
            Grid.SetRow(typePate, 0);

            suggestion.Background = Brushes.DarkKhaki;
            suggestion.Visibility = Visibility.Collapsed;
            Grid.SetColumn(suggestion, 1);
            Grid.SetRow(suggestion, 1);
            Grid.SetRowSpan(suggestion, 3);

            pain.Background = Brushes.DarkKhaki;
            pain.Visibility = Visibility.Collapsed;
            Grid.SetColumn(pain, 1);
            Grid.SetRow(pain, 1);
            Grid.SetRowSpan(pain, 3);

            snacking.Background = Brushes.DarkKhaki;
            snacking.Visibility = Visibility.Collapsed;
            Grid.SetColumn(snacking, 1);
            Grid.SetRow(snacking, 1);
            Grid.SetRowSpan(snacking, 3);

            salade.Background = Brushes.DarkKhaki;
            salade.Visibility = Visibility.Collapsed;
            Grid.SetColumn(salade, 1);
            Grid.SetRow(salade, 1);
            Grid.SetRowSpan(salade, 3);
            
            dipy.Background = Brushes.DarkKhaki;
            dipy.Visibility = Visibility.Collapsed;
            Grid.SetColumn(dipy, 1);
            Grid.SetRow(dipy, 1);
            Grid.SetRowSpan(dipy, 3);

            dessert.Background = Brushes.DarkKhaki;
            dessert.Visibility = Visibility.Collapsed;
            Grid.SetColumn(dessert, 1);
            Grid.SetRow(dessert, 1);
            Grid.SetRowSpan(dessert, 3);

            boisson.Background = Brushes.DarkKhaki;
            boisson.Visibility = Visibility.Collapsed;
            Grid.SetColumn(boisson, 1);
            Grid.SetRow(boisson, 1);
            Grid.SetRowSpan(boisson, 3);

            ingrédient.Background = Brushes.DarkKhaki;
            ingrédient.Visibility = Visibility.Collapsed;
            Grid.SetColumn(ingrédient, 1);
            Grid.SetRow(ingrédient, 1);
            Grid.SetRowSpan(ingrédient, 3);

            
            //All Bouton salade
            sql.AllProduitByTypeProduit(3, out data, out nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btn = new Button();
                btn.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                btn.Tag = i + 1;
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Yellow;
                btn.FontWeight = FontWeights.Bold;
                btn.Margin = new Thickness(5);
                btn.Click += new RoutedEventHandler(Click_pizza);
                salade.Children.Add(btn);
            }
            //All Bouton pain
            sql.AllProduitByTypeProduit(4, out data, out nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btn = new Button();
                btn.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                btn.Tag = i + 1;
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Yellow;
                btn.FontWeight = FontWeights.Bold;
                btn.Margin = new Thickness(5);
                btn.Click += new RoutedEventHandler(Click_pizza);
                pain.Children.Add(btn);
            }
            //All Bouton snacking
            sql.AllProduitByTypeProduit(5, out data, out nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btn = new Button();
                btn.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                btn.Tag = i + 1;
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Yellow;
                btn.FontWeight = FontWeights.Bold;
                btn.Margin = new Thickness(5);
                btn.Click += new RoutedEventHandler(Click_pizza);
                snacking.Children.Add(btn);
            }
            //All Bouton dessert
            sql.AllProduitByTypeProduit(6, out data, out nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btn = new Button();
                btn.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                btn.Tag = i + 1;
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Yellow;
                btn.FontWeight = FontWeights.Bold;
                btn.Margin = new Thickness(5);
                btn.Click += new RoutedEventHandler(Click_pizza);
                dessert.Children.Add(btn);
            }
            //All Bouton boisson
            sql.AllProduitByTypeProduit(7, out data, out nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btn = new Button();
                btn.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                btn.Tag = i + 1;
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Yellow;
                btn.FontWeight = FontWeights.Bold;
                btn.Margin = new Thickness(5);
                btn.Click += new RoutedEventHandler(Click_pizza);
                boisson.Children.Add(btn);
            }
            //All Bouton dipy
            sql.AllProduitByTypeProduit(8, out data, out nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btn = new Button();
                btn.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                btn.Tag = i + 1;
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Yellow;
                btn.FontWeight = FontWeights.Bold;
                btn.Margin = new Thickness(5);
                btn.Click += new RoutedEventHandler(Click_pizza);
                dipy.Children.Add(btn);
            }
            //All Bouton suggestion
            sql.AllProduitByTypeProduit(9, out data, out nbrRow);
            for (int i = 0; i < nbrRow; i++)
            {
                Button btn = new Button();
                btn.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                btn.Tag = i + 1;
                btn.Width = 100;
                btn.Height = 100;
                btn.Background = Brushes.Yellow;
                btn.FontWeight = FontWeights.Bold;
                btn.Margin = new Thickness(5);
                btn.Click += new RoutedEventHandler(Click_pizza);
                suggestion.Children.Add(btn);
            }

            ControlTemplate radioStyle = VisuelCSS.CreateButtonStyleTemplate();

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

            radioTailleMédium.Content = "RÉGULAR";
            radioTailleMédium.FontWeight = FontWeights.Bold;
            radioTailleMédium.GroupName = "taillePizza";
            //radioTailleMédium.Checked = TaillePizza_Checked;
            radioTailleMédium.Margin = new Thickness(200,5,5,5);
            radioTailleMédium.Template = radioStyle;
            radioTailleMédium.Width = 100;
            radioTailleMédium.Height = 40;
            radioTailleMédium.IsChecked = true;
            radioTailleMédium.HorizontalAlignment = HorizontalAlignment.Right;

            radioTailleLarge.Content = "LARGE";
            radioTailleLarge.FontWeight = FontWeights.Bold;
            radioTailleLarge.GroupName = "taillePizza";
            //radioTailleLarge.Checked = TaillePizza_Checked;
            radioTailleLarge.Margin = new Thickness(5);
            radioTailleLarge.Template = radioStyle;
            radioTailleLarge.Width = 100;
            radioTailleLarge.Height = 40;
            radioTailleLarge.HorizontalAlignment = HorizontalAlignment.Right;

            taillePizza.Children.Add(radioTailleMédium);
            taillePizza.Children.Add(radioTailleLarge);

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

            typePate.Children.Add(radioPateRigatoni);
            typePate.Children.Add(radioPateTagliatelle);
            typePate.Children.Add(radioPateSpaghetti);
            typePate.Children.Add(radioPateTortellini);
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
            catalogueWrap.Add("PAIN", pain);
            catalogueWrap.Add("SALADE", salade);
            catalogueWrap.Add("DESSERT", dessert);
            catalogueWrap.Add("BOISSON", boisson);
            catalogueWrap.Add("DIPY", dipy);
            catalogueWrap.Add("SNACKING", snacking);

            foreach (var wrap in catalogueWrap.Values)
            {
                grdMain.Children.Add(wrap);
            }
            grdMain.Children.Add(selectionAffichage);
            grdMain.Children.Add(btnDéconnexion);
            //-------------------------------------------------------------------------------------------------------
        }



        public void Click_pizza(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
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
            catalogueWrap[categori].Visibility = Visibility.Visible;

        }

        public void Click_Exit(object sender, RoutedEventArgs e)
        {
            PagePrincipale.MainInfo.Visibility = Visibility.Visible;
            PagePrincipale.MainInfo.Content = new Déconnexion();
        }

        public void Pizza_Checked(object sender, RoutedEventArgs e)
        {
            pizza.Children.Clear();
            RadioButton rb = (RadioButton)sender;
            if (rb.Content == "ALL")
            {
                sql.AllProduitByTypeProduit(1, out DataTable data, out int nbrRow);
                for (int i = 0; i < nbrRow; i++)
                {
                    Button btn = new Button();
                    btn.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                    btn.Tag = i + 1;
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Yellow;
                    btn.FontWeight = FontWeights.Bold;
                    btn.Margin = new Thickness(5);
                    btn.Click += new RoutedEventHandler(Click_pizza);
                    pizza.Children.Add(btn);
                }
            }
            else if (rb.Content == "TRADITIONELLE")
            {
                sql.AllPizzaByTypeProduit(1, out DataTable data, out int nbrRow);
                for (int i = 0; i < nbrRow; i++)
                {
                    Button btn = new Button();
                    btn.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                    btn.Tag = i + 1;
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Yellow;
                    btn.FontWeight = FontWeights.Bold;
                    btn.Margin = new Thickness(5);
                    btn.Click += new RoutedEventHandler(Click_pizza);
                    pizza.Children.Add(btn);
                }
            }
            else if (rb.Content == "SPÉCIALE")
            {
                sql.AllPizzaByTypeProduit(2, out DataTable data, out int nbrRow);
                for (int i = 0; i < nbrRow; i++)
                {
                    Button btn = new Button();
                    btn.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                    btn.Tag = i + 1;
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Yellow;
                    btn.FontWeight = FontWeights.Bold;
                    btn.Margin = new Thickness(5);
                    btn.Click += new RoutedEventHandler(Click_pizza);
                    pizza.Children.Add(btn);
                }
            }
            else if (rb.Content == "BLANCHE")
            {
                sql.AllPizzaByTypeProduit(3, out DataTable data, out int nbrRow);
                for (int i = 0; i < nbrRow; i++)
                {
                    Button btn = new Button();
                    btn.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                    btn.Tag = i + 1;
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Yellow;
                    btn.FontWeight = FontWeights.Bold;
                    btn.Margin = new Thickness(5);
                    btn.Click += new RoutedEventHandler(Click_pizza);
                    pizza.Children.Add(btn);
                }
            }
            else if (rb.Content == "DE LA MER")
            {
                sql.AllPizzaByTypeProduit(4, out DataTable data, out int nbrRow);
                for (int i = 0; i < nbrRow; i++)
                {
                    Button btn = new Button();
                    btn.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                    btn.Tag = i + 1;
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Yellow;
                    btn.FontWeight = FontWeights.Bold;
                    btn.Margin = new Thickness(5);
                    btn.Click += new RoutedEventHandler(Click_pizza);
                    pizza.Children.Add(btn);
                }
            }
            
        }

        public void Pate_Checked(object sender, RoutedEventArgs e)
        {
            pate.Children.Clear();
            RadioButton rb = (RadioButton)sender;
            if (rb.Content == "TORTELLINI")
            {
                string libelle = "Tomate crème";
                for (int i = 0; i < 2; i++)
                {
                    sql.ProduitByLibelle(libelle, out DataTable data);
                    Button btn = new Button();
                    btn.Content = data.Rows[0]["libelle"].ToString().ToUpper();
                    btn.Tag = i + 1;
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Yellow;
                    btn.FontWeight = FontWeights.Bold;
                    btn.Margin = new Thickness(5);
                    btn.Click += new RoutedEventHandler(Click_pizza);
                    pate.Children.Add(btn);
                    libelle = "Jambon crème";
                }
            }
            else
            {
                //All Bouton pate
                sql.AllProduitByTypeProduit(2, out DataTable data, out int nbrRow);
                for (int i = 0; i < nbrRow; i++)
                {
                    Button btn = new Button();
                    btn.Content = data.Rows[i]["libelle"].ToString().ToUpper();
                    btn.Tag = i + 1;
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.Background = Brushes.Yellow;
                    btn.FontWeight = FontWeights.Bold;
                    btn.Margin = new Thickness(5);
                    btn.Click += new RoutedEventHandler(Click_pizza);
                    pate.Children.Add(btn);
                }
            }
        }
    }
}
