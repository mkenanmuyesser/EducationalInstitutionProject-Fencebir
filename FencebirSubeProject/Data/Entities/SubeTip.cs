using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class SubeTip
    {
        public SubeTip()
        {
            Sube = new HashSet<Sube>();
        }

        public int SubeTipId { get; set; }
        public string SubeTipAdi { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }

        public virtual ICollection<Sube> Sube { get; set; }
    }
}
