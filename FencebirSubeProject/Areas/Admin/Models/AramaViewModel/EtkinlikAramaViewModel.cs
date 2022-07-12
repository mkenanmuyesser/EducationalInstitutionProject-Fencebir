using FencebirSubeProject.Areas.Admin.Models.AramaViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class EtkinlikAramaViewModel : BaseAramaViewModel
    {
        public List<SubeSonucViewModel> SubeList { get; set; }
        public List<EtkinlikTipSonucViewModel> EtkinlikTipList { get; set; }
        public int SubeId { get; set; }
        public int EtkinlikTipId { get; set; }
        public string EtkinlikKonu { get; set; }
    }
}
