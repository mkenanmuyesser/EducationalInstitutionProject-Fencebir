using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FencebirSubeProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubeTemsilciController : BaseController
    {
        private readonly SubeBS _SubeBS;
        private readonly SubeTipBS _SubeTipBS;
        private readonly SubeSehirBS _SubeSehirBS;
        public SubeTemsilciController()
        {
            _SubeBS = new SubeBS();
            _SubeTipBS = new SubeTipBS();
            _SubeSehirBS = new SubeSehirBS();
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

        #region SubeTemsilci

        [HttpGet]
        [ActionName("SubeTemsilciKayit")]
        public async Task<IActionResult> SubeTemsilciKayitGet(int? id)
        {
            int subeId = KullaniciDataGetir().SubeId;

            SubeKayitViewModel model;
            if (id.HasValue && id.Value > 0)
            {
                model = await _SubeBS.SubeKayitViewModelGetir(id.Value);

                if (subeId != 1 && model.SubeId != subeId)
                    return Redirect("/Admin/SubeTemsilci");
            }
            else
            {
                if (subeId != 1)
                    return Redirect("/Admin/SubeTemsilci");

                model = new SubeKayitViewModel()
                {
                    SubeTipId = 2,
                    SubeSehirId = 6,
                    SubeAdi = "Şube Adı",
                    SubeAttribute = "SubeAdi",
                    Resim = "/Uploads/Site/noimg.png",
                    Sira = 1,
                    AktifMi = true,
                };
            }

            var subeTipList = await _SubeTipBS.SubeTipListGetir();
            subeTipList.RemoveAt(0);
            var subeSehirList = await _SubeSehirBS.SubeSehirListGetir();

            model.SubeTipList = subeTipList;
            model.SubeSehirList = subeSehirList;

            return View(model);
        }

        [HttpPost]
        [ActionName("SubeTemsilciKayit")]
        public async Task<JsonResult> SubeTemsilciKayitPost(SubeKayitViewModel model, IFormFile dosya)
        {
            var kullaniciData = KullaniciDataGetir();
            model.IslemKullaniciId = kullaniciData.KullaniciId;
            model.IslemTarih = DateTime.Now;

            JsonResult result;

            if (dosya != null && dosya.Length > 0)
            {
                string ext = dosya.FileName.Split('.').LastOrDefault().ToLower();
                if (ext != null && (ext == "gif" || ext == "png" || ext == "jpg" || ext == "jpeg" || ext == "jpeg"))
                {
                    using (var ms = new MemoryStream())
                    {
                        dosya.CopyTo(ms);
                        model.Dosya = ms.ToArray();
                        model.DosyaAdi = dosya.FileName;
                    }
                }
                else
                {
                    result = Json(new { id = 0, message = "control", operation = "file" });
                }
            }

            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (model.SubeAttribute.Split(' ').Count() > 1 || !regexItem.IsMatch(model.SubeAttribute))
            {
                result = Json(new { id = 0, message = "control", operation = "wrongattribute" });
            }
            else
            {
                var subeKontrol = await _SubeBS.SubeKontrol(model.SubeId, model.SubeAttribute);

                if (subeKontrol)
                {
                    var id = await _SubeBS.SubeKaydet(model);

                    if (id > 0)
                        result = Json(new { id = id, message = "success", operation = model.SubeId > 0 ? "update" : "insert" });
                    else
                        result = Json(new { id = id, message = "error", operation = model.SubeId > 0 ? "update" : "insert" });
                }
                else
                {
                    result = Json(new { id = 0, message = "control", operation = "sameattribute" });
                }
            }

            return result;
        }

        [HttpGet]
        [ActionName("SubeTemsilciArama")]
        public async Task<IActionResult> SubeTemsilciAramaGet()
        {
            var subeTipList = await _SubeTipBS.SubeTipListGetir();
            var subeSehirList = await _SubeSehirBS.SubeSehirListGetir();

            var model = new SubeAramaViewModel()
            {
                SubeTipList = subeTipList,
                SubeSehirList = subeSehirList,
                Aktiflik = 1
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("SubeTemsilciArama")]
        public async Task<IActionResult> SubeTemsilciAramaPost(SubeAramaViewModel model)
        {
            int subeId = KullaniciDataGetir().SubeId;

            if (subeId == 1)
            {
                model.SubeId = subeId;
            }
            else
            {
                model.SubeId = subeId;
                model.Aktiflik = -1;
                model.SubeTipId = 0;
                model.SubeSehirId = 0;
                model.SubeAdi = null;
            }

            var result = await _SubeBS.SubeAramaSonucViewModelGetir(model);
            int totalCount = result.Any() ? result.FirstOrDefault().TotalCount : 0;

            return Json(new { draw = model.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = result });
        }

        [HttpGet]
        [ActionName("SubeTemsilciSil")]
        public async Task<JsonResult> SubeTemsilciGet(int id)
        {
            var data = await _SubeBS.SubeSil(id);

            JsonResult result = Json(data);

            return result;
        }

        #endregion
    }
}
