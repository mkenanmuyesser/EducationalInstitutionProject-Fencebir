using FencebirSubeProject.Areas.Admin.Controllers;
using FencebirSubeProject.Areas.Admin.Models;
using FencebirSubeProject.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;

namespace FencebirSubeProject.Infra
{
    public class AdminActionFilter : Attribute, IActionFilter
    {
        public AdminActionFilter()
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var baseController = context.Controller as BaseController;
            var kullaniciLoginData = context.HttpContext.Session.GetObjectFromJson<KullaniciGirisModel>("KullaniciGirisData");

            if (baseController == null)
            {
                GiriseDon(context);
            }
            else
            {
                if (kullaniciLoginData == null)
                {
                    GiriseDon(context);
                }
                else
                {
                    baseController.ViewBag.KullaniciGirisData = kullaniciLoginData;
                }
            }
        }

        private void GiriseDon(ActionExecutingContext context)
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                area = "Admin",
                controller = "Giris",
                action = "Index",
                ReturnUrl = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetEncodedUrl(context.HttpContext.Request)
            }));
        }
    }
}
