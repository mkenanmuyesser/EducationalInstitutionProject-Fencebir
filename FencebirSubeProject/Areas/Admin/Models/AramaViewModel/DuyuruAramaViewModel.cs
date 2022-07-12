using FencebirSubeProject.Areas.Admin.Models.AramaViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class DuyuruAramaViewModel : BaseAramaViewModel
    {
        public List<SubeSonucViewModel> SubeList { get; set; }
        public int SubeId { get; set; }
        public string Icerik { get; set; }
    }
}
