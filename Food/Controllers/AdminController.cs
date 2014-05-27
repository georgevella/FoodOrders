using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodOrder.ViewModels;

namespace FoodOrder.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View(new AdminConsoleViewModel());
        }

        [HttpPost]
        public JsonResult SaveStore(string name)
        {
            return Json(true);
        }
    }
}