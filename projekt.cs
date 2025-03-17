using System;
using System.Collections.Generic;
using System.IO;

namespace ParkingSystem
{
    class Program      // pierwsza klasa
    {
        static void Main(string[] args)
        {
            AplikacjaParkingowa app = new AplikacjaParkingowa();
            app.Uruchom();
        }
    }
    
    abstract class Pojazd         // 2 klasa dziedziczna
    {
        public string NumerRejestracyjny { get; set; }
        public string Marka { get; set; }
        
        protected Pojazd(string numerRejestracyjny, string marka)
        {
            NumerRejestracyjny = numerRejestracyjny;
            Marka = marka;
        }
    }
    
    class Samochod : Pojazd          // dziedziczna do 2 klasy
    {
        public Samochod(string numerRejestracyjny, string marka) : base(numerRejestracyjny, marka) { }
    }
    
    class Motocykl : Pojazd    // dziedziczna do 2 klasy
    {
        public Motocykl(string numerRejestracyjny, string marka) : base(numerRejestracyjny, marka) { }
    }
    
    class MiejsceParkingowe         // 3 klasa
    {
        public int Numer { get; set; }
        public Pojazd? ZajetyPrzez { get; set; }
    }
    
    class Parking               // 4 klasa
    {
        private List<MiejsceParkingowe> miejsca = new List<MiejsceParkingowe>();
        
        public Parking(int liczbaMiejsc)
        {
            for (int i = 1; i <= liczbaMiejsc; i++)
            {
                miejsca.Add(new MiejsceParkingowe { Numer = i, ZajetyPrzez = null });
            }
        }
        
        public bool ParkujPojazd(Pojazd pojazd)
        {
            foreach (var miejsce in miejsca)
            {
                if (miejsce.ZajetyPrzez == null)
                {
                    miejsce.ZajetyPrzez = pojazd;
                    return true;
                }
            }
            return false;
        }
        
        public bool ZwolnijMiejsce(int numerMiejsca)
        {
            var miejsce = miejsca.Find(m => m.Numer == numerMiejsca);
            if (miejsce != null && miejsce.ZajetyPrzez != null)
            {
                miejsce.ZajetyPrzez = null;
                return true;
            }
            return false;
        }
        
        public void WyswietlStanParkingowy()
{
    foreach (var miejsce in miejsca)
    {
        if (miejsce.ZajetyPrzez != null)
        {
            Console.WriteLine($"Miejsce {miejsce.Numer}: {miejsce.ZajetyPrzez.Marka} {miejsce.ZajetyPrzez.NumerRejestracyjny}");
        }
        else
        {
            Console.WriteLine($"Miejsce {miejsce.Numer}: wolne");
        }
    }
}
    }
    
    class PlikManager      // 5 klasa
    {
        private const string Sciezka = "parking_data.txt";
        
        public static void ZapiszDane(Parking parking)
        {
            using (StreamWriter writer = new StreamWriter(Sciezka))
            {
                // Tu zapisujemy dane miejsc parkingowych
            }
        }
        
        public static void WczytajDane(Parking parking)
        {
            if (File.Exists(Sciezka))
            {
                // Tu wczytujemy dane
            }
        }
    }
    
    class AplikacjaParkingowa       // 6 klasa 
    {
        private Parking parking = new Parking(5);
        
        public void Uruchom()
        {
            while (true)
            {
                Console.WriteLine("1. Parkuj pojazd\n2. Zwolnij miejsce\n3. Wyświetl stan\n4. Wyjście");
                string wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        Console.Write("Wybierz typ pojazdu (1 - Samochód, 2 - Motocykl): ");
                        string typ = Console.ReadLine();

                        Console.Write("Numer rejestracyjny: ");
                        string nr = Console.ReadLine();

                        Console.Write("Marka: ");
                        string marka = Console.ReadLine();

                        Pojazd nowyPojazd = (typ == "2") ? new Motocykl(nr, marka) : new Samochod(nr, marka);
                
                        if (parking.ParkujPojazd(nowyPojazd))
                            Console.WriteLine("Pojazd zaparkowany.");
                        else
                            Console.WriteLine("Brak wolnych miejsc.");
                
                        break;

                    case "2":
                        Console.Write("Numer miejsca: ");
                        int numerMiejsca = int.Parse(Console.ReadLine());
                        if (parking.ZwolnijMiejsce(numerMiejsca))
                            Console.WriteLine("Miejsce zwolnione.");
                        else
                            Console.WriteLine("Błąd: miejsce jest już puste lub nie istnieje.");
                        break;

                    case "3":
                        parking.WyswietlStanParkingowy();
                        break;

                    case "4":
                        return;
                }
            }
        }
    }
}
