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
    public class GaleriBS
    {
        #region Admin

        public async Task<int> GaleriKaydet(GaleriKayitViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var galeri = new Galeri();

                if (model.GaleriId == 0)
                {
                    galeri = new Galeri()
                    {
                        //OgretmenId = model.OgretmenId,
                        SubeId = model.SubeId,
                        GaleriTipId = model.GaleriTipId,
                        Aciklama = model.Aciklama,
                        Tarih = DateTime.Parse(model.Tarih),
                        ResimUrl = model.DosyaAdi,
                        Resim = model.Dosya,
                        Anasayfa = model.Anasayfa,
                        KayitKullaniciId = model.IslemKullaniciId,
                        KayitTarih = model.IslemTarih,
                        GuncellemeId = null,
                        GuncellemeTarih = null,
                        Sira = model.Sira,
                        AktifMi = model.AktifMi,
                    };

                    dbContext.Galeri.Add(galeri);
                }
                else
                {
                    galeri = await GaleriGetir(model.GaleriId);
                    dbContext.Entry(galeri).State = EntityState.Modified;

                    galeri.SubeId = model.SubeId;
                    galeri.GaleriTipId = model.GaleriTipId;
                    galeri.Aciklama = model.Aciklama;
                    galeri.Tarih = DateTime.Parse(model.Tarih);
                    galeri.Anasayfa = model.Anasayfa;
                    galeri.GuncellemeId = model.IslemKullaniciId;
                    galeri.GuncellemeTarih = model.IslemTarih;
                    galeri.Sira = model.Sira;
                    galeri.AktifMi = model.AktifMi;

                    if (model.Dosya != null)
                    {
                        galeri.ResimUrl = model.DosyaAdi;
                        galeri.Resim = model.Dosya;
                    }
                }

                var id = await dbContext.SaveChangesAsync();

                return id > 0 ? galeri.GaleriId : 0;
            }
        }

        public async Task<bool> GaleriSil(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var galeri = await GaleriGetir(id);
                dbContext.Entry(galeri).State = EntityState.Modified;

                galeri.AktifMi = false;

                var result = await dbContext.SaveChangesAsync();

                return result > 0 ? true : false;
            }
        }

        public async Task<Galeri> GaleriGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Galeri.SingleOrDefaultAsync(p => p.GaleriId == id);
            }
        }

        public async Task<GaleriKayitViewModel> GaleriKayitViewModelGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Galeri.Where(p => p.GaleriId == id)
                                             .Select(p => new GaleriKayitViewModel
                                             {
                                                 GaleriId = p.GaleriId,
                                                 SubeId = p.SubeId,
                                                 GaleriTipId = p.GaleriTipId,
                                                 Aciklama = p.Aciklama,
                                                 Tarih = p.Tarih.Date.ToString("dd.MM.yyyy"),
                                                 Anasayfa = p.Anasayfa,
                                                 DosyaAdi = p.ResimUrl,
                                                 Dosya = p.Resim,
                                                 Resim = p.Resim == null ? "/Uploads/Site/noimg.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                                 Sira = p.Sira,
                                                 AktifMi = p.AktifMi
                                             })
                                             .SingleOrDefaultAsync();
            }
        }

        public async Task<List<GaleriAramaSonucViewModel>> GaleriAramaSonucViewModelGetir(GaleriAramaViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var query = dbContext.Galeri.Include("Sube")
                                            .Include("GaleriTip")
                                            .Where(p => (model.Aktiflik == -1 || p.AktifMi == Convert.ToBoolean(model.Aktiflik)) &&
                                                        (model.SubeId == 0 || p.SubeId == model.SubeId) &&
                                                        (model.GaleriTipId == 0 || p.GaleriTipId == model.GaleriTipId) &&
                                                        (model.Aciklama == null || p.Aciklama.Contains(model.Aciklama)));

                return await query.OrderByDescending(p => p.GaleriId)
                                  .Select(p => new GaleriAramaSonucViewModel
                                  {
                                      TotalCount = query.Count(),

                                      GaleriId = p.GaleriId,
                                      SubeAdi = p.Sube.SubeAdi,
                                      GaleriTipAdi = p.GaleriTip.GaleriTipAdi,
                                      Aciklama = p.Aciklama,
                                      Tarih = p.Tarih.Date.ToString("dd.MM.yyyy"),
                                      Anasayfa = p.Anasayfa,
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

        public async Task<List<GaleriViewModel>> GaleriListGetir(bool anasayfa, int subeId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Galeri.AsNoTracking()
                                             .Where(p => p.AktifMi &&
                                                           ((anasayfa && p.Anasayfa == anasayfa) || !anasayfa) &&
                                                           p.SubeId == subeId)
                                             .OrderBy(p => p.Sira)
                                             .Select(p => new GaleriViewModel
                                             {
                                                 Aciklama = p.Aciklama,
                                                 Tarih = p.Tarih,
                                                 Resim = p.Resim == null ? "/Uploads/Site/no_img_450x250.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                             })
                                             .ToListAsync();
            }
        }

        #endregion
    }
}
