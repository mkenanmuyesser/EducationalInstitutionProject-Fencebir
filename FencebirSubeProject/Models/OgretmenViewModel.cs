using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FencebirSubeProject.Models
{
    public class OgretmenViewModel
    {
        public string AdSoyad { get; set; }
        public string Unvan { get; set; }
        public string Aciklama { get; set; }
        public string Resim { get; set; }
        public string FacebookHesapUrl { get; set; }
        public string InstagramHesapUrl { get; set; }
        public string TwitterHesapUrl { get; set; }
        public string WhatsappHesapUrl { get; set; }
        public string YoutubeHesapUrl { get; set; }
    }
}