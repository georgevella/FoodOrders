using System.Web.Mvc;
using FoodOrder.DataAccess;

namespace FoodOrder.Controllers
{
    public class OrderController : BaseController
    {
        public OrderController(IDataAccessLayer dataAccessLayer) : base(dataAccessLayer)
        {
        }

        public ActionResult New()
        {
            throw new System.NotImplementedException();
        }
    }
}