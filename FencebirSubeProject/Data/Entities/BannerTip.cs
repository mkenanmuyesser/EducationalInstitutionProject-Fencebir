using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class BannerTip
    {
        public BannerTip()
        {
            Banner = new HashSet<Banner>();
        }

        public int BannerTipId { get; set; }
        public string BannerTipAdi { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }

        public virtual ICollection<Banner> Banner { get; set; }
    }
}
