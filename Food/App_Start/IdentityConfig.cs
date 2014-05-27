using FoodOrder.DataAccess.Model;
using NHibernate;
using NHibernate.IdentityStore;

namespace FoodOrder
{
    public class IdentityConfig
    {
         
    }

    public class ApplicationUserManager : NHibernateUserManager<User>
    {
        public ApplicationUserManager(ISession session) : base(session)
        {

        }
    }
}