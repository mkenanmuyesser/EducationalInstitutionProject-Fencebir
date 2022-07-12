using FencebirSubeProject.Areas.Admin.Models.KayitViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class EtkinlikKayitViewModel: BaseKayitViewModel
    {
        public int EtkinlikId { get; set; }

        public List<SubeSonucViewModel> SubeList { get; set; }
        public List<EtkinlikTipSonucViewModel> EtkinlikTipList { get; set; }
        public int SubeId { get; set; }
        public int EtkinlikTipId { get; set; }
        public string EtkinlikKonu { get; set; }
        public string Tarih { get; set; }
        public string BaslangicZaman { get; set; }
        public string BitisZaman { get; set; }
        public string Yer { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }     
    }
}
