using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_1_Getraenkeautomat
{
    internal class Getränkeautomat
    {
        public string Bezeichnung { get; set; } = string.Empty;
        public string Standort { get; set; } = string.Empty;
        public decimal EingeworfenesGeld { get; set; } = 0;

        int euro1 = 0;
        public int Euro1
        {
            get
            {
                return this.euro1;
            }
            set
            {
                this.euro1 = value;
                CheckStatus();
            }
        }

        int cent50 = 0;
        public int Cent50
        {
            get
            {
                return this.cent50;
            }
            set
            {
                this.cent50 = value;
                CheckStatus();
            }
        }

        int cent20 = 0;
        public int Cent20
        {
            get
            {
                return this.cent20;
            }
            set
            {
                this.cent20 = value;
                CheckStatus();
            }
        }

        int cent10 = 0;
        public int Cent10
        {
            get
            {
                return this.cent10;
            }
            set
            {
                this.cent10 = value;
                CheckStatus();
            }
        }
        public string Status { get; private set; } = "Nicht Betriebsbreit";

        private List<Fach> fächer = new List<Fach>();

        public Getränkeautomat(string bezeichnung, string standort)
        {
            this.Bezeichnung = bezeichnung;
            this.Standort = standort;
        }

        public void FachHinzufügen(int fachNummer)
        {
            // Prüfe ob es die fachNummer bereits gibt
            foreach (Fach fach in fächer)
            {
                if (fach.Nummer == fachNummer)
                {
                    throw new Exception($"Die Fachnummer {fachNummer} ist bereits vorhanden.");
                }
            }

            try
            {
                Fach neuesFach = new Fach(fachNummer); // Erstelle neues Fach mit der Fachnummer
                this.fächer.Add(neuesFach); // Füge das neue Fach zur Fachliste hinzu
            }
            catch (Exception fachException)
            {
                throw fachException;
            }
        }
        public void ProduktHinzufügen(Produkt neuesProdukt, int fachNummer)
        {
            // Gibt es das Fach übehaupt?
            bool fachGefunden = false;
            Fach meinFach = null;

            foreach (Fach fach in fächer)
            {
                if (fach.Nummer == fachNummer)
                {
                    // Fach gefunden
                    fachGefunden = true;
                    meinFach = fach; // Speichert die Referenz des gefundenen Faches
                }
            }

            if (fachGefunden == true)
            {
                // Füge Produkt zum Fach hinzu
                meinFach.LegeProduktInFachEin(neuesProdukt);
                CheckStatus();
            }
            else
            {
                throw new Exception($"Fach mit der Nummer {fachNummer} nicht vorhanden.");
            }
        }
        private void CheckStatus()
        {
            // Ist zumindest ein Prdukt im Automaten
            bool produktGefunden = false;
            foreach (var fach in fächer)
            {
                if (fach.istLeer == false)
                {
                    produktGefunden = true;
                }
            }
            //produktGefunden = fächer.Where(f => f.istLeer == true).Any();

            // Ist in jedem Münzfach mehr als 0 Münzen vorhanden und zumindest ein Produkt im Automaten vorhanden
            if (produktGefunden == true && this.Euro1 > 0 && this.Cent50 > 0 && this.Cent20 > 0 && this.Cent10 > 0)
            {
                this.Status = "Betriebsbereit";
            }
            else if (produktGefunden == false)
            {
                this.Status = "Kein Produkt vorhanden";
            }
            else
            {
                this.Status = "Zu wenig Wechselgeld vorhanden";
            }
        }
        public Produkt ZeigeProdukt(int fachNummer)
        {
            // Hole fach aus der Fachliste mit der Fachnummer = fachNummer
            bool fachGefunden = false;
            Fach gefundenesFach = null;

            foreach (Fach fach in fächer)
            {
                if (fach.Nummer == fachNummer)
                {
                    fachGefunden = true;
                    gefundenesFach = fach;
                }
            }

            if (fachGefunden == false)
            {
                throw new Exception($"Das Fach mit der Nummer {fachNummer} exisitiert nicht.");
            }

            Produkt meinProdukt = gefundenesFach.ZeigeErstesProduktAusFach();
            return meinProdukt;
        }
        public Produkt[] AlleProdukteDesFaches(int fachNummer)
        {
            // Hole fach aus der Fachliste mit der Fachnummer = fachNummer
            bool fachGefunden = false;
            Fach gefundenesFach = null;

            foreach (Fach fach in fächer)
            {
                if (fach.Nummer == fachNummer)
                {
                    fachGefunden = true;
                    gefundenesFach = fach;
                }
            }

            if (fachGefunden == false)
            {
                throw new Exception($"Das Fach mit der Nummer {fachNummer} exisitiert nicht.");
            }

            Produkt[] meinProdukt = gefundenesFach.ZeigeAlleProdukteAusFach();
            return meinProdukt;
        }
        public Produkt HoleProdukt(int fachNummer)
        {
            // Hole fach aus der Fachliste mit der Fachnummer = fachNummer
            bool fachGefunden = false;
            Fach gefundenesFach = null;

            foreach (Fach fach in fächer)
            {
                if (fach.Nummer == fachNummer)
                {
                    fachGefunden = true;
                    gefundenesFach = fach;
                }
            }

            if (fachGefunden == false)
            {
                throw new Exception($"Das Fach mit der Nummer {fachNummer} exisitiert nicht.");
            }

            Produkt meinProdukt = gefundenesFach.HoleProduktAusFach();
            return meinProdukt;
        }
        public int FachAnzahl()
        {
            return this.fächer.Count();
        }
    }
}
