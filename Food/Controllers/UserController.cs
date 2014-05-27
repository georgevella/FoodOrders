using System.Web.Mvc;

namespace FoodOrder.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}