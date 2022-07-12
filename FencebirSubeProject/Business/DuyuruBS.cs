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
    public class DuyuruBS
    {
        #region Admin

        public async Task<int> DuyuruKaydet(DuyuruKayitViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var duyuru = new Duyuru();

                if (model.DuyuruId == 0)
                {
                    duyuru = new Duyuru()
                    {
                        //DuyuruId = model.DuyuruId,
                        SubeId = model.SubeId,
                        Icerik = model.Icerik,
                        Tarih = DateTime.Parse(model.Tarih),
                        KayitKullaniciId = model.IslemKullaniciId,
                        KayitTarih = model.IslemTarih,
                        GuncellemeId = null,
                        GuncellemeTarih = null,
                        Sira = model.Sira,
                        AktifMi = model.AktifMi
                    };

                    dbContext.Duyuru.Add(duyuru);
                }
                else
                {
                    duyuru = await DuyuruGetir(model.DuyuruId);
                    dbContext.Entry(duyuru).State = EntityState.Modified;

                    duyuru.SubeId = model.SubeId;
                    duyuru.Icerik = model.Icerik;
                    duyuru.Tarih = DateTime.Parse(model.Tarih);
                    duyuru.GuncellemeId = model.IslemKullaniciId;
                    duyuru.GuncellemeTarih = model.IslemTarih;
                    duyuru.Sira = model.Sira;
                    duyuru.AktifMi = model.AktifMi;
                }

                var id = await dbContext.SaveChangesAsync();

                return id > 0 ? duyuru.DuyuruId : 0;
            }
        }

        public async Task<bool> DuyuruSil(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var duyuru = await DuyuruGetir(id);
                dbContext.Entry(duyuru).State = EntityState.Modified;

                duyuru.AktifMi = false;

                var result = await dbContext.SaveChangesAsync();

                return result > 0 ? true : false;
            }
        }

        public async Task<Duyuru> DuyuruGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Duyuru.SingleOrDefaultAsync(p => p.DuyuruId == id);
            }
        }

        public async Task<DuyuruKayitViewModel> DuyuruKayitViewModelGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Duyuru.Where(p => p.DuyuruId == id)
                                             .Select(p => new DuyuruKayitViewModel
                                             {
                                                 DuyuruId = p.DuyuruId,
                                                 SubeId = p.SubeId,
                                                 Icerik = p.Icerik,
                                                 Tarih = p.Tarih.Date.ToString("dd.MM.yyyy"),
                                                 Sira = p.Sira,
                                                 AktifMi = p.AktifMi
                                             })
                                             .SingleOrDefaultAsync();
            }
        }

        public async Task<List<DuyuruAramaSonucViewModel>> DuyuruAramaSonucViewModelGetir(DuyuruAramaViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var query = dbContext.Duyuru.Include("Sube")
                                            .Where(p => (model.Aktiflik == -1 || p.AktifMi == Convert.ToBoolean(model.Aktiflik)) &&
                                                        (model.SubeId == 0 || p.SubeId == model.SubeId) &&
                                                        (model.Icerik == null || p.Icerik.Contains(model.Icerik)));

                return await query.OrderByDescending(p => p.DuyuruId)
                                  .Select(p => new DuyuruAramaSonucViewModel
                                  {
                                      TotalCount = query.Count(),

                                      DuyuruId = p.DuyuruId,
                                      SubeAdi = p.Sube.SubeAdi,
                                      Icerik = p.Icerik,
                                      Tarih = p.Tarih.ToString("dd.MM.yyyy"),
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

        public async Task<List<DuyuruViewModel>> DuyuruListGetir(int subeId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Duyuru.AsNoTracking()
                                             .Where(p => p.AktifMi &&
                                                         p.SubeId == subeId)
                                             .OrderBy(p => p.Sira)
                                             .Select(p => new DuyuruViewModel
                                             {
                                                 Icerik = p.Icerik,
                                                 Tarih = p.Tarih
                                             })
                                             .ToListAsync();
            }
        }

        #endregion
    }
}
