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
    public class BlogBS
    {
        #region Admin

        public async Task<int> BlogKaydet(BlogKayitViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var blog = new Blog();

                if (model.BlogId == 0)
                {
                    blog = new Blog()
                    {
                        //BlogId = model.BlogId,
                        SubeId = model.SubeId,
                        BlogTipId = model.BlogTipId,
                        Baslik = model.Baslik,
                        KisaIcerik = model.KisaIcerik,
                        Icerik = model.Icerik,
                        Etiketler = model.Etiketler,
                        YayinTarihi = DateTime.Parse(model.YayinTarihi),
                        ResimUrl = model.DosyaAdi,
                        Resim = model.Dosya,
                        Anasayfa = model.Anasayfa,
                        OkunmaSayisi = model.OkunmaSayisi,
                        KayitKullaniciId = model.IslemKullaniciId,
                        KayitTarih = model.IslemTarih,
                        GuncellemeId = null,
                        GuncellemeTarih = null,
                        Sira = model.Sira,
                        AktifMi = model.AktifMi,
                    };

                    dbContext.Blog.Add(blog);
                }
                else
                {
                    blog = await BlogGetir(model.BlogId);
                    dbContext.Entry(blog).State = EntityState.Modified;

                    blog.SubeId = model.SubeId;
                    blog.BlogTipId = model.BlogTipId;
                    blog.Baslik = model.Baslik;
                    blog.KisaIcerik = model.KisaIcerik;
                    blog.Icerik = model.Icerik;
                    blog.Etiketler = model.Etiketler;
                    blog.YayinTarihi = DateTime.Parse(model.YayinTarihi);
                    blog.Anasayfa = model.Anasayfa;
                    blog.OkunmaSayisi = model.OkunmaSayisi;
                    blog.GuncellemeId = model.IslemKullaniciId;
                    blog.GuncellemeTarih = model.IslemTarih;
                    blog.Sira = model.Sira;
                    blog.AktifMi = model.AktifMi;

                    if (model.Dosya != null)
                    {
                        blog.ResimUrl = model.DosyaAdi;
                        blog.Resim = model.Dosya;
                    }
                }

                var id = await dbContext.SaveChangesAsync();

                return id > 0 ? blog.BlogId : 0;
            }
        }

        public async Task<bool> BlogSil(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var blog = await BlogGetir(id);
                dbContext.Entry(blog).State = EntityState.Modified;

                blog.AktifMi = false;

                var result = await dbContext.SaveChangesAsync();

                return result > 0 ? true : false;
            }
        }

        public async Task<Blog> BlogGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Blog.SingleOrDefaultAsync(p => p.BlogId == id);
            }
        }

        public async Task<BlogKayitViewModel> BlogKayitViewModelGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Blog.Where(p => p.BlogId == id)
                                           .Select(p => new BlogKayitViewModel
                                           {
                                               BlogId = p.BlogId,
                                               SubeId = p.SubeId,
                                               BlogTipId = p.BlogTipId,
                                               Baslik = p.Baslik,
                                               KisaIcerik = p.KisaIcerik,
                                               Icerik = p.Icerik,
                                               Etiketler = p.Etiketler,
                                               YayinTarihi = p.YayinTarihi.Date.ToString("dd.MM.yyyy"),
                                               Anasayfa = p.Anasayfa,
                                               OkunmaSayisi = p.OkunmaSayisi,
                                               DosyaAdi = p.ResimUrl,
                                               Dosya = p.Resim,
                                               Resim = p.Resim == null ? "/Uploads/Site/noimg.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                               Sira = p.Sira,
                                               AktifMi = p.AktifMi
                                           })
                                           .SingleOrDefaultAsync();
            }
        }

        public async Task<List<BlogAramaSonucViewModel>> BlogAramaSonucViewModelGetir(BlogAramaViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var query = dbContext.Blog.Include("Sube")
                                          .Include("BlogTip")
                                          .Where(p => (model.Aktiflik == -1 || p.AktifMi == Convert.ToBoolean(model.Aktiflik)) &&
                                                      (model.SubeId == 0 || p.SubeId == model.SubeId) &&
                                                      (model.BlogTipId == 0 || p.BlogTipId == model.BlogTipId) &&
                                                      (model.Baslik == null || p.Baslik.Contains(model.Baslik)) &&
                                                      (model.Icerik == null || p.Icerik.Contains(model.Icerik)) &&
                                                      (model.Etiketler == null || p.Baslik.Contains(model.Etiketler)));

                return await query.OrderByDescending(p => p.BlogId)
                                  .Select(p => new BlogAramaSonucViewModel
                                  {
                                      TotalCount = query.Count(),

                                      BlogId = p.BlogId,
                                      SubeAdi = p.Sube.SubeAdi,
                                      BlogTipAdi = p.BlogTip.BlogTipAdi,
                                      Baslik = p.Baslik,
                                      KisaIcerik = p.KisaIcerik,
                                      Etiketler = p.Etiketler,
                                      YayinTarihi = p.YayinTarihi.Date.ToString("dd.MM.yyyy"),
                                      Anasayfa = p.Anasayfa,
                                      OkunmaSayisi = p.OkunmaSayisi,
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

        public async Task<List<BlogViewModel>> BlogListGetir(bool anasayfa, int subeId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Blog.AsNoTracking()
                                           .Where(p => p.AktifMi &&
                                                       ((anasayfa && p.Anasayfa == anasayfa) || !anasayfa) &&
                                                       ((subeId == 1 && p.SubeId == subeId) || (subeId != 1 && (p.SubeId == subeId || p.SubeId == 1))))
                                           .OrderBy(p => p.Sira)
                                           .Select(p => new BlogViewModel
                                           {
                                               BlogId = p.BlogId,
                                               Baslik = p.Baslik,
                                               KisaIcerik = p.KisaIcerik,
                                               Icerik = p.Icerik,
                                               Resim = p.Resim == null ? "/Uploads/Site/noimg.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                               Etiketler = p.Etiketler,
                                               YayinTarihi = p.YayinTarihi,
                                               OkunmaSayisi = p.OkunmaSayisi
                                           })
                                           .ToListAsync();
            }
        }

        public async Task<BlogViewModel> BlogDataGetir(int blogId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var data = await dbContext.Blog.SingleOrDefaultAsync(p => p.AktifMi &&
                                                                          p.BlogId == blogId);
                var result = new BlogViewModel();

                if (data != null)
                {
                    data.OkunmaSayisi++;
                    await dbContext.SaveChangesAsync();

                    result = new BlogViewModel
                    {
                        BlogId = data.BlogId,
                        Baslik = data.Baslik,
                        KisaIcerik = data.KisaIcerik,
                        Icerik = data.Icerik,
                        Resim = data.Resim == null ? "/Uploads/Site/noimg.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(data.Resim, 0, data.Resim.Length)),
                        Etiketler = data.Etiketler,
                        YayinTarihi = data.YayinTarihi,
                        OkunmaSayisi = data.OkunmaSayisi
                    };
                }

                return result ?? new BlogViewModel();
            }
        }

        #endregion
    }
}
