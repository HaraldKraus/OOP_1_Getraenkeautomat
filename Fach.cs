using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace OOP_1_Getraenkeautomat
{
    internal class Fach
    {
        public int Nummer { get; set; } = -1;
        public bool istLeer { get; private set; } = true;
        public int Füllstand { get; private set; } = 0;

        private Queue<Produkt> produkteImFach = new Queue<Produkt>();

        public Fach(int nummer)
        {
            if (nummer > 0)
            {
                this.Nummer = nummer;
            }
            else
            {
                throw new Exception($"Die nummer des Faches muss größer 0 sein. Gegeben {nummer}");
            }
        }

        public void LegeProduktInFachEin(Produkt neuesProdukt)
        {
            // Lege Produkt ins Fach
            this.produkteImFach.Enqueue(neuesProdukt);

            // Ändere Füllstand
            this.Füllstand++;

            // Das Fach ist nicht mehr leer
            this.istLeer = false;
        }
        public Produkt HoleProduktAusFach()
        {
            // Hole Produkt aus Fach
            Produkt produktAusFach = this.produkteImFach.Dequeue();

            // Ändere Füllstand
            this.Füllstand--;

            // Ist das Fach leer?
            if (this.Füllstand == 0)
            {
                this.istLeer = true;
            }

            return produktAusFach;
        }
        public Produkt ZeigeErstesProduktAusFach()
        {
            if (this.produkteImFach.Count <= 0)
            {
                throw new Exception("Das Fach ist leer");
            }

            Produkt erstesProdukt = this.produkteImFach.Peek();
            return erstesProdukt;
        }
        public Produkt[] ZeigeAlleProdukteAusFach()
        {
            return this.produkteImFach.ToArray();
        }
    }
}
