using FencebirSubeProject.Areas.Admin.Models.AramaSonucViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class EtkinlikAramaSonucViewModel : BaseAramaSonucViewModel
    {
        [Key]
        public int EtkinlikId { get; set; }
        public string SubeAdi { get; set; }
        public string EtkinlikTipAdi { get; set; }
        public string EtkinlikKonu { get; set; }
        public string TarihZaman { get; set; }
        public string Yer { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
