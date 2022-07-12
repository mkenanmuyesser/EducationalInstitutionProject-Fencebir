using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class Sube
    {
        public Sube()
        {
            Banner = new HashSet<Banner>();
            Blog = new HashSet<Blog>();
            Duyuru = new HashSet<Duyuru>();
            Etkinlik = new HashSet<Etkinlik>();
            Galeri = new HashSet<Galeri>();
            Icerik = new HashSet<Icerik>();
            Kullanici = new HashSet<Kullanici>();
            OgrenciYorum = new HashSet<OgrenciYorum>();
            Ogretmen = new HashSet<Ogretmen>();
        }

        public int SubeId { get; set; }
        public int SubeTipId { get; set; }
        public int SubeSehirId { get; set; }
        public string SubeAdi { get; set; }
        public string SubeAttribute { get; set; }
        public string Aciklama { get; set; }
        public string Adres { get; set; }
        public string Telefon1 { get; set; }
        public string Telefon2 { get; set; }
        public string Fax1 { get; set; }
        public string Fax2 { get; set; }
        public string Eposta { get; set; }
        public string FacebookHesapUrl { get; set; }
        public string InstagramHesapUrl { get; set; }
        public string TwitterHesapUrl { get; set; }
        public string WhatsappHesapUrl { get; set; }
        public string YoutubeHesapUrl { get; set; }
        public string GonderilecekEpostaTanim { get; set; }
        public string GonderilecekEpostaKullaniciAdi { get; set; }
        public string GonderilecekEpostaSifre { get; set; }
        public string GonderilecekEpostaHost { get; set; }
        public int GonderilecekEpostaPort { get; set; }
        public bool GonderilecekEpostaSsl { get; set; }
        public bool GonderilecekEpostaAktifMi { get; set; }
        public string ResimUrl { get; set; }
        public byte[] Resim { get; set; }
        public int KayitKullaniciId { get; set; }
        public DateTime KayitTarih { get; set; }
        public int? GuncellemeId { get; set; }
        public DateTime? GuncellemeTarih { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }

        public virtual SubeSehir SubeSehir { get; set; }
        public virtual SubeTip SubeTip { get; set; }
        public virtual ICollection<Banner> Banner { get; set; }
        public virtual ICollection<Blog> Blog { get; set; }
        public virtual ICollection<Duyuru> Duyuru { get; set; }
        public virtual ICollection<Etkinlik> Etkinlik { get; set; }
        public virtual ICollection<Galeri> Galeri { get; set; }
        public virtual ICollection<Icerik> Icerik { get; set; }
        public virtual ICollection<Kullanici> Kullanici { get; set; }
        public virtual ICollection<OgrenciYorum> OgrenciYorum { get; set; }
        public virtual ICollection<Ogretmen> Ogretmen { get; set; }
    }
}
