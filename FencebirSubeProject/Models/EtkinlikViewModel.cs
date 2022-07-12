using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Models
{
    public class EtkinlikViewModel
    {
        public string EtkinlikKonu { get; set; }
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
        public TimeSpan BaslangicZaman { get; set; }
        public TimeSpan BitisZaman { get; set; }
        public string Yer { get; set; }
    }
}
