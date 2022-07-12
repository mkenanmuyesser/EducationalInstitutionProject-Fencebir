using FencebirSubeProject.Business;
using FencebirSubeProject.Infra;
using FencebirSubeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FencebirSubeProject.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return Redirect("/");
        }
    }
}
