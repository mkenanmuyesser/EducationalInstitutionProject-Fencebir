using FencebirSubeProject.Data;
using FencebirSubeProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Business
{
    public class KurumTipBS
    {
        public async Task<List<KurumTipViewModel>> KurumTipListGetir(int? subeId = null)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.KurumTip.AsNoTracking()
                                               .Where(p => p.AktifMi)
                                               .OrderBy(p => p.Sira)
                                               .Select(p => new KurumTipViewModel
                                               {
                                                   KurumTipId = p.KurumTipId,
                                                   KurumTipAdi = p.KurumTipAdi
                                               })
                                               .ToListAsync();
            }
        }

        public async Task<KurumTipViewModel> KurumTipDataGetir(int kurumTipId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.KurumTip.AsNoTracking()
                                               .Where(p => p.KurumTipId == kurumTipId)
                                               .OrderBy(p => p.Sira)
                                               .Select(p => new KurumTipViewModel
                                               {
                                                   KurumTipId = p.KurumTipId,
                                                   KurumTipAdi = p.KurumTipAdi
                                               })
                                               .SingleOrDefaultAsync();
            }
        }
    }
}
