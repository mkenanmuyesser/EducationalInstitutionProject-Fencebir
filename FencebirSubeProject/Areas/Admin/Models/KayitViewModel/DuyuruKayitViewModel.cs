using FencebirSubeProject.Areas.Admin.Models.KayitViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class DuyuruKayitViewModel : BaseKayitViewModel
    {
        public int DuyuruId { get; set; }

        public List<SubeSonucViewModel> SubeList { get; set; }
        public int SubeId { get; set; }
        public string Icerik { get; set; }
        public string Tarih { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }     
    }
}
