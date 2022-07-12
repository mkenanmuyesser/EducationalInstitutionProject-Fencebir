using FencebirSubeProject.Business;
using FencebirSubeProject.Infra;
using FencebirSubeProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FencebirSubeProject.Controllers
{
    [FrontEndActionFilter]
    public class BaseController : Controller
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
        public BaseController()
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

        public async Task<IActionResult> Index(string id)
        {
            var controller = RouteData.Values["Controller"].ToString();

            var subeId = await SubeIdGetir(id);
            if (subeId == 0)
                return Redirect("/");

            var bannerList = await _BannerBS.BannerListGetir(subeId);
            var duyuruList = await _DuyuruBS.DuyuruListGetir(subeId);
            var hosgeldinBilgiMetniData = await _IcerikBS.HosgeldinBilgiMetniDataGetir(subeId);
            var konuTipList = await _KonuTipBS.KonuTipListGetir();
            var videoBilgiMetniData = await _IcerikBS.VideoBilgiMetniDataGetir(subeId);
            var videoEmbedCodeData = await _IcerikBS.VideoBilgiEmbedCodeDataGetir(subeId);
            var subeList = controller == "Kurumsal" ? await _SubeBS.SubeListGetir() : new List<SubeViewModel>();
            var temsilciList = controller == "Kurumsal" ? await _SubeBS.TemsilciListGetir() : new List<SubeViewModel>();
            var ogrenciYorumList = await _OgrenciYorumBS.OgrenciYorumListGetir(subeId);
            var ogretmenList = await _OgretmenBS.OgretmenListGetir(true, subeId);
            var galeriList = await _GaleriBS.GaleriListGetir(true, subeId);
            var blogList = await _BlogBS.BlogListGetir(true, subeId);

            AnasayfaViewModel model = new AnasayfaViewModel()
            {
                BannerList = bannerList,
                DuyuruList = duyuruList,
                HosgeldinBilgiData = new HosgeldinBilgiViewModel()
                {
                    HosgeldinBilgiMetni = hosgeldinBilgiMetniData,
                    BilgiTalepData = new BilgiTalepViewModel()
                    {
                        KonuTipList = konuTipList
                    }
                },
                VideoBilgiData = new VideoBilgiViewModel()
                {
                    VideoBilgiMetni = videoBilgiMetniData,
                    VideoBilgiEmbedCode = videoEmbedCodeData,
                },
                SubeList = subeList,
                TemsilciList = temsilciList,
                OgrenciYorumList = ogrenciYorumList,
                OgretmenList = ogretmenList,
                GaleriList = galeriList,
                BlogList = blogList,

            };

            return View("~/Views/Kurumsal/Index.cshtml", model);
        }

        public async Task<IActionResult> Yayin()
        {
            var model = await _YayinBS.YayinListGetir();

            return View("~/Views/Kurumsal/Yayin.cshtml", model);
        }

        public async Task<IActionResult> YayinDetay()
        {
            var model = await _YayinBS.YayinListGetir();

            return View("~/Views/Kurumsal/YayinDetay.cshtml", model);
        }

        public async Task<FileResult> YayinDosyaIndir(int yayinid)
        {
            var dosya = await _YayinBS.YayinOrnekDosyaGetir(yayinid);
            return File(dosya, System.Net.Mime.MediaTypeNames.Application.Octet, "Ornek.pdf");
        }

        public async Task<IActionResult> Takvim(string id)
        {
            var subeId = await SubeIdGetir(id);
            if (subeId == 0)
                return Redirect("/");

            var model = await _EtkinlikBS.EtkinlikListGetir(subeId);

            return View("~/Views/Kurumsal/Takvim.cshtml", model);
        }

        public async Task<IActionResult> Ogretmen(string id)
        {
            var subeId = await SubeIdGetir(id);
            if (subeId == 0)
                return Redirect("/");

            var model = await _OgretmenBS.OgretmenListGetir(false, subeId);

            return View("~/Views/Kurumsal/Ogretmen.cshtml", model);
        }

        public async Task<IActionResult> Galeri(string id)
        {
            var subeId = await SubeIdGetir(id);
            if (subeId == 0)
                return Redirect("/");

            var model = await _GaleriBS.GaleriListGetir(false, subeId);

            return View("~/Views/Kurumsal/Galeri.cshtml", model);
        }

        public async Task<IActionResult> Blog(string id)
        {
            var subeId = await SubeIdGetir(id);
            if (subeId == 0)
                return Redirect("/");

            var model = await _BlogBS.BlogListGetir(false, subeId);

            return View("~/Views/Kurumsal/Blog.cshtml", model);
        }

        public async Task<IActionResult> BlogDetay(string id, int bdId)
        {
            var subeId = await SubeIdGetir(id);
            if (subeId == 0)
                return Redirect("/");

            var model = await _BlogBS.BlogDataGetir(bdId);

            return View("~/Views/Kurumsal/BlogDetay.cshtml", model);
        }

        public async Task<IActionResult> Iletisim(string id)
        {
            var subeId = await SubeIdGetir(id);
            if (subeId == 0)
                return Redirect("/");

            var model = await _SubeBS.IletisimDataGetir(subeId);

            return View("~/Views/Kurumsal/Iletisim.cshtml", model);
        }

        public async Task<IActionResult> Hakkimizda(string id)
        {
            var subeId = await SubeIdGetir(id);
            if (subeId == 0)
                return Redirect("/");

            var hakkimizdaUstBilgiMetni = await _IcerikBS.HakkimizdaUstBilgiMetniDataGetir(subeId);
            var hakkimizdaAltBilgiMetni = await _IcerikBS.HakkimizdaAltBilgiMetniDataGetir(subeId);
            var model = new HakkimizdaViewModel()
            {
                HakkimizdaUstBilgiMetni = hakkimizdaUstBilgiMetni,
                HakkimizdaAltBilgiMetni = hakkimizdaAltBilgiMetni
            };

            return View("~/Views/Kurumsal/Hakkimizda.cshtml", model); ;
        }

        public async Task<IActionResult> CerezPolitikasi()
        {
            var model = await _IcerikBS.CerezPolitikasiBilgiMetniDataGetir();

            return View("~/Views/Kurumsal/CerezPolitikasi.cshtml", model);
        }

        public IActionResult Bakim(string id)
        {
            return View();
        }

        public IActionResult Hata(string id)
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> BilgiTalepGonder(BilgiTalepViewModel model)
        {
            var epostaResult = await _EmailHelper.BilgiTalepEpostaGonder(model, 1);
            var mesajResult = await _MesajBS.BilgiTalepKaydet(model, 1);

            var result = Json(new { eposta = epostaResult, mesaj = mesajResult > 0 });
            return result;
        }

        [HttpPost]
        public async Task<JsonResult> IletisimTalepGonder(IletisimTalepViewModel model)
        {
            var epostaResult = await _EmailHelper.IletisimTalepEpostaGonder(model, 1);
            var mesajResult = await _MesajBS.IletisimTalepKaydet(model, 1);

            var result = Json(new { eposta = epostaResult, mesaj = mesajResult > 0 });
            return result;
        }

        private async Task<int> SubeIdGetir(string id)
        {
            int subeId = 0;
            string controller = RouteData.Values["controller"].ToString();
            if (controller == "Kurumsal")
            {
                subeId = 1;
            }
            else
            {
                if (string.IsNullOrEmpty(id))
                    subeId = 0;
                else
                    subeId = await _SubeBS.SubeTemsilciGetir(id);
            }

            return subeId;
        }
    }
}
