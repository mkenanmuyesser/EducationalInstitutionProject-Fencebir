using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class GaleriTip
    {
        public GaleriTip()
        {
            Galeri = new HashSet<Galeri>();
        }

        public int GaleriTipId { get; set; }
        public string GaleriTipAdi { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }

        public virtual ICollection<Galeri> Galeri { get; set; }
    }
}
