using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class Etkinlik
    {
        public int EtkinlikId { get; set; }
        public int EtkinlikTipId { get; set; }
        public int SubeId { get; set; }
        public string EtkinlikKonu { get; set; }
        public DateTime Tarih { get; set; }
        public TimeSpan BaslangicZaman { get; set; }
        public TimeSpan BitisZaman { get; set; }
        public string Yer { get; set; }
        public int KayitKullaniciId { get; set; }
        public DateTime KayitTarih { get; set; }
        public int? GuncellemeId { get; set; }
        public DateTime? GuncellemeTarih { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }

        public virtual EtkinlikTip EtkinlikTip { get; set; }
        public virtual Sube Sube { get; set; }
    }
}
