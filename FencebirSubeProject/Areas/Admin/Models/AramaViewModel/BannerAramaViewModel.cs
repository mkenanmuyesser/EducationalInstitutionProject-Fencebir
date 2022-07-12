using FencebirSubeProject.Areas.Admin.Models.AramaViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class BannerAramaViewModel : BaseAramaViewModel
    {
        public List<SubeSonucViewModel> SubeList { get; set; }
        public List<BannerTipSonucViewModel> BannerTipList { get; set; }
        public int SubeId { get; set; }
        public int BannerTipId { get; set; }
        public string Adi { get; set; }        
    }
}
