using System;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main()
    {
        Console.WriteLine("Wybierz opcję:");
        Console.WriteLine("1. Zapisz dane do pliku JSON");
        Console.WriteLine("2. Odczytaj dane z pliku JSON");
        string? wybor = Console.ReadLine();

        switch (wybor)
        {
            case "1":
                ZapiszDane();
                break;
            case "2":
                OdczytajDane();
                break;
            default:
                Console.WriteLine("Nieprawidłowy wybór.");
                break;
        }
    }

    static void ZapiszDane()
    {
        Console.WriteLine("Podaj imię:");
        string? imie = Console.ReadLine();
        Console.WriteLine("Podaj wiek:");
        string? wiekInput = Console.ReadLine();
        int? wiek = null;
        if (!int.TryParse(wiekInput, out int parsedWiek))
        {
            Console.WriteLine("Nieprawidłowy wiek.");
            return;
        }
        wiek = parsedWiek;
        Console.WriteLine("Podaj adres:");
        string? adres = Console.ReadLine();

        if (imie == null || adres == null)
        {
            Console.WriteLine("Nieprawidłowe dane wejściowe.");
            return;
        }

        DaneUzytkownika daneUzytkownika = new DaneUzytkownika(imie, wiek.Value, adres);

        try
        {
            string json = JsonSerializer.Serialize(daneUzytkownika);
            File.WriteAllText("dane.json", json);
            Console.WriteLine("Dane zostały zapisane do pliku.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd podczas zapisu danych: " + ex.Message);
        }
    }

    static void OdczytajDane()
    {
        try
        {
            string json = File.ReadAllText("dane.json");
            DaneUzytkownika? daneUzytkownika = JsonSerializer.Deserialize<DaneUzytkownika>(json);

            if (daneUzytkownika != null)
            {
                Console.WriteLine("Imię: " + daneUzytkownika.Imie);
                Console.WriteLine("Wiek: " + daneUzytkownika.Wiek);
                Console.WriteLine("Adres: " + daneUzytkownika.Adres);
            }
            else
            {
                Console.WriteLine("Nie udało się odczytać danych.");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Plik z danymi nie istnieje.");
        }
        catch (JsonException)
        {
            Console.WriteLine("Nieprawidłowy format pliku z danymi.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd podczas odczytu danych: " + ex.Message);
        }
    }

}

class DaneUzytkownika
{
    public string Imie { get; set; }
    public int Wiek { get; set; }
    public string Adres { get; set; }

    public DaneUzytkownika(string imie, int wiek, string adres)
    {
        Imie = imie;
        Wiek = wiek;
        Adres = adres;
    }
}
