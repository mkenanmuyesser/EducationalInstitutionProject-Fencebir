using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Models
{
    public class AnasayfaViewModel
    {
        public List<BannerViewModel> BannerList { get; set; }
        public List<SubeViewModel> SubeTemsilciList { get; set; }
        public List<DuyuruViewModel> DuyuruList { get; set; }
        public HosgeldinBilgiViewModel HosgeldinBilgiData { get; set; }
        public VideoBilgiViewModel VideoBilgiData { get; set; }
        public List<SubeViewModel> SubeList { get; set; }
        public List<SubeViewModel> TemsilciList { get; set; }
        public List<OgrenciYorumViewModel> OgrenciYorumList { get; set; }
        public List<OgretmenViewModel> OgretmenList { get; set; }
        public List<GaleriViewModel> GaleriList { get; set; }
        public List<BlogViewModel> BlogList { get; set; }
        public int Durum { get; set; }
    }
}
