using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Data;
using FencebirSubeProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Business
{
    public class BannerTipBS
    {
        #region Admin
        public async Task<List<BannerTipSonucViewModel>> BannerTipListGetir()
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.BannerTip.AsNoTracking()
                                                .Where(p => p.AktifMi)
                                                .OrderBy(p => p.Sira)
                                                .Select(p => new BannerTipSonucViewModel
                                                {
                                                    BannerTipId = p.BannerTipId,
                                                    BannerTipAdi = p.BannerTipAdi
                                                })
                                                .ToListAsync();
            }
        }

        #endregion

        #region FrontEnd

        #endregion
    }
}
