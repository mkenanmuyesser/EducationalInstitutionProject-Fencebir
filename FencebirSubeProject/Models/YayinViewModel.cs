namespace FencebirSubeProject.Models
{
    public class YayinViewModel
    {
        public int YayinId { get; set; }
        public string Ad { get; set; }
        public decimal? EskiFiyat { get; set; }
        public decimal? YeniFiyat { get; set; }
        public string Resim { get; set; }
        public bool DosyaVarmi { get; set; }
        //public string OzetDosya { get; set; }
    }
}
