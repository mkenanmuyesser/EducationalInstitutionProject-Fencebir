using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Data;
using FencebirSubeProject.Entities;
using FencebirSubeProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace FencebirSubeProject.Business
{
    public class EtkinlikBS
    {
        #region Admin

        public async Task<int> EtkinlikKaydet(EtkinlikKayitViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var etkinlik = new Etkinlik();

                if (model.EtkinlikId == 0)
                {
                    etkinlik = new Etkinlik()
                    {
                        //EtkinlikId = model.EtkinlikId,
                        EtkinlikTipId = model.EtkinlikTipId,
                        SubeId = model.SubeId,
                        EtkinlikKonu = model.EtkinlikKonu,
                        Tarih = DateTime.Parse(model.Tarih),
                        BaslangicZaman = TimeSpan.Parse(model.BaslangicZaman),
                        BitisZaman = TimeSpan.Parse(model.BitisZaman),
                        Yer = model.Yer,
                        KayitKullaniciId = model.IslemKullaniciId,
                        KayitTarih = model.IslemTarih,
                        GuncellemeId = null,
                        GuncellemeTarih = null,
                        Sira = model.Sira,
                        AktifMi = model.AktifMi
                    };

                    dbContext.Etkinlik.Add(etkinlik);
                }
                else
                {
                    etkinlik = await EtkinlikGetir(model.EtkinlikId);
                    dbContext.Entry(etkinlik).State = EntityState.Modified;

                    etkinlik.EtkinlikTipId = model.EtkinlikTipId;
                    etkinlik.SubeId = model.SubeId;
                    etkinlik.EtkinlikKonu = model.EtkinlikKonu;
                    etkinlik.Tarih = DateTime.Parse(model.Tarih);
                    etkinlik.BaslangicZaman = TimeSpan.Parse(model.BaslangicZaman);
                    etkinlik.BitisZaman = TimeSpan.Parse(model.BitisZaman);
                    etkinlik.Yer = model.Yer;
                    etkinlik.GuncellemeId = model.IslemKullaniciId;
                    etkinlik.GuncellemeTarih = model.IslemTarih;
                    etkinlik.Sira = model.Sira;
                    etkinlik.AktifMi = model.AktifMi;
                }

                var id = await dbContext.SaveChangesAsync();

                return id > 0 ? etkinlik.EtkinlikId : 0;
            }
        }

        public async Task<bool> EtkinlikSil(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var etkinlik = await EtkinlikGetir(id);
                dbContext.Entry(etkinlik).State = EntityState.Modified;

                etkinlik.AktifMi = false;

                var result = await dbContext.SaveChangesAsync();

                return result > 0 ? true : false;
            }
        }

        public async Task<Etkinlik> EtkinlikGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Etkinlik.SingleOrDefaultAsync(p => p.EtkinlikId == id);
            }
        }

        public async Task<EtkinlikKayitViewModel> EtkinlikKayitViewModelGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Etkinlik.Where(p => p.EtkinlikId == id)
                                               .Select(p => new EtkinlikKayitViewModel
                                               {
                                                   EtkinlikId = p.EtkinlikId,
                                                   SubeId = p.SubeId,
                                                   EtkinlikTipId = p.EtkinlikTipId,
                                                   EtkinlikKonu = p.EtkinlikKonu,
                                                   Tarih = p.Tarih.Date.ToString("dd.MM.yyyy"),
                                                   BaslangicZaman = p.BaslangicZaman.ToString(),
                                                   BitisZaman = p.BitisZaman.ToString(),
                                                   Yer = p.Yer,
                                                   Sira = p.Sira,
                                                   AktifMi = p.AktifMi
                                               })
                                               .SingleOrDefaultAsync();
            }
        }

        public async Task<List<EtkinlikAramaSonucViewModel>> EtkinlikAramaSonucViewModelGetir(EtkinlikAramaViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var query = dbContext.Etkinlik.Include("Sube")
                                              .Include("EtkinlikTip")
                                              .Where(p => (model.Aktiflik == -1 || p.AktifMi == Convert.ToBoolean(model.Aktiflik)) &&
                                                          (model.SubeId == 0 || p.SubeId == model.SubeId) &&
                                                          (model.EtkinlikTipId == 0 || p.EtkinlikTipId == model.EtkinlikTipId) &&
                                                          (model.EtkinlikKonu == null || p.EtkinlikKonu.Contains(model.EtkinlikKonu)));

                return await query.OrderByDescending(p => p.EtkinlikId)
                                  .Select(p => new EtkinlikAramaSonucViewModel
                                  {
                                      TotalCount = query.Count(),

                                      EtkinlikId = p.EtkinlikId,
                                      SubeAdi = p.Sube.SubeAdi,
                                      EtkinlikTipAdi = p.EtkinlikTip.EtkinlikTipAdi,
                                      EtkinlikKonu = p.EtkinlikKonu,
                                      TarihZaman = p.Tarih.ToString("dd.MM.yyyy") + "  " + p.BaslangicZaman.ToString("hh\\:mm") + "-" + p.BitisZaman.ToString("hh\\:mm"),
                                      Yer = p.Yer,
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

        public async Task<List<EtkinlikViewModel>> EtkinlikListGetir(int subeId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Etkinlik.AsNoTracking()
                                               .Where(p => p.AktifMi &&
                                                           p.SubeId == subeId)
                                               .OrderBy(p => p.Sira)
                                               .Select(p => new EtkinlikViewModel
                                               {
                                                   EtkinlikKonu = p.EtkinlikKonu,
                                                   Tarih = p.Tarih,
                                                   BaslangicZaman = p.BaslangicZaman,
                                                   BitisZaman = p.BitisZaman,
                                                   Yer = p.Yer
                                               })
                                               .ToListAsync();
            }
        }

        #endregion
    }
}
