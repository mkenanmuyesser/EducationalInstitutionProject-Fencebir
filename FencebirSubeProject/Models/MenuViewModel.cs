using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Models
{
    public class MenuViewModel
    {
        public List<SubeViewModel> SubeList { get; set; }
        public List<SubeViewModel> TemsilciList { get; set; }
        public bool Yayin { get; set; }
        public bool Ogretmen { get; set; }
        public bool Galeri { get; set; }
        public bool Blog { get; set; }
    }
}
