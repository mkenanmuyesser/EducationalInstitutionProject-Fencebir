using FencebirSubeProject.Areas.Admin.Models.AramaViewModel;
using System.Collections.Generic;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class BilgiMetniAramaViewModel : BaseAramaViewModel
    {
        public List<SubeSonucViewModel> SubeList { get; set; }
        public List<IcerikTipSonucViewModel> IcerikTipList { get; set; }
        public int SubeId { get; set; }
        public int IcerikTipId { get; set; }
        public string IcerikMetin { get; set; }
    }
}
