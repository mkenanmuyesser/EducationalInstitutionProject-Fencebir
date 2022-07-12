using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class Kullanici
    {
        public int KullaniciId { get; set; }
        public int SubeId { get; set; }
        public string Eposta { get; set; }
        public string Sifre { get; set; }      
        public int KayitKullaniciId { get; set; }
        public DateTime KayitTarih { get; set; }
        public int? GuncellemeKullaniciId { get; set; }
        public DateTime? GuncellemeTarih { get; set; }
        public bool AktifMi { get; set; }

        public virtual Sube Sube { get; set; }
    }
}
