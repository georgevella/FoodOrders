using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodOrder.DataAccess;
using FoodOrder.ViewModels;

namespace FoodOrder.Controllers
{
    public class HomeController : BaseController
    {        

        public HomeController(IDataAccessLayer dal) : base(dal)
        {

        }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View(new HomeViewModel()
            {
                StoreName = "Mercury", OrderHandler = "George"
            });
        }

        public ActionResult Something()
        {
            throw new NotImplementedException();
        }
    }
}