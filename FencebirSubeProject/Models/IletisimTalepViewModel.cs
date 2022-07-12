using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Models
{
    public class IletisimTalepViewModel
    {
        public string AdSoyad { get; set; }
        public string Eposta { get; set; }
        public string Konu { get; set; }
        public string Telefon { get; set; }
        public string Mesaj { get; set; }
    }
}
