using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//permet de voir cette page
using CrispyLo.Views;
using CrispyLo.Classes;
using CrispyLo.Views.ComInternet;

namespace CrispyLo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Restaurant { get; set; }
        public List<Produit> Panier { get; set; } = new List<Produit>();
        public int IdCommande { get; set; }
        public bool EstAdmin { get; set; }
        public MediaPlayer SonNotification { get; set; } = new MediaPlayer();
        public String ChemminDB { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            PrepareInitialize();
        }

        public void PrepareInitialize()
        {
            IdCommande = -1;
            EstAdmin = false;
            string cheminSon = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sound", "myinstants.mp3");
            SonNotification.Open(new Uri(cheminSon));
            Main.Content = new Login();
            RechercheCommande.Content = new RechercheCommande();
        }
    }
}