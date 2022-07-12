using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class KullaniciGirisModel
    {
        public int KullaniciId { get; set; }
        public int SubeId { get; set; }
        public string Eposta { get; set; }      
        public string SubeAdi { get; set; }
        public string KullaniciTipAdi { get; set; }
    }
}
