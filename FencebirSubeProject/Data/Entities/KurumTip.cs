using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class KurumTip
    {
        public int KurumTipId { get; set; }
        public string KurumTipAdi { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
