using System;
using System.Data;
using NHibernate;

namespace FoodOrder.DataAccess
{
    public interface IDataAccessLayer : IDisposable
    {
        IRepository<T> GetRepositoryFor<T>() where T : class;
        void Commit();

        ISession GetSession();

        ITransaction BeginTransaction();
        ITransaction BeginTransaction(IsolationLevel level);
    }

    class DataAccessLayer : IDataAccessLayer
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IDbConnection _connection;
        private readonly ISession _session;
        private ITransaction _transaction = null;

        public DataAccessLayer(ISessionFactory sessionFactory)
        {
            _connection = null;
            _sessionFactory = sessionFactory;
            _session = _sessionFactory.OpenSession();
        }

        public DataAccessLayer(ISessionFactory sessionFactory, IDbConnection connection)
        {
            _sessionFactory = sessionFactory;
            _connection = connection;
            _session = _sessionFactory.OpenSession(connection);
            //_transaction = _session.BeginTransaction();
        }

        public ISessionFactory SessionFactory
        {
            get { return _sessionFactory; }
        }

        public virtual void Rollback()
        {
            //_transaction.Rollback();
        }

        public virtual void Commit()
        {
            _session.Flush();
            //_transaction.Commit();
        }

        public ISession GetSession()
        {
            return _session;
        }

        public ITransaction BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
            return _transaction;
        }

        public ITransaction BeginTransaction(IsolationLevel level)
        {
            _transaction = _session.BeginTransaction(level);
            return _transaction;

        }

        public virtual void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();

            if (_session.IsOpen)
                _session.Close();

            _session.Dispose();

            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public IRepository<T> GetRepositoryFor<T>() where T : class
        {
            return new RepositoryImplementation<T>(_session);
        }
    }

}