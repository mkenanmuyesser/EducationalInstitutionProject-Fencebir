using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class Icerik
    {
        public int IcerikId { get; set; }
        public int IcerikTipId { get; set; }
        public int SubeId { get; set; }
        public string IcerikMetin { get; set; }
        public bool HtmlIcerik { get; set; }
        public int KayitKullaniciId { get; set; }
        public DateTime KayitTarih { get; set; }
        public int? GuncellemeKullaniciId { get; set; }
        public DateTime? GuncellemeTarih { get; set; }

        public virtual IcerikTip IcerikTip { get; set; }
        public virtual Sube Sube { get; set; }
    }
}
