using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Business;
using Microsoft.AspNetCore.Mvc;

namespace FencebirSubeProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KullaniciController : BaseController
    {
        private readonly SubeBS _SubeBS;
        private readonly KullaniciBS _KullaniciBS;
        public KullaniciController()
        {
            _SubeBS = new SubeBS();
            _KullaniciBS = new KullaniciBS();
        }

        #region Index

        [HttpGet]
        [ActionName("Index")]
        public IActionResult IndexGet()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            return View();
        }

        #endregion

        #region Kullanici

        [HttpGet]
        [ActionName("KullaniciKayit")]
        public async Task<IActionResult> KullaniciKayitGet(int? id)
        {
            int subeId = KullaniciDataGetir().SubeId;

            KullaniciKayitViewModel model;
            if (id.HasValue && id.Value > 0)
            {
                model = await _KullaniciBS.KullaniciKayitViewModelGetir(id.Value);
            }
            else
            {
                model = new KullaniciKayitViewModel()
                {
                    SubeId = subeId,
                    KullaniciId = id ?? 0,
                    AktifMi = true,
                };
            }

            if (subeId != 1 && model.SubeId != subeId)
                return Redirect("/Admin/Kullanici");

            var subeList = await _SubeBS.TumSubeListGetir();

            model.SubeList = subeList;

            return View(model);
        }

        [HttpPost]
        [ActionName("KullaniciKayit")]
        public async Task<JsonResult> KullaniciKayitPost(KullaniciKayitViewModel model)
        {
            var kullaniciData = KullaniciDataGetir();
            model.SubeId = kullaniciData.SubeId == 1 ? model.SubeId : kullaniciData.SubeId;
            model.IslemKullaniciId = kullaniciData.KullaniciId;
            model.IslemTarih = DateTime.Now;

            JsonResult result;

            var kullaniciKontrol = model.KullaniciId > 0 ? true : await _KullaniciBS.KullaniciKontrol(model.Eposta);

            if (kullaniciKontrol)
            {
                var id = await _KullaniciBS.KullaniciKaydet(model);

                if (id > 0)
                    result = Json(new { id = id, message = "success", operation = model.KullaniciId > 0 ? "update" : "insert" });
                else
                    result = Json(new { id = id, message = "error", operation = model.KullaniciId > 0 ? "update" : "insert" });
            }
            else
            {
                result = Json(new { id = 0, message = "control", operation = "" });
            }

            return result;
        }

        [HttpGet]
        [ActionName("KullaniciArama")]
        public async Task<IActionResult> KullaniciAramaGet()
        {
            var subeList = await _SubeBS.TumSubeListGetir();

            var model = new KullaniciAramaViewModel()
            {
                SubeId = KullaniciDataGetir().SubeId,
                SubeList = subeList,
                Aktiflik = 1
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("KullaniciArama")]
        public async Task<IActionResult> KullaniciAramaPost(KullaniciAramaViewModel model)
        {
            var result = await _KullaniciBS.KullaniciAramaSonucViewModelGetir(model);
            int totalCount = result.Any() ? result.FirstOrDefault().TotalCount : 0;

            return Json(new { draw = model.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = result });
        }

        [HttpGet]
        [ActionName("KullaniciSil")]
        public async Task<JsonResult> KullaniciSilGet(int id)
        {
            var data = await _KullaniciBS.KullaniciSil(id);

            JsonResult result = Json(data);

            return result;
        }

        #endregion
    }
}
