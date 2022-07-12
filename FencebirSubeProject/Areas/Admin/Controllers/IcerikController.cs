using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FencebirSubeProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IcerikController : BaseController
    {
        private readonly SubeBS _SubeBS;
        private readonly EtkinlikTipBS _EtkinlikTipBS;
        private readonly EtkinlikBS _EtkinlikBS;
        private readonly DuyuruBS _DuyuruBS;
        private readonly BannerBS _BannerBS;
        private readonly BannerTipBS _BannerTipBS;
        private readonly OgrenciYorumBS _OgrenciYorumBS;
        private readonly OgretmenBS _OgretmenBS;
        private readonly GaleriBS _GaleriBS;
        private readonly GaleriTipBS _GaleriTipBS;
        private readonly YayinBS _YayinBS;
        private readonly BlogBS _BlogBS;
        private readonly BlogTipBS _BlogTipBS;
        private readonly IcerikBS _IcerikBS;
        private readonly IcerikTipBS _IcerikTipBS;

        public IcerikController()
        {
            _SubeBS = new SubeBS();
            _EtkinlikTipBS = new EtkinlikTipBS();
            _EtkinlikBS = new EtkinlikBS();
            _DuyuruBS = new DuyuruBS();
            _BannerBS = new BannerBS();
            _BannerTipBS = new BannerTipBS();
            _OgrenciYorumBS = new OgrenciYorumBS();
            _OgretmenBS = new OgretmenBS();
            _GaleriBS = new GaleriBS();
            _GaleriTipBS = new GaleriTipBS();
            _YayinBS = new YayinBS();
            _BlogBS = new BlogBS();
            _BlogTipBS = new BlogTipBS();
            _IcerikBS = new IcerikBS();
            _IcerikTipBS = new IcerikTipBS();
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

        #region Etkinlik

        [HttpGet]
        [ActionName("EtkinlikKayit")]
        public async Task<IActionResult> EtkinlikKayitGet(int? id)
        {
            int subeId = KullaniciDataGetir().SubeId;

            EtkinlikKayitViewModel model;
            if (id.HasValue && id.Value > 0)
            {
                model = await _EtkinlikBS.EtkinlikKayitViewModelGetir(id.Value);
            }
            else
            {
                model = new EtkinlikKayitViewModel()
                {
                    SubeId = subeId,
                    EtkinlikId = id ?? 0,
                    Tarih = DateTime.Now.Date.ToString("dd.MM.yyyy"),
                    BaslangicZaman = new TimeSpan(9, 0, 0).ToString(),
                    BitisZaman = new TimeSpan(10, 0, 0).ToString(),
                    Sira = 1,
                    AktifMi = true,
                };
            }

            if (subeId != 1 && model.SubeId != subeId)
                return Redirect("/Admin/Icerik");

            var subeList = await _SubeBS.TumSubeListGetir();
            var etkinlikTipList = await _EtkinlikTipBS.EtkinlikTipListGetir();

            model.SubeList = subeList;
            model.EtkinlikTipList = etkinlikTipList;

            return View(model);
        }

        [HttpPost]
        [ActionName("EtkinlikKayit")]
        public async Task<JsonResult> EtkinlikKayitPost(EtkinlikKayitViewModel model)
        {
            var kullaniciData = KullaniciDataGetir();
            model.SubeId = kullaniciData.SubeId == 1 ? model.SubeId : kullaniciData.SubeId;
            model.IslemKullaniciId = kullaniciData.KullaniciId;
            model.IslemTarih = DateTime.Now;

            var id = await _EtkinlikBS.EtkinlikKaydet(model);

            JsonResult result;
            if (id > 0)
                result = Json(new { id = id, message = "success", operation = model.EtkinlikId > 0 ? "update" : "insert" });
            else
                result = Json(new { id = id, message = "error", operation = model.EtkinlikId > 0 ? "update" : "insert" });

            return result;
        }

        [HttpGet]
        [ActionName("EtkinlikArama")]
        public async Task<IActionResult> EtkinlikAramaGet()
        {
            var subeList = await _SubeBS.TumSubeListGetir();
            var etkinlikTipList = await _EtkinlikTipBS.EtkinlikTipListGetir();

            var model = new EtkinlikAramaViewModel()
            {
                SubeId = KullaniciDataGetir().SubeId,
                SubeList = subeList,
                EtkinlikTipList = etkinlikTipList,
                Aktiflik = 1
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("EtkinlikArama")]
        public async Task<IActionResult> EtkinlikAramaPost(EtkinlikAramaViewModel model)
        {
            var result = await _EtkinlikBS.EtkinlikAramaSonucViewModelGetir(model);
            int totalCount = result.Any() ? result.FirstOrDefault().TotalCount : 0;

            return Json(new { draw = model.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = result });
        }

        [HttpGet]
        [ActionName("EtkinlikSil")]
        public async Task<JsonResult> EtkinlikSilGet(int id)
        {
            var data = await _EtkinlikBS.EtkinlikSil(id);

            JsonResult result = Json(data);

            return result;
        }

        #endregion

        #region Duyuru

        [HttpGet]
        [ActionName("DuyuruKayit")]
        public async Task<IActionResult> DuyuruKayitGet(int? id)
        {
            int subeId = KullaniciDataGetir().SubeId;

            DuyuruKayitViewModel model;
            if (id.HasValue && id.Value > 0)
            {
                model = await _DuyuruBS.DuyuruKayitViewModelGetir(id.Value);
            }
            else
            {
                model = new DuyuruKayitViewModel()
                {
                    SubeId = subeId,
                    DuyuruId = id ?? 0,
                    Tarih = DateTime.Now.Date.ToString("dd.MM.yyyy"),
                    Sira = 1,
                    AktifMi = true,
                };
            }

            if (subeId != 1 && model.SubeId != subeId)
                return Redirect("/Admin/Icerik");

            var subeList = await _SubeBS.TumSubeListGetir();

            model.SubeList = subeList;

            return View(model);
        }

        [HttpPost]
        [ActionName("DuyuruKayit")]
        public async Task<JsonResult> DuyuruKayitPost(DuyuruKayitViewModel model)
        {
            var kullaniciData = KullaniciDataGetir();
            model.SubeId = kullaniciData.SubeId == 1 ? model.SubeId : kullaniciData.SubeId;
            model.IslemKullaniciId = kullaniciData.KullaniciId;
            model.IslemTarih = DateTime.Now;

            var id = await _DuyuruBS.DuyuruKaydet(model);

            JsonResult result;
            if (id > 0)
                result = Json(new { id = id, message = "success", operation = model.DuyuruId > 0 ? "update" : "insert" });
            else
                result = Json(new { id = id, message = "error", operation = model.DuyuruId > 0 ? "update" : "insert" });

            return result;
        }

        [HttpGet]
        [ActionName("DuyuruArama")]
        public async Task<IActionResult> DuyuruAramaGet()
        {
            var subeList = await _SubeBS.TumSubeListGetir();

            var model = new DuyuruAramaViewModel()
            {
                SubeId = KullaniciDataGetir().SubeId,
                SubeList = subeList,
                Aktiflik = 1
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("DuyuruArama")]
        public async Task<IActionResult> DuyuruAramaPost(DuyuruAramaViewModel model)
        {
            var result = await _DuyuruBS.DuyuruAramaSonucViewModelGetir(model);
            int totalCount = result.Any() ? result.FirstOrDefault().TotalCount : 0;

            return Json(new { draw = model.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = result });
        }

        [HttpGet]
        [ActionName("DuyuruSil")]
        public async Task<JsonResult> DuyuruSilGet(int id)
        {
            var data = await _DuyuruBS.DuyuruSil(id);

            JsonResult result = Json(data);

            return result;
        }

        #endregion

        #region Banner

        [HttpGet]
        [ActionName("BannerKayit")]
        public async Task<IActionResult> BannerKayitGet(int? id)
        {
            int subeId = KullaniciDataGetir().SubeId;

            BannerKayitViewModel model;
            if (id.HasValue && id.Value > 0)
            {
                model = await _BannerBS.BannerKayitViewModelGetir(id.Value);
            }
            else
            {
                model = new BannerKayitViewModel()
                {
                    SubeId = subeId,
                    Resim = "/Uploads/Site/noimg.png",
                    Sira = 1,
                    AktifMi = true,
                };
            }

            if (subeId != 1 && model.SubeId != subeId)
                return Redirect("/Admin/Icerik");

            var subeList = await _SubeBS.TumSubeListGetir();
            var bannerTipList = await _BannerTipBS.BannerTipListGetir();

            model.SubeList = subeList;
            model.BannerTipList = bannerTipList;

            return View(model);
        }

        [HttpPost]
        [ActionName("BannerKayit")]
        public async Task<JsonResult> BannerKayitPost(BannerKayitViewModel model, IFormFile dosya)
        {
            var kullaniciData = KullaniciDataGetir();
            model.SubeId = kullaniciData.SubeId == 1 ? model.SubeId : kullaniciData.SubeId;
            model.IslemKullaniciId = kullaniciData.KullaniciId;
            model.IslemTarih = DateTime.Now;

            JsonResult result;

            if (dosya != null && dosya.Length > 0)
            {
                string ext = dosya.FileName.Split('.').LastOrDefault().ToLower();
                if (ext != null && (ext == "gif" || ext == "png" || ext == "jpg" || ext == "jpeg"))
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

            var id = await _BannerBS.BannerKaydet(model);

            if (id > 0)
                result = Json(new { id = id, message = "success", operation = model.BannerId > 0 ? "update" : "insert" });
            else
                result = Json(new { id = id, message = "error", operation = model.BannerId > 0 ? "update" : "insert" });

            return result;
        }

        [HttpGet]
        [ActionName("BannerArama")]
        public async Task<IActionResult> BannerAramaGet()
        {
            var subeList = await _SubeBS.TumSubeListGetir();
            var bannerTipList = await _BannerTipBS.BannerTipListGetir();

            var model = new BannerAramaViewModel()
            {
                SubeId = KullaniciDataGetir().SubeId,
                SubeList = subeList,
                BannerTipList = bannerTipList,
                Aktiflik = 1
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("BannerArama")]
        public async Task<IActionResult> BannerAramaPost(BannerAramaViewModel model)
        {
            var result = await _BannerBS.BannerAramaSonucViewModelGetir(model);
            int totalCount = result.Any() ? result.FirstOrDefault().TotalCount : 0;

            return Json(new { draw = model.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = result });
        }

        [HttpGet]
        [ActionName("BannerSil")]
        public async Task<JsonResult> BannerSilGet(int id)
        {
            var data = await _BannerBS.BannerSil(id);

            JsonResult result = Json(data);

            return result;
        }

        #endregion

        #region Blog

        [HttpGet]
        [ActionName("BlogKayit")]
        public async Task<IActionResult> BlogKayitGet(int? id)
        {
            int subeId = KullaniciDataGetir().SubeId;

            BlogKayitViewModel model;
            if (id.HasValue && id.Value > 0)
            {
                model = await _BlogBS.BlogKayitViewModelGetir(id.Value);
            }
            else
            {
                model = new BlogKayitViewModel()
                {
                    SubeId = subeId,
                    Anasayfa = true,
                    Resim = "/Uploads/Site/noimg.png",
                    Sira = 1,
                    AktifMi = true,
                };
            }

            if (subeId != 1 && model.SubeId != subeId)
                return Redirect("/Admin/Icerik");

            var subeList = await _SubeBS.TumSubeListGetir();
            var blogTipList = await _BlogTipBS.BlogTipListGetir();

            model.SubeList = subeList;
            model.BlogTipList = blogTipList;

            return View(model);
        }

        [HttpPost]
        [ActionName("BlogKayit")]
        public async Task<JsonResult> BlogKayitPost(BlogKayitViewModel model, IFormFile dosya)
        {
            var kullaniciData = KullaniciDataGetir();
            model.SubeId = kullaniciData.SubeId == 1 ? model.SubeId : kullaniciData.SubeId;
            model.IslemKullaniciId = kullaniciData.KullaniciId;
            model.IslemTarih = DateTime.Now;

            JsonResult result;

            if (dosya != null && dosya.Length > 0)
            {
                string ext = dosya.FileName.Split('.').LastOrDefault().ToLower();
                if (ext != null && (ext == "gif" || ext == "png" || ext == "jpg" || ext == "jpeg"))
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

            var id = await _BlogBS.BlogKaydet(model);

            if (id > 0)
                result = Json(new { id = id, message = "success", operation = model.BlogId > 0 ? "update" : "insert" });
            else
                result = Json(new { id = id, message = "error", operation = model.BlogId > 0 ? "update" : "insert" });

            return result;
        }

        [HttpGet]
        [ActionName("BlogArama")]
        public async Task<IActionResult> BlogAramaGet()
        {
            var subeList = await _SubeBS.TumSubeListGetir();
            var blogTipList = await _BlogTipBS.BlogTipListGetir();

            var model = new BlogAramaViewModel()
            {
                SubeId = KullaniciDataGetir().SubeId,
                SubeList = subeList,
                BlogTipList = blogTipList,
                Aktiflik = 1
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("BlogArama")]
        public async Task<IActionResult> BlogAramaPost(BlogAramaViewModel model)
        {
            var result = await _BlogBS.BlogAramaSonucViewModelGetir(model);
            int totalCount = result.Any() ? result.FirstOrDefault().TotalCount : 0;

            return Json(new { draw = model.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = result });
        }

        [HttpGet]
        [ActionName("BlogSil")]
        public async Task<JsonResult> BlogSilGet(int id)
        {
            var data = await _BlogBS.BlogSil(id);

            JsonResult result = Json(data);

            return result;
        }

        #endregion

        #region Öğrenci Yorum

        [HttpGet]
        [ActionName("OgrenciYorumKayit")]
        public async Task<IActionResult> OgrenciYorumKayitGet(int? id)
        {
            int subeId = KullaniciDataGetir().SubeId;

            OgrenciYorumKayitViewModel model;
            if (id.HasValue && id.Value > 0)
            {
                model = await _OgrenciYorumBS.OgrenciYorumKayitViewModelGetir(id.Value);
            }
            else
            {
                model = new OgrenciYorumKayitViewModel()
                {
                    SubeId = subeId,
                    Resim = "/Uploads/Site/noimg.png",
                    Sira = 1,
                    AktifMi = true,
                };
            }

            if (subeId != 1 && model.SubeId != subeId)
                return Redirect("/Admin/Icerik");

            var subeList = await _SubeBS.TumSubeListGetir();

            model.SubeList = subeList;

            return View(model);
        }

        [HttpPost]
        [ActionName("OgrenciYorumKayit")]
        public async Task<JsonResult> OgrenciYorumKayitPost(OgrenciYorumKayitViewModel model, IFormFile dosya)
        {
            var kullaniciData = KullaniciDataGetir();
            model.SubeId = kullaniciData.SubeId == 1 ? model.SubeId : kullaniciData.SubeId;
            model.IslemKullaniciId = kullaniciData.KullaniciId;
            model.IslemTarih = DateTime.Now;

            JsonResult result;

            if (dosya != null && dosya.Length > 0)
            {
                string ext = dosya.FileName.Split('.').LastOrDefault().ToLower();
                if (ext != null && (ext == "gif" || ext == "png" || ext == "jpg" || ext == "jpeg"))
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

            var id = await _OgrenciYorumBS.OgrenciYorumKaydet(model);

            if (id > 0)
                result = Json(new { id = id, message = "success", operation = model.OgrenciYorumId > 0 ? "update" : "insert" });
            else
                result = Json(new { id = id, message = "error", operation = model.OgrenciYorumId > 0 ? "update" : "insert" });

            return result;
        }

        [HttpGet]
        [ActionName("OgrenciYorumArama")]
        public async Task<IActionResult> OgrenciYorumAramaGet()
        {
            var subeList = await _SubeBS.TumSubeListGetir();

            var model = new OgrenciYorumAramaViewModel()
            {
                SubeId = KullaniciDataGetir().SubeId,
                SubeList = subeList,
                Aktiflik = 1
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("OgrenciYorumArama")]
        public async Task<IActionResult> OgrenciYorumAramaPost(OgrenciYorumAramaViewModel model)
        {
            var result = await _OgrenciYorumBS.OgrenciYorumAramaSonucViewModelGetir(model);
            int totalCount = result.Any() ? result.FirstOrDefault().TotalCount : 0;

            return Json(new { draw = model.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = result });
        }

        [HttpGet]
        [ActionName("OgrenciYorumSil")]
        public async Task<JsonResult> OgrenciYorumSilGet(int id)
        {
            var data = await _OgrenciYorumBS.OgrenciYorumSil(id);

            JsonResult result = Json(data);

            return result;
        }

        #endregion

        #region Öğretmen

        [HttpGet]
        [ActionName("OgretmenKayit")]
        public async Task<IActionResult> OgretmenKayitGet(int? id)
        {
            int subeId = KullaniciDataGetir().SubeId;

            OgretmenKayitViewModel model;
            if (id.HasValue && id.Value > 0)
            {
                model = await _OgretmenBS.OgretmenKayitViewModelGetir(id.Value);
            }
            else
            {
                model = new OgretmenKayitViewModel()
                {
                    SubeId = subeId,
                    Anasayfa = true,
                    Resim = "/Uploads/Site/noimg.png",
                    Sira = 1,
                    AktifMi = true,
                };
            }

            if (subeId != 1 && model.SubeId != subeId)
                return Redirect("/Admin/Icerik");

            var subeList = await _SubeBS.TumSubeListGetir();

            model.SubeList = subeList;

            return View(model);
        }

        [HttpPost]
        [ActionName("OgretmenKayit")]
        public async Task<JsonResult> OgretmenKayitPost(OgretmenKayitViewModel model, IFormFile dosya)
        {
            var kullaniciData = KullaniciDataGetir();
            model.SubeId = kullaniciData.SubeId == 1 ? model.SubeId : kullaniciData.SubeId;
            model.IslemKullaniciId = kullaniciData.KullaniciId;
            model.IslemTarih = DateTime.Now;

            JsonResult result;

            if (dosya != null && dosya.Length > 0)
            {
                string ext = dosya.FileName.Split('.').LastOrDefault().ToLower();
                if (ext != null && (ext == "gif" || ext == "png" || ext == "jpg" || ext == "jpeg"))
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

            var id = await _OgretmenBS.OgretmenKaydet(model);

            if (id > 0)
                result = Json(new { id = id, message = "success", operation = model.OgretmenId > 0 ? "update" : "insert" });
            else
                result = Json(new { id = id, message = "error", operation = model.OgretmenId > 0 ? "update" : "insert" });

            return result;
        }

        [HttpGet]
        [ActionName("OgretmenArama")]
        public async Task<IActionResult> OgretmenAramaGet()
        {
            var subeList = await _SubeBS.TumSubeListGetir();

            var model = new OgretmenAramaViewModel()
            {
                SubeId = KullaniciDataGetir().SubeId,
                SubeList = subeList,
                Aktiflik = 1
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("OgretmenArama")]
        public async Task<IActionResult> OgretmenAramaPost(OgretmenAramaViewModel model)
        {
            var result = await _OgretmenBS.OgretmenAramaSonucViewModelGetir(model);
            int totalCount = result.Any() ? result.FirstOrDefault().TotalCount : 0;

            return Json(new { draw = model.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = result });
        }

        [HttpGet]
        [ActionName("OgretmenSil")]
        public async Task<JsonResult> OgretmenSilGet(int id)
        {
            var data = await _OgretmenBS.OgretmenSil(id);

            JsonResult result = Json(data);

            return result;
        }

        #endregion

        #region Galeri

        [HttpGet]
        [ActionName("GaleriKayit")]
        public async Task<IActionResult> GaleriKayitGet(int? id)
        {
            int subeId = KullaniciDataGetir().SubeId;

            GaleriKayitViewModel model;
            if (id.HasValue && id.Value > 0)
            {
                model = await _GaleriBS.GaleriKayitViewModelGetir(id.Value);
            }
            else
            {
                model = new GaleriKayitViewModel()
                {
                    SubeId = subeId,
                    Anasayfa = true,
                    Resim = "/Uploads/Site/noimg.png",
                    Sira = 1,
                    AktifMi = true,
                };
            }

            if (subeId != 1 && model.SubeId != subeId)
                return Redirect("/Admin/Icerik");

            var subeList = await _SubeBS.TumSubeListGetir();
            var galeriTipList = await _GaleriTipBS.GaleriTipListGetir();

            model.SubeList = subeList;
            model.GaleriTipList = galeriTipList;

            return View(model);
        }

        [HttpPost]
        [ActionName("GaleriKayit")]
        public async Task<JsonResult> GaleriKayitPost(GaleriKayitViewModel model, IFormFile dosya)
        {
            var kullaniciData = KullaniciDataGetir();
            model.SubeId = kullaniciData.SubeId == 1 ? model.SubeId : kullaniciData.SubeId;
            model.IslemKullaniciId = kullaniciData.KullaniciId;
            model.IslemTarih = DateTime.Now;

            JsonResult result;

            if (dosya != null && dosya.Length > 0)
            {
                string ext = dosya.FileName.Split('.').LastOrDefault().ToLower();
                if (ext != null && (ext == "gif" || ext == "png" || ext == "jpg" || ext == "jpeg"))
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

            var id = await _GaleriBS.GaleriKaydet(model);

            if (id > 0)
                result = Json(new { id = id, message = "success", operation = model.GaleriId > 0 ? "update" : "insert" });
            else
                result = Json(new { id = id, message = "error", operation = model.GaleriId > 0 ? "update" : "insert" });

            return result;
        }

        [HttpGet]
        [ActionName("GaleriArama")]
        public async Task<IActionResult> GaleriAramaGet()
        {
            var subeList = await _SubeBS.TumSubeListGetir();
            var galeriTipList = await _GaleriTipBS.GaleriTipListGetir();

            var model = new GaleriAramaViewModel()
            {
                SubeId = KullaniciDataGetir().SubeId,
                SubeList = subeList,
                GaleriTipList = galeriTipList,
                Aktiflik = 1
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("GaleriArama")]
        public async Task<IActionResult> GaleriAramaPost(GaleriAramaViewModel model)
        {
            var result = await _GaleriBS.GaleriAramaSonucViewModelGetir(model);
            int totalCount = result.Any() ? result.FirstOrDefault().TotalCount : 0;

            return Json(new { draw = model.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = result });
        }

        [HttpGet]
        [ActionName("GaleriSil")]
        public async Task<JsonResult> GaleriSilGet(int id)
        {
            var data = await _GaleriBS.GaleriSil(id);

            JsonResult result = Json(data);

            return result;
        }

        #endregion

        #region Bilgi Metni

        [HttpGet]
        [ActionName("BilgiMetniKayit")]
        public async Task<IActionResult> BilgiMetniKayitGet(int id)
        {
            int subeId = KullaniciDataGetir().SubeId;

            BilgiMetniKayitViewModel model;
            if (id > 0)
            {
                model = await _IcerikBS.BilgiMetniKayitViewModelGetir(id);
            }
            else
            {
                return Redirect("/Admin/Icerik");
            }

            if (subeId != 1 && model.SubeId != subeId)
                return Redirect("/Admin/Icerik");

            var subeList = await _SubeBS.TumSubeListGetir();
            var icerikTipList = await _IcerikTipBS.IcerikTipListGetir();

            model.SubeList = subeList;
            model.IcerikTipList = icerikTipList;

            return View(model);
        }

        [HttpPost]
        [ActionName("BilgiMetniKayit")]
        public async Task<JsonResult> BilgiMetniKayitPost(BilgiMetniKayitViewModel model)
        {
            var kullaniciData = KullaniciDataGetir();
            model.SubeId = kullaniciData.SubeId == 1 ? model.SubeId : kullaniciData.SubeId;
            model.IslemKullaniciId = kullaniciData.KullaniciId;
            model.IslemTarih = DateTime.Now;

            JsonResult result;

            var id = await _IcerikBS.IcerikKaydet(model);

            if (id > 0)
                result = Json(new { id = id, message = "success", operation = model.IcerikId > 0 ? "update" : "insert" });
            else
                result = Json(new { id = id, message = "error", operation = model.IcerikId > 0 ? "update" : "insert" });

            return result;
        }

        [HttpGet]
        [ActionName("BilgiMetniArama")]
        public async Task<IActionResult> BilgiMetniAramaGet()
        {
            var subeList = await _SubeBS.TumSubeListGetir();
            var icerikTipList = await _IcerikTipBS.IcerikTipListGetir();

            var model = new BilgiMetniAramaViewModel()
            {
                SubeId = KullaniciDataGetir().SubeId,
                SubeList = subeList,
                IcerikTipList = icerikTipList,
                Aktiflik = 1
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("BilgiMetniArama")]
        public async Task<IActionResult> BilgiMetniAramaPost(BilgiMetniAramaViewModel model)
        {
            var result = await _IcerikBS.BilgiMetniAramaSonucViewModelGetir(model);
            int totalCount = result.Any() ? result.FirstOrDefault().TotalCount : 0;

            return Json(new { draw = model.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = result });
        }

        #endregion

        #region Yayın

        [HttpGet]
        [ActionName("YayinKayit")]
        public async Task<IActionResult> YayinKayitGet(int? id)
        {
            int subeId = KullaniciDataGetir().SubeId;
            if (subeId != 1)
                return Redirect("/Admin/Icerik");

            YayinKayitViewModel model;
            if (id.HasValue && id.Value > 0)
            {
                model = await _YayinBS.YayinKayitViewModelGetir(id.Value);
            }
            else
            {
                model = new YayinKayitViewModel()
                {
                    Resim = "/Uploads/Site/noimg.png",
                    Sira = 1,
                    AktifMi = true,
                };
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("YayinKayit")]
        public async Task<JsonResult> YayinKayitPost(YayinKayitViewModel model, IFormFile dosya, IFormFile ozetDosya)
        {
            var kullaniciData = KullaniciDataGetir();
            int subeId = kullaniciData.SubeId;

            JsonResult result = Json(new { id = 0, message = "error", operation = model.YayinId > 0 ? "update" : "insert" });

            if (subeId == 1)
            {
                model.IslemKullaniciId = kullaniciData.KullaniciId;
                model.IslemTarih = DateTime.Now;

                if (dosya != null && dosya.Length > 0)
                {
                    string ext = dosya.FileName.Split('.').LastOrDefault().ToLower();
                    if (ext != null && (ext == "gif" || ext == "png" || ext == "jpg" || ext == "jpeg"))
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

                if (ozetDosya != null && ozetDosya.Length > 0)
                {
                    string ext = ozetDosya.FileName.Split('.').LastOrDefault();
                    if (ext != null && ext == "pdf")
                    {
                        using (var ms = new MemoryStream())
                        {
                            ozetDosya.CopyTo(ms);
                            model.OzetDosya = ms.ToArray();
                            model.OzetDosyaAdi = ozetDosya.FileName;
                        }
                    }
                    else
                    {
                        result = Json(new { id = 0, message = "control", operation = "file" });
                    }
                }

                var id = await _YayinBS.YayinKaydet(model);

                if (id > 0)
                    result = Json(new { id = id, message = "success", operation = model.YayinId > 0 ? "update" : "insert" });
                else
                    result = Json(new { id = id, message = "error", operation = model.YayinId > 0 ? "update" : "insert" });
            }

            return result;
        }

        [HttpGet]
        [ActionName("YayinArama")]
        public async Task<IActionResult> YayinAramaGet()
        {
            var model = new YayinAramaViewModel()
            {
                Aktiflik = 1
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("YayinArama")]
        public async Task<IActionResult> YayinAramaPost(YayinAramaViewModel model)
        {
            var result = await _YayinBS.YayinAramaSonucViewModelGetir(model);
            int totalCount = result.Any() ? result.FirstOrDefault().TotalCount : 0;

            return Json(new { draw = model.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = result });
        }

        [HttpGet]
        [ActionName("YayinSil")]
        public async Task<JsonResult> YayinSilGet(int id)
        {
            var data = await _YayinBS.YayinSil(id);

            JsonResult result = Json(data);

            return result;
        }

        [HttpGet]
        [ActionName("YayinPdfPartial")]
        public PartialViewResult YayinPdfPartialGet(int id)
        {
            ViewData["Id"] = id;
            return PartialView("~/Areas/Admin/Views/Icerik/Partials/DosyaIndirSil.cshtml", ViewData);
        }

        [HttpGet]
        [ActionName("YayinPdfIndir")]
        public async Task<JsonResult> YayinPdfIndirGet(int id)
        {
            var data = await _YayinBS.PdfIndir(id);

            JsonResult result = Json(data);

            return result;
        }

        [HttpGet]
        [ActionName("YayinPdfSil")]
        public async Task<JsonResult> YayinPdfSilGet(int id)
        {
            var data = await _YayinBS.PdfSil(id);

            JsonResult result = Json(data);

            return result;
        }

        #endregion
    }
}
