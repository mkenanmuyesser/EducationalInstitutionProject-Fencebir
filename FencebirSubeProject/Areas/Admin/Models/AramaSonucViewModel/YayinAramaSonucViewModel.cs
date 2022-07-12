using FencebirSubeProject.Areas.Admin.Models.AramaSonucViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class YayinAramaSonucViewModel : BaseAramaSonucViewModel
    {
        [Key]
        public int YayinId { get; set; }
        public string Resim { get; set; }
        public string Ad { get; set; }
        public string EskiFiyat { get; set; }
        public string YeniFiyat { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
