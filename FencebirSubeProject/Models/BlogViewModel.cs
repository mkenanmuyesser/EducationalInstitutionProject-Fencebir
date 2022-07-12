using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace FencebirSubeProject.Models
{
    public class BlogViewModel
    {
        public int BlogId { get; set; }
        public string Baslik { get; set; }
        public string KisaIcerik { get; set; }
        public string Icerik { get; set; }
        public string Resim { get; set; }
        public string Etiketler { get; set; }
        public DateTime YayinTarihi { get; set; }
        public int OkunmaSayisi { get; set; }
    }
}