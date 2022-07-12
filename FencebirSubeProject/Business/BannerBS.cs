using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Data;
using FencebirSubeProject.Entities;
using FencebirSubeProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Business
{
    public class BannerBS
    {
        #region Admin

        public async Task<int> BannerKaydet(BannerKayitViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var banner = new Banner();

                if (model.BannerId == 0)
                {
                    banner = new Banner()
                    {
                        //BannerId = model.BannerId,
                        BannerTipId = model.BannerTipId,
                        SubeId = model.SubeId,
                        Adi = model.Adi,
                        ResimUrl = model.DosyaAdi,
                        Resim = model.Dosya,
                        Aciklama1 = model.Aciklama1,
                        Aciklama2 = model.Aciklama2,
                        Aciklama3 = model.Aciklama3,
                        Link = model.Link,
                        LinkAciklama = model.LinkAciklama,
                        BannerOlusturma = model.SubeId == 1 ? model.BannerOlusturma : false,
                        KayitKullaniciId = model.IslemKullaniciId,
                        KayitTarih = model.IslemTarih,
                        GuncellemeId = null,
                        GuncellemeTarih = null,
                        Sira = model.Sira,
                        AktifMi = model.AktifMi
                    };

                    dbContext.Banner.Add(banner);
                }
                else
                {
                    banner = await BannerGetir(model.BannerId);
                    dbContext.Entry(banner).State = EntityState.Modified;

                    banner.BannerTipId = model.BannerTipId;
                    banner.SubeId = model.SubeId;
                    banner.Adi = model.Adi;
                    banner.Aciklama1 = model.Aciklama1;
                    banner.Aciklama2 = model.Aciklama2;
                    banner.Aciklama3 = model.Aciklama3;
                    banner.Link = model.Link;
                    banner.LinkAciklama = model.LinkAciklama;
                    banner.BannerOlusturma = model.SubeId == 1 ? model.BannerOlusturma : false;
                    banner.GuncellemeId = model.IslemKullaniciId;
                    banner.GuncellemeTarih = model.IslemTarih;
                    banner.Sira = model.Sira;
                    banner.AktifMi = model.AktifMi;

                    if (model.Dosya != null)
                    {
                        banner.ResimUrl = model.DosyaAdi;
                        banner.Resim = model.Dosya;
                    }
                }

                var id = await dbContext.SaveChangesAsync();

                return id > 0 ? banner.BannerId : 0;
            }
        }

        public async Task<bool> BannerSil(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var banner = await BannerGetir(id);
                dbContext.Entry(banner).State = EntityState.Modified;

                banner.AktifMi = false;

                var result = await dbContext.SaveChangesAsync();

                return result > 0 ? true : false;
            }
        }

        public async Task<Banner> BannerGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Banner.SingleOrDefaultAsync(p => p.BannerId == id);
            }
        }

        public async Task<List<Banner>> OlusturulacakBannerGetir()
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Banner.Where(p => p.AktifMi &&
                                                         p.SubeId == 1 &&
                                                         p.BannerOlusturma)
                                             .ToListAsync();
            }
        }

        public async Task<BannerKayitViewModel> BannerKayitViewModelGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Banner.Where(p => p.BannerId == id)
                                             .Select(p => new BannerKayitViewModel
                                             {
                                                 BannerId = p.BannerId,
                                                 SubeId = p.SubeId,
                                                 BannerTipId = p.BannerTipId,
                                                 Adi = p.Adi,
                                                 Aciklama1 = p.Aciklama1,
                                                 Aciklama2 = p.Aciklama2,
                                                 Aciklama3 = p.Aciklama3,
                                                 Link = p.Link,
                                                 LinkAciklama = p.LinkAciklama,
                                                 BannerOlusturma = p.BannerOlusturma,
                                                 DosyaAdi = p.ResimUrl,
                                                 Dosya = p.Resim,
                                                 Resim = p.Resim == null ? "/Uploads/Site/noimg.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                                 Sira = p.Sira,
                                                 AktifMi = p.AktifMi
                                             })
                                             .SingleOrDefaultAsync();
            }
        }

        public async Task<List<BannerAramaSonucViewModel>> BannerAramaSonucViewModelGetir(BannerAramaViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var query = dbContext.Banner.Include("Sube")
                                            .Include("BannerTip")
                                            .Where(p => (model.Aktiflik == -1 || p.AktifMi == Convert.ToBoolean(model.Aktiflik)) &&
                                                        (model.SubeId == 0 || p.SubeId == model.SubeId) &&
                                                        (model.BannerTipId == 0 || p.BannerTipId == model.BannerTipId) &&
                                                        (model.Adi == null || p.Adi.Contains(model.Adi)));

                return await query.OrderByDescending(p => p.SubeId)
                                  .Select(p => new BannerAramaSonucViewModel
                                  {
                                      TotalCount = query.Count(),

                                      BannerId = p.BannerId,
                                      SubeAdi = p.Sube.SubeAdi,
                                      BannerTipAdi = p.BannerTip.BannerTipAdi,
                                      Adi = p.Adi,
                                      Aciklama1 = p.Aciklama1,
                                      Aciklama2 = p.Aciklama2,
                                      Aciklama3 = p.Aciklama3,
                                      Link = p.Link,
                                      LinkAciklama = p.LinkAciklama,
                                      Resim = p.Resim == null ? "/Uploads/Site/noimg.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                      Sira = p.Sira,
                                      AktifMi = p.AktifMi
                                  })
                                  .Skip(model.start)
                                  .Take(model.length)
                                  .ToListAsync();
            }
        }

        #endregion

        #region FrontEnd

        public async Task<List<BannerViewModel>> BannerListGetir(int subeId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Banner.AsNoTracking()
                                             .Where(p => p.AktifMi &&
                                                         p.SubeId == subeId)
                                             .OrderBy(p => p.Sira)
                                             .Select(p => new BannerViewModel
                                             {
                                                 BannerTipId = p.BannerTipId,
                                                 Adi = p.Adi,
                                                 Resim = p.Resim == null ? "/Uploads/Site/no_img_1920x1080.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                                 Aciklama1 = p.Aciklama1,
                                                 Aciklama2 = p.Aciklama2,
                                                 Aciklama3 = p.Aciklama3,
                                                 Link = p.Link,
                                                 LinkAciklama = p.LinkAciklama
                                             })
                                            .ToListAsync();
            }
        }

        #endregion
    }
}
