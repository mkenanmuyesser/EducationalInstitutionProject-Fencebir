using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class Blog
    {
        public int BlogId { get; set; }
        public int BlogTipId { get; set; }
        public int SubeId { get; set; }
        public string Baslik { get; set; }
        public string KisaIcerik { get; set; }
        public string Icerik { get; set; }
        public string ResimUrl { get; set; }
        public byte[] Resim { get; set; }
        public string Etiketler { get; set; }
        public DateTime YayinTarihi { get; set; }
        public bool Anasayfa { get; set; }
        public int OkunmaSayisi { get; set; }
        public int KayitKullaniciId { get; set; }
        public DateTime KayitTarih { get; set; }
        public int? GuncellemeId { get; set; }
        public DateTime? GuncellemeTarih { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }

        public virtual BlogTip BlogTip { get; set; }
        public virtual Sube Sube { get; set; }
    }
}
