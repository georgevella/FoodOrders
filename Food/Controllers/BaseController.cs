using System.Web.Mvc;
using FoodOrder.DataAccess;

namespace FoodOrder.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        private readonly IDataAccessLayer _dataAccessLayer;

        protected BaseController(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        public IDataAccessLayer Dal()
        {
            return _dataAccessLayer;
        }
    }
}