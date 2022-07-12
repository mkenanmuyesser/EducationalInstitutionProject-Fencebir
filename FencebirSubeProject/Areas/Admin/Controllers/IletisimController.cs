using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Business;
using FencebirSubeProject.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FencebirSubeProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IletisimController : BaseController
    {
        private readonly SubeBS _SubeBS;
        private readonly MesajBS _MesajBS;
        public IletisimController()
        {
            _SubeBS = new SubeBS();
            _MesajBS = new MesajBS();
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

        #region Bilgi Talep

        [HttpGet]
        [ActionName("BilgiTalepArama")]
        public async Task<IActionResult> BilgiTalepAramaGet()
        {
            var subeList = await _SubeBS.TumSubeListGetir();

            var model = new MesajAramaViewModel()
            {
                SubeId = KullaniciDataGetir().SubeId,
                SubeList = subeList,
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("BilgiTalepArama")]
        public async Task<IActionResult> BilgiTalepAramaPost(MesajAramaViewModel model)
        {
            var result = await _MesajBS.MesajAramaSonucViewModelGetir(model, MesajTipEnum.BilgiTalep);
            int totalCount = result.Any() ? result.FirstOrDefault().TotalCount : 0;

            return Json(new { draw = model.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = result });
        }

        #endregion

        #region İletişim Talep

        [HttpGet]
        [ActionName("IletisimTalepArama")]
        public async Task<IActionResult> IletisimTalepAramaGet()
        {
            var subeList = await _SubeBS.TumSubeListGetir();

            var model = new MesajAramaViewModel()
            {
                SubeId = KullaniciDataGetir().SubeId,
                SubeList = subeList,
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("IletisimTalepArama")]
        public async Task<IActionResult> IletisimTalepAramaPost(MesajAramaViewModel model)
        {
            var result = await _MesajBS.MesajAramaSonucViewModelGetir(model, MesajTipEnum.IletisimTalep);
            int totalCount = result.Any() ? result.FirstOrDefault().TotalCount : 0;

            return Json(new { draw = model.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = result });
        }

        #endregion

        #region Franchise Talep

        [HttpGet]
        [ActionName("FranchiseTalepArama")]
        public async Task<IActionResult> FranchiseTalepAramaGet()
        {
            int subeId = KullaniciDataGetir().SubeId;

            if (subeId != 1)
                return Redirect("/Admin/Iletisim/Index");

            var subeList = await _SubeBS.TumSubeListGetir();

            var model = new MesajAramaViewModel()
            {
                SubeId = KullaniciDataGetir().SubeId,
                SubeList = subeList,
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("FranchiseTalepArama")]
        public async Task<IActionResult> FranchiseTalepAramaPost(MesajAramaViewModel model)
        {
            var result = await _MesajBS.MesajAramaSonucViewModelGetir(model, MesajTipEnum.FranchiseTalep);
            int totalCount = result.Any() ? result.FirstOrDefault().TotalCount : 0;

            return Json(new { draw = model.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = result });
        }

        [HttpGet]
        [ActionName("MesajDosyaIndir")]
        public async Task<JsonResult> MesajDosyaIndirGet(int id)
        {
            var data = await _MesajBS.MesajDosyaGetir(id);

            JsonResult result = Json(new { file = Convert.ToBase64String(data.Dosya, 0, data.Dosya.Length), fileName = data.DosyaAdi });

            return result;
        }

        #endregion
    }
}
