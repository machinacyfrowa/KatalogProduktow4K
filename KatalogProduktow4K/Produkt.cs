using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KatalogProduktow4K
{
    public class Produkt : INotifyPropertyChanged
    {
        public string Nazwa { get; set; } = "";
        private double cena;
        public double Cena
        {
            //jeśli ktoś chce odczytać cenę, zwracamy jej wartość
            get { return cena; }
            // Cena nie może być ujemna, więc jeśli ktoś spróbuje ustawić wartość ujemną, ustawiamy ją na 0
            set { cena = value < 0 ? 0 : value; Zmiana(nameof(Cena)); }
        }
        private int ilosc;
        public int Ilosc
        {
            get { return ilosc; }
            set { ilosc = value < 0 ? 0 : value; Zmiana(nameof(Ilosc)); }
        }
        public string Kategoria { get; set; } = "";
        //nadpisujemy domyślną metodę ToString, aby zwracała czytelny opis produktu
        //public override string ToString()
        //{
        //    return $"{Nazwa} ({Kategoria}): {Cena:F2} zł, ilość: {Ilosc}";
        //}
        public event PropertyChangedEventHandler? PropertyChanged;

        private void Zmiana(string nazwaWlasciwosci)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nazwaWlasciwosci));
        }
    }
}
