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
    public class SubeTipBS
    {
        #region Admin

        public async Task<List<SubeTipSonucViewModel>> SubeTipListGetir()
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.SubeTip.AsNoTracking()
                                              .Where(p => p.AktifMi)
                                              .OrderBy(p => p.Sira)
                                              .Select(p => new SubeTipSonucViewModel
                                              {
                                                  SubeTipId = p.SubeTipId,
                                                  SubeTipAdi = p.SubeTipAdi
                                              })
                                              .ToListAsync();
            }
        }

        #endregion

        #region FrontEnd

        #endregion
    }
}
