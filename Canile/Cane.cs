using System;
using System.Collections.Generic;
using System.Text;

namespace Canile
{
    public enum StatoSalute
    {
        Sano,
        InCura,
        InQuarantena,
        Convalescenza,
        DaVaccinare
    }

    public enum razza
    {
        Alano,
        SanBernardo,
        Terranova,
        MastinoNapoletano,
        MastinoTibetano,
        PastoreAbruzzese,
        Akita,
        BovaroDelBernese,

        PastoreTedesco,
        PastoreBelga,
        GoldenRetriever,
        LabradorRetriever,
        Husky,
        Doberman,
        Boxer,

        Pitbull,
        BullTerrier,
        Bulldog,
        BorderCollie,
        CockerSpaniel,
        AustralianShepherd,

        Shibainu,
        Volpino,
        Beagle,
        Bassotto,
        JackRussell,
        Carlino,
        Maltese,
        Chihuahua,
        Barboncino
    }
    public class Cane
    {
        public string nome { get; set; }
        public int anno { get; set; }
        public razza razza { get; set; }
        public DateTime ingresso { get; set; }
        public bool stato { get; set; } // true = addottato
        public string note { get; set; }

        public Cane(string n, int a, razza r, DateTime i, string no)
        {
            nome = n;
            anno = a;
            razza = r;
            ingresso = i;
            stato = false;
            note = no;
        }

        public override string ToString()
        {
            return $"nome {nome} | razza {razza} | anno di nascita {anno}";
        }

    }
}
