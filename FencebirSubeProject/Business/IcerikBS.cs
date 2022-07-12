using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Data;
using FencebirSubeProject.Entities;
using FencebirSubeProject.Enums;
using FencebirSubeProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FencebirSubeProject.Infra;

namespace FencebirSubeProject.Business
{
    public class IcerikBS
    {
        #region Admin

        public async Task<bool> IcerikKontrol(int subeId, int icerikTipId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var result = await dbContext.Icerik.AsNoTracking()
                                                   .AnyAsync(p => p.IcerikTipId == icerikTipId &&
                                                                  p.SubeId == subeId);

                return !result;
            }
        }

        public async Task<int> IcerikKaydet(BilgiMetniKayitViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var icerik = new Icerik();

                if (model.IcerikId == 0)
                {
                    icerik = new Icerik()
                    {
                        //IcerikId = model.IcerikId,
                        SubeId = model.SubeId,
                        IcerikTipId = model.IcerikTipId,
                        IcerikMetin = model.HtmlIcerik ? model.IcerikMetin.HtmlDecode() : model.IcerikMetin,
                        HtmlIcerik = model.HtmlIcerik,
                        KayitKullaniciId = model.IslemKullaniciId,
                        KayitTarih = model.IslemTarih,
                        GuncellemeKullaniciId = null,
                        GuncellemeTarih = null,
                    };

                    dbContext.Icerik.Add(icerik);
                }
                else
                {
                    icerik = await IcerikGetir(model.IcerikId);
                    dbContext.Entry(icerik).State = EntityState.Modified;

                    icerik.SubeId = model.SubeId;
                    icerik.IcerikTipId = model.IcerikTipId;
                    icerik.IcerikMetin = model.HtmlIcerik ? model.IcerikMetin.HtmlDecode() : model.IcerikMetin;
                    icerik.HtmlIcerik = model.HtmlIcerik;
                    icerik.GuncellemeKullaniciId = model.IslemKullaniciId;
                    icerik.GuncellemeTarih = model.IslemTarih;
                }

                var id = await dbContext.SaveChangesAsync();

                return id > 0 ? icerik.IcerikId : 0;
            }
        }

        public async Task<Icerik> IcerikGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Icerik.SingleOrDefaultAsync(p => p.IcerikId == id);
            }
        }

        public async Task<BilgiMetniKayitViewModel> BilgiMetniKayitViewModelGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Icerik.Where(p => p.IcerikId == id)
                                             .Select(p => new BilgiMetniKayitViewModel
                                             {
                                                 IcerikId = p.IcerikId,
                                                 SubeId = p.SubeId,
                                                 IcerikTipId = p.IcerikTipId,
                                                 IcerikMetin = p.IcerikMetin,
                                                 HtmlIcerik = p.HtmlIcerik,
                                             })
                                             .SingleOrDefaultAsync();
            }
        }

        public async Task<List<BilgiMetniAramaSonucViewModel>> BilgiMetniAramaSonucViewModelGetir(BilgiMetniAramaViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var query = dbContext.Icerik.Include("Sube")
                                            .Include("IcerikTip")
                                            .Where(p => (model.SubeId == 0 || p.SubeId == model.SubeId) &&
                                                        (model.IcerikTipId == 0 || p.IcerikTipId == model.IcerikTipId) &&
                                                        (model.IcerikMetin == null || p.IcerikMetin.Contains(model.IcerikMetin)));

                return await query.OrderByDescending(p => p.IcerikId)
                                  .Select(p => new BilgiMetniAramaSonucViewModel
                                  {
                                      TotalCount = query.Count(),

                                      IcerikId = p.IcerikId,
                                      SubeAdi = p.Sube.SubeAdi,
                                      IcerikTipAdi = p.IcerikTip.IcerikTipAdi,
                                      IcerikMetin = p.IcerikMetin,
                                      HtmlIcerik = p.HtmlIcerik
                                  })
                                  .Skip(model.start)
                                  .Take(model.length)
                                  .ToListAsync();
            }
        }

        #endregion

        #region FrontEnd

        public async Task<IcerikViewModel> CerezPolitikasiBilgiMetniDataGetir()
        {
            using (var dbContext = new ProjectDBContext())
            {
                var result = await dbContext.Icerik.AsNoTracking()
                                                   .Where(p => p.IcerikId == 1)
                                                   .Select(p => new IcerikViewModel
                                                   {
                                                       IcerikMetin = p.IcerikMetin
                                                   })
                                                   .SingleOrDefaultAsync();

                return result ?? new IcerikViewModel();
            }
        }

        public async Task<IcerikViewModel> HosgeldinBilgiMetniDataGetir(int subeId)
        {
            int icerikTipId = Convert.ToInt32(IcerikTipEnum.HosgeldinBilgiMetni);

            using (var dbContext = new ProjectDBContext())
            {
                var result = await dbContext.Icerik.AsNoTracking()
                                                   .Where(p => p.IcerikTipId == icerikTipId &&
                                                               p.SubeId == subeId)
                                                   .Select(p => new IcerikViewModel
                                                   {
                                                       IcerikMetin = p.IcerikMetin
                                                   })
                                                   .SingleOrDefaultAsync();
                return result ?? new IcerikViewModel();
            }
        }

        public async Task<IcerikViewModel> VideoBilgiMetniDataGetir(int subeId)
        {
            int icerikTipId = Convert.ToInt32(IcerikTipEnum.VideoBilgiMetni);

            using (var dbContext = new ProjectDBContext())
            {
                var result = await dbContext.Icerik.AsNoTracking()
                                                   .Where(p => p.IcerikTipId == icerikTipId &&
                                                               p.SubeId == subeId)
                                                   .Select(p => new IcerikViewModel
                                                   {
                                                       IcerikMetin = p.IcerikMetin
                                                   })
                                                   .SingleOrDefaultAsync();
                return result ?? new IcerikViewModel();
            }
        }

        public async Task<IcerikViewModel> VideoBilgiEmbedCodeDataGetir(int subeId)
        {
            int icerikTipId = Convert.ToInt32(IcerikTipEnum.VideoBilgiEmbedCode);

            using (var dbContext = new ProjectDBContext())
            {
                var result = await dbContext.Icerik.AsNoTracking()
                                                   .Where(p => p.IcerikTipId == icerikTipId &&
                                                               p.SubeId == subeId)
                                                   .Select(p => new IcerikViewModel
                                                   {
                                                       IcerikMetin = p.IcerikMetin
                                                   })
                                                   .SingleOrDefaultAsync();
                return result ?? new IcerikViewModel();
            }
        }

        public async Task<IcerikViewModel> HakkimizdaUstBilgiMetniDataGetir(int subeId)
        {
            int icerikTipId = Convert.ToInt32(IcerikTipEnum.HakkimizdaUstBilgiMetni);

            using (var dbContext = new ProjectDBContext())
            {
                var result = await dbContext.Icerik.AsNoTracking()
                                                   .Where(p => p.IcerikTipId == icerikTipId &&
                                                               p.SubeId == subeId)
                                                   .Select(p => new IcerikViewModel
                                                   {
                                                       IcerikMetin = p.IcerikMetin
                                                   })
                                                  .SingleOrDefaultAsync();

                return result ?? new IcerikViewModel();
            }
        }

        public async Task<IcerikViewModel> HakkimizdaAltBilgiMetniDataGetir(int subeId)
        {
            int icerikTipId = Convert.ToInt32(IcerikTipEnum.HakkimizdaAltBilgiMetni);

            using (var dbContext = new ProjectDBContext())
            {
                var result = await dbContext.Icerik.AsNoTracking()
                                                   .Where(p => p.IcerikTipId == icerikTipId &&
                                                               p.SubeId == subeId)
                                                   .Select(p => new IcerikViewModel
                                                   {
                                                       IcerikMetin = p.IcerikMetin
                                                   })
                                                   .SingleOrDefaultAsync();

                return result ?? new IcerikViewModel();
            }
        }

        public async Task<IcerikViewModel> SirketHaritaKoduDataGetir(int subeId)
        {
            int icerikTipId = Convert.ToInt32(IcerikTipEnum.SirketHaritaKodu);

            using (var dbContext = new ProjectDBContext())
            {
                var result = await dbContext.Icerik.AsNoTracking()
                                                   .Where(p => p.IcerikTipId == icerikTipId &&
                                                               p.SubeId == subeId)
                                                   .Select(p => new IcerikViewModel
                                                   {
                                                       IcerikMetin = p.IcerikMetin
                                                   })
                                                  .SingleOrDefaultAsync();

                return result ?? new IcerikViewModel();
            }
        }

        public async Task<IcerikViewModel> FranchiseBilgiMetniDataGetir()
        {
            int icerikTipId = Convert.ToInt32(IcerikTipEnum.FranchiseBilgiMetni);

            using (var dbContext = new ProjectDBContext())
            {
                var result = await dbContext.Icerik.AsNoTracking()
                                                   .Where(p => p.IcerikTipId == icerikTipId)
                                                   .Select(p => new IcerikViewModel
                                                   {
                                                       IcerikMetin = p.IcerikMetin
                                                   })
                                                  .SingleOrDefaultAsync();

                return result ?? new IcerikViewModel();
            }
        }

        #endregion
    }
}
