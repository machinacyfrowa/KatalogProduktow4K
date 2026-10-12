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

namespace KatalogProduktow4K
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            KategorieComboBox.ItemsSource = new string[] { "Elektronika", "Odzież", "Książki", "Meble" };
            KategorieComboBox.SelectedIndex = 0;
        }

        private void Oblicz(object sender, RoutedEventArgs e)
        {
            //TODO: pobierz tekst z pola z ceną i zmień go na liczbę
            //TODO: pobierz tekst z pola z ilością i zmień go na liczbę
            //TODO: Jeśli wartości są niepoprawne lub ujemne wypisz komunikat w polu wynik albo rzuć wyjątek
            //TODO: oblicz wartość zamówienia i wypisz ją w polu wynik

            //double cena = double.Parse(CenaTextBox.Text);
            //jeżeli tryparse sie nie powiedzie to zmien komuniat na polu wynik
            //i wyjdź z metody Oblicz()
            if (!double.TryParse(CenaTextBox.Text, out double cena))
            {
                WynikTextBlock.Text = "Niepoprawna wartość ceny";
                return;
            }
            //tak samo z ilością
            if (!int.TryParse(IloscTextBox.Text, out int ilosc))
            {
                WynikTextBlock.Text = "Niepoprawna wartość ilości";
                return;
            }
            double wartoscZamowienia = cena * ilosc;
            WynikTextBlock.Text = wartoscZamowienia.ToString();
        }
    }
}