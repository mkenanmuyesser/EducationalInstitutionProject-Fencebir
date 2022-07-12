namespace FencebirSubeProject.Models
{
    public class IletisimViewModel
    {
        public string SirketAdres { get; set; }
        public string SirketTelefon1 { get; set; }
        public string SirketTelefon2 { get; set; }
        public string SirketFax1 { get; set; }
        public string SirketFax2 { get; set; }
        public string SirketEposta { get; set; }
        public string SirketMapCode { get; set; }
        public string FacebookHesapUrl { get; set; }
        public string InstagramHesapUrl { get; set; }
        public string TwitterHesapUrl { get; set; }
        public string WhatsappHesapUrl { get; set; }
        public string YoutubeHesapUrl { get; set; }
        public string Logo { get; set; }

        public IletisimTalepViewModel IletisimTalepData { get; set; }
    }
}