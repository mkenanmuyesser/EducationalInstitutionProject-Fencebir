using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class IcerikTip
    {
        public IcerikTip()
        {
            Icerik = new HashSet<Icerik>();
        }

        public int IcerikTipId { get; set; }
        public string IcerikTipAdi { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }

        public virtual ICollection<Icerik> Icerik { get; set; }
    }
}
