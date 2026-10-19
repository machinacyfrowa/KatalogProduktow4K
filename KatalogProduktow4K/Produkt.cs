using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogProduktow4K
{
    public class Produkt
    {
        public string Nazwa { get; set; } = "";
        private double cena;
        public double Cena
        {
            //jeśli ktoś chce odczytać cenę, zwracamy jej wartość
            get { return cena; }
            // Cena nie może być ujemna, więc jeśli ktoś spróbuje ustawić wartość ujemną, ustawiamy ją na 0
            set { cena = value < 0 ? 0 : value; }
        }
        public int Ilosc { get; set; }
        public string Kategoria { get; set; } = "";
        //nadpisujemy domyślną metodę ToString, aby zwracała czytelny opis produktu
        public override string ToString()
        {
            return $"{Nazwa} ({Kategoria}): {Cena:F2} zł, ilość: {Ilosc}";
        }
    }
}
