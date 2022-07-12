using FencebirSubeProject.Areas.Admin.Models.AramaSonucViewModel;
using System.ComponentModel.DataAnnotations;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class BilgiMetniAramaSonucViewModel : BaseAramaSonucViewModel
    {
        [Key]
        public int IcerikId { get; set; }
        public string SubeAdi { get; set; }
        public string IcerikTipAdi { get; set; }
        public string IcerikMetin { get; set; }
        public bool HtmlIcerik { get; set; }
    }
}
