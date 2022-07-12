using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class IndexViewModel
    {
        public List<SubeSonucViewModel> SubeList { get; set; }
        public int SubeId { get; set; }

        public int BilgiTalep { get; set; }
        public int IletisimTalep { get; set; }
        public int FranchiseTalep { get; set; }
        public int SubeSayisi { get; set; }
        public int TemsilciSayisi { get; set; }
        public int BlogSayisi { get; set; }
        public int OkunmaSayisi { get; set; }
        public int YayinSayisi{ get; set; }
        public int EtkinlikSayisi { get; set; }
    }
}
