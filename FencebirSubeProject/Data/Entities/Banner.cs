using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class Banner
    {
        public int BannerId { get; set; }
        public int BannerTipId { get; set; }
        public int SubeId { get; set; }
        public string Adi { get; set; }
        public string ResimUrl { get; set; }
        public byte[] Resim { get; set; }
        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
        public string Link { get; set; }
        public string LinkAciklama { get; set; }
        public bool BannerOlusturma { get; set; }
        public int KayitKullaniciId { get; set; }
        public DateTime KayitTarih { get; set; }
        public int? GuncellemeId { get; set; }
        public DateTime? GuncellemeTarih { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }

        public virtual BannerTip BannerTip { get; set; }
        public virtual Sube Sube { get; set; }
    }
}
