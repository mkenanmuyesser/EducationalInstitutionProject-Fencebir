using FencebirSubeProject.Areas.Admin.Models.KayitViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class KullaniciKayitViewModel : BaseKayitViewModel
    {
        public int KullaniciId { get; set; }

        public List<SubeSonucViewModel> SubeList { get; set; }
        public int SubeId { get; set; }
        public string Eposta { get; set; }
        public string Sifre { get; set; }
        public string KayitTarih { get; set; }
        public bool AktifMi { get; set; }     
    }
}
