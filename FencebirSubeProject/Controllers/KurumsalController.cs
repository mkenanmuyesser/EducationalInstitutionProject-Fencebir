using FencebirSubeProject.Business;
using FencebirSubeProject.Infra;
using FencebirSubeProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace FencebirSubeProject.Controllers
{
    public class KurumsalController : BaseController
    {
        private readonly BannerBS _BannerBS;
        private readonly DuyuruBS _DuyuruBS;
        private readonly IcerikBS _IcerikBS;
        private readonly KonuTipBS _KonuTipBS;
        private readonly SubeBS _SubeBS;
        private readonly OgrenciYorumBS _OgrenciYorumBS;
        private readonly OgretmenBS _OgretmenBS;
        private readonly GaleriBS _GaleriBS;
        private readonly BlogBS _BlogBS;
        private readonly YayinBS _YayinBS;
        private readonly EtkinlikBS _EtkinlikBS;
        private readonly MesajBS _MesajBS;
        private readonly EmailHelper _EmailHelper;
        private readonly SubeSehirBS _SubeSehirBS;
        private readonly KurumTipBS _KurumTipBS;
        public KurumsalController()
        {
            _BannerBS = new BannerBS();
            _DuyuruBS = new DuyuruBS();
            _IcerikBS = new IcerikBS();
            _KonuTipBS = new KonuTipBS();
            _SubeBS = new SubeBS();
            _OgrenciYorumBS = new OgrenciYorumBS();
            _OgretmenBS = new OgretmenBS();
            _GaleriBS = new GaleriBS();
            _BlogBS = new BlogBS();
            _YayinBS = new YayinBS();
            _EtkinlikBS = new EtkinlikBS();
            _MesajBS = new MesajBS();
            _SubeSehirBS = new SubeSehirBS();
            _KurumTipBS = new KurumTipBS();
            _EmailHelper = new EmailHelper();
        }

        public async Task<IActionResult> Franchise()
        {
            var franchiseBilgiMetni = await _IcerikBS.FranchiseBilgiMetniDataGetir();
            var sehirBilgiList = await _SubeSehirBS.SehirBilgiListGetir();
            var kurumTipList = await _KurumTipBS.KurumTipListGetir();

            var model = new FranchiseViewModel()
            {
                FranchiseMetin = franchiseBilgiMetni,
                FranchiseTalepData = new FranchiseTalepViewModel()
                {
                    SehirBilgiList = sehirBilgiList,
                    KurumTipList = kurumTipList
                }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> FranchiseTalepGonder(FranchiseTalepViewModel model, IFormFile dosya)
        {
            if (dosya != null && dosya.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    dosya.CopyTo(ms);
                    model.Dosya = ms.ToArray();
                    model.DosyaAdi = dosya.FileName;
                }
            }

            var epostaResult = await _EmailHelper.FranchiseTalepEpostaGonder(model, 1);
            var mesajResult = await _MesajBS.FranchiseTalepKaydet(model, 1);

            var result = Json(new { eposta = epostaResult, mesaj = mesajResult > 0 });
            return result;
        }
    }
}
