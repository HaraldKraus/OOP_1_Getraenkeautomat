using System.Runtime.CompilerServices;
using System.Security.Authentication;

namespace OOP_1_Getraenkeautomat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // MHD der Produkte
            DateOnly einJahr = DateOnly.FromDateTime(DateTime.Now).AddYears(1); // Heute in 1 Jahr

            // Getränkeautomat erstellen
            Getränkeautomat meinGetränkeautomat = new Getränkeautomat("HAKOmat", "HAK Mistelbach");

            // Fächer hinzufügen
            meinGetränkeautomat.FachHinzufügen(1);
            meinGetränkeautomat.FachHinzufügen(2);
            meinGetränkeautomat.FachHinzufügen(3);

            Produkt wasser = new Produkt("Wasser", new decimal(1.4), einJahr);

            // 3 Produkte in Fach 1 einlegen
            meinGetränkeautomat.ProduktHinzufügen(wasser, 1);
            meinGetränkeautomat.ProduktHinzufügen(wasser, 1);
            meinGetränkeautomat.ProduktHinzufügen(wasser, 1);

            Produkt apfelsaft = new Produkt("Apfelsaft", new decimal(2.5), einJahr.AddYears(1));

            // 3 Produkte in Fach 2 einlegen
            meinGetränkeautomat.ProduktHinzufügen(apfelsaft, 2);
            meinGetränkeautomat.ProduktHinzufügen(apfelsaft, 2);
            meinGetränkeautomat.ProduktHinzufügen(apfelsaft, 2);

            // Wechselgeld einfüllen
            meinGetränkeautomat.Euro1 = 10;
            meinGetränkeautomat.Cent50 = 10;
            meinGetränkeautomat.Cent20 = 10;
            meinGetränkeautomat.Cent10 = 10;

            int eingabe = -1;

            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("###################");
                Console.WriteLine("# Getränkeautomat #");
                Console.WriteLine("###################");

                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Guthaben: {meinGetränkeautomat.EingeworfenesGeld} Euro");
                Console.ResetColor();

                Console.WriteLine("");
                ZeigeGetränkeautomat(meinGetränkeautomat);

                Console.WriteLine();
                Console.WriteLine("Mögliche Eingaben: 1..Preis anzeigen, 2..Geld einwerfen, 3..Produkt wählen, 4..Geldrückgabe, 5..Status, 0..Exit");
                Console.WriteLine();

                Console.Write("Eingabe: ");
                string tmpEingabe = Console.ReadLine();

                if (int.TryParse(tmpEingabe, out eingabe) == false)
                {
                    ZeigeFehler("Fehler: Falsche Eingabe");
                    Console.ReadKey();
                    eingabe = -1;
                    continue;
                }

                if (eingabe < 0 || eingabe > 5)
                {
                    ZeigeFehler("Fehler: Falsche Eingabe");
                    Console.ReadKey();
                    eingabe = -1;
                    continue;
                }

                // Main Code
                switch (eingabe)
                {
                    case 1:
                        // Der User hat 1 eingegeben, zeige Preis für Produkt an
                        Console.Clear();
                        ZeigeGetränkeautomat(meinGetränkeautomat);
                        Console.WriteLine("");
                        Console.Write("Fachnummer: ");

                        string tmpSFachnummer = Console.ReadLine();
                        int tmpFachnummer = -1;
                        Produkt gesuchtesProdukt = null;

                        if (int.TryParse(tmpSFachnummer, out tmpFachnummer) == false)
                        {
                            ZeigeFehler("Falsche Eingabe.");
                            Console.ReadKey();
                            continue;
                        }
                        try
                        {
                            gesuchtesProdukt = meinGetränkeautomat.ZeigeProdukt(tmpFachnummer);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("");
                            Console.WriteLine($"{gesuchtesProdukt.Name}: {gesuchtesProdukt.Preis} Euro");
                            Console.ResetColor();
                        }
                        catch (Exception e)
                        {
                            ZeigeFehler(e.Message);
                        }
                        Console.ReadKey();
                        break;
                    case 2: // Münzeinwurf
                        string münzEingabeS = string.Empty;
                        int münzEingabe = -1;

                        do
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"Guthaben: {meinGetränkeautomat.EingeworfenesGeld} Euro");
                            Console.ResetColor();

                            Console.WriteLine("");
                            Console.WriteLine("Münzeinwurf: 1..1 Euro, 2..50 Cent, 3..20 Cent, 4..10 Cent, 0..Exit");
                            Console.Write("Eingabe: ");
                            münzEingabeS = Console.ReadLine();

                            if (int.TryParse(münzEingabeS, out münzEingabe) == false)
                            {
                                ZeigeFehler("Falsche Eingabe");
                                Console.ReadKey();
                                münzEingabe = -1;
                                continue;
                            }

                            if (münzEingabe < 0 || münzEingabe > 4)
                            {
                                ZeigeFehler("Falsche Eingabe");
                                Console.ReadKey();
                                münzEingabe = -1;
                                continue;
                            }

                            switch (münzEingabe)
                            {
                                case 1:
                                    meinGetränkeautomat.Euro1++;
                                    meinGetränkeautomat.EingeworfenesGeld += 1;
                                    break;
                                case 2:
                                    meinGetränkeautomat.Cent50++;
                                    meinGetränkeautomat.EingeworfenesGeld += new decimal(0.5);
                                    break;
                                case 3:
                                    meinGetränkeautomat.Cent20++;
                                    meinGetränkeautomat.EingeworfenesGeld += new decimal(0.2);
                                    break;
                                case 4:
                                    meinGetränkeautomat.Cent10++;
                                    meinGetränkeautomat.EingeworfenesGeld += new decimal(0.1);
                                    break;
                            }

                        } while (münzEingabe != 0);

                        break;
                    case 3: // Produkt kaufen
                        Console.Clear();
                        ZeigeGetränkeautomat(meinGetränkeautomat);
                        Console.WriteLine("");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Geld eingeworfen: {meinGetränkeautomat.EingeworfenesGeld} Euro");
                        Console.ResetColor();
                        Console.WriteLine("");
                        Console.Write("Fachnummer: ");

                        tmpSFachnummer = Console.ReadLine();
                        tmpFachnummer = -1;
                        gesuchtesProdukt = null;

                        if (int.TryParse(tmpSFachnummer, out tmpFachnummer) == false)
                        {
                            ZeigeFehler("Falsche Eingabe.");
                            Console.ReadKey();
                            continue;
                        }
                        try
                        {
                            gesuchtesProdukt = meinGetränkeautomat.ZeigeProdukt(tmpFachnummer);
                            if (meinGetränkeautomat.EingeworfenesGeld < gesuchtesProdukt.Preis)
                            {
                                ZeigeFehler("Zu wenig Geld eingeworfen.");
                                Console.ReadKey();
                                continue;
                            }

                            gesuchtesProdukt = meinGetränkeautomat.HoleProdukt(tmpFachnummer);
                            meinGetränkeautomat.EingeworfenesGeld -= gesuchtesProdukt.Preis;

                            Console.WriteLine($"Produkt gekauft: {gesuchtesProdukt.Name} um {gesuchtesProdukt.Preis} Euro");
                            Console.WriteLine();
                            Wechselgeld(meinGetränkeautomat);
                        }
                        catch (Exception e)
                        {
                            ZeigeFehler(e.Message);
                        }
                        Console.ReadKey();

                        break;
                    case 4:
                        Console.Clear();
                        if (meinGetränkeautomat.EingeworfenesGeld > 0)
                        {
                            Wechselgeld(meinGetränkeautomat);
                        }
                        else
                        {
                            ZeigeFehler("Kein Geld eingeworfen.");
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Automaten Status:");
                        Console.WriteLine();
                        Console.WriteLine($"Bezeichnung: {meinGetränkeautomat.Bezeichnung}");
                        Console.WriteLine($"Standort: {meinGetränkeautomat.Standort}");
                        Console.WriteLine($"Aktuelle Zeit: {DateTime.Now.ToString("dd.MM.yyyy mm:hh")} Uhr");
                        Console.WriteLine($"Status: {meinGetränkeautomat.Status}");
                        Console.WriteLine($"Anzahl Fächer: {meinGetränkeautomat.FachAnzahl()}");
                        
                        for (int x = 1; x <= meinGetränkeautomat.FachAnzahl(); x++)
                        {
                            try
                            {
                                Console.WriteLine();
                                Console.WriteLine($"Fach {x}:");
                                if (meinGetränkeautomat.AlleProdukteDesFaches(x).Count() == 0)
                                {
                                    ZeigeFehler("Das Fach ist leer.");
                                }
                                else
                                {
                                    int i = 1;
                                    foreach (Produkt p in meinGetränkeautomat.AlleProdukteDesFaches(x))
                                    {
                                        if(p.Ablaufdatum < DateOnly.FromDateTime(DateTime.Now))
                                        {
                                            ZeigeFehler($"{i}: {p.Name} / {p.Preis} Euro / {p.Ablaufdatum.ToString("dd.MM.yyyy")} > PRODUKT ABGELAUFEN");
                                        }
                                        Console.WriteLine($"{i}: {p.Name} / {p.Preis} Euro / {p.Ablaufdatum.ToString("dd.MM.yyyy")}");
                                        i++;
                                    }
                                }                                
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Fach {x}: {e.Message}");
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine($"1 Euro Münzfach:  {meinGetränkeautomat.Euro1} Stück");
                        Console.WriteLine($"50 Cent Münzfach: {meinGetränkeautomat.Cent50} Stück");
                        Console.WriteLine($"20 Cent Münzfach: {meinGetränkeautomat.Cent20} Stück");
                        Console.WriteLine($"10 Cent Münzfach: {meinGetränkeautomat.Cent10} Stück");
                        Console.WriteLine();
                        Console.ReadKey();
                        break;
                }

            } while (eingabe != 0);
        }

        static void ZeigeFehler(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void ZeigeGetränkeautomat(Getränkeautomat meinGetränkeautomat)
        {
            Console.WriteLine($"*  {meinGetränkeautomat.Bezeichnung} | {meinGetränkeautomat.Status}   *");
            Console.WriteLine("+-----------------------------+");
            for (int x = 1; x <= meinGetränkeautomat.FachAnzahl(); x++)
            {
                try
                {
                    Console.WriteLine($"Fach {x}: {meinGetränkeautomat.ZeigeProdukt(x).Name}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Fach {x}: {e.Message}");
                }
            }
            Console.WriteLine("+-----------------------------+");
        }

        static void Wechselgeld(Getränkeautomat meinGetränkeautomat)
        {
            Console.WriteLine("Münzrückgabe");
            if(meinGetränkeautomat.EingeworfenesGeld == 0)
            {
                Console.WriteLine("Betrag wurde genau eingeworfen.");
                return;
            }
            while (meinGetränkeautomat.EingeworfenesGeld > 0)
            {
                if (meinGetränkeautomat.EingeworfenesGeld >= 1 && meinGetränkeautomat.Euro1 > 0)
                {
                    Console.WriteLine("1 Euro Münze");
                    meinGetränkeautomat.EingeworfenesGeld -= 1m;
                    meinGetränkeautomat.Euro1--;
                }
                else if (meinGetränkeautomat.EingeworfenesGeld >= 0.5m && meinGetränkeautomat.Cent50 > 0)
                {
                    Console.WriteLine("50 Cent Münze");
                    meinGetränkeautomat.EingeworfenesGeld -= 0.5m;
                    meinGetränkeautomat.Cent50--;
                }
                else if (meinGetränkeautomat.EingeworfenesGeld >= 0.2m && meinGetränkeautomat.Cent20 > 0)
                {
                    Console.WriteLine("20 Cent Münze");
                    meinGetränkeautomat.EingeworfenesGeld -= 0.2m;
                    meinGetränkeautomat.Cent20--;
                }
                else if (meinGetränkeautomat.EingeworfenesGeld >= 0.1m && meinGetränkeautomat.Cent10 > 0)
                {
                    Console.WriteLine("10 Cent Münze");
                    meinGetränkeautomat.EingeworfenesGeld -= 0.1m;
                    meinGetränkeautomat.Cent10--;
                }
                else
                {
                    Console.WriteLine("Zu wenig Wechselgeld.");
                    meinGetränkeautomat.EingeworfenesGeld = 0;
                }
            } 
        }
    }
}
