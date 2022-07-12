using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Business;
using FencebirSubeProject.Infra;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GirisController : Controller
    {
        KullaniciBS _KullaniciBS;
        public GirisController()
        {
            _KullaniciBS = new KullaniciBS();
        }

        #region Index

        [HttpGet]
        [ActionName("Index")]
        public IActionResult IndexGet(string returnUrl)
        {
            KullaniciGirisViewModel model = new KullaniciGirisViewModel()
            {
                ReturnUrl = returnUrl,
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<JsonResult> IndexPost(KullaniciGirisViewModel model)
        {
            var kullaniciGirisData = await _KullaniciBS.KullaniciGirisDataGetir(model);

            JsonResult result;
            if (kullaniciGirisData == null)
            {
                result = Json(new { message = "error", returnUrl = ""});
            }
            else
            {
                HttpContext.Session.SetObjectAsJson("KullaniciGirisData", kullaniciGirisData);

                var returnUrl = string.IsNullOrEmpty(model.ReturnUrl) ? "/Admin": model.ReturnUrl;
                result = Json(new { message = "success", returnUrl = returnUrl });
            }

            return result;
        }

        #endregion
    }
}
