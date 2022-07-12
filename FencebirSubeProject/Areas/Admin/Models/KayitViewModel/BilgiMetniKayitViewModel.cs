using FencebirSubeProject.Areas.Admin.Models.KayitViewModel;
using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class BilgiMetniKayitViewModel : BaseKayitViewModel
    {
        public int IcerikId { get; set; }

        public List<SubeSonucViewModel> SubeList { get; set; }
        public List<IcerikTipSonucViewModel> IcerikTipList { get; set; }
        public int SubeId { get; set; }
        public int IcerikTipId { get; set; }
        public string IcerikMetin { get; set; }
        public bool HtmlIcerik { get; set; }
    }
}
