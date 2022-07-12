using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Business
{
    public class BlogTipBS
    {
        #region Admin

        public async Task<List<BlogTipSonucViewModel>> BlogTipListGetir()
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.BlogTip.AsNoTracking()
                                              .Where(p => p.AktifMi)
                                              .OrderBy(p => p.Sira)
                                              .Select(p => new BlogTipSonucViewModel
                                              {
                                                  BlogTipId = p.BlogTipId,
                                                  BlogTipAdi = p.BlogTipAdi
                                              })
                                              .ToListAsync();
            }
        }

        #endregion

        #region FrontEnd

        #endregion
    }
}
