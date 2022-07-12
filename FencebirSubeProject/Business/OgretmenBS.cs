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
    public class OgretmenBS
    {
        #region Admin

        public async Task<int> OgretmenKaydet(OgretmenKayitViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var ogretmen = new Ogretmen();

                if (model.OgretmenId == 0)
                {
                    ogretmen = new Ogretmen()
                    {
                        //OgretmenId = model.OgretmenId,
                        SubeId = model.SubeId,
                        AdSoyad = model.AdSoyad,
                        Unvan = model.Unvan,
                        Aciklama = model.Aciklama,
                        ResimUrl = model.DosyaAdi,
                        Resim = model.Dosya,
                        FacebookHesapUrl = model.FacebookHesapUrl,
                        InstagramHesapUrl = model.InstagramHesapUrl,
                        TwitterHesapUrl = model.TwitterHesapUrl,
                        WhatsappHesapUrl = model.WhatsappHesapUrl,
                        YoutubeHesapUrl = model.YoutubeHesapUrl,
                        Anasayfa = model.Anasayfa,
                        KayitKullaniciId = model.IslemKullaniciId,
                        KayitTarih = model.IslemTarih,
                        GuncellemeId = null,
                        GuncellemeTarih = null,
                        Sira = model.Sira,
                        AktifMi = model.AktifMi
                    };

                    dbContext.Ogretmen.Add(ogretmen);
                }
                else
                {
                    ogretmen = await OgretmenGetir(model.OgretmenId);
                    dbContext.Entry(ogretmen).State = EntityState.Modified;

                    ogretmen.SubeId = model.SubeId;
                    ogretmen.AdSoyad = model.AdSoyad;
                    ogretmen.Unvan = model.Unvan;
                    ogretmen.Aciklama = model.Aciklama;
                    ogretmen.FacebookHesapUrl = model.FacebookHesapUrl;
                    ogretmen.InstagramHesapUrl = model.InstagramHesapUrl;
                    ogretmen.TwitterHesapUrl = model.TwitterHesapUrl;
                    ogretmen.WhatsappHesapUrl = model.WhatsappHesapUrl;
                    ogretmen.YoutubeHesapUrl = model.YoutubeHesapUrl;
                    ogretmen.Anasayfa = model.Anasayfa;
                    ogretmen.GuncellemeId = model.IslemKullaniciId;
                    ogretmen.GuncellemeTarih = model.IslemTarih;
                    ogretmen.Sira = model.Sira;
                    ogretmen.AktifMi = model.AktifMi;

                    if (model.Dosya != null)
                    {
                        ogretmen.ResimUrl = model.DosyaAdi;
                        ogretmen.Resim = model.Dosya;
                    }
                }

                var id = await dbContext.SaveChangesAsync();

                return id > 0 ? ogretmen.OgretmenId : 0;
            }
        }

        public async Task<bool> OgretmenSil(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var ogretmen = await OgretmenGetir(id);
                dbContext.Entry(ogretmen).State = EntityState.Modified;

                ogretmen.AktifMi = false;

                var result = await dbContext.SaveChangesAsync();

                return result > 0 ? true : false;
            }
        }

        public async Task<Ogretmen> OgretmenGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Ogretmen.SingleOrDefaultAsync(p => p.OgretmenId == id);
            }
        }

        public async Task<OgretmenKayitViewModel> OgretmenKayitViewModelGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Ogretmen.Where(p => p.OgretmenId == id)
                                               .Select(p => new OgretmenKayitViewModel
                                               {
                                                   OgretmenId = p.OgretmenId,
                                                   SubeId = p.SubeId,
                                                   AdSoyad = p.AdSoyad,
                                                   Unvan = p.Unvan,
                                                   Aciklama = p.Aciklama,
                                                   FacebookHesapUrl = p.FacebookHesapUrl,
                                                   InstagramHesapUrl = p.InstagramHesapUrl,
                                                   TwitterHesapUrl = p.TwitterHesapUrl,
                                                   WhatsappHesapUrl = p.WhatsappHesapUrl,
                                                   YoutubeHesapUrl = p.YoutubeHesapUrl,
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

        public async Task<List<OgretmenAramaSonucViewModel>> OgretmenAramaSonucViewModelGetir(OgretmenAramaViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var query = dbContext.Ogretmen.Include("Sube")
                                              .Where(p => (model.Aktiflik == -1 || p.AktifMi == Convert.ToBoolean(model.Aktiflik)) &&
                                                          (model.SubeId == 0 || p.SubeId == model.SubeId) &&
                                                          (model.AdSoyad == null || p.AdSoyad.Contains(model.AdSoyad)) &&
                                                          (model.Unvan == null || p.AdSoyad.Contains(model.Unvan)));

                return await query.OrderByDescending(p => p.SubeId)
                                  .Select(p => new OgretmenAramaSonucViewModel
                                  {
                                      TotalCount = query.Count(),

                                      OgretmenId = p.OgretmenId,
                                      SubeAdi = p.Sube.SubeAdi,
                                      AdSoyad = p.AdSoyad,
                                      Unvan = p.Unvan,
                                      Aciklama = p.Aciklama,
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

        public async Task<List<OgretmenViewModel>> OgretmenListGetir(bool anasayfa, int subeId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Ogretmen.AsNoTracking()
                                               .Where(p => p.AktifMi &&
                                                           ((anasayfa && p.Anasayfa == anasayfa) || !anasayfa) &&
                                                           p.SubeId == subeId)
                                               .OrderBy(p => p.Sira)
                                               .Select(p => new OgretmenViewModel
                                               {
                                                   AdSoyad = p.AdSoyad,
                                                   Unvan = p.Unvan,
                                                   Aciklama = p.Aciklama,
                                                   Resim = p.Resim == null ? "/Uploads/Site/noimg.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                                   FacebookHesapUrl = p.FacebookHesapUrl,
                                                   InstagramHesapUrl = p.InstagramHesapUrl,
                                                   TwitterHesapUrl = p.TwitterHesapUrl,
                                                   WhatsappHesapUrl = p.WhatsappHesapUrl,
                                                   YoutubeHesapUrl = p.YoutubeHesapUrl
                                               })
                                               .ToListAsync();
            }
        }

        #endregion
    }
}
