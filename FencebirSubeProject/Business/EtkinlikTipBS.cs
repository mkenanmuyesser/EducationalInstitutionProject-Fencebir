using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Data;
using FencebirSubeProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Business
{
    public class EtkinlikTipBS
    {
        #region Admin
        public async Task<List<EtkinlikTipSonucViewModel>> EtkinlikTipListGetir()
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.EtkinlikTip.AsNoTracking()
                                                  .Where(p => p.AktifMi)
                                                  .OrderBy(p => p.Sira)
                                                  .Select(p => new EtkinlikTipSonucViewModel
                                                  {
                                                      EtkinlikTipId = p.EtkinlikTipId,
                                                      EtkinlikTipAdi = p.EtkinlikTipAdi
                                                  })
                                                  .ToListAsync();
            }
        }

        #endregion

        #region FrontEnd

        #endregion
    }
}
