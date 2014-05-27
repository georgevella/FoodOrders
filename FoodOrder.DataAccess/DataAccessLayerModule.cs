using NHibernate;
using Ninject.Modules;

namespace FoodOrder.DataAccess
{
    public class DataAccessLayerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataAccessLayerFactory>().To<DataAccessLayerFactory>().InSingletonScope();
            Bind<IDataAccessLayer>().ToProvider<IDataAccessLayerFactory>();
            Bind<ISession>().ToProvider<NHibernateSessionProvider>();

        }
    }
}