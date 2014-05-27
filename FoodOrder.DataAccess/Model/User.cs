using System;
using System.Collections.Generic;
using NHibernate.IdentityStore.Models;

namespace FoodOrder.DataAccess.Model
{
    public class User : IdentityUser
    {
        private IList<OrderItem> _orderedItems = new List<OrderItem>();
        private IList<Order> _orders = new List<Order>();

        public virtual string DisplayName { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual IList<OrderItem> OrderedItems
        {
            get { return _orderedItems; }
            set { _orderedItems = value; }
        }

        public virtual IList<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        public virtual Order CreateOrderFrom(Store store)
        {
            var order = new Order()
            {
                OrderDate = DateTime.Now,
                Store = store,
            };

            Orders.Add(order);

            return order;
        }
    }

    public class UserMap : IdentityUserMap<User>
    {
        public UserMap()
        {
            Table("`tbUsers`");            

            Map(x => x.DisplayName).Column("`displayname`").Not.Nullable();
            Map(x => x.EmailAddress).Column("`emailAddress`").Not.Nullable().Unique();
            Map(x => x.PhoneNumber).Column("`phone`").Not.Nullable();

            HasMany(x => x.OrderedItems).KeyColumn("`orderedBy`").Cascade.All();
            HasMany(x => x.Orders).KeyColumn("`coordinator`");
        }
    }
}