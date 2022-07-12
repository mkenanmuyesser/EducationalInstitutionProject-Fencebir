using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Data;
using FencebirSubeProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Business
{
    public class SubeSehirBS
    {
        #region Admin

        public async Task<List<SubeSehirSonucViewModel>> SubeSehirListGetir()
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.SubeSehir.AsNoTracking()
                                                .OrderBy(p => p.SubeSehirId)
                                                .Select(p => new SubeSehirSonucViewModel
                                                {
                                                    SubeSehirId = p.SubeSehirId,
                                                    SubeSehirAdi = p.SubeSehirAdi
                                                })
                                                .ToListAsync();
            }
        }

        #endregion

        #region FrontEnd

        public async Task<List<SehirBilgiViewModel>> SehirBilgiListGetir(int? subeId = null)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.SubeSehir.AsNoTracking()
                                                .OrderBy(p => p.SubeSehirId)
                                                .Select(p => new SehirBilgiViewModel
                                                {
                                                    SehirId = p.SubeSehirId,
                                                    SehirAdi = p.SubeSehirAdi
                                                })
                                                .ToListAsync();
            }
        }

        public async Task<SehirBilgiViewModel> SehirBilgiDataGetir(int subeSehirId)
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.SubeSehir.AsNoTracking()
                                                .Where(p => p.SubeSehirId == subeSehirId)
                                                .Select(p => new SehirBilgiViewModel
                                                {
                                                    SehirId = p.SubeSehirId,
                                                    SehirAdi = p.SubeSehirAdi
                                                })
                                                .SingleOrDefaultAsync();
            }
        }

        #endregion
    }
}
