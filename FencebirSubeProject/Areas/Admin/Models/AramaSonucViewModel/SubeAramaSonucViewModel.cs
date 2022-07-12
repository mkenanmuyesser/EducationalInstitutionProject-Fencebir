using FencebirSubeProject.Areas.Admin.Models.AramaSonucViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class SubeAramaSonucViewModel : BaseAramaSonucViewModel
    {
        [Key]
        public int SubeId { get; set; }
        public string Resim { get; set; }
        public string SubeTipAdi { get; set; }
        public string SubeSehirAdi { get; set; }
        public string SubeAdi { get; set; }
        public string SubeAttribute { get; set; }
        public string Aciklama { get; set; }  
        public string SiteUrl { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
