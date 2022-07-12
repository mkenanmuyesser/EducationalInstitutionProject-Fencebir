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
    public class GaleriTipBS
    {
        #region Admin

        public async Task<List<GaleriTipSonucViewModel>> GaleriTipListGetir()
        {
            using (var dbContext = new ProjectDBContext())
            {
                return await dbContext.GaleriTip.AsNoTracking()
                                                .Where(p => p.AktifMi)
                                                .OrderBy(p => p.Sira)
                                                .Select(p => new GaleriTipSonucViewModel
                                                {
                                                    GaleriTipId = p.GaleriTipId,
                                                    GaleriTipAdi = p.GaleriTipAdi
                                                })
                                                .ToListAsync();
            }
        }

        #endregion

        #region FrontEnd

        #endregion
    }
}
