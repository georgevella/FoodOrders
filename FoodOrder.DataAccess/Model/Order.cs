using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;

namespace FoodOrder.DataAccess.Model
{
    public class Order
    {
        private IList<OrderItem> _items = new List<OrderItem>();
        
        [Key]
        public virtual int Id { get; set; }

        public virtual DateTime OrderDate { get; set; }

        public virtual IList<OrderItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public virtual Store Store { get; set; }

        public Order()
        {
            
        }

        public virtual OrderItem AddItem(MenuItem menuItem, User cuser)
        {
            var orderItem = new OrderItem()
            {
                Item = menuItem,
                OrderedOn = DateTime.Now,
                OrderedBy = cuser
            };

            cuser.OrderedItems.Add(orderItem);
            Items.Add(orderItem);

            return orderItem;
        }
    }

    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("`tbOrders`");

            Id(x => x.Id).Column("`id`").GeneratedBy.HiLo("300");

            Map(x => x.OrderDate).Column("`orderdate`").Not.Nullable();

            HasMany(x => x.Items).KeyColumn("`orderid`").Not.KeyNullable();
            References(x => x.Store).Column("`storeid`");
        }
    }
}