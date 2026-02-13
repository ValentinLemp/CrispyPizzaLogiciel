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

namespace CrispyLo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Restaurent { get; set; }
        public List<Produit> Panier { get; set; } = new List<Produit>();
        public MainWindow()
        {
            InitializeComponent();
            PrepareInitialize();
        }

        public void PrepareInitialize()
        {
            //Main.Content = new Home();
            Main.Content = new Login();
        }
    }
}