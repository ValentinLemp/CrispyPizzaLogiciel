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
using CrispyLo.Classes;

namespace CrispyLo.Views.Info
{
    /// <summary>
    /// Logique d'interaction pour WriteNote.xaml
    /// </summary>
    public partial class WriteNote : Page
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
        int Index = 0;
        TextBox text = new TextBox();


        public WriteNote(int index)
        {
            InitializeComponent();
            PrepareInitialize(index);
        }
        public void PrepareInitialize(int index)
        {
            this.Width = 600;
            this.Height = 300;
            this.Background = Brushes.LightSlateGray;

            Index = index;

            TextBlock textBlock = new TextBlock();
            Button quitter = new Button();
            Button enregistrer = new Button();
            Button supprimer = new Button();

            textBlock.Text = "Note";
            textBlock.FontWeight = FontWeights.Black;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Top;
            textBlock.FontSize = 25;
            textBlock.Margin = new Thickness(0,20,0,0);
            textBlock.Background = Brushes.Transparent;
            Grid.SetColumnSpan(textBlock, 2);
            Grid.SetRow(textBlock, 0);

            text.Height = 40;
            text.Width = 500;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            Grid.SetColumnSpan(text, 2);
            Grid.SetRow(text, 0);

            quitter.Content = "X";
            quitter.FontWeight = FontWeights.Bold;
            quitter.Height = 40;
            quitter.Width = 40;
            quitter.Background = Brushes.Red;
            quitter.Foreground = Brushes.White;
            quitter.Click += new RoutedEventHandler(Click_Quitter);
            quitter.HorizontalAlignment = HorizontalAlignment.Right;
            quitter.VerticalAlignment = VerticalAlignment.Top;
            quitter.Margin = new Thickness(10);
            Grid.SetColumn(quitter, 1);
            Grid.SetRow(quitter, 0);

            enregistrer.Content = "Enregistrer";
            enregistrer.FontWeight = FontWeights.Bold;
            enregistrer.Height = 40;
            enregistrer.Width = 80;
            enregistrer.Click += new RoutedEventHandler(Click_Enregistrer);
            enregistrer.HorizontalAlignment = HorizontalAlignment.Center;
            enregistrer.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetColumn(enregistrer, 1);
            Grid.SetRow(enregistrer, 1);

            supprimer.Content = "supprimer";
            supprimer.FontWeight = FontWeights.Bold;
            supprimer.Height = 40;
            supprimer.Width = 80;
            supprimer.Background = Brushes.Red;
            supprimer.Foreground = Brushes.White;
            supprimer.Click += new RoutedEventHandler(Click_Supprimer);
            supprimer.VerticalAlignment = VerticalAlignment.Top;
            supprimer.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumn(supprimer, 0);
            Grid.SetRow(supprimer, 1);

            if (PagePrincipale.Panier[Index].Note == null)
            {
                supprimer.Visibility = Visibility.Collapsed;
            }
            else
            {
                text.Text = PagePrincipale.Panier[Index].Note.Substring(4);
            }

            grdMain.Children.Add(textBlock);
            grdMain.Children.Add(text);
            grdMain.Children.Add(enregistrer);
            grdMain.Children.Add(supprimer);
            grdMain.Children.Add(quitter);
        }

        public void Click_Enregistrer(object sender, RoutedEventArgs e)
        {
            if (text.Text != "")
            {
                PagePrincipale.Panier[Index].Note = "--> " + text.Text;
                PagePrincipale.MainInfo.Visibility = Visibility.Collapsed;
            }
        }
        public void Click_Supprimer(object sender, RoutedEventArgs e)
        {
            PagePrincipale.Panier[Index].Note = null;
            PagePrincipale.MainInfo.Visibility = Visibility.Collapsed;
        }
        public void Click_Quitter(object sender, RoutedEventArgs e)
        {
            PagePrincipale.MainInfo.Visibility = Visibility.Collapsed;
        }
    }
}
