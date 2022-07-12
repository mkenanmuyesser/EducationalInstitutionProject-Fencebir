using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Models
{
    public class EpostaGonderimViewModel
    {
        public string GonderilecekEpostaHost  { get; set; }
        public int GonderilecekEpostaPort { get; set; }
        public string GonderilecekEpostaKullaniciAdi { get; set; }
        public string GonderilecekEpostaSifre { get; set; }
        public string GonderilecekEpostaTanim { get; set; }
        public bool GonderilecekEpostaSsl { get; set; }
    }
}
