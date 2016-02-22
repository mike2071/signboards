using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SignBoards.Services;

namespace SignBoards.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISignBoardServices _signBoardServices;

        public HomeController()
        {
            _signBoardServices = new SignBoardServices();
        }

        public ActionResult Index()
        {
            var signsToTake = 4;
            var signBoards = _signBoardServices.TakeSignBoards(signsToTake);
            return View(signBoards);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}