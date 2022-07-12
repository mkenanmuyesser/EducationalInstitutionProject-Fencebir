using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Models
{
    public class BilgiTalepViewModel
    {
        public List<KonuTipViewModel> KonuTipList { get; set; }
        public string AdSoyad { get; set; }
        public string Eposta { get; set; }
        public string Telefon { get; set; }
        public int KonuTipId { get; set; }
        public string Sinif { get; set; }
        public string Mesaj { get; set; }
    }
}
