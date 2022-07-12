using FencebirSubeProject.Areas.Admin.Models.KayitViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class BannerKayitViewModel : BaseKayitViewModel
    {
        public int BannerId { get; set; }

        public List<SubeSonucViewModel> SubeList { get; set; }
        public List<BannerTipSonucViewModel> BannerTipList { get; set; }
        public int SubeId { get; set; }
        public int BannerTipId { get; set; }
        public string Adi { get; set; }
        public byte[] Dosya { get; set; }
        public string DosyaAdi { get; set; }
        public string Resim { get; set; }
        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
        public string Link { get; set; }
        public string LinkAciklama { get; set; }
        public bool BannerOlusturma { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
