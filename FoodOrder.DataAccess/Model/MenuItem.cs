using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;

namespace FoodOrder.DataAccess.Model
{
    public class MenuItem
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual float Price { get; set; }

        public virtual bool CanHaveExtras { get; set; }

        public virtual Store Store { get; set; }

    }

    public class MenuItemMap : ClassMap<MenuItem>
    {
        public MenuItemMap()
        {
            Table("`tbMenuItems`");

            Id(x => x.Id).Column("`id`").GeneratedBy.HiLo("200");

            Map(x => x.Name).Column("`name`");
            Map(x => x.Price).Column("`price`");
            Map(x => x.CanHaveExtras).Column("`canHaveExtras`");

            References(x => x.Store).Column("`storeid`");
        }
    }
}