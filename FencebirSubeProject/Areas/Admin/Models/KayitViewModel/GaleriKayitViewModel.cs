using FencebirSubeProject.Areas.Admin.Models.KayitViewModel;
using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class GaleriKayitViewModel : BaseKayitViewModel
    {
        public int GaleriId { get; set; }

        public List<SubeSonucViewModel> SubeList { get; set; }
        public List<GaleriTipSonucViewModel> GaleriTipList { get; set; }
        public int SubeId { get; set; }
        public int GaleriTipId { get; set; }
        public string Aciklama { get; set; }
        public string Tarih { get; set; }
        public bool Anasayfa { get; set; }
        public byte[] Dosya { get; set; }
        public string DosyaAdi { get; set; }
        public string Resim { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
