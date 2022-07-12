using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class OgrenciYorum
    {
        public int OgrenciYorumId { get; set; }
        public int SubeId { get; set; }
        public string OgrenciAdSoyad { get; set; }
        public string Yorum { get; set; }
        public string ResimUrl { get; set; }
        public byte[] Resim { get; set; }
        public int KayitKullaniciId { get; set; }
        public DateTime KayitTarih { get; set; }
        public int? GuncellemeId { get; set; }
        public DateTime? GuncellemeTarih { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }

        public virtual Sube Sube { get; set; }
    }
}
