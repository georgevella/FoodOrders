using System.Web.Mvc;
using FoodOrder.DataAccess;
using FoodOrder.DataAccess.Model;

namespace FoodOrder.Areas.Data.Controllers
{
    public class BaseDataController : Controller
    {
        private readonly IDataAccessLayer _dataAccessLayer;

        protected BaseDataController(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;

            // make the stores available to all controllers in the viewbag
            ViewBag.Stores = GetRepositoryFor<Store>();
        }

        protected IDataAccessLayer DataAccessLayer()
        {
            return _dataAccessLayer;
        }

        protected IRepository<T> GetRepositoryFor<T>() where T : class
        {
            return _dataAccessLayer.GetRepositoryFor<T>();
        }
    }
}