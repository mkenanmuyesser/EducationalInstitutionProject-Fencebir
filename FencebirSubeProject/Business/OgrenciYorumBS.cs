using FencebirSubeProject.Data;
using FencebirSubeProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using FencebirSubeProject.Entities;
using FencebirSubeProject.Areas.Admin.Models;

namespace FencebirSubeProject.Business
{
    public class OgrenciYorumBS
    {
        #region Admin

        public async Task<int> OgrenciYorumKaydet(OgrenciYorumKayitViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var ogrenciYorum = new OgrenciYorum();

                if (model.OgrenciYorumId == 0)
                {
                    ogrenciYorum = new OgrenciYorum()
                    {
                        //OgrenciYorumId = model.OgrenciYorumId,
                        SubeId = model.SubeId,
                        OgrenciAdSoyad = model.OgrenciAdSoyad,
                        Yorum = model.Yorum,
                        ResimUrl = model.DosyaAdi,
                        Resim = model.Dosya,
                        KayitKullaniciId = model.IslemKullaniciId,
                        KayitTarih = model.IslemTarih,
                        GuncellemeId = null,
                        GuncellemeTarih = null,
                        Sira = model.Sira,
                        AktifMi = model.AktifMi
                    };

                    dbContext.OgrenciYorum.Add(ogrenciYorum);
                }
                else
                {
                    ogrenciYorum = await OgrenciYorumGetir(model.OgrenciYorumId);
                    dbContext.Entry(ogrenciYorum).State = EntityState.Modified;

                    ogrenciYorum.SubeId = model.SubeId;
                    ogrenciYorum.OgrenciAdSoyad = model.OgrenciAdSoyad;
                    ogrenciYorum.Yorum = model.Yorum;
                    ogrenciYorum.GuncellemeId = model.IslemKullaniciId;
                    ogrenciYorum.GuncellemeTarih = model.IslemTarih;
                    ogrenciYorum.Sira = model.Sira;
                    ogrenciYorum.AktifMi = model.AktifMi;

                    if (model.Dosya != null)
                    {
                        ogrenciYorum.ResimUrl = model.DosyaAdi;
                        ogrenciYorum.Resim = model.Dosya;
                    }
                }

                var id = await dbContext.SaveChangesAsync();

                return id > 0 ? ogrenciYorum.OgrenciYorumId : 0;
            }
        }

        public async Task<bool> OgrenciYorumSil(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var ogrenciYorum = await OgrenciYorumGetir(id);
                dbContext.Entry(ogrenciYorum).State = EntityState.Modified;

                ogrenciYorum.AktifMi = false;

                var result = await dbContext.SaveChangesAsync();

                return result > 0 ? true : false;
            }
        }

        public async Task<OgrenciYorum> OgrenciYorumGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.OgrenciYorum.SingleOrDefaultAsync(p => p.OgrenciYorumId == id);
            }
        }

        public async Task<OgrenciYorumKayitViewModel> OgrenciYorumKayitViewModelGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.OgrenciYorum.Where(p => p.OgrenciYorumId == id)
                                                   .Select(p => new OgrenciYorumKayitViewModel
                                                   {
                                                       OgrenciYorumId = p.OgrenciYorumId,
                                                       SubeId = p.SubeId,
                                                       OgrenciAdSoyad = p.OgrenciAdSoyad,
                                                       Yorum = p.Yorum,
                                                       DosyaAdi = p.ResimUrl,
                                                       Dosya = p.Resim,
                                                       Resim = p.Resim == null ? "/Uploads/Site/noimg.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                                       Sira = p.Sira,
                                                       AktifMi = p.AktifMi
                                                   })
                                                   .SingleOrDefaultAsync();
            }
        }

        public async Task<List<OgrenciYorumAramaSonucViewModel>> OgrenciYorumAramaSonucViewModelGetir(OgrenciYorumAramaViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var query = dbContext.OgrenciYorum.Include("Sube")
                                                  .Where(p => (model.Aktiflik == -1 || p.AktifMi == Convert.ToBoolean(model.Aktiflik)) &&
                                                              (model.SubeId == 0 || p.SubeId == model.SubeId) &&
                                                              (model.OgrenciAdSoyad == null || p.OgrenciAdSoyad.Contains(model.OgrenciAdSoyad)));

                return await query.OrderByDescending(p => p.SubeId)
                                  .Select(p => new OgrenciYorumAramaSonucViewModel
                                  {
                                      TotalCount = query.Count(),

                                      OgrenciYorumId = p.OgrenciYorumId,
                                      SubeAdi = p.Sube.SubeAdi,
                                      OgrenciAdSoyad = p.OgrenciAdSoyad,
                                      Yorum = p.Yorum,
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

        public async Task<List<OgrenciYorumViewModel>> OgrenciYorumListGetir(int subeId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.OgrenciYorum.AsNoTracking()
                                                   .Where(p => p.AktifMi &&
                                                               p.SubeId == subeId)
                                                   .OrderBy(p => p.Sira)
                                                   .Select(p => new OgrenciYorumViewModel
                                                   {
                                                       OgrenciAdSoyad = p.OgrenciAdSoyad,
                                                       Yorum = p.Yorum,
                                                       Resim = p.Resim == null ? "/Uploads/Site/noimg.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                                   })
                                                   .ToListAsync();
            }
        }

        #endregion
    }
}
