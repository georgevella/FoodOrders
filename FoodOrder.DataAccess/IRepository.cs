using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;

namespace FoodOrder.DataAccess
{
    public interface IRepository<T> : IQueryable<T>
    {
        T Get(object id);
        T Get(Func<T, bool> predicate);
        IQueryable<T> All();
        IQueryable<T> All(Func<T, bool> predicate);

        T Insert(T entity);
        void Update(T entity);
        void InsertOrUpdate(T entity);
        void Delete(T entity);

        IQueryOver<T,T> Query();
    }

    public class RepositoryImplementation<T> : IRepository<T> where T : class
    {
        private readonly ISession _session;
        private readonly Expression _expression;
        private readonly Type _elementType;
        private readonly IQueryProvider _provider;

        public RepositoryImplementation(ISession session)
        {
            _session = session;

            var queryable = _session.Query<T>();

            
            _expression = queryable.Expression;
            _elementType = queryable.ElementType;
            _provider = queryable.Provider;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _session.Query<T>().GetEnumerator();
        }

        public IQueryOver<T,T> Query()
        {
            return _session.QueryOver<T>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region IQueryable
        Expression IQueryable.Expression { get { return _expression; } }
        Type IQueryable.ElementType { get { return _elementType; } }
        IQueryProvider IQueryable.Provider { get { return _provider; } }
        #endregion

        public T Get(object id)
        {
            return (T)_session.Get<T>(id);
        }

        public T Get(Func<T, bool> predicate)
        {
            return _session.Query<T>().Where(predicate).FirstOrDefault();
        }

        public IQueryable<T> All()
        {
            return _session.Query<T>();
        }

        public IQueryable<T> All(Func<T, bool> predicate)
        {
            return _session.Query<T>().Where(predicate).AsQueryable();
        }

        public T Insert(T entity)
        {
            _session.Save(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _session.Update(entity);
        }

        public void InsertOrUpdate(T entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            _session.Delete(entity);
        }
    }
}