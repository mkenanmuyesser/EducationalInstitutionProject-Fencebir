using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class Galeri
    {
        public int GaleriId { get; set; }
        public int GaleriTipId { get; set; }
        public int SubeId { get; set; }
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
        public string ResimUrl { get; set; }
        public byte[] Resim { get; set; }
        public bool Anasayfa { get; set; }
        public int KayitKullaniciId { get; set; }
        public DateTime KayitTarih { get; set; }
        public int? GuncellemeId { get; set; }
        public DateTime? GuncellemeTarih { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }

        public virtual GaleriTip GaleriTip { get; set; }
        public virtual Sube Sube { get; set; }
    }
}
