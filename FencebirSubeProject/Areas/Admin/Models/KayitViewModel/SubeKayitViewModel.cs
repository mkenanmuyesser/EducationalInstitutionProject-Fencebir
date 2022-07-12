using FencebirSubeProject.Areas.Admin.Models.KayitViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class SubeKayitViewModel : BaseKayitViewModel
    {
        public int SubeId { get; set; }

        public List<SubeTipSonucViewModel> SubeTipList { get; set; }
        public List<SubeSehirSonucViewModel> SubeSehirList { get; set; }     
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
        public byte[] Dosya { get; set; }
        public string DosyaAdi { get; set; }
        public string Resim { get; set; }
        public string SiteUrl { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }     
    }
}
