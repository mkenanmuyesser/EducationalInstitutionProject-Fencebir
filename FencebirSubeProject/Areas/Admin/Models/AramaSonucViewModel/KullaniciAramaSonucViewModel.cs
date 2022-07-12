using FencebirSubeProject.Areas.Admin.Models.AramaSonucViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class KullaniciAramaSonucViewModel : BaseAramaSonucViewModel
    {
        [Key]
        public int KullaniciId { get; set; }
        public string SubeAdi { get; set; }
        public string Eposta { get; set; }
        public string KayitTarih { get; set; }
        public bool AktifMi { get; set; }
    }
}
