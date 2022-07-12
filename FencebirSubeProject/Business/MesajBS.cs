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
    public class MesajBS
    {
        #region Admin

        public async Task<List<MesajAramaSonucViewModel>> MesajAramaSonucViewModelGetir(MesajAramaViewModel model, MesajTipEnum mesajTip)
        {
            using (var dbContext = new ProjectDBContext())
            {
                int mesajTipId = Convert.ToInt32(mesajTip);
                var query = dbContext.Mesaj.Include("Sube")
                                           .Where(p => p.MesajTipId == mesajTipId &&
                                                       (model.SubeId == 0 || p.SubeId == model.SubeId) &&
                                                       (model.MesajIcerik == null || p.MesajIcerik.Contains(model.MesajIcerik)));

                return await query.OrderByDescending(p => p.MesajId)
                                  .Select(p => new MesajAramaSonucViewModel
                                  {
                                      TotalCount = query.Count(),

                                      MesajId = p.MesajId,
                                      SubeAdi = p.Sube.SubeAdi,
                                      MesajIcerik = p.MesajIcerik,
                                      GonderimTarihi = p.GonderimTarihi.ToString("dd.MM.yyyy HH:mm"),
                                      DosyaAdi = p.DosyaAdi
                                  })
                                  .Skip(model.start)
                                  .Take(model.length)
                                  .ToListAsync();
            }
        }

        public async Task<DosyaSonucViewModel> MesajDosyaGetir(int id)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Mesaj.Where(p => p.MesajId == id)
                                            .Select(p => new DosyaSonucViewModel
                                            {
                                                Dosya = p.Dosya,
                                                DosyaAdi = p.DosyaAdi
                                            })
                                            .SingleOrDefaultAsync();
            }
        }

        #endregion

        #region FrontEnd

        public async Task<int> BilgiTalepKaydet(BilgiTalepViewModel model, int subeId)
        {
            KonuTipBS _KonuTipBS = new KonuTipBS();
            var konuTip = await _KonuTipBS.KonuTipDataGetir(model.KonuTipId);

            using (var dbContext = new ProjectDBContext())
            {
                int mesajTip = Convert.ToInt32(MesajTipEnum.BilgiTalep);
                string icerik = "<h4>Bilgi Talep</h4><br/>" +
                                "<b>Ad Soyad : </b>" + model.AdSoyad + "<br/>" +
                                "<b>E-posta </b>: " + model.Eposta + "<br/>" +
                                "<b>Telefon </b>: " + model.Telefon + "<br/>" +
                                "<b>Konu : </b>" + konuTip.KonuTipAdi + "<br/>" +
                                "<b>Sınıf : </b>" + model.Sinif + "<br/>" +
                                "<b>Mesaj </b>: " + model.Mesaj;

                var mesaj = new Mesaj()
                {
                    //MesajId = -1,
                    MesajTipId = mesajTip,
                    SubeId = subeId,
                    MesajIcerik = icerik,
                    GonderimTarihi = DateTime.Now
                };
                dbContext.Mesaj.Add(mesaj);
                return await dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> IletisimTalepKaydet(IletisimTalepViewModel model, int subeId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                int mesajTip = Convert.ToInt32(MesajTipEnum.IletisimTalep);
                string icerik = "<h4>İletişim Talep</h4><br/>" +
                                "<b>Ad Soyad : </b>" + model.AdSoyad + "<br/>" +
                                "<b>Konu : </b>" + model.Konu + "<br/>" +
                                "<b>E-posta </b>: " + model.Eposta + "<br/>" +
                                "<b>Telefon </b>: " + model.Telefon + "<br/>" +
                                "<b>Mesaj </b>: " + model.Mesaj;

                var mesaj = new Mesaj()
                {
                    //MesajId = -1,
                    MesajTipId = mesajTip,
                    SubeId = subeId,
                    MesajIcerik = icerik,
                    GonderimTarihi = DateTime.Now
                };
                dbContext.Mesaj.Add(mesaj);
                return await dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> FranchiseTalepKaydet(FranchiseTalepViewModel model, int subeId)
        {
            SubeSehirBS _SubeSehirBS = new SubeSehirBS();
            var sehir = await _SubeSehirBS.SehirBilgiDataGetir(model.SehirId);

            KurumTipBS _KurumTipBS = new KurumTipBS();
            var kurumTip = await _KurumTipBS.KurumTipDataGetir(model.KurumTipId);

            using (var dbContext = new ProjectDBContext())
            {
                int mesajTip = Convert.ToInt32(MesajTipEnum.FranchiseTalep);
                string icerik = "<h4>Franchise Talep</h4><br/>" +
                                "<b>Ad : </b>" + model.Ad + "<br/>" +
                                "<b>Soyad : </b>" + model.Soyad + "<br/>" +
                                "<b>Telefon : </b>" + model.Telefon + "<br/>" +
                                "<b>E-posta </b>: " + model.Eposta + "<br/>" +
                                "<b>Şehir </b>: " + sehir.SehirAdi + "<br/>" +
                                "<b>Kurum Tip </b>: " + kurumTip.KurumTipAdi + "<br/>" +
                                "<b>Açıklama </b>: " + model.Aciklama;

                var mesaj = new Mesaj()
                {
                    //MesajId = -1,
                    MesajTipId = mesajTip,
                    SubeId = subeId,
                    MesajIcerik = icerik,
                    GonderimTarihi = DateTime.Now,
                    Dosya = model.Dosya,
                    DosyaAdi = model.DosyaAdi
                };
                dbContext.Mesaj.Add(mesaj);
                return await dbContext.SaveChangesAsync();
            }
        }

        #endregion
    }
}
