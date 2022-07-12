using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Data;
using FencebirSubeProject.Entities;
using FencebirSubeProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using FencebirSubeProject.Infra;

namespace FencebirSubeProject.Business
{
    public class KullaniciBS
    {
        #region Admin

        public async Task<int> KullaniciKaydet(KullaniciKayitViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var kullanici = new Kullanici();

                if (model.KullaniciId == 0)
                {
                    kullanici = new Kullanici()
                    {
                        //KullaniciId = model.KullaniciId,
                        SubeId = model.SubeId,
                        Eposta = model.Eposta,
                        Sifre = model.Sifre.Encode(),
                        KayitKullaniciId = model.IslemKullaniciId,
                        KayitTarih = model.IslemTarih,
                        GuncellemeKullaniciId = null,
                        GuncellemeTarih = null,
                        AktifMi = model.AktifMi
                    };

                    dbContext.Kullanici.Add(kullanici);
                }
                else
                {
                    kullanici = await KullaniciGetir(model.KullaniciId);
                    dbContext.Entry(kullanici).State = EntityState.Modified;

                    kullanici.SubeId = model.SubeId;
                    //kullanici.Eposta = model.Eposta;
                    kullanici.Sifre = model.Sifre.Encode();
                    kullanici.GuncellemeKullaniciId = model.IslemKullaniciId;
                    kullanici.GuncellemeTarih = model.IslemTarih;
                    kullanici.AktifMi = model.AktifMi;
                }

                var id = await dbContext.SaveChangesAsync();

                return id > 0 ? kullanici.KullaniciId : 0;
            }
        }

        public async Task<bool> KullaniciSil(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var kullanici = await KullaniciGetir(id);
                dbContext.Entry(kullanici).State = EntityState.Modified;

                kullanici.AktifMi = false;

                var result = await dbContext.SaveChangesAsync();

                return result > 0 ? true : false;
            }
        }

        public async Task<bool> KullaniciKontrol(string eposta)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var result = await dbContext.Kullanici.SingleOrDefaultAsync(p => p.Eposta == eposta);
                return result == null ? true : false;
            }
        }

        public async Task<Kullanici> KullaniciGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Kullanici.SingleOrDefaultAsync(p => p.KullaniciId == id);
            }
        }

        public async Task<KullaniciKayitViewModel> KullaniciKayitViewModelGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Kullanici.Where(p => p.KullaniciId == id)
                                                .Select(p => new KullaniciKayitViewModel
                                                {
                                                    KullaniciId = p.KullaniciId,
                                                    SubeId = p.SubeId,
                                                    Eposta = p.Eposta,
                                                    Sifre = null,
                                                    AktifMi = p.AktifMi
                                                })
                                                .SingleOrDefaultAsync();
            }
        }

        public async Task<List<KullaniciAramaSonucViewModel>> KullaniciAramaSonucViewModelGetir(KullaniciAramaViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var query = dbContext.Kullanici.Include("Sube")
                                               .Where(p => (model.Aktiflik == -1 || p.AktifMi == Convert.ToBoolean(model.Aktiflik)) &&
                                                           (model.SubeId == 0 || p.SubeId == model.SubeId) &&
                                                           (model.Eposta == null || p.Eposta.Contains(model.Eposta)));

                return await query.OrderByDescending(p => p.KullaniciId)
                                  .Select(p => new KullaniciAramaSonucViewModel
                                  {
                                      TotalCount = query.Count(),

                                      KullaniciId = p.KullaniciId,
                                      SubeAdi = p.Sube.SubeAdi,
                                      Eposta = p.Eposta,
                                      KayitTarih = p.KayitTarih.ToString("dd.MM.yyyy"),
                                      AktifMi = p.AktifMi
                                  })
                                  .Skip(model.start)
                                  .Take(model.length)
                                  .ToListAsync();
            }
        }

        public async Task<KullaniciGirisModel> KullaniciGirisDataGetir(KullaniciGirisViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Kullanici.Include("Sube")
                                                .Include("Sube.SubeTip")
                                                .Where(p => p.AktifMi &&
                                                            p.Eposta == model.Eposta &&
                                                            p.Sifre == model.Sifre.Encode())
                                                .Select(p => new KullaniciGirisModel
                                                {
                                                    KullaniciId = p.KullaniciId,
                                                    SubeId = p.SubeId,
                                                    Eposta = p.Eposta,
                                                    SubeAdi = p.Sube.SubeAdi,
                                                    KullaniciTipAdi = p.Sube.SubeTip.SubeTipAdi + " Kullanıcısı"
                                                })
                                                .SingleOrDefaultAsync();
            }
        }

        #endregion

        #region FrontEnd

        #endregion
    }
}
