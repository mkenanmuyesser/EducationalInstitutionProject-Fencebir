using FencebirSubeProject.Areas.Admin.Models.KayitViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class OgretmenKayitViewModel : BaseKayitViewModel
    {
        public int OgretmenId { get; set; }

        public List<SubeSonucViewModel> SubeList { get; set; }
        public int SubeId { get; set; }
        public string AdSoyad { get; set; }
        public string Unvan { get; set; }
        public string Aciklama { get; set; }
        public string FacebookHesapUrl { get; set; }
        public string InstagramHesapUrl { get; set; }
        public string TwitterHesapUrl { get; set; }
        public string WhatsappHesapUrl { get; set; }
        public string YoutubeHesapUrl { get; set; }
        public bool Anasayfa { get; set; }
        public byte[] Dosya { get; set; }
        public string DosyaAdi { get; set; }
        public string Resim { get; set; }     
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
