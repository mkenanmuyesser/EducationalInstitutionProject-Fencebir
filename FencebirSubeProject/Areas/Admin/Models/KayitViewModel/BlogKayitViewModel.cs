using FencebirSubeProject.Areas.Admin.Models.KayitViewModel;
using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class BlogKayitViewModel : BaseKayitViewModel
    {
        public int BlogId { get; set; }

        public List<SubeSonucViewModel> SubeList { get; set; }
        public List<BlogTipSonucViewModel> BlogTipList { get; set; }
        public int SubeId { get; set; }
        public int BlogTipId { get; set; }
        public string Baslik { get; set; }
        public string KisaIcerik { get; set; }
        public string Icerik { get; set; }
        public string Etiketler { get; set; }
        public string YayinTarihi { get; set; }
        public bool Anasayfa { get; set; }
        public int OkunmaSayisi { get; set; }
        public byte[] Dosya { get; set; }
        public string DosyaAdi { get; set; }
        public string Resim { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
