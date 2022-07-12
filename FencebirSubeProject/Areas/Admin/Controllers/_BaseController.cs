using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Infra;
using Microsoft.AspNetCore.Mvc;

namespace FencebirSubeProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminActionFilter]
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        [HttpGet]
        [ActionName("Cikis")]
        public IActionResult CikisGet()
        {
            //sessionları sil ve girişe yönlendir
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("KullaniciGirisData");
            return Redirect("/Admin/Giris");
        }

        public KullaniciGirisModel KullaniciDataGetir()
        {
            return ViewBag.KullaniciGirisData as KullaniciGirisModel;
        }
    }
}
