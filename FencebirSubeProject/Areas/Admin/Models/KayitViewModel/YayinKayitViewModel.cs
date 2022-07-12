using FencebirSubeProject.Areas.Admin.Models.KayitViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class YayinKayitViewModel : BaseKayitViewModel
    {
        public int YayinId { get; set; }

        public string Ad { get; set; }
        public string EskiFiyat { get; set; }
        public string YeniFiyat { get; set; }
        public byte[] Dosya { get; set; }
        public string DosyaAdi { get; set; }
        public string Pdf { get; set; }
        public byte[] OzetDosya { get; set; }
        public string OzetDosyaAdi { get; set; }
        public string Resim { get; set; }     
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
