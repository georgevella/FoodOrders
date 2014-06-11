using System;
using System.Configuration;
using System.Data.Common;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.IdentityStore.Models;
using NHibernate.Tool.hbm2ddl;
using Ninject;
using Ninject.Activation;

namespace FoodOrder.DataAccess
{
    public interface IDataAccessLayerFactory : IDisposable, IProvider<IDataAccessLayer>
    {

        IDataAccessLayer Open();
    }

    public class DataAccessLayerFactory : Provider<IDataAccessLayer>, IInitializable, IDataAccessLayerFactory
    {
        
        private ISessionFactory _sessionFactory;

        public DataAccessLayerFactory()
        {

        }

        private static FluentConfiguration InternalBuildConfiguration()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            return Fluently.Configure()
                .Mappings(c => c.FluentMappings.AddFromAssembly(executingAssembly))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<IdentityUser>())
                .ExposeConfiguration(c => c.SetProperty("hbm2ddl.keywords", "auto-quote"));
            //.ExposeConfiguration(c => c.SetProperty("show_sql", "true"));
            //.Diagnostics(x => x.OutputToFile("C:\\nhib.txt").Enable(true));


        }

        public void Dispose()
        {
            _sessionFactory.Close();
            _sessionFactory.Dispose();
        }



        public void Initialize()
        {
            var csSetting = ConfigurationManager.ConnectionStrings["FoodOrderDb"];

            var cfg = InternalBuildConfiguration()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(b => b.FromConnectionStringWithKey("FoodOrderDb")).Dialect<MsSqlAzure2008Dialect>())                
                .BuildConfiguration();

            // check the database
            var provider = DbProviderFactories.GetFactory(csSetting.ProviderName);
            using (var con = provider.CreateConnection())
            {
                con.ConnectionString = csSetting.ConnectionString;
                con.Open();
                var metadata = new DatabaseMetadata(con, new MsSqlAzure2008Dialect());
                try
                {
                    cfg.ValidateSchema(new MsSqlAzure2008Dialect(), metadata);
                }
                catch (HibernateException e)
                {
                    new SchemaExport(cfg).Create(true, true);
                }
            }
           
            _sessionFactory = cfg.BuildSessionFactory();
        }

        public IDataAccessLayer Open()
        {
            return new DataAccessLayer(_sessionFactory);
        }

        protected override IDataAccessLayer CreateInstance(IContext context)
        {
            return Open();
        }
    }
}
