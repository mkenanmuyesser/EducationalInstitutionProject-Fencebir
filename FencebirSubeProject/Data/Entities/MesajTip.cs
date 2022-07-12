using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class MesajTip
    {
        public MesajTip()
        {
            Mesaj = new HashSet<Mesaj>();
        }

        public int MesajTipId { get; set; }
        public string MesajTipAdi { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }

        public virtual ICollection<Mesaj> Mesaj { get; set; }
    }
}
