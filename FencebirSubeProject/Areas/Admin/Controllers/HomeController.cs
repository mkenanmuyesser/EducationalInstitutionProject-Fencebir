using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Business;
using Microsoft.AspNetCore.Mvc;

namespace FencebirSubeProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : BaseController
    {
        private readonly DashboardBS _DashboardBS;
        public HomeController()
        {
            _DashboardBS = new DashboardBS();
        }

        #region Index

        [HttpGet]
        [ActionName("Index")]
        public async Task<IActionResult> IndexGet(int? id)
        {
            int subeId = KullaniciDataGetir().SubeId;
            id = id.HasValue ? id.Value : subeId;

            if (subeId != 1 && id != subeId)
                return Redirect("/Admin");

            var model = await _DashboardBS.DashboardVeriGetir(id.Value);

            return View(model);
        }

        #endregion
    }
}
