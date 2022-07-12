using FencebirSubeProject.Business;
using FencebirSubeProject.Controllers;
using FencebirSubeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace FencebirSubeProject.Infra
{
    public class FrontEndActionFilter : Attribute, IActionFilter
    {
        private readonly _BaseBS BaseBS;
        public FrontEndActionFilter()
        {
            BaseBS = new _BaseBS();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            var descriptor = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor;
            int subeId = 0;
            string controller = descriptor.ControllerName;
            if (controller == "Kurumsal")
            {
                subeId = 1;
            }
            else
            {
                if (context.ActionArguments.ContainsKey("id"))
                {
                    string id = context.ActionArguments["id"].ToString();
                    subeId = BaseBS.SubeTemsilciIdGetir(id).Result;
                }
                else
                    subeId = 0;
            }

            var iletisimData = BaseBS.IletisimDataGetir(subeId).Result;
            var subeList = BaseBS.SubeListGetir().Result;
            var temsilciList = BaseBS.TemsilciListGetir().Result;
            var yayinVarmi = BaseBS.YayinVarMi().Result;
            var ogretmenVarmi = BaseBS.OgretmenVarMi(false, subeId).Result;
            var galeriVarmi = BaseBS.GaleriVarMi(false, subeId).Result;
            var blogVarmi = BaseBS.BlogVarMi(false, subeId).Result;

            var baseController = context.Controller as BaseController;

            if (baseController != null)
            {
                baseController.ViewBag.LayoutData = new LayoutViewModel()
                {
                    Logo = iletisimData.Logo,
                    IletisimData = iletisimData,
                    MenuSubeTemsilciList = new MenuViewModel()
                    {
                        SubeList = subeList,
                        TemsilciList = temsilciList,
                        Yayin = yayinVarmi,
                        Ogretmen = ogretmenVarmi,
                        Galeri = galeriVarmi,
                        Blog = blogVarmi
                    }
                };
            }
        }
    }
}
