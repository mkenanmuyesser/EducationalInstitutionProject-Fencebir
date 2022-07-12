using FencebirSubeProject.Areas.Admin.Models.AramaSonucViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class OgretmenAramaSonucViewModel : BaseAramaSonucViewModel
    {
        [Key]
        public int OgretmenId { get; set; }
        public string Resim { get; set; }
        public string SubeAdi { get; set; }
        public string AdSoyad { get; set; }
        public string Unvan { get; set; }
        public string Aciklama { get; set; }
        public bool Anasayfa { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
