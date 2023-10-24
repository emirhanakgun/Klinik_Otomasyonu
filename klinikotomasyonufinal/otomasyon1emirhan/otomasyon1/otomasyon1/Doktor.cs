using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace KlinikOtomasyonu1
{
    internal class Doktor
    {
        public string DoktorAd { get; set; }
        public string DoktorSoyadi { get; set; }
        public string DoktorTcNo { get; set; }



        public Doktor(string ad, string soyad, string tcNo)
        {
            DoktorAd = ad;
            DoktorSoyadi = soyad;
            DoktorTcNo = tcNo;
        }



        public override string ToString()
        {
            return $"{DoktorAd} {DoktorSoyadi}";
        }
    }
}