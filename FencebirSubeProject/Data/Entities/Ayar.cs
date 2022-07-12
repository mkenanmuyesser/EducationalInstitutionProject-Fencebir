using System;

namespace FencebirSubeProject.Entities
{
    public partial class Ayar
    {
        public int AyarId { get; set; }
        public bool IpBloklamaAktifMi { get; set; }
        public string IpBlokListesi { get; set; }
        public bool UygulamaAktifMi { get; set; }
        public int IslemKullaniciId { get; set; }
        public DateTime IslemTarih { get; set; }
    }
}
