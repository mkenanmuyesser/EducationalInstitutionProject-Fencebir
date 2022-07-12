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

namespace FencebirSubeProject.Business
{
    public class SubeBS
    {
        #region Admin

        public async Task<List<SubeSonucViewModel>> TumSubeListGetir()
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Sube.AsNoTracking()
                                           .Include("SubeSehir")
                                           .Include("SubeTip")
                                           .OrderBy(p => p.Sira)
                                           .Select(p => new SubeSonucViewModel
                                           {
                                               SubeId = p.SubeId,
                                               SubeTipId = p.SubeTipId,
                                               SubeSehirId = p.SubeSehirId,
                                               SubeTipAdi = p.SubeTip.SubeTipAdi,
                                               SubeSehirAdi = p.SubeSehir.SubeSehirAdi,
                                               SubeAdi = p.SubeAdi,
                                               SubeAttribute = p.SubeAttribute
                                           })
                                           .ToListAsync();
            }
        }

        public async Task<int> SubeKaydet(SubeKayitViewModel model)
        {
            BannerBS _BannerBS = new BannerBS();
            IcerikBS _IcerikBS = new IcerikBS();
            IcerikTipBS _IcerikTipBS = new IcerikTipBS();

            var bannerList = await _BannerBS.OlusturulacakBannerGetir();
            var icerikTipList = await _IcerikTipBS.IcerikTipListGetir();
            int cerezPolitikasiBilgiMetni = Convert.ToInt32(IcerikTipEnum.CerezPolitikasiBilgiMetni);
            int franchiseBilgiMetni = Convert.ToInt32(IcerikTipEnum.FranchiseBilgiMetni);

            using (var dbContext = new ProjectDBContext())
            {
                var sube = new Sube();

                if (model.SubeId == 0)
                {
                    sube = new Sube()
                    {
                        //SubeId = model.SubeId,
                        SubeTipId = model.SubeTipId,
                        SubeSehirId = model.SubeSehirId,
                        SubeAdi = model.SubeAdi,
                        SubeAttribute = model.SubeAttribute,
                        Aciklama = model.Aciklama,
                        Adres = model.Adres,
                        Telefon1 = model.Telefon1,
                        Telefon2 = model.Telefon2,
                        Fax1 = model.Fax1,
                        Fax2 = model.Fax2,
                        Eposta = model.Eposta,
                        FacebookHesapUrl = model.FacebookHesapUrl,
                        InstagramHesapUrl = model.InstagramHesapUrl,
                        TwitterHesapUrl = model.TwitterHesapUrl,
                        WhatsappHesapUrl = model.WhatsappHesapUrl,
                        YoutubeHesapUrl = model.YoutubeHesapUrl,
                        GonderilecekEpostaTanim = model.GonderilecekEpostaTanim,
                        GonderilecekEpostaKullaniciAdi = model.GonderilecekEpostaKullaniciAdi,
                        GonderilecekEpostaSifre = model.GonderilecekEpostaSifre,
                        GonderilecekEpostaHost = model.GonderilecekEpostaHost,
                        GonderilecekEpostaPort = model.GonderilecekEpostaPort,
                        GonderilecekEpostaSsl = model.GonderilecekEpostaSsl,
                        GonderilecekEpostaAktifMi = model.GonderilecekEpostaAktifMi,
                        ResimUrl = model.DosyaAdi,
                        Resim = model.Dosya,
                        KayitKullaniciId = model.IslemKullaniciId,
                        KayitTarih = model.IslemTarih,
                        GuncellemeId = null,
                        GuncellemeTarih = null,
                        Sira = model.Sira,
                        AktifMi = model.AktifMi
                    };

                    #region Banner, Içerik ve İçerik Ayar

                    foreach (var banner in bannerList)
                    {
                        var subeBanner = new Banner()
                        {
                            Sube = sube,
                            BannerTipId = banner.BannerTipId,
                            Adi = banner.Adi,
                            ResimUrl = banner.ResimUrl,
                            Resim = banner.Resim,
                            Aciklama1 = banner.Aciklama1,
                            Aciklama2 = banner.Aciklama2,
                            Aciklama3 = banner.Aciklama3,
                            Link = banner.Link,
                            LinkAciklama = banner.LinkAciklama,
                            BannerOlusturma = false,
                            KayitKullaniciId = model.IslemKullaniciId,
                            KayitTarih = model.IslemTarih,
                            AktifMi = true,
                        };
                        sube.Banner.Add(subeBanner);
                    }

                    //şube ile birlikte banner ve içerikler insert edilecek
                    foreach (var icerikTip in icerikTipList)
                    {
                        if (!(icerikTip.IcerikTipId == cerezPolitikasiBilgiMetni || icerikTip.IcerikTipId == franchiseBilgiMetni))
                        {
                            var subeIcerik = new Icerik()
                            {
                                Sube = sube,
                                IcerikTipId = icerikTip.IcerikTipId,
                                IcerikMetin = icerikTip.IcerikTipAdi + "<p>Buraya metin gelecek...</p>",
                                HtmlIcerik = false,
                                KayitKullaniciId = model.IslemKullaniciId,
                                KayitTarih = model.IslemTarih,
                            };
                            sube.Icerik.Add(subeIcerik);
                        }
                    }

                    #endregion

                    dbContext.Sube.Add(sube);
                }
                else
                {
                    sube = await SubeGetir(model.SubeId);
                    dbContext.Entry(sube).State = EntityState.Modified;

                    sube.SubeTipId = model.SubeTipId;
                    sube.SubeSehirId = model.SubeSehirId;
                    sube.SubeAdi = model.SubeAdi;
                    sube.SubeAttribute = model.SubeAttribute;
                    sube.Aciklama = model.Aciklama;
                    sube.Adres = model.Adres;
                    sube.Telefon1 = model.Telefon1;
                    sube.Telefon2 = model.Telefon2;
                    sube.Fax1 = model.Fax1;
                    sube.Fax2 = model.Fax2;
                    sube.Eposta = model.Eposta;
                    sube.FacebookHesapUrl = model.FacebookHesapUrl;
                    sube.InstagramHesapUrl = model.InstagramHesapUrl;
                    sube.TwitterHesapUrl = model.TwitterHesapUrl;
                    sube.WhatsappHesapUrl = model.WhatsappHesapUrl;
                    sube.YoutubeHesapUrl = model.YoutubeHesapUrl;
                    sube.GonderilecekEpostaTanim = model.GonderilecekEpostaTanim;
                    sube.GonderilecekEpostaKullaniciAdi = model.GonderilecekEpostaKullaniciAdi;
                    sube.GonderilecekEpostaSifre = model.GonderilecekEpostaSifre;
                    sube.GonderilecekEpostaHost = model.GonderilecekEpostaHost;
                    sube.GonderilecekEpostaPort = model.GonderilecekEpostaPort;
                    sube.GonderilecekEpostaSsl = model.GonderilecekEpostaSsl;
                    sube.GonderilecekEpostaAktifMi = model.GonderilecekEpostaAktifMi;
                    sube.GuncellemeId = model.IslemKullaniciId;
                    sube.GuncellemeTarih = model.IslemTarih;
                    sube.Sira = model.Sira;
                    sube.AktifMi = model.AktifMi;

                    if (model.Dosya != null)
                    {
                        sube.ResimUrl = model.DosyaAdi;
                        sube.Resim = model.Dosya;
                    }

                    #region Banner, Içerik ve İçerik Ayar

                    //eğer veri yoksa şube ile birlikte banner ve içerikler insert edilecek
                    var bannerKontrol = await _BannerBS.BannerListGetir(model.SubeId);
                    if (!bannerKontrol.Any())
                    {
                        foreach (var banner in bannerList)
                        {
                            var subeBanner = new Banner()
                            {
                                Sube = sube,
                                BannerTipId = banner.BannerTipId,
                                Adi = banner.Adi,
                                ResimUrl = banner.ResimUrl,
                                Resim = banner.Resim,
                                Aciklama1 = banner.Aciklama1,
                                Aciklama2 = banner.Aciklama2,
                                Aciklama3 = banner.Aciklama3,
                                Link = banner.Link,
                                LinkAciklama = banner.LinkAciklama,
                                BannerOlusturma = false,
                                KayitKullaniciId = model.IslemKullaniciId,
                                KayitTarih = model.IslemTarih,
                                AktifMi = true,
                            };
                            sube.Banner.Add(subeBanner);
                        }
                    }

                    foreach (var icerikTip in icerikTipList)
                    {
                        if (!(icerikTip.IcerikTipId == cerezPolitikasiBilgiMetni || icerikTip.IcerikTipId == franchiseBilgiMetni))
                        {
                            var icerikKontrol = await _IcerikBS.IcerikKontrol(model.SubeId, icerikTip.IcerikTipId);
                            if (icerikKontrol)
                            {
                                var subeIcerik = new Icerik()
                                {
                                    Sube = sube,
                                    IcerikTipId = icerikTip.IcerikTipId,
                                    IcerikMetin = icerikTip.IcerikTipAdi + "<p>Buraya metin gelecek...</p>",
                                    HtmlIcerik = false,
                                };
                                sube.Icerik.Add(subeIcerik);
                            }
                        }
                    }

                    #endregion
                }

                var id = await dbContext.SaveChangesAsync();

                return id > 0 ? sube.SubeId : 0;
            }
        }

        public async Task<bool> SubeSil(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                if (id != 1)
                {
                    var sube = await SubeGetir(id);
                    dbContext.Entry(sube).State = EntityState.Modified;

                    sube.AktifMi = false;

                    var result = await dbContext.SaveChangesAsync();

                    return result > 0 ? true : false;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> SubeKontrol(int subeId, string subeAttribute)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var result = await dbContext.Sube.AnyAsync(p => p.SubeId != subeId && p.SubeAttribute.ToLower() == subeAttribute.ToLower());
                return !result;
            }
        }

        public async Task<Sube> SubeGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Sube.SingleOrDefaultAsync(p => p.SubeId == id);
            }
        }

        public async Task<SubeKayitViewModel> SubeKayitViewModelGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Sube.Where(p => p.SubeId == id)
                                           .Select(p => new SubeKayitViewModel
                                           {
                                               SubeId = p.SubeId,
                                               SubeTipId = p.SubeTipId,
                                               SubeSehirId = p.SubeSehirId,
                                               SubeAdi = p.SubeAdi,
                                               SubeAttribute = p.SubeAttribute,
                                               Aciklama = p.Aciklama,
                                               Adres = p.Adres,
                                               Telefon1 = p.Telefon1,
                                               Telefon2 = p.Telefon2,
                                               Fax1 = p.Fax1,
                                               Fax2 = p.Fax2,
                                               Eposta = p.Eposta,
                                               FacebookHesapUrl = p.FacebookHesapUrl,
                                               InstagramHesapUrl = p.InstagramHesapUrl,
                                               TwitterHesapUrl = p.TwitterHesapUrl,
                                               WhatsappHesapUrl = p.WhatsappHesapUrl,
                                               YoutubeHesapUrl = p.YoutubeHesapUrl,
                                               GonderilecekEpostaTanim = p.GonderilecekEpostaTanim,
                                               GonderilecekEpostaKullaniciAdi = p.GonderilecekEpostaKullaniciAdi,
                                               GonderilecekEpostaSifre = p.GonderilecekEpostaSifre,
                                               GonderilecekEpostaHost = p.GonderilecekEpostaHost,
                                               GonderilecekEpostaPort = p.GonderilecekEpostaPort,
                                               GonderilecekEpostaSsl = p.GonderilecekEpostaSsl,
                                               GonderilecekEpostaAktifMi = p.GonderilecekEpostaAktifMi,
                                               DosyaAdi = p.ResimUrl,
                                               Dosya = p.Resim,
                                               Resim = p.Resim == null ? "/Uploads/Site/noimg.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                               Sira = p.Sira,
                                               AktifMi = p.AktifMi
                                           })
                                           .SingleOrDefaultAsync();
            }
        }

        public async Task<List<SubeAramaSonucViewModel>> SubeAramaSonucViewModelGetir(SubeAramaViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var query = dbContext.Sube.Include("SubeTip")
                                          .Include("SubeSehir")
                                          .Where(p => (model.SubeId == 1 || (model.SubeId != 1 && p.SubeId == model.SubeId)) &&
                                                      (model.Aktiflik == -1 || p.AktifMi == Convert.ToBoolean(model.Aktiflik)) &&
                                                      (model.SubeTipId == 0 || p.SubeTipId == model.SubeTipId) &&
                                                      (model.SubeSehirId == 0 || p.SubeTipId == model.SubeSehirId) &&
                                                      (model.SubeAdi == null || p.SubeAdi.Contains(model.SubeAdi)));

                return await query.OrderByDescending(p => p.SubeId)
                                  .Select(p => new SubeAramaSonucViewModel
                                  {
                                      TotalCount = query.Count(),

                                      SubeId = p.SubeId,
                                      SubeTipAdi = p.SubeTip.SubeTipAdi,
                                      SubeSehirAdi = p.SubeSehir.SubeSehirAdi,
                                      SubeAdi = p.SubeAdi,
                                      SubeAttribute = p.SubeAttribute,
                                      Aciklama = p.Aciklama,
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

        public async Task<List<SubeViewModel>> SubeListGetir()
        {
            int subeTipId = Convert.ToInt32(SubeTipEnum.Sube);

            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Sube.AsNoTracking()
                                           .Include("SubeSehir")
                                           .Where(p => p.AktifMi &&
                                                       p.SubeTipId == subeTipId)
                                           .OrderBy(p => p.Sira)
                                           .Select(p => new SubeViewModel
                                           {
                                               SubeTip = p.SubeTipId,
                                               SubeAdi = p.SubeAdi,
                                               SubeAttribute = p.SubeAttribute,
                                               SubeSehir = p.SubeSehir.SubeSehirAdi,
                                               SubeResim = p.Resim == null ? "/Uploads/Site/noimg.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length))
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
                                           .Include("SubeSehir")
                                           .Where(p => p.AktifMi &&
                                                       p.SubeTipId == subeTipId)
                                           .OrderBy(p => p.Sira)
                                           .Select(p => new SubeViewModel
                                           {
                                               SubeTip = p.SubeTipId,
                                               SubeAdi = p.SubeAdi,
                                               SubeAttribute = p.SubeAttribute,
                                               SubeSehir = p.SubeSehir.SubeSehirAdi,
                                               SubeResim = p.Resim == null ? "/Uploads/Site/noimg.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length))
                                           })
                                           .ToListAsync();
            }
        }

        public async Task<int> SubeTemsilciGetir(string attribute)
        {
            using (var dbContext = new ProjectDBContext())
            {
                var result = await dbContext.Sube.SingleOrDefaultAsync(p => p.SubeAttribute == attribute);
                return result == null ? 0 : result.SubeId;
            }
        }

        public async Task<IletisimViewModel> IletisimDataGetir(int subeId)
        {
            IcerikBS _IcerikBS = new IcerikBS();
            var sirketHaritaKodu = await _IcerikBS.SirketHaritaKoduDataGetir(subeId);

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
                                                     SirketMapCode = sirketHaritaKodu.IcerikMetin,
                                                     FacebookHesapUrl = p.FacebookHesapUrl,
                                                     InstagramHesapUrl = p.InstagramHesapUrl,
                                                     TwitterHesapUrl = p.TwitterHesapUrl,
                                                     WhatsappHesapUrl = p.WhatsappHesapUrl,
                                                     YoutubeHesapUrl = p.YoutubeHesapUrl,
                                                     Logo = p.Resim == null ? "/Uploads/Site/only_logo.png" : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(p.Resim, 0, p.Resim.Length)),
                                                 })
                                                 .SingleOrDefaultAsync();

                return result ?? new IletisimViewModel();
            }
        }

        public async Task<EpostaGonderimViewModel> EpostaGonderimDataGetir(int subeId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Sube.AsNoTracking()
                                           .Where(p => p.SubeId == subeId)
                                           .Select(p => new EpostaGonderimViewModel
                                           {
                                               GonderilecekEpostaHost = p.GonderilecekEpostaHost,
                                               GonderilecekEpostaPort = p.GonderilecekEpostaPort,
                                               GonderilecekEpostaKullaniciAdi = p.GonderilecekEpostaKullaniciAdi,
                                               GonderilecekEpostaSifre = p.GonderilecekEpostaSifre,
                                               GonderilecekEpostaTanim = p.GonderilecekEpostaTanim,
                                               GonderilecekEpostaSsl = p.GonderilecekEpostaSsl
                                           })
                                          .SingleOrDefaultAsync();
            }
        }

        #endregion
    }
}
