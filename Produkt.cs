using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_1_Getraenkeautomat
{
    internal class Produkt
    {
        public string Name { get; private set; } = string.Empty;
        public decimal Preis { get; set; } = 0;
        public DateOnly Ablaufdatum { get; private set; } = DateOnly.MinValue;

        public Produkt(string name, decimal preis, DateOnly ablaufdatum)
        {
            this.Name = name;
            if (preis > 0)
            {
                this.Preis = preis;
            }
            else
            {
                throw new Exception($"Der Preis muss größer als 0 sein. Gegeben {preis}");
            }

            if (ablaufdatum >= DateOnly.FromDateTime(DateTime.Now))
            {
                this.Ablaufdatum = ablaufdatum;
            }
            else
            {
                throw new Exception($"Das Ablaufdatum darf nicht in der Vergangenheit liegen. Gegeben {ablaufdatum.ToString()}");
            }

        }
    }
}
