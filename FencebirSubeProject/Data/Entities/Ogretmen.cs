using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class Ogretmen
    {
        public int OgretmenId { get; set; }
        public int SubeId { get; set; }
        public string AdSoyad { get; set; }
        public string Unvan { get; set; }
        public string Aciklama { get; set; }
        public string ResimUrl { get; set; }
        public byte[] Resim { get; set; }
        public string FacebookHesapUrl { get; set; }
        public string InstagramHesapUrl { get; set; }
        public string TwitterHesapUrl { get; set; }
        public string WhatsappHesapUrl { get; set; }
        public string YoutubeHesapUrl { get; set; }
        public bool Anasayfa { get; set; }
        public int KayitKullaniciId { get; set; }
        public DateTime KayitTarih { get; set; }
        public int? GuncellemeId { get; set; }
        public DateTime? GuncellemeTarih { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }

        public virtual Sube Sube { get; set; }
    }
}
