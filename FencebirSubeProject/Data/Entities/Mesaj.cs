using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class Mesaj
    {
        public int MesajId { get; set; }
        public int MesajTipId { get; set; }
        public int SubeId { get; set; }
        public string MesajIcerik { get; set; }       
        public DateTime GonderimTarihi { get; set; }
        public byte[] Dosya { get; set; }
        public string DosyaAdi { get; set; }

        public virtual MesajTip MesajTip { get; set; }
        public virtual Sube Sube { get; set; }
    }
}
