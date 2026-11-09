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
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace KatalogProduktow4K
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //jeżeli potrzebujemy skorzystać z czegoś przez data binding, to musi publiczne i 
        //musi być własnością (property), a nie polem (field) - musi mieć get i set
        public ObservableCollection<Produkt> produkty { get; set; } = new ObservableCollection<Produkt>();
        public MainWindow()
        {
            InitializeComponent();
            KategorieComboBox.ItemsSource = new string[] { "Elektronika", "Odzież", "Książki", "Meble" };
            KategorieComboBox.SelectedIndex = 0;
            produkty.Add(new Produkt { Nazwa = "Laptop", Cena = 3500, Ilosc = 5, Kategoria = "Elektronika" });
            produkty.Add(new Produkt { Nazwa = "Koszula", Cena = 120, Ilosc = 10, Kategoria = "Odzież" });
            //ListaProduktow.ItemsSource = produkty;
            //zamiast przypisywać itemsource możemmy ustalić kontekst danych dla całego okna,
            //wtedy wszystkie kontrolki w tym oknie mogą korzystać z tego kontekstu
            //i przez to czytać dane z właściwości tego okna (czyli z MainWindow)
            //takie jak public ObservableCollection<Produkt> produkty { get; set; }
            DataContext = this;
        }

        private void Oblicz(object sender, RoutedEventArgs e)
        {
            // || cena < 0 — TryParse przepuści -5, więc ujemne trzeba odrzucić osobno
            if (!double.TryParse(CenaTextBox.Text, out double cena) || cena < 0)
            {
                WynikTextBlock.Text = "Niepoprawna wartość ceny";
                return;
            }
            if (!int.TryParse(IloscTextBox.Text, out int ilosc) || ilosc < 0)
            {
                WynikTextBlock.Text = "Niepoprawna wartość ilości";
                return;
            }

            // ItemsSource to tablica string, więc SelectedItem jest stringiem (albo null)
            string kategoria = KategorieComboBox.SelectedItem as string ?? "brak kategorii";

            double wartoscZamowienia = cena * ilosc;
            // IsChecked jest typu bool? (trzy stany), stąd porównanie z true
            if (PromocjaCheckBox.IsChecked == true)
            {
                wartoscZamowienia *= 0.9;   // promocja -10%
            }

            // F2 = dwa miejsca po przecinku, separator zgodny z ustawieniami systemu
            WynikTextBlock.Text = $"{NazwaTextBox.Text} ({kategoria}): {wartoscZamowienia:F2} zł";
        }

        private void DodajProdukt(object sender, RoutedEventArgs e)
        {
            string nazwa = NazwaTextBox.Text;
            double cena = double.Parse(CenaTextBox.Text);
            int ilosc = int.Parse(IloscTextBox.Text);
            string kategoria = KategorieComboBox.SelectedItem as string ?? "brak kategorii";
            
            produkty.Add(new Produkt { Nazwa = nazwa, Cena = cena, Ilosc = ilosc, Kategoria = kategoria});

        }

        private void UsunProdukt(object sender, RoutedEventArgs e)
        {
            if(ListaProduktow.SelectedItem is Produkt wybranyProdukt)
            {
                MessageBoxResult wynik = 
                    MessageBox.Show($"Czy na pewno chcesz usunąć produkt: {wybranyProdukt.Nazwa}?", 
                    "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(wynik == MessageBoxResult.Yes)
                {
                    produkty.Remove(wybranyProdukt);
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano produktu do usunięcia.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PokazZaznaczony(object sender, SelectionChangedEventArgs e)
        {
            if(ListaProduktow.SelectedItem is not Produkt p) return;
            //tego juz nie musimy sprawdzać, bo w if sprawdzamy czy jest to Produkt
            //Produkt p = ListaProduktow.SelectedItem as Produkt;
            NazwaTextBox.Text = p.Nazwa;
            CenaTextBox.Text = p.Cena.ToString("F2");
            IloscTextBox.Text = p.Ilosc.ToString();
        }

        private void Zapisz(object sender, RoutedEventArgs e)
        {
            //sprawdz czy mamy prawidłowo zaznaczony na liście produkt,
            //bo jeśli nie to nie zapisujemy
            if(ListaProduktow.SelectedItem is not Produkt p) return;
            //po drugie parsujemy cene i ilosc, bo to są stringi w TextBoxach
            if (!double.TryParse(CenaTextBox.Text, out double cena) || cena < 0) return;
            if (!int.TryParse(IloscTextBox.Text, out int ilosc) || ilosc < 0) return;
            string nazwa = NazwaTextBox.Text;
            //aktualizujemy właściwości obiektu Produkt, który jest w kolekcji ObservableCollection
            p.Nazwa = nazwa;
            p.Cena = cena;
            p.Ilosc = ilosc;
            WynikTextBlock.Text = $"Zapisano zmiany dla produktu: {p.Nazwa}";
        }
    }
}