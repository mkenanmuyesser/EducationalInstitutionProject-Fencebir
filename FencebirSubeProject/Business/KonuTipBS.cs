using FencebirSubeProject.Data;
using FencebirSubeProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Business
{
    public class KonuTipBS
    {
        public async Task<List<KonuTipViewModel>> KonuTipListGetir()
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.KonuTip.AsNoTracking()
                                              .Where(p => p.AktifMi)
                                              .OrderBy(p => p.Sira)
                                              .Select(p => new KonuTipViewModel
                                              {
                                                  KonuTipId = p.KonuTipId,
                                                  KonuTipAdi = p.KonuTipAdi
                                              })
                                              .ToListAsync();
            }
        }

        public async Task<KonuTipViewModel> KonuTipDataGetir(int konuTipId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.KonuTip.AsNoTracking()
                                              .Where(p => p.KonuTipId == konuTipId)
                                              .OrderBy(p => p.Sira)
                                              .Select(p => new KonuTipViewModel
                                              {
                                                  KonuTipId = p.KonuTipId,
                                                  KonuTipAdi = p.KonuTipAdi
                                              })
                                              .SingleOrDefaultAsync();
            }
        }
    }
}
