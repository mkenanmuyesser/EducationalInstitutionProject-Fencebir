using FencebirSubeProject.Areas.Admin.Models.AramaSonucViewModel;
using System.ComponentModel.DataAnnotations;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class BlogAramaSonucViewModel : BaseAramaSonucViewModel
    {
        [Key]
        public int BlogId { get; set; }
        public string Resim { get; set; }
        public string SubeAdi { get; set; }
        public string BlogTipAdi { get; set; }
        public string Baslik { get; set; }
        public string KisaIcerik { get; set; }
        public string Etiketler { get; set; }
        public string YayinTarihi { get; set; }
        public bool Anasayfa { get; set; }
        public int OkunmaSayisi { get; set; }    
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    }
}
