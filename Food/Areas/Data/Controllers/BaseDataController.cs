using System.Web.Mvc;
using FoodOrder.DataAccess;

namespace FoodOrder.Areas.Data.Controllers
{
    public class BaseDataController : Controller
    {
        private readonly IDataAccessLayer _dataAccessLayer;

        public BaseDataController(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        public IDataAccessLayer DataAccessLayer()
        {
            return _dataAccessLayer;
        }

        public IRepository<T> GetRepositoryFor<T>() where T : class
        {
            return _dataAccessLayer.GetRepositoryFor<T>();
        }
    }
}