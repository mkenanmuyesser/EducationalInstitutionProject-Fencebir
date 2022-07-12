using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Enums;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Business
{
    public class DashboardBS
    {
        private readonly SubeBS _SubeBS;

        private readonly EtkinlikBS _EtkinlikBS;
        private readonly YayinBS _YayinBS;
        private readonly BlogBS _BlogBS;
        private readonly MesajBS _MesajBS;
        public DashboardBS()
        {
            _SubeBS = new SubeBS();
            _EtkinlikBS = new EtkinlikBS();
            _YayinBS = new YayinBS();
            _BlogBS = new BlogBS();
            _MesajBS = new MesajBS();
        }

        #region Admin

        public async Task<IndexViewModel> DashboardVeriGetir(int subeId)
        {
            var subeList = await _SubeBS.TumSubeListGetir();
            var bilgiTalepList = await _MesajBS.MesajAramaSonucViewModelGetir(new MesajAramaViewModel() { SubeId = subeId, start = 0, length = 1 }, MesajTipEnum.BilgiTalep);
            var iletisimTalepList = await _MesajBS.MesajAramaSonucViewModelGetir(new MesajAramaViewModel() { SubeId = subeId, start = 0, length = 1 }, MesajTipEnum.IletisimTalep);
            var franchiseTalepList = await _MesajBS.MesajAramaSonucViewModelGetir(new MesajAramaViewModel() { SubeId = subeId, start = 0, length = 1 }, MesajTipEnum.FranchiseTalep);
            var blogList = await _BlogBS.BlogAramaSonucViewModelGetir(new BlogAramaViewModel() { SubeId = subeId, Aktiflik = 1, start = 0, length = 1000000000 });
            var yayinList = await _YayinBS.YayinAramaSonucViewModelGetir(new YayinAramaViewModel() { Aktiflik = 1, start = 0, length = 1 });
            var etkinlikList = await _EtkinlikBS.EtkinlikAramaSonucViewModelGetir(new EtkinlikAramaViewModel() { SubeId = subeId, Aktiflik = 1, start = 0, length = 1 });

            var model = new IndexViewModel()
            {
                SubeId = subeId,
                SubeList = subeList,
                BilgiTalep = bilgiTalepList.Any() ? bilgiTalepList.FirstOrDefault().TotalCount : 0,
                IletisimTalep = iletisimTalepList.Any() ? iletisimTalepList.FirstOrDefault().TotalCount : 0,
                FranchiseTalep = franchiseTalepList.Any() ? franchiseTalepList.FirstOrDefault().TotalCount : 0,
                SubeSayisi = subeList.Count(p => p.SubeTipId == 2),
                TemsilciSayisi = subeList.Count(p => p.SubeTipId == 3),
                BlogSayisi = blogList.Any() ? blogList.FirstOrDefault().TotalCount : 0,
                OkunmaSayisi = blogList.Any() ? blogList.Sum(p => p.OkunmaSayisi) : 0,
                YayinSayisi = yayinList.Any() ? yayinList.FirstOrDefault().TotalCount : 0,
                EtkinlikSayisi = etkinlikList.Any() ? etkinlikList.FirstOrDefault().TotalCount : 0,
            };

            return model;
        }

        #endregion

        #region FrontEnd

        #endregion
    }
}
