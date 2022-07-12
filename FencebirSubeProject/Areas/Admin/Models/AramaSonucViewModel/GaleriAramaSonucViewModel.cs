using FencebirSubeProject.Areas.Admin.Models.AramaSonucViewModel;
using System.ComponentModel.DataAnnotations;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class GaleriAramaSonucViewModel : BaseAramaSonucViewModel
    {
        [Key]
        public int GaleriId { get; set; }
        public string Resim { get; set; }
        public string SubeAdi { get; set; }
        public string GaleriTipAdi { get; set; }
        public string Aciklama { get; set; }
        public string Tarih { get; set; }
        public bool Anasayfa { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
