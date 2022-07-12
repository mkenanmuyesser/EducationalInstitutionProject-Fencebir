using FencebirSubeProject.Data;
using FencebirSubeProject.Enums;
using FencebirSubeProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Business
{
    public class _BaseBS
    {
        private readonly SubeBS _SubeBS;
        private readonly OgretmenBS _OgretmenBS;
        private readonly GaleriBS _GaleriBS;
        private readonly BlogBS _BlogBS;
        private readonly YayinBS _YayinBS;
        public _BaseBS()
        {
            _SubeBS = new SubeBS();
            _OgretmenBS = new OgretmenBS();
            _GaleriBS = new GaleriBS();
            _BlogBS = new BlogBS();
            _YayinBS = new YayinBS();
        }

        public async Task<int> SubeTemsilciIdGetir(string attribute)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var result = await dbContext.Sube.Where(p => p.SubeAttribute == attribute).Select(p => p.SubeId).SingleOrDefaultAsync();
                return result == null ? 0 : result;
            }
        }

        public async Task<IletisimViewModel> IletisimDataGetir(int subeId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var result = await dbContext.Sube.AsNoTracking()
                                                 .Where(p => p.SubeId == subeId)
                                                 .Select(p => new IletisimViewModel
                                                 {
                                                     SirketAdres = p.Adres,
                                                     SirketTelefon1 = p.Telefon1,
                                                     SirketTelefon2 = p.Telefon2,
                                                     SirketFax1 = p.Fax1,
                                                     SirketFax2 = p.Fax2,
                                                     SirketEposta = p.Eposta,
                                                     SirketMapCode = null,
                                                     FacebookHesapUrl = p.FacebookHesapUrl,
                                                     InstagramHesapUrl = p.InstagramHesapUrl,
                                                     TwitterHesapUrl = p.TwitterHesapUrl,
                                                     WhatsappHesapUrl = p.WhatsappHesapUrl,
                                                     YoutubeHesapUrl = p.YoutubeHesapUrl,
                                                     Logo = p.Resim == null ? "/Uploads/Site/only_logo.png" : string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                                 })
                                                 .SingleOrDefaultAsync();

                return result ?? new IletisimViewModel();
            }
        }

        public async Task<List<SubeViewModel>> SubeListGetir()
        {
            int subeTipId = Convert.ToInt32(SubeTipEnum.Sube);

            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Sube.AsNoTracking()
                                           .Where(p => p.AktifMi &&
                                                       p.SubeTipId == subeTipId)
                                           .OrderBy(p => p.Sira)
                                           .Select(p => new SubeViewModel
                                           {
                                               SubeTip = p.SubeTipId,
                                               SubeAdi = p.SubeAdi,
                                               SubeAttribute = p.SubeAttribute,
                                               SubeSehir = null,
                                               SubeResim = null
                                           })
                                           .ToListAsync();
            }
        }

        public async Task<List<SubeViewModel>> TemsilciListGetir()
        {
            int subeTipId = Convert.ToInt32(SubeTipEnum.Temsilci);

            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Sube.AsNoTracking()
                                           .Where(p => p.AktifMi &&
                                                       p.SubeTipId == subeTipId)
                                           .OrderBy(p => p.Sira)
                                           .Select(p => new SubeViewModel
                                           {
                                               SubeTip = p.SubeTipId,
                                               SubeAdi = p.SubeAdi,
                                               SubeAttribute = p.SubeAttribute,
                                               SubeSehir = null,
                                               SubeResim = null
                                           })
                                           .ToListAsync();
            }
        }

        public async Task<bool> YayinVarMi()
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Yayin.AsNoTracking()
                                            .AnyAsync(p => p.AktifMi);
            }
        }

        public async Task<bool> OgretmenVarMi(bool anasayfa, int subeId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Ogretmen.AsNoTracking()
                                               .AnyAsync(p => p.AktifMi &&
                                                           ((anasayfa && p.Anasayfa == anasayfa) || !anasayfa) &&
                                                           p.SubeId == subeId);
            }
        }

        public async Task<bool> GaleriVarMi(bool anasayfa, int subeId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Galeri.AsNoTracking()
                                             .AnyAsync(p => p.AktifMi &&
                                                           ((anasayfa && p.Anasayfa == anasayfa) || !anasayfa) &&
                                                           p.SubeId == subeId);
            }
        }

        public async Task<bool> BlogVarMi(bool anasayfa, int subeId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Blog.AsNoTracking()
                                           .AnyAsync(p => p.AktifMi &&
                                                       ((anasayfa && p.Anasayfa == anasayfa) || !anasayfa) &&
                                                       ((subeId == 1 && p.SubeId == subeId) || (subeId != 1 && (p.SubeId == subeId || p.SubeId == 1))));
            }
        }
    }
}
