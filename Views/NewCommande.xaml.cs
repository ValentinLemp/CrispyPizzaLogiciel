using CrispyLo.Classes;
using CrispyLo.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CrispyLo.Views
{
    /// <summary>
    /// Logique d'interaction pour NewCommande.xaml
    /// </summary>
    public partial class NewCommande : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        ControlTemplate radioStyle = VisuelCSS.CreateButtonStyleTemplate();

        public NewCommande()
        {
            InitializeComponent();
            PrepareInitialize();
        }
        StackPanel panelGénéral = new StackPanel();

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
        public void PrepareInitialize()
        {
            //this.Background = Brushes.White;

            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grdMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(76) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grdMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            StackPanel panelCouleur = new StackPanel();
            panelCouleur.Background = Brushes.White;
            Grid.SetColumn(panelCouleur, 0);
            Grid.SetRow(panelCouleur, 1);
            Grid.SetRowSpan(panelCouleur, 2);
            grdMain.Children.Add(panelCouleur);

            WrapPanel typeCommande = new WrapPanel();
            Grid.SetColumn(typeCommande, 0);
            Grid.SetRow(typeCommande, 0);

            encodage.Height = 260;
            encodage.VerticalAlignment = VerticalAlignment.Bottom;
            encodage.HorizontalAlignment = HorizontalAlignment.Stretch;
            Grid.SetColumn(encodage, 0);
            Grid.SetRow(encodage, 1);
            encodage.Background = Brushes.Transparent;

            WrapPanel heure = new WrapPanel();
            Grid.SetColumn(heure, 1);
            Grid.SetRow(heure, 1);
            heure.Background = Brushes.AliceBlue;

            WrapPanel resume = new WrapPanel();
            Grid.SetColumn(resume, 1);
            Grid.SetRow(resume, 2);
            resume.Background = Brushes.Khaki;

            RadioButton rdEmporter = new RadioButton();
            RadioButton rdLivraison = new RadioButton();

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
            Localite.Margin = new Thickness(0, 0, 30, 0);
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
            Commentaire.Width = 500;
            Commentaire.Height = 100;
            Commentaire.FontSize = 15;
            panel5.Children.Add(commentaire);
            panel5.Children.Add(Commentaire);
            panel5.VerticalAlignment = VerticalAlignment.Bottom;
            panel5.HorizontalAlignment = HorizontalAlignment.Left;
            panel5.Margin = new Thickness(20, 0, 0, 0);
            Grid.SetColumn(panel5, 0);
            Grid.SetRow(panel5, 1);
            grdMain.Children.Add(panel5);

            grdMain.Children.Add(resume);
            grdMain.Children.Add(heure);
            grdMain.Children.Add(typeCommande);
            grdMain.Children.Add(encodage);

            Horloge();
            Heure();
            grdMain.Children.Add(panelGénéral);
        }

        public void Horloge()
        {
            Grid.SetColumn(panelGénéral, 1);
            Grid.SetRow(panelGénéral, 1);

            WrapPanel wrap = new WrapPanel();
            TextBlock date = new TextBlock();
            DatePicker Date = new DatePicker();

            date.Text = "Date prévu : ";
            date.Width = 100;
            date.Height = 25;
            date.FontWeight = FontWeights.Bold;
            date.FontSize = 15;
            date.Margin = new Thickness(0, 0, 10, 0);
            Date.SelectedDate = DateTime.Now;
            Date.Width = 115;
            Date.Height = 25;
            wrap.HorizontalAlignment = HorizontalAlignment.Right;
            wrap.VerticalAlignment = VerticalAlignment.Top;
            wrap.Margin = new Thickness(20);
            wrap.Children.Add(date);
            wrap.Children.Add(Date);
            panelGénéral.Children.Add(wrap);
        }

        public void Heure()
        {
            WrapPanel wrap = new WrapPanel();
            TextBlock heure = new TextBlock();
            TextBox Heure = new TextBox();

            heure.Text = "Heure prévue :";
            heure.Width = 110;
            heure.Height = 25;
            heure.FontWeight = FontWeights.Bold;
            heure.FontSize = 15;
            heure.Margin = new Thickness(0, 0, 10, 0);
            Heure.Text = DateTime.Now.ToString("HH:mm");
            Heure.Width = 115;
            Heure.Height = 25;
            Heure.FontSize = 15;
            Heure.FontWeight= FontWeights.Bold;
            Heure.BorderThickness = new Thickness(0);
            Heure.Background = Brushes.Transparent;
            wrap.HorizontalAlignment = HorizontalAlignment.Right;
            wrap.VerticalAlignment = VerticalAlignment.Top;
            wrap.Margin = new Thickness(20);
            wrap.Children.Add(heure);
            wrap.Children.Add(Heure);
            panelGénéral.Children.Add(wrap);

            StackPanel panel = new StackPanel();
            panel.HorizontalAlignment = HorizontalAlignment.Right;
            panel.Margin = new Thickness(0,0,80,0);

            WrapPanel ligne = new WrapPanel();
            Button btn = new Button();
            btn.Content = "11";
            btn.Height = 50;
            btn.Width = 50;
            btn.Margin = new Thickness(3);
            btn.Click += new RoutedEventHandler(Click_Heure);
            ligne.Children.Add(btn);

            Button btn1 = new Button();
            btn1.Content = "12";
            btn1.Height = 50;
            btn1.Width = 50;
            btn1.Margin = new Thickness(3,3,15,3);
            btn1.Click += new RoutedEventHandler(Click_Heure);
            ligne.Children.Add(btn1);

            Button btn2 = new Button();
            btn2.Content = "00";
            btn2.Height = 50;
            btn2.Width = 50;
            btn2.Margin = new Thickness(3);
            btn2.Foreground = Brushes.Violet;
            btn2.Click += new RoutedEventHandler(Click_Minute);
            ligne.Children.Add(btn2);

            panel.Children.Add(ligne);

            WrapPanel ligne1 = new WrapPanel();
            Button btn3 = new Button();
            btn3.Content = "13";
            btn3.Height = 50;
            btn3.Width = 50;
            btn3.Margin = new Thickness(3);
            btn3.Click += new RoutedEventHandler(Click_Heure);
            ligne1.Children.Add(btn3);

            Button btn4 = new Button();
            btn4.Content = "14";
            btn4.Height = 50;
            btn4.Width = 50;
            btn4.Margin = new Thickness(3, 3, 15, 3);
            btn4.Click += new RoutedEventHandler(Click_Heure);
            ligne1.Children.Add(btn4);

            Button btn5 = new Button();
            btn5.Content = "15";
            btn5.Height = 50;
            btn5.Width = 50;
            btn5.Margin = new Thickness(3);
            btn5.Foreground = Brushes.Violet;
            btn5.Click += new RoutedEventHandler(Click_Minute);
            ligne1.Children.Add(btn5);

            panel.Children.Add(ligne1);

            WrapPanel ligne2 = new WrapPanel();
            Button btn6 = new Button();
            btn6.Content = "18";
            btn6.Height = 50;
            btn6.Width = 50;
            btn6.Margin = new Thickness(3);
            btn6.Click += new RoutedEventHandler(Click_Heure);
            ligne2.Children.Add(btn6);

            Button btn7 = new Button();
            btn7.Content = "19";
            btn7.Height = 50;
            btn7.Width = 50;
            btn7.Margin = new Thickness(3, 3, 15, 3);
            btn7.Click += new RoutedEventHandler(Click_Heure);
            ligne2.Children.Add(btn7);

            Button btn8 = new Button();
            btn8.Content = "30";
            btn8.Height = 50;
            btn8.Width = 50;
            btn8.Margin = new Thickness(3);
            btn8.Foreground = Brushes.Violet;
            btn8.Click += new RoutedEventHandler(Click_Minute);
            ligne2.Children.Add(btn8);

            panel.Children.Add(ligne2);

            WrapPanel ligne3 = new WrapPanel();
            Button btn9 = new Button();
            btn9.Content = "20";
            btn9.Height = 50;
            btn9.Width = 50;
            btn9.Margin = new Thickness(3);
            btn9.Click += new RoutedEventHandler(Click_Heure);
            ligne3.Children.Add(btn9);

            Button btn10 = new Button();
            btn10.Content = "21";
            btn10.Height = 50;
            btn10.Width = 50;
            btn10.Margin = new Thickness(3, 3, 15, 3);
            btn10.Click += new RoutedEventHandler(Click_Heure);
            ligne3.Children.Add(btn10);

            Button btn11 = new Button();
            btn11.Content = "45";
            btn11.Height = 50;
            btn11.Width = 50;
            btn11.Margin = new Thickness(3);
            btn11.Foreground = Brushes.Violet;
            btn11.Click += new RoutedEventHandler(Click_Minute);
            ligne3.Children.Add(btn11);

            panel.Children.Add(ligne3);

            panelGénéral.Children.Add(panel);
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
        }
        public void Click_Heure(object sender, RoutedEventArgs e)
        {
        }
    }
}
