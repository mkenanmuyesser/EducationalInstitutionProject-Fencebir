using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Data;
using FencebirSubeProject.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FencebirSubeProject.Business
{
    public class AyarBS
    {
        #region Admin

        public async Task<int> AyarKaydet(AyarKayitViewModel model)
        {
            using (var dbContext = new ProjectDBContext())
            {
                Ayar ayar = await AyarGetir();
                dbContext.Entry(ayar).State = EntityState.Modified;
                ayar.IpBloklamaAktifMi = model.IpBloklamaAktifMi;
                ayar.IpBlokListesi = model.IpBlokListesi;
                ayar.UygulamaAktifMi = model.UygulamaAktifMi;
                ayar.IslemKullaniciId = model.IslemKullaniciId;
                ayar.IslemTarih = model.IslemTarih;

                var id = await dbContext.SaveChangesAsync();

                return id > 0 ? ayar.AyarId : 0;
            }
        }

        public async Task<Ayar> AyarGetir()
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.Ayar.SingleOrDefaultAsync();
            }
        }

        #endregion

        #region FrontEnd      

        #endregion
    }
}
