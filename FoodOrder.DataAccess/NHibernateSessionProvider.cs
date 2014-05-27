using NHibernate;
using Ninject.Activation;

namespace FoodOrder.DataAccess
{
    public class NHibernateSessionProvider : Provider<ISession>
    {
        private readonly IDataAccessLayer _dataAccessLayer;

        public NHibernateSessionProvider(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        protected override ISession CreateInstance(IContext context)
        {
            return _dataAccessLayer.GetSession();
        }
    }
}