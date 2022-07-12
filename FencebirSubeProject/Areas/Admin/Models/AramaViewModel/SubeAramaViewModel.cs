using FencebirSubeProject.Areas.Admin.Models.AramaViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class SubeAramaViewModel : BaseAramaViewModel
    {      
        public List<SubeTipSonucViewModel> SubeTipList { get; set; }
        public List<SubeSehirSonucViewModel> SubeSehirList { get; set; }
        public int SubeId { get; set; }
        public int SubeTipId { get; set; }
        public int SubeSehirId { get; set; }
        public string SubeAdi { get; set; }        
    }
}
