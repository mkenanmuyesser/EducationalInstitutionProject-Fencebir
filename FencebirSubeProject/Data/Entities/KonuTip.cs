using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class KonuTip
    {
        public int KonuTipId { get; set; }
        public string KonuTipAdi { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
