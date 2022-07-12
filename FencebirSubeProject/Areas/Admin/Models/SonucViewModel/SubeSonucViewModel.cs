using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class SubeSonucViewModel
    {
        public int SubeId { get; set; }
        public int SubeTipId { get; set; }
        public int SubeSehirId { get; set; }
        public string SubeTipAdi { get; set; }
        public string SubeSehirAdi { get; set; }
        public string SubeAdi { get; set; }
        public string SubeAttribute { get; set; }
    }
}
