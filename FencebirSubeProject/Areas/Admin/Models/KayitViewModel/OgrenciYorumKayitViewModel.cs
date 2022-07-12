using FencebirSubeProject.Areas.Admin.Models.KayitViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class OgrenciYorumKayitViewModel : BaseKayitViewModel
    {
        public int OgrenciYorumId { get; set; }

        public List<SubeSonucViewModel> SubeList { get; set; }
        public int SubeId { get; set; }
        public string OgrenciAdSoyad { get; set; }
        public string Yorum { get; set; }
        public byte[] Dosya { get; set; }
        public string DosyaAdi { get; set; }
        public string Resim { get; set; }     
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
