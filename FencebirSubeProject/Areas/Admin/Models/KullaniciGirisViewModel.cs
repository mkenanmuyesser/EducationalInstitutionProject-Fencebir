using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class KullaniciGirisViewModel
    {
        public string Eposta { get; set; }
        public string Sifre { get; set; }
        public string ReturnUrl { get; set; }
    }
}
