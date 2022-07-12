using FencebirSubeProject.Areas.Admin.Models.KayitViewModel;

namespace FencebirSubeProject.Areas.Admin.Models
{
    public class AyarKayitViewModel : BaseKayitViewModel
    {
        public bool IpBloklamaAktifMi { get; set; }
        public string IpBlokListesi { get; set; }
        public bool UygulamaAktifMi { get; set; }
    }
}
