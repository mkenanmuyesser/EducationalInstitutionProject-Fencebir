using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Models
{
    public class FranchiseTalepViewModel
    {
        public List<SehirBilgiViewModel> SehirBilgiList { get; set; }
        public List<KurumTipViewModel> KurumTipList { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public int SehirId { get; set; }
        public int KurumTipId { get; set; }
        public string Aciklama { get; set; }
        public byte[] Dosya { get; set; }
        public string DosyaAdi { get; set; }
    }
}
