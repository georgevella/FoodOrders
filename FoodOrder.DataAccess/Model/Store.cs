using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;

namespace FoodOrder.DataAccess.Model
{
    public class Store
    {
        private IList<MenuItem> _menu = new List<MenuItem>();

        [Key]
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<MenuItem> Menu
        {
            get { return _menu; }
            set { _menu = value; }
        }

        public Store()
        {
            
        }
    }

    public class StoreMap : ClassMap<Store>
    {
        public StoreMap()
        {
            Table("`tbStore`");

            Id(x => x.Id).Column("`id`").GeneratedBy.HiLo("100");

            Map(x => x.Name).Column("`name`").Not.Nullable().Unique();

            HasMany(x => x.Menu).KeyColumn("`storeid`").Not.KeyNullable().Cascade.All();
        }
    }
}