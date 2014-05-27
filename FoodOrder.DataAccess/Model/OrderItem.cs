using System;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;

namespace FoodOrder.DataAccess.Model
{
    public class OrderItem
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual MenuItem Item { get; set; }

        public virtual DateTime OrderedOn { get; set; }

        public virtual User OrderedBy { get; set; }
    }

    public class OrderedItemMap : ClassMap<OrderItem>
    {
        public OrderedItemMap()
        {
            Table("`tbOrderItems`");

            Id(x => x.Id).Column("`id`").GeneratedBy.HiLo("100");

            Map(x => x.OrderedOn).Column("`orderedOn`").Not.Nullable();
            
            References(x => x.Item).Column("`itemId`").Not.Nullable();
            References(x => x.OrderedBy).Column("`orderedBy`").Not.Nullable();
        }
    }
}