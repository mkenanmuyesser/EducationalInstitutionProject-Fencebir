using FencebirSubeProject.Areas.Admin.Models.AramaSonucViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class MesajAramaSonucViewModel : BaseAramaSonucViewModel
    {
        [Key]
        public int MesajId { get; set; }
        public string SubeAdi { get; set; }
        public string MesajIcerik { get; set; }
        public string GonderimTarihi { get; set; }
        public string DosyaAdi { get; set; }
    }
}
