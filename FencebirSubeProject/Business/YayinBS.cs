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
    public class YayinBS
    {
        #region Admin

        public async Task<int> YayinKaydet(YayinKayitViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var yayin = new Yayin();

                if (model.YayinId == 0)
                {
                    yayin = new Yayin()
                    {
                        //YayinId = model.YayinId,
                        Ad = model.Ad,
                        EskiFiyat = string.IsNullOrEmpty(model.EskiFiyat) ? (decimal?)null : Decimal.Parse(model.EskiFiyat.Replace('.', ',')),
                        YeniFiyat = string.IsNullOrEmpty(model.YeniFiyat) ? (decimal?)null : Decimal.Parse(model.YeniFiyat.Replace('.', ',')),
                        ResimUrl = model.DosyaAdi,
                        Resim = model.Dosya,
                        OzetDosyaUrl = model.OzetDosyaAdi,
                        OzetDosya = model.OzetDosya,
                        KayitKullaniciId = model.IslemKullaniciId,
                        KayitTarih = model.IslemTarih,
                        GuncellemeId = null,
                        GuncellemeTarih = null,
                        Sira = model.Sira,
                        AktifMi = model.AktifMi
                    };

                    dbContext.Yayin.Add(yayin);
                }
                else
                {
                    yayin = await YayinGetir(model.YayinId);
                    dbContext.Entry(yayin).State = EntityState.Modified;

                    yayin.Ad = model.Ad;
                    yayin.EskiFiyat = string.IsNullOrEmpty(model.EskiFiyat) ? (decimal?)null : Decimal.Parse(model.EskiFiyat.Replace('.', ','));
                    yayin.YeniFiyat = string.IsNullOrEmpty(model.YeniFiyat) ? (decimal?)null : Decimal.Parse(model.YeniFiyat.Replace('.', ','));
                    yayin.GuncellemeId = model.IslemKullaniciId;
                    yayin.GuncellemeTarih = model.IslemTarih;
                    yayin.Sira = model.Sira;
                    yayin.AktifMi = model.AktifMi;

                    if (model.Dosya != null)
                    {
                        yayin.ResimUrl = model.DosyaAdi;
                        yayin.Resim = model.Dosya;
                    }

                    if (model.OzetDosya != null)
                    {
                        yayin.OzetDosyaUrl = model.OzetDosyaAdi;
                        yayin.OzetDosya = model.OzetDosya;
                    }
                }

                var id = await dbContext.SaveChangesAsync();

                return id > 0 ? yayin.YayinId : 0;
            }
        }

        public async Task<bool> YayinSil(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var yayin = await YayinGetir(id);
                dbContext.Entry(yayin).State = EntityState.Modified;

                yayin.AktifMi = false;

                var result = await dbContext.SaveChangesAsync();

                return result > 0 ? true : false;
            }
        }

        public async Task<bool> PdfSil(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var yayin = await YayinGetir(id);
                dbContext.Entry(yayin).State = EntityState.Modified;

                yayin.OzetDosya = null;
                yayin.OzetDosyaUrl = null;

                var result = await dbContext.SaveChangesAsync();

                return result > 0 ? true : false;
            }
        }

        public async Task<string> PdfIndir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var yayin = await YayinGetir(id);

                var result = yayin.OzetDosya == null ? "" : String.Format("data:application/pdf;base64,{0}", Convert.ToBase64String(yayin.OzetDosya, 0, yayin.OzetDosya.Length));

                return result;
            }
        }

        public async Task<Yayin> YayinGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Yayin.SingleOrDefaultAsync(p => p.YayinId == id);
            }
        }

        public async Task<YayinKayitViewModel> YayinKayitViewModelGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Yayin.Where(p => p.YayinId == id)
                                            .Select(p => new YayinKayitViewModel
                                            {
                                                YayinId = p.YayinId,
                                                Ad = p.Ad,
                                                EskiFiyat = p.EskiFiyat.HasValue ? p.EskiFiyat.Value.ToString() : "",
                                                YeniFiyat = p.YeniFiyat.HasValue ? p.YeniFiyat.Value.ToString() : "",
                                                DosyaAdi = p.ResimUrl,
                                                Dosya = p.Resim,
                                                Resim = p.Resim == null ? "/Uploads/Site/noimg.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                                //OzetDosyaAdi = p.OzetDosyaUrl,
                                                //OzetDosya = p.OzetDosya,
                                                Pdf = p.OzetDosya == null ? "" : p.OzetDosyaUrl,
                                                //Pdf = p.OzetDosya == null ? "" : String.Format("data:application/octet-stream;base64,{0}", Convert.ToBase64String(p.OzetDosya, 0, p.OzetDosya.Length)),
                                                Sira = p.Sira,
                                                AktifMi = p.AktifMi
                                            })
                                            .SingleOrDefaultAsync();
            }
        }

        public async Task<List<YayinAramaSonucViewModel>> YayinAramaSonucViewModelGetir(YayinAramaViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var query = dbContext.Yayin.Where(p => (model.Aktiflik == -1 || p.AktifMi == Convert.ToBoolean(model.Aktiflik)) &&
                                                       (model.Ad == null || p.Ad.Contains(model.Ad)));

                return await query.OrderByDescending(p => p.YayinId)
                                  .Select(p => new YayinAramaSonucViewModel
                                  {
                                      TotalCount = query.Count(),

                                      YayinId = p.YayinId,
                                      Ad = p.Ad,
                                      EskiFiyat = p.EskiFiyat.HasValue ? p.EskiFiyat.Value.ToString() : "",
                                      YeniFiyat = p.YeniFiyat.HasValue ? p.YeniFiyat.Value.ToString() : "",
                                      //YeniFiyat = p.YeniFiyat.HasValue ? p.YeniFiyat.Value.ToString(CultureInfo.GetCultureInfo("tr-TR")) : "",
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

        public async Task<List<YayinViewModel>> YayinListGetir()
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Yayin.AsNoTracking()
                                            .Where(p => p.AktifMi)
                                            .OrderBy(p => p.Sira)
                                            .Select(p => new YayinViewModel
                                            {
                                                YayinId = p.YayinId,
                                                Ad = p.Ad,
                                                EskiFiyat = p.EskiFiyat,
                                                YeniFiyat = p.YeniFiyat,
                                                Resim = p.Resim == null ? "/Uploads/Site/noimg.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                                DosyaVarmi = p.OzetDosya == null ? false : true,
                                                //OzetDosya = p.OzetDosya == null ? "" : String.Format("data:application/pdf;base64,{0}", Convert.ToBase64String(p.OzetDosya, 0, p.OzetDosya.Length))
                                            })
                                            .ToListAsync();
            }
        }

        public async Task<byte[]> YayinOrnekDosyaGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Yayin.AsNoTracking()
                                            .Where(p => p.YayinId == id)
                                            .OrderBy(p => p.Sira)
                                            .Select(p =>
                                                p.OzetDosya
                                            )
                                            .SingleOrDefaultAsync();
            }
        }

        #endregion
    }
}
