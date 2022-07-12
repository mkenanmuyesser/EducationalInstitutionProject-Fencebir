using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class EtkinlikTip
    {
        public EtkinlikTip()
        {
            Etkinlik = new HashSet<Etkinlik>();
        }

        public int EtkinlikTipId { get; set; }
        public string EtkinlikTipAdi { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }

        public virtual ICollection<Etkinlik> Etkinlik { get; set; }
    }
}
