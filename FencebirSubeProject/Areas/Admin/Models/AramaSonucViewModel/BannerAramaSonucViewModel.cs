using FencebirSubeProject.Areas.Admin.Models.AramaSonucViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class BannerAramaSonucViewModel : BaseAramaSonucViewModel
    {
        [Key]
        public int BannerId { get; set; }
        public string Resim { get; set; }
        public string SubeAdi { get; set; }
        public string BannerTipAdi { get; set; }
        public string Adi { get; set; }
        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
        public string Link { get; set; }
        public string LinkAciklama { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
