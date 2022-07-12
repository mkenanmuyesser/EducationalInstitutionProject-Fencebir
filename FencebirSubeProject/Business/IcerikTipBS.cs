using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Data;
using FencebirSubeProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Business
{
    public class IcerikTipBS
    {
        #region Admin
        public async Task<List<IcerikTipSonucViewModel>> IcerikTipListGetir()
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.IcerikTip.AsNoTracking()
                                                .Where(p => p.AktifMi)
                                                .OrderBy(p => p.Sira)
                                                .Select(p => new IcerikTipSonucViewModel
                                                {
                                                    IcerikTipId = p.IcerikTipId,
                                                    IcerikTipAdi = p.IcerikTipAdi
                                                })
                                                .ToListAsync();
            }
        }

        #endregion

        #region FrontEnd

        #endregion
    }
}
