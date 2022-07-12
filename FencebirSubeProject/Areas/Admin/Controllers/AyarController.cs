using FencebirSubeProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AyarController : BaseController
    {
        #region Index

        [HttpGet]
        [ActionName("Index")]
        public async Task<IActionResult> IndexGet()
        {
            AyarKayitViewModel model = new AyarKayitViewModel()
            {
               
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<JsonResult> IndexPost(AyarKayitViewModel model)
        {
            //var kullaniciData = KullaniciDataGetir();           
            //model.IslemKullaniciId = kullaniciData.KullaniciId;
            //model.IslemTarih = DateTime.Now;

            //var id = await _EtkinlikBS.EtkinlikKaydet(model);

            //JsonResult result;
            //if (id > 0)
            //    result = Json(new { id = id, message = "success", operation = model.EtkinlikId > 0 ? "update" : "insert" });
            //else
            //    result = Json(new { id = id, message = "error", operation = model.EtkinlikId > 0 ? "update" : "insert" });

            //return result;

            return null;
        }

        #endregion
    }
}
