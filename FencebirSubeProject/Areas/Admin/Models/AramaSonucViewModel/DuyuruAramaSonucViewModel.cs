using FencebirSubeProject.Areas.Admin.Models.AramaSonucViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class DuyuruAramaSonucViewModel : BaseAramaSonucViewModel
    {
        [Key]
        public int DuyuruId { get; set; }
        public string SubeAdi { get; set; }
        public string Icerik { get; set; }
        public string Tarih { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
