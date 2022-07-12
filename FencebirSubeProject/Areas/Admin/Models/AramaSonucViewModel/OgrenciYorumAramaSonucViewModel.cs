using FencebirSubeProject.Areas.Admin.Models.AramaSonucViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class OgrenciYorumAramaSonucViewModel : BaseAramaSonucViewModel
    {
        [Key]
        public int OgrenciYorumId { get; set; }
        public string Resim { get; set; }
        public string SubeAdi { get; set; }
        public string OgrenciAdSoyad { get; set; }
        public string Yorum { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
