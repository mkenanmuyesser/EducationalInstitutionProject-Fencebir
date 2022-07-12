using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class Yayin
    {
        public int YayinId { get; set; }
        public string Ad { get; set; }
        public decimal? EskiFiyat { get; set; }
        public decimal? YeniFiyat { get; set; }
        public string ResimUrl { get; set; }
        public byte[] Resim { get; set; }
        public string OzetDosyaUrl { get; set; }
        public byte[] OzetDosya { get; set; }
        public int KayitKullaniciId { get; set; }
        public DateTime KayitTarih { get; set; }
        public int? GuncellemeId { get; set; }
        public DateTime? GuncellemeTarih { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
