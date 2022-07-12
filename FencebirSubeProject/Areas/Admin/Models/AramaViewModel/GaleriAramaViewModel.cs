using FencebirSubeProject.Areas.Admin.Models.AramaViewModel;
using System.Collections.Generic;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class GaleriAramaViewModel : BaseAramaViewModel
    {
        public List<SubeSonucViewModel> SubeList { get; set; }
        public List<GaleriTipSonucViewModel> GaleriTipList { get; set; }
        public int SubeId { get; set; }
        public int GaleriTipId { get; set; }
        public string Aciklama { get; set; }
    }
}
