using FencebirSubeProject.Areas.Admin.Models.AramaViewModel;
using System.Collections.Generic;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class BlogAramaViewModel : BaseAramaViewModel
    {
        public List<SubeSonucViewModel> SubeList { get; set; }
        public List<BlogTipSonucViewModel> BlogTipList { get; set; }
        public int SubeId { get; set; }
        public int BlogTipId { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string Etiketler { get; set; }
    }
}
